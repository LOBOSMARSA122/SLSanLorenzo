using Infragistics.Win;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
  
    public partial class frmPopupAnamnesis : Form
    {
        private string _serviceId ;
        private int _AptitupESOId;
        private int? _generoId;
        private ServiceBL _serviceBL = new ServiceBL();
        public frmPopupAnamnesis(string pstrServiceId, int pintAptitudESOId, int? pintGeneroId)
        {
            _serviceId =pstrServiceId;
            _AptitupESOId = pintAptitudESOId;
            _generoId = pintGeneroId;
            InitializeComponent();
        }

        private void btnGuardarAnamnesis_Click(object sender, EventArgs e)
        {
            if (uvAnamnesis.Validate(true, false).IsValid)
            {
                DialogResult Result = MessageBox.Show("¿Está seguro de grabar este registro?:", "CONFIRMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (Result == DialogResult.Yes)
                {

                    OperationResult objOperationResult = new OperationResult();

                    serviceDto serviceDTO = new serviceDto();

                    serviceDTO.v_ServiceId = _serviceId;
                    serviceDTO.v_MainSymptom = chkPresentaSisntomas.Checked ? txtSintomaPrincipal.Text : null;
                    serviceDTO.i_TimeOfDisease = chkPresentaSisntomas.Checked ? int.Parse(txtValorTiempoEnfermedad.Text) : (int?)null;
                    serviceDTO.i_TimeOfDiseaseTypeId = chkPresentaSisntomas.Checked ? int.Parse(cbCalendario.SelectedValue.ToString()) : -1;
                    serviceDTO.v_Story = txtRelato.Text;
                    serviceDTO.i_DreamId = -1;
                    serviceDTO.i_UrineId = -1;
                    serviceDTO.i_DepositionId = -1;
                    serviceDTO.v_Findings = "";
                    serviceDTO.i_AppetiteId =-1;
                    serviceDTO.i_ThirstId = -1;
                    serviceDTO.d_Fur = dtpFur.Checked ? dtpFur.Value : (DateTime?)null;
                    serviceDTO.v_CatemenialRegime = txtRegimenCatamenial.Text;
                    serviceDTO.i_MacId = int.Parse(cbMac.SelectedValue.ToString());
                    serviceDTO.i_HasSymptomId = Convert.ToInt32(chkPresentaSisntomas.Checked);

                    serviceDTO.d_PAP = dtpPAP.Checked ? dtpPAP.Value : (DateTime?)null;
                    serviceDTO.d_Mamografia = dtpMamografia.Checked ? dtpMamografia.Value : (DateTime?)null;
                    serviceDTO.v_Gestapara = txtGestapara.Text;
                    serviceDTO.v_Menarquia = txtMenarquia.Text;
                    serviceDTO.v_CiruGine = txtCiruGine.Text;
                    serviceDTO.v_Findings ="";

                    serviceDTO.v_FechaUltimoPAP = txtFechaUltimoPAP.Text;
                    serviceDTO.v_ResultadosPAP = txtResultadoPAP.Text;
                    serviceDTO.v_FechaUltimaMamo = txtFechaUltimaMamo.Text;
                    serviceDTO.v_ResultadoMamo = txtResultadoPAP.Text;

                    serviceDTO.v_InicioVidaSexaul = txtVidaSexual.Text;
                    serviceDTO.v_NroParejasActuales = txtNroParejasActuales.Text;
                    serviceDTO.v_NroAbortos = txtNroAbortos.Text;
                    serviceDTO.v_PrecisarCausas = txtNroCausa.Text;

                    // datos de cabecera del Servicio
                    serviceDTO.i_AptitudeStatusId = _AptitupESOId;

                    // Actualizar
                    _serviceBL.UpdateAnamnesis(ref objOperationResult, serviceDTO, Globals.ClientSession.GetAsList());

                    // Analizar el resultado de la operación
                    if (objOperationResult.Success != 1)
                    {
                        MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Se grabó correctamente", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }

            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmPopupAnamnesis_Load(object sender, EventArgs e)
        {



            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(cbCalendario, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 133, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbMac, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 134, null), DropDownListAction.Select);



            ServiceList personData = _serviceBL.GetServicePersonData(ref objOperationResult, _serviceId);

            // cargar datos INICIALES de ANAMNESIS
            chkPresentaSisntomas.Checked = Convert.ToBoolean(personData.i_HasSymptomId);
            // Activar / Desactivar segun check presenta sintomas
            txtSintomaPrincipal.Enabled = chkPresentaSisntomas.Checked;
            txtValorTiempoEnfermedad.Enabled = chkPresentaSisntomas.Checked;
            cbCalendario.Enabled = chkPresentaSisntomas.Checked;

            txtSintomaPrincipal.Text = string.IsNullOrEmpty(personData.v_MainSymptom) ? "No Refiere" : personData.v_MainSymptom;
            txtValorTiempoEnfermedad.Text = personData.i_TimeOfDisease == null ? string.Empty : personData.i_TimeOfDisease.ToString();
            cbCalendario.SelectedValue = personData.i_TimeOfDiseaseTypeId == null ? "1" : personData.i_TimeOfDiseaseTypeId.ToString();
            txtRelato.Text = string.IsNullOrEmpty(personData.v_Story) ? "Paciente Asintomático" : personData.v_Story;


            txtFechaUltimoPAP.Text = string.IsNullOrEmpty(personData.v_FechaUltimoPAP) ? "" : personData.v_FechaUltimoPAP;
            txtFechaUltimaMamo.Text = string.IsNullOrEmpty(personData.v_FechaUltimaMamo) ? "" : personData.v_FechaUltimaMamo;
            txtResultadoPAP.Text = string.IsNullOrEmpty(personData.v_ResultadosPAP) ? "" : personData.v_ResultadosPAP;
            txtResultadoMamo.Text = string.IsNullOrEmpty(personData.v_ResultadoMamo) ? "" : personData.v_ResultadoMamo;

            txtVidaSexual.Text = string.IsNullOrEmpty(personData.v_InicioVidaSexaul) ? "" : personData.v_InicioVidaSexaul;
            txtNroParejasActuales.Text = string.IsNullOrEmpty(personData.v_NroParejasActuales) ? "" : personData.v_NroParejasActuales;
            txtNroAbortos.Text = string.IsNullOrEmpty(personData.v_NroAbortos) ? "" : personData.v_NroAbortos;
            txtNroCausa.Text = string.IsNullOrEmpty(personData.v_PrecisarCausas) ? "" : personData.v_PrecisarCausas;

            if (personData.d_Fur != null)
            {
                dtpFur.Checked = true;
                dtpFur.Value = personData.d_Fur.Value.Date;
            }
            txtRegimenCatamenial.Text = personData.v_CatemenialRegime;
            cbMac.SelectedValue = personData.i_MacId == null ? "1" : personData.i_MacId.ToString();


            //-----------------------------------------------------------------
            if (personData.d_PAP != null)
            {
                dtpPAP.Value = personData.d_PAP.Value;
                dtpPAP.Checked = true;
            }


            if (personData.d_Mamografia != null)
            {
                dtpMamografia.Value = personData.d_Mamografia.Value;
                dtpMamografia.Checked = true;
            }


            txtGestapara.Text = string.IsNullOrEmpty(personData.v_Gestapara) ? "G ( )  P ( ) ( ) ( ) ( ) " : personData.v_Gestapara;
            txtMenarquia.Text = personData.v_Menarquia;
            txtCiruGine.Text = personData.v_CiruGine;



          Gender _sexType; _sexType = (Gender)_generoId;
            switch (_sexType)
            {
                case Gender.MASCULINO:
                    gbAntGinecologicos.Enabled = false;
                    dtpFur.Enabled = false;
                    txtRegimenCatamenial.Enabled = false;
                    break;
                case Gender.FEMENINO:
                    gbAntGinecologicos.Enabled = true;
                    dtpFur.Enabled = true;
                    txtRegimenCatamenial.Enabled = true;
                    break;
                default:
                    break;
            }













        }

        private void chkPresentaSisntomas_CheckedChanged(object sender, EventArgs e)
        {
            txtSintomaPrincipal.Enabled = chkPresentaSisntomas.Checked;
            txtValorTiempoEnfermedad.Enabled = chkPresentaSisntomas.Checked;
            cbCalendario.Enabled = chkPresentaSisntomas.Checked;

            if (chkPresentaSisntomas.Checked)
            {
                uvAnamnesis.GetValidationSettings(txtSintomaPrincipal).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", true, typeof(string));
                uvAnamnesis.GetValidationSettings(txtSintomaPrincipal).IsRequired = true;
                uvAnamnesis.GetValidationSettings(txtValorTiempoEnfermedad).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", true, typeof(string));
                uvAnamnesis.GetValidationSettings(txtValorTiempoEnfermedad).IsRequired = true;
                uvAnamnesis.GetValidationSettings(cbCalendario).Condition = new OperatorCondition(ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
                uvAnamnesis.GetValidationSettings(cbCalendario).IsRequired = true;
            }
            else
            {
                uvAnamnesis.GetValidationSettings(txtSintomaPrincipal).IsRequired = false;
                uvAnamnesis.GetValidationSettings(txtValorTiempoEnfermedad).IsRequired = false;
                uvAnamnesis.GetValidationSettings(cbCalendario).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvAnamnesis.GetValidationSettings(cbCalendario).IsRequired = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}
