using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmMatrizSeguimiento : Form
    {
        string strFilterExpression;
        public frmMatrizSeguimiento()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();

                List<string> Filters = new List<string>();
                DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
                DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

                if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
                {
                    var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
                    Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" + id3[1] + "\"");
                }

                // Create the Filter Expression
                strFilterExpression = null;
                if (Filters.Count > 0)
                {
                    foreach (string item in Filters)
                    {
                        strFilterExpression = strFilterExpression + item + " && ";
                    }
                    strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
                }

                if (MedicalCenter.v_IdentificationNumber == "20505518145")
                {
                    var objData = new PacientBL().ReporteMatrizSeguimientoSanJoaquin(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grdData.DataSource = objData;
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                }
                else
                {
                    //var objData = new PacientBL().ReporteMatrizExcel(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    var objData = new PacientBL().ReporteMatrizSeguimientoSanJoaquin(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);

                    grdData.DataSource = objData;
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                }
                btnExport.Enabled = grdData.Rows.Count > 0;
            }
        }

        private void frmMatrizSeguimiento_Load(object sender, EventArgs e)
        {
            DateTime fechatemp = DateTime.Today;
            DateTime fecha1 = new DateTime(fechatemp.Year, fechatemp.Month, 1);

            OperationResult objOperationResult = new OperationResult();
            //var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);
            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, 9);
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);


            if (Globals.ClientSession.i_SystemUserId == 31)
            {
                ddlCustomerOrganization.Enabled = false;
                ddlCustomerOrganization.SelectedValue = "N009-OO000000108|N009-OL000000115";
            }

            dtpDateTimeStar.Value = fecha1;
            dptDateTimeEnd.Value = DateTime.Today;
            dtpDateTimeStar.CustomFormat = "dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = "dd/MM/yyyy";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //"Matriz de datos <Empresa Cliente> de <fecha inicio> a <fecha fin>"
            string NombreArchivo = "";

            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Matriz de Seguimiento " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
                //NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text;

            }
            else
            {
                NombreArchivo = "Matriz de Seguimiento de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
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
