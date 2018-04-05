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
    public partial class frmMovementStock : Form   
    {       
        string _ProductId;
        string _WarehouseId;
        MovementBL _objLogisticBL = new MovementBL();

        public frmMovementStock( string pstrProductId,string pstrWarehouseId)
        {
            _ProductId = pstrProductId;
            _WarehouseId = pstrWarehouseId;
            InitializeComponent();
        }

        private void frmMovementStock_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            this.BindGrid();

            MovementBL _objLogisticBL = new MovementBL();
            List<MovementDetailList> objMovementDetailList = new List<MovementDetailList>();
            OperationResult objOperationResult = new OperationResult();
            // Get the Entity Data           
            objMovementDetailList = _objLogisticBL.GetMovementDetailListByProductId(ref objOperationResult, 0, null, _ProductId, _WarehouseId);
            grdData.DataSource = objMovementDetailList;
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objMovementDetailList.Count());
        }

        private void BindGrid()
        {

            var objData = GetData(0, null, _ProductId, _WarehouseId);

            grdData.DataSource = objData;
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<MovementDetailList> GetData(int pintPageIndex, int? pintPageSize, string pstrProductId, string pstrWarehouse)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objLogisticBL.GetMovementDetailListByProductId(ref objOperationResult, pintPageIndex, pintPageSize, pstrProductId, pstrWarehouse);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
