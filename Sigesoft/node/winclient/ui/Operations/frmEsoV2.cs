using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinMaskedEdit;
using Infragistics.Win.UltraWinTabControl;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.UI.Operations.Popups;
using Sigesoft.Node.WinClient.UI.UserControls;
using System.ComponentModel;
using System.Text;
using Sigesoft.Node.WinClient.UI.Reports;
using System.Data;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using Sigesoft.Node.Contasol.Integration;
using System.Transactions;
using NetPdf;


namespace Sigesoft.Node.WinClient.UI.Operations
{
    public partial class FrmEsoV2 : Form
    {
        #region Instancias
        public int _profesionId;
        public class RunWorkerAsyncPackage
        {
            public Infragistics.Win.UltraWinTabControl.UltraTabPageControl SelectedTab { get; set; }
            public List<DiagnosticRepositoryList> ExamDiagnosticComponentList { get; set; }
            public servicecomponentDto ServiceComponent { get; set; }
            public int? i_SystemUserSuplantadorId { get; set; }
        }
        public class ValidacionAMC
        {
            public string v_ServiceComponentFieldValuesId { get; set; }
            public string v_Value1 { get; set; }
            public string v_ComponentFieldsId { get; set; }
        }
        public class DatosModificados
        {
            public string v_ComponentFieldsId { get; set; }
        }
        private bool _cancelEventSelectedIndexChange;
        List<ValidacionAMC> _oListValidacionAMC = new List<ValidacionAMC>();
        //private readonly ServiceBL _serviceBl = new ServiceBL();
        private List<string> _filesNameToMerge = null;
        private readonly string _serviceId;
        private readonly string _componentIdDefault;
        private readonly string _action;
        private Dictionary<string, UltraValidator> _dicUltraValidators;
        private OperationResult _objOperationResult = new OperationResult();
        private string _personId;
        private static List<GroupParameter> _cboEso;
        private bool _isDisabledButtonsExamDx;
        private readonly int _nodeId;
        private readonly int _roleId;
        private readonly int _userId;
        private string _componentId;
        private string _serviceComponentId;
        TypeESO _esoTypeId;
        private int _age;
        private List<KeyValueDTO> _formActions;
        private List<DiagnosticRepositoryList> _tmpExamDiagnosticComponentList;
        private DiagnosticRepositoryList _tmpTotalDiagnostic;
        private List<RestrictionList> _tmpRestrictionList;
        private List<RecomendationList> _tmpRecomendationList;
        private string _componentIdConclusionesDX;
        private Keys _currentDownKey = Keys.None;
        private bool _removerTotalDiagnostico;
        private bool _removerRecomendacionAnalisisDx;
        private bool _removerRestriccionAnalisis;

        private bool _removerRecomendaciones_Conclusiones;
        private bool _removerRestricciones_ConclusionesTratamiento;

        private List<DiagnosticRepositoryList> _tmpTotalConclusionesDxByServiceIdList;
        private readonly List<DiagnosticRepositoryList> _tmpTotalDiagnosticList = null;
        private List<DiagnosticRepositoryList> _tmpTotalDiagnosticByServiceIdList;
        public string _diagnosticId;
        private int? _rowIndexConclucionesDX;
        private List<RecomendationList> _tmpRecomendationConclusionesList;
        private List<RestrictionList> _tmpRestrictionListConclusiones = null;
        private List<ComponentList> _tmpServiceComponentsForBuildMenuList = null;

        private List<ServiceComponentFieldsList> _serviceComponentFieldsList = null;
        private List<ServiceComponentFieldValuesList> _serviceComponentFieldValuesList = null;
        private List<ServiceComponentFieldValuesList> _tmpListValuesOdontograma = null;

        private bool _isChangeValue = false;
        private int? _sexTypeId;
        private Gender _sexType;
        private bool flagValueChange = false;
        private bool _chkApprovedEnabled;
        private string _oldValue;

        private string _serviceIdByWiewServiceHistory;
        private int _tipo;
        private string _ProtocolId;
        serviceDto idPerson = new serviceDto();
        string PacientId;
        private string _examName;
        private ServiceComponentList _serviceComponentsInfo = null;
        private string _Dni;
        string _personName;
        int GrupoEtario;
        private int _AMCGenero;
        private int _masterServiceId;

        string adolId = string.Empty;
        private adolescenteDto objAdolDto = null;

        string adulId = string.Empty;
        private adultoDto objAdultoDto = null;

        string adulmayId = string.Empty;
        private adultomayorDto objAdultoMAyorDto = null;

        string ninioId = string.Empty;
        private ninioDto objNinioDto = null;
        private DateTime? _FechaServico;
        #endregion
        private PacientBL _pacientBL = new PacientBL();
        public int? _EstadoComponente = null;
        private ServiceBL _serviceBL = new ServiceBL();
        private string _customerOrganizationName;
        private ServiceList _datosPersona;
        private AtencionesIntegralesBL _objAtencionesIntegralesBl = new AtencionesIntegralesBL();
        byte[] _personImage;
        private string _v_CustomerOrganizationId;

        public FrmEsoV2(string serviceId, string componentIdDefault, string action, int roleId, int nodeId, int userId, int tipo)
        {
            _serviceId = serviceId;
            _componentIdDefault = componentIdDefault;
            _action = action;
            _roleId = roleId;
            _nodeId = nodeId;
            _userId = userId;
            _tipo = tipo;
            InitializeComponent();
        }
        private void FrmEsoV2_Load(object sender, EventArgs e)
        {
            InitializeForm();
            ViewMode(_action);
            _profesionId = int.Parse(Globals.ClientSession.i_ProfesionId.ToString());
            OperationResult objOperationResult = new OperationResult();
            ServiceList personData = new ServiceBL().GetServicePersonData(ref objOperationResult, _serviceId);

            if (personData.v_CustomerOrganizationId == "N009-OO000000587" || personData.v_EmployerOrganizationId == "N009-OO000000587" || personData.v_WorkingOrganizationId == "N009-OO000000587")
            {
                checkFirmaYanacocha.Visible = true;
                checkFirmaYanacocha.Enabled = true;
            }
            else
            {
                checkFirmaYanacocha.Visible = false;
                checkFirmaYanacocha.Enabled = false;
            }
            splitContainer2.SplitterDistance = splitContainer2.Height - 200;
        }

        private void InitializeForm()
        {
            InitializeData();
            InitializeStaticControls();
            LoadData();
        }

        private void LoadTabExamenes(ServiceData dataService)
        {
           _cboEso = GetListCboEso();
            SumaryWorker(dataService);
            CreateMissingExamens();
        }

        private static List<GroupParameter> GetListCboEso()
        {
            return BLL.Utils.GetSystemParameterForComboFormEso();
        }

        private void CreateMissingExamens()
        {
            try
            {
                using (new LoadingClass.PleaseWait(this.Location, "Cargando..."))
                {
                var listExamenes = new ServiceBL().ListMissingExamenesNames(ref _objOperationResult, _serviceId, _nodeId, _roleId).ToList();
                foreach (var examen in listExamenes)
                {
                    var componentsId = new ServiceBL().ConcatenateComponents(_serviceId, examen.v_ComponentId);
                    AsyncCreateNextExamen(componentsId, examen.v_CategoryName);
                }
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SumaryWorker(ServiceData dataService)
        {
            lblTrabajador.Text = dataService.Trabajador;
            lblProtocolName.Text = dataService.ProtocolName;
            _esoTypeId = (TypeESO)dataService.EsoTypeId;
            _age = Common.Utils.GetAge(dataService.FechaNacimiento.Value);
        }

        private static List<rolenodecomponentprofileDto> UserPermission()
        {
            var readingPermission = new ServiceBL().GetRoleNodeComponentProfileByRoleNodeId_(int.Parse(Globals.ClientSession.i_CurrentExecutionNodeId.ToString()), int.Parse(Globals.ClientSession.i_RoleId.ToString())).FindAll(p => p.i_Read == 1);
            return readingPermission;
        }

        private void AsyncCreateNextExamen(string componentId, string threadName)
        {
            //ThreadPool.QueueUserWorkItem(CallBack,componentId);
            var t = new Thread(() => { DoWorkGetExamen(componentId); }) { Name = threadName };
            t.Priority = t.Name == _componentIdDefault ? ThreadPriority.Highest : ThreadPriority.Lowest;
            t.Start();
        }

        private void CallBack(object state)
        {
            var componentId = (string)state; 
            var nextExamen = new ServiceBL().ExamenByDefaultOrAssigned(ref _objOperationResult, _serviceId, componentId)[0];
            var serviceComponentsInfo = new ServiceBL().GetServiceComponentsInfo(ref _objOperationResult, nextExamen.v_ServiceComponentId, _serviceId);
            CreateExamen(nextExamen, serviceComponentsInfo);
        }

        public void DoWorkGetExamen(string componentId)
        {
            if (_tmpServiceComponentsForBuildMenuList == null)
            {
                _tmpServiceComponentsForBuildMenuList = new List<ComponentList>();
            }
            var nextExamen = new ServiceBL().ExamenByDefaultOrAssigned(ref _objOperationResult, _serviceId, componentId)[0];
            _tmpServiceComponentsForBuildMenuList.Add(nextExamen);
            var  serviceComponentsInfo  =  new ServiceBL().GetServiceComponentsInfo(ref _objOperationResult, nextExamen.v_ServiceComponentId, _serviceId);
            CreateExamen(nextExamen, serviceComponentsInfo);
        }

        private void CreateExamen(ComponentList component, ServiceComponentList serviceComponentsInfo)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ComponentList, ServiceComponentList>(CreateExamen), component, serviceComponentsInfo);
            }
            else
            {
                var row = 1;
                const int column = 1;
                var fieldsByGroupBoxCount = 1;

                var tab = CrearTab(component);
                var tlpParent = CreateTableLayoutPanelParent(tab);

                var uv = CreateValidators(component);
                foreach (var groupComponentName in component.GroupedComponentsName)
                {
                    var gbComponent = CreateGroupBoxComponent(groupComponentName);
                    row++;
                    var tlpChildren = CreateTableLayoutPanelChildren(groupComponentName);
                    var groupBoxes = GetGroupBoxes(component, groupComponentName.v_ComponentId);
                    foreach (var groupbox in groupBoxes)
                    {
                        var groupBox = CreateGroupBoxForGroup(groupbox, serviceComponentsInfo);
                        fieldsByGroupBoxCount++;
                        var tableLayoutPanel = CreateTableLayoutForControls(groupbox, component);
                        var fieldsByGroupBox = GetFieldsEachGroups(component, groupbox, groupComponentName);
                        fieldsByGroupBox.Aggregate(1, (current, field) => CreateCampoComponente(component, field, groupbox.i_Column, current, tableLayoutPanel, uv));
                        groupBox.Controls.Add(tableLayoutPanel);
                        tlpChildren.Controls.Add(groupBox, column, fieldsByGroupBoxCount);
                    }
                    gbComponent.Controls.Add(tlpChildren);
                    tlpParent.Controls.Add(gbComponent, column, row);
                }
                tlpParent.AutoScroll = true;
                tlpParent.Dock = DockStyle.Fill;
                tlpParent.BackColor = Color.Gray;
                _cancelEventSelectedIndexChange = true;
                if (component.i_ServiceComponentStatusId == (int) ServiceComponentStatus.Iniciado ||
                    component.i_ServiceComponentStatusId == (int) ServiceComponentStatus.PorIniciar)
                    SetDefaultValueAfterBuildMenu(component);
                else
                {
                    SetDefaultValueAfterBuildMenu(component);
                    SearchControlAndSetValue(tab.TabPage, serviceComponentsInfo);
                }
                _cancelEventSelectedIndexChange = false;
                
            }
        }
        
        private void SearchControlAndSetValue(Control tlpParent, ServiceComponentList serviceComponentsInfo)
        {
            
                KeyTagControl keyTagControl = null;
                var breakHazChildrenUc = false;
                ValidacionAMC oValidacionAMC = null;
                var x = serviceComponentsInfo.ServiceComponentFields;
                var y = x.Select(p => p.ServiceComponentFieldValues).ToList();

                //var value = y.Find(p => p[0].v_ComponentFieldId = "idcomponent");

                _oListValidacionAMC = new List<ValidacionAMC>();
                foreach (var item in y)
                {
                    oValidacionAMC = new ValidacionAMC();
                    oValidacionAMC.v_ServiceComponentFieldValuesId = item[0].v_ServiceComponentFieldValuesId;
                    oValidacionAMC.v_Value1 = item[0].v_Value1;
                    oValidacionAMC.v_ComponentFieldsId = item[0].v_ComponentFieldId;
                    _oListValidacionAMC.Add(oValidacionAMC);
                }
                foreach (Control ctrl in tlpParent.Controls)
                {
                    if (ctrl.Tag != null)
                    {
                        var t = ctrl.Tag.GetType();
                        if (t == typeof(KeyTagControl))
                        {
                            // Capturar objeto tag
                            keyTagControl = (KeyTagControl)ctrl.Tag;

                            List<ServiceComponentFieldValuesList> dataSourceUserControls;
                            switch (keyTagControl.i_ControlId)
                            {
                                case (int)ControlType.UcOdontograma:
                                    
                                      #region Setear valores en Odontograma
                                        dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                        dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("ODO"));
                                        ((UserControls.ucOdontograma)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                        ((UserControls.ucOdontograma)ctrl).DataSource = dataSourceUserControls;
                                        breakHazChildrenUc = true;

                                #endregion
                                    break;
                                case (int)ControlType.UcAudiometria:
                                  #region Setear valores en udiometria

                                    dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                    dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("AUD"));
                                    ((UserControls.ucAudiometria)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                    ((UserControls.ucAudiometria)ctrl).DataSource = dataSourceUserControls;
                                    breakHazChildrenUc = true;

                                #endregion
                                    break;
                                case (int)ControlType.UcSomnolencia:
                                    #region Setear valores en udiometria

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("SOM"));
                                ((UserControls.ucSomnolencia)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucSomnolencia)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                    break;
                                case (int)ControlType.UcAcumetria:
                                    #region Setear valores en udiometria

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("ACU"));
                                ((UserControls.ucAcumetria)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucAcumetria)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                    break;
                                case (int)ControlType.UcFototipo:
                               #region Setear valores en FotoTipo

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("FOT"));
                                var multimediaFileId = dataSourceUserControls.Find(xp => xp.v_ComponentFieldId == Constants.txt_MULTIMEDIA_FILE_FOTO_TIPO).v_Value1;
                                ServiceBL oServiceBl = new ServiceBL();
                                var a = oServiceBl.ObtenerImageMultimedia(multimediaFileId);
                                ((UserControls.ucFotoTipo)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                dataSourceUserControls.Add(new ServiceComponentFieldValuesList(){fotoTipo = a});
                                ((UserControls.ucFotoTipo)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                    break;
                                case (int)ControlType.UcSintomaticoRespi:
                                 #region Setear valores en udiometria

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("RES"));
                                ((UserControls.ucSintomaticoResp)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucSintomaticoResp)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                    break;
                                case (int)ControlType.UcRxLumboSacra:
                                   #region Setear valores en udiometria

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("RXL"));
                                ((UserControls.ucRXLumboSacra)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucRXLumboSacra)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                    break;
                                case (int)ControlType.UcOjoSeco:
                                     #region Setear valores en udiometria

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("OJS"));
                                ((UserControls.ucOjoSeco)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucOjoSeco)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                    break;
                                case (int)ControlType.UcOtoscopia:
                                     #region Setear valores en udiometria

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("OTO"));
                                ((UserControls.ucOtoscopia)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucOtoscopia)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                    break;
                                case (int)ControlType.UcEvaluacionErgonomica:
                                     #region Setear valores en udiometria

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("EVA"));
                                ((UserControls.ucEvaluacionErgonomica)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucEvaluacionErgonomica)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                    break;
                                case (int)ControlType.UcOsteoMuscular:
                                 #region Setear valores en udiometria

                                dataSourceUserControls = serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("OTS"));
                                ((UserControls.ucOsteoMuscular)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucOsteoMuscular)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUc = true;

                                #endregion
                                    break;
                               
                                default:
                                    foreach (var item in serviceComponentsInfo.ServiceComponentFields)
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
                                    break;
                            }
                        }
                    }

                    if (!ctrl.HasChildren) continue;
                    if (!breakHazChildrenUc && keyTagControl == null)
                    {
                        SearchControlAndSetValue(ctrl, serviceComponentsInfo);
                    }
                }
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
                case ControlType.Radiobutton:
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
                case ControlType.UcSomnolencia:
                    _tmpListValuesOdontograma = ((UserControls.ucSomnolencia)ctrl).DataSource;
                    break;
                case ControlType.UcAcumetria:
                    _tmpListValuesOdontograma = ((UserControls.ucAcumetria)ctrl).DataSource;
                    break;
                case ControlType.UcSintomaticoRespi:
                    _tmpListValuesOdontograma = ((UserControls.ucSintomaticoResp)ctrl).DataSource;
                    break;
                case ControlType.UcRxLumboSacra:
                    _tmpListValuesOdontograma = ((UserControls.ucRXLumboSacra)ctrl).DataSource;
                    break;
                case ControlType.UcOtoscopia:
                    _tmpListValuesOdontograma = ((UserControls.ucOtoscopia)ctrl).DataSource;
                    break;
                case ControlType.UcEvaluacionErgonomica:
                    _tmpListValuesOdontograma = ((UserControls.ucEvaluacionErgonomica)ctrl).DataSource;
                    break;
                case ControlType.UcOjoSeco:
                    _tmpListValuesOdontograma = ((UserControls.ucOjoSeco)ctrl).DataSource;
                    break;
                case ControlType.UcOsteoMuscular:
                    _tmpListValuesOdontograma = ((UserControls.ucOsteoMuscular)ctrl).DataSource;
                    break;
                case ControlType.UcFototipo:
                    _tmpListValuesOdontograma = ((UserControls.ucFotoTipo)ctrl).DataSource;
                    break;

                //case ControlType.ucPsicologia:
                //    if (_esoTypeId == TypeESO.PreOcupacional)
                //    {
                //        _tmpListValuesOdontograma = ((UserControls.ucPsychologicalExam)ctrl).DataSource;
                //    }
                //    else if (_esoTypeId == TypeESO.PeriodicoAnual)
                //    {
                //        _tmpListValuesOdontograma = ((UserControls.ucPsychologicalExamAnual)ctrl).DataSource;
                //    }
                //    break;
                //case ControlType.ucEspirometria:
                //    _tmpListValuesOdontograma = ((UserControls.ucEspirometria)ctrl).DataSource;
                //    break;
                //case ControlType.ucOftalmologia:
                //    _tmpListValuesOdontograma = ((UserControls.ucOftalmologia)ctrl).DataSource;
                //    break;
                //case ControlType.ucRadiografiaOIT:
                //    _tmpListValuesOdontograma = ((UserControls.ucRadiografiaOIT)ctrl).DataSource;
                //    break;
                default:
                    break;
            }

            return value1;
        }

        private void comboBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        private static void SetValueControl(int controlId, Control ctrl, string componentFieldsId, string tagComponentFieldsId, string value1, SiNo hasAutomaticDx)
        {
            switch ((ControlType)controlId)
            {
                case ControlType.Fecha:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        ((DateTimePicker)ctrl).Value = Convert.ToDateTime(value1);
                        if (hasAutomaticDx == SiNo.SI)
                            ctrl.BackColor = Color.Pink;
                        else
                            ctrl.BackColor = Color.White;
                    }
                    break;
                case ControlType.CadenaTextual:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        ((TextBox)ctrl).Text = value1;
                        if (hasAutomaticDx == SiNo.SI)
                            ctrl.BackColor = Color.Pink;
                        else
                            ctrl.BackColor = Color.White;

                    }
                    break;
                case ControlType.CadenaMultilinea:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        ((TextBox)ctrl).Text = value1;
                        if (hasAutomaticDx == SiNo.SI)
                            ctrl.BackColor = Color.Pink;
                        else
                            ctrl.BackColor = Color.White;

                    }
                    break;
                case ControlType.NumeroEntero:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        if (string.IsNullOrEmpty(value1))
                            value1 = "0";

                        ((UltraNumericEditor)ctrl).Value = value1;
                        if (hasAutomaticDx == SiNo.SI)
                            ctrl.BackColor = Color.Pink;
                        else
                            ctrl.BackColor = Color.White;
                    }
                    break;
                case ControlType.NumeroDecimal:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        if (string.IsNullOrEmpty(value1))
                            value1 = "0";

                        ((UltraNumericEditor)ctrl).Value = value1;
                        if (hasAutomaticDx == SiNo.SI)
                            ctrl.BackColor = Color.Pink;
                        else
                            ctrl.BackColor = Color.White;
                    }
                    break;
                case ControlType.SiNoCheck:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        ((CheckBox)ctrl).Checked = Convert.ToBoolean(int.Parse(value1));
                    }
                    break;
                case ControlType.SiNoRadioButton:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        ((RadioButton)ctrl).Checked = Convert.ToBoolean(int.Parse(value1));
                    }
                    break;

                case ControlType.Radiobutton:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        ((RadioButton)ctrl).Checked = Convert.ToBoolean(int.Parse(value1));
                    }
                    break;
                case ControlType.SiNoCombo:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        ((ComboBox)ctrl).SelectedValue = value1;
                    }
                    break;
                case ControlType.Lista:
                    if (componentFieldsId == tagComponentFieldsId)
                    {
                        var cb = (ComboBox)ctrl;
                        cb.SelectedValue = value1;
                    }
                    break;
            }
        }
        
        private void SetDefaultValueAfterBuildMenu(ComponentList examen)
        {
            try
            {
                var findTab = tcExamList.Tabs[examen.v_ComponentId];

                foreach (ComponentFieldsList cf in examen.Fields)
                {
                    var ctrl = findTab.TabPage.Controls.Find(cf.v_ComponentFieldId, true);

                    if (ctrl.Length != 0)
                    {
                        #region Setear valor x defecto del control

                        switch ((ControlType)cf.i_ControlId)
                        {
                            case ControlType.Fecha:
                                DateTimePicker dtp = (DateTimePicker)ctrl[0];
                                dtp.CreateControl();
                                dtp.Value = string.IsNullOrEmpty(cf.v_DefaultText) ? DateTime.Now : DateTime.Parse(cf.v_DefaultText);
                                break;
                            case ControlType.CadenaTextual:
                                TextBox txtt = (TextBox)ctrl[0];
                                txtt.CreateControl();
                                txtt.Text = cf.v_DefaultText;
                                txtt.BackColor = Color.White;
                                break;
                            case ControlType.CadenaMultilinea:
                                TextBox txtm = (TextBox)ctrl[0];
                                txtm.CreateControl();
                                txtm.Text = cf.v_DefaultText;
                                txtm.BackColor = Color.White;
                                break;
                            case ControlType.NumeroEntero:
                                UltraNumericEditor uni = (UltraNumericEditor)ctrl[0];
                                uni.CreateControl();
                                uni.Value = string.IsNullOrEmpty(cf.v_DefaultText) ? 0 : int.Parse(cf.v_DefaultText);
                                uni.BackColor = Color.White;
                                break;
                            case ControlType.NumeroDecimal:
                                UltraNumericEditor und = (UltraNumericEditor)ctrl[0];
                                und.CreateControl();
                                und.Value = string.IsNullOrEmpty(cf.v_DefaultText) ? 0 : double.Parse(cf.v_DefaultText);
                                und.BackColor = Color.White;
                                break;
                            case ControlType.SiNoCheck:
                                CheckBox chkSiNo = (CheckBox)ctrl[0];
                                chkSiNo.CreateControl();
                                chkSiNo.Checked = !string.IsNullOrEmpty(cf.v_DefaultText) && Convert.ToBoolean(int.Parse(cf.v_DefaultText));
                                break;
                            case ControlType.SiNoRadioButton:
                                RadioButton rbSiNo = (RadioButton)ctrl[0];
                                rbSiNo.CreateControl();
                                rbSiNo.Checked = !string.IsNullOrEmpty(cf.v_DefaultText) && Convert.ToBoolean(int.Parse(cf.v_DefaultText));
                                break;
                            case ControlType.Radiobutton:
                                RadioButton rb = (RadioButton)ctrl[0];
                                rb.CreateControl();
                                rb.Checked = !string.IsNullOrEmpty(cf.v_DefaultText) && Convert.ToBoolean(int.Parse(cf.v_DefaultText));
                                break;
                            case ControlType.SiNoCombo:
                                ComboBox cbSiNo = (ComboBox)ctrl[0];
                                cbSiNo.CreateControl();
                                cbSiNo.SelectedValue = string.IsNullOrEmpty(cf.v_DefaultText) ? "-1" : cf.v_DefaultText;
                                cbSiNo.MouseWheel += new MouseEventHandler(comboBox1_MouseWheel);
                                break;
                            case ControlType.UcFileUpload:
                                break;
                            case ControlType.Lista:
                                ComboBox cbList = (ComboBox)ctrl[0];
                                cbList.CreateControl();
                                cbList.SelectedValue = string.IsNullOrEmpty(cf.v_DefaultText) ? "-1" : cf.v_DefaultText;
                                cbList.MouseWheel += new MouseEventHandler(comboBox1_MouseWheel);
                                break;
                            case ControlType.UcOdontograma:
                                //(UserControls.ucOdontograma).ClearValueControl();;
                                break;
                            case ControlType.UcAudiometria:
                                ((ucAudiometria)ctrl[0]).ClearValueControl();
                                break;
                            case ControlType.UcSomnolencia:
                                ((ucSomnolencia)ctrl[0]).ClearValueControl();
                                break;
                            case ControlType.UcAcumetria:
                                ((ucAcumetria)ctrl[0]).ClearValueControl();
                                break;
                            case ControlType.UcSintomaticoRespi:
                                ((ucSintomaticoResp)ctrl[0]).ClearValueControl();
                                break;
                            case ControlType.UcRxLumboSacra:
                                ((ucRXLumboSacra)ctrl[0]).ClearValueControl();
                                break;
                            case ControlType.UcOtoscopia:
                                ((ucOtoscopia)ctrl[0]).ClearValueControl();
                                break;
                            case ControlType.UcEvaluacionErgonomica:
                                ((ucEvaluacionErgonomica)ctrl[0]).ClearValueControl();
                                break;
                            case ControlType.UcOsteoMuscular:
                                ((ucOsteoMuscular)ctrl[0]).ClearValueControl();
                                break;
                          
                            case ControlType.UcOjoSeco:
                                ((ucOjoSeco)ctrl[0]).ClearValueControl();
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

        private int CreateCampoComponente(ComponentList component, ComponentFieldsList field, int groupboxIColumn, int nroControlNet, TableLayoutPanel tableLayoutPanel, UltraValidator ultraValidator)
        {
            CreateLabelField(field, groupboxIColumn, nroControlNet, tableLayoutPanel);
            nroControlNet++;

            CreateControl(component, field, groupboxIColumn, nroControlNet, tableLayoutPanel, ultraValidator);
            nroControlNet++;

            CreateUnidadMedida(field, groupboxIColumn, nroControlNet, tableLayoutPanel);
            nroControlNet++;

            return nroControlNet;
        }

        private static void CreateUnidadMedida(ComponentFieldsList field, int groupboxIColumn, int nroControlNet, TableLayoutPanel tableLayoutPanel)
        {
            var labelUnidadMedida = LabelUnidadMedida(field);
            var fila = RedondeoMayor(nroControlNet, groupboxIColumn * Constants.COLUMNAS_POR_CONTROL);
            var columna = nroControlNet - (fila - 1) * (groupboxIColumn * Constants.COLUMNAS_POR_CONTROL);
            tableLayoutPanel.Controls.Add(labelUnidadMedida, columna - 1, fila - 1);
        }

        private void CreateControl(ComponentList component, ComponentFieldsList field, int column, int nroControlNet, TableLayoutPanel tableLayoutPanel, UltraValidator ultraValidator)
        {       
            try
            {
                var control = CreateControlInEso(field, component, ultraValidator);
                var fila = RedondeoMayor(nroControlNet, column * Constants.COLUMNAS_POR_CONTROL);
                var columna = nroControlNet - (fila - 1) * (column * Constants.COLUMNAS_POR_CONTROL);
                tableLayoutPanel.Controls.Add(control, columna - 1, fila - 1);
            }
            catch (Exception ex)
            {
                
                throw;
            }
          
        }

        private static Label LabelUnidadMedida(ComponentFieldsList field)
        {
            var lbl1 = new Label
            {
                AutoSize = false,
                Width = 50,
                Text = field.v_MeasurementUnitName
            };
            lbl1.Font = new Font(lbl1.Font, FontStyle.Bold | FontStyle.Italic);
            lbl1.TextAlign = ContentAlignment.BottomLeft;

            return lbl1;
        }

        private Control CreateControlInEso(ComponentFieldsList field, ComponentList component, UltraValidator validator)
        {
            var esMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            UltraNumericEditor une;
            TextBox txt;
            Control ctl = null;

            switch ((ControlType)field.i_ControlId)
            {
                #region Creacion del control
                case ControlType.Fecha:
                    var dtp = new DateTimePicker();
                    dtp.Width = field.i_ControlWidth;
                    dtp.Height = field.i_HeightControl;
                    dtp.Name = field.v_ComponentFieldId;
                    dtp.Format = DateTimePickerFormat.Custom;
                    dtp.CustomFormat = @"dd/MM/yyyy";
                    ctl = dtp;
                    break;

                case ControlType.CadenaTextual:
                    txt = new TextBox();
                    txt.Width = field.i_ControlWidth;
                    txt.Height = field.i_HeightControl;
                    txt.MaxLength = field.i_MaxLenght;
                    txt.Name = field.v_ComponentFieldId;
                    txt.CharacterCasing = esMayuscula == 1 ? CharacterCasing.Upper : CharacterCasing.Normal;

                    if (field.i_IsCalculate == (int)SiNo.SI)
                    {
                        txt.Enabled = false;
                    }
                    else
                    {
                        txt.Leave += txt_Leave;
                    }

                    if (field.i_IsRequired == (int)SiNo.SI)
                        SetControlValidate(field.i_ControlId, txt, null, null, validator);

                    txt.Enter += Capture_Value;

                    //AMC
                    if (field.i_Enabled == (int)SiNo.NO)
                    {
                        txt.Enabled = true;
                    }
                    else
                    {
                        txt.Enabled = false;
                    }


                    if (field.i_ReadOnly == (int)SiNo.SI)
                    {
                        txt.ReadOnly = true;
                    }
                    else
                    {
                        txt.ReadOnly = false;
                    }

                    if (_action == "View")
                    {
                        txt.ReadOnly = true;
                    }

                    ctl = txt;
                    break;
                case ControlType.CadenaMultilinea:
                    txt = new TextBox()
                    {
                        Width = field.i_ControlWidth,
                        Height = field.i_HeightControl,
                        Multiline = true,
                        MaxLength = field.i_MaxLenght,
                        ScrollBars = ScrollBars.Vertical,
                        Name = field.v_ComponentFieldId
                    };

                    //AMC
                    if (field.i_Enabled == (int)SiNo.NO)
                    {
                        txt.Enabled = true;
                    }
                    else
                    {
                        txt.Enabled = false;
                    }


                    if (field.i_ReadOnly == (int)SiNo.SI)
                    {
                        txt.ReadOnly = true;
                    }
                    else
                    {
                        txt.ReadOnly = false;
                    }

                    txt.CharacterCasing = esMayuscula == 1 ? CharacterCasing.Upper : CharacterCasing.Normal;

                    txt.Enter += Capture_Value;
                    txt.Leave += txt_Leave;

                    if (field.i_IsRequired == (int)SiNo.SI)
                        SetControlValidate(field.i_ControlId, txt, null, null, validator);

                    if (_action == "View")
                    {
                        txt.ReadOnly = true;
                    }

                    ctl = txt;
                    break;
                case ControlType.NumeroEntero:
                    une = new UltraNumericEditor()
                    {
                        Width = field.i_ControlWidth,
                        Height = field.i_HeightControl,
                        NumericType = NumericType.Integer,
                        PromptChar = ' ',
                        Name = field.v_ComponentFieldId,
                        MaskDisplayMode = MaskMode.Raw

                    };

                    // Asociar el control a un evento
                    une.Enter += Capture_Value;

                    if (field.i_IsCalculate == (int)SiNo.SI)
                    {
                        une.ReadOnly = true;
                        une.ValueChanged += txt_ValueChanged;
                    }
                    else
                    {
                        une.Leave += txt_Leave;
                    }

                    if (field.i_IsRequired == (int)SiNo.SI)
                    {
                        // Establecer condición por rangos
                        SetControlValidate(field.i_ControlId, une, field.r_ValidateValue1, field.r_ValidateValue2, validator);
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
                        Width = field.i_ControlWidth,
                        Height = field.i_HeightControl,
                        PromptChar = ' ',
                        Name = field.v_ComponentFieldId,
                        NumericType = NumericType.Double,
                        MaskDisplayMode = MaskMode.Raw

                    };

                    if (field.i_NroDecimales == 1)
                    {
                        une.MaskInput = "nnnnn.n";
                    }
                    else if (field.i_NroDecimales == 2)
                    {
                        une.MaskInput = "nnnnn.nn";
                    }
                    else if (field.i_NroDecimales == 3)
                    {
                        une.MaskInput = "nnnnn.nnn";
                    }
                    else if (field.i_NroDecimales == 4)
                    {
                        une.MaskInput = "nnnnn.nnnn";
                    }




                    // Asociar el control a un evento
                    une.Enter += Capture_Value;

                    if (field.i_IsCalculate == (int)SiNo.SI)
                    {
                        une.ValueChanged += txt_ValueChanged;
                        une.ReadOnly = true;
                    }
                    else
                    {
                        une.Leave += txt_Leave;
                    }

                    if (field.i_IsRequired == (int)SiNo.SI)
                    {
                        // Establecer condición por rangos                                                              
                        SetControlValidate(field.i_ControlId, une, field.r_ValidateValue1, field.r_ValidateValue2, validator);
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
                        Width = field.i_ControlWidth,
                        Height = field.i_HeightControl,
                        Text = @"Si/No",
                        Name = field.v_ComponentFieldId,
                    };

                    ctl.Enter += Capture_Value;
                    ctl.Leave += txt_Leave;

                    if (_action == "View")
                    {
                        ctl.Enabled = false;
                    }

                    break;
                case ControlType.SiNoRadioButton:
                    ctl = new RadioButton()
                    {
                        Width = field.i_ControlWidth,
                        Height = field.i_HeightControl,
                        Text = @"Si/No",
                        Name = field.v_ComponentFieldId
                    };

                    ctl.Enter += Capture_Value;
                    ctl.Leave += txt_Leave;

                    if (_action == "View")
                    {
                        ctl.Enabled = false;
                    }

                    break;
                case ControlType.Radiobutton:
                    ctl = new RadioButton()
                    {
                        Width = field.i_ControlWidth,
                        Height = field.i_HeightControl,
                        Name = field.v_ComponentFieldId
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
                        Width = field.i_ControlWidth,
                        Height = field.i_HeightControl,
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Name = field.v_ComponentFieldId
                    };
                    var data = _cboEso.Find(p => p.Id == field.i_GroupId.ToString()).Items;
                    var list = ConvertClassToStruct(data);
                    Utils.LoadDropDownList((ComboBox)ctl, "Value1", "Id", list, DropDownListAction.Select);

                    if (field.i_IsRequired == (int)SiNo.SI)
                    {
                        SetControlValidate(field.i_ControlId, ctl, null, null, validator);
                    }

                    ctl.Enter += Capture_Value;
                    ctl.Leave += txt_Leave;

                    if (_action == "View")
                    {
                        ctl.Enabled = false;
                    }

                    break;
                #region ...
                case ControlType.UcFileUpload:
                    var ucFileUpload = new ucFileUpload
                    {
                        PersonId = _personId,
                        Name = field.v_ComponentFieldId,
                        Dni = _Dni,
                        Fecha  = _FechaServico.Value.ToString("ddMMyyyy"),
                        Consultorio = component.v_CategoryName,
                        ServiceComponentId = component.v_ServiceComponentId
                    };
                    //ucFileUpload.Dni = _Dni;
                    //ucFileUpload.Fecha = lblFecInicio.Text;
                    //ucFileUpload.Consultorio = com.v_CategoryName;
                    //ucFileUpload.ServiceComponentId = com.v_ServiceComponentId;

                    //ctl = new Sigesoft.Node.WinClient.UI.UserControls.ucFileUpload();
                    ctl = ucFileUpload;
                    break;
                case ControlType.UcOdontograma:
                    var ucOdontograma = new ucOdontograma {Name = field.v_ComponentFieldId};
                    ctl = ucOdontograma;
                    break;
                case ControlType.UcFototipo:
                    var ucFotoTipo = new ucFotoTipo { Name = field.v_ComponentFieldId };
                    ctl = ucFotoTipo;
                    break;
                case ControlType.UcAudiometria:
                    var ucAudiometria = new ucAudiometria
                    {
                        Name = field.v_ComponentFieldId,
                        PersonId = _personId,
                        ServiceComponentId = component.v_ServiceComponentId
                    };
                    ctl = ucAudiometria;
                    break;
                case ControlType.UcSomnolencia:
                    var ucSomnolencia = new ucSomnolencia {Name = field.v_ComponentFieldId};
                    ctl = ucSomnolencia;
                    break;

                case ControlType.UcBoton:
                    var ucBoton = new ucBoton
                    {
                        Name = field.v_ComponentFieldId,
                        Examen = component.v_Name
                    };
                    //ucBoton.Dni = _Dni;
                    //ucBoton.FechaServicio = _FechaServico.Value;
                    ctl = ucBoton;
                    break;

                case ControlType.UcAcumetria:
                    var ucAcumetria = new ucAcumetria {Name = field.v_ComponentFieldId};
                    ctl = ucAcumetria;
                    break;
                case ControlType.UcSintomaticoRespi:
                    var ucSintomaticoRespi = new ucSintomaticoResp {Name = field.v_ComponentFieldId};
                    ctl = ucSintomaticoRespi;
                    break;
                case ControlType.UcRxLumboSacra:
                    var ucRxLumboSacra = new ucRXLumboSacra {Name = field.v_ComponentFieldId};
                    ctl = ucRxLumboSacra;
                    break;               
                case ControlType.UcOjoSeco:
                    var ucOjoSeco = new ucOjoSeco {Name = field.v_ComponentFieldId};
                    ctl = ucOjoSeco;
                    break;

                case ControlType.UcOtoscopia:
                    var ucOtoscopia = new ucOtoscopia {Name = field.v_ComponentFieldId};
                    ctl = ucOtoscopia;
                    break;

                case ControlType.UcEvaluacionErgonomica:
                    var ucEvaluacionErgonomica = new ucEvaluacionErgonomica {Name = field.v_ComponentFieldId};
                    ctl = ucEvaluacionErgonomica;
                    break;

                case ControlType.UcOsteoMuscular:
                    var ucOsteo = new ucOsteoMuscular {Name = field.v_ComponentFieldId};
                    ctl = ucOsteo;
                    break;


                #endregion
               
                case ControlType.Lista:
                    var cb = new ComboBox()
                    {
                        Width = field.i_ControlWidth,
                        Height = field.i_HeightControl,
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Name = field.v_ComponentFieldId
                    };
                    var dataLista = _cboEso.Find(p => p.Id == field.i_GroupId.ToString()).Items;
                    var Listas = ConvertClassToStruct(dataLista);
                    Utils.LoadDropDownList(cb, "Value1", "Id", Listas, DropDownListAction.Select);

                    if (field.i_IsRequired == (int)SiNo.SI)
                    {
                        SetControlValidate(field.i_ControlId, cb, null, null, validator);
                    }

                    // Setear levantamiento de popup para el ingreso de los hallazgos solo cuando 
                    // se seleccione un valor alterado

                  if ((field.v_ComponentId == Constants.EXAMEN_FISICO_ID
                                                || field.v_ComponentId == Constants.RX_TORAX_ID
                                                || field.v_ComponentId == Constants.OFTALMOLOGIA_ID
                                                || field.v_ComponentId == Constants.ALTURA_ESTRUCTURAL_ID
                                                || field.v_ComponentId == Constants.TACTO_RECTAL_ID
                                                || field.v_ComponentId == Constants.EVAL_NEUROLOGICA_ID
                                                || field.v_ComponentId == Constants.TEST_ROMBERG_ID
                                                || field.v_ComponentId == Constants.TAMIZAJE_DERMATOLOGIO_ID
                                                || field.v_ComponentId == Constants.GINECOLOGIA_ID
                                                || field.v_ComponentId == Constants.EXAMEN_MAMA_ID
                                                || field.v_ComponentId == Constants.AUDIOMETRIA_ID
                                                || field.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID
                                                || field.v_ComponentId == Constants.ESPIROMETRIA_ID
                                                || field.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1
                                                || field.v_ComponentId == Constants.PRUEBA_ESFUERZO_ID
                                                || field.v_ComponentId == Constants.TAMIZAJE_DERMATOLOGIO_ID
                                                || field.v_ComponentId == Constants.ODONTOGRAMA_ID
                                                || field.v_ComponentId == Constants.EXAMEN_FISICO_7C_ID)
                                                && (field.i_GroupId == (int)SystemParameterGroups.ConHallazgoSinHallazgosNoSeRealizo))
                    {
                        cb.SelectedIndexChanged += new EventHandler(cb_SelectedIndexChanged);
                    }

                    cb.Enter += Capture_Value;
                    cb.Leave += txt_Leave;

                    if (_action == "View")
                    {
                        cb.Enabled = false;
                    }

                    ctl = cb;
                    break;

                default:
                    MessageBox.Show("ALERT", field.i_ControlId.ToString());
                    break;

                #endregion
            }

            if (ctl != null)
                ctl.Tag = new KeyTagControl
                {
                    i_ControlId = field.i_ControlId,
                    v_ComponentId = field.v_ComponentId,
                    v_ComponentFieldsId = field.v_ComponentFieldId,
                    i_IsSourceFieldToCalculate = field.i_IsSourceFieldToCalculate,
                    v_Formula = field.v_Formula,
                    v_TargetFieldOfCalculateId = field.v_TargetFieldOfCalculateId,
                    v_SourceFieldToCalculateJoin = field.v_SourceFieldToCalculateJoin,
                    v_FormulaChild = field.v_FormulaChild,
                    Formula = field.Formula,
                    TargetFieldOfCalculateId = field.TargetFieldOfCalculateId,
                    v_TextLabel = field.v_TextLabel,
                    v_ComponentName = component.v_Name
                };

            return ctl;
        }

        private static List<StructKeyDto> ConvertClassToStruct(IEnumerable<KeyValueDTO> data)
        {
            return data.Select(item => new StructKeyDto
            {
                Value1 = item.Value1, Id = item.Id
            }).ToList();
        }
        
        public  struct StructKeyDto
        {
            public string Id { get; set; }
            public string Value1 { get; set; }
            public string Value2 { get; set; }
            public string Value3 { get; set; }
            public float Value4 { get; set; }
            public int GrupoId { get; set; }
            public int ParameterId { get; set; }
            public string Field { get; set; }
            public int ParentId { get; set; }
        }

        private static void CreateLabelField(ComponentFieldsList fields, int column, int nroControlNet, TableLayoutPanel tableLayoutPanel)
        {
            var lbl = CreateLabel(fields);
            var fila = RedondeoMayor(nroControlNet, column * Constants.COLUMNAS_POR_CONTROL);
            var columna = nroControlNet - (fila - 1) * (column * Constants.COLUMNAS_POR_CONTROL);
            tableLayoutPanel.Controls.Add(lbl, columna - 1, fila - 1);
        }

        private static Label CreateLabel(ComponentFieldsList fields)
        {
            var lbl = new Label
            {
                Text = fields.v_TextLabel,
                Width = fields.i_LabelWidth,
                TextAlign = ContentAlignment.BottomRight,
                AutoSize = false
            };
            lbl.Font = new Font(lbl.Font.FontFamily.Name, 7.25F);

            return lbl;
        }

        private static IEnumerable<ComponentFieldsList> GetFieldsEachGroups(ComponentList component, ComponentFieldsList groupbox, ComponentList groupComponentName)
        {
            return component.Fields.FindAll(p => p.v_Group == groupbox.v_Group && p.v_ComponentId == groupComponentName.v_ComponentId);

        }

        private UltraTab CrearTab(ComponentList component)
        {
            var ultraTab = new UltraTab
            {
                Text = component.v_Name,
                Key = component.v_ComponentId,
                Tag = component.v_ServiceComponentId,
                ToolTipText = component.v_Name + @" / " + component.v_ServiceComponentId + @" / " + component.v_ComponentId
            };
            if (component.i_ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado) ultraTab.Appearance.BackColor = Color.Pink;
            tcExamList.Tabs.Add(ultraTab);
            return ultraTab;
        }

        private static bool VisibleTab(ComponentList component)
        {
            var readingPermission = UserPermission();
            return readingPermission.Find(p => component.v_ComponentId.Contains(p.v_ComponentId)) != null;
        }

        private static TableLayoutPanel CreateTableLayoutPanelParent(UltraTab tab)
        {
            var tblpParent = new TableLayoutPanel
            {
                Name = "tblpParent",
                ColumnCount = 1,
                RowCount = new List<ComponentFieldsList>().Count
            };
            tab.TabPage.Controls.Add(tblpParent);
            return tblpParent;
        }

        private UltraValidator CreateValidators(ComponentList component)
        {
            var uv = CreateUltraValidatorByComponentId(component.v_ComponentId);
            return uv;
        }

        private static GroupBox CreateGroupBoxComponent(ComponentList groupBox)
        {
            var subcategory = ObtenerCategory(groupBox.v_ComponentId);
            var gbGroupedComponent = new GroupBox
            {
                Text = groupBox.v_GroupedComponentName + " - " + subcategory,
                Name = "gb_" + groupBox.v_GroupedComponentName,
                BackColor = Color.LightCyan,
                AutoSize = true,
                Dock = DockStyle.Top,

            };
          
            return gbGroupedComponent;
        }

        private static object ObtenerCategory(string componentId)
        {
            var name = new ServiceBL().GetSubCategoryName(componentId);

            if (name == null)
            {
                return "";
            }
            return name;
        }

        private static TableLayoutPanel CreateTableLayoutForControls(ComponentFieldsList groupbox, ComponentList component)
        {
            var tblpGroup = new TableLayoutPanel
            {
                Name = "tblpGroup_" + groupbox.v_Group,
                ColumnCount = groupbox.i_Column * Constants.COLUMNAS_POR_CONTROL,
                RowCount = RedondeoMayor(component.Fields.Count, groupbox.i_Column),
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            return tblpGroup;
        }

        private static int RedondeoMayor(int a, int b)
        {
            return (int)Math.Ceiling(a / (double)b);
        }

        private static GroupBox CreateGroupBoxForGroup(ComponentFieldsList groupbox, ServiceComponentList serviceComponentsInfo)
        {
            var groupBox = new GroupBox
            {
                Text = groupbox.v_Group,
                Name = "gb_" + groupbox.v_Group,
                BackColor = Color.Azure,
                AutoSize = true,
                Dock = DockStyle.Top
            };
            var x = serviceComponentsInfo.ServiceComponentFields;
            if (serviceComponentsInfo.v_ComponentId == "N009-ME000000062")
            {
                if (serviceComponentsInfo.v_ServiceComponentStatusName != "POR INICIAR")
                {
                    var y = x.Select(p => p.ServiceComponentFieldValues).ToList();
                    if (y.Find(p => p[0].v_ComponentFieldId == "N002-MF000000222")[0].v_Value1 == "1" && groupbox.v_Group == "C 3. Forma y Tamaño: (Consulte las radiografías estandar, se requieres dos símbolos; marque un primario y un secundario)")
                    {
                        groupBox.Enabled = false;
                    }
                    if (y.Find(p => p[0].v_ComponentFieldId == "N009-MF000000761")[0].v_Value1 == "1" && (groupbox.v_Group == "C 4.1 Placa Pleurales" || groupbox.v_Group == "C 4.2 Engrosamiento Difuso de la Pleura"))
                    {
                        groupBox.Enabled = false;
                    }
                    if (y.Find(p => p[0].v_ComponentFieldId == "N009-MF000000760")[0].v_Value1 == "1" && groupbox.v_Group == "C 6. MARQUE  la respuesta adecuada; si marca \"od\", escriba a continuación un COMENTARIO")
                    {
                        groupBox.Enabled = false;
                    }
                }
                else
                {
                    if (groupbox.v_Group == "C 3. Forma y Tamaño: (Consulte las radiografías estandar, se requieres dos símbolos; marque un primario y un secundario)" || groupbox.v_Group == "C 4.1 Placa Pleurales" || groupbox.v_Group == "C 4.2 Engrosamiento Difuso de la Pleura" || groupbox.v_Group == "C 6. MARQUE  la respuesta adecuada; si marca \"od\", escriba a continuación un COMENTARIO")
                    {
                        groupBox.Enabled = false;
                    }
                }
                
            }
            return groupBox;
        }

        private static IEnumerable<ComponentFieldsList> GetGroupBoxes(ComponentList component, string componentId)
        {
            try
            {
                var fieldsByComponent = component.Fields
                                      .ToList()
                                      .FindAll(p => p.v_ComponentId == componentId);

                var groupBoxes = fieldsByComponent.GroupBy(e => new { e.v_Group }).Select(g => g.First()).OrderBy(o => o.v_Group).ToList();

                return groupBoxes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private static TableLayoutPanel CreateTableLayoutPanelChildren(ComponentList groupComponentName)
        {
            TableLayoutPanel tblpGroupedComponent = new TableLayoutPanel
            {
                Name = "tblpGroup_" + groupComponentName.v_GroupedComponentName,
                ColumnCount = 1,
                RowCount = 1,
                Dock = DockStyle.Fill,
                AutoSize = true
            };
            return tblpGroupedComponent;
        }

        private UltraValidator CreateUltraValidatorByComponentId(string componentId)
        {
            var uv = new UltraValidator(components);

            if (_dicUltraValidators == null)
                _dicUltraValidators = new Dictionary<string, UltraValidator>();

            _dicUltraValidators.Add(componentId, uv);

            return uv;
        }

        private void InitializeStaticControls()
        {
            LoadComboBox();
            FormActions();
        }

        private void FormActions()
        {
            _formActions = BLL.Utils.SetFormActionsInSession("frmEso", _nodeId, _roleId, _userId);
            btnGuardarExamen.Enabled = BLL.Utils.IsActionEnabled("frmEso_EXAMENES_SAVE", _formActions);
            btnAgregarTotalDiagnostico.Enabled = BLL.Utils.IsActionEnabled("frmEso_ANADX_ADDDX", _formActions);
            _removerTotalDiagnostico = BLL.Utils.IsActionEnabled("frmEso_ANADX_REMOVEDX", _formActions);
            btnAgregarRecomendaciones_AnalisisDx.Enabled = BLL.Utils.IsActionEnabled("frmEso_ANADX_ADDRECOME", _formActions);
            _removerRecomendacionAnalisisDx = BLL.Utils.IsActionEnabled("frmEso_ANADX_REMOVERECOME", _formActions);
            btnAgregarRestriccion_Analisis.Enabled = BLL.Utils.IsActionEnabled("frmEso_ANADX_ADDRESTRIC", _formActions);
            _removerRestriccionAnalisis = BLL.Utils.IsActionEnabled("frmEso_ANADX_REMOVERESTRIC", _formActions);
            btnAceptarDX.Enabled = BLL.Utils.IsActionEnabled("frmEso_ANADX_SAVE", _formActions);

            btnAgregarRecomendaciones_Conclusiones.Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_CONCLUSIONES_ADDRECOME", _formActions);
            btnAgregarRestriccion_ConclusionesTratamiento.Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_CONCLUSIONES_ADDRESTRIC", _formActions);

            _removerRecomendaciones_Conclusiones = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_CONCLUSIONES_REMOVERECOME", _formActions);
            _removerRestricciones_ConclusionesTratamiento = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_CONCLUSIONES_REMOVERESTRIC", _formActions);
            if (btnAceptarDX.Enabled) return;
            cbCalificacionFinal.Enabled = false;
            cbTipoDx.Enabled = false;
            cbEnviarAntecedentes.Enabled = false;
            dtpFechaVcto.Enabled = false;
        }

        private void LoadData()
        {
            //revisar
            OperationResult objOperationResult = new OperationResult();
            ServiceList personData = new ServiceBL().GetServicePersonData(ref objOperationResult, _serviceId);
            
            var dataService = GetServiceData(_serviceId);
            _cancelEventSelectedIndexChange = true;
            LoadTabAnamnesis(dataService);
            _cancelEventSelectedIndexChange = false;
            LoadTabExamenes(dataService);
            LoadTabControlCalidad();
            LoadTabAptitud();

            _ProtocolId = personData.v_ProtocolId;
            _personName = string.Format("{0} {1} {2}", personData.v_FirstLastName, personData.v_SecondLastName, personData.v_FirstName);
            _personId = personData.v_PersonId;
            _Dni = personData.v_DocNumber;
            _masterServiceId = personData.i_MasterServiceId.Value;
            if (_masterServiceId == 2) btnReceta.Enabled = false;
            else btnReceta.Enabled = true;

            cbAptitudEso.SelectedValue = personData.i_AptitudeStatusId.ToString();
            txtComentarioAptitud.Text = personData.v_ObsStatusService;

            DateTime? FechaActual = DateTime.Now;
            dptDateGlobalExp.Value = personData.d_GlobalExpirationDate == null ? FechaActual.Value.Date.AddYears(1) : personData.d_GlobalExpirationDate.Value;
            //cbNuevoControl.SelectedValue = personData.i_IsNewControl;

            _FechaServico = personData.d_ServiceDate;
            LoadTabGenericDataandAntecedent(personData);

        }

        private void LoadTabGenericDataandAntecedent(ServiceList genericDataAntecedent)
        {
            OperationResult objOperationResult = new OperationResult();

            Utils.LoadDropDownList(cboGenero, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 100, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cboEstadoCivil, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 101, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cboGradoInstruccion, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 108, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlBloodGroupId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 154, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlBloodFactorId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 155, null), DropDownListAction.Select);

            txtApellidoPaterno.Text = genericDataAntecedent.v_FirstLastName;
            txtApellidoMaterno.Text = genericDataAntecedent.v_SecondLastName;
            txtNombres.Text = genericDataAntecedent.v_FirstName;
            cboGenero.SelectedValue = genericDataAntecedent.i_SexTypeId.ToString();
            txtEdad.Text = _age.ToString();
            dtpFechaNacimiento.Value = genericDataAntecedent.d_BirthDate.Value;
            txtLugarNacimiento.Text = genericDataAntecedent.v_BirthPlace;
            txtProcedencia.Text = genericDataAntecedent.v_Procedencia;
            ddlBloodFactorId.SelectedValue = genericDataAntecedent.i_BloodFactorId.ToString();
            ddlBloodGroupId.SelectedValue = genericDataAntecedent.i_BloodGroupId.ToString();
            cboGradoInstruccion.SelectedValue = genericDataAntecedent.i_LevelOfId.ToString();
            cboEstadoCivil.SelectedValue = genericDataAntecedent.i_MaritalStatusId.ToString();
            txtOcupacion.Text = genericDataAntecedent.v_CurrentOccupation;
            txtDomicilio.Text = genericDataAntecedent.v_AdressLocation;
            txtCentroEducativo.Text = genericDataAntecedent.v_CentroEducativo;
            txtHijosVivos.Text = genericDataAntecedent.TotalHijos.ToString();
            lblDni.Text = genericDataAntecedent.v_DocNumber.ToString();

            idPerson = new AtencionesIntegralesBL().GetService(_serviceId);

            if (_age <= 12)
            {
                Utils.LoadDropDownList(listaEstadoCivilMujer, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 101, null), DropDownListAction.Select);
                Utils.LoadDropDownList(listaGradoInstruccionMujer, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 108, null), DropDownListAction.Select);
                Utils.LoadDropDownList(listTipoAfiliacionMujer, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 188, null), DropDownListAction.Select);

                Utils.LoadDropDownList(listaEstadoCivilHombre, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 101, null), DropDownListAction.Select);
                Utils.LoadDropDownList(listaGradoInstruccionHombre, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 108, null), DropDownListAction.Select);
                Utils.LoadDropDownList(listTipoAfiliacionHombre, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 188, null), DropDownListAction.Select);


                tbcDatos.TabPages.Remove(tbpAdultoMayor);
                tbcDatos.TabPages.Remove(tbpAdolescente);

                objNinioDto = new AtencionesIntegralesBL().GetNinio(ref objOperationResult, idPerson.v_PersonId);

                if (objNinioDto != null)
                {
                    txtNombreMadrePadreResponsable.Text = objNinioDto.v_NombreCuidador;
                    txtEdadMadrePadreResponsable.Text = objNinioDto.v_EdadCuidador;
                    txtDNIMadrePadreResponsable.Text = objNinioDto.v_DniCuidador;

                    txtNombrePadreTutor.Text = objNinioDto.v_NombrePadre;
                    txtEdadPadre.Text = objNinioDto.v_EdadPadre;
                    txtDNIPadre.Text = objNinioDto.v_DniPadre;
                    listTipoAfiliacionHombre.SelectedValue = objNinioDto.i_TipoAfiliacionPadre.ToString();
                    txtAfiliacionPadre.Text = objNinioDto.v_CodigoAfiliacionPadre;
                    listaGradoInstruccionHombre.SelectedValue = objNinioDto.i_GradoInstruccionPadre.ToString();
                    txtOcupacionPadre.Text = objNinioDto.v_OcupacionPadre;
                    listaEstadoCivilHombre.SelectedValue = objNinioDto.i_EstadoCivilIdPadre.ToString();
                    txtReligionPadre.Text = objNinioDto.v_ReligionPadre;

                    txtNombreMadreTutor.Text = objNinioDto.v_NombreMadre;
                    txtEdadMadre.Text = objNinioDto.v_EdadMadre;
                    txtDNIMadre.Text = objNinioDto.v_DniMadre;
                    listTipoAfiliacionMujer.SelectedValue = objNinioDto.i_TipoAfiliacionMadre.ToString();
                    txtAfiliacionMadre.Text = objNinioDto.v_CodigoAfiliacionMadre;
                    listaGradoInstruccionMujer.SelectedValue = objNinioDto.i_GradoInstruccionMadre.ToString();
                    txtOcupacionMadre.Text = objNinioDto.v_OcupacionMadre;
                    listaEstadoCivilMujer.SelectedValue = objNinioDto.i_EstadoCivilIdMadre1.ToString();
                    txtReligionMadre.Text = objNinioDto.v_ReligionMadre;

                    textPatologiasGestacion.Text = objNinioDto.v_PatologiasGestacion;
                    txtNEmbarazo.Text = objNinioDto.v_nEmbarazos;
                    txtAPN.Text = objNinioDto.v_nAPN;
                    textLugarAPN.Text = objNinioDto.v_LugarAPN;
                    textComplicacionesParto.Text = objNinioDto.v_ComplicacionesParto;
                    textQuienAtendio.Text = objNinioDto.v_Atencion;

                    textEdadNacer.Text = objNinioDto.v_EdadGestacion;
                    textPesoNacer.Text = objNinioDto.v_Peso;
                    textTallaNacer.Text = objNinioDto.v_Talla;
                    textPerimetroCefalico.Text = objNinioDto.v_PerimetroCefalico;
                    textTiempoHospit.Text = objNinioDto.v_TiempoHospitalizacion;
                    textPerimetroToracico.Text = objNinioDto.v_PerimetroToracico;
                    textEspecificacionesNacer.Text = objNinioDto.v_EspecificacionesNac;
                    txtInicioAlimentación.Text = objNinioDto.v_InicioAlimentacionComp;
                    textAlergiaMedicamentos.Text = objNinioDto.v_AlergiasMedicamentos;
                    textOtrosAntecedentes.Text = objNinioDto.v_OtrosAntecedentes;

                    textQuienTuberculosis.Text = objNinioDto.v_QuienTuberculosis;
                    if (objNinioDto.i_QuienTuberculosis == (int)Enfermedad.Si)
                        rbSiTuberculosis.Checked = true;
                    else
                        rbNoTuberculosis.Checked = true;

                    textQuienASMA.Text = objNinioDto.v_QuienAsma;
                    if (objNinioDto.i_QuienAsma == (int)Enfermedad.Si)
                        rbSiAsma.Checked = true;
                    else
                        rbNoAsma.Checked = true;

                    textQuienVIH.Text = objNinioDto.v_QuienVIH;
                    if (objNinioDto.i_QuienVIH == (int)Enfermedad.Si)
                        rbSiVih.Checked = true;
                    else
                        rbNoVih.Checked = true;

                    textQuienDiabetes.Text = objNinioDto.v_QuienDiabetes;
                    if (objNinioDto.i_QuienDiabetes == (int)Enfermedad.Si)
                        rbSiDiabetes.Checked = true;
                    else
                        rbNoDiabetes.Checked = true;

                    textQuienEpilepsia.Text = objNinioDto.v_QuienEpilepsia;
                    if (objNinioDto.i_QuienEpilepsia == (int)Enfermedad.Si)
                        rbSiEpilepsia.Checked = true;
                    else
                        rbNoEpilepsia.Checked = true;

                    textQuienAlergias.Text = objNinioDto.v_QuienAlergias;
                    if (objNinioDto.i_QuienAlergias == (int)Enfermedad.Si)
                        rbSiAlergia.Checked = true;
                    else
                        rbNoAlergia.Checked = true;

                    textQuienViolenciaFamiliar.Text = objNinioDto.v_QuienViolenciaFamiliar;
                    if (objNinioDto.i_QuienViolenciaFamiliar == (int)Enfermedad.Si)
                        rbSiViolencia.Checked = true;
                    else
                        rbNoViolencia.Checked = true;

                    textQuienAlcoholismo.Text = objNinioDto.v_QuienAlcoholismo;
                    if (objNinioDto.i_QuienAlcoholismo == (int)Enfermedad.Si)
                        rbSiAlcoholismo.Checked = true;
                    else
                        rbNoAlcoholismo.Checked = true;

                    textQuienDrogadiccion.Text = objNinioDto.v_QuienDrogadiccion;
                    if (objNinioDto.i_QuienDrogadiccion == (int)Enfermedad.Si)
                        rbSiDrogadiccion.Checked = true;
                    else
                        rbNoDrogadiccion.Checked = true;

                    textQuienHepatitisB.Text = objNinioDto.v_QuienHeptitisB;
                    if (objNinioDto.i_QuienHeptitisB == (int)Enfermedad.Si)
                        rbSiHepatitis.Checked = true;
                    else
                        rbNoHepatitis.Checked = true;

                    textAguaPotable.Text = objNinioDto.v_EspecificacionesAgua;
                    textDesague.Text = objNinioDto.v_EspecificacionesDesague;
                }
            }
            else if (13 <= _age && _age <= 17)
            {
                tbcDatos.TabPages.Remove(tbpNinio);
                tbcDatos.TabPages.Remove(tbpAdultoMayor);

                objAdolDto = new AtencionesIntegralesBL().GetAdolescente(ref objOperationResult, idPerson.v_PersonId);

                if (objAdolDto != null)
                {
                    textNombreCuidadorAdol.Text = objAdolDto.v_NombreCuidador;
                    textDniCuidadorAdol.Text = objAdolDto.v_DniCuidador;
                    textEdadAdol.Text = objAdolDto.v_EdadCuidador;
                    textViveConAdol.Text = objAdolDto.v_ViveCon;
                    txtAdoEdadIniTrab.Text = objAdolDto.v_EdadInicioTrabajo;
                    txtAdoTipoTrab.Text = objAdolDto.v_TipoTrabajo;
                    txtAdoNroTv.Text = objAdolDto.v_NroHorasTv;
                    txtAdoNroJuegos.Text = objAdolDto.v_NroHorasJuegos;
                    txtAdoMenarquia.Text = objAdolDto.v_MenarquiaEspermarquia;
                    txtAdoEdadRS.Text = objAdolDto.v_EdadInicioRS;
                    textObservacionesSexualidadAdol.Text = objAdolDto.v_Observaciones;
                }
            }
            else if (18 <= _age && _age <= 64)
            {
                tbcDatos.TabPages.Remove(tbpNinio);
                tbcDatos.TabPages.Remove(tbpAdolescente);
                CargarGrillaEmbarazos(idPerson.v_PersonId);

                if (genericDataAntecedent.i_SexTypeId == (int)Gender.MASCULINO)
                {
                    pnlMenarquia.Enabled = false;
                    pnlEmbarazo.Enabled = false;

                    objAdultoDto = new AtencionesIntegralesBL().GetAdulto(ref objOperationResult, idPerson.v_PersonId);

                    if (objAdultoDto != null)
                    {
                        txtAmCuidador.Text = objAdultoDto.v_NombreCuidador;
                        txtAmCuidadorEdad.Text = objAdultoDto.v_EdadCuidador;
                        txtAmCuidadorDni.Text = objAdultoDto.v_DniCuidador;
                        textOtroAntecedentesFamiliares.Text = objAdultoDto.v_OtrosAntecedentes;
                        textFlujoVaginalPatológico.Text = objAdultoDto.v_FlujoVaginal;
                        textMedicamentoFrecuenteDosis.Text = objAdultoDto.v_MedicamentoFrecuente;
                        textReaccionAlergicaMedicamentos.Text = objAdultoDto.v_ReaccionAlergica;
                        txtAmInicioRs.Text = objAdultoDto.v_InicioRS;
                        txtAmNroPs.Text = objAdultoDto.v_NroPs;
                        txtAmFechaUR.Text = objAdultoDto.v_FechaUR;
                    }
                }
                else
                {
                    pnlMenarquia.Enabled = true;
                    pnlEmbarazo.Enabled = true;

                    objAdultoDto = new AtencionesIntegralesBL().GetAdulto(ref objOperationResult, idPerson.v_PersonId);

                    if (objAdultoDto != null)
                    {
                        txtAmCuidador.Text = objAdultoDto.v_NombreCuidador;
                        txtAmCuidadorEdad.Text = objAdultoDto.v_EdadCuidador;
                        txtAmCuidadorDni.Text = objAdultoDto.v_DniCuidador;
                        textOtroAntecedentesFamiliares.Text = objAdultoDto.v_OtrosAntecedentes;
                        textFlujoVaginalPatológico.Text = objAdultoDto.v_FlujoVaginal;
                        textMedicamentoFrecuenteDosis.Text = objAdultoDto.v_MedicamentoFrecuente;
                        textReaccionAlergicaMedicamentos.Text = objAdultoDto.v_ReaccionAlergica;
                        txtAmInicioRs.Text = objAdultoDto.v_InicioRS;
                        txtAmNroPs.Text = objAdultoDto.v_NroPs;
                        txtAmFechaUR.Text = objAdultoDto.v_FechaUR;
                        txtAmRC.Text = objAdultoDto.v_RC;
                        txtAmNroParto.Text = objAdultoDto.v_Parto;
                        txtAmPrematuro.Text = objAdultoDto.v_Prematuro;
                        txtAmAborto.Text = objAdultoDto.v_Aborto;
                        textObservacionesEmbarazo.Text = objAdultoDto.v_ObservacionesEmbarazo;
                    }
                }
            }
            else
            {
                tbcDatos.TabPages.Remove(tbpNinio);
                tbcDatos.TabPages.Remove(tbpAdolescente);

                if (genericDataAntecedent.i_SexTypeId == (int)Gender.MASCULINO)
                {
                    pnlMenarquia.Enabled = false;
                    pnlEmbarazo.Enabled = false;

                    objAdultoMAyorDto = new AtencionesIntegralesBL().GetAdultoMayor(ref objOperationResult, idPerson.v_PersonId);

                    if (objAdultoMAyorDto != null)
                    {
                        txtAmCuidador.Text = objAdultoMAyorDto.v_NombreCuidador;
                        txtAmCuidadorEdad.Text = objAdultoMAyorDto.v_EdadCuidador;
                        txtAmCuidadorDni.Text = objAdultoMAyorDto.v_DniCuidador;
                        textFlujoVaginalPatológico.Text = objAdultoMAyorDto.v_FlujoVaginal;
                        textMedicamentoFrecuenteDosis.Text = objAdultoMAyorDto.v_MedicamentoFrecuente;
                        textReaccionAlergicaMedicamentos.Text = objAdultoMAyorDto.v_ReaccionAlergica;
                        txtAmInicioRs.Text = objAdultoMAyorDto.v_InicioRS;
                        txtAmNroPs.Text = objAdultoMAyorDto.v_NroPs;
                        txtAmFechaUR.Text = objAdultoMAyorDto.v_FechaUR;
                    }
                }
                else
                {
                    pnlMenarquia.Enabled = true;
                    pnlEmbarazo.Enabled = true;

                    objAdultoMAyorDto = new AtencionesIntegralesBL().GetAdultoMayor(ref objOperationResult, idPerson.v_PersonId);

                    if (objAdultoMAyorDto != null)
                    {
                        txtAmCuidador.Text = objAdultoMAyorDto.v_NombreCuidador;
                        txtAmCuidadorEdad.Text = objAdultoMAyorDto.v_EdadCuidador;
                        txtAmCuidadorDni.Text = objAdultoMAyorDto.v_DniCuidador;
                        textFlujoVaginalPatológico.Text = objAdultoMAyorDto.v_FlujoVaginal;
                        textMedicamentoFrecuenteDosis.Text = objAdultoMAyorDto.v_MedicamentoFrecuente;
                        textReaccionAlergicaMedicamentos.Text = objAdultoMAyorDto.v_ReaccionAlergica;
                        txtAmInicioRs.Text = objAdultoMAyorDto.v_InicioRS;
                        txtAmNroPs.Text = objAdultoMAyorDto.v_NroPs;
                        txtAmFechaUR.Text = objAdultoMAyorDto.v_FechaUR;
                        txtAmRC.Text = objAdultoMAyorDto.v_RC;
                        txtAmNroParto.Text = objAdultoMAyorDto.v_Parto;
                        txtAmPrematuro.Text = objAdultoMAyorDto.v_Prematuro;
                        txtAmAborto.Text = objAdultoMAyorDto.v_Aborto;
                        textObservacionesEmbarazo.Text = objAdultoMAyorDto.v_ObservacionesEmbarazo;
                    }
                }
            }
        }

        private void CargarGrillaEmbarazos(string personId)
        {
            OperationResult objOperationResult = new OperationResult();
            var objData = new EmbarazoBL().GetEmbarazoFiltered(ref objOperationResult, 0, null, "", "", personId);

            grdEmbarazos.DataSource = objData;
        }
        private static void LoadTabAptitud()
        {
            //throw new NotImplementedException();
        }

        private void LoadTabControlCalidad()
        {
            List<DiagnosticRepositoryList> tmpTotalDiagnosticByServiceIdList = null;
            Task.Factory.StartNew(() =>
            {
                tmpTotalDiagnosticByServiceIdList = new ServiceBL().GetServiceComponentDisgnosticsByServiceId(ref _objOperationResult, _serviceId);
            }).ContinueWith(t =>
            {
                grdTotalDiagnosticos.DataSource = tmpTotalDiagnosticByServiceIdList;
                lblRecordCountTotalDiagnosticos.Text = string.Format("Se encontraron {0} registros.", tmpTotalDiagnosticByServiceIdList.Count());
                SetCurrentRow();

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private int? _rowIndex;

        private void SetCurrentRow()
        {
            if (_rowIndex == null || grdTotalDiagnosticos.Rows.Count == 0) return;
            var rowCount = grdTotalDiagnosticos.Rows.Count;
            var rows = rowCount - _rowIndex.Value;
            int i;
            if (rows != 0)
                i = _rowIndex.Value;
            else
                i = _rowIndex.Value - 1;

            grdTotalDiagnosticos.Rows[i].Selected = true;
            grdTotalDiagnosticos.ActiveRowScrollRegion.ScrollRowIntoView(grdTotalDiagnosticos.Rows[i]);
        }

        private ServiceData GetServiceData(string serviceId)
        {
            return new ServiceBL().GetServiceData(ref _objOperationResult, serviceId);
        }

        private void LoadTabAnamnesis(ServiceData dataAnamnesis)
        {
            OperationResult objOperationResult = new OperationResult();
            ServiceList personData = new ServiceBL().GetServicePersonData(ref objOperationResult, _serviceId);
            
            chkPresentaSisntomas.Checked = Convert.ToBoolean(dataAnamnesis.HasSymptomId);
            txtSintomaPrincipal.Text = string.IsNullOrEmpty(dataAnamnesis.MainSymptom) ? "No Refiere" : dataAnamnesis.MainSymptom;
            txtValorTiempoEnfermedad.Enabled = chkPresentaSisntomas.Checked;
            txtValorTiempoEnfermedad.Text = dataAnamnesis.TimeOfDisease == null ? string.Empty : dataAnamnesis.TimeOfDisease.ToString();
            cbCalendario.Enabled = chkPresentaSisntomas.Checked;
            cbCalendario.SelectedValue = dataAnamnesis.TimeOfDiseaseTypeId == null ? "1" : dataAnamnesis.TimeOfDiseaseTypeId.ToString();
            txtRelato.Text = string.IsNullOrEmpty(dataAnamnesis.Story) ? "Paciente Asintomático" : dataAnamnesis.Story;
            _cancelEventSelectedIndexChange = true;
            cbSueño.SelectedValue = dataAnamnesis.DreamId == null ? "1" : dataAnamnesis.DreamId.ToString();
            cbApetito.SelectedValue = dataAnamnesis.AppetiteId == null ? "1" : dataAnamnesis.AppetiteId.ToString();
            cbDeposiciones.SelectedValue = dataAnamnesis.DepositionId == null ? "1" : dataAnamnesis.DepositionId.ToString();
            cbOrina.SelectedValue = dataAnamnesis.UrineId == null ? "1" : dataAnamnesis.UrineId.ToString();
            cbSed.SelectedValue = dataAnamnesis.ThirstId == null ? "1" : dataAnamnesis.ThirstId.ToString();
            _cancelEventSelectedIndexChange = false;
            txtHallazgos.Text = string.IsNullOrEmpty(dataAnamnesis.Findings) ? "Sin Alteración" : dataAnamnesis.Findings;
            //txtFechaUltimoPAP.Text = string.IsNullOrEmpty(personData.v_FechaUltimoPAP) ? "" : personData.v_FechaUltimoPAP;
            txtMenarquia.Text = dataAnamnesis.Menarquia;
            txtGestapara.Text = string.IsNullOrEmpty(dataAnamnesis.Gestapara) ? "G ( )  P ( ) ( ) ( ) ( ) " : dataAnamnesis.Gestapara;
            cbMac.SelectedValue = dataAnamnesis.MacId == null ? "1" : dataAnamnesis.MacId.ToString();
            txtRegimenCatamenial.Text = dataAnamnesis.CatemenialRegime;
            txtCiruGine.Text = dataAnamnesis.CiruGine;
            txtFecVctoGlobal.Text = dataAnamnesis.GlobalExpirationDate == null ? "NO REQUIERE" : dataAnamnesis.GlobalExpirationDate.Value.ToShortDateString();

            txtVidaSexual.Text = dataAnamnesis.v_InicioVidaSexaul;
            txtNroParejasActuales.Text = dataAnamnesis.v_NroParejasActuales;
            txtNroAbortos.Text = dataAnamnesis.v_NroAbortos;
            txtNroCausa.Text = dataAnamnesis.v_PrecisarCausas;

            
            if (dataAnamnesis.Pap != null) dtpPAP.Value = dataAnamnesis.Pap.Value;
            if (dataAnamnesis.Pap != null) dtpPAP.Checked = true;
            if (dataAnamnesis.Fur != null) dtpFur.Value = dataAnamnesis.Fur.Value;
            if (dataAnamnesis.Fur != null) dtpFur.Checked = true;
            if (dataAnamnesis.Mamografia != null) dtpMamografia.Value = dataAnamnesis.Mamografia.Value;
            if (dataAnamnesis.Mamografia != null) dtpMamografia.Checked = true;
            _sexType = (Gender)dataAnamnesis.SexTypeId;
            BlockControlsByGender(dataAnamnesis.SexTypeId);
            ConsolidadoAntecedentes(dataAnamnesis.PersonId);
            HistorialServiciosAnteriores(dataAnamnesis.PersonId, dataAnamnesis.ServiceId);
            ///
            txtResultadoPAP.Text = string.IsNullOrEmpty(personData.v_ResultadosPAP) ? "" : personData.v_ResultadosPAP;
            txtResultadoMamo.Text = string.IsNullOrEmpty(personData.v_ResultadoMamo) ? "" : personData.v_ResultadoMamo;

            txtVidaSexual.Text = string.IsNullOrEmpty(personData.v_InicioVidaSexaul) ? "" : personData.v_InicioVidaSexaul;
            txtNroParejasActuales.Text = string.IsNullOrEmpty(personData.v_NroParejasActuales) ? "" : personData.v_NroParejasActuales;
            txtNroAbortos.Text = string.IsNullOrEmpty(personData.v_NroAbortos) ? "" : personData.v_NroAbortos;
            txtNroCausa.Text = string.IsNullOrEmpty(personData.v_PrecisarCausas) ? "" : personData.v_PrecisarCausas;
            ///
            _AMCGenero = personData.i_SexTypeId.Value;
            _sexType = (Gender)personData.i_SexTypeId;
            _sexTypeId = personData.i_SexTypeId;
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

        }

        private void HistorialServiciosAnteriores(string personId, string serviceId)
        {
            var services = new ServiceBL().GetServicesConsolidateForService(ref _objOperationResult, personId, serviceId);
            grdServiciosAnteriores.DataSource = services;
            GetValueResult(_objOperationResult);
        }

        private void ConsolidadoAntecedentes(string personId)
        {
            GetAntecedentConsolidateForService(personId);
        }

        private void GetAntecedentConsolidateForService(string personId)
        {
            var antecedent = new ServiceBL().GetAntecedentConsolidateForService(ref _objOperationResult, personId);
            if (antecedent == null) return;
            grdAntecedentes.DataSource = antecedent;
            GetValueResult(_objOperationResult);
        }

        private static void GetValueResult(OperationResult objOperationResult)
        {
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show(Constants.GenericErrorMessage, "¡SE OBTUVO UN ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BlockControlsByGender(int? sexTypeId)
        {
            if (sexTypeId != null)
                switch ((Gender)sexTypeId)
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
                }
        }

        private void LoadComboBox()
        {
            OperationResult objOperationResult = new OperationResult();
            var listDataForCombo = BLL.Utils.GetSystemParameterForComboForm(ref _objOperationResult, "frmEso");
            var data124 = listDataForCombo.Find(p => p.Id == "124").Items;
            Utils.LoadDropDownList(cbAptitudEso, "Value1", "Id", ConvertClassToStruct(data124), DropDownListAction.Select);
            var data133 = listDataForCombo.Find(p => p.Id == "133").Items;
            Utils.LoadDropDownList(cbCalendario, "Value1", "Id", ConvertClassToStruct(data133), DropDownListAction.Select);
            //var data183 = listDataForCombo.Find(p => p.Id == "183").Items;
            //Utils.LoadDropDownList(cbSueño, "Value1", "Id", ConvertClassToStruct(data183), DropDownListAction.Select);
            //Utils.LoadDropDownList(cbOrina, "Value1", "Id", ConvertClassToStruct(data183), DropDownListAction.Select);
            //Utils.LoadDropDownList(cbDeposiciones, "Value1", "Id", ConvertClassToStruct(data183), DropDownListAction.Select);
            //Utils.LoadDropDownList(cbApetito, "Value1", "Id", ConvertClassToStruct(data183), DropDownListAction.Select);
            //Utils.LoadDropDownList(cbSed, "Value1", "Id", ConvertClassToStruct(data183), DropDownListAction.Select);
            Utils.LoadDropDownList(cbSueño, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 135, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbOrina, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 135, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbDeposiciones, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 135, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbApetito, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 135, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cbSed, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 135, null), DropDownListAction.Select);
            var data134 = listDataForCombo.Find(p => p.Id == "134").Items;
            Utils.LoadDropDownList(cbMac, "Value1", "Id", ConvertClassToStruct(data134), DropDownListAction.Select);
            var serviceComponent = listDataForCombo.Find(p => p.Id == "127").Items;
            Utils.LoadDropDownList(cbEstadoComponente, "Value1", "Id", serviceComponent.FindAll(p => p.Id != ((int)ServiceComponentStatus.PorIniciar).ToString()));
            Utils.LoadDropDownList(cbTipoProcedenciaExamen, "Value1", "Id", listDataForCombo.Find(p => p.Id == "132").Items);
            cbTipoProcedenciaExamen.SelectedValue = Convert.ToInt32(ComponenteProcedencia.Interno).ToString();
            var data138 = listDataForCombo.Find(p => p.Id == "138").Items;
            Utils.LoadDropDownList(cbCalificacionFinal, "Value1", "Id", ConvertClassToStruct(data138), DropDownListAction.Select);
            var data139 = listDataForCombo.Find(p => p.Id == "139").Items;
            Utils.LoadDropDownList(cbTipoDx, "Value1", "Id", ConvertClassToStruct(data139), DropDownListAction.Select);
            var data111 = listDataForCombo.Find(p => p.Id == "111").Items;
            Utils.LoadDropDownList(cbEnviarAntecedentes, "Value1", "Id", ConvertClassToStruct(data111), DropDownListAction.Select);

        }

        private void ViewMode(string mode)
        {
            if (_tipo == (int)MasterService.AtxMedicaParticular || _tipo == (int)MasterService.AtxMedicaSeguros)
            {
                //tcSubMain.TabPages.Remove(tp);
                tcSubMain.TabPages.Remove(General);
                tcSubMain.TabPages.Remove(tpConclusion);
            }
            else
            {
                tcSubMain.TabPages.Remove(tpAtencionIntegral);
                tcSubMain.TabPages.Remove(tpDatosGeneralesAntecedentes);
                tcSubMain.TabPages.Remove(tpCuidadosPreventivos);
                tcSubMain.SelectedIndex = 0;
            }

            // Setear por default un examen componente desde consusltorio
            //#region Set Tab x default

            //if (!string.IsNullOrEmpty(_componentIdByDefault))
            //{
            //    var comp = _componentIdByDefault.Split('|');

            //    foreach (var tab in tcExamList.Tabs)
            //    {
            //        var arrfind = Array.FindAll(comp, p => tab.Key.Contains(p));

            //        if (arrfind.Length > 0)
            //        {
            //            tab.Selected = true;
            //            break;
            //        }
            //    }
            //}
            //else
            //{
            //    tcExamList.Tabs[0].Selected = true;
            //}

            //#endregion

            // Setear pestaña de Aptitud x default
            if (mode == "Service")
                tcSubMain.SelectedIndex = 2;

            // Información para grillas
            GetTotalDiagnosticsForGridView();
            GetConclusionesDiagnosticasForGridView();
            ConclusionesyTratamiento_LoadAllGrid();
            gbEdicionDiagnosticoTotal.Enabled = false;
            if (_tipo == (int)MasterService.AtxMedicaParticular || _tipo == (int)MasterService.AtxMedicaSeguros)
            {
                ConstruirFormularioAntecedentes();
                ConstruirFormularioCuidadosPreventivos();
            }


            if (mode == "View")
            {
                gbAntecedentes.Enabled = false;
                gbServiciosAnteriores.Enabled = false;
                cbAptitudEso.Enabled = false;
                gbConclusionesDiagnosticas.Enabled = false;
                gbRecomendaciones_Conclusiones.Enabled = false;
                gbRestricciones_Conclusiones.Enabled = false;
                btnGuardarConclusiones.Enabled = false;
                btnAceptarDX.Enabled = false;
                cbAptitudEso.Enabled = false;
                btnGuardarExamen.Enabled = false;
                btnVisorReporteExamen.Enabled = false;
            }

            //if (mode == "View")
            //{
            //    OnlyRead();
            //}
            //else if (mode == "Service")
            //{
            //    tcSubMain.SelectedIndex = 3;
            //}
                
        }
        private void ConstruirFormularioAntecedentes()
        {
            int GrupoBase = 282; //Antecedentes
            GrupoEtario = new ServiceBL().ObtenerIdGrupoEtarioDePaciente(_age);
            int Grupo = int.Parse(GrupoBase.ToString() + GrupoEtario.ToString());

            List<frmEsoAntecedentesPadre> AntecedentesActuales = new ServiceBL().ObtenerEsoAntecedentesPorGrupoId(Grupo, GrupoEtario, _personId);

            if (AntecedentesActuales.Count == 0)
            {
                btnGuardarAntecedentes.Visible = false;
                label58.Visible = false;
                ultraAntActuales.Visible = false;
            }
            else
            {
                ultraAntActuales.DataSource = AntecedentesActuales;
                ultraAntActuales.DisplayLayout.Bands[0].Columns[0].CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
            }

            int GrupoEtarioAnterior = 0;

            switch (GrupoEtario)
            {
                case 1:
                    {
                        GrupoEtarioAnterior = 1;
                        break;
                    }
                case 2:
                    {
                        GrupoEtarioAnterior = 2;
                        break;
                    }
                case 3:
                    {
                        GrupoEtarioAnterior = 3;
                        break;
                    }
                case 4:
                    {
                        GrupoEtarioAnterior = 4;
                        break;
                    }
                default:
                    {
                        GrupoEtarioAnterior = 0;
                        break;
                    }
            }
            Grupo = int.Parse(GrupoBase.ToString() + GrupoEtarioAnterior.ToString());
            List<frmEsoAntecedentesPadre> AntecedentesAnteriores = new ServiceBL().ObtenerEsoAntecedentesPorGrupoId(Grupo, GrupoEtarioAnterior, _personId);

            if (AntecedentesAnteriores.Count == 0)
            {
                label39.Visible = false;
                ultraAntAnteriores.Visible = false;
            }
            else
            {
                ultraAntAnteriores.DataSource = AntecedentesAnteriores;
            }
        }

        private void ConstruirFormularioCuidadosPreventivos()
        {
            int GrupoBase = 0;
            switch (GrupoEtario)
            {
                case (int)Sigesoft.Common.GrupoEtario.Ninio:
                    {
                        GrupoBase = 292;
                        break;
                    }
                case (int)Sigesoft.Common.GrupoEtario.Adolecente:
                    {
                        GrupoBase = 285;
                        break;
                    }
                case (int)Sigesoft.Common.GrupoEtario.Adulto:
                    {
                        if (_AMCGenero == 1)
                        {
                            GrupoBase = 284;
                            break;
                        }
                        else
                        {
                            GrupoBase = 283;
                            break;
                        }
                    }
                case (int)Sigesoft.Common.GrupoEtario.AdultoMayor:
                    {
                        GrupoBase = 286;
                        break;
                    }
                default:
                    {
                        GrupoBase = 0;
                        break;
                    }
            }

            if (GrupoBase == 0)
            {
                dataGridView1.Visible = false;
                btnGuardarCuidadosPreventivos.Visible = false;
                return;
            }

            List<frmEsoCuidadosPreventivosFechas> Fechas = new ServiceBL().ObtenerFechasCuidadosPreventivos(GrupoBase, _personId);
            List<frmEsoCuidadosPreventivosComentarios> Comentarios = new ServiceBL().ObtenerComentariosCuidadosPreventivos(_personId);

            if (Fechas.Count == 0)
            {
                dataGridView1.Visible = false;
            }
            else
            {
                dataGridView1.Columns.Add("GrupoId", "GrupoId");
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns.Add("ParameterId", "ParameterId");
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns.Add("Nombre", "Nombre");
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                int ContadorColumna = 2;
                bool AgregarTitulos = true;
                foreach (var F in Fechas)
                {
                    ContadorColumna++;
                    dataGridView1.Columns.Add("Fecha" + (ContadorColumna - 2), F.FechaServicio.ToShortDateString());
                    dataGridView1.Columns[ContadorColumna].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridView1.Columns[ContadorColumna].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    if (ContadorColumna != (Fechas.Count + 2))
                        dataGridView1.Columns[ContadorColumna].ReadOnly = true;

                    int ContadorFila = -1;
                    foreach (var L in F.Listado)
                    {
                        ContadorFila++;
                        if (AgregarTitulos)
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[ContadorFila].Cells[0].Value = L.GrupoId;
                            dataGridView1.Rows[ContadorFila].Cells[1].Value = L.ParameterId;
                            dataGridView1.Rows[ContadorFila].Cells[2].Value = L.Nombre;

                            if (L.Hijos != null)
                                dataGridView1.Rows[ContadorFila].Cells[2].Style.Font = new Font(FontFamily.GenericSansSerif, 10f, FontStyle.Bold);
                        }

                        if (L.Hijos != null)
                        {
                            dataGridView1.Rows[ContadorFila].Cells[ContadorColumna].ReadOnly = true;
                            ContadorFila = AgregarHijosDeTablaRecursivo(L, AgregarTitulos, F.FechaServicio, ContadorColumna, ContadorFila);
                        }
                        else
                        {

                            DataGridViewCheckBoxCell cell = new DataGridViewCheckBoxCell(false);
                            cell.Value = L.Valor;
                            dataGridView1.Rows[ContadorFila].Cells[ContadorColumna] = cell;
                            if (ContadorColumna != Fechas.Count)
                                dataGridView1.Rows[ContadorFila].Cells[ContadorColumna].ReadOnly = true;
                        }
                    }
                    AgregarTitulos = false;
                }

                ContadorColumna++;
                dataGridView1.Columns.Add("Comentarios", "Comentarios");
                dataGridView1.Columns[ContadorColumna].SortMode = DataGridViewColumnSortMode.NotSortable;


                foreach (DataGridViewRow D in dataGridView1.Rows)
                {
                    int CelIndex = D.Cells.Count - 2;

                    DataGridViewCheckBoxCell cell = D.Cells[CelIndex] as DataGridViewCheckBoxCell;

                    if (cell != null)
                    {
                        D.Cells["Comentarios"].Value = Comentarios.Where(x => x.GrupoId == int.Parse(D.Cells["GrupoId"].Value.ToString()) && x.ParametroId == int.Parse(D.Cells["ParameterId"].Value.ToString())).FirstOrDefault() == null ? "" : Comentarios.Where(x => x.GrupoId == int.Parse(D.Cells["GrupoId"].Value.ToString()) && x.ParametroId == int.Parse(D.Cells["ParameterId"].Value.ToString())).FirstOrDefault().Comentario;
                    }
                    else
                    {
                        D.Cells["Comentarios"].ReadOnly = true;
                    }
                }
            }
        }
        private void OnlyRead()
        {
            //Anamnesis
            gbSintomasySignos.Enabled = false;
            gbFuncionesBiologicas.Enabled = false;
            btnGuardarAnamnesis.Enabled = false;
            //Examenes
            gbDiagnosticoExamen.Enabled = false;
            txtComentario.Enabled = false;
            cbEstadoComponente.Enabled = false;
            cbTipoProcedenciaExamen.Enabled = false;
            //Diagnósticos
            gbTotalDiagnostico.Enabled = false;
            gbEdicionDiagnosticoTotal.Enabled = false;
            btnAceptarDX.Enabled = false;
        }

        private void btnViewWorker_Click_1(object sender, EventArgs e)
        {
            var frmWorkerData = new FrmWorkerData(_serviceId);
            frmWorkerData.ShowDialog();
        }

        private void lblView_Click_1(object sender, EventArgs e)
        {
            var lbl = sender as Label;
            if (lbl != null && lbl.Text == "Ver Diagnósticos ...")
            {
                splitContainer2.SplitterDistance = splitContainer2.Height - 200;
                lbl.Text = string.Format("{0}", "Ver menos");
                lbl.ForeColor = Color.Blue;
            }
            else
            {
                if (lbl == null) return;
                splitContainer2.SplitterDistance = splitContainer2.Height;
                lbl.Text = "Ver Diagnósticos ...";
                lbl.ForeColor = Color.Red;
            }

        }

        private static void SetControlValidate(int controlId, Control ctrl, float? validateValue1, float? validateValue2, UltraValidator uv)
        {
            // Objetos para validar
            RangeCondition rc;
            ValidationSettings vs;

            uv.ErrorAppearance.BackColor = Color.FromArgb(255, 255, 192);
            uv.ErrorAppearance.BackGradientStyle = GradientStyle.Vertical;
            uv.ErrorAppearance.BorderColor = Color.Pink;
            uv.NotificationSettings.Action = NotificationAction.MessageBox;

            switch ((ControlType)controlId)
            {
                case ControlType.CadenaTextual:
                    uv.GetValidationSettings((TextBox)ctrl).IsRequired = true;
                    break;
                case ControlType.CadenaMultilinea:
                    uv.GetValidationSettings((TextBox)ctrl).IsRequired = true;
                    break;
                case ControlType.NumeroEntero:
                    // Establecer condición por rangos
                    rc = new RangeCondition(validateValue1,
                                                             validateValue2,
                                                             typeof(int));
                    vs = uv.GetValidationSettings((UltraNumericEditor)ctrl);
                    vs.Condition = rc;
                    break;
                case ControlType.NumeroDecimal:
                    // Establecer condición por rangos
                    rc = new RangeCondition(validateValue1,
                                                             validateValue2,
                                                             typeof(double));
                    vs = uv.GetValidationSettings((UltraNumericEditor)ctrl);
                    vs.Condition = rc;
                    break;
                case ControlType.SiNoCheck:
                    break;
                case ControlType.SiNoRadioButton:
                    break;
                case ControlType.Radiobutton:
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
            }
        }

        #region Events


        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (_cancelEventSelectedIndexChange)
                return;

            var tagCtrl = (KeyTagControl)((ComboBox)sender).Tag;
            var componentId = tagCtrl.v_ComponentId;
            var value1 = int.Parse(((ComboBox)sender).SelectedValue.ToString());

            TextBox field = null;

            if (value1 == (int)NormalAlterado.Alterado)
            {
                var frm = new Operations.Popups.frmRegisterFinding(tagCtrl.v_ComponentName, "", tagCtrl.v_TextLabel);

                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.Cancel)
                    return;

                if (_componentId.Contains(Constants.EXAMEN_FISICO_ID))
                {
                    field = (TextBox)FindControlInCurrentTab(Constants.EXAMEN_FISICO_HALLAZGOS_ID)[0];

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

                if (_componentId.Contains(Constants.EXAMEN_FISICO_ID))
                { }
            }
        }
        private int AgregarHijosDeTablaRecursivo(frmEsoCuidadosPreventivos Lista, bool AgregarTitulos, DateTime FechaServicio, int ContadorColumna, int ContadorFila)
        {
            foreach (var L in Lista.Hijos)
            {
                ContadorFila++;
                if (AgregarTitulos)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[ContadorFila].Cells[0].Value = L.GrupoId;
                    dataGridView1.Rows[ContadorFila].Cells[1].Value = L.ParameterId;
                    dataGridView1.Rows[ContadorFila].Cells[2].Value = L.Nombre;

                    if (L.Hijos != null)
                        dataGridView1.Rows[ContadorFila].Cells[2].Style.Font = new Font(FontFamily.GenericSansSerif, 10f, FontStyle.Bold);
                }

                if (L.Hijos != null)
                {
                    ContadorFila = AgregarHijosDeTablaRecursivo(L, AgregarTitulos, FechaServicio, ContadorColumna, ContadorFila);
                }
                else
                {

                    DataGridViewCheckBoxCell cell = new DataGridViewCheckBoxCell(false);
                    cell.Value = L.Valor;
                    dataGridView1.Rows[ContadorFila].Cells[ContadorColumna] = cell;
                }
            }

            return ContadorFila;
        }

        private void txt_Leave(object sender, System.EventArgs e)
        {
            flagValueChange = true;

            // Capturar el control invocador
            Control senderCtrl = (Control)sender;
            // Obtener información contenida en la propiedad Tag del control invocante
            var tagCtrl = (KeyTagControl)senderCtrl.Tag;
            string valueToAnalyze = GetValueControl(tagCtrl.i_ControlId, senderCtrl);
            int isSourceField = tagCtrl.i_IsSourceFieldToCalculate;

            Dictionary<string, object> Params = null;
            List<double> evalExpResultList = new List<double>();

            #region logica de modificacion de flag [_isChangeValue]

            if (!_isChangeValue)
            {
                if (_oldValue != valueToAnalyze)
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

                    } 

                    bool isFound = false;
                    
                    var isContain = formu.v_Formula.Contains("/");
                    if (isContain)
                    {
                        foreach (var item in Params)
                        {
                            if (item.Value.ToString() == "0")
                            {
                                isFound = true;
                                break;
                            }
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

                GeneratedAutoDX(valueToAnalyze, senderCtrl, tagCtrl);
            }
            else
            {
                GeneratedAutoDX(valueToAnalyze, senderCtrl, tagCtrl);
            }

        }

        private void Capture_Value(object sender, EventArgs e)
        {
            Control senderCtrl = (Control)sender;
            // Obtener información contenida en la propiedad Tag del control invocante
            var tagCtrl = (KeyTagControl)senderCtrl.Tag;
            // Capturar valor inicial
            //_oldValue = GetValueControl(tagCtrl.i_ControlId, senderCtrl);
            if (tagCtrl.v_ComponentId == "N009-ME000000062")
            {
                GroupBox gb = null;
                gb = (GroupBox)FindControlInCurrentTab("gb_C 3. Forma y Tamaño: (Consulte las radiografías estandar, se requieres dos símbolos; marque un primario y un secundario)")[0];

                if (tagCtrl.v_ComponentFieldsId == "N002-MF000000222")
                {
                    gb.Enabled = false;
                }
                else if (tagCtrl.v_ComponentFieldsId == "N002-MF000000220" || tagCtrl.v_ComponentFieldsId == "N002-MF000000221" || tagCtrl.v_ComponentFieldsId == "N002-MF000000223" || tagCtrl.v_ComponentFieldsId == "N009-MF000000720"
                    || tagCtrl.v_ComponentFieldsId == "N009-MF000000721" || tagCtrl.v_ComponentFieldsId == "N009-MF000000722" || tagCtrl.v_ComponentFieldsId == "N009-MF000000723" || tagCtrl.v_ComponentFieldsId == "N009-MF000000724"
                    || tagCtrl.v_ComponentFieldsId == "N009-MF000000725" || tagCtrl.v_ComponentFieldsId == "N009-MF000000726"
                    || tagCtrl.v_ComponentFieldsId == "N009-MF000000727")
                {
                    gb.Enabled = true;
                }

                GroupBox gb41 = null;
                GroupBox gb42 = null;
                gb41 = (GroupBox)FindControlInCurrentTab("gb_C 4.1 Placa Pleurales")[0];
                gb42 = (GroupBox)FindControlInCurrentTab("gb_C 4.2 Engrosamiento Difuso de la Pleura")[0];
                if (tagCtrl.v_ComponentFieldsId == "N009-MF000003194")
                {
                    gb41.Enabled = true;
                    gb42.Enabled = true;

                }
                else if (tagCtrl.v_ComponentFieldsId == "N009-MF000000761")
                {
                    gb41.Enabled = false;
                    gb42.Enabled = false;
                }

                GroupBox gb6 = null;
                gb6 = (GroupBox)FindControlInCurrentTab("gb_C 6. MARQUE  la respuesta adecuada; si marca \"od\", escriba a continuación un COMENTARIO")[0];

                if (tagCtrl.v_ComponentFieldsId == "N009-MF000003195")
                {
                    gb6.Enabled = true;
                    gb6.Enabled = true;

                }
                else if (tagCtrl.v_ComponentFieldsId == "N009-MF000000760")
                {
                    gb6.Enabled = false;
                    gb6.Enabled = false;
                }
            }
            else if (tagCtrl.v_ComponentId == "N009-ME000000444")
            {
                GroupBox gbE = null;
                gbE = (GroupBox)FindControlInCurrentTab("gb_E. FUNCIONES COGNITIVAS")[0];
                GroupBox gbF = null;
                gbF = (GroupBox)FindControlInCurrentTab("gb_F. ASPECTOS INTRA E INTERPERSONALES")[0];
                GroupBox gbG = null;
                gbG = (GroupBox)FindControlInCurrentTab("gb_G. PERFIL DE PERSONALIDAD - TEST DE COLORES")[0];
                GroupBox gbH = null;
                gbH = (GroupBox)FindControlInCurrentTab("gb_H. ADAPTABILIDAD, CAPACIDAD DE AFRONTE - HOMBRE BAJO LA LLUVIA")[0];

                GroupBox gbI = null;
                gbI = (GroupBox)FindControlInCurrentTab("gb_I. FUNCIONES COGNITIVAS.")[0];
                GroupBox gbJ = null;
                gbJ = (GroupBox)FindControlInCurrentTab("gb_J. ASPECTOS INTRA E INTERPERSONALES")[0];
                GroupBox gbK = null;
                gbK = (GroupBox)FindControlInCurrentTab("gb_K. PERFIL DE PERSONALIDAD")[0];
                GroupBox gbL = null;
                gbL = (GroupBox)FindControlInCurrentTab("gb_L.  ADAPTABILIDAD - CLIMA SOCIAL, LABORAL")[0];

                var value = GetValueControl(tagCtrl.i_ControlId, senderCtrl);


                RadioButton valSC = (RadioButton)FindControlInCurrentTab("N009-MF000003531")[0];
                RadioButton valSI = (RadioButton)FindControlInCurrentTab("N009-MF000003530")[0];
                RadioButton valGA = (RadioButton)FindControlInCurrentTab("N009-MF000003532")[0];

                if (tagCtrl.v_ComponentFieldsId == "N009-MF000003531")
                {

                    gbE.Enabled = true;
                    gbF.Enabled = true;
                    gbG.Enabled = true;
                    gbH.Enabled = true;

                    gbI.Enabled = true;
                    gbJ.Enabled = true;
                    gbK.Enabled = true;
                    gbL.Enabled = true;

                    if (valSC.Checked != true)
                    {
                        gbE.Enabled = false;
                        gbF.Enabled = false;
                        gbG.Enabled = false;
                        gbH.Enabled = false;

                        gbI.Enabled = true;
                        gbJ.Enabled = true;
                        gbK.Enabled = true;
                        gbL.Enabled = true;
                    }
                    //else
                    //{
                    //    gbE.Enabled = true;
                    //    gbF.Enabled = true;
                    //    gbG.Enabled = true;
                    //    gbH.Enabled = true;
                    //}

                }
                else if (tagCtrl.v_ComponentFieldsId == "N009-MF000003530")
                {
                    gbE.Enabled = true;
                    gbF.Enabled = true;
                    gbG.Enabled = true;
                    gbH.Enabled = true;

                    gbI.Enabled = true;
                    gbJ.Enabled = true;
                    gbK.Enabled = true;
                    gbL.Enabled = true;

                    if (valSI.Checked != true)
                    {
                        gbI.Enabled = false;
                        gbJ.Enabled = false;
                        gbK.Enabled = false;
                        gbL.Enabled = false;

                        gbE.Enabled = true;
                        gbF.Enabled = true;
                        gbG.Enabled = true;
                        gbH.Enabled = true;
                    }
                    //else
                    //{
                    //    gbI.Enabled = true;
                    //    gbJ.Enabled = true;
                    //    gbK.Enabled = true;
                    //    gbL.Enabled = true;
                    //}
                }


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
                string valueToAnalyze = GetValueControl(tagCtrl.i_ControlId, senderCtrl);
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

                GeneratedAutoDX(valueToAnalyze, senderCtrl, tagCtrl);

            }

        }

        #endregion

        private Control[] FindControlInCurrentTab(string key)
        {
            // Obtener TabPage actual
            var currentTabPage = tcExamList.SelectedTab.TabPage;
            var findControl = currentTabPage.Controls.Find(key, true);
            return findControl;
        }
        
        //private void EditTotalDiagnosticos()
        //{
        //    if (grdTotalDiagnosticos.Selected.Rows.Count == 0)return;
        //    _rowIndex = grdTotalDiagnosticos.Selected.Rows[0].Cells["v_DiagnosticRepositoryId"].Row.Index;
        //    var diagnosticRepositoryId = grdTotalDiagnosticos.Selected.Rows[0].Cells["v_DiagnosticRepositoryId"].Value.ToString();
        //    var objOperationResult = new OperationResult();
        //    _tmpTotalDiagnostic = new ServiceBL().GetServiceComponentTotalDiagnostics(ref objOperationResult, diagnosticRepositoryId);
        //    _componentIdConclusionesDX = _tmpTotalDiagnostic.v_ComponentId;
        //    if (_tmpTotalDiagnostic.i_FinalQualificationId != null)
        //    {
        //        var califiFinalId = (FinalQualification)_tmpTotalDiagnostic.i_FinalQualificationId;
        //        lblDiagnostico.Text = _tmpTotalDiagnostic.v_DiseasesName;
        //        cbCalificacionFinal.SelectedValue = califiFinalId == FinalQualification.SinCalificar ? ((int)FinalQualification.Presuntivo).ToString() : ((int)califiFinalId).ToString();
        //        //cbTipoDx.SelectedValue = _tmpTotalDiagnostic.i_DiagnosticTypeId == null ? ((int)TipoDx.Otros).ToString() : _tmpTotalDiagnostic.i_DiagnosticTypeId.ToString();
        //        cbTipoDx.SelectedValue = _tmpTotalDiagnostic.i_DiagnosticTypeId == null ? "-1" : _tmpTotalDiagnostic.i_DiagnosticTypeId.ToString();
        //        cbEnviarAntecedentes.SelectedValue = _tmpTotalDiagnostic.i_IsSentToAntecedent == null ? ((int)SiNo.NO).ToString() : _tmpTotalDiagnostic.i_IsSentToAntecedent.ToString();
        //        califiFinalId = califiFinalId == FinalQualification.SinCalificar ? FinalQualification.Presuntivo : califiFinalId;

        //        if (btnAceptarDX.Enabled)
        //        {
        //            #region Validaciones

        //            if (califiFinalId == FinalQualification.Definitivo
        //                || califiFinalId == FinalQualification.Presuntivo)
        //            {
        //                cbTipoDx.Enabled = true;
        //                cbEnviarAntecedentes.Enabled = true;
        //                dtpFechaVcto.Enabled = true;
        //            }
        //            else
        //            {
        //                cbTipoDx.Enabled = false;
        //                cbEnviarAntecedentes.Enabled = false;
        //                dtpFechaVcto.Enabled = false;
        //            }

        //            if (_tmpTotalDiagnostic.v_ComponentId == null)
        //                btnAgregarDX.Enabled = true;
        //            else
        //                btnAgregarDX.Enabled = false;

        //            #endregion

        //        }
        //    }

        //    if (_tmpTotalDiagnostic.d_ExpirationDateDiagnostic != null)
        //            dtpFechaVcto.Value = _tmpTotalDiagnostic.d_ExpirationDateDiagnostic.Value;

        //        _tmpRestrictionList = _tmpTotalDiagnostic.Restrictions;
        //        _tmpRecomendationList = _tmpTotalDiagnostic.Recomendations;

        //        // Cargar grilla Restricciones
        //        grdRestricciones_AnalisisDiagnostico.DataSource = new RestrictionList();
        //        grdRestricciones_AnalisisDiagnostico.DataSource = _tmpRestrictionList;
        //        grdRestricciones_AnalisisDiagnostico.Refresh();
        //        lblRecordCountRestricciones_AnalisisDx.Text = string.Format("Se encontraron {0} registros.", _tmpRestrictionList.Count());

        //        // Cargar grilla Recomendaciones
        //        grdRecomendaciones_AnalisisDiagnostico.DataSource = new RecomendationList();
        //        grdRecomendaciones_AnalisisDiagnostico.DataSource = _tmpRecomendationList;
        //        grdRecomendaciones_AnalisisDiagnostico.Refresh();
        //        lblRecordCountRecomendaciones_AnalisisDx.Text = string.Format("Se encontraron {0} registros.", _tmpRecomendationList.Count());

        //        gbEdicionDiagnosticoTotal.Enabled = true;
        //}

        private void EditTotalDiagnosticos()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(EditTotalDiagnosticos));
            }
            else
            {
                if (grdTotalDiagnosticos.Selected.Rows.Count == 0)
                    return;

                _rowIndex = grdTotalDiagnosticos.Selected.Rows[0].Cells["v_DiagnosticRepositoryId"].Row.Index;

                // Capturar ID para buscar en BD
                string diagnosticRepositoryId = grdTotalDiagnosticos.Selected.Rows[0].Cells["v_DiagnosticRepositoryId"].Value.ToString();

                OperationResult objOperationResult = new OperationResult();
                _tmpTotalDiagnostic = _serviceBL.GetServiceComponentTotalDiagnostics(ref objOperationResult, diagnosticRepositoryId);

                _componentIdConclusionesDX = _tmpTotalDiagnostic.v_ComponentId;

                var califiFinalId = (FinalQualification)_tmpTotalDiagnostic.i_FinalQualificationId;

                // Alejandro: Mostar data
                lblDiagnostico.Text = _tmpTotalDiagnostic.v_DiseasesName;
                cbCalificacionFinal.SelectedValue = califiFinalId == FinalQualification.SinCalificar ? ((int)FinalQualification.Presuntivo).ToString() : ((int)califiFinalId).ToString();
                cbTipoDx.SelectedValue = _tmpTotalDiagnostic.i_DiagnosticTypeId == null ? ((int)TipoDx.Otros).ToString() : _tmpTotalDiagnostic.i_DiagnosticTypeId.ToString();
                cbEnviarAntecedentes.SelectedValue = _tmpTotalDiagnostic.i_IsSentToAntecedent == null ? ((int)SiNo.NO).ToString() : _tmpTotalDiagnostic.i_IsSentToAntecedent.ToString();

                // Alejandro: Set dx presuntivo x default si el estado es SinCalificar
                califiFinalId = califiFinalId == FinalQualification.SinCalificar ? FinalQualification.Presuntivo : califiFinalId;

                if (btnAceptarDX.Enabled)
                {
                    #region Validaciones

                    if (califiFinalId == FinalQualification.Definitivo
                            || califiFinalId == FinalQualification.Presuntivo)
                    {
                        cbTipoDx.Enabled = true;
                        cbEnviarAntecedentes.Enabled = true;
                        dtpFechaVcto.Enabled = true;
                    }
                    else
                    {
                        cbTipoDx.Enabled = false;
                        cbEnviarAntecedentes.Enabled = false;
                        dtpFechaVcto.Enabled = false;
                    }

                    if (_tmpTotalDiagnostic.v_ComponentId == null)
                        btnAgregarDX.Enabled = true;
                    else
                        btnAgregarDX.Enabled = false;

                    //var automaticManual = (AutoManual)_tmpTotalDiagnostic.i_AutoManualId;
                    //switch (automaticManual)
                    //{
                    //    case AutoManual.Automático:
                    //        btnAgregarDX.Enabled = false;
                    //        break;
                    //    case AutoManual.Manual:
                    //        btnAgregarDX.Enabled = true;
                    //        break;
                    //    default:
                    //        break;
                    //}

                    #endregion

                }

                if (_tmpTotalDiagnostic.d_ExpirationDateDiagnostic != null)
                {
                    //dtpFechaVcto.Checked = true;
                    dtpFechaVcto.Value = _tmpTotalDiagnostic.d_ExpirationDateDiagnostic.Value;
                }


                // refrescar mis listas temporales [Restricciones / Recomendaciones] con data k viene de BD
                _tmpRestrictionList = _tmpTotalDiagnostic.Restrictions;
                _tmpRecomendationList = _tmpTotalDiagnostic.Recomendations;

                // Cargar grilla Restricciones
                grdRestricciones_AnalisisDiagnostico.DataSource = new RestrictionList();
                grdRestricciones_AnalisisDiagnostico.DataSource = _tmpRestrictionList;
                grdRestricciones_AnalisisDiagnostico.Refresh();
                lblRecordCountRestricciones_AnalisisDx.Text = string.Format("Se encontraron {0} registros.", _tmpRestrictionList.Count());

                // Cargar grilla Recomendaciones
                grdRecomendaciones_AnalisisDiagnostico.DataSource = new RecomendationList();
                grdRecomendaciones_AnalisisDiagnostico.DataSource = _tmpRecomendationList;
                grdRecomendaciones_AnalisisDiagnostico.Refresh();
                lblRecordCountRecomendaciones_AnalisisDx.Text = string.Format("Se encontraron {0} registros.", _tmpRecomendationList.Count());

                gbEdicionDiagnosticoTotal.Enabled = true;

            }

        }

        private void grdTotalDiagnosticos_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            EditTotalDiagnosticos();
            var flag = false;
            if (btnAceptarDX.Enabled)
            {
                if (((UltraGrid)sender).Selected.Rows.Count != 0)
                {
                    var componentId = ((UltraGrid)sender).Selected.Rows[0].Cells["v_ComponentId"].Value;
                    flag = componentId == null;
                }
            }
            btnRemoverTotalDiagnostico.Enabled = (grdTotalDiagnosticos.Selected.Rows.Count > 0 && _removerTotalDiagnostico && flag);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGuardarAnamnesis_Click(object sender, EventArgs e)
        {
            if (uvAnamnesis.Validate(true, false).IsValid)
            {
                var result = MessageBox.Show(@"¿Está seguro de grabar este registro?:", @"CONFIRMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes) return;

                #region Cargar Entidad
                var serviceDto = new serviceDto
                {
                    v_ServiceId = _serviceId,
                    v_MainSymptom = chkPresentaSisntomas.Checked ? txtSintomaPrincipal.Text : null,
                    i_TimeOfDisease = chkPresentaSisntomas.Checked ? int.Parse(txtValorTiempoEnfermedad.Text) : (int?)null,
                    i_TimeOfDiseaseTypeId = chkPresentaSisntomas.Checked ? int.Parse(cbCalendario.SelectedValue.ToString()) : -1,
                    v_Story = txtRelato.Text,
                    i_DreamId = int.Parse(cbSueño.SelectedValue.ToString()),
                    i_UrineId = int.Parse(cbOrina.SelectedValue.ToString()),
                    i_DepositionId = int.Parse(cbDeposiciones.SelectedValue.ToString()),
                    v_Findings = txtHallazgos.Text,
                    i_AppetiteId = int.Parse(cbApetito.SelectedValue.ToString()),
                    i_ThirstId = int.Parse(cbSed.SelectedValue.ToString()),
                    d_Fur = dtpFur.Checked ? dtpFur.Value : (DateTime?)null,
                    v_CatemenialRegime = txtRegimenCatamenial.Text,
                    i_MacId = int.Parse(cbMac.SelectedValue.ToString()),
                    i_HasSymptomId = Convert.ToInt32(chkPresentaSisntomas.Checked),
                    d_PAP = dtpPAP.Checked ? dtpPAP.Value : (DateTime?)null,
                    d_Mamografia = dtpMamografia.Checked ? dtpMamografia.Value : (DateTime?)null,
                    v_Gestapara = txtGestapara.Text,
                    v_Menarquia = txtMenarquia.Text,
                    v_CiruGine = txtCiruGine.Text,
                    i_AptitudeStatusId = int.Parse(cbAptitudEso.SelectedValue.ToString()),
                    v_InicioVidaSexaul = txtVidaSexual.Text,
                    v_NroParejasActuales = txtNroParejasActuales.Text,
                    v_NroAbortos = txtNroAbortos.Text,
                    v_PrecisarCausas = txtNroCausa.Text

                };
                #endregion

                Task.Factory.StartNew(() =>
                {
                    new ServiceBL().UpdateAnamnesis(ref _objOperationResult, serviceDto, Globals.ClientSession.GetAsList());
                },TaskCreationOptions.LongRunning);
            }
            else
            {
                MessageBox.Show(@"Por favor corrija la información ingresada. Vea los indicadores de error.", @"Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tcExamList_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
        {
            _examName = e.Tab.Text;

            EXAMENES_lblComentarios.Text = string.Format("Comentarios de {0}", _examName);
            EXAMENES_lblEstadoComponente.Text = string.Format("Estado del exámen ({0})", _examName);
            btnGuardarExamen.Text = string.Format("&Guardar ({0})", _examName);
            _componentId = e.Tab.Key;
            _serviceComponentId = e.Tab.Tag.ToString();
            LoadDataBySelectedComponent(_componentId, _serviceComponentId);
           
        }

        private void LoadDataBySelectedComponent(string componentId, string serviceComponentId)
        {
            SetSecurityByComponentC(componentId);
            PopulateGridDiagnostic(componentId);
            PaintGrdDiagnosticoPorExamenComponente();
            PopulateDataExam(serviceComponentId);
        }

        private void SearchControlAndSetValue_New(Control ctrlContainer)
        {
                KeyTagControl keyTagControl = null;
                bool breakHazChildrenUC = false;
                List<ServiceComponentFieldValuesList> dataSourceUserControls = null;


                ValidacionAMC oValidacionAMC = null;
                var x = _serviceComponentsInfo.ServiceComponentFields;
                var y = x.Select(p => p.ServiceComponentFieldValues).ToList();
                _oListValidacionAMC = new List<ValidacionAMC>();
                foreach (var item in y)
                {
                    oValidacionAMC = new ValidacionAMC();
                    oValidacionAMC.v_ServiceComponentFieldValuesId = item[0].v_ServiceComponentFieldValuesId;
                    oValidacionAMC.v_Value1 = item[0].v_Value1;
                    oValidacionAMC.v_ComponentFieldsId = item[0].v_ComponentFieldId;
                    _oListValidacionAMC.Add(oValidacionAMC);
                }

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
                            else if (keyTagControl.i_ControlId == (int)ControlType.UcSomnolencia)
                            {
                                #region Setear valores en udiometria

                                dataSourceUserControls = _serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("SOM"));
                                ((UserControls.ucSomnolencia)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucSomnolencia)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUC = true;

                                #endregion
                            }
                            else if (keyTagControl.i_ControlId == (int)ControlType.UcAcumetria)
                            {
                                #region Setear valores en udiometria

                                dataSourceUserControls = _serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("ACU"));
                                ((UserControls.ucAcumetria)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucAcumetria)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUC = true;

                                #endregion
                            }
                            else if (keyTagControl.i_ControlId == (int)ControlType.UcFototipo)
                            {
                                #region Setear valores en FotoTipo

                                dataSourceUserControls = _serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("FOT"));
                                var multimediaFileId = dataSourceUserControls.Find(xp => xp.v_ComponentFieldId == Constants.txt_MULTIMEDIA_FILE_FOTO_TIPO).v_Value1;
                                ServiceBL oServiceBl = new ServiceBL();
                                var a = oServiceBl.ObtenerImageMultimedia(multimediaFileId);
                                ((UserControls.ucFotoTipo)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                dataSourceUserControls.Add(new ServiceComponentFieldValuesList() { fotoTipo = a });
                                ((UserControls.ucFotoTipo)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUC = true;

                                #endregion
                            }
                            else if (keyTagControl.i_ControlId == (int)ControlType.UcSintomaticoRespi)
                            {
                                #region Setear valores en udiometria

                                dataSourceUserControls = _serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("RES"));
                                ((UserControls.ucSintomaticoResp)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucSintomaticoResp)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUC = true;

                                #endregion
                            }
                            else if (keyTagControl.i_ControlId == (int)ControlType.UcRxLumboSacra)
                            {
                                #region Setear valores en udiometria

                                dataSourceUserControls = _serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("RXL"));
                                ((UserControls.ucRXLumboSacra)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucRXLumboSacra)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUC = true;

                                #endregion
                            }
                            else if (keyTagControl.i_ControlId == (int)ControlType.UcOjoSeco)
                            {
                                #region Setear valores en udiometria

                                dataSourceUserControls = _serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("OJS"));
                                ((UserControls.ucOjoSeco)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucOjoSeco)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUC = true;

                                #endregion
                            }
                            else if (keyTagControl.i_ControlId == (int)ControlType.UcOtoscopia)
                            {
                                #region Setear valores en udiometria

                                dataSourceUserControls = _serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("OTO"));
                                ((UserControls.ucOtoscopia)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucOtoscopia)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUC = true;

                                #endregion
                            }
                            else if (keyTagControl.i_ControlId == (int)ControlType.UcEvaluacionErgonomica)
                            {
                                #region Setear valores en udiometria

                                dataSourceUserControls = _serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("EVA"));
                                ((UserControls.ucEvaluacionErgonomica)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucEvaluacionErgonomica)ctrl).DataSource = dataSourceUserControls;
                                breakHazChildrenUC = true;

                                #endregion
                            }
                            else if (keyTagControl.i_ControlId == (int)ControlType.UcOsteoMuscular)
                            {
                                #region Setear valores en udiometria

                                dataSourceUserControls = _serviceComponentsInfo.ServiceComponentFields.SelectMany(p => p.ServiceComponentFieldValues).ToList();
                                dataSourceUserControls = dataSourceUserControls.FindAll(p => p.v_ComponentFieldId.Contains("OTS"));
                                ((UserControls.ucOsteoMuscular)ctrl).DataSource = new List<ServiceComponentFieldValuesList>();
                                ((UserControls.ucOsteoMuscular)ctrl).DataSource = dataSourceUserControls;
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
                            SearchControlAndSetValue_New(ctrl);
                        }
                    }
                }

            

        }

        private void LoadDataBySelectedComponent_New(string pstrComponentId)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(LoadDataBySelectedComponent_New), pstrComponentId);
            }
            else
            {
                var arrComponentId = _componentId.Split('|');

                if (arrComponentId.Contains(Constants.AUDIOMETRIA_ID)//audiometría
                    || arrComponentId.Contains("N009-ME000000337")
                    || arrComponentId.Contains(Constants.AUDIO_COIMOLACHE)

                    || arrComponentId.Contains(Constants.APENDICE_ID)//cardiologia
                    || arrComponentId.Contains(Constants.ELECTROCARDIOGRAMA_ID)
                    || arrComponentId.Contains(Constants.ELECTRO_GOLD)
                    || arrComponentId.Contains(Constants.PRUEBA_ESFUERZO_ID)

                    || arrComponentId.Contains(Constants.ESPIROMETRIA_ID)//espirometría

                    || arrComponentId.Contains(Constants.INFORME_LABORATORIO_ID)//laboratorio
                    || arrComponentId.Contains(Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID)

                    || arrComponentId.Contains(Constants.EXAMEN_FISICO_7C_ID)//medicina
                    || arrComponentId.Contains(Constants.EXAMEN_FISICO_ID)
                    || arrComponentId.Contains(Constants.ATENCION_INTEGRAL_ID)
                    || arrComponentId.Contains(Constants.C_N_ID)//cuestionario nordico
                    || arrComponentId.Contains(Constants.FICHA_SAS_ID)
                    || arrComponentId.Contains(Constants.EVA_ERGONOMICA_ID)
                    || arrComponentId.Contains(Constants.EVA_NEUROLOGICA_ID)
                    || arrComponentId.Contains(Constants.ALTURA_7D_ID)
                    || arrComponentId.Contains(Constants.ALTURA_ESTRUCTURAL_ID)
                    || arrComponentId.Contains(Constants.EXAMEN_SUF_MED__OPERADORES_ID)
                    || arrComponentId.Contains(Constants.OSTEO_MUSCULAR_ID_1)
                    || arrComponentId.Contains(Constants.FOTO_TIPO_ID)
                    || arrComponentId.Contains(Constants.OSTEO_COIMO)
                    || arrComponentId.Contains(Constants.SINTOMATICO_ID)
                    || arrComponentId.Contains(Constants.FICHA_SUFICIENCIA_MEDICA_ID)
                    || arrComponentId.Contains(Constants.TAMIZAJE_DERMATOLOGIO_ID)
                    || arrComponentId.Contains(Constants.TEST_VERTIGO_ID)
                    || arrComponentId.Contains(Constants.EVA_OSTEO_ID)

                    || arrComponentId.Contains(Constants.ODONTOGRAMA_ID)//odontologia

                    || arrComponentId.Contains(Constants.OFTALMOLOGIA_ID)//oftalmologia
                    || arrComponentId.Contains(Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID)
                    || arrComponentId.Contains(Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID)
                    || arrComponentId.Contains(Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID)
                    || arrComponentId.Contains(Constants.INFORME_OFTALMOLOGICO_HUDBAY_ID)
                    || arrComponentId.Contains(Constants.PETRINOVIC_ID)
                    || arrComponentId.Contains(Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_ID)
                    || arrComponentId.Contains("N009-ME000000437")

                    //|| arrComponentId.Contains(Constants.PSICOLOGIA_ID)//psicologia
                    //|| arrComponentId.Contains(Constants.HISTORIA_CLINICA_PSICOLOGICA_ID)
                    //|| arrComponentId.Contains(Constants.FICHA_PSICOLOGICA_OCUPACIONAL_GOLDFIELDS)
                    //|| arrComponentId.Contains(Constants.INFORME_PSICOLOGICO_OCUPACIONAL_GOLDFIELDS)
                    //|| arrComponentId.Contains(Constants.SOMNOLENCIA_ID)

                    || arrComponentId.Contains(Constants.OIT_ID)//rayos x
                    || arrComponentId.Contains(Constants.RX_TORAX_ID)
                    || arrComponentId.Contains(Constants.EXCEPCIONES_RX_ID)
                    || arrComponentId.Contains(Constants.EXCEPCIONES_RX_AUTORIZACION_ID)

                    || arrComponentId.Contains(Constants.LUMBOSACRA_ID)
                    || arrComponentId.Contains(Constants.ELECTROENCEFALOGRAMA_ID)
                    || arrComponentId.Contains("N009-ME000000302")
                    )
                {
                    btnVisorReporteExamen.Text = string.Format("&Ver Reporte de ({0})", _examName);
                    btnVisorReporteExamen.Visible = true;
                }
                else
                {
                    btnVisorReporteExamen.Visible = false;
                }


                OperationResult objOperationResult = new OperationResult();

                if (_serviceComponentsInfo != null)
                    _serviceComponentsInfo = null;

                // Mostrar data de serviceComponent
                _serviceComponentsInfo = _serviceBL.GetServiceComponentsInfo(ref objOperationResult, _serviceComponentId, _serviceId);






                if (_serviceComponentsInfo != null)
                {
                    txtComentario.Text = _serviceComponentsInfo.v_Comment;
                    cbEstadoComponente.SelectedValue = _serviceComponentsInfo.i_ServiceComponentStatusId == (int)ServiceComponentStatus.PorIniciar ? ((int)ServiceComponentStatus.Iniciado).ToString() : _serviceComponentsInfo.i_ServiceComponentStatusId.ToString();
                    //_EstadoComponente = _serviceComponentsInfo.i_ServiceComponentStatusId == (int)ServiceComponentStatus.PorIniciar ? ((int)ServiceComponentStatus.Iniciado) : _serviceComponentsInfo.i_ServiceComponentStatusId;
                    cbTipoProcedenciaExamen.SelectedValue = _serviceComponentsInfo.i_ExternalInternalId == null ? "1" : _serviceComponentsInfo.i_ExternalInternalId.ToString();
                    chkApproved.Checked = Convert.ToBoolean(_serviceComponentsInfo.i_IsApprovedId);


                    #region Permisos de lectura / Escritura x componente de acuerdo al rol del usuario

                    SetSecurityByComponent();

                    #endregion

                    if (_serviceComponentsInfo.ServiceComponentFields.Count != 0)
                    {
                        // Flag para disparar el evento del selectedIndexChange luego de setear los valores x default
                        _cancelEventSelectedIndexChange = true;
                        // Llenar valores            
                        SearchControlAndSetValue_New(tcExamList.SelectedTab.TabPage);

                        _cancelEventSelectedIndexChange = false;
                    }
                    else
                    {
                        // Setear valores x defecto configurados en BD            
                        SetDefaultValueBySelectedTab();
                    }

                    // Setear campos de auditoria
                    //tslUsuarioCrea.Text = string.Format("Usuario Crea : {0}", _serviceComponentsInfo.v_CreationUser);
                    //tslFechaCrea.Text = string.Format("Fecha Crea : {0} {1}", _serviceComponentsInfo.d_CreationDate.Value.ToShortDateString(), _serviceComponentsInfo.d_CreationDate.Value.ToShortTimeString());
                    //tslUsuarioAct.Text = string.Format("Usuario Act : {0}", _serviceComponentsInfo.v_UpdateUser == null ? string.Empty : _serviceComponentsInfo.v_UpdateUser);
                    //tslFechaAct.Text = string.Format("Fecha Act : {0} {1}", _serviceComponentsInfo.d_UpdateDate == null ? string.Empty : _serviceComponentsInfo.d_UpdateDate.Value.ToShortDateString(), _serviceComponentsInfo.d_UpdateDate == null ? string.Empty : _serviceComponentsInfo.d_UpdateDate.Value.ToShortTimeString());

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
                    grdDiagnosticoPorExamenComponente.DataSource = diagnosticList;
                    lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", diagnosticList.Count());

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
                    grdDiagnosticoPorExamenComponente.DataSource = new DiagnosticRepositoryList();
                    lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", 0);

                }

            }

            //if (tcExamList.SelectedTab.TabPage.Tab.Text == "MEDICINA")
            //{
            //    string pstrPersonId = _personId;

            //    Popups.frmPopupAntecedentes frm = new Popups.frmPopupAntecedentes(pstrPersonId);
            //    frm.ShowDialog();
            //}

        }

        private void PopulateGridDiagnostic(string componentId)
        {
            ClearGridDiagnostic();
            List<DiagnosticRepositoryList> diagnosticList = null;
            Task.Factory.StartNew(() =>
            {
                diagnosticList = new ServiceBL().GetServiceComponentDisgnosticsForGridView(ref _objOperationResult, _serviceId, componentId);
            }).ContinueWith(t =>
            {
                if (!diagnosticList.Any()) return;
                _tmpExamDiagnosticComponentList = diagnosticList;
                grdDiagnosticoPorExamenComponente.DataSource = diagnosticList;
                lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", diagnosticList.Count);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ClearGridDiagnostic()
        {
            _tmpExamDiagnosticComponentList = null;
            grdDiagnosticoPorExamenComponente.DataSource = new List<DiagnosticRepositoryList>();
            lblRecordCountDiagnosticoPorExamenCom.Text = @"Se encontraron 0 registros.";
        }

        private void PopulateDataExam(string serviceComponentId)
        {
           
            ServiceComponentList serviceComponentsInfo = null;
            Task.Factory.StartNew(() =>
            {
                serviceComponentsInfo =  new ServiceBL().GetServiceComponentsInfo(ref _objOperationResult, serviceComponentId, _serviceId);

            }).ContinueWith(t =>
            {
                
                if (serviceComponentsInfo == null) return;
                txtComentario.Text = serviceComponentsInfo.v_Comment;
                cbEstadoComponente.SelectedValue = serviceComponentsInfo.i_ServiceComponentStatusId == (int)ServiceComponentStatus.PorIniciar ? ((int)ServiceComponentStatus.Iniciado).ToString() : serviceComponentsInfo.i_ServiceComponentStatusId.ToString();
                cbTipoProcedenciaExamen.SelectedValue = serviceComponentsInfo.i_ExternalInternalId == null ? "1" : serviceComponentsInfo.i_ExternalInternalId.ToString();
                chkApproved.Checked = Convert.ToBoolean(serviceComponentsInfo.i_IsApprovedId);
                if (serviceComponentsInfo.d_UpdateDate != null)
                    lblUsuGraba.Text = serviceComponentsInfo.v_UpdateUser == null ? "" : serviceComponentsInfo.v_UpdateUser.ToUpper();
                if (serviceComponentsInfo.d_UpdateDate != null)
                    lblFechaGraba.Text = serviceComponentsInfo.d_UpdateDate == null ? "": serviceComponentsInfo.d_UpdateDate.Value.ToString("dd-MMMM-yyyy (hh:mm) ");
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void TsmverMas_Click(object sender, EventArgs e)
        {
            lblView_Click_1(sender, e);
        }

        private void lblTrabajador_Click(object sender, EventArgs e)
        {
            var frmWorkerData = new FrmWorkerData(_serviceId);
            frmWorkerData.ShowDialog();
        }

        private void lblProtocolName_Click(object sender, EventArgs e)
        {
            var frmWorkerData = new FrmWorkerData(_serviceId);
            frmWorkerData.ShowDialog();
        }

        private void grdDiagnosticoPorExamenComponente_ClickCell(object sender, ClickCellEventArgs e)
        {
            ValidateRemoveDxAutomatic();
        }

        private void grdDiagnosticoPorExamenComponente_AfterPerformAction(object sender, AfterUltraGridPerformActionEventArgs e)
        {
            ValidateRemoveDxAutomatic();
        }

        private void ValidateRemoveDxAutomatic()
        {
            if (grdDiagnosticoPorExamenComponente.Selected.Rows.Count <= 0) return;
            if (!_isDisabledButtonsExamDx) return;
            var autoManualId = (AutoManual)grdDiagnosticoPorExamenComponente.Selected.Rows[0].Cells["i_AutoManualId"].Value;
            switch (autoManualId)
            {
                case AutoManual.Automático:
                    btnRemoverDxExamen.Enabled = false;
                    break;
                case AutoManual.Manual:
                    btnRemoverDxExamen.Enabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetSecurityByComponent()
        {
            OperationResult objOperationResult = new OperationResult();

            var nodeId = Globals.ClientSession.i_CurrentExecutionNodeId;
            var roleId = Globals.ClientSession.i_RoleId.Value;

            bool isReadOnly = false;
            bool isWriteOnly = false;

            // Obtener campos de un componente especifico
            var componentFields = _tmpServiceComponentsForBuildMenuList.Find(p => p.v_ComponentId == _componentId).Fields;
            // Obtener permisos de cada examen de un rol especifico
            var componentProfile = new ServiceBL().GetRoleNodeComponentProfile(ref objOperationResult, nodeId, roleId, _componentId);

            if (componentProfile != null)
            {
                if (componentProfile.i_Read == (int)SiNo.SI && componentProfile.i_Write == (int)SiNo.SI)
                {
                    isReadOnly = false;
                    btnGuardarExamen.Enabled = true;
                }
                else
                {
                    isReadOnly = true;
                    btnGuardarExamen.Enabled = false;
                }

                if (componentProfile.i_Write == (int)SiNo.SI)
                {
                    isWriteOnly = true;
                    btnGuardarExamen.Enabled = true;
                }
                else
                {
                    isWriteOnly = false;
                    btnGuardarExamen.Enabled = false;
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
                                if (_action == "View")
                                {
                                    txtt.ReadOnly = true;
                                }
                                break;
                            case ControlType.CadenaMultilinea:
                                TextBox txtm = (TextBox)ctrl__[0];
                                txtm.CreateControl();
                                txtm.ReadOnly = isReadOnly;
                                if (_action == "View")
                                {
                                    txtm.ReadOnly = true;
                                }
                                break;
                            case ControlType.NumeroEntero:
                                UltraNumericEditor uni = (UltraNumericEditor)ctrl__[0];
                                uni.CreateControl();
                                uni.ReadOnly = isReadOnly;
                                if (_action == "View")
                                {
                                    uni.ReadOnly = true;
                                }
                                break;
                            case ControlType.NumeroDecimal:
                                UltraNumericEditor und = (UltraNumericEditor)ctrl__[0];
                                und.CreateControl();
                                und.ReadOnly = isReadOnly;
                                if (_action == "View")
                                {
                                    und.ReadOnly = true;
                                }
                                break;
                            case ControlType.SiNoCheck:
                                CheckBox chkSiNo = (CheckBox)ctrl__[0];
                                chkSiNo.CreateControl();
                                chkSiNo.Enabled = isWriteOnly;
                                if (_action == "View")
                                {
                                    chkSiNo.Enabled = false;
                                }
                                break;
                            case ControlType.SiNoRadioButton:
                                RadioButton rbSiNo = (RadioButton)ctrl__[0];
                                rbSiNo.CreateControl();
                                rbSiNo.Enabled = isWriteOnly;
                                if (_action == "View")
                                {
                                    rbSiNo.Enabled = false;
                                }
                                break;
                            case ControlType.Radiobutton:
                                RadioButton rb = (RadioButton)ctrl__[0];
                                rb.CreateControl();
                                rb.Enabled = isWriteOnly;
                                if (_action == "View")
                                {
                                    rb.Enabled = false;
                                }
                                break;
                            case ControlType.SiNoCombo:
                                ComboBox cbSiNo = (ComboBox)ctrl__[0];
                                cbSiNo.CreateControl();
                                cbSiNo.Enabled = isWriteOnly;
                                if (_action == "View")
                                {
                                    cbSiNo.Enabled = false;
                                }
                                break;
                            case ControlType.UcFileUpload:
                                break;
                            case ControlType.Lista:
                                ComboBox cbList = (ComboBox)ctrl__[0];
                                cbList.CreateControl();
                                cbList.Enabled = isWriteOnly;
                                if (_action == "View")
                                {
                                    cbList.Enabled = false;
                                }
                                break;
                            default:
                                break;
                        }

                        #endregion
                    }
                }

                #endregion

                #region Es Diagnosticable

                if (componentProfile.i_Dx == (int)SiNo.SI)
                {
                    btnAgregarDxExamen.Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_EXAMENES_ADDDX", _formActions);
                    btnEditarDxExamen.Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_EXAMENES_EDITDX", _formActions);
                    btnRemoverDxExamen.Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_EXAMENES_REMOVEDX", _formActions);

                }

                if (componentProfile.i_Dx == (int)SiNo.NO)
                {
                    btnAgregarDxExamen.Enabled = false;
                    btnEditarDxExamen.Enabled = false;
                    btnRemoverDxExamen.Enabled = false;
                }

                #endregion

                #region Es Aprobable?

                if (componentProfile.i_Approved == (int)SiNo.NO)
                {
                    chkApproved.Enabled = false;
                }
                else if (componentProfile.i_Approved == (int)SiNo.SI)
                {
                    chkApproved.Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEso_EXAMENES_APPROVED", _formActions);
                }

                #endregion

            }
            else
            {
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
                                txtt.ReadOnly = true;
                                if (_action == "View")
                                {
                                    txtt.ReadOnly = true;
                                }
                                break;
                            case ControlType.CadenaMultilinea:
                                TextBox txtm = (TextBox)ctrl__[0];
                                txtm.CreateControl();
                                txtm.ReadOnly = true;
                                if (_action == "View")
                                {
                                    txtm.ReadOnly = true;
                                }
                                break;
                            case ControlType.NumeroEntero:
                                UltraNumericEditor uni = (UltraNumericEditor)ctrl__[0];
                                uni.CreateControl();
                                uni.ReadOnly = true;
                                if (_action == "View")
                                {
                                    uni.ReadOnly = true;
                                }
                                break;
                            case ControlType.NumeroDecimal:
                                UltraNumericEditor und = (UltraNumericEditor)ctrl__[0];
                                und.CreateControl();
                                und.ReadOnly = true;
                                if (_action == "View")
                                {
                                    und.ReadOnly = true;
                                }
                                break;
                            case ControlType.SiNoCheck:
                                CheckBox chkSiNo = (CheckBox)ctrl__[0];
                                chkSiNo.CreateControl();
                                chkSiNo.Enabled = false;
                                if (_action == "View")
                                {
                                    chkSiNo.Enabled = false;
                                }
                                break;
                            case ControlType.SiNoRadioButton:
                                RadioButton rbSiNo = (RadioButton)ctrl__[0];
                                rbSiNo.CreateControl();
                                rbSiNo.Enabled = false;
                                if (_action == "View")
                                {
                                    rbSiNo.Enabled = false;
                                }
                                break;
                            case ControlType.Radiobutton:
                                RadioButton rb = (RadioButton)ctrl__[0];
                                rb.CreateControl();
                                rb.Enabled = isWriteOnly;
                                if (_action == "View")
                                {
                                    rb.Enabled = false;
                                }
                                break;
                            case ControlType.SiNoCombo:
                                ComboBox cbSiNo = (ComboBox)ctrl__[0];
                                cbSiNo.CreateControl();
                                cbSiNo.Enabled = false;
                                if (_action == "View")
                                {
                                    cbSiNo.Enabled = false;
                                }
                                break;
                            case ControlType.UcFileUpload:
                                break;
                            case ControlType.Lista:
                                ComboBox cbList = (ComboBox)ctrl__[0];
                                cbList.CreateControl();
                                cbList.Enabled = false;
                                if (_action == "View")
                                {
                                    cbList.Enabled = false;
                                }
                                break;
                            default:
                                break;
                        }

                        #endregion
                    }
                }

                #endregion

                btnGuardarExamen.Enabled = false;
                btnAgregarDxExamen.Enabled = false;
                btnEditarDxExamen.Enabled = false;
                btnRemoverDxExamen.Enabled = false;

                // el check se activa o desactiva dependiendo del rol
                chkApproved.Enabled = false;
            }

            if (cbEstadoComponente.SelectedValue.ToString() == ((int)ServiceComponentStatus.Iniciado).ToString())
            {
                btnGuardarExamen.Enabled = false;
                btnVisorReporteExamen.Enabled = false;
            }
            else
            {
                btnGuardarExamen.Enabled = true;
                btnVisorReporteExamen.Enabled = true;
            }


        }
        private void SetSecurityByComponentC(string componentId)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(SetSecurityByComponentC), componentId);
            }
            else
            {
                var arrComponentId = _componentId.Split('|');

                if (arrComponentId.Contains(Constants.AUDIOMETRIA_ID)//audiometría
                    || arrComponentId.Contains("N009-ME000000337")
                    || arrComponentId.Contains(Constants.AUDIO_COIMOLACHE)

                    || arrComponentId.Contains(Constants.APENDICE_ID)//cardiologia
                    || arrComponentId.Contains(Constants.ELECTROCARDIOGRAMA_ID)
                    || arrComponentId.Contains(Constants.ELECTRO_GOLD)
                    || arrComponentId.Contains(Constants.PRUEBA_ESFUERZO_ID)

                    || arrComponentId.Contains(Constants.ESPIROMETRIA_ID)//espirometría

                    || arrComponentId.Contains(Constants.INFORME_LABORATORIO_ID)//laboratorio
                    || arrComponentId.Contains(Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID)

                    || arrComponentId.Contains(Constants.EXAMEN_FISICO_7C_ID)//medicina
                    || arrComponentId.Contains(Constants.EXAMEN_FISICO_ID)
                    || arrComponentId.Contains(Constants.ATENCION_INTEGRAL_ID)
                    || arrComponentId.Contains(Constants.C_N_ID)//cuestionario nordico
                    || arrComponentId.Contains(Constants.FICHA_SAS_ID)
                    || arrComponentId.Contains(Constants.EVA_ERGONOMICA_ID)
                    || arrComponentId.Contains(Constants.EVA_NEUROLOGICA_ID)
                    || arrComponentId.Contains(Constants.ALTURA_7D_ID)
                    || arrComponentId.Contains(Constants.ALTURA_ESTRUCTURAL_ID)
                    || arrComponentId.Contains(Constants.EXAMEN_SUF_MED__OPERADORES_ID)
                    || arrComponentId.Contains(Constants.OSTEO_MUSCULAR_ID_1)
                    || arrComponentId.Contains(Constants.FOTO_TIPO_ID)
                    || arrComponentId.Contains(Constants.OSTEO_COIMO)
                    || arrComponentId.Contains(Constants.SINTOMATICO_ID)
                    || arrComponentId.Contains(Constants.FICHA_SUFICIENCIA_MEDICA_ID)
                    || arrComponentId.Contains(Constants.TAMIZAJE_DERMATOLOGIO_ID)
                    || arrComponentId.Contains(Constants.TEST_VERTIGO_ID)
                    || arrComponentId.Contains(Constants.EVA_OSTEO_ID)

                    || arrComponentId.Contains(Constants.ODONTOGRAMA_ID)//odontologia

                    || arrComponentId.Contains(Constants.OFTALMOLOGIA_ID)//oftalmologia
                    || arrComponentId.Contains(Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID)
                    || arrComponentId.Contains(Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID)
                    || arrComponentId.Contains(Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID)
                    || arrComponentId.Contains(Constants.INFORME_OFTALMOLOGICO_HUDBAY_ID)
                    || arrComponentId.Contains(Constants.PETRINOVIC_ID)
                    || arrComponentId.Contains(Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_ID)
                    || arrComponentId.Contains("N009-ME000000437")

                    || arrComponentId.Contains(Constants.PSICOLOGIA_ID)//psicologia
                    || arrComponentId.Contains(Constants.HISTORIA_CLINICA_PSICOLOGICA_ID)
                    || arrComponentId.Contains(Constants.FICHA_PSICOLOGICA_OCUPACIONAL_GOLDFIELDS)
                    || arrComponentId.Contains(Constants.INFORME_PSICOLOGICO_OCUPACIONAL_GOLDFIELDS)
                    || arrComponentId.Contains(Constants.SOMNOLENCIA_ID)

                    || arrComponentId.Contains(Constants.OIT_ID)//rayos x
                    || arrComponentId.Contains(Constants.RX_TORAX_ID)
                    || arrComponentId.Contains(Constants.EXCEPCIONES_RX_ID)
                    || arrComponentId.Contains(Constants.EXCEPCIONES_RX_AUTORIZACION_ID)

                    || arrComponentId.Contains(Constants.LUMBOSACRA_ID)
                    || arrComponentId.Contains(Constants.ELECTROENCEFALOGRAMA_ID)
                    || arrComponentId.Contains("N009-ME000000302")
                    )
                {
                    btnVisorReporteExamen.Text = string.Format("&Ver Reporte de ({0})", _examName);
                    btnVisorReporteExamen.Visible = true;
                }
                else
                {
                    btnVisorReporteExamen.Visible = false;
                }


                OperationResult objOperationResult = new OperationResult();

                if (_serviceComponentsInfo != null)
                    _serviceComponentsInfo = null;

                // Mostrar data de serviceComponent
                _serviceComponentsInfo = new ServiceBL().GetServiceComponentsInfo(ref objOperationResult, _serviceComponentId, _serviceId);

                
                if (_serviceComponentsInfo != null)
                {
                    txtComentario.Text = _serviceComponentsInfo.v_Comment;
                    cbEstadoComponente.SelectedValue = _serviceComponentsInfo.i_ServiceComponentStatusId == (int)ServiceComponentStatus.PorIniciar ? ((int)ServiceComponentStatus.Iniciado).ToString() : _serviceComponentsInfo.i_ServiceComponentStatusId.ToString();
                    //_EstadoComponente = _serviceComponentsInfo.i_ServiceComponentStatusId == (int)ServiceComponentStatus.PorIniciar ? ((int)ServiceComponentStatus.Iniciado) : _serviceComponentsInfo.i_ServiceComponentStatusId;
                    cbTipoProcedenciaExamen.SelectedValue = _serviceComponentsInfo.i_ExternalInternalId == null ? "1" : _serviceComponentsInfo.i_ExternalInternalId.ToString();
                    chkApproved.Checked = Convert.ToBoolean(_serviceComponentsInfo.i_IsApprovedId);


                    #region Permisos de lectura / Escritura x componente de acuerdo al rol del usuario

                    SetSecurityByComponent();

                    #endregion

                    if (_serviceComponentsInfo.ServiceComponentFields.Count != 0)
                    {
                        _cancelEventSelectedIndexChange = true;
                        SearchControlAndSetValue(tcExamList.SelectedTab.TabPage, _serviceComponentsInfo);

                        _cancelEventSelectedIndexChange = false;
                    }
                    else
                    {        
                        SetDefaultValueBySelectedTab();
                    }

                    lblUsuGraba.Text = string.Format("Usuario Crea : {0}", _serviceComponentsInfo.v_CreationUser);
                    lblFechaGraba.Text = string.Format("Fecha Crea : {0} {1}", _serviceComponentsInfo.d_CreationDate.Value.ToShortDateString(), _serviceComponentsInfo.d_CreationDate.Value.ToShortTimeString());
                }

                var diagnosticList = new ServiceBL().GetServiceComponentDisgnosticsForGridView(ref objOperationResult,
                                                                                        _serviceId,
                                                                                        componentId);

                if (_tmpExamDiagnosticComponentList != null)
                    _tmpExamDiagnosticComponentList = null;

                if (diagnosticList != null && diagnosticList.Count != 0)
                {
                    grdDiagnosticoPorExamenComponente.DataSource = diagnosticList;
                    lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", diagnosticList.Count());

                   _tmpExamDiagnosticComponentList = diagnosticList;

                    if (objOperationResult.Success != 1)
                    {
                        MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Limpiar la grilla de DX con una entidad vacia
                    grdDiagnosticoPorExamenComponente.DataSource = new DiagnosticRepositoryList();
                    lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", 0);

                }

            }
            ExamConfiguration(componentId);
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
                                //AMC15
                                if (cf.v_ComponentFieldId == "N009-MF000001788" || cf.v_ComponentFieldId == "N002-MF000000211")
                                {
                                    txtt.Text = _Dni;
                                }
                                else if (cf.v_ComponentFieldId == "N009-MF000000588" || cf.v_ComponentFieldId == "N009-MF000000587")
                                {
                                    txtt.Text = DateTime.Now.ToShortDateString();
                                }
                                else
                                {
                                    txtt.Text = cf.v_DefaultText;
                                }


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
                            case ControlType.Radiobutton:
                                RadioButton rb = (RadioButton)field[0];
                                rb.CreateControl();
                                rb.Checked = string.IsNullOrEmpty(cf.v_DefaultText) ? false : Convert.ToBoolean(int.Parse(cf.v_DefaultText));
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
                            case ControlType.UcSomnolencia:
                                ((UserControls.ucSomnolencia)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcAcumetria:
                                ((UserControls.ucAcumetria)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcFototipo:
                                ((UserControls.ucFotoTipo)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcSintomaticoRespi:
                                ((UserControls.ucSintomaticoResp)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcRxLumboSacra:
                                ((UserControls.ucRXLumboSacra)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcOtoscopia:
                                ((UserControls.ucOtoscopia)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcEvaluacionErgonomica:
                                ((UserControls.ucEvaluacionErgonomica)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcOsteoMuscular:
                                ((UserControls.ucOsteoMuscular)field[0]).ClearValueControl();
                                break;
                            case ControlType.UcOjoSeco:
                                ((UserControls.ucOjoSeco)field[0]).ClearValueControl();
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
        private void ExamConfiguration(string componentId)
        {
            var objOperationResult = new OperationResult();
            var componentProfile = new ServiceBL().GetRoleNodeComponentProfile(ref objOperationResult, _nodeId, _roleId, componentId);

            IsExamDiagnosable(componentProfile.i_Dx);
            IsExamApproved(componentProfile.i_Approved);
        }
        
        private void IsExamApproved(int? idApproved)
        {
            switch (idApproved)
            {
                case (int)SiNo.NO:
                    // el check de APROBADO está desactivado. No importando el permiso del rol
                    chkApproved.Enabled = false;
                    break;
                case (int)SiNo.SI:
                    // el check se activa o desactiva dependiendo del rol
                    chkApproved.Enabled = BLL.Utils.IsActionEnabled("frmEso_EXAMENES_APPROVED", _formActions);
                    break;
            }
        }

        private void IsExamDiagnosable(int? idDx)
        {
            switch (idDx)
            {
                case (int)SiNo.SI:
                    //la sección se activa o desactiva dependiendo del PERMISO. Los diagnósticos automáticos deben seguir funcionando y reportándose.
                    btnAgregarDxExamen.Enabled = BLL.Utils.IsActionEnabled("frmEso_EXAMENES_ADDDX", _formActions);
                    btnEditarDxExamen.Enabled = BLL.Utils.IsActionEnabled("frmEso_EXAMENES_EDITDX", _formActions);
                    btnRemoverDxExamen.Enabled = BLL.Utils.IsActionEnabled("frmEso_EXAMENES_REMOVEDX", _formActions);
                    break;
                case (int)SiNo.NO:
                    // toda la sección esta desactivada, pero los diagnósticos automáticos deben seguir funcionando y reportándose.
                    btnAgregarDxExamen.Enabled = false;
                    btnEditarDxExamen.Enabled = false;
                    btnRemoverDxExamen.Enabled = false;
                    _isDisabledButtonsExamDx = true;
                    break;
            }
        }

        private void btnRemoverDxExamen_Click(object sender, EventArgs e)
        {
            if (!ValidateSelected()) return;
            var result = MessageBox.Show(@"¿Está seguro de eliminar este registro?:", @"ADVERTENCIA!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;
            var diagnosticRepositoryId = grdDiagnosticoPorExamenComponente.Selected.Rows[0].Cells["v_DiagnosticRepositoryId"].Value.ToString();
            var recordType = int.Parse(grdDiagnosticoPorExamenComponente.Selected.Rows[0].Cells["i_RecordType"].Value.ToString());
            var findResult = _tmpExamDiagnosticComponentList.Find(p => p.v_DiagnosticRepositoryId == diagnosticRepositoryId);
            switch (recordType)
            {
                case (int) RecordType.Temporal:
                    _tmpExamDiagnosticComponentList.Remove(findResult);
                    break;
                case (int) RecordType.NoTemporal:
                    findResult.i_RecordStatus = (int) RecordStatus.EliminadoLogico;
                    foreach (var item in findResult.Recomendations)
                    {
                        item.i_RecordStatus = (int) RecordStatus.EliminadoLogico;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            BindGridDiagnosticTemporal(_tmpExamDiagnosticComponentList);
            //var dataList = _tmpExamDiagnosticComponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
            //grdDiagnosticoPorExamenComponente.DataSource = dataList;
            //lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", _tmpExamDiagnosticComponentList.Count());
        }

        private bool ValidateSelected()
        {
            if (grdDiagnosticoPorExamenComponente.Selected.Rows.Count != 0)
                return true;

            MessageBox.Show(@"Por favor seleccione un registro.", @"Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void btnAgregarDxExamen_Click(object sender, EventArgs e)
        {
            var frm = new frmAddExamDiagnosticComponent("New"){_componentId = _componentId,_serviceId = _serviceId};

            if (_tmpExamDiagnosticComponentList != null)
                frm._tmpExamDiagnosticComponentList = _tmpExamDiagnosticComponentList;

            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;
            BindGridDiagnosticTemporal(frm._tmpExamDiagnosticComponentList);
        }

        private void BindGridDiagnosticTemporal(List<DiagnosticRepositoryList> listDx)
        {
            if (listDx == null) return;
            _tmpExamDiagnosticComponentList = listDx;

            var dataList = _tmpExamDiagnosticComponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
            grdDiagnosticoPorExamenComponente.DataSource = new DiagnosticRepositoryList();
            grdDiagnosticoPorExamenComponente.DataSource = dataList;
            lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
        }

        private void btnEditarDxExamen_Click(object sender, EventArgs e)
        {
            if (!ValidateSelected()) return;
            var frm = new frmAddExamDiagnosticComponent("Edit");

            var diagnosticRepositoryId = grdDiagnosticoPorExamenComponente.Selected.Rows[0].Cells["v_DiagnosticRepositoryId"].Value.ToString();
            frm._diagnosticRepositoryId = diagnosticRepositoryId;
            frm._componentId = _componentId;
            frm._serviceId = _serviceId;

            if (_tmpExamDiagnosticComponentList != null)
                frm._tmpExamDiagnosticComponentList = _tmpExamDiagnosticComponentList;

            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;
            BindGridDiagnosticTemporal(frm._tmpExamDiagnosticComponentList);
            PaintGrdDiagnosticoPorExamenComponente();

            //if (frm._tmpExamDiagnosticComponentList != null)
            //{
            //    _tmpExamDiagnosticComponentList = frm._tmpExamDiagnosticComponentList;

            //    var dataList = _tmpExamDiagnosticComponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
            //    grdDiagnosticoPorExamenComponente.DataSource = new DiagnosticRepositoryList();
            //    grdDiagnosticoPorExamenComponente.DataSource = dataList;
            //    grdDiagnosticoPorExamenComponente.Refresh();
            //    lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", dataList.Count());

            //    
            //}
            //if (grdDiagnosticoPorExamenComponente.Selected.Rows.Count != 0) return;
            //MessageBox.Show(@"Por favor seleccione un registro.", @"Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void grdDiagnosticoPorExamenComponente_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            var caliFinal = (PreQualification)e.Row.Cells["i_PreQualificationId"].Value;

            switch (caliFinal)
            {
                case PreQualification.SinPreCalificar:
                    e.Row.Appearance.BackColor = Color.Pink;
                    e.Row.Appearance.BackColor2 = Color.Pink;
                    break;
                case PreQualification.Aceptado:
                    e.Row.Appearance.BackColor = Color.LawnGreen;
                    e.Row.Appearance.BackColor2 = Color.LawnGreen;
                    break;
                case PreQualification.Rechazado:
                    e.Row.Appearance.BackColor = Color.DarkGray;
                    e.Row.Appearance.BackColor2 = Color.DarkGray;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            e.Row.Appearance.BackGradientStyle = GradientStyle.VerticalBump;
        }

        private void PaintGrdDiagnosticoPorExamenComponente()
        {
            foreach (var t in grdDiagnosticoPorExamenComponente.Rows)
            {
                var caliFinal = (PreQualification)t.Cells["i_PreQualificationId"].Value;
                switch (caliFinal)
                {
                    case PreQualification.SinPreCalificar:
                        t.Appearance.BackColor = Color.Pink;
                        t.Appearance.BackColor2 = Color.Pink;
                        break;
                    case PreQualification.Aceptado:
                        t.Appearance.BackColor = Color.LawnGreen;
                        t.Appearance.BackColor2 = Color.LawnGreen;
                        break;
                    case PreQualification.Rechazado:
                        t.Appearance.BackColor = Color.DarkGray;
                        t.Appearance.BackColor2 = Color.DarkGray;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                t.Appearance.BackGradientStyle = GradientStyle.VerticalBump;
            }
        }

        private void grdTotalDiagnosticos_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            var cell = e.Row.Cells["i_FinalQualificationId"].Value;
            if (cell != null)
            {
                var caliFinal = (FinalQualification)cell;
                var dx = e.Row.Cells["v_DiseasesId"].Value.ToString();

                switch (caliFinal)
                {
                    case FinalQualification.SinCalificar:

                        if (dx != Constants.EXAMEN_DE_SALUD_SIN_ALTERACION)
                        {
                            e.Row.Appearance.BackColor = Color.Pink;
                            e.Row.Appearance.BackColor2 = Color.Pink;
                        }
                        else
                        {
                            e.Row.Appearance.BackColor = Color.White;
                            e.Row.Appearance.BackColor2 = Color.White;
                        }

                        break;
                    case FinalQualification.Definitivo:
                        e.Row.Appearance.BackColor = Color.LawnGreen;
                        e.Row.Appearance.BackColor2 = Color.LawnGreen;
                        break;
                    case FinalQualification.Presuntivo:
                        e.Row.Appearance.BackColor = Color.LawnGreen;
                        e.Row.Appearance.BackColor2 = Color.LawnGreen;
                        break;
                    case FinalQualification.Descartado:
                        e.Row.Appearance.BackColor = Color.DarkGray;
                        e.Row.Appearance.BackColor2 = Color.DarkGray;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            e.Row.Appearance.BackGradientStyle = GradientStyle.VerticalBump;
        }

        private void grdTotalDiagnosticos_BeforePerformAction(object sender, BeforeUltraGridPerformActionEventArgs e)
        {
            if ((e.UltraGridAction == UltraGridAction.NextRow && _currentDownKey == Keys.Right)
                   || (e.UltraGridAction == UltraGridAction.PrevRow && _currentDownKey == Keys.Left))
            {
                e.Cancel = true;
            }
        }

        private void grdTotalDiagnosticos_KeyDown(object sender, KeyEventArgs e)
        {
            _currentDownKey = e.KeyData;

            if (e.KeyData == Keys.Right)
            {
                grdTotalDiagnosticos.ActiveColScrollRegion.Scroll(ColScrollAction.Right);
            }
            else if (e.KeyData == Keys.Left)
            {
                grdTotalDiagnosticos.ActiveColScrollRegion.Scroll(ColScrollAction.Left);
            }
        }

        private void grdTotalDiagnosticos_KeyUp(object sender, KeyEventArgs e)
        {
            _currentDownKey = Keys.None;
        }

        private void btnAgregarRecomendaciones_AnalisisDx_Click(object sender, EventArgs e)
        {
            var frm = new frmMasterRecommendationRestricction("Recomendaciones", (int)Typifying.Recomendaciones, ModeOperation.Total);
            frm.ShowDialog();

            if (_tmpRecomendationList == null)
            {
                _tmpRecomendationList = new List<RecomendationList>();
            }

            var recomendationId = frm._masterRecommendationRestricctionId;
            var recommendationName = frm._masterRecommendationRestricctionName;

            if (recomendationId != null && recommendationName != null)
            {
                var recomendation = _tmpRecomendationList.Find(p => p.v_MasterRecommendationId == recomendationId);

                if (recomendation == null)   // agregar con normalidad [insert]  a la bolsa  
                {
                    // Agregar restricciones a la Lista
                    RecomendationList recomendationList = new RecomendationList();

                    recomendationList.v_RecommendationId = Guid.NewGuid().ToString();
                    recomendationList.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                    recomendationList.v_MasterRecommendationId = recomendationId;
                    recomendationList.v_ServiceId = _serviceId;
                    recomendationList.v_ComponentId = _tmpTotalDiagnostic.v_ComponentId;
                    recomendationList.v_RecommendationName = recommendationName;
                    recomendationList.i_RecordStatus = (int)RecordStatus.Agregado;
                    recomendationList.i_RecordType = (int)RecordType.Temporal;

                    _tmpRecomendationList.Add(recomendationList);
                }
                else    // La restriccion ya esta agregado en la bolsa hay que actualizar su estado
                {
                    if (recomendation.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                    {
                        if (recomendation.i_RecordType == (int)RecordType.NoTemporal)   // El registro Tiene in ID de BD
                        {
                            recomendation.v_MasterRecommendationId = recomendationId;
                            recomendation.v_RecommendationName = recommendationName;
                            recomendation.i_RecordStatus = (int)RecordStatus.Grabado;
                        }
                        else if (recomendation.i_RecordType == (int)RecordType.Temporal)   // El registro tiene un ID temporal [GUID]
                        {
                            recomendation.v_MasterRecommendationId = recomendationId;
                            recomendation.v_RecommendationName = recommendationName;
                            recomendation.i_RecordStatus = (int)RecordStatus.Agregado;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Por favor seleccione otra Recomendación. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
            }

            var dataList = _tmpRecomendationList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

            // Cargar grilla
            grdRecomendaciones_AnalisisDiagnostico.DataSource = new RecomendationList();
            grdRecomendaciones_AnalisisDiagnostico.DataSource = dataList;
            grdRecomendaciones_AnalisisDiagnostico.Refresh();
            lblRecordCountRecomendaciones_AnalisisDx.Text = string.Format("Se encontraron {0} registros.", dataList.Count());

        }

        private void btnRemoverRecomendacion_AnalisisDx_Click(object sender, EventArgs e)
        {
            if (grdRecomendaciones_AnalisisDiagnostico.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un registro.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == DialogResult.Yes)
            {
                // Delete the item

                // Capturar id desde la grilla de restricciones
                var recomendationId = grdRecomendaciones_AnalisisDiagnostico.Selected.Rows[0].Cells["v_RecommendationId"].Value.ToString();

                // Buscar registro para remover
                var findResult = _tmpRecomendationList.Find(p => p.v_RecommendationId == recomendationId);

                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
                var dataList = _tmpRecomendationList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                grdRecomendaciones_AnalisisDiagnostico.DataSource = new RecomendationList();
                grdRecomendaciones_AnalisisDiagnostico.DataSource = dataList;
                grdRecomendaciones_AnalisisDiagnostico.Refresh();
                lblRecordCountRecomendaciones_AnalisisDx.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
        }

        private void grdRecomendaciones_AnalisisDiagnostico_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnRemoverRecomendacion_AnalisisDx.Enabled = (grdRecomendaciones_AnalisisDiagnostico.Selected.Rows.Count > 0 && _removerRecomendacionAnalisisDx);
        }

        private void grdRestricciones_AnalisisDiagnostico_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnRemoverRestriccion_Analisis.Enabled = (grdRestricciones_AnalisisDiagnostico.Selected.Rows.Count > 0 && _removerRestriccionAnalisis);
        }

        private void btnAgregarRestriccion_Analisis_Click(object sender, EventArgs e)
        {
            var frm = new frmMasterRecommendationRestricction("Restricciones", (int)Typifying.Restricciones, ModeOperation.Total);
            frm.ShowDialog();

            if (_tmpRestrictionList == null)
            {
                _tmpRestrictionList = new List<RestrictionList>();
            }

            var restrictionId = frm._masterRecommendationRestricctionId;
            var restrictionName = frm._masterRecommendationRestricctionName;

            if (restrictionId != null && restrictionName != null)
            {
                var restriction = _tmpRestrictionList.Find(p => p.v_MasterRestrictionId == restrictionId);

                if (restriction == null)   // agregar con normalidad [insert]  a la bolsa  
                {
                    // Agregar restricciones a la Lista
                    RestrictionList restrictionByDiagnostic = new RestrictionList();

                    restrictionByDiagnostic.v_RestrictionByDiagnosticId = Guid.NewGuid().ToString();
                    restrictionByDiagnostic.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                    restrictionByDiagnostic.v_MasterRestrictionId = restrictionId;
                    restrictionByDiagnostic.v_ServiceId = _serviceId;
                    restrictionByDiagnostic.v_ComponentId = _tmpTotalDiagnostic.v_ComponentId;
                    restrictionByDiagnostic.v_RestrictionName = restrictionName;
                    restrictionByDiagnostic.i_RecordStatus = (int)RecordStatus.Agregado;
                    restrictionByDiagnostic.i_RecordType = (int)RecordType.Temporal;

                    _tmpRestrictionList.Add(restrictionByDiagnostic);

                }
                else    // La restriccion ya esta agregado en la bolsa hay que actualizar su estado
                {
                    if (restriction.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                    {
                        if (restriction.i_RecordType == (int)RecordType.NoTemporal)   // El registro Tiene in ID de BD
                        {
                            restriction.v_MasterRestrictionId = restrictionId;
                            restriction.v_RestrictionName = restrictionName;
                            restriction.i_RecordStatus = (int)RecordStatus.Grabado;
                        }
                        else if (restriction.i_RecordType == (int)RecordType.Temporal)   // El registro tiene un ID temporal [GUID]
                        {
                            restriction.v_MasterRestrictionId = restrictionId;
                            restriction.v_RestrictionName = restrictionName;
                            restriction.i_RecordStatus = (int)RecordStatus.Agregado;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor seleccione otra Restriccón. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
            }

            var dataList = _tmpRestrictionList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

            // Cargar grilla
            grdRestricciones_AnalisisDiagnostico.DataSource = new RestrictionList();
            grdRestricciones_AnalisisDiagnostico.DataSource = dataList;
            grdRestricciones_AnalisisDiagnostico.Refresh();
            lblRecordCountRestricciones_AnalisisDx.Text = string.Format("Se encontraron {0} registros.", dataList.Count());

        }

        private void btnRemoverRestriccion_Analisis_Click(object sender, EventArgs e)
        {
            if (grdRestricciones_AnalisisDiagnostico.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un registro.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == DialogResult.Yes)
            {
                // Delete the item

                // Capturar id desde la grilla de restricciones
                var restrictionByDiagnosticId = grdRestricciones_AnalisisDiagnostico.Selected.Rows[0].Cells["v_RestrictionByDiagnosticId"].Value.ToString();

                // Buscar registro para remover
                var findResult = _tmpRestrictionList.Find(p => p.v_RestrictionByDiagnosticId == restrictionByDiagnosticId);
                // Borrado logico
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;

                var dataList = _tmpRestrictionList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                grdRestricciones_AnalisisDiagnostico.DataSource = new RestrictionList();
                grdRestricciones_AnalisisDiagnostico.DataSource = dataList;
                grdRestricciones_AnalisisDiagnostico.Refresh();
                lblRecordCountRestricciones_AnalisisDx.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
        }

        private void btnAceptarDX_Click_1(object sender, EventArgs e)
        {
            var califiFinalId = (FinalQualification)int.Parse(cbCalificacionFinal.SelectedValue.ToString());
            if (califiFinalId == FinalQualification.Descartado || califiFinalId == FinalQualification.SinCalificar)
            {
                GuardarDx();
            }
            else
            {
                if (uvAnalisisDx.Validate(true, false).IsValid)
                {
                    GuardarDx();
                }
                else
                {
                    MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void GuardarDx()
        {
            DialogResult Result = MessageBox.Show("¿Está seguro de grabar este registro?:", "CONFIRMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Result == DialogResult.Yes)
            {

                OperationResult objOperationResult = new OperationResult();

                // Grabar Dx por examen componente mas sus restricciones
                if (_diagnosticId != null)
                    _tmpTotalDiagnostic.v_DiseasesId = _diagnosticId;
                if (cbCalificacionFinal.SelectedValue.ToString() != "-1")
                    _tmpTotalDiagnostic.i_FinalQualificationId = int.Parse(cbCalificacionFinal.SelectedValue.ToString());
                if (cbTipoDx.SelectedValue.ToString() != "-1")
                    _tmpTotalDiagnostic.i_DiagnosticTypeId = int.Parse(cbTipoDx.SelectedValue.ToString());
                if (cbEnviarAntecedentes.SelectedValue.ToString() != "-1")
                    _tmpTotalDiagnostic.i_IsSentToAntecedent = int.Parse(cbEnviarAntecedentes.SelectedValue.ToString());

                _tmpTotalDiagnostic.d_ExpirationDateDiagnostic = dtpFechaVcto.Checked ? dtpFechaVcto.Value.Date : (DateTime?)null;

                _tmpTotalDiagnostic.Restrictions = _tmpRestrictionList;
                _tmpTotalDiagnostic.Recomendations = _tmpRecomendationList;

                #region UTILIZAR FIRMA (Suplantar profesional)

                if (chkUtilizaFirmaControlAuditoria.Checked)
                {
                    var frm = new Popups.frmSelectSignature();
                    frm.ShowDialog();

                    if (frm.DialogResult != System.Windows.Forms.DialogResult.Cancel)
                    {
                        if (frm.i_SystemUserSuplantadorId != null)
                        {
                            Globals.ClientSession.i_SystemUserId = (int)frm.i_SystemUserSuplantadorId;
                        }
                        else
                        {
                            Globals.ClientSession.i_SystemUserId = Globals.ClientSession.i_SystemUserCopyId;
                        }

                    }

                }

                #endregion

                new ServiceBL().UpdateTotalDiagnostic(ref objOperationResult,
                                                 _tmpTotalDiagnostic,
                                                 _serviceId,
                                                 Globals.ClientSession.GetAsList());


                InitializeData();
                //EditTotalDiagnosticos(_rowIndex);
                EditTotalDiagnosticos();
                // Refrescar grilla de Total de DX
                GetTotalDiagnosticsForGridView();
                ConclusionesyTratamiento_LoadAllGrid();

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

        private void btnAgregarTotalDiagnostico_Click(object sender, EventArgs e)
        {
            var frm = new Popups.frmAddTotalDiagnostic();
            //frm._componentId = _componentId;
            frm._serviceId = _serviceId;

            if (_tmpTotalDiagnosticList != null)
            {
                frm._tmpTotalDiagnosticList = _tmpTotalDiagnosticByServiceIdList;
            }

            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            // Refrescar grilla de Total de DX
            GetTotalDiagnosticsForGridView();
            ConclusionesyTratamiento_LoadAllGrid();
        }

        private void GetTotalDiagnosticsForGridView()
        {
                OperationResult objOperationResult = new OperationResult();
                _tmpTotalDiagnosticByServiceIdList = new ServiceBL().GetServiceComponentDisgnosticsByServiceId(ref objOperationResult, _serviceId);

                if (_tmpTotalDiagnosticByServiceIdList == null)
                    return;

                grdTotalDiagnosticos.DataSource = _tmpTotalDiagnosticByServiceIdList;
                lblRecordCountTotalDiagnosticos.Text = string.Format("Se encontraron {0} registros.", _tmpTotalDiagnosticByServiceIdList.Count());

                // Analizar el resultado de la operación
                if (objOperationResult.Success != 1)
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Seleccionar la fila que estaba marcada antes del refrescado
                SetCurrentRow();
        }

        private void ConclusionesyTratamiento_LoadAllGrid()
        {
            OperationResult objOperationResult = new OperationResult();

            #region Conclusiones Diagnósticas

            GetConclusionesDiagnosticasForGridView();

            #endregion

            #region Recomendación

            _tmpRecomendationConclusionesList = new ServiceBL().GetServiceRecommendationByServiceId(ref objOperationResult, _serviceId);

            if (_tmpRecomendationConclusionesList == null)
                return;

            grdRecomendaciones_Conclusiones.DataSource = _tmpRecomendationConclusionesList;
            lblRecordCountRecomendaciones_Conclusiones.Text = string.Format("Se encontraron {0} registros.",
                                                            _tmpRecomendationConclusionesList.Count());

            #endregion

            #region Restricciones

            _tmpRestrictionListConclusiones = new ServiceBL().GetServiceRestrictionsForGridView(ref objOperationResult, _serviceId);

            if (_tmpRestrictionListConclusiones == null)
                return;

            grdRestricciones_Conclusiones.DataSource = _tmpRestrictionListConclusiones;
            lblRecordCountRestricciones_Conclusiones.Text = string.Format("Se encontraron {0} registros.", _tmpRestrictionListConclusiones.Count());

            #endregion

            // Analizar el resultado de la operación
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetConclusionesDiagnosticasForGridView()
        {
            var objOperationResult = new OperationResult();
            _tmpTotalConclusionesDxByServiceIdList = new ServiceBL().GetServiceComponentConclusionesDxServiceId(ref objOperationResult, _serviceId);

            if (_tmpTotalConclusionesDxByServiceIdList == null)
                return;

            grdConclusionesDiagnosticas.DataSource = _tmpTotalConclusionesDxByServiceIdList;
            lblRecordCountConclusionesDiagnosticas.Text = string.Format("Se encontraron {0} registros.", _tmpTotalConclusionesDxByServiceIdList.Count());

            // Analizar el resultado de la operación
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (_rowIndexConclucionesDX != null)
            {
                if (grdConclusionesDiagnosticas.Rows.Count != 0)
                {
                    // Seleccionar la fila
                    grdConclusionesDiagnosticas.Rows[_rowIndexConclucionesDX.Value].Selected = true;
                    grdConclusionesDiagnosticas.ActiveRowScrollRegion.ScrollRowIntoView(grdConclusionesDiagnosticas.Rows[_rowIndexConclucionesDX.Value]);
                    //grdTotalDiagnosticos.ActiveRowScrollRegion.ScrollPosition = _rowIndex;
                }
            }

        }

        private void btnAgregarDX_Click(object sender, EventArgs e)
        {
            var returnDiseasesList = new DiseasesList();
            var frm = new frmDiseases();

            frm.ShowDialog();
            returnDiseasesList = frm._objDiseasesList;

            if (returnDiseasesList.v_DiseasesId == null) return;
            lblDiagnostico.Text = returnDiseasesList.v_Name + " / " + returnDiseasesList.v_CIE10Id;
            _diagnosticId = returnDiseasesList.v_DiseasesId;
        }
        
        private void grdConclusionesDiagnosticas_ClickCell(object sender, ClickCellEventArgs e)
        {
            _rowIndexConclucionesDX = e.Cell.Row.Index;
            GetConclusionesDiagnosticasForGridView();
        }

        private void btnRemoverTotalDiagnostico_Click(object sender, EventArgs e)
        {
            if (grdTotalDiagnosticos.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un registro.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == DialogResult.Yes)
            {
                // Delete the item
                var diagnosticRepositoryId = grdTotalDiagnosticos.Selected.Rows[0].Cells["v_DiagnosticRepositoryId"].Value.ToString();
                new ServiceBL().DeleteTotalDiagnostic(ref objOperationResult, diagnosticRepositoryId, Globals.ClientSession.GetAsList());
                // Analizar el resultado de la operación
                if (objOperationResult.Success != 1)
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                GetTotalDiagnosticsForGridView();
                ConclusionesyTratamiento_LoadAllGrid();

                // Limpiar grillas despues de borrar y la grilla quede vacia
                if (grdTotalDiagnosticos.Rows.Count == 0)
                {
                    gbEdicionDiagnosticoTotal.Enabled = false;
                    lblDiagnostico.Text = string.Empty;
                    cbCalificacionFinal.SelectedValue = "-1";
                    cbEnviarAntecedentes.SelectedValue = "-1";
                    cbTipoDx.SelectedValue = "-1";
                    dtpFechaVcto.Value = DateTime.Now;
                }

            }
        }

        private void btnRefrescarTotalDiagnostico_Click(object sender, EventArgs e)
        {
            GetTotalDiagnosticsForGridView();
        }

        private void ucAudiometria_AfterValueChange(object sender, AudiometriaAfterValueChangeEventArgs e)
        {
            var diagnosticRepository = e.PackageSynchronization as List<DiagnosticRepositoryList>;

            #region Delete Dx

            List<DiagnosticRepositoryList> findControlResult = null;
            
            if (_tmpExamDiagnosticComponentList != null)
            {
                // Buscar control que haya generado algun DX automático
                findControlResult = _tmpExamDiagnosticComponentList.FindAll(p => e.ListcomponentFieldsId.Contains(p.v_ComponentFieldsId) && p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
            }

            // Remover DX (automático) encontrado.
            if (findControlResult != null)
            {
                foreach (var item in findControlResult)
                {
                    if (item.i_RecordType == (int)RecordType.Temporal)
                    {
                        _tmpExamDiagnosticComponentList.Remove(item);
                    }
                    else
                    {
                        item.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
                        item.Recomendations.ForEach(f => f.i_RecordStatus = (int)RecordStatus.EliminadoLogico);
                        item.Restrictions.ForEach(f => f.i_RecordStatus = (int)RecordStatus.EliminadoLogico);
                    }
                }

            }

            #endregion

            // Si se generó un DX (automático).
            if (diagnosticRepository != null)
            {
                // Set id servicio
                diagnosticRepository.ForEach(p => p.v_ServiceId = _serviceId);
                diagnosticRepository.SelectMany(p => p.Recomendations).ToList().ForEach(f => f.v_ServiceId = _serviceId);
                diagnosticRepository.SelectMany(p => p.Restrictions).ToList().ForEach(f => f.v_ServiceId = _serviceId);

                if (_tmpExamDiagnosticComponentList != null)
                {
                    // Se agrega el DX obtenido a la lista de DX general.
                    _tmpExamDiagnosticComponentList.AddRange(diagnosticRepository);
                }
                else
                {
                    _tmpExamDiagnosticComponentList = new List<DiagnosticRepositoryList>();
                    _tmpExamDiagnosticComponentList.AddRange(diagnosticRepository);
                }

            }

            if (_tmpExamDiagnosticComponentList != null)
            {
                // Filtar para Mostrar en la grilla solo registros que no están eliminados
                var dataList = _tmpExamDiagnosticComponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                // Refrescar grilla                        
                grdDiagnosticoPorExamenComponente.DataSource = dataList;
                lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }

        }

        private void chkPresentaSisntomas_CheckedChanged(object sender, EventArgs e)
        {
            txtSintomaPrincipal.Enabled = chkPresentaSisntomas.Checked;
            txtValorTiempoEnfermedad.Enabled = chkPresentaSisntomas.Checked;
            cbCalendario.Enabled = chkPresentaSisntomas.Checked;

            if (chkPresentaSisntomas.Checked)
            {
                uvAnamnesis.GetValidationSettings(txtSintomaPrincipal).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", true, typeof(string));
                uvAnamnesis.GetValidationSettings(txtSintomaPrincipal).IsRequired = true;
                uvAnamnesis.GetValidationSettings(txtValorTiempoEnfermedad).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", true, typeof(string));
                uvAnamnesis.GetValidationSettings(txtValorTiempoEnfermedad).IsRequired = true;
                uvAnamnesis.GetValidationSettings(cbCalendario).Condition = new OperatorCondition(ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
                uvAnamnesis.GetValidationSettings(cbCalendario).IsRequired = true;
            }
            else
            {
                uvAnamnesis.GetValidationSettings(txtSintomaPrincipal).IsRequired = false;
                uvAnamnesis.GetValidationSettings(txtValorTiempoEnfermedad).IsRequired = false;
                uvAnamnesis.GetValidationSettings(cbCalendario).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvAnamnesis.GetValidationSettings(cbCalendario).IsRequired = false;
            }
        }

        private void btnGuardarExamen_Click(object sender, EventArgs e)
        {
            
            GrabarAsync();

            #region Old

            //if (_isChangeValue && cbEstadoComponente.SelectedValue.ToString() == ((int)ServiceComponentStatus.Iniciado).ToString())
            //{
            //    btnGuardarExamen.Enabled = true;
            //    //var result = MessageBox.Show("Ha realizado cambios, por lo tanto el estado del examen NO debe ser INICIADO", "CONFIRMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    //return;
            //}
            //else
            //{
            //    if (cbEstadoComponente.SelectedValue.ToString() == ((int)ServiceComponentStatus.Iniciado).ToString())
            //    {
            //        MessageBox.Show("El estado debe ser diferente de INICIADO para poder grabar.", "CONFIRMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return;
            //    }
            //    else
            //    {
            //        _chkApprovedEnabled = chkApproved.Enabled;

            //        _chkApprovedEnabled = chkApproved.Enabled;

            //        var scope = new TransactionScope(
            //            TransactionScopeOption.RequiresNew,
            //            new TransactionOptions()
            //            {

            //                IsolationLevel = System.Transactions.IsolationLevel.Snapshot
            //            });

            //        using (scope)
            //        {
            //            SaveExamBySelectedTab(tcExamList.SelectedTab.TabPage);
            //        }
            //    }
            //}

            #endregion

        }

        private void GrabarAsync()
        {
            UltraValidator uv = null;
            var result = _dicUltraValidators.TryGetValue(_componentId, out uv);
            if (!result) return;

            if (uv.Validate(false, true).IsValid)
            {
                _chkApprovedEnabled = chkApproved.Enabled;

                if (!validacionAuditado())
                {
                    if (int.Parse(cbEstadoComponente.SelectedValue.ToString()) ==
                        (int)ServiceComponentStatus.Auditado && _profesionId == 30) //evaluador
                    {

                        MessageBox.Show("El examen ya fue AUDITADO, no puede guardar los cambios.", "CONFIRMACIÓN!",
                            MessageBoxButtons.OK, MessageBoxIcon.Question);
                        return;
                    }
                    var respuesta = MessageBox.Show("¿Está seguro de grabar este registro?", "CONFIRMACIÓN!",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta == DialogResult.Yes)
                    {
                        IniciarGrabadoAsincrono(tcExamList.SelectedTab.TabPage);
                        GrabarDiagnosticos();
                        _isChangeValue = false;
                    }
                }
            }
        }

        private bool validacionAuditado()
        {
            if (int.Parse(cbEstadoComponente.SelectedValue.ToString()) == (int)ServiceComponentStatus.Auditado && _profesionId == 30)//evaluador
            {
                MessageBox.Show("El examen ya fue AUDITADO, no puede guardar los cambios.", "CONFIRMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return true;
            }
            return false;
        }

        private void IniciarGrabadoAsincrono(Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl)
        {
            _serviceComponentFieldsList = new List<ServiceComponentFieldsList>();
            ProcessControlBySelectedTab_AMC(ultraTabPageControl);

            DataEso oDataEso = new DataEso();
            oDataEso.categoria = ultraTabPageControl.Tab.Text;
            oDataEso.v_ServiceId = _serviceId;
            oDataEso.v_PersonId = _personId;
            oDataEso.v_ServiceComponentId = _serviceComponentId;
            oDataEso.Estado = "En espera";
            oDataEso.NroIntentos = "----";
            oDataEso.datos = _serviceComponentFieldsList;
            OpenFormProcesoEso(oDataEso);

        }

        private void GrabarDiagnosticos()
        {
            OperationResult objOperationResult = new OperationResult();
            int? systemUserSuplantadorId = 0;

            #region Capturar [Comentarios, estado, procedencia de un exmanen componente]

            var serviceComponentDto = new servicecomponentDto();
            serviceComponentDto.v_ServiceComponentId = _serviceComponentId;
            //Obtener fecha de Actualizacion
            var FechaUpdate = _serviceBL.GetServiceComponent(ref objOperationResult, _serviceComponentId).d_UpdateDate;
            serviceComponentDto.v_Comment = txtComentario.Text;
            //grabar estado del examen según profesión del usuario
            if (_profesionId == 30) // evaluador
            {
                serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Evaluado;
                _EstadoComponente = (int)ServiceComponentStatus.Evaluado;
            }
            else if (_profesionId == 31 || _profesionId == 32)//auditor
            {
                serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Auditado;
                _EstadoComponente = (int)ServiceComponentStatus.Auditado;
            }
            serviceComponentDto.i_ServiceComponentStatusId = int.Parse(cbEstadoComponente.SelectedValue.ToString());
            _EstadoComponente = int.Parse(cbEstadoComponente.SelectedValue.ToString());
            serviceComponentDto.i_ExternalInternalId = int.Parse(cbTipoProcedenciaExamen.SelectedValue.ToString());
            serviceComponentDto.i_IsApprovedId = Convert.ToInt32(chkApproved.Checked);

            serviceComponentDto.v_ComponentId = _componentId;
            serviceComponentDto.v_ServiceId = _serviceId;
            serviceComponentDto.d_UpdateDate = FechaUpdate;
            #endregion

            if (chkUtilizarFirma.Checked)
            {
                var frm = new Popups.frmSelectSignature();
                frm.ShowDialog();

                if (frm.DialogResult != System.Windows.Forms.DialogResult.Cancel)
                {
                    systemUserSuplantadorId = frm.i_SystemUserSuplantadorId;
                }
                var t = new Thread(() =>
                {
                    using (new LoadingClass.PleaseWait(this.Location, "Cargando..."))
                    {
                        Thread.Sleep(2500);
                        MessageBox.Show("Grabado correctamente", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    };
                    ;
                });
                t.Start();
               
               
            }
            else
            {
                var t = new Thread(() =>
                {
                    using (new LoadingClass.PleaseWait(this.Location, "Cargando..."))
                    {
                        Thread.Sleep(2500);
                        MessageBox.Show("Grabado correctamente", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    };
                    ;
                });
                t.Start();
            }

            #region GRABAR DATOS ADICIONALES COMO [Diagnósticos + restricciones + recomendaciones]

            // Grabar Dx por examen componente mas sus restricciones
            if (systemUserSuplantadorId != null)
            {
                Globals.ClientSession.i_SystemUserId = (int)systemUserSuplantadorId;
            }
            else
            {
                Globals.ClientSession.i_SystemUserId = Globals.ClientSession.i_SystemUserCopyId;
            }

            _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                               _tmpExamDiagnosticComponentList,
                                                serviceComponentDto,
                                                Globals.ClientSession.GetAsList(),
                                                _chkApprovedEnabled, chkUtilizarFirma.Checked);


            _serviceComponentFieldsList = null;
            _tmpListValuesOdontograma = null;
            #endregion

            #region refrescar

            flagValueChange = false;
            InitializeData();
            LoadDataBySelectedComponent_New(_componentId);
            GetTotalDiagnosticsForGridView();
            ConclusionesyTratamiento_LoadAllGrid();



            #endregion


        }

        private void SaveExamBySelectedTab(Infragistics.Win.UltraWinTabControl.UltraTabPageControl selectedTab)
        {
            // Desactivar el flag de hubo alguna modificacion
            _isChangeValue = false;
            _chkApprovedEnabled = chkApproved.Enabled;


            UltraValidator uv = null;

            try
            {
                var result = _dicUltraValidators.TryGetValue(_componentId, out uv);

                if (!result)
                    return;

                if (uv.Validate(false, true).IsValid)
                {
                    if (int.Parse(cbEstadoComponente.SelectedValue.ToString()) == (int)ServiceComponentStatus.Auditado && _profesionId == 30)//evaluador
                    {

                        MessageBox.Show("El examen ya fue AUDITADO, no puede guardar los cambios.", "CONFIRMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        return;
                    }

                    DialogResult Result = MessageBox.Show("¿Está seguro de grabar este registro?", "CONFIRMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    using (new LoadingClass.PleaseWait(this.Location, "Grabando..."))
                    {
                        OperationResult objOperationResult = new OperationResult();
                        if (Result == DialogResult.Yes)
                        {
                            // Mostrar pantalla grabando...
                            this.Enabled = false;
                            //frmWaiting.Show(this);

                            #region Capturar [Comentarios, estado, procedencia de un exmanen componente]

                            var serviceComponentDto = new servicecomponentDto();
                            serviceComponentDto.v_ServiceComponentId = _serviceComponentId;
                            //Obtener fecha de Actualizacion
                            var FechaUpdate = _serviceBL.GetServiceComponent(ref objOperationResult, _serviceComponentId).d_UpdateDate;
                            serviceComponentDto.v_Comment = txtComentario.Text;
                            //grabar estado del examen según profesión del usuario
                            if (_profesionId == 30) // evaluador
                            {
                                serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Evaluado;
                                _EstadoComponente = (int)ServiceComponentStatus.Evaluado;
                            }
                            else if (_profesionId == 31 || _profesionId == 32)//auditor
                            {
                                serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Auditado;
                                _EstadoComponente = (int)ServiceComponentStatus.Auditado;
                            }
                            serviceComponentDto.i_ServiceComponentStatusId = int.Parse(cbEstadoComponente.SelectedValue.ToString());
                            _EstadoComponente = int.Parse(cbEstadoComponente.SelectedValue.ToString());
                            serviceComponentDto.i_ExternalInternalId = int.Parse(cbTipoProcedenciaExamen.SelectedValue.ToString());
                            serviceComponentDto.i_IsApprovedId = Convert.ToInt32(chkApproved.Checked);

                            serviceComponentDto.v_ComponentId = _componentId;
                            serviceComponentDto.v_ServiceId = _serviceId;
                            serviceComponentDto.d_UpdateDate = FechaUpdate;
                            #endregion

                            // Generar packete con data para grabar y pasarselo al hilo 
                            RunWorkerAsyncPackage packageForSave = new RunWorkerAsyncPackage();

                            if (chkUtilizarFirma.Checked)
                            {
                                var frm = new Popups.frmSelectSignature();
                                frm.ShowDialog();

                                if (frm.DialogResult != System.Windows.Forms.DialogResult.Cancel)
                                {
                                    packageForSave.i_SystemUserSuplantadorId = frm.i_SystemUserSuplantadorId;
                                    var t = new Thread(() =>
                                    {
                                        using (new LoadingClass.PleaseWait(this.Location, "Cargando..."))
                                        {
                                            Thread.Sleep(2500);
                                            MessageBox.Show("Grabado correctamente", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        };
                                        ;
                                    });
                                    t.Start();
                                }
                                else
                                {
                                    var t = new Thread(() =>
                                    {
                                        using (new LoadingClass.PleaseWait(this.Location, "Cargando..."))
                                        {
                                            Thread.Sleep(2500);
                                            MessageBox.Show("Grabado correctamente", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        };
                                        ;
                                    });
                                    t.Start();
                                }

                            }

                            if (cbEstadoComponente.SelectedValue.ToString() == "8")
                            {
                                var frm = new Operations.Popups.frmEspecialista();
                                frm.ShowDialog();
                                if (frm.DialogResult != System.Windows.Forms.DialogResult.Cancel)
                                {
                                    serviceComponentDto.i_SystemUserEspecialistaId = frm.i_SystemUserEspecialistaId;
                                }
                            }


                            packageForSave.SelectedTab = selectedTab;
                            packageForSave.ExamDiagnosticComponentList = _tmpExamDiagnosticComponentList;
                            packageForSave.ServiceComponent = serviceComponentDto;

                            bgwSaveExamen.RunWorkerAsync(packageForSave);

                        }



                    }
                }
                else
                {
                    //MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            catch (Exception ex)
            {
                //CloseErrorfrmWaiting();             
                MessageBox.Show(Common.Utils.ExceptionFormatter(ex), "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveExamWherePendingChange()
        {
            #region Validacion antes de navegar de tab en tab

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

                var somnolencia = _tmpServiceComponentsForBuildMenuList
                                      .Find(p => p.v_ComponentId == _componentId)
                                      .Fields.Find(p => p.i_ControlId == (int)ControlType.UcSomnolencia);

                if (somnolencia != null)
                {
                    var ucSomnolencia = (UserControls.ucSomnolencia)FindControlInCurrentTab(somnolencia.v_ComponentFieldId)[0];

                    if (ucSomnolencia.IsChangeValueControl)
                    {
                        _isChangeValue = true;

                    }
                }


                var acumetria = _tmpServiceComponentsForBuildMenuList
                                       .Find(p => p.v_ComponentId == _componentId)
                                       .Fields.Find(p => p.i_ControlId == (int)ControlType.UcAcumetria);

                if (acumetria != null)
                {
                    var ucAcumetria = (UserControls.ucAcumetria)FindControlInCurrentTab(acumetria.v_ComponentFieldId)[0];

                    if (ucAcumetria.IsChangeValueControl)
                    {
                        _isChangeValue = true;

                    }
                }



                var fototipo = _tmpServiceComponentsForBuildMenuList
                    .Find(p => p.v_ComponentId == _componentId)
                    .Fields.Find(p => p.i_ControlId == (int)ControlType.UcFototipo);

                if (fototipo != null)
                {
                    var ucFotoTipo = (UserControls.ucFotoTipo)FindControlInCurrentTab(fototipo.v_ComponentFieldId)[0];

                    if (ucFotoTipo.IsChangeValueControl)
                    {
                        _isChangeValue = true;

                    }
                }

                var SintomaticoRespi = _tmpServiceComponentsForBuildMenuList
                                 .Find(p => p.v_ComponentId == _componentId)
                                 .Fields.Find(p => p.i_ControlId == (int)ControlType.UcSintomaticoRespi);

                if (SintomaticoRespi != null)
                {
                    var ucSintomaticoRespi = (UserControls.ucSintomaticoResp)FindControlInCurrentTab(SintomaticoRespi.v_ComponentFieldId)[0];

                    if (ucSintomaticoRespi.IsChangeValueControl)
                    {
                        _isChangeValue = true;

                    }
                }

                var RxLumboSacra = _tmpServiceComponentsForBuildMenuList
                               .Find(p => p.v_ComponentId == _componentId)
                               .Fields.Find(p => p.i_ControlId == (int)ControlType.UcRxLumboSacra);

                if (RxLumboSacra != null)
                {
                    var ucRxLumboSacra = (UserControls.ucRXLumboSacra)FindControlInCurrentTab(RxLumboSacra.v_ComponentFieldId)[0];

                    if (ucRxLumboSacra.IsChangeValueControl)
                    {
                        _isChangeValue = true;

                    }
                }

                var Otoscopia = _tmpServiceComponentsForBuildMenuList
                            .Find(p => p.v_ComponentId == _componentId)
                            .Fields.Find(p => p.i_ControlId == (int)ControlType.UcOtoscopia);

                if (Otoscopia != null)
                {
                    var UcOtoscopia = (UserControls.ucOtoscopia)FindControlInCurrentTab(Otoscopia.v_ComponentFieldId)[0];

                    if (UcOtoscopia.IsChangeValueControl)
                    {
                        _isChangeValue = true;

                    }
                }

                var EvaluacionErgonomica = _tmpServiceComponentsForBuildMenuList
                           .Find(p => p.v_ComponentId == _componentId)
                           .Fields.Find(p => p.i_ControlId == (int)ControlType.UcEvaluacionErgonomica);

                if (EvaluacionErgonomica != null)
                {
                    var UcEvaluacionErgonomica = (UserControls.ucEvaluacionErgonomica)FindControlInCurrentTab(EvaluacionErgonomica.v_ComponentFieldId)[0];

                    if (UcEvaluacionErgonomica.IsChangeValueControl)
                    {
                        _isChangeValue = true;

                    }
                }




                var Osteomuscular = _tmpServiceComponentsForBuildMenuList
                         .Find(p => p.v_ComponentId == _componentId)
                         .Fields.Find(p => p.i_ControlId == (int)ControlType.UcOsteoMuscular);

                if (Osteomuscular != null)
                {
                    var UcOsteomuscular = (UserControls.ucOsteoMuscular)FindControlInCurrentTab(Osteomuscular.v_ComponentFieldId)[0];

                    if (UcOsteomuscular.IsChangeValueControl)
                    {
                        _isChangeValue = true;

                    }
                }

                var OjoSeco = _tmpServiceComponentsForBuildMenuList
                             .Find(p => p.v_ComponentId == _componentId)
                             .Fields.Find(p => p.i_ControlId == (int)ControlType.UcOjoSeco);

                if (OjoSeco != null)
                {
                    var ucOjoSeco = (UserControls.ucOjoSeco)FindControlInCurrentTab(OjoSeco.v_ComponentFieldId)[0];

                    if (ucOjoSeco.IsChangeValueControl)
                    {
                        _isChangeValue = true;

                    }
                }

            }

            if (_isChangeValue)
            {
                //e.Cancel = true;

                var result = MessageBox.Show("Ha realizado cambios, desea guardarlos antes de ir a otro exámen.", "CONFIRMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveExamBySelectedTab(tcExamList.SelectedTab.TabPage);
                }
                else
                {
                    _isChangeValue = false;
                    //e.Cancel = false;                  
                }

            }

            #endregion
            //#region Validacion antes de navegar de tab en tab

            //if (_componentId != null)
            //{
            //    var audiometria = _tmpServiceComponentsForBuildMenuList
            //                          .Find(p => p.v_ComponentId == _componentId)
            //                          .Fields.Find(p => p.i_ControlId == (int)ControlType.UcAudiometria);

            //    if (audiometria != null)
            //    {
            //        var ucAudiometria = (UserControls.ucAudiometria)FindControlInCurrentTab(audiometria.v_ComponentFieldId)[0];

            //        if (ucAudiometria.IsChangeValueControl)
            //        {
            //            _isChangeValue = true;
            //        }
            //    }

            //    var odontograma = _tmpServiceComponentsForBuildMenuList
            //                          .Find(p => p.v_ComponentId == _componentId)
            //                          .Fields.Find(p => p.i_ControlId == (int)ControlType.UcOdontograma);

            //    if (odontograma != null)
            //    {
            //        var ucOdontograma = (UserControls.ucOdontograma)FindControlInCurrentTab(odontograma.v_ComponentFieldId)[0];

            //        if (ucOdontograma.IsChangeValueControl)
            //        {
            //            _isChangeValue = true;
            //            ucOdontograma.IsChangeValueControl = false;
            //        }

            //    }


            //}

            //if (_isChangeValue)
            //{
            //    //e.Cancel = true;

            //    var result = MessageBox.Show("Ha realizado cambios, desea guardarlos antes de ir a otro exámen.", "CONFIRMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //    if (result == DialogResult.Yes)
            //    {
            //        SaveExamBySelectedTab(tcExamList.SelectedTab.TabPage);
            //    }
            //    else
            //    {
            //        _isChangeValue = false;
            //        //e.Cancel = false;                  
            //    }

            //}

            //#endregion
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

                        if (keyTagControl.i_ControlId == (int)ControlType.UcOdontograma
                            || keyTagControl.i_ControlId == (int)ControlType.UcAudiometria)
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

        private Control[] FindDynamicControl(string key)
        {
            // Obtener TabPage actual
            var currentTabPage = tcExamList.SelectedTab.TabPage;
            //var findControl = currentTabPage.Controls.Find(key, true);

            var findControl = tcExamList.Tabs.TabControl.Controls.Find(key, true);

            return findControl;
        }

        private void GeneratedAutoDX(string valueToAnalyze, Control senderCtrl, KeyTagControl tagCtrl)
        {
            string componentFieldsId = tagCtrl.v_ComponentFieldsId;

            // Retorna el DX (automático) generado, luego de una serie de evaluaciones.
            var diagnosticRepository = SearchDxSugeridoOfSystem(valueToAnalyze, componentFieldsId);

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
                senderCtrl.BackColor = Color.Pink;   // DX Alterado              

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
                grdDiagnosticoPorExamenComponente.DataSource = dataList;
                lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
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
                    dataType = "int";
                    break;
                case ControlType.SiNoRadioButton:
                    break;
                case ControlType.Radiobutton:
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

        private DiagnosticRepositoryList SearchDxSugeridoOfSystem(string valueToAnalyze, string pComponentFieldsId)
        {
            DiagnosticRepositoryList diagnosticRepository = null;
            string matchValId = null;
            bool exitLoop = false;
            var componentField = _tmpServiceComponentsForBuildMenuList
                                .Find(p => p.v_ComponentId == _componentId)
                                .Fields.Find(p => p.v_ComponentFieldId == pComponentFieldsId);

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
                                    if (int.Parse(valueToAnalyze) == int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) == double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_noesIgualque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) != int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) != double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMenorque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) < int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < 18.5 (bajo peso)
                                    if (double.Parse(valueToAnalyze) < double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMenorIgualque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) <= int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < 18.5 (bajo peso)
                                    if (double.Parse(valueToAnalyze) <= double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorque_A:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) > int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) > double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorIgualque_A:
                                // X >= 40.0 (Obesidad clase III)
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) >= int.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < 18.5 (bajo peso)
                                    if (double.Parse(valueToAnalyze) >= double.Parse(val.v_AnalyzingValue1))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorque_A_yMenorque_B:

                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) > int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyze) < int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) > double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyze) < double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorque_A_yMenorIgualque_B:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) > int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyze) <= int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    // X < A && X <= B 
                                    if (double.Parse(valueToAnalyze) > double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyze) <= double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorIgualque_A_yMenorque_B:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) >= int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyze) < int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    if (double.Parse(valueToAnalyze) >= double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyze) < double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            case Operator2Values.X_esMayorIgualque_A_yMenorIgualque_B:
                                if (dataTypeControlToParse == "int")
                                {
                                    if (int.Parse(valueToAnalyze) >= int.Parse(val.v_AnalyzingValue1) && int.Parse(valueToAnalyze) <= int.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                else if (dataTypeControlToParse == "double")
                                {
                                    var parse = double.Parse(valueToAnalyze);
                                    if (double.Parse(valueToAnalyze) >= double.Parse(val.v_AnalyzingValue1) && double.Parse(valueToAnalyze) <= double.Parse(val.v_AnalyzingValue2))
                                        exitLoop = true;
                                }
                                break;
                            default:
                                MessageBox.Show("valor no encontrado " + valueToAnalyze);
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


                            //if (val.v_DiseasesId == "N009-DD000000789" || val.v_DiseasesId == "N009-DD000000788" || val.v_DiseasesId == "N009-DD000000787" || val.v_DiseasesId == "N009-DD000000786" || val.v_DiseasesId == "N009-DD000000785"
                            //    || val.v_DiseasesId == "N009-DD000000784" || val.v_DiseasesId == "N009-DD000000783" || val.v_DiseasesId == "N009-DD000000782" || val.v_DiseasesId == "N009-DD000000780" || val.v_DiseasesId == "N009-DD000000779"
                            //    || val.v_DiseasesId == "N009-DD000000778" || val.v_DiseasesId == "N009-DD000000777" || val.v_DiseasesId == "N009-DD000000776" || val.v_DiseasesId == "N009-DD000000775" || val.v_DiseasesId == "N009-DD000000774"
                            //    || val.v_DiseasesId == "N009-DD000000661" || val.v_DiseasesId == "N009-DD000000505" || val.v_DiseasesId == "N009-DD000000484" || val.v_DiseasesId == "N009-DD000000483" || val.v_DiseasesId == "N009-DD000000771"
                            //    || val.v_DiseasesId == "N009-DD000000662" || val.v_DiseasesId == "N009-DD000000795" || val.v_DiseasesId == "N009-DD000000797" || val.v_DiseasesId == "N009-DD000000799")
                            //{
                            //if (val.v_CIE10 == "Z000" || val.v_CIE10 == "Z001")
                            //{
                            //    diagnosticRepository.i_DiagnosticTypeId = (int)TipoDx.Normal;
                            //    diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.Descartado;
                            //}
                            //else
                            //{
                            //    diagnosticRepository.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                            //    diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                            //}


                            diagnosticRepository.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                            diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.Descartado;

                            diagnosticRepository.v_ServiceId = _serviceId;
                            diagnosticRepository.v_ComponentId = val.v_ComponentId;
                            diagnosticRepository.v_DiseasesName = val.v_DiseasesName;
                            diagnosticRepository.v_AutoManualName = "AUTOMÁTICO";
                            diagnosticRepository.v_RestrictionsName = ConcatenateRestrictions(val.Restrictions);
                            diagnosticRepository.v_RecomendationsName = ConcatenateRecommendations(val.Recomendations);
                            diagnosticRepository.v_PreQualificationName = "SIN PRE-CALIFICAR";
                            // ID enlace DX automatico para grabar valores dinamicos
                            diagnosticRepository.v_ComponentFieldValuesId = val.v_ComponentFieldValuesId;
                            diagnosticRepository.v_ComponentFieldsId = pComponentFieldsId;
                            diagnosticRepository.Recomendations = RefreshRecomendationList(val.Recomendations);
                            diagnosticRepository.Restrictions = RefreshRestrictionList(val.Restrictions);
                            diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                            diagnosticRepository.i_RecordType = (int)RecordType.Temporal;

                            int vm = val.i_ValidationMonths == null ? 0 : val.i_ValidationMonths.Value;
                            diagnosticRepository.d_ExpirationDateDiagnostic = DateTime.Now.AddMonths(vm);

                            #endregion
                            //#region CREAR / AGREGAR DX (automático)

                            //matchValId = val.v_ComponentFieldValuesId;

                            //// Si el valor analizado se encuentra en el rango de valores NORMALES, 
                            //// entonces NO se genera un DX (automático).
                            //if (val.v_DiseasesId == null)
                            //    break;

                            //val.Recomendations.ForEach(item => { item.v_RecommendationId = Guid.NewGuid().ToString(); });
                            //val.Restrictions.ForEach(item => { item.v_RestrictionByDiagnosticId = Guid.NewGuid().ToString(); });
                            //// Insertar DX sugerido (automático) a la bolsa de DX 
                            //diagnosticRepository = new DiagnosticRepositoryList();
                            //diagnosticRepository.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                            //diagnosticRepository.v_DiseasesId = val.v_DiseasesId;
                            //diagnosticRepository.i_AutoManualId = (int)AutoManual.Automático;
                            //diagnosticRepository.i_PreQualificationId = (int)PreQualification.SinPreCalificar;
                            //diagnosticRepository.i_FinalQualificationId = (int)FinalQualification.SinCalificar;
                            //diagnosticRepository.v_ServiceId = _serviceId;
                            //diagnosticRepository.v_ComponentId = val.v_ComponentId;
                            //diagnosticRepository.v_DiseasesName = val.v_DiseasesName;
                            //diagnosticRepository.v_AutoManualName = "AUTOMÁTICO";
                            //diagnosticRepository.v_RestrictionsName = ConcatenateRestrictions(val.Restrictions);
                            //diagnosticRepository.v_RecomendationsName = ConcatenateRecommendations(val.Recomendations);
                            //diagnosticRepository.v_PreQualificationName = "SIN PRE-CALIFICAR";
                            //// ID enlace DX automatico para grabar valores dinamicos
                            //diagnosticRepository.v_ComponentFieldValuesId = val.v_ComponentFieldValuesId;
                            //diagnosticRepository.v_ComponentFieldsId = pComponentFieldsId;
                            //diagnosticRepository.Recomendations = RefreshRecomendationList(val.Recomendations);
                            //diagnosticRepository.Restrictions = RefreshRestrictionList(val.Restrictions);
                            //diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                            //diagnosticRepository.i_RecordType = (int)RecordType.Temporal;

                            //int vm = val.i_ValidationMonths == null ? 0 : val.i_ValidationMonths.Value;
                            //diagnosticRepository.d_ExpirationDateDiagnostic = DateTime.Now.AddMonths(vm);

                            //#endregion
                            break;
                        }

                    }
                }
            }

            return diagnosticRepository;

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
                restriction.v_ComponentId = item.v_ComponentId;

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
                recomendation.v_RecommendationId = item.v_RecommendationId;
                recomendation.v_MasterRecommendationId = item.v_MasterRecommendationId;  // ID -> RECOME / RESTRIC (BOLSA CONFIG POR M. MENDEZ)
                recomendation.v_RecommendationName = item.v_RecommendationName;
                recomendation.i_RecordStatus = (int)RecordStatus.Agregado;
                recomendation.i_RecordType = (int)RecordType.Temporal;
                recomendation.v_ComponentId = item.v_ComponentId;

                recomendationsList.Add(recomendation);
            }

            return recomendationsList;
        }

        private void bgwSaveExamen_DoWork(object sender, DoWorkEventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            using (new LoadingClass.PleaseWait(this.Location, "Grabando..."))
            {
                RunWorkerAsyncPackage packageForSave = (RunWorkerAsyncPackage)e.Argument;
                bool result = false;
                #region GRABAR CONTROLES DINAMICOS

                var selectedTab = (Infragistics.Win.UltraWinTabControl.UltraTabPageControl)packageForSave.SelectedTab;

                var serviceComponentId = selectedTab.Tab.Tag.ToString();

                ProcessControlBySelectedTab(selectedTab);

                if (packageForSave.ServiceComponent.d_UpdateDate == null)
                {
                    result = new ServiceBL().AddServiceComponentValues(ref objOperationResult,
                                                           _serviceComponentFieldsList,
                                                           Globals.ClientSession.GetAsList(),
                                                           _personId,
                                                           serviceComponentId);
                }
                else
                {
                    var DatosAntiguos = _oListValidacionAMC.AsEnumerable()
                  .GroupBy(x => x.v_ServiceComponentFieldValuesId)
                  .Select(group => group.First()).ToList();

                    List<DatosModificados> ListaDatoModificado = new List<DatosModificados>();
                    DatosModificados oDatosModificados = null;

                    foreach (var itemAntiguo in DatosAntiguos)
                    {
                        var Coincidencia = _serviceComponentFieldsList.FindAll(p => p.v_ComponentFieldsId == itemAntiguo.v_ComponentFieldsId);

                        if (Coincidencia.Count != 0)
                        {
                            if (itemAntiguo.v_Value1 != Coincidencia[0].ServiceComponentFieldValues[0].v_Value1)
                            {
                                oDatosModificados = new DatosModificados();
                                oDatosModificados.v_ComponentFieldsId = itemAntiguo.v_ComponentFieldsId;
                                ListaDatoModificado.Add(oDatosModificados);
                            }
                        }
                    }

                    //Agregar los datos nuevos

                    foreach (var item in _serviceComponentFieldsList)
                    {
                        var Coincidencia = DatosAntiguos.FindAll(p => p.v_ComponentFieldsId == item.v_ComponentFieldsId);
                        if (Coincidencia.Count == 0)
                        {
                            oDatosModificados = new DatosModificados();
                            oDatosModificados.v_ComponentFieldsId = item.v_ComponentFieldsId;
                            ListaDatoModificado.Add(oDatosModificados);
                        }
                    }

                    string[] ListaDatoModificado_ = new string[ListaDatoModificado.Count()];
                    for (int i = 0; i < ListaDatoModificado.Count(); i++)
                    {
                        ListaDatoModificado_[i] = ListaDatoModificado[i].v_ComponentFieldsId;
                    }

                    //var DatosGrabar = _serviceComponentFieldsList.FindAll(p => p.v_ComponentFieldsId.Contains(ListaDatoModificado.));
                    var DatosGrabar = _serviceComponentFieldsList.FindAll(p => ListaDatoModificado_.Contains(p.v_ComponentFieldsId));


                    result = new ServiceBL().AddServiceComponentValues(ref objOperationResult,
                                                                DatosGrabar,
                                                                Globals.ClientSession.GetAsList(),
                                                                _personId,
                                                                _serviceComponentId);
                }
               

                #endregion

                #region GRABAR DATOS ADICIONALES COMO [Diagnósticos + restricciones + recomendaciones]

                // Grabar Dx por examen componente mas sus restricciones
                if (packageForSave.i_SystemUserSuplantadorId != null)
                {
                    Globals.ClientSession.i_SystemUserId = (int)packageForSave.i_SystemUserSuplantadorId;
                }
                else
                {
                    Globals.ClientSession.i_SystemUserId = Globals.ClientSession.i_SystemUserCopyId;
                }

                //new ServiceBL().AddDiagnosticRepository(ref objOperationResult,
                //                                    packageForSave.ExamDiagnosticComponentList,
                //                                    packageForSave.ServiceComponent,
                //                                    Globals.ClientSession.GetAsList(),
                //                                    _chkApprovedEnabled);
                new ServiceBL().AddDiagnosticRepository(ref objOperationResult,
                                    packageForSave.ExamDiagnosticComponentList,
                                    packageForSave.ServiceComponent,
                                    Globals.ClientSession.GetAsList(),
                                    _chkApprovedEnabled, chkUtilizarFirma.Checked);



                #endregion

                // Limpiar lista temp
                _serviceComponentFieldsList = null;
                _tmpListValuesOdontograma = null;

                if (!result)
                {
                    MessageBox.Show("Error al grabar los componentes. Comunicarse con el administrador del sistema", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                // Analizar el resultado de la operación
                if (objOperationResult.Success != 1)
                {
                    //CloseErrorfrmWaiting();
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.Invoke((MethodInvoker)(() =>
                {
                    #region refrescar

                    flagValueChange = false;
                    //InitializeData();
                    //LoadDataBySelectedComponent(_componentId);
                    GetTotalDiagnosticsForGridView();
                    ConclusionesyTratamiento_LoadAllGrid();

                    #endregion

                }));

            }
        }

        private void bgwSaveExamen_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
        }

        private void tcExamList_SelectedTabChanging(object sender, SelectedTabChangingEventArgs e)
        {
            var t = new Thread(() =>
            {
                using (new LoadingClass.PleaseWait(this.Location, "Cargando..."))
                {
                    Thread.Sleep(2500);
                };
                ;
            });
            t.Start();
            var obj = e.Tab.TabControl.ActiveTab;
            SaveExamWherePendingChange();
            
        }

        private Control[] FindControlInCurrentTab_OIT(string key)
        {
            var currentTabPage = tcExamList.SelectedTab.TabPage;
            var findControl = currentTabPage.Controls.Find(key, true);
            return findControl;
        }

        private void btnGuardarConclusiones_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("¿Está seguro de grabar este registro?:", "CONFIRMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Result == DialogResult.Yes)
            {

                OperationResult objOperationResult = new OperationResult();

                var serviceDTO = new serviceDto();

                //int? hazRestriction = null;

                // Datos de Servicio
                serviceDTO.v_ServiceId = _serviceId;
                //serviceDTO.i_HasRestrictionId = hazRestriction;

                // datos de cabecera del Servicio
                serviceDTO.i_AptitudeStatusId = int.Parse(cbAptitudEso.SelectedValue.ToString());

                //if (txtFecVctoGlobal.Text != dtpFecVctoGlobal.Value.ToShortTimeString())
                //{
                //    DialogResult Result2 = MessageBox.Show("¿Está seguro de Actualizar la Fecha de Vencimiento?:", "CONFIRMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //    if (Result2 == DialogResult.Yes)
                //    { serviceDTO.d_GlobalExpirationDate = dtpFecVctoGlobal.Value; }
                //    else
                //    { serviceDTO.d_GlobalExpirationDate = DateTime.Parse(txtFecVctoGlobal.Text); ; }
                //}

                #region UTILIZAR FIRMA (Suplantar profesional)

                if (chkUtilizaFirmaAptitud.Checked)
                {
                    var frm = new Popups.frmSelectSignature();
                    frm.ShowDialog();

                    if (frm.DialogResult != System.Windows.Forms.DialogResult.Cancel)
                    {
                        if (frm.i_SystemUserSuplantadorId != null)
                        {
                            Globals.ClientSession.i_SystemUserId = (int)frm.i_SystemUserSuplantadorId;
                        }
                        else
                        {
                            Globals.ClientSession.i_SystemUserId = Globals.ClientSession.i_SystemUserCopyId;
                        }
                    }
                }
                else
                {
                    Globals.ClientSession.i_SystemUserId = Globals.ClientSession.i_SystemUserCopyId;
                }

                #endregion

                new ServiceBL().AddConclusiones(ref objOperationResult,
                                           _tmpRestrictionListConclusiones,
                                           _tmpRecomendationConclusionesList,
                                           serviceDTO,
                                           null,
                                           Globals.ClientSession.GetAsList(), checkFirmaYanacocha.Checked);


                // Refrescar todas las grillas
                //InitializeData();
                ConclusionesyTratamiento_LoadAllGrid();

                // Analizar el resultado de la operación
                if (objOperationResult.Success != 1)
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    #region Mensaje de información de guardado
                    DialogResult Result1 = MessageBox.Show("Se guardó correctamente.", "CORRECTO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    #endregion
                }


            }
        }

        List<string> DevolverListaCAPs(string pstrServiceId)
        {
            List<ServiceComponentList> serviceComponents = new List<ServiceComponentList>();
            List<ServiceComponentList> ListaOrdenada = new List<ServiceComponentList>();

            //serviceComponents = _serviceBL.GetServiceComponentsForManagementReport(pstrServiceId);

            List<ServiceComponentList> ConsolidadoReportes = new List<ServiceComponentList>();
            ConsolidadoReportes.Add(new ServiceComponentList { Orden = 1, v_ComponentName = "CERTIFICADO APTITUD", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD });

            ListaOrdenada = ConsolidadoReportes.OrderBy(p => p.Orden).ToList();
            List<string> Lista = new List<string>();

            foreach (var item in ListaOrdenada)
            {
                Lista.Add(item.v_ComponentId);
            }
            return Lista;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewEditAntecedent();
        }

        private void ViewEditAntecedent()
        {
            frmHistory frm = new frmHistory(_personId);
            frm.MdiParent = this.MdiParent;
            frm.Show();
            // refresca grilla de antecedentes
            GetAntecedentConsolidateForService(_personId);
        }

        private void btnCerrarESO_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void mnuVerServicio_Click(object sender, EventArgs e)
        {
            var frm = new Operations.FrmEsoV2(_serviceIdByWiewServiceHistory, null, "View", Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, (int)MasterService.Eso);
            frm.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var datosP = new PacientBL().DevolverDatosPaciente(_serviceId);

            var ServiceDate = grdServiciosAnteriores.Selected.Rows[0].Cells["d_ServiceDate"].Value.ToString();

            if (ServiceDate.ToString().Split(' ')[0] == DateTime.Now.ToString().Split(' ')[0])
            {
                var frm = new Operations.FrmEsoV2(_serviceIdByWiewServiceHistory, null, "", Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, (int)MasterService.Eso);
                frm.ShowDialog();
            }
            else
            {
                var frm = new Operations.FrmEsoV2(_serviceIdByWiewServiceHistory, null, "View", Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, (int)MasterService.Eso);
                frm.ShowDialog();
            }
        }

        private void btnPerson_Click_1(object sender, EventArgs e)
        {
            var frm = new frmPacient(_personId);
            frm.ShowDialog();
        }

        private void btnReceta_Click(object sender, EventArgs e)
        {
            //grdTotalDiagnosticos.DataSource = _tmpTotalDiagnosticByServiceIdList;
            frmRecetaMedica frm = new frmRecetaMedica(_tmpTotalDiagnosticByServiceIdList, _serviceId, _ProtocolId);
            frm.ShowDialog();
        }

        private void btnVisorReporteExamen_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando Reporte..."))
            {
                OperationResult objOperationResult = new OperationResult();
                PacientListNew filiationData = new PacientBL().GetPacientReportEPS_NewOrganizationId(_serviceId);
                PacientId = filiationData.PersonId;
                frmManagementReports frmManagmentReport = new frmManagementReports();
                DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
                List<ServiceComponentListReportSolo> serviceComponents = _serviceBL.GetServiceComponentsReportForReportSolo(_serviceId);
                var arrComponentId = _componentId.Split('|');
                #region audiometria
                if (arrComponentId.Contains(Constants.AUDIOMETRIA_ID)
                    || arrComponentId.Contains("N009-ME000000337")
                    || arrComponentId.Contains(Constants.AUDIO_COIMOLACHE))
                {
                    List<string> componentIds = new List<string>();
                    ServiceComponentListReportSolo audiometria = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.AUDIOMETRIA_ID);
                    ServiceComponentListReportSolo cuestionarioEspCoimolache = serviceComponents.Find(p => p.v_ComponentId == "N009-ME000000337");
                    ServiceComponentListReportSolo audioCoimolache = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.AUDIO_COIMOLACHE);

                    if (audiometria != null)
                    {
                        if (filiationData.EmpresaClienteId == "N009-OO000000587")
                        {
                            componentIds.Add(Constants.AUDIOMETRIA_ID + "|40");
                        }
                        else
                        {
                            componentIds.Add(Constants.AUDIOMETRIA_ID);
                        }
                    }
                    if (cuestionarioEspCoimolache != null)
                    {
                        componentIds.Add("N009-ME000000337");
                    }
                    if (audioCoimolache != null)
                    {
                        componentIds.Add(Constants.AUDIO_COIMOLACHE);
                    }

                    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
                }
                #endregion
                #region cardiologia
                else if (arrComponentId.Contains(Constants.APENDICE_ID)//cardiologia
                        || arrComponentId.Contains(Constants.ELECTROCARDIOGRAMA_ID)
                        || arrComponentId.Contains(Constants.ELECTRO_GOLD)
                        || arrComponentId.Contains(Constants.PRUEBA_ESFUERZO_ID))
                {
                    List<string> componentIds = new List<string>();
                    ServiceComponentListReportSolo apendice = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.APENDICE_ID);
                    ServiceComponentListReportSolo electrocardiograma = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ELECTROCARDIOGRAMA_ID);
                    ServiceComponentListReportSolo electroGold = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ELECTRO_GOLD);
                    ServiceComponentListReportSolo pruebaEsfuerzo = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PRUEBA_ESFUERZO_ID);

                    if (apendice != null)
                    {
                        componentIds.Add(Constants.APENDICE_ID + "|43");
                    }
                    if (electrocardiograma != null)
                    {
                        componentIds.Add(Constants.ELECTROCARDIOGRAMA_ID);
                    }
                    if (electroGold != null)
                    {
                        componentIds.Add(Constants.ELECTRO_GOLD);
                    }
                    if (pruebaEsfuerzo != null)
                    {
                        componentIds.Add(Constants.PRUEBA_ESFUERZO_ID);
                    }

                    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
                }
                #endregion
                #region espirometria
                else if (arrComponentId.Contains(Constants.ESPIROMETRIA_ID))
                {
                    List<string> componentIds = new List<string>();
                    ServiceComponentListReportSolo espirometria = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ESPIROMETRIA_ID);

                    if (espirometria != null)
                    {
                        if (filiationData.EmpresaClienteId == "N009-OO000000587")
                        {
                            componentIds.Add(Constants.ESPIROMETRIA_ID + "|35");
                        }
                        else if (filiationData.EmpresaClienteId == "N009-OO000000589"
                            || filiationData.EmpresaClienteId == "N009-OO000000590"
                            || filiationData.EmpresaClienteId == "N002-OO000003575")
                        {
                            componentIds.Add(Constants.ESPIROMETRIA_ID + "|54");
                        }
                        else if (filiationData.EmpresaClienteId == "N009-OO000000591")
                        {
                            componentIds.Add(Constants.ESPIROMETRIA_ID + "|58");
                        }
                        else
                        {
                            componentIds.Add(Constants.INFORME_ESPIROMETRIA + "|46");
                        }
                    }
                    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
                }
                #endregion
                #region laboratorio
                else if (arrComponentId.Contains(Constants.INFORME_LABORATORIO_ID)//laboratorio
                        || arrComponentId.Contains(Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID))
                {
                    List<string> componentIds = new List<string>();
                    ServiceComponentListReportSolo informeLab = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.INFORME_LABORATORIO_ID);
                    ServiceComponentListReportSolo toxCocaMarihuana = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID);

                    if (informeLab != null)
                    {
                        componentIds.Add(Constants.INFORME_LABORATORIO_CLINICO);
                    }
                    if (toxCocaMarihuana != null)
                    {
                        if (filiationData.EmpresaClienteId == "N009-OO000000587")
                        {
                            componentIds.Add(Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID + "|48");
                        }
                        else if (filiationData.EmpresaClienteId == "N009-OO000000589")
                        {
                            componentIds.Add(Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID);
                        }
                    }


                    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
                }
                #endregion
                #region medicina
                else if (arrComponentId.Contains(Constants.EXAMEN_FISICO_7C_ID)//medicina
                        || arrComponentId.Contains(Constants.EXAMEN_FISICO_ID)
                        || arrComponentId.Contains(Constants.ATENCION_INTEGRAL_ID)
                        || arrComponentId.Contains(Constants.C_N_ID)//cuestionario nordico
                        || arrComponentId.Contains(Constants.FICHA_SAS_ID)
                        || arrComponentId.Contains(Constants.EVA_ERGONOMICA_ID)
                        || arrComponentId.Contains(Constants.EVA_NEUROLOGICA_ID)
                        || arrComponentId.Contains(Constants.ALTURA_7D_ID)
                        || arrComponentId.Contains(Constants.ALTURA_ESTRUCTURAL_ID)
                        || arrComponentId.Contains(Constants.EXAMEN_SUF_MED__OPERADORES_ID)
                        || arrComponentId.Contains(Constants.OSTEO_MUSCULAR_ID_1)
                        || arrComponentId.Contains(Constants.FOTO_TIPO_ID)
                        || arrComponentId.Contains(Constants.OSTEO_COIMO)
                        || arrComponentId.Contains(Constants.SINTOMATICO_ID)
                        || arrComponentId.Contains(Constants.FICHA_SUFICIENCIA_MEDICA_ID)
                        || arrComponentId.Contains(Constants.TAMIZAJE_DERMATOLOGIO_ID)
                        || arrComponentId.Contains(Constants.TEST_VERTIGO_ID)
                        || arrComponentId.Contains(Constants.EVA_OSTEO_ID))
                {
                    List<string> componentIds = new List<string>();
                    ServiceComponentListReportSolo anexo16 = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_ID);
                    ServiceComponentListReportSolo anexo312 = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ID);
                    ServiceComponentListReportSolo atencionIntegral = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ID);
                    ServiceComponentListReportSolo cuestionarioNordico = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.C_N_ID);
                    ServiceComponentListReportSolo sas = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FICHA_SAS_ID);
                    ServiceComponentListReportSolo evaluacionErgonomica = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EVA_ERGONOMICA_ID);
                    ServiceComponentListReportSolo evaluacionNeurologica = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EVA_NEUROLOGICA_ID);
                    ServiceComponentListReportSolo alturageografica = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ALTURA_7D_ID);
                    ServiceComponentListReportSolo alturaestructural = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_ID);
                    ServiceComponentListReportSolo suficienciamedica = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_ID);
                    ServiceComponentListReportSolo osteoMuscular = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OSTEO_MUSCULAR_ID_1);
                    ServiceComponentListReportSolo fotoTipo = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FOTO_TIPO_ID);
                    ServiceComponentListReportSolo osteoCoimo = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OSTEO_COIMO);
                    ServiceComponentListReportSolo sintomatico = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.SINTOMATICO_ID);
                    ServiceComponentListReportSolo fichaSufcMedica = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_ID);
                    ServiceComponentListReportSolo TamizajeDermat = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TAMIZAJE_DERMATOLOGIO_ID);
                    ServiceComponentListReportSolo testVertigo = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TEST_VERTIGO_ID);
                    ServiceComponentListReportSolo evaluacionOsteomuscular = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EVA_OSTEO_ID);

                    if (anexo16 != null)
                    {
                        if (filiationData.EmpresaClienteId == "N009-OO000000587")
                        {
                            componentIds.Add(Constants.INFORME_ANEXO_16_YANACOCHA);
                        }
                        else if (filiationData.EmpresaClienteId == "N009-OO000000589")
                        {
                            componentIds.Add(Constants.INFORME_ANEXO_16_GOLD_FIELD);
                        }
                        else if (filiationData.EmpresaClienteId == "N009-OO000000591")
                        {
                            componentIds.Add(Constants.INFORME_ANEXO_16_SHAHUINDO);
                        }
                        else if (filiationData.EmpresaClienteId == "N009-OO000000590" || filiationData.EmpresaClienteId == "N002-OO000003575")
                        {
                            componentIds.Add(Constants.INFORME_ANEXO_16_COIMOLACHE);
                        }
                        else
                        {
                            componentIds.Add(Constants.INFORME_ANEXO_7C);
                        }
                    }
                    if (anexo312 != null)
                    {
                        componentIds.Add(Constants.EXAMEN_FISICO_ID);
                    }
                    if (atencionIntegral != null)
                    {
                        componentIds.Add(Constants.ATENCION_INTEGRAL_ID);
                    }
                    if (cuestionarioNordico != null)
                    {
                        componentIds.Add(Constants.C_N_ID);
                    }
                    if (sas != null)
                    {
                        componentIds.Add(Constants.FICHA_SAS_ID);
                    }
                    if (evaluacionErgonomica != null)
                    {
                        componentIds.Add(Constants.EVA_ERGONOMICA_ID);
                    }
                    if (evaluacionNeurologica != null)
                    {
                        componentIds.Add(Constants.EVA_NEUROLOGICA_ID);
                    }
                    if (alturageografica != null)
                    {
                        componentIds.Add(Constants.ALTURA_7D_ID);
                    }
                    if (alturaestructural != null)
                    {
                        componentIds.Add(Constants.ALTURA_ESTRUCTURAL_ID);
                    }
                    if (suficienciamedica != null)
                    {
                        componentIds.Add(Constants.EXAMEN_SUF_MED__OPERADORES_ID);
                    }
                    if (osteoMuscular != null)
                    {
                        componentIds.Add(Constants.OSTEO_MUSCULAR_ID_1);
                    }
                    if (fotoTipo != null)
                    {
                        componentIds.Add(Constants.FOTO_TIPO_ID);
                    }
                    if (osteoCoimo != null)
                    {
                        componentIds.Add(Constants.OSTEO_COIMO);
                    }
                    if (sintomatico != null)
                    {
                        componentIds.Add(Constants.SINTOMATICO_ID);
                    }
                    if (fichaSufcMedica != null)
                    {
                        componentIds.Add(Constants.FICHA_SUFICIENCIA_MEDICA_ID);
                    }
                    if (TamizajeDermat != null)
                    {
                        componentIds.Add(Constants.TAMIZAJE_DERMATOLOGIO_ID);
                    }
                    if (testVertigo != null)
                    {
                        componentIds.Add(Constants.TEST_VERTIGO_ID);
                    }
                    if (evaluacionOsteomuscular != null)
                    {
                        componentIds.Add(Constants.EVA_OSTEO_ID);
                    }
                    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
                }
                #endregion
                #region odontologia
                else if (arrComponentId.Contains(Constants.ODONTOGRAMA_ID))
                {
                    List<string> componentIds = new List<string>();
                    ServiceComponentListReportSolo odontograma = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ODONTOGRAMA_ID);

                    if (odontograma != null)
                    {
                        componentIds.Add(Constants.ODONTOGRAMA_ID);
                    }
                    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
                }
                #endregion
                #region oftalmologia
                else if (arrComponentId.Contains(Constants.OFTALMOLOGIA_ID)//oftalmologia
                        || arrComponentId.Contains(Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID)
                        || arrComponentId.Contains(Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID)
                        || arrComponentId.Contains(Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID)
                        || arrComponentId.Contains(Constants.INFORME_OFTALMOLOGICO_HUDBAY_ID)
                        || arrComponentId.Contains(Constants.PETRINOVIC_ID)
                        || arrComponentId.Contains(Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_ID)
                        || arrComponentId.Contains("N009-ME000000437"))
                {
                    List<string> componentIds = new List<string>();
                    ServiceComponentListReportSolo agudezavisual = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OFTALMOLOGIA_ID);
                    ServiceComponentListReportSolo oftsimple = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID);
                    ServiceComponentListReportSolo oftcompleto = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID);
                    ServiceComponentListReportSolo oftYanacocha = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID);
                    ServiceComponentListReportSolo oftHudbay = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_ID);
                    ServiceComponentListReportSolo petrinovic = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PETRINOVIC_ID);
                    ServiceComponentListReportSolo certificadoPsico = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_ID);
                    ServiceComponentListReportSolo oftalmoPsico = serviceComponents.Find(p => p.v_ComponentId == "N009-ME000000437");

                    if (agudezavisual != null)
                    {
                        componentIds.Add(Constants.OFTALMOLOGIA_ID);
                    }
                    if (oftsimple != null)
                    {
                        componentIds.Add(Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID);
                    }
                    if (oftcompleto != null)
                    {
                        componentIds.Add(Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID);
                    }
                    if (oftYanacocha != null)
                    {
                        componentIds.Add(Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID);
                    }
                    if (oftHudbay != null)
                    {
                        componentIds.Add(Constants.INFORME_OFTALMOLOGICO_HUDBAY_ID);
                    }
                    if (petrinovic != null)
                    {
                        componentIds.Add(Constants.PETRINOVIC_ID);
                    }
                    if (certificadoPsico != null)
                    {
                        componentIds.Add(Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_ID);
                    }
                    if (oftalmoPsico != null)
                    {
                        componentIds.Add("N009-ME000000437");
                    }

                    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
                }
                #endregion
                #region psicologia
                else if (arrComponentId.Contains(Constants.PSICOLOGIA_ID)//psicologia
                        || arrComponentId.Contains(Constants.HISTORIA_CLINICA_PSICOLOGICA_ID)
                        || arrComponentId.Contains(Constants.FICHA_PSICOLOGICA_OCUPACIONAL_GOLDFIELDS)
                        || arrComponentId.Contains(Constants.INFORME_PSICOLOGICO_OCUPACIONAL_GOLDFIELDS)
                        || arrComponentId.Contains(Constants.SOMNOLENCIA_ID))
                {
                    List<string> componentIds = new List<string>();

                    ServiceComponentListReportSolo psico = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PSICOLOGIA_ID);
                    ServiceComponentListReportSolo psicoHist = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HISTORIA_CLINICA_PSICOLOGICA_ID);
                    ServiceComponentListReportSolo psicoGoldHis = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_OCUPACIONAL_GOLDFIELDS);
                    ServiceComponentListReportSolo psicoGolFich = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_OCUPACIONAL_GOLDFIELDS);
                    ServiceComponentListReportSolo somnolencia = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.SOMNOLENCIA_ID);


                    if (psico != null)
                    {
                        componentIds.Add(Constants.PSICOLOGIA_ID);
                    }
                    if (psicoHist != null)
                    {
                        if (filiationData.EmpresaClienteId == "N009-OO000000587")
                        {
                            componentIds.Add(Constants.HISTORIA_CLINICA_PSICOLOGICA_ID + "|42");
                        }
                        else
                        {
                            componentIds.Add(Constants.HISTORIA_CLINICA_PSICOLOGICA_ID);
                        }
                    }
                    if (psicoGoldHis != null)
                    {
                        componentIds.Add(Constants.FICHA_PSICOLOGICA_OCUPACIONAL_GOLDFIELDS);
                    }
                    if (psicoGolFich != null)
                    {
                        componentIds.Add(Constants.INFORME_PSICOLOGICO_OCUPACIONAL_GOLDFIELDS);
                    }
                    if (somnolencia != null)
                    {
                        componentIds.Add(Constants.SOMNOLENCIA_ID);
                    }

                    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
                }
                #endregion
                #region rayosx
                else if (arrComponentId.Contains(Constants.OIT_ID)//rayos x
                        || arrComponentId.Contains(Constants.RX_TORAX_ID)
                        || arrComponentId.Contains(Constants.EXCEPCIONES_RX_ID)
                        || arrComponentId.Contains(Constants.EXCEPCIONES_RX_AUTORIZACION_ID))
                {
                    List<string> componentIds = new List<string>();
                    ServiceComponentListReportSolo oit = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OIT_ID);
                    ServiceComponentListReportSolo torax = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.RX_TORAX_ID);
                    ServiceComponentListReportSolo excepciones = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXCEPCIONES_RX_ID);
                    ServiceComponentListReportSolo autorizacion = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXCEPCIONES_RX_AUTORIZACION_ID);

                    if (oit != null)
                    {
                        if (filiationData.EmpresaClienteId == "N009-OO000000587")
                        {
                            componentIds.Add(Constants.OIT_ID + "|45");
                        }
                        else
                        {
                            componentIds.Add(Constants.OIT_ID);
                        }
                    }
                    if (torax != null)
                    {
                        componentIds.Add(Constants.RX_TORAX_ID);
                    }

                    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
                }
                #endregion
                #region lumbar
                else if (arrComponentId.Contains(Constants.LUMBOSACRA_ID)
                        || arrComponentId.Contains(Constants.ELECTROENCEFALOGRAMA_ID)
                        || arrComponentId.Contains("N009-ME000000302"))
                {
                    List<string> componentIds = new List<string>();
                    ServiceComponentListReportSolo lumbosacra = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.LUMBOSACRA_ID);
                    ServiceComponentListReportSolo electroencefalograma = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ELECTROENCEFALOGRAMA_ID);
                    ServiceComponentListReportSolo columnaCervicoDorso = serviceComponents.Find(p => p.v_ComponentId == "N009-ME000000302");

                    if (lumbosacra != null)
                    {
                        componentIds.Add(Constants.LUMBOSACRA_ID);
                    }
                    if (electroencefalograma != null)
                    {
                        componentIds.Add(Constants.ELECTROENCEFALOGRAMA_ID);
                    }

                    if (columnaCervicoDorso != null)
                    {
                        componentIds.Add("N009-ME000000302");
                    }

                    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
                }
                #endregion
            };

            #region Old
            //OperationResult objOperationResult = new OperationResult();

            //ServiceList personData = new ServiceBL().GetServicePersonData(ref objOperationResult, _serviceId);

            //ServiceList _DataService = new ServiceBL().GetServiceReport(_serviceId);


            //PacientList filiationData = new PacientBL().GetPacientReportEPS(_serviceId);

            //idPerson = new AtencionesIntegralesBL().GetService(_serviceId);
            //PacientId = idPerson.v_PersonId.ToString();

            //frmManagementReports frmManagmentReport = new frmManagementReports();
            //DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();

            //List<ServiceComponentList> serviceComponents = new ServiceBL().GetServiceComponentsReport(_serviceId);

            //var arrComponentId = _componentId.Split('|');
            //#region audiometria
            //if (arrComponentId.Contains(Constants.AUDIOMETRIA_ID)
            //    || arrComponentId.Contains("N009-ME000000337")
            //    || arrComponentId.Contains(Constants.AUDIO_COIMOLACHE))
            //{
            //    List<string> componentIds = new List<string>();
            //    ServiceComponentList audiometria = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.AUDIOMETRIA_ID);
            //    ServiceComponentList cuestionarioEspCoimolache = serviceComponents.Find(p => p.v_ComponentId == "N009-ME000000337");
            //    ServiceComponentList audioCoimolache = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.AUDIO_COIMOLACHE);

            //    if (audiometria != null)
            //    {
            //        //    componentIds.Add(Constants.AUDIOMETRIA_ID);
            //        if (filiationData.EmpresaClienteId == "N009-OO000000587")
            //        {
            //            componentIds.Add(Constants.AUDIOMETRIA_ID + "|40");
            //        }
            //        else
            //        {
            //            componentIds.Add(Constants.AUDIOMETRIA_ID);
            //        }
            //    }
            //    if (cuestionarioEspCoimolache != null)
            //    {
            //        //if (filiationData.EmpresaClienteId == "N009-OO000000591")
            //        //{
            //        componentIds.Add("N009-ME000000337");
            //        //}
            //    }
            //    if (audioCoimolache != null)
            //    {
            //        //if (filiationData.EmpresaClienteId == "N009-OO000000589"
            //        //    || filiationData.EmpresaClienteId == "N009-OO000000590")
            //        //{
            //        componentIds.Add(Constants.AUDIO_COIMOLACHE);
            //        //}
            //    }

            //    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
            //}
            //#endregion
            //#region cardiologia
            //else if (arrComponentId.Contains(Constants.APENDICE_ID)//cardiologia
            //        || arrComponentId.Contains(Constants.ELECTROCARDIOGRAMA_ID)
            //        || arrComponentId.Contains(Constants.ELECTRO_GOLD)
            //        || arrComponentId.Contains(Constants.PRUEBA_ESFUERZO_ID))
            //{
            //    List<string> componentIds = new List<string>();
            //    ServiceComponentList apendice = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.APENDICE_ID);
            //    ServiceComponentList electrocardiograma = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ELECTROCARDIOGRAMA_ID);
            //    ServiceComponentList electroGold = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ELECTRO_GOLD);
            //    ServiceComponentList pruebaEsfuerzo = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PRUEBA_ESFUERZO_ID);

            //    if (apendice != null)
            //    {
            //        //if (filiationData.EmpresaClienteId == "N009-OO000000587")
            //        //{
            //        componentIds.Add(Constants.APENDICE_ID + "|43");
            //        //}
            //    }
            //    if (electrocardiograma != null)
            //    {
            //        componentIds.Add(Constants.ELECTROCARDIOGRAMA_ID);
            //    }
            //    if (electroGold != null)
            //    {
            //        //if (filiationData.EmpresaClienteId == "N009-OO000000589"
            //        //    || filiationData.EmpresaClienteId == "N009-OO000000590"
            //        //    || filiationData.EmpresaClienteId == "N009-OO000000591")
            //        //{
            //        componentIds.Add(Constants.ELECTRO_GOLD);
            //        //}
            //    }
            //    if (pruebaEsfuerzo != null)
            //    {
            //        componentIds.Add(Constants.PRUEBA_ESFUERZO_ID);
            //    }

            //    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
            //}
            //#endregion
            //#region espirometria
            //else if (arrComponentId.Contains(Constants.ESPIROMETRIA_ID))
            //{
            //    List<string> componentIds = new List<string>();
            //    ServiceComponentList espirometria = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ESPIROMETRIA_ID);

            //    if (espirometria != null)
            //    {
            //        if (filiationData.EmpresaClienteId == "N009-OO000000587")
            //        {
            //            componentIds.Add(Constants.ESPIROMETRIA_ID + "|35");
            //        }
            //        else if (filiationData.EmpresaClienteId == "N009-OO000000589"
            //            || filiationData.EmpresaClienteId == "N009-OO000000590"
            //            || filiationData.EmpresaClienteId == "N002-OO000003575")
            //        {
            //            componentIds.Add(Constants.ESPIROMETRIA_ID + "|54");
            //        }
            //        else if (filiationData.EmpresaClienteId == "N009-OO000000591")
            //        {
            //            componentIds.Add(Constants.ESPIROMETRIA_ID + "|58");
            //        }
            //        else
            //        {
            //            componentIds.Add(Constants.INFORME_ESPIROMETRIA + "|46");
            //        }
            //    }
            //    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
            //}
            //#endregion
            //#region laboratorio
            //else if (arrComponentId.Contains(Constants.INFORME_LABORATORIO_ID)//laboratorio
            //        || arrComponentId.Contains(Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID))
            //{
            //    List<string> componentIds = new List<string>();
            //    ServiceComponentList informeLab = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.INFORME_LABORATORIO_ID);
            //    ServiceComponentList toxCocaMarihuana = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID);

            //    if (informeLab != null)
            //    {
            //        componentIds.Add(Constants.INFORME_LABORATORIO_CLINICO);
            //    }
            //    if (toxCocaMarihuana != null)
            //    {
            //        if (filiationData.EmpresaClienteId == "N009-OO000000587")
            //        {
            //            componentIds.Add(Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID + "|48");
            //        }
            //        else if (filiationData.EmpresaClienteId == "N009-OO000000589")
            //        {
            //            componentIds.Add(Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID);
            //        }
            //    }


            //    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
            //}
            //#endregion
            //#region medicina
            //else if (arrComponentId.Contains(Constants.EXAMEN_FISICO_7C_ID)//medicina
            //        || arrComponentId.Contains(Constants.EXAMEN_FISICO_ID)
            //        || arrComponentId.Contains(Constants.ATENCION_INTEGRAL_ID)
            //        || arrComponentId.Contains(Constants.C_N_ID)//cuestionario nordico
            //        || arrComponentId.Contains(Constants.FICHA_SAS_ID)
            //        || arrComponentId.Contains(Constants.EVA_ERGONOMICA_ID)
            //        || arrComponentId.Contains(Constants.EVA_NEUROLOGICA_ID)
            //        || arrComponentId.Contains(Constants.ALTURA_7D_ID)
            //        || arrComponentId.Contains(Constants.ALTURA_ESTRUCTURAL_ID)
            //        || arrComponentId.Contains(Constants.EXAMEN_SUF_MED__OPERADORES_ID)
            //        || arrComponentId.Contains(Constants.OSTEO_MUSCULAR_ID_1)
            //        || arrComponentId.Contains(Constants.FOTO_TIPO_ID)
            //        || arrComponentId.Contains(Constants.OSTEO_COIMO)
            //        || arrComponentId.Contains(Constants.SINTOMATICO_ID)
            //        || arrComponentId.Contains(Constants.FICHA_SUFICIENCIA_MEDICA_ID)
            //        || arrComponentId.Contains(Constants.TAMIZAJE_DERMATOLOGIO_ID)
            //        || arrComponentId.Contains(Constants.TEST_VERTIGO_ID)
            //        || arrComponentId.Contains(Constants.EVA_OSTEO_ID))
            //{
            //    List<string> componentIds = new List<string>();
            //    ServiceComponentList anexo16 = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_FISICO_7C_ID);
            //    ServiceComponentList anexo312 = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ID);
            //    ServiceComponentList atencionIntegral = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ATENCION_INTEGRAL_ID);
            //    ServiceComponentList cuestionarioNordico = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.C_N_ID);
            //    ServiceComponentList sas = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FICHA_SAS_ID);
            //    ServiceComponentList evaluacionErgonomica = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EVA_ERGONOMICA_ID);
            //    ServiceComponentList evaluacionNeurologica = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EVA_NEUROLOGICA_ID);
            //    ServiceComponentList alturageografica = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ALTURA_7D_ID);
            //    ServiceComponentList alturaestructural = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ALTURA_ESTRUCTURAL_ID);
            //    ServiceComponentList suficienciamedica = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_SUF_MED__OPERADORES_ID);
            //    ServiceComponentList osteoMuscular = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OSTEO_MUSCULAR_ID_1);
            //    ServiceComponentList fotoTipo = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FOTO_TIPO_ID);
            //    ServiceComponentList osteoCoimo = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OSTEO_COIMO);
            //    ServiceComponentList sintomatico = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.SINTOMATICO_ID);
            //    ServiceComponentList fichaSufcMedica = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FICHA_SUFICIENCIA_MEDICA_ID);
            //    ServiceComponentList TamizajeDermat = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TAMIZAJE_DERMATOLOGIO_ID);
            //    ServiceComponentList testVertigo = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.TEST_VERTIGO_ID);
            //    ServiceComponentList evaluacionOsteomuscular = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EVA_OSTEO_ID);

            //    if (anexo16 != null)
            //    {
            //        //componentIds.Add(Constants.EXAMEN_FISICO_7C_ID);
            //        if (filiationData.EmpresaClienteId == "N009-OO000000587")
            //        {
            //            componentIds.Add(Constants.INFORME_ANEXO_16_YANACOCHA);
            //        }
            //        else if (filiationData.EmpresaClienteId == "N009-OO000000589")
            //        {
            //            componentIds.Add(Constants.INFORME_ANEXO_16_GOLD_FIELD);
            //        }
            //        else if (filiationData.EmpresaClienteId == "N009-OO000000591")
            //        {
            //            componentIds.Add(Constants.INFORME_ANEXO_16_SHAHUINDO);
            //        }
            //        else if (filiationData.EmpresaClienteId == "N009-OO000000590" || filiationData.EmpresaClienteId == "N002-OO000003575")
            //        {
            //            componentIds.Add(Constants.INFORME_ANEXO_16_COIMOLACHE);
            //        }
            //        else
            //        {
            //            componentIds.Add(Constants.INFORME_ANEXO_7C);
            //        }
            //    }
            //    if (anexo312 != null)
            //    {
            //        componentIds.Add(Constants.EXAMEN_FISICO_ID);
            //    }
            //    if (atencionIntegral != null)
            //    {
            //        componentIds.Add(Constants.ATENCION_INTEGRAL_ID);
            //    }
            //    if (cuestionarioNordico != null)
            //    {
            //        componentIds.Add(Constants.C_N_ID);
            //    }
            //    if (sas != null)
            //    {
            //        componentIds.Add(Constants.FICHA_SAS_ID);
            //    }
            //    if (evaluacionErgonomica != null)
            //    {
            //        componentIds.Add(Constants.EVA_ERGONOMICA_ID);
            //    }
            //    if (evaluacionNeurologica != null)
            //    {
            //        componentIds.Add(Constants.EVA_NEUROLOGICA_ID);
            //    }
            //    if (alturageografica != null)
            //    {
            //        componentIds.Add(Constants.ALTURA_7D_ID);
            //    }
            //    if (alturaestructural != null)
            //    {
            //        componentIds.Add(Constants.ALTURA_ESTRUCTURAL_ID);
            //    }
            //    if (suficienciamedica != null)
            //    {
            //        componentIds.Add(Constants.EXAMEN_SUF_MED__OPERADORES_ID);
            //    }
            //    if (osteoMuscular != null)
            //    {
            //        componentIds.Add(Constants.OSTEO_MUSCULAR_ID_1);
            //    }
            //    if (fotoTipo != null)
            //    {
            //        componentIds.Add(Constants.FOTO_TIPO_ID);
            //    }
            //    if (osteoCoimo != null)
            //    {
            //        componentIds.Add(Constants.OSTEO_COIMO);
            //    }
            //    if (sintomatico != null)
            //    {
            //        componentIds.Add(Constants.SINTOMATICO_ID);
            //    }
            //    if (fichaSufcMedica != null)
            //    {
            //        componentIds.Add(Constants.FICHA_SUFICIENCIA_MEDICA_ID);
            //    }
            //    if (TamizajeDermat != null)
            //    {
            //        componentIds.Add(Constants.TAMIZAJE_DERMATOLOGIO_ID);
            //    }
            //    if (testVertigo != null)
            //    {
            //        componentIds.Add(Constants.TEST_VERTIGO_ID);
            //    }
            //    if (evaluacionOsteomuscular != null)
            //    {
            //        componentIds.Add(Constants.EVA_OSTEO_ID);
            //    }
            //    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
            //}
            //#endregion
            //#region odontologia
            //else if (arrComponentId.Contains(Constants.ODONTOGRAMA_ID))
            //{
            //    List<string> componentIds = new List<string>();
            //    ServiceComponentList odontograma = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ODONTOGRAMA_ID);

            //    if (odontograma != null)
            //    {
            //        componentIds.Add(Constants.ODONTOGRAMA_ID);
            //    }
            //    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
            //}
            //#endregion
            //#region oftalmologia
            //else if (arrComponentId.Contains(Constants.OFTALMOLOGIA_ID)//oftalmologia
            //        || arrComponentId.Contains(Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID)
            //        || arrComponentId.Contains(Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID)
            //        || arrComponentId.Contains(Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID)
            //        || arrComponentId.Contains(Constants.INFORME_OFTALMOLOGICO_HUDBAY_ID)
            //        || arrComponentId.Contains(Constants.PETRINOVIC_ID)
            //        || arrComponentId.Contains(Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_ID)
            //        || arrComponentId.Contains("N009-ME000000437"))
            //{
            //    List<string> componentIds = new List<string>();
            //    ServiceComponentList agudezavisual = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OFTALMOLOGIA_ID);
            //    ServiceComponentList oftsimple = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID);
            //    ServiceComponentList oftcompleto = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID);
            //    ServiceComponentList oftYanacocha = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID);
            //    ServiceComponentList oftHudbay = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.INFORME_OFTALMOLOGICO_HUDBAY_ID);
            //    ServiceComponentList petrinovic = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PETRINOVIC_ID);
            //    ServiceComponentList certificadoPsico = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_ID);
            //    ServiceComponentList oftalmoPsico = serviceComponents.Find(p => p.v_ComponentId == "N009-ME000000437");

            //    if (agudezavisual != null)
            //    {
            //        componentIds.Add(Constants.OFTALMOLOGIA_ID);
            //    }
            //    if (oftsimple != null)
            //    {
            //        componentIds.Add(Constants.EXAMEN_OFTALMOLOGICO_SIMPLE_ID);
            //    }
            //    if (oftcompleto != null)
            //    {
            //        componentIds.Add(Constants.EXAMEN_OFTALMOLOGICO_COMPLETO_ID);
            //    }
            //    if (oftYanacocha != null)
            //    {
            //        componentIds.Add(Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID);
            //    }
            //    if (oftHudbay != null)
            //    {
            //        componentIds.Add(Constants.INFORME_OFTALMOLOGICO_HUDBAY_ID);
            //    }
            //    if (petrinovic != null)
            //    {
            //        componentIds.Add(Constants.PETRINOVIC_ID);
            //    }
            //    if (certificadoPsico != null)
            //    {
            //        componentIds.Add(Constants.CERTIFICADO_PSICOSENSOMETRICO_DATOS_ID);
            //    }
            //    if (oftalmoPsico != null)
            //    {
            //        componentIds.Add("N009-ME000000437");
            //    }

            //    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
            //}
            //#endregion
            //#region psicologia
            //else if (arrComponentId.Contains(Constants.PSICOLOGIA_ID)//psicologia
            //        || arrComponentId.Contains(Constants.HISTORIA_CLINICA_PSICOLOGICA_ID)
            //        || arrComponentId.Contains(Constants.FICHA_PSICOLOGICA_OCUPACIONAL_GOLDFIELDS)
            //        || arrComponentId.Contains(Constants.INFORME_PSICOLOGICO_OCUPACIONAL_GOLDFIELDS)
            //        || arrComponentId.Contains(Constants.SOMNOLENCIA_ID))
            //{
            //    List<string> componentIds = new List<string>();

            //    ServiceComponentList psico = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.PSICOLOGIA_ID);
            //    ServiceComponentList psicoHist = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.HISTORIA_CLINICA_PSICOLOGICA_ID);
            //    ServiceComponentList psicoGoldHis = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.FICHA_PSICOLOGICA_OCUPACIONAL_GOLDFIELDS);
            //    ServiceComponentList psicoGolFich = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.INFORME_PSICOLOGICO_OCUPACIONAL_GOLDFIELDS);
            //    ServiceComponentList somnolencia = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.SOMNOLENCIA_ID);


            //    if (psico != null)
            //    {
            //        componentIds.Add(Constants.PSICOLOGIA_ID);
            //    }
            //    if (psicoHist != null)
            //    {
            //        if (filiationData.EmpresaClienteId == "N009-OO000000587")
            //        {
            //            componentIds.Add(Constants.HISTORIA_CLINICA_PSICOLOGICA_ID + "|42");
            //        }
            //        else
            //        {
            //            componentIds.Add(Constants.HISTORIA_CLINICA_PSICOLOGICA_ID);
            //        }
            //    }
            //    if (psicoGoldHis != null)
            //    {
            //        componentIds.Add(Constants.FICHA_PSICOLOGICA_OCUPACIONAL_GOLDFIELDS);
            //    }
            //    if (psicoGolFich != null)
            //    {
            //        componentIds.Add(Constants.INFORME_PSICOLOGICO_OCUPACIONAL_GOLDFIELDS);
            //    }
            //    if (somnolencia != null)
            //    {
            //        componentIds.Add(Constants.SOMNOLENCIA_ID);
            //    }

            //    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
            //}
            //#endregion
            //#region rayosx
            //else if (arrComponentId.Contains(Constants.OIT_ID)//rayos x
            //        || arrComponentId.Contains(Constants.RX_TORAX_ID)
            //        || arrComponentId.Contains(Constants.EXCEPCIONES_RX_ID)
            //        || arrComponentId.Contains(Constants.EXCEPCIONES_RX_AUTORIZACION_ID))
            //{
            //    List<string> componentIds = new List<string>();
            //    ServiceComponentList oit = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.OIT_ID);
            //    ServiceComponentList torax = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.RX_TORAX_ID);
            //    ServiceComponentList excepciones = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXCEPCIONES_RX_ID);
            //    ServiceComponentList autorizacion = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.EXCEPCIONES_RX_AUTORIZACION_ID);

            //    if (oit != null)
            //    {
            //        if (filiationData.EmpresaClienteId == "N009-OO000000587")
            //        {
            //            componentIds.Add(Constants.OIT_ID + "|45");
            //        }
            //        else
            //        {
            //            componentIds.Add(Constants.OIT_ID);
            //        }
            //    }
            //    if (torax != null)
            //    {
            //        componentIds.Add(Constants.RX_TORAX_ID);
            //    }
            //    //if (excepciones != null)
            //    //{
            //    //    componentIds.Add(Constants.EXCEPCIONES_RX_ID);
            //    //}
            //    //if (autorizacion != null)
            //    //{
            //    //    componentIds.Add(Constants.EXCEPCIONES_RX_AUTORIZACION_ID);
            //    //}

            //    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
            //}
            //#endregion
            //#region lumbar
            //else if (arrComponentId.Contains(Constants.LUMBOSACRA_ID)
            //        || arrComponentId.Contains(Constants.ELECTROENCEFALOGRAMA_ID)
            //        || arrComponentId.Contains("N009-ME000000302"))
            //{
            //    List<string> componentIds = new List<string>();
            //    ServiceComponentList lumbosacra = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.LUMBOSACRA_ID);
            //    ServiceComponentList electroencefalograma = serviceComponents.Find(p => p.v_ComponentId == Sigesoft.Common.Constants.ELECTROENCEFALOGRAMA_ID);
            //    ServiceComponentList columnaCervicoDorso = serviceComponents.Find(p => p.v_ComponentId == "N009-ME000000302");

            //    if (lumbosacra != null)
            //    {
            //        componentIds.Add(Constants.LUMBOSACRA_ID);
            //    }
            //    if (electroencefalograma != null)
            //    {
            //        componentIds.Add(Constants.ELECTROENCEFALOGRAMA_ID);
            //    }

            //    if (columnaCervicoDorso != null)
            //    {
            //        componentIds.Add("N009-ME000000302");
            //    }

            //    frmManagmentReport.reportSolo(componentIds, PacientId, _serviceId);
            //}
            //#endregion
            #endregion
            
        }

        private void btn7C_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                // Elegir la ruta para guardar el PDF combinado
                //saveFileDialog1.FileName = string.Format("{0} Ficha Médica 7 C", _personName);

                //saveFileDialog1.Filter = "Files (*.pdf;)|*.pdf;";

                //GenerateAnexo7C(saveFileDialog1.FileName);

                //RunFile(saveFileDialog1.FileName);
                return;
            }
        }

        private void btn312_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                // Elegir la ruta para guardar el PDF combinado
                //saveFileDialog1.FileName = string.Format("{0} Ficha Médica", _personName);

                //saveFileDialog1.Filter = "Files (*.pdf;)|*.pdf;";

                //GenerateAnexo312(saveFileDialog1.FileName);

                //RunFile(saveFileDialog1.FileName);
                return;
            }
        }

        private void btnInterConsulta_Click(object sender, EventArgs e)
        {
            frmInterconsulta frm = new frmInterconsulta(_serviceId);
            frm.ShowDialog();
        }

        private void btnSubirInterconsulta_Click(object sender, EventArgs e)
        {
            frmSubirInterconsulta frm = new frmSubirInterconsulta(_serviceId, _personName);
            frm.ShowDialog();
        }

        private void btnCertificadoAptitud_Click(object sender, EventArgs e)
        {
           
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevoCronico_Click(object sender, EventArgs e)
        {
            Popups.frmPopupAtencionIntegral frm = new Popups.frmPopupAtencionIntegral("Cronico", "New", _personId, "");
            frm.ShowDialog();

            CargarGrillaProblemas(_personId);
        }
        private void CargarGrillaProblemas(string personId)
        {
            OperationResult objOperationResult = new OperationResult();
            var objData = new ProblemaBL().GetProblemaPagedAndFiltered(ref objOperationResult, 0, null, "", "", personId);
            grdCronicos.DataSource = objData.FindAll(p => p.i_Tipo == (int)TipoProblema.Cronico);
            grdAgudos.DataSource = objData.FindAll(p => p.i_Tipo == (int)TipoProblema.Agudo);
        }

        private void btnEditarCronico_Click(object sender, EventArgs e)
        {
            string id = grdCronicos.Selected.Rows[0].Cells["v_ProblemaId"].Value.ToString();

            Popups.frmPopupAtencionIntegral frm = new Popups.frmPopupAtencionIntegral("Cronico", "Edit", _personId, id);
            frm.ShowDialog();
            CargarGrillaProblemas(_personId);
        }

        private void btnEliminarCronico_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "¡ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                string id = grdCronicos.Selected.Rows[0].Cells["v_ProblemaId"].Value.ToString();
                new ProblemaBL().DeleteProblema(ref objOperationResult, id, Globals.ClientSession.GetAsList());
                CargarGrillaProblemas(_personId);
            }
        }

        private void btnNuevoAgudo_Click(object sender, EventArgs e)
        {
            Popups.frmPopupAtencionIntegral frm = new Popups.frmPopupAtencionIntegral("Agudo", "New", _personId, "");
            frm.ShowDialog();
            CargarGrillaProblemas(_personId);
        }

        private void btnEditarAgudo_Click(object sender, EventArgs e)
        {
            string id = grdAgudos.Selected.Rows[0].Cells["v_ProblemaId"].Value.ToString();
            Popups.frmPopupAtencionIntegral frm = new Popups.frmPopupAtencionIntegral("Agudo", "Edit", _personId, id);
            frm.ShowDialog();
            CargarGrillaProblemas(_personId);
        }

        private void btnEliminarAgudo_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "¡ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                string id = grdAgudos.Selected.Rows[0].Cells["v_ProblemaId"].Value.ToString();
                new ProblemaBL().DeleteProblema(ref objOperationResult, id, Globals.ClientSession.GetAsList());
                CargarGrillaProblemas(_personId);
            }
        }

        private void btnNuevoPlan_Click(object sender, EventArgs e)
        {
            Popups.frmPopupAtencionIntegral frm = new Popups.frmPopupAtencionIntegral("Plan", "New", _personId, "");
            frm.ShowDialog();
            CargarGrillaPlanAtencionIntegral(_personId);
        }
        private void CargarGrillaPlanAtencionIntegral(string personId)
        {
            OperationResult objOperationResult = new OperationResult();
            var objData = new PlanIntegralBL().GetPlanIntegralAndFiltered(ref objOperationResult, 0, null, "", "", personId);
            grdPlanIntegral.DataSource = objData;
        }

        private void btnEditarPlan_Click(object sender, EventArgs e)
        {
            string id = grdPlanIntegral.Selected.Rows[0].Cells["v_PlanIntegral"].Value.ToString();
            Popups.frmPopupAtencionIntegral frm = new Popups.frmPopupAtencionIntegral("Plan", "Edit", _personId, id);
            frm.ShowDialog();
            CargarGrillaPlanAtencionIntegral(_personId);
        }

        private void btnEliminarPlan_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "¡ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                string id = grdPlanIntegral.Selected.Rows[0].Cells["v_PlanIntegral"].Value.ToString();
                new PlanIntegralBL().DeletePlanIntegral(ref objOperationResult, id, Globals.ClientSession.GetAsList());
                CargarGrillaPlanAtencionIntegral(_personId);
            }
        }

        private void btnNuevoEmbarazo_Click(object sender, EventArgs e)
        {
            idPerson = new AtencionesIntegralesBL().GetService(_serviceId);
            PacientId = idPerson.v_PersonId.ToString();
            frmEmbarazo frm = new frmEmbarazo("New", PacientId, "");

            frm.ShowDialog();
            CargarGrillaEmbarazos(idPerson.v_PersonId);
        }

        private void btnEditarEmbarazo_Click(object sender, EventArgs e)
        {
            idPerson = new AtencionesIntegralesBL().GetService(_serviceId);
            PacientId = idPerson.v_PersonId.ToString();
            string id = grdEmbarazos.Selected.Rows[0].Cells["v_EmbarazoId"].Value.ToString();

            frmEmbarazo frm = new frmEmbarazo("Edit", PacientId, id);
            frm.ShowDialog();
            CargarGrillaEmbarazos(idPerson.v_PersonId);
        }

        private void btnEliminarEmbarazo_Click(object sender, EventArgs e)
        {
            idPerson = new AtencionesIntegralesBL().GetService(_serviceId);
            PacientId = idPerson.v_PersonId.ToString();
            OperationResult objOperationResult = new OperationResult();
            string id = grdEmbarazos.Selected.Rows[0].Cells["v_EmbarazoId"].Value.ToString();
            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "¡ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                new EmbarazoBL().DeleteEmbarazo(ref objOperationResult, id, Globals.ClientSession.GetAsList());
                CargarGrillaEmbarazos(idPerson.v_PersonId);
            }
            else
            {
                this.Close();
            }
        }

        private void btnGuardarAntecedentes_Click(object sender, EventArgs e)
        {
            List<frmEsoAntecedentesPadre> Listado = new List<frmEsoAntecedentesPadre>();

            foreach (var G in ultraAntActuales.Rows)
            {
                List<frmEsoAntecedentesHijo> ListadoHijos = new List<frmEsoAntecedentesHijo>();
                foreach (var H in G.ChildBands[0].Rows)
                {
                    frmEsoAntecedentesHijo Hijo = new frmEsoAntecedentesHijo()
                    {
                        Nombre = H.Cells["Nombre"].Text,
                        SI = bool.Parse(H.Cells[1].Text),
                        NO = bool.Parse(H.Cells[2].Text),
                        GrupoId = int.Parse(H.Cells["GrupoId"].Text),
                        ParametroId = int.Parse(H.Cells["ParametroId"].Text)
                    };

                    ListadoHijos.Add(Hijo);
                }

                frmEsoAntecedentesPadre Padre = new frmEsoAntecedentesPadre()
                {
                    GrupoId = int.Parse(G.Cells["GrupoId"].Text),
                    Nombre = G.Cells["Nombre"].Text,
                    ParametroId = int.Parse(G.Cells["ParametroId"].Text),
                    Hijos = ListadoHijos
                };

                Listado.Add(Padre);
            }

            bool response = false;


            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                OperationResult objOperationResult = new OperationResult();
                personDto oDatosPersonales = new personDto();
                idPerson = new AtencionesIntegralesBL().GetService(_serviceId);
                PacientId = idPerson.v_PersonId.ToString();
                oDatosPersonales = new PacientBL().GetPerson(ref objOperationResult, PacientId);



                oDatosPersonales.v_PersonId = PacientId;
                oDatosPersonales.v_FirstName = txtNombres.Text.Trim();
                oDatosPersonales.v_FirstLastName = txtApellidoPaterno.Text.Trim();
                oDatosPersonales.v_SecondLastName = txtApellidoMaterno.Text.Trim();

                oDatosPersonales.i_SexTypeId = int.Parse(cboGenero.SelectedValue.ToString());
                oDatosPersonales.v_BirthPlace = txtLugarNacimiento.Text.Trim();
                oDatosPersonales.v_Procedencia = txtProcedencia.Text.Trim();
                oDatosPersonales.i_LevelOfId = int.Parse(cboGradoInstruccion.SelectedValue.ToString());
                oDatosPersonales.i_MaritalStatusId = int.Parse(cboEstadoCivil.SelectedValue.ToString());
                oDatosPersonales.v_CurrentOccupation = txtOcupacion.Text.Trim();
                oDatosPersonales.v_AdressLocation = txtDomicilio.Text.Trim();
                oDatosPersonales.v_CentroEducativo = txtCentroEducativo.Text.Trim();
                oDatosPersonales.i_NumberLivingChildren = int.Parse(txtHijosVivos.Text);
                _Dni = lblDni.Text;

                PacientId = new PacientBL().UpdatePacient(ref objOperationResult, oDatosPersonales, Globals.ClientSession.GetAsList(), _Dni, lblDni.Text);

                ServiceList personData = new ServiceBL().GetServicePersonData(ref objOperationResult, _serviceId);

                _sexType = (Gender)personData.i_SexTypeId;
                _sexTypeId = personData.i_SexTypeId;

                if (_age <= 12)
                {
                    if (objNinioDto == null)
                    {
                        objNinioDto = new ninioDto();
                    }

                    if (objNinioDto.v_NinioId == null)
                    {
                        objNinioDto.v_PersonId = idPerson.v_PersonId.ToString();

                        objNinioDto.v_NombreCuidador = txtNombreMadrePadreResponsable.Text;
                        objNinioDto.v_EdadCuidador = txtEdadMadrePadreResponsable.Text;
                        objNinioDto.v_DniCuidador = txtDNIMadrePadreResponsable.Text;

                        objNinioDto.v_NombrePadre = txtNombrePadreTutor.Text;
                        objNinioDto.v_EdadPadre = txtEdadPadre.Text;
                        objNinioDto.v_DniPadre = txtDNIPadre.Text;
                        objNinioDto.i_TipoAfiliacionPadre = Convert.ToInt32(listTipoAfiliacionHombre.SelectedValue);
                        objNinioDto.v_CodigoAfiliacionPadre = txtAfiliacionPadre.Text;
                        objNinioDto.i_GradoInstruccionPadre = Convert.ToInt32(listaGradoInstruccionHombre.SelectedValue);
                        objNinioDto.v_OcupacionPadre = txtOcupacionPadre.Text;
                        objNinioDto.i_EstadoCivilIdPadre = Convert.ToInt32(listaEstadoCivilHombre.SelectedValue);
                        objNinioDto.v_ReligionPadre = txtReligionPadre.Text;

                        objNinioDto.v_NombreMadre = txtNombreMadreTutor.Text;
                        objNinioDto.v_EdadMadre = txtEdadMadre.Text;
                        objNinioDto.v_DniMadre = txtDNIMadre.Text;
                        objNinioDto.i_TipoAfiliacionMadre = Convert.ToInt32(listTipoAfiliacionMujer.SelectedValue);
                        objNinioDto.v_CodigoAfiliacionMadre = txtAfiliacionMadre.Text;
                        objNinioDto.i_GradoInstruccionMadre = Convert.ToInt32(listaGradoInstruccionMujer.SelectedValue);
                        objNinioDto.v_OcupacionMadre = txtOcupacionMadre.Text;
                        objNinioDto.i_EstadoCivilIdMadre1 = Convert.ToInt32(listaEstadoCivilMujer.SelectedValue);
                        objNinioDto.v_ReligionMadre = txtReligionMadre.Text;

                        objNinioDto.v_PatologiasGestacion = textPatologiasGestacion.Text;
                        objNinioDto.v_nEmbarazos = txtNEmbarazo.Text;
                        objNinioDto.v_nAPN = txtAPN.Text;
                        objNinioDto.v_LugarAPN = textLugarAPN.Text;
                        objNinioDto.v_ComplicacionesParto = textComplicacionesParto.Text;
                        objNinioDto.v_Atencion = textQuienAtendio.Text;

                        objNinioDto.v_EdadGestacion = textEdadNacer.Text;
                        objNinioDto.v_Peso = textPesoNacer.Text;
                        objNinioDto.v_Talla = textTallaNacer.Text;
                        objNinioDto.v_PerimetroCefalico = textPerimetroCefalico.Text;
                        objNinioDto.v_TiempoHospitalizacion = textTiempoHospit.Text;
                        objNinioDto.v_PerimetroToracico = textPerimetroToracico.Text;
                        objNinioDto.v_EspecificacionesNac = textEspecificacionesNacer.Text;
                        objNinioDto.v_InicioAlimentacionComp = txtInicioAlimentación.Text;
                        objNinioDto.v_AlergiasMedicamentos = textAlergiaMedicamentos.Text;
                        objNinioDto.v_OtrosAntecedentes = textOtrosAntecedentes.Text;

                        objNinioDto.v_QuienTuberculosis = textQuienTuberculosis.Text;
                        objNinioDto.i_QuienTuberculosis = rbSiTuberculosis.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienAsma = textQuienASMA.Text;
                        objNinioDto.i_QuienAsma = rbSiAsma.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienVIH = textQuienVIH.Text;
                        objNinioDto.i_QuienVIH = rbSiVih.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienDiabetes = textQuienDiabetes.Text;
                        objNinioDto.i_QuienDiabetes = rbSiDiabetes.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienEpilepsia = textQuienEpilepsia.Text;
                        objNinioDto.i_QuienEpilepsia = rbSiEpilepsia.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienAlergias = textQuienAlergias.Text;
                        objNinioDto.i_QuienAlergias = rbSiAlergia.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienViolenciaFamiliar = textQuienViolenciaFamiliar.Text;
                        objNinioDto.i_QuienViolenciaFamiliar = rbSiViolencia.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienAlcoholismo = textQuienAlcoholismo.Text;
                        objNinioDto.i_QuienAlcoholismo = rbSiAlcoholismo.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienDrogadiccion = textQuienDrogadiccion.Text;
                        objNinioDto.i_QuienDrogadiccion = rbSiDrogadiccion.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienHeptitisB = textQuienHepatitisB.Text;
                        objNinioDto.i_QuienHeptitisB = rbSiHepatitis.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_EspecificacionesAgua = textAguaPotable.Text;
                        objNinioDto.v_EspecificacionesDesague = textDesague.Text;

                        ninioId = new AtencionesIntegralesBL().AddNinio(ref objOperationResult, objNinioDto, Globals.ClientSession.GetAsList());
                    }
                    else
                    {
                        objNinioDto.v_NombreCuidador = txtNombreMadrePadreResponsable.Text;
                        objNinioDto.v_EdadCuidador = txtEdadMadrePadreResponsable.Text;
                        objNinioDto.v_DniCuidador = txtDNIMadrePadreResponsable.Text;

                        objNinioDto.v_NombrePadre = txtNombrePadreTutor.Text;
                        objNinioDto.v_EdadPadre = txtEdadPadre.Text;
                        objNinioDto.v_DniPadre = txtDNIPadre.Text;
                        objNinioDto.i_TipoAfiliacionPadre = Convert.ToInt32(listTipoAfiliacionHombre.SelectedValue);
                        objNinioDto.v_CodigoAfiliacionPadre = txtAfiliacionPadre.Text;
                        objNinioDto.i_GradoInstruccionPadre = Convert.ToInt32(listaGradoInstruccionHombre.SelectedValue);
                        objNinioDto.v_OcupacionPadre = txtOcupacionPadre.Text;
                        objNinioDto.i_EstadoCivilIdPadre = Convert.ToInt32(listaEstadoCivilHombre.SelectedValue);
                        objNinioDto.v_ReligionPadre = txtReligionPadre.Text;

                        objNinioDto.v_NombreMadre = txtNombreMadreTutor.Text;
                        objNinioDto.v_EdadMadre = txtEdadMadre.Text;
                        objNinioDto.v_DniMadre = txtDNIMadre.Text;
                        objNinioDto.i_TipoAfiliacionMadre = Convert.ToInt32(listTipoAfiliacionMujer.SelectedValue);
                        objNinioDto.v_CodigoAfiliacionMadre = txtAfiliacionMadre.Text;
                        objNinioDto.i_GradoInstruccionMadre = Convert.ToInt32(listaGradoInstruccionMujer.SelectedValue);
                        objNinioDto.v_OcupacionMadre = txtOcupacionMadre.Text;
                        objNinioDto.i_EstadoCivilIdMadre1 = Convert.ToInt32(listaEstadoCivilMujer.SelectedValue);
                        objNinioDto.v_ReligionMadre = txtReligionMadre.Text;

                        objNinioDto.v_PatologiasGestacion = textPatologiasGestacion.Text;
                        objNinioDto.v_nEmbarazos = txtNEmbarazo.Text;
                        objNinioDto.v_nAPN = txtAPN.Text;
                        objNinioDto.v_LugarAPN = textLugarAPN.Text;
                        objNinioDto.v_ComplicacionesParto = textComplicacionesParto.Text;
                        objNinioDto.v_Atencion = textQuienAtendio.Text;

                        objNinioDto.v_EdadGestacion = textEdadNacer.Text;
                        objNinioDto.v_Peso = textPesoNacer.Text;
                        objNinioDto.v_Talla = textTallaNacer.Text;
                        objNinioDto.v_PerimetroCefalico = textPerimetroCefalico.Text;
                        objNinioDto.v_TiempoHospitalizacion = textTiempoHospit.Text;
                        objNinioDto.v_PerimetroToracico = textPerimetroToracico.Text;
                        objNinioDto.v_EspecificacionesNac = textEspecificacionesNacer.Text;
                        objNinioDto.v_InicioAlimentacionComp = txtInicioAlimentación.Text;
                        objNinioDto.v_AlergiasMedicamentos = textAlergiaMedicamentos.Text;
                        objNinioDto.v_OtrosAntecedentes = textOtrosAntecedentes.Text;

                        objNinioDto.v_QuienTuberculosis = textQuienTuberculosis.Text;
                        objNinioDto.i_QuienTuberculosis = rbSiTuberculosis.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienAsma = textQuienASMA.Text;
                        objNinioDto.i_QuienAsma = rbSiAsma.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienVIH = textQuienVIH.Text;
                        objNinioDto.i_QuienVIH = rbSiVih.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienDiabetes = textQuienDiabetes.Text;
                        objNinioDto.i_QuienDiabetes = rbSiDiabetes.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienEpilepsia = textQuienEpilepsia.Text;
                        objNinioDto.i_QuienEpilepsia = rbSiEpilepsia.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienAlergias = textQuienAlergias.Text;
                        objNinioDto.i_QuienAlergias = rbSiAlergia.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienViolenciaFamiliar = textQuienViolenciaFamiliar.Text;
                        objNinioDto.i_QuienViolenciaFamiliar = rbSiViolencia.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienAlcoholismo = textQuienAlcoholismo.Text;
                        objNinioDto.i_QuienAlcoholismo = rbSiAlcoholismo.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienDrogadiccion = textQuienDrogadiccion.Text;
                        objNinioDto.i_QuienDrogadiccion = rbSiDrogadiccion.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_QuienHeptitisB = textQuienHepatitisB.Text;
                        objNinioDto.i_QuienHeptitisB = rbSiHepatitis.Checked ? (int)Enfermedad.Si : (int)Enfermedad.No;

                        objNinioDto.v_EspecificacionesAgua = textAguaPotable.Text;
                        objNinioDto.v_EspecificacionesDesague = textDesague.Text;

                        new AtencionesIntegralesBL().UpdNinio(ref objOperationResult, objNinioDto, Globals.ClientSession.GetAsList());
                    }
                    Refresh();
                }
                else if (13 <= _age && _age <= 17)
                {
                    if (objAdolDto == null)
                    {
                        objAdolDto = new adolescenteDto();
                    }

                    if (objAdolDto.v_AdolescenteId == null)
                    {
                        objAdolDto.v_PersonId = idPerson.v_PersonId.ToString();
                        objAdolDto.v_NombreCuidador = textNombreCuidadorAdol.Text;
                        objAdolDto.v_DniCuidador = textDniCuidadorAdol.Text;
                        objAdolDto.v_EdadCuidador = textEdadAdol.Text;
                        objAdolDto.v_ViveCon = textViveConAdol.Text;
                        objAdolDto.v_EdadInicioTrabajo = txtAdoEdadIniTrab.Text;
                        objAdolDto.v_TipoTrabajo = txtAdoTipoTrab.Text;
                        objAdolDto.v_NroHorasTv = txtAdoNroTv.Text;
                        objAdolDto.v_NroHorasJuegos = txtAdoNroJuegos.Text;
                        objAdolDto.v_MenarquiaEspermarquia = txtAdoMenarquia.Text;
                        objAdolDto.v_EdadInicioRS = txtAdoEdadRS.Text;
                        objAdolDto.v_Observaciones = textObservacionesSexualidadAdol.Text;

                        adolId = new AtencionesIntegralesBL().AddAdolescente(ref objOperationResult, objAdolDto, Globals.ClientSession.GetAsList());
                    }
                    else
                    {
                        objAdolDto.v_NombreCuidador = textNombreCuidadorAdol.Text;
                        objAdolDto.v_DniCuidador = textDniCuidadorAdol.Text;
                        objAdolDto.v_EdadCuidador = textEdadAdol.Text;
                        objAdolDto.v_ViveCon = textViveConAdol.Text;
                        objAdolDto.v_EdadInicioTrabajo = txtAdoEdadIniTrab.Text;
                        objAdolDto.v_TipoTrabajo = txtAdoTipoTrab.Text;
                        objAdolDto.v_NroHorasTv = txtAdoNroTv.Text;
                        objAdolDto.v_NroHorasJuegos = txtAdoNroJuegos.Text;
                        objAdolDto.v_MenarquiaEspermarquia = txtAdoMenarquia.Text;
                        objAdolDto.v_EdadInicioRS = txtAdoEdadRS.Text;
                        objAdolDto.v_Observaciones = textObservacionesSexualidadAdol.Text;

                        new AtencionesIntegralesBL().UpdAdolescente(ref objOperationResult, objAdolDto, Globals.ClientSession.GetAsList());
                    }
                }
                else if (18 <= _age && _age <= 64)
                {

                    if (personData.i_SexTypeId == (int)Gender.MASCULINO)
                    {
                        if (objAdultoDto == null)
                        {
                            objAdultoDto = new adultoDto();
                        }

                        if (objAdultoDto.v_AdultoId == null)
                        {
                            objAdultoDto.v_PersonId = idPerson.v_PersonId.ToString();
                            objAdultoDto.v_NombreCuidador = txtAmCuidador.Text;
                            objAdultoDto.v_EdadCuidador = txtAmCuidadorEdad.Text;
                            objAdultoDto.v_DniCuidador = txtAmCuidadorDni.Text;
                            objAdultoDto.v_OtrosAntecedentes = textOtroAntecedentesFamiliares.Text;
                            objAdultoDto.v_FlujoVaginal = textFlujoVaginalPatológico.Text;
                            objAdultoDto.v_MedicamentoFrecuente = textMedicamentoFrecuenteDosis.Text;
                            objAdultoDto.v_ReaccionAlergica = textReaccionAlergicaMedicamentos.Text;
                            objAdultoDto.v_InicioRS = txtAmInicioRs.Text;
                            objAdultoDto.v_NroPs = txtAmNroPs.Text;

                            adulId = new AtencionesIntegralesBL().AddAdulto(ref objOperationResult, objAdultoDto, Globals.ClientSession.GetAsList());
                        }
                        else
                        {
                            objAdultoDto.v_NombreCuidador = txtAmCuidador.Text;
                            objAdultoDto.v_EdadCuidador = txtAmCuidadorEdad.Text;
                            objAdultoDto.v_DniCuidador = txtAmCuidadorDni.Text;
                            objAdultoDto.v_OtrosAntecedentes = textOtroAntecedentesFamiliares.Text;
                            objAdultoDto.v_FlujoVaginal = textFlujoVaginalPatológico.Text;
                            objAdultoDto.v_MedicamentoFrecuente = textMedicamentoFrecuenteDosis.Text;
                            objAdultoDto.v_ReaccionAlergica = textReaccionAlergicaMedicamentos.Text;
                            objAdultoDto.v_InicioRS = txtAmInicioRs.Text;
                            objAdultoDto.v_NroPs = txtAmNroPs.Text;
                            objAdultoDto.v_FechaUR = txtAmFechaUR.Text;

                            new AtencionesIntegralesBL().UpdAdulto(ref objOperationResult, objAdultoDto, Globals.ClientSession.GetAsList());
                        }
                    }
                    else
                    {
                        if (objAdultoDto == null)
                        {
                            objAdultoDto = new adultoDto();
                        }

                        if (objAdultoDto.v_AdultoId == null)
                        {
                            objAdultoDto.v_PersonId = idPerson.v_PersonId.ToString();
                            objAdultoDto.v_NombreCuidador = txtAmCuidador.Text;
                            objAdultoDto.v_EdadCuidador = txtAmCuidadorEdad.Text;
                            objAdultoDto.v_DniCuidador = txtAmCuidadorDni.Text;
                            objAdultoDto.v_OtrosAntecedentes = textOtroAntecedentesFamiliares.Text;
                            objAdultoDto.v_FlujoVaginal = textFlujoVaginalPatológico.Text;
                            objAdultoDto.v_MedicamentoFrecuente = textMedicamentoFrecuenteDosis.Text;
                            objAdultoDto.v_ReaccionAlergica = textReaccionAlergicaMedicamentos.Text;
                            objAdultoDto.v_InicioRS = txtAmInicioRs.Text;
                            objAdultoDto.v_NroPs = txtAmNroPs.Text;
                            objAdultoDto.v_FechaUR = txtAmFechaUR.Text;
                            objAdultoDto.v_RC = txtAmRC.Text;
                            objAdultoDto.v_Parto = txtAmNroParto.Text;
                            objAdultoDto.v_Prematuro = txtAmPrematuro.Text;
                            objAdultoDto.v_Aborto = txtAmAborto.Text;
                            objAdultoDto.v_ObservacionesEmbarazo = textObservacionesEmbarazo.Text;

                            adulId = new AtencionesIntegralesBL().AddAdulto(ref objOperationResult, objAdultoDto, Globals.ClientSession.GetAsList());
                        }
                        else
                        {
                            objAdultoDto.v_NombreCuidador = txtAmCuidador.Text;
                            objAdultoDto.v_EdadCuidador = txtAmCuidadorEdad.Text;
                            objAdultoDto.v_DniCuidador = txtAmCuidadorDni.Text;
                            objAdultoDto.v_OtrosAntecedentes = textOtroAntecedentesFamiliares.Text;
                            objAdultoDto.v_FlujoVaginal = textFlujoVaginalPatológico.Text;
                            objAdultoDto.v_MedicamentoFrecuente = textMedicamentoFrecuenteDosis.Text;
                            objAdultoDto.v_ReaccionAlergica = textReaccionAlergicaMedicamentos.Text;
                            objAdultoDto.v_InicioRS = txtAmInicioRs.Text;
                            objAdultoDto.v_NroPs = txtAmNroPs.Text;
                            objAdultoDto.v_FechaUR = txtAmFechaUR.Text;
                            objAdultoDto.v_RC = txtAmRC.Text;
                            objAdultoDto.v_Parto = txtAmNroParto.Text;
                            objAdultoDto.v_Prematuro = txtAmPrematuro.Text;
                            objAdultoDto.v_Aborto = txtAmAborto.Text;
                            objAdultoDto.v_ObservacionesEmbarazo = textObservacionesEmbarazo.Text;

                            new AtencionesIntegralesBL().UpdAdulto(ref objOperationResult, objAdultoDto, Globals.ClientSession.GetAsList());
                        }

                    }
                }
                else
                {
                    if (personData.i_SexTypeId == (int)Gender.MASCULINO)
                    {
                        if (objAdultoMAyorDto == null)
                        {
                            objAdultoMAyorDto = new adultomayorDto();
                        }

                        if (objAdultoMAyorDto.v_AdultoMayorId == null)
                        {
                            objAdultoMAyorDto.v_PersonId = idPerson.v_PersonId.ToString();
                            objAdultoMAyorDto.v_NombreCuidador = txtAmCuidador.Text;
                            objAdultoMAyorDto.v_EdadCuidador = txtAmCuidadorEdad.Text;
                            objAdultoMAyorDto.v_DniCuidador = txtAmCuidadorDni.Text;
                            objAdultoMAyorDto.v_FlujoVaginal = textFlujoVaginalPatológico.Text;
                            objAdultoMAyorDto.v_MedicamentoFrecuente = textMedicamentoFrecuenteDosis.Text;
                            objAdultoMAyorDto.v_ReaccionAlergica = textReaccionAlergicaMedicamentos.Text;
                            objAdultoMAyorDto.v_InicioRS = txtAmInicioRs.Text;
                            objAdultoMAyorDto.v_NroPs = txtAmNroPs.Text;

                            adulmayId = new AtencionesIntegralesBL().AddAdultoMayor(ref objOperationResult, objAdultoMAyorDto, Globals.ClientSession.GetAsList());
                        }
                        else
                        {
                            objAdultoMAyorDto.v_NombreCuidador = txtAmCuidador.Text;
                            objAdultoMAyorDto.v_EdadCuidador = txtAmCuidadorEdad.Text;
                            objAdultoMAyorDto.v_DniCuidador = txtAmCuidadorDni.Text;
                            objAdultoMAyorDto.v_FlujoVaginal = textFlujoVaginalPatológico.Text;
                            objAdultoMAyorDto.v_MedicamentoFrecuente = textMedicamentoFrecuenteDosis.Text;
                            objAdultoMAyorDto.v_ReaccionAlergica = textReaccionAlergicaMedicamentos.Text;
                            objAdultoMAyorDto.v_InicioRS = txtAmInicioRs.Text;
                            objAdultoMAyorDto.v_NroPs = txtAmNroPs.Text;
                            objAdultoMAyorDto.v_FechaUR = txtAmFechaUR.Text;

                            new AtencionesIntegralesBL().UpdAdultoMayor(ref objOperationResult, objAdultoMAyorDto, Globals.ClientSession.GetAsList());
                        }
                    }
                    else
                    {
                        if (objAdultoMAyorDto == null)
                        {
                            objAdultoMAyorDto = new adultomayorDto();
                        }

                        if (objAdultoMAyorDto.v_AdultoMayorId == null)
                        {
                            objAdultoMAyorDto.v_PersonId = idPerson.v_PersonId.ToString();
                            objAdultoMAyorDto.v_NombreCuidador = txtAmCuidador.Text;
                            objAdultoMAyorDto.v_EdadCuidador = txtAmCuidadorEdad.Text;
                            objAdultoMAyorDto.v_DniCuidador = txtAmCuidadorDni.Text;
                            objAdultoMAyorDto.v_FlujoVaginal = textFlujoVaginalPatológico.Text;
                            objAdultoMAyorDto.v_MedicamentoFrecuente = textMedicamentoFrecuenteDosis.Text;
                            objAdultoMAyorDto.v_ReaccionAlergica = textReaccionAlergicaMedicamentos.Text;
                            objAdultoMAyorDto.v_InicioRS = txtAmInicioRs.Text;
                            objAdultoMAyorDto.v_NroPs = txtAmNroPs.Text;
                            objAdultoMAyorDto.v_FechaUR = txtAmFechaUR.Text;
                            objAdultoMAyorDto.v_RC = txtAmRC.Text;
                            objAdultoMAyorDto.v_Parto = txtAmNroParto.Text;
                            objAdultoMAyorDto.v_Prematuro = txtAmPrematuro.Text;
                            objAdultoMAyorDto.v_Aborto = txtAmAborto.Text;
                            objAdultoMAyorDto.v_ObservacionesEmbarazo = textObservacionesEmbarazo.Text;

                            adulmayId = new AtencionesIntegralesBL().AddAdultoMayor(ref objOperationResult, objAdultoMAyorDto, Globals.ClientSession.GetAsList());
                        }
                        else
                        {
                            objAdultoMAyorDto.v_NombreCuidador = txtAmCuidador.Text;
                            objAdultoMAyorDto.v_EdadCuidador = txtAmCuidadorEdad.Text;
                            objAdultoMAyorDto.v_DniCuidador = txtAmCuidadorDni.Text;
                            objAdultoMAyorDto.v_FlujoVaginal = textFlujoVaginalPatológico.Text;
                            objAdultoMAyorDto.v_MedicamentoFrecuente = textMedicamentoFrecuenteDosis.Text;
                            objAdultoMAyorDto.v_ReaccionAlergica = textReaccionAlergicaMedicamentos.Text;
                            objAdultoMAyorDto.v_InicioRS = txtAmInicioRs.Text;
                            objAdultoMAyorDto.v_NroPs = txtAmNroPs.Text;
                            objAdultoMAyorDto.v_FechaUR = txtAmFechaUR.Text;
                            objAdultoMAyorDto.v_RC = txtAmRC.Text;
                            objAdultoMAyorDto.v_Parto = txtAmNroParto.Text;
                            objAdultoMAyorDto.v_Prematuro = txtAmPrematuro.Text;
                            objAdultoMAyorDto.v_Aborto = txtAmAborto.Text;
                            objAdultoMAyorDto.v_ObservacionesEmbarazo = textObservacionesEmbarazo.Text;

                            new AtencionesIntegralesBL().UpdAdultoMayor(ref objOperationResult, objAdultoMAyorDto, Globals.ClientSession.GetAsList());
                        }

                    }
                }


                response = new ServiceBL().GuardarAntecedenteAsistencial(Listado, Globals.ClientSession.i_SystemUserId, _personId, GrupoEtario, Globals.ClientSession.i_CurrentExecutionNodeId);
            }

            if (response)
            {
                MessageBox.Show("Registro guardado satisfactoriamente!.", "CORRECTO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Sucedió un error al intentar guardar la información .... intente más tarde.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarCuidadosPreventivos_Click(object sender, EventArgs e)
        {
            frmEsoCuidadosPreventivosFechas data = new frmEsoCuidadosPreventivosFechas();
            data.FechaServicio = _FechaServico.Value;

            List<frmEsoCuidadosPreventivos> Hijos = new List<frmEsoCuidadosPreventivos>();
            List<frmEsoCuidadosPreventivosComentarios> Comentarios = new List<frmEsoCuidadosPreventivosComentarios>();

            foreach (DataGridViewRow D in dataGridView1.Rows)
            {
                int CelIndex = D.Cells.Count - 2;

                DataGridViewCheckBoxCell cell = D.Cells[CelIndex] as DataGridViewCheckBoxCell;

                if (cell != null)
                {
                    frmEsoCuidadosPreventivos H = new frmEsoCuidadosPreventivos()
                    {
                        GrupoId = int.Parse(D.Cells["GrupoID"].Value.ToString()),
                        ParameterId = int.Parse(D.Cells["ParameterId"].Value.ToString()),
                        Valor = bool.Parse(cell.Value.ToString())
                    };

                    Hijos.Add(H);

                    frmEsoCuidadosPreventivosComentarios C = new frmEsoCuidadosPreventivosComentarios()
                    {
                        GrupoId = int.Parse(D.Cells["GrupoID"].Value.ToString()),
                        ParametroId = int.Parse(D.Cells["ParameterId"].Value.ToString()),
                        Comentario = D.Cells["Comentarios"].Value.ToString()
                    };

                    Comentarios.Add(C);
                }
            }

            data.Listado = Hijos;

            bool response = false;

            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                response = new ServiceBL().GuardarCuidadosPreventivos(data, _personId, Globals.ClientSession.i_SystemUserId, Globals.ClientSession.i_CurrentExecutionNodeId, Comentarios);
            }

            if (response)
            {
                MessageBox.Show("Registro guardado satisfactoriamente!.", "CORRECTO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Sucedió un error al intentar guardar la información .... intente más tarde.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbEstadoComponente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEstadoComponente.SelectedValue.ToString() == "2")
            {
                btnGuardarExamen.Enabled = false;
                btnVisorReporteExamen.Enabled = false;
            }
            else
            {
                btnGuardarExamen.Enabled = true;
                btnVisorReporteExamen.Enabled = true;
            }
        }

        private void gbFuncionesBiologicas_Enter(object sender, EventArgs e)
        {

        }

        private void grdConclusionesDiagnosticas_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            var caliFinal = (FinalQualification)e.Row.Cells["i_FinalQualificationId"].Value;

            switch (caliFinal)
            {
                case FinalQualification.SinCalificar:
                    e.Row.Appearance.BackColor = Color.Pink;
                    e.Row.Appearance.BackColor2 = Color.Pink;
                    break;
                case FinalQualification.Definitivo:
                    e.Row.Appearance.BackColor = Color.LawnGreen;
                    e.Row.Appearance.BackColor2 = Color.LawnGreen;
                    break;
                case FinalQualification.Presuntivo:
                    e.Row.Appearance.BackColor = Color.LawnGreen;
                    e.Row.Appearance.BackColor2 = Color.LawnGreen;
                    break;
                case FinalQualification.Descartado:
                    e.Row.Appearance.BackColor = Color.DarkGray;
                    e.Row.Appearance.BackColor2 = Color.DarkGray;
                    break;
                default:
                    break;
            }

            //Y doy el efecto degradado vertical
            e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
        }

        private void grdServiciosAnteriores_ClickCell(object sender, ClickCellEventArgs e)
        {
            btnVerServicioAnterior.Enabled = (grdServiciosAnteriores.Selected.Rows.Count > 0);

            if (grdServiciosAnteriores.Selected.Rows.Count == 0)
                return;

            _serviceIdByWiewServiceHistory = grdServiciosAnteriores.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
        }

        private void btnVerEditarAntecedentes_Click(object sender, EventArgs e)
        {
            ViewEditAntecedent();
        }


        private void InitializeData()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(InitializeData));
            }
            else
            {

                // Cargar datos generales del paciente
                OperationResult objOperationResult = new OperationResult();

                ServiceList personData = _serviceBL.GetServicePersonData(ref objOperationResult, _serviceId);
                _datosPersona = personData;
                _v_CustomerOrganizationId = personData.v_CustomerOrganizationId;
                _Dni = personData.v_DocNumber;

                _FechaServico = personData.d_ServiceDate;
                _personName = string.Format("{0} {1} {2}", personData.v_FirstLastName, personData.v_SecondLastName, personData.v_FirstName);
                //gbDatosPaciente.Text = string.Format("Datos del Paciente: {0} Tipo de Servicio: {1} Servicio: {2}", _personName, personData.v_ServiceTypeName, personData.v_MasterServiceName);
                _personId = personData.v_PersonId;
                //if (personData.b_PersonImage != null)
                //    pbPersonImage.Image = Common.Utils.byteArrayToImage(personData.b_PersonImage);
                _personImage = personData.b_PersonImage;

                lblTipoEso.Text = personData.v_EsoTypeName;
                lblServicio.Text = personData.v_ServiceId;
                lblProtocolName.Text = personData.v_ProtocolName;
                _ProtocolId = personData.v_ProtocolId;
                _customerOrganizationName = personData.v_EmployerOrganizationName;
                //lblFecVctoGlobal.Text = personData.d_GlobalExpirationDate == null ? "NO REQUIERE" : personData.d_GlobalExpirationDate.Value.ToShortDateString();
                lblFecVctoObs.Text = personData.d_ObsExpirationDate == null ? string.Empty : personData.d_ObsExpirationDate.Value.ToShortDateString();
                //lblGeso.Text = personData.v_GroupOcupationName;
                //lblTipoServ.Text = personData.v_ServiceTypeName;
                //lblServicio.Text = personData.v_MasterServiceName;
                _masterServiceId = personData.i_MasterServiceId.Value;

                if (_masterServiceId == 2) btnReceta.Enabled = false;
                else btnReceta.Enabled = true;

                cbAptitudEso.SelectedValue = personData.i_AptitudeStatusId.ToString();
                txtComentarioAptitud.Text = personData.v_ObsStatusService;

                DateTime? FechaActual = DateTime.Now;
                dptDateGlobalExp.Value = personData.d_GlobalExpirationDate == null ? FechaActual.Value.Date.AddYears(1) : personData.d_GlobalExpirationDate.Value;
                //cbNuevoControl.SelectedValue = personData.i_IsNewControl;
                //lblFecInicio.Text = personData.d_ServiceDate == null ? string.Empty : personData.d_ServiceDate.Value.ToShortDateString();

                // calcular edad
                _age = DateTime.Today.AddTicks(-personData.d_BirthDate.Value.Ticks).Year - 1;
                //lblEdad.Text = _age.ToString();
                _AMCGenero = personData.i_SexTypeId.Value;
                //lblGenero.Text = personData.v_GenderName;
                //lblPuesto.Text = personData.v_CurrentOccupation;

                // cargar datos INICIALES de ANAMNESIS
                chkPresentaSisntomas.Checked = Convert.ToBoolean(personData.i_HasSymptomId); //
                // Activar / Desactivar segun check presenta sintomas
                txtSintomaPrincipal.Enabled = chkPresentaSisntomas.Checked;//
                txtValorTiempoEnfermedad.Enabled = chkPresentaSisntomas.Checked;//
                cbCalendario.Enabled = chkPresentaSisntomas.Checked;

                txtSintomaPrincipal.Text = string.IsNullOrEmpty(personData.v_MainSymptom) ? "No Refiere" : personData.v_MainSymptom;
                txtValorTiempoEnfermedad.Text = personData.i_TimeOfDisease == null ? string.Empty : personData.i_TimeOfDisease.ToString();
                cbCalendario.SelectedValue = personData.i_TimeOfDiseaseTypeId == null ? "1" : personData.i_TimeOfDiseaseTypeId.ToString();
                txtRelato.Text = string.IsNullOrEmpty(personData.v_Story) ? "Paciente Asintomático" : personData.v_Story;

                _cancelEventSelectedIndexChange = true;
                cbSueño.SelectedValue = personData.i_DreamId == null ? "1" : personData.i_DreamId.ToString();
                cbOrina.SelectedValue = personData.i_UrineId == null ? "1" : personData.i_UrineId.ToString();
                cbDeposiciones.SelectedValue = personData.i_DepositionId == null ? "1" : personData.i_DepositionId.ToString();
                cbApetito.SelectedValue = personData.i_AppetiteId == null ? "1" : personData.i_AppetiteId.ToString();
                cbSed.SelectedValue = personData.i_ThirstId == null ? "1" : personData.i_ThirstId.ToString();
                _cancelEventSelectedIndexChange = false;

                txtHallazgos.Text = string.IsNullOrEmpty(personData.v_Findings) ? "Sin Alteración" : personData.v_Findings;
                if (personData.d_Fur != null)
                {
                    dtpFur.Checked = true;
                    dtpFur.Value = personData.d_Fur.Value.Date;
                }
                txtRegimenCatamenial.Text = personData.v_CatemenialRegime;
                cbMac.SelectedValue = personData.i_MacId == null ? "1" : personData.i_MacId.ToString();

                //txtFechaUltimoPAP.Text = string.IsNullOrEmpty(personData.v_FechaUltimoPAP) ? "" : personData.v_FechaUltimoPAP;
                //txtFechaUltimaMamo.Text = string.IsNullOrEmpty(personData.v_FechaUltimaMamo) ? "" : personData.v_FechaUltimaMamo;
                txtResultadoPAP.Text = string.IsNullOrEmpty(personData.v_ResultadosPAP) ? "" : personData.v_ResultadosPAP;
                txtResultadoMamo.Text = string.IsNullOrEmpty(personData.v_ResultadoMamo) ? "" : personData.v_ResultadoMamo;

                txtVidaSexual.Text = string.IsNullOrEmpty(personData.v_InicioVidaSexaul) ? "" : personData.v_InicioVidaSexaul;
                txtNroParejasActuales.Text = string.IsNullOrEmpty(personData.v_NroParejasActuales) ? "" : personData.v_NroParejasActuales;
                txtNroAbortos.Text = string.IsNullOrEmpty(personData.v_NroAbortos) ? "" : personData.v_NroAbortos;
                txtNroCausa.Text = string.IsNullOrEmpty(personData.v_PrecisarCausas) ? "" : personData.v_PrecisarCausas;


                //-----------------------------------------------------------------
                //if (personData.d_PAP != null)
                //{
                //    dtpPAP.Value = personData.d_PAP.Value;
                //    dtpPAP.Checked = true;
                //}


                //if (personData.d_Mamografia != null)
                //{
                //    dtpMamografia.Value = personData.d_Mamografia.Value;
                //    dtpMamografia.Checked = true;
                //}


                txtGestapara.Text = string.IsNullOrEmpty(personData.v_Gestapara) ? "G ( )  P ( ) ( ) ( ) ( ) " : personData.v_Gestapara;
                txtMenarquia.Text = personData.v_Menarquia;
                txtCiruGine.Text = personData.v_CiruGine;

                //-----------------------------------------------------------------

                _sexType = (Gender)personData.i_SexTypeId;
                _sexTypeId = personData.i_SexTypeId;
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

                #region Antecedentes / Servicios

                // Cargar grilla
                GetAntecedentConsolidateForService(_personId);
                GetServicesConsolidateForService(_personId);

                #endregion

                #region Datos Personales

                Utils.LoadDropDownList(cboGenero, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 100, null), DropDownListAction.Select);
                Utils.LoadDropDownList(cboEstadoCivil, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 101, null), DropDownListAction.Select);
                Utils.LoadDropDownList(cboGradoInstruccion, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 108, null), DropDownListAction.Select);
                Utils.LoadDropDownList(ddlBloodGroupId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 154, null), DropDownListAction.Select);
                Utils.LoadDropDownList(ddlBloodFactorId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 155, null), DropDownListAction.Select);


                txtApellidoPaterno.Text = personData.v_FirstLastName;
                txtApellidoMaterno.Text = personData.v_SecondLastName;
                txtNombres.Text = personData.v_FirstName;
                cboGenero.SelectedValue = personData.i_SexTypeId.ToString();
                txtEdad.Text = _age.ToString();
                dtpFechaNacimiento.Value = personData.d_BirthDate.Value;
                txtLugarNacimiento.Text = personData.v_BirthPlace;
                txtProcedencia.Text = personData.v_Procedencia;
                ddlBloodFactorId.SelectedValue = personData.i_BloodFactorId.ToString();
                ddlBloodGroupId.SelectedValue = personData.i_BloodGroupId.ToString();
                cboGradoInstruccion.SelectedValue = personData.i_LevelOfId.ToString();
                cboEstadoCivil.SelectedValue = personData.i_MaritalStatusId.ToString();
                txtOcupacion.Text = personData.v_CurrentOccupation;
                txtDomicilio.Text = personData.v_AdressLocation;
                txtCentroEducativo.Text = personData.v_CentroEducativo;
                txtHijosVivos.Text = personData.TotalHijos.ToString();
                lblDni.Text = personData.v_DocNumber.ToString();

                idPerson = _objAtencionesIntegralesBl.GetService(_serviceId);

                //_personId
                if (_age <= 12)
                {
                    Utils.LoadDropDownList(listaEstadoCivilMujer, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 101, null), DropDownListAction.Select);
                    Utils.LoadDropDownList(listaGradoInstruccionMujer, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 108, null), DropDownListAction.Select);
                    Utils.LoadDropDownList(listTipoAfiliacionMujer, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 188, null), DropDownListAction.Select);

                    Utils.LoadDropDownList(listaEstadoCivilHombre, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 101, null), DropDownListAction.Select);
                    Utils.LoadDropDownList(listaGradoInstruccionHombre, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 108, null), DropDownListAction.Select);
                    Utils.LoadDropDownList(listTipoAfiliacionHombre, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 188, null), DropDownListAction.Select);


                    tbcDatos.TabPages.Remove(tbpAdultoMayor);
                    tbcDatos.TabPages.Remove(tbpAdolescente);

                    objNinioDto = _objAtencionesIntegralesBl.GetNinio(ref objOperationResult, idPerson.v_PersonId);

                    if (objNinioDto != null)
                    {
                        txtNombreMadrePadreResponsable.Text = objNinioDto.v_NombreCuidador;
                        txtEdadMadrePadreResponsable.Text = objNinioDto.v_EdadCuidador;
                        txtDNIMadrePadreResponsable.Text = objNinioDto.v_DniCuidador;

                        txtNombrePadreTutor.Text = objNinioDto.v_NombrePadre;
                        txtEdadPadre.Text = objNinioDto.v_EdadPadre;
                        txtDNIPadre.Text = objNinioDto.v_DniPadre;
                        listTipoAfiliacionHombre.SelectedValue = objNinioDto.i_TipoAfiliacionPadre.ToString();
                        txtAfiliacionPadre.Text = objNinioDto.v_CodigoAfiliacionPadre;
                        listaGradoInstruccionHombre.SelectedValue = objNinioDto.i_GradoInstruccionPadre.ToString();
                        txtOcupacionPadre.Text = objNinioDto.v_OcupacionPadre;
                        listaEstadoCivilHombre.SelectedValue = objNinioDto.i_EstadoCivilIdPadre.ToString();
                        txtReligionPadre.Text = objNinioDto.v_ReligionPadre;

                        txtNombreMadreTutor.Text = objNinioDto.v_NombreMadre;
                        txtEdadMadre.Text = objNinioDto.v_EdadMadre;
                        txtDNIMadre.Text = objNinioDto.v_DniMadre;
                        listTipoAfiliacionMujer.SelectedValue = objNinioDto.i_TipoAfiliacionMadre.ToString();
                        txtAfiliacionMadre.Text = objNinioDto.v_CodigoAfiliacionMadre;
                        listaGradoInstruccionMujer.SelectedValue = objNinioDto.i_GradoInstruccionMadre.ToString();
                        txtOcupacionMadre.Text = objNinioDto.v_OcupacionMadre;
                        listaEstadoCivilMujer.SelectedValue = objNinioDto.i_EstadoCivilIdMadre1.ToString();
                        txtReligionMadre.Text = objNinioDto.v_ReligionMadre;

                        textPatologiasGestacion.Text = objNinioDto.v_PatologiasGestacion;
                        txtNEmbarazo.Text = objNinioDto.v_nEmbarazos;
                        txtAPN.Text = objNinioDto.v_nAPN;
                        textLugarAPN.Text = objNinioDto.v_LugarAPN;
                        textComplicacionesParto.Text = objNinioDto.v_ComplicacionesParto;
                        textQuienAtendio.Text = objNinioDto.v_Atencion;

                        textEdadNacer.Text = objNinioDto.v_EdadGestacion;
                        textPesoNacer.Text = objNinioDto.v_Peso;
                        textTallaNacer.Text = objNinioDto.v_Talla;
                        textPerimetroCefalico.Text = objNinioDto.v_PerimetroCefalico;
                        textTiempoHospit.Text = objNinioDto.v_TiempoHospitalizacion;
                        textPerimetroToracico.Text = objNinioDto.v_PerimetroToracico;
                        textEspecificacionesNacer.Text = objNinioDto.v_EspecificacionesNac;
                        txtInicioAlimentación.Text = objNinioDto.v_InicioAlimentacionComp;
                        textAlergiaMedicamentos.Text = objNinioDto.v_AlergiasMedicamentos;
                        textOtrosAntecedentes.Text = objNinioDto.v_OtrosAntecedentes;

                        textQuienTuberculosis.Text = objNinioDto.v_QuienTuberculosis;
                        if (objNinioDto.i_QuienTuberculosis == (int)Enfermedad.Si)
                            rbSiTuberculosis.Checked = true;
                        else
                            rbNoTuberculosis.Checked = true;

                        textQuienASMA.Text = objNinioDto.v_QuienAsma;
                        if (objNinioDto.i_QuienAsma == (int)Enfermedad.Si)
                            rbSiAsma.Checked = true;
                        else
                            rbNoAsma.Checked = true;

                        textQuienVIH.Text = objNinioDto.v_QuienVIH;
                        if (objNinioDto.i_QuienVIH == (int)Enfermedad.Si)
                            rbSiVih.Checked = true;
                        else
                            rbNoVih.Checked = true;

                        textQuienDiabetes.Text = objNinioDto.v_QuienDiabetes;
                        if (objNinioDto.i_QuienDiabetes == (int)Enfermedad.Si)
                            rbSiDiabetes.Checked = true;
                        else
                            rbNoDiabetes.Checked = true;

                        textQuienEpilepsia.Text = objNinioDto.v_QuienEpilepsia;
                        if (objNinioDto.i_QuienEpilepsia == (int)Enfermedad.Si)
                            rbSiEpilepsia.Checked = true;
                        else
                            rbNoEpilepsia.Checked = true;

                        textQuienAlergias.Text = objNinioDto.v_QuienAlergias;
                        if (objNinioDto.i_QuienAlergias == (int)Enfermedad.Si)
                            rbSiAlergia.Checked = true;
                        else
                            rbNoAlergia.Checked = true;

                        textQuienViolenciaFamiliar.Text = objNinioDto.v_QuienViolenciaFamiliar;
                        if (objNinioDto.i_QuienViolenciaFamiliar == (int)Enfermedad.Si)
                            rbSiViolencia.Checked = true;
                        else
                            rbNoViolencia.Checked = true;

                        textQuienAlcoholismo.Text = objNinioDto.v_QuienAlcoholismo;
                        if (objNinioDto.i_QuienAlcoholismo == (int)Enfermedad.Si)
                            rbSiAlcoholismo.Checked = true;
                        else
                            rbNoAlcoholismo.Checked = true;

                        textQuienDrogadiccion.Text = objNinioDto.v_QuienDrogadiccion;
                        if (objNinioDto.i_QuienDrogadiccion == (int)Enfermedad.Si)
                            rbSiDrogadiccion.Checked = true;
                        else
                            rbNoDrogadiccion.Checked = true;

                        textQuienHepatitisB.Text = objNinioDto.v_QuienHeptitisB;
                        if (objNinioDto.i_QuienHeptitisB == (int)Enfermedad.Si)
                            rbSiHepatitis.Checked = true;
                        else
                            rbNoHepatitis.Checked = true;

                        textAguaPotable.Text = objNinioDto.v_EspecificacionesAgua;
                        textDesague.Text = objNinioDto.v_EspecificacionesDesague;
                    }
                }
                else if (13 <= _age && _age <= 17)
                {
                    tbcDatos.TabPages.Remove(tbpNinio);
                    tbcDatos.TabPages.Remove(tbpAdultoMayor);

                    objAdolDto = _objAtencionesIntegralesBl.GetAdolescente(ref objOperationResult, idPerson.v_PersonId);

                    if (objAdolDto != null)
                    {
                        textNombreCuidadorAdol.Text = objAdolDto.v_NombreCuidador;
                        textDniCuidadorAdol.Text = objAdolDto.v_DniCuidador;
                        textEdadAdol.Text = objAdolDto.v_EdadCuidador;
                        textViveConAdol.Text = objAdolDto.v_ViveCon;
                        txtAdoEdadIniTrab.Text = objAdolDto.v_EdadInicioTrabajo;
                        txtAdoTipoTrab.Text = objAdolDto.v_TipoTrabajo;
                        txtAdoNroTv.Text = objAdolDto.v_NroHorasTv;
                        txtAdoNroJuegos.Text = objAdolDto.v_NroHorasJuegos;
                        txtAdoMenarquia.Text = objAdolDto.v_MenarquiaEspermarquia;
                        txtAdoEdadRS.Text = objAdolDto.v_EdadInicioRS;
                        textObservacionesSexualidadAdol.Text = objAdolDto.v_Observaciones;
                    }
                }
                else if (18 <= _age && _age <= 64)
                {
                    tbcDatos.TabPages.Remove(tbpNinio);
                    tbcDatos.TabPages.Remove(tbpAdolescente);
                    CargarGrillaEmbarazos(idPerson.v_PersonId);

                    if (personData.i_SexTypeId == (int)Gender.MASCULINO)
                    {
                        pnlMenarquia.Enabled = false;
                        pnlEmbarazo.Enabled = false;

                        objAdultoDto = _objAtencionesIntegralesBl.GetAdulto(ref objOperationResult, idPerson.v_PersonId);

                        if (objAdultoDto != null)
                        {
                            txtAmCuidador.Text = objAdultoDto.v_NombreCuidador;
                            txtAmCuidadorEdad.Text = objAdultoDto.v_EdadCuidador;
                            txtAmCuidadorDni.Text = objAdultoDto.v_DniCuidador;
                            textOtroAntecedentesFamiliares.Text = objAdultoDto.v_OtrosAntecedentes;
                            textFlujoVaginalPatológico.Text = objAdultoDto.v_FlujoVaginal;
                            textMedicamentoFrecuenteDosis.Text = objAdultoDto.v_MedicamentoFrecuente;
                            textReaccionAlergicaMedicamentos.Text = objAdultoDto.v_ReaccionAlergica;
                            txtAmInicioRs.Text = objAdultoDto.v_InicioRS;
                            txtAmNroPs.Text = objAdultoDto.v_NroPs;
                            txtAmFechaUR.Text = objAdultoDto.v_FechaUR;
                            //txtAmRC.Text = objAdultoDto.v_RC;
                            //txtAmNroParto.Text = objAdultoDto.v_Parto;
                            //txtAmPrematuro.Text = objAdultoDto.v_Prematuro;
                            //txtAmAborto.Text = objAdultoDto.v_Aborto;
                            //textObservacionesEmbarazo.Text = objAdultoDto.v_ObservacionesEmbarazo;
                        }
                    }
                    else
                    {
                        pnlMenarquia.Enabled = true;
                        pnlEmbarazo.Enabled = true;

                        objAdultoDto = _objAtencionesIntegralesBl.GetAdulto(ref objOperationResult, idPerson.v_PersonId);

                        if (objAdultoDto != null)
                        {
                            txtAmCuidador.Text = objAdultoDto.v_NombreCuidador;
                            txtAmCuidadorEdad.Text = objAdultoDto.v_EdadCuidador;
                            txtAmCuidadorDni.Text = objAdultoDto.v_DniCuidador;
                            textOtroAntecedentesFamiliares.Text = objAdultoDto.v_OtrosAntecedentes;
                            textFlujoVaginalPatológico.Text = objAdultoDto.v_FlujoVaginal;
                            textMedicamentoFrecuenteDosis.Text = objAdultoDto.v_MedicamentoFrecuente;
                            textReaccionAlergicaMedicamentos.Text = objAdultoDto.v_ReaccionAlergica;
                            txtAmInicioRs.Text = objAdultoDto.v_InicioRS;
                            txtAmNroPs.Text = objAdultoDto.v_NroPs;
                            txtAmFechaUR.Text = objAdultoDto.v_FechaUR;
                            txtAmRC.Text = objAdultoDto.v_RC;
                            txtAmNroParto.Text = objAdultoDto.v_Parto;
                            txtAmPrematuro.Text = objAdultoDto.v_Prematuro;
                            txtAmAborto.Text = objAdultoDto.v_Aborto;
                            textObservacionesEmbarazo.Text = objAdultoDto.v_ObservacionesEmbarazo;
                        }
                    }
                }
                else
                {
                    tbcDatos.TabPages.Remove(tbpNinio);
                    tbcDatos.TabPages.Remove(tbpAdolescente);

                    if (personData.i_SexTypeId == (int)Gender.MASCULINO)
                    {
                        pnlMenarquia.Enabled = false;
                        pnlEmbarazo.Enabled = false;

                        objAdultoMAyorDto = _objAtencionesIntegralesBl.GetAdultoMayor(ref objOperationResult, idPerson.v_PersonId);

                        if (objAdultoMAyorDto != null)
                        {
                            txtAmCuidador.Text = objAdultoMAyorDto.v_NombreCuidador;
                            txtAmCuidadorEdad.Text = objAdultoMAyorDto.v_EdadCuidador;
                            txtAmCuidadorDni.Text = objAdultoMAyorDto.v_DniCuidador;
                            //textOtroAntecedentesFamiliares.Text = objAdultoMAyorDto.v_OtrosAntecedentes;
                            textFlujoVaginalPatológico.Text = objAdultoMAyorDto.v_FlujoVaginal;
                            textMedicamentoFrecuenteDosis.Text = objAdultoMAyorDto.v_MedicamentoFrecuente;
                            textReaccionAlergicaMedicamentos.Text = objAdultoMAyorDto.v_ReaccionAlergica;
                            txtAmInicioRs.Text = objAdultoMAyorDto.v_InicioRS;
                            txtAmNroPs.Text = objAdultoMAyorDto.v_NroPs;
                            txtAmFechaUR.Text = objAdultoMAyorDto.v_FechaUR;
                            //txtAmRC.Text = objAdultoDto.v_RC;
                            //txtAmNroParto.Text = objAdultoDto.v_Parto;
                            //txtAmPrematuro.Text = objAdultoDto.v_Prematuro;
                            //txtAmAborto.Text = objAdultoDto.v_Aborto;
                            //textObservacionesEmbarazo.Text = objAdultoDto.v_ObservacionesEmbarazo;
                        }
                    }
                    else
                    {
                        pnlMenarquia.Enabled = true;
                        pnlEmbarazo.Enabled = true;

                        objAdultoMAyorDto = _objAtencionesIntegralesBl.GetAdultoMayor(ref objOperationResult, idPerson.v_PersonId);

                        if (objAdultoMAyorDto != null)
                        {
                            txtAmCuidador.Text = objAdultoMAyorDto.v_NombreCuidador;
                            txtAmCuidadorEdad.Text = objAdultoMAyorDto.v_EdadCuidador;
                            txtAmCuidadorDni.Text = objAdultoMAyorDto.v_DniCuidador;
                            //textOtroAntecedentesFamiliares.Text = objAdultoMAyorDto.v_OtrosAntecedentes;
                            textFlujoVaginalPatológico.Text = objAdultoMAyorDto.v_FlujoVaginal;
                            textMedicamentoFrecuenteDosis.Text = objAdultoMAyorDto.v_MedicamentoFrecuente;
                            textReaccionAlergicaMedicamentos.Text = objAdultoMAyorDto.v_ReaccionAlergica;
                            txtAmInicioRs.Text = objAdultoMAyorDto.v_InicioRS;
                            txtAmNroPs.Text = objAdultoMAyorDto.v_NroPs;
                            txtAmFechaUR.Text = objAdultoMAyorDto.v_FechaUR;
                            txtAmRC.Text = objAdultoMAyorDto.v_RC;
                            txtAmNroParto.Text = objAdultoMAyorDto.v_Parto;
                            txtAmPrematuro.Text = objAdultoMAyorDto.v_Prematuro;
                            txtAmAborto.Text = objAdultoMAyorDto.v_Aborto;
                            textObservacionesEmbarazo.Text = objAdultoMAyorDto.v_ObservacionesEmbarazo;
                        }
                    }
                }

                #endregion

                #region Cargar Atención Integral

                CargarGrillaProblemas(_personId);
                CargarGrillaPlanAtencionIntegral(_personId);
                #endregion

                // Analizar el resultado de la operación
                if (objOperationResult.Success != 1)
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

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


        private void OpenFormProcesoEso(DataEso datos)
        {
            Form procesoEso = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "frmProcesosEso").SingleOrDefault<Form>();

            if (procesoEso != null)
            {
                procesoEso.Select();
                procesoEso.WindowState = FormWindowState.Minimized;
                var frm = (frmProcesosEso)procesoEso;
                frm.DataSource = datos;
            }
            else
            {
                procesoEso = new frmProcesosEso();
                var frm = (frmProcesosEso)procesoEso;
                frm.DataSource = datos;
                procesoEso.Show();
            }
        }


        private void ProcessControlBySelectedTab_AMC(Infragistics.Win.UltraWinTabControl.UltraTabPageControl selectedTab)
        {
            if (_serviceComponentFieldsList == null)
                _serviceComponentFieldsList = new List<ServiceComponentFieldsList>();

            KeyTagControl keyTagControl = null;

            string value1 = null;

            ServiceComponentFieldsList serviceComponentFields = null;
            ServiceComponentFieldValuesList serviceComponentFieldValues = null;

            var serviceComponentId = selectedTab.Tab.Tag.ToString();
            var componentId = selectedTab.Tab.Key;
            var component = _tmpServiceComponentsForBuildMenuList.Find(p => p.v_ComponentId == componentId);

            if (_EstadoComponente == (int)ServiceComponentStatus.Evaluado)
            {
                selectedTab.Tab.Appearance.BackColor = Color.Pink;
            }
            else
            {
                selectedTab.Tab.Appearance.BackColor = Color.Transparent;
            }
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

                    if (keyTagControl.i_ControlId == (int)ControlType.UcOdontograma || keyTagControl.i_ControlId == (int)ControlType.UcAudiometria || keyTagControl.i_ControlId == (int)ControlType.UcSomnolencia || keyTagControl.i_ControlId == (int)ControlType.UcAcumetria || keyTagControl.i_ControlId == (int)ControlType.UcSintomaticoRespi || keyTagControl.i_ControlId == (int)ControlType.UcRxLumboSacra || keyTagControl.i_ControlId == (int)ControlType.UcOtoscopia || keyTagControl.i_ControlId == (int)ControlType.UcEvaluacionErgonomica || keyTagControl.i_ControlId == (int)ControlType.UcOjoSeco || keyTagControl.i_ControlId == (int)ControlType.UcOsteoMuscular || keyTagControl.i_ControlId == (int)ControlType.UcFototipo || keyTagControl.i_ControlId == (int)ControlType.Fecha)
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


        private void cbSueño_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Flag
            if (_cancelEventSelectedIndexChange)
                return;

            if (int.Parse(cbSueño.SelectedValue.ToString()) == (int)NormalAlterado.Alterado)
            {
                var frm = new Operations.Popups.frmRegisterFinding("Funciones Biológicas", "", "Sueño");

                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.Cancel)
                    return;

                //txtHallazgos.Text = null;
                #region Obtener campo Hallazgo


                #endregion

                if (txtHallazgos.Text != null)
                {
                    #region Escribir en el campo hallazgo

                    StringBuilder sb = new StringBuilder();

                    if (txtHallazgos.Text == string.Empty)
                    {
                        sb.Append(frm.FindingText);
                    }
                    else
                    {
                        sb.Append(txtHallazgos.Text);
                        sb.Append("\r\n");
                        sb.Append(frm.FindingText);
                    }

                    txtHallazgos.Text = sb.ToString();

                    #endregion

                }


            }
        }

        private void cbApetito_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Flag
            if (_cancelEventSelectedIndexChange)
                return;
            if (int.Parse(cbApetito.SelectedValue.ToString()) == (int)NormalAlterado.Alterado)
            {
                var frm = new Operations.Popups.frmRegisterFinding("Funciones Biológicas", "", "Apetito");

                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.Cancel)
                    return;

                //txtHallazgos.Text = null;
                #region Obtener campo Hallazgo


                #endregion

                if (txtHallazgos.Text != null)
                {
                    #region Escribir en el campo hallazgo

                    StringBuilder sb = new StringBuilder();

                    if (txtHallazgos.Text == string.Empty)
                    {
                        sb.Append(frm.FindingText);
                    }
                    else
                    {
                        sb.Append(txtHallazgos.Text);
                        sb.Append("\r\n");
                        sb.Append(frm.FindingText);
                    }

                    txtHallazgos.Text = sb.ToString();

                    #endregion

                }


            }
        }

        private void cbOrina_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Flag
            if (_cancelEventSelectedIndexChange)
                return;

            if (int.Parse(cbOrina.SelectedValue.ToString()) == (int)NormalAlterado.Alterado)
            {
                var frm = new Operations.Popups.frmRegisterFinding("Funciones Biológicas", "", "Orina");

                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.Cancel)
                    return;

                //txtHallazgos.Text = null;
                #region Obtener campo Hallazgo


                #endregion

                if (txtHallazgos.Text != null)
                {
                    #region Escribir en el campo hallazgo

                    StringBuilder sb = new StringBuilder();

                    if (txtHallazgos.Text == string.Empty)
                    {
                        sb.Append(frm.FindingText);
                    }
                    else
                    {
                        sb.Append(txtHallazgos.Text);
                        sb.Append("\r\n");
                        sb.Append(frm.FindingText);
                    }

                    txtHallazgos.Text = sb.ToString();

                    #endregion

                }


            }
        }

        private void cbSed_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Flag
            if (_cancelEventSelectedIndexChange)
                return;
            if (int.Parse(cbSed.SelectedValue.ToString()) == (int)NormalAlterado.Alterado)
            {
                var frm = new Operations.Popups.frmRegisterFinding("Funciones Biológicas", "", "Sed");

                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.Cancel)
                    return;

                //txtHallazgos.Text = null;
                #region Obtener campo Hallazgo


                #endregion

                if (txtHallazgos.Text != null)
                {
                    #region Escribir en el campo hallazgo

                    StringBuilder sb = new StringBuilder();

                    if (txtHallazgos.Text == string.Empty)
                    {
                        sb.Append(frm.FindingText);
                    }
                    else
                    {
                        sb.Append(txtHallazgos.Text);
                        sb.Append("\r\n");
                        sb.Append(frm.FindingText);
                    }

                    txtHallazgos.Text = sb.ToString();

                    #endregion

                }


            }
        }

        private void cbDeposiciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Flag
            if (_cancelEventSelectedIndexChange)
                return;
            if (int.Parse(cbDeposiciones.SelectedValue.ToString()) == (int)NormalAlterado.Alterado)
            {
                var frm = new Operations.Popups.frmRegisterFinding("Funciones Biológicas", "", "Deposiciones");

                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.Cancel)
                    return;

                //txtHallazgos.Text = null;
                #region Obtener campo Hallazgo


                #endregion

                if (txtHallazgos.Text != null)
                {
                    #region Escribir en el campo hallazgo

                    StringBuilder sb = new StringBuilder();

                    if (txtHallazgos.Text == string.Empty)
                    {
                        sb.Append(frm.FindingText);
                    }
                    else
                    {
                        sb.Append(txtHallazgos.Text);
                        sb.Append("\r\n");
                        sb.Append(frm.FindingText);
                    }

                    txtHallazgos.Text = sb.ToString();

                    #endregion

                }


            }
        }

        private void ultraTabSharedControlsPage1_MouseLeave(object sender, EventArgs e)
        {

        }


        private void FrmEsoV2_SizeChanged(object sender, EventArgs e)
        {
            gbRecomendaciones_Conclusiones.Width = tpConclusion.Width / 2 - 50;
            gbRestricciones_Conclusiones.Width = tpConclusion.Width / 2 - 20;

        }

        private void btnVerServicioAnterior_Click(object sender, EventArgs e)
        {
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceIdByWiewServiceHistory);

            var ServiceDate = grdServiciosAnteriores.Selected.Rows[0].Cells["d_ServiceDate"].Value.ToString();
            if (ServiceDate.ToString().Split(' ')[0] == DateTime.Now.ToString().Split(' ')[0])
            {

                var frm = new Operations.FrmEsoV2(_serviceIdByWiewServiceHistory, null, "", Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, (int)MasterService.Eso);
                frm.ShowDialog();
            }
            else
            {

                var frm = new Operations.FrmEsoV2(_serviceIdByWiewServiceHistory, null, "View", Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_SystemUserId, (int)MasterService.Eso);
                frm.ShowDialog();
            }
        }

        private void tcSubMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcSubMain.SelectedIndex == 1)
            {
                lblView.Visible = true;
            }
            else if (tcSubMain.SelectedIndex == 3)
            {
            //    btnGuardarConclusiones.Visible = true;
            //    btnGuardarConclusiones.Location = new Point(1147, 500);
            //    btnInterConsulta.Visible = true;
            //    btnInterConsulta.Location = new Point(12, 500);
            //    btnSubirInterconsulta.Visible = true;
            //    btnSubirInterconsulta.Location = new Point(175, 500);
            //    btnCertificadoAptitud.Visible = true;
            //    btnCertificadoAptitud.Location = new Point(338, 500);
            //    btn312.Visible = true;
            //    btn312.Location = new Point(508, 500);
            //    btn7C.Visible = true;
            //    btn7C.Location = new Point(676, 500);
                chkinterconsulta.Visible = true;
                chkinterconsulta.Location = new Point(504, 186);
            //    chkUtilizaFirmaAptitud.Visible = true;
            //    chkUtilizaFirmaAptitud.Location = new Point(1014, 500);
            //    checkFirmaYanacocha.Visible = true;
            //    checkFirmaYanacocha.Location = new Point(850, 500);

            }
            else
            {
                lblView.Visible = false;
            }
        }

        private void cbAptitudEso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAptitudEso.SelectedIndex != (int)AptitudeStatus.Apto && cbAptitudEso.SelectedIndex != (int)AptitudeStatus.SinAptitud)
            {
                label34.Visible = true;
                txtComentarioAptitud.Visible = true;
            }
            else
            {
                label34.Visible = false;
                txtComentarioAptitud.Visible = false;
            }
        }
        
    }
}
