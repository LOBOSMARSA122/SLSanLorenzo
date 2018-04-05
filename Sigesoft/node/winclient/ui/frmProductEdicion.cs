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
    public partial class frmProductEdicion : Form
    {
        ProductBL _objBL = new ProductBL();
        productDto _productDto = new productDto();
        string _ProductId, _Mode;

        public frmProductEdicion(string pstrProductId, string pstrMode)
        {
            InitializeComponent();
            this.Text = this.Text + " (" + pstrProductId + ")";
            _ProductId = pstrProductId;
            _Mode = pstrMode;
        }

        private void frmProductEdicion_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            OperationResult objOperationResult = new OperationResult();
            ////Llenado de combos
            Utils.LoadComboTreeBoxList(ddlCategoryId, BLL.Utils.GetDataHierarchyForComboTreeBox(ref objOperationResult, 103, null), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlMeasurementUnitId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 105, null), DropDownListAction.Select);

            dtpExpirationDate.CustomFormat = "dd/MM/yyyy";

            if (_Mode == "New")
            {
                // Additional logic here.

            }
            else if (_Mode == "Edit")
            {
                // Get the Entity Data

                _productDto = _objBL.GetProduct(ref objOperationResult, _ProductId);

                ComboTreeNode nodoABuscar = ddlCategoryId.AllNodes.First(x => x.Tag.ToString() == _productDto.i_CategoryId.ToString());
                ddlCategoryId.SelectedNode = nodoABuscar;
                txtName.Text = _productDto.v_Name;
                txtGenericName.Text = _productDto.v_GenericName;
                txtBarCode.Text = _productDto.v_BarCode;
                txtBarCode.Text = _productDto.v_ProductCode;
                txtBrand.Text = _productDto.v_Brand;
                txtModel.Text = _productDto.v_Model;
                txtSerialNumber.Text = _productDto.v_SerialNumber;
                if (_productDto.d_ExpirationDate == null)
                {
                    dtpExpirationDate.Checked = false;
                }
                else
                {
                    dtpExpirationDate.Checked = true;
                    dtpExpirationDate.Value = (DateTime)_productDto.d_ExpirationDate;
                }
                
                ddlMeasurementUnitId.SelectedValue = _productDto.i_MeasurementUnitId.ToString();
                txtReferentialCostPrice.Text = _productDto.r_ReferentialCostPrice.ToString();
                txtReferentialSalesPrice.Text = _productDto.r_ReferentialSalesPrice.ToString();
                txtPresentation.Text = _productDto.v_Presentation;
                txtAdiccionalInformation.Text = _productDto.v_AdditionalInformation;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (uvProduct.Validate(true, false).IsValid)
            {
                if (txtName.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para el producto.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_Mode == "New")
                {
                    _productDto = new productDto();

                    _productDto.i_CategoryId = Convert.ToInt32(ddlCategoryId.SelectedNode.Tag);
                    _productDto.v_Name = txtName.Text.Trim();
                    _productDto.v_GenericName = txtGenericName.Text.Trim();
                    _productDto.v_BarCode = txtBarCode.Text.Trim();
                    _productDto.v_ProductCode = txtBarCode.Text.Trim();
                    _productDto.v_Brand = txtBrand.Text.Trim();
                    _productDto.v_Model = txtModel.Text.Trim();
                    _productDto.v_SerialNumber = txtSerialNumber.Text.Trim();
                    //_productDto.d_ExpirationDate = string.IsNullOrEmpty(dtpExpirationDate.Text) ? (DateTime?)null : DateTime.Parse(dtpExpirationDate.Text);

                    if (dtpExpirationDate.Checked == false)
                    {
                        _productDto.d_ExpirationDate = null;
                    }
                    else
                    {
                        _productDto.d_ExpirationDate = dtpExpirationDate.Value;
                    }                    
                    _productDto.i_MeasurementUnitId = Convert.ToInt32(ddlMeasurementUnitId.SelectedValue);
                    _productDto.r_ReferentialCostPrice = string.IsNullOrEmpty(txtReferentialCostPrice.Text) ? (float?)null : float.Parse(txtReferentialCostPrice.Text);
                    _productDto.r_ReferentialSalesPrice = string.IsNullOrEmpty(txtReferentialSalesPrice.Text) ? (float?)null : float.Parse(txtReferentialSalesPrice.Text);
                    _productDto.v_Presentation = txtPresentation.Text.Trim();
                    _productDto.v_AdditionalInformation = txtAdiccionalInformation.Text.Trim();

                    // Save the data
                    _objBL.AddProduct(ref objOperationResult, _productDto, Globals.ClientSession.GetAsList());


                }
                else if (_Mode == "Edit")
                {
                    _productDto.i_CategoryId = Convert.ToInt32(ddlCategoryId.SelectedNode.Tag);
                    _productDto.v_Name = txtName.Text.Trim();
                    _productDto.v_GenericName = txtGenericName.Text.Trim();
                    _productDto.v_BarCode = txtBarCode.Text.Trim();
                    _productDto.v_ProductCode = txtBarCode.Text.Trim();
                    _productDto.v_Brand = txtBrand.Text.Trim();
                    _productDto.v_Model = txtModel.Text.Trim();
                    _productDto.v_SerialNumber = txtSerialNumber.Text.Trim();
                    if (dtpExpirationDate.Checked == false)
                    {
                        _productDto.d_ExpirationDate = null;
                    }
                    else
                    {
                        _productDto.d_ExpirationDate = dtpExpirationDate.Value;
                    }       
                    //_productDto.d_ExpirationDate = string.IsNullOrEmpty(dtpExpirationDate.Text) ? (DateTime?)null : DateTime.Parse(dtpExpirationDate.Text);
                    _productDto.i_MeasurementUnitId = Convert.ToInt32(ddlMeasurementUnitId.SelectedValue);
                    _productDto.r_ReferentialCostPrice = string.IsNullOrEmpty(txtReferentialCostPrice.Text) ? (float?)null : float.Parse(txtReferentialCostPrice.Text);
                    _productDto.r_ReferentialSalesPrice = string.IsNullOrEmpty(txtReferentialSalesPrice.Text) ? (float?)null : float.Parse(txtReferentialSalesPrice.Text);
                    _productDto.v_Presentation = txtPresentation.Text.Trim();
                    _productDto.v_AdditionalInformation = txtAdiccionalInformation.Text.Trim();

                    // Save the data
                    _objBL.UpdateProduct(ref objOperationResult, _productDto, Globals.ClientSession.GetAsList());


                }
                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else  // Operación con error
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void txtReferentialCostPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
              if (e.KeyChar ==8 ) {
         e.Handled = false;
         return;
       }


       bool IsDec = false;
        int nroDec = 0;

        for (int i = 0; i < txtReferentialCostPrice.Text.Length; i++)
        {
            if (txtReferentialCostPrice.Text[i] == '.')
            IsDec = true;

         if ( IsDec && nroDec++ >=2) {
            e.Handled = true;
            return;
         }
       }

       if ( e.KeyChar>=48 && e.KeyChar<=57)
         e.Handled = false;
       else if (e.KeyChar==46)         
         e.Handled = (IsDec) ? true:false;
       else
         e.Handled = true;

     
        }

        private void txtReferentialSalesPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }


            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txtReferentialSalesPrice.Text.Length; i++)
            {
                if (txtReferentialSalesPrice.Text[i] == '.')
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
    }
}
