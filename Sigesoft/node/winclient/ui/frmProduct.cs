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


namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmProduct : Form
    {
        ProductBL _objBL = new ProductBL();
        string strFilterExpression;
        string _Mode;

        public frmProduct()
        {
            InitializeComponent();
        }

        public frmProduct(string pstrMode)
        {
            InitializeComponent();
            _Mode = pstrMode;
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            List<KeyValueDTO> _formActions = Sigesoft.Node.WinClient.BLL.Utils.SetFormActionsInSession("frmProduct", Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_SystemUserId);
                    

            contextMenuStrip1.Items["mnuGridNuevo"].Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmProduct_ADD", _formActions); ;
            contextMenuStrip1.Items["mnuGridModificar"].Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmProduct_EDIT", _formActions);
            contextMenuStrip1.Items["eliminarToolStripMenuItem"].Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmProduct_DELETE", _formActions);
                      
            // Establecer el filtro inicial para los datos
            strFilterExpression = null;
       
            btnFilter_Click(sender, e);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtBrand.Text)) Filters.Add("v_Brand.Contains(\"" + txtBrand.Text.Trim() + "\")");
            if (!string.IsNullOrEmpty(txtName.Text)) Filters.Add("v_Name.Contains(\"" + txtName.Text.Trim() + "\")");
            if (!string.IsNullOrEmpty(txtSerialNumber.Text)) Filters.Add("v_SerialNumber.Contains(\"" + txtSerialNumber.Text.Trim() + "\")");
            Filters.Add("i_IsDeleted==0");
            // Create the Filter Expression
            strFilterExpression = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    strFilterExpression = strFilterExpression + item + " && ";
                }
                strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
            }

            this.BindGrid();
        }

        private void BindGrid()
        {

            var objData = GetData(0, null, "v_Name ASC", strFilterExpression);

            grdData.DataSource = objData;
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<ProductList> GetData(int pintPageIndex, int ?pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objBL.GetProductsPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void mnuGridNuevo_Click(object sender, EventArgs e)
        {
            frmProductEdicion frm = new frmProductEdicion("0", "New");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void mnuGridModificar_Click(object sender, EventArgs e)
        {
            string strProductId = grdData.Selected.Rows[0].Cells[0].Value.ToString();

            frmProductEdicion frm = new frmProductEdicion(strProductId, "Edit");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                // Delete the item              
                string pstrProductId = grdData.Selected.Rows[0].Cells[0].Value.ToString();

                _objBL.DeleteProduct(ref objOperationResult, pstrProductId, Globals.ClientSession.GetAsList());

                btnFilter_Click(sender, e);
            }
        }

        private void grdData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    grdData.Rows[row.Index].Selected = true;
                    contextMenuStrip1.Items["mnuGridNuevo"].Enabled = true;
                    contextMenuStrip1.Items["mnuGridModificar"].Enabled = true;
                    if (_Mode == "View")
                    {
                        contextMenuStrip1.Items["eliminarToolStripMenuItem"].Enabled = false;
                    }
                    else
                    {
                        contextMenuStrip1.Items["eliminarToolStripMenuItem"].Enabled = true;
                    }
                }
                else
                {
                    contextMenuStrip1.Items["mnuGridNuevo"].Enabled = true;
                    contextMenuStrip1.Items["mnuGridModificar"].Enabled = false;
                    contextMenuStrip1.Items["eliminarToolStripMenuItem"].Enabled = false;
                   
                }

            } 
        }
        
    }
}
