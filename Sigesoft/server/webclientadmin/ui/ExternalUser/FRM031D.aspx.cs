using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BLL;
using System.Data;
using System.IO;
using System.Diagnostics;
using NetPdf;
using Sigesoft.Server.WebClientAdmin.BE;

namespace Sigesoft.Server.WebClientAdmin.UI.ExternalUser
{
    public partial class FRM031D : System.Web.UI.Page
    {
        private List<string> _filesNameToMerge = new List<string>();
        private MergeExPDF _mergeExPDF = new MergeExPDF();
        PacientBL _pacientBL = new PacientBL();
        HistoryBL _historyBL = new HistoryBL();
        ServiceBL _serviceBL = new ServiceBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> _filesNameToMerge = new List<string>();

            List<MyListWeb> ListaServicios = (List<MyListWeb>)Session["objLista"];
            var ListaExamenes = (List<string>)Session["objListaExamenes"];

          

            for (int i = 0; i < ListaServicios.Count(); i++)
            {
             
                if (ListaExamenes.Count() == 1)
                {
                    var ruta = Server.MapPath("files/EM" + ListaServicios[i].IdServicio + ".pdf");
                    _filesNameToMerge.Add(ruta);

                    GeneratePDFOnlyOne(ListaExamenes[0], ruta, ListaServicios[i].IdServicio, ListaServicios[i].IdPaciente, ListaServicios[i].EmpresaCliente);
     
                }
                else
                {
                    //_filesNameToMerge = new List<string>();
                    foreach (var item in ListaExamenes)
                    {
                        var ruta1 = Server.MapPath("files/EX-" + ListaServicios[i].IdServicio + "-" + item.ToString() + ".pdf");
                        _filesNameToMerge.Add(ruta1);

                        GeneratePDF(item, ruta1, ListaServicios[i].IdServicio, ListaServicios[i].IdPaciente, ListaServicios[i].EmpresaCliente);
                    }
                }    
            }
            _mergeExPDF.FilesName = _filesNameToMerge;
           
            string Dif = Guid.NewGuid().ToString();
            string NewPath = Server.MapPath("files/" + Dif + ".pdf");

            _mergeExPDF.DestinationFile = NewPath;
            _mergeExPDF.Execute();
            Session["EliminarArchivo"] = NewPath;
            ShowPdf1.FilePath = "files/" + Dif + ".pdf";
           
          
        }

        private void GeneratePDFOnlyOne(string componentId, string RutaTemporal, string ServicioId, string PacienteId, string EmpresaCliente)
        {
            switch (componentId)
            {
                case Constants.INFORME_ANEXO_312:
                    GenerateAnexo312(RutaTemporal, ServicioId, PacienteId, EmpresaCliente);
                    break;
                case Constants.INFORME_FICHA_MEDICA_TRABAJADOR:
                    GenerateInformeMedicoTrabajador(RutaTemporal, ServicioId, PacienteId, EmpresaCliente);
                    break;
                case Constants.INFORME_ANEXO_7C:
                    GenerateAnexo7C(RutaTemporal, ServicioId, PacienteId, EmpresaCliente);
                    break;
                case Constants.INFORME_CLINICO:
                    GenerateInformeExamenClinico(RutaTemporal, ServicioId, PacienteId, EmpresaCliente);
                    break;
                case Constants.INFORME_LABORATORIO_CLINICO:
                    GenerateLaboratorioReport(RutaTemporal, ServicioId, PacienteId, EmpresaCliente);
                    break;
                default:
                    break;
            }
        }

