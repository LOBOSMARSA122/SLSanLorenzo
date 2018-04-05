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
    public partial class frmStockMaxMin : Form
    {
        MovementBL objLogistBL = new MovementBL();
        string _ProductId;
        string _WarehouseId;

        public frmStockMaxMin(string pstrWarehouseId, string pstrProductId)
        {
            InitializeComponent();
            _ProductId = pstrProductId;
            _WarehouseId = pstrWarehouseId;
        }

        private void txtStockMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }


            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txtStockMax.Text.Length; i++)
            {
                if (txtStockMax.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }


            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void txtStockMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txtStockMin.Text.Length; i++)
            {
                if (txtStockMin.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }


            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void frmStockMaxMin_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            productwarehouseDto objProductWarehouseDto = new productwarehouseDto();
            objProductWarehouseDto = objLogistBL.GetProductWarehouse(ref objOperationResult, _WarehouseId, _ProductId);
            txtStockMax.Text = objProductWarehouseDto.r_StockMax.ToString();
            txtStockMin.Text = objProductWarehouseDto.r_StockMin.ToString(); ;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (uvStockMinMax.Validate(true, false).IsValid)
            {
            
             OperationResult objOperationResult = new OperationResult();
            productwarehouseDto objProductWarehouseDto = new productwarehouseDto();
           
            objProductWarehouseDto = objLogistBL.GetProductWarehouse(ref objOperationResult, _WarehouseId, _ProductId);
            objProductWarehouseDto.r_StockMax = float.Parse(txtStockMax.Text);
            objProductWarehouseDto.r_StockMin = float.Parse(txtStockMin.Text);

            objLogistBL.UpdateProductWarehouse(ref objOperationResult, objProductWarehouseDto, Globals.ClientSession.GetAsList());
            //// Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else  // Operación con error
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Se queda en el formulario.
            }
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
