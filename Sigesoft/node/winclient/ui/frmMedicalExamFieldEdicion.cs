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
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinMaskedEdit;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmMedicalExamFieldEdicion : Form
    {
        string strFilterExpression;
        string _Mode;
        string _MedicalExamId;
        string _MedicalExamFliedId;
        string _TextLabelOld;
        string _DefaultText;
        MedicalExamFieldsBL _objMedicalExamFieldsBL =  new MedicalExamFieldsBL();
        componentfieldDto _objmedicalexamfieldDto;
        componentfieldsDto _objmedicalexamfieldsDto;
        MedicalExamFieldValuesBL _objMedicalExamFieldValuesBL = new MedicalExamFieldValuesBL();
        List<SearchComponentFieldsList> _TempSearchComponentFieldsList ;

        #region Controls
        TextBox objTextBox = null;
        UltraNumericEditor une = null;
        ComboBox combo = null;
        CheckBox chk = null;
        RadioButton rb1 , rb2 = null;
        #endregion

        public frmMedicalExamFieldEdicion(string strMedicalExamId, string pstrMode, string strMedicalExamFliedId)
        {
            InitializeComponent();
            _Mode = pstrMode;
            _MedicalExamId = strMedicalExamId;
            _MedicalExamFliedId = strMedicalExamFliedId;
        }

        private void BindUltraDropDown()
        {
            OperationResult objOperationResult = new OperationResult();

            _TempSearchComponentFieldsList = _objMedicalExamFieldsBL.FillComponentFieldList(ref objOperationResult);

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


        private void frmMedicalExamFieldEdicion_Load(object sender, EventArgs e)
        {
            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);

            }


            #endregion
            OperationResult objOperationResult = new OperationResult();
            // Establecer el filtro inicial para los datos
            _TempSearchComponentFieldsList = new List<SearchComponentFieldsList>();
            _TempSearchComponentFieldsList= _objMedicalExamFieldsBL.FillComponentFieldList(ref objOperationResult);

            txtLabel.Select();


            txtLabel.DisplayMember = "Nombre";
            txtLabel.ValueMember = "Nombre";
            txtLabel.DataSource = _TempSearchComponentFieldsList;

            txtLabel.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            txtLabel.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.txtLabel.DropDownWidth = 750; 
            //this.txtLabel.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;

            txtLabel.DisplayLayout.Bands[0].Columns[0].Width = 290;
            txtLabel.DisplayLayout.Bands[0].Columns[1].Width = 250;
            txtLabel.DisplayLayout.Bands[0].Columns[2].Width = 190;

            strFilterExpression = null;

            //Llenado de combos
            Utils.LoadDropDownList(ddlGroupId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 112, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlUniMedId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 105, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlGroup, "Value1", "", BLL.Utils.GetMedicalExamGrupo(ref objOperationResult, _MedicalExamId));
            Utils.LoadDropDownList(ddlComponentId, "Value1", "Id", BLL.Utils.GetComponents(ref objOperationResult), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlFieldAndId, "Value1", "Id", BLL.Utils.GetFieldsByComponent(ref objOperationResult, "-1","Total"), DropDownListAction.Select);


            //Cargar SearchComponentFieldsList Temporalmente
              if (_Mode == "New")
                {
                // Additional logic here.
                    grdDataMedicalExamFieldValue.Enabled = false;
                }
              else if (_Mode == "Edit")
              {
                  grdDataMedicalExamFieldValue.Enabled = true;
                  // Get the Entity Data
                  _objmedicalexamfieldDto = new componentfieldDto();

                  _objmedicalexamfieldDto = _objMedicalExamFieldsBL.GetMedicalExamFields(ref objOperationResult, _MedicalExamFliedId);
                  _objmedicalexamfieldsDto = _objMedicalExamFieldsBL.GetComponentFields(ref objOperationResult, _MedicalExamFliedId, _MedicalExamId);
                  txtLabel.Text = _objmedicalexamfieldDto.v_TextLabel;
                  _TextLabelOld = _objmedicalexamfieldDto.v_TextLabel;
                  txtAbbreviation.Text = _objmedicalexamfieldDto.v_abbreviation;
                  unLabelWidth.Value = _objmedicalexamfieldDto.i_LabelWidth;

                  unValidateValue1.Text = _objmedicalexamfieldDto.r_ValidateValue1.ToString();
                  unValidateValue2.Text = _objmedicalexamfieldDto.r_ValidateValue2.ToString();

                  unMaxLenght.Text = _objmedicalexamfieldDto.i_MaxLenght.ToString();


                 


                  if (_objmedicalexamfieldDto.i_IsRequired == 1)
                  {
                      rbYesRequired.Checked = true;
                  }
                  else
                  {
                      rbYesRequired.Checked = false;
                  }

                  if (_objmedicalexamfieldDto.i_IsCalculate == 1)
                  {
                      rbYesIsCalculate.Checked = true;

                  }
                  else
                  {
                      rbYesIsCalculate.Checked = false;
                  }
                  unGroupId.Text = _objmedicalexamfieldDto.i_GroupId.ToString();
                  txtColumn.Text = _objmedicalexamfieldDto.i_Column.ToString();

                  ddlGroup.Text = _objmedicalexamfieldsDto.v_Group;
                  _DefaultText = _objmedicalexamfieldDto.v_DefaultText;
                  ddlGroupId.SelectedValue = _objmedicalexamfieldDto.i_ControlId.ToString();

                  //AMC
                  if (ddlGroupId.SelectedValue.ToString() == ((int)(ControlType.NumeroDecimal)).ToString())
                  {
                      cboNroDecimales.Enabled = true;
                      cboNroDecimales.Text = _objmedicalexamfieldDto.i_NroDecimales.ToString();
                  }
                  

                  if (ddlGroupId.SelectedValue.ToString() == ((int)(ControlType.CadenaMultilinea)).ToString() || ddlGroupId.SelectedValue.ToString() == ((int)(ControlType.CadenaTextual)).ToString())
                  {

                      chkEnabled.Enabled = true;
                      chkReadOnly.Enabled = true;

                      if (_objmedicalexamfieldDto.i_Enabled == 1)
                      {
                          chkEnabled.Checked = true;
                      }
                      else
                      {
                          chkEnabled.Checked = false;
                      }

                      if (_objmedicalexamfieldDto.i_ReadOnly == 1)
                      {
                          chkReadOnly.Checked = true;
                      }
                      else
                      {
                          chkReadOnly.Checked = false;
                      }
                      
                  }
                  
                  ddlUniMedId.SelectedValue = _objmedicalexamfieldDto.i_MeasurementUnitId.ToString();

                  unItemId.Text = _objmedicalexamfieldDto.i_ItemId.ToString();
                  unControlWidth.Value = _objmedicalexamfieldDto.i_WidthControl;
                  unHeightControl.Value = _objmedicalexamfieldDto.i_HeightControl;
                  unOrder.Value = _objmedicalexamfieldDto.i_Order;
                  txtFormula.Text = _objmedicalexamfieldDto.v_Formula;
                  unValidateValue1.Text = _objmedicalexamfieldDto.r_ValidateValue1.ToString();
                  unValidateValue2.Text = _objmedicalexamfieldDto.r_ValidateValue2.ToString();

                    if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 1)//Cadena Textual
                    {
                        unGroupId.Enabled = false;
                        unItemId.Enabled = false;
                        unMaxLenght.Enabled = true;
                        objTextBox.Text = _DefaultText;
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 2) // Cadena Multimedia
                    {
                        unGroupId.Enabled = false;
                        unItemId.Enabled = false;
                        unMaxLenght.Enabled = true;
                        objTextBox.Text = _DefaultText;
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 3) // Número Entero
                    {
                        unGroupId.Enabled = false;
                        unItemId.Enabled = false;
                        unMaxLenght.Text = "1";
                        unMaxLenght.Enabled = false;
                        une.Value = _DefaultText;
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 4) // Número Decimal
                    {
                        unGroupId.Enabled = false;
                        unItemId.Enabled = false;
                        unMaxLenght.Text = "1";
                        unMaxLenght.Enabled = false;
                        une.Value = _DefaultText;
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 5) // Check
                    {
                        unGroupId.Enabled = false;
                        unItemId.Enabled = false;
                        unMaxLenght.Text = "1";
                        unMaxLenght.Enabled = false;

                        if (_DefaultText == "1")
                        {
                            chk.Checked = true;
                        }
                        else
                        {
                            chk.Checked = false;
                        }
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 6) // Radio Button
                    {
                        unGroupId.Enabled = false;
                        unItemId.Enabled = false;
                        unMaxLenght.Text = "1";
                        unMaxLenght.Enabled = false;

                        //if (_objmedicalexamfieldsDto.v_DefaultText == "1")
                        //{
                        //    rb1.Checked = true;
                        //}
                        //else
                        //{
                        //    rb2.Checked = true;
                        //}
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 30) // Radio Button
                    {
                        unGroupId.Enabled = false;
                        unItemId.Enabled = false;
                        unMaxLenght.Text = "1";
                        unMaxLenght.Enabled = false;

                        if (_DefaultText == "1")
                        {
                            chk.Checked = true;
                        }
                        else
                        {
                            chk.Checked = false;
                        }
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 7) // Sino Combo
                    {
                        Utils.LoadDropDownList(combo, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);
                        combo.SelectedValue = _DefaultText;
                        unGroupId.Enabled = false;
                        unItemId.Enabled = false;
                        unMaxLenght.Text = "1";
                        unMaxLenght.Enabled = false;

                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 9) // Lista
                    {
                        combo.SelectedValue = _DefaultText;
                        unMaxLenght.Text = "1";
                        unMaxLenght.Enabled = false;
                    }
                    BindGridMedicalExamFieldValues(_MedicalExamFliedId);
              }
        }

        private void mnuGridNewMedicalExamFieldValue_Click(object sender, EventArgs e)
        {
            //string MedicalExamFieldValueId =  grdDataMedicalExamFieldValue.Selected.Rows[0].Cells[0].Value.ToString();

            frmMedicalExamFieldValueEdicion frm = new frmMedicalExamFieldValueEdicion(_MedicalExamFliedId,"","New");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                BindGridMedicalExamFieldValues(_MedicalExamFliedId);
            }
        }

        private void mnuGridEditMedicalExamFieldValue_Click(object sender, EventArgs e)
        {
            string MedicalExamFieldValueId = grdDataMedicalExamFieldValue.Selected.Rows[0].Cells[0].Value.ToString();

            frmMedicalExamFieldValueEdicion frm = new frmMedicalExamFieldValueEdicion(_MedicalExamFliedId, MedicalExamFieldValueId, "Edit");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                BindGridMedicalExamFieldValues(_MedicalExamFliedId);
            }
        }

        private void BindGridMedicalExamFieldValues(string MedicalExamFliedId)
        {
            strFilterExpression = "v_MedicalExamFieldsId ==" + "\"" + MedicalExamFliedId + "\"";
            var objData = GetDataMedicalExamFieldValues(0, null, "", strFilterExpression);

            grdDataMedicalExamFieldValue.DataSource = objData;
            lblRecordCountMedicalExamFieldValues.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<MedicalExamFieldValuesList> GetDataMedicalExamFieldValues(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objMedicalExamFieldValuesBL.GetMedicalExamFieldValuesPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }       

        private void btnOK_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();    
            
            //string pstrFilterEpression;
            if (ddlGroupId.SelectedValue.ToString() == ((int)ControlType.NumeroDecimal).ToString() )
            {
                #region Validacion

                if (ultraValidator1.Validate(true, false).IsValid)
                {
                    if (unValidateValue1.Text == "")
                    {
                        MessageBox.Show("Por favor ingrese un valor apropiado para Valor 1.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (unValidateValue2.Text == "")
                    {
                        MessageBox.Show("Por favor ingrese un valor apropiado para Valor 2.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                #endregion
            }
            if (uvMedicalExamField.Validate(true, false).IsValid)
            {
                #region Validacion

                if (txtLabel.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para el Nombre Label.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }              
                if (MessageBox.Show("¿Está seguro de agregar / modificar la Campos al examen?.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }

                #endregion  

                if (_Mode == "New")
                {
                    _objmedicalexamfieldDto = new componentfieldDto();
                    _objmedicalexamfieldsDto = new componentfieldsDto();
                     //Populate the entity
                    //_objmedicalexamfieldDto.v_ComponentId = MedicalExamId;
                    int indice = txtLabel.Text.IndexOf("/", 1);
                    //_objmedicalexamfieldDto.v_TextLabel = txtLabel.Text.Substring(0, indice + 1);
                    if (indice != -1)
                    {
                        _objmedicalexamfieldDto.v_TextLabel = txtLabel.Text.Substring(0, indice);
                    }
                    else
                    {
                        _objmedicalexamfieldDto.v_TextLabel = txtLabel.Text;
                    } 
                    _objmedicalexamfieldDto.v_abbreviation = txtAbbreviation.Text;
                    _objmedicalexamfieldDto.i_LabelWidth = (int)unLabelWidth.Value;
                     //_objmedicalexamfieldDto.i_ItemId == null ? 0 : int.Parse(unItemId.Text.ToString());
                    _objmedicalexamfieldDto.r_ValidateValue1 = _objmedicalexamfieldDto.r_ValidateValue1 == null ? 0 :  float.Parse(unValidateValue1.Text.ToString());
                    _objmedicalexamfieldDto.r_ValidateValue2 = _objmedicalexamfieldDto.r_ValidateValue2 == null ? 0 : float.Parse(unValidateValue2.Text.ToString());

                    //_objmedicalexamfieldsDto.v_DefaultText = txtDefaultTextMultiLine.Text;
                    _objmedicalexamfieldDto.i_MaxLenght = int.Parse(unMaxLenght.Text.ToString());

                    if (rbYesRequired.Checked)
                    {
                        _objmedicalexamfieldDto.i_IsRequired = 1;
                    }
                    else
                    {
                        _objmedicalexamfieldDto.i_IsRequired = 0;
                    }

                    if (rbYesIsCalculate.Checked)
                    {
                        _objmedicalexamfieldDto.i_IsCalculate = 1;
                    }
                    else
                    {
                        _objmedicalexamfieldDto.i_IsCalculate = 0;
                    }


                    if (cboNroDecimales.Enabled)
                    {
                        _objmedicalexamfieldDto.i_NroDecimales = int.Parse(cboNroDecimales.Text.ToString());
                    }
                    else
                    {
                        _objmedicalexamfieldDto.i_NroDecimales = 0;
                    }

                    if (chkEnabled.Checked)
                    {
                        _objmedicalexamfieldDto.i_Enabled = 1;
                    }
                    else
                    {
                        _objmedicalexamfieldDto.i_Enabled = 0;
                    }

                    if (chkReadOnly.Checked)
                    {
                        _objmedicalexamfieldDto.i_ReadOnly = 1;
                    }
                    else
                    {
                        _objmedicalexamfieldDto.i_ReadOnly = 0;
                    }

                    _objmedicalexamfieldDto.i_ControlId = Int32.Parse(ddlGroupId.SelectedValue.ToString());
                    _objmedicalexamfieldDto.i_Column = Int32.Parse(txtColumn.Text);
                    _objmedicalexamfieldDto.i_MeasurementUnitId = Int32.Parse(ddlUniMedId.SelectedValue.ToString());
                    _objmedicalexamfieldDto.i_GroupId = int.Parse(unGroupId.Text.ToString());
                    _objmedicalexamfieldDto.i_ItemId =   _objmedicalexamfieldDto.i_ItemId == null ? 0 : int.Parse(unItemId.Text.ToString());
                    _objmedicalexamfieldDto.i_WidthControl = (int)unControlWidth.Value;
                    _objmedicalexamfieldDto.i_HeightControl = (int)unHeightControl.Value;
                    _objmedicalexamfieldDto.i_Order = (int)unOrder.Value;
                    _objmedicalexamfieldDto.v_Formula = txtFormula.Text;

                    //_objmedicalexamfieldDto.r_ValidateValue1 = float.Parse(unValidateValue1.Text.ToString());
                    //_objmedicalexamfieldDto.r_ValidateValue2 = float.Parse(unValidateValue2.Text.ToString());

                    if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 1)//Cadena Textual
                    {
                        _objmedicalexamfieldDto.v_DefaultText = objTextBox.Text;
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 2) // Cadena Multimedia
                    {
                        _objmedicalexamfieldDto.v_DefaultText = objTextBox.Text;
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 3) // Número Entero
                    {
                        _objmedicalexamfieldDto.v_DefaultText = une.Value.ToString();
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 4) // Número Decimal
                    {
                        _objmedicalexamfieldDto.v_DefaultText = une.Value.ToString();
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 5) //Sino Check
                    {
                        if (chk.Checked)
                        {
                            _objmedicalexamfieldDto.v_DefaultText = "1"; // SI
                        }
                        else
                        {
                            _objmedicalexamfieldDto.v_DefaultText = "0"; // NO
                        }                        
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 6) //Sino Radio Button
                    {
                        if (rb1.Checked)
                        {
                            _objmedicalexamfieldDto.v_DefaultText = "1"; // SI
                        }
                        else
                        {
                            _objmedicalexamfieldDto.v_DefaultText = "0"; // NO
                        }
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 30) //Radio Button
                    {
                        if (chk.Checked)
                        {
                            _objmedicalexamfieldDto.v_DefaultText = "1"; // SI
                        }
                        else
                        {
                            _objmedicalexamfieldDto.v_DefaultText = "0"; // NO
                        }
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 7) // Sino Combo
                    {
                        _objmedicalexamfieldDto.v_DefaultText = combo.SelectedValue.ToString();
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 9) // Lista
                    {
                        _objmedicalexamfieldDto.v_DefaultText = combo.SelectedValue.ToString();
                    }

                    //if (ddlGroup.SelectedIndex == 0)
                    //{
                    //    _objmedicalexamfieldsDto.v_Group = null;
                    //}
                    //else
                    //{
                        _objmedicalexamfieldsDto.v_Group = ddlGroup.Text;
                    //}

                
                    //Cargar Entidad para ComponentFields
                    _objmedicalexamfieldsDto.v_ComponentId = _MedicalExamId;

                    //Save the data
                    _MedicalExamFliedId = _objMedicalExamFieldsBL.AddMedicalExamFields(ref objOperationResult, _objmedicalexamfieldDto, _objmedicalexamfieldsDto,_MedicalExamId,Globals.ClientSession.GetAsList());
                    if (_MedicalExamFliedId != "0")
                    {
                        _objmedicalexamfieldDto = _objMedicalExamFieldsBL.GetMedicalExamFields(ref objOperationResult, _MedicalExamFliedId);
                        _Mode = "Edit";
                    }                    
                }
                else if (_Mode == "Edit")
                {
                 //Populate the entity
                    int indice = txtLabel.Text.IndexOf("/", 1);
                    if (indice != -1)
                    {
                        _objmedicalexamfieldDto.v_TextLabel = txtLabel.Text.Substring(0, indice);
                    }
                    else
                    { 
                        _objmedicalexamfieldDto.v_TextLabel = txtLabel.Text;
                    } 
                    
                    //_objmedicalexamfieldDto.v_TextLabel = txtLabel.Text;
                    _objmedicalexamfieldDto.i_LabelWidth = (int)unLabelWidth.Value;
                    _objmedicalexamfieldDto.v_abbreviation = txtAbbreviation.Text;

                    _objmedicalexamfieldDto.r_ValidateValue1 = float.Parse(unValidateValue1.Text.ToString());
                    _objmedicalexamfieldDto.r_ValidateValue2 = float.Parse(unValidateValue2.Text.ToString());

                    _objmedicalexamfieldDto.i_MaxLenght = int.Parse(unMaxLenght.Text.ToString());
                    if (rbYesRequired.Checked)
                    {
                        _objmedicalexamfieldDto.i_IsRequired = 1;
                    }
                    else
                    {
                        _objmedicalexamfieldDto.i_IsRequired = 0;
                    }

                    if (rbYesIsCalculate.Checked)
                    {
                        _objmedicalexamfieldDto.i_IsCalculate = 1;
                    }
                    else
                    {
                        _objmedicalexamfieldDto.i_IsCalculate = 0;
                    }

                    if (cboNroDecimales.Enabled)
                    {
                        _objmedicalexamfieldDto.i_NroDecimales = int.Parse(cboNroDecimales.Text.ToString());
                    }
                    else
                    {
                        _objmedicalexamfieldDto.i_NroDecimales = 0;
                    }

                    if (chkEnabled.Checked)
                    {
                        _objmedicalexamfieldDto.i_Enabled = 1;
                    }
                    else
                    {
                        _objmedicalexamfieldDto.i_Enabled = 0;
                    }

                    if (chkReadOnly.Checked)
                    {
                        _objmedicalexamfieldDto.i_ReadOnly = 1;
                    }
                    else
                    {
                        _objmedicalexamfieldDto.i_ReadOnly = 0;
                    }

                    _objmedicalexamfieldDto.i_ControlId = Int32.Parse(ddlGroupId.SelectedValue.ToString());
                    _objmedicalexamfieldDto.i_Column = Int32.Parse(txtColumn.Text);
                    _objmedicalexamfieldDto.i_MeasurementUnitId = Int32.Parse(ddlUniMedId.SelectedValue.ToString());
                    _objmedicalexamfieldDto.i_GroupId = int.Parse(unGroupId.Text.ToString());
                    _objmedicalexamfieldDto.i_ItemId = int.Parse(unItemId.Text.ToString());
                    _objmedicalexamfieldDto.i_WidthControl = (int)unControlWidth.Value;
                    _objmedicalexamfieldDto.i_HeightControl = (int)unHeightControl.Value;
                    _objmedicalexamfieldDto.i_Order = (int)unOrder.Value;
                    _objmedicalexamfieldDto.v_Formula = txtFormula.Text;

                    //_objmedicalexamfieldDto.r_ValidateValue1 = float.Parse(unValidateValue1.Text.ToString());
                    //_objmedicalexamfieldDto.r_ValidateValue2 = float.Parse(unValidateValue2.Text.ToString());
                    _objmedicalexamfieldDto.r_ValidateValue1 = _objmedicalexamfieldDto.r_ValidateValue1 == null ? 0 : float.Parse(unValidateValue1.Text.ToString());
                    _objmedicalexamfieldDto.r_ValidateValue2 = _objmedicalexamfieldDto.r_ValidateValue2 == null ? 0 : float.Parse(unValidateValue2.Text.ToString());


                    if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 1)//Cadena Textual
                    {
                        _objmedicalexamfieldDto.v_DefaultText = objTextBox.Text;
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 2) // Cadena Multimedia
                    {
                        _objmedicalexamfieldDto.v_DefaultText = objTextBox.Text;
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 3) // Número Entero
                    {
                        _objmedicalexamfieldDto.v_DefaultText = une.Value.ToString();
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 4) // Número Decimal
                    {
                        _objmedicalexamfieldDto.v_DefaultText = une.Value.ToString();
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 5) //Sino Check
                    {
                        if (chk.Checked)
                        {
                            _objmedicalexamfieldDto.v_DefaultText = "1"; // SI
                        }
                        else
                        {
                            _objmedicalexamfieldDto.v_DefaultText = "0"; // NO
                        }
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 6) //Sino Radio Button
                    {
                        if (rb1.Checked)
                        {
                            _objmedicalexamfieldDto.v_DefaultText = "1"; // SI
                        }
                        else
                        {
                            _objmedicalexamfieldDto.v_DefaultText = "0"; // NO
                        }
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 30) //Radio Button
                    {
                        if (chk.Checked)
                        {
                            _objmedicalexamfieldDto.v_DefaultText = "1"; // SI
                        }
                        else
                        {
                            _objmedicalexamfieldDto.v_DefaultText = "0"; // NO
                        }
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 7) // Sino Combo
                    {
                        _objmedicalexamfieldDto.v_DefaultText = combo.SelectedValue.ToString();
                    }
                    else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == 9) // Lista
                    {
                        _objmedicalexamfieldDto.v_DefaultText = combo.SelectedValue.ToString();
                    }

                    //if (ddlGroup.SelectedIndex == 0)
                    //{
                    //    _objmedicalexamfieldsDto.v_Group = null;

                    //}
                    //else
                    //{
                        _objmedicalexamfieldsDto.v_Group = ddlGroup.Text;
                    //}
         
                     //Save the data       
                        bool Result = _objMedicalExamFieldsBL.UpdateComponentFields(ref objOperationResult, _objmedicalexamfieldsDto, _TextLabelOld, txtLabel.Text, Globals.ClientSession.GetAsList());
                   if (Result)
                   {
                       _objMedicalExamFieldsBL.UpdateMedicalExamField(ref objOperationResult, _objmedicalexamfieldDto, _MedicalExamId, Globals.ClientSession.GetAsList());                    
                   }
                }

                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    MessageBox.Show("El registro se guardó correctamente", "CORRECTO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    grdDataMedicalExamFieldValue.Enabled = true;
                    //this.Close();
                }
                else  // Operación con error
                {
                    if (objOperationResult.ErrorMessage !="")
                    {
                        MessageBox.Show(objOperationResult.ErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Se queda en el formulario.
                    }
                   
                }
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            //this.DialogResult = System.Windows.Forms.DialogResult.OK;
            //this.Close();            

        }

        private void mnuGridDeleteMedicalExamFieldValue_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                // Delete the item
                //string pstrLocationId = grdData.SelectedRows[0].Cells[0].Value.ToString();
                string strMedicalExamFieldId = grdDataMedicalExamFieldValue.Selected.Rows[0].Cells[0].Value.ToString();

                _objMedicalExamFieldValuesBL.DeleteMedicalExamFieldValues(ref objOperationResult, strMedicalExamFieldId, Globals.ClientSession.GetAsList());

                BindGridMedicalExamFieldValues(_MedicalExamFliedId);
            }
        }

        private void grdDataMedicalExamFieldValue_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    grdDataMedicalExamFieldValue.Rows[row.Index].Selected = true;
                    contextMenuMedicalExamFieldValues.Items["mnuGridNewMedicalExamFieldValue"].Enabled = true;
                    contextMenuMedicalExamFieldValues.Items["mnuGridEditMedicalExamFieldValue"].Enabled = true;
                    contextMenuMedicalExamFieldValues.Items["mnuGridDeleteMedicalExamFieldValue"].Enabled = true;
                }
                else
                {
                    contextMenuMedicalExamFieldValues.Items["mnuGridNewMedicalExamFieldValue"].Enabled = true;
                    contextMenuMedicalExamFieldValues.Items["mnuGridEditMedicalExamFieldValue"].Enabled = false;
                    contextMenuMedicalExamFieldValues.Items["mnuGridDeleteMedicalExamFieldValue"].Enabled = false;
                }

            //} 
        }

        private void txtColumn_KeyPress(object sender, KeyPressEventArgs e)
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

        private void unValidateValue1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < unValidateValue1.Text.Length; i++)
            {
                if (unValidateValue1.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }

            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar ==45)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void unValidateValue2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < unValidateValue2.Text.Length; i++)
            {
                if (unValidateValue2.Text[i] == '.')
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

        private void rbYesIsCalculate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbYesIsCalculate.Checked == true)
            {
                
                txtFormula.Enabled = true;
                ddlFieldAndId.Enabled = true;
                ddlComponentId.Enabled = true;
                btnAddField.Enabled = true;

            }
            else
            {
              
                txtFormula.Enabled = false;
                ddlFieldAndId.Enabled = false;
                ddlComponentId.Enabled = false;
                btnAddField.Enabled = false;
                ddlFieldAndId.SelectedValue = "-1";
            }

        }

        private void rbNoIsCalculate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNoIsCalculate.Checked == true)
            {
                txtFormula.Enabled = false;
                txtFormula.Text = "";
            }
            else
            {
                txtFormula.Enabled = true;
            }
        }

        private void ddlGroupId_SelectedIndexChanged(object sender, EventArgs e)
        {           
            ClearflowLayoutPanel();

            if (int.Parse(ddlGroupId.SelectedValue.ToString()) == -1)
            {
                unGroupId.Enabled = false;
                unItemId.Enabled = false;
                chkEnabled.Enabled = false;
                chkReadOnly.Enabled = false;
                chkReadOnly.Checked = false;
                chkEnabled.Checked = false;
                cboNroDecimales.Enabled = false;
                cboNroDecimales.Text = "1";
                return;
            } 

            if (int.Parse(ddlGroupId.SelectedValue.ToString()) == ((int)ControlType.CadenaTextual)) //Cadena Textual
            {               
                objTextBox = new TextBox();

                objTextBox.Name = "text";
                objTextBox.Text = "";
                objTextBox.Width = 300;
                objTextBox.Height = 21;
                unGroupId.Text = "0";
                objTextBox.MaxLength = int.Parse(unMaxLenght.Text.ToString());

                flowLayoutPanel1.Controls.Add(objTextBox);

                unGroupId.Enabled = false;
                unItemId.Enabled = false;              
                unMaxLenght.Enabled = true;
                chkEnabled.Enabled = true;
                chkReadOnly.Enabled = true;
                cboNroDecimales.Enabled = false;
                cboNroDecimales.Text = "1";
            }
            else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == ((int)ControlType.CadenaMultilinea) ) // Cadena Multimedia
            {
                unGroupId.Enabled = false;
                unItemId.Enabled = false;
                unMaxLenght.Enabled = true;

                objTextBox = new TextBox();
                unGroupId.Text = "0";
                objTextBox.Name = "textMultiline";
                objTextBox.Text = "";
                objTextBox.Width = 300;
                objTextBox.Height = 80;
                objTextBox.ScrollBars = ScrollBars.Vertical;
                objTextBox.Multiline = true;
                objTextBox.MaxLength = int.Parse(unMaxLenght.Text.ToString());
                chkEnabled.Enabled = true;
                chkReadOnly.Enabled = true;
                cboNroDecimales.Enabled = false;
                cboNroDecimales.Text = "1";
                flowLayoutPanel1.Controls.Add(objTextBox);           
            }
            else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == ((int)ControlType.NumeroEntero)) // Número Entero
            {                
                une = new UltraNumericEditor()
                {
                    Width =100,
                    Height =21,
                    NumericType = NumericType.Integer,
                    PromptChar = ' ',
                    Name =  "textNumericInteger"
                    
                };
                unGroupId.Enabled = false;
                unItemId.Enabled = false;
                unMaxLenght.Text = "1";
                unMaxLenght.Enabled = false;
                unGroupId.Text = "0";
                cboNroDecimales.Enabled = false;
                chkEnabled.Enabled = false;
                chkReadOnly.Enabled = false;
                chkReadOnly.Checked = false;
                chkEnabled.Checked = false;

                cboNroDecimales.Text = "1";
                flowLayoutPanel1.Controls.Add(une);
            }
            else if (int.Parse(ddlGroupId.SelectedValue.ToString()) ==  ((int)ControlType.NumeroDecimal)) // Número Decimal
            {
                une = new UltraNumericEditor()
                {
                    Width = 100,
                    Height = 21,
                    PromptChar = ' ',  
                    Name="textNumericDecimal",
                    NumericType = NumericType.Double,
                    MaskDisplayMode = MaskMode.IncludeBoth                         
                };
                unGroupId.Enabled = false;
                unItemId.Enabled = false;
                unMaxLenght.Text = "1";
                unMaxLenght.Enabled = false;
                unGroupId.Text = "0";
                cboNroDecimales.Enabled = true;
                chkEnabled.Enabled = false;
                chkReadOnly.Enabled = false;
                chkReadOnly.Checked = false;
                chkEnabled.Checked = false;
                flowLayoutPanel1.Controls.Add(une);
            }
            else if (int.Parse(ddlGroupId.SelectedValue.ToString()) ==  ((int)ControlType.SiNoCheck))//Sino Check
            {
                chk = new CheckBox();
                chk.Checked = false;
                chk.Text = "Si";
                flowLayoutPanel1.Controls.Add(chk);

                unGroupId.Enabled = false;
                unItemId.Enabled = false;
                unMaxLenght.Text = "1";
                unMaxLenght.Enabled = false;
                cboNroDecimales.Enabled = false;
                chkEnabled.Enabled = false;
                chkReadOnly.Enabled = false;
                chkReadOnly.Checked = false;
                chkEnabled.Checked = false;
                cboNroDecimales.Text = "1";
                unGroupId.Text = "0";

            }
            else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == ((int)ControlType.SiNoRadioButton)) //Sino Radio Button
            {
                rb1 = new RadioButton();
                rb2 = new RadioButton();

                rb1.Checked = true;
                rb1.Text = "Si";
                rb1.Name = "rbSi";

                rb2.Checked = false;
                rb2.Text = "No";
                rb2.Name = "rbNo";

                flowLayoutPanel1.Controls.Add(rb1);
                flowLayoutPanel1.Controls.Add(rb2);

                unGroupId.Enabled = false;
                unItemId.Enabled = false;
                unMaxLenght.Text = "1";
                unMaxLenght.Enabled = false;
                cboNroDecimales.Enabled = false;
                chkEnabled.Enabled = false;
                chkReadOnly.Enabled = false;
                chkReadOnly.Checked = false;
                chkEnabled.Checked = false;
                cboNroDecimales.Text = "1";
                unGroupId.Text = "0";

            }
            else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == ((int)ControlType.Radiobutton)) // Radio Button
            {
                chk = new CheckBox();
                chk.Checked = false;
                chk.Text = "Si";
                flowLayoutPanel1.Controls.Add(chk);

                unGroupId.Enabled = false;
                unItemId.Enabled = false;
                unMaxLenght.Text = "1";
                unMaxLenght.Enabled = false;
                cboNroDecimales.Enabled = false;
                chkEnabled.Enabled = false;
                chkReadOnly.Enabled = false;
                chkReadOnly.Checked = false;
                chkEnabled.Checked = false;
                cboNroDecimales.Text = "1";
                unGroupId.Text = "0";

            }
            else if (int.Parse(ddlGroupId.SelectedValue.ToString()) == ((int)ControlType.SiNoCombo)) // Sino Combo
            {
                OperationResult objOperationResult = new OperationResult();
                combo = new ComboBox();
                combo.DropDownStyle = ComboBoxStyle.DropDownList;
                flowLayoutPanel1.Controls.Add(combo);

                unGroupId.Enabled = false;
                unItemId.Enabled = false;
                unMaxLenght.Text = "1";
                unMaxLenght.Enabled = false;
                cboNroDecimales.Enabled = false;
                chkEnabled.Enabled = false;
                chkReadOnly.Enabled = false;
                chkReadOnly.Checked = false;
                chkEnabled.Checked = false;
                cboNroDecimales.Text = "1";
                unGroupId.Text = "111";

                Utils.LoadDropDownList(combo, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);
                flowLayoutPanel1.Controls.Add(combo);

            }
            else if (int.Parse(ddlGroupId.SelectedValue.ToString()) ==  ((int)ControlType.Lista)) // Lista
            {
                if (unGroupId.Text.ToString() == "")
                {
                    return;
                }

                unGroupId.Enabled = true;
                unItemId.Enabled = true;
                unMaxLenght.Text = "1";
                unMaxLenght.Enabled = false;
                chkEnabled.Enabled = false;
                chkReadOnly.Enabled = false;
                chkReadOnly.Checked = false;
                chkEnabled.Checked = false;
                cboNroDecimales.Enabled = false;

                cboNroDecimales.Text = "1";
                    OperationResult objOperationResult = new OperationResult();
                    combo = new ComboBox();
                    combo.DropDownStyle = ComboBoxStyle.DropDownList;
                    combo.Width = 200;

                    if (unGroupId.Text.ToString() != "0")
                    {
                        Utils.LoadDropDownList(combo, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, int.Parse(unGroupId.Text.ToString()), null), DropDownListAction.Select);
                        flowLayoutPanel1.Controls.Add(combo);

                        if (_Mode == "Edit")
                        {
                            combo.SelectedValue = _DefaultText;
                        }
                    }   
            }
        }

        void ClearflowLayoutPanel()
        {
            while (flowLayoutPanel1.Controls.Count > 0)
            {
                var controltoremove = flowLayoutPanel1.Controls[0];
                flowLayoutPanel1.Controls.Remove(controltoremove);
                controltoremove.Dispose();
            }
        }

        private void btnAddField_Click(object sender, EventArgs e)
        {
            if (ddlFieldAndId.SelectedValue.ToString() == "-1") return;

            var x = (KeyValueDTO)ddlFieldAndId.SelectedItem;

            txtFormula.Text = txtFormula.Text + "[" + x.Id+ "]";

            ddlFieldAndId.SelectedValue = "-1";
        }

        private void unGroupId_KeyPress(object sender, KeyPressEventArgs e)
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

        private void unGroupId_TextChanged(object sender, EventArgs e)
        {
           
         
        }

        private void unMaxLenght_TextChanged(object sender, EventArgs e)
        {
            if (objTextBox ==  null)
            {
                return;
            }
            if (unMaxLenght.Text.ToString() != "")
            {
                if (int.Parse(unMaxLenght.Text.ToString()) <= 0 || int.Parse(unMaxLenght.Text.ToString()) > 9000)
                {
                    MessageBox.Show("El Nro Caracteres debe estar entre 1 y 9000", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (unMaxLenght.Text == "")
                {
                    unMaxLenght.Text = "1";
                }
                else
                {                  
                    objTextBox.MaxLength = int.Parse(unMaxLenght.Text.ToString());
                }
            }
           
           
        }

        private void unMaxLenght_KeyPress(object sender, KeyPressEventArgs e)
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

        private void unGroupId_Leave(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            if (unGroupId.Text.ToString() == "") unGroupId.Text = "0";
           
            if ((unGroupId.Text.ToString() != "0") )
            {
                Utils.LoadDropDownList(combo, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, int.Parse(unGroupId.Text.ToString()), null), DropDownListAction.Select);
                flowLayoutPanel1.Controls.Add(combo);
            }
        }

        private void ddlComponentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlFieldAndId, "Value1", "Id", BLL.Utils.GetFieldsByComponent(ref objOperationResult, ddlComponentId.SelectedValue.ToString(),"Total"), DropDownListAction.Select);
        }

        //private void ultraDropDown1_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        //{
        //    //UltraGridLayout layout = e.Layout;
        //    //UltraGridBand band = layout.Bands[0];

        //    //band.Columns["ComponentFieldName"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
        //    // Set the scroll style to immediate so when the user scrolls the drop down 
        //    // using scroll thumb, rows get scrolled imediately.
        //    e.Layout.ScrollStyle = ScrollStyle.Immediate;

        //    // Change the order in which columns get displayed in the UltraDropDown.
        //    e.Layout.Bands[0].Columns["i_Order"].Header.VisiblePosition = 0;
        //    e.Layout.Bands[0].Columns["ComponentFieldName"].Header.VisiblePosition = 1;

        //    // Hide columns you don't want shown.
        //    //e.Layout.Bands[0].Columns["UnitsOnOrder"].Hidden = true;

        //    // Sort the items in the drop down by ProductName column.
        //    e.Layout.Bands[0].SortedColumns.Clear();
        //    e.Layout.Bands[0].SortedColumns.Add("ComponentFieldName", false);

        //    // Set the border style of the drop down.
        //    e.Layout.BorderStyle = UIElementBorderStyle.Solid;	
      
        //}

        private void txtLabel_RowSelected(object sender, RowSelectedEventArgs e)
        {
            //if (e.Row == null) return;

            //var ultraCombo = (UltraCombo)sender;

            //var joinedfields = string.Format("{0} / {1} / {2}",
            //                        e.Row.Cells["Nombre"].Value.ToString(),
            //                        e.Row.Cells["Grupo"].Value.ToString(),
            //                        e.Row.Cells["Componente"].Value.ToString());

            //ultraCombo.Text = joinedfields;
        }
             
        private void txtLabel_BeforeColumnChooserDisplayed(object sender, BeforeColumnChooserDisplayedEventArgs e)
        {
            //UltraCombo uc = new UltraCombo();
            //uc = (UltraCombo)e.Cell.EditorComponentResolved;
            //uc.DisplayLayout.Bands[0].Columns[0].Width = 250; 
        }

        private void txtLabel_BeforeDropDownResizeStart(object sender, EventArgs e)
        {
            
        }

        private void txtLabel_BeforeColPosChanged(object sender, BeforeColPosChangedEventArgs e)
        {
            
        }

        private void grdDataMedicalExamFieldValue_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //string MedicalExamFieldValueId =  grdDataMedicalExamFieldValue.Selected.Rows[0].Cells[0].Value.ToString();

            frmMedicalExamFieldValueEdicion frm = new frmMedicalExamFieldValueEdicion(_MedicalExamFliedId, "", "New");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                BindGridMedicalExamFieldValues(_MedicalExamFliedId);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string MedicalExamFieldValueId = grdDataMedicalExamFieldValue.Selected.Rows[0].Cells[0].Value.ToString();

            frmMedicalExamFieldValueEdicion frm = new frmMedicalExamFieldValueEdicion(_MedicalExamFliedId, MedicalExamFieldValueId, "Edit");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                BindGridMedicalExamFieldValues(_MedicalExamFliedId);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                // Delete the item
                //string pstrLocationId = grdData.SelectedRows[0].Cells[0].Value.ToString();
                string strMedicalExamFieldId = grdDataMedicalExamFieldValue.Selected.Rows[0].Cells[0].Value.ToString();

                _objMedicalExamFieldValuesBL.DeleteMedicalExamFieldValues(ref objOperationResult, strMedicalExamFieldId, Globals.ClientSession.GetAsList());

                BindGridMedicalExamFieldValues(_MedicalExamFliedId);
            }
        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnabled.Checked)
            {
                chkReadOnly.Checked = false;
            }
            
        }

        private void chkReadOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReadOnly.Checked)
            {
                chkEnabled.Checked = false;
            }
        }

  
    }
}
