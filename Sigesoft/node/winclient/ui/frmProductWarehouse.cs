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
    public partial class frmProductWarehouse : Form
    {
        ProductBL _objBL = new ProductBL();
        string strFilterExpression;
        string _strProductId;
        public frmProductWarehouse()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (txtBrand.Text.Trim() == "" && txtName.Text.Trim()=="" && txtSerialNumber.Text.Trim() =="")
            {
                MessageBox.Show("Por favor debe llenar alguno de los campos de búsqueda.", "VALIDACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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

            if (grdData1.Rows.Count > 0)
            {
                grdData1.Rows[0].Selected = true;
                grdData1.Select();
            }
        }

        private void BindGrid()
        {

            var objData = GetData(0, null, "v_Name ASC", strFilterExpression);

            grdData1.DataSource = objData;
            lblRecordCountGrdData1.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<ProductList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objBL.GetProductsPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void grdData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    _strProductId = grdData1.Selected.Rows[0].Cells["v_ProductId"].Value.ToString();
                    BindGridGrdData1andGrdData2(_strProductId);
                }
            }          
        }

        private void BindGridGrdData1andGrdData2(string strProductId)
        {
            MovementBL _objLogisticBL = new MovementBL();
            List<MovementDetailList> objMovementDetailList = new List<MovementDetailList>();
            List<ProductWarehouseList> objProductWarehouseList = new List<ProductWarehouseList>();
            OperationResult objOperationResult = new OperationResult();

            // Fill GrdData2  

            if (ddlQuantity.SelectedValue.ToString() == "-1")
            {
                objMovementDetailList = _objLogisticBL.GetMovementDetailListByProductId1(ref objOperationResult, 0, null, strProductId, 5000);           
            }
            else
            {
                objMovementDetailList = _objLogisticBL.GetMovementDetailListByProductId1(ref objOperationResult, 0, null, strProductId, int.Parse(ddlQuantity.SelectedValue.ToString()));
           
            }
           
            grdData2.DataSource = objMovementDetailList;
            lblRecordCountGrdData2.Text = string.Format("Se encontraron {0} registros.", objMovementDetailList.Count());

            // Fill GrdData2           
            objProductWarehouseList = _objLogisticBL.GetProductWarehousePagedAndFiltered1(ref objOperationResult, 0, null, strProductId);
            grdData3.DataSource = objProductWarehouseList;
            lblRecordCountGrdData3.Text = string.Format("Se encontraron {0} registros.", objProductWarehouseList.Count());

        }

        private void frmProductWarehouse_Load(object sender, EventArgs e)
        {
            List<KeyValueDTO> c = new List<KeyValueDTO>();
            c.Add(new KeyValueDTO { Id = "-1", Value1 = "--Todos--" });
            c.Add(new KeyValueDTO { Id = "10", Value1 = "10" });
            c.Add(new KeyValueDTO { Id = "20", Value1 = "20" });
            c.Add(new KeyValueDTO { Id = "30", Value1 = "30" });
            c.Add(new KeyValueDTO { Id = "50", Value1 = "50" });
            c.Add(new KeyValueDTO { Id = "100", Value1 = "100" });

                //Indicar qué propiedad se verá en la lista
                ddlQuantity.DisplayMember = "Value1";
                //Indicar qué valor tendrá cada ítem
                ddlQuantity.ValueMember = "Id";
                ddlQuantity.DataSource = c;
                //ddlQuantity.SelectedIndex = 1;
                ddlQuantity.SelectedValue = "10";
                txtName.Select();

         
                

        }

        private void ddlQuantity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridGrdData1andGrdData2(_strProductId);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = string.Empty;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdData2, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }           
        }

        private void grdData1_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grdData1.Selected.Rows.Count == 0) return;
            _strProductId = grdData1.Selected.Rows[0].Cells["v_ProductId"].Value.ToString();
            BindGridGrdData1andGrdData2(_strProductId);
            btnExport.Enabled = grdData2.Rows.Count > 0;
            btnExportPdf.Enabled = grdData2.Rows.Count > 0;
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = string.Empty;
            saveFileDialog1.Filter = "Files (*.pdf;*)|*.pdf;*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridDocumentExporter1.Export(this.grdData2, saveFileDialog1.FileName, Infragistics.Win.UltraWinGrid.DocumentExport.GridExportFileFormat.PDF);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
