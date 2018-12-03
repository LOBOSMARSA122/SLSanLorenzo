using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class FrmAgendaDinamica : Form
    {
        public FrmAgendaDinamica()
        {
            InitializeComponent();
        }
       
        private void frmAgendaDinamica_Load(object sender, EventArgs e)
        {
            grdDataService.DisplayLayout.Bands[0].Columns.Add("xxx", "yyyy");
            grdDataService.DisplayLayout.Bands[0].Columns["xxx"].DataType = typeof(Boolean);
            grdDataService.DisplayLayout.Bands[0].Columns["xxx"].CellActivation = Activation.AllowEdit;
            grdDataService.DisplayLayout.Bands[0].Columns["xxx"].CellClickAction = CellClickAction.Edit;


            OperationResult objOperationResult = new OperationResult();
            var objData = new ServiceBL().GetServicesPagedAndFiltered_F(ref objOperationResult, 0, null, "", "", DateTime.Parse("01/08/2018"), DateTime.Parse("01/09/2018"), null, DateTime.Parse("01/01/2000"), DateTime.Parse("01/01/2050"), "");
            grdDataService.DataSource = objData;


            
            
        }

        private void grdDataService_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            e.Layout.Override.HeaderClickAction = HeaderClickAction.Select;
            e.Layout.Override.SelectTypeCol = SelectType.None;         
        }

        private void grdDataService_MouseUp(object sender, MouseEventArgs e)
        {
            var grid = (UltraGrid)sender;
            var element = grid.DisplayLayout.UIElement.LastElementEntered;
            if(element == null)return;
            var header = element.GetContext(typeof(Infragistics.Win.UltraWinGrid.ColumnHeader)) as Infragistics.Win.UltraWinGrid.ColumnHeader;

            if (header == null) return;
            var dialogResult = MessageBox.Show(@"presionó " + header.Column.Key, @"INFORMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                //grid.DisplayLayout.Bands[0].Columns[header.Column.Key].CellActivation = Activation.Disabled;
                try
                {
                    var isBound = grid.DisplayLayout.Bands[0].Columns[header.Column.Key].IsBound;
                    if(!isBound)grid.DisplayLayout.Bands[0].Columns.Remove(header.Column.Key);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
               
            }
        }

        private void grdDataService_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (e.Cell.Column.DataType.Name != "Boolean")return;
            //if (e.Cell.Column.Key != "xxx") return;
            e.Cell.Value = e.Cell.Value.ToString() == "False";
        }

        private void btnAgregarRegistro_Click(object sender, EventArgs e)
        {
            if (grdAgenda.ActiveRow != null)
            {
                if (grdAgenda.ActiveRow.Cells["NroDocumento"].Value != null)
                {
                    UltraGridRow row = grdAgenda.DisplayLayout.Bands[0].AddNew();
                    if (row == null) return;
                    grdAgenda.Rows.Move(row, grdAgenda.Rows.Count - 1);
                    grdAgenda.ActiveRowScrollRegion.ScrollRowIntoView(row);
                    row.Cells["NroDocumento"].Value = "";
                    row.Cells["Nombre"].Value = "";
                    row.Cells["ApellidoPaterno"].Value = "";
                    row.Cells["ApellidoMaterno"].Value = "";
                    row.Cells["FechaNacimiento"].Value = "";
                    row.Cells["Genero"].Value = "";
                    row.Cells["Puesto"].Value = "";
                }
            }
            else
            {
                grdAgenda.Rows[0].Activate();
                UltraGridRow row = grdAgenda.DisplayLayout.Bands[0].AddNew();
                if (row == null) return;
                grdAgenda.Rows.Move(row, grdAgenda.Rows.Count - 1);
                grdAgenda.ActiveRowScrollRegion.ScrollRowIntoView(row);
                row.Cells["NroDocumento"].Value = "";
                row.Cells["Nombre"].Value = "";
                row.Cells["ApellidoPaterno"].Value = "";
                row.Cells["ApellidoMaterno"].Value = "";
                row.Cells["FechaNacimiento"].Value = "";
                row.Cells["Genero"].Value = "";
                row.Cells["Puesto"].Value = "";
            }
            EnterEditMode();
        }

        private void EnterEditMode()
        {
            //var ultimaFila = grdData.Rows.LastOrDefault();
            //if (ultimaFila != null && !cboDocumento.Focused)
            //{
            //    grdData.Focus();
            //    grdData.ActiveColScrollRegion.Scroll(ColScrollAction.Left);
            //    grdData.ActiveCell = ultimaFila.Cells["v_CodigoInterno"];
            //    grdData.PerformAction(UltraGridAction.EnterEditMode, false, false);
            //    if (!grdData.ActiveCell.IsInEditMode)
            //    {
            //        grdData.PerformAction(UltraGridAction.EnterEditMode, false, false);
            //    }
            //}
        }
    }
}
