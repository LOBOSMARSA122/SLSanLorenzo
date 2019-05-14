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

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmEditarProfesional : Form
    {
        private string _mode;
        private string _medicoId;
        HospitalizacionBL oHospitalizacionBL = new HospitalizacionBL();


        #region GetChanges
        string[] nombreCampos =
        {

            "ddlServiceTypeId", "ddlMasterServiceId", "txtClinica", "txtMedico"
        };

        private List<Campos> ListValuesCampo = new List<Campos>();

        private string GetChanges()
        {
            string cadena = oHospitalizacionBL.GetComentaryUpdateByMedicoId(_medicoId);
            string oldComentary = cadena;
            cadena += "<FechaActualiza:" + DateTime.Now.ToString() + "|UsuarioActualiza:" + Globals.ClientSession.v_UserName + "|";
            bool change = false;
            foreach (var item in nombreCampos)
            {
                var fields = this.Controls.Find(item, true);
                string keyTagControl;
                string value1;

                if (fields.Length > 0)
                {
                    keyTagControl = fields[0].GetType().Name;
                    value1 = GetValueControl(keyTagControl, fields[0]);

                    var ValorCampo = ListValuesCampo.Find(x => x.NombreCampo == item).ValorCampo;
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

        private void SetOldValues()
        {
            ListValuesCampo = new List<Campos>();
            string keyTagControl = null;
            string value1 = null;
            foreach (var item in nombreCampos)
            {

                var fields = this.Controls.Find(item, true);

                if (fields.Length > 0)
                {
                    keyTagControl = fields[0].GetType().Name;
                    value1 = GetValueControl(keyTagControl, fields[0]);

                    Campos _Campo = new Campos();
                    _Campo.NombreCampo = item;
                    _Campo.ValorCampo = value1;
                    ListValuesCampo.Add(_Campo);
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
        


        public frmEditarProfesional(string mode, string medicoId)
        {
            _mode = mode;
            _medicoId = medicoId;
            InitializeComponent();
        }

        private void frmEditarProfesional_Load(object sender, EventArgs e)
        {
            LoadComboBox();

            if (_mode == "New")
            {
                
            }
            else
            {
                ddlUsuario.Enabled = false;
                OperationResult objOperationResult1 = new OperationResult();
                var o =oHospitalizacionBL.GetMedico(ref objOperationResult1, _medicoId);

                ddlUsuario.SelectedValue = o.i_SystemUserId.ToString();
                ddlServiceTypeId.SelectedValue = o.i_MasterServiceTypeId.ToString();
                ddlMasterServiceId.SelectedValue = o.i_MasterServiceId.ToString();
                txtClinica.Text = o.r_Clinica.ToString();
                txtMedico.Text = o.r_Medico.ToString();

                SetOldValues();
            }
        }

        //private void LoadData()
        //{
        //    var o = oHospitalizacionBL.GetMedico(ref objOperationResult1, _medicoId);
        //    ddlServiceTypeId.SelectedValue = o.i_MasterServiceTypeId.ToString();
        //    ddlMasterServiceId.SelectedValue = o.i_MasterServiceId.ToString();
        //    txtClinica.Text = o.r_Clinica.ToString();
        //    txtMedico.Text = o.r_Medico.ToString();
        //}
        private void LoadComboBox()
        {
            OperationResult objOperationResult1 = new OperationResult();
            Utils.LoadDropDownList(ddlUsuario, "Value1", "Id", BLL.Utils.GetProfessional(ref objOperationResult1, ""), DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlServiceTypeId, "Value1", "Id", BLL.Utils.GetSystemParameterForComboGrupo(ref objOperationResult1, 119, null), DropDownListAction.Select);

            //Utils.LoadDropDownList(ddlServiceTypeId, "Value1", "Id", BLL.Utils.GetServiceType(ref objOperationResult1, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetMasterService(ref objOperationResult1, -1, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlServiceTypeId, "Value1", "Id", BLL.Utils.GetSystemParameterByParentIdForCombo(ref objOperationResult1, 119, -1, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult1, -1, null), DropDownListAction.Select);


        }

        private void ddlUsuario_SelectedValueChanged(object sender, EventArgs e)
        {
            ProfessionalBL oProfessionalBL = new ProfessionalBL();
            OperationResult objOperationResult = new OperationResult();
            SystemUserList oSystemUserList = new SystemUserList();

            if (ddlUsuario.SelectedValue == null)
                return;

            if (ddlUsuario.SelectedValue.ToString() == "-1")
            {
                lblNombreProfesional.Text = "Nombres y Apellidos del Profesional";
                return;
            }

            oSystemUserList = oProfessionalBL.GetSystemUserName(ref objOperationResult, int.Parse(ddlUsuario.SelectedValue.ToString()));

            lblNombreProfesional.Text = oSystemUserList.v_PersonName;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (uvPacient.Validate(true, false).IsValid)
            {
                OperationResult objOperationResult = new OperationResult();
                medicoDto OmedicoDto = new medicoDto();
                OmedicoDto.i_SystemUserId = int.Parse(ddlUsuario.SelectedValue.ToString());
                OmedicoDto.i_MasterServiceTypeId = int.Parse(ddlServiceTypeId.SelectedValue.ToString());
                OmedicoDto.i_MasterServiceId = int.Parse(ddlMasterServiceId.SelectedValue.ToString());
                OmedicoDto.r_Clinica = decimal.Parse(txtClinica.Text);
                OmedicoDto.r_Medico = decimal.Parse(txtMedico.Text);
                if (_mode == "New")
                {
                    oHospitalizacionBL.AddMedico(ref objOperationResult, OmedicoDto, Globals.ClientSession.GetAsList());
                }
                else
                {
                    OmedicoDto.v_MedicoId = _medicoId;
                    OmedicoDto.v_ComentaryUpdate = GetChanges();
                    oHospitalizacionBL.UpdateMedico(ref objOperationResult, OmedicoDto, Globals.ClientSession.GetAsList());
                }
               
                DialogResult = DialogResult.OK;
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cboTipoServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlServiceTypeId.SelectedValue.ToString() == "-1")
            //{
            //    ddlMasterServiceId.SelectedValue = "-1";
            //    ddlMasterServiceId.Enabled = false;
            //    return;
            //}

            //ddlMasterServiceId.Enabled = true;
            //OperationResult objOperationResult = new OperationResult();
            //Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetMasterService(ref objOperationResult, int.Parse(ddlServiceTypeId.SelectedValue.ToString()), Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.Select);
          
        }

        private void ddlMasterServiceId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ddlServiceTypeId_TextChanged(object sender, EventArgs e)
        {
            if (ddlServiceTypeId.SelectedIndex == 0 || ddlServiceTypeId.SelectedIndex == -1)
                return;

            OperationResult objOperationResult = new OperationResult();
            var id = int.Parse(ddlServiceTypeId.SelectedValue.ToString());
            Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetSystemParameterByParentIdForCombo(ref objOperationResult, 119, id, null), DropDownListAction.Select);

        }
    }
}

