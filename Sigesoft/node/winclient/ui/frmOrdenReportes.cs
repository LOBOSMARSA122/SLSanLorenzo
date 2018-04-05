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
using NetPdf;
using System.IO;
using System.Diagnostics;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmOrdenReportes : Form
    {
        OrganizationBL oOrganizationBL = new OrganizationBL();
        List<string> _listaEmpresas = new List<string>();
        string _empresaPlantillaId = "";
        string _nombreEmpresaPlantilla = "";

        public frmOrdenReportes(List<string> plistListaEmpresas, string pstrEmpresaPlantillaId, string pstrNombreEmpresaPlantilla)
        {
            _listaEmpresas = plistListaEmpresas;
            _empresaPlantillaId = pstrEmpresaPlantillaId;
            _nombreEmpresaPlantilla = pstrNombreEmpresaPlantilla;
            InitializeComponent();
        }

        private void frmOrdenReportes_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            txtEmpresaBase.Text = _nombreEmpresaPlantilla;
          
           var Lista = oOrganizationBL.GetOrdenReportes(ref objOperationResult, _empresaPlantillaId);
           if (Lista.Count > 0)
           {
               List<OrdenReportes> ListaCompletaReportes = new List<OrdenReportes>();
               ListaCompletaReportes = oOrganizationBL.GetAllOrdenReporteNuevo(ref objOperationResult, 0, null, "", "");

               foreach (var ListaReportes in ListaCompletaReportes)
               {
                   foreach (var item in Lista)
                   {
                       if (item.v_ComponenteId == ListaReportes.v_ComponenteId)
                       {
                           ListaReportes.v_OrdenReporteId = item.v_OrdenReporteId;
                           ListaReportes.b_Seleccionar = true;
                           ListaReportes.v_ComponenteId = item.v_ComponenteId;
                           ListaReportes.v_NombreReporte = item.v_NombreReporte;
                           ListaReportes.i_Orden = item.i_Orden.Value;
                           ListaReportes.v_NombreCrystal = item.v_NombreCrystal;
                           ListaReportes.i_NombreCrystalId = item.i_NombreCrystalId == null ? (int?)null : item.i_NombreCrystalId.Value;
                       }
                   }
               }
               ListaCompletaReportes.Sort((x, y) => x.i_Orden.Value.CompareTo(y.i_Orden.Value));
               grdData.DataSource = ListaCompletaReportes;

           }
           else
           {
               bindgridNew();
           }

      
        }

        private void bindgridNew()
        {
            OperationResult objOperationResult = new OperationResult();
            var Lista =  oOrganizationBL.GetAllOrdenReporteNuevo(ref objOperationResult, 0, null, "", "");
            grdData.DataSource = Lista;
        }

        private void grdData_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if ((e.Cell.Column.Key == "b_Seleccionar"))
            {
                if ((e.Cell.Value.ToString() == "False"))
                {
                    e.Cell.Value = true;

                }
                else
                {
                    e.Cell.Value = false;
                }

            }
        }

        private void grdData_SelectionDrag(object sender, CancelEventArgs e)
        {
            grdData.DoDragDrop(grdData.Selected.Rows, DragDropEffects.Move);
        }

        private void grdData_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            UltraGrid grid = sender as UltraGrid;
            Point pointInGridCoords = grid.PointToClient(new Point(e.X, e.Y));
            if (pointInGridCoords.Y < 20)
                // Scroll up.
                this.grdData.ActiveRowScrollRegion.Scroll(RowScrollAction.LineUp);
            else if (pointInGridCoords.Y > grid.Height - 20)
                // Scroll down.
                this.grdData.ActiveRowScrollRegion.Scroll(RowScrollAction.LineDown);
        }

        private void grdData_DragDrop(object sender, DragEventArgs e)
        {
            int dropIndex;

            // Get the position on the grid where the dragged row(s) are to be dropped.
            //get the grid coordinates of the row (the drop zone)
            UIElement uieOver = grdData.DisplayLayout.UIElement.ElementFromPoint(grdData.PointToClient(new Point(e.X, e.Y)));

            //get the row that is the drop zone/or where the dragged row is to be dropped
            UltraGridRow ugrOver = uieOver.GetContext(typeof(UltraGridRow), true) as UltraGridRow;
            if (ugrOver != null)
            {
                dropIndex = ugrOver.Index;    //index/position of drop zone in grid

                //get the dragged row(s)which are to be dragged to another position in the grid
                SelectedRowsCollection SelRows = (SelectedRowsCollection)e.Data.GetData(typeof(SelectedRowsCollection)) as
                SelectedRowsCollection;
                //get the count of selected rows and drop each starting at the dropIndex
                foreach (UltraGridRow aRow in SelRows)
                {
                    //move the selected row(s) to the drop zone
                    grdData.Rows.Move(aRow, dropIndex);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            ordenreporteDto oordenreporteDto = null;
            List<ordenreporteDto> ListaOrdem = new List<ordenreporteDto>();
            int Contador =1;

            foreach (var itemEmpresa in _listaEmpresas)
	        {
                //Eliminar Antiguos Registros
                oOrganizationBL.DeleteOrdenReportes(ref objOperationResult, itemEmpresa.ToString());
                Contador = 1;
		          foreach (var item in grdData.Rows)
	                {
		                oordenreporteDto = new ordenreporteDto();

                        if ((bool)item.Cells["b_Seleccionar"].Value)
                        {
		                    oordenreporteDto.i_Orden = Contador;
                            oordenreporteDto.v_OrganizationId = itemEmpresa.ToString();
                            oordenreporteDto.v_NombreReporte = item.Cells["v_NombreReporte"].Value.ToString();
                            oordenreporteDto.v_ComponenteId = item.Cells["v_ComponenteId"].Value.ToString();
                            oordenreporteDto.v_NombreCrystal = item.Cells["v_NombreCrystal"].Value == null ? "" : item.Cells["v_NombreCrystal"].Value.ToString();
                            oordenreporteDto.i_NombreCrystalId = item.Cells["i_NombreCrystalId"].Value == null ? (int?)null : int.Parse(item.Cells["i_NombreCrystalId"].Value.ToString());
                            ListaOrdem.Add(oordenreporteDto);
                            Contador++;
	                    }
	                }
	        }
            
            oOrganizationBL.AddOrdenReportes(ref objOperationResult, ListaOrdem, Globals.ClientSession.GetAsList());

            if (objOperationResult.Success == 1)  // Operación sin error
            {
                this.Close();
            }
            else  // Operación con error
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Se queda en el formulario.
            }
        }

        private void grdData_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            var IdComponente = grdData.Selected.Rows[0].Cells["v_ComponenteId"].Value.ToString();

            frmSeleccionarNombreCrystal frm = new frmSeleccionarNombreCrystal(IdComponente);
            frm.ShowDialog();
             var x = frm.NombreCrystal;
             var y = frm.IdNombreCrystal;
              var o=  grdData.Selected.Rows[0].Cells["v_NombreCrystal"];
              var p = grdData.Selected.Rows[0].Cells["i_NombreCrystalId"];
              o.Value = x;
              p.Value = y;
           
        }

     

    }
}
