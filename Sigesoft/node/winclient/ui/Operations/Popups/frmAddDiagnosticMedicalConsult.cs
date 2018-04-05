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
using Infragistics.Win;

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class frmAddDiagnosticMedicalConsult : Form
    {
        #region Declarations

        private ServiceBL _serviceBL = new ServiceBL();
        public string _mode = string.Empty;
        /// <summary>
        ///  Almacena Temporalmente la lista de los diagnósticos totales en el form [frmAddTotalDiagnostic]
        /// </summary>      
        public List<DiagnosticRepositoryList> _tmpDiagnosticList = null;
        public string _diagnosticId = null;
        /// <summary>
        /// PK de tabla Temporal para realizar una busqueda y saber que registro selecionar
        /// </summary>
        public string _diagnosticRepositoryId = null;
        public string _componentId;
        public string _serviceId;
        public DiseasesList _objDiseasesList = null;
        DiagnosticRepositoryList _diagnosticRepository = null;

        #endregion

        public frmAddDiagnosticMedicalConsult()
        {
            InitializeComponent();
        }

        private void frmAddTotalDiagnostic_Load(object sender, EventArgs e)
        {
            this.Height = 153;

            // Llenado de combos
            LoadComboBox();       

            if (_mode == "New")
            {
                // Setear valores por defecto 
                lblDiagnostico.Text = _objDiseasesList.v_Name;

            }
            else if (_mode == "Edit")
            {
                var findResult = _tmpDiagnosticList.Find(p => p.v_DiagnosticRepositoryId == _diagnosticRepositoryId);

                lblDiagnostico.Text = findResult.v_DiseasesName;

                var tipo = findResult.i_DiagnosticTypeId;
                var origen = findResult.i_DiagnosticSourceId;                     

                cbCalificacionFinal.SelectedValue = findResult.i_FinalQualificationId.ToString();
                cbEnviarAntecedentes.SelectedValue = findResult.i_IsSentToAntecedent.ToString();
                cbTipoOcurrencia.SelectedValue = tipo.ToString();
                cbOrigenOcurrencia.SelectedValue =  origen.ToString();

                if (tipo == (int)TipoOcurrencia.Accidente && origen == (int)OrigenOcurrencia.Laboral)
                {

                    #region Accidente laboral
                    //                 

                    var shapeAccidentItem = ((List<KeyValueDTO>)cbFormaAccidente.DataSource)
                                            .Find(p => p.Id == findResult.i_ShapeAccidentId.ToString());
                    var index1 = cbFormaAccidente.Items.IndexOf(shapeAccidentItem);

                    var bodyPartItem = ((List<KeyValueDTO>)cbParteCuerpo.DataSource)
                                       .Find(p => p.Id == findResult.i_BodyPartId.ToString());
                    var index2 = cbParteCuerpo.Items.IndexOf(bodyPartItem);

                    var classificationOfWorkAccidentItem = ((List<KeyValueDTO>)cbClasificacionAccLab.DataSource)
                                       .Find(p => p.Id == findResult.i_ClassificationOfWorkAccidentId.ToString());
                    var index3 = cbClasificacionAccLab.Items.IndexOf(classificationOfWorkAccidentItem);

                    cbFormaAccidente.SelectedIndex = index1;
                    cbParteCuerpo.SelectedIndex = index2;
                    cbClasificacionAccLab.SelectedIndex = index3; 
   
                    #endregion
                                  
                }
                else if (tipo == (int)TipoOcurrencia.Enfermedad && origen == (int)OrigenOcurrencia.Laboral)
                {
                    #region Enfermedad laboral

                    // Enfermedad laboral   
                    var riskFactorItem = ((List<KeyValueDTO>)cbFactorRiesgo.DataSource)
                                         .Find(p => p.Id == findResult.i_RiskFactorId.ToString());
                    var index1 = cbFactorRiesgo.Items.IndexOf(riskFactorItem);

                    var classificationOfWorkdiseaseItem = ((List<KeyValueDTO>)cbClasificacionEnfLab.DataSource)
                                                          .Find(p => p.Id == findResult.i_ClassificationOfWorkdiseaseId.ToString());
                    var index2 = cbClasificacionEnfLab.Items.IndexOf(classificationOfWorkdiseaseItem);

                    cbFactorRiesgo.SelectedIndex = index1;
                    cbClasificacionEnfLab.SelectedIndex = index2;   

                    #endregion
                            
                }
               
            }
        }

        private void LoadComboBox()
        {
            OperationResult objOperationResult = new OperationResult();

            cbTipoOcurrencia.SelectedValueChanged -= cbTipoOcurrencia_SelectedValueChanged;
            cbOrigenOcurrencia.SelectedValueChanged -= cbOrigenOcurrencia_SelectedValueChanged;

            Utils.LoadDropDownList(cbCalificacionFinal, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 138, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbTipoOcurrencia, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 166, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbOrigenOcurrencia, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 167, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbEnviarAntecedentes, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);

            // Accidentes realcionadas al trabajo
            Utils.LoadDropDownList(cbFormaAccidente, "Value1", "ItemId", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 113, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbParteCuerpo, "Value1", "ItemId", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 114, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbClasificacionAccLab, "Value1", "ItemId", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 116, null), DropDownListAction.Select);

            // Enfermedades realcionadas al trabajo           
            Utils.LoadDropDownList(cbClasificacionEnfLab, "Value1", "ItemId", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 117, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbFactorRiesgo, "Value1", "ItemId", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 118, null), DropDownListAction.Select);

            cbTipoOcurrencia.SelectedValueChanged += cbTipoOcurrencia_SelectedValueChanged;
            cbOrigenOcurrencia.SelectedValueChanged += cbOrigenOcurrencia_SelectedValueChanged;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (uvAddTotalDiagnostic.Validate(true, false).IsValid)
            {
                if (_tmpDiagnosticList == null)
                    _tmpDiagnosticList = new List<DiagnosticRepositoryList>();

                if (_mode == "New")
                {                   
                    var findResult = _tmpDiagnosticList.Find(p => p.v_DiagnosticRepositoryId == _diagnosticRepositoryId);

                    _diagnosticRepository = new DiagnosticRepositoryList();

                    if (findResult == null)   // agregar con normalidad  a la bolsa de DX 
                    {
                        _diagnosticRepository.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                        _diagnosticRepository.v_ServiceId = _serviceId;
                        _diagnosticRepository.v_DiseasesId = _objDiseasesList.v_DiseasesId;
                        _diagnosticRepository.i_FinalQualificationId = int.Parse(cbCalificacionFinal.SelectedValue.ToString());              
                        _diagnosticRepository.i_IsSentToAntecedent = int.Parse(cbEnviarAntecedentes.SelectedValue.ToString());
                        _diagnosticRepository.i_DiagnosticTypeId = int.Parse(cbTipoOcurrencia.SelectedValue.ToString());
                        _diagnosticRepository.i_DiagnosticSourceId = int.Parse(cbOrigenOcurrencia.SelectedValue.ToString());
                        // Accidente laboral                   
                        _diagnosticRepository.i_ShapeAccidentId = int.Parse(((KeyValueDTO)cbFormaAccidente.SelectedValue).Id);
                        _diagnosticRepository.i_BodyPartId = int.Parse(((KeyValueDTO)cbParteCuerpo.SelectedValue).Id);
                        _diagnosticRepository.i_ClassificationOfWorkAccidentId = int.Parse(((KeyValueDTO)cbClasificacionAccLab.SelectedValue).Id);
                        // Enfermedad laboral
                        _diagnosticRepository.i_RiskFactorId = int.Parse(((KeyValueDTO)cbFactorRiesgo.SelectedValue).Id);
                        _diagnosticRepository.i_ClassificationOfWorkdiseaseId = int.Parse(((KeyValueDTO)cbClasificacionEnfLab.SelectedValue).Id);

                        _diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                        _diagnosticRepository.i_RecordType = (int)RecordType.Temporal;

                        _diagnosticRepository.v_FinalQualificationName = cbCalificacionFinal.Text;
                        _diagnosticRepository.v_DiseasesName = lblDiagnostico.Text;
                        _diagnosticRepository.v_RiskFactorName = cbFactorRiesgo.Text;
                        _diagnosticRepository.v_ClassificationOfWorkdiseaseName = cbClasificacionEnfLab.Text;
                        _diagnosticRepository.v_IsSentToAntecedentName = _diagnosticRepository.i_IsSentToAntecedent == (int)SiNo.SI ? "SI" : "NO";
                        _diagnosticRepository.v_FinalQualificationName = cbCalificacionFinal.Text;                      
                        _diagnosticRepository.v_DiagnosticTypeName = cbTipoOcurrencia.Text;
                        _diagnosticRepository.v_DiagnosticSourceName = cbOrigenOcurrencia.Text;

                        _tmpDiagnosticList.Add(_diagnosticRepository);                                            
                        
                    }                   
                }
                else if (_mode == "Edit")
                {
                    var findResult = _tmpDiagnosticList.Find(p => p.v_DiagnosticRepositoryId == _diagnosticRepositoryId);
                                     
                    findResult.i_FinalQualificationId = int.Parse(cbCalificacionFinal.SelectedValue.ToString());              
                    findResult.i_IsSentToAntecedent = int.Parse(cbEnviarAntecedentes.SelectedValue.ToString());
                    findResult.i_DiagnosticTypeId = int.Parse(cbTipoOcurrencia.SelectedValue.ToString());
                    findResult.i_DiagnosticSourceId = int.Parse(cbOrigenOcurrencia.SelectedValue.ToString());
                    // Accidente laboral                   
                    findResult.i_ShapeAccidentId = int.Parse(((KeyValueDTO)cbFormaAccidente.SelectedValue).Id);
                    findResult.i_BodyPartId = int.Parse(((KeyValueDTO)cbParteCuerpo.SelectedValue).Id);
                    findResult.i_ClassificationOfWorkAccidentId = int.Parse(((KeyValueDTO)cbClasificacionAccLab.SelectedValue).Id);
                    // Enfermedad laboral
                    findResult.i_RiskFactorId = int.Parse(((KeyValueDTO)cbFactorRiesgo.SelectedValue).Id);
                    findResult.i_ClassificationOfWorkdiseaseId = int.Parse(((KeyValueDTO)cbClasificacionEnfLab.SelectedValue).Id);

                    findResult.i_RecordStatus = (int)RecordStatus.Modificado;                    
                    findResult.v_FinalQualificationName = cbCalificacionFinal.Text;
                    findResult.v_DiseasesName = lblDiagnostico.Text;
                    findResult.v_RiskFactorName = cbFactorRiesgo.Text;
                    findResult.v_ClassificationOfWorkdiseaseName = cbClasificacionEnfLab.Text;
                    findResult.v_IsSentToAntecedentName = findResult.i_IsSentToAntecedent == (int)SiNo.SI ? "SI" : "NO";
                    findResult.v_FinalQualificationName = cbCalificacionFinal.Text;
                    findResult.v_DiagnosticTypeName = cbTipoOcurrencia.Text;
                    findResult.v_DiagnosticSourceName = cbOrigenOcurrencia.Text;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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
            }
        }

        private void SelectChangeEvent(int tipo, int origen)
        {        
            if (tipo == -1 || origen == -1)
            {
                gbAccidenteLaboral.Visible = false;
                gbEnfermedadLaboral.Visible = false;
                this.Height = 153;

                // Remover validacion Accidente Laboral
                uvAddTotalDiagnostic.GetValidationSettings(cbFormaAccidente).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbFormaAccidente).IsRequired = false;
                uvAddTotalDiagnostic.GetValidationSettings(cbParteCuerpo).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbParteCuerpo).IsRequired = false;
                uvAddTotalDiagnostic.GetValidationSettings(cbClasificacionAccLab).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbClasificacionAccLab).IsRequired = false;

                // Remover validacion Enfermedad Laboral
                uvAddTotalDiagnostic.GetValidationSettings(cbFactorRiesgo).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbFactorRiesgo).IsRequired = false;
                uvAddTotalDiagnostic.GetValidationSettings(cbClasificacionEnfLab).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbClasificacionEnfLab).IsRequired = false;
                return;
            }

            if (tipo == (int)TipoOcurrencia.Accidente && origen == (int)OrigenOcurrencia.Laboral)
            {
                gbAccidenteLaboral.Visible = true;
                gbEnfermedadLaboral.Visible = false;
                this.Height = 248;

                // Add validacion
                uvAddTotalDiagnostic.GetValidationSettings(cbFormaAccidente).Condition = new OperatorCondition(ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbFormaAccidente).IsRequired = true;
                uvAddTotalDiagnostic.GetValidationSettings(cbParteCuerpo).Condition = new OperatorCondition(ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbParteCuerpo).IsRequired = true;
                uvAddTotalDiagnostic.GetValidationSettings(cbClasificacionAccLab).Condition = new OperatorCondition(ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbParteCuerpo).IsRequired = true;

                // Remover validacion
                uvAddTotalDiagnostic.GetValidationSettings(cbFactorRiesgo).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbFactorRiesgo).IsRequired = false;
                uvAddTotalDiagnostic.GetValidationSettings(cbClasificacionEnfLab).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbClasificacionEnfLab).IsRequired = false;

            }
            else if (tipo == (int)TipoOcurrencia.Enfermedad && origen == (int)OrigenOcurrencia.Laboral)
            {
                gbEnfermedadLaboral.Visible = true;
                gbAccidenteLaboral.Visible = false;
                this.Height = 207;

                // Add validacion
                uvAddTotalDiagnostic.GetValidationSettings(cbFactorRiesgo).Condition = new OperatorCondition(ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbFactorRiesgo).IsRequired = true;
                uvAddTotalDiagnostic.GetValidationSettings(cbClasificacionEnfLab).Condition = new OperatorCondition(ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbClasificacionEnfLab).IsRequired = true;

                // Remover validacion
                uvAddTotalDiagnostic.GetValidationSettings(cbFormaAccidente).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbFormaAccidente).IsRequired = false;
                uvAddTotalDiagnostic.GetValidationSettings(cbParteCuerpo).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbParteCuerpo).IsRequired = false;
                uvAddTotalDiagnostic.GetValidationSettings(cbClasificacionAccLab).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbClasificacionAccLab).IsRequired = false;

            }
            else
            {
                gbAccidenteLaboral.Visible = false;
                gbEnfermedadLaboral.Visible = false;
                this.Height = 153;

                // Remover validacion Accidente Laboral
                uvAddTotalDiagnostic.GetValidationSettings(cbFormaAccidente).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbFormaAccidente).IsRequired = false;
                uvAddTotalDiagnostic.GetValidationSettings(cbParteCuerpo).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbParteCuerpo).IsRequired = false;
                uvAddTotalDiagnostic.GetValidationSettings(cbClasificacionAccLab).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbClasificacionAccLab).IsRequired = false;

                // Remover validacion Enfermedad Laboral
                uvAddTotalDiagnostic.GetValidationSettings(cbFactorRiesgo).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbFactorRiesgo).IsRequired = false;
                uvAddTotalDiagnostic.GetValidationSettings(cbClasificacionEnfLab).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvAddTotalDiagnostic.GetValidationSettings(cbClasificacionEnfLab).IsRequired = false;

            }         

        }

        private void cbTipoOcurrencia_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectChangeEvent(int.Parse(cbTipoOcurrencia.SelectedValue.ToString()), int.Parse(cbOrigenOcurrencia.SelectedValue.ToString()));
        }

        private void cbOrigenOcurrencia_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectChangeEvent(int.Parse(cbTipoOcurrencia.SelectedValue.ToString()), int.Parse(cbOrigenOcurrencia.SelectedValue.ToString()));
        }


    }
}
