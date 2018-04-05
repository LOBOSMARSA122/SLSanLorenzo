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
    public partial class frmSupplierEdicion : Form
    {
        SupplierBL _objBL = new SupplierBL();
        supplierDto _supplierDto = new supplierDto();
        string _SupplierId, _Mode;
        string _Temp_IdentificationNumber = null;

        public frmSupplierEdicion(string pstrSupplierId, string pstrMode)
        {
            InitializeComponent();
            this.Text = this.Text + " (" + pstrSupplierId + ")";
            _SupplierId = pstrSupplierId;
            _Mode = pstrMode;
        }

        private void frmSupplierEdicion_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            OperationResult objOperationResult = new OperationResult();
            //Llenado de combos

            //Organization
            Utils.LoadDropDownList(ddlSectorTypeId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 104, null), DropDownListAction.Select);
                    
            if (_Mode == "New")
            {
                // Additional logic here.

            }
            else if (_Mode == "Edit")
            {
                // Get the Entity Data

                _supplierDto = _objBL.GetSupplier(ref objOperationResult, _SupplierId);

                ddlSectorTypeId.SelectedValue = _supplierDto.i_SectorTypeId.ToString();
                txtName.Text = _supplierDto.v_Name;
                txtIdentificationNumber.Text = _supplierDto.v_IdentificationNumber;
                txtAddress.Text = _supplierDto.v_Address;
                txtPhoneNumber.Text = _supplierDto.v_PhoneNumber;
                txtEmail.Text = _supplierDto.v_Mail;

                _Temp_IdentificationNumber = _supplierDto.v_IdentificationNumber;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string pstrFilterEpression;
            if (uvOrganization.Validate(true, false).IsValid)
            {
                if (txtIdentificationNumber.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para la Nro Identificación.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtName.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para la Razón Social.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }               
                //if (!Common.Utils.ValidateIdentificationDocumentPeru(txtIdentificationNumber.Text))
                //{
                //    MessageBox.Show("El Nro. identificación es errado .", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                if (_Mode == "New")
                {
                    _supplierDto = new supplierDto();
                    // Populate the entity
                    _supplierDto.i_SectorTypeId = int.Parse(ddlSectorTypeId.SelectedValue.ToString());
                    _supplierDto.v_IdentificationNumber = txtIdentificationNumber.Text.Trim();
                    _supplierDto.v_Name = txtName.Text.Trim();
                    _supplierDto.v_Address = txtAddress.Text.Trim();
                    _supplierDto.v_PhoneNumber = txtPhoneNumber.Text.Trim();
                    _supplierDto.v_Mail = txtEmail.Text.Trim();

                    pstrFilterEpression = "i_IsDeleted==0 && v_IdentificationNumber==(\"" + txtIdentificationNumber.Text.Trim() + "\")";
                    if (_objBL.GetSuppliersPagedAndFiltered(ref objOperationResult, null, null, null, pstrFilterEpression).Count() != 0)
                    {
                        MessageBox.Show("El Nro. Identificación ya existe:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        // Save the data
                        _objBL.AddSupplier(ref objOperationResult, _supplierDto, Globals.ClientSession.GetAsList());
                    }
                }
                else if (_Mode == "Edit")
                {
                    // Populate the entity
                    _supplierDto.v_SupplierId = _SupplierId;
                    _supplierDto.i_SectorTypeId = int.Parse(ddlSectorTypeId.SelectedValue.ToString());
                    _supplierDto.v_IdentificationNumber = txtIdentificationNumber.Text.Trim();
                    _supplierDto.v_Name = txtName.Text.Trim();
                    _supplierDto.v_Address = txtAddress.Text.Trim();
                    _supplierDto.v_PhoneNumber = txtPhoneNumber.Text.Trim();
                    _supplierDto.v_Mail = txtEmail.Text.Trim();

                    if (_Temp_IdentificationNumber == _supplierDto.v_IdentificationNumber)
                    {
                        // Save the data
                        _objBL.UpdateSupplier(ref objOperationResult, _supplierDto, Globals.ClientSession.GetAsList());
                    }
                    else
                    {
                        pstrFilterEpression = "i_IsDeleted==0 && v_IdentificationNumber==(\"" + txtIdentificationNumber.Text.Trim() + "\")";
                        if (_objBL.GetSuppliersPagedAndFiltered(ref objOperationResult, null, null, null, pstrFilterEpression).Count() != 0)
                        {
                            MessageBox.Show("El Nro Identificación ya existe:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            // Save the data
                            _objBL.UpdateSupplier(ref objOperationResult, _supplierDto, Globals.ClientSession.GetAsList());
                        }
                    }
                }


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
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
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

    }
}
