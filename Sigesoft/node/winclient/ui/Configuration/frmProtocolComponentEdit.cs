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
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Node.WinClient.BE.Custom;

namespace Sigesoft.Node.WinClient.UI.Configuration
{
    public partial class frmProtocolComponentEdit : Form
    {
        #region Declarations

        private string _mode = null;
        private string _id = string.Empty;
        private MedicalExamBL _medicalExamBL = new MedicalExamBL();
        private ProtocolBL _protocolBL = new ProtocolBL();
        private ProtocolComponentList _protocolcomponent = null;
        public List<ProtocolComponentList> _tmpProtocolcomponentList = null;
        MedicalExamFieldsBL _medicalExamFieldsBL = new MedicalExamFieldsBL();
        string _componentId = null;  

        #endregion
             
        public frmProtocolComponentEdit(string id, string mode)
        {
            InitializeComponent();
            _id = id;
            _mode = mode;
        }

        private List<MedicalExamList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var dataList = _medicalExamBL.GetMedicalExamPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dataList;
        }       

        private void btnFilter_Click(object sender, EventArgs e)
        {
            var filterExpression = string.Format("v_Name.Contains(\"{0}\")", txtComponentName.Text);
            var dataList = GetData(0, null, "v_Name ASC", filterExpression);

            if (dataList != null)
            {
                if (dataList.Count != 0)
                {
                    grdComponent.DataSource = dataList;
                    lblRecordCount1.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
                }
                else
                {
                    grdComponent.DataSource = null;
                    lblRecordCount1.Text = string.Format("Se encontraron {0} registros.", 0);
                }

            }
        }
     
