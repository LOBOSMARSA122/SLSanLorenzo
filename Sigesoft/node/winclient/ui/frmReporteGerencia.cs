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
    public partial class frmReporteGerencia : Form
    {
        public frmReporteGerencia()
        {
            InitializeComponent();
        }

        private void frmReporteGerencia_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(cboTipoCaja, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 329, null), DropDownListAction.All);
            dtpDateTimeStar.CustomFormat = "dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = "dd/MM/yyyy";
            dtpDateTimeStar.Value = dtpDateTimeStar.Value.AddDays(0);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);
            var oCustomReportBL = new CustomReportBL();

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                if (tabControl1.SelectedTab.Name == "tpResumenCaja")
                {
                    var data = oCustomReportBL.ResumenCaja(pdatBeginDate.Value, pdatEndDate.Value, int.Parse(cboTipoCaja.SelectedValue.ToString()));
                    txtTotalIngreso.Text = data.Sum(s => s.totalIngreso).ToString();
                    txtTotalEgreso.Text = data.Sum(s => s.totalEgreso).ToString();
                    txtTotalSaldo.Text = data.Sum(s => s.totalSaldo).ToString();
                    grdGrid.DataSource = data;
                }
                else if (tabControl1.SelectedTab.Name == "tpResumenTipoEmpresa")
                {
                    var data = oCustomReportBL.ReportResumenTipoEmpresa(pdatBeginDate.Value, pdatEndDate.Value);
                    grdTipoEmpresa.DataSource = data;
                }
                else
                {
                    var data = oCustomReportBL.ReportResumenTipoPago(pdatBeginDate.Value, pdatEndDate.Value, int.Parse(cboTipoCaja.SelectedValue.ToString()));

                    grdDataTipoPago.DataSource = data;
                                    }
                
            };
           
        }

        private void btnExportResumenCaja_Click(object sender, EventArgs e)
        {
            //"Matriz de datos <Empresa Cliente> de <fecha inicio> a <fecha fin>"
            string NombreArchivo = "";

            NombreArchivo = "Resumen de Caja del " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;


            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdField.FileName = NombreArchivo;
            sfdField.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdField.ShowDialog() == DialogResult.OK)
            {
                this.ugeResumenCaja.Export(this.grdGrid, sfdField.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportTipoPago_Click(object sender, EventArgs e)
        {
            //"Matriz de datos <Empresa Cliente> de <fecha inicio> a <fecha fin>"
            string NombreArchivo = "";

            NombreArchivo = "Resumen de Tipo Pago del " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;


            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdField.FileName = NombreArchivo;
            sfdField.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdField.ShowDialog() == DialogResult.OK)
            {
                this.ugeResumenTipo.Export(this.grdDataTipoPago, sfdField.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportTipoEmpresa_Click(object sender, EventArgs e)
        {
            //"Matriz de datos <Empresa Cliente> de <fecha inicio> a <fecha fin>"
            string NombreArchivo = "";

            NombreArchivo = "Resumen de Tipo Empresa del " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;


            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            sfdField.FileName = NombreArchivo;
            sfdField.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfdField.ShowDialog() == DialogResult.OK)
            {
                this.ugeResumenEmpresa.Export(this.grdTipoEmpresa, sfdField.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

       
    }
}