        private void GenerateAnexo312(string pathFile, string ServicioId, string PacienteId, string EmpresaCliente)
        {
            var filiationData = _pacientBL.GetPacientReportEPS(ServicioId);
            var _listAtecedentesOcupacionales = _historyBL.GetHistoryReport(PacienteId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(PacienteId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(PacienteId);
            var _DataService = _serviceBL.GetServiceReport(ServicioId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(PacienteId);

            var Antropometria = _serviceBL.ValoresComponente(ServicioId, Constants.ANTROPOMETRIA_ID);
            var FuncionesVitales = _serviceBL.ValoresComponente(ServicioId, Constants.FUNCIONES_VITALES_ID);
            var ExamenFisico = _serviceBL.ValoresComponente(ServicioId, Constants.EXAMEN_FISICO_ID);
            var Oftalmologia = _serviceBL.ValoresComponente(ServicioId, Constants.OFTALMOLOGIA_ID);
            var Psicologia = _serviceBL.ValoresExamenComponete(ServicioId, Constants.PSICOLOGIA_ID, 195);
            var OIT = _serviceBL.ValoresExamenComponete(ServicioId, Constants.OIT_ID, 211);

            var RX = _serviceBL.ValoresExamenComponete(ServicioId, Constants.RX_TORAX_ID, 211);
            var RX1 = _serviceBL.ValoresExamenComponete(ServicioId, Constants.RX_TORAX_ID, 135);
            var Laboratorio = _serviceBL.ValoresComponente(ServicioId, Constants.INFORME_LABORATORIO_ID);
            //var Audiometria = _serviceBL.ValoresComponente(ServicioId, Constants.AUDIOMETRIA_ID);
            var Audiometria = _serviceBL.GetDiagnosticForAudiometria(ServicioId, Constants.AUDIOMETRIA_ID);
            var Espirometria = _serviceBL.ValoresExamenComponete(ServicioId, Constants.ESPIROMETRIA_ID, 210);
            var _DiagnosticRepository = _serviceBL.GetServiceDisgnosticsReports(ServicioId);
            var _Recomendation = _serviceBL.GetServiceRecommendationByServiceId(ServicioId);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(ServicioId);
            var ValoresDxLab = _serviceBL.ValoresComponenteAMC(ServicioId, 1);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            //var TestIhihara = _serviceBL.ValoresComponente(ServicioId, Constants.TEST_ISHIHARA_ID);
            //var TestEstereopsis = _serviceBL.ValoresComponente(ServicioId, Constants.TEST_ESTEREOPSIS_ID);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(ServicioId);


            FichaMedicaOcupacional312.CreateFichaMedicalOcupacional312Report(_DataService,
                     filiationData, _listAtecedentesOcupacionales, _listaPatologicosFamiliares,
                     _listMedicoPersonales, _listaHabitoNocivos,
                     Audiometria,// Psicologia, OIT, RX,  , Espirometria,
                     _DiagnosticRepository, _Recomendation, _ExamenesServicio, ValoresDxLab, MedicalCenter,//TestIhihara,TestEstereopsis,
                     serviceComponents, pathFile);
        }

        private void GenerateInformeMedicoTrabajador(string pathFile, string ServicioId, string PacienteId, string EmpresaCliente)
        {
            var filiationData = _pacientBL.GetPacientReportEPS(ServicioId);
            var personMedicalHistory = _historyBL.GetPersonMedicalHistoryReport(PacienteId);
            var noxiousHabit = _historyBL.GetNoxiousHabitsReport(PacienteId);
            var familyMedicalAntecedent = _historyBL.GetFamilyMedicalAntecedentsReport(PacienteId);
            var anamnesis = _serviceBL.GetAnamnesisReport(ServicioId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(ServicioId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(ServicioId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            ReportPDF.CreateMedicalReportForTheWorker(filiationData,
                                            personMedicalHistory,
                                            noxiousHabit,
                                            familyMedicalAntecedent,
                                            anamnesis,
                                            serviceComponents,
                                            diagnosticRepository,
                                            EmpresaCliente,
                                            MedicalCenter,
                                            pathFile);

        }

        private void GenerateAnexo7C(string pathFile, string ServicioId, string PacienteId, string EmpresaCliente)
        {
            var _DataService = _serviceBL.GetServiceReport(ServicioId);
            var filiationData = _pacientBL.GetPacientReportEPS(ServicioId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(PacienteId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(PacienteId);
            var _Valores = _serviceBL.GetServiceComponentsReport(ServicioId);
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(PacienteId);
            var _PiezasCaries = _serviceBL.GetCantidadCaries(ServicioId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_CARIES_ID);
            var _PiezasAusentes = _serviceBL.GetCantidadAusentes(ServicioId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_AUSENTES_ID);
            //var CuadroVacio = Sigesoft.Common.Utils.FileToByteArray(@"D:\RepSIGSO_v1.0\dev\src\server\webclientadmin\ui\images\icons\CuadradoVacio.png"); // Sigesoft.Common.Utils.BitmapToByteArray(Sigesoft.Server.WebClientAdmin.UI.Common.);
            //var CuadroCheck = Sigesoft.Common.Utils.FileToByteArray(@"D:\RepSIGSO_v1.0\dev\src\server\webclientadmin\ui\images\icons\CuadradoCheck.png");//Sigesoft.Common.Utils.BitmapToByteArray(Sigesoft.Node.WinClient.UI.Resources.CuadradoCheck);
            var CuadroVacio = Sigesoft.Common.Utils.FileToByteArray(Server.MapPath(@"~\images\icons\CuadradoVacio.png"));
            var CuadroCheck = Sigesoft.Common.Utils.FileToByteArray(Server.MapPath(@"~\images\icons\CuadradoCheck.png"));
            var Pulmones = Sigesoft.Common.Utils.FileToByteArray(Server.MapPath(@"~\images\icons\MisPulmones.jpg"));
            
            //var Pulmones = Sigesoft.Common.Utils.FileToByteArray(@"D:\RepSIGSO_v1.0\dev\src\server\webclientadmin\ui\images\icons\MisPulmones.jpg"); //Sigesoft.Common.Utils.BitmapToByteArray(Sigesoft.Node.WinClient.UI.Resources.MisPulmones);
            var Audiometria = _serviceBL.ValoresComponenteOdontogramaValue1(ServicioId, Constants.AUDIOMETRIA_ID);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(ServicioId);

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            ReportPDF.CreateAnexo7C(_DataService,filiationData, _Valores, _listMedicoPersonales,
                                    _listaPatologicosFamiliares, _listaHabitoNocivos,
                                    CuadroVacio, CuadroCheck, Pulmones, _PiezasCaries,
                                    _PiezasAusentes, Audiometria, diagnosticRepository, MedicalCenter,
                                    pathFile);

        }


        private void GeneratePDF(string componentId, string RutaTemporal, string ServicioId, string PacienteId, string EmpresaCliente)
        {
            switch (componentId)
            {
                case Constants.INFORME_ANEXO_312:
                    GenerateAnexo312(RutaTemporal, ServicioId, PacienteId, EmpresaCliente);
                    _filesNameToMerge.Add(RutaTemporal);
                    break;
                case Constants.INFORME_FICHA_MEDICA_TRABAJADOR:
                    GenerateInformeMedicoTrabajador(RutaTemporal, ServicioId, PacienteId, EmpresaCliente);
                    _filesNameToMerge.Add(RutaTemporal);
                    break;
                case Constants.INFORME_ANEXO_7C:
                    GenerateAnexo7C(RutaTemporal, ServicioId, PacienteId, EmpresaCliente);
                    _filesNameToMerge.Add(RutaTemporal);
                    break;
                case Constants.INFORME_CLINICO:
                    GenerateInformeExamenClinico(RutaTemporal, ServicioId, PacienteId, EmpresaCliente);
                    _filesNameToMerge.Add(RutaTemporal);
                    break;
                case Constants.INFORME_LABORATORIO_CLINICO:
                    GenerateLaboratorioReport(RutaTemporal, ServicioId, PacienteId, EmpresaCliente);
                    _filesNameToMerge.Add(RutaTemporal);
                    break;
                default:
                    break;
            }
        }

        private void GenerateInformeExamenClinico(string pathFile, string ServicioId, string PacienteId, string EmpresaCliente)
        {
            var filiationData = _pacientBL.GetPacientReportEPS(ServicioId);
            var personMedicalHistory = _historyBL.GetPersonMedicalHistoryReport(PacienteId);
            var noxiousHabit = _historyBL.GetNoxiousHabitsReport(PacienteId);
            var familyMedicalAntecedent = _historyBL.GetFamilyMedicalAntecedentsReport(PacienteId);
            var anamnesis = _serviceBL.GetAnamnesisReport(ServicioId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(ServicioId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(ServicioId);
            var doctoPhisicalExam = _serviceBL.GetDoctoPhisicalExam(ServicioId);

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

            ReportPDF.CreateMedicalReportForExamenClinico(filiationData,
                                            personMedicalHistory,
                                            noxiousHabit,
                                            familyMedicalAntecedent,
                                            anamnesis,
                                            serviceComponents,
                                            diagnosticRepository,
                                            EmpresaCliente,
                                            MedicalCenter,
                                            pathFile,
                                            doctoPhisicalExam);


        }

        private void GenerateLaboratorioReport(string pathFile, string ServicioId, string PacienteId, string EmpresaCliente)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(ServicioId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(ServicioId);

            LaboratorioReport.CreateLaboratorioReport(filiationData, serviceComponents, MedicalCenter, pathFile);
        }
    }
}