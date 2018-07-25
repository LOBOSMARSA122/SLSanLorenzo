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

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmEditarProfesional : Form
    {
        private string _mode;
        private string _medicoId;
        HospitalizacionBL oHospitalizacionBL = new HospitalizacionBL();
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
                OperationResult objOperationResult1 = new OperationResult();
                var o =oHospitalizacionBL.GetMedico(ref objOperationResult1, _medicoId);

                ddlUsuario.SelectedValue = o.i_SystemUserId.ToString();
                cboGrupo.SelectedValue = o.i_GrupoId.ToString();
                txtClinica.Text = o.r_Clinica.ToString();
                txtMedico.Text = o.r_Medico.ToString();
            }
        }

        private void LoadComboBox()
        {
            OperationResult objOperationResult1 = new OperationResult();
            Utils.LoadDropDownList(ddlUsuario, "Value1", "Id", BLL.Utils.GetProfessional(ref objOperationResult1, ""), DropDownListAction.Select);
            Utils.LoadDropDownList(cboGrupo, "Value1", "Id", BLL.Utils.GetSystemParameterForComboGrupo(ref objOperationResult1, 119, null), DropDownListAction.Select);

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
                OmedicoDto.i_GrupoId = int.Parse(cboGrupo.SelectedValue.ToString());
                OmedicoDto.r_Clinica = decimal.Parse(txtClinica.Text);
                OmedicoDto.r_Medico = decimal.Parse(txtMedico.Text);
                if (_mode == "New")
                {
                    oHospitalizacionBL.AddMedico(ref objOperationResult, OmedicoDto, Globals.ClientSession.GetAsList());
                }
                else
                {
                    OmedicoDto.v_MedicoId = _medicoId;
                    oHospitalizacionBL.UpdateMedico(ref objOperationResult, OmedicoDto, Globals.ClientSession.GetAsList());
                }
               
                DialogResult = DialogResult.OK;
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

