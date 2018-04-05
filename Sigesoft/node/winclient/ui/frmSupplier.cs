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
    public partial class frmSupplier : Form
    {
        SupplierBL _objBL = new SupplierBL();
        string strFilterExpression;
        string _Mode;
        public string _SupplierId;

        public frmSupplier(string pstrMode)
        {
            InitializeComponent();
            _Mode = pstrMode;
        }

        public frmSupplier()
        {
            InitializeComponent();
        }

        private void frmSupplier_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            List<KeyValueDTO> _formActions = Sigesoft.Node.WinClient.BLL.Utils.SetFormActionsInSession("frmSupplier", Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_SystemUserId);

            contextMenuStrip1.Items["mnuGridNuevo"].Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmSupplier_ADD", _formActions); ;
            contextMenuStrip1.Items["mnuGridModificar"].Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmSupplier_EDIT", _formActions);
            contextMenuStrip1.Items["eliminarToolStripMenuItem"].Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmSupplier_DELETE", _formActions);
            // Establecer el filtro inicial para los datos
            strFilterExpression = null;
            //Llenado de combos
            Utils.LoadDropDownList(ddlSectorTypeId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 104, null), DropDownListAction.All);
            
            btnFilter_Click(sender, e);

            if (_Mode =="View")
            {
                contextMenuStrip1.Items["mnuGridNuevo"].Enabled = true;
                contextMenuStrip1.Items["mnuGridModificar"].Enabled = true;
                contextMenuStrip1.Items["eliminarToolStripMenuItem"].Enabled = false;
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
           if (ddlSectorTypeId.SelectedValue.ToString() != "-1") Filters.Add("i_SectorTypeId==" + ddlSectorTypeId.SelectedValue);
            if (!string.IsNullOrEmpty(txtIdentificationNumber.Text)) Filters.Add("v_IdentificationNumber==" + "\"" + txtIdentificationNumber.Text.Trim() + "\"");
            if (!string.IsNullOrEmpty(txtName.Text)) Filters.Add("v_Name.Contains(\"" + txtName.Text.Trim() + "\")");
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

        private List<SupplierList> GetData(int pintPageIndex, int ?pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objBL.GetSuppliersPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }
        
        private void mnuGridEliminar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                // Delete the item
                string pstrSupplierId = grdData.Selected.Rows[0].Cells[0].Value.ToString();


                _objBL.DeleteSupplier(ref objOperationResult, pstrSupplierId, Globals.ClientSession.GetAsList());

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
                    contextMenuStrip1.Items["eliminarToolStripMenuItem"].Enabled = true;
                }
                else
                {
                    contextMenuStrip1.Items["mnuGridNuevo"].Enabled = true;
                    contextMenuStrip1.Items["mnuGridModificar"].Enabled = false;
                    contextMenuStrip1.Items["eliminarToolStripMenuItem"].Enabled = false;
                                        
                }
            }
        }

        private void mnuGridNuevo_Click(object sender, EventArgs e)
        {
            frmSupplierEdicion frm = new frmSupplierEdicion("0", "New");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void mnuGridModificar_Click(object sender, EventArgs e)
        {
            string strSupplierId = grdData.Selected.Rows[0].Cells[0].Value.ToString();

            frmSupplierEdicion frm = new frmSupplierEdicion(strSupplierId, "Edit");
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
                string strSupplierId = grdData.Selected.Rows[0].Cells[0].Value.ToString();
                _objBL.DeleteSupplier(ref objOperationResult, strSupplierId, Globals.ClientSession.GetAsList());

                btnFilter_Click(sender, e);
            }
        }

        private void txtIdentificationNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan
                    e.Handled = true;
                }           
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            if (_Mode == "View")
            {
                if (grdData.Selected.Rows.Count == 0) return;
                _SupplierId = grdData.Selected.Rows[0].Cells[0].Value.ToString();
                this.Close();
            }
        }
 
    }
}
