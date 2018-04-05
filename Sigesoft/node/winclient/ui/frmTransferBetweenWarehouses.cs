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
    public partial class frmTransferBetweenWarehouses : Form
    {
        private string _Mode;
        string _MovementId;
        private string _WarehouseId;
        private int _rowIndex;
        bool _booAlreadySaved;

        List<MovementDetailList> _TempMovementDetailList = null;
        MovementDetailList _objMovementDetailList = null;
        List<movementdetailDto> _movementdetailListDto = null;

        public frmTransferBetweenWarehouses(string pstrWarehouseName,string pstrWarehouseId,string pstrMovementId,string pstrMode)
        {
            InitializeComponent();
            _Mode = pstrMode;
            _MovementId = pstrMovementId;
            _WarehouseId = pstrWarehouseId;

            if (_Mode == "New")
            {
                this.Text = this.Text + "Nueva Transferencia ";
            }
            else
            {
                this.Text = this.Text + "Editar Transferencia ";
            }
        }

        private void frmTransferBetweenWarehouses_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlProductId, "Value1", "Id", BLL.Utils.GetProductWarehouse(ref objOperationResult, "Id==" + "\"" + -1 + "\""), DropDownListAction.Select); // el combo producto se carga vacio hasta que se seleccione un almacén de origen.
            lblNodeSource.Text = Globals.ClientSession.v_CurrentExecutionNodeName;
            dtpDate.CustomFormat = "dd/MM/yyyy";
            if (_Mode == "New")
            {
                _booAlreadySaved = false;
                //Source                
                Utils.LoadDropDownList(ddlOrganizationLocationSourceId, "Value1", "Id", BLL.Utils.GetJoinOrganizationAndLocationNotInRestricted(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.Select);
                Utils.LoadDropDownList(ddlWarehouseSourceId, "Value1", "Id", BLL.Utils.GetWarehouseNotInRestricted(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId, "-1", "-1"), DropDownListAction.Select);

                //Destination
                Utils.LoadDropDownList(ddlNodeDestinationId, "Value1", "Id", BLL.Utils.GetAllNodeForCombo(ref objOperationResult), DropDownListAction.Select);
                if (rbLocal.Checked == true)
                {
                    ddlNodeDestinationId.Enabled = false;
                    ddlNodeDestinationId.SelectedValue = Globals.ClientSession.i_CurrentExecutionNodeId.ToString();
                }
                else
                {
                    ddlNodeDestinationId.Enabled = true;
                }

                Utils.LoadDropDownList(ddlOrganizationLocationDestinationId, "Value1", "Id", BLL.Utils.GetJoinOrganizationAndLocationNotInRestricted(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.Select);
                Utils.LoadDropDownList(ddlWarehouseDestinationId, "Value1", "Id", BLL.Utils.GetWarehouseNotInRestricted(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId, "-1", "-1"), DropDownListAction.Select);

                txtIsProcessed.Text = "NO";
                txtDocReference.Text = "";
                dtpDate.Value = DateTime.Now.Date;
            }
            else if (_Mode == "Edit")
            {
                // Get the Entity Data

                warehouseDto objwarehouseDto = new warehouseDto();
                WarehouseBL objWarehouseBL = new WarehouseBL();

                movementDto objMovementDto = new movementDto();
                MovementBL objMovementBL = new MovementBL();

                movementdetailDto objmovementdetailDto = new movementdetailDto();

                supplierDto objSupplierDto = new supplierDto();
                SupplierBL objSupplierBL = new SupplierBL();

                nodeorganizationlocationwarehouseprofileDto objnodeorganizationlocationwarehouseprofileDto = new nodeorganizationlocationwarehouseprofileDto();
                NodeBL objNodeBL = new NodeBL();
                _booAlreadySaved = true;
                string pstrFilterExpression = null;

                objMovementDto = objMovementBL.GetMovement(ref objOperationResult, _MovementId);

                //Source
                Utils.LoadDropDownList(ddlOrganizationLocationSourceId, "Value1", "Id", BLL.Utils.GetJoinOrganizationAndLocationNotInRestricted(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.Select);
                objwarehouseDto = objWarehouseBL.GetWarehouse(ref objOperationResult, objMovementDto.v_WarehouseId);
                ddlOrganizationLocationSourceId.SelectedValue = Globals.ClientSession.i_CurrentExecutionNodeId+ "|"+objwarehouseDto.v_OrganizationId+"|"+objwarehouseDto.v_LocationId;
                ddlWarehouseSourceId.SelectedValue = objwarehouseDto.v_WarehouseId;

                //Destination
                Utils.LoadDropDownList(ddlNodeDestinationId, "Value1", "Id", BLL.Utils.GetAllNodeForCombo(ref objOperationResult), DropDownListAction.Select);                
          
                objnodeorganizationlocationwarehouseprofileDto=  objNodeBL.GetNodeOrganizationLocationWarehouseProfile(ref objOperationResult, objMovementDto.v_RemoteWarehouseId);

                if (Globals.ClientSession.i_CurrentExecutionNodeId.ToString() == objnodeorganizationlocationwarehouseprofileDto.i_NodeId.ToString())
                {
                    ddlNodeDestinationId.Enabled = false;
                    rbLocal.Checked = true;
                }
                else
                {
                    ddlNodeDestinationId.Enabled = true;
                    rbRemote.Checked = true;
                }
                
                ddlNodeDestinationId.SelectedValue = objnodeorganizationlocationwarehouseprofileDto.i_NodeId.ToString();
                ddlOrganizationLocationDestinationId.SelectedValue = objnodeorganizationlocationwarehouseprofileDto.i_NodeId.ToString() + "|" + objnodeorganizationlocationwarehouseprofileDto.v_OrganizationId + "|" + objnodeorganizationlocationwarehouseprofileDto.v_LocationId;
                ddlWarehouseDestinationId.SelectedValue = objMovementDto.v_RemoteWarehouseId;                
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
                }

                pstrFilterExpression = "v_MovementId==" + "\"" + objMovementDto.v_MovementId + "\"" + "&&" + "v_WarehouseId==" + "\"" + objMovementDto.v_WarehouseId + "\"";
                _TempMovementDetailList = objMovementBL.GetMovementDeatilPagedAndFiltered(ref objOperationResult, 0, null, "", pstrFilterExpression);
                lblRecordCount.Text = string.Format("Se encontraron {0} registros.", _TempMovementDetailList.Count());

                grdData.DataSource = _TempMovementDetailList;
            }
        }

        private void ddlOrganizationLocationSourceId_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadcbWarehouseSource();
        }

        private void LoadcbWarehouseSource()
        {
            OperationResult objOperationResult = new OperationResult();
            var index = ddlOrganizationLocationSourceId.SelectedIndex;

            if (index == 0)
            {
                Utils.LoadDropDownList(ddlWarehouseSourceId, "Value1", "Id", BLL.Utils.GetWarehouseNotInRestricted(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId, "-1", "-1"), DropDownListAction.Select);
                return;
            }

            var dataList = ddlOrganizationLocationSourceId.SelectedValue.ToString().Split('|');
            string idOrg = dataList[1];
            string idLoc = dataList[2];

            Utils.LoadDropDownList(ddlWarehouseSourceId, "Value1", "Id", BLL.Utils.GetWarehouseNotInRestricted(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId, idOrg, idLoc), DropDownListAction.Select);
        }

        private void ddlOrganizationLocationDestinationId_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadcbWarehouseDestination();
        }

        private void LoadcbWarehouseDestination()
        {
            OperationResult objOperationResult = new OperationResult();
            var index = ddlOrganizationLocationDestinationId.SelectedIndex;
            if (index == 0 || index == -1)
            {
                Utils.LoadDropDownList(ddlWarehouseDestinationId, "Value1", "Id", BLL.Utils.GetWarehouseNotInRestricted(ref objOperationResult,int.Parse(ddlNodeDestinationId.SelectedValue.ToString()), "-1", "-1"), DropDownListAction.Select);
                return;
            }
            var dataList = ddlOrganizationLocationDestinationId.SelectedValue.ToString().Split('|');
            string idOrg = dataList[1];
            string idLoc = dataList[2];

            var objData1 = BLL.Utils.GetWarehouseNotInRestricted(ref objOperationResult, int.Parse(ddlNodeDestinationId.SelectedValue.ToString()), idOrg, idLoc);  // Motivos de Ingreso
            
            var objData1List = objData1.ToList();
            if (_Mode =="New")
            {
                objData1List.RemoveAll(item => item.Id == ddlWarehouseSourceId.SelectedValue.ToString());
            }            

            Utils.LoadDropDownList(ddlWarehouseDestinationId, "Value1", "Id", objData1List, DropDownListAction.Select);
        }

        private void txtProductSearch_TextChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlProductId, "Value1", "Id", BLL.Utils.GetProductWarehouse(ref objOperationResult, "Value2==" + "\"" + ddlWarehouseSourceId.SelectedValue.ToString() + "\"" + " && " + "Value1.Contains(\"" + txtProductSearch.Text.Trim() + "\")"), DropDownListAction.Select);          
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

        private void rbLocal_CheckedChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            Utils.LoadDropDownList(ddlNodeDestinationId, "Value1", "Id", BLL.Utils.GetAllNodeForCombo(ref objOperationResult), DropDownListAction.Select);           
            ddlNodeDestinationId.SelectedValue = Globals.ClientSession.i_CurrentExecutionNodeId.ToString();
            
            ddlNodeDestinationId.Enabled = false;
        }

        private void rbRemote_CheckedChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            var objData = BLL.Utils.GetAllNodeForCombo(ref objOperationResult);
            var objDataList = objData.ToList();
            objDataList.RemoveAll(item => item.Id == Globals.ClientSession.i_CurrentExecutionNodeId.ToString());
            Utils.LoadDropDownList(ddlNodeDestinationId, "Value1", "Id", objDataList, DropDownListAction.Select);
            ddlNodeDestinationId.Enabled = true;
        }

        private void ddlNodeDestinationId_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (ddlNodeDestinationId.DataSource ==  null)
            {
                return;
            }
            Utils.LoadDropDownList(ddlOrganizationLocationDestinationId, "Value1", "Id", BLL.Utils.GetJoinOrganizationAndLocationNotInRestricted(ref objOperationResult, int.Parse(ddlNodeDestinationId.SelectedValue.ToString())), DropDownListAction.Select);           
        }

        private void ddlWarehouseSourceId_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (ddlWarehouseSourceId.SelectedValue == null) return;
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlProductId, "Value1", "Id", BLL.Utils.GetProductWarehouse(ref objOperationResult, "Id==" + "\"" + ddlWarehouseSourceId.SelectedValue.ToString() + "\""), DropDownListAction.Select);
            txtProductSearch.Text = "";

            if (_TempMovementDetailList != null)
            {
                DialogResult Result = MessageBox.Show("Si se cambia el Almcén de origen se borrara todo el detalle de la transferencia.¿Desea cambiar de Almacén de origen?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    ddlOrganizationLocationDestinationId.SelectedValue = "-1";
                    _TempMovementDetailList = new List<MovementDetailList>();
                    grdData.DataSource = _TempMovementDetailList;
                }
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (uvAddItem.Validate(true, false).IsValid)
            {
                OperationResult objOperationResult = new OperationResult();

                ProductBL objProductBL = new ProductBL();
                List<ProductList> objProductList = new List<ProductList>();

                var x = (KeyValueDTO)ddlProductId.SelectedItem;

                if (Single.Parse(txtQuantity.Text.ToString()) ==0)
                {
                    MessageBox.Show("La cantidad debe ser mayor a 0", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (x.Value4 < Single.Parse(txtQuantity.Text.ToString()) )
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

            if (uiElement == null || uiElement.Parent == null)return;

            // Capturar valor de una celda especifica al hace click derecho sobre la celda k se quiere su valor
            Infragistics.Win.UltraWinGrid.UltraGridCell cell = (Infragistics.Win.UltraWinGrid.UltraGridCell)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridCell));
            Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

            if (row != null)
            {
                grdData.Rows[row.Index].Selected = true;
                contextMenuGrdData.Items["removerToolStripMenuItem"].Enabled = true;
                _rowIndex = row.Index;
            }
            else
            {
                contextMenuGrdData.Items["removerToolStripMenuItem"].Enabled = false;
            }
        }

        private void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            if (uvTransferWarehouse.Validate(true, false).IsValid)
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

            if (_Mode == "New")
            {
                //Source
                objmovementDto.v_WarehouseId = ddlWarehouseSourceId.SelectedValue.ToString();

                //Destination
                objmovementDto.v_RemoteWarehouseId = ddlWarehouseDestinationId.SelectedValue.ToString();

                if (rbLocal.Checked)
                {
                    objmovementDto.i_MotiveTypeId = 19; //EGRESO POR TRANSFERENCIA DE ALMACENES INTERNOS
                    objmovementDto.i_ProcessTypeId = (int)Common.ProcessType.LOCAL;
                }
                else if (rbRemote.Checked)
                {
                    objmovementDto.i_MotiveTypeId = 20; //EGRESO POR TRANSFERENCIA DE ALMACENES EXTERNOS
                    objmovementDto.i_ProcessTypeId = (int)Common.ProcessType.REMOTO;
                }     
           
                objmovementDto.d_Date = dtpDate.Value;
                objmovementDto.i_MovementTypeId = (int)Common.MovementType.EGRESO; // EGRESO DE ALMACÉN
                objmovementDto.v_ReferenceDocument = txtDocReference.Text.Trim();
                objmovementDto.i_IsLocallyProcessed = (int)Common.SiNo.NO; // El movimiento no está procesado aún Localmente
                objmovementDto.i_IsRemoteProcessed = (int)Common.SiNo.NO; // El movimiento no está procesado aún Remotamente

                if (_TempMovementDetailList == null || _TempMovementDetailList.Count == 0)
                {
                    _movementdetailListDto = null;
                    MessageBox.Show("El detalle no puede estar vacio.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return  false;
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

                _MovementId = objMovementBL.AddMovement(ref objOperationResult, objmovementDto, _movementdetailListDto, Globals.ClientSession.GetAsList());
            }
            else if (_Mode == "Edit")
            {
                objmovementDto.v_MovementId = _MovementId;
                //Source
                objmovementDto.v_WarehouseId = ddlWarehouseSourceId.SelectedValue.ToString();

                //Destination
                objmovementDto.v_RemoteWarehouseId = ddlWarehouseDestinationId.SelectedValue.ToString();

                if (rbLocal.Checked)
                {
                    objmovementDto.i_MotiveTypeId = 19; //EGRESO POR TRANSFERENCIA DE ALMACENES INTERNOS
                    objmovementDto.i_ProcessTypeId = (int)Common.ProcessType.LOCAL;
                }
                else if (rbRemote.Checked)
                {
                    objmovementDto.i_MotiveTypeId = 20; //EGRESO POR TRANSFERENCIA DE ALMACENES EXTERNOS
                    objmovementDto.i_ProcessTypeId = (int)Common.ProcessType.REMOTO;
                }              
                objmovementDto.d_Date = dtpDate.Value;
                objmovementDto.r_TotalQuantity = 0;
                objmovementDto.i_MovementTypeId = (int)Common.MovementType.EGRESO; // EGRESO DE ALMACÉN
                objmovementDto.v_ReferenceDocument = txtDocReference.Text.Trim();
                objmovementDto.i_IsLocallyProcessed = (int)Common.SiNo.NO; ; // El movimiento no está procesado aún

                if (_TempMovementDetailList == null || _TempMovementDetailList.Count == 0)
                {
                    _movementdetailListDto = null;
                    MessageBox.Show("El detalle no puede estar vacio.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return  false;
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
            return booResult;
        }

        private void btnConfirmProcess_Click(object sender, EventArgs e)
        {
            if (uvTransferWarehouse.Validate(true, false).IsValid)
            {
                //Guardar y Confirmar
                ConfirmMovement();
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConfirmMovement()
        {
            OperationResult objOperationResult = new OperationResult();
            MovementBL objMovementBL = new MovementBL();
            movementDto objmovementDto = new movementDto();

            // Primero se debe guardar todo el movimiento
            if (!SaveMainEntity())
            {
                MessageBox.Show("Hubo errores al guardar el movimiento. No se puede procesar.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };
            
            if (rbLocal.Checked)
            {
                objmovementDto.i_MotiveTypeId = 19; //EGRESO POR TRANSFERENCIA DE ALMACENES INTERNOS
                objmovementDto.i_ProcessTypeId = (int)Common.ProcessType.LOCAL;
            }
            else if (rbRemote.Checked)
            {
                objmovementDto.i_MotiveTypeId = 20; //EGRESO POR TRANSFERENCIA DE ALMACENES EXTERNOS
                objmovementDto.i_ProcessTypeId = (int)Common.ProcessType.REMOTO;
            }              

            objmovementDto.v_SupplierId = null;
            objmovementDto.d_Date = dtpDate.Value;
            objmovementDto.v_WarehouseId = ddlWarehouseDestinationId.SelectedValue.ToString();
            objmovementDto.v_RemoteWarehouseId = ddlWarehouseDestinationId.SelectedValue.ToString();
            objmovementDto.v_ReferenceDocument = txtDocReference.Text.Trim(); ;
            objmovementDto.i_IsLocallyProcessed = (int)Common.SiNo.SI;  //El movimiento inicia como no procesado
            objmovementDto.i_MovementTypeId = (int)Common.MovementType.INGRESO; //(Ingreso, Egreso)

            objMovementBL.ProcessTransfer(ref objOperationResult,objmovementDto,_movementdetailListDto, Globals.ClientSession.i_CurrentExecutionNodeId, int.Parse(ddlNodeDestinationId.SelectedValue.ToString()), _MovementId, Globals.ClientSession.GetAsList());

            //// Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;

                if (objOperationResult.ErrorMessage != null)
                {
                    MessageBox.Show("Informacion :" + System.Environment.NewLine + objOperationResult.ErrorMessage, "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Se procesó el movimiento de ingreso satisfactoriamente. Los stocks del Almacén han sido actualizados.", "CORRECTO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }               
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

    }
}
