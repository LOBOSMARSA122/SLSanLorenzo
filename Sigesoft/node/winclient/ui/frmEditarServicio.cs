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
using Sigesoft.Node.WinClient.BE.Custom;
using Infragistics.Win.UltraWinGrid;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmEditarServicio : Form
    {
        private string _serviceId;
        private string _PersonId;
        private string _protocolId;
        private ProtocolBL _protocolBL = new ProtocolBL();
        private ServiceBL _ServiceBL = new ServiceBL();
        private protocolDto _protocolDTO = null;
        private serviceDto _serviceDTO = null;
        private string _protocolName;
        string NumberDocument;



        #region GetChanges
        string[] nombreCamposPersona =
        {
            "txtName", "ddlDocTypeId", "txtFirstLastName", "txtDocNumber", "txtSecondLastName", "ddlSexTypeId", "ddlMaritalStatusId",
            "txtMail", "txtTelephoneNumber", "ddlLevelOfId"
        };

        string[] nombreCamposProtocolo =
        {
            "txtProtocolName", "cbGeso", "cbOrganizationInvoice", "cbServiceType", "cbOrganization", "cbService", "cbIntermediaryOrganization",
            "txtCentroCosto", "cbEsoType"
        };

        private List<Campos> ListValuesCampoPersona = new List<Campos>();
        private List<Campos> ListValuesCampoProtocolo = new List<Campos>();
        private string GetChangesPerson()
        {
            string cadena = new PacientBL().GetComentaryUpdateByPersonId(_PersonId);
            string oldComentary = cadena;
            cadena += "<FechaActualiza:" + DateTime.Now.ToString() + "|UsuarioActualiza:" + Globals.ClientSession.v_UserName + "|";
            bool change = false;
            foreach (var item in nombreCamposPersona)
            {
                var fields = this.Controls.Find(item, true);
                string keyTagControl;
                string value1;

                if (fields.Length > 0)
                {
                    keyTagControl = fields[0].GetType().Name;
                    value1 = GetValueControl(keyTagControl, fields[0]);

                    var ValorCampo = ListValuesCampoPersona.Find(x => x.NombreCampo == item).ValorCampo;
                    if (ValorCampo != value1)
                    {
                        cadena += item + ":" + ValorCampo + "|";
                        change = true;
                    }
                }
            }
            if (change)
            {
                return cadena;
            }

            return oldComentary;
        }
        private string GetChangesProtocol()
        {
            string cadena = _protocolBL.GetComentaryUpdateByProtocolId(_protocolId);
            string oldComentary = cadena;
            cadena += "<FechaActualiza:" + DateTime.Now.ToString() + "|UsuarioActualiza:" + Globals.ClientSession.v_UserName + "|";
            bool change = false;
            foreach (var item in nombreCamposProtocolo)
            {
                var fields = this.Controls.Find(item, true);
                string keyTagControl;
                string value1;

                if (fields.Length > 0)
                {
                    keyTagControl = fields[0].GetType().Name;
                    value1 = GetValueControl(keyTagControl, fields[0]);

                    var ValorCampo = ListValuesCampoProtocolo.Find(x => x.NombreCampo == item).ValorCampo;
                    if (ValorCampo != value1)
                    {
                        cadena += item + ":" + ValorCampo + "|";
                        change = true;
                    }
                }
            }
            if (change)
            {
                return cadena;
            }

            return oldComentary;
        }

        private void SetOldValuesPerson()
        {
            ListValuesCampoPersona = new List<Campos>();
            string keyTagControl = null;
            string value1 = null;
            foreach (var item in nombreCamposPersona)
            {

                var fields = this.Controls.Find(item, true);

                if (fields.Length > 0)
                {
                    keyTagControl = fields[0].GetType().Name;
                    value1 = GetValueControl(keyTagControl, fields[0]);

                    Campos _Campo = new Campos();
                    _Campo.NombreCampo = item;
                    _Campo.ValorCampo = value1;
                    ListValuesCampoPersona.Add(_Campo);
                }
            }
        }

        private void SetOldValuesProtocol()
        {
            ListValuesCampoProtocolo = new List<Campos>();
            string keyTagControl = null;
            string value1 = null;
            foreach (var item in nombreCamposProtocolo)
            {
                var fields = this.Controls.Find(item, true);

                if (fields.Length > 0)
                {
                    keyTagControl = fields[0].GetType().Name;
                    value1 = GetValueControl(keyTagControl, fields[0]);
                    Campos _Campo = new Campos();
                    _Campo.NombreCampo = item;
                    _Campo.ValorCampo = value1;
                    ListValuesCampoProtocolo.Add(_Campo);
                }
            }
        }

        private string GetValueControl(string ControlId, Control ctrl)
        {
            string value1 = null;

            switch (ControlId)
            {
                case "TextBox":
                    value1 = ((TextBox)ctrl).Text;
                    break;
                case "ComboBox":
                    value1 = ((ComboBox)ctrl).Text;
                    break;
                case "CheckBox":
                    value1 = Convert.ToInt32(((CheckBox)ctrl).Checked).ToString();
                    break;
                case "RadioButton":
                    value1 = Convert.ToInt32(((RadioButton)ctrl).Checked).ToString();
                    break;
                case "DateTimePicker":
                    value1 = ((DateTimePicker)ctrl).Text; ;
                    break;
                case "UltraCombo":
                    value1 = ((UltraCombo)ctrl).Text; ;
                    break;
                default:
                    break;
            }

            return value1;
        }

        #endregion
        

        public frmEditarServicio(string pServiceId, string pProtocolId, string pPersonId)
        {
            _protocolId = pProtocolId;
            _serviceId = pServiceId;
            _PersonId = pPersonId;
            InitializeComponent();

            OperationResult objOperationResult = new OperationResult();
            //var dataListPc = _protocolBL.GetProtocolComponents(ref objOperationResult, _protocolId);
            //var dataListPc = _ServiceBL.GetServiceComponentsLiquidacion(ref objOperationResult, _serviceId);

            //grdDataLocation.DataSource = dataListPc;
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
            //SERVICIO - CENTRO DE COSTO
            var servicio = new ServiceBL().GetService(ref objOperationResult, _serviceId);
            txtCentroCosto.Text = servicio.v_centrocosto;
            // Componentes del protocolo
            var dataListPc = _ServiceBL.GetServiceComponentsLiquidacion(ref objOperationResult, _serviceId);
            float Total = 0;
            foreach (var item in dataListPc)
            {
                Total = Total + item.r_Price.Value;
            }
            lblCostoTotal.Text = Total.ToString();

            grdDataLocation.DataSource = dataListPc;

            SetOldValuesPerson();
            SetOldValuesProtocol();
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
            NumberDocument = objpacientDto.v_DocNumber;
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
                    objpersonDto.v_ComentaryUpdate = GetChangesPerson();
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
                //if (_serviceDTO == null)
                //{
                //    _serviceDTO = new serviceDto();
                //}
                _serviceDTO = new ServiceBL().GetService(ref objOperationResult, _serviceId);

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
                _protocolDTO.v_ComentaryUpdate = GetChangesProtocol();
                _protocolBL.UpdateProtocol(ref objOperationResult,
                        _protocolDTO,
                        _protocolcomponentListDTO,
                        _protocolcomponentListDTO,
                        _protocolcomponentListDTO,
                        Globals.ClientSession.GetAsList());

                _serviceDTO.v_centrocosto = txtCentroCosto.Text;
                _serviceDTO.v_ServiceId = _serviceId;
                //_ServiceBL.UpdateService(ref objOperationResult, _serviceDTO, Globals.ClientSession.GetAsList());
                _ServiceBL.UpdateService(ref objOperationResult, _serviceDTO, Globals.ClientSession.GetAsList());

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

        private void grdProtocolComponent_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void grdDataLocation_DoubleClick(object sender, EventArgs e)
        {
        }
            

        private void grdDataLocation_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void grdDataLocation_DoubleClick_1(object sender, EventArgs e)
        {
             OperationResult objOperationResult = new OperationResult();
             var ServiceComponentId = grdDataLocation.Selected.Rows[0].Cells["v_ServiceComponentId"].Value.ToString();
            var component = grdDataLocation.Selected.Rows[0].Cells["v_ComponentName"].Value.ToString();
            var componentId = grdDataLocation.Selected.Rows[0].Cells["v_ComponentId"].Value.ToString();
            var price = grdDataLocation.Selected.Rows[0].Cells["r_Price"].Value.ToString();

            FormPrecioComponente frm = new FormPrecioComponente(component, price, "Change");
            frm.ShowDialog();

            var oservicecomponentDto = new servicecomponentDto();

          var obj =  _ServiceBL.GetServiceComponentsLiquidacion(ref objOperationResult, _serviceId);

          //var x = obj.Find(p => p.v_ComponentId == componentId);
          oservicecomponentDto.v_ServiceComponentId = ServiceComponentId;
        oservicecomponentDto.r_Price = frm.Precio;

        _ServiceBL.ActualizarPrecioComponente(oservicecomponentDto.r_Price.Value, oservicecomponentDto.v_ServiceComponentId);

            var dataListPc = _ServiceBL.GetServiceComponentsLiquidacion(ref objOperationResult, _serviceId);

            grdDataLocation.DataSource = dataListPc;

            float Total = 0;
            foreach (var item in dataListPc)
            {
                Total = Total + item.r_Price.Value;
            }
            lblCostoTotal.Text = Total.ToString();
        } 
    }
}
