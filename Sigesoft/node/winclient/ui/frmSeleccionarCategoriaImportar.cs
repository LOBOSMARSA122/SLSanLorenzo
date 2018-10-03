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
using System.IO;
using NetPdf;
using Infragistics.Win.UltraWinGrid;
namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmSeleccionarCategoriaImportar : Form
    {
        ServiceBL _serviceBL = new ServiceBL();  
        List<KeyValueDTO> _componentListTemp = new List<KeyValueDTO>();
        public frmSeleccionarCategoriaImportar()
        {
            InitializeComponent();
        }

        private void frmSeleccionarCategoriaImportar_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            var componentProfile = _serviceBL.GetRoleNodeComponentProfileByRoleNodeId(Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_RoleId.Value);

            _componentListTemp = BLL.Utils.GetAllComponents(ref objOperationResult);

            var sss = _componentListTemp.FindAll(p => p.Value4 == 14);

            var xxx = _componentListTemp.FindAll(p => p.Value4 != -1);


            List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();

            groupComponentList.AddRange(_componentListTemp.ToList().FindAll(p => p.Value4 == -1));
            var results = groupComponentList.FindAll(f => componentProfile.Any(t => t.v_ComponentId == f.Value2));

            Utils.LoadDropDownList(ddlConsultorio, "Value1", "Id", results, DropDownListAction.Select);
        }

        private void btnImportarExcel_Click(object sender, EventArgs e)
        {
            if (uvImportacion.Validate(true, false).IsValid)
            {

                #region Declaraciones
                OperationResult objOperationResult = new OperationResult();
                ServiceComponentFieldsList serviceComponentFields = null;
                ServiceComponentFieldValuesList serviceComponentFieldValues = null;
                List<ServiceComponentFieldValuesList> _serviceComponentFieldValuesList = null;
                List<ServiceComponentFieldsList> _serviceComponentFieldsList = null;


                MedicalExamFieldValuesBL oMedicalExamFieldValuesBL = new MedicalExamFieldValuesBL();
                string ServiceComponentId = "";
                string Personid = "";


                if (_serviceComponentFieldsList == null)
                    _serviceComponentFieldsList = new List<ServiceComponentFieldsList>();

                #endregion

                var x = (KeyValueDTO)ddlConsultorio.SelectedItem;
                  openFileDialog1.FileName = string.Empty;
             openFileDialog1.Filter = "Image Files (*.xls;*.xlsx;*.xlsm)|*.xls;*.xlsx;*.xlsm";
             if (openFileDialog1.ShowDialog() == DialogResult.OK)
             {
                    var Ext = Path.GetExtension(openFileDialog1.FileName).ToUpper();

                    if (Ext == ".XLSX" || Ext == ".XLS" || Ext == ".XLSM")
                    {

                        Infragistics.Documents.Excel.Workbook workbook1 = Infragistics.Documents.Excel.Workbook.Load(openFileDialog1.FileName);

                        Infragistics.Documents.Excel.Worksheet worksheet1 = workbook1.Worksheets["EXAMEN"];


                        int fila = 3;
                        int colInicio = 0;
                        int colFinal = 0;
                        int filaAntecedentes = 3;
                        int filaDxAudiometria = 3;
                        int filaDxLaboratorio = 3;
                        int filaDxMedicina = 3;
                        int filaDxOftalmologia = 3;
                        int filaDxPsicologia = 3;
                        int tope = 0;
                        //Obtener el nombre del Examen
                        var Examen = worksheet1.Rows[fila].Cells[2].Value.ToString();
                        servicecomponentDto serviceComponentDto = new servicecomponentDto();
                        List<DiagnosticRepositoryList> ListaDxByComponent = new List<DiagnosticRepositoryList>();
                      
                        PacientBL oPacientBL = new PacientBL();

                        #region OFTALMOLOGÍA
                        if (x.Value1 == "OFTALMOLOGÍA")
                        {
                            while (worksheet1.Rows[fila].Cells[0].Value != null)
                            {
                                //Inicializamos columnas del excel
                                colInicio = 49;
                                colFinal = 83;
                                tope = fila;
                                //Cargar Entidad Service Component
                                serviceComponentDto = new servicecomponentDto();
                                serviceComponentDto.v_ServiceComponentId = worksheet1.Rows[fila].Cells[1].Value.ToString();
                                serviceComponentDto.v_Comment = "";
                                serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Evaluado;
                                serviceComponentDto.i_ExternalInternalId = (int)ComponenteProcedencia.Interno;
                                serviceComponentDto.i_IsApprovedId = (int)SiNo.NO;
                                string ComponentId = worksheet1.Rows[fila].Cells[2].Value.ToString();
                                serviceComponentDto.v_ComponentId = ComponentId;
                                string ServiceId = worksheet1.Rows[fila].Cells[0].Value.ToString();
                                serviceComponentDto.v_ServiceId = ServiceId;

                                ServiceComponentId = worksheet1.Rows[fila].Cells[1].Value.ToString();
                                Personid = worksheet1.Rows[fila].Cells[3].Value.ToString();

                                for (int c = colInicio; c < colFinal; c++)
                                {
                                    serviceComponentFields = new ServiceComponentFieldsList();

                                    serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                    serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                    _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                    serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                    serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                    serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                    _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                    serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                    // Agregar a mi lista
                                    _serviceComponentFieldsList.Add(serviceComponentFields);
                                }

                                fila += 1;
                                var result = _serviceBL.AddServiceComponentValues(ref objOperationResult,
                                                              _serviceComponentFieldsList,
                                                              Globals.ClientSession.GetAsList(),
                                                              Personid,
                                                              ServiceComponentId);

                                //lIMPIAR LA LISTA DE DXS
                                ListaDxByComponent = new List<DiagnosticRepositoryList>();
                                //Elminar los Dx antiguos
                                _serviceBL.EliminarDxAniguosPorComponente(ServiceId, Constants.OFTALMOLOGIA_ID, Globals.ClientSession.GetAsList());

                                for (int i = filaDxOftalmologia; i < tope + 4; i++)
                                {
                                    if (worksheet1.Rows[filaDxOftalmologia].Cells[43].Value.ToString() != "0")
                                    {
                                        DiagnosticRepositoryList DxByComponent = new DiagnosticRepositoryList();

                                        List<RecomendationList> Recomendations = new List<RecomendationList>();
                                        List<RestrictionList> Restrictions = new List<RestrictionList>();


                                        DxByComponent.i_AutoManualId = 1;
                                        DxByComponent.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                                        DxByComponent.i_PreQualificationId = 1;
                                        string DiseasesId = worksheet1.Rows[filaDxOftalmologia].Cells[42].Value.ToString();


                                        DxByComponent.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                                        DxByComponent.i_RecordType = (int)RecordType.Temporal;
                                        DxByComponent.i_RecordStatus = (int)RecordStatus.Agregado;
                                        DxByComponent.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                                        DxByComponent.d_ExpirationDateDiagnostic = DateTime.Now;



                                        DxByComponent.v_ComponentId = Constants.OFTALMOLOGIA_ID;
                                        DxByComponent.v_DiseasesId = DiseasesId;
                                        DxByComponent.v_ServiceId = ServiceId;
                                        string DxFrecuente = worksheet1.Rows[filaDxOftalmologia].Cells[43].Value.ToString();

                                        //Obtener las recomendaciones y las restricciones por medio del DxFrecuenteId


                                        DxByComponent.Recomendations = oMedicalExamFieldValuesBL.ObtenerListaRecomendacionesFrecuentes(DxFrecuente, ServiceId, ComponentId);

                                        ListaDxByComponent.Add(DxByComponent);
                                    }

                                    filaDxOftalmologia += 1;
                                }

                                _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                     ListaDxByComponent,
                                                     serviceComponentDto,
                                                     Globals.ClientSession.GetAsList(),
                                                     true, null);

                            }





                        }
                        #endregion

                        #region TRIAJE
                        if (x.Value1 == "TRIAJE")
                        {
                            while (worksheet1.Rows[fila].Cells[0].Value != null)
                            {
                                colInicio = 58;
                                colFinal = 70;
                                tope = fila;
                                //Cargar Entidad Service Component
                                serviceComponentDto = new servicecomponentDto();
                                serviceComponentDto.v_ServiceComponentId = worksheet1.Rows[fila].Cells[1].Value.ToString();
                                serviceComponentDto.v_Comment = "";
                                serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Evaluado;
                                serviceComponentDto.i_ExternalInternalId = (int)ComponenteProcedencia.Interno;
                                serviceComponentDto.i_IsApprovedId = (int)SiNo.NO;
                                string ComponentId = worksheet1.Rows[fila].Cells[2].Value.ToString();
                                serviceComponentDto.v_ComponentId = ComponentId;
                                string ServiceId = worksheet1.Rows[fila].Cells[0].Value.ToString();
                                serviceComponentDto.v_ServiceId = ServiceId;

                                ServiceComponentId = worksheet1.Rows[fila].Cells[1].Value.ToString();
                                Personid = worksheet1.Rows[fila].Cells[3].Value.ToString();

                                //Actualizar Datos de Personas
                                personDto opersonDto = new personDto();
                                opersonDto.v_DocNumber = worksheet1.Rows[fila].Cells[5].Value.ToString();
                                opersonDto.d_Birthdate = DateTime.Parse(worksheet1.Rows[fila].Cells[75].Value.ToString());
                                opersonDto.v_AdressLocation = worksheet1.Rows[fila].Cells[76].Value.ToString();
                                opersonDto.i_DepartmentId = int.Parse(worksheet1.Rows[fila].Cells[77].Value.ToString());
                                opersonDto.i_ProvinceId = int.Parse(worksheet1.Rows[fila].Cells[78].Value.ToString());
                                opersonDto.i_DistrictId = int.Parse(worksheet1.Rows[fila].Cells[79].Value.ToString());
                                opersonDto.i_TypeOfInsuranceId = int.Parse(worksheet1.Rows[fila].Cells[80].Value.ToString());
                                opersonDto.i_MaritalStatusId = int.Parse(worksheet1.Rows[fila].Cells[81].Value.ToString());
                                opersonDto.i_LevelOfId = int.Parse(worksheet1.Rows[fila].Cells[82].Value.ToString());
                                opersonDto.v_TelephoneNumber = worksheet1.Rows[fila].Cells[83].Value.ToString();
                                opersonDto.i_NumberLiveChildren = int.Parse(worksheet1.Rows[fila].Cells[84].Value.ToString());
                                opersonDto.i_SexTypeId = int.Parse(worksheet1.Rows[fila].Cells[85].Value.ToString());
                                opersonDto.v_CurrentOccupation = worksheet1.Rows[fila].Cells[86].Value.ToString();

                                oPacientBL.UpdatePacient(ref objOperationResult, opersonDto, Globals.ClientSession.GetAsList(), opersonDto.v_DocNumber, opersonDto.v_DocNumber);
                                

                                for (int c = colInicio; c < colFinal; c++)
                                {
                                    serviceComponentFields = new ServiceComponentFieldsList();

                                    serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                    serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                    _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                    serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                    serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                    serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                    _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                    serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                    // Agregar a mi lista
                                    _serviceComponentFieldsList.Add(serviceComponentFields);

                                }
                                //lIMPIAR LA LISTA DE DXS
                                ListaDxByComponent = new List<DiagnosticRepositoryList>();
                                if (worksheet1.Rows[fila].Cells[73].Value.ToString() != "0")
                                {
                                    DiagnosticRepositoryList DxByComponent = new DiagnosticRepositoryList();

                                    List<RecomendationList> Recomendations = new List<RecomendationList>();
                                    List<RestrictionList> Restrictions = new List<RestrictionList>();


                                    DxByComponent.i_AutoManualId = 1;
                                    DxByComponent.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                                    DxByComponent.i_PreQualificationId = 1;
                                    DxByComponent.v_ComponentFieldsId = worksheet1.Rows[0].Cells[73].Value.ToString();
                                    //Obtener el Componente que está amarrado al DX
                                    string ComponentDx = oMedicalExamFieldValuesBL.ObtenerComponentDx(worksheet1.Rows[0].Cells[73].Value.ToString());
                                    string DiseasesId = worksheet1.Rows[fila].Cells[73].Value.ToString();
                                    string ComponentFieldId = worksheet1.Rows[0].Cells[73].Value.ToString();

                                    DiagnosticRepositoryList oDiagnosticRepositoryListOld = _serviceBL.VerificarDxExistente(ServiceId, DiseasesId, ComponentDx, ComponentFieldId);
                                    if (oDiagnosticRepositoryListOld != null)
                                    {
                                        oDiagnosticRepositoryListOld.v_DiagnosticRepositoryId = oDiagnosticRepositoryListOld.v_DiagnosticRepositoryId;
                                        oDiagnosticRepositoryListOld.i_RecordType = (int)RecordType.NoTemporal;
                                        oDiagnosticRepositoryListOld.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
                                        oDiagnosticRepositoryListOld.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                                        ListaDxByComponent.Add(oDiagnosticRepositoryListOld);
                                    }

                                    DxByComponent.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                                    DxByComponent.i_RecordType = (int)RecordType.Temporal;
                                    DxByComponent.i_RecordStatus = (int)RecordStatus.Agregado;
                                    DxByComponent.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;




                                    DxByComponent.d_ExpirationDateDiagnostic = DateTime.Now;

                                    string ComponentFieldValuesId = oMedicalExamFieldValuesBL.ObtenerIdComponentFieldValues(ComponentFieldId, DiseasesId);
                                    DxByComponent.v_ComponentFieldValuesId = ComponentFieldValuesId;


                                    DxByComponent.v_ComponentId = ComponentDx;
                                    DxByComponent.v_DiseasesId = DiseasesId;
                                    DxByComponent.v_ServiceId = ServiceId;


                                    //Obtener las recomendaciones

                                    DxByComponent.Recomendations = oMedicalExamFieldValuesBL.ObtenerListaRecomendaciones(ComponentFieldValuesId, ServiceId, ComponentId);

                                    ListaDxByComponent.Add(DxByComponent);

                                }

                                fila += 10;


                                var result = _serviceBL.AddServiceComponentValues(ref objOperationResult,
                                                                     _serviceComponentFieldsList,
                                                                     Globals.ClientSession.GetAsList(),
                                                                     Personid,
                                                                     ServiceComponentId);



                                _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                            ListaDxByComponent,
                                                            serviceComponentDto,
                                                            Globals.ClientSession.GetAsList(),
                                                            true, false);





                                #region Antedecentes Ocupacionales

                                //Eliminar todo lo que es Historia del Paciente
                                HistoryBL oHistoryBL = new HistoryBL();
                                oHistoryBL.EliminarHystoriaPaciente(Personid);


                                for (int i = filaAntecedentes; i < tope + 10; i++)
                                {

                                    #region Ocupacionales


                                    if (worksheet1.Rows[filaAntecedentes].Cells[36].Value != null)
                                    {

                                        //HISTORy
                                        historyDto ohistoryDto = new historyDto();

                                        WorkstationDangersList objWorkstationDangers;
                                        List<WorkstationDangersList> _TempWorkstationDangersList = new List<WorkstationDangersList>();

                                        TypeOfEEPList objTypeOfEEP;
                                        List<TypeOfEEPList> _TempTypeOfEEPList = new List<TypeOfEEPList>();



                                        ohistoryDto.v_PersonId = Personid;
                                        ohistoryDto.d_StartDate = DateTime.Parse(worksheet1.Rows[filaAntecedentes].Cells[36].Value.ToString());
                                        ohistoryDto.d_EndDate = DateTime.Parse(worksheet1.Rows[filaAntecedentes].Cells[37].Value.ToString());
                                        ohistoryDto.v_Organization = worksheet1.Rows[filaAntecedentes].Cells[38].Value.ToString();
                                        ohistoryDto.v_TypeActivity = worksheet1.Rows[filaAntecedentes].Cells[39].Value.ToString();
                                        ohistoryDto.v_workstation = worksheet1.Rows[filaAntecedentes].Cells[40].Value.ToString();
                                        ohistoryDto.i_TrabajoActual = worksheet1.Rows[filaAntecedentes].Cells[41].Value.ToString() == "SI" ? 1 : 0;
                                        ohistoryDto.i_GeografixcaHeight = worksheet1.Rows[filaAntecedentes].Cells[42].Value.ToString() == "---" ? 0 : int.Parse(worksheet1.Rows[filaAntecedentes].Cells[42].Value.ToString());
                                        int TipoOperacionId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[43].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[43].Value.ToString().Length - 1, 1));
                                        ohistoryDto.i_TypeOperationId = TipoOperacionId;




                                        //Peligro en el Puesto 1
                                        if (worksheet1.Rows[filaAntecedentes].Cells[44].Value != null)
                                        {
                                            objWorkstationDangers = new WorkstationDangersList();
                                            int PeligroPuestoId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[44].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[44].Value.ToString().Length - 2, 2));
                                            objWorkstationDangers.i_DangerId = PeligroPuestoId;
                                            objWorkstationDangers.i_RecordStatus = (int)RecordStatus.Agregado;
                                            objWorkstationDangers.i_RecordType = (int)RecordType.Temporal;
                                            _TempWorkstationDangersList.Add(objWorkstationDangers);
                                        }

                                        //Peligro en el Puesto 2
                                        if (worksheet1.Rows[filaAntecedentes].Cells[45].Value != null)
                                        {
                                            objWorkstationDangers = new WorkstationDangersList();
                                            int PeligroPuestoId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[45].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[45].Value.ToString().Length - 2, 2));
                                            objWorkstationDangers.i_DangerId = PeligroPuestoId;
                                            objWorkstationDangers.i_RecordStatus = (int)RecordStatus.Agregado;
                                            objWorkstationDangers.i_RecordType = (int)RecordType.Temporal;
                                            _TempWorkstationDangersList.Add(objWorkstationDangers);
                                        }

                                        //Peligro en el Puesto 3
                                        if (worksheet1.Rows[filaAntecedentes].Cells[46].Value != null)
                                        {
                                            objWorkstationDangers = new WorkstationDangersList();
                                            int PeligroPuestoId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[46].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[46].Value.ToString().Length - 2, 2));
                                            objWorkstationDangers.i_DangerId = PeligroPuestoId;
                                            objWorkstationDangers.i_RecordStatus = (int)RecordStatus.Agregado;
                                            objWorkstationDangers.i_RecordType = (int)RecordType.Temporal;
                                            _TempWorkstationDangersList.Add(objWorkstationDangers);
                                        }

                                        //Peligro en el Puesto 4
                                        if (worksheet1.Rows[filaAntecedentes].Cells[47].Value != null)
                                        {
                                            objWorkstationDangers = new WorkstationDangersList();
                                            int PeligroPuestoId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[47].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[47].Value.ToString().Length - 2, 2));
                                            objWorkstationDangers.i_DangerId = PeligroPuestoId;
                                            objWorkstationDangers.i_RecordStatus = (int)RecordStatus.Agregado;
                                            objWorkstationDangers.i_RecordType = (int)RecordType.Temporal;
                                            _TempWorkstationDangersList.Add(objWorkstationDangers);
                                        }

                                        //Peligro en el Puesto 5
                                        if (worksheet1.Rows[filaAntecedentes].Cells[48].Value != null)
                                        {
                                            objWorkstationDangers = new WorkstationDangersList();
                                            int PeligroPuestoId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[48].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[48].Value.ToString().Length - 2, 2));
                                            objWorkstationDangers.i_DangerId = PeligroPuestoId;
                                            objWorkstationDangers.i_RecordStatus = (int)RecordStatus.Agregado;
                                            objWorkstationDangers.i_RecordType = (int)RecordType.Temporal;
                                            _TempWorkstationDangersList.Add(objWorkstationDangers);
                                        }

                                        //Peligro en el Puesto 6
                                        if (worksheet1.Rows[filaAntecedentes].Cells[49].Value != null)
                                        {
                                            objWorkstationDangers = new WorkstationDangersList();
                                            int PeligroPuestoId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[49].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[49].Value.ToString().Length - 2, 2));
                                            objWorkstationDangers.i_DangerId = PeligroPuestoId;
                                            objWorkstationDangers.i_RecordStatus = (int)RecordStatus.Agregado;
                                            objWorkstationDangers.i_RecordType = (int)RecordType.Temporal;
                                            _TempWorkstationDangersList.Add(objWorkstationDangers);
                                        }

                                        //Peligro en el Puesto 7
                                        if (worksheet1.Rows[filaAntecedentes].Cells[50].Value != null)
                                        {
                                            objWorkstationDangers = new WorkstationDangersList();
                                            int PeligroPuestoId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[50].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[50].Value.ToString().Length - 2, 2));
                                            objWorkstationDangers.i_DangerId = PeligroPuestoId;
                                            objWorkstationDangers.i_RecordStatus = (int)RecordStatus.Agregado;
                                            objWorkstationDangers.i_RecordType = (int)RecordType.Temporal;
                                            _TempWorkstationDangersList.Add(objWorkstationDangers);
                                        }


                                        //EPP 1
                                        if (worksheet1.Rows[filaAntecedentes].Cells[51].Value != null)
                                        {
                                            objTypeOfEEP = new TypeOfEEPList();
                                            int TypeEppId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[51].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[51].Value.ToString().Length - 2, 2));
                                            objTypeOfEEP.i_TypeofEEPId = TypeEppId;
                                            objTypeOfEEP.i_RecordStatus = (int)RecordStatus.Agregado;
                                            objTypeOfEEP.i_RecordType = (int)RecordType.Temporal;
                                            _TempTypeOfEEPList.Add(objTypeOfEEP);
                                        }

                                        //EPP 2
                                        if (worksheet1.Rows[filaAntecedentes].Cells[52].Value != null)
                                        {
                                            objTypeOfEEP = new TypeOfEEPList();
                                            int TypeEppId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[52].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[52].Value.ToString().Length - 2, 2));
                                            objTypeOfEEP.i_TypeofEEPId = TypeEppId;
                                            objTypeOfEEP.i_RecordStatus = (int)RecordStatus.Agregado;
                                            objTypeOfEEP.i_RecordType = (int)RecordType.Temporal;
                                            _TempTypeOfEEPList.Add(objTypeOfEEP);
                                        }
                                        //EPP 3
                                        if (worksheet1.Rows[filaAntecedentes].Cells[53].Value != null)
                                        {
                                            objTypeOfEEP = new TypeOfEEPList();
                                            int TypeEppId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[53].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[53].Value.ToString().Length - 2, 2));
                                            objTypeOfEEP.i_TypeofEEPId = TypeEppId;
                                            objTypeOfEEP.i_RecordStatus = (int)RecordStatus.Agregado;
                                            objTypeOfEEP.i_RecordType = (int)RecordType.Temporal;
                                            _TempTypeOfEEPList.Add(objTypeOfEEP);
                                        }

                                        //EPP 4
                                        if (worksheet1.Rows[filaAntecedentes].Cells[54].Value != null)
                                        {
                                            objTypeOfEEP = new TypeOfEEPList();
                                            int TypeEppId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[54].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[54].Value.ToString().Length - 2, 2));
                                            objTypeOfEEP.i_TypeofEEPId = TypeEppId;
                                            objTypeOfEEP.i_RecordStatus = (int)RecordStatus.Agregado;
                                            objTypeOfEEP.i_RecordType = (int)RecordType.Temporal;
                                            _TempTypeOfEEPList.Add(objTypeOfEEP);
                                        }

                                        //EPP 5
                                        if (worksheet1.Rows[filaAntecedentes].Cells[55].Value != null)
                                        {
                                            objTypeOfEEP = new TypeOfEEPList();
                                            int TypeEppId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[55].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[55].Value.ToString().Length - 2, 2));
                                            objTypeOfEEP.i_TypeofEEPId = TypeEppId;
                                            objTypeOfEEP.i_RecordStatus = (int)RecordStatus.Agregado;
                                            objTypeOfEEP.i_RecordType = (int)RecordType.Temporal;
                                            _TempTypeOfEEPList.Add(objTypeOfEEP);
                                        }

                                        oHistoryBL.AddHistory(ref objOperationResult, _TempWorkstationDangersList, _TempTypeOfEEPList, ohistoryDto, Globals.ClientSession.GetAsList());

                                    }
                                    #endregion

                                    #region Personales
                                    List<personmedicalhistoryDto> _personmedicalhistoryDto = new List<personmedicalhistoryDto>();
                                    personmedicalhistoryDto personmedicalhistoryDtoDto = new personmedicalhistoryDto();


                                    if (worksheet1.Rows[filaAntecedentes].Cells[9].Value != null)
                                    {
                                        int TipoDxId = int.Parse(worksheet1.Rows[filaAntecedentes].Cells[11].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[11].Value.ToString().Length - 1, 1));
                                        personmedicalhistoryDtoDto.i_TypeDiagnosticId = TipoDxId;
                                        personmedicalhistoryDtoDto.v_PersonId = Personid;
                                        string PersonalDiseasesId = worksheet1.Rows[filaAntecedentes].Cells[9].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[9].Value.ToString().Length - 16, 16);
                                        personmedicalhistoryDtoDto.v_DiseasesId = PersonalDiseasesId;
                                        personmedicalhistoryDtoDto.d_StartDate =  DateTime.Parse(worksheet1.Rows[filaAntecedentes].Cells[10].Value.ToString());
                                        personmedicalhistoryDtoDto.v_DiagnosticDetail = worksheet1.Rows[filaAntecedentes].Cells[12].Value.ToString();
                                        personmedicalhistoryDtoDto.v_TreatmentSite = worksheet1.Rows[filaAntecedentes].Cells[13].Value.ToString();
                                        personmedicalhistoryDtoDto.i_AnswerId = (int)SiNo.SI;

                                        _personmedicalhistoryDto.Add(personmedicalhistoryDtoDto);

                                        oHistoryBL.AddPersonMedicalHistory(ref objOperationResult,
                                                  _personmedicalhistoryDto,
                                                  null,
                                                  null,
                                                  Globals.ClientSession.GetAsList());

                                    }
                                    #endregion

                                    #region Hábitos Noxivos
                                    List<noxioushabitsDto> _noxioushabitsDto = new List<noxioushabitsDto>();
                                    noxioushabitsDto noxioushabitsDto = new noxioushabitsDto();


                                    if (worksheet1.Rows[filaAntecedentes].Cells[14].Value != null)
                                    {
                                        int TipoHabitoId = -1;
                                        if (worksheet1.Rows[filaAntecedentes].Cells[14].Value.ToString().Trim() == "TABAQUISMO")
                                        {
                                            TipoHabitoId = 1;
                                        }
                                        else if (worksheet1.Rows[filaAntecedentes].Cells[14].Value.ToString().Trim() == "CONSUMO DE ALCOHOL")
                                        {
                                            TipoHabitoId = 2;
                                        }
                                        else if (worksheet1.Rows[filaAntecedentes].Cells[14].Value.ToString().Trim() == "CONSUMO DE DROGAS")
                                        {
                                            TipoHabitoId = 3;
                                        }
                                        else if (worksheet1.Rows[filaAntecedentes].Cells[14].Value.ToString().Trim() == "ACTIVIDAD FÍSICA")
                                        {
                                            TipoHabitoId = 4;
                                        }
                                        noxioushabitsDto.i_TypeHabitsId = TipoHabitoId;

                                        noxioushabitsDto.v_Frequency = worksheet1.Rows[filaAntecedentes].Cells[15].Value.ToString();
                                        noxioushabitsDto.v_Comment = worksheet1.Rows[filaAntecedentes].Cells[16].Value.ToString();
                                        noxioushabitsDto.v_PersonId = Personid;

                                        _noxioushabitsDto.Add(noxioushabitsDto);
                                        oHistoryBL.AddNoxiousHabits(ref objOperationResult,
                                                       _noxioushabitsDto,
                                                       null,
                                                       null,
                                                       Globals.ClientSession.GetAsList());

                                    }
                                    #endregion

                                    #region Antecedentes Familiares

                                    List<familymedicalantecedentsDto> _familymedicalantecedentsDto;
                                    familymedicalantecedentsDto familymedicalantecedentsDto = new familymedicalantecedentsDto();

                                    //Padre
                                    if (worksheet1.Rows[filaAntecedentes].Cells[18].Value != null)
                                    {
                                        _familymedicalantecedentsDto = new List<familymedicalantecedentsDto>();
                                        familymedicalantecedentsDto.i_TypeFamilyId = 53;//Padre
                                        familymedicalantecedentsDto.v_PersonId = Personid;
                                        string FamiliarDiseasesId = worksheet1.Rows[filaAntecedentes].Cells[18].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[18].Value.ToString().Length - 16, 16);

                                        familymedicalantecedentsDto.v_DiseasesId = FamiliarDiseasesId;
                                        familymedicalantecedentsDto.v_Comment = worksheet1.Rows[filaAntecedentes].Cells[19].Value.ToString();

                                        _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);


                                        oHistoryBL.AddFamilyMedicalAntecedents(ref objOperationResult,
                                                                                 _familymedicalantecedentsDto,
                                                                                 null,
                                                                                 null,
                                                                                 Globals.ClientSession.GetAsList());
                                    }

                                    //Madre
                                    if (worksheet1.Rows[filaAntecedentes].Cells[20].Value != null)
                                    {
                                        _familymedicalantecedentsDto = new List<familymedicalantecedentsDto>();
                                        familymedicalantecedentsDto.i_TypeFamilyId = 43;//Padre
                                        familymedicalantecedentsDto.v_PersonId = Personid;
                                        string FamiliarDiseasesId = worksheet1.Rows[filaAntecedentes].Cells[20].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[20].Value.ToString().Length - 16, 16);

                                        familymedicalantecedentsDto.v_DiseasesId = FamiliarDiseasesId;
                                        familymedicalantecedentsDto.v_Comment = worksheet1.Rows[filaAntecedentes].Cells[21].Value.ToString();

                                        _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);


                                        oHistoryBL.AddFamilyMedicalAntecedents(ref objOperationResult,
                                                                                 _familymedicalantecedentsDto,
                                                                                 null,
                                                                                 null,
                                                                                 Globals.ClientSession.GetAsList());
                                    }

                                    //Esposos
                                    if (worksheet1.Rows[filaAntecedentes].Cells[22].Value != null)
                                    {
                                        _familymedicalantecedentsDto = new List<familymedicalantecedentsDto>();
                                        familymedicalantecedentsDto.i_TypeFamilyId = 22;//Padre
                                        familymedicalantecedentsDto.v_PersonId = Personid;
                                        string FamiliarDiseasesId = worksheet1.Rows[filaAntecedentes].Cells[22].Value.ToString().Substring(worksheet1.Rows[filaAntecedentes].Cells[22].Value.ToString().Length - 16, 16);

                                        familymedicalantecedentsDto.v_DiseasesId = FamiliarDiseasesId;
                                        familymedicalantecedentsDto.v_Comment = worksheet1.Rows[filaAntecedentes].Cells[23].Value.ToString();

                                        _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);


                                        oHistoryBL.AddFamilyMedicalAntecedents(ref objOperationResult,
                                                                                 _familymedicalantecedentsDto,
                                                                                 null,
                                                                                 null,
                                                                                 Globals.ClientSession.GetAsList());
                                    }

                                    #endregion

                                    filaAntecedentes += 1;
                                }




                                #endregion



                            }


                        }
                        #endregion

                        #region AUDIOMETRÍA
                        if (x.Value1 == "AUDIOMETRÍA")
                        {
                            while (worksheet1.Rows[fila].Cells[0].Value != null)
                            {
                                //Inicializamos columnas del excel
                                colInicio = 57;
                                colFinal = 140;
                                tope = fila;
                                //Cargar Entidad Service Component
                                serviceComponentDto = new servicecomponentDto();
                                serviceComponentDto.v_ServiceComponentId = worksheet1.Rows[fila].Cells[1].Value.ToString();
                                serviceComponentDto.v_Comment = "";
                                serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Evaluado;
                                serviceComponentDto.i_ExternalInternalId = (int)ComponenteProcedencia.Interno;
                                serviceComponentDto.i_IsApprovedId = (int)SiNo.NO;
                                serviceComponentDto.i_ApprovedUpdateUserId = (int)Globals.ClientSession.i_SystemUserId;  // Int32.Parse(ClientSession[2]);
                                string ComponentId = worksheet1.Rows[fila].Cells[2].Value.ToString();
                                serviceComponentDto.v_ComponentId = ComponentId;
                                string ServiceId = worksheet1.Rows[fila].Cells[0].Value.ToString();
                                serviceComponentDto.v_ServiceId = ServiceId;

                                ServiceComponentId = worksheet1.Rows[fila].Cells[1].Value.ToString();
                                Personid = worksheet1.Rows[fila].Cells[3].Value.ToString();

                                for (int c = colInicio; c <= colFinal; c++)
                                {

                                    var Valor = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                    if (Valor != "null")
                                    {
                                    serviceComponentFields = new ServiceComponentFieldsList();

                                    serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                    serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                    _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                    serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                    serviceComponentFieldValues.v_ComponentFieldValuesId = null;


                                    serviceComponentFieldValues.v_Value1 = Valor;

                                    _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);
                                  
                                         
                                    serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                    // Agregar a mi lista
                                    _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }

                                fila += 3;
                         
                                var result = _serviceBL.AddServiceComponentValues(ref objOperationResult,
                                                           _serviceComponentFieldsList,
                                                              Globals.ClientSession.GetAsList(),
                                                              Personid,
                                                              ServiceComponentId);

                                //lIMPIAR LA LISTA DE DXS
                                ListaDxByComponent = new List<DiagnosticRepositoryList>();
                                //Elminar los Dx antiguos
                                _serviceBL.EliminarDxAniguosPorComponente(ServiceId, Constants.AUDIOMETRIA_ID, Globals.ClientSession.GetAsList());

                                for (int i = filaDxAudiometria; i < tope + 3; i++)
                                {
                                    if (worksheet1.Rows[filaDxAudiometria].Cells[145].Value.ToString() != "0")
                                    {
                                        DiagnosticRepositoryList DxByComponent = new DiagnosticRepositoryList();

                                        List<RecomendationList> Recomendations = new List<RecomendationList>();
                                        List<RestrictionList> Restrictions = new List<RestrictionList>();


                                        DxByComponent.i_AutoManualId = 1;
                                        DxByComponent.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                                        DxByComponent.i_PreQualificationId = 1;
                                        string DiseasesId = worksheet1.Rows[filaDxAudiometria].Cells[144].Value.ToString();

                        

                                        DxByComponent.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                                        DxByComponent.i_RecordType = (int)RecordType.Temporal;
                                        DxByComponent.i_RecordStatus = (int)RecordStatus.Agregado;
                                        DxByComponent.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;                         
                                        DxByComponent.d_ExpirationDateDiagnostic = DateTime.Now;



                                        DxByComponent.v_ComponentId = Constants.AUDIOMETRIA_ID;
                                        DxByComponent.v_DiseasesId = DiseasesId;
                                        DxByComponent.v_ServiceId = ServiceId;
                                        string DxFrecuente = worksheet1.Rows[filaDxAudiometria].Cells[145].Value.ToString();

                                        //Obtener las recomendaciones y las restricciones por medio del DxFrecuenteId


                                        DxByComponent.Recomendations = oMedicalExamFieldValuesBL.ObtenerListaRecomendacionesFrecuentes(DxFrecuente, ServiceId, ComponentId);

                                        ListaDxByComponent.Add(DxByComponent);
                                    }

                                    filaDxAudiometria += 1;
                                }


                                _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                     ListaDxByComponent,
                                                     serviceComponentDto,
                                                     Globals.ClientSession.GetAsList(),
                                                     true, false);
                            }
                        }



                        
                        #endregion

                        #region LABORATORIO
                        if (x.Value1 == "LABORATORIO")
                        {
                            while (worksheet1.Rows[fila].Cells[0].Value != null)
                            {
                                //Inicializamos columnas del excel
                                tope = fila;

                                //Cargar Entidad Service Component
                                serviceComponentDto = new servicecomponentDto();
                                serviceComponentDto.v_ServiceComponentId = worksheet1.Rows[fila].Cells[1].Value.ToString();
                                serviceComponentDto.v_Comment = "";
                                serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Evaluado;
                                serviceComponentDto.i_ExternalInternalId = (int)ComponenteProcedencia.Interno;
                                serviceComponentDto.i_IsApprovedId = (int)SiNo.NO;
                                string ComponentId = worksheet1.Rows[fila].Cells[2].Value.ToString();
                                serviceComponentDto.v_ComponentId = ComponentId;
                                string ServiceId = worksheet1.Rows[fila].Cells[0].Value.ToString();
                                serviceComponentDto.v_ServiceId = ServiceId;

                                ServiceComponentId = worksheet1.Rows[fila].Cells[1].Value.ToString();
                                Personid = worksheet1.Rows[fila].Cells[3].Value.ToString();

                                
                                //OBTENER LOS EXAMENES DE LABORATORIO
                                List<ServiceComponentList>  Componentes = _serviceBL.GetServiceComponentByCategoryId(ref objOperationResult, 1, ServiceId);
                                #region EOC
                                var EOC = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[78].Value.ToString());
                                if (EOC != null)
                                {
                                    colInicio = 79;
                                    colFinal = 98;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {

                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);

                                    }
                                }
                                #endregion

                                #region HC
                                var HC = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[98].Value.ToString());
                                if (HC != null)
                                {
                                      colInicio = 99;
                                    colFinal = 111;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }
                                #endregion

                                #region Hemograma
                                var H = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[111].Value.ToString());
                                if (H != null)
                                {
                                    colInicio = 112;
                                    colFinal = 127;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region Glucosa
                                var Glucosa = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[127].Value.ToString());
                                if (Glucosa != null)
                                {
                                    colInicio = 128;
                                    colFinal = 129;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region CTotal
                                var CTotal = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[129].Value.ToString());
                                if (CTotal != null)
                                {
                                    colInicio = 130;
                                    colFinal = 131;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region Trigli
                                var Trigli = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[131].Value.ToString());
                                if (Trigli != null)
                                {
                                    colInicio = 132;
                                    colFinal = 133;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region FacSan
                                var FacSan = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[133].Value.ToString());
                                if (FacSan != null)
                                {
                                    colInicio = 134;
                                    colFinal = 136;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region Creatina
                                var Creatina = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[136].Value.ToString());
                                if (Creatina != null)
                                {
                                    colInicio = 137;
                                    colFinal = 138;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region VDRL
                                var VDRL = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[138].Value.ToString());
                                if (VDRL != null)
                                {
                                    colInicio = 139;
                                    colFinal = 140;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region HA
                                var HA = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[140].Value.ToString());
                                if (HA != null)
                                {
                                    colInicio = 141;
                                    colFinal = 143;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region HEC
                                var HEC = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[143].Value.ToString());
                                if (HEC != null)
                                {
                                    colInicio = 144;
                                    colFinal = 146;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region AntiPros
                                var AntiPros = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[146].Value.ToString());
                                if (AntiPros != null)
                                {
                                    colInicio = 147;
                                    colFinal = 148;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region ParaSimp
                                var ParaSimp = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[148].Value.ToString());
                                if (ParaSimp != null)
                                {
                                    colInicio = 149;
                                    colFinal = 160;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region AgluLam
                                var AgluLam = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[160].Value.ToString());
                                if (AgluLam != null)
                                {
                                    colInicio = 161;
                                    colFinal = 166;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region SubUnidad
                                var SubUnidad = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[166].Value.ToString());
                                if (SubUnidad != null)
                                {
                                    colInicio = 167;
                                    colFinal = 169;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region VIH
                                var VIH = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[169].Value.ToString());
                                if (VIH != null)
                                {
                                    colInicio = 170;
                                    colFinal = 172;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region ParaSeri
                                var ParaSeri = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[172].Value.ToString());
                                if (ParaSeri != null)
                                {
                                    colInicio = 173;
                                    colFinal = 203;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region Coca
                                var Coca = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[203].Value.ToString());
                                if (Coca != null)
                                {
                                    colInicio = 204;
                                    colFinal = 206;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion
                                
                                #region TGP
                                var TGP = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[208].Value.ToString());
                                if (TGP != null)
                                {
                                    colInicio = 209;
                                    colFinal = 210;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region Plomo
                                var Plomo = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[210].Value.ToString());
                                if (Plomo != null)
                                {
                                    colInicio = 211;
                                    colFinal = 212;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region Urea
                                var Urea = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[212].Value.ToString());
                                if (Urea != null)
                                {
                                    colInicio = 213;
                                    colFinal = 214;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region BKDI
                                var BKDI = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[214].Value.ToString());
                                if (BKDI != null)
                                {
                                    colInicio = 215;
                                    colFinal = 218;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region Acido
                                var Acido = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[218].Value.ToString());
                                if (Acido != null)
                                {
                                    colInicio = 219;
                                    colFinal = 220;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region PerfilH
                                var PerfilH = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[220].Value.ToString());
                                if (PerfilH != null)
                                {
                                    colInicio = 221;
                                    colFinal = 231;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                #region PerfilL
                                var PerfilL = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[231].Value.ToString());
                                if (PerfilL != null)
                                {
                                    colInicio = 232;
                                    colFinal = 237;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {
                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);
                                    }
                                }


                                #endregion

                                fila += 5;

                                var result = _serviceBL.AddServiceComponentValues(ref objOperationResult,
                                                                 _serviceComponentFieldsList,
                                                                 Globals.ClientSession.GetAsList(),
                                                                 Personid,
                                                                 ServiceComponentId);

                                //lIMPIAR LA LISTA DE DXS
                                ListaDxByComponent = new List<DiagnosticRepositoryList>();


                                for (int i = filaDxLaboratorio; i < tope + 5; i++)
                                {
                                    if (worksheet1.Rows[filaDxLaboratorio].Cells[243].Value.ToString() != "0")
                                    {
                                        //Obetener Componente.
                                        string IDComponente = worksheet1.Rows[filaDxLaboratorio].Cells[240].Value.ToString();

                                        //Elminar los Dx antiguos

                                        _serviceBL.EliminarDxAniguosPorComponente(ServiceId, IDComponente, Globals.ClientSession.GetAsList());

                                        DiagnosticRepositoryList DxByComponent = new DiagnosticRepositoryList();

                                        List<RecomendationList> Recomendations = new List<RecomendationList>();
                                        List<RestrictionList> Restrictions = new List<RestrictionList>();


                                        DxByComponent.i_AutoManualId = 1;
                                        DxByComponent.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                                        DxByComponent.i_PreQualificationId = 1;
                                        string DiseasesId = worksheet1.Rows[filaDxLaboratorio].Cells[242].Value.ToString();

                                        DxByComponent.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                                        DxByComponent.i_RecordType = (int)RecordType.Temporal;
                                        DxByComponent.i_RecordStatus = (int)RecordStatus.Agregado;
                                        DxByComponent.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                                        DxByComponent.d_ExpirationDateDiagnostic = DateTime.Now;



                                        DxByComponent.v_ComponentId = IDComponente;
                                        DxByComponent.v_DiseasesId = DiseasesId;
                                        DxByComponent.v_ServiceId = ServiceId;
                                        string DxFrecuente = worksheet1.Rows[filaDxLaboratorio].Cells[243].Value.ToString();

                                        //Obtener las recomendaciones y las restricciones por medio del DxFrecuenteId


                                        DxByComponent.Recomendations = oMedicalExamFieldValuesBL.ObtenerListaRecomendacionesFrecuentes(DxFrecuente, ServiceId, IDComponente);

                                        ListaDxByComponent.Add(DxByComponent);
                                    }

                                    filaDxLaboratorio += 1;
                                }


                                _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                     ListaDxByComponent,
                                                     serviceComponentDto,
                                                     Globals.ClientSession.GetAsList(),
                                                     true, false);

                               
                            }
                        }



                        #endregion

                        #region PSICOLOGÍA
                        if (x.Value1 == "PSICOLOGÍA")
                        {
                            while (worksheet1.Rows[fila].Cells[0].Value != null)
                            {
                                //Inicializamos columnas del excel
                                tope = fila;

                                //Cargar Entidad Service Component
                                serviceComponentDto = new servicecomponentDto();
                                serviceComponentDto.v_ServiceComponentId = worksheet1.Rows[fila].Cells[1].Value.ToString();
                                serviceComponentDto.v_Comment = "";
                                serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Evaluado;
                                serviceComponentDto.i_ExternalInternalId = (int)ComponenteProcedencia.Interno;
                                serviceComponentDto.i_IsApprovedId = (int)SiNo.NO;
                                string ComponentId = worksheet1.Rows[fila].Cells[2].Value.ToString();
                                serviceComponentDto.v_ComponentId = ComponentId;
                                string ServiceId = worksheet1.Rows[fila].Cells[0].Value.ToString();
                                serviceComponentDto.v_ServiceId = ServiceId;

                                ServiceComponentId = worksheet1.Rows[fila].Cells[1].Value.ToString();
                                Personid = worksheet1.Rows[fila].Cells[3].Value.ToString();


                                //OBTENER LOS EXAMENES DE LABORATORIO
                                List<ServiceComponentList> Componentes = _serviceBL.GetServiceComponentByCategoryId(ref objOperationResult, 7, ServiceId);
                            
                                #region HISTCLIPSI
                                var HISTCLIPSI = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[34].Value.ToString());
                                if (HISTCLIPSI != null)
                                {
                                    colInicio = 35;
                                    colFinal = 66;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {

                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);

                                    }
                                }
                                #endregion

                                #region INFPSICO
                                var INFPSICO = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[67].Value.ToString());
                                if (INFPSICO != null)
                                {
                                    colInicio = 68;
                                    colFinal = 87;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {

                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);

                                    }
                                }
                                #endregion

                                fila += 4;

                                var result = _serviceBL.AddServiceComponentValues(ref objOperationResult,
                                                                 _serviceComponentFieldsList,
                                                                 Globals.ClientSession.GetAsList(),
                                                                 Personid,
                                                                 ServiceComponentId);

                                //lIMPIAR LA LISTA DE DXS
                                ListaDxByComponent = new List<DiagnosticRepositoryList>();
                                //Elminar los Dx antiguos
                                _serviceBL.EliminarDxAniguosPorComponente(ServiceId, Constants.PSICOLOGIA_ID, Globals.ClientSession.GetAsList());

                                for (int i = filaDxPsicologia; i < tope + 5; i++)
                                {
                                    if (worksheet1.Rows[filaDxPsicologia].Cells[90].Value.ToString() != "0")
                                    {
                                        DiagnosticRepositoryList DxByComponent = new DiagnosticRepositoryList();

                                        List<RecomendationList> Recomendations = new List<RecomendationList>();
                                        List<RestrictionList> Restrictions = new List<RestrictionList>();


                                        DxByComponent.i_AutoManualId = 1;
                                        DxByComponent.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                                        DxByComponent.i_PreQualificationId = 1;
                                        string DiseasesId = worksheet1.Rows[filaDxPsicologia].Cells[89].Value.ToString();



                                        DxByComponent.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                                        DxByComponent.i_RecordType = (int)RecordType.Temporal;
                                        DxByComponent.i_RecordStatus = (int)RecordStatus.Agregado;
                                        DxByComponent.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                                        DxByComponent.d_ExpirationDateDiagnostic = DateTime.Now;



                                        DxByComponent.v_ComponentId = Constants.PSICOLOGIA_ID;
                                        DxByComponent.v_DiseasesId = DiseasesId;
                                        DxByComponent.v_ServiceId = ServiceId;
                                        string DxFrecuente = worksheet1.Rows[filaDxPsicologia].Cells[90].Value.ToString();

                                        //Obtener las recomendaciones y las restricciones por medio del DxFrecuenteId


                                        DxByComponent.Recomendations = oMedicalExamFieldValuesBL.ObtenerListaRecomendacionesFrecuentes(DxFrecuente, ServiceId, ComponentId);

                                        ListaDxByComponent.Add(DxByComponent);
                                    }

                                    filaDxPsicologia += 1;
                                }

                                _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                     ListaDxByComponent,
                                                     serviceComponentDto,
                                                     Globals.ClientSession.GetAsList(),
                                                     true, false);

                            }
                        }




                        #endregion

                        #region MEDICINA
                        if (x.Value1 == "MEDICINA")
                        {
                            while (worksheet1.Rows[fila].Cells[0].Value != null)
                            {
                                //Inicializamos columnas del excel
                                tope = fila;

                                //Cargar Entidad Service Component
                                serviceComponentDto = new servicecomponentDto();
                                serviceComponentDto.v_ServiceComponentId = worksheet1.Rows[fila].Cells[1].Value.ToString();
                                serviceComponentDto.v_Comment = "";
                                serviceComponentDto.i_ServiceComponentStatusId = (int)ServiceComponentStatus.Evaluado;
                                serviceComponentDto.i_ExternalInternalId = (int)ComponenteProcedencia.Interno;
                                serviceComponentDto.i_IsApprovedId = (int)SiNo.NO;
                                string ComponentId = worksheet1.Rows[fila].Cells[2].Value.ToString();
                                serviceComponentDto.v_ComponentId = ComponentId;
                                string ServiceId = worksheet1.Rows[fila].Cells[0].Value.ToString();
                                serviceComponentDto.v_ServiceId = ServiceId;

                                ServiceComponentId = worksheet1.Rows[fila].Cells[1].Value.ToString();
                                Personid = worksheet1.Rows[fila].Cells[3].Value.ToString();


                                //OBTENER LOS EXAMENES DE LABORATORIO
                                List<ServiceComponentList> Componentes = _serviceBL.GetServiceComponentByCategoryId(ref objOperationResult, 11, ServiceId);


                                #region EXAFISICO
                                var EXAFISICO = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[75].Value.ToString());
                                if (EXAFISICO != null)
                                {
                                    colInicio = 76;
                                    colFinal = 118;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {

                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);

                                    }
                                }
                                #endregion

                                #region OSTEOMUSCULAR
                                var OSTEOMUSCULAR = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[118].Value.ToString());
                                if (OSTEOMUSCULAR != null)
                                {
                                    colInicio = 119;
                                    colFinal = 294;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {

                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);

                                    }
                                }
                                #endregion

                                #region EXAALTURA
                                var EXAALTURA = Componentes.Find(p => p.v_ComponentId == worksheet1.Rows[0].Cells[294].Value.ToString());
                                if (EXAALTURA != null)
                                {
                                    colInicio = 295;
                                    colFinal = 331;

                                    for (int c = colInicio; c < colFinal; c++)
                                    {

                                        serviceComponentFields = new ServiceComponentFieldsList();

                                        serviceComponentFields.v_ComponentFieldsId = worksheet1.Rows[0].Cells[c].Value.ToString();
                                        serviceComponentFields.v_ServiceComponentId = ServiceComponentId;

                                        _serviceComponentFieldValuesList = new List<ServiceComponentFieldValuesList>();
                                        serviceComponentFieldValues = new ServiceComponentFieldValuesList();

                                        serviceComponentFieldValues.v_ComponentFieldValuesId = null;
                                        serviceComponentFieldValues.v_Value1 = worksheet1.Rows[fila].Cells[c].Value.ToString();
                                        _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

                                        serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

                                        // Agregar a mi lista
                                        _serviceComponentFieldsList.Add(serviceComponentFields);

                                    }
                                }
                                #endregion


                                fila += 11;

                                //lIMPIAR LA LISTA DE DXS
                                ListaDxByComponent = new List<DiagnosticRepositoryList>();


                                for (int i = filaDxMedicina; i < tope + 11; i++)
                                {
                                    if (worksheet1.Rows[filaDxMedicina].Cells[337].Value.ToString() != "0") //dxfrecuenteID
                                    {
                                        //Obetener Componente
                                        string IDComponente = worksheet1.Rows[filaDxMedicina].Cells[335].Value.ToString();

                                        //Elminar los Dx antiguos

                                        _serviceBL.EliminarDxAniguosPorComponente(ServiceId, IDComponente, Globals.ClientSession.GetAsList());

                                        DiagnosticRepositoryList DxByComponent = new DiagnosticRepositoryList();

                                        List<RecomendationList> Recomendations = new List<RecomendationList>();
                                        List<RestrictionList> Restrictions = new List<RestrictionList>();


                                        DxByComponent.i_AutoManualId = 1;
                                        DxByComponent.i_FinalQualificationId = (int)FinalQualification.Definitivo;
                                        DxByComponent.i_PreQualificationId = 1;
                                        string DiseasesId = worksheet1.Rows[filaDxMedicina].Cells[336].Value.ToString();

                                        DxByComponent.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                                        DxByComponent.i_RecordType = (int)RecordType.Temporal;
                                        DxByComponent.i_RecordStatus = (int)RecordStatus.Agregado;
                                        DxByComponent.i_DiagnosticTypeId = (int)TipoDx.Enfermedad_Comun;
                                        DxByComponent.d_ExpirationDateDiagnostic = DateTime.Now;



                                        DxByComponent.v_ComponentId = IDComponente;
                                        DxByComponent.v_DiseasesId = DiseasesId;
                                        DxByComponent.v_ServiceId = ServiceId;
                                        string DxFrecuente = worksheet1.Rows[filaDxMedicina].Cells[337].Value.ToString();

                                        //Obtener las recomendaciones y las restricciones por medio del DxFrecuenteId


                                        DxByComponent.Recomendations = oMedicalExamFieldValuesBL.ObtenerListaRecomendacionesFrecuentes(DxFrecuente, ServiceId, IDComponente);

                                        ListaDxByComponent.Add(DxByComponent);
                                    }

                                    filaDxMedicina += 1;
                                }

                                var result = _serviceBL.AddServiceComponentValues(ref objOperationResult,
                                                                 _serviceComponentFieldsList,
                                                                 Globals.ClientSession.GetAsList(),
                                                                 Personid,
                                                                 ServiceComponentId);

                                _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                     ListaDxByComponent,
                                                     serviceComponentDto,
                                                     Globals.ClientSession.GetAsList(),
                                                     true, false);

                            }
                        }



                        #endregion


                        MessageBox.Show("La importación fue correcta.", "VALIDACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }



             }








            }
           
        }
    }
}
