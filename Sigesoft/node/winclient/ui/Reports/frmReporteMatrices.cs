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
using System.IO;
using NetPdf;
using Infragistics.Win.UltraWinGrid;
namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmReporteMatrices : Form
    {
        ServiceBL _serviceBL = new ServiceBL();
        string strFilterExpression;
        public frmReporteMatrices()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                List<string> Filters = new List<string>();
                DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
                DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

                if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
                {
                    var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
                    Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" + id3[1] + "\"");
                }

                if (ddlProtocolId.SelectedValue.ToString() != "-1")
                    Filters.Add("IdProtocolId==" + "\"" + ddlProtocolId.SelectedValue + "\"");

                //Filters.Add("IdTrabajador =N009-PP000000208");

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

                var x = tabControl1.SelectedTab.Name;



                if (tabControl1.SelectedTab.Name == "tpMdAramark")
                {
                    var objData = _serviceBL.ReporteAramark(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grdDataAramark.DataSource = objData;
                }
                else if (tabControl1.SelectedTab.Name == "tpMdMasterClinico")
                {
                    var objData = _serviceBL.ReporteMasterClinico(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grdDataMasterClinico.DataSource = objData;
                }
                else if (tabControl1.SelectedTab.Name == "tpMdDisal")
                {
                    var objData = _serviceBL.ReporteDisal(pdatBeginDate, pdatEndDate, ddlCustomerOrganization.SelectedValue.ToString(), strFilterExpression);
                    grdDataDisal.DataSource = objData;
                }

            
                //lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
            }
        }

        private void ddlCustomerOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ddlCustomerOrganization_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlCustomerOrganization.SelectedValue == null)
                return;

            if (ddlCustomerOrganization.SelectedValue.ToString() == "-1")
            {
                ddlProtocolId.SelectedValue = "-1";
                ddlProtocolId.Enabled = false;
                return;
            }
            ddlProtocolId.Enabled = true;
            OperationResult objOperationResult = new OperationResult();
            var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, id3[0], id3[1], null), DropDownListAction.All);

        }

        private void frmReporteMatrices_Load(object sender, EventArgs e)
        {
            DateTime fechatemp = DateTime.Today;
            DateTime fecha1 = new DateTime(fechatemp.Year, fechatemp.Month, 1);

            OperationResult objOperationResult = new OperationResult();
            //var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);
            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, 9);
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);

            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, "-1", "-1", null), DropDownListAction.All);

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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnExportAramark_Click(object sender, EventArgs e)
        {
            //"Matriz de datos <Empresa Cliente> de <fecha inicio> a <fecha fin>"
            string NombreArchivo = "";

            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
                //NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text;

            }
            else
            {
                NombreArchivo = "Matriz de datos de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
                //NombreArchivo = "Matriz de datos";
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdAramark.FileName = NombreArchivo;
            sfdAramark.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdAramark.ShowDialog() == DialogResult.OK)
            {
                this.ugeAramark.Export(this.grdDataAramark, sfdAramark.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportclinico_Click(object sender, EventArgs e)
        {
            //"Matriz de datos <Empresa Cliente> de <fecha inicio> a <fecha fin>"
            string NombreArchivo = "";

            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
                //NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text;

            }
            else
            {
                NombreArchivo = "Matriz de datos de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
                //NombreArchivo = "Matriz de datos";
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdClinico.FileName = NombreArchivo;
            sfdClinico.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdClinico.ShowDialog() == DialogResult.OK)
            {
                this.ugeClinico.Export(this.grdDataMasterClinico, sfdClinico.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportDisal_Click(object sender, EventArgs e)
        {
            //"Matriz de datos <Empresa Cliente> de <fecha inicio> a <fecha fin>"
            string NombreArchivo = "";

            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
                //NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text;

            }
            else
            {
                NombreArchivo = "Matriz de datos de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
                //NombreArchivo = "Matriz de datos";
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdDisal.FileName = NombreArchivo;
            sfdDisal.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdDisal.ShowDialog() == DialogResult.OK)
            {
                this.ugeDisal.Export(this.grdDataDisal, sfdDisal.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void grdDataAramark_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnExportAramark.Enabled = grdDataAramark.Rows.Count > 0;
        }

        private void grdDataMasterClinico_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnExportclinico.Enabled = grdDataMasterClinico.Rows.Count > 0;
        }

        private void grdDataDisal_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnExportDisal.Enabled = grdDataDisal.Rows.Count > 0;
        }
    }
}
