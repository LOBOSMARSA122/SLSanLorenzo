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
using System.Web.Configuration;
using FineUI;
using System.Text.RegularExpressions;

namespace Sigesoft.Server.WebClientAdmin.UI.Consultorios
{
    public partial class frmVisorReporte : System.Web.UI.Page
    {
        string _ruta;
        DataSet dsGetRepo = null;
        ReportDocument rp;
        DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
        List<string> _filesNameToMerge = new List<string>();
        ServiceBL _serviceBL = new ServiceBL();
        PacientBL _pacientBL = new PacientBL();
        HistoryBL _historyBL = new HistoryBL();
        
        protected void Page_Load(object sender, EventArgs e)
        {

            _ruta = WebConfigurationManager.AppSettings["rutaReportes"];
            string path;

            string Mode = Request.QueryString["Mode"].ToString();

            if (Mode == "Psico")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N002-ME000000033";
                GeneratePsico(_ruta, Session["ServiceId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Psicología.pdf";
            }
            if (Mode == "AudioCI")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + Constants.AUDIOMETRIA_ID;
                GenerateAudimetriaCI(_ruta, Session["ServiceId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Audiometría.pdf";
                
            }
            if (Mode == "Oftalmo")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N005-ME000000028";
                GenerateOftalmologia(string.Format("{0}.pdf", Path.Combine(path)), Session["ServiceId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Oftalmología.pdf";
            }
            if (Mode == "OftalmoCI")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + Constants.APENDICE_N_2_EVALUACION_OFTALMOLOGICA_YANACOCHA_ID;
                GenerateOftalmologiaCI(_ruta, Session["ServiceId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Oftalmología.pdf";
            }

            if (Mode == "OdontoCI")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + Constants.ODONTOGRAMA_ID;
                GenerateOdontogramaCI(path, Session["ServiceId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Odontología.pdf";
            }
            if (Mode == "RX")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N002-ME000000032";
                GenerateRX(_ruta, Session["ServiceId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "RayosX.pdf";
            }
            if (Mode == "OIT")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N009-ME000000062";
                GenerateRayosX(_ruta, Session["ServiceId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "RayosX.pdf";
            }
            if (Mode == "Lab")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + Constants.INFORME_LABORATORIO_CLINICO;
                GenerateLaboratorio(string.Format("{0}.pdf", Path.Combine(path)), Session["ServiceId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "INFORME_LABORATORIO_CLINICO.pdf";
            }
            if (Mode == "Cardio")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N002-ME000000025";
                GenerateElectro(_ruta, Session["ServiceId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Cardiología.pdf";
            }
            if (Mode == "EKG")
            {
                path = _ruta;
                GenerateEKG(string.Format("{0}.pdf", Path.Combine(path)), Session["ServiceId"].ToString(), Session["PersonId"].ToString());                   
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "apendiceNro5.pdf";
            }
            if (Mode == "312")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + Constants.INFORME_ANEXO_312;
                GenerateAnexo312(string.Format("{0}.pdf", Path.Combine(path)), Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Informe_312.pdf";
            }
            if (Mode == "Anexo16")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + Constants.INFORME_ANEXO_16_COIMOLACHE;
                GenerateAnexo16GoldField(string.Format("{0}.pdf", Path.Combine(path)), Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Informe_Anexo16.pdf";
            }
            if (Mode == "AlturaCI")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N005-ME000000117";
                GenerateAltura18_CI(_ruta, Session["ServiceId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Altura.pdf";
            }
            if (Mode == "AlturaEstructural")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + Constants.ALTURA_ESTRUCTURAL_ID;
                GenerateAlturaEstructural(path, Session["ServiceId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "AlturaEstructural.pdf";
            }
            if (Mode == "OsteoCI")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N005-ME000000046";
                GenerateOsteomuscular(_ruta, Session["ServiceId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "OsteoMuscular.pdf";
            }
            if (Mode == "TamizajeCI")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N005-ME000000116";
                GenerateTamizaje(_ruta, Session["ServiceId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Tamizaje.pdf";
            }
            if (Mode == "7D")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + Constants.ALTURA_7D_ID;
                Generate7D(string.Format("{0}.pdf", Path.Combine(path)), Session["ServiceId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "7D.pdf";
            }

            if (Mode == "Espiro")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + Constants.ESPIROMETRIA_ID;
                GenerateEspirometria(_ruta, Session["ServiceId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Espirometría.pdf";
            }
            if (Mode == "Sintomatico")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N009-ME000000116";
                GenerateSintomatico(_ruta, Session["ServiceId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Sintomático_Respiratorio.pdf";
            }
            if (Mode == "Certificado")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "CAP";
                GenerateCAP(_ruta, Session["ServiceId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Certificado_Aptitud.pdf";
            }
            if (Mode == "InformeCI")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + Constants.INFORME_FICHA_MEDICA_TRABAJADOR_CI;
                GenerateInformeMedicoTrabajadorInternacional(string.Format("{0}.pdf", Path.Combine(path)), Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Informe_Trabajador.pdf";
            }

            if (Mode == "Consolidado")
            {
                 var servicios = (List<MyListWeb>)Session["objListaCambioFecha"];

                 if (servicios == null)
                 {
                      lblmensaje.Text = "no ha seleccionado ningún servicio";
                         return;
                 }
                 if (servicios.Count == 1)
                 {
                     if (servicios[0].i_StatusLiquidation == null)
                     {
                         lblmensaje.Text = "el servicio: " + servicios[0].IdServicio + " no ha generado sus reportes";
                         return;
                     }

                     path = _ruta + servicios[0].IdServicio;

                     System.IO.File.Copy(_ruta + servicios[0].IdServicio.ToString() + ".pdf", Server.MapPath("files/" + servicios[0].IdServicio.ToString() + ".pdf"), true);

                     ShowPdf1.FilePath = @"files\" + servicios[0].IdServicio.ToString() + ".pdf";
                 }
                 else
                 {
                     List<string> ListaRutas = new List<string>();
                     foreach (var item in servicios)
                     {
                         if (item.i_StatusLiquidation == null)
                         {
                             lblmensaje.Text = "el servicio: " + item.IdServicio + " no ha generado sus reportes";
                             //ShowPdf1.FilePath = @"files\nada.pdf";
                             return;
                         }
                         path = _ruta + item.IdServicio+ ".pdf";
                         ListaRutas.Add(path);
                     }

                     MergeExPDF _mergeExPDF = new MergeExPDF();
                     _mergeExPDF.FilesName = ListaRutas;
                     _mergeExPDF.DestinationFile = _ruta + servicios[0].IdServicio + "_CONSOLIDADO.pdf";
                     _mergeExPDF.Execute();

                     System.IO.File.Copy(_ruta + servicios[0].IdServicio + "_CONSOLIDADO.pdf", Server.MapPath("files/" + servicios[0].IdServicio + "_CONSOLIDADO.pdf"), true);

                     ShowPdf1.FilePath = @"files\" + servicios[0].IdServicio + "_CONSOLIDADO.pdf";
                 }
            }

        }

        private void GenerateAlturaEstructural(string _ruta, string _serviceId)
        {
            var servicesId9 = new List<string>();
            servicesId9.Add(_serviceId);
            var componentReportId9 = new ServiceBL().ObtenerIdsParaImportacionExcel(servicesId9, 7);
            var dataListForReport = new PacientBL().GetAlturaEstructural(_serviceId, Constants.ALTURA_ESTRUCTURAL_ID, Constants.ALTURA_ESTRUCTURAL_ID);
            Session["NombreTrabajador"] = componentReportId9[0].Paciente.Replace(" ","_");
            dsGetRepo = new DataSet();

            DataTable dt_ALTURA_ESTRUCTURAL_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt_ALTURA_ESTRUCTURAL_ID.TableName = "dtAlturaEstructural";

            dsGetRepo.Tables.Add(dt_ALTURA_ESTRUCTURAL_ID);

            rp = new Sigesoft.Node.WinClient.UI.Reports.crAlturaMayor();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();
            System.IO.File.Copy(_ruta + ".pdf", Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "AlturaEstructural" + ".pdf"), true);
        }

        private void GenerateOftalmologiaCI(string _ruta, string _serviceId)
        {
            var component = "N002-ME000000028";
            var OFTALMO_ANTIGUO = new ServiceBL().ReportOftalmologia_CI(_serviceId, component);
            if (OFTALMO_ANTIGUO.Count == 0)
            {
                component = "N009-ME000000028";
                OFTALMO_ANTIGUO = new ServiceBL().ReportOftalmologia_CI(_serviceId, component);
            }
            //Session["NombreTrabajador"] = OFTALMO_ANTIGUO[0]..Replace(" ", "_");
            dsGetRepo = new DataSet();
            DataTable dt_OFTALMO_ANTIGUO = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(OFTALMO_ANTIGUO);
            dt_OFTALMO_ANTIGUO.TableName = "dtOftalmologia";
            dsGetRepo.Tables.Add(dt_OFTALMO_ANTIGUO);


            rp = new Sigesoft.Node.WinClient.UI.Reports.crOftalmologia();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + Constants.OFTALMOLOGIA_ID + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();

            System.IO.File.Copy(_ruta + _serviceId + "-" + Constants.OFTALMOLOGIA_ID + ".pdf", Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Oftalmología" + ".pdf"), true);
        }

        private void GenerateEKG(string pathFile, string _serviceId, string _pacienteId)
        {
            var ListaComponentes = _serviceBL.ObtenerComponetesPorServicio(Session["ServiceId"].ToString());
            foreach (var item in ListaComponentes)
            {
                if (item.ComponentId == Constants.APENDICE_ID)
                {
                    var APENDICE_ID = new ServiceBL().GetReportEstudioElectrocardiografico(_serviceId, Constants.APENDICE_ID);
                    Session["NombreTrabajador"] = Regex.Replace(APENDICE_ID[0].DatosPaciente, " ", "_");
                    dsGetRepo = new DataSet();
                    DataTable dtAPENDICE_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(APENDICE_ID);
                    dtAPENDICE_ID.TableName = "dtEstudioElectrocardiografico";
                    dsGetRepo.Tables.Add(dtAPENDICE_ID);
                    rp = new Sigesoft.Node.WinClient.UI.Reports.crApendice05_EKG();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = pathFile + _serviceId + "-" + Constants.APENDICE_ID + "_02" + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    System.IO.File.Copy(pathFile + _serviceId + "-" + Constants.APENDICE_ID + "_02" + ".pdf", Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "apendiceNro5.pdf"), true);

                    
                }
                if (item.ComponentId == Constants.ELECTRO_GOLD)
                {
                    var electroGold = new ServiceBL().GetReportElectroGold(_serviceId, Constants.ELECTRO_GOLD);
                    Session["NombreTrabajador"] = Regex.Replace(electroGold[0].DatosPaciente, " ", "_");
                    dsGetRepo = new DataSet();
                    DataTable dt_ELECTRO_GOLD = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(electroGold);
                    dt_ELECTRO_GOLD.TableName = "dtEstudioElectrocardiografico";
                    dsGetRepo.Tables.Add(dt_ELECTRO_GOLD);

                    rp = new Sigesoft.Node.WinClient.UI.Reports.crInformeElectroCardiografiaoGoldField_EKG();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = pathFile + _serviceId + "-" + Constants.ELECTRO_GOLD + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    System.IO.File.Copy(pathFile + _serviceId + "-" + Constants.ELECTRO_GOLD + ".pdf", Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "apendiceNro5.pdf"), true);

                }
                if (item.ComponentId == Constants.ELECTROCARDIOGRAMA_ID)
                {
                    var ELECTROCARDIOGRAMA_ID = new ServiceBL().GetReportEstudioElectrocardiografico(_serviceId, Constants.ELECTROCARDIOGRAMA_ID);
                    Session["NombreTrabajador"] = Regex.Replace(ELECTROCARDIOGRAMA_ID[0].DatosPaciente, " ", "_");
                    dsGetRepo = new DataSet();
                    DataTable dt_ELECTROCARDIOGRAMA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ELECTROCARDIOGRAMA_ID);
                    dt_ELECTROCARDIOGRAMA_ID.TableName = "dtEstudioElectrocardiografico";
                    dsGetRepo.Tables.Add(dt_ELECTROCARDIOGRAMA_ID);
                    rp = new Sigesoft.Node.WinClient.UI.Reports.crEstudioElectrocardiografico();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = pathFile + _serviceId + "-" + Constants.ELECTROCARDIOGRAMA_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                    System.IO.File.Copy(pathFile + _serviceId + "-" + Constants.ELECTROCARDIOGRAMA_ID + ".pdf", Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "apendiceNro5.pdf"), true);
                }
            }
        }

        private void GeneratePsico(string _ruta, string _serviceId)
        {
            var PSICOLOGIA_ID = new PacientBL().GetFichaPsicologicaOcupacional(_serviceId);
            dsGetRepo = new DataSet();
            DataTable dt_PSICOLOGIA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(PSICOLOGIA_ID);
            dt_PSICOLOGIA_ID.TableName = "InformePsico";
            dsGetRepo.Tables.Add(dt_PSICOLOGIA_ID);
            rp = new Sigesoft.Node.WinClient.UI.Reports.InformePsicologicoOcupacional();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + Constants.PSICOLOGIA_ID + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();

            var HISTORIA_CLINICA_PSICOLOGICA_ID = new ServiceBL().GetHistoriaClinicaPsicologica(_serviceId, Constants.HISTORIA_CLINICA_PSICOLOGICA_ID);
            dsGetRepo = new DataSet();
            DataTable dt_HISTORIA_CLINICA_PSICOLOGICA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(HISTORIA_CLINICA_PSICOLOGICA_ID);
            dt_HISTORIA_CLINICA_PSICOLOGICA_ID.TableName = "dtHistoriaClinicaPsicologica";
            dsGetRepo.Tables.Add(dt_HISTORIA_CLINICA_PSICOLOGICA_ID);

            rp = new Sigesoft.Node.WinClient.UI.Reports.crApendice04_Psico_01();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + Constants.HISTORIA_CLINICA_PSICOLOGICA_ID + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();

            rp = new Sigesoft.Node.WinClient.UI.Reports.crApendice04_Psico_02();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + Constants.HISTORIA_CLINICA_PSICOLOGICA_ID + "2" + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();

            Session["NombreTrabajador"] = PSICOLOGIA_ID[0].v_Pacient.Replace(" ", "_");
            MergeExPDF _mergeExPDF = new MergeExPDF();
            var x = ((List<string>)Session["filesNameToMerge"]).ToList();
            _mergeExPDF.FilesName = x;
            _mergeExPDF.DestinationFile = _ruta + Session["ServiceId"].ToString() + "-N002-ME000000033_MERGE.pdf";
            objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Psicología" + ".pdf");
            _mergeExPDF.Execute();
            System.IO.File.Copy(_ruta + Session["ServiceId"].ToString() + "-N002-ME000000033_MERGE.pdf", Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Psicología" + ".pdf"), true);
        }

        private void GenerateAudimetriaCI(string _ruta, string _serviceId)
        {
            var serviceBL = new ServiceBL();
            var dsAudiometria = new DataSet();
            var dxList_CI = serviceBL.GetDiagnosticRepositoryByComponent(_serviceId, Constants.AUDIOMETRIA_ID);
            if (dxList_CI.Count == 0)
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
                foreach (var item in dxList_CI)
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


            var recom_CI = dxList_CI.SelectMany(s1 => s1.Recomendations).ToList();
            if (recom_CI.Count == 0)
            {
                Sigesoft.Node.WinClient.BE.RecomendationList oRecomendationList = new Sigesoft.Node.WinClient.BE.RecomendationList();
                List<Sigesoft.Node.WinClient.BE.RecomendationList> Lista = new List<Sigesoft.Node.WinClient.BE.RecomendationList>();
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
                var dtReco = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(recom_CI);
                dtReco.TableName = "dtRecomendation";
                dsAudiometria.Tables.Add(dtReco);
            }

            //-------******************************************************************************************

            var audioUserControlList_CI = serviceBL.ReportAudiometriaUserControl(_serviceId, Constants.AUDIOMETRIA_ID);
            var audioCabeceraList_CI = serviceBL.ReportAudiometria_CI(_serviceId, Constants.AUDIOMETRIA_ID);
            var dtAudiometriaUserControl_CI = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(audioUserControlList_CI);
            var dtCabecera_CI = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(audioCabeceraList_CI);
            dtCabecera_CI.TableName = "dtAudiometria";
            dtAudiometriaUserControl_CI.TableName = "dtAudiometriaUserControl";
            dsAudiometria.Tables.Add(dtCabecera_CI);
            dsAudiometria.Tables.Add(dtAudiometriaUserControl_CI);
            rp = new Sigesoft.Node.WinClient.UI.Reports.crFichaAudiometria();
            rp.SetDataSource(dsAudiometria);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + "N002-ME000000005" + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            Session["NombreTrabajador"] = audioCabeceraList_CI[0].v_FullPersonName.Replace(" ","_");
            objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Audiometría" + ".pdf");
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();

            rp.Close();
        }

        private void GenerateOftalmologia(string _path, string _serviceId)
        {
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter_Logo();//_serviceBL.GetInfoMedicalCenter();//
            var filiationData = _pacientBL.GetPacientReportEPS_Oftalmo(_serviceId);//_pacientBL.GetPacientReportEPS(_serviceId);//
            var serviceComponents = _serviceBL.GetServiceComponentsReport_Oftalmo(_serviceId);//_serviceBL.GetServiceComponentsReport(_serviceId);//
            var datosP = _pacientBL.DevolverDatosPaciente_Oftalmo(_serviceId);//_pacientBL.DevolverDatosPaciente(_serviceId);//
            //var _DataService = _serviceBL.GetServiceReport(_serviceId);
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.Oftalmología, _serviceId);
            var _ExamenesServicio = serviceComponents;//_serviceBL.GetServiceComponentsReport(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            ApendiceN2_Evaluacion_Oftalmologica_Yanacocha.CreateApendiceN2_Evaluacion_Oftalmologica_Yanacocha(filiationData, serviceComponents, MedicalCenter, datosP, _path, datosGrabo, _ExamenesServicio, diagnosticRepository);
            Session["NombreTrabajador"] = datosP.Trabajador.Replace(" ", "_");
            System.IO.File.Copy(_path,Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Oftalmología" + ".pdf"),true);
        }

        private void GenerateOdontogramaCI(string _path, string _serviceId)
        {
            var Path1 = Request.ApplicationPath;
            var ODONTOGRAMA_ID = new ServiceBL().ReportOdontograma(_serviceId, Constants.ODONTOGRAMA_ID, Path1);
            Session["NombreTrabajador"] = ODONTOGRAMA_ID[0].Trabajador.Replace(" ","_");
            dsGetRepo = new DataSet();
            DataTable dt_ODONTOGRAMA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ODONTOGRAMA_ID);
            dt_ODONTOGRAMA_ID.TableName = "dtOdontograma";
            dsGetRepo.Tables.Add(dt_ODONTOGRAMA_ID);
            rp = new Sigesoft.Node.WinClient.UI.Reports.crOdontograma();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _path + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();
            System.IO.File.Copy(_path + ".pdf", Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Odontología" + ".pdf"), true);      
        }

        private void GenerateRX(string _ruta, string _serviceId)
        {
            var RX_TORAX_ID = new ServiceBL().ReportRadiologico(_serviceId, Constants.RX_TORAX_ID);
            dsGetRepo = new DataSet();
            DataTable dt_RX_TORAX_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(RX_TORAX_ID);
            dt_RX_TORAX_ID.TableName = "dtRadiologico";
            dsGetRepo.Tables.Add(dt_RX_TORAX_ID);
            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crInformeRadiologico();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + Constants.RX_TORAX_ID + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            Session["NombreTrabajador"] = RX_TORAX_ID[0].Paciente.Replace(" ", "_");
            objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "RayosX" + ".pdf");
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();
        }

        private void GenerateRayosX(string _ruta, string _serviceId)
        {
            var ListaComponentes = _serviceBL.ObtenerComponetesPorServicio(_serviceId);
            foreach (var item in ListaComponentes)
            {
                if (item.ComponentId == Constants.OIT_ID)
                {
                    var OIT_ID = new ServiceBL().ReportInformeRadiografico(_serviceId, Constants.OIT_ID);
                    dsGetRepo = new DataSet();
                    DataTable dt_OIT_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(OIT_ID);
                    dt_OIT_ID.TableName = "dtInformeRadiografico";
                    dsGetRepo.Tables.Add(dt_OIT_ID);
                    rp = new Sigesoft.Node.WinClient.UI.Reports.crApendice06_OIT();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + Constants.OIT_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    Session["NombreTrabajador"] = OIT_ID[0].Nombre.Replace(" ", "_");
                    objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "RayosX" + ".pdf");
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                }
                if (item.ComponentId == Constants.RX_TORAX_ID)
                {
                    var RX_TORAX_ID = new ServiceBL().ReportRadiologico(_serviceId, Constants.RX_TORAX_ID);
                    dsGetRepo = new DataSet();
                    DataTable dt_RX_TORAX_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(RX_TORAX_ID);
                    dt_RX_TORAX_ID.TableName = "dtRadiologico";
                    dsGetRepo.Tables.Add(dt_RX_TORAX_ID);
                    rp = new Sigesoft.Node.WinClient.UI.Reports.crInformeRadiologico();
                    rp.SetDataSource(dsGetRepo);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + Constants.RX_TORAX_ID + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    Session["NombreTrabajador"] = RX_TORAX_ID[0].Paciente.Replace(" ", "_");
                    objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "RayosX" + ".pdf");
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                }
            }
        }

        private void GenerateLaboratorio(string pathFile, string _serviceId)
        {          
            PacientBL _pacientBL = new PacientBL();
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter_Logo();//_serviceBL.GetInfoMedicalCenter();//
            var filiationData = _pacientBL.GetPacientReportEPS_Lab(_serviceId);//_pacientBL.GetPacientReportEPS(_serviceId);//
            var serviceComponents = _serviceBL.GetServiceComponentsReport_Lab(_serviceId);//_serviceBL.GetServiceComponentsReport(_serviceId);//
            Session["NombreTrabajador"] = filiationData.Trabajador.Replace(" ", "_");
            LaboratorioReport.CreateLaboratorioReport(filiationData, serviceComponents, MedicalCenter, pathFile);
            System.IO.File.Copy(pathFile, Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "INFORME_LABORATORIO_CLINICO" + ".pdf"), true);
        }

        private void GenerateElectro(string _ruta, string _serviceId)
        {
            var ELECTROCARDIOGRAMA_ID = new ServiceBL().GetReportEstudioElectrocardiografico(_serviceId, Constants.ELECTROCARDIOGRAMA_ID);
            dsGetRepo = new DataSet();
            DataTable dt_ELECTROCARDIOGRAMA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ELECTROCARDIOGRAMA_ID);
            dt_ELECTROCARDIOGRAMA_ID.TableName = "dtEstudioElectrocardiografico";
            dsGetRepo.Tables.Add(dt_ELECTROCARDIOGRAMA_ID);
            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crEstudioElectrocardiografico();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + Constants.ELECTROCARDIOGRAMA_ID + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            Session["NombreTrabajador"] = ELECTROCARDIOGRAMA_ID[0].DatosPaciente.Replace(" ", "_");
            objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Cardiología" + ".pdf");
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();
        }

        private void GenerateAnexo312(string pathFile, string _serviceId, string _pacienteId)
        {
            PacientBL _pacientBL = new PacientBL();
            HistoryBL _historyBL = new HistoryBL();
            Session["ValidacionReporte312"] = true;
            var filiationData = _pacientBL.GetPacientReportEPS(_serviceId);
            var _listAtecedentesOcupacionales = _historyBL.GetHistoryReport(_pacienteId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacienteId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacienteId);
            var _DataService = _serviceBL.GetServiceReport(_serviceId);
            if (_DataService == null)
            {
                Session["ValidacionReporte312"] = false;
                return;
            }
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacienteId);

            var Antropometria = _serviceBL.ValoresComponente(_serviceId, Constants.ANTROPOMETRIA_ID);
            var FuncionesVitales = _serviceBL.ValoresComponente(_serviceId, Constants.FUNCIONES_VITALES_ID);
            var ExamenFisico = _serviceBL.ValoresComponente(_serviceId, Constants.EXAMEN_FISICO_ID);
            var Oftalmologia = _serviceBL.ValoresComponente(_serviceId, "N005-ME000000028");
            var Psicologia = _serviceBL.ValoresExamenComponete(_serviceId, Constants.PSICOLOGIA_ID, 195);
            var OIT = _serviceBL.ValoresExamenComponete(_serviceId, Constants.OIT_ID, 211);
            var RX = _serviceBL.ValoresExamenComponete(_serviceId, Constants.RX_TORAX_ID, 135);
            var Laboratorio = _serviceBL.ValoresComponente(_serviceId, Constants.INFORME_LABORATORIO_ID);
            var Audiometria = _serviceBL.GetDiagnosticForAudiometria(_serviceId, Constants.AUDIOMETRIA_ID);
            var Espirometria = _serviceBL.ValoresExamenComponete(_serviceId, Constants.ESPIROMETRIA_ID, 210);
            var _DiagnosticRepository = _serviceBL.GetServiceDisgnosticsReports(_serviceId);
            var _Recomendation = _serviceBL.GetServiceRecommendationByServiceId(_serviceId);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(_serviceId);
            var ValoresDxLab = _serviceBL.ValoresComponenteAMC_(_serviceId, 1);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var TestIhihara = _serviceBL.ValoresComponente(_serviceId, Constants.TEST_ISHIHARA_ID);
            var TestEstereopsis = _serviceBL.ValoresComponente(_serviceId, Constants.TEST_ESTEREOPSIS_ID);

            FichaMedicaOcupacional312_CI.CreateFichaMedicalOcupacional312Report(_DataService,
                        filiationData, _listAtecedentesOcupacionales, _listaPatologicosFamiliares,
                        _listMedicoPersonales, _listaHabitoNocivos, Antropometria, FuncionesVitales,
                        ExamenFisico, Oftalmologia, Psicologia, OIT, RX, Laboratorio, Audiometria, Espirometria,
                        _DiagnosticRepository, _Recomendation, _ExamenesServicio, ValoresDxLab, MedicalCenter, TestIhihara, TestEstereopsis,
                        pathFile);

            Session["NombreTrabajador"] = filiationData.Trabajador.Replace(" ", "_");
            System.IO.File.Copy(_ruta + Session["NombreTrabajador"].ToString() + "-" + "Informe_312" + ".pdf", Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Informe_312" + ".pdf"), true);
     
        }

        private void GenerateOsteomuscular(string ruta, string serviceId)
        {
            DataSet dsOsteomuscularNuevo = new DataSet();
            var servicesId7 = new List<string>();
            servicesId7.Add(serviceId);
            var componentReportId7 = new ServiceBL().ObtenerIdsParaImportacionExcel(servicesId7, 11);
            var OSTEO_MUSCULAR_ID_1 = new PacientBL().ReportOsteoMuscularNuevo(serviceId, Constants.OSTEO_MUSCULAR_ID_1, componentReportId7[0].ComponentId);
            var UC_OSTEO_ID = new ServiceBL().ReporteOsteomuscular(serviceId, componentReportId7[0].ComponentId);
            DataTable dt_UC_OSTEO_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(UC_OSTEO_ID);
            DataTable dtOSTEO_MUSCULAR_ID_1 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(OSTEO_MUSCULAR_ID_1);

            dtOSTEO_MUSCULAR_ID_1.TableName = "dtOsteomuscularNuevo";
            dt_UC_OSTEO_ID.TableName = "dtOsteoMus";

            dsOsteomuscularNuevo.Tables.Add(dtOSTEO_MUSCULAR_ID_1);
            dsOsteomuscularNuevo.Tables.Add(dt_UC_OSTEO_ID);

            rp = new Sigesoft.Node.WinClient.UI.Reports.crMuscoloEsqueletico();
            rp.SetDataSource(dsOsteomuscularNuevo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.OSTEO_MUSCULAR_ID_1 + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();

            rp = new Sigesoft.Node.WinClient.UI.Reports.crMuscoloEsqueletico2();
            rp.SetDataSource(dsOsteomuscularNuevo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = ruta + serviceId + "-" + Constants.OSTEO_MUSCULAR_ID_2 + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();

            Session["NombreTrabajador"] = OSTEO_MUSCULAR_ID_1[0].NOMBRE_PACIENTE.Replace(" ", "_");
            MergeExPDF _mergeExPDF = new MergeExPDF();
            var x = ((List<string>)Session["filesNameToMerge"]).ToList();
            _mergeExPDF.FilesName = x;
            _mergeExPDF.DestinationFile = _ruta + Session["NombreTrabajador"].ToString() + "-OsteoMuscular.pdf";
            _mergeExPDF.Execute();

            System.IO.File.Copy(_ruta + Session["NombreTrabajador"].ToString() + "-" + "OsteoMuscular" + ".pdf", Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "OsteoMuscular" + ".pdf"), true);
        }

        private void GenerateAltura18_CI(string _ruta, string _serviceId)
        {
            var ALTURA_18_ID = new ServiceBL().GetAlturaEstructural_CI(_serviceId, "N005-ME000000117");
            dsGetRepo = new DataSet();
            DataTable dtALTURA_18_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ALTURA_18_ID);
            dtALTURA_18_ID.TableName = "dtAltura18_CI";
            dsGetRepo.Tables.Add(dtALTURA_18_ID);
            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crAltura1_8_Inter();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + "N005-ME000000117" + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            //Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            Session["NombreTrabajador"] = ALTURA_18_ID[0].NombreTrabajador.Replace(" ", "_");
            objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Altura" + ".pdf");
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();

            rp.Close();
        }

        private void Generate7D(string _ruta, string _serviceId)
          {             
            var _DataService = _serviceBL.GetInformacion_OtrosExamenes(_serviceId);             
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();              
            //var filiationData = _pacientBL.GetPacientReportEPSFirmaMedicoOcupacional(_serviceId);              
            var serviceComponents = _serviceBL.GetServiceComponentsReport(_serviceId);             
            var datosP = _pacientBL.DevolverDatosPaciente(_serviceId);              
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);            
            var datosGrabo = _serviceBL.DevolverDatosUsuarioGraboExamen((int)CategoryTypeExam.ExamenFisico, _serviceId);
            Anexo16A.CreateAnexo16A(_DataService, _ruta, datosP, MedicalCenter, serviceComponents, diagnosticRepository, datosGrabo);
            Session["NombreTrabajador"] = datosP.Trabajador.Replace(" ","_");
            System.IO.File.Copy(_ruta , Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "7D" + ".pdf"), true);       
          }

        private void GenerateTamizaje(string _ruta, string _serviceId)
          {
              var TAMIZAJE_DERMATOLOGIO_ID = new ServiceBL().ReportTamizajeDermatologico_CI(_serviceId, Constants.TAMIZAJE_DERMATOLOGIO_ID_CI);
              dsGetRepo = new DataSet();
              DataTable dt_TAMIZAJE_DERMATOLOGIO_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(TAMIZAJE_DERMATOLOGIO_ID);
              dt_TAMIZAJE_DERMATOLOGIO_ID.TableName = "dtEvaDermatologicoCI";
              dsGetRepo.Tables.Add(dt_TAMIZAJE_DERMATOLOGIO_ID);
              rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crEvaluacionDermatologicaCI();
              rp.SetDataSource(dsGetRepo);
              rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
              rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
              objDiskOpt = new DiskFileDestinationOptions();
              objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + Constants.TAMIZAJE_DERMATOLOGIO_ID_CI + ".pdf";
              _filesNameToMerge.Add(objDiskOpt.DiskFileName);
              rp.ExportOptions.DestinationOptions = objDiskOpt;
              rp.Export();

              Session["NombreTrabajador"] = TAMIZAJE_DERMATOLOGIO_ID[0].NombreTrabajador.Replace(" ", "_");
              objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Tamizaje" + ".pdf");
              _filesNameToMerge.Add(objDiskOpt.DiskFileName);
              rp.ExportOptions.DestinationOptions = objDiskOpt;
              rp.Export();
              rp.Close();
          }

        private void GenerateEspirometria(string _ruta, string _serviceId)
          {
              var ESPIROMETRIA_ID = new ServiceBL().GetReportCuestionarioEspirometria(_serviceId, Constants.ESPIROMETRIA_ID);
              dsGetRepo = new DataSet();
              DataTable dt_ESPIROMETRIA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ESPIROMETRIA_ID);
              dt_ESPIROMETRIA_ID.TableName = "dtCuestionarioEspirometria";
              dsGetRepo.Tables.Add(dt_ESPIROMETRIA_ID);
              rp = new Sigesoft.Node.WinClient.UI.Reports.crApendice08_Espiro02();
              rp.SetDataSource(dsGetRepo);
              rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
              rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
              objDiskOpt = new DiskFileDestinationOptions();
              objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + Constants.ESPIROMETRIA_ID + ".pdf";
              _filesNameToMerge.Add(objDiskOpt.DiskFileName);
              Session["filesNameToMerge"] = _filesNameToMerge;
              rp.ExportOptions.DestinationOptions = objDiskOpt;
              rp.Export();

              Session["NombreTrabajador"] = ESPIROMETRIA_ID[0].NombreTrabajador.Replace(" ", "_");
              objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Espirometría" + ".pdf"); 
              _filesNameToMerge.Add(objDiskOpt.DiskFileName);
              rp.ExportOptions.DestinationOptions = objDiskOpt;
              rp.Export();

              rp.Close();
          }

        private void GenerateSintomatico(string _ruta, string _serviceId)
        {
            var SINTOMATICO_RESPIRATORIO = new ServiceBL().ReporteSintomaticoRespiratorio(_serviceId, Constants.SINTOMATICO_RESPIRATORIO);

            dsGetRepo = new DataSet();

            DataTable dt_SINTOMATICO_RESPIRATORIO = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(SINTOMATICO_RESPIRATORIO);
            dt_SINTOMATICO_RESPIRATORIO.TableName = "dtSintomaticoRes";
            dsGetRepo.Tables.Add(dt_SINTOMATICO_RESPIRATORIO);

            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crSintomaticoResp();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();

            objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + Constants.SINTOMATICO_RESPIRATORIO + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;

            rp.Export();

            Session["NombreTrabajador"] = SINTOMATICO_RESPIRATORIO[0].Trabajador.Replace(" ", "_");
            objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Sintomático_Respiratorio" + ".pdf");
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();

            rp.Close();
        }

        private void GenerateCAP(string _ruta, string _serviceId)
        {
            OperationResult objOperationResult = new OperationResult();
            var INFORME_CERTIFICADO_APTITUD = new ServiceBL().GetAptitudeCertificate(ref objOperationResult, _serviceId);
            if (INFORME_CERTIFICADO_APTITUD == null)
            {
                return;
            }
            DataSet ds1 = new DataSet();
            DataTable dtINFORME_CERTIFICADO_APTITUD = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(INFORME_CERTIFICADO_APTITUD);
            dtINFORME_CERTIFICADO_APTITUD.TableName = "AptitudeCertificate";
            ds1.Tables.Add(dtINFORME_CERTIFICADO_APTITUD);
            var TipoServicio = INFORME_CERTIFICADO_APTITUD[0].i_EsoTypeId;
            Session["NombreTrabajador"] = INFORME_CERTIFICADO_APTITUD[0].v_FirstName + "_" + INFORME_CERTIFICADO_APTITUD[0].v_FirstLastName + "_" + INFORME_CERTIFICADO_APTITUD[0].v_SecondLastName;
            Session["NombreTrabajador"] = Regex.Replace(Session["NombreTrabajador"].ToString()," ", "_");
            if (TipoServicio == ((int)TypeESO.Retiro).ToString())
            {
                rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crOccupationalMedicalAptitudeCertificateRetiros();
                rp.SetDataSource(ds1);
                rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                objDiskOpt = new DiskFileDestinationOptions();
                objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + Constants.INFORME_CERTIFICADO_APTITUD + ".pdf";
                _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                Session["filesNameToMerge"] = _filesNameToMerge;
                rp.ExportOptions.DestinationOptions = objDiskOpt;
                rp.Export();
                objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Certificado_Aptitud" + ".pdf");
                _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                rp.ExportOptions.DestinationOptions = objDiskOpt;
                rp.Export();
                rp.Close();
            }
            else
            {
                if (INFORME_CERTIFICADO_APTITUD[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs)
                {
                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crCertficadoObservado();
                    rp.SetDataSource(ds1);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + Constants.INFORME_CERTIFICADO_APTITUD + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Certificado_Aptitud" + ".pdf");
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();

                }
                else
                {     
                    rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crOccupationalMedicalAptitudeCertificate();
                    rp.SetDataSource(ds1);
                    rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    objDiskOpt = new DiskFileDestinationOptions();
                    objDiskOpt.DiskFileName = _ruta + _serviceId + "-" + Constants.INFORME_CERTIFICADO_APTITUD + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Certificado_Aptitud" + ".pdf");
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();
                    rp.Close();
                }
            }
        }

        private void GenerateInformeMedicoTrabajadorInternacional(string pathFile, string _serviceId, string _pacienteId)
        {
            PacientBL oPacientBL = new PacientBL();        
            HistoryBL _historyBL = new HistoryBL();
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var Cabecera = oPacientBL.DevolverDatosPaciente(_serviceId);
            var AntOcupacionales = _historyBL.GetHistoryReport(_pacienteId);
            var HabitosNocivos = _historyBL.GetNoxiousHabitsReport(_pacienteId);
            var AntPersonales = oPacientBL.DevolverAntecedentesPersonales(_pacienteId);
            var Valores = _serviceBL.GetServiceComponentsReport(_serviceId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReportCI(_serviceId);
            var AntFami = _historyBL.GetFamilyMedicalAntecedentsReport(_pacienteId);
            var Reco = _serviceBL.ConcatenateRecommendationByService(_serviceId);
            InformeMedicoTrabajadorInternacional.CreateInformeMedicoTrabajadorInternacional(pathFile, Cabecera, HabitosNocivos, AntFami, Valores, diagnosticRepository, AntPersonales, AntOcupacionales, MedicalCenter, Reco);
            Session["NombreTrabajador"] = Cabecera.Trabajador.Replace(" ", "_");
            System.IO.File.Copy(pathFile, Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Informe_Trabajador" + ".pdf"), true);
     
        }

        private void GenerateAnexo16GoldField(string pathFile, string _serviceId, string _pacienteId)
        {
            var _DataService = _serviceBL.GetServiceReport_Anexo16GoldField(_serviceId);//_serviceBL.GetServiceReport(_serviceId);//
            var filiationData = _pacientBL.GetPacientReportEPS_Photo(_serviceId);//_pacientBL.GetPacientReportEPS(_serviceId);//
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(_pacienteId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(_pacienteId);
            var _Valores = _serviceBL.GetServiceComponentsReport_Anexo16GoldField(_serviceId);//_serviceBL.GetServiceComponentsReport(_serviceId);//
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(_pacienteId);
            var _PiezasCaries = _serviceBL.GetCantidadCaries(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_CARIES_ID);
            var _PiezasAusentes = _serviceBL.GetCantidadAusentes(_serviceId, Constants.ODONTOGRAMA_ID, Constants.ODONTOGRAMA_PIEZAS_AUSENTES_ID);
            var CuadroVacio = Sigesoft.Common.Utils.BitmapToByteArray(ResourcesWeb.CuadradoVacio);
            var CuadroCheck = Sigesoft.Common.Utils.BitmapToByteArray(ResourcesWeb.CuadradoCheck);
            var Pulmones = Sigesoft.Common.Utils.BitmapToByteArray(ResourcesWeb.MisPulmones);
            var Audiometria = _serviceBL.ValoresComponenteOdontogramaValue1(_serviceId, Constants.AUDIOMETRIA_ID);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReport(_serviceId);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter_Logo();//_serviceBL.GetInfoMedicalCenter();//
            ReportPDF.CreateAnexo16GoldField(_DataService, filiationData, _Valores, _listMedicoPersonales,
                                    _listaPatologicosFamiliares, _listaHabitoNocivos,
                                    CuadroVacio, CuadroCheck, Pulmones, _PiezasCaries,
                                    _PiezasAusentes, Audiometria, diagnosticRepository, MedicalCenter,
                                    pathFile);
            Session["NombreTrabajador"]=Regex.Replace(_DataService.v_Pacient," ","_");
            System.IO.File.Copy(pathFile, Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Informe_Anexo16.pdf"), true);
        }


    }
}