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

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            _listGerenciaTipoPago = oGerenciaTipoPagoBl.Filter(pdatBeginDate.Value, pdatEndDate.Value);
            grdTree.DataSource = oGerenciaTipoPagoBl.ProcessDataTreeView(_listGerenciaTipoPago);

        }

        private void FrmTipoPago_Load(object sender, EventArgs e)
        {
            dtpDateTimeStar.CustomFormat = @"dd/MM/yyyy";
            dptDateTimeEnd.CustomFormat = @"dd/MM/yyyy";
        }

        private void grdTree_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (grdTree.Selected.Rows.Count == 0) return;
            if (grdTree.Selected.Rows[0].Cells != null)
            {
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
                        grdData.DataSource = _listGerenciaTipoPago;
                    }
                    else if (rowSelected.Band.Index.ToString() == "1")
                    {
                        var tipoPago = grdTree.Selected.Rows[0].Cells["TipoPago"].Value.ToString();

                        grdData.DataSource = _listGerenciaTipoPago.FindAll(p => p.CondicionPago == tipoPago);
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
                        grdData.DataSource = _listGerenciaTipoPago
                            .FindAll(p => p.Empresa == empresa && p.CondicionPago == tipoPago).ToList();
                    }
                }
            }
        }

        private void grdTree_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void grdData_InitializeRow(object sender, InitializeRowEventArgs e)
        {

        }
    }
}
