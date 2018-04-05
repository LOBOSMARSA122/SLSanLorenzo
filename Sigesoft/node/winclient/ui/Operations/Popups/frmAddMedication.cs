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
    public partial class frmAddMedication : Form
    {
        #region Declarations

        private List<KeyValueDTO> _keyValueDTO = null;
        public List<MedicationList> _tmpMedicationList = null;
        public string _serviceId;
        private NodeBL _nodeBL = new NodeBL();
        public string _pharmacyWarehouseId;

        #endregion
       
        public frmAddMedication()
        {
            InitializeComponent();
        }

        private void frmAddMedication_Load(object sender, EventArgs e)
        {
            LoadComboBox();

            txtProductSearch.Select();
        }

        private void LoadComboBox()
        {
            // Llenado de combos

            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(cbVia, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 142, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbMedicamento, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, -1, null), DropDownListAction.Select);         

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (uvAddMedication.Validate(true, false).IsValid)
            {

                if (_tmpMedicationList == null)
                    _tmpMedicationList = new List<MedicationList>();

                string productId = cbMedicamento.SelectedValue.ToString();

                var medication = _tmpMedicationList.Find(p => p.v_ProductId == productId);

                // Validar si el producto ya esta agregado
                if (medication == null)   // agregar con normalidad [insert]  a la bolsa  
                {

                    MedicationList medicationList = new MedicationList();
                    medicationList.v_ProductId = cbMedicamento.SelectedValue.ToString();
                    medicationList.v_ServiceId = _serviceId;
                    medicationList.v_ProductName = cbMedicamento.Text;
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
                            medication.v_ProductId = cbMedicamento.SelectedValue.ToString();
                            medication.v_ProductName = cbMedicamento.Text;
                            medication.v_PresentationName = lblPresentacion.Text;
                            medication.r_Quantity = float.Parse(txtCantidad.Text);
                            medication.v_Doses = txtDosis.Text;
                            medication.i_ViaId = int.Parse(cbVia.SelectedValue.ToString());
                            medication.v_ViaName = cbVia.Text;
                            medication.i_RecordStatus = (int)RecordStatus.Grabado;
                        }
                        else if (medication.i_RecordType == (int)RecordType.Temporal)   // El registro tiene un ID temporal [GUID]
                        {
                            medication.v_ProductId = cbMedicamento.SelectedValue.ToString();
                            medication.v_ProductName = cbMedicamento.Text;
                            medication.v_PresentationName = lblPresentacion.Text;
                            medication.r_Quantity = float.Parse(txtCantidad.Text);
                            medication.v_Doses = txtDosis.Text;
                            medication.i_ViaId = int.Parse(cbVia.SelectedValue.ToString());
                            medication.v_ViaName = cbVia.Text;
                            medication.i_RecordStatus = (int)RecordStatus.Agregado;
                        }                       
                    }
                    else
                    {
                        MessageBox.Show("Por favor seleccione otro Medicamento. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }       
            
        }

        private void txtCriterio_TextChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            int nodeId = 0;

            if (Globals.ClientSession != null)          
                nodeId = Globals.ClientSession.i_CurrentExecutionNodeId;
         
            nodeDto node = _nodeBL.GetNodeByNodeId(ref objOperationResult, nodeId);
            _pharmacyWarehouseId = node.v_PharmacyWarehouseId;

            if (node != null)
            {
                _keyValueDTO = BLL.Utils.GetProductWarehouse(ref objOperationResult, "Value2==" + "\"" + _pharmacyWarehouseId + "\"" + "&&" + "Value1.Contains(\"" + txtProductSearch.Text.Trim() + "\")");
                Utils.LoadDropDownList(cbMedicamento, "Value1", "Id", _keyValueDTO, DropDownListAction.Select);
            }                    

        }

        private void cbMedicamento_SelectedValueChanged(object sender, EventArgs e)
        {
            var valueId = cbMedicamento.SelectedValue;
            if (valueId != null)
            {
                if (valueId.ToString() == "-1")
                {
                    lblPresentacion.Text = string.Empty;
                    return;
                }

                //Setear la presentacion del medicamento
                lblPresentacion.Text = _keyValueDTO.Find(p => p.Id == valueId.ToString()).Value3;
            }
        }

        
    }
}
