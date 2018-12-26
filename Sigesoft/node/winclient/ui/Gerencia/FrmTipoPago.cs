using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.Gerencia
{
    public partial class FrmTipoPago : Form
    {
        GerenciaTipoPagoBl oGerenciaTipoPagoBl = new GerenciaTipoPagoBl();
        private List<GerenciaTipoPago> _listGerenciaTipoPago = new List<GerenciaTipoPago>();

        public FrmTipoPago()
        {
            InitializeComponent();
        }

        private void FrmTipoPago_Load(object sender, EventArgs e)
        {
            dtpDateTimeStar.CustomFormat = @"dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = @"dd/MM/yyyy";
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(0);

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                _listGerenciaTipoPago = oGerenciaTipoPagoBl.Filter(pdatBeginDate.Value, pdatEndDate.Value);
                grdTree.DataSource = oGerenciaTipoPagoBl.ProcessDataTreeView(_listGerenciaTipoPago);
            }
           
        }
        
        private void grdTree_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (grdTree.Selected.Rows.Count == 0) return;
            if (grdTree.Selected.Rows[0].Cells == null) return;

            grdData.DisplayLayout.Bands[0].Columns["Trabajador"].Hidden = true;
            grdData.DisplayLayout.Bands[0].Columns["FechaServicio"].Hidden = true;
            grdData.DisplayLayout.Bands[0].Columns["Compania"].Hidden = true;
            grdData.DisplayLayout.Bands[0].Columns["Contratista"].Hidden = true;
            grdData.DisplayLayout.Bands[0].Columns["CostoExamen"].Hidden = true;
            grdData.DisplayLayout.Bands[0].Columns["TipoEso"].Hidden = true;

            foreach (UltraGridRow rowSelected in grdTree.Selected.Rows)
            {
                if (rowSelected.Band.Index.ToString() == "0")
                {
                    var agrupador = grdTree.Selected.Rows[0].Cells["Agrupador"].Value.ToString();
                    var comprobantes = _listGerenciaTipoPago.FindAll(p => p.Agrupador == agrupador).GroupBy(g => g.Comprobante).Select(s => s.First()).ToList();
                    grdData.DataSource = comprobantes;

                }
                else if (rowSelected.Band.Index.ToString() == "1")
                {
                    var agrupador = grdTree.Selected.Rows[0].Cells["Agrupador"].Value.ToString();
                    var tipoPago = grdTree.Selected.Rows[0].Cells["TipoPago"].Value.ToString();


                    var listaIngresosPorTipoPago = _listGerenciaTipoPago.FindAll(p => p.CondicionPago == tipoPago && p.Agrupador == agrupador).ToList();
                    var listaIngresosPorTipoPagoYComprobante = listaIngresosPorTipoPago.ToList().GroupBy(g => g.Comprobante).Select(s => s.First()).ToList();
                    grdData.DataSource = listaIngresosPorTipoPagoYComprobante;
                }
                else if (rowSelected.Band.Index.ToString() == "2")
                {
                    var empresa = grdTree.Selected.Rows[0].Cells["EmpresaNombre"].Value.ToString();
                    var tipoPago = grdTree.Selected.Rows[0].Cells["TipoPago"].Value.ToString();

                    List<GerenciaTipoPago> list = new List<GerenciaTipoPago>();
                    var x = _listGerenciaTipoPago
                        .FindAll(p => p.Empresa == empresa && p.CondicionPago == tipoPago).ToList()[0];
                    list.Add(x);
                    grdData.DataSource = list;
                }
                else if (rowSelected.Band.Index.ToString() == "3")
                {
                    grdData.DisplayLayout.Bands[0].Columns["Trabajador"].Hidden = false;
                    grdData.DisplayLayout.Bands[0].Columns["FechaServicio"].Hidden = false;
                    grdData.DisplayLayout.Bands[0].Columns["Compania"].Hidden = false;
                    grdData.DisplayLayout.Bands[0].Columns["Contratista"].Hidden = false;
                    grdData.DisplayLayout.Bands[0].Columns["CostoExamen"].Hidden = false;
                    grdData.DisplayLayout.Bands[0].Columns["TipoEso"].Hidden = false;

                    var empresa = grdTree.Selected.Rows[0].Cells["EmpresaNombre"].Value.ToString();
                    var tipoPago = grdTree.Selected.Rows[0].Cells["TipoPago"].Value.ToString();
                    if(grdTree.Selected.Rows[0].Cells["Eso"].Value == null) return;
                    var tipoEso = grdTree.Selected.Rows[0].Cells["Eso"].Value.ToString();
                    grdData.DataSource = _listGerenciaTipoPago
                        .FindAll(p => p.Empresa == empresa && p.CondicionPago == tipoPago && p.TipoEso == tipoEso).ToList();
                }
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            var  nombreArchivo = "Reporte Por Tipo de Pago de " + dtpDateTimeStar.Text + " hasta " + dptDateTimeEnd.Text;

            nombreArchivo = nombreArchivo.Replace("/", "_");
            nombreArchivo = nombreArchivo.Replace(":", "_");

            sfd.FileName = nombreArchivo;
            sfd.Filter = @"Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (sfd.ShowDialog() != DialogResult.OK) return;
            uge.Export(grdData, sfd.FileName);
            MessageBox.Show(@"Se exportaron correctamente los datos.", @" ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
