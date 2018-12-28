using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.Gerencia
{
    public partial class FrmCredito : Form
    {
        private GerenciaCreditoBl oGerenciaCreditoBl = new GerenciaCreditoBl();
        private List<GerenciaCredito> _listGerenciaCredito = new List<GerenciaCredito>();
        public FrmCredito()
        {
            InitializeComponent();
        }

        private void FrmCredito_Load(object sender, EventArgs e)
        {
            dtpDateTimeStar.CustomFormat = @"dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = @"dd/MM/yyyy";
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(0);

            using (new LoadingClass.PleaseWait(Location, "Generando..."))
            {
                _listGerenciaCredito = oGerenciaCreditoBl.Filter(pdatBeginDate.Value, pdatEndDate.Value);
                grdTree.DataSource = oGerenciaCreditoBl.ProcessDataTreeView(_listGerenciaCredito);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {

        }

        private void grdTree_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (grdTree.Selected.Rows.Count == 0) return;
            if (grdTree.Selected.Rows[0].Cells == null) return;
            foreach (UltraGridRow rowSelected in grdTree.Selected.Rows)
            {
                if (rowSelected.Band.Index.ToString() == "0")
                {
                    var comprobantes = _listGerenciaCredito;
                    grdData.DataSource = comprobantes;
                }
                else if (rowSelected.Band.Index.ToString() == "1")
                {
                    var tipo = grdTree.Selected.Rows[0].Cells["Tipo"].Value.ToString();
                    grdData.DataSource = _listGerenciaCredito.FindAll(p => p.xxx == tipo);
                }
                else if (rowSelected.Band.Index.ToString() == "2")
                {
                    var tipo = grdTree.Selected.Rows[0].Cells["Tipo"].Value.ToString();
                    var empresa = grdTree.Selected.Rows[0].Cells["Empresa"].Value.ToString();
                    grdData.DataSource = _listGerenciaCredito.FindAll(p => p.xxx == tipo && p.EmpresaFacturacion == empresa);
                }
            }
        }
    }
}
