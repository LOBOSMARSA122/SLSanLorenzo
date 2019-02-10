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
    public partial class frmMedicalExamFieldValueEdicion : Form
    {
        string Mode;
        string MedicalExamFliedId;
        string _MedicalExamFliedValueId;
        private int _rowIndexRestriction;
        private int _rowIndexRecommendation;
        private string _ComponentFieldValuesRestrictionId = string.Empty;
        private string _ComponentFieldValuesRecommendationId = string.Empty;

        private DiseasesList _objDiseasesList = new DiseasesList();

        List<ComponentFieldValuesRestrictionList> _TempComponentFieldValuesRestrictionList = null;
        ComponentFieldValuesRestrictionList _objcomponentfieldvaluesrestriction = null;

        List<ComponentFieldValuesRecommendationList> _TempComponentFieldValuesRecommendationList = null;
        ComponentFieldValuesRecommendationList _objcomponentfieldvaluesrecommendation = null;
    
        MedicalExamFieldValuesBL _objMedicalExamFieldsBL = new MedicalExamFieldValuesBL();
        componentfieldvaluesDto _objmedicalexamfieldValuesDto;

        List<componentfieldvaluesrestrictionDto> _componentfieldvaluesrestrictionListDto = null;
        List<componentfieldvaluesrestrictionDto> _componentfieldvaluesrestrictionListDtoDelete = null;

        List<componentfieldvaluesrecommendationDto> _componentfieldvaluesrecommendationListDto = null;
        List<componentfieldvaluesrecommendationDto> _componentfieldvaluesrecommendationListDtoDelete = null;
        
        diseasesDto objdiseasesDto = new diseasesDto();

        public frmMedicalExamFieldValueEdicion(string strMedicalExamFieldId,string strMedicalExamFieldValueId, string pstrMode)
        {
            InitializeComponent();
            Mode = pstrMode;
            MedicalExamFliedId = strMedicalExamFieldId;
            _MedicalExamFliedValueId =strMedicalExamFieldValueId;
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
            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);
            }


            #endregion
            OperationResult objOperationResult = new OperationResult();

            MedicalExamFieldValuesBL objMedicalExamFieldValuesBLBL = new MedicalExamFieldValuesBL();            

            //Llenado de combos
            Utils.LoadDropDownList(ddlOperatorId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 129, "v_Value2"), DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlRestrictionId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 107, null), DropDownListAction.Select);
            //Utils.LoadDropDownList(ddlRecommendationId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 112, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlIsAnormal, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSexTypeId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 100, null), DropDownListAction.Select);
            txtAnalyzeValue1.Select();
              if (Mode == "New")
            {
                // Additional logic here.

            }
              else if (Mode == "Edit")
              {
                  // Get the Entity Data
                  _objmedicalexamfieldValuesDto = new componentfieldvaluesDto();

                  _objmedicalexamfieldValuesDto = _objMedicalExamFieldsBL.GetMedicalExamFieldValues(ref objOperationResult, _MedicalExamFliedValueId);

                  txtAnalyzeValue1.Text =_objmedicalexamfieldValuesDto.v_AnalyzingValue1.ToString();
                  txtAnalyzeValue2.Text = _objmedicalexamfieldValuesDto.v_AnalyzingValue2.ToString() == "0" ? string.Empty : _objmedicalexamfieldValuesDto.v_AnalyzingValue2.ToString();
                  ddlOperatorId.SelectedValue = _objmedicalexamfieldValuesDto.i_OperatorId.ToString();
                  txtLegalStandard.Text = _objmedicalexamfieldValuesDto.v_LegalStandard;
                  ddlIsAnormal.SelectedValue = _objmedicalexamfieldValuesDto.i_IsAnormal.ToString();
                  objdiseasesDto=   _objMedicalExamFieldsBL.GetDiseases(ref objOperationResult, _objmedicalexamfieldValuesDto.v_DiseasesId);

                  ddlSexTypeId.SelectedValue = _objmedicalexamfieldValuesDto.i_GenderId.ToString()  ;

                  txtDiagnostic.Text = objdiseasesDto == null ? String.Empty : objdiseasesDto.v_Name + " - CÓDIGO CIE10 : " + objdiseasesDto.v_CIE10Id;
                  _objDiseasesList.v_DiseasesId = objdiseasesDto == null ? null : objdiseasesDto.v_DiseasesId;
                  //_objDiseasesList.

                  _TempComponentFieldValuesRestrictionList = objMedicalExamFieldValuesBLBL.GetRestrictionsPagedAndFiltered(ref objOperationResult, 0, null, "", "v_ComponentFieldValuesId==  \"" + _objmedicalexamfieldValuesDto.v_ComponentFieldValuesId + "\"");
                  grdDataRestrictions.DataSource = _TempComponentFieldValuesRestrictionList;

                  _TempComponentFieldValuesRecommendationList = objMedicalExamFieldValuesBLBL.GetRecommendationsPagedAndFiltered(ref objOperationResult, 0, null, "", "v_ComponentFieldValuesId==  \"" + _objmedicalexamfieldValuesDto.v_ComponentFieldValuesId + "\"");
                  grdDataRecommendation.DataSource = _TempComponentFieldValuesRecommendationList;

              }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            _componentfieldvaluesrestrictionListDto = new List<componentfieldvaluesrestrictionDto>();
            _componentfieldvaluesrecommendationListDto = new List<componentfieldvaluesrecommendationDto>();

            if (uvMedicalExamFiedValues.Validate(true, false).IsValid)
            {
                if (txtAnalyzeValue1.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para el Nombre Valor Analizar 1.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("¿Está seguro de agregar / modificar la Campos al examen?.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                if (Mode == "New")
                {
                    _objmedicalexamfieldValuesDto = new componentfieldvaluesDto();
                    // Populate the entity+
                    _objmedicalexamfieldValuesDto.v_ComponentFieldId = MedicalExamFliedId;
                    _objmedicalexamfieldValuesDto.v_AnalyzingValue1 = txtAnalyzeValue1.Text;
                    _objmedicalexamfieldValuesDto.v_AnalyzingValue2 = txtAnalyzeValue2.Text;
                    _objmedicalexamfieldValuesDto.i_OperatorId =  Int32.Parse(ddlOperatorId.SelectedValue.ToString());
                    _objmedicalexamfieldValuesDto.i_GenderId = int.Parse(ddlSexTypeId.SelectedValue.ToString());
                    _objmedicalexamfieldValuesDto.v_LegalStandard =txtLegalStandard.Text;
                    _objmedicalexamfieldValuesDto.i_IsAnormal = Int32.Parse( ddlIsAnormal.SelectedValue.ToString());
                    _objmedicalexamfieldValuesDto.v_DiseasesId = _objDiseasesList.v_DiseasesId;
                    // Save the data
                  _MedicalExamFliedValueId= _objMedicalExamFieldsBL.AddMedicalExamFieldValues(ref objOperationResult,_TempComponentFieldValuesRestrictionList,_TempComponentFieldValuesRecommendationList, _objmedicalexamfieldValuesDto, Globals.ClientSession.GetAsList());
                    
                }
                else if (Mode == "Edit")
                {
                    _componentfieldvaluesrestrictionListDtoDelete = new List<componentfieldvaluesrestrictionDto>();
                    _componentfieldvaluesrecommendationListDtoDelete = new List<componentfieldvaluesrecommendationDto>();
                    // Populate the entity
                    _objmedicalexamfieldValuesDto.v_ComponentFieldId = MedicalExamFliedId;
                    _objmedicalexamfieldValuesDto.v_AnalyzingValue1 = txtAnalyzeValue1.Text;
                    _objmedicalexamfieldValuesDto.v_AnalyzingValue2 = txtAnalyzeValue2.Text;
                    _objmedicalexamfieldValuesDto.i_OperatorId = Int32.Parse(ddlOperatorId.SelectedValue.ToString());
                    _objmedicalexamfieldValuesDto.i_GenderId = int.Parse(ddlSexTypeId.SelectedValue.ToString());

                    _objmedicalexamfieldValuesDto.v_LegalStandard = txtLegalStandard.Text;
                    _objmedicalexamfieldValuesDto.i_IsAnormal = Int32.Parse(ddlIsAnormal.SelectedValue.ToString());
                    _objmedicalexamfieldValuesDto.v_DiseasesId = _objDiseasesList.v_DiseasesId;

                    //Temporal de Restricción
                    foreach (var item in _TempComponentFieldValuesRestrictionList)
                    {
                         // Add
                        if (item.i_RecordType == (int)RecordType.Temporal && item.i_RecordStatus == (int)RecordStatus.Agregado)
                        {
                            componentfieldvaluesrestrictionDto componentfieldvaluesrestrictionDto = new componentfieldvaluesrestrictionDto();

                            componentfieldvaluesrestrictionDto.v_ComponentFieldValuesRestrictionId = item.v_ComponentFieldValuesRestrictionId;
                            componentfieldvaluesrestrictionDto.v_ComponentFieldValuesId = item.v_ComponentFieldValuesId;
                            componentfieldvaluesrestrictionDto.v_MasterRecommendationRestricctionId = item.v_MasterRecommendationRestricctionId;

                            _componentfieldvaluesrestrictionListDto.Add(componentfieldvaluesrestrictionDto);
                        }

                        // Delete
                        if (item.i_RecordType == (int)RecordType.NoTemporal && item.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                        {
                            componentfieldvaluesrestrictionDto componentfieldvaluesrestrictionDto = new componentfieldvaluesrestrictionDto();

                            componentfieldvaluesrestrictionDto.v_ComponentFieldValuesRestrictionId = item.v_ComponentFieldValuesRestrictionId;
                            _componentfieldvaluesrestrictionListDtoDelete.Add(componentfieldvaluesrestrictionDto);
                        }  
                    }

                    //Temporal de Recomendaciones
                    foreach (var item in _TempComponentFieldValuesRecommendationList)
                    {
                        // Add
                        if (item.i_RecordType == (int)RecordType.Temporal && item.i_RecordStatus == (int)RecordStatus.Agregado)
                        {
                            componentfieldvaluesrecommendationDto componentfieldvaluesrecommendationDto = new componentfieldvaluesrecommendationDto();

                            componentfieldvaluesrecommendationDto.v_ComponentFieldValuesRecommendationId = item.v_ComponentFieldValuesRecommendationId;
                            componentfieldvaluesrecommendationDto.v_ComponentFieldValuesId = item.v_ComponentFieldValuesId;
                            componentfieldvaluesrecommendationDto.v_MasterRecommendationRestricctionId = item.v_MasterRecommendationRestricctionId;

                            _componentfieldvaluesrecommendationListDto.Add(componentfieldvaluesrecommendationDto);
                        }

                        // Delete
                        if (item.i_RecordType == (int)RecordType.NoTemporal && item.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                        {
                            componentfieldvaluesrecommendationDto componentfieldvaluesrecommendationDto = new componentfieldvaluesrecommendationDto();

                            componentfieldvaluesrecommendationDto.v_ComponentFieldValuesRecommendationId = item.v_ComponentFieldValuesRecommendationId;
                            _componentfieldvaluesrecommendationListDtoDelete.Add(componentfieldvaluesrecommendationDto);
                        }
                    }

                    // Save the data
                    _objMedicalExamFieldsBL.UpdateMedicalExamFieldValues(ref objOperationResult,
                        _componentfieldvaluesrestrictionListDto, 
                        _componentfieldvaluesrestrictionListDtoDelete,
                        _componentfieldvaluesrecommendationListDto,
                        _componentfieldvaluesrecommendationListDtoDelete,
                        _objmedicalexamfieldValuesDto, 
                        Globals.ClientSession.GetAsList());

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

        private void btnDiseases_Click(object sender, EventArgs e)
        {
            frmDiseases frm = new frmDiseases();           
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.Cancel) return;         

            _objDiseasesList = frm._objDiseasesList;

            if (_objDiseasesList.v_CIE10Id == null)
            {
                //txtDiagnostic.Text = String.Empty;
            }
            else
            {
                txtDiagnostic.Text = _objDiseasesList.v_Name + " / " + _objDiseasesList.v_CIE10Id;
            }
                
        }     
   
        private void ddlIsAnormal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIsAnormal.SelectedValue.ToString() == ((int)Common.SiNo.NO).ToString())
            {
                txtDiagnostic.Text = String.Empty;
                _objDiseasesList.v_DiseasesId = null;
                _objDiseasesList.v_CIE10Id = null;
                _objDiseasesList.v_Name = null;
         
            }
        }

        #region Restriction

        private void btnNewRestriction_Click(object sender, EventArgs e)
        {
            frmMasterRecommendationRestricction frm = new frmMasterRecommendationRestricction("Restricciones",(int)Common.Typifying.Restricciones,Common.ModeOperation.Total);
            frm.ShowDialog();

            if (frm._masterRecommendationRestricctionId != null)
            {
                if (_TempComponentFieldValuesRestrictionList == null)
                {
                    _TempComponentFieldValuesRestrictionList = new List<ComponentFieldValuesRestrictionList>();
                }
                _objcomponentfieldvaluesrestriction = new ComponentFieldValuesRestrictionList();

                var findResult = _TempComponentFieldValuesRestrictionList.Find(p => p.v_MasterRecommendationRestricctionId == frm._masterRecommendationRestricctionId.ToString());
                if (findResult == null)
                {
                    _objcomponentfieldvaluesrestriction.v_ComponentFieldValuesRestrictionId = Guid.NewGuid().ToString();
                    _objcomponentfieldvaluesrestriction.v_ComponentFieldValuesId = _MedicalExamFliedValueId;
                    _objcomponentfieldvaluesrestriction.i_RecordStatus = (int)RecordStatus.Agregado;
                    _objcomponentfieldvaluesrestriction.i_RecordType = (int)RecordType.Temporal;
                    _objcomponentfieldvaluesrestriction.v_MasterRecommendationRestricctionId = frm._masterRecommendationRestricctionId.ToString();
                    _objcomponentfieldvaluesrestriction.v_RestrictionName = frm._masterRecommendationRestricctionName.ToString();

                    _TempComponentFieldValuesRestrictionList.Add(_objcomponentfieldvaluesrestriction);
                }
                else // La restriccion ya esta agregado en la bolsa hay que actualizar su estado
                {
                    if (findResult.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                    {
                        if (findResult.i_RecordType == (int)RecordType.NoTemporal)   // El registro Tiene in ID de BD
                        {
                            findResult.v_MasterRecommendationRestricctionId = frm._masterRecommendationRestricctionId.ToString();
                            findResult.v_RestrictionName = frm._masterRecommendationRestricctionName.ToString();
                            findResult.i_RecordStatus = (int)RecordStatus.Grabado;
                        }
                        else if (findResult.i_RecordType == (int)RecordType.Temporal)   // El registro tiene un ID temporal [GUID]
                        {
                            findResult.v_MasterRecommendationRestricctionId = frm._masterRecommendationRestricctionId.ToString();
                            findResult.v_RestrictionName = frm._masterRecommendationRestricctionName.ToString();
                            findResult.i_RecordStatus = (int)RecordStatus.Agregado;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor seleccione otra Recomendación. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                var dataList = _TempComponentFieldValuesRestrictionList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                // Cargar grilla
                grdDataRestrictions.DataSource = new ComponentFieldValuesRestrictionList();
                grdDataRestrictions.DataSource = dataList;
                grdDataRestrictions.Refresh();
            }

        }

        private void grdDataRestrictions_MouseDown(object sender, MouseEventArgs e)
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
                grdDataRestrictions.Rows[row.Index].Selected = true;
                contextMenuRestriction.Items["mnuRemoveRestriction"].Enabled = true;
                _rowIndexRestriction = row.Index;

                if (grdDataRestrictions.Selected.Rows[0].Cells[0].Value != null)
                {
                    _ComponentFieldValuesRestrictionId = grdDataRestrictions.Selected.Rows[0].Cells[0].Value.ToString();
                }
            }
            else
            {
                contextMenuRestriction.Items["mnuRemoveRestriction"].Enabled = false;
            }
        }

        private void mnuRemoveRestriction_Click(object sender, EventArgs e)
        {
            if (Mode == "New")
            {
                _TempComponentFieldValuesRestrictionList.RemoveAt(_rowIndexRestriction);
                grdDataRestrictions.DataSource = new ComponentFieldValuesRestrictionList();
                grdDataRestrictions.DataSource = _TempComponentFieldValuesRestrictionList;
                grdDataRestrictions.Refresh();
            }
            else if (Mode == "Edit")
            {
                var findResult = _TempComponentFieldValuesRestrictionList.Find(p => p.v_ComponentFieldValuesRestrictionId == _ComponentFieldValuesRestrictionId);
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;

                var dataList = _TempComponentFieldValuesRestrictionList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdDataRestrictions.DataSource = new ComponentFieldValuesRestrictionList();
                grdDataRestrictions.DataSource = dataList;
                grdDataRestrictions.Refresh();
            }
        }

        #endregion

        #region Recommendation

        private void btnNewRecommendation_Click(object sender, EventArgs e)
        {
            frmMasterRecommendationRestricction frm = new frmMasterRecommendationRestricction(" Recomendaciones ",(int)Common.Typifying.Recomendaciones,Common.ModeOperation.Total);
            frm.ShowDialog();

                if (_TempComponentFieldValuesRecommendationList == null)
                {
                    _TempComponentFieldValuesRecommendationList = new List<ComponentFieldValuesRecommendationList>();
                }
                _objcomponentfieldvaluesrecommendation = new ComponentFieldValuesRecommendationList();

                if (frm._masterRecommendationRestricctionId != null)
                {                   
              
                var findResult = _TempComponentFieldValuesRecommendationList.Find(p => p.v_MasterRecommendationRestricctionId == frm._masterRecommendationRestricctionId.ToString());
                if (findResult == null)
                {
                    _objcomponentfieldvaluesrecommendation.v_ComponentFieldValuesRecommendationId = Guid.NewGuid().ToString();
                    _objcomponentfieldvaluesrecommendation.v_ComponentFieldValuesId = _MedicalExamFliedValueId;
                    _objcomponentfieldvaluesrecommendation.i_RecordStatus = (int)RecordStatus.Agregado;
                    _objcomponentfieldvaluesrecommendation.i_RecordType = (int)RecordType.Temporal;
                    _objcomponentfieldvaluesrecommendation.v_MasterRecommendationRestricctionId = frm._masterRecommendationRestricctionId.ToString();
                    _objcomponentfieldvaluesrecommendation.v_RecomendationName = frm._masterRecommendationRestricctionName.ToString();

                    _TempComponentFieldValuesRecommendationList.Add(_objcomponentfieldvaluesrecommendation);
                }
                else// La restriccion ya esta agregado en la bolsa hay que actualizar su estado
                {
                    if (findResult.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                    {
                        if (findResult.i_RecordType == (int)RecordType.NoTemporal)   // El registro Tiene in ID de BD
                        {
                            findResult.v_MasterRecommendationRestricctionId = frm._masterRecommendationRestricctionId.ToString();
                            findResult.v_RecomendationName = frm._masterRecommendationRestricctionName.ToString();
                            findResult.i_RecordStatus = (int)RecordStatus.Grabado;
                        }
                        else if (findResult.i_RecordType == (int)RecordType.Temporal)   // El registro tiene un ID temporal [GUID]
                        {
                            findResult.v_MasterRecommendationRestricctionId = frm._masterRecommendationRestricctionId.ToString();
                            findResult.v_RecomendationName = frm._masterRecommendationRestricctionName.ToString();
                            findResult.i_RecordStatus = (int)RecordStatus.Agregado;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor seleccione otra Recomendación. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                var dataList = _TempComponentFieldValuesRecommendationList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                // Cargar grilla
                grdDataRecommendation.DataSource = new ComponentFieldValuesRecommendationList();
                grdDataRecommendation.DataSource = dataList;
                grdDataRecommendation.Refresh();
                }

        }

        private void mnuRemoveRecommendation_Click(object sender, EventArgs e)
        {
            if (Mode == "New")
            {
                 _TempComponentFieldValuesRecommendationList.RemoveAt(_rowIndexRecommendation);
                grdDataRecommendation.DataSource = new ComponentFieldValuesRecommendationList();
                grdDataRecommendation.DataSource = _TempComponentFieldValuesRecommendationList;
                grdDataRecommendation.Refresh();
            }
            else if (Mode == "Edit")
            {
                var findResult = _TempComponentFieldValuesRecommendationList.Find(p => p.v_ComponentFieldValuesRecommendationId == _ComponentFieldValuesRecommendationId);
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;

                var dataList = _TempComponentFieldValuesRecommendationList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdDataRecommendation.DataSource = new ComponentFieldValuesRecommendationList();
                grdDataRecommendation.DataSource = dataList;
                grdDataRecommendation.Refresh();
            }
        }

        private void grdDataRecommendation_MouseDown(object sender, MouseEventArgs e)
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
                grdDataRecommendation.Rows[row.Index].Selected = true;
                contextMenuRecommendation.Items["mnuRemoveRecommendation"].Enabled = true;
                _rowIndexRecommendation = row.Index;

                if (grdDataRecommendation.Selected.Rows[0].Cells[0].Value != null)
                {
                    _ComponentFieldValuesRecommendationId = grdDataRecommendation.Selected.Rows[0].Cells[0].Value.ToString();
                }
            }
            else
            {
                contextMenuRecommendation.Items["mnuRemoveRecommendation"].Enabled = false;
            }
        }
        #endregion

        private void grdDataRestrictions_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Mode == "New")
            {
                _TempComponentFieldValuesRecommendationList.RemoveAt(_rowIndexRecommendation);
                grdDataRecommendation.DataSource = new ComponentFieldValuesRecommendationList();
                grdDataRecommendation.DataSource = _TempComponentFieldValuesRecommendationList;
                grdDataRecommendation.Refresh();
            }
            else if (Mode == "Edit")
            {
                var findResult = _TempComponentFieldValuesRecommendationList.Find(p => p.v_ComponentFieldValuesRecommendationId == _ComponentFieldValuesRecommendationId);
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;

                var dataList = _TempComponentFieldValuesRecommendationList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdDataRecommendation.DataSource = new ComponentFieldValuesRecommendationList();
                grdDataRecommendation.DataSource = dataList;
                grdDataRecommendation.Refresh();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Mode == "New")
            {
                _TempComponentFieldValuesRestrictionList.RemoveAt(_rowIndexRestriction);
                grdDataRestrictions.DataSource = new ComponentFieldValuesRestrictionList();
                grdDataRestrictions.DataSource = _TempComponentFieldValuesRestrictionList;
                grdDataRestrictions.Refresh();
            }
            else if (Mode == "Edit")
            {
                var findResult = _TempComponentFieldValuesRestrictionList.Find(p => p.v_ComponentFieldValuesRestrictionId == _ComponentFieldValuesRestrictionId);
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;

                var dataList = _TempComponentFieldValuesRestrictionList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdDataRestrictions.DataSource = new ComponentFieldValuesRestrictionList();
                grdDataRestrictions.DataSource = dataList;
                grdDataRestrictions.Refresh();
            }
        }

        private void grdDataRecommendation_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            btnRemoverRecomedaciones.Enabled = (grdDataRecommendation.Selected.Rows.Count > 0);
        }

        private void grdDataRestrictions_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            btnRemoverRestricciones.Enabled = (grdDataRestrictions.Selected.Rows.Count > 0);
        }

      

    }
}
