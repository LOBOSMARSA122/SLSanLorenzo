using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using ScrapperReniecSunat;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using ColumnStyle = Infragistics.Win.UltraWinGrid.ColumnStyle;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class FrmAgendaDinamica : Form
    {
        UltraCombo _ucGenero = new UltraCombo();

        public FrmAgendaDinamica()
        {
            InitializeComponent();
        }
       
        private void frmAgendaDinamica_Load(object sender, EventArgs e)
        {
            grdSchedule.DataSource = new BindingList<AgendaDinamica>();
            //CargarCombosDetalle();
        }

        private void CargarCombosDetalle()
        {
            OperationResult objOperationResult = new OperationResult();

            #region Configura Combo Tipo Documento
            UltraGridBand _ultraGridBanda = new UltraGridBand("Band 0", -1);
            UltraGridColumn _ultraGridColumnaID = new UltraGridColumn("Id");
            UltraGridColumn _ultraGridColumnaDescripcion = new UltraGridColumn("Value1");
            UltraGridColumn _ultraGridColumnaSiglas = new UltraGridColumn("Value2");
            _ultraGridColumnaID.Header.Caption = "Cod.";
            _ultraGridColumnaDescripcion.Header.Caption = "Descripción";
            _ultraGridColumnaSiglas.Header.Caption = "Siglas";
            _ultraGridColumnaID.Width = 30;
            _ultraGridColumnaDescripcion.Width = 200;
            _ultraGridColumnaSiglas.Width = 80;
            _ultraGridBanda.Columns.AddRange(new object[] { _ultraGridColumnaID, _ultraGridColumnaDescripcion, _ultraGridColumnaSiglas });
            _ucGenero.DisplayLayout.BandsSerializer.Add(_ultraGridBanda);
            _ucGenero.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
            _ucGenero.DropDownWidth = 330;
            #endregion

            //Utils.Windows.LoadUltraComboList(ucTipoDocumento, "Value2", "Id", _objDocumentoBL.ObtenDocumentosParaComboGridTesoreria(ref objOperationResult, null), DropDownListAction.Select);
        }

        private void btnAgregarRegistro_Click(object sender, EventArgs e)
        {
            if (grdSchedule.ActiveRow != null)
            {
                if (grdSchedule.ActiveRow.Cells["NroDocumento"].Value != null)
                {
                    UltraGridRow row = grdSchedule.DisplayLayout.Bands[0].AddNew();
                    if (row == null) return;
                    grdSchedule.Rows.Move(row, grdSchedule.Rows.Count - 1);
                    grdSchedule.ActiveRowScrollRegion.ScrollRowIntoView(row);
                    row.Cells["NroDocumento"].Value = "";
                    row.Cells["Nombre"].Value = "";
                    row.Cells["ApellidoPaterno"].Value = "";
                    row.Cells["ApellidoMaterno"].Value = "";
                    row.Cells["FechaNacimiento"].Value = "";
                    row.Cells["GeneroId"].Value = "";
                    row.Cells["Puesto"].Value = "";
                }
            }
            else
            {
                UltraGridRow row = grdSchedule.DisplayLayout.Bands[0].AddNew();
                if (row == null) return;
                grdSchedule.Rows.Move(row, grdSchedule.Rows.Count - 1);
                grdSchedule.ActiveRowScrollRegion.ScrollRowIntoView(row);
                row.Cells["NroDocumento"].Value = "";
                row.Cells["Nombre"].Value = "";
                row.Cells["ApellidoPaterno"].Value = "";
                row.Cells["ApellidoMaterno"].Value = "";
                row.Cells["FechaNacimiento"].Value = "";
                row.Cells["GeneroId"].Value = "";
                row.Cells["Puesto"].Value = "";
            }
            EnterEditMode();
        }

        private void EnterEditMode()
        {
            var lastRow = grdSchedule.Rows.LastOrDefault();
            if (lastRow != null)
            {
                grdSchedule.Focus();
                grdSchedule.ActiveColScrollRegion.Scroll(ColScrollAction.Left);
                grdSchedule.ActiveCell = lastRow.Cells["NroDocumento"];
                grdSchedule.PerformAction(UltraGridAction.EnterEditMode, false, false);
            }
        }

        private void ObtenerDatosDni(string dni)
        {
            var f = new frmBuscarDatos(dni);
            if (f.ConexionDisponible)
            {
                f.ShowDialog();

                switch (f.Estado)
                {
                    case Estado.NoResul:
                        MessageBox.Show(@"No se encontró datos de el DNI");
                        break;

                    case Estado.Ok:
                        if (f.Datos != null)
                        {
                            if (!f.EsContribuyente)
                            {
                                var datos = (ReniecResultDto)f.Datos;
                                grdSchedule.ActiveRow.Cells["Nombre"].Value = datos.Nombre;
                                grdSchedule.ActiveRow.Cells["ApellidoPaterno"].Value = datos.ApellidoPaterno;
                                grdSchedule.ActiveRow.Cells["ApellidoMaterno"].Value = datos.ApellidoMaterno;
                                grdSchedule.ActiveRow.Cells["FechaNacimiento"].Value = datos.FechaNacimiento;

                                var lastRow = grdSchedule.Rows.LastOrDefault();
                                if (lastRow != null)
                                {
                                    grdSchedule.Focus();
                                    grdSchedule.ActiveColScrollRegion.Scroll(ColScrollAction.Left);
                                    grdSchedule.ActiveCell = lastRow.Cells["Puesto"];
                                    grdSchedule.PerformAction(UltraGridAction.EnterEditMode, false, false);
                                }
                            }
                        }
                        break;
                }
            }
            else
                MessageBox.Show(@"No se pudo conectar la página", @"Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      
        private void grdSchedule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var dni = grdSchedule.ActiveRow.Cells["NroDocumento"].Text;
                ObtenerDatosDni(dni);
            }
        }

        private void grdSchedule_KeyUp(object sender, KeyEventArgs e)
        {
            var grid = (UltraGrid)sender;

            if (e.KeyCode == Keys.Down)
            {
                btnAgregarRegistro_Click(sender, e);
                // Go down one row
                grid.PerformAction(UltraGridAction.BelowCell);
            }
        }

        private void grdSchedule_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            //e.Layout.Bands[0].Columns["GeneroId"].EditorComponent = _ucGenero;
            e.Layout.Bands[0].Columns["GeneroId"].Style = ColumnStyle.DropDownList;
        }
    }
}