        private void LoadComboBox()
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(cbOperador, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 117, null));

            Utils.LoadDropDownList(cbGenero, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 130, null));
            Utils.LoadDropDownList(cbGrupoEtario, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 254, null), DropDownListAction.All);
            
        }

        private void chkIsConditional_CheckedChanged(object sender, EventArgs e)
        {
            gbConditional.Enabled = (chkExamenCondicional.Checked);

            if (!chkExamenCondicional.Checked)
            {
                cbOperador.SelectedValue = "6";
                txtEdad.Value = 0;
                cbGenero.SelectedValue = ((int)GenderConditional.AMBOS).ToString();
                cbGrupoEtario.SelectedValue = "-1";
            }

        }
        private void SearchControlAndSetEvents(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).CharacterCasing = CharacterCasing.Upper;
                }

                if (ctrl is Infragistics.Win.UltraWinEditors.UltraTextEditor)
                {
                    ((Infragistics.Win.UltraWinEditors.UltraTextEditor)ctrl).CharacterCasing = CharacterCasing.Upper;
                }
                if (ctrl.HasChildren)
                    SearchControlAndSetEvents(ctrl);

            }

        }


        private void frmProtocolComponentEdit_Load(object sender, EventArgs e)
        {
            //OperationResult objOperationResult = new OperationResult();
            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);

            }


            #endregion

            LoadComboBox();

            cbGenero.SelectedValue = ((int)GenderConditional.AMBOS).ToString();
            cbOperador.SelectedValue = ((int) Operator2Values.X_esMayorIgualque_A).ToString();

            if (_mode == "New")
            {
                // Additional logic here.             
                txtComponentName.Select();

            }
            else if (_mode == "Edit")
            {
                gbFilter.Enabled = false;
                gbAddExam.Enabled = false;              

                var findResult = _tmpProtocolcomponentList.Find(p => p.v_ProtocolComponentId == _id);

                lblExamenSeleccionado.Text = findResult.v_ComponentName;
                txtPrecioFinal.Value = findResult.r_Price;
                chkExamenAdicional.Checked = Convert.ToBoolean(findResult.i_isAdditional);
                chkExamenCondicional.Checked = Convert.ToBoolean(findResult.i_IsConditionalId);                
                cbOperador.SelectedValue = findResult.i_OperatorId.ToString();
                txtEdad.Value = findResult.i_Age;
                cbGenero.SelectedValue = findResult.i_GenderId.ToString();
                cbGrupoEtario.SelectedValue = findResult.i_GrupoEtarioId.ToString();
                if (findResult.i_IsConditionalIMC == 1)
                {
                    chkIMC.Checked = true;
                }
                else
                {
                    chkIMC.Checked = false;
                }
                txtMayorque.Value = findResult.r_Imc;


            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_tmpProtocolcomponentList == null)
                _tmpProtocolcomponentList = new List<ProtocolComponentList>();

            #region Validations

            if (_mode == "New")
            {
                if (grdComponent.Selected.Rows.Count == 0)
                {
                    MessageBox.Show("Por favor seleccione un Exámen Médico para agregar al protocolo", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            //if (txtFinalPrice.Value.ToString() == "0")
            //{
            //    MessageBox.Show("Por favor escriba un precio de vta valido", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            if (chkExamenCondicional.Checked)
            {
                if (cbOperador.SelectedIndex != 0)
                {
                    if (Convert.ToInt32(txtEdad.Value) == 0)
                    {
                        MessageBox.Show("Por favor ingrese una edad.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEdad.Focus();
                        return;
                    }
                }
            }

            if (chkIMC.Checked)
            {
               
                    if (Convert.ToInt32(txtMayorque.Value) == 0)
                    {
                        MessageBox.Show("Por favor ingrese un valor para el I.M.C.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMayorque.Focus();
                        return;
                    }
            }

            bool IsAddedComponent = _tmpProtocolcomponentList.Exists(p => p.v_ComponentId == _componentId && p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

            if (IsAddedComponent)
            {
                MessageBox.Show("Por favor seleccione otro Exámen Médico. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            #endregion

            OperationResult objOperationResult = new OperationResult();

            string[] componentIdFromProtocol = _tmpProtocolcomponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico)
                                                                               .Select(p => p.v_ComponentId).ToArray();

            #region En un protocolo no se debe poder agregar un Componente que tenga un campo, que se repite en otro componente del mismo protocolo.           
           
            // Verificar si el componente actual a agregar tiene campos ya existentes en el mismo protocolo.
            bool IsExists = _protocolBL.IsExistscomponentfieldsInCurrentProtocol(ref objOperationResult, componentIdFromProtocol, _componentId);

            if (IsExists)
            {
                var msj = string.Format("El examen ({0}) no se puede agregar porqué tiene un campo que se repite en otro componente del mismo protocolo.", lblExamenSeleccionado.Text);
                MessageBox.Show(msj, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
          
            #endregion        
         
            #region En un protocolo No se debe permitir agregar un Componente que tenga un campo formula que depende de otr componente que NO está en mismo protocolo. Si esto ocurre debe decir indicar lo siguiente: "El campo formula XXXXX depende de los campos YYY, ZZZZ que están en los componentes LLLLLL, y MMMMMM. Por favor agrege previamente los componentes LLLL y MMMM al protocolo.

            SiNo IsExists__ = _protocolBL.IsExistsFormula(ref objOperationResult, componentIdFromProtocol, _componentId);

            if (IsExists__ == SiNo.NO)
            {
                MessageBox.Show(objOperationResult.ReturnValue, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            #endregion  
                    
            string opera = string.Empty;
            string gender = string.Empty;
            int? age = 0;

            if (chkExamenCondicional.Checked)
            {
                opera = cbOperador.SelectedIndex != 0 ? cbOperador.Text : string.Empty;
                gender = cbGenero.Text;
                age = Convert.ToInt32(txtEdad.Value);
            }
            else
            {
                gender = "AMBOS";
            }

            if (_mode == "New")
            {
               
                var findResult = _tmpProtocolcomponentList.Find(p => p.v_ComponentId == _componentId);

                _protocolcomponent = new ProtocolComponentList();

                if (findResult == null)   // agregar con normalidad  a la bolsa de examenes del protocolo
                {                   
                    _protocolcomponent.v_ProtocolComponentId = Guid.NewGuid().ToString();
                    _protocolcomponent.v_ComponentId = _componentId;
                    _protocolcomponent.v_ComponentName = grdComponent.Selected.Rows[0].Cells[1].Value.ToString();

                    _protocolcomponent.r_Price = float.Parse(txtPrecioFinal.Value.ToString());
                    _protocolcomponent.v_Operator = opera;
                    _protocolcomponent.i_Age = age;
                    _protocolcomponent.v_Gender = gender;

                    _protocolcomponent.i_IsConditionalIMC = chkIMC.Checked == true ? 1 : 0;
                    _protocolcomponent.r_Imc = decimal.Parse(txtMayorque.Value.ToString());

                    _protocolcomponent.i_isAdditional = Convert.ToInt32(chkExamenAdicional.Checked);
                    _protocolcomponent.i_IsConditionalId = Convert.ToInt32(chkExamenCondicional.Checked);
                    _protocolcomponent.v_IsConditional = chkExamenCondicional.Checked ? "Si" : "No";
                    _protocolcomponent.i_OperatorId = Convert.ToInt32(cbOperador.SelectedValue);
                    _protocolcomponent.i_GenderId = Convert.ToInt32(cbGenero.SelectedValue);
                    _protocolcomponent.i_GrupoEtarioId = Convert.ToInt32(cbGrupoEtario.SelectedValue);
                    _protocolcomponent.i_RecordStatus = (int)RecordStatus.Agregado;
                    _protocolcomponent.v_ComponentTypeName = grdComponent.Selected.Rows[0].Cells["v_ComponentTypeName"].Value.ToString();
                    _protocolcomponent.i_RecordType = (int)RecordType.Temporal;

                    _tmpProtocolcomponentList.Add(_protocolcomponent);
                }
                else    // El examen ya esta agregado en la bolsa hay que actualizar su estado
                {
                    if (findResult.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                    {
                        if (findResult.i_RecordType == (int)RecordType.NoTemporal)   // El registro Tiene in ID de BD
                        {
                            findResult.r_Price = float.Parse(txtPrecioFinal.Value.ToString());
                            findResult.v_Operator = opera;
                            findResult.i_Age = age;
                            findResult.v_Gender = gender;

                            _protocolcomponent.i_IsConditionalIMC = chkIMC.Checked == true ? 1 : 0;
                            _protocolcomponent.r_Imc = decimal.Parse(txtMayorque.Value.ToString());

                            findResult.i_isAdditional = Convert.ToInt32(chkExamenAdicional.Checked);
                            findResult.i_IsConditionalId = Convert.ToInt32(chkExamenCondicional.Checked);
                            findResult.v_IsConditional = chkExamenCondicional.Checked ? "Si" : "No";
                            findResult.i_OperatorId = Convert.ToInt32(cbOperador.SelectedValue);
                            findResult.i_GenderId = Convert.ToInt32(cbGenero.SelectedValue);
                            findResult.i_GrupoEtarioId = Convert.ToInt32(cbGrupoEtario.SelectedValue);
                            findResult.v_ComponentTypeName = grdComponent.Selected.Rows[0].Cells["v_ComponentTypeName"].Value.ToString();
                            findResult.i_RecordStatus = (int)RecordStatus.Grabado;                           
                        }
                        else if (findResult.i_RecordType == (int)RecordType.Temporal)   // El registro tiene un ID temporal [GUID]
                        {
                            findResult.r_Price = float.Parse(txtPrecioFinal.Value.ToString());
                            findResult.v_Operator = opera;
                            findResult.i_Age = age;
                            findResult.v_Gender = gender;

                            _protocolcomponent.i_IsConditionalIMC = chkIMC.Checked == true ? 1 : 0;
                            _protocolcomponent.r_Imc = decimal.Parse(txtMayorque.Value.ToString());

                            findResult.i_isAdditional = Convert.ToInt32(chkExamenAdicional.Checked);
                            findResult.i_IsConditionalId = Convert.ToInt32(chkExamenCondicional.Checked);
                            findResult.v_IsConditional = chkExamenCondicional.Checked ? "Si" : "No";
                            findResult.i_OperatorId = Convert.ToInt32(cbOperador.SelectedValue);
                            findResult.i_GenderId = Convert.ToInt32(cbGenero.SelectedValue);
                            findResult.i_GrupoEtarioId = Convert.ToInt32(cbGrupoEtario.SelectedValue);
                            findResult.v_ComponentTypeName = grdComponent.Selected.Rows[0].Cells["v_ComponentTypeName"].Value.ToString();
                            findResult.i_RecordStatus = (int)RecordStatus.Agregado;                           
                        }                      
                        
                    }
                    else
                    {
                        MessageBox.Show("Por favor seleccione otro Exámen Médico. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }               
                }

            }
            else if (_mode == "Edit")
            {
                var findResult = _tmpProtocolcomponentList.Find(p => p.v_ProtocolComponentId == _id);

                findResult.r_Price = float.Parse(txtPrecioFinal.Value.ToString());
                findResult.v_Operator = opera;
                findResult.i_Age = age;
                findResult.v_Gender = gender;

                findResult.i_IsConditionalIMC = chkIMC.Checked == true ? 1 : 0;
                if (txtMayorque.Value == null)
                {
                    txtMayorque.Value = 0;
                }
                findResult.r_Imc = decimal.Parse(txtMayorque.Value.ToString());

                findResult.i_isAdditional = Convert.ToInt32(chkExamenAdicional.Checked);
                findResult.i_IsConditionalId = Convert.ToInt32(chkExamenCondicional.Checked);
                findResult.v_IsConditional = chkExamenCondicional.Checked ? "Si" : "No";
                findResult.i_OperatorId = Convert.ToInt32(cbOperador.SelectedValue);
                findResult.i_GenderId = Convert.ToInt32(cbGenero.SelectedValue);
                findResult.i_GrupoEtarioId = Convert.ToInt32(cbGrupoEtario.SelectedValue);
                findResult.i_RecordStatus = (int)RecordStatus.Modificado;

            }
            //this.Close();
            MessageBox.Show("Se grabo correctamente.", "INFORMACION!", MessageBoxButtons.OK, MessageBoxIcon.Information);                       
        }

        private void grdComponent_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (((UltraGrid)sender).Selected.Rows.Count != 0)
            {
                UltraGrid grd = ((UltraGrid)sender);
                _componentId = grdComponent.Selected.Rows[0].Cells[0].Value.ToString();
                txtPrecioFinal.Value = grd.Selected.Rows[0].Cells["r_BasePrice"].Value.ToString();
                lblExamenSeleccionado.Text = grd.Selected.Rows[0].Cells["v_Name"].Value.ToString();
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void cbOperator_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gbAddExam_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void chkIMC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIMC.Checked)
            {
                txtMayorque.Enabled = true;
            }
            else
            {
                txtMayorque.Enabled = false;
                txtMayorque.Value = "0";
            }
        }

        private void txtComponentName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(sender, e);
            }
        }

        private void txtFinalPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnOK_Click(sender, e);
            }
        }      
        
    }
}
