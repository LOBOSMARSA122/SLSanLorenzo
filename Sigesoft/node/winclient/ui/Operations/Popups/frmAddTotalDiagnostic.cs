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

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class frmAddTotalDiagnostic : Form
    {
        #region Declarations

        private ServiceBL _serviceBL = new ServiceBL();
        private DiagnosticRepositoryList _diagnosticRepository = null;
        string _mode = string.Empty;
        /// <summary>
        ///  Almacena Temporalmente la lista de los diagnósticos totales en el form [frmAddTotalDiagnostic]
        /// </summary>      
        public List<DiagnosticRepositoryList> _tmpTotalDiagnosticList = null;
        public string _diagnosticId = null;
        /// <summary>
        /// PK de tabla Temporal para realizar una busqueda y saber que registro selecionar
        /// </summary>
        public string _diagnosticRepositoryId = null;
        private List<RestrictionList> _tmpRestrictionByDiagnosticList = null;
        private List<RecomendationList> _tmpRecomendationList = null;
        public string _componentId;
        public string _serviceId;
        private List<KeyValueDTO> _componentListTemp = new List<KeyValueDTO>();
        private List<string> _componentIds;
        private string _examenId;
        string strFilterExpression;
        List<DxFrecuenteList> _TempListaDxfrecuentes = new List<DxFrecuenteList>();
        private string _DiseasesId = "";
        private string _CIE10Id = "";
        string _DxFrecuenteId = null;
        #endregion

        public frmAddTotalDiagnostic()
        {
            InitializeComponent();
        }

        private void frmAddTotalDiagnostic_Load(object sender, EventArgs e)
        {
            // Llenado de combos

            OperationResult objOperationResult = new OperationResult();
       
            Utils.LoadDropDownList(cbCalificacionFinal, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 138, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbTipoDx, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 139, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbEnviarAntecedentes, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);
            LoadOffice();
            // Setear valor x default 

            cbCalificacionFinal.SelectedValue = ((int)FinalQualification.Presuntivo).ToString();
            cbTipoDx.SelectedValue = ((int)TipoDx.Otros).ToString();
            cbEnviarAntecedentes.SelectedValue = ((int)SiNo.NO).ToString();

            if (_mode == "New")
            {
                // Setear valores por defecto             

            }
            else if (_mode == "Edit")
            {
               
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (uvAddTotalDiagnostic.Validate(true, false).IsValid)
            {
                OperationResult objOperationResult = new OperationResult();

                if (_tmpTotalDiagnosticList == null)             
                    _tmpTotalDiagnosticList = new List<DiagnosticRepositoryList>();
               
                DiagnosticRepositoryList diagnosticRepository = new DiagnosticRepositoryList();

                diagnosticRepository.v_ServiceId = _serviceId;
                diagnosticRepository.v_DiseasesId = _diagnosticId;
                diagnosticRepository.i_AutoManualId  = (int?)AutoManual.Manual;             
                diagnosticRepository.i_PreQualificationId = (int?)PreQualification.Aceptado;
                diagnosticRepository.i_FinalQualificationId = int.Parse(cbCalificacionFinal.SelectedValue.ToString());
                diagnosticRepository.i_DiagnosticTypeId = int.Parse(cbTipoDx.SelectedValue.ToString());
                diagnosticRepository.i_IsSentToAntecedent = int.Parse(cbEnviarAntecedentes.SelectedValue.ToString());
                diagnosticRepository.d_ExpirationDateDiagnostic = dtpFechaVcto.Checked ? dtpFechaVcto.Value.Date : (DateTime?)null;
                //diagnosticRepository.v_ComponentId = _componentIds[0];
                diagnosticRepository.v_ComponentId = _examenId;

                diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                diagnosticRepository.i_RecordType = (int)RecordType.Temporal;
                diagnosticRepository.Restrictions = _tmpRestrictionByDiagnosticList;
                diagnosticRepository.Recomendations = _tmpRecomendationList;

                _tmpTotalDiagnosticList.Add(diagnosticRepository);

                // Grabar DX + restricciones / recomendaciones
                _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                    _tmpTotalDiagnosticList,
                                                    null,
                                                    Globals.ClientSession.GetAsList(),
                                                    null,null);

                // Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    this.DialogResult = DialogResult.OK;
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

        private void btnAgregarDx_Click(object sender, EventArgs e)
        {
            DiseasesList returnDiseasesList = new DiseasesList();
            frmDiseases frm = new frmDiseases();

            frm.ShowDialog();
            returnDiseasesList = frm._objDiseasesList;

            if (returnDiseasesList.v_DiseasesId != null)
            {
                lblDiagnostico.Text = returnDiseasesList.v_Name + " / " + returnDiseasesList.v_CIE10Id;
                _diagnosticId = returnDiseasesList.v_DiseasesId;
                _DiseasesId = returnDiseasesList.v_DiseasesId;
                _CIE10Id = returnDiseasesList.v_CIE10Id;
            }
        }

        private void btnAgregarRestriccion_Click(object sender, EventArgs e)
        {
            var x = (KeyValueDTO)ddlComponentId.SelectedItem;
           

            if (x.Value4== 0.0)
            {
                MessageBox.Show("Por favor seleccione un consultorio", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboExamen.SelectedValue== null)
            {
                MessageBox.Show("Por favor seleccione un Examen", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var frm = new frmMasterRecommendationRestricction("Restricciones",(int)Typifying.Restricciones, ModeOperation.Total);
            frm.ShowDialog();   

            if (_tmpRestrictionByDiagnosticList == null)
            {
                _tmpRestrictionByDiagnosticList = new List<RestrictionList>();
            }

            var restrictionId = frm._masterRecommendationRestricctionId;
            var restrictionName = frm._masterRecommendationRestricctionName;

            if (restrictionId != null && restrictionName != null)
            {
                var restriction = _tmpRestrictionByDiagnosticList.Find(p => p.v_MasterRestrictionId == restrictionId);

                if (restriction == null)   // agregar con normalidad [insert]  a la bolsa  
                {
                    // Agregar restricciones a la Lista
                    RestrictionList restrictionByDiagnostic = new RestrictionList();

                    restrictionByDiagnostic.v_RestrictionByDiagnosticId = Guid.NewGuid().ToString();
                    restrictionByDiagnostic.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                    restrictionByDiagnostic.v_MasterRestrictionId = restrictionId;
                    restrictionByDiagnostic.v_ServiceId = _serviceId;
                    restrictionByDiagnostic.v_ComponentId = _componentId;
                    restrictionByDiagnostic.v_RestrictionName = restrictionName;
                    restrictionByDiagnostic.i_RecordStatus = (int)RecordStatus.Agregado;
                    restrictionByDiagnostic.i_RecordType = (int)RecordType.Temporal;
                    //restrictionByDiagnostic.v_ComponentId = _componentIds[0];
                    restrictionByDiagnostic.v_ComponentId = _examenId;

                    _tmpRestrictionByDiagnosticList.Add(restrictionByDiagnostic);

                }
                else    // La restriccion ya esta agregado en la bolsa 
                {
                    MessageBox.Show("Por favor seleccione otra Restriccón. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Cargar grilla
            grdRestricciones.DataSource = new RestrictionList();
            grdRestricciones.DataSource = _tmpRestrictionByDiagnosticList;
            grdRestricciones.Refresh();
            lblRecordCountRecomendaciones_AnalisisDx.Text = string.Format("Se encontraron {0} registros.", _tmpRestrictionByDiagnosticList.Count());

        }

        private void btnAgregarRecomendaciones_Click(object sender, EventArgs e)
        {
            var x = (KeyValueDTO)ddlComponentId.SelectedItem;


            if (x.Value4 == 0.0)
            {
                MessageBox.Show("Por favor seleccione un consultorio", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboExamen.SelectedValue == null)
            {
                MessageBox.Show("Por favor seleccione un Examen", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var frm = new frmMasterRecommendationRestricction("Recomendaciones" ,(int)Typifying.Recomendaciones, ModeOperation.Total);
            frm.ShowDialog(); 
           
            if (_tmpRecomendationList == null)
            {
                _tmpRecomendationList = new List<RecomendationList>();
            }

            var recomendationId = frm._masterRecommendationRestricctionId;
            var recommendationName = frm._masterRecommendationRestricctionName;

            if (recomendationId != null && recommendationName != null)
            {
                var recomendation = _tmpRecomendationList.Find(p => p.v_RecommendationId == recomendationId);

                if (recomendation == null)   // agregar con normalidad [insert]  a la bolsa  
                {
                    // Agregar restricciones a la Lista
                    RecomendationList recomendationList = new RecomendationList();

                    recomendationList.v_RecommendationId = Guid.NewGuid().ToString();
                    recomendationList.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                    recomendationList.v_MasterRecommendationId = recomendationId;
                    recomendationList.v_ServiceId = _serviceId;
                    recomendationList.v_ComponentId = _componentId;
                    recomendationList.v_RecommendationName = recommendationName;
                    recomendationList.i_RecordStatus = (int)RecordStatus.Agregado;
                    recomendationList.i_RecordType = (int)RecordType.Temporal;
                    //recomendationList.v_ComponentId = _componentIds[0];
                    recomendationList.v_ComponentId = _examenId;

                    _tmpRecomendationList.Add(recomendationList);

                }
                else    // La restriccion ya esta agregado en la bolsa hay que actualizar su estado
                {
                    MessageBox.Show("Por favor seleccione otra Recomendación. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;

                }
            }

            // Cargar grilla
            grdRecomendaciones.DataSource = new RecomendationList();
            grdRecomendaciones.DataSource = _tmpRecomendationList;
            grdRecomendaciones.Refresh();
            lblRecordCountRecomendaciones_AnalisisDx.Text = string.Format("Se encontraron {0} registros.", _tmpRecomendationList.Count());

        }

        private void btnRemoverRestriccion_Click(object sender, EventArgs e)
        {
            if (grdRestricciones.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un registro.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Capturar id desde la grilla de restricciones
            var restrictionId = grdRestricciones.Selected.Rows[0].Cells["v_MasterRestrictionId"].Value.ToString();

            // Buscar registro para remover
            var findResult = _tmpRestrictionByDiagnosticList.Find(p => p.v_MasterRestrictionId == restrictionId);
            // Borrado logico
            _tmpRestrictionByDiagnosticList.Remove(findResult);
                
            grdRestricciones.DataSource = new RestrictionList();
            grdRestricciones.DataSource = _tmpRestrictionByDiagnosticList;
            grdRestricciones.Refresh();
            lblRecordCountRestricciones_AnalisisDx.Text = string.Format("Se encontraron {0} registros.", _tmpRestrictionByDiagnosticList.Count());         

        }

        private void btnRemoverRecomendacion_Click(object sender, EventArgs e)
        {
            if (grdRecomendaciones.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un registro.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Capturar id desde la grilla de restricciones
            var recommendationId = grdRecomendaciones.Selected.Rows[0].Cells["v_MasterRecommendationId"].Value.ToString();

            // Buscar registro para remover
            var findResult = _tmpRecomendationList.Find(p => p.v_MasterRecommendationId == recommendationId);
            // Borrado logico
            _tmpRecomendationList.Remove(findResult);

            grdRecomendaciones.DataSource = new RecomendationList();
            grdRecomendaciones.DataSource = _tmpRecomendationList;
            grdRecomendaciones.Refresh();
            lblRecordCountRecomendaciones_AnalisisDx.Text = string.Format("Se encontraron {0} registros.", _tmpRecomendationList.Count());         

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void LoadOffice()
        {

            // Obtener permisos de cada examen de un rol especifico
            var componentProfile = _serviceBL.GetRoleNodeComponentProfileByRoleNodeId(Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_RoleId.Value);

            //*********************************************

            ddlComponentId.SelectedValueChanged -= ddlComponentId_SelectedValueChanged;

            OperationResult objOperationResult = new OperationResult();

            _componentListTemp = BLL.Utils.GetAllComponentsByRoleNodeId(Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_RoleId.Value);
            //_componentListTemp = _componentListTemp.FindAll(f => componentProfile.Any(t => t.v_ComponentId == f.Value2));

            var xxx = _componentListTemp.FindAll(p => p.Value4 != -1);

            List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();

            groupComponentList.AddRange(_componentListTemp.ToList().FindAll(p => p.Value4 == -1));

            // Remover los componentes que no estan asignados al rol del usuario
            //var results = groupComponentList.FindAll(f => componentProfile.Any(t => t.v_ComponentId == f.Value2));
            //var dd = groupComponentList.FindAll(p => componentProfile.FindAll(o => o.v_ComponentId == p.Value2));

            Utils.LoadDropDownList(ddlComponentId, "Value1", "Id", groupComponentList, DropDownListAction.Select);

            ddlComponentId.SelectedValueChanged += ddlComponentId_SelectedValueChanged;
        }

        private void ddlComponentId_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlComponentId.SelectedIndex == 0)                     
                return;
                   
            _componentIds = new List<string>();
            var eee = (KeyValueDTO)ddlComponentId.SelectedItem;

            if (eee.Value4.ToString() == "-1")
            {
                _componentIds.Add(eee.Value2);
            }
            else
            {
                var ListaExamenes = _componentListTemp.FindAll(p => p.Value4 == eee.Value4);
                Utils.LoadDropDownList(cboExamen, "Value3", "Value2", ListaExamenes, DropDownListAction.Select);

                _componentIds = ListaExamenes.Select(s => s.Value2)
                                                .OrderBy(p => p).ToList();
            }

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void cbEnviarAntecedentes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (ultraValidator2.Validate(true, false).IsValid)
            {
                // Get the filters from the UI
                List<string> Filters = new List<string>();

                if (rbCode.Checked == true)
                {
                    if (!string.IsNullOrEmpty(txtDiseasesFilter.Text)) Filters.Add("v_CIE10Id.Contains(\"" + txtDiseasesFilter.Text.Trim() + "\")");
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtDiseasesFilter.Text)) Filters.Add("v_DiseasesName.Contains(\"" + txtDiseasesFilter.Text.Trim() + "\")");

                }

                // Create the Filter Expression
                strFilterExpression = null;
                if (Filters.Count > 0)
                {
                    foreach (string item in Filters)
                    {
                        strFilterExpression = strFilterExpression + item + " || ";
                    }
                    strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
                }
                this.BindGrid();
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void BindGrid()
        {
            var objData = GetData(0, null, "v_Name ASC, v_DiseasesId ASC", strFilterExpression);
            grdData.DataSource = objData;
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<DxFrecuenteList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DxFrecuenteBL oDxFrecuenteBL = new DxFrecuenteBL();

            var _objData = oDxFrecuenteBL.GetDxFrecuentes(ref objOperationResult, 0, null, "", pstrFilterExpression);  // _objMedicalExamFieldValuesBL.GetDiseasesPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);
            _TempListaDxfrecuentes = _objData;
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {

            //if (ddlComponentId.SelectedValue.ToString() == "-1")
            //{
            //    MessageBox.Show("Por favor seleccione un examen", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            var x = (KeyValueDTO)ddlComponentId.SelectedItem;


            if (x.Value4 == 0.0)
            {
                MessageBox.Show("Por favor seleccione un consultorio", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboExamen.SelectedValue == null)
            {
                MessageBox.Show("Por favor seleccione un Examen", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lblDiagnostico.Text = "";
            _tmpRecomendationList = new List<RecomendationList>();
            _tmpRestrictionByDiagnosticList = new List<RestrictionList>();

            var Filtro = _TempListaDxfrecuentes.Find(p => p.v_DxFrecuenteId == _DxFrecuenteId);

            lblDiagnostico.Text = Filtro.v_DiseasesName + " / " + Filtro.v_CIE10Id;
            _diagnosticId = Filtro.v_DiseasesId;

            var Recomendaciones = Filtro.DxFrecuenteDetalle.FindAll(p => p.i_Tipo != 2);

            foreach (var item in Recomendaciones)
            {
                if (_tmpRecomendationList == null)
                {
                    _tmpRecomendationList = new List<RecomendationList>();
                }

                var recomendation = _tmpRecomendationList.Find(p => p.v_MasterRecommendationId == item.v_MasterRecommendationRestricctionId);

                if (recomendation == null)   // agregar con normalidad [insert]  a la bolsa  
                {
                    RecomendationList recomendationList = new RecomendationList();

                    recomendationList.v_RecommendationId = Guid.NewGuid().ToString();
                    recomendationList.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                    recomendationList.v_MasterRecommendationId = item.v_MasterRecommendationRestricctionId;
                    recomendationList.v_ServiceId = _serviceId;
                    //recomendationList.v_ComponentId = _componentIds[0];
                    recomendationList.v_ComponentId = _examenId;
                    //recomendationList.v_ComponentId = ddlComponentId.SelectedValue.ToString(); //_componentId;
                    recomendationList.v_RecommendationName = item.v_RecomendacionName;
                    recomendationList.i_RecordStatus = (int)RecordStatus.Agregado;
                    recomendationList.i_RecordType = (int)RecordType.Temporal;

                    _tmpRecomendationList.Add(recomendationList);
                }
            }

            var dataListReco = _tmpRecomendationList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

            // Cargar grilla
            grdRecomendaciones.DataSource = new RecomendationList();
            grdRecomendaciones.DataSource = dataListReco;
            grdRecomendaciones.Refresh();
            lblRecordCountRecomendaciones_AnalisisDx.Text = string.Format("Se encontraron {0} registros.", dataListReco.Count());


            var Restricciones = Filtro.DxFrecuenteDetalle.FindAll(p => p.i_Tipo != 1);

            foreach (var item in Restricciones)
            {
                if (_tmpRestrictionByDiagnosticList == null)
                {
                    _tmpRestrictionByDiagnosticList = new List<RestrictionList>();
                }
                var restriction = _tmpRestrictionByDiagnosticList.Find(p => p.v_MasterRestrictionId == item.v_MasterRecommendationRestricctionId);

                if (restriction == null)   // agregar con normalidad [insert]  a la bolsa  
                {
                    RestrictionList restrictionByDiagnostic = new RestrictionList();
                    restrictionByDiagnostic.v_RestrictionByDiagnosticId = Guid.NewGuid().ToString();
                    restrictionByDiagnostic.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                    restrictionByDiagnostic.v_MasterRestrictionId = item.v_MasterRecommendationRestricctionId;
                    restrictionByDiagnostic.v_ServiceId = _serviceId;
                    //restrictionByDiagnostic.v_ComponentId = _componentIds[0];
                    restrictionByDiagnostic.v_ComponentId = _examenId;
                    //restrictionByDiagnostic.v_ComponentId = ddlComponentId.SelectedValue.ToString(); // _componentId;
                    restrictionByDiagnostic.v_RestrictionName = item.v_RestriccionName;
                    restrictionByDiagnostic.i_RecordStatus = (int)RecordStatus.Agregado;
                    restrictionByDiagnostic.i_RecordType = (int)RecordType.Temporal;

                    _tmpRestrictionByDiagnosticList.Add(restrictionByDiagnostic);
                }

            }


            var dataListRest = _tmpRestrictionByDiagnosticList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

            // Cargar grilla
            grdRestricciones.DataSource = new RestrictionList();
            grdRestricciones.DataSource = dataListRest;
            grdRestricciones.Refresh();
            lblRecordCountRestricciones_AnalisisDx.Text = string.Format("Se encontraron {0} registros.", dataListRest.Count());


        }

        private void btnSeleccionarDx_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (uvAddExamDiagnostic.Validate(true, false).IsValid)
            {
                if (_tmpTotalDiagnosticList == null)
                    _tmpTotalDiagnosticList = new List<DiagnosticRepositoryList>();

            
                    var findResult = _tmpTotalDiagnosticList.Find(p => p.v_DiagnosticRepositoryId == _diagnosticRepositoryId);

                    _diagnosticRepository = new DiagnosticRepositoryList();

                    if (findResult == null)   // agregar con normalidad  a la bolsa de DX 
                    {
                        //_diagnosticRepository.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                        //_diagnosticRepository.v_DiseasesId = _diagnosticId;
                        //_diagnosticRepository.i_AutoManualId = int.Parse(cbAutoManual.SelectedValue.ToString());
                        //_diagnosticRepository.i_PreQualificationId = int.Parse(cbPreCalificacion.SelectedValue.ToString());
                        ////_diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.SinCalificar;
                        //_diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                        //_diagnosticRepository.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                        //_diagnosticRepository.v_ServiceId = _serviceId;
                        //_diagnosticRepository.v_ComponentId = ddlComponentId.SelectedValue.ToString();  //_componentId;
                        //_diagnosticRepository.v_DiseasesName = lblDiagnostico.Text;
                        //_diagnosticRepository.v_AutoManualName = cbAutoManual.Text;
                        //_diagnosticRepository.v_RestrictionsName = ConcatenateRestrictions();
                        //_diagnosticRepository.v_RecomendationsName = ConcatenateRecommendations();
                        //_diagnosticRepository.v_PreQualificationName = cbPreCalificacion.Text;
                        //_diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                        //_diagnosticRepository.i_RecordType = (int)RecordType.Temporal;
                        //_diagnosticRepository.Restrictions = _tmpRestrictionByDiagnosticList;
                        //_diagnosticRepository.Recomendations = _tmpRecomendationList;


                        _diagnosticRepository.v_ServiceId = _serviceId;
                        _diagnosticRepository.v_DiseasesId = _diagnosticId;
                        _diagnosticRepository.i_AutoManualId = (int?)AutoManual.Manual;
                        _diagnosticRepository.i_PreQualificationId = (int?)PreQualification.Aceptado;
                        _diagnosticRepository.i_FinalQualificationId = int.Parse(cbCalificacionFinal.SelectedValue.ToString());
                        _diagnosticRepository.i_DiagnosticTypeId = int.Parse(cbTipoDx.SelectedValue.ToString());
                        _diagnosticRepository.i_IsSentToAntecedent = int.Parse(cbEnviarAntecedentes.SelectedValue.ToString());
                        _diagnosticRepository.d_ExpirationDateDiagnostic = dtpFechaVcto.Checked ? dtpFechaVcto.Value.Date : (DateTime?)null;
                        //_diagnosticRepository.v_ComponentId = _componentIds[0];
                        _diagnosticRepository.v_ComponentId = _examenId;

                        _diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                        _diagnosticRepository.i_RecordType = (int)RecordType.Temporal;
                        _diagnosticRepository.Restrictions = _tmpRestrictionByDiagnosticList;
                        _diagnosticRepository.Recomendations = _tmpRecomendationList;



                        _tmpTotalDiagnosticList.Add(_diagnosticRepository);

                        // Grabar DX + restricciones / recomendaciones
                        _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                            _tmpTotalDiagnosticList,
                                                            null,
                                                            Globals.ClientSession.GetAsList(),
                                                            null,false);

                        // Analizar el resultado de la operación
                        if (objOperationResult.Success == 1)  // Operación sin error
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else  // Operación con error
                        {
                            MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // Se queda en el formulario.
                        }
                    }
            
                //else if (_mode == "Edit")
                //{
                //    var findResult = _tmpTotalDiagnosticList.Find(p => p.v_DiagnosticRepositoryId == _diagnosticRepositoryId);

                //    findResult.v_DiseasesId = _diagnosticId == null ? findResult.v_DiseasesId : _diagnosticId;
                //    findResult.i_AutoManualId = int.Parse(cbAutoManual.SelectedValue.ToString());
                //    findResult.i_PreQualificationId = int.Parse(cbPreCalificacion.SelectedValue.ToString());
                //    //findResult.i_FinalQualificationId = (int)FinalQualification.SinCalificar;
                //    _diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                //    _diagnosticRepository.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                //    findResult.v_DiseasesName = lblDiagnostico.Text;
                //    findResult.v_AutoManualName = cbAutoManual.Text;
                //    findResult.v_RestrictionsName = ConcatenateRestrictions();
                //    findResult.v_RecomendationsName = ConcatenateRecommendations();
                //    findResult.v_PreQualificationName = cbPreCalificacion.Text;
                //    findResult.i_RecordStatus = (int)RecordStatus.Modificado;
                //    findResult.Restrictions = _tmpRestrictionByDiagnosticList;
                //    findResult.Recomendations = _tmpRecomendationList;
                //    findResult.v_ServiceId = _serviceId;

                //    findResult.v_ComponentId = ddlComponentId.SelectedValue.ToString();
                //}
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void grdData_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            foreach (UltraGridRow rowSelected in this.grdData.Selected.Rows)
            {
                if (rowSelected.Band.Index.ToString() == "0")
                {
                    _DxFrecuenteId = grdData.Selected.Rows[0].Cells["v_DxFrecuenteId"].Value.ToString();

                    btnSeleccionarDx.Enabled = true;

                    return;
                }
                else
                {
                    btnSeleccionarDx.Enabled = false;
                }
            }
        }

        private void cboExamen_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboExamen.SelectedValue == null) return;
            if (cboExamen.SelectedValue == "-1") return;

            _examenId = cboExamen.SelectedValue.ToString();
        }
    }
}
