using Infragistics.Win.UltraWinGrid;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Gerencia
{
    public partial class FrmTipoExamen_Asistencial : Form
    {
        private GerenciaTipoExamenBl oGerenciaTipoExamenBl = new GerenciaTipoExamenBl();
        private List<GerenciaTipoExamen> _listGerenciaTipoExamen = new List<GerenciaTipoExamen>();

        public FrmTipoExamen_Asistencial()
        {
            InitializeComponent();
        }

        private void FrmTipoExamen_Asistencial_Load(object sender, EventArgs e)
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
                _listGerenciaTipoExamen = oGerenciaTipoExamenBl.Filter(pdatBeginDate.Value, pdatEndDate.Value);
                grdTree.DataSource = oGerenciaTipoExamenBl.ProcessDataTreeView(_listGerenciaTipoExamen);
            }
        }

        private void grdTree_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grdTree.Selected.Rows.Count == 0) return;
            if (grdTree.Selected.Rows[0].Cells == null) return;

            foreach (UltraGridRow rowSelected in grdTree.Selected.Rows)
            {
                if (rowSelected.Band.Index.ToString() == "0")
                {
                    grdData.DataSource = _listGerenciaTipoExamen;
                }
                else if (rowSelected.Band.Index.ToString() == "1")
                {
                    var tipoEso = grdTree.Selected.Rows[0].Cells["TipoEso"].Value.ToString();
                    grdData.DataSource = _listGerenciaTipoExamen.FindAll(p => p.TipoEso == tipoEso).ToList();
                }
                else if (rowSelected.Band.Index.ToString() == "2")
                {
                    var tipoEso = grdTree.Selected.Rows[0].Cells["TipoEso"].Value.ToString();
                    var empresa = grdTree.Selected.Rows[0].Cells["Nombre"].Value.ToString();

                    grdData.DataSource = _listGerenciaTipoExamen.FindAll(p => p.TipoEso == tipoEso && p.Empresa == empresa).ToList();
                }
            }
        }
    }
}
