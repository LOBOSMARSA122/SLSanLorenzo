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
    public partial class frmStock : Form
    {
        string strFilterExpression;

        public frmStock()
        {
            InitializeComponent();
        }
         
        private void frmStock_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            strFilterExpression = null;
            var nodeId = Globals.ClientSession.i_CurrentExecutionNodeId;

            Utils.LoadComboTreeBoxList(ddlCategoryId, BLL.Utils.GetDataHierarchyForComboTreeBox(ref objOperationResult, 103, null), DropDownListAction.All);
            Utils.LoadDropDownList(ddlOrganizationLocationId, "Value1", "Id", BLL.Utils.GetJoinOrganizationAndLocationNotInRestricted(ref objOperationResult, nodeId), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlware, "Value1", "Id", BLL.Utils.GetWarehouseNotInRestricted(ref objOperationResult, nodeId, "-1", "-1"), DropDownListAction.Select);

          
            ddlCodicionStock.Text = "--Todos--";
        }

        private void ddlOrganizationLocationId_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadcbWarehouse();
        }

        private void LoadcbWarehouse()
        {
            var nodeId = Globals.ClientSession.i_CurrentExecutionNodeId;

            OperationResult objOperationResult = new OperationResult();
            var index = ddlOrganizationLocationId.SelectedIndex;
            if (index == 0)
            {
                Utils.LoadDropDownList(ddlware, "Value1", "Id", BLL.Utils.GetWarehouseNotInRestricted(ref objOperationResult, nodeId, "-1", "-1"), DropDownListAction.Select);
                return;
            }
            var dataList = ddlOrganizationLocationId.SelectedValue.ToString().Split('|');
            string idOrg = dataList[1];
            string idLoc = dataList[2];

            Utils.LoadDropDownList(ddlware, "Value1", "Id", BLL.Utils.GetWarehouseNotInRestricted(ref objOperationResult, nodeId, idOrg, idLoc), DropDownListAction.Select);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            string strCadena = null;
            strFilterExpression = null;
            OperationResult objOperationResult = new OperationResult();
            DataHierarchyBL objDataHierarchyBL = new DataHierarchyBL();
            datahierarchyDto objdatahierarchyDto = new datahierarchyDto();
            List<DataHierarchyList> DataHierarchyList = new List<DataHierarchyList>();

            string selectedItemddlCodicionStock = ddlCodicionStock.Items[ddlCodicionStock.SelectedIndex].ToString();

            if (ultraValidator1.Validate(true, false).IsValid)
            {
                // Get the filters from the UI
                List<string> Filters = new List<string>();
                if (!string.IsNullOrEmpty(txtName.Text)) Filters.Add("(v_ProductName.Contains(\"" + txtName.Text.Trim() + "\"))");
                if (ddlCategoryId.Text != "--Todos--")
                {
                    int idCategory = Convert.ToInt32(ddlCategoryId.SelectedNode.Tag);
                    objdatahierarchyDto = objDataHierarchyBL.GetDataHierarchy(ref objOperationResult, 103, idCategory);

                    DataHierarchyList = objDataHierarchyBL.GetDataHierarchies(ref objOperationResult, 103, objdatahierarchyDto.i_ItemId);

                    if (DataHierarchyList.Count == 0)
                    {
                        strCadena = "i_CategoryId==" + ddlCategoryId.SelectedNode.Tag + " || ";
                    }
                    else
                    {
                        foreach (var item in DataHierarchyList)
                        {
                            strCadena = strCadena + "i_CategoryId==" + item.i_ItemId + " || ";
                        }
                    }

                    if (strCadena != null)
                    {
                        strCadena = strCadena.Substring(0, strCadena.Length - 4);
                        Filters.Add("(" + strCadena + ")");
                    }
                }
                if (ddlware.SelectedValue.ToString() != "-1") Filters.Add("v_WarehouseId==" + "\"" + ddlware.SelectedValue + "\"");
                if (!string.IsNullOrEmpty(txtBrand.Text)) Filters.Add("v_Brand.Contains(\"" + txtBrand.Text.Trim() + "\")");
                if (!string.IsNullOrEmpty(txtSerialNumber.Text)) Filters.Add("v_SerialNumber.Contains(\"" + txtSerialNumber.Text.Trim() + "\")");
                if (ddlCodicionStock.SelectedIndex == 1)
                {
                    Filters.Add("r_StockActual < r_StockMin ");
                }
                else if (ddlCodicionStock.SelectedIndex == 2)
                {
                    Filters.Add("r_StockActual > r_StockMax ");
                }
                //// Create the Filter Expression
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
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BindGrid()
        {
            var objData = GetData(0, null, strFilterExpression, ddlware.SelectedValue.ToString());
            grdData.DataSource = objData;
            grdData.DataBind();
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
    
        }

        private ProductWarehouseList[] GetData(int pintPageIndex,  int? pintPageSize, string pstrFilterExpression, string pintWarehouseId)
        {
            OperationResult objOperationResult = new OperationResult();
            MovementBL objLogistBL = new MovementBL();

            ProductWarehouseList[] _objData = objLogistBL.GetProductWarehousePagedAndFiltered(ref objOperationResult, pintPageIndex,   pintPageSize, strFilterExpression, pintWarehouseId).ToArray();
             
            return _objData;
        }

        private void grdData_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {

            Double StockActual = double.Parse(e.Row.Cells["r_StockActual"].Value.ToString());
            Double StockMinimo = double.Parse(e.Row.Cells["r_StockMin"].Value.ToString());
            Double StockMaximo = double.Parse(e.Row.Cells["r_StockMax"].Value.ToString());

            if (StockActual <StockMinimo)
            {
                e.Row.Appearance.BackColor = Color.Pink;
            }
            else if (StockActual > StockMaximo)
            {
                e.Row.Appearance.BackColor = Color.SkyBlue;
            } 
        }

        private void toolStripMenuView_Click(object sender, EventArgs e)
        {
            string strProductId = grdData.Selected.Rows[0].Cells[2].Value.ToString();
            string strWareHouseId = grdData.Selected.Rows[0].Cells[0].Value.ToString();
            frmMovementStock frm = new frmMovementStock(strProductId, strWareHouseId);
            frm.ShowDialog();

      
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
                    contextMenuStrip1.Items["toolStripMenuView"].Enabled = true;
                    contextMenuStrip1.Items["modificarStockMáximoYMínimoToolStripMenuItem"].Enabled = true;
                }
                else
                {
                    contextMenuStrip1.Items["toolStripMenuView"].Enabled = false;
                    contextMenuStrip1.Items["modificarStockMáximoYMínimoToolStripMenuItem"].Enabled = false;
                }
            } 
        }

        private void modificarStockMáximoYMínimoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strProductId = grdData.Selected.Rows[0].Cells[2].Value.ToString();
            string strWareHouseId = grdData.Selected.Rows[0].Cells[0].Value.ToString();
            frmStockMaxMin frm = new frmStockMaxMin(strWareHouseId,strProductId);
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }
     
    }
}
