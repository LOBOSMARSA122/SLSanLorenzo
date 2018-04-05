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

        protected void Page_Load(object sender, EventArgs e)
        {

            _ruta = WebConfigurationManager.AppSettings["rutaReportes"];
            string path;

            string Mode = Request.QueryString["Mode"].ToString();

            if (Mode == "Psico")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N002-ME000000033";
                GeneratePsico(_ruta, Session["ServiceId"].ToString());
                //ShowPdf1.FilePath = @"files\" + Session["ServiceId"].ToString() + "-" + "N002-ME000000033_MERGE.pdf";
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Psicología.pdf";
            }
            if (Mode == "AudioCI")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N005-ME000000005";
                GenerateAudimetriaCI(_ruta, Session["ServiceId"].ToString());
                //ShowPdf1.FilePath = @"files\" + Session["ServiceId"].ToString() + "-" + "N005-ME000000005.pdf";
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Audiometría.pdf";
                
            }
            if (Mode == "OftalmoCI")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N005-ME000000028";
                GenerateOftalmologiaCI(_ruta, Session["ServiceId"].ToString());
                //ShowPdf1.FilePath = @"files\" + Session["ServiceId"].ToString() + "-" + "N005-ME000000028.pdf";
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Oftalmología.pdf";
            }
            if (Mode == "OdontoCI")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N005-ME000000027";
                GenerateOdontogramaCI(_ruta, Session["ServiceId"].ToString());
                //ShowPdf1.FilePath = @"files\" + Session["ServiceId"].ToString() + "-" + "N005-ME000000027.pdf";
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Odontología.pdf";
            }
            if (Mode == "RX")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N002-ME000000032";
                GenerateRX(_ruta, Session["ServiceId"].ToString());
                //ShowPdf1.FilePath = @"files\" + Session["ServiceId"].ToString() + "-" + "N002-ME000000032.pdf";
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "RayosX.pdf";
            }
            if (Mode == "OIT")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N009-ME000000062";
                GenerateOIT(_ruta, Session["ServiceId"].ToString());
                //ShowPdf1.FilePath = @"files\" + Session["ServiceId"].ToString() + "-" + "N009-ME000000062.pdf";
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "OIT.pdf";
            }
            if (Mode == "Lab")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + Constants.INFORME_LABORATORIO_CLINICO;
                GenerateLaboratorio(string.Format("{0}.pdf", Path.Combine(path)), Session["ServiceId"].ToString());
                //ShowPdf1.FilePath = @"files\" + Session["ServiceId"].ToString() + "-" + Constants.INFORME_LABORATORIO_CLINICO+".pdf";
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "INFORME_LABORATORIO_CLINICO.pdf";
            }
            if (Mode == "Cardio")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N002-ME000000025";
                GenerateElectro(_ruta, Session["ServiceId"].ToString());
                //ShowPdf1.FilePath = @"files\" + Session["ServiceId"].ToString() + "-" + "N002-ME000000025.pdf";
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Cardiología.pdf";
            }
            if (Mode == "312")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + Constants.INFORME_ANEXO_312;
                GenerateAnexo312(string.Format("{0}.pdf", Path.Combine(path)), Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                //ShowPdf1.FilePath = @"files\" + Session["ServiceId"].ToString() + "-" + Constants.INFORME_ANEXO_312 + ".pdf";
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Informe_312.pdf";
            }
            if (Mode == "AlturaCI")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N005-ME000000117";
                GenerateAltura18_CI(_ruta, Session["ServiceId"].ToString());
                //ShowPdf1.FilePath = @"files\" + Session["ServiceId"].ToString() + "-" + "N005-ME000000117.pdf";
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Altura.pdf";
            }
            if (Mode == "OsteoCI")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N005-ME000000046";
                GenerateOsteomuscular(_ruta, Session["ServiceId"].ToString());
                //ShowPdf1.FilePath = @"files\" + Session["ServiceId"].ToString() + "-" + "N005-ME000000046_MERGE.pdf";
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "OsteoMuscular.pdf";
            }
            if (Mode == "TamizajeCI")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N005-ME000000116";
                GenerateTamizaje(_ruta, Session["ServiceId"].ToString());
                //ShowPdf1.FilePath = @"files\" + Session["ServiceId"].ToString() + "-" + "N005-ME000000116" + ".pdf";
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Tamizaje.pdf";
            }
            if (Mode == "7D")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N002-ME000000045";
                Generate7D(_ruta, Session["ServiceId"].ToString());
                //ShowPdf1.FilePath = @"files\" + Session["ServiceId"].ToString() + "-" + "N002-ME000000045" + ".pdf";
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "7D.pdf";
            }

            if (Mode == "Espiro")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N002-ME000000031";
                GenerateEspirometria(_ruta, Session["ServiceId"].ToString());
                //ShowPdf1.FilePath = @"files\" + Session["ServiceId"].ToString() + "-" + "N002-ME000000031" + ".pdf";
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Espirometría.pdf";
            }
            if (Mode == "Sintomatico")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "N009-ME000000116";
                GenerateSintomatico(_ruta, Session["ServiceId"].ToString());
                //ShowPdf1.FilePath = @"files\" + Session["ServiceId"].ToString() + "-" + "N009-ME000000116" + ".pdf";
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Sintomático_Respiratorio.pdf";
            }
            if (Mode == "Certificado")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + "CAP";
                GenerateCAP(_ruta, Session["ServiceId"].ToString());
                //ShowPdf1.FilePath = @"files\" + Session["ServiceId"].ToString() + "-" + "CAP" + ".pdf";
                ShowPdf1.FilePath = @"files\" + Session["NombreTrabajador"].ToString() + "-" + "Certificado_Aptitud.pdf";
            }
            if (Mode == "InformeCI")
            {
                path = _ruta + Session["ServiceId"].ToString() + "-" + Constants.INFORME_FICHA_MEDICA_TRABAJADOR_CI;
                GenerateInformeMedicoTrabajadorInternacional(string.Format("{0}.pdf", Path.Combine(path)), Session["ServiceId"].ToString(), Session["PersonId"].ToString());
                //ShowPdf1.FilePath = @"files\" + Session["ServiceId"].ToString() + "-" + Constants.INFORME_FICHA_MEDICA_TRABAJADOR_CI + ".pdf";
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

        private void GeneratePsico(string _ruta, string p)
        {
            var PSICOLOGIA_ID = new PacientBL().GetFichaPsicologicaOcupacional(p);
            dsGetRepo = new DataSet();
            DataTable dt_PSICOLOGIA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(PSICOLOGIA_ID);
            dt_PSICOLOGIA_ID.TableName = "InformePsico";
            dsGetRepo.Tables.Add(dt_PSICOLOGIA_ID);
            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.InformePsicologicoOcupacional();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.PSICOLOGIA_ID + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();


            var HISTORIA_CLINICA_PSICOLOGICA_ID = new ServiceBL().GetHistoriaClinicaPsicologica(p, Constants.HISTORIA_CLINICA_PSICOLOGICA_ID);
            dsGetRepo = new DataSet();
            DataTable dt_HISTORIA_CLINICA_PSICOLOGICA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(HISTORIA_CLINICA_PSICOLOGICA_ID);
            dt_HISTORIA_CLINICA_PSICOLOGICA_ID.TableName = "dtHistoriaClinicaPsicologica";
            dsGetRepo.Tables.Add(dt_HISTORIA_CLINICA_PSICOLOGICA_ID);
            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crHistoriaClinicaPsicologica();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.HISTORIA_CLINICA_PSICOLOGICA_ID + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();


            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crHistoriaClinicaPsicologica2();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.HISTORIA_CLINICA_PSICOLOGICA_ID + "2" + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();


            Session["NombreTrabajador"] = PSICOLOGIA_ID[0].v_Pacient.Replace(" ", "_");
            MergeExPDF _mergeExPDF = new MergeExPDF();
            var x = ((List<string>)Session["filesNameToMerge"]).ToList();
            _mergeExPDF.FilesName = x;
            //_mergeExPDF.DestinationFile = _ruta + Session["ServiceId"].ToString() + "-N002-ME000000033_MERGE.pdf";
            objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Psicología" + ".pdf");
            _mergeExPDF.Execute();
            
            System.IO.File.Copy(_ruta + Session["NombreTrabajador"].ToString() + "-" + "Psicología" + ".pdf", Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Psicología" + ".pdf"), true);
     
        }

        private void GenerateAudimetriaCI(string _ruta, string p)
        {
            var serviceBL = new ServiceBL();
            var dsAudiometria = new DataSet();
            var dxList_CI = serviceBL.GetDiagnosticRepositoryByComponent(p, "N005-ME000000005");
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

            var audioUserControlList_CI = serviceBL.ReportAudiometriaUserControl(p, "N005-ME000000005");
            var audioCabeceraList_CI = serviceBL.ReportAudiometria_CI(p, "N005-ME000000005");
            var dtAudiometriaUserControl_CI = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(audioUserControlList_CI);
            var dtCabecera_CI = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(audioCabeceraList_CI);

            dtCabecera_CI.TableName = "dtAudiometria";
            dtAudiometriaUserControl_CI.TableName = "dtAudiometriaUserControl";

            dsAudiometria.Tables.Add(dtCabecera_CI);
            dsAudiometria.Tables.Add(dtAudiometriaUserControl_CI);

            rp = new AdministradorServicios.crFichaAudiometria_inter();

            rp.SetDataSource(dsAudiometria);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + p + "-" + "N005-ME000000005" + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();

            //objDiskOpt.DiskFileName = Server.MapPath("files/" + p + "-" + "N005-ME000000005" + ".pdf");
            Session["NombreTrabajador"] = audioCabeceraList_CI[0].v_FullPersonName.Replace(" ","_");
            objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Audiometría" + ".pdf");
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();

            rp.Close();
        }

        private void GenerateOftalmologiaCI(string _ruta, string p)
        {
            var OFTALMOLOGIA_CI_ID = new ServiceBL().ReportOftalmologia_CI(p, "N005-ME000000028");

            dsGetRepo = new DataSet();
            DataTable dt_OFTALMOLOGIA_CI_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(OFTALMOLOGIA_CI_ID);
            dt_OFTALMOLOGIA_CI_ID.TableName = "dtOftalomologia_CI";
            dsGetRepo.Tables.Add(dt_OFTALMOLOGIA_CI_ID);

            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crOftalmologia_inter();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + p + "-" + "N005-ME000000028" + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();

            Session["NombreTrabajador"] = OFTALMOLOGIA_CI_ID[0].NombreTrabajador.Replace(" ", "_");
            //objDiskOpt.DiskFileName = Server.MapPath("files/" + p + "-" + "N005-ME000000028" + ".pdf");
            objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Oftalmología" + ".pdf");
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();

            rp.Close();
        }

        private void GenerateOdontogramaCI(string _ruta, string p)
        {
            var ODONTOGRAMA_CI = new ServiceBL().GetOdontograma_CI(p, "N005-ME000000027");
            dsGetRepo = new DataSet();
            DataTable dt_ODONTOGRAMA_CI = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ODONTOGRAMA_CI);
            dt_ODONTOGRAMA_CI.TableName = "dtOdonto_CI";
            dsGetRepo.Tables.Add(dt_ODONTOGRAMA_CI);

            if (ODONTOGRAMA_CI != null)
            {
                rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crOdontoCI();
                rp.SetDataSource(dsGetRepo);
                rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                objDiskOpt = new DiskFileDestinationOptions();
                objDiskOpt.DiskFileName = _ruta + p + "-" + "N005-ME000000027" + ".pdf";
                _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                Session["filesNameToMerge"] = _filesNameToMerge;
                rp.ExportOptions.DestinationOptions = objDiskOpt;
                rp.Export();

                Session["NombreTrabajador"] = ODONTOGRAMA_CI[0].NombreTrabajador.Replace(" ", "_");
                //objDiskOpt.DiskFileName = Server.MapPath("files/" + p + "-" + "N005-ME000000027" + ".pdf");
                objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Odontología" + ".pdf");
                _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                rp.ExportOptions.DestinationOptions = objDiskOpt;
                rp.Export();


                rp.Close();
            }

        }

        private void GenerateRX(string _ruta, string p)
        {
            var RX_TORAX_ID = new ServiceBL().ReportRadiologico(p, Constants.RX_TORAX_ID);
            dsGetRepo = new DataSet();

            DataTable dt_RX_TORAX_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(RX_TORAX_ID);

            dt_RX_TORAX_ID.TableName = "dtRadiologico";

            dsGetRepo.Tables.Add(dt_RX_TORAX_ID);
            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crInformeRadiologico();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.RX_TORAX_ID + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();

            Session["NombreTrabajador"] = RX_TORAX_ID[0].Paciente.Replace(" ", "_");
            //objDiskOpt.DiskFileName = Server.MapPath("files/" + p + "-" + Constants.RX_TORAX_ID + ".pdf");
            objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "RayosX" + ".pdf");
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();

            rp.Close();
        }

        private void GenerateOIT(string _ruta, string p)
        {
            var OIT_ID = new ServiceBL().ReportInformeRadiografico(p, Constants.OIT_ID);

            dsGetRepo = new DataSet();
            DataTable dt_OIT_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(OIT_ID);
            dt_OIT_ID.TableName = "dtInformeRadiografico";
            dsGetRepo.Tables.Add(dt_OIT_ID);

            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crInformeRadiograficoOIT();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.OIT_ID + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();

            Session["NombreTrabajador"] = OIT_ID[0].Nombre.Replace(" ", "_");
            //objDiskOpt.DiskFileName = Server.MapPath("files/" + p + "-" + Constants.OIT_ID + ".pdf");
            objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "OIT" + ".pdf");
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();

            rp.Close();
        }

        private void GenerateLaboratorio(string pathFile, string ServicioId)
        {
            PacientBL _pacientBL = new PacientBL();
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var filiationData = _pacientBL.GetPacientReportEPS(ServicioId);
            var serviceComponents = _serviceBL.GetServiceComponentsReport(ServicioId);

            Session["NombreTrabajador"] = filiationData.Trabajador.Replace(" ", "_");

            LaboratorioReport.CreateLaboratorioReport(filiationData, serviceComponents, MedicalCenter, pathFile);
            //System.IO.File.Copy(_ruta + Session["ServiceId"].ToString() + "-" + Constants.INFORME_LABORATORIO_CLINICO + ".pdf", Server.MapPath("files/" + Session["ServiceId"].ToString() + "-" + Constants.INFORME_LABORATORIO_CLINICO + ".pdf"), true);
            System.IO.File.Copy(_ruta + Session["NombreTrabajador"].ToString() + "-" + "INFORME_LABORATORIO_CLINICO" + ".pdf", Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "INFORME_LABORATORIO_CLINICO" + ".pdf"), true);
        }

        private void GenerateElectro(string _ruta, string p)
        {
            var ELECTROCARDIOGRAMA_ID = new ServiceBL().GetReportEstudioElectrocardiografico(p, Constants.ELECTROCARDIOGRAMA_ID);

            dsGetRepo = new DataSet();

            DataTable dt_ELECTROCARDIOGRAMA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ELECTROCARDIOGRAMA_ID);
            dt_ELECTROCARDIOGRAMA_ID.TableName = "dtEstudioElectrocardiografico";
            dsGetRepo.Tables.Add(dt_ELECTROCARDIOGRAMA_ID);
            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crEstudioElectrocardiografico();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.ELECTROCARDIOGRAMA_ID + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();

            Session["NombreTrabajador"] = ELECTROCARDIOGRAMA_ID[0].DatosPaciente.Replace(" ", "_");
            //objDiskOpt.DiskFileName = Server.MapPath("files/" + p + "-" + Constants.ELECTROCARDIOGRAMA_ID + ".pdf");
            objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Cardiología" + ".pdf");
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();
        }

        private void GenerateAnexo312(string pathFile, string ServicioId, string PacienteId)
        {
            PacientBL _pacientBL = new PacientBL();
            HistoryBL _historyBL = new HistoryBL();
            Session["ValidacionReporte312"] = true;
            var filiationData = _pacientBL.GetPacientReportEPS(ServicioId);
            var _listAtecedentesOcupacionales = _historyBL.GetHistoryReport(PacienteId);
            var _listaPatologicosFamiliares = _historyBL.GetFamilyMedicalAntecedentsReport(PacienteId);
            var _listMedicoPersonales = _historyBL.GetPersonMedicalHistoryReport(PacienteId);
            var _DataService = _serviceBL.GetServiceReport(ServicioId);
            if (_DataService == null)
            {
                Session["ValidacionReporte312"] = false;
                return;
            }
            var _listaHabitoNocivos = _historyBL.GetNoxiousHabitsReport(PacienteId);

            var Antropometria = _serviceBL.ValoresComponente(ServicioId, Constants.ANTROPOMETRIA_ID);
            var FuncionesVitales = _serviceBL.ValoresComponente(ServicioId, Constants.FUNCIONES_VITALES_ID);
            var ExamenFisico = _serviceBL.ValoresComponente(ServicioId, Constants.EXAMEN_FISICO_ID);
            var Oftalmologia = _serviceBL.ValoresComponente(ServicioId, "N005-ME000000028");
            var Psicologia = _serviceBL.ValoresExamenComponete(ServicioId, Constants.PSICOLOGIA_ID, 195);
            var OIT = _serviceBL.ValoresExamenComponete(ServicioId, Constants.OIT_ID, 211);
            var RX = _serviceBL.ValoresExamenComponete(ServicioId, Constants.RX_TORAX_ID, 135);
            var Laboratorio = _serviceBL.ValoresComponente(ServicioId, Constants.INFORME_LABORATORIO_ID);
            //var Audiometria = _serviceBL.ValoresComponente(ServicioId, Constants.AUDIOMETRIA_ID);
            var Audiometria = _serviceBL.GetDiagnosticForAudiometria(ServicioId, Constants.AUDIOMETRIA_ID);
            var Espirometria = _serviceBL.ValoresExamenComponete(ServicioId, Constants.ESPIROMETRIA_ID, 210);
            var _DiagnosticRepository = _serviceBL.GetServiceDisgnosticsReports(ServicioId);
            var _Recomendation = _serviceBL.GetServiceRecommendationByServiceId(ServicioId);
            var _ExamenesServicio = _serviceBL.GetServiceComponentsReport(ServicioId);
            var ValoresDxLab = _serviceBL.ValoresComponenteAMC_(ServicioId, 1);
            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var TestIhihara = _serviceBL.ValoresComponente(ServicioId, Constants.TEST_ISHIHARA_ID);
            var TestEstereopsis = _serviceBL.ValoresComponente(ServicioId, Constants.TEST_ESTEREOPSIS_ID);


            FichaMedicaOcupacional312_CI.CreateFichaMedicalOcupacional312Report(_DataService,
                        filiationData, _listAtecedentesOcupacionales, _listaPatologicosFamiliares,
                        _listMedicoPersonales, _listaHabitoNocivos, Antropometria, FuncionesVitales,
                        ExamenFisico, Oftalmologia, Psicologia, OIT, RX, Laboratorio, Audiometria, Espirometria,
                        _DiagnosticRepository, _Recomendation, _ExamenesServicio, ValoresDxLab, MedicalCenter, TestIhihara, TestEstereopsis,
                        pathFile);

            Session["NombreTrabajador"] = filiationData.Trabajador.Replace(" ", "_");

            //System.IO.File.Copy(_ruta + Session["ServiceId"].ToString() + "-" + Constants.INFORME_ANEXO_312 + ".pdf", Server.MapPath("files/" + Session["ServiceId"].ToString() + "-" + Constants.INFORME_ANEXO_312 + ".pdf"), true);
            System.IO.File.Copy(_ruta + Session["NombreTrabajador"].ToString() + "-" + "Informe_312" + ".pdf", Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Informe_312" + ".pdf"), true);
     
        }

        private void GenerateOsteomuscular(string ruta, string serviceId)
        {
            var OSTEOMUCULAR_CI_ID = new ServiceBL().GetMusculoEsqueletico_ClinicaInternacional(serviceId, "N005-ME000000046");
            dsGetRepo = new DataSet();
            DataTable dt_OSTEOMUSCULAR_CI_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(OSTEOMUCULAR_CI_ID);
            dt_OSTEOMUSCULAR_CI_ID.TableName = "DataTable1";
            dsGetRepo.Tables.Add(dt_OSTEOMUSCULAR_CI_ID);

            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crMuscoloEsqueletico();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = ruta + serviceId + "-" + "N005-ME000000046" + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();


            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crMuscoloEsqueletico2();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = ruta + serviceId + "-" + "N005-ME000000046" + "-2.pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();


            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crMuscoloEsqueletico3();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = ruta + serviceId + "-" + "N005-ME000000046" + "-3.pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();
            rp.Close();

            Session["NombreTrabajador"] = OSTEOMUCULAR_CI_ID[0].NOMBREAPELLIDOTRABAJADOR.Replace(" ", "_");

            MergeExPDF _mergeExPDF = new MergeExPDF();
            var x = ((List<string>)Session["filesNameToMerge"]).ToList();
            _mergeExPDF.FilesName = x;
            _mergeExPDF.DestinationFile = _ruta + Session["NombreTrabajador"].ToString() + "-OsteoMuscular.pdf";
            _mergeExPDF.Execute();


            //System.IO.File.Copy(_ruta + Session["ServiceId"].ToString() + "-" + "N005-ME000000046_MERGE" + ".pdf", Server.MapPath("files/" + Session["ServiceId"].ToString() + "-" + "N005-ME000000046_MERGE" + ".pdf"), true);
            System.IO.File.Copy(_ruta + Session["NombreTrabajador"].ToString() + "-" + "OsteoMuscular" + ".pdf", Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "OsteoMuscular" + ".pdf"), true);
     


        }

        private void GenerateAltura18_CI(string _ruta, string p)
        {
            var ALTURA_18_ID = new ServiceBL().GetAlturaEstructural_CI(p, "N005-ME000000117");

            dsGetRepo = new DataSet();

            DataTable dtALTURA_18_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ALTURA_18_ID);
            dtALTURA_18_ID.TableName = "dtAltura18_CI";
            dsGetRepo.Tables.Add(dtALTURA_18_ID);

            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crAltura1_8_Inter();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();
            objDiskOpt.DiskFileName = _ruta + p + "-" + "N005-ME000000117" + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            //Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();

            Session["NombreTrabajador"] = ALTURA_18_ID[0].NombreTrabajador.Replace(" ", "_");
            //objDiskOpt.DiskFileName = Server.MapPath("files/" + p + "-" + "N005-ME000000117" + ".pdf");
            objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Altura" + ".pdf");
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();

            rp.Close();
        }

        private void Generate7D(string _ruta, string p)
          {
              var AscensoAlturas = new ServiceBL().ReportAscensoGrandesAlturas(p, Constants.ALTURA_7D_ID);
              var FuncionesVitales = new ServiceBL().ReportFuncionesVitales(p, Constants.FUNCIONES_VITALES_ID);
              var Antropometria = new ServiceBL().ReportAntropometria(p, Constants.ANTROPOMETRIA_ID);

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

              rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crAnexo7D();
              rp.SetDataSource(dsGetRepo);
              rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
              rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
              objDiskOpt = new DiskFileDestinationOptions();
              objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.ALTURA_7D_ID + ".pdf";
              _filesNameToMerge.Add(objDiskOpt.DiskFileName);
              rp.ExportOptions.DestinationOptions = objDiskOpt;
              rp.Export();

              Session["NombreTrabajador"] = AscensoAlturas[0].v_Pacient.Replace(" ", "_");
              //objDiskOpt.DiskFileName = Server.MapPath("files/" + p + "-" + Constants.ALTURA_7D_ID + ".pdf");
              objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "7D" + ".pdf");
              _filesNameToMerge.Add(objDiskOpt.DiskFileName);
              rp.ExportOptions.DestinationOptions = objDiskOpt;
              rp.Export();
              rp.Close();
          }

        private void GenerateTamizaje(string _ruta, string p)
          {
              var TAMIZAJE_DERMATOLOGIO_ID = new ServiceBL().ReportTamizajeDermatologico_CI(p, Constants.TAMIZAJE_DERMATOLOGIO_ID_CI);

              dsGetRepo = new DataSet();
              DataTable dt_TAMIZAJE_DERMATOLOGIO_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(TAMIZAJE_DERMATOLOGIO_ID);
              dt_TAMIZAJE_DERMATOLOGIO_ID.TableName = "dtEvaDermatologicoCI";
              dsGetRepo.Tables.Add(dt_TAMIZAJE_DERMATOLOGIO_ID);

              rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crEvaluacionDermatologicaCI();
              rp.SetDataSource(dsGetRepo);
              rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
              rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
              objDiskOpt = new DiskFileDestinationOptions();
              objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.TAMIZAJE_DERMATOLOGIO_ID_CI + ".pdf";
              _filesNameToMerge.Add(objDiskOpt.DiskFileName);
              rp.ExportOptions.DestinationOptions = objDiskOpt;
              rp.Export();

              Session["NombreTrabajador"] = TAMIZAJE_DERMATOLOGIO_ID[0].NombreTrabajador.Replace(" ", "_");
              //objDiskOpt.DiskFileName = Server.MapPath("files/" + p + "-" + Constants.TAMIZAJE_DERMATOLOGIO_ID_CI + ".pdf");
              objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Tamizaje" + ".pdf");
              _filesNameToMerge.Add(objDiskOpt.DiskFileName);
              rp.ExportOptions.DestinationOptions = objDiskOpt;
              rp.Export();
              rp.Close();
          }

        private void GenerateEspirometria(string _ruta, string p)
          {
              var ESPIROMETRIA_ID = new ServiceBL().GetReportCuestionarioEspirometria(p, Constants.ESPIROMETRIA_ID);

              dsGetRepo = new DataSet();
              DataTable dt_ESPIROMETRIA_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(ESPIROMETRIA_ID);
              dt_ESPIROMETRIA_ID.TableName = "dtCuestionarioEspirometria";
              dsGetRepo.Tables.Add(dt_ESPIROMETRIA_ID);
              rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crCuestionarioEspirometria();
              rp.SetDataSource(dsGetRepo);
              rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
              rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
              objDiskOpt = new DiskFileDestinationOptions();
              objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.ESPIROMETRIA_ID + ".pdf";
              _filesNameToMerge.Add(objDiskOpt.DiskFileName);
              Session["filesNameToMerge"] = _filesNameToMerge;
              rp.ExportOptions.DestinationOptions = objDiskOpt;
              rp.Export();

              Session["NombreTrabajador"] = ESPIROMETRIA_ID[0].NombreTrabajador.Replace(" ", "_");
              objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Espirometría" + ".pdf");
              //objDiskOpt.DiskFileName = Server.MapPath("files/" + p + "-" + "N002-ME000000031" + ".pdf");
              _filesNameToMerge.Add(objDiskOpt.DiskFileName);
              rp.ExportOptions.DestinationOptions = objDiskOpt;
              rp.Export();

              rp.Close();
          }

        private void GenerateSintomatico(string _ruta, string p)
        {
            var SINTOMATICO_RESPIRATORIO = new ServiceBL().ReporteSintomaticoRespiratorio(p, Constants.SINTOMATICO_RESPIRATORIO);

            dsGetRepo = new DataSet();

            DataTable dt_SINTOMATICO_RESPIRATORIO = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(SINTOMATICO_RESPIRATORIO);
            dt_SINTOMATICO_RESPIRATORIO.TableName = "dtSintomaticoRes";
            dsGetRepo.Tables.Add(dt_SINTOMATICO_RESPIRATORIO);

            rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crSintomaticoResp();
            rp.SetDataSource(dsGetRepo);
            rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            objDiskOpt = new DiskFileDestinationOptions();

            objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.SINTOMATICO_RESPIRATORIO + ".pdf";
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            Session["filesNameToMerge"] = _filesNameToMerge;
            rp.ExportOptions.DestinationOptions = objDiskOpt;

            rp.Export();

            Session["NombreTrabajador"] = SINTOMATICO_RESPIRATORIO[0].Trabajador.Replace(" ", "_");
            objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Sintomático_Respiratorio" + ".pdf");
            //objDiskOpt.DiskFileName = Server.MapPath("files/" + p + "-" + "N009-ME000000116" + ".pdf");
            _filesNameToMerge.Add(objDiskOpt.DiskFileName);
            rp.ExportOptions.DestinationOptions = objDiskOpt;
            rp.Export();

            rp.Close();
        }

        private void GenerateCAP(string _ruta, string p)
        {
            OperationResult objOperationResult = new OperationResult();

            var INFORME_CERTIFICADO_APTITUD = new ServiceBL().GetAptitudeCertificate(ref objOperationResult, p);

            if (INFORME_CERTIFICADO_APTITUD == null)
            {
                return;
            }
            DataSet ds1 = new DataSet();

            DataTable dtINFORME_CERTIFICADO_APTITUD = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(INFORME_CERTIFICADO_APTITUD);

            dtINFORME_CERTIFICADO_APTITUD.TableName = "AptitudeCertificate";
            ds1.Tables.Add(dtINFORME_CERTIFICADO_APTITUD);

            var TipoServicio = INFORME_CERTIFICADO_APTITUD[0].i_EsoTypeId;
            Session["NombreTrabajador"] = INFORME_CERTIFICADO_APTITUD[0].v_FirstName + "_" + INFORME_CERTIFICADO_APTITUD[0].v_FirstLastName + "-" + INFORME_CERTIFICADO_APTITUD[0].v_SecondLastName;
            if (TipoServicio == ((int)TypeESO.Retiro).ToString())
            {
                rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crOccupationalMedicalAptitudeCertificateRetiros();
                rp.SetDataSource(ds1);
                rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                objDiskOpt = new DiskFileDestinationOptions();
                objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.INFORME_CERTIFICADO_APTITUD + ".pdf";
                _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                Session["filesNameToMerge"] = _filesNameToMerge;
                rp.ExportOptions.DestinationOptions = objDiskOpt;
                rp.Export();

                objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Certificado_Aptitud" + ".pdf");
                //objDiskOpt.DiskFileName = Server.MapPath("files/" + p + "-" + Constants.INFORME_CERTIFICADO_APTITUD + ".pdf");
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
                    objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.INFORME_CERTIFICADO_APTITUD + ".pdf";
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    Session["filesNameToMerge"] = _filesNameToMerge;
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();

                    objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Certificado_Aptitud" + ".pdf");
                    //objDiskOpt.DiskFileName = Server.MapPath("files/" + p + "-" + Constants.INFORME_CERTIFICADO_APTITUD + ".pdf");
                    _filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    rp.ExportOptions.DestinationOptions = objDiskOpt;
                    rp.Export();

                    rp.Close();

                }
                else
                {
                    //QUITAR
                    //rp = new Sigesoft.Server.WebClientAdmin.UI.AdministradorServicios.crOccupationalMedicalAptitudeCertificate();
                    //rp.SetDataSource(ds1);

                    //rp.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    //rp.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    //objDiskOpt = new DiskFileDestinationOptions();
                    //objDiskOpt.DiskFileName = _ruta + p + "-" + Constants.INFORME_CERTIFICADO_APTITUD + ".pdf";
                    //_filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    //Session["filesNameToMerge"] = _filesNameToMerge;
                    //rp.ExportOptions.DestinationOptions = objDiskOpt;
                    //rp.Export();

                    //objDiskOpt.DiskFileName = Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Certificado_Aptitud" + ".pdf");
                    ////objDiskOpt.DiskFileName = Server.MapPath("files/" + p + "-" + Constants.INFORME_CERTIFICADO_APTITUD + ".pdf");
                    //_filesNameToMerge.Add(objDiskOpt.DiskFileName);
                    //rp.ExportOptions.DestinationOptions = objDiskOpt;
                    //rp.Export();

                    //rp.Close();
                }
            }
        }

        private void GenerateInformeMedicoTrabajadorInternacional(string pathFile, string ServicioId, string PacienteId)
        {
            PacientBL oPacientBL = new PacientBL();        
            HistoryBL _historyBL = new HistoryBL();

            var MedicalCenter = _serviceBL.GetInfoMedicalCenter();
            var Cabecera = oPacientBL.DevolverDatosPaciente(ServicioId);
            var AntOcupacionales = _historyBL.GetHistoryReport(PacienteId);
            var HabitosNocivos = _historyBL.GetNoxiousHabitsReport(PacienteId);
            var AntPersonales = oPacientBL.DevolverAntecedentesPersonales(PacienteId);
            var Valores = _serviceBL.GetServiceComponentsReport(ServicioId);
            var diagnosticRepository = _serviceBL.GetServiceComponentConclusionesDxServiceIdReportCI(ServicioId);
            var AntFami = _historyBL.GetFamilyMedicalAntecedentsReport(PacienteId);
            var Reco = _serviceBL.ConcatenateRecommendationByService(ServicioId);
            InformeMedicoTrabajadorInternacional.CreateInformeMedicoTrabajadorInternacional(pathFile, Cabecera, HabitosNocivos, AntFami, Valores, diagnosticRepository, AntPersonales, AntOcupacionales, MedicalCenter, Reco);
            //System.IO.File.Copy(_ruta + Session["ServiceId"].ToString() + "-" + Constants.INFORME_FICHA_MEDICA_TRABAJADOR_CI + ".pdf", Server.MapPath("files/" + Session["ServiceId"].ToString() + "-" + Constants.INFORME_FICHA_MEDICA_TRABAJADOR_CI + ".pdf"), true);
            Session["NombreTrabajador"] = Cabecera.Trabajador.Replace(" ", "_");
            System.IO.File.Copy(_ruta + Session["NombreTrabajador"].ToString() + "-" + "Informe_Trabajador" + ".pdf", Server.MapPath("files/" + Session["NombreTrabajador"].ToString() + "-" + "Informe_Trabajador" + ".pdf"), true);
     
        }

    }
}