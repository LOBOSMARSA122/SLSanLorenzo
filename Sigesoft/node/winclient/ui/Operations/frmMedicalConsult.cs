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
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using System.Text.RegularExpressions;
using Infragistics.Win.UltraWinMaskedEdit;
using System.Reflection;

namespace Sigesoft.Node.WinClient.UI.Operations
{
    public partial class frmMedicalConsult : Form
    {
        public class RunWorkerAsyncPackage
        {
            public Infragistics.Win.UltraWinTabControl.UltraTabPageControl SelectedTab { get; set; }
            public List<DiagnosticRepositoryList> ExamDiagnosticComponentList { get; set; }
            public servicecomponentDto ServiceComponent { get; set; }

        }     

        #region Declarations
        public List<DiagnosticRepositoryList> _interconsultations = null;
        public List<DiagnosticRepositoryList> _medicalBreaks = null;
        private List<BE.ComponentList> _tmpServiceComponentsForBuildMenuList = null;
        private ServiceBL _serviceBL = new ServiceBL();
        private string _serviceId = null;
        private string _componentId;
        private string _serviceComponentId;
        private string _componentIdByDefault;
        private int? _rowIndex;
        private int? _rowIndexConclucionesDX;
        private int? _rowIndexDescansoMedico;
        public string _diagnosticId = null;
        private Keys _currentDownKey = Keys.None;    
        private List<ComponentFieldsList> groupedFields = new List<ComponentFieldsList>();

        /// <summary>
        /// lista temporal (solo diagnosticos vinculados a examenes / componentes)
        /// </summary>
        private List<DiagnosticRepositoryList> _tmpExamDiagnosticComponentList = null;
        /// <summary>
        /// Almacena Temporalmente la lista de los diagnósticos totales
        /// </summary>
        private List<DiagnosticRepositoryList> _tmpDiagnosticList = null;
        private List<DiagnosticRepositoryList> _tmpDiagnosticPlanTrabajoList = null;

        /// <summary>
        /// almacena temporalmente lista de diagnosticos [Definitivos / Presuntivos]
        /// en la pestaña Conclusiones y tratamiento
        private List<DiagnosticRepositoryList> _tmpTotalConclusionesDxByServiceIdList = null;

        /// <summary>
        /// almacena temporalmente lista de diagnosticos [Definitivos / Presuntivos] para generar descanso Médico 
        /// en la pestaña Conclusiones y tratamiento
        /// </summary>
        private List<DiagnosticRepositoryList> _tmpTotalConclusionesDxForMedicalBreakList = null;

        private DiagnosticRepositoryList _tmpTotalDiagnostic = null;

        /// <summary>
        /// Lista temporal de restricciones usadas en la pestaña de Analisis de Diagnósticos
        /// </summary>
        private List<RestrictionList> _tmpRestrictionList = null;
        /// <summary>
        /// Lista temporal de restricciones usadas en la pestaña de Conclusiones
        /// </summary>
        private List<RestrictionList> _tmpRestrictionListConclusiones = null;
        private List<RecomendationList> _tmpRecomendationList = null;

        /// <summary>
        /// Lista temporal de recomendaciones usada en la pestaña de Conclusiones
        /// </summary>
        private List<RecomendationList> _tmpRecomendationConclusionesList = null;

        private List<MedicationList> _tmpMedicationList = null;
        private List<ProcedureByServiceList> _tmpProcedureList = null;

        private List<ServiceComponentFieldsList> _serviceComponentFieldsList = null;
        private List<ServiceComponentFieldValuesList> _serviceComponentFieldValuesList = null;

        private ServiceComponentList _serviceComponentsInfo = null;

        private bool flagValueChange = false;

        private string _componentIdConclusionesDX = null;

        private Dictionary<string, UltraValidator> _dicUltraValidators = null;
        private bool _isChangeValue = false;
        private string _oldValue;
        private int _age;
        private Gender _sexType;
        private string _personId;
        private string _serviceIdByWiewServiceHistory;
        private string _action;
        private string _pharmacyWarehouseId;
        private int _masterServiceId;
        private List<KeyValueDTO> _formActions = null;
        private string _examName;
        bool isDisabledButtonsExamDx = false;
        byte[] _personImage;
        string _personName;

        private List<ServiceComponentFieldValuesList> _tmpListValuesOdontograma = null;
        frmWaiting frmWaiting = new frmWaiting("Grabando...");

        private bool _cancelEventselectedIndexChange;
        private string _productId;
        public List<AuxiliaryExamList> _auxiliaryExams = null;

        #region Busqueda de Diagnósticos

        MedicalExamFieldValuesBL _objMedicalExamFieldValuesBL = new MedicalExamFieldValuesBL();
        public DiseasesList _objDiseasesList = null;
        string strFilterExpression;
        public string strEnfermedad;

        #endregion

        #endregion

        public frmMedicalConsult(string serviceId, string componentIdByDefault, string action)
        {
            InitializeComponent();
            _serviceId = serviceId;
            _componentIdByDefault = componentIdByDefault;
            _action = action;
       
        }   

        private void frmEso_Load(object sender, EventArgs e)
        {
          
            LoadComboBox();
            InitializeData();
            BuildMenu();
         
            // PESTAÑA ANAMNESIS X DEFECTO
            tcSubMain.SelectedIndex = 0;
            // Setear por default un examen componente desde consusltorio

            #region Set Tab x default

            if (_componentIdByDefault != null)
            {
                var findComponent = _tmpServiceComponentsForBuildMenuList.Find(p => p.v_ComponentId == _componentIdByDefault);

                if (findComponent != null)
                {
                    tcExamList.Tabs[_componentIdByDefault].Selected = true;
                }
                else
                {
                    tcExamList.Tabs[0].Selected = true;
                }
            }
            else
            {
                tcExamList.Tabs[0].Selected = true;
            }

            #endregion
         
            // Información para grillas
            //GetTotalDiagnosticsForGridView();
                  
            //ConclusionesyTratamiento_LoadAllGrid();
          

            if (_action == "View")
            {
                cbNuevoControl.Enabled = false;
                cboEvolucion.Enabled = false;
                gbAntecedentes.Enabled = false;
                gbServiciosAnteriores.Enabled = false;

                #region Anamnesis

                gbSintomasySignos.Enabled = false;
                //gbFuncionesBiologicas.Enabled = false;
                btnGuardarConsultaMedica.Enabled = false;

                #endregion

                #region Examenes

              

                #endregion

                #region Analisis de diagnosticos

                gbTotalDiagnostico.Enabled = false;
            

                #endregion

                #region Conclusiones

            
                gbRestricciones_Conclusiones.Enabled = false;
           

                #endregion

                #region Tratamiento

                gbTratamiento.Enabled = false;

                #endregion

            }

            ///////////////

            //#region Simular sesion
            //ClientSession objClientSession = new ClientSession();
            //objClientSession.i_SystemUserId = 1;
            //objClientSession.v_UserName = "sa";
            //objClientSession.i_CurrentExecutionNodeId = 2;
            //objClientSession.v_CurrentExecutionNodeName = "SALUS";
            ////_ClientSession.i_CurrentOrganizationId = 57;
            //objClientSession.v_PersonId = "N000-P0000000001";

            //// Pasar el objeto de sesión al gestor de objetos globales
            //Globals.ClientSession = objClientSession;
            //#endregion      
            
        }    

        private void BuildMenu()
        {

            try
            {
                //this.tcExamList.Tabs.Clear();
                // construir menu dinamico    

                // Lista de Componentes / Campos / Values
                OperationResult objOperationResult = new OperationResult();
                _tmpServiceComponentsForBuildMenuList = new ServiceBL().GetServiceComponentsForBuildMenu(ref objOperationResult, _serviceId);
                
                #region Declarations Controls

                Label lbl = null;
                Infragistics.Win.UltraWinTabControl.UltraTab tab = null;
                UltraNumericEditor une = null;
                TextBox txt = null;
                ComboBox cb = null;
                GroupBox gb = null;
                Control ctl = null;
                UltraValidator uv = null;
                #endregion

                int i = 1;

                foreach (BE.ComponentList com in _tmpServiceComponentsForBuildMenuList)
                {

                    #region crear y configurar Tab Component y FlowLayoutPanel (padre) por cada tab que se agregue dinamicamente

                    // Crear TAB del componente
                    tab = new Infragistics.Win.UltraWinTabControl.UltraTab();
                    tab.Text = com.v_Name;
                    tab.Key = com.v_ComponentId;
                    tab.Tag = com.v_ServiceComponentId;
                    tab.ToolTipText = com.v_Name + " / " + com.v_ServiceComponentId;
                    tcExamList.Tabs.Add(tab);

                    // Incrustar el flpParent por cada tab
                    // Crear Flowlayout del Componente
                    TableLayoutPanel tblpParent;
                    tblpParent = new TableLayoutPanel();
                    tblpParent.Name = "tblpParent";
                    tblpParent.ColumnCount = 1;
                    tblpParent.RowCount = groupedFields.Count;
                    tab.TabPage.Controls.Add(tblpParent);

                    // Crear validadores para cada componente examen [triaje,Audio,etc]
                    uv = CreateUltraValidatorByComponentId(com.v_ComponentId);

                    #endregion                         

                    #region Formar grupos para los campos (controles)
                  
                    List<ComponentFieldsList> groups = com.Fields.GroupBy(e => new { e.v_Group }).Select(g => g.First()).OrderBy(o => o.v_Group).ToList();

                    #endregion

                    foreach (var g in groups)
                    {
                        #region Crear control GroupBox para agrupar los campos (controles)

                        // Crear y configurar GroupBox por cada grupo
                        gb = new GroupBox();
                        gb.Text = g.v_Group;
                        gb.Name = "gb_" + g.v_Group;
                        gb.BackColor = Color.LightCyan;
                        gb.AutoSize = true;
                        gb.Dock = DockStyle.Top;

                        i++;

                        // Definir el table layout para los controles del grupo
                        TableLayoutPanel tblpGroup;
                        tblpGroup = new TableLayoutPanel();
                        tblpGroup.Name = "tblpGroup_" + g.v_Group;
                        tblpGroup.ColumnCount = g.i_Column * Constants.COLUMNAS_POR_CONTROL;
                        tblpGroup.RowCount = RedondeoMayor(com.Fields.Count, g.i_Column);
                        tblpGroup.Dock = DockStyle.Fill;
                        tblpGroup.AutoSize = true;
                        //tblpGroup.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetPartial;                        

                        #endregion

                        // Recorrer todos los controles del grupo
                        int nroControlNET = 1;
                        int fila, columna;

                        #region Buscar campos pertenecientes a un grupo, crearlos y configurarlos para Agregarlos dentro de de cada Tab / GroupBox

                        // Buscar campos para agruparlos 
                        groupedFields = com.Fields.FindAll(p => p.v_Group == g.v_Group);

                        foreach (ComponentFieldsList f in groupedFields)
                        {
                            // PONER EL LABEL
                            lbl = new Label();
                            lbl.Text = f.v_TextLabel;
                            lbl.Width = f.i_LabelWidth;
                            lbl.TextAlign = ContentAlignment.BottomRight;
                            lbl.AutoSize = false;
                            lbl.Font = new Font(lbl.Font.FontFamily.Name, 7.25F); 
                            //lbl.TextAlign = ContentAlignment.TopLeft;
                            fila = RedondeoMayor(nroControlNET, g.i_Column * Constants.COLUMNAS_POR_CONTROL);
                            columna = nroControlNET - (fila - 1) * (g.i_Column * Constants.COLUMNAS_POR_CONTROL);
                            tblpGroup.Controls.Add(lbl, columna - 1, fila - 1);
                            nroControlNET++;                         

                            switch ((ControlType)f.i_ControlId)
                            {
                                #region Creacion del control

                                case ControlType.CadenaTextual:
                                    txt = new TextBox();
                                    txt.Width = f.i_ControlWidth;
                                    txt.Height = f.i_HeightControl;
                                    txt.MaxLength = f.i_MaxLenght;
                                    txt.Name = f.v_ComponentFieldId;                                  

                                    if (f.i_IsCalculate == (int)SiNo.SI)
                                    {
                                        txt.Enabled = false;
                                    }
                                    else
                                    {
                                        txt.Leave += new EventHandler(txt_Leave);
                                    }

                                    if (f.i_IsRequired == (int)SiNo.SI)
                                        SetControlValidate(f.i_ControlId, txt, null, null, uv);

                                    txt.Enter += new EventHandler(Capture_Value);

                                    if (_action == "View")
                                    {
                                        txt.ReadOnly = true;
                                    }                                 

                                    ctl = txt;
                                    break;
                                case ControlType.CadenaMultilinea:
                                    txt = new TextBox()
                                    {
                                        Width = f.i_ControlWidth,
                                        Height = f.i_HeightControl,
                                        Multiline = true,
                                        MaxLength = f.i_MaxLenght,
                                        ScrollBars = ScrollBars.Vertical,
                                        Name = f.v_ComponentFieldId,                                      
                                    };

                                    txt.Enter += new EventHandler(Capture_Value);
                                    txt.Leave += new EventHandler(txt_Leave);

                                    if (f.i_IsRequired == (int)SiNo.SI)
                                        SetControlValidate(f.i_ControlId, txt, null, null, uv);

                                    if (_action == "View")
                                    {
                                        txt.ReadOnly = true;
                                    }                                 

                                    ctl = txt;
                                    break;
                                case ControlType.NumeroEntero:
                                    une = new UltraNumericEditor()
                                    {
                                        Width = f.i_ControlWidth,
                                        Height = f.i_HeightControl,
                                        NumericType = NumericType.Integer,
                                        PromptChar = ' ',
                                        Name = f.v_ComponentFieldId,
                                        MaskDisplayMode = MaskMode.Raw
                                        
                                    };

                                    // Asociar el control a un evento
                                    une.Enter += new EventHandler(Capture_Value);

                                    if (f.i_IsCalculate == (int)SiNo.SI)
                                    {
                                        une.ReadOnly = true;
                                        une.ValueChanged += new EventHandler(txt_ValueChanged);
                                    }
                                    else
                                    {
                                        une.Leave += new EventHandler(txt_Leave);
                                    }

                                    if (f.i_IsRequired == (int)SiNo.SI)
                                    {
                                        // Establecer condición por rangos
                                        SetControlValidate(f.i_ControlId, une, f.r_ValidateValue1, f.r_ValidateValue2, uv);
                                    }

                                    if (_action == "View")
                                    {
                                        une.ReadOnly = true;
                                    }                                

                                    ctl = une;
                                    break;
                                case ControlType.NumeroDecimal:
                                    une = new UltraNumericEditor()
                                    {
                                        Width = f.i_ControlWidth,
                                        Height = f.i_HeightControl,
                                        PromptChar = ' ',
                                        Name = f.v_ComponentFieldId,
                                        NumericType = NumericType.Double,
                                        MaskDisplayMode = MaskMode.Raw
                                        
                                    };

                                    // Asociar el control a un evento
                                    une.Enter += new EventHandler(Capture_Value);

                                    if (f.i_IsCalculate == (int)SiNo.SI)
                                    {
                                        une.ValueChanged += new EventHandler(txt_ValueChanged);
                                        une.ReadOnly = true;                                                                     
                                    }
                                    else
                                    {
                                        une.Leave += new EventHandler(txt_Leave);
                                    }

                                    if (f.i_IsRequired == (int)SiNo.SI)
                                    {
                                        // Establecer condición por rangos                                                              
                                        SetControlValidate(f.i_ControlId, une, f.r_ValidateValue1, f.r_ValidateValue2, uv);
                                    }

                                    if (_action == "View")
                                    {
                                        une.ReadOnly = true;
                                    }

                                    ctl = une;
                                    break;
                                case ControlType.SiNoCheck:
                                    ctl = new CheckBox()
                                    {
                                        Width = f.i_ControlWidth,
                                        Height = f.i_HeightControl,
                                        Text = "Si/No",
                                        Name = f.v_ComponentFieldId,                                      
                                    };

                                    ctl.Enter += new EventHandler(Capture_Value);
                                    ctl.Leave += new EventHandler(txt_Leave);

                                    if (_action == "View")
                                    {
                                        ctl.Enabled = false;
                                    }

                                    break;
                                case ControlType.SiNoRadioButton:
                                    ctl = new RadioButton()
                                    {
                                        Width = f.i_ControlWidth,
                                        Height = f.i_HeightControl,
                                        Text = "Si/No",
                                        Name = f.v_ComponentFieldId
                                    };

                                    ctl.Enter += new EventHandler(Capture_Value);
                                    ctl.Leave += new EventHandler(txt_Leave);

                                    if (_action == "View")
                                    {
                                        ctl.Enabled = false;
                                    }

                                    break;
                                case ControlType.SiNoCombo:
                                    ctl = new ComboBox()
                                    {
                                        Width = f.i_ControlWidth,
                                        Height = f.i_HeightControl,
                                        DropDownStyle = ComboBoxStyle.DropDownList,
                                        Name = f.v_ComponentFieldId
                                    };

                                    Utils.LoadDropDownList((ComboBox)ctl, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, f.i_GroupId, null), DropDownListAction.Select);                                

                                    if (f.i_IsRequired == (int)SiNo.SI)
                                    {
                                        SetControlValidate(f.i_ControlId, ctl, null, null, uv);
                                    }

                                    ctl.Enter += new EventHandler(Capture_Value);
                                    ctl.Leave += new EventHandler(txt_Leave);

                                    if (_action == "View")
                                    {
                                        ctl.Enabled = false;
                                    }

                                    break;
                                case ControlType.UcFileUpload:
                                    var ucFileUpload = new Sigesoft.Node.WinClient.UI.UserControls.ucFileUpload();
                                    ucFileUpload.PersonId = _personId;
                                    ucFileUpload.ServiceComponentId = com.v_ServiceComponentId;
                                    ucFileUpload.Name = f.v_ComponentFieldId;

                                    //ctl = new Sigesoft.Node.WinClient.UI.UserControls.ucFileUpload();
                                    ctl = ucFileUpload;
                                    break;
                                case ControlType.UcOdontograma:
                                    var ucOdontograma = new Sigesoft.Node.WinClient.UI.UserControls.ucOdontograma();
                                    ucOdontograma.Name = f.v_ComponentFieldId;
                                    ctl = ucOdontograma;                                  
                                    break;
                                case ControlType.UcAudiometria:
                                    var ucAudiometria = new Sigesoft.Node.WinClient.UI.UserControls.ucAudiometria();
                                    ucAudiometria.Name = f.v_ComponentFieldId;
                                    ctl = ucAudiometria;
                                    break;
                                case ControlType.Lista:
                                    cb = new ComboBox()
                                    {
                                        Width = f.i_ControlWidth,
                                        Height = f.i_HeightControl,
                                        DropDownStyle = ComboBoxStyle.DropDownList,
                                        Name = f.v_ComponentFieldId
                                    };
                                   
                                    //Utils.LoadDropDownList((ComboBox)ctl, "Value1", "Id", BLL.Utils.GetDataHierarchyForComboAndItemId(ref objOperationResult, f.i_GroupId, f.i_ItemId, null), DropDownListAction.Select);
                                    var data = BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, f.i_GroupId, null);
                                  
                                    Utils.LoadDropDownList(cb, "Value1", "Id", data, DropDownListAction.Select);
                                   
                                    if (f.i_IsRequired == (int)SiNo.SI)
                                    {
                                        SetControlValidate(f.i_ControlId, cb, null, null, uv);
                                    }

                                    // Setear levantamiento de popup para el ingreso de los hallazgos solo cuando se seleccione un valor alterado
                                    if (f.v_ComponentId == Constants.EXAMEN_FISICO_ID)
                                    {
                                        cb.SelectedIndexChanged += new EventHandler(cb_SelectedIndexChanged);
                                    }
                                  
                                    cb.Enter += new EventHandler(Capture_Value);
                                    cb.Leave += new EventHandler(txt_Leave);

                                    if (_action == "View")
                                    {
                                        cb.Enabled = false;
                                    }

                                    ctl = cb;
                                    break;
                                default:
                                    break;

                                #endregion

                            }
                            
                            //ctl.CreateControl();
                           
                            // PONER EL CONTROL ESPECIFICO
                            ctl.Tag = new KeyTagControl
                            {
                                i_ControlId = f.i_ControlId,
                                v_ComponentFieldsId = f.v_ComponentFieldId,
                                i_IsSourceFieldToCalculate = f.i_IsSourceFieldToCalculate,
                                v_Formula = f.v_Formula,
                                v_TargetFieldOfCalculateId = f.v_TargetFieldOfCalculateId,
                                v_SourceFieldToCalculateJoin = f.v_SourceFieldToCalculateJoin,
                                v_FormulaChild = f.v_FormulaChild,
                                Formula = f.Formula,
                                TargetFieldOfCalculateId = f.TargetFieldOfCalculateId,
                                v_TextLabel = f.v_TextLabel,
                                v_ComponentName = com.v_Name
                            };
                           

                            // Agregar el control al contenedor
                            fila = RedondeoMayor(nroControlNET, g.i_Column * Constants.COLUMNAS_POR_CONTROL);
                            columna = nroControlNET - (fila - 1) * (g.i_Column * Constants.COLUMNAS_POR_CONTROL);
                            tblpGroup.Controls.Add(ctl, columna - 1, fila - 1);
                            nroControlNET++;                          

                            // label de unid medida.
                            Label lbl1 = new Label();
                            lbl1.AutoSize = false;
                            lbl1.Width = 50;
                            lbl1.Text = f.v_MeasurementUnitName;
                            lbl1.Font = new Font(lbl1.Font, FontStyle.Bold | FontStyle.Italic);
                            lbl1.TextAlign = ContentAlignment.BottomLeft;
                            fila = RedondeoMayor(nroControlNET, g.i_Column * Constants.COLUMNAS_POR_CONTROL);
                            columna = nroControlNET - (fila - 1) * (g.i_Column * Constants.COLUMNAS_POR_CONTROL);
                            tblpGroup.Controls.Add(lbl1, columna - 1, fila - 1);
                            nroControlNET++;

                        }

                        #endregion

                        gb.Controls.Add(tblpGroup);                      
                        tblpParent.Controls.Add(gb, 1, i);                      
                    }

                    tblpParent.AutoScroll = true;
                    tblpParent.Dock = DockStyle.Fill;
                   
                }

