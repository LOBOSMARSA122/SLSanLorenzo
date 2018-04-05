using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.BE;


namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class frmSelectSignature : Form
    {
        public int? i_SystemUserSuplantadorId { get; set; }

        public frmSelectSignature()
        {
            InitializeComponent();
        }

        private void frmSelectSignature_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlUsuario, "Value1", "Id", BLL.Utils.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (uvSelectSignature.Validate(true, false).IsValid)
            {
                i_SystemUserSuplantadorId = Convert.ToInt32(ddlUsuario.SelectedValue);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          
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
    }
}
