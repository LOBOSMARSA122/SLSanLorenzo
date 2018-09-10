using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmEditarServicio : Form
    {
        private string _serviceId;
        private string _PersonId;
        private string _protocolId;
        private ProtocolBL _protocolBL = new ProtocolBL();
        private protocolDto _protocolDTO = null;
        private string _protocolName;
        string NumberDocument;

        public frmEditarServicio(string pServiceId, string pProtocolId, string pPersonId)
        {
            _protocolId = pProtocolId;
            _serviceId = pServiceId;
            _PersonId = pPersonId;
            InitializeComponent();

            OperationResult objOperationResult = new OperationResult();
            var dataListPc = _protocolBL.GetProtocolComponents(ref objOperationResult, _protocolId);

            grdProtocolComponent.DataSource = dataListPc;             
        }

        private void frmEditarServicio_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            LoadComboBox();
            LoadPerson();
            _protocolDTO = _protocolBL.GetProtocol(ref objOperationResult, _protocolId);
            string idOrgInter = "-1";

            // cabecera del protocolo
            txtProtocolName.Text = _protocolDTO.v_Name;
            cbEsoType.SelectedValue = _protocolDTO.i_EsoTypeId.ToString();
            // Almacenar temporalmente
            _protocolName = txtProtocolName.Text;

            if (_protocolDTO.v_WorkingOrganizationId != "-1" && _protocolDTO.v_WorkingLocationId != "-1")
            {
                idOrgInter = string.Format("{0}|{1}", _protocolDTO.v_WorkingOrganizationId, _protocolDTO.v_WorkingLocationId);
            }

            cbIntermediaryOrganization.SelectedValue = idOrgInter;
            cbOrganizationInvoice.SelectedValue = string.Format("{0}|{1}", _protocolDTO.v_CustomerOrganizationId, _protocolDTO.v_CustomerLocationId);
            cbOrganization.SelectedValue = string.Format("{0}|{1}", _protocolDTO.v_EmployerOrganizationId, _protocolDTO.v_EmployerLocationId);
            cbGeso.SelectedValue = _protocolDTO.v_GroupOccupationId;
            cbServiceType.SelectedValue = _protocolDTO.i_MasterServiceTypeId.ToString();
            cbService.SelectedValue = _protocolDTO.i_MasterServiceId.ToString();

            // Componentes del protocolo
            var dataListPc = _protocolBL.GetProtocolComponents(ref objOperationResult, _protocolId);
            float Total = 0;
            foreach (var item in dataListPc)
            {
                Total = Total + item.r_Price.Value;
            }
            lblCostoTotal.Text = Total.ToString();

            grdProtocolComponent.DataSource = dataListPc;
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPerson()
        {
            PacientList objpacientDto = new PacientList();
            PacientBL _objPacientBL = new PacientBL();
            OperationResult objOperationResult = new OperationResult();

            objpacientDto = _objPacientBL.GetPacient(ref objOperationResult, _PersonId, null);
            NumberDocument = txtDocNumber.Text;
            txtName.Text = objpacientDto.v_FirstName;
            txtFirstLastName.Text = objpacientDto.v_FirstLastName;
            txtSecondLastName.Text = objpacientDto.v_SecondLastName;
            ddlDocTypeId.SelectedValue = objpacientDto.i_DocTypeId.ToString();
            ddlSexTypeId.SelectedValue = objpacientDto.i_SexTypeId.ToString();
            ddlMaritalStatusId.SelectedValue = objpacientDto.i_MaritalStatusId.ToString();
            ddlLevelOfId.SelectedValue = objpacientDto.i_LevelOfId.ToString();
            txtDocNumber.Text = objpacientDto.v_DocNumber;
            txtTelephoneNumber.Text = objpacientDto.v_TelephoneNumber;
            txtMail.Text = objpacientDto.v_Mail;
        }

        private void LoadComboBox()
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(cbGeso, "Value1", "Id", BLL.Utils.GetGESO(ref objOperationResult, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbEsoType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 118, null), DropDownListAction.Select);
            
            // Lista de empresas por nodo
            int nodeId = int.Parse(Common.Utils.GetApplicationConfigValue("NodeId"));
            OperationResult objOperationResult1 = new OperationResult();
            var dataListOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);
            var dataListOrganization1 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);
            var dataListOrganization2 = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult1, nodeId);


            Utils.LoadDropDownList(cbOrganization,
                "Value1",
                "Id",
                dataListOrganization,
                DropDownListAction.Select);

            Utils.LoadDropDownList(cbIntermediaryOrganization,
               "Value1",
               "Id",
               dataListOrganization1,
               DropDownListAction.Select);

            Utils.LoadDropDownList(cbOrganizationInvoice,
              "Value1",
              "Id",
              dataListOrganization2,
              DropDownListAction.Select);

            Utils.LoadDropDownList(cbServiceType, "Value1", "Id", BLL.Utils.GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, -1, null), DropDownListAction.Select);
            // combo servicio
            Utils.LoadDropDownList(cbService, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, -1, null), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlMaritalStatusId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 101, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDocTypeId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 106, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSexTypeId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 100, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLevelOfId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 108, null), DropDownListAction.Select);
            
        }

         PacientBL _objPacientBL = new PacientBL();
          OperationResult objOperationResult = new OperationResult();
        private void btnSavePacient_Click(object sender, EventArgs e)
        {
            // Populate the entity

                   var objpersonDto = new personDto();

                    objpersonDto = _objPacientBL.GetPerson(ref objOperationResult, _PersonId);
                    objpersonDto.v_PersonId = _PersonId;
                    objpersonDto.v_FirstName = txtName.Text.Trim();
                    objpersonDto.v_FirstLastName = txtFirstLastName.Text.Trim();
                    objpersonDto.v_SecondLastName = txtSecondLastName.Text.Trim();
                    objpersonDto.i_DocTypeId = Convert.ToInt32(ddlDocTypeId.SelectedValue);
                    objpersonDto.i_SexTypeId = Convert.ToInt32(ddlSexTypeId.SelectedValue);
                    objpersonDto.i_MaritalStatusId = Convert.ToInt32(ddlMaritalStatusId.SelectedValue);
                    objpersonDto.i_LevelOfId = Convert.ToInt32(ddlLevelOfId.SelectedValue);
                    objpersonDto.v_DocNumber = txtDocNumber.Text.Trim();
               
                    objpersonDto.v_TelephoneNumber = txtTelephoneNumber.Text.Trim();
                  
                    objpersonDto.v_Mail = txtMail.Text.Trim();
                    string Result = "";
                    // Save the data
                    Result = _objPacientBL.UpdatePacient(ref objOperationResult, objpersonDto, Globals.ClientSession.GetAsList(), NumberDocument, txtDocNumber.Text);

                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    MessageBox.Show("Se grabó correctamente.", "! INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else  // Operación con error
                {
                    if (Result == "-1")
                    {
                        MessageBox.Show("El Número de documento ya existe.", "! ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(Constants.GenericErrorMessage, "! ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Se queda en el formulario.
                    }
                }
        }

        private void cbOrganizationInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbOrganizationInvoice.SelectedValue == "-1") return;
            if (cbOrganizationInvoice.SelectedValue != null)
            {
                var id1 = cbOrganizationInvoice.SelectedValue.ToString();

                cbOrganization.SelectedValue = id1;
                cbIntermediaryOrganization.SelectedValue = id1;
            }
        }

        private void LoadcbGESO()
        {
            var index = cbOrganization.SelectedIndex;

            if (index == 0 || index == -1)
            {
                OperationResult objOperationResult = new OperationResult();
                Utils.LoadDropDownList(cbGeso, "Value1", "Id", BLL.Utils.GetGESO(ref objOperationResult, null), DropDownListAction.Select);
                return;
            }

            var dataList = cbOrganization.SelectedValue.ToString().Split('|');
            string idOrg = dataList[0];
            string idLoc = dataList[1];

            OperationResult objOperationResult1 = new OperationResult();
            Utils.LoadDropDownList(cbGeso, "Value1", "Id", BLL.Utils.GetGESOByOrgIdAndLocationId(ref objOperationResult1, idOrg, idLoc), DropDownListAction.Select);
        }

        private void cbOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadcbGESO();
        }

        private void cbIntermediaryOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbServiceType_TextChanged(object sender, EventArgs e)
        {
            if (cbServiceType.SelectedIndex == 0 || cbServiceType.SelectedIndex == -1)
                return;

            OperationResult objOperationResult = new OperationResult();
            var id = int.Parse(cbServiceType.SelectedValue.ToString());
            Utils.LoadDropDownList(cbService, "Value1", "Id", BLL.Utils.GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, id, null), DropDownListAction.Select);

        }

        private List<protocolcomponentDto> _protocolcomponentListDTO = null;
        private void button1_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            _protocolcomponentListDTO = new List<protocolcomponentDto>();
            if (uvProtocol.Validate(true, false).IsValid)
            {

                var id = cbOrganization.SelectedValue.ToString().Split('|');
                var id1 = cbOrganizationInvoice.SelectedValue.ToString().Split('|');
                var id2 = cbIntermediaryOrganization.SelectedValue.ToString().Split('|');

                if (_protocolDTO == null)
                {
                    _protocolDTO = new protocolDto();
                }

                _protocolDTO.v_Name = txtProtocolName.Text;
                _protocolDTO.v_EmployerOrganizationId = id[0];
                _protocolDTO.v_EmployerLocationId = id[1];
                _protocolDTO.i_EsoTypeId = int.Parse(cbEsoType.SelectedValue.ToString());
                _protocolDTO.v_GroupOccupationId = cbGeso.SelectedValue.ToString();
                _protocolDTO.v_CustomerOrganizationId = id1[0];
                _protocolDTO.v_CustomerLocationId = id1[1];
                _protocolDTO.v_WorkingOrganizationId = id2[0];
                _protocolDTO.v_WorkingLocationId = cbIntermediaryOrganization.SelectedValue.ToString() != "-1" ? id2[1] : "-1";
                _protocolDTO.i_MasterServiceId = int.Parse(cbService.SelectedValue.ToString());
                _protocolDTO.i_MasterServiceTypeId = int.Parse(cbServiceType.SelectedValue.ToString());
                _protocolDTO.v_ProtocolId = _protocolId;
                _protocolBL.UpdateProtocol(ref objOperationResult,
                        _protocolDTO,
                        _protocolcomponentListDTO,
                        _protocolcomponentListDTO,
                        _protocolcomponentListDTO,
                        Globals.ClientSession.GetAsList());

                }

                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    MessageBox.Show("Se grabo correctamente.", "INFORMACION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else  // Operación con error
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }


    }
}
