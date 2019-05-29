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
using Sigesoft.Node.WinClient.BE.Custom;


namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmMedicalExamEdicion : Form
    { 
         MedicalExamBL _objMedicalExamBL = new MedicalExamBL();
         componentDto objmedicalexamDto;
        private string Orden;
        string MedicalExamId;
        string Mode;
        string _NameComponentOld;
        private ComboTreeNode valueCategoryId;
        private string nameCategoryId;


        #region GetChanges
        private List<Campos> ListValuesCampo = new List<Campos>();

        string[] nombreCampos =
        {

            "txtInsertName", "ddlCategoryId", "ddlDiagnosableId", "unBasePrice", "ddlComponentTypeId",
            "ddlUIIsVisibleId", "unUIIndex", "ddlIsApprovedId", "unValidInDays", "txtCodigoSegus", "txtTarifaSegus",
            "ddlUnidadProductiva", "ddlKindOfService", "cbRecargable"
        };       

        private string GetChanges()
        {
            string commentaryUpdate = _objMedicalExamBL.GetCommentaryUpdateByComponentId(objmedicalexamDto.v_ComponentId);
            string oldCommentary = commentaryUpdate;
            commentaryUpdate += "<FechaActualiza:" + DateTime.Now.ToString() + "|UsuarioActualiza:" + Globals.ClientSession.v_UserName + "|";
            bool change = false;
            foreach (var item in nombreCampos)
            {
                var fields = this.Controls.Find(item, true);
                string keyTagControl;
                string value1;

                if (fields.Length > 0)
                {
                    keyTagControl = fields[0].GetType().Name;
                    value1 = GetValueControl(keyTagControl, fields[0]);

                    var ValorCampo = ListValuesCampo.Find(x => x.NombreCampo == item).ValorCampo;
                    if (ValorCampo != value1)
                    {
                        commentaryUpdate += item + ":" + ValorCampo + "|";
                        change = true;
                    }
                }
            }
            if (change)
            {
                return commentaryUpdate;
            }

            return oldCommentary;
        }

        private void SetOldValues()
        {
            ListValuesCampo = new List<Campos>();
            string keyTagControl = null;
            string value1 = null;
            foreach (var item in nombreCampos)
            {
                var fields = this.Controls.Find(item, true);

                if (fields.Length > 0)
                {
                    keyTagControl = fields[0].GetType().Name;
                    value1 = GetValueControl(keyTagControl, fields[0]);

                    Campos _Campo = new Campos();
                    _Campo.NombreCampo = item;
                    _Campo.ValorCampo = value1;
                    ListValuesCampo.Add(_Campo);
                }
            }
        }

        private string GetValueControl(string ControlId, Control ctrl)
        {
            string value1 = null;

            switch (ControlId)
            {
                case "TextBox":
                    value1 = ((TextBox)ctrl).Text;
                    break;
                case "ComboBox":
                    value1 = ((ComboBox)ctrl).Text;
                    break;
                case "CheckBox":
                    value1 = Convert.ToInt32(((CheckBox)ctrl).Checked).ToString() == "0" ? "SI" : "NO";
                    break;
                case "RadioButton":
                    value1 = Convert.ToInt32(((RadioButton)ctrl).Checked).ToString() == "0" ? "SI" : "NO";
                    break;
                case "ComboTreeBox":
                    if (((ComboTreeBox)ctrl).Text == "--Seleccionar--")
                    {
                        value1 = "--Seleccionar--";
                    }
                    else
                    {
                        value1 = ((ComboTreeBox)ctrl).SelectedNode.Parent.Text;
                    }
                    
                    break;
                default:
                    break;
            }

            return value1;
        }

        #endregion
        


        public frmMedicalExamEdicion( string strMedicalExamId,string pstrMode, string orden)
        {
            InitializeComponent();
            Mode = pstrMode;
            Orden = orden;
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
                Utils.LoadDropDownList(cbRecargable, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);
                Utils.LoadDropDownList(ddlComponentTypeId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 126, null), DropDownListAction.Select);
                Utils.LoadDropDownList(ddlKindOfService, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 358, null), DropDownListAction.Select);
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

                    ComboTreeNode nodoABuscar = null;

                    foreach (var nodeParent in ddlCategoryId.Nodes)
                    {
                        foreach (var nodeChildCategory in nodeParent.Nodes)
                        {
                            
                            if (nodeChildCategory.Tag.ToString() == objmedicalexamDto.i_CategoryId.ToString())
                            {
                                
                                foreach (var nodeChildSubCategory in nodeChildCategory.Nodes)
                                {
                                    if (nodeChildSubCategory.Tag.ToString() == Orden)
                                    {
                                        nodoABuscar = nodeChildSubCategory;
                                    }
                                    
                                }

                            }

                        }
                    }
                    if (nodoABuscar != null)
                    {
                        ddlCategoryId.SelectedNode = nodoABuscar;
                    }
                    

                    unBasePrice.Text = objmedicalexamDto.r_BasePrice.ToString();
                    ddlDiagnosableId.SelectedValue = objmedicalexamDto.i_DiagnosableId.ToString();
                    ddlComponentTypeId.SelectedValue = objmedicalexamDto.i_ComponentTypeId.ToString();
                    ddlKindOfService.SelectedValue = objmedicalexamDto.i_KindOfService.ToString();
                    cbRecargable.SelectedValue = objmedicalexamDto.i_PriceIsRecharged.ToString();
                    ddlUIIsVisibleId.SelectedValue = objmedicalexamDto.i_UIIsVisibleId.ToString();
                    unUIIndex.Value = objmedicalexamDto.i_UIIndex;

                    ddlIsApprovedId.SelectedValue = objmedicalexamDto.i_IsApprovedId.ToString();
                    unValidInDays.Value = objmedicalexamDto.i_ValidInDays.ToString();
                    if (objmedicalexamDto.v_IdUnidadProductiva != null) ddlUnidadProductiva.SelectedValue = objmedicalexamDto.v_IdUnidadProductiva;
                    else ddlUnidadProductiva.SelectedIndex = 0;
                    txtTarifaSegus.Text = objmedicalexamDto.r_PriceSegus.ToString();
                    txtCodigoSegus.Text = objmedicalexamDto.v_CodigoSegus;
                }

                SetOldValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

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
                    float priceSegus;
                    if (txtTarifaSegus.Text == "")
                    {
                        priceSegus = 0;
                    }
                    else
                    {
                        priceSegus = float.Parse(txtTarifaSegus.Text);
                    }
                    objmedicalexamDto = new componentDto();

                    // Populate the entity
                    objmedicalexamDto.v_Name = txtInsertName.Text;

                    objmedicalexamDto.i_CategoryId = Convert.ToInt32(ddlCategoryId.SelectedNode.Parent.Tag);
                        objmedicalexamDto.r_BasePrice = float.Parse(unBasePrice.Text);
                        objmedicalexamDto.i_DiagnosableId = Convert.ToInt32(ddlDiagnosableId.SelectedValue);
                        objmedicalexamDto.i_ComponentTypeId = Convert.ToInt32(ddlComponentTypeId.SelectedValue);
                        objmedicalexamDto.i_UIIsVisibleId = Convert.ToInt32(ddlUIIsVisibleId.SelectedValue);
                        objmedicalexamDto.i_UIIndex = Convert.ToInt32(unUIIndex.Value);
                        objmedicalexamDto.i_IsApprovedId = Convert.ToInt32(ddlIsApprovedId.SelectedValue);
                        objmedicalexamDto.i_ValidInDays = Convert.ToInt32(unValidInDays.Value);
                        objmedicalexamDto.v_IdUnidadProductiva = ddlUnidadProductiva.SelectedValue.ToString();
                        objmedicalexamDto.r_PriceSegus = priceSegus;
                        objmedicalexamDto.v_CodigoSegus = txtCodigoSegus.Text;
                        objmedicalexamDto.i_KindOfService = Int32.Parse(ddlKindOfService.SelectedValue.ToString());
                        objmedicalexamDto.i_PriceIsRecharged = Int32.Parse(cbRecargable.SelectedValue.ToString());
                        // Save the data
                        _objMedicalExamBL.AddMedicalExam(ref objOperationResult, objmedicalexamDto, Globals.ClientSession.GetAsList());

                }
                else if (Mode == "Edit")
                {
                    bool pbIsChangeName;
                    float priceSegus;
                    if (txtTarifaSegus.Text == "")
                    {
                        priceSegus = 0;
                    }
                    else
                    {
                        priceSegus = float.Parse(txtTarifaSegus.Text);
                    }
                    
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

                    objmedicalexamDto.i_CategoryId = Convert.ToInt32(ddlCategoryId.SelectedNode.Parent.Tag);
                    objmedicalexamDto.r_BasePrice = float.Parse(unBasePrice.Text);
                    objmedicalexamDto.i_DiagnosableId = Convert.ToInt32(ddlDiagnosableId.SelectedValue);
                    objmedicalexamDto.i_ComponentTypeId = Convert.ToInt32(ddlComponentTypeId.SelectedValue);
                    objmedicalexamDto.i_UIIsVisibleId = Convert.ToInt32(ddlUIIsVisibleId.SelectedValue);
                    objmedicalexamDto.i_UIIndex = Convert.ToInt32(unUIIndex.Value);
                    objmedicalexamDto.i_IsApprovedId = Convert.ToInt32(ddlIsApprovedId.SelectedValue);
                    objmedicalexamDto.i_ValidInDays = Convert.ToInt32(unValidInDays.Value);
                    objmedicalexamDto.v_IdUnidadProductiva = ddlUnidadProductiva.SelectedValue.ToString();
                    objmedicalexamDto.r_PriceSegus = priceSegus;
                    objmedicalexamDto.v_CodigoSegus = txtCodigoSegus.Text;
                    objmedicalexamDto.i_KindOfService = Int32.Parse(ddlKindOfService.SelectedValue.ToString());
                    objmedicalexamDto.i_PriceIsRecharged = Int32.Parse(cbRecargable.SelectedValue.ToString());
                    objmedicalexamDto.v_ComentaryUpdate = GetChanges();
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
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
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

        private void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            frmAddLineSAM frm = new frmAddLineSAM();
            frm.Show();
        }

        

        private void ddlCategoryId_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (ddlCategoryId.Path.Split('\\').Length == 2 || ddlCategoryId.Path.Split('\\').Length == 1 && ddlCategoryId.Text != "--Seleccionar--")
            {
                ddlCategoryId.SelectedNode = valueCategoryId;
            }

            valueCategoryId = ddlCategoryId.SelectedNode;
            var nroOrden = ddlCategoryId.SelectedNode.Tag.ToString();
            unUIIndex.Text = nroOrden;
        }


    }
}
