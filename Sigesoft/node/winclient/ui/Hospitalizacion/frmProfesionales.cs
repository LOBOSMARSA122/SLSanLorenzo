using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmProfesionales : Form
    {
        HospitalizacionBL oHospitalizacionBL = new HospitalizacionBL();
        private string _medicoId;
        public frmProfesionales()
        {
            InitializeComponent();
        }

        private void frmProfesionales_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult1 = new OperationResult();
            Utils.LoadDropDownList(ddlUsuario, "Value1", "Id", BLL.Utils.GetProfessional(ref objOperationResult1, ""), DropDownListAction.Select);
           
            BindGrid("");
        }

        private void BindGrid(string strFilterExpression)
        {
            var data = oHospitalizacionBL.GetMedicosGrid(strFilterExpression);
            grdData.DataSource = data;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var frm = new frmEditarProfesional("New", "");
            frm.ShowDialog();
            BindGrid("");
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (grdData.Selected.Rows.Count > 0)
            {
                _medicoId = grdData.Selected.Rows[0].Cells["v_MedicoId"].Value.ToString();
                var frm = new frmEditarProfesional("Edit", _medicoId);
                frm.ShowDialog();

                BindGrid("");
            }
            else
            {
                MessageBox.Show("Seleccione una fila.", "VALIDACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult1 = new OperationResult();
            _medicoId = grdData.Selected.Rows[0].Cells["v_MedicoId"].Value.ToString();
            oHospitalizacionBL.DeleteMedico(ref objOperationResult1, _medicoId, Globals.ClientSession.GetAsList());

            BindGrid("");

            MessageBox.Show("Se eliminó correctamente", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }

        private void grdData_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            foreach (UltraGridRow rowSelected in this.grdData.Selected.Rows)
            {
                btnEditar.Enabled = 
                btnEliminar.Enabled =

                (grdData.Selected.Rows.Count > 0);

                if (grdData.Selected.Rows.Count == 0)
                    return;

                _medicoId = grdData.Selected.Rows[0].Cells["v_MedicoId"].Value.ToString();
            }
        }

        string strFilterExpression;
        private void btnFilter_Click(object sender, EventArgs e)
        {
            List<string> Filters = new List<string>();
            if (ddlUsuario.SelectedValue.ToString() != "-1")
                Filters.Add("i_SystemUserId ==" + ddlUsuario.SelectedValue);

            strFilterExpression = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    strFilterExpression = strFilterExpression + item + " && ";
                }
                strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
            }

            BindGrid(strFilterExpression);
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

        private void verCambiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _medicoId = grdData.Selected.Rows[0].Cells["v_MedicoId"].Value.ToString();
            string commentary = oHospitalizacionBL.GetComentaryUpdateByMedicoId(_medicoId);
            if (commentary == "" || commentary == null)
            {
                MessageBox.Show("Aún no se han realizado cambios.", "AVISO", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            var frm = new frmViewChanges(commentary);
            frm.ShowDialog();
        }

        private void grdData_MouseDown(object sender, MouseEventArgs e)
        {
            if (grdData.Rows == null)
            {

                cmProfesionales.Items["verCambiosToolStripMenuItem"].Enabled = false;
            }
            else if (grdData.Rows.Count == 0)
            {
                cmProfesionales.Items["verCambiosToolStripMenuItem"].Enabled = false;
            }
            else
            {
                cmProfesionales.Items["verCambiosToolStripMenuItem"].Enabled = true;
            }
        }
    }
}
