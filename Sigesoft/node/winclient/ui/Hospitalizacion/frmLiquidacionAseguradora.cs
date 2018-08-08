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
using System.Diagnostics;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmLiquidacionAseguradora : Form
    {
        string strFilterExpression;

        public frmLiquidacionAseguradora()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtPacient.Text)) Filters.Add("PacientDocument.Contains(\"" + txtPacient.Text.Trim() + "\")");
            if (cboEmpresa.SelectedValue.ToString() != "-1")
            {
                Filters.Add("EmpresaId==" + "\"" + cboEmpresa.SelectedValue + "\"");
            }
          
            strFilterExpression = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    strFilterExpression = strFilterExpression + item + " && ";
                }
                strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
            }

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            var objData = GetData(strFilterExpression);
            grdData.DataSource = objData;
            lblRecordCountCalendar.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<LiquidacionAseguradora> GetData(string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            AseguradoraBL oAseguradoraBL = new AseguradoraBL();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            var _objData = oAseguradoraBL.GetLiquidacionAseguradoraPagedAndFiltered(ref objOperationResult, pstrFilterExpression, pdatBeginDate, pdatEndDate);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void frmLiquidacionAseguradora_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            var dataListOrganization = BLL.Utils.GetOrganization(ref objOperationResult);
            Utils.LoadDropDownList(cboEmpresa, "Value1", "Id", dataListOrganization, DropDownListAction.All);          
            
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Reporte Liquidación Asegueradora del " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text;
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
