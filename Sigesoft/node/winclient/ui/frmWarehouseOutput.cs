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
    public partial class frmWarehouseOutput : Form
    {
        private string _Mode;
        string _MovementId;
        private string _WarehouseId;
        private int _rowIndex;
        bool _booAlreadySaved;

        List<MovementDetailList> _TempMovementDetailList = null;
        MovementDetailList _objMovementDetailList = null;
        List<movementdetailDto> _movementdetailListDto = null;

        public frmWarehouseOutput(string pstrWarehouseName, string pstrWarehouseId, string pstrMovementId, string pstrMode)
        {
            InitializeComponent();
            _Mode = pstrMode;
            _MovementId = pstrMovementId;
            _WarehouseId = pstrWarehouseId;

            if (_Mode == "New")
            {
                this.Text = this.Text + "Nuevo Movimiento de Egreso - Almacén: " + pstrWarehouseName ;
            }
            else
            {
                this.Text = this.Text + "Editar Movimiento de Egreso - Almacén: " + pstrWarehouseName + "/ Id: " + pstrWarehouseId;
            }
        }

        private void frmWarehouseOutput_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            OperationResult objOperationResult = new OperationResult();

            var objData1 = BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 110, "");  // Motivos de Ingreso
            // Remover el elemento TRANSFERENCIA ENTRE ALMACENES
            var objData1List = objData1.ToList();
            objData1List.RemoveAll(item => Int32.Parse(item.Id) < 11);
            objData1List.RemoveAll(item => Int32.Parse(item.Id) > 18);
            //// Llenar el combo de Motivos
            Utils.LoadDropDownList(ddlMotiveId, "Value1", "Id", objData1List, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlProductId, "Value1", "Id", BLL.Utils.GetProductWarehouse(ref objOperationResult, "Value2==" + "\"" + _WarehouseId + "\""), DropDownListAction.Select);
            if (_Mode == "New")
            {
                txtIsProcessed.Text = "NO";
                txtDocReference.Text = "";
                dtpDate.Value = DateTime.Now.Date;
                _booAlreadySaved = false;

            }
            else if (_Mode == "Edit")
            {
                // Get the Entity Data

                movementDto objMovementDto = new movementDto();
                MovementBL objMovementBL = new MovementBL();

                movementdetailDto objmovementdetailDto = new movementdetailDto();

                supplierDto objSupplierDto = new supplierDto();
                SupplierBL objSupplierBL = new SupplierBL();

                string pstrFilterExpression = null;

                objMovementDto = objMovementBL.GetMovement(ref objOperationResult, _MovementId);

                ddlMotiveId.SelectedValue = objMovementDto.i_MotiveTypeId.ToString();
                txtDocReference.Text = objMovementDto.v_ReferenceDocument;
                dtpDate.Value = (DateTime)objMovementDto.d_Date;
                if (objMovementDto.i_IsLocallyProcessed == (int)Common.SiNo.NO)
                {
                    txtIsProcessed.Text = "NO";
                    btnSaveRefresh.Enabled = true;
                    btnConfirmProcess.Enabled = true;
                    btnDiscardProcess.Enabled = true;
                }
                else
                {
                    txtIsProcessed.Text = "SI";
                    btnSaveRefresh.Enabled = false;
                    btnConfirmProcess.Enabled = false;
                    btnDiscardProcess.Enabled = false;
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = false;
                }

                pstrFilterExpression = "v_MovementId==" + "\"" + objMovementDto.v_MovementId + "\"" + "&&" + "v_WarehouseId==" + "\"" + objMovementDto.v_WarehouseId + "\"";
                _TempMovementDetailList = objMovementBL.GetMovementDeatilPagedAndFiltered(ref objOperationResult, 0, null, "", pstrFilterExpression);
                lblRecordCount.Text = string.Format("Se encontraron {0} registros.", _TempMovementDetailList.Count());

                grdData.DataSource = _TempMovementDetailList;
                _booAlreadySaved = true;
            }

        }

        private void txtProductSearch_TextChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlProductId, "Value1", "Id", BLL.Utils.GetProductWarehouse(ref objOperationResult, "Value2==" + "\"" + _WarehouseId + "\"" + " && " +  "Value1.Contains(\"" + txtProductSearch.Text.Trim() + "\")"), DropDownListAction.Select);          
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txtQuantity.Text.Length; i++)
            {
                if (txtQuantity.Text[i] == '.')
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
            
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (uvAddItem.Validate(true, false).IsValid)
            {
                OperationResult objOperationResult = new OperationResult();

                ProductBL objProductBL = new ProductBL();
                List<ProductList> objProductList = new List<ProductList>();

                var x = (KeyValueDTO)ddlProductId.SelectedItem;

                if (x.Value4 < Single.Parse(txtQuantity.Text.ToString()))
                {
                    MessageBox.Show("La cantidad es mayor al stock actual, por favor ingrese una cantidad igual o menor al stock actual", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_TempMovementDetailList == null)
                {
                    _TempMovementDetailList = new List<MovementDetailList>();
                }
                grdData.DataSource = new MovementDetailList();
                _objMovementDetailList = new MovementDetailList();
                objProductList = objProductBL.GetProductsPagedAndFiltered(ref objOperationResult, 0, null, "", "v_ProductId==" + "\"" + ddlProductId.SelectedValue.ToString() + "\"");

                //Buscar si un producto ya esta en la Grilla
                var findResult = _TempMovementDetailList.Find(p => p.v_ProductId == ddlProductId.SelectedValue.ToString());
                if (findResult == null)
                {
                    _objMovementDetailList.v_ProductId = objProductList[0].v_ProductId;
                    _objMovementDetailList.v_ProductName = objProductList[0].v_Name;
                    _objMovementDetailList.v_CategoryName = objProductList[0].v_CategoryName;
                    _objMovementDetailList.v_Brand = objProductList[0].v_Brand;
                    _objMovementDetailList.v_Model = objProductList[0].v_Model;
                    _objMovementDetailList.v_SerialNumber = objProductList[0].v_SerialNumber;
                    _objMovementDetailList.r_Quantity = float.Parse(txtQuantity.Text.Trim().ToString());
                    _TempMovementDetailList.Add(_objMovementDetailList);
                    grdData.DataSource = _TempMovementDetailList;
                }
                else
                {
                    var findIndex = _TempMovementDetailList.FindIndex(p => p.v_ProductId == ddlProductId.SelectedValue.ToString());

                    _objMovementDetailList.v_ProductId = objProductList[0].v_ProductId;
                    _objMovementDetailList.v_ProductName = objProductList[0].v_Name;
                    _objMovementDetailList.v_CategoryName = objProductList[0].v_CategoryName;
                    _objMovementDetailList.v_Brand = objProductList[0].v_Brand;
                    _objMovementDetailList.v_Model = objProductList[0].v_Model;
                    _objMovementDetailList.v_SerialNumber = objProductList[0].v_SerialNumber;
                    _objMovementDetailList.r_Quantity = float.Parse(txtQuantity.Text.Trim().ToString()) + findResult.r_Quantity;

                    bool Result;
                    if (x.Value4 < _objMovementDetailList.r_Quantity)
                    {
                        _objMovementDetailList.r_Quantity = _objMovementDetailList.r_Quantity - float.Parse(txtQuantity.Text.Trim().ToString());
                        Result = true;
                    }
                    else
                    {
                        Result = false;
                    }
                    
                    _TempMovementDetailList.Add(_objMovementDetailList);
                    _TempMovementDetailList.RemoveAt(findIndex);
                    grdData.DataSource = _TempMovementDetailList;

                    if (Result)
                    {
                        MessageBox.Show("La cantidad es mayor al stock actual, por favor ingrese una cantidad igual o menor al stock actual", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                       
                }
                grdData.Refresh();
                lblRecordCount.Text = string.Format("Se encontraron {0} registros.", _TempMovementDetailList.Count());               
                txtQuantity.Text = "";
                txtProductSearch.Text = "";
                ddlProductId.SelectedValue = "-1";
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void removerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _TempMovementDetailList.RemoveAt(_rowIndex);
            grdData.Refresh();
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", _TempMovementDetailList.Count());
      
        }

        private void grdData_MouseDown(object sender, MouseEventArgs e)
        {
            Point point = new System.Drawing.Point(e.X, e.Y);
            Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

            if (uiElement == null || uiElement.Parent == null)
                return;

            // Capturar valor de una celda especifica al hace click derecho sobre la celda k se quiere su valor
            Infragistics.Win.UltraWinGrid.UltraGridCell cell = (Infragistics.Win.UltraWinGrid.UltraGridCell)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridCell));
            Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

            if (row != null)
            {
                grdData.Rows[row.Index].Selected = true;
                if (txtIsProcessed.Text == "NO")
                {
                    contextMenuGrdData.Items["removerToolStripMenuItem"].Enabled = true;
                }
                else if (txtIsProcessed.Text == "SI")
                {
                    contextMenuGrdData.Items["removerToolStripMenuItem"].Enabled = false;
                }                
                _rowIndex = row.Index;
            }
            else
            {
                contextMenuGrdData.Items["removerToolStripMenuItem"].Enabled = false;
            }
        }

        private void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            if (uvWarehouseOutPut.Validate(true, false).IsValid)
            {
                // Guardar y refrescar.
                SaveMainEntity();
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }           
        }

        private bool SaveMainEntity()
        {
            OperationResult objOperationResult = new OperationResult();
            movementDto objmovementDto = new movementDto();
            MovementBL objMovementBL = new MovementBL();
            _movementdetailListDto = new List<movementdetailDto>();
            bool booResult = false;

            //if (uvWarehouseOutPut.Validate(true, false).IsValid)
            //{
                if (_Mode == "New")
                {
                    objmovementDto.i_MotiveTypeId = int.Parse(ddlMotiveId.SelectedValue.ToString());

                    objmovementDto.d_Date = dtpDate.Value;
                    objmovementDto.v_WarehouseId = _WarehouseId;
                    objmovementDto.v_ReferenceDocument = txtDocReference.Text.Trim();
                    objmovementDto.i_IsLocallyProcessed = (int)Common.SiNo.NO; ; // El movimiento no está procesado aún
                    objmovementDto.i_ProcessTypeId = (int)Common.ProcessType.LOCAL;// Procesado Localmente
                    objmovementDto.i_MovementTypeId = (int)Common.MovementType.EGRESO; // EGRESO DE ALMACÉN
                    objmovementDto.r_TotalQuantity = 0;
                     if (_TempMovementDetailList == null || _TempMovementDetailList.Count == 0)
                    {
                        _movementdetailListDto = null;
                        MessageBox.Show("El detalle no puede estar vacio.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                  
                        foreach (var item in _TempMovementDetailList)
                        {
                            movementdetailDto objmovementdetailDto = new movementdetailDto();

                            objmovementdetailDto.v_ProductId = item.v_ProductId;
                            objmovementdetailDto.r_Quantity = item.r_Quantity;
                            objmovementdetailDto.r_Price = item.r_Price;
                            objmovementdetailDto.i_MovementTypeId = item.i_MovementTypeId;
                            _movementdetailListDto.Add(objmovementdetailDto);
                            objmovementDto.r_TotalQuantity = objmovementDto.r_TotalQuantity + item.r_Quantity;
                        }

                 _MovementId= objMovementBL.AddMovement(ref objOperationResult, objmovementDto, _movementdetailListDto, Globals.ClientSession.GetAsList());
                }
                 else if (_Mode == "Edit")
	            {
                    objmovementDto.v_MovementId = _MovementId;
                    objmovementDto.i_MotiveTypeId = int.Parse(ddlMotiveId.SelectedValue.ToString());

                    objmovementDto.d_Date = dtpDate.Value;
                    objmovementDto.v_WarehouseId = _WarehouseId;
                    objmovementDto.v_ReferenceDocument = txtDocReference.Text.Trim();
                    objmovementDto.i_IsLocallyProcessed = (int)Common.SiNo.NO; ; // El movimiento no está procesado aún
                    objmovementDto.i_ProcessTypeId = (int)Common.ProcessType.LOCAL;// Procesado Localmente
                    objmovementDto.i_MovementTypeId = (int)Common.MovementType.EGRESO; // EGRESO DE ALMACÉN
                    objmovementDto.r_TotalQuantity = 0;
                    if (_TempMovementDetailList == null || _TempMovementDetailList.Count == 0)
                    {
                        _movementdetailListDto = null;
                        MessageBox.Show("El detalle no puede estar vacio.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    foreach (var item in _TempMovementDetailList)
                    {
                        movementdetailDto objmovementdetailDto = new movementdetailDto();

                        objmovementdetailDto.v_ProductId = item.v_ProductId;
                        objmovementdetailDto.r_Quantity = item.r_Quantity;
                        objmovementdetailDto.r_Price = item.r_Price;
                        objmovementdetailDto.i_MovementTypeId = item.i_MovementTypeId;
                        _movementdetailListDto.Add(objmovementdetailDto);
                        objmovementDto.r_TotalQuantity = objmovementDto.r_TotalQuantity + item.r_Quantity;
                    }
                    objMovementBL.UpdateMovement(ref objOperationResult, objmovementDto, _movementdetailListDto, Globals.ClientSession.GetAsList());

	            }
                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                    booResult = true;
                }
                else  // Operación con error
                {
                    MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Se queda en el formulario.
                    booResult = false;
                }
            //}
            //else
            //{
            //    MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
                return booResult;
        }

        private void ConfirmMovement()
        {
            MovementBL objMovementBL = new MovementBL();
            // Primero se debe guardar todo el movimiento
            if (!SaveMainEntity())
            {
                MessageBox.Show("Hubo errores al guardar el movimiento. No se puede procesar.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };

            OperationResult objOperationResult = new OperationResult();
            objMovementBL.ProcessMovementEgreso(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId, _MovementId, Globals.ClientSession.GetAsList());

            //// Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                MessageBox.Show("Se procesó el movimiento de ingreso satisfactoriamente. Los stocks del Almacén han sido actualizados.", "CORRECTO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                this.Close();
            }
            else  // Operación con error
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Se queda en el formulario.
            }
        }

        private void btnDiscardProcess_Click(object sender, EventArgs e)
        {
            this.DiscardMovement();
        }

        private void DiscardMovement()
        {
            OperationResult objOperationResult = new OperationResult();
            bool booAlreadySaved = _booAlreadySaved;

            if (!booAlreadySaved)
            {
                // Si es un nuevo movimiento (no ha sido grabado). Cerrar la ventana solamente.
                this.Close();
            }
            else
            {
                //// Si es un movimiento que ya fué grabado en este pantalla.  Eliminarlo de la BD
                MovementBL objMovementBL = new MovementBL();

                objMovementBL.DiscardMovement(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId, _MovementId, Globals.ClientSession.GetAsList());
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
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirmProcess_Click(object sender, EventArgs e)
        {
            if (uvWarehouseOutPut.Validate(true, false).IsValid)
            {
                //Guardar y Confirmar
                ConfirmMovement();
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

    }
}
