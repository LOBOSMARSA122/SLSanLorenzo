using NetPdf;
using Sigesoft.Common;
using Sigesoft.Node.Contasol.Integration;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmManagementReportsMedical : Form
    {

        public List<ProblemasList> problemasList { get; set; }
        public List<PersonList> personalList;

        AtencionIntegralBL atencionIntegralBL = new AtencionIntegralBL();
        ServiceBL _serviceBL = new ServiceBL();
        PacientBL _pacientBL = new PacientBL();
        RecetaBl objRecetaBl = new RecetaBl();
        OperationResult _objOperationResult = new OperationResult();
         List<DiagnosticRepositoryList> _listDiagnosticRepositoryLists;

        private MergeExPDF _mergeExPDF = new MergeExPDF();
        public List<string> _filesNameToMerge = new List<string>();
        private string _serviceId;
        private string _EmpresaClienteId;
        private string _pacientId;
        private string _customerOrganizationName;
        private string _personFullName;
        string ruta;
        private readonly int _edad;
        
        public frmManagementReportsMedical(string serviceId, string pacientId, string customerOrganizationName, string personFullName, string pstrEmpresaCliente, int edad)
        {
            InitializeComponent();
            _serviceId = serviceId;
            _pacientId = pacientId;
            _customerOrganizationName = customerOrganizationName;
            _personFullName = personFullName;           
            _EmpresaClienteId = pstrEmpresaCliente;
            _edad = edad;
        }

        private void frmManagementReportsMedical_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            List<ServiceComponentList> serviceComponents = new List<ServiceComponentList>();
            serviceComponents = _serviceBL.GetServiceComponentsForManagementReport(_serviceId);
            if (_edad <= 12)
            {
                serviceComponents.Add(new ServiceComponentList { Orden = 1, v_ComponentName = "HISTORIA CLÍNICA NIÑO", v_ComponentId = Constants.FORMATO_ATENCION_NINIO });
            }
            else if (13 <= _edad && _edad  <= 17)
            {
                serviceComponents.Add(new ServiceComponentList { Orden = 1, v_ComponentName = "ADOLESCENTE", v_ComponentId = Constants.FORMATO_ATENCION_INTEGRAL_ADOLESCENTE });
            }
            else if (18 <= _edad && _edad <= 64)
            {
                serviceComponents.Add(new ServiceComponentList { Orden = 1, v_ComponentName = "ADULTO", v_ComponentId = Constants.FORMATO_ATENCION_INTEGRAL_ADULTO });
            }
            else
            {
                serviceComponents.Add(new ServiceComponentList { Orden = 1, v_ComponentName = "ADULTO MAYOR", v_ComponentId = Constants.FORMATO_ATENCION_INTEGRAL_ADULTO_MAYOR });
            }

            serviceComponents.Add(new ServiceComponentList { Orden = 1, v_ComponentName = "ATENCIÓN INTEGRAL", v_ComponentId = Constants.ATENCION_INTEGRAL });
            serviceComponents.Add(new ServiceComponentList { Orden = 27, v_ComponentName = "INFORME DE LABORATORIO", v_ComponentId = Constants.INFORME_LABORATORIO_CLINICO });
            OrganizationBL oOrganizationBL = new OrganizationBL();
            List<ServiceComponentList> ListaOrdenada;
            List<ServiceComponentList> ListaFinalOrdena = new List<ServiceComponentList>();
            var ListaOrdenReportes = oOrganizationBL.GetOrdenReportes(ref objOperationResult, _EmpresaClienteId);

            if (ListaOrdenReportes.Count > 0)
            {
                ListaOrdenada = new List<ServiceComponentList>();
                ServiceComponentList oServiceComponentList = null;


                foreach (var item in ListaOrdenReportes)
                {
                    oServiceComponentList = new ServiceComponentList();
                    oServiceComponentList.v_ComponentName = item.v_NombreReporte;
                    oServiceComponentList.v_ComponentId = item.v_ComponenteId + "|" + item.i_NombreCrystalId;
                    ListaOrdenada.Add(oServiceComponentList);
                }

                foreach (var item in ListaOrdenada)
                {
                    var array = item.v_ComponentId.Split('|');
                    foreach (var item1 in serviceComponents)
                    {
                        if (array[0].ToString() == item1.v_ComponentId)
                        {
                            ListaFinalOrdena.Add(item);
                        }
                    }
                }

                chklConsolidadoReportes.DataSource = ListaFinalOrdena;
                chklConsolidadoReportes.DisplayMember = "v_ComponentName";
                chklConsolidadoReportes.ValueMember = "v_ComponentId";
            }
            else
            {
                chklConsolidadoReportes.DataSource = serviceComponents;
                chklConsolidadoReportes.DisplayMember = "v_ComponentName";
                chklConsolidadoReportes.ValueMember = "v_ComponentId";
            }

           
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodos.Checked)
            {
                chklChekedAll(chklConsolidadoReportes, true);
                chklConsolidadoReportes.Enabled = false;
                SelectChangeConsolidadoReportes();
                chkTodos.Text = "Deseleccionar Todos";
            }
            else
            {
                chklConsolidadoReportes.Enabled = true;
                chklChekedAll(chklConsolidadoReportes, false);
                SelectChangeConsolidadoReportes();
                chkTodos.Text = "Seleccionar Todos";
            }
        }

        private void chklChekedAll(CheckedListBox chkl, bool checkedState)
        {
            for (int i = 0; i < chkl.Items.Count; i++)
            {
                chkl.SetItemChecked(i, checkedState);
            }
        }

        private void SelectChangeConsolidadoReportes()
        {
            var s = GetChekedItems(chklConsolidadoReportes);

        }

        private List<string> GetChekedItems(CheckedListBox chkl)
        {
            List<string> componentId = new List<string>();

            for (int i = 0; i < chkl.CheckedItems.Count; i++)
            {
                ServiceComponentList com = (ServiceComponentList)chkl.CheckedItems[i];
                componentId.Add(com.v_ComponentId);
            }

            return componentId.Count == 0 ? null : componentId;
        }

        private void btnConsolidadoReportes_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("¿Desea publicar a la WEB?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            OperationResult objOperationResult = new OperationResult();

            string ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();
            string rutaBasura = Common.Utils.GetApplicationConfigValue("rutaReportesBasura").ToString();
            string rutaConsolidado = Common.Utils.GetApplicationConfigValue("rutaConsolidado").ToString();

            var Reportes = GetChekedItems(chklConsolidadoReportes);
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                CrearReportesCrystal(_serviceId, _pacientId, Reportes, Result == System.Windows.Forms.DialogResult.Yes ? true : false);
            };

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                var x = _filesNameToMerge.ToList();
                _mergeExPDF.FilesName = x;
                _mergeExPDF.DestinationFile = Application.StartupPath + @"\TempMerge\" + _serviceId + ".pdf";
                _mergeExPDF.DestinationFile = ruta + _serviceId + ".pdf"; ;
                _mergeExPDF.Execute();
                _mergeExPDF.RunFile();

                var oService = _serviceBL.GetServiceShort(_serviceId);
                _mergeExPDF.FilesName = x;
                _mergeExPDF.DestinationFile = Application.StartupPath + @"\TempMerge\" + oService.Empresa + " - " + oService.Paciente + " - " + oService.FechaServicio.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                _mergeExPDF.DestinationFile = rutaConsolidado + oService.Empresa + " - " + oService.Paciente + " - " + oService.FechaServicio.Value.ToString("dd MMMM,  yyyy") + ".pdf";
                _mergeExPDF.Execute();


                //Cambiar de estado a generado de reportes
                _serviceBL.UpdateStatusPreLiquidation(ref objOperationResult, 2, _serviceId, Globals.ClientSession.GetAsList());
            }
            else
            {
                var x = _filesNameToMerge.ToList();
                _mergeExPDF.FilesName = x;
                _mergeExPDF.DestinationFile = Application.StartupPath + @"\TempMerge\" + _serviceId + ".pdf"; ;
                _mergeExPDF.DestinationFile = rutaBasura + _serviceId + ".pdf"; ;
                _mergeExPDF.Execute();
                _mergeExPDF.RunFile();
            }

        }

        public void CrearReportesCrystal(string serviceId, string pPacienteId, List<string> reportesId, bool Publicar)
        {
            OperationResult objOperationResult = new OperationResult();
            MultimediaFileBL _multimediaFileBL = new MultimediaFileBL();
            crConsolidatedReports rp = null;
            ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();
            rp = new Reports.crConsolidatedReports();
            _filesNameToMerge = new List<string>();

            foreach (var com in reportesId)
            {
                //string CompnenteId = "";
                int IdCrystal = 0;
                //Obtener el Id del componente 

                var array = com.Split('|');

                if (array.Count() == 1)
                {
                    IdCrystal = 0;
                }
                else if (array[1] == "")
                {
                    IdCrystal = 0;
                }
                else
                {
                    IdCrystal = int.Parse(array[1].ToString());
                }

                ChooseReport(array[0], serviceId, pPacienteId, IdCrystal);

            }
        }

        private void ChooseReport_(string componentId, string serviceId, string pPacienteId, int pintIdCrystal)
        {
            ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();           

            switch (componentId)
            {
                case Constants.FORMATO_ATENCION_INTEGRAL_ADULTO_MAYOR:
                    GenerateAtencionIntegralAdultoMayor(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + Constants.FORMATO_ATENCION_INTEGRAL_ADULTO_MAYOR)));
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + componentId)));
                    break;

                case Constants.FORMATO_ATENCION_INTEGRAL_ADULTO:
                    GenerateAtencionIntegralAdulto(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + Constants.FORMATO_ATENCION_INTEGRAL_ADULTO)));
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + componentId)));
                    break;

                case Constants.FORMATO_ATENCION_INTEGRAL_ADOLESCENTE:
                    GenerateAtencionIntegralAdolescente(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + Constants.FORMATO_ATENCION_INTEGRAL_ADOLESCENTE)));
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + componentId)));
                    break;

                case Constants.FORMATO_ATENCION_NINIO:
                    GenerateConsultaMedicaNinio(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + Constants.FORMATO_ATENCION_NINIO)));
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + componentId)));
                    break;
                case Constants.ATENCION_INTEGRAL:
                    GenerateAtencionIntegral(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + Constants.ATENCION_INTEGRAL)));
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + componentId)));
                    break;
            }   
        }
        private void GenerateTOXICOLOGICO_COCAINA_MARIHUANA_TODOS(string pathFile)
        {
            var _DataService = _serviceBL.GetInformacion_Laboratorio(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Laboratorio, _serviceId);

            TOXICOLOGICO_COCAINA_MARIHUANA_TODOS.CreateTOXICOLOGICO_COCAINA_MARIHUANA_TODOS(_DataService, pathFile, datosP, MedicalCenter, filiationData, serviceComponents, datosGrabo);
        }
        private void GenerateAtencionIntegral(string pathFile)
        {
            //var _DataService = _serviceBL.GetServiceReport(_serviceId);
            // en esta variable va a traer todos los valores de los examenes
            var exams = _serviceBL.GetServiceComponentsReport(_serviceId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            int GrupoEtario = 0;
            int Grupo = 0;
            #region
            if (_edad <= 12)
            {
                GrupoEtario = 4;
                Grupo = 2824;
            }
            else if (13 <= _edad && _edad  <= 17)
            {
                GrupoEtario = 2;
                Grupo = 2822;
            }
            else if (18 <= _edad && _edad <= 64)
            {
                GrupoEtario = 1;
                Grupo = 2821;
            }
            else
            {
                GrupoEtario = 3;
                Grupo = 2823;
            }

            #endregion
            var listAntecedentes = _serviceBL.ObtenerEsoAntecedentesPorGrupoId(Grupo, GrupoEtario, _pacientId);
            var datosNin = _pacientBL.DevolverNinio(_serviceId);
            var datosAdol = _pacientBL.DevolverAdolescente(_serviceId);
            var datosAdul = _pacientBL.DevolverAdulto(_serviceId);
            var listEmb = _pacientBL.GetEmbarazos(_pacientId);
            var datosAdulMay = _pacientBL.DevolverAdultoMayor(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var medico = _pacientBL.ObtenerDatosMedicoMedicina(_serviceId, Constants.ATENCION_INTEGRAL_ID, Constants.EXAMEN_FISICO_7C_ID);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(_serviceId);
            var medicina = objRecetaBl.GetReceta(_serviceId);
            var medicoTratante = new ServiceBL().GetMedicoTratante(_serviceId);
            var datosGrabo = new ServiceBL().DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);


            AtencionIntegral.CreateAtencionIntegral(pathFile, medico, datosP, listAntecedentes, MedicalCenter, exams, datosNin, datosAdol, datosAdul, listEmb, datosAdulMay, diagnosticRepository, medicina, _ExamenesServicio, medicoTratante, datosGrabo);

        }

        private void GenerateAtencionIntegralAdultoMayor(string pathFile)
        {
            var listaProblema = atencionIntegralBL.GetAtencionIntegral(_pacientId);
            var listPlanIntegral = atencionIntegralBL.GetPlanIntegral(_pacientId);
            var datosPersonales = _pacientBL.GetDatosPersonalesAtencion(_serviceId);

            var listEmb = _pacientBL.GetEmbarazos(_pacientId);
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);

            int GrupoEtario = 3;
            int Grupo = 2823;
            var listAntecedentes = _serviceBL.ObtenerEsoAntecedentesPorGrupoId(Grupo, GrupoEtario, _pacientId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            
            //datos adicionales adultomayor
            var datosAdulMay = _pacientBL.DevolverAdultoMayor(_serviceId);

            int GrupoBase = 286;

            List<frmEsoCuidadosPreventivosFechas> Fechas = _serviceBL.ObtenerFechasCuidadosPreventivos(GrupoBase, _pacientId);
            if (Fechas.Count > 6)
                Fechas = Fechas.Skip((Fechas.Count - 6)).ToList();

            List<frmEsoCuidadosPreventivosComentarios> Comentarios = _serviceBL.ObtenerComentariosCuidadosPreventivos(_pacientId);

            AtencionIntegralAdultoMayor.CreateAtencionIntegral(pathFile, listaProblema, listPlanIntegral, datosPersonales, datosP, listAntecedentes, Fechas, MedicalCenter,datosAdulMay,listEmb, Comentarios);
        }

        private void GenerateAtencionIntegralAdulto(string pathFile)
        {
            var listaProblema = atencionIntegralBL.GetAtencionIntegral(_pacientId);
            var listPlanIntegral = atencionIntegralBL.GetPlanIntegral(_pacientId);
            var datosPersonales = _pacientBL.GetDatosPersonalesAtencion(_serviceId);

            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);

            var listEmb = _pacientBL.GetEmbarazos(_pacientId);

            //datos adicionales adulto
            var datosAdul = _pacientBL.DevolverAdulto(_serviceId);

            int GrupoEtario = 1;
            int Grupo = 2821;
            var listAntecedentes = _serviceBL.ObtenerEsoAntecedentesPorGrupoId(Grupo, GrupoEtario, _pacientId);


            int GrupoBase = 284;

            if (datosPersonales.Genero == "FEMENINO")
            {
                GrupoBase = 283;
                
            }

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            List<frmEsoCuidadosPreventivosFechas> Fechas = _serviceBL.ObtenerFechasCuidadosPreventivos(GrupoBase, _pacientId);
            if (Fechas.Count > 6)
                Fechas = Fechas.Skip((Fechas.Count - 6)).ToList();

            List<frmEsoCuidadosPreventivosComentarios> Comentarios = _serviceBL.ObtenerComentariosCuidadosPreventivos(_pacientId);

            var datosPaciente = _pacientBL.GetPacientReport(_pacientId);

            AtencionIntegralAdulto.CreateAtencionIntegral(pathFile, listaProblema, listPlanIntegral, datosPersonales, datosP, listAntecedentes, Fechas, MedicalCenter,datosAdul,listEmb, Comentarios);
        }

        private void GenerateAtencionIntegralAdolescente(string pathFile)
        {
            var listaProblema = atencionIntegralBL.GetAtencionIntegral(_pacientId);
            var listPlanIntegral = atencionIntegralBL.GetPlanIntegral(_pacientId);
            var datosPaciente = _pacientBL.GetDatosPersonalesAtencion(_serviceId);

            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);

            //datos adicionales adolescente
            var datosAdol = _pacientBL.DevolverAdolescente(_serviceId);

            int GrupoEtario = 2;
            int Grupo = 2822;
            var listAntecedentes = _serviceBL.ObtenerEsoAntecedentesPorGrupoId(Grupo, GrupoEtario, _pacientId);

            int GrupoBase = 285;
            if (datosPaciente.Genero.ToUpper() == "MUJER")
                GrupoBase = 283;
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            List<frmEsoCuidadosPreventivosFechas> Fechas = _serviceBL.ObtenerFechasCuidadosPreventivos(GrupoBase, _pacientId);
            if (Fechas.Count > 6)
                Fechas = Fechas.Skip((Fechas.Count - 6)).ToList();

            List<frmEsoCuidadosPreventivosComentarios> Comentarios = _serviceBL.ObtenerComentariosCuidadosPreventivos(_pacientId);

            SAtencionIntegralAdolescente.CreateAtencionIntegral(pathFile, listaProblema, listPlanIntegral, datosPaciente, datosP, listAntecedentes, Fechas,MedicalCenter,datosAdol, Comentarios);
        }

        private void GenerateConsultaMedicaNinio(string pathFile)
        {
            var listaProblema = atencionIntegralBL.GetAtencionIntegral(_pacientId);
            var listPlanIntegral = atencionIntegralBL.GetPlanIntegral(_pacientId);
            var datosPaciente = _pacientBL.GetDatosPersonalesAtencion(_serviceId);

            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            int GrupoEtario = 4;
            int Grupo = 2824;
            var listAntecedentes = _serviceBL.ObtenerEsoAntecedentesPorGrupoId(Grupo, GrupoEtario, _pacientId);

            //datos adicionales ninio
            var datosNin = _pacientBL.DevolverNinio(_serviceId);

            int GrupoBase = 292;
            //if (datosPaciente.Genero.ToUpper() == "MUJER")
            //    GrupoBase = 283;

            List<frmEsoCuidadosPreventivosFechas> Fechas = _serviceBL.ObtenerFechasCuidadosPreventivos(GrupoBase, _pacientId);
            if (Fechas.Count > 6)
                Fechas = Fechas.Skip((Fechas.Count - 6)).ToList();

            List<frmEsoCuidadosPreventivosComentarios> Comentarios = _serviceBL.ObtenerComentariosCuidadosPreventivos(_pacientId);

            Ninio.CreateAtencionNinio(pathFile, listaProblema, listPlanIntegral, datosPaciente, datosP, listAntecedentes, Fechas, MedicalCenter,datosNin, Comentarios);
        }

        private string _tempSourcePath;
        DataSet dsGetRepo = null;

        private void ChooseReport(string componentId, string serviceId, string pPacienteId, int pintIdCrystal)
        {
            ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();
            _tempSourcePath = Path.Combine(Application.StartupPath, "TempMerge");

            DataSet ds = null;
            DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
            OperationResult objOperationResult = new OperationResult();
            _serviceId = serviceId;
            _pacientId = pPacienteId;
            switch (componentId)
            {
                case Constants.INFORME_CERTIFICADO_APTITUD:
                    var INFORME_CERTIFICADO_APTITUD = new ServiceBL().GetAptitudeCertificate(ref objOperationResult, _serviceId);

                    if (INFORME_CERTIFICADO_APTITUD == null)
                    {
                        break;
                    }
                    DataSet ds1 = new DataSet();

                    DataTable dtINFORME_CERTIFICADO_APTITUD = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(INFORME_CERTIFICADO_APTITUD);

                    dtINFORME_CERTIFICADO_APTITUD.TableName = "AptitudeCertificate";
                    ds1.Tables.Add(dtINFORME_CERTIFICADO_APTITUD);


                    var TipoServicio = INFORME_CERTIFICADO_APTITUD[0].i_EsoTypeId;
                    ReportDocument rp;



                    if (pintIdCrystal == 24)
                    {
                        if (TipoServicio == ((int)TypeESO.Retiro).ToString())
                        {
                            rp = new Reports.crOccupationalRetirosSinFirma();
                            rp.SetDataSource(ds1);

                            string rutaCertificado = Common.Utils.GetApplicationConfigValue("CertificadoRetiro").ToString();
                            //rp = new Reports.crOccupationalRetirosSinFirma();
                            rp.SetDataSource(ds1);
                            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            objDiskOpt = new DiskFileDestinationOptions();
                            objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD[0].v_OrganizationName + "-" + INFORME_CERTIFICADO_APTITUD[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD + "-" + INFORME_CERTIFICADO_APTITUD[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                            rp.ExportOptions.DestinationOptions = objDiskOpt;
                            rp.Export();


                        }
                        else
                        {
                            if (INFORME_CERTIFICADO_APTITUD[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs)
                            {
                                rp = new Reports.crCertficadoObservadoSinFirma();
                                rp.SetDataSource(ds1);


                                string rutaCertificado = Common.Utils.GetApplicationConfigValue("CertificadoObs").ToString();
                                //rp = new Reports.crCertficadoObservadoSinFirma();
                                rp.SetDataSource(ds1);
                                rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                objDiskOpt = new DiskFileDestinationOptions();
                                objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD[0].v_OrganizationName + "-" + INFORME_CERTIFICADO_APTITUD[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD + "-" + INFORME_CERTIFICADO_APTITUD[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                                rp.ExportOptions.DestinationOptions = objDiskOpt;
                                rp.Export();

                            }
                            else
                            {
                                rp = new Reports.crOccupationalCertificateSinFirma();
                                rp.SetDataSource(ds1);

                                string rutaCertificado = Common.Utils.GetApplicationConfigValue("Certificado312").ToString();
                                //rp = new Reports.crOccupationalCertificateSinFirma();
                                rp.SetDataSource(ds1);
                                rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                objDiskOpt = new DiskFileDestinationOptions();
                                objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD[0].v_OrganizationName + "-" + INFORME_CERTIFICADO_APTITUD[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD + "-" + INFORME_CERTIFICADO_APTITUD[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                                rp.ExportOptions.DestinationOptions = objDiskOpt;
                                rp.Export();

                            }
                        }
                    }
                    else if (pintIdCrystal == 23)
                    {
                        rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                        rp.SetDataSource(ds1);
                        string rutaCertificado = Common.Utils.GetApplicationConfigValue("Certificado312").ToString();
                        //rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                        rp.SetDataSource(ds1);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD[0].v_OrganizationName + "-" + INFORME_CERTIFICADO_APTITUD[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD + "-" + INFORME_CERTIFICADO_APTITUD[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                    }
                    else if (pintIdCrystal == 25)
                    {
                        string rutaCertificado = Common.Utils.GetApplicationConfigValue("Certificado312").ToString();
                        rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                        rp.SetDataSource(ds1);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD[0].v_OrganizationName + "-" + INFORME_CERTIFICADO_APTITUD[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD + "-" + INFORME_CERTIFICADO_APTITUD[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();

                    }
                    else if (pintIdCrystal == 26)
                    {
                        rp = new Reports.crOccupationalMedicalAptitudeCertificateSinDx();
                        rp.SetDataSource(ds1);

                        string rutaCertificado = Common.Utils.GetApplicationConfigValue("Certificado312").ToString();
                        //rp = new Reports.crOccupationalMedicalAptitudeCertificateSinDx();
                        rp.SetDataSource(ds1);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD[0].v_OrganizationName + "-" + INFORME_CERTIFICADO_APTITUD[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD + "-" + INFORME_CERTIFICADO_APTITUD[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();

                    }
                    else if (pintIdCrystal == 28)
                    {
                        rp = new Reports.crOccupationaCertificateSinDxSinFirma();
                        rp.SetDataSource(ds1);

                        string rutaCertificado = Common.Utils.GetApplicationConfigValue("Certificado312").ToString();
                        //rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                        rp.SetDataSource(ds1);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD[0].v_OrganizationName + "-" + INFORME_CERTIFICADO_APTITUD[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD + "-" + INFORME_CERTIFICADO_APTITUD[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();

                    }
                    else
                    {
                        if (TipoServicio == ((int)TypeESO.Retiro).ToString())
                        {
                            rp = new Reports.crOccupationalMedicalAptitudeCertificateRetiros();
                            rp.SetDataSource(ds1);

                            string rutaCertificado = Common.Utils.GetApplicationConfigValue("CertificadoRetiro").ToString();
                            //rp = new Reports.crOccupationalMedicalAptitudeCertificateRetiros();
                            rp.SetDataSource(ds1);
                            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            objDiskOpt = new DiskFileDestinationOptions();
                            objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD[0].v_OrganizationName + "-" + INFORME_CERTIFICADO_APTITUD[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD + "-" + INFORME_CERTIFICADO_APTITUD[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                            rp.ExportOptions.DestinationOptions = objDiskOpt;
                            rp.Export();
                        }
                        else
                        {
                            if (INFORME_CERTIFICADO_APTITUD[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs)
                            {
                                rp = new Reports.crCertficadoObservado();
                                rp.SetDataSource(ds1);

                                string rutaCertificado = Common.Utils.GetApplicationConfigValue("CertificadoObs").ToString();
                                //rp = new Reports.crCertficadoObservado();
                                rp.SetDataSource(ds1);
                                rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                objDiskOpt = new DiskFileDestinationOptions();
                                objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD[0].v_OrganizationName + "-" + INFORME_CERTIFICADO_APTITUD[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD + "-" + INFORME_CERTIFICADO_APTITUD[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                                rp.ExportOptions.DestinationOptions = objDiskOpt;
                                rp.Export();
                            }
                            else
                            {
                                rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                                rp.SetDataSource(ds1);

                                string rutaCertificado = Common.Utils.GetApplicationConfigValue("Certificado312").ToString();
                                //rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                                rp.SetDataSource(ds1);
                                rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                objDiskOpt = new DiskFileDestinationOptions();
                                objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD[0].v_OrganizationName + "-" + INFORME_CERTIFICADO_APTITUD[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD + "-" + INFORME_CERTIFICADO_APTITUD[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                                rp.ExportOptions.DestinationOptions = objDiskOpt;
                                rp.Export();
                            }
                        }
                    }

                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_CERTIFICADO_APTITUD + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.INFORME_ANTECEDENTE_PATOLOGICO:
                    var INFORME_ANTECEDENTE_PATOLOGICO = new ServiceBL().GetReportAntecedentePatologico(_pacientId, _serviceId);

                    dsGetRepo = new DataSet();
                    DataTable dtANTECEDENTE_PATOLOGICO_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(INFORME_ANTECEDENTE_PATOLOGICO);
                    dtANTECEDENTE_PATOLOGICO_ID.TableName = "dtFichaAntecedentePatologico";
                    dsGetRepo.Tables.Add(dtANTECEDENTE_PATOLOGICO_ID);

                    rp = new Reports.crFichaAntecedentePatologico01();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_ANTECEDENTE_PATOLOGICO + "01" + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();


                    var CIRUGIAS = new ServiceBL().GetCirugias(_pacientId, _serviceId);

                    dsGetRepo = new DataSet();
                    DataTable dtCirugias = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(CIRUGIAS);
                    dtCirugias.TableName = "dtCirugias";
                    dsGetRepo.Tables.Add(dtCirugias);

                    rp = new Reports.crFichaAntecedentePatologico02();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_ANTECEDENTE_PATOLOGICO + "02" + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.OSTEO_COIMO:
                    var OSTEO_COIMO = new ServiceBL().GetReportOsteoCoimalache(_serviceId, Constants.OSTEO_COIMO);

                    dsGetRepo = new DataSet();
                    DataTable dtOSTEO_COIMO = BLL.Utils.ConvertToDatatable(OSTEO_COIMO);
                    dtOSTEO_COIMO.TableName = "OstioCoimolache";
                    dsGetRepo.Tables.Add(dtOSTEO_COIMO);

                    rp = new crOstioCoimolache();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.OSTEO_COIMO + "01" + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();

                    var UC_OSTEO_COIMA_ID = new ServiceBL().ReporteUCOsteoCoimalache(_serviceId, Constants.OSTEO_COIMO);
                    DataSet dsOsteomuscularCoima = new DataSet();
                    DataTable dt_UC_OSTEO_COIMA_ID = BLL.Utils.ConvertToDatatable(UC_OSTEO_COIMA_ID);
                    dt_UC_OSTEO_COIMA_ID.TableName = "dtUCOsteoMus";
                    dsOsteomuscularCoima.Tables.Add(dt_UC_OSTEO_COIMA_ID);
                    rp = new crFichaEvaluacionMusc_OC();
                    rp.SetDataSource(dsOsteomuscularCoima);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.OSTEO_COIMO + "02" + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;
                case Constants.TOXICOLOGICO_ID:
                    var TOXICOLOGICO_ID = new ServiceBL().GetReportToxicologico(_serviceId, Constants.TOXICOLOGICO_ID);

                    dsGetRepo = new DataSet();
                    DataTable dtTOXICOLOGICO_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(TOXICOLOGICO_ID);
                    dtTOXICOLOGICO_ID.TableName = "dtAutorizacionDosajeDrogas";
                    dsGetRepo.Tables.Add(dtTOXICOLOGICO_ID);

                    rp = new Reports.crAutorizacionDosajeDrogras();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.TOXICOLOGICO_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.INFORME_DECLARACION_CI:
                    var DECLARACION_CI_INFORMADO = new PacientBL().GetReportConsentimiento(_serviceId);

                    dsGetRepo = new DataSet();
                    DataTable dtDECLARACION_CI = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(DECLARACION_CI_INFORMADO);
                    dtDECLARACION_CI.TableName = "dtConsentimiento";
                    dsGetRepo.Tables.Add(dtDECLARACION_CI);
                    if (pintIdCrystal == 51)
                    {
                        rp = new Reports.crDeclaracion_YanaGold();
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_DECLARACION_CI + "03" + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                    }
                    break;

                case Constants.CONSENTIMIENTO_INFORMADO:
                    var CONSENTIMIENTO_INFORMADO = new PacientBL().GetReportConsentimiento(_serviceId);

                    dsGetRepo = new DataSet();
                    DataTable dtCONSENTIMIENTO_INFORMADO = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(CONSENTIMIENTO_INFORMADO);
                    dtCONSENTIMIENTO_INFORMADO.TableName = "dtConsentimiento";
                    dsGetRepo.Tables.Add(dtCONSENTIMIENTO_INFORMADO);


                    if (pintIdCrystal == 50)
                    {
                        rp = new Reports.crConsentimiento_YanaGold();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.CONSENTIMIENTO_INFORMADO + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                    }

                    else
                    {
                        rp = new Reports.crConsentimiento();
                        rp.SetDataSource(dsGetRepo);

                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.CONSENTIMIENTO_INFORMADO + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                    }
                    break;

                case Constants.AUDIOMETRIA_AUDIOMAX_ID:
                    var AUDIOMETRIA_AUDIOMAX_ID = new PacientBL().GetReportConsentimiento(_serviceId);

                    dsGetRepo = new DataSet();
                    DataTable dtAUDIOMETRIA_AUDIOMAX_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(AUDIOMETRIA_AUDIOMAX_ID);
                    dtAUDIOMETRIA_AUDIOMAX_ID.TableName = "dtAudiometriaAudiomax";
                    dsGetRepo.Tables.Add(dtAUDIOMETRIA_AUDIOMAX_ID);
                    rp = new Reports.crAudiometriaAudioMax();
                    rp.SetDataSource(dsGetRepo);

                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.CONSENTIMIENTO_INFORMADO + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.AUDIOMETRIA_AUDIOMAX_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.OSTEO_MUSCULAR_ID_1:
                    DataSet dsOsteomuscularNuevo = new DataSet();
                      var servicesId4 = new List<string>();
                    servicesId4.Add(_serviceId);
                    var componentReportId = new ServiceBL().ObtenerIdsParaImportacionExcel(servicesId4,11);
                    var OSTEO_MUSCULAR_ID_1 = new PacientBL().ReportOsteoMuscularNuevo(_serviceId, componentId, componentReportId[0].ComponentId);


                    //var OSTEO_MUSCULAR_ID_1 = new PacientBL().ReportOsteoMuscularNuevo(_serviceId, Constants.OSTEO_MUSCULAR_ID_1);
                    var UC_OSTEO_ID = new ServiceBL().ReporteOsteomuscular(_serviceId, Constants.OSTEO_MUSCULAR_ID_1);
                    DataTable dt_UC_OSTEO_ID = BLL.Utils.ConvertToDatatable(UC_OSTEO_ID);
                    DataTable dtOSTEO_MUSCULAR_ID_1 = BLL.Utils.ConvertToDatatable(OSTEO_MUSCULAR_ID_1);

                    dtOSTEO_MUSCULAR_ID_1.TableName = "dtOsteomuscularNuevo";
                    dt_UC_OSTEO_ID.TableName = "dtOsteoMus";

                    dsOsteomuscularNuevo.Tables.Add(dtOSTEO_MUSCULAR_ID_1);
                    dsOsteomuscularNuevo.Tables.Add(dt_UC_OSTEO_ID);


                    rp = new Reports.crMuscoloEsqueletico();
                    rp.SetDataSource(dsOsteomuscularNuevo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.OSTEO_MUSCULAR_ID_1 + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();


                    rp = new Reports.crMuscoloEsqueletico2();
                    rp.SetDataSource(dsOsteomuscularNuevo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.OSTEO_MUSCULAR_ID_2 + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.OSTEO_MUSCULAR_ID_2:
                    var OSTEO_MUSCULAR_ID_2 = new PacientBL().GetMusculoEsqueletico(_serviceId, Constants.OSTEO_MUSCULAR_ID_2);

                    dsGetRepo = new DataSet();
                    DataTable dtOSTEO_MUSCULAR_ID_2 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(OSTEO_MUSCULAR_ID_2);
                    dtOSTEO_MUSCULAR_ID_2.TableName = "dtOstomuscular";
                    dsGetRepo.Tables.Add(dtOSTEO_MUSCULAR_ID_2);
                    rp = new Reports.crEvaluacionOsteomuscular();
                    rp.SetDataSource(dsGetRepo);

                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.OSTEO_MUSCULAR_ID_2 + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.OSTEO_MUSCULAR_ID_2 + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL:

                    var Path123 = Application.StartupPath;
                    var INFORME_CERTIFICADO_APTITUD_EMPRESARIAL = new ServiceBL().GetCAPE(_serviceId);
                    dsGetRepo = new DataSet();

                    DataTable dt_INFORME_CERTIFICADO_APTITUD_EMPRESARIAL = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(INFORME_CERTIFICADO_APTITUD_EMPRESARIAL);

                    dt_INFORME_CERTIFICADO_APTITUD_EMPRESARIAL.TableName = "AptitudeCertificate";

                    dsGetRepo.Tables.Add(dt_INFORME_CERTIFICADO_APTITUD_EMPRESARIAL);

                    TipoServicio = INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].i_EsoTypeId;

                    if (pintIdCrystal == 30)
                    {
                        if (TipoServicio == ((int)TypeESO.Retiro).ToString())
                        {
                            rp = new Reports.crOccupationalMedicalAptitudeCertificateRetiros();
                            rp.SetDataSource(dsGetRepo);
                        }
                        else
                        {
                            if (INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs)
                            {
                                rp = new Reports.crCertficadoObservado();
                                //rp.SetDataSource(dsGetRepo);
                                string rutaCertificado = Common.Utils.GetApplicationConfigValue("CertificadoEmp").ToString();
                                //rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                                rp.SetDataSource(dsGetRepo);
                                rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                objDiskOpt = new DiskFileDestinationOptions();
                                objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].EmpresaCliente + "-" + INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL + "-" + INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                                rp.ExportOptions.DestinationOptions = objDiskOpt;
                                rp.Export();
                            }
                            else
                            {
                                rp = new Reports.crCertificadoEmpresarialSinFirma();
                                //rp.SetDataSource(dsGetRepo);
                                string rutaCertificado = Common.Utils.GetApplicationConfigValue("CertificadoEmp").ToString();
                                //rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                                rp.SetDataSource(dsGetRepo);
                                rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                objDiskOpt = new DiskFileDestinationOptions();
                                objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].EmpresaCliente + "-" + INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL + "-" + INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                                rp.ExportOptions.DestinationOptions = objDiskOpt;
                                rp.Export();
                            }
                        }
                    }
                    else
                    {
                        if (TipoServicio == ((int)TypeESO.Retiro).ToString())
                        {
                            rp = new Reports.crOccupationalMedicalAptitudeCertificateRetiros();
                            //rp.SetDataSource(dsGetRepo);
                            string rutaCertificado = Common.Utils.GetApplicationConfigValue("CertificadoEmp").ToString();
                            //rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                            rp.SetDataSource(dsGetRepo);
                            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            objDiskOpt = new DiskFileDestinationOptions();
                            objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].EmpresaCliente + "-" + INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL + "-" + INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                            rp.ExportOptions.DestinationOptions = objDiskOpt;
                            rp.Export();
                        }
                        else
                        {
                            if (INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs)
                            {
                                rp = new Reports.crCertficadoObservado();
                                rp.SetDataSource(dsGetRepo);
                                string rutaCertificado = Common.Utils.GetApplicationConfigValue("CertificadoEmp").ToString();
                                //rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                                rp.SetDataSource(dsGetRepo);
                                rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                objDiskOpt = new DiskFileDestinationOptions();
                                objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].EmpresaCliente + "-" + INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL + "-" + INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                                rp.ExportOptions.DestinationOptions = objDiskOpt;
                                rp.Export();
                            }
                            else
                            {
                                rp = new Reports.crCertificadoDeAptitudEmpresarial();
                                //rp.SetDataSource(dsGetRepo);

                                string rutaCertificado = Common.Utils.GetApplicationConfigValue("CertificadoEmp").ToString();
                                //rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                                rp.SetDataSource(dsGetRepo);
                                rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                objDiskOpt = new DiskFileDestinationOptions();
                                objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].EmpresaCliente + "-" + INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL + "-" + INFORME_CERTIFICADO_APTITUD_EMPRESARIAL[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                                rp.ExportOptions.DestinationOptions = objDiskOpt;
                                rp.Export();

                            }
                        }
                    }

                    //rp = new Reports.crCertificadoDeAptitudEmpresarial();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;

                case Constants.INFORME_HISTORIA_OCUPACIONAL:

                    var INFORME_HISTORIA_OCUPACIONAL = new ServiceBL().ReportHistoriaOcupacional(_serviceId);

                    dsGetRepo = new DataSet();
                    DataTable dtINFORME_HISTORIA_OCUPACIONAL = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(INFORME_HISTORIA_OCUPACIONAL);
                    dtINFORME_HISTORIA_OCUPACIONAL.TableName = "HistoriaOcupacional";
                    dsGetRepo.Tables.Add(dtINFORME_HISTORIA_OCUPACIONAL);


                    if (pintIdCrystal == 37)
                    {
                        rp = new Reports.crApendice01_HistoriaOcupacional();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_HISTORIA_OCUPACIONAL + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;

                        rp.Export();
                        rp.Close();
                    }
                    else if (pintIdCrystal == 52)
                    {

                        rp = new Reports.crFichaMedicaOcupacional_CHO();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_HISTORIA_OCUPACIONAL + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;

                        rp.Export();
                        rp.Close();
                    }
                    else if (pintIdCrystal == 56)
                    {

                        rp = new Reports.crfichaComplementaria_HO();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_HISTORIA_OCUPACIONAL + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;

                        rp.Export();
                        rp.Close();
                    }
                    else if (pintIdCrystal == 57)
                    {

                        rp = new Reports.crAnexo02_HistoriaOcupacional();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_HISTORIA_OCUPACIONAL + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;

                        rp.Export();
                        rp.Close();
                    }
                    else
                    {
                        rp = new Reports.crHistoriaOcupacional();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_HISTORIA_OCUPACIONAL + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();

                    }

                    break;
                case Constants.ALTURA_7D_ID:

                    var servicesId1 = new List<string>();
                    servicesId1.Add(_serviceId);
                    var componentReportId1 = new ServiceBL().ObtenerIdsParaImportacionExcel(servicesId1, 11);

                    var AscensoAlturas = new ServiceBL().ReportAscensoGrandesAlturas(_serviceId, componentId, componentReportId1[0].ComponentId);
                    var FuncionesVitales = new ServiceBL().ReportFuncionesVitales(_serviceId, Constants.FUNCIONES_VITALES_ID);
                    var Antropometria = new ServiceBL().ReportAntropometria(_serviceId, Constants.ANTROPOMETRIA_ID);

                    dsGetRepo = new DataSet("Anexo7D");

                    DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(AscensoAlturas);
                    dt.TableName = "dtAnexo7D";
                    dsGetRepo.Tables.Add(dt);

                    DataTable dt1 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(FuncionesVitales);
                    dt1.TableName = "dtFuncionesVitales";
                    dsGetRepo.Tables.Add(dt1);

                    DataTable dt2 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Antropometria);
                    dt2.TableName = "dtAntropometria";
                    dsGetRepo.Tables.Add(dt2);

                    rp = new Reports.crAnexo7D();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.ALTURA_7D_ID + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.ALTURA_7D_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();

                    break;
                case Constants.ALTURA_ESTRUCTURAL_ID:

                    var servicesId2 = new List<string>();
                    servicesId2.Add(_serviceId);
                    var componentReportId2 = new ServiceBL().ObtenerIdsParaImportacionExcel(servicesId2, 11);
                    var dataListForReport = new PacientBL().GetAlturaEstructural(_serviceId, componentId, componentReportId2[0].ComponentId);

                    //var dataListForReport = new PacientBL().GetAlturaEstructural(_serviceId, Constants.ALTURA_ESTRUCTURAL_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_ALTURA_ESTRUCTURAL_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

                    dt_ALTURA_ESTRUCTURAL_ID.TableName = "dtAlturaEstructural";

                    dsGetRepo.Tables.Add(dt_ALTURA_ESTRUCTURAL_ID);

                    rp = new Reports.crAlturaMayor();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.ALTURA_ESTRUCTURAL_ID + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.ALTURA_ESTRUCTURAL_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.APENDICE_ID:

                    var APENDICE_ID = new ServiceBL().GetReportEstudioElectrocardiografico(_serviceId, Constants.APENDICE_ID);

                    dsGetRepo = new DataSet();

                    DataTable dtAPENDICE_ID = BLL.Utils.ConvertToDatatable(APENDICE_ID);
                    dtAPENDICE_ID.TableName = "dtEstudioElectrocardiografico";
                    dsGetRepo.Tables.Add(dtAPENDICE_ID);
                    if (pintIdCrystal == 43)
                    {
                        rp = new Reports.crApendice05_EKG();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.APENDICE_ID + "_02" + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                    }
                    break;

                case Constants.ELECTRO_GOLD:

                    var electroGold = new ServiceBL().GetReportElectroGold(_serviceId, Constants.ELECTRO_GOLD);
                    dsGetRepo = new DataSet();
                    DataTable dt_ELECTRO_GOLD = BLL.Utils.ConvertToDatatable(electroGold);
                    dt_ELECTRO_GOLD.TableName = "dtEstudioElectrocardiografico";
                    dsGetRepo.Tables.Add(dt_ELECTRO_GOLD);

                    rp = new crInformeElectroCardiografiaoGoldField_EKG();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.ELECTRO_GOLD + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;

                case Constants.ELECTROCARDIOGRAMA_ID:

                    var ELECTROCARDIOGRAMA_ID = new ServiceBL().GetReportEstudioElectrocardiografico(_serviceId, Constants.ELECTROCARDIOGRAMA_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_ELECTROCARDIOGRAMA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ELECTROCARDIOGRAMA_ID);
                    dt_ELECTROCARDIOGRAMA_ID.TableName = "dtEstudioElectrocardiografico";
                    dsGetRepo.Tables.Add(dt_ELECTROCARDIOGRAMA_ID);


                    if (pintIdCrystal == 15)
                    {
                        rp = new Reports.crEstudioElectrocardiograficoMedico();
                    }
                    else if (pintIdCrystal == 16)
                    {
                        rp = new Reports.crEstudioElectrocardiograficoSinFirma();
                    }
                    else
                    {
                        rp = new Reports.crEstudioElectrocardiografico();
                    }

                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.ELECTROCARDIOGRAMA_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();

                    break;

                case Constants.PRUEBA_ESFUERZO_ID:
                    var aptitudeCertificate = new ServiceBL().GetReportPruebaEsfuerzo(_serviceId, Constants.PRUEBA_ESFUERZO_ID);
                    var FuncionesVitales1 = new ServiceBL().ReportFuncionesVitales(_serviceId, Constants.FUNCIONES_VITALES_ID);
                    var Antropometria1 = new ServiceBL().ReportAntropometria(_serviceId, Constants.ANTROPOMETRIA_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_PRUEBA_ESFUERZO_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(aptitudeCertificate);
                    dt_PRUEBA_ESFUERZO_ID.TableName = "dtPruebaEsfuerzo";
                    dsGetRepo.Tables.Add(dt_PRUEBA_ESFUERZO_ID);

                    DataTable dt1_PRUEBA_ESFUERZO_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(FuncionesVitales1);
                    dt1_PRUEBA_ESFUERZO_ID.TableName = "dtFuncionesVitales";
                    dsGetRepo.Tables.Add(dt1_PRUEBA_ESFUERZO_ID);

                    DataTable dt2_PRUEBA_ESFUERZO_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Antropometria1);
                    dt2_PRUEBA_ESFUERZO_ID.TableName = "dtAntropometria";
                    dsGetRepo.Tables.Add(dt2_PRUEBA_ESFUERZO_ID);

                    rp = new Reports.crPruebaEsfuerzo();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.PRUEBA_ESFUERZO_ID + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.PRUEBA_ESFUERZO_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;
                case Constants.ODONTOGRAMA_ID:
                    var Path1 = Application.StartupPath;
                    var ODONTOGRAMA_ID = new ServiceBL().ReportOdontograma(_serviceId, Constants.ODONTOGRAMA_ID, Path1);

                    dsGetRepo = new DataSet();

                    DataTable dt_ODONTOGRAMA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ODONTOGRAMA_ID);
                    dt_ODONTOGRAMA_ID.TableName = "dtOdontograma";
                    dsGetRepo.Tables.Add(dt_ODONTOGRAMA_ID);


                    if (pintIdCrystal == 19)
                    {
                        rp = new Reports.crOdontogramaSinFirma();
                    }
                    else
                    {
                        rp = new Reports.crOdontograma();
                    }

                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.ODONTOGRAMA_ID + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.ODONTOGRAMA_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;
                case Constants.AUDIOMETRIA_ID:

                    var serviceBL = new ServiceBL();
                    DataSet dsAudiometria = new DataSet();

                    var dxList = serviceBL.GetDiagnosticRepositoryByComponent(_serviceId, Constants.AUDIOMETRIA_ID);
                    if (dxList.Count == 0)
                    {
                        DiagnosticRepositoryList oDiagnosticRepositoryList = new DiagnosticRepositoryList();
                        List<DiagnosticRepositoryList> Lista = new List<DiagnosticRepositoryList>();
                        oDiagnosticRepositoryList.v_ServiceId = "Sin Id";
                        oDiagnosticRepositoryList.v_DiseasesName = "Sin Alteración";
                        oDiagnosticRepositoryList.v_DiagnosticRepositoryId = "Sin Id";
                        Lista.Add(oDiagnosticRepositoryList);
                        var dtDx = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Lista);
                        dtDx.TableName = "dtDiagnostic";
                        dsAudiometria.Tables.Add(dtDx);
                    }
                    else
                    {
                        List<DiagnosticRepositoryList> ListaDxsAudio = new List<DiagnosticRepositoryList>();
                        DiagnosticRepositoryList oDiagnosticRepositoryList;
                        foreach (var item in dxList)
                        {
                            oDiagnosticRepositoryList = new DiagnosticRepositoryList();

                            oDiagnosticRepositoryList.v_DiseasesName = item.v_DiseasesName;
                            oDiagnosticRepositoryList.v_DiagnosticRepositoryId = item.v_DiagnosticRepositoryId;
                            oDiagnosticRepositoryList.v_ServiceId = item.v_ServiceId;
                            ListaDxsAudio.Add(oDiagnosticRepositoryList);
                        }


                        var dtDx = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ListaDxsAudio);
                        dtDx.TableName = "dtDiagnostic";
                        dsAudiometria.Tables.Add(dtDx);
                    }


                    var recom = dxList.SelectMany(s1 => s1.Recomendations).ToList();
                    if (recom.Count == 0)
                    {
                        RecomendationList oRecomendationList = new RecomendationList();
                        List<RecomendationList> Lista = new List<RecomendationList>();

                        oRecomendationList.v_ServiceId = "Sin Id";
                        oRecomendationList.v_RecommendationName = "Sin Recomendaciones";
                        oRecomendationList.v_DiagnosticRepositoryId = "Sin Id";
                        Lista.Add(oRecomendationList);
                        var dtReco = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Lista);
                        dtReco.TableName = "dtRecomendation";
                        dsAudiometria.Tables.Add(dtReco);

                    }
                    else
                    {
                        var dtReco = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(recom);
                        dtReco.TableName = "dtRecomendation";
                        dsAudiometria.Tables.Add(dtReco);
                    }


                    //-------******************************************************************************************

                    var audioUserControlList = serviceBL.ReportAudiometriaUserControl(_serviceId, Constants.AUDIOMETRIA_ID);
                    var audioCabeceraList = serviceBL.ReportAudiometria(_serviceId, Constants.AUDIOMETRIA_ID);
                    var dtAudiometriaUserControl = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(audioUserControlList);
                    var dtCabecera = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(audioCabeceraList);

                    dtCabecera.TableName = "dtAudiometria";
                    dtAudiometriaUserControl.TableName = "dtAudiometriaUserControl";

                    dsAudiometria.Tables.Add(dtCabecera);
                    dsAudiometria.Tables.Add(dtAudiometriaUserControl);

                    var MedicalCenter1 = new ServiceBL().GetInfoMedicalCenter();

                    if (MedicalCenter1.v_IdentificationNumber == "20506336245")
                    {
                        rp = new Reports.crFichaAudiometriaAudiomax01();
                        rp.SetDataSource(dsAudiometria);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.AUDIOMETRIA_ID + "1.pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();

                        rp = new Reports.crFichaAudiometriaAudiomax02();
                        rp.SetDataSource(dsAudiometria);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.AUDIOMETRIA_ID + "2.pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                    }
                    else if (pintIdCrystal == 40)
                    {
                        rp = new Reports.crApendice03_Audio();
                        rp.SetDataSource(dsAudiometria);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.AUDIOMETRIA_ID + "_03" + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                    }
                    else
                    {
                        if (pintIdCrystal == 11)
                        {
                            rp = new Reports.crFichaAudiometriaMedico();
                        }
                        else if (pintIdCrystal == 12)
                        {
                            rp = new Reports.crFichaAudiometriaSinFirma();
                        }
                        else
                        {
                            rp = new Reports.crFichaAudiometria();
                        }


                        rp.SetDataSource(dsAudiometria);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.AUDIOMETRIA_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                    }





                    // Historia Ocupacional Audiometria
                    //var dataListForReport_1 = new ServiceBL().ReportHistoriaOcupacionalAudiometria(_serviceId);

                    //if (dataListForReport_1.Count != 0)
                    //{
                    //    dsGetRepo = new DataSet();
                    //    DataTable dt_dataListForReport_1 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport_1);

                    //    dt_dataListForReport_1.TableName = "dtHistoriaOcupacional";

                    //    dsGetRepo.Tables.Add(dt_dataListForReport_1);

                    //    rp = new Reports.crHistoriaOcupacionalAudiometria();
                    //    rp.SetDataSource(dsGetRepo);
                    //    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    //    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    //    objDiskOpt = new DiskFileDestinationOptions();
                    //    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + "AUDIOMETRIA_ID_HISTORIA" + ".pdf";
                    //    objDiskOpt.DiskFileName = ruta + serviceId + "-" + "AUDIOMETRIA_ID_HISTORIA" + ".pdf";
                    //    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    //    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    //    rp.Export();
                    //    rp.Close();
                    //}

                    break;

                case Constants.GINECOLOGIA_ID:      // Falta implementar
                    var GINECOLOGIA_ID = new ServiceBL().GetReportEvaluacionGinecologico(_serviceId, Constants.GINECOLOGIA_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_GINECOLOGIA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(GINECOLOGIA_ID);
                    dt_GINECOLOGIA_ID.TableName = "dtEvaGinecologico";
                    dsGetRepo.Tables.Add(dt_GINECOLOGIA_ID);

                    rp = new Reports.crEvaluacionGenecologica();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.GINECOLOGIA_ID + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.GINECOLOGIA_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.OFTALMOLOGIA_ID:

                    var OFTALMO_ANTIGUO = new PacientBL().GetOftalmologia(_serviceId, Constants.OFTALMOLOGIA_ID);
                    dsGetRepo = new DataSet();
                    DataTable dt_OFTALMO_ANTIGUO = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(OFTALMO_ANTIGUO);
                    dt_OFTALMO_ANTIGUO.TableName = "dtOftalmologia";
                    dsGetRepo.Tables.Add(dt_OFTALMO_ANTIGUO);

                    if (pintIdCrystal == 39)
                    {
                        rp = new Reports.crApendice02_Oftalmo();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.OFTALMOLOGIA_ID + "_02" + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                    }
                    else
                    {
                        rp = new Reports.crOftalmologia();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.OFTALMOLOGIA_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                    }

                    break;

                case Constants.PSICOLOGIA_ID:
                    var PSICOLOGIA_ID = new PacientBL().GetFichaPsicologicaOcupacional(_serviceId);

                    dsGetRepo = new DataSet();

                    DataTable dt_PSICOLOGIA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(PSICOLOGIA_ID);

                    dt_PSICOLOGIA_ID.TableName = "InformePsico";

                    dsGetRepo.Tables.Add(dt_PSICOLOGIA_ID);

                    rp = new Reports.InformePsicologicoOcupacional();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.PSICOLOGIA_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();

                    break;

                case Constants.RX_TORAX_ID:


                    var RX_TORAX_ID = new ServiceBL().ReportRadiologico(_serviceId, Constants.RX_TORAX_ID);
                    dsGetRepo = new DataSet();

                    DataTable dt_RX_TORAX_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(RX_TORAX_ID);

                    dt_RX_TORAX_ID.TableName = "dtRadiologico";

                    dsGetRepo.Tables.Add(dt_RX_TORAX_ID);

                    if (pintIdCrystal == 7)
                    {
                        rp = new Reports.crInformeRadiologicoMedico();
                    }
                    else if (pintIdCrystal == 8)
                    {
                        rp = new Reports.crInformeRadiologicoSinFirma();
                    }
                    else
                    {
                        rp = new Reports.crInformeRadiologico();
                    }


                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.RX_TORAX_ID + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.RX_TORAX_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.OIT_ID:
                    var OIT_ID = new ServiceBL().ReportInformeRadiografico(_serviceId, Constants.OIT_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_OIT_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(OIT_ID);
                    dt_OIT_ID.TableName = "dtInformeRadiografico";
                    dsGetRepo.Tables.Add(dt_OIT_ID);


                    if (pintIdCrystal == 45)
                    {
                        rp = new Reports.crApendice06_OIT();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.OIT_ID + ".pdf";
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.OIT_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                    }
                    else
                    {
                        rp = new Reports.crInformeRadiograficoOIT();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.OIT_ID + ".pdf";
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.OIT_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();
                    }
                    break;

                case Constants.TAMIZAJE_DERMATOLOGIO_ID:
                    var TAMIZAJE_DERMATOLOGIO_ID = new ServiceBL().ReportTamizajeDermatologico(_serviceId, Constants.TAMIZAJE_DERMATOLOGIO_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_TAMIZAJE_DERMATOLOGIO_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(TAMIZAJE_DERMATOLOGIO_ID);
                    dt_TAMIZAJE_DERMATOLOGIO_ID.TableName = "dtTamizajeDermatologico";
                    dsGetRepo.Tables.Add(dt_TAMIZAJE_DERMATOLOGIO_ID);

                    rp = new Reports.crTamizajeDermatologico();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.TAMIZAJE_DERMATOLOGIO_ID + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.TAMIZAJE_DERMATOLOGIO_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;
                case Constants.INFORME_ESPIROMETRIA:

                    var INFORME_ESPIROMETRIA = new ServiceBL().GetReportCuestionarioEspirometria(_serviceId, Constants.ESPIROMETRIA_ID);

                    dsGetRepo = new DataSet();
                    DataTable dtINFORME_ESPIROMETRIA = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(INFORME_ESPIROMETRIA);
                    dtINFORME_ESPIROMETRIA.TableName = "dtCuestionarioEspirometria";
                    dsGetRepo.Tables.Add(dtINFORME_ESPIROMETRIA);
                    if (pintIdCrystal == 46)
                    {
                        rp = new Reports.crApendice08_Espiro02();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_ESPIROMETRIA + "_04" + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;

                        rp.Export();
                        rp.Close();
                    }
                    break;
                case Constants.ESPIROMETRIA_ID:

                    var ESPIROMETRIA_ID = new ServiceBL().GetReportCuestionarioEspirometria(_serviceId, Constants.ESPIROMETRIA_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_ESPIROMETRIA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ESPIROMETRIA_ID);
                    dt_ESPIROMETRIA_ID.TableName = "dtCuestionarioEspirometria";
                    dsGetRepo.Tables.Add(dt_ESPIROMETRIA_ID);

                    if (pintIdCrystal == 54)
                    {

                        rp = new Reports.crEspiroCuestionarioGoldField();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.ESPIROMETRIA_ID + "_02" + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;

                        rp.Export();
                        rp.Close();
                    }
                    else
                        if (pintIdCrystal == 35)
                        {
                            rp = new Reports.crApendice08_Espiro01();
                            rp.SetDataSource(dsGetRepo);
                            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            objDiskOpt = new DiskFileDestinationOptions();
                            objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.ESPIROMETRIA_ID + "_03" + ".pdf";
                            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                            rp.ExportOptions.DestinationOptions = objDiskOpt;

                            rp.Export();
                            rp.Close();
                        }
                        else if (pintIdCrystal == 58)
                        {
                            rp = new Reports.crAnexo05_Espiro();
                            rp.SetDataSource(dsGetRepo);
                            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            objDiskOpt = new DiskFileDestinationOptions();
                            objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.ESPIROMETRIA_ID + "_05" + ".pdf";
                            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                            rp.ExportOptions.DestinationOptions = objDiskOpt;

                            rp.Export();
                            rp.Close();
                        }
                        else
                        {
                            rp = new Reports.crCuestionarioEspirometria();
                            rp.SetDataSource(dsGetRepo);
                            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            objDiskOpt = new DiskFileDestinationOptions();
                            objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.ESPIROMETRIA_ID + ".pdf";
                            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                            rp.ExportOptions.DestinationOptions = objDiskOpt;
                            rp.Export();
                            rp.Close();

                        }

                    break;

                case Constants.EVALUACION_PSICOLABORAL:
                    var EVALUACION_PSICOLABORAL = new ServiceBL().GetReportEvaluacionPsicolaborlaPersonal(_serviceId, Constants.EVALUACION_PSICOLABORAL);

                    dsGetRepo = new DataSet();
                    DataTable dt_EVALUACION_PSICOLABORAL = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(EVALUACION_PSICOLABORAL);
                    dt_EVALUACION_PSICOLABORAL.TableName = "dtEvaluacionPsicolaboralPersonal";
                    dsGetRepo.Tables.Add(dt_EVALUACION_PSICOLABORAL);

                    rp = new Reports.crEvaluacionPsicolaboralPersonal();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.EVALUACION_PSICOLABORAL + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.EVALUACION_PSICOLABORAL + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;



                case Constants.TESTOJOSECO_ID:

                    var TESTOJOSECO_ID = new ServiceBL().ReporteOjoSeco(_serviceId, Constants.TESTOJOSECO_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_TESTOJOSECO_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(TESTOJOSECO_ID);
                    dt_TESTOJOSECO_ID.TableName = "dtOjoSeco";
                    dsGetRepo.Tables.Add(dt_TESTOJOSECO_ID);

                    rp = new Reports.crCuestionarioOjoSeco();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.TESTOJOSECO_ID + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.TESTOJOSECO_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.C_N_ID:
                    var C_N_ID = new ServiceBL().GetReportCuestionarioNordico(_serviceId, Constants.C_N_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_C_N_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(C_N_ID);
                    dt_C_N_ID.TableName = "dtCustionarioNordico";
                    dsGetRepo.Tables.Add(dt_C_N_ID);

                    rp = new Reports.crCuestionarioNordico();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.C_N_ID + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.C_N_ID + "_01.pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();


                    rp = new Reports.crCuestionarioNordico_02();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.C_N_ID + "_02" + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;


                    break;
                case Constants.CUESTIONARIO_ACTIVIDAD_FISICA:
                    var CUESTIONARIO_ACTIVIDAD_FISICA = new ServiceBL().GetReportCuestionarioActividadFisica(_serviceId, Constants.CUESTIONARIO_ACTIVIDAD_FISICA);

                    dsGetRepo = new DataSet();

                    DataTable dt_CUESTIONARIO_ACTIVIDAD_FISICA = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(CUESTIONARIO_ACTIVIDAD_FISICA);
                    dt_CUESTIONARIO_ACTIVIDAD_FISICA.TableName = "dtCustionarioActividadFisica";
                    dsGetRepo.Tables.Add(dt_CUESTIONARIO_ACTIVIDAD_FISICA);

                    rp = new Reports.crCuestionarioActividadFisica();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.CUESTIONARIO_ACTIVIDAD_FISICA + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.CUESTIONARIO_ACTIVIDAD_FISICA + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;

                case Constants.INFORME_ECOGRAFICO_PROSTATA_ID:
                    var INFORME_ECOGRAFICO_PROSTATA_ID = new ServiceBL().GetReportInformeEcograficoProstata(_serviceId, Constants.INFORME_ECOGRAFICO_PROSTATA_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_INFORME_ECOGRAFICO_PROSTATA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(INFORME_ECOGRAFICO_PROSTATA_ID);
                    dt_INFORME_ECOGRAFICO_PROSTATA_ID.TableName = "dtInformeEcograficoProstata";
                    dsGetRepo.Tables.Add(dt_INFORME_ECOGRAFICO_PROSTATA_ID);
                    rp = new Reports.crInformeEcograficoProstata();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.INFORME_ECOGRAFICO_PROSTATA_ID + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_ECOGRAFICO_PROSTATA_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();

                    break;

                case Constants.ECOGRAFIA_ABDOMINAL_ID:
                    var ECOGRAFIA_ABDOMINAL_ID = new ServiceBL().GetReportInformeEcograficoAbdominal(_serviceId, Constants.ECOGRAFIA_ABDOMINAL_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_ECOGRAFIA_ABDOMINAL_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ECOGRAFIA_ABDOMINAL_ID);
                    dt_ECOGRAFIA_ABDOMINAL_ID.TableName = "dtInformeEcograficoAbdominal";
                    dsGetRepo.Tables.Add(dt_ECOGRAFIA_ABDOMINAL_ID);

                    rp = new Reports.crInformeEcograficoAbdominal();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.ECOGRAFIA_ABDOMINAL_ID + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.ECOGRAFIA_ABDOMINAL_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;

                case Constants.ECOGRAFIA_RENAL_ID:
                    var ECOGRAFIA_RENAL_ID = new ServiceBL().GetReportInformeEcograficoRenal(_serviceId, Constants.ECOGRAFIA_RENAL_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_ECOGRAFIA_RENAL_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ECOGRAFIA_RENAL_ID);
                    dt_ECOGRAFIA_RENAL_ID.TableName = "dtInformeEcograficoRenal";
                    dsGetRepo.Tables.Add(dt_ECOGRAFIA_RENAL_ID);

                    rp = new Reports.crInformeEcograficoRenal();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.ECOGRAFIA_RENAL_ID + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.ECOGRAFIA_RENAL_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;



                case Constants.INFORME_CERTIFICADO_APTITUD_SM:
                    var INFORME_CERTIFICADO_APTITUD_SM = new ServiceBL().GetCAPSM(_serviceId);

                    dsGetRepo = new DataSet();
                    DataTable dtINFORME_CERTIFICADO_APTITUD_SM = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(INFORME_CERTIFICADO_APTITUD_SM);
                    dtINFORME_CERTIFICADO_APTITUD_SM.TableName = "AptitudeCertificate";
                    dsGetRepo.Tables.Add(dtINFORME_CERTIFICADO_APTITUD_SM);
                    rp = new Reports.crCertficadoDeAptitudSM();
                    rp.SetDataSource(dsGetRepo);

                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.INFORME_CERTIFICADO_APTITUD_SM + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_CERTIFICADO_APTITUD_SM + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;






                case Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX:
                    var Path3 = Application.StartupPath;
                    var INFORME_CERTIFICADO_APTITUD_SIN_DX = new ServiceBL().GetCAPSD(_serviceId, Path3);
                    dsGetRepo = new DataSet();
                    DataTable dtINFORME_CERTIFICADO_APTITUD_SIN_DX = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(INFORME_CERTIFICADO_APTITUD_SIN_DX);
                    dtINFORME_CERTIFICADO_APTITUD_SIN_DX.TableName = "AptitudeCertificate";
                    dsGetRepo.Tables.Add(dtINFORME_CERTIFICADO_APTITUD_SIN_DX);



                    if (INFORME_CERTIFICADO_APTITUD_SIN_DX == null)
                    {
                        break;
                    }

                    TipoServicio = INFORME_CERTIFICADO_APTITUD_SIN_DX[0].i_EsoTypeId;

                    if (pintIdCrystal == 28)
                    {
                        if (TipoServicio == ((int)TypeESO.Retiro).ToString())
                        {
                            rp = new Reports.crOccupationalRetirosSinDxSinFirma();
                            //rp.SetDataSource(dsGetRepo);
                            string rutaCertificado = Common.Utils.GetApplicationConfigValue("CertificadoSinDX").ToString();
                            //rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                            rp.SetDataSource(dsGetRepo);
                            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            objDiskOpt = new DiskFileDestinationOptions();
                            objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].v_OrganizationName + "-" + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX + "-" + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                            rp.ExportOptions.DestinationOptions = objDiskOpt;
                            rp.Export();

                        }
                        else
                        {
                            if (INFORME_CERTIFICADO_APTITUD_SIN_DX[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs)
                            {
                                rp = new Reports.crCertficadoObservadoSinDxSinFirma();
                                //rp.SetDataSource(dsGetRepo);
                                string rutaCertificado = Common.Utils.GetApplicationConfigValue("CertificadoSinDX").ToString();
                                //rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                                rp.SetDataSource(dsGetRepo);
                                rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                objDiskOpt = new DiskFileDestinationOptions();
                                objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].v_OrganizationName + "-" + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX + "-" + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                                rp.ExportOptions.DestinationOptions = objDiskOpt;
                                rp.Export();
                            }
                            else
                            {
                                rp = new Reports.crCertificadoSinDXSinFirma();
                                //rp.SetDataSource(dsGetRepo);
                                string rutaCertificado = Common.Utils.GetApplicationConfigValue("CertificadoSinDX").ToString();
                                //rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                                rp.SetDataSource(dsGetRepo);
                                rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                objDiskOpt = new DiskFileDestinationOptions();
                                objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].v_OrganizationName + "-" + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX + "-" + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                                rp.ExportOptions.DestinationOptions = objDiskOpt;
                                rp.Export();
                            }
                        }
                    }
                    else
                    {
                        if (TipoServicio == ((int)TypeESO.Retiro).ToString())
                        {
                            rp = new Reports.crOccupationalRetirosSinDx();
                            //rp.SetDataSource(dsGetRepo);
                            string rutaCertificado = Common.Utils.GetApplicationConfigValue("CertificadoSinDX").ToString();
                            //rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                            rp.SetDataSource(dsGetRepo);
                            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            objDiskOpt = new DiskFileDestinationOptions();
                            objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].v_OrganizationName + "-" + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX + "-" + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                            rp.ExportOptions.DestinationOptions = objDiskOpt;
                            rp.Export();
                        }
                        else
                        {
                            if (INFORME_CERTIFICADO_APTITUD_SIN_DX[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs)
                            {
                                rp = new Reports.crCertficadoObservadoSinDx();
                                //rp.SetDataSource(dsGetRepo);
                                string rutaCertificado = Common.Utils.GetApplicationConfigValue("CertificadoSinDX").ToString();
                                //rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                                rp.SetDataSource(dsGetRepo);
                                rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                objDiskOpt = new DiskFileDestinationOptions();
                                objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].v_OrganizationName + "-" + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX + "-" + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                                rp.ExportOptions.DestinationOptions = objDiskOpt;
                                rp.Export();
                            }
                            else
                            {
                                rp = new Reports.crCertificadoDeAptitudSinDX();
                                //rp.SetDataSource(dsGetRepo);
                                string rutaCertificado = Common.Utils.GetApplicationConfigValue("CertificadoSinDX").ToString();
                                //rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                                rp.SetDataSource(dsGetRepo);
                                rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                objDiskOpt = new DiskFileDestinationOptions();
                                objDiskOpt.DiskFileName = rutaCertificado + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].v_OrganizationName + "-" + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].v_PersonName + "-" + Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX + "-" + INFORME_CERTIFICADO_APTITUD_SIN_DX[0].d_ServiceDate.Value.ToString("dd MMMM,  yyyy") + ".pdf";

                                rp.ExportOptions.DestinationOptions = objDiskOpt;
                                rp.Export();
                            }
                        }
                    }



                    //rp = new Reports.crCertificadoDeAptitudSinDX();
                    rp.SetDataSource(dsGetRepo);

                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;


                case Constants.TEST_VERTIGO_ID:
                      var servicesId6 = new List<string>();
                    servicesId6.Add(_serviceId);
                    var componentReportId6 = new ServiceBL().ObtenerIdsParaImportacionExcel(servicesId6, 11);
                    var TEST_VERTIGO_ID = new ServiceBL().GetReportTestVertigo(_serviceId, componentId, componentReportId6[0].ComponentId);

                    //var TEST_VERTIGO_ID = new ServiceBL().GetReportTestVertigo(_serviceId, Constants.TEST_VERTIGO_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_TEST_VERTIGO_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(TEST_VERTIGO_ID);
                    dt_TEST_VERTIGO_ID.TableName = "dtTestVertigo";
                    dsGetRepo.Tables.Add(dt_TEST_VERTIGO_ID);

                    rp = new Reports.crTestDeVertigo();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.TEST_VERTIGO_ID + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.TEST_VERTIGO_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;

                case Constants.EVA_CARDIOLOGICA_ID:
                    var EVA_CARDIOLOGICA_ID = new ServiceBL().GetReportEvaluacionCardiologia(_serviceId, Constants.EVA_CARDIOLOGICA_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_EVA_CARDIOLOGICA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(EVA_CARDIOLOGICA_ID);
                    dt_EVA_CARDIOLOGICA_ID.TableName = "dtEvaCardiologia";
                    dsGetRepo.Tables.Add(dt_EVA_CARDIOLOGICA_ID);
                    rp = new Reports.crEvaluacionCardiologicaSM();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.EVA_CARDIOLOGICA_ID + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.EVA_CARDIOLOGICA_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;
                case Constants.EVA_OSTEO_ID:

                    var EVA_OSTEO_ID = new ServiceBL().GetReportEvaOsteoSanMartin(_serviceId, Constants.EVA_OSTEO_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_EVA_OSTEO_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(EVA_OSTEO_ID);
                    dt_EVA_OSTEO_ID.TableName = "dtEvaOsteo";
                    dsGetRepo.Tables.Add(dt_EVA_OSTEO_ID);


                    rp = new Reports.crOsteoSanMartin();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.EVA_OSTEO_ID + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.EVA_OSTEO_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;

                case Constants.HISTORIA_CLINICA_PSICOLOGICA_ID:
                    var HISTORIA_CLINICA_PSICOLOGICA_ID = new ServiceBL().GetHistoriaClinicaPsicologica(_serviceId, Constants.HISTORIA_CLINICA_PSICOLOGICA_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_HISTORIA_CLINICA_PSICOLOGICA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(HISTORIA_CLINICA_PSICOLOGICA_ID);
                    dt_HISTORIA_CLINICA_PSICOLOGICA_ID.TableName = "dtHistoriaClinicaPsicologica";
                    dsGetRepo.Tables.Add(dt_HISTORIA_CLINICA_PSICOLOGICA_ID);
                    if (pintIdCrystal == 42)
                    {

                        rp = new Reports.crApendice04_Psico_01();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.PSICOLOGIA_ID + "_01" + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();

                        rp = new Reports.crApendice04_Psico_02();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.PSICOLOGIA_ID + "_02" + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();

                    }
                    else if (pintIdCrystal == 55)
                    {

                        rp = new Reports.crHistoriaClinicaPsicologica_GOLD();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.PSICOLOGIA_ID + "01" + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();

                        rp = new Reports.crHistoriaClinicaPsicologica2_GOLD();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.PSICOLOGIA_ID + "02" + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;
                        rp.Export();
                        rp.Close();

                    }
                    else
                    {
                        rp = new Reports.crHistoriaClinicaPsicologica();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.HISTORIA_CLINICA_PSICOLOGICA_ID + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;

                        rp.Export();
                        rp.Close();


                        rp = new Reports.crHistoriaClinicaPsicologica2();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.HISTORIA_CLINICA_PSICOLOGICA_ID + "2" + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;

                        rp.Export();
                        rp.Close();
                    }
                    break;
                case Constants.EVA_NEUROLOGICA_ID:
                    var EVA_NEUROLOGICA_ID = new ServiceBL().GetReportEvaNeurologica(_serviceId, Constants.EVA_NEUROLOGICA_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_EVA_NEUROLOGICA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(EVA_NEUROLOGICA_ID);
                    dt_EVA_NEUROLOGICA_ID.TableName = "dtEvaNeurologica";
                    dsGetRepo.Tables.Add(dt_EVA_NEUROLOGICA_ID);

                    rp = new Reports.crEvaluacionNeurologica();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.EVA_NEUROLOGICA_ID + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.EVA_NEUROLOGICA_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;



                case Constants.SINTOMATICO_RESPIRATORIO:
                    var SINTOMATICO_RESPIRATORIO = new ServiceBL().ReporteSintomaticoRespiratorio(_serviceId, Constants.SINTOMATICO_RESPIRATORIO);

                    dsGetRepo = new DataSet();

                    DataTable dt_SINTOMATICO_RESPIRATORIO = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(SINTOMATICO_RESPIRATORIO);
                    dt_SINTOMATICO_RESPIRATORIO.TableName = "dtSintomaticoRes";
                    dsGetRepo.Tables.Add(dt_SINTOMATICO_RESPIRATORIO);

                    rp = new Reports.crSintomaticoResp();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.SINTOMATICO_RESPIRATORIO + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.SINTOMATICO_RESPIRATORIO + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;

                case Constants.FICHA_OTOSCOPIA:
                    var FICHA_OTOSCOPIA = new ServiceBL().GetReportFichaOtoscopia(_serviceId, Constants.FICHA_OTOSCOPIA);

                    dsGetRepo = new DataSet();

                    DataTable dt_FICHA_OTOSCOPIA = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(FICHA_OTOSCOPIA);
                    dt_FICHA_OTOSCOPIA.TableName = "dtOtoscopia";
                    dsGetRepo.Tables.Add(dt_FICHA_OTOSCOPIA);

                    rp = new Reports.crEvaluacionNeurologica();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.FICHA_OTOSCOPIA + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.FICHA_OTOSCOPIA + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;


                case Constants.SOMNOLENCIA_ID:
                    //var SOMNOLENCIA_ID = new ServiceBL().ReporteSomnolencia(_serviceId, Constants.SOMNOLENCIA_ID);
                    
                    var servicesId12 = new List<string>();
                    servicesId12.Add(_serviceId);
                    var componentReportId12 = new ServiceBL().ObtenerIdsParaImportacionExcel(servicesId12, 11);
                    var SOMNOLENCIA_ID = new ServiceBL().ReporteSomnolencia(_serviceId, componentId, componentReportId12[0].ComponentId);

                    dsGetRepo = new DataSet();

                    DataTable dt_SOMNOLENCIA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(SOMNOLENCIA_ID);
                    dt_SOMNOLENCIA_ID.TableName = "dtSomnolencia";
                    dsGetRepo.Tables.Add(dt_SOMNOLENCIA_ID);

                    rp = new Reports.crTestEpwotrh();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.FICHA_OTOSCOPIA + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.SOMNOLENCIA_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;



                case Constants.ACUMETRIA_ID:
                    var ACUMETRIA_ID = new ServiceBL().ReporteAcumetria(_serviceId, Constants.ACUMETRIA_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_ACUMETRIA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ACUMETRIA_ID);
                    dt_ACUMETRIA_ID.TableName = "dtAcumetria";
                    dsGetRepo.Tables.Add(dt_ACUMETRIA_ID);

                    rp = new Reports.crFichaAcumetria();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.FICHA_OTOSCOPIA + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.ACUMETRIA_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;


                case Constants.EVA_ERGONOMICA_ID:
                    var EVA_ERGONOMICA_ID = new ServiceBL().ReporteErgnomia(_serviceId, Constants.EVA_ERGONOMICA_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_EVA_ERGONOMICA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(EVA_ERGONOMICA_ID);
                    dt_EVA_ERGONOMICA_ID.TableName = "dtErgonomia";
                    dsGetRepo.Tables.Add(dt_EVA_ERGONOMICA_ID);

                    rp = new Reports.crEvaluacionErgonomica01();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.EVA_ERGONOMICA_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();



                    rp = new Reports.crEvaluacionErgonomica02();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + "EVA_ERGONOMICA_ID2" + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();



                    rp = new Reports.crEvaluacionErgonomica03();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + "EVA_ERGONOMICA_ID3" + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;


                case Constants.OTOSCOPIA_ID:
                    var OTOSCOPIA_ID = new ServiceBL().ReporteOtoscopia(_serviceId, Constants.OTOSCOPIA_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_OTOSCOPIA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(OTOSCOPIA_ID);
                    dt_OTOSCOPIA_ID.TableName = "dtOtoscopia";
                    dsGetRepo.Tables.Add(dt_OTOSCOPIA_ID);

                    rp = new Reports.crFichaOtoscopia();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.FICHA_OTOSCOPIA + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.FICHA_OTOSCOPIA + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;

                case Constants.SINTOMATICO_ID:
                    var SINTOMATICO_ID = new ServiceBL().ReporteSintomaticoRespiratorio(_serviceId, Constants.SINTOMATICO_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_SINTOMATICO_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(SINTOMATICO_ID);
                    dt_SINTOMATICO_ID.TableName = "dtSintomaticoRes";
                    dsGetRepo.Tables.Add(dt_SINTOMATICO_ID);

                    rp = new Reports.crSintomaticoResp();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.FICHA_OTOSCOPIA + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.SINTOMATICO_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;


                case Constants.LUMBOSACRA_ID:
                    /*
                    var Componente = "";
                    var o = ConsolidadoReportes.FindAll(p => p.i_CategoryId == 6).OrderBy(p => p.v_ComponentId).FirstOrDefault();
                    var oLumbar = ConsolidadoReportes.FindAll(p => p.i_CategoryId == 21).OrderBy(p => p.v_ComponentId).FirstOrDefault();

                    var categoryId = o.i_CategoryId;
                    var LUMBOSACRA_ID = new List<LumboSacracs>();
                    if (categoryId == 6)
                    {
                        Componente = o.v_ComponentId;
                        LUMBOSACRA_ID = new ServiceBL().ReporteLumboSaca(_serviceId, Componente);

                    }
                    else
                    {
                        Componente = Constants.LUMBOSACRA_ID;
                        LUMBOSACRA_ID = new ServiceBL().ReporteLumboSaca(_serviceId, Componente);
                    }
                   
                     */

                    var LUMBOSACRA_ID = new ServiceBL().ReporteLumboSaca(_serviceId, Constants.LUMBOSACRA_ID);
                    dsGetRepo = new DataSet();

                    DataTable dt_LUMBOSACRA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(LUMBOSACRA_ID);
                    dt_LUMBOSACRA_ID.TableName = "dtLumboSacra";
                    dsGetRepo.Tables.Add(dt_LUMBOSACRA_ID);

                    rp = new Reports.crInformeRadiologicoLumbar();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.FICHA_OTOSCOPIA + ".pdf";
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.LUMBOSACRA_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();
                    break;




                //case Constants.OJO_SECO_ID:
                //    var OJO_SECO_ID = new ServiceBL().ReporteOjoSeco(_serviceId, Constants.OJO_SECO_ID);

                //    dsGetRepo = new DataSet();

                //    DataTable dt_OJO_SECO_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(OJO_SECO_ID);
                //    dt_OJO_SECO_ID.TableName = "dtOjoSeco";
                //    dsGetRepo.Tables.Add(dt_OJO_SECO_ID);

                //    rp = new Reports.crCuestionarioOjoSeco();
                //    rp.SetDataSource(dsGetRepo);
                //    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                //    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                //    objDiskOpt = new DiskFileDestinationOptions();
                //    //objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\" + Constants.FICHA_OTOSCOPIA + ".pdf";
                //    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.FICHA_OTOSCOPIA + ".pdf";
                //    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                //    rp.ExportOptions.DestinationOptions = objDiskOpt;

                //    rp.Export();
                //    rp.Close();
                //    break;





                case Constants.AutoevaTrabEquipo_ID:
                    var AutoevaTrabEquipo = new ServiceBL().ReporteAutoevaTrabEquipo(_serviceId, Constants.AutoevaTrabEquipo_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_AutoevaTrabEquipo = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(AutoevaTrabEquipo);
                    dt_AutoevaTrabEquipo.TableName = "dtAutoevaTrabEquipo";
                    dsGetRepo.Tables.Add(dt_AutoevaTrabEquipo);

                    rp = new Reports.crAutoevaTrabEquipo();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.AutoevaTrabEquipo_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;


                case Constants.Cuestionariogradodeafectividad_ID:
                    var Cuestionariogradodeafectividad = new ServiceBL().ReporteCuestionariogradodeafectividad(_serviceId, Constants.Cuestionariogradodeafectividad_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_Cuestionariogradodeafectividad = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Cuestionariogradodeafectividad);
                    dt_Cuestionariogradodeafectividad.TableName = "dtCuestionariogradodeafectividad";
                    dsGetRepo.Tables.Add(dt_Cuestionariogradodeafectividad);

                    rp = new Reports.crCuestionariogradodeafectividad();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.Cuestionariogradodeafectividad_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.Fobiasocial01_ID:
                    var Fobiasocial01 = new ServiceBL().ReporteFobiaSocial01(_serviceId, Constants.Fobiasocial01_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_Fobiasocial01 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Fobiasocial01);
                    dt_Fobiasocial01.TableName = "dtFobiasocial01";
                    dsGetRepo.Tables.Add(dt_Fobiasocial01);

                    rp = new Reports.crFobiasocial01();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.Fobiasocial01_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.Fobiasocial02_ID:
                    var Fobiasocial02 = new ServiceBL().ReporteFobiaSocial02(_serviceId, Constants.Fobiasocial02_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_Fobiasocial02 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Fobiasocial02);
                    dt_Fobiasocial02.TableName = "dtFobiasocial02";
                    dsGetRepo.Tables.Add(dt_Fobiasocial02);

                    rp = new Reports.crFobiasocial02();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.Fobiasocial02_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.Testdepersonalldad_ID:
                    var Testdepersonalldad = new ServiceBL().ReporteTestdepersonalldad(_serviceId, Constants.Testdepersonalldad_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_Testdepersonalldad = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Testdepersonalldad);
                    dt_Testdepersonalldad.TableName = "dtTestdepersonalldad";
                    dsGetRepo.Tables.Add(dt_Testdepersonalldad);

                    rp = new Reports.crTestdepersonalldad();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.Testdepersonalldad_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.FobiasocialAdmin_ID:
                    var FobiasocialAdmin = new ServiceBL().ReporteFobiasocialAdmin(_serviceId, Constants.FobiasocialAdmin_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_FobiasocialAdmin = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(FobiasocialAdmin);
                    dt_FobiasocialAdmin.TableName = "dtFobiasocialAdmin";
                    dsGetRepo.Tables.Add(dt_FobiasocialAdmin);

                    rp = new Reports.crFobiasocialAdmin();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.FobiasocialAdmin_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;


                case Constants.Testdefatiga_ID:
                    var Testdefatiga = new ServiceBL().ReporteTestdefatiga(_serviceId, Constants.Testdefatiga_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_Testdefatiga = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Testdefatiga);
                    dt_Testdefatiga.TableName = "dtTestdefatiga";
                    dsGetRepo.Tables.Add(dt_Testdefatiga);

                    rp = new Reports.crTestdefatiga();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.Testdefatiga_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.Maslachestres_ID:
                    var Maslachestres = new ServiceBL().ReporteMaslachestres(_serviceId, Constants.Maslachestres_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_Maslachestres = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Maslachestres);
                    dt_Maslachestres.TableName = "dtMaslachestres";
                    dsGetRepo.Tables.Add(dt_Maslachestres);

                    rp = new Reports.crMaslachestres();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.Maslachestres_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.Testdeansiedad_ID:
                    var Testdeansiedad = new ServiceBL().ReporteTestdeansiedad(_serviceId, Constants.Testdeansiedad_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_Testdeansiedad = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Testdeansiedad);
                    dt_Testdeansiedad.TableName = "dtTestdeansiedad";
                    dsGetRepo.Tables.Add(dt_Testdeansiedad);

                    rp = new Reports.crTestdeansiedad();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.Testdeansiedad_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case Constants.Testdedepresion_ID:
                    var Testdedepresion = new ServiceBL().ReporteTestdedepresion(_serviceId, Constants.Testdedepresion_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_Testdedepresion_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Testdedepresion);
                    dt_Testdedepresion_ID.TableName = "dtTestdedepresion";
                    dsGetRepo.Tables.Add(dt_Testdedepresion_ID);

                    rp = new Reports.crTestdedepresión();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.Testdedepresion_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;



                case Constants.CuestionarioAutoeva_ID:
                    var CuestionarioAutoeva = new ServiceBL().ReporteCuestionarioAutoeva(_serviceId, Constants.CuestionarioAutoeva_ID);

                    dsGetRepo = new DataSet();
                    DataTable dt_CuestionarioAutoeva_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(CuestionarioAutoeva);
                    dt_CuestionarioAutoeva_ID.TableName = "dtCuestionarioAutoeva";
                    dsGetRepo.Tables.Add(dt_CuestionarioAutoeva_ID);

                    rp = new Reports.crCuestionarioAutoeva();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.CuestionarioAutoeva_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;


                case Constants.CUESTIONARIO_ISTAS:
                    var CUESTIONARIO_ISTAS = new ServiceBL().ReporteCuestionarioIstas(_serviceId, Constants.CUESTIONARIO_ISTAS);

                    dsGetRepo = new DataSet();

                    DataTable dt_CUESTIONARIO_ISTAS = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(CUESTIONARIO_ISTAS);
                    dt_CUESTIONARIO_ISTAS.TableName = "dtCuestionario_Istas";
                    dsGetRepo.Tables.Add(dt_CUESTIONARIO_ISTAS);

                    rp = new Reports.crCuestionario_Istas_1();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.CUESTIONARIO_ISTAS + "01.pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();

                    //rp = new Reports.crCuestionario_Istas_2();
                    //rp.SetDataSource(dsGetRepo);
                    //rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    //rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    //objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.CUESTIONARIO_ISTAS + "02.pdf";
                    //_filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    //rp.ExportOptions.DestinationOptions = objDiskOpt;

                    //rp.Export();
                    //rp.Close();
                    break;


                case Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID:
                    var TOXICOLOGICO_COCAINA_MARIHUANA_ID = new ServiceBL().GetReportCocainaMarihuana(_serviceId, Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID);

                    dsGetRepo = new DataSet();

                    DataTable dt_TOXICOLOGICO_COCAINA_MARIHUANA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(TOXICOLOGICO_COCAINA_MARIHUANA_ID);
                    dt_TOXICOLOGICO_COCAINA_MARIHUANA_ID.TableName = "dtAutorizacionDosajeDrogas";
                    dsGetRepo.Tables.Add(dt_TOXICOLOGICO_COCAINA_MARIHUANA_ID);

                    if (pintIdCrystal == 48)
                    {
                        rp = new Reports.crApendice09_Drogas();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID +
                                                  "02" + ".pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;

                        rp.Export();
                        rp.Close();
                    }
                    else
                    {

                        rp = new Reports.crCocainaMarihuana01();
                        rp.SetDataSource(dsGetRepo);
                        rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        objDiskOpt = new DiskFileDestinationOptions();
                        objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID + "01.pdf";
                        _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                        rp.ExportOptions.DestinationOptions = objDiskOpt;

                        rp.Export();
                        rp.Close();
                    }

                    break;

                case Constants.AUDIO_COIMOLACHE:
                    var AUDIO_COIMOLACHE_ID = new ServiceBL().GetAudiometriaCoimolache(_serviceId, Constants.AUDIO_COIMOLACHE);
                    dsGetRepo = new DataSet();
                    DataTable dtAUDIO_COIMOLACHE_ID = BLL.Utils.ConvertToDatatable(AUDIO_COIMOLACHE_ID);
                    dtAUDIO_COIMOLACHE_ID.TableName = "dtAudioCoimo";
                    dsGetRepo.Tables.Add(dtAUDIO_COIMOLACHE_ID);
                    rp = new Reports.crCuestionarioAudioCoimolache();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.AUDIO_COIMOLACHE + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;

                case "N009-ME000000337":
                    var Cusestionario_audiometria = new ServiceBL().GetCustionarioAudiometria(_serviceId, "N009-ME000000337");

                    dsGetRepo = new DataSet();
                    DataTable dtCuestionario_Audio = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Cusestionario_audiometria);
                    dtCuestionario_Audio.TableName = "dtCuestAudio";
                    dsGetRepo.Tables.Add(dtCuestionario_Audio);
                    rp = new Reports.CuestionarioAudiometria();
                    rp.SetDataSource(dsGetRepo);

                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + "N009-ME000000337" + "_01.pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();

                    rp = new Reports.CuestionarioAudiometria02();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + "N009-ME000000337" + "_02.pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;

                    rp.Export();
                    rp.Close();

                    break;


                case "N009-ME000000407":
                    var Carne_Vacuna = new ServiceBL().GetCarneVacuna(_serviceId, "N009-ME000000407");

                    dsGetRepo = new DataSet();
                    DataTable dtCarneVacuna = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Carne_Vacuna);
                    dtCarneVacuna.TableName = "dtCarneVacuna";
                    dsGetRepo.Tables.Add(dtCarneVacuna);
                    rp = new Reports.crCarneDeVacunas();
                    rp.SetDataSource(dsGetRepo);

                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = ruta + serviceId + "-" + "N009-ME000000407" + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    break;
              
                case Constants.INFORME_LABORATORIO_CLINICO:
                    GenerateLaboratorioReport(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + Constants.INFORME_LABORATORIO_CLINICO)));
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + componentId)));
                    break;

                case Constants.FORMATO_ATENCION_INTEGRAL_ADULTO_MAYOR:
                    GenerateAtencionIntegralAdultoMayor(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + Constants.FORMATO_ATENCION_INTEGRAL_ADULTO_MAYOR)));
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + componentId)));
                    break;

                case Constants.FORMATO_ATENCION_INTEGRAL_ADULTO:
                    GenerateAtencionIntegralAdulto(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + Constants.FORMATO_ATENCION_INTEGRAL_ADULTO)));
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + componentId)));
                    break;

                case Constants.FORMATO_ATENCION_INTEGRAL_ADOLESCENTE:
                    GenerateAtencionIntegralAdolescente(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + Constants.FORMATO_ATENCION_INTEGRAL_ADOLESCENTE)));
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + componentId)));
                    break;

                case Constants.FORMATO_ATENCION_NINIO:
                    GenerateConsultaMedicaNinio(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + Constants.FORMATO_ATENCION_NINIO)));
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + componentId)));
                    break;
                case Constants.TOXICOLOGICO_COCAINA_MARIHUANA_T:
                    GenerateTOXICOLOGICO_COCAINA_MARIHUANA_TODOS(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + Constants.TOXICOLOGICO_COCAINA_MARIHUANA_T)));
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + componentId)));
                    break;
                case Constants.ATENCION_INTEGRAL:
                    GenerateAtencionIntegral(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + Constants.ATENCION_INTEGRAL)));
                    _filesNameToMerge.Add(string.Format("{0}.pdf", Path.Combine(ruta, _serviceId + "-" + componentId)));
                    break;
                default:
                    break;
            }
        }

        private void GenerateLaboratorioReport(string pathFile)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);

            LaboratorioReport.CreateLaboratorioReport(filiationData, serviceComponents, MedicalCenter, pathFile);
        }
        
    }
}