                // Flag para disparar el evento del selectedIndexChange luego de setear los valores x default
                _cancelEventselectedIndexChange = true;
                SetDefaultValueAfterBuildMenu();
                _cancelEventselectedIndexChange = false;
              
            }
            catch (Exception ex)
            {               
                MessageBox.Show(ex.Message);
            }

           
        }

        private void InitializeData()
        {
            // Cargar datos generales del paciente
            OperationResult objOperationResult = new OperationResult();
           
            ServiceList personData = _serviceBL.GetServicePersonData(ref objOperationResult, _serviceId);
            _personName = string.Format("{0} {1} {2}", personData.v_FirstName, personData.v_FirstLastName, personData.v_SecondLastName);
            gbDatosPaciente.Text = string.Format("Datos del Paciente: {0} Tipo de Servicio: {1} Servicio: {2}", _personName, personData.v_ServiceTypeName, personData.v_MasterServiceName);
            _personId = personData.v_PersonId;
            if (personData.b_PersonImage != null)
                pbPersonImage.Image = Common.Utils.byteArrayToImage(personData.b_PersonImage);
            _personImage = personData.b_PersonImage;
       
        
            //lblTipoServ.Text = personData.v_ServiceTypeName;
            //lblServicio.Text = personData.v_MasterServiceName;
            _masterServiceId = personData.i_MasterServiceId.Value;
         
            //cbNuevoControl.SelectedValue = personData.i_IsNewControl;
            lblFecInicio.Text = personData.d_ServiceDate == null ? string.Empty : personData.d_ServiceDate.Value.ToShortDateString();

            // calcular edad
            _age = DateTime.Today.AddTicks(-personData.d_BirthDate.Value.Ticks).Year - 1;
            lblEdad.Text = _age.ToString();
            lblGenero.Text = personData.v_GenderName;

            //if (personData.i_ServiceTypeId == (int)ServiceType.Empresarial)        
            //    cbNuevoControl.Enabled = false;
                    
            // cargar datos INICIALES de ANAMNESIS            

            txtSintomaPrincipal.Text = personData.v_MainSymptom;
            txtResultadosExaAux.Text = personData.v_ExaAuxResult;

            txtValorTiempoEnfermedad.Text = personData.i_TimeOfDisease == null ? string.Empty : personData.i_TimeOfDisease.ToString();
            cbCalendario.SelectedValue = personData.i_TimeOfDiseaseTypeId == null ? "1" : personData.i_TimeOfDiseaseTypeId.ToString();
            txtRelato.Text = personData.v_Story;

            if (personData.d_Fur != null)
            {
                dtpFur.Checked = true;
                dtpFur.Value = personData.d_Fur.Value.Date;
            }
            txtRegimenCatamenial.Text = personData.v_CatemenialRegime;
            cbMac.SelectedValue = personData.i_MacId == null ? "1" : personData.i_MacId.ToString();
            cbNuevoControl.SelectedValue = personData.i_IsNewControl == null ? "-1" : personData.i_IsNewControl.ToString();
            cboEvolucion.SelectedValue = personData.i_Evolucion == null ? "-1" : personData.i_Evolucion.ToString();


            if (personData.d_PAP != null)
            {
                dtpPAP.Value = personData.d_PAP.Value;
                dtpPAP.Checked = true;
            }


            if (personData.d_Mamografia != null)
            {
                dtpMamografia.Value = personData.d_Mamografia.Value;
                dtpMamografia.Checked = true;
            }

            cboInicioEnf.SelectedValue = personData.i_InicioEnf == null ? "-1" : personData.i_InicioEnf.ToString();
            cboCursoEnf.SelectedValue = personData.i_CursoEnf == null ? "-1" : personData.i_CursoEnf.ToString();
            txtGestapara.Text = string.IsNullOrEmpty(personData.v_Gestapara) ? "G ( )  P ( ) ( ) ( ) ( ) " : personData.v_Gestapara;
            txtMenarquia.Text = personData.v_Menarquia;
            txtCiruGine.Text = personData.v_CiruGine;

            //cbSueño.SelectedValue = personData.i_DreamId == null ? "1" : personData.i_DreamId.ToString();
            //cbOrina.SelectedValue = personData.i_UrineId == null ? "1" : personData.i_UrineId.ToString();
            //cbDeposiciones.SelectedValue = personData.i_DepositionId == null ? "1" : personData.i_DepositionId.ToString();
            //cbApetito.SelectedValue = personData.i_AppetiteId == null ? "1" : personData.i_AppetiteId.ToString();
            //cbSed.SelectedValue = personData.i_ThirstId == null ? "1" : personData.i_ThirstId.ToString();
            if (personData.d_Fur != null)
                dtpFur.Value = personData.d_Fur.Value;
            txtRegimenCatamenial.Text = personData.v_CatemenialRegime;

            _sexType = (Gender)personData.i_SexTypeId;

            switch (_sexType)
            {
                case Gender.MASCULINO:
                    gbAntGinecologicos.Enabled = false;
                    dtpFur.Enabled = false;
                    txtRegimenCatamenial.Enabled = false;
                    break;
                case Gender.FEMENINO:
                    gbAntGinecologicos.Enabled = true;
                    dtpFur.Enabled = true;
                    txtRegimenCatamenial.Enabled = true;
                    break;
                default:
                    break;
            }
     

            #region Plan Trabajo

            GetDiagnosticsForGridView();

            // Interconsulta
            if (personData.i_HazInterconsultationId == (int?)SiNo.SI)
            {
                rbSiInterconsulta.Checked = true;
                rbNoInterconsulta.Checked = false;            
            }
            else
            {
                rbNoInterconsulta.Checked = true;
                rbSiInterconsulta.Checked = false;              
            }

            // Restriccion
            if (personData.i_HasRestrictionId == (int?)SiNo.SI)
            {
                //rbNoRestricciones.CheckedChanged -= rbNoRestricciones_CheckedChanged;
                rbSiRestricciones.Checked = true;
                //rbNoRestricciones.CheckedChanged += rbNoRestricciones_CheckedChanged;
                //rbNoRestricciones.Checked = false;            
            }
            else
            {
                //rbNoRestricciones.Checked = true;
                //rbSiRestricciones.Checked = false;          
            }

            txtRecomendacionesGenerales.Text = personData.v_GeneralRecomendations;

            cbDestino.SelectedValue = personData.i_DestinationMedicationId == null ? "-1" : personData.i_DestinationMedicationId.ToString();
            cbMedioTransporte.SelectedValue = personData.i_TransportMedicationId == null ? "-1" : personData.i_TransportMedicationId.ToString();
                    
            chkDescansoMedico.Checked = personData.i_HasMedicalBreakId == null ? false : Convert.ToBoolean(personData.i_HasMedicalBreakId);

            if (personData.d_MedicalBreakStartDate != null)
                dtpFechaIniDescansoMedico.Value = personData.d_MedicalBreakStartDate.Value;
            if (personData.d_MedicalBreakEndDate != null)
                dtpFechaFinDescansoMedico.Value = personData.d_MedicalBreakEndDate.Value;

            GetDiagnosticsPlanTrabajoForGridView();

            if (personData.d_NextAppointment != null)
                dtpProxCitaRef.Value = personData.d_NextAppointment.Value;

            // Enviar a seguimiento
            if (personData.i_SendToTracking == (int?)SiNo.SI)
            {
                rbSiTracking.Checked = true;
                rbNoTracking.Checked = false;
            }
            else
            {
                rbNoTracking.Checked = true;
                rbSiTracking.Checked = false;
            }

            #endregion

            #region Antecedentes / Servicios

            // Cargar grilla
            GetAntecedentConsolidateForService(_personId);
            GetServicesConsolidateForService(_personId); 

            #endregion

            #region FormActions

            _formActions = Sigesoft.Node.WinClient.BLL.Utils.SetFormActionsInSession("frmEso",
                                                                                Globals.ClientSession.i_CurrentExecutionNodeId,
                                                                                 Globals.ClientSession.i_RoleId.Value,
                                                                                Globals.ClientSession.i_SystemUserId);
            // Setear privilegios / permisos

            var enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_ANADX_EDIT", _formActions);

            //btnAgregarTotalDiagnostico.Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_ANADX_ADDDX", _formActions);
            //btnRemoverTotalDiagnostico.Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_ANADX_REMOVEDX", _formActions);

          

            #endregion

            // Analizar el resultado de la operación
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadComboBox()
        {
            // Llenado de combos
           
            OperationResult objOperationResult = new OperationResult();

            #region Cabecera servicio

        
            Utils.LoadDropDownList(cbNuevoControl, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 143, null), DropDownListAction.Select);

            #endregion

            #region Anamnesis

            Utils.LoadDropDownList(cbCalendario, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 133, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbMac, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 134, null), DropDownListAction.Select);

            Utils.LoadDropDownList(cboInicioEnf, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 118, null), DropDownListAction.Select);

            Utils.LoadDropDownList(cboCursoEnf, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 119, null), DropDownListAction.Select);

            Utils.LoadDropDownList(cboEvolucion, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 120, null), DropDownListAction.Select);
          
           
            #endregion

            #region Examenes
            var serviceComponent =  BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 127, null);          
                  

            #endregion     

            #region Conclusiones y Tratamiento

            Utils.LoadDropDownList(cbDestino, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 140, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbMedioTransporte, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 141, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbProcedimientos, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);
                           
            #endregion

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void pbPersonImage_Click(object sender, EventArgs e)
        {
            if (_personImage != null)
            {
                var frm = new Popups.frmPreviewImagePerson(_personImage, _personName);
                frm.ShowDialog();
            }

        }

        #region Consulta Médica

        #region Anamnesis 

        private void txtValorTiempoEnfermedad_KeyPress(object sender, KeyPressEventArgs e)
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

        private void cbSueño_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbSueño.SelectedValue.ToString() == "-1")
            //    return;

            //RegisterAlteration("Sueño");
           

        }

        private void RegisterAlteration(string label)
        {
            //var value = int.Parse(cbSueño.SelectedValue.ToString());

            //if (value == (int)NormalAlterado.Alterado)
            //{
            //    txtAlteraciones.Focus();
            //    // txtAlteraciones.SelectedText = string.Empty;
            //    // txtAlteraciones.SelectionFont = new System.Drawing.Font(txtLog.SelectionFont, FontStyle.Regular);
            //    txtAlteraciones.SelectionColor = Color.DarkBlue;
            //    txtAlteraciones.AppendText(string.Format("{0}: \n\r", label));               
            //    txtAlteraciones.Select(txtAlteraciones.Text.Length, 0);

            //    //txtAlteraciones.ScrollToCaret();               
            //}
          
        }

        #endregion

        #region Examen

        private void btnGuardarConsultaMedica_Click(object sender, EventArgs e)
        {
            SaveMedicalConsult();
        }

        private bool SaveMedicalConsult()
        {
            bool isOk = false;

            if (uvAnamnesis.Validate(true, false).IsValid)
            {
                DialogResult Result = MessageBox.Show("¿Está seguro de grabar este registro?:", "CONFIRMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (Result == DialogResult.Yes)
                {
                    OperationResult objOperationResult = new OperationResult();

                    serviceDto serviceDTO = new serviceDto();

                    #region Fill serviceDTO

                    serviceDTO.v_ServiceId = _serviceId;
                    serviceDTO.v_MainSymptom = string.IsNullOrEmpty(txtSintomaPrincipal.Text) ? null : txtSintomaPrincipal.Text;
                    serviceDTO.i_TimeOfDisease = string.IsNullOrEmpty(txtValorTiempoEnfermedad.Text) ? (int?)null : int.Parse(txtValorTiempoEnfermedad.Text);
                    serviceDTO.i_TimeOfDiseaseTypeId = int.Parse(cbCalendario.SelectedValue.ToString());
                    serviceDTO.v_Story = txtRelato.Text;

                    serviceDTO.d_Fur = dtpFur.Checked ? dtpFur.Value : (DateTime?)null;
                    serviceDTO.v_CatemenialRegime = txtRegimenCatamenial.Text;
                    serviceDTO.i_MacId = int.Parse(cbMac.SelectedValue.ToString());
                    serviceDTO.d_PAP = dtpPAP.Checked ? dtpPAP.Value : (DateTime?)null;
                    serviceDTO.d_Mamografia = dtpMamografia.Checked ? dtpMamografia.Value : (DateTime?)null;
                 
                    serviceDTO.v_Gestapara = txtGestapara.Text;
                    serviceDTO.v_Menarquia = txtMenarquia.Text;
                    serviceDTO.v_CiruGine = txtCiruGine.Text;
                    serviceDTO.i_InicioEnf = int.Parse(cboInicioEnf.SelectedValue.ToString());
                    serviceDTO.i_CursoEnf = int.Parse(cboCursoEnf.SelectedValue.ToString());


                    // datos de cabecera del Servicio                    
                    serviceDTO.i_IsNewControl = int.Parse(cbNuevoControl.SelectedValue.ToString());
                    serviceDTO.i_Evolucion = int.Parse(cboEvolucion.SelectedValue.ToString());
                    #endregion

                    // Actualizar
                    _serviceBL.UpdateAnamnesis(ref objOperationResult, serviceDTO, Globals.ClientSession.GetAsList());

                    // Analizar el resultado de la operación
                    if (objOperationResult.Success != 1)
                    {
                        MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isOk = false;
                        return isOk;
                    }

                    SaveExamBySelectedTab(tcExamList.SelectedTab.TabPage);

                    isOk = true;                    
                }
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                isOk = false;
            }

            return isOk;
        }

        private void GetDiagnosticsForGridView()
        {
            OperationResult objOperationResult = new OperationResult();
            _tmpDiagnosticList = _serviceBL.GetDisgnosticsByServiceId(ref objOperationResult, _serviceId);

            grdDiagnosticos.DataSource = _tmpDiagnosticList;
            lblRecordCountDiagnosticos.Text = string.Format("Se encontraron {0} registros.", _tmpDiagnosticList.Count());

            // Analizar el resultado de la operación
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   

        }

        private int RedondeoMayor(int a, int b)
        {
            return (int)Math.Ceiling((double)a / (double)b);
        }    

        private void tcExamList_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            flagValueChange = false;
          
            _examName = e.Tab.Text;
            _componentId = e.Tab.Key;
            _serviceComponentId = e.Tab.Tag.ToString();         

            LoadDataBySelectedComponent(_componentId);
          
        }

        private void LoadDataBySelectedComponent(string pstrComponentId)
        {           
            if (tcExamList.SelectedTab == null)              
                return;         

            OperationResult objOperationResult = new OperationResult();

            if (_serviceComponentsInfo != null)
                _serviceComponentsInfo = null;

            // Mostrar data de serviceComponent
            _serviceComponentsInfo = _serviceBL.GetServiceComponentsInfo(ref objOperationResult, _serviceComponentId, _serviceId);

            if (_serviceComponentsInfo != null)
            {              

                #region Permisos de lectura / Escritura x componente de acuerdo al rol del usuario

                SetSecurityByComponent();
                                        
                #endregion                    

                if (_serviceComponentsInfo.ServiceComponentFields.Count != 0)
                {
                    // Flag para disparar el evento del selectedIndexChange luego de setear los valores x default
                    _cancelEventselectedIndexChange = true;
                    // Llenar valores            
                    SearchControlAndSetValue(tcExamList.SelectedTab.TabPage);
                    _cancelEventselectedIndexChange = false;
                }
                else
                {
                    // Setear valores x defecto configurados en BD            
                    SetDefaultValueBySelectedTab();                  
                }
              
              
            }
                      
            var diagnosticList = _serviceBL.GetServiceComponentDisgnosticsForGridView(ref objOperationResult,
                                                                                    _serviceId,
                                                                                    pstrComponentId);

            // Limpiar variable que contiene los Dx sugeridos / manuales
            if (_tmpExamDiagnosticComponentList != null)         
                _tmpExamDiagnosticComponentList = null;

            if (diagnosticList != null && diagnosticList.Count != 0)
            {
                // Cargar la grilla de DX sugeridos / manuales
             

                // Cargar mi lista temporal con data k viene de BD
                _tmpExamDiagnosticComponentList = diagnosticList;

                // Analizar el resultado de la operación
                if (objOperationResult.Success != 1)
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Limpiar la grilla de DX con una entidad vacia
              
                     
            }
           
        }

        private void ProcessControlBySelectedTab(Infragistics.Win.UltraWinTabControl.UltraTabPageControl selectedTab)
        {         
            if (_serviceComponentFieldsList == null)
                _serviceComponentFieldsList = new List<ServiceComponentFieldsList>();

            KeyTagControl keyTagControl = null;

            string value1 = null;
           
            ServiceComponentFieldsList serviceComponentFields = null;
            ServiceComponentFieldValuesList serviceComponentFieldValues = null;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action<Infragistics.Win.UltraWinTabControl.UltraTabPageControl>(ProcessControlBySelectedTab), selectedTab);
            }
            else
            {
                var serviceComponentId = selectedTab.Tab.Tag.ToString();
                var componentId = selectedTab.Tab.Key;
                var component = _tmpServiceComponentsForBuildMenuList.Find(p => p.v_ComponentId == componentId);

                foreach (var item in component.Fields)
                {
                    #region Nueva logica de busqueda de los campos por ID

                    var fields = selectedTab.Controls.Find(item.v_ComponentFieldId, true);

                    if (fields.Length != 0)
                    {
                        // Capturar objeto tag
                        keyTagControl = (KeyTagControl)fields[0].Tag;

                        // Datos de servicecomponentfieldValues Ejem: 1.80 ; 95 KG
                        value1 = GetValueControl(keyTagControl.i_ControlId, fields[0]);

                        if (keyTagControl.i_ControlId == (int)ControlType.UcOdontograma || keyTagControl.i_ControlId == (int)ControlType.UcAudiometria)
                        {
                            foreach (var value in _tmpListValuesOdontograma)
                            {
                                #region Armar entidad de datos desde los user controls [Odontograma / Audiometria]

                                _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                serviceComponentFields = new ServiceComponentFieldsList();
                                serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                serviceComponentFields.v_ComponentFieldsId = value.v_ComponentFieldId;
                                serviceComponentFields.v_ServiceComponentId = serviceComponentId;

                                serviceComponentFieldValues.v_Value1 = value.v_Value1;
                                _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;
                                // Agregar a mi lista
                                _serviceComponentFieldsList.Add(serviceComponentFields);

                                #endregion
                            }
                        }
                        else    // Todos los demas examenes
                        {
                            #region Armar entidad de datos que se va a grabar

                            // Datos de servicecomponentfields Ejem: Talla ; Peso ; etc
                            serviceComponentFields = new ServiceComponentFieldsList();

                            serviceComponentFields.v_ComponentFieldsId = keyTagControl.v_ComponentFieldsId;
                            serviceComponentFields.v_ServiceComponentId = serviceComponentId;

                            _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                            serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                            serviceComponentFieldValues.v_ComponentFieldValuesId = keyTagControl.v_ComponentFieldValuesId;
                            serviceComponentFieldValues.v_Value1 = value1;
                            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                            // Agregar a mi lista
                            _serviceComponentFieldsList.Add(serviceComponentFields);

                            #endregion
                        }                 
                    }

                    #endregion

                }              

            }

        }      

        private void SaveExamBySelectedTab(Infragistics.Win.UltraWinTabControl.UltraTabPageControl selectedTab)
        { 
             // Desactivar el flag de hubo alguna modificacion
            _isChangeValue = false;
            
            UltraValidator uv = null;
         
            try
            {
                var result = _dicUltraValidators.TryGetValue(_componentId, out uv);

                if (!result)
                    return;

                if (uv.Validate(false, true).IsValid)
                {
                    //DialogResult Result = MessageBox.Show("¿Está seguro de grabar este registro?", "CONFIRMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    //if (Result == DialogResult.Yes)
                    //{
                        // Mostrar pantalla grabando...
                        this.Enabled = false;
                        frmWaiting.Show(this);                                         

                        // Generar packete con data para grabar y pasarselo al hilo 
                        RunWorkerAsyncPackage packageForSave = new RunWorkerAsyncPackage();
                        packageForSave.SelectedTab = selectedTab;
                        packageForSave.ExamDiagnosticComponentList = _tmpDiagnosticList;
                        packageForSave.ServiceComponent = null;

                        bgwSaveExamen.RunWorkerAsync(packageForSave);             
                     
                    //}
                }
                else
                {
                    //MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                CloseErrorfrmWaiting();
                MessageBox.Show(ex.Message);
            }
        }

        private void bgwSaveExamen_DoWork(object sender, DoWorkEventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            try
            {
                RunWorkerAsyncPackage packageForSave = (RunWorkerAsyncPackage)e.Argument;

                #region GRABAR CONTROLES DINAMICOS
             
                var selectedTab = (Infragistics.Win.UltraWinTabControl.UltraTabPageControl) packageForSave.SelectedTab;
               
                ProcessControlBySelectedTab(selectedTab);

                _serviceBL.AddServiceComponentValues(ref objOperationResult,
                                                            _serviceComponentFieldsList,
                                                            Globals.ClientSession.GetAsList(),
                                                            _personId,
                                                            _serviceComponentId);            

                #endregion             

                #region GRABAR DATOS ADICIONALES COMO [Diagnósticos + restricciones + recomendaciones]

                // Grabar Dx por examen componente mas sus restricciones
                
                _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                    packageForSave.ExamDiagnosticComponentList,
                                                    packageForSave.ServiceComponent,
                                                    Globals.ClientSession.GetAsList(),
                                                    null, false);


                #endregion

                // Limpiar lista temp
                _serviceComponentFieldsList = null;
                _tmpListValuesOdontograma = null;

                // Analizar el resultado de la operación
                if (objOperationResult.Success != 1)
                {
                    CloseErrorfrmWaiting();
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                CloseErrorfrmWaiting();
                MessageBox.Show(ex.Message);
            }
         
        }

        private void bgwSaveExamen_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            #region refrescar

            flagValueChange = false;
            InitializeData();
            LoadDataBySelectedComponent(_componentId);
            //GetDiagnosticsForGridView();
            //GetDiagnosticsPlanTrabajoForGridView();
            //ConclusionesyTratamiento_LoadAllGrid();

            #endregion      

            this.Enabled = true;
            frmWaiting.Visible = false;

          
        }

        private void btnAgregarDxExamen_Click(object sender, EventArgs e)
        {
            var frm = new Popups.frmAddExamDiagnosticComponent("New");
            frm._componentId = _componentId;
            frm._serviceId = _serviceId;

            if (_tmpExamDiagnosticComponentList != null)
            {
                frm._tmpExamDiagnosticComponentList = _tmpExamDiagnosticComponentList;
            }

            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            // Refrescar grilla
            // Actualizar variable
            if (frm._tmpExamDiagnosticComponentList != null)
            {
                _tmpExamDiagnosticComponentList = frm._tmpExamDiagnosticComponentList;

              
            }
        }

        private DiagnosticRepositoryList SearchDxSugeridoOfSystem(string valueToAnalyzing, string pcomponentFieldsId)
        {          
            DiagnosticRepositoryList diagnosticRepository = null;

            if (string.IsNullOrEmpty(_componentId))
                return diagnosticRepository;

            string matchValId = null;
            bool exitLoop = false;         
            var componentField = _tmpServiceComponentsForBuildMenuList
                                .Find(p => p.v_ComponentId == _componentId)
                                .Fields.Find(p => p.v_ComponentFieldId == pcomponentFieldsId);

            if (componentField != null)
            {
                // Obtener el tipo de dato al cual se va castear un control especifico
                string dataTypeControlToParse = GetDataTypeControl(componentField.i_ControlId);
                
                if (componentField != null)
                {
                    foreach (ComponentFieldValues val in componentField.Values)
                    {
                        switch ((Operator2Values)val.i_OperatorId)
                        {
                            #region Analizar valor ingresado x el medico contra una serie de valores k se obtinen desde la BD

                            case Operator2Values.X_esIgualque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyzing) == int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyzing) == double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_noesIgualque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyzing) != int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyzing) != double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMenorque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyzing) < int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < 18.5 (bajo peso)
                                    if (double.Parse(valueToAnalyzing) < double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMenorIgualque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyzing) <= int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < 18.5 (bajo peso)
                                    if (double.Parse(valueToAnalyzing) <= double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyzing) > int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyzing) > double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorIgualque_A:
                                // X >= 40.0 (Obesidad clase III)
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyzing) >= int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < 18.5 (bajo peso)
                                    if (double.Parse(valueToAnalyzing) >= double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorque_A_yMenorque_B:

                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyzing) > int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyzing) < int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {                                   
                                    if (double.Parse(valueToAnalyzing) > double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyzing) < double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorque_A_yMenorIgualque_B:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyzing) > int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyzing) <= int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < A && X <= B 
                                    if (double.Parse(valueToAnalyzing) > double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyzing) <= double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorIgualque_A_yMenorque_B:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyzing) >= int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyzing) < int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyzing) >= double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyzing) < double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorIgualque_A_yMenorIgualque_B:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyzing) >= int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyzing) <= int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    var parse = double.Parse(valueToAnalyzing);
                                    if (double.Parse(valueToAnalyzing) >= double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyzing) <= double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            default:
                                MessageBox.Show("valor no encontrado " + valueToAnalyzing);
                                break;

                            #endregion
                        }

                        if (exitLoop)
                        {
                            #region CREAR / AGREGAR DX (automático)

                            matchValId = val.v_ComponentFieldValuesId;

                            // Si el valor analizado se encuentra en el rango de valores NORMALES, 
                            // entonces NO se genera un DX (automático).
                            if (val.v_DiseasesId == null)
                                break;

                            val.Recomendations.ForEach(item => { item.v_RecommendationId = Guid.NewGuid().ToString(); });
                            val.Restrictions.ForEach(item => { item.v_RestrictionByDiagnosticId = Guid.NewGuid().ToString(); });
                            // Insertar DX sugerido (automático) a la bolsa de DX 
                            diagnosticRepository = new DiagnosticRepositoryList();
                            diagnosticRepository.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                            diagnosticRepository.v_DiseasesId = val.v_DiseasesId;
                            diagnosticRepository.i_AutoManualId = (int)AutoManual.Automático;
                            diagnosticRepository.i_PreQualificationId = (int)PreQualification.SinPreCalificar;
                            diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.SinCalificar;
                            diagnosticRepository.v_ServiceId = _serviceId;
                            diagnosticRepository.v_ComponentId = _componentId;
                            diagnosticRepository.v_DiseasesName = val.v_DiseasesName;
                            diagnosticRepository.v_AutoManualName = "AUTOMÁTICO";
                            diagnosticRepository.v_RestrictionsName = ConcatenateRestrictions(val.Restrictions);
                            diagnosticRepository.v_RecomendationsName = ConcatenateRecommendations(val.Recomendations);
                            diagnosticRepository.v_PreQualificationName = "SIN PRE-CALIFICAR";
                            // ID enlace DX automatico para grabar valores dinamicos
                            diagnosticRepository.v_ComponentFieldValuesId = val.v_ComponentFieldValuesId;
                            diagnosticRepository.v_ComponentFieldsId = pcomponentFieldsId;
                            diagnosticRepository.Recomendations = RefreshRecomendationList(val.Recomendations);
                            diagnosticRepository.Restrictions = RefreshRestrictionList(val.Restrictions);
                            diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                            diagnosticRepository.i_RecordType = (int)RecordType.Temporal;

                            int vm = val.i_ValidationMonths == null ? 0 : val.i_ValidationMonths.Value;
                            diagnosticRepository.d_ExpirationDateDiagnostic = DateTime.Now.AddMonths(vm);

                            #endregion
                            break;
                        }

                    }
                }
            }

            return diagnosticRepository;
               
        }

        private bool? SaveExamWherePendingChange()
        {
            #region Validar cambios en los User Controls

            if (_componentId != null)
            {
                var audiometria = _tmpServiceComponentsForBuildMenuList
                                      .Find(p => p.v_ComponentId == _componentId)
                                      .Fields.Find(p => p.i_ControlId == (int)ControlType.UcAudiometria);

                if (audiometria != null)
                {
                    var ucAudiometria = (UserControls.ucAudiometria)FindControlInCurrentTab(audiometria.v_ComponentFieldId)[0];

                    if (ucAudiometria.IsChangeValueControl)
                    {
                        _isChangeValue = true;
                    }
                }

                var odontograma = _tmpServiceComponentsForBuildMenuList
                                      .Find(p => p.v_ComponentId == _componentId)
                                      .Fields.Find(p => p.i_ControlId == (int)ControlType.UcOdontograma);

                if (odontograma != null)
                {
                    var ucOdontograma = (UserControls.ucOdontograma)FindControlInCurrentTab(odontograma.v_ComponentFieldId)[0];

                    if (ucOdontograma.IsChangeValueControl)
                    {
                        _isChangeValue = true;

                    }
                }
            }

            #endregion

            bool? isOk = null;

            #region Validacion antes de navegar de tab en tab

            if (_isChangeValue)
            {             
                var rpta = MessageBox.Show("Ha realizado cambios, desea guardarlos antes de ir a otro exámen.", "CONFIRMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rpta == DialogResult.Yes)
                {
                    //SaveExamBySelectedTab(tcExamList.SelectedTab.TabPage);
                    isOk = SaveMedicalConsult();                                         
                }
                else
                {                  
                    _isChangeValue = false;                              
                }
            }

            return isOk;

            #endregion
        }

        private void tcExamList_SelectedTabChanging(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs e)
        {
            var isOk = SaveExamWherePendingChange();

            if (isOk != null)
            {
                if (!(isOk.Value))
                {                                     
                    e.Cancel = true;
                    var previousSelectedTab = tcExamList.SelectedTab.TabPage;  
                    tcExamList.Tabs[previousSelectedTab.Tab.Key].Selected = true;

                    Infragistics.Win.Appearance ab = new Infragistics.Win.Appearance();
                    //ab.BackColor = Color.FromArgb(193,213,239);
                    //ab.BackColor2 = Color.FromArgb(193, 213, 239);
                    ab.BackGradientStyle = GradientStyle.None;
                    ab.BackColor = Color.FromArgb(220, 235, 255);
                    ab.BackColor2 = Color.FromArgb(220, 235, 255);
                    ab.BorderAlpha = Alpha.Transparent;
                    e.Tab.Appearance = null;
                }
            }               
        }

        private void AddDiagnosticForMedicalConsult(DiseasesList diseases)
        {
            if (_tmpDiagnosticList != null)
            {
                var find = _tmpDiagnosticList.Find(p => p.v_DiseasesId == diseases.v_DiseasesId && p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                if (find != null)
                {
                    MessageBox.Show("Por favor seleccione otro Diagnóstico. Este ya esta agregado", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var frm = new Popups.frmAddDiagnosticMedicalConsult();
            //frm._componentId = _componentId;
            frm._mode = "New";
            frm._objDiseasesList = diseases;
            frm._serviceId = _serviceId;

            if (_tmpDiagnosticList != null)          
                frm._tmpDiagnosticList = _tmpDiagnosticList;
           
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            // Refrescar grilla
            // Actualizar variable
            if (frm._tmpDiagnosticList != null)
            {
                _tmpDiagnosticList = frm._tmpDiagnosticList;

                var dataList = _tmpDiagnosticList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdDiagnosticos.DataSource = dataList;
                lblRecordCountDiagnosticos.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }        
        }

        private void btnAgregarDiagnostico_Click(object sender, EventArgs e)
        {
            FindDiagnosticToAddMedicalConsult();
        }

        private void btnEditDiagnostico_Click(object sender, EventArgs e)
        {
            var frm = new Popups.frmAddDiagnosticMedicalConsult();
            frm._mode = "Edit";

            var diagnosticRepositoryId = grdDiagnosticos.Selected.Rows[0].Cells["v_DiagnosticRepositoryId"].Value.ToString();
            frm._diagnosticRepositoryId = diagnosticRepositoryId;

            //frm._componentId = _componentId;
            frm._serviceId = _serviceId;

            if (_tmpDiagnosticList != null)
            {
                frm._tmpDiagnosticList = _tmpDiagnosticList;
            }

            frm.ShowDialog();

            // Refrescar grilla
            // Actualizar variable

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            if (frm._tmpDiagnosticList != null)
            {
                _tmpDiagnosticList = frm._tmpDiagnosticList;

                var dataList = _tmpDiagnosticList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdDiagnosticos.DataSource = new DiagnosticRepositoryList();
                grdDiagnosticos.DataSource = dataList;
                lblRecordCountDiagnosticos.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
        }

        private void btnRemoveDiagnostico_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == DialogResult.Yes)
            {
                // Delete the item

                // Capturar id desde la griila de restricciones
                var diagnosticRepositoryId = grdDiagnosticos.Selected.Rows[0].Cells["v_DiagnosticRepositoryId"].Value.ToString();

                int recordType = int.Parse(grdDiagnosticos.Selected.Rows[0].Cells["i_RecordType"].Value.ToString());

                // Buscar registro para remover
                var findResult = _tmpDiagnosticList.Find(p => p.v_DiagnosticRepositoryId == diagnosticRepositoryId);

                if (recordType == (int)RecordType.Temporal)
                {
                    _tmpDiagnosticList.Remove(findResult);
                }
                else if (recordType == (int)RecordType.NoTemporal)
                {
                    findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
                }

                var dataList = _tmpDiagnosticList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                grdDiagnosticos.DataSource = dataList;
                lblRecordCountDiagnosticos.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
        }

        private void grdDiagnosticos_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnEditDiagnostico.Enabled = btnRemoveDiagnostico.Enabled = (grdDiagnosticos.Selected.Rows.Count > 0);
        }

        private void grdBusquedaDx_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            FindDiagnosticToAddMedicalConsult();
        }

        #region Util

        private void CloseErrorfrmWaiting()
        {
            if (frmWaiting.InvokeRequired)
            {
                this.Invoke(new Action(CloseErrorfrmWaiting));
            }
            else
            {
                this.Enabled = true;
                frmWaiting.Visible = false;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void SetSecurityByComponent()
        {
            OperationResult objOperationResult = new OperationResult();

            var nodeId = Globals.ClientSession.i_CurrentExecutionNodeId;
            var roleId = Globals.ClientSession.i_RoleId.Value;

            var compProfile = _serviceBL.GetRoleNodeComponentProfile(ref objOperationResult, nodeId, roleId, _componentId);

            if (compProfile == null)
                return;

            var componentFields = _tmpServiceComponentsForBuildMenuList.Find(p => p.v_ComponentId == _componentId).Fields;

            bool isReadOnly = false;

            if (compProfile.i_Read == (int)SiNo.SI)
            {
                isReadOnly = true;
            }

            if (compProfile.i_Write == (int)SiNo.SI)
            {
                isReadOnly = false;
            }

            #region Establecer permisos Lectura / escritura a cada campo de un examen componente
           
            foreach (ComponentFieldsList cf in componentFields)
            {
                var ctrl__ = tcExamList.SelectedTab.TabPage.Controls.Find(cf.v_ComponentFieldId, true);

                if (ctrl__.Length != 0)
                {
                    #region Setear valor

                    switch ((ControlType)cf.i_ControlId)
                    {
                        case ControlType.CadenaTextual:
                            TextBox txtt = (TextBox)ctrl__[0];
                            txtt.CreateControl();
                            txtt.ReadOnly = isReadOnly;
                            break;
                        case ControlType.CadenaMultilinea:
                            TextBox txtm = (TextBox)ctrl__[0];
                            txtm.CreateControl();
                            txtm.ReadOnly = isReadOnly;
                            break;
                        case ControlType.NumeroEntero:
                            UltraNumericEditor uni = (UltraNumericEditor)ctrl__[0];
                            uni.CreateControl();
                            uni.ReadOnly = isReadOnly;
                            break;
                        case ControlType.NumeroDecimal:
                            UltraNumericEditor und = (UltraNumericEditor)ctrl__[0];
                            und.CreateControl();
                            und.ReadOnly = isReadOnly;
                            break;
                        case ControlType.SiNoCheck:
                            CheckBox chkSiNo = (CheckBox)ctrl__[0];
                            chkSiNo.CreateControl();
                            chkSiNo.Enabled = isReadOnly;
                            break;
                        case ControlType.SiNoRadioButton:
                            RadioButton rbSiNo = (RadioButton)ctrl__[0];
                            rbSiNo.CreateControl();
                            rbSiNo.Enabled = isReadOnly;
                            break;
                        case ControlType.SiNoCombo:
                            ComboBox cbSiNo = (ComboBox)ctrl__[0];
                            cbSiNo.CreateControl();
                            cbSiNo.Enabled = isReadOnly;
                            break;
                        case ControlType.UcFileUpload:
                            break;
                        case ControlType.Lista:
                            ComboBox cbList = (ComboBox)ctrl__[0];
                            cbList.CreateControl();
                            cbList.Enabled = isReadOnly;
                            break;
                        default:
                            break;
                    }

                    #endregion
                }
            }

            #endregion

   

        }

        private UltraValidator CreateUltraValidatorByComponentId(string componentId)
        {          
            UltraValidator uv = new UltraValidator(this.components);

            if (_dicUltraValidators == null)
                _dicUltraValidators = new Dictionary<string, UltraValidator>();

            _dicUltraValidators.Add(componentId, uv);       
            return uv;
        }

        private Control[] FindDynamicControl(string key)
        {
            // Obtener TabPage actual
            var currentTabPage = tcExamList.SelectedTab.TabPage;
            //var findControl = currentTabPage.Controls.Find(key, true);

            var findControl =  tcExamList.Tabs.TabControl.Controls.Find(key, true);

            return findControl;
        }

        private Control[] FindControlInCurrentTab(string key)
        {
            // Obtener TabPage actual
            var currentTabPage = tcExamList.SelectedTab.TabPage;
            var findControl = currentTabPage.Controls.Find(key, true);       
            return findControl;
        }

        private void SearchControlAndSetValue(Control ctrlContainer)
        {
            KeyTagControl keyTagControl = null;
            bool breakHazChildrenUC = false;
            List<ServiceComponentFieldValuesList> dataSourceUserControls = null; 

            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl.Tag != null)
                {
                    var t = ctrl.Tag.GetType();

                    // Los controles que tienen el objeto KeyTagControl en su propiedad Tag son los controles que se crean dinamicamente
                    // y tienen una logica particular de muestreo de datos

                    if (t == typeof(KeyTagControl))
                    {                                            
                        // Capturar objeto tag
                        keyTagControl = (KeyTagControl)ctrl.Tag;

                        if (keyTagControl.i_ControlId == (int)ControlType.UcOdontograma)
                        {
                            #region Setear valores en Odontograma

                            dataSourceUserControls = _serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                            dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("ODO"));
                            ((UserControls.ucOdontograma)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                            ((UserControls.ucOdontograma)ctrl).DataSource = dataSourceUserControls;
                            breakHazChildrenUC = true;

                            #endregion

                        }
                        else if (keyTagControl.i_ControlId == (int)ControlType.UcAudiometria)
                        {
                            #region Setear valores en udiometria

                            dataSourceUserControls = _serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                            dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("AUD"));
                            ((UserControls.ucAudiometria)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                            ((UserControls.ucAudiometria)ctrl).DataSource = dataSourceUserControls;
                            breakHazChildrenUC = true;

                            #endregion
                        }
                        else
                        { 
                            foreach (var item in _serviceComponentsInfo.ServiceComponentFields)
                            {
                                var componentFieldsId = item.v_ComponentFieldsId;

                                foreach (var fv in item.ServiceComponentFieldValues)
                                {
                                    #region Setear valores en el caso de controles dinamicos

                                    SetValueControl(keyTagControl.i_ControlId,
                                                    ctrl,
                                                    componentFieldsId,
                                                    keyTagControl.v_ComponentFieldsId,
                                                    fv.v_Value1,
                                                    item.i_HasAutomaticDxId == null ? (int)SiNo.NO : (SiNo)item.i_HasAutomaticDxId);

                                    #endregion
                                }
                            }
                        }
                    }
                }

                if (ctrl.HasChildren)
                {
                    if (!breakHazChildrenUC && keyTagControl == null)
                    {
                        SearchControlAndSetValue(ctrl);
                    }
                }
            }       

        }

        private Control GetControl(int ControlId, Control ctrl)
        {
            Control ctrlToCast = null;

            switch ((ControlType)ControlId)
            {
                case ControlType.NumeroEntero:
                    ctrlToCast = (UltraNumericEditor)ctrl;
                    break;
                case ControlType.NumeroDecimal:
                    ctrlToCast = (UltraNumericEditor)ctrl;
                    break;
                case ControlType.SiNoCheck:
                    ctrlToCast = (CheckBox)ctrl;
                    break;
                case ControlType.SiNoRadioButton:
                    ctrlToCast = (RadioButton)ctrl;
                    break;
                case ControlType.SiNoCombo:
                    ctrlToCast = (ComboBox)ctrl;
                    break;
                case ControlType.Lista:
                    ctrlToCast = (ComboBox)ctrl;
                    break;
                default:
                    break;
            }

            return ctrl;
        }

        private string GetDataTypeControl(int ControlId)
        {
            string dataType = null;

            switch ((ControlType)ControlId)
            {             
                case ControlType.NumeroEntero:
                    dataType = "int";
                    break;
                case ControlType.NumeroDecimal:
                    dataType = "double";
                    break;
                case ControlType.SiNoCheck:
                    break;
                case ControlType.SiNoRadioButton:
                    break;
                case ControlType.SiNoCombo:
                    break;             
                case ControlType.Lista:
                    dataType = "int";
                    break;
                default:
                    break;
            }

            return dataType;
        }

        private string GetValueControl(int ControlId, Control ctrl)
        {
            string value1 = null;

            switch ((ControlType)ControlId)
            {
                case ControlType.CadenaTextual:
                    value1 = ((TextBox)ctrl).Text;
                    break;
                case ControlType.CadenaMultilinea:
                    value1 = ((TextBox)ctrl).Text;
                    break;
                case ControlType.NumeroEntero:
                    value1 = ((UltraNumericEditor)ctrl).Value.ToString();
                    break;
                case ControlType.NumeroDecimal:
                    //value1 = ((UltraNumericEditor)ctrl).Value.ToString();
                    value1 = ctrl.Text.Trim();
                    break;
                case ControlType.SiNoCheck:
                    value1 = Convert.ToInt32(((CheckBox)ctrl).Checked).ToString();
                    break;
                case ControlType.SiNoRadioButton:
                    value1 = Convert.ToInt32(((RadioButton)ctrl).Checked).ToString();
                    break;
                case ControlType.SiNoCombo:
                    value1 = ((ComboBox)ctrl).SelectedValue.ToString();
                    break;
                case ControlType.Lista:
                    value1 = ((ComboBox)ctrl).SelectedValue.ToString();
                    break;
                case ControlType.UcOdontograma:                 
                    _tmpListValuesOdontograma = ((UserControls.ucOdontograma)ctrl).DataSource;
                    break;
                case ControlType.UcAudiometria:
                    _tmpListValuesOdontograma = ((UserControls.ucAudiometria)ctrl).DataSource;
                    break;
                default:
                    break;
            }

            return value1;
        }

        private void SetValueControl(int ControlId, Control ctrl, string ComponentFieldsId, string Tag_ComponentFieldsId, string Value1, SiNo HasAutomaticDx)
        {            
            switch ((ControlType)ControlId)
            {
                case ControlType.CadenaTextual:
                    if (ComponentFieldsId == Tag_ComponentFieldsId)
                    {
                        ((TextBox)ctrl).Text = Value1;
                        if (HasAutomaticDx == SiNo.SI)
                            ctrl.BackColor = Color.Red;

                    }
                    break;
                case ControlType.CadenaMultilinea:
                    if (ComponentFieldsId == Tag_ComponentFieldsId)
                    {
                        ((TextBox)ctrl).Text = Value1;
                        if (HasAutomaticDx == SiNo.SI)
                            ctrl.BackColor = Color.Red;

                    }
                    break;
                case ControlType.NumeroEntero:
                    if (ComponentFieldsId == Tag_ComponentFieldsId)
                    {
                        ((UltraNumericEditor)ctrl).Value = Value1;
                        if (HasAutomaticDx == SiNo.SI)
                            ctrl.BackColor = Color.Red;
                       
                    }                   
                    break;
                case ControlType.NumeroDecimal:
                    if (ComponentFieldsId == Tag_ComponentFieldsId)
                    {
                        ((UltraNumericEditor)ctrl).Value = Value1;
                        if (HasAutomaticDx == SiNo.SI)
                            ctrl.BackColor = Color.Red;
                        //else
                        //    ctrl.BackColor = Color.White;
                    }                      
                    break;
                case ControlType.SiNoCheck:
                    if (ComponentFieldsId == Tag_ComponentFieldsId)
                    {
                        ((CheckBox)ctrl).Checked = Convert.ToBoolean(int.Parse(Value1));
                    }
                    break;
                case ControlType.SiNoRadioButton:
                    if (ComponentFieldsId == Tag_ComponentFieldsId)
                    {
                        ((RadioButton)ctrl).Checked = Convert.ToBoolean(int.Parse(Value1));
                    }
                    break;
                case ControlType.SiNoCombo:
                    if (ComponentFieldsId == Tag_ComponentFieldsId)
                    {
                        ((ComboBox)ctrl).SelectedValue = Value1;
                    }
                    break;
                case ControlType.Lista:
                    if (ComponentFieldsId == Tag_ComponentFieldsId)
                    {
                        var cb = (ComboBox)ctrl;                       
                        cb.SelectedValue = Value1;
                    }
                    break;
                default:
                    break;
            }          
        }

        private void ClearAndSetValueControlByDefault(int ControlId, Control ctrl)
        {
            switch ((ControlType)ControlId)
            {
                case ControlType.CadenaTextual:
                    ((TextBox)ctrl).Text = string.Empty;
                    ctrl.BackColor = Color.White;
                    break;
                case ControlType.CadenaMultilinea:
                    ((TextBox)ctrl).Text = string.Empty;
                    ctrl.BackColor = Color.White;
                    break;
                case ControlType.NumeroEntero:                    
                    ((UltraNumericEditor)ctrl).Value = 0;                      
                    ctrl.BackColor = Color.White;                
                    break;
                case ControlType.NumeroDecimal:                   
                    ((UltraNumericEditor)ctrl).Value = 0.00;                      
                    ctrl.BackColor = Color.White;                   
                    break;
                case ControlType.SiNoCheck:                   
                    ((CheckBox)ctrl).Checked = false;                 
                    break;
                case ControlType.SiNoRadioButton:                  
                    ((RadioButton)ctrl).Checked = false;                 
                    break;
                case ControlType.SiNoCombo:                   
                    ((ComboBox)ctrl).SelectedValue = "-1";                  
                    break;
                case ControlType.Lista:                   
                        var cb = (ComboBox)ctrl;
                        cb.SelectedValue = "-1";              
                    break;
                default:
                    break;
            }
        }

        private List<RestrictionList> RefreshRestrictionList(List<RestrictionList> prestrictions)
        {
            var restrictionsList = new List<RestrictionList>();

            foreach (var item in prestrictions)
            {
                // Agregar restricciones (Automáticas) a la Lista mas lo que ya tiene
                RestrictionList restriction = new RestrictionList();

                restriction.v_RestrictionByDiagnosticId = item.v_RestrictionByDiagnosticId;
                restriction.v_ServiceId = _serviceId;
                restriction.v_DiagnosticRepositoryId = item.v_DiagnosticRepositoryId;
                restriction.v_MasterRestrictionId = item.v_MasterRestrictionId;
                restriction.v_RestrictionName = item.v_RestrictionName;
                restriction.i_RecordStatus = (int)RecordStatus.Agregado;
                restriction.i_RecordType = (int)RecordType.Temporal;

                restrictionsList.Add(restriction);
            }

            return restrictionsList;
        }

        private List<RecomendationList> RefreshRecomendationList(List<RecomendationList> precomendations)
        {
            var recomendationsList = new List<RecomendationList>();

            foreach (var item in precomendations)
            {
                // Agregar restricciones a la Lista mas lo que ya tiene
                RecomendationList recomendation = new RecomendationList();

                recomendation.v_RecommendationId = item.v_RecommendationId;
                recomendation.v_ServiceId = _serviceId;
                recomendation.v_DiagnosticRepositoryId = item.v_DiagnosticRepositoryId;
                recomendation.v_MasterRecommendationId = item.v_RecommendationId;
                recomendation.v_RecommendationName = item.v_RecommendationName;
                recomendation.i_RecordStatus = (int)RecordStatus.Agregado;
                recomendation.i_RecordType = (int)RecordType.Temporal;

                recomendationsList.Add(recomendation);
            }

            return recomendationsList;
        }

        private string ConcatenateRestrictions(List<RestrictionList> prestrictions)
        {
            if (prestrictions == null)
                return string.Empty;

            var qry = (from a in prestrictions  // RESTRICCIONES POR Diagnosticos                                           
                       where a.i_RecordStatus != (int)RecordStatus.EliminadoLogico
                       select new
                       {
                           v_RestrictionsName = a.v_RestrictionName
                       }).ToList();

            return string.Join(", ", qry.Select(p => p.v_RestrictionsName));
        }

        private string ConcatenateRecommendations(List<RecomendationList> precomendations)
        {
            if (precomendations == null)
                return string.Empty;

            var qry = (from a in precomendations  // RESTRICCIONES POR Diagnosticos                                           
                       where a.i_RecordStatus != (int)RecordStatus.EliminadoLogico
                       select new
                       {
                           v_RecommendationName = a.v_RecommendationName
                       }).ToList();

            return string.Join(", ", qry.Select(p => p.v_RecommendationName));
        }

        private void SetControlValidate(int ControlId, Control ctrl, float? ValidateValue1, float? ValidateValue2, UltraValidator uv)
        {
            // Objetos para validar
            RangeCondition rc = null;
            ValidationSettings vs = null;
          
            uv.ErrorAppearance.BackColor = Color.FromArgb(255, 128, 0);
            uv.ErrorAppearance.BackGradientStyle = GradientStyle.Vertical;
            uv.ErrorAppearance.BorderColor = Color.Red;
            uv.NotificationSettings.Action = NotificationAction.MessageBox;           

            switch ((ControlType)ControlId)
            {
                case ControlType.CadenaTextual:
                    uv.GetValidationSettings((TextBox)ctrl).IsRequired = true;
                    break;
                case ControlType.CadenaMultilinea:
                    uv.GetValidationSettings((TextBox)ctrl).IsRequired = true;
                    break;
                case ControlType.NumeroEntero:
                    // Establecer condición por rangos
                    rc = new Infragistics.Win.RangeCondition(ValidateValue1,
                                                             ValidateValue2,
                                                             typeof(int));
                    vs = uv.GetValidationSettings((UltraNumericEditor)ctrl);
                    vs.Condition = rc;
                    break;
                case ControlType.NumeroDecimal:
                    // Establecer condición por rangos
                    rc = new Infragistics.Win.RangeCondition(ValidateValue1,
                                                             ValidateValue2,
                                                             typeof(double));
                    vs = uv.GetValidationSettings((UltraNumericEditor)ctrl);
                    vs.Condition = rc;
                    break;
                case ControlType.SiNoCheck:
                    break;
                case ControlType.SiNoRadioButton:
                    break;
                case ControlType.SiNoCombo:
                    uv.GetValidationSettings(ctrl).Condition = new OperatorCondition(ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
                    uv.GetValidationSettings(ctrl).EmptyValueCriteria = EmptyValueCriteria.NullOrEmptyString;
                    uv.GetValidationSettings(ctrl).IsRequired = true;
                    break;
                case ControlType.UcFileUpload:
                    break;
                case ControlType.Lista:
                    uv.GetValidationSettings(ctrl).Condition = new OperatorCondition(ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
                    uv.GetValidationSettings(ctrl).EmptyValueCriteria = EmptyValueCriteria.NullOrEmptyString;
                    uv.GetValidationSettings(ctrl).IsRequired = true;
                    break;
                default:
                    break;
            }                 
        } 

        private void ValidateRemoveDxAutomatic()
        {
            //if (grdDiagnosticoPorExamenComponente.Selected.Rows.Count > 0)
            //{
            //    if (isDisabledButtonsExamDx)
            //    {
            //        var autoManualId = (AutoManual)grdDiagnosticoPorExamenComponente.Selected.Rows[0].Cells["i_AutoManualId"].Value;

            //        switch (autoManualId)
            //        {
            //            case AutoManual.Automático:
            //                btnRemoverDxExamen.Enabled = false;
            //                break;
            //            case AutoManual.Manual:
            //                btnRemoverDxExamen.Enabled = true;
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //}
          
        }

        private void SetDefaultValueAfterBuildMenu()
        {
            try
            {
                // Establecer valores x defecto  a los controles
                foreach (BE.ComponentList com in _tmpServiceComponentsForBuildMenuList)
                {
                    // Capturar tab [Triaje, sensometria]
                    var findTab = tcExamList.Tabs[com.v_ComponentId];

                    foreach (ComponentFieldsList cf in com.Fields)
                    {
                        var ctrl__ = findTab.TabPage.Controls.Find(cf.v_ComponentFieldId, true);

                        if (ctrl__.Length != 0)
                        {
                            #region Setear valor x defecto del control
                                                     
                            switch ((ControlType)cf.i_ControlId)
                            {
                                case ControlType.CadenaTextual:
                                    TextBox txtt = (TextBox)ctrl__[0];
                                    txtt.CreateControl();
                                    txtt.Text = cf.v_DefaultText;
                                    txtt.BackColor = Color.White;
                                    break;
                                case ControlType.CadenaMultilinea:
                                    TextBox txtm = (TextBox)ctrl__[0];
                                    txtm.CreateControl();
                                    txtm.Text = cf.v_DefaultText;
                                    txtm.BackColor = Color.White;
                                    break;
                                case ControlType.NumeroEntero:
                                    UltraNumericEditor uni = (UltraNumericEditor)ctrl__[0];
                                    uni.CreateControl();
                                    uni.Value = string.IsNullOrEmpty(cf.v_DefaultText) ? 0 : int.Parse(cf.v_DefaultText);
                                    uni.BackColor = Color.White;
                                    break;
                                case ControlType.NumeroDecimal:
                                    UltraNumericEditor und = (UltraNumericEditor)ctrl__[0];
                                    und.CreateControl();
                                    und.Value = string.IsNullOrEmpty(cf.v_DefaultText) ? 0 : double.Parse(cf.v_DefaultText);
                                    und.BackColor = Color.White;
                                    break;
                                case ControlType.SiNoCheck:
                                    CheckBox chkSiNo = (CheckBox)ctrl__[0];
                                    chkSiNo.CreateControl();
                                    chkSiNo.Checked = string.IsNullOrEmpty(cf.v_DefaultText) ? false : Convert.ToBoolean(int.Parse(cf.v_DefaultText));
                                    break;
                                case ControlType.SiNoRadioButton:
                                    RadioButton rbSiNo = (RadioButton)ctrl__[0];
                                    rbSiNo.CreateControl();
                                    rbSiNo.Checked = string.IsNullOrEmpty(cf.v_DefaultText) ? false : Convert.ToBoolean(int.Parse(cf.v_DefaultText));
                                    break;
                                case ControlType.SiNoCombo:
                                    ComboBox cbSiNo = (ComboBox)ctrl__[0];
                                    cbSiNo.CreateControl();
                                    cbSiNo.SelectedValue = string.IsNullOrEmpty(cf.v_DefaultText) ? "-1" : cf.v_DefaultText;
                                    break;
                                case ControlType.UcFileUpload:
                                    break;
                                case ControlType.Lista:
                                    ComboBox cbList = (ComboBox)ctrl__[0];
                                    cbList.CreateControl();
                                    cbList.SelectedValue = string.IsNullOrEmpty(cf.v_DefaultText) ? "-1" : cf.v_DefaultText;
                                    break;
                                case ControlType.UcOdontograma:
                                    //((UserControls.ucOdontograma)ctrl__[0]).ClearValueControl();;
                                    break;
                                case ControlType.UcAudiometria:
                                    ((UserControls.ucAudiometria)ctrl__[0]).ClearValueControl();
                                    break;
                                default:
                                    break;
                            }

                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetDefaultValueBySelectedTab()
        {
            try
            {
                var component = _tmpServiceComponentsForBuildMenuList.Find(p => p.v_ComponentId == _componentId);

                foreach (ComponentFieldsList cf in component.Fields)
                {
                    var field = tcExamList.SelectedTab.TabPage.Controls.Find(cf.v_ComponentFieldId, true);

                    if (field.Length != 0)
                    {
                        #region Setear valor x defecto del control

                        switch ((ControlType)cf.i_ControlId)
                        {
                            case ControlType.CadenaTextual:
                                TextBox txtt = (TextBox)field[0];
                                txtt.CreateControl();
                                txtt.Text = cf.v_DefaultText;
                                txtt.BackColor = Color.White;
                                break;
                            case ControlType.CadenaMultilinea:
                                TextBox txtm = (TextBox)field[0];
                                txtm.CreateControl();
                                txtm.Text = cf.v_DefaultText;
                                txtm.BackColor = Color.White;
                                break;
                            case ControlType.NumeroEntero:
                                UltraNumericEditor uni = (UltraNumericEditor)field[0];
                                uni.CreateControl();
                                uni.Value = string.IsNullOrEmpty(cf.v_DefaultText) ? 0 : int.Parse(cf.v_DefaultText);
                                uni.BackColor = Color.White;
                                break;
                            case ControlType.NumeroDecimal:
                                UltraNumericEditor und = (UltraNumericEditor)field[0];
                                und.CreateControl();
                                und.Value = string.IsNullOrEmpty(cf.v_DefaultText) ? 0 : double.Parse(cf.v_DefaultText);
                                und.BackColor = Color.White;
                                break;
                            case ControlType.SiNoCheck:
                                CheckBox chkSiNo = (CheckBox)field[0];
                                chkSiNo.CreateControl();
                                chkSiNo.Checked = string.IsNullOrEmpty(cf.v_DefaultText) ? false : Convert.ToBoolean(int.Parse(cf.v_DefaultText));
                                break;
                            case ControlType.SiNoRadioButton:
                                RadioButton rbSiNo = (RadioButton)field[0];
                                rbSiNo.CreateControl();
                                rbSiNo.Checked = string.IsNullOrEmpty(cf.v_DefaultText) ? false : Convert.ToBoolean(int.Parse(cf.v_DefaultText));
                                break;
                            case ControlType.SiNoCombo:
                                ComboBox cbSiNo = (ComboBox)field[0];
                                cbSiNo.CreateControl();
                                cbSiNo.SelectedValue = string.IsNullOrEmpty(cf.v_DefaultText) ? "-1" : cf.v_DefaultText;
                                break;
                            case ControlType.UcFileUpload:
                                break;
                            case ControlType.Lista:
                                ComboBox cbList = (ComboBox)field[0];
                                cbList.CreateControl();
                                cbList.SelectedValue = string.IsNullOrEmpty(cf.v_DefaultText) ? "-1" : cf.v_DefaultText;
                                break;
                            case ControlType.UcOdontograma:
                                ((UserControls.ucOdontograma)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcAudiometria:
                                ((UserControls.ucAudiometria)field[0]).ClearValueControl();
                                break;
                            default:
                                break;
                        }

                        #endregion
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #endregion   

        #region Busqueda de Diagnósticos

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (uvBusquedaDx2.Validate(true, false).IsValid)
            {
                // Get the filters from the UI
                List<string> Filters = new List<string>();

                if (rbCode.Checked == true)
                {
                    if (!string.IsNullOrEmpty(txtDiseasesFilter.Text)) Filters.Add("v_CIE10Id.Contains(\"" + txtDiseasesFilter.Text.Trim() + "\")");
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtDiseasesFilter.Text)) Filters.Add("v_Name.Contains(\"" + txtDiseasesFilter.Text.Trim() + "\")");
                    if (!string.IsNullOrEmpty(txtDiseasesFilter.Text)) Filters.Add("v_CIE10Description1.Contains(\"" + txtDiseasesFilter.Text.Trim() + "\")");
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
            grdBusquedaDx.DataSource = objData;
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<DiseasesList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();


            var _objData = _objMedicalExamFieldValuesBL.GetDiseasesPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void FindDiagnosticToAddMedicalConsult()
        {
            OperationResult objOperationResult = new OperationResult();
            diseasesDto objDiseaseDto = new diseasesDto();
            diseasesDto objDiseaseDto1 = new diseasesDto();
            if (grdBusquedaDx.Selected.Rows == null) return;
            if (grdBusquedaDx.Selected.Rows.Count == 0) return;

            if (grdBusquedaDx.Selected.Rows[0].Cells["v_DiseasesId"].Value == null)
            {
                objDiseaseDto.v_CIE10Id = grdBusquedaDx.Selected.Rows[0].Cells[1].Value.ToString();
                objDiseaseDto.v_Name = grdBusquedaDx.Selected.Rows[0].Cells[2].Value.ToString();

                objDiseaseDto1 = _objMedicalExamFieldValuesBL.GetIsValidateDiseases(ref objOperationResult, objDiseaseDto.v_Name);

                if (objDiseaseDto1 == null)
                {
                    objDiseaseDto.v_DiseasesId = _objMedicalExamFieldValuesBL.AddDiseases(ref objOperationResult, objDiseaseDto, Globals.ClientSession.GetAsList());
                }
                else
                {
                    MessageBox.Show("Escoja uno que tenga código interno", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _objDiseasesList = new DiseasesList();
                _objDiseasesList.v_DiseasesId = objDiseaseDto.v_DiseasesId;
                _objDiseasesList.v_CIE10Id = objDiseaseDto.v_CIE10Id;
                _objDiseasesList.v_Name = objDiseaseDto.v_Name;

                AddDiagnosticForMedicalConsult(_objDiseasesList);
            }
            else
            {
                _objDiseasesList = new DiseasesList();
                _objDiseasesList.v_DiseasesId = grdBusquedaDx.Selected.Rows[0].Cells[0].Value.ToString();
                _objDiseasesList.v_CIE10Id = grdBusquedaDx.Selected.Rows[0].Cells[1].Value.ToString();
                _objDiseasesList.v_Name = grdBusquedaDx.Selected.Rows[0].Cells[2].Value.ToString();
                AddDiagnosticForMedicalConsult(_objDiseasesList);
            }
           
        }

        private void grdBusquedaDx_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnAgregarDiagnostico.Enabled = (grdBusquedaDx.Selected.Rows.Count > 0);
           
        }

        private void grdBusquedaDx_Click(object sender, EventArgs e)
        {
            if (grdBusquedaDx.Selected.Rows == null) return;
            if (grdBusquedaDx.Selected.Rows.Count == 0) return;

            btnUpdateandSelect.Enabled = true;
            if (grdBusquedaDx.Selected.Rows[0].Cells[2].Value != null)
            {
                txtDiseases.Text = grdBusquedaDx.Selected.Rows[0].Cells[2].Value.ToString();
            }
            else
            {
                txtDiseases.Text = "No tiene Nombre";
            }

        }

        private void btnUpdateandSelect_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            diseasesDto objDiseaseDto = new diseasesDto();
            diseasesDto objDiseaseDto1 = new diseasesDto();
            if (btnUpdateandSelect.Enabled == false) return;

            if (uvBusquedaDx1.Validate(true, false).IsValid)
            {
                if (grdBusquedaDx.Selected.Rows[0].Cells[0].Value != null)
                {
                    objDiseaseDto = _objMedicalExamFieldValuesBL.GetDiseases(ref  objOperationResult, grdBusquedaDx.Selected.Rows[0].Cells[0].Value.ToString());

                    objDiseaseDto.v_CIE10Id = grdBusquedaDx.Selected.Rows[0].Cells[1].Value.ToString();
                    objDiseaseDto.v_Name = txtDiseases.Text;
                    _objMedicalExamFieldValuesBL.UpdateDiseases(ref objOperationResult, objDiseaseDto, Globals.ClientSession.GetAsList());

                    _objDiseasesList.v_DiseasesId = objDiseaseDto.v_DiseasesId;
                    _objDiseasesList.v_CIE10Id = objDiseaseDto.v_CIE10Id;
                    _objDiseasesList.v_Name = objDiseaseDto.v_Name;
                }
                else
                {
                    objDiseaseDto.v_CIE10Id = grdBusquedaDx.Selected.Rows[0].Cells[1].Value.ToString();
                    objDiseaseDto.v_Name = txtDiseases.Text;


                    objDiseaseDto1 = _objMedicalExamFieldValuesBL.GetIsValidateDiseases(ref objOperationResult, objDiseaseDto.v_Name);

                    if (objDiseaseDto1 == null)
                    {
                        objDiseaseDto.v_DiseasesId = _objMedicalExamFieldValuesBL.AddDiseases(ref objOperationResult, objDiseaseDto, Globals.ClientSession.GetAsList());
                    }
                    else
                    {
                        MessageBox.Show("Escoja uno que tenga código interno", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    //objDiseaseDto.v_DiseasesId=  _objMedicalExamFieldValuesBL.AddDiseases(ref objOperationResult, objDiseaseDto, Globals.ClientSession.GetAsList());

                    _objDiseasesList.v_DiseasesId = objDiseaseDto.v_DiseasesId;
                    _objDiseasesList.v_CIE10Id = grdBusquedaDx.Selected.Rows[0].Cells[1].Value.ToString();
                    _objDiseasesList.v_Name = txtDiseases.Text;
                }

                //strEnfermedad = objDiseaseDto.v_DiseasesId;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    
        private void txtDiseases_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnUpdateandSelect_Click(sender, e);
            }
        }

        private void txtDiseasesFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnFilter_Click(sender, e);
            }
        }

      

        #endregion

        #endregion

        #region Plan de Trabajo

        private void GetDiagnosticsPlanTrabajoForGridView()
        {
            OperationResult objOperationResult = new OperationResult();
            _tmpDiagnosticPlanTrabajoList = _serviceBL.GetDisgnosticsByServiceId(ref objOperationResult, _serviceId);

            grdDiagnosticoPlanTrabajo.DataSource = _tmpDiagnosticPlanTrabajoList;
            gbDisgnosticosPlanTrabajo.Text = string.Format("Se encontraron {0} Diagnósticos.", _tmpDiagnosticPlanTrabajoList.Count());

            #region Medicación

            _tmpMedicationList = _serviceBL.GetServiceMedicationsForGridView(ref objOperationResult, _serviceId);

            grdMedicacion.DataSource = _tmpMedicationList;
            lblRecordCountMedication.Text = string.Format("Se encontraron {0} registros.", _tmpMedicationList.Count());

            #endregion

            #region Procedimientos
            _tmpProcedureList = _serviceBL.GetServiceProceduresForGridView(ref objOperationResult, _serviceId);

            grdProcedimientos.DataSource = _tmpProcedureList;
            lblRecordCountProcedures.Text = string.Format("Se encontraron {0} registros.", _tmpProcedureList.Count());

            #endregion

            #region Examenes Auxiliares

            _auxiliaryExams = _serviceBL.GetAuxiliaryExams(ref objOperationResult, _serviceId);

            grdExamenAuxiliar.DataSource = _auxiliaryExams;
            lblRecordCountExamenAuxiliar.Text = string.Format("Se encontraron {0} registros.", _auxiliaryExams.Count());

            #endregion

            #region Interconsulta

            _interconsultations = _serviceBL.GetInterconsultations(ref objOperationResult, _serviceId);

            grdInterConsulta.DataSource = _interconsultations;
            lblRecordCountInterConsulta.Text = string.Format("Se encontraron {0} registros.", _interconsultations.Count());

            #endregion

            #region Descanso Medico

            _medicalBreaks = _serviceBL.GetMedicalBreaks(ref objOperationResult, _serviceId);

            grdDescansoMedico.DataSource = _medicalBreaks;
            lblRecordCountDescansoMedico.Text = string.Format("Se encontraron {0} registros.", _medicalBreaks.Count());

            #endregion

            #region Restricciones

            _tmpRestrictionList = _serviceBL.GetServiceRestrictionsForGridView(ref objOperationResult, _serviceId);

            grdRestricciones.DataSource = _tmpRestrictionList;
            lblRecordCountRestricciones.Text = string.Format("Se encontraron {0} registros.", _tmpRestrictionList.Count());

            #endregion

            // Analizar el resultado de la operación
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        
        }
 
        private void btnAgregarMedicacion_Click(object sender, EventArgs e)
        {
            var frm = new Popups.frmAddMedicationMedicalConsult();
            frm._mode = "New";
            frm._serviceId = _serviceId;

            if (_tmpMedicationList != null)
                frm._tmpMedicationList = _tmpMedicationList;

            frm.btnAddAndNew.Click += new EventHandler(frmAddMedicationMedicalConsult_btnAdd_Click);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            _pharmacyWarehouseId = frm._pharmacyWarehouseId;
            // Refrescar grilla
            // Actualizar variable
            if (frm._tmpMedicationList != null)
            {
                _tmpMedicationList = frm._tmpMedicationList;

                var dataList = _tmpMedicationList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdMedicacion.DataSource = dataList;
                lblRecordCountMedication.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
               
            }

        }

        private void btnEditarMedicacion_Click(object sender, EventArgs e)
        {
            var frm = new Popups.frmAddMedicationMedicalConsult();
            frm._id = _productId;
            frm._mode = "Edit";

            if (_tmpMedicationList != null)
            {
                frm._tmpMedicationList = _tmpMedicationList;
            }
            frm.ShowDialog();

            if (frm._tmpMedicationList != null)
            {
                _tmpMedicationList = frm._tmpMedicationList;

                var dataList = _tmpMedicationList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                               
                grdMedicacion.DataSource = new List<MedicationList>();
                grdMedicacion.DataSource = dataList;
                lblRecordCountMedication.Text = string.Format("Se encontraron {0} registros.", dataList.Count());                                       
            }
        }

        private void btnRemoverMedicacion_Click(object sender, EventArgs e)
        {
            if (grdMedicacion.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un registro.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == DialogResult.Yes)
            {
                // Capturar id desde la grilla 
                var ProductId = grdMedicacion.Selected.Rows[0].Cells["v_ProductId"].Value.ToString();

                // Buscar registro para remover
                var findResult = _tmpMedicationList.Find(p => p.v_ProductId == ProductId);
                // Borrado logico
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;

                var dataList = _tmpMedicationList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                grdMedicacion.DataSource = new MedicationList();
                grdMedicacion.DataSource = dataList;
                grdMedicacion.Refresh();
                lblRecordCountMedication.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
        }      

        private void btnAgregarProcedimiento_Click(object sender, EventArgs e)
        {
            if (cbProcedimientos.SelectedIndex == 0)
            {
                MessageBox.Show("Por favor seleccione un procedimiento para agregar.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_tmpProcedureList == null)
                _tmpProcedureList = new List<ProcedureByServiceList>();

            int procedureId = int.Parse(cbProcedimientos.SelectedValue.ToString());

            var procedure = _tmpProcedureList.Find(p => p.i_ProcedureId == procedureId);

            if (procedure == null)   // agregar con normalidad [insert]  a la bolsa  
            {
                // Agregar procedimiento a la Lista
                ProcedureByServiceList procedureByServiceList = new ProcedureByServiceList();

                procedureByServiceList.i_ItemId = _tmpProcedureList.Count + 1;
                procedureByServiceList.i_ProcedureId = procedureId;
                procedureByServiceList.v_ProcedureName = cbProcedimientos.Text;
                procedureByServiceList.v_ServiceId = _serviceId;

                procedureByServiceList.i_RecordStatus = (int)RecordStatus.Agregado;
                procedureByServiceList.i_RecordType = (int)RecordType.Temporal;

                _tmpProcedureList.Add(procedureByServiceList);
            }
            else    // el procedimiento ya esta agregado en la bolsa hay que actualizar su estado
            {
                if (procedure.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                {
                    if (procedure.i_RecordType == (int)RecordType.NoTemporal)   // El registro Tiene in ID de BD
                    {
                        procedure.i_ProcedureId = procedureId;
                        procedure.v_ProcedureName = cbProcedimientos.Text;
                        procedure.i_RecordStatus = (int)RecordStatus.Grabado;
                    }
                    else if (procedure.i_RecordType == (int)RecordType.Temporal)   // El registro tiene un ID temporal [GUID]
                    {
                        procedure.i_ProcedureId = procedureId;
                        procedure.v_ProcedureName = cbProcedimientos.Text;
                        procedure.i_RecordStatus = (int)RecordStatus.Agregado;
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione otro Procedimiento. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }

            var dataList = _tmpProcedureList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

            // Cargar grilla
            grdProcedimientos.DataSource = new ProcedureByServiceList();
            grdProcedimientos.DataSource = dataList;
            grdProcedimientos.Refresh();
            lblRecordCountProcedures.Text = string.Format("Se encontraron {0} registros.", dataList.Count());

        }

        private void btnRemoverProcedimiento_Click(object sender, EventArgs e)
        {
            if (grdProcedimientos.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un registro.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == DialogResult.Yes)
            {
                // Capturar id desde la grilla 
                var procedureId = int.Parse(grdProcedimientos.Selected.Rows[0].Cells["i_ProcedureId"].Value.ToString());

                // Buscar registro para remover
                var findResult = _tmpProcedureList.Find(p => p.i_ProcedureId == procedureId);
                // Borrado logico
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;

                var dataList = _tmpProcedureList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                grdProcedimientos.DataSource = new ProcedureByServiceList();
                grdProcedimientos.DataSource = dataList;
                grdProcedimientos.Refresh();
                lblRecordCountProcedures.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
        }

        private void MarcarFilaParaPintadoGrilla(UltraGrid grd, string key)
        {
            // Pinta fila seleccionada
            for (int i = 0; i < grd.Rows.Count; i++)
            {
                int? keyName = (int?)grd.Rows[i].Cells[key].Value;

                if (keyName == 1)
                {
                    grd.Rows[i].Appearance.BackColor = Color.LawnGreen;
                    grd.Rows[i].Appearance.BackColor2 = Color.LawnGreen;
                    //Y doy el efecto degradado vertical
                    grd.Rows[i].Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                }
                else
                {
                    grd.Rows[i].Appearance.BackColor = Color.FromArgb(192, 255, 255);
                    grd.Rows[i].Appearance.BackColor2 = Color.FromArgb(192, 255, 255);
                    //Y doy el efecto degradado vertical
                    grd.Rows[i].Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
                }

            }
        }    

        private void btnGuardarPlanTrabajo_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("¿Está seguro de grabar este registro?:", "CONFIRMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Result == DialogResult.Yes)
            {

                OperationResult objOperationResult = new OperationResult();

                var serviceDTO = new serviceDto();

                int? hazInterconsultation = null;

                if (rbSiInterconsulta.Checked)
                    hazInterconsultation = (int)SiNo.SI;
                if (rbNoInterconsulta.Checked)
                    hazInterconsultation = (int)SiNo.NO;

                int? hazRestriction = null;

                if (rbSiRestricciones.Checked)
                    hazRestriction = (int)SiNo.SI;
                if (rbNoRestricciones.Checked)
                    hazRestriction = (int)SiNo.NO;

                int? senToTracking = null;

                if (rbSiTracking.Checked)
                    senToTracking = (int)SiNo.SI;
                if (rbNoTracking.Checked)
                    senToTracking = (int)SiNo.NO;

                // Datos de Servicio
                serviceDTO.v_ServiceId = _serviceId;
                serviceDTO.i_DestinationMedicationId = int.Parse(cbDestino.SelectedValue.ToString());
                serviceDTO.i_TransportMedicationId = int.Parse(cbMedioTransporte.SelectedValue.ToString());
                serviceDTO.i_HazInterconsultationId = hazInterconsultation;
                serviceDTO.i_HasMedicalBreakId = Convert.ToInt32(chkDescansoMedico.Checked);
                serviceDTO.i_HasRestrictionId = hazRestriction;
                serviceDTO.d_MedicalBreakStartDate = chkDescansoMedico.Checked ? dtpFechaIniDescansoMedico.Value.Date : (DateTime?)null;
                serviceDTO.d_MedicalBreakEndDate = chkDescansoMedico.Checked ? dtpFechaFinDescansoMedico.Value.Date : (DateTime?)null;
             
                serviceDTO.v_GeneralRecomendations = txtRecomendacionesGenerales.Text;
                serviceDTO.d_NextAppointment = dtpProxCitaRef.Value.Date;
                serviceDTO.i_SendToTracking = senToTracking;
                serviceDTO.v_ExaAuxResult = txtResultadosExaAux.Text;

                // datos de cabecera del Servicio
            
                serviceDTO.i_IsNewControl = int.Parse(cbNuevoControl.SelectedValue.ToString());
                serviceDTO.i_MasterServiceId = _masterServiceId;

                _serviceBL.AddWorkPlan(ref objOperationResult,
                                            _tmpMedicationList,
                                            _tmpProcedureList,
                                            _auxiliaryExams,                                                                                   
                                            _interconsultations,
                                            _tmpRestrictionList,
                                            _medicalBreaks,
                                            serviceDTO,                                        
                                            Globals.ClientSession.GetAsList());           

                // Recargar todas las grillas
                InitializeData();
                //ConclusionesyTratamiento_LoadAllGrid();

                // Analizar el resultado de la operación
                if (objOperationResult.Success != 1)
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    #region Mensaje de información de guardado

                    MessageBox.Show("se guardó correctamente.", "CORRECTO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    #endregion
                }
            }
        }

        private void btnAgregarExamenAuxiliar_Click(object sender, EventArgs e)
        {
            var frm = new Operations.Popups.frmAddAuxiliaryExam();
            frm._mode = "New";
            frm._auxiliaryExams = _auxiliaryExams;
            frm._serviceId = _serviceId;
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            // Refrescar grilla
            // Actualizar variable
            if (frm._auxiliaryExams != null)
            {
                _auxiliaryExams = frm._auxiliaryExams;

                var dataList = _auxiliaryExams.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdExamenAuxiliar.DataSource = dataList;
                lblRecordCountExamenAuxiliar.Text = string.Format("Se encontraron {0} registros.", dataList.Count());

            }

        }

        private void frmAddMedicationMedicalConsult_btnAdd_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            if (btn.Tag == null)
                return;

            var medicationList = (List<MedicationList>)btn.Tag;
         
            // Refrescar grilla
            // Actualizar variable
           
            _tmpMedicationList = medicationList;

            var dataList = _tmpMedicationList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
            grdMedicacion.DataSource = new List<MedicationList>();
            grdMedicacion.DataSource = dataList;
            lblRecordCountMedication.Text = string.Format("Se encontraron {0} registros.", dataList.Count());                 
         
        }

        private void grdMedicacion_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnEditarMedicacion.Enabled = btnRemoverMedicacion.Enabled = (grdMedicacion.Selected.Rows.Count > 0);

            if (grdMedicacion.Selected.Rows.Count == 0)
                return;

            _productId = grdMedicacion.Selected.Rows[0].Cells["v_ProductId"].Value.ToString();
          
        }

        private void grdProcedimientos_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnRemoverProcedimiento.Enabled = (grdProcedimientos.Selected.Rows.Count > 0);
        }

        private void grdExamenAuxiliar_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnRemoverExamenAuxiliar.Enabled = (grdExamenAuxiliar.Selected.Rows.Count > 0);
            btnAgregarResultadoExaAux.Enabled = (grdExamenAuxiliar.Selected.Rows.Count > 0);
        }

        private void grdRestricciones_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnRemoverRestricciones.Enabled = (grdRestricciones.Selected.Rows.Count > 0);
        }

        private void btnAgregarRestriccion_Click(object sender, EventArgs e)
        {
            var frm = new frmMasterRecommendationRestricction("Restricciones", (int)Typifying.Restricciones, ModeOperation.Total);
            frm._restrictions = _tmpRestrictionList;
            frm._nameInvokerForm = "frmMedicalConsult";
            frm._serviceId = _serviceId;
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            if (frm._restrictions != null)
            {
                _tmpRestrictionList = frm._restrictions;

                var dataList = _tmpRestrictionList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                ultraGrid1.DataSource = dataList;
                // Cargar grilla
                //grdRestricciones.DataSource = new RestrictionList();
                grdRestricciones.DataSource = dataList;
                lblRecordCountRestricciones.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
        }

        private void btnRemoverRestricciones_Click(object sender, EventArgs e)
        {
            if (grdRestricciones.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un registro.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:", "CONFIRMA LA ELIMINACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Result == DialogResult.Yes)
            {
                // Capturar id desde la grilla 
                var restrictionId = grdRestricciones.Selected.Rows[0].Cells["v_MasterRestrictionId"].Value.ToString();

                // Buscar registro para remover
                var findResult = _tmpRestrictionList.Find(p => p.v_MasterRestrictionId == restrictionId);
                // Borrado logico
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;

                var dataList = _tmpRestrictionList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                grdRestricciones.DataSource = new RestrictionList();
                grdRestricciones.DataSource = dataList;            
                lblRecordCountRestricciones.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
        }

        private void btnRemoverExamenAuxiliar_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == DialogResult.Yes)
            {
                // Capturar id desde la grilla 
                var medicalExamId = grdExamenAuxiliar.Selected.Rows[0].Cells["v_MedicalExamId"].Value.ToString();

                // Buscar registro para remover
                var findResult = _auxiliaryExams.Find(p => p.v_ComponentId == medicalExamId);
                // Borrado logico
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;

                var dataList = _auxiliaryExams.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                grdExamenAuxiliar.DataSource = new MedicalExamList();
                grdExamenAuxiliar.DataSource = dataList;
                lblRecordCountExamenAuxiliar.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
        }

        private void btnAgregarInterConsulta_Click(object sender, EventArgs e)
        {
            var frm = new Operations.Popups.frmAddInterConsultation();
            frm._mode = "New";
            frm._serviceId = _serviceId;
            frm._interconsultations = _interconsultations;
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            // Refrescar grilla
            // Actualizar variable
            if (frm._interconsultations != null)
            {
                _interconsultations = frm._interconsultations;

                var dataList = _interconsultations.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdInterConsulta.DataSource = dataList;
                lblRecordCountInterConsulta.Text = string.Format("Se encontraron {0} registros.", dataList.Count());

            }
        }

        private void btnRemoverInterConsulta_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == DialogResult.Yes)
            {
                // Capturar id desde la grilla 
                var diagnosticRepositoryId = grdInterConsulta.Selected.Rows[0].Cells["v_DiagnosticRepositoryId"].Value.ToString();

                // Buscar registro para remover
                var findResult = _interconsultations.Find(p => p.v_DiagnosticRepositoryId == diagnosticRepositoryId);
                // Borrado logico
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;

                var dataList = _interconsultations.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                grdInterConsulta.DataSource = new MedicalExamList();
                grdInterConsulta.DataSource = dataList;
                lblRecordCountInterConsulta.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
        }

        private void btnAgregarDescansoMedico_Click(object sender, EventArgs e)
        {
            var frm = new Operations.Popups.frmAddMedicalBreak();
            frm._mode = "New";
            frm._serviceId = _serviceId;
            frm._medicalBreaks = _medicalBreaks;
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            // Refrescar grilla
            // Actualizar variable
            if (frm._medicalBreaks != null)
            {
                _medicalBreaks = frm._medicalBreaks;

                var dataList = _medicalBreaks.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdDescansoMedico.DataSource = dataList;
                lblRecordCountDescansoMedico.Text = string.Format("Se encontraron {0} registros.", dataList.Count());

            }
        }

        private void btnRemoverDescansoMedico_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == DialogResult.Yes)
            {
                // Capturar id desde la grilla 
                var diagnosticRepositoryId = grdInterConsulta.Selected.Rows[0].Cells["v_DiagnosticRepositoryId"].Value.ToString();

                // Buscar registro para remover
                var findResult = _interconsultations.Find(p => p.v_DiagnosticRepositoryId == diagnosticRepositoryId);
                // Borrado logico
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;

                var dataList = _medicalBreaks.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                grdDescansoMedico.DataSource = new MedicalExamList();
                grdDescansoMedico.DataSource = dataList;
                lblRecordCountDescansoMedico.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
        }

        private void grdDescansoMedico_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnRemoverDescansoMedico.Enabled = (grdDescansoMedico.Selected.Rows.Count > 0);
        }

        private void grdInterConsulta_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnRemoverInterConsulta.Enabled = (grdInterConsulta.Selected.Rows.Count > 0);
        }

        private void rbSiRestricciones_CheckedChanged(object sender, EventArgs e)
        {
            if (!(rbSiRestricciones.Checked))
                return;

            grdRestricciones.Enabled = true;
            btnAgregarRestriccion.Enabled = true;

            // Reset Descanso Medico
            chkDescansoMedico.Checked = false;
            chkDescansoMedico.Enabled = false;

            dtpFechaIniDescansoMedico.Enabled = 
                dtpFechaFinDescansoMedico.Enabled = 
                btnAgregarDescansoMedico.Enabled = 
                grdDescansoMedico.Enabled = false;

            // Limpiar grilla
            grdDescansoMedico.DataSource = new DiagnosticRepositoryList();
            lblRecordCountDescansoMedico.Text = string.Format("Se encontraron {0} registros.", 0);
            // Limpiar variable global
            //_medicalBreaks = null;

            if (_tmpRestrictionList != null && _tmpRestrictionList.Count != 0)
            {
                var dataList = _tmpRestrictionList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                // Cargar grilla
                //grdRestricciones.DataSource = new RestrictionList();
                grdRestricciones.DataSource = dataList;
                grdRestricciones.DataBind();

                //ultraGrid1.Refresh();             
                ultraGrid1.DataSource = dataList;
                //ultraGrid1.Rows.Refresh(Infragistics.Win.UltraWinGrid.RefreshRow.ReloadData);
                //ultraGrid1.Rows.Refresh(Infragistics.Win.UltraWinGrid.RefreshRow.RefreshDisplay);
                
                //grdRestricciones.Refresh();
                lblRecordCountRestricciones.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }

        }

        private void rbNoRestricciones_CheckedChanged(object sender, EventArgs e)
        {
            if (!(rbNoRestricciones.Checked))
                return;

            grdRestricciones.Enabled = false;
            btnAgregarRestriccion.Enabled = false;
                    
            // Limpiar grilla
            RestrictionList newd = new RestrictionList();
            grdRestricciones.DataSource = newd;       
          
            lblRecordCountRestricciones.Text = string.Format("Se encontraron {0} registros.", 0);

            //ultraGrid1.DisplayLayout.Bands[0].Reset();
            ultraGrid1.DataSource = newd;
            //ultraGrid1.ResetDisplayLayout();         
           
            
           // ultraGrid1.Layouts.Clear();
          
            // Marcar como eleiminado
            //foreach (var item in _tmpRestrictionList)
            //{
            //    item.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
            //}         

            chkDescansoMedico.Enabled = true;
        }

        private void chkDescansoMedico_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaIniDescansoMedico.Enabled = dtpFechaFinDescansoMedico.Enabled = btnAgregarDescansoMedico.Enabled = grdDescansoMedico.Enabled = (chkDescansoMedico.Checked);
        }

        private void rbSiInterconsulta_CheckedChanged(object sender, EventArgs e)
        {
            grdInterConsulta.Enabled = true;
            btnAgregarInterConsulta.Enabled = true;
           
        }

        private void rbNoInterconsulta_CheckedChanged(object sender, EventArgs e)
        {
            grdInterConsulta.Enabled = false;
            btnAgregarInterConsulta.Enabled = false;
          

            // Limpiar grilla
            grdInterConsulta.DataSource = new DiagnosticRepositoryList();
            lblRecordCountInterConsulta.Text = string.Format("Se encontraron {0} registros.", 0);
            // Limpiar variable global
            _interconsultations = null;
        }

        #endregion

        #region Custom Events

        private void GeneratedAutoDX(string analyzingValue, string componentFieldsId, Control senderCtrl, KeyTagControl tagCtrl)
        {
            // Retorna el DX (automático) generado, luego de una serie de evaluaciones.
            var diagnosticRepository = SearchDxSugeridoOfSystem(analyzingValue, componentFieldsId);

            DiagnosticRepositoryList findControlResult = null;

            if (_tmpExamDiagnosticComponentList != null)
            {
                // Buscar control que haya generado algun DX automático
                findControlResult = _tmpExamDiagnosticComponentList.Find(p => p.v_ComponentFieldsId == componentFieldsId && p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
            }
           
            // Remover DX (automático) encontrado.
            if (findControlResult != null)
            {
                if (findControlResult.i_RecordType == (int)RecordType.Temporal)
                    _tmpExamDiagnosticComponentList.Remove(findControlResult);
                else
                    findControlResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
            }

            // Si se generó un DX (automático).
            if (diagnosticRepository != null)
            {              
                // Setear v_ComponentFieldValuesId en mi variable de información TAG
                tagCtrl.v_ComponentFieldValuesId = diagnosticRepository.v_ComponentFieldValuesId;

                // Pintar de rojo el fondo del control que generó el DX (automático) 
                // en caso hubiera una alteracion si es normal NO se pinta.               
                senderCtrl.BackColor = Color.Red;   // DX Alterado              

                if (_tmpExamDiagnosticComponentList != null)
                {
                    // Se agrega el DX obtenido a la lista de DX general.
                    _tmpExamDiagnosticComponentList.Add(diagnosticRepository);
                }
                else
                {
                    _tmpExamDiagnosticComponentList = new List<DiagnosticRepositoryList>();
                    _tmpExamDiagnosticComponentList.Add(diagnosticRepository);
                }
            }
            else        // No
            {
                senderCtrl.BackColor = Color.White;
            }

            if (_tmpExamDiagnosticComponentList != null)
            {
                // Filtar para Mostrar en la grilla solo registros que no están eliminados
                var dataList = _tmpExamDiagnosticComponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                // Refrescar grilla                        
                //grdDiagnosticoPorExamenComponente.DataSource = dataList;
                //lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
        }

        private void txt_ValueChanged(object sender, EventArgs e)
        {
            if (flagValueChange)
            {
                // Capturar el control invocador
                Control senderCtrl = (Control)sender;
                // Obtener información contenida en la propiedad Tag del control invocante
                var tagCtrl = (KeyTagControl)senderCtrl.Tag;
                string componentFieldsId = tagCtrl.v_ComponentFieldsId;
                string analyzingValue = GetValueControl(tagCtrl.i_ControlId, senderCtrl);
                int isSourceField = tagCtrl.i_IsSourceFieldToCalculate;
                Dictionary<string, object> Params = null;
                List<double> evalExpResultList = new List<double>();

                ////MessageBox.Show(senderCtrl.Text);
                if (isSourceField == (int)SiNo.SI)
                {
                    #region Nueva logica de calculo de formula soporta n parametros

                    // Recorrer las formulas en las cuales el campo esta referenciado
                    foreach (var formu in tagCtrl.Formula)
                    {
                        // Obtener Campos fuente participantes en el calculo
                        var sourceFields = Common.Utils.GetTextFromExpressionInCorchete(formu.v_Formula);
                        Params = new Dictionary<string, object>();

                        foreach (string sf in sourceFields)
                        {                           
                            // Buscar controles fuentes
                            var findCtrlResult = FindDynamicControl(sf);
                            var length = findCtrlResult.Length;
                            // La busqueda si tuvo exito
                            if (length != 0)
                            {
                                // Obtener información del control encontrado 
                                var tagSourceField = (KeyTagControl)findCtrlResult[0].Tag;
                                // Obtener el tipo de dato al cual se va castear un control encontrado
                                string dtc = GetDataTypeControl(tagSourceField.i_ControlId);
                                // Obtener value del control encontrado
                                var value = GetValueControl(tagSourceField.i_ControlId, findCtrlResult[0]);

                                if (dtc == "int")
                                {
                                    //var ival = int.Parse(value);
                                    Params[sf] = int.Parse(value);
                                }
                                else if (dtc == "double")
                                {
                                    Params[sf] = double.Parse(value);
                                }
                            }
                            else
                            {
                                if (sf.ToUpper() == "EDAD")
                                {
                                    Params[sf] = _age;
                                }
                                else if (sf.ToUpper() == "GENERO_2")
                                {
                                    Params[sf] = _sexType == Gender.FEMENINO ? 0 : 1;
                                }
                                else if (sf.ToUpper() == "GENERO_1")
                                {
                                    Params[sf] = _sexType == Gender.MASCULINO ? 0 : 1;
                                }
                            }

                        } // fin foreach sourceFields

                        bool isFound = false;

                        // Buscar algun cero
                        foreach (var item in Params)
                        {
                            if (item.Value.ToString() == "0" && 
                                item.Key != "EDAD" && 
                                item.Key != "GENERO_1" && 
                                item.Key != "GENERO_2")
                            {
                                isFound = true;
                                break;
                            }
                        }

                        if (!isFound)
                        {
                            var evalExpResult = Common.Utils.EvaluateExpression(formu.v_Formula, Params);
                            evalExpResultList.Add(evalExpResult);
                        }

                    } // fin foreach Formula

                    // Mostrar el resultado en el control indicado
                    if (evalExpResultList.Count != 0)
                    {
                        for (int i = 0; i < tagCtrl.TargetFieldOfCalculateId.Count; i++)
                        {
                            var targetFieldOfCalculate1 = FindDynamicControl(tagCtrl.TargetFieldOfCalculateId[i].v_TargetFieldOfCalculateId);

                            for (int j = 0; j < evalExpResultList.Count; j++)
                            {                           
                                if (i == j)
                                {
                                    targetFieldOfCalculate1[0].Text = evalExpResultList[j].ToString();
                                }                               
                            }
                        }
                    } 

                    #endregion

                }

                GeneratedAutoDX(analyzingValue, componentFieldsId, senderCtrl, tagCtrl);
            
            }
         
        }    

        private void txt_Leave(object sender, System.EventArgs e)
        {
            flagValueChange = true;

            // Capturar el control invocador
            Control senderCtrl = (Control)sender;
            // Obtener información contenida en la propiedad Tag del control invocante
            var tagCtrl = (KeyTagControl)senderCtrl.Tag;            
            string componentFieldsId = tagCtrl.v_ComponentFieldsId;
            string analyzingValue = GetValueControl(tagCtrl.i_ControlId, senderCtrl);
            int isSourceField = tagCtrl.i_IsSourceFieldToCalculate;

            Dictionary<string, object> Params = null;
            List<double> evalExpResultList = new List<double>();

            #region logica de modificacion de flag [_isChangeValue]

            if (!_isChangeValue)
            {
                if (_oldValue != analyzingValue)
                {
                    _isChangeValue = true;
                }
            }

            #endregion

            if (isSourceField == (int)SiNo.SI)
            {
               
                #region Nueva logica de calculo de formula soporta n parametros

                // Recorrer las formulas en las cuales el campo esta referenciado
                foreach (var formu in tagCtrl.Formula)
                {
                    // Obtener Campos fuente participantes en el calculo
                    var sourceFields = Common.Utils.GetTextFromExpressionInCorchete(formu.v_Formula);
                    Params = new Dictionary<string, object>();

                    foreach (string sf in sourceFields)
                    {
                        // Buscar controles fuentes
                        var findCtrlResult = FindDynamicControl(sf);
                        var length = findCtrlResult.Length;
                        // La busqueda si tuvo exito
                        if (length != 0)
                        {
                            // Obtener información del control encontrado 
                            var tagSourceField = (KeyTagControl)findCtrlResult[0].Tag;
                            // Obtener el tipo de dato al cual se va castear un control encontrado
                            string dtc = GetDataTypeControl(tagSourceField.i_ControlId);
                            // Obtener value del control encontrado
                            var value = GetValueControl(tagSourceField.i_ControlId, findCtrlResult[0]);

                            if (dtc == "int")
                            {
                                //var ival = int.Parse(value);
                                Params[sf] = int.Parse(value);
                            }
                            else if (dtc == "double")
                            {
                                Params[sf] = double.Parse(value);
                            }
                        }
                        else
                        {
                            if (sf.ToUpper() == "EDAD")
                            {
                                Params[sf] = _age;
                            }
                            else if (sf.ToUpper() == "GENERO_2")
                            {
                                Params[sf] = _sexType == Gender.FEMENINO ? 0 : 1;
                            }
                            else if (sf.ToUpper() == "GENERO_1")
                            {
                                Params[sf] = _sexType == Gender.MASCULINO ? 0 : 1;
                            }
                        }

                    } // fin foreach sourceFields

                    bool isFound = false;

                    // Buscar algun cero
                    foreach (var item in Params)
                    {
                        if (item.Value.ToString() == "0" &&
                            item.Key != "EDAD" &&
                            item.Key != "GENERO_1" &&
                            item.Key != "GENERO_2")
                        {
                            isFound = true;
                            break;
                        }
                    }

                    if (!isFound)
                    {
                        var evalExpResult = Common.Utils.EvaluateExpression(formu.v_Formula, Params);
                        evalExpResultList.Add(evalExpResult);
                        var targetFieldOfCalculate1 = FindDynamicControl(formu.v_TargetFieldOfCalculateId);
                        targetFieldOfCalculate1[0].Text = evalExpResult.ToString();                       
                    }

                } // fin foreach Formula
                  
                #endregion
            }
            else
            {
                GeneratedAutoDX(analyzingValue, componentFieldsId, senderCtrl, tagCtrl);           
            }    
       
        }

        private void Capture_Value(object sender, EventArgs e)
        {
            Control senderCtrl = (Control)sender;
            // Obtener información contenida en la propiedad Tag del control invocante
            var tagCtrl = (KeyTagControl)senderCtrl.Tag;
            // Capturar valor inicial
            _oldValue = GetValueControl(tagCtrl.i_ControlId, senderCtrl);          
          
        }

        private void cb_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Flag
            if (_cancelEventselectedIndexChange)        
                return;          

            var tagCtrl = (KeyTagControl)((ComboBox)sender).Tag;   
            var value1 = int.Parse(((ComboBox)sender).SelectedValue.ToString());

            if (value1 == (int)NormalAlterado.Alterado)
            {
                var frm = new Operations.Popups.frmRegisterFinding(tagCtrl.v_ComponentName, "", tagCtrl.v_TextLabel);
                
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.Cancel)
                    return;

                TextBox field = null;

                if (_componentId == Constants.EXAMEN_FISICO_ID)
                {
                    field = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_HALLAZGOS_ID)[0];                             
                }

                if (field != null)
                {
                    StringBuilder sb = new StringBuilder();
                    if (field.Text == string.Empty)
                    {
                        sb.Append(frm.FindingText);
                    }
                    else
                    {
                        sb.Append(field.Text);
                        sb.Append("\r\n");
                        sb.Append(frm.FindingText);
                    }
                    
                    field.Text = sb.ToString();                   
                    
                }    

               
                
            }

        }
       
        #endregion 

        #region Antecedentes

        private void GetAntecedentConsolidateForService(string personId)
        {
            OperationResult objOperationResult = new OperationResult();
            var antecedent = _serviceBL.GetAntecedentConsolidateForService(ref objOperationResult, personId);
          
            // Analizar el resultado de la operación
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (antecedent.Count > 0)
                grdAntecedentes.DataSource = antecedent;
        }

        private void GetServicesConsolidateForService(string personId)
        {
            OperationResult objOperationResult = new OperationResult();
            var services = _serviceBL.GetServicesConsolidateForService(ref objOperationResult, personId, _serviceId);

            grdServiciosAnteriores.DataSource = services;

            // Analizar el resultado de la operación
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuGridAntecedent_Click(object sender, EventArgs e)
        {
            ViewEditAntecedent();         
        }

        private void ViewEditAntecedent()
        {
            frmHistory frm = new frmHistory(_personId);
            frm.ShowDialog();
            // refresca grilla de antecedentes
            GetAntecedentConsolidateForService(_personId);
        }

        private void mnuVerServicio_Click(object sender, EventArgs e)
        {
            var frm = new Operations.frmEso(_serviceIdByWiewServiceHistory, null, "View", (int)MasterService.Eso);
            frm.ShowDialog();
        }

        private void grdServiciosAnteriores_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    grdServiciosAnteriores.Rows[row.Index].Selected = true;                   
                    _serviceIdByWiewServiceHistory = grdServiciosAnteriores.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
                    cmVerServicioAnterior.Items["mnuVerServicio"].Enabled = true;
                }
                else
                {
                    cmVerServicioAnterior.Items["mnuVerServicio"].Enabled = false;

                }

            }
        }

        private void btnVerEditarAntecedentes_Click(object sender, EventArgs e)
        {
            ViewEditAntecedent();
        }
        
        #endregion                        

        private void tcSubMain_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            SaveExamWherePendingChange(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (!(rbNoRestricciones.Checked))
            //    return;

            grdRestricciones.Enabled = false;
            btnAgregarRestriccion.Enabled = false;

            // Limpiar grilla
            RestrictionList newd = new RestrictionList();
            grdRestricciones.DataSource = newd;

            lblRecordCountRestricciones.Text = string.Format("Se encontraron {0} registros.", 0);

            ultraGrid1.Refresh();
            ultraGrid1.DataSource = newd;
            ultraGrid1.DataBind();
            // Marcar como eleiminado
            //foreach (var item in _tmpRestrictionList)
            //{
            //    item.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
            //}         

            chkDescansoMedico.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (!(rbSiRestricciones.Checked))
            //    return;

            grdRestricciones.Enabled = true;
            btnAgregarRestriccion.Enabled = true;

            // Reset Descanso Medico
            chkDescansoMedico.Checked = false;
            chkDescansoMedico.Enabled = false;

            dtpFechaIniDescansoMedico.Enabled =
                dtpFechaFinDescansoMedico.Enabled =
                btnAgregarDescansoMedico.Enabled =
                grdDescansoMedico.Enabled = false;

            // Limpiar grilla
            grdDescansoMedico.DataSource = new DiagnosticRepositoryList();
            lblRecordCountDescansoMedico.Text = string.Format("Se encontraron {0} registros.", 0);
            // Limpiar variable global
            //_medicalBreaks = null;

            if (_tmpRestrictionList != null && _tmpRestrictionList.Count != 0)
            {
                var dataList = _tmpRestrictionList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                // Cargar grilla
                //grdRestricciones.DataSource = new RestrictionList();
                grdRestricciones.DataSource = dataList;
                grdRestricciones.DataBind();

                ultraGrid1.Refresh();
                ultraGrid1.DataSource = dataList;
                ultraGrid1.DataBind();

                //grdRestricciones.Refresh();
                lblRecordCountRestricciones.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
        }

        private void btnAgregarResultadoExaAux_Click(object sender, EventArgs e)
        {

            string Examen = grdExamenAuxiliar.Selected.Rows[0].Cells["v_ComponentName"].Value.ToString();
            Popups.frmAtxResultadoExamenAuxiliar frm = new Popups.frmAtxResultadoExamenAuxiliar(Examen);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            //txtResultadosExaAux.Text = frm.FindingText.Substring(frm.FindingText.IndexOf(':') + 2);

            if (txtResultadosExaAux != null)
            {
      

                StringBuilder sb = new StringBuilder();

                if (txtResultadosExaAux.Text == string.Empty)
                {
                    sb.Append(frm.FindingText);
                }
                else
                {
                    sb.Append(txtResultadosExaAux.Text);
                    sb.Append("\r\n");
                    sb.Append(frm.FindingText);
                }

                txtResultadosExaAux.Text = sb.ToString();
            }

        }    
       
    }

}
   