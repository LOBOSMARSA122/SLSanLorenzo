using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.Gerencia
{
    public partial class FrmContrata : Form
    {
        GerenciaContrataBl oGerenciaContrataBl = new GerenciaContrataBl();
        private List<GerenciaTipoPago> _listGerenciaTipoPago = new List<GerenciaTipoPago>();

        public FrmContrata()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            _listGerenciaTipoPago = oGerenciaContrataBl.Filter(pdatBeginDate.Value, pdatEndDate.Value);
            grdTree.DataSource = oGerenciaContrataBl.ProcessDataTreeView(_listGerenciaTipoPago);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {

        }

        private void grdTree_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (grdTree.Selected.Rows.Count == 0) return;
            if (grdTree.Selected.Rows[0].Cells == null) return;

            grdData.DisplayLayout.Bands[0].Columns["Trabajador"].Hidden = true;
            grdData.DisplayLayout.Bands[0].Columns["CondicionPago"].Hidden = true;
            grdData.DisplayLayout.Bands[0].Columns["Empresa"].Hidden = true;
            grdData.DisplayLayout.Bands[0].Columns["Importe"].Hidden = true;
            grdData.DisplayLayout.Bands[0].Columns["CostoExamen"].Hidden = true;
            //grdData.DisplayLayout.Bands[0].Columns["TipoEso"].Hidden = true;

            foreach (UltraGridRow rowSelected in grdTree.Selected.Rows)
            {
                if (rowSelected.Band.Index.ToString() == "0")
                {
                    grdData.DataSource = _listGerenciaTipoPago;
                }
                else if (rowSelected.Band.Index.ToString() == "1")
                {
                    grdData.DisplayLayout.Bands[0].Columns["CostoExamen"].Hidden = false;
                    var contrataName = grdTree.Selected.Rows[0].Cells["ContrataName"].Value.ToString();

                    grdData.DataSource = _listGerenciaTipoPago.FindAll(p => p.Compania == contrataName);
                }
                else if (rowSelected.Band.Index.ToString() == "2")
                {
                    grdData.DisplayLayout.Bands[0].Columns["Trabajador"].Hidden = false;
                    grdData.DisplayLayout.Bands[0].Columns["CondicionPago"].Hidden = false;
                    grdData.DisplayLayout.Bands[0].Columns["CostoExamen"].Hidden = false;
                    var contrataName = grdTree.Selected.Rows[0].Cells["ContrataName"].Value.ToString();
                    grdData.DataSource = _listGerenciaTipoPago.FindAll(p => p.Compania == contrataName);
                }
            }
        }
    }
}
