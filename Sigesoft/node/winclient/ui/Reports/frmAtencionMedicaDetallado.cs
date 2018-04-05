using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;



namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmAtencionMedicaDetallado : Form
    {
        public frmAtencionMedicaDetallado()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
           
            ServiceBL oServiceBL = new ServiceBL();

            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            var Lista =  oServiceBL.ReporteAtencionMedicaDetallada(pdatBeginDate, pdatEndDate, int.Parse(ddlUsuario.SelectedValue.ToString()));
            if (Lista.Count > 0)
            {
                btnExport.Enabled = true;
            }
            else
            {
                btnExport.Enabled = false;
            }
            grdData.DataSource = Lista;
        }

        private void frmAtencionMedicaDetallado_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlUsuario, "Value1", "Id", BLL.Utils.GetProfessional(ref objOperationResult, ""), DropDownListAction.All);

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
                lblNombreProfesional.Text = "Todos los Profesionales";
                return;
            }

            oSystemUserList = oProfessionalBL.GetSystemUserName(ref objOperationResult, int.Parse(ddlUsuario.SelectedValue.ToString()));

            lblNombreProfesional.Text = oSystemUserList.v_PersonName;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //"Matriz de datos <Empresa Cliente> de <fecha inicio> a <fecha fin>"
            string NombreArchivo = "";

            if (ddlUsuario.SelectedValue.ToString() == "-1")
            {
                NombreArchivo = "Reporte de Atenciones Médicas de todos los profesionales " + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
                //NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text;

            }
            else
            {
                NombreArchivo = "Reporte de Atenciones Médicas del profesional " + lblNombreProfesional.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
                //NombreArchivo = "Matriz de datos";
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdData, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
