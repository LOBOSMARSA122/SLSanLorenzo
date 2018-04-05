using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class frmAddMedicationMedicalConsult : Form
    {
        #region Declarations

        public List<MedicationList> _tmpMedicationList = null;
        public string _pharmacyWarehouseId;
        public string _productName;
        public string _productId;
        public string _serviceId;
        public string _mode = null;
        public string _id = string.Empty;

        #endregion
       
        public frmAddMedicationMedicalConsult()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool answer = Save();

            if (!answer)
                return;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmAddMedicationMedicalConsult_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            MedicamentSearch();

            OperationResult objOperationResult = new OperationResult();             
            var nodeId = Globals.ClientSession.i_CurrentExecutionNodeId;
            
            nodeDto node = new NodeBL().GetNodeByNodeId(ref objOperationResult, nodeId);
            _pharmacyWarehouseId = string.Empty;
            
            if (node != null)
            {
                _pharmacyWarehouseId = node.v_PharmacyWarehouseId;
            }

            if (_mode == "New")
            {
                txtProductSearch.Select();

            }
            else if (_mode == "Edit")
            {
                btnAddAndNew.Enabled = false;

                var medication = _tmpMedicationList.Find(p => p.v_ProductId == _id);

                _productId = medication.v_ProductId;
                _serviceId = medication.v_ServiceId;
                _productName = medication.v_ProductName;
                lblPresentacion.Text = medication.v_PresentationName;
                txtCantidad.Text = medication.r_Quantity.ToString() ;
                txtDosis.Text = medication.v_Doses;
                cbVia.SelectedValue = medication.i_ViaId.ToString();
                cbVia.Text = medication.v_ViaName;

            }

        }

        private void LoadComboBox()
        {
            // Llenado de combos

            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(cbVia, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 142, null), DropDownListAction.Select);
          

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnAddAndNew_Click(object sender, EventArgs e)
        {
            bool answer = Save();

            if (!answer)
                return;

            ClearControl();
        }

        private bool Save()
        {
            if (uvAddMedication.Validate(true, false).IsValid)
            {
                if (string.IsNullOrEmpty(_productId))
                {
                    MessageBox.Show("Por favor seleccione un medicamento.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (_tmpMedicationList == null)
                    _tmpMedicationList = new List<MedicationList>();

                var medication = _tmpMedicationList.Find(p => p.v_ProductId == _productId);

                // Validar si el producto ya esta agregado
                if (medication == null)   // agregar con normalidad [insert]  a la bolsa  
                {
                    MedicationList medicationList = new MedicationList();
                    medicationList.v_ProductId = _productId;
                    medicationList.v_ServiceId = _serviceId;
                    medicationList.v_ProductName = _productName;
                    medicationList.v_PresentationName = lblPresentacion.Text;
                    medicationList.r_Quantity = float.Parse(txtCantidad.Text);
                    medicationList.v_Doses = txtDosis.Text;
                    medicationList.i_ViaId = int.Parse(cbVia.SelectedValue.ToString());
                    medicationList.v_ViaName = cbVia.Text;
                    medicationList.i_RecordStatus = (int)RecordStatus.Agregado;
                    medicationList.i_RecordType = (int)RecordType.Temporal;

                    _tmpMedicationList.Add(medicationList);

                   
                }
                else    // el producto ya esta agregado en la bolsa hay que actualizar su estado
                {
                    if (medication.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                    {
                        if (medication.i_RecordType == (int)RecordType.NoTemporal)   // El registro Tiene in ID de BD
                        {                        
                            medication.r_Quantity = float.Parse(txtCantidad.Text);
                            medication.v_Doses = txtDosis.Text;
                            medication.i_ViaId = int.Parse(cbVia.SelectedValue.ToString());
                            medication.v_ViaName = cbVia.Text;
                            medication.i_RecordStatus = (int)RecordStatus.Grabado;
                        }
                        else if (medication.i_RecordType == (int)RecordType.Temporal)   // El registro tiene un ID temporal [GUID]
                        {                         
                            medication.r_Quantity = float.Parse(txtCantidad.Text);
                            medication.v_Doses = txtDosis.Text;
                            medication.i_ViaId = int.Parse(cbVia.SelectedValue.ToString());
                            medication.v_ViaName = cbVia.Text;
                            medication.i_RecordStatus = (int)RecordStatus.Agregado;
                        }
                    }
                    else
                    {                    
                        medication.r_Quantity = float.Parse(txtCantidad.Text);
                        medication.v_Doses = txtDosis.Text;
                        medication.i_ViaId = int.Parse(cbVia.SelectedValue.ToString());
                        medication.v_ViaName = cbVia.Text;
                        medication.i_RecordStatus = (int)RecordStatus.Modificado;                    
                    }
                }

                btnAddAndNew.Tag = _tmpMedicationList;
                return true;
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

        }

        private void btnSearchMedicamento_Click(object sender, EventArgs e)
        {
            MedicamentSearch();
        }

        private void MedicamentSearch()
        {
            OperationResult objOperationResult = new OperationResult();            
            MovementBL objLogistBL = new MovementBL();
            var filterExpression = BuildFilterExpression();

            int IdAlmacen = int.Parse(Common.Utils.GetApplicationConfigValue("AlmacenId"));

            var medicaments = objLogistBL.DevolverProductos(txtProductSearch.Text, IdAlmacen, int.Parse(Globals.ClientSession.i_RolVentaId.ToString()));
            if (medicaments != null)
            {
                if (medicaments.Count > 0)
                {

                    var MedicamentosGenericos = medicaments.FindAll(p => p.v_CategoriaId == "N002-TL000000003");
                    grdMedicament.DataSource = MedicamentosGenericos;
                    //grdMedicament.Rows[0].Selected = true;
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", MedicamentosGenericos.Count());


                    var MedicamentosRecomendados = medicaments.FindAll(p => p.v_CategoriaId == "N002-TL000000004");
                    grdMedicamentRecomendado.DataSource = MedicamentosRecomendados;
                    lblRecordCountRecomendado.Text = string.Format("Se encontraron {0} registros.", MedicamentosRecomendados.Count());


                    var MedicamentosMarca = medicaments.FindAll(p => p.v_CategoriaId == "N002-TL000000005");
                    grdMedicamentMarca.DataSource = MedicamentosMarca;
                    lblRecordCountMarca.Text = string.Format("Se encontraron {0} registros.", MedicamentosMarca.Count());
                }
                else
                {
                    List<ProductWarehouseList> Lista = new List<ProductWarehouseList>();
                    grdMedicament.DataSource = Lista;
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", 0);

                    grdMedicamentRecomendado.DataSource = Lista;
                    lblRecordCountRecomendado.Text = string.Format("Se encontraron {0} registros.", 0);


                    grdMedicamentMarca.DataSource = Lista;
                    lblRecordCountMarca.Text = string.Format("Se encontraron {0} registros.", 0);
                }
            }
           
        }

        private string BuildFilterExpression()
        {
            // Get the filters from the UI
            string filterExpression = string.Empty;

            List<string> Filters = new List<string>();

            if (!string.IsNullOrEmpty(txtProductSearch.Text))
                Filters.Add("(v_ProductName.Contains(\"" + txtProductSearch.Text.Trim() + "\"))");
         
            // Create the Filter Expression
           
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    filterExpression = filterExpression + item + " && ";
                }

                filterExpression = filterExpression.Substring(0, filterExpression.Length - 4);
            }

            return filterExpression;
        }

        private void txtProductSearch_TextChanged(object sender, EventArgs e)
        {
            MedicamentSearch();
        }

        private void grdMedicament_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grdMedicament.Selected.Rows.Count > 0)
            {
                _productId = grdMedicament.Selected.Rows[0].Cells["v_ProductId"].Value.ToString();
                _productName = lblPresentacion.Text = grdMedicament.Selected.Rows[0].Cells["v_ProductName"].Value.ToString();
                gbMedicamentoSeleccionado.Text = string.Format("Medicamento seleccionado < {0} >", grdMedicament.Selected.Rows[0].Cells["v_ProductName"].Value.ToString());
                lblPresentacion.Text = grdMedicament.Selected.Rows[0].Cells["v_Presentation"].Value.ToString();
             
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
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

        private void ClearControl()
        {
            txtProductSearch.Text = string.Empty;
            grdMedicament.DataSource = new List<ProductWarehouseList>();
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", 0);
            lblPresentacion.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            cbVia.SelectedValue = "-1";
            txtDosis.Text = string.Empty;
            txtProductSearch.Focus();
        }

        private void grdMedicamentRecomendado_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grdMedicamentRecomendado.Selected.Rows.Count > 0)
            {
                _productId = grdMedicamentRecomendado.Selected.Rows[0].Cells["v_ProductId"].Value.ToString();
                _productName = lblPresentacion.Text = grdMedicamentRecomendado.Selected.Rows[0].Cells["v_ProductName"].Value.ToString();
                gbMedicamentoSeleccionado.Text = string.Format("Medicamento seleccionado < {0} >", grdMedicamentRecomendado.Selected.Rows[0].Cells["v_ProductName"].Value.ToString());
                lblPresentacion.Text = grdMedicamentRecomendado.Selected.Rows[0].Cells["v_Presentation"].Value.ToString();

            }
        }

        private void grdMedicamentMarca_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grdMedicamentMarca.Selected.Rows.Count > 0)
            {
                _productId = grdMedicamentMarca.Selected.Rows[0].Cells["v_ProductId"].Value.ToString();
                _productName = lblPresentacion.Text = grdMedicamentMarca.Selected.Rows[0].Cells["v_ProductName"].Value.ToString();
                gbMedicamentoSeleccionado.Text = string.Format("Medicamento seleccionado < {0} >", grdMedicamentMarca.Selected.Rows[0].Cells["v_ProductName"].Value.ToString());
                lblPresentacion.Text = grdMedicamentMarca.Selected.Rows[0].Cells["v_Presentation"].Value.ToString();

            }
        }

       
    }
}
