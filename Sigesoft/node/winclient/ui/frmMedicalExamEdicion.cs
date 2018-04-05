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
using Sigesoft.Node.Contasol.Integration.Contasol;


namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmMedicalExamEdicion : Form
    { 
         MedicalExamBL _objMedicalExamBL = new MedicalExamBL();
         componentDto objmedicalexamDto;

        string MedicalExamId;
        string Mode;
        string _NameComponentOld;

        public frmMedicalExamEdicion( string strMedicalExamId,string pstrMode)
        {
            InitializeComponent();
            Mode = pstrMode;
            MedicalExamId = strMedicalExamId;
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


        private void frmMedicalExamEdicion_Load(object sender, EventArgs e)
        {
            try
            {
                #region Mayusculas - Normal
                var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
                if (_EsMayuscula == 1)
                {
                    SearchControlAndSetEvents(this);

                }


                #endregion
                OperationResult objOperationResult = new OperationResult();

                //Llenado de combos
                Utils.LoadComboTreeBoxList(ddlCategoryId, BLL.Utils.GetSystemParameterForComboTreeBox(ref objOperationResult, 116, null), DropDownListAction.Select);
                Utils.LoadDropDownList(ddlDiagnosableId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);
                Utils.LoadDropDownList(ddlComponentTypeId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 126, null), DropDownListAction.Select);
                Utils.LoadDropDownList(ddlUIIsVisibleId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);
                Utils.LoadDropDownList(ddlIsApprovedId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);
                MedicamentoDao.ObtenerLineasParaCombo(ddlUnidadProductiva);
                if (Mode == "New")
                {
                    // Additional logic here.

                }
                else if (Mode == "Edit")
                {
                    // Get the Entity Data
                    objmedicalexamDto = new componentDto();

                    objmedicalexamDto = _objMedicalExamBL.GetMedicalExam(ref objOperationResult, MedicalExamId);

                    txtInsertName.Text = objmedicalexamDto.v_Name;
                    _NameComponentOld = objmedicalexamDto.v_Name;

                    ComboTreeNode nodoABuscar = ddlCategoryId.AllNodes.First(x => x.Tag.ToString() == objmedicalexamDto.i_CategoryId.ToString());
                    ddlCategoryId.SelectedNode = nodoABuscar;

                    unBasePrice.Text = objmedicalexamDto.r_BasePrice.ToString();
                    ddlDiagnosableId.SelectedValue = objmedicalexamDto.i_DiagnosableId.ToString();
                    ddlComponentTypeId.SelectedValue = objmedicalexamDto.i_ComponentTypeId.ToString();

                    ddlUIIsVisibleId.SelectedValue = objmedicalexamDto.i_UIIsVisibleId.ToString();
                    unUIIndex.Value = objmedicalexamDto.i_UIIndex;

                    ddlIsApprovedId.SelectedValue = objmedicalexamDto.i_IsApprovedId.ToString();
                    unValidInDays.Value = objmedicalexamDto.i_ValidInDays.ToString();
                    if (objmedicalexamDto.v_IdUnidadProductiva != null) ddlUnidadProductiva.SelectedValue = objmedicalexamDto.v_IdUnidadProductiva;
                    else ddlUnidadProductiva.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(ddlCategoryId.Text);

            OperationResult objOperationResult = new OperationResult();

            if (uvMedicalExamEdit.Validate(true, false).IsValid)
            {
                if (txtInsertName.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para la Examen Médico.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("¿Está seguro de agregar / modificar la Examen Médico?.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }

                 if (Mode == "New")
                {
                    objmedicalexamDto = new componentDto();

                    // Populate the entity
                    objmedicalexamDto.v_Name = txtInsertName.Text;
                 
                        objmedicalexamDto.i_CategoryId = Convert.ToInt32(ddlCategoryId.SelectedNode.Tag);
                        objmedicalexamDto.r_BasePrice = float.Parse(unBasePrice.Text);
                        objmedicalexamDto.i_DiagnosableId = Convert.ToInt32(ddlDiagnosableId.SelectedValue);
                        objmedicalexamDto.i_ComponentTypeId = Convert.ToInt32(ddlComponentTypeId.SelectedValue);
                        objmedicalexamDto.i_UIIsVisibleId = Convert.ToInt32(ddlUIIsVisibleId.SelectedValue);
                        objmedicalexamDto.i_UIIndex = Convert.ToInt32(unUIIndex.Value);
                        objmedicalexamDto.i_IsApprovedId = Convert.ToInt32(ddlIsApprovedId.SelectedValue);
                        objmedicalexamDto.i_ValidInDays = Convert.ToInt32(unValidInDays.Value);
                        objmedicalexamDto.v_IdUnidadProductiva = ddlUnidadProductiva.SelectedValue.ToString();
                        // Save the data
                        _objMedicalExamBL.AddMedicalExam(ref objOperationResult, objmedicalexamDto, Globals.ClientSession.GetAsList());

                }
                else if (Mode == "Edit")
                {
                    bool pbIsChangeName;
                    // Populate the entity
                    if (_NameComponentOld != txtInsertName.Text)
                    {
                        pbIsChangeName = true;
                    }
                    else
                    {
                        pbIsChangeName = false;
                    }
                    objmedicalexamDto.v_Name = txtInsertName.Text;                   
                    
                    objmedicalexamDto.i_CategoryId = Convert.ToInt32(ddlCategoryId.SelectedNode.Tag);
                    objmedicalexamDto.r_BasePrice = float.Parse(unBasePrice.Text);
                    objmedicalexamDto.i_DiagnosableId = Convert.ToInt32(ddlDiagnosableId.SelectedValue);
                    objmedicalexamDto.i_ComponentTypeId = Convert.ToInt32(ddlComponentTypeId.SelectedValue);
                    objmedicalexamDto.i_UIIsVisibleId = Convert.ToInt32(ddlUIIsVisibleId.SelectedValue);
                    objmedicalexamDto.i_UIIndex = Convert.ToInt32(unUIIndex.Value);
                    objmedicalexamDto.i_IsApprovedId = Convert.ToInt32(ddlIsApprovedId.SelectedValue);
                    objmedicalexamDto.i_ValidInDays = Convert.ToInt32(unValidInDays.Value);
                    objmedicalexamDto.v_IdUnidadProductiva = ddlUnidadProductiva.SelectedValue.ToString();
                    // Save the data
                    _objMedicalExamBL.UpdateMedicalExam(ref objOperationResult,pbIsChangeName, objmedicalexamDto, Globals.ClientSession.GetAsList());

                }

                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else  // Operación con error
                {
                    if (objOperationResult.ErrorMessage != null)
                    {
                        MessageBox.Show(objOperationResult.ErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   
                    }
                    else
                    {
                        MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //this.Close();
        }

        private void unBasePrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < unBasePrice.Text.Length; i++)
            {
                if (unBasePrice.Text[i] == '.')
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
