using CrystalDecisions.Shared;
using NetPdf;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.UI.Reports;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sigesoft.Server.WebClientAdmin.UI.ExternalUser
{
    public partial class FRM031F : System.Web.UI.Page
    {

        string _serviceId;
        DataSet dsGetRepo = null;
        private MergeExPDF _mergeExPDF = new MergeExPDF();
        protected void Page_Load(object sender, EventArgs e)
        {
            List<MyListWeb> ListaServicios = (List<MyListWeb>)Session["objLista"];
            var ListaExamenes = (List<string>)Session["objListaExamenes"];
            List<string> _filesNameToMerge = new List<string>();
            if (!IsPostBack)
            {
                _serviceId = ListaServicios[0].IdServicio;
                var rp = new crConsolidatedReports();

                foreach (var com in ListaExamenes)
                {
                    //ChooseReport(rp, com);
                }

                var ruta = Server.MapPath("files/CM" + ListaServicios[0].IdServicio + ".pdf");
                _filesNameToMerge.Add(ruta);
                rp.ExportToDisk(ExportFormatType.PortableDocFormat, ruta);

                _mergeExPDF.FilesName = _filesNameToMerge;
                string Dif = Guid.NewGuid().ToString();
                string NewPath = Server.MapPath("files/" + Dif + ".pdf");
                _mergeExPDF.DestinationFile = NewPath;               
                _mergeExPDF.Execute();
                Session["EliminarArchivo"] = NewPath;
                ShowPdf1.FilePath = "files/" + Dif + ".pdf";

                //ShowPdf1.FilePath = "files/" + Dif + ".pdf";

                //ShowPdf1.FilePath = @"D:\RepSIGSO_v1.0\dev\src\server\webclientadmin\ui\ExternalUser\files\CMN009-SR000000353.pdf";
            }
           
        }

        #region Bind Report
        private DataSet GetReportSomnolencia()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().ReporteSomnolencia(_serviceId, Constants.SOMNOLENCIA_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtTestVertigo";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }


        private DataSet GetReportTestVertigo()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportTestVertigo(_serviceId, Constants.TEST_VERTIGO_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtTestVertigo";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetReportSintomatico()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().ReporteSintomaticoRespiratorio(_serviceId, Constants.SINTOMATICO_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtSintomaticoRes";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetReportEvaErgonomica()
        {
            var dataListForReport = new ServiceBL().ReporteErgnomia(_serviceId, Constants.EVA_ERGONOMICA_ID);
      
            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt.TableName = "dtErgonomia";
           
            dsGetRepo.Tables.Add(dt);
           
            return dsGetRepo;

        }

        private DataSet GetReportOtoscopia()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().ReporteOtoscopia(_serviceId, Constants.OTOSCOPIA_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtOtoscopia";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetReportAcumetria()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().ReporteAcumetria(_serviceId, Constants.ACUMETRIA_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtAcumetria";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetReportEvaNeurologica()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportEvaNeurologica(_serviceId, Constants.EVA_NEUROLOGICA_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtEvaNeurologica";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetReportTestOjoSeco()
        {
            //OperationResult objOperationResult = new OperationResult();

            //var dataListForReport = new ServiceBL().ReporteOjoSeco(_serviceId, Constants.TESTOJOSECO_ID);

            //dsGetRepo = new DataSet();

            //DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            //dt.TableName = "dtOjoSeco";
            //dsGetRepo.Tables.Add(dt);

            //return dsGetRepo;

            return null;
        }

        private DataSet GetReportCuestionarioNordico()
        {
            //OperationResult objOperationResult = new OperationResult();

            //var dataListForReport = new ServiceBL().GetReportCuestionarioNordico(_serviceId, Constants.C_N_ID);

            //dsGetRepo = new DataSet();

            //DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            //dt.TableName = "dtCustionarioNordico";
            //dsGetRepo.Tables.Add(dt);

            //return dsGetRepo;

            return null;
        }

        private DataSet GetReportAnexo7D()
        {

            var AscensoAlturas = new Sigesoft.Server.WebClientAdmin.BLL.ServiceBL().ReportAscensoGrandesAlturas(_serviceId, Constants.ALTURA_7D_ID);
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

            return dsGetRepo;


        }

        private DataSet GetReportAlturaFisica()
        {

            var dataListForReport = new PacientBL().GetAlturaEstructural(_serviceId, Constants.ALTURA_ESTRUCTURAL_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt.TableName = "dtAlturaEstructural";

            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;

        }

        private DataSet GetReportConsentimientoInformado()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportConsentimiento(_serviceId);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtConsentimiento";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetReportCertificadoAptitud()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetAptitudeCertificate(ref objOperationResult, _serviceId);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "AptitudeCertificate";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetReportOsteomuscular1()
        {
            var dataListForReport = new ServiceBL().ReportOsteoMuscularNuevo(_serviceId, Constants.OSTEO_MUSCULAR_ID_1);
            var UC_OSTEO_ID = new ServiceBL().ReporteOsteomuscular(_serviceId, Constants.OSTEO_MUSCULAR_ID_1);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            DataTable dt_UC_OSTEO_ID = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(UC_OSTEO_ID);

            dt.TableName = "dtOsteomuscularNuevo";
            dt_UC_OSTEO_ID.TableName = "dtOsteoMus";

            dsGetRepo.Tables.Add(dt);
            dsGetRepo.Tables.Add(dt_UC_OSTEO_ID);

            return dsGetRepo;

        }

        private DataSet GetReportOsteomuscular2()
        {
            var OSTEO_MUSCULAR_ID_2 = new ServiceBL().GetMusculoEsqueletico(_serviceId, Constants.OSTEO_MUSCULAR_ID_2);
          
            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(OSTEO_MUSCULAR_ID_2);
            
            dt.TableName = "dtOstomuscular";

            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;

        }

        private DataSet GetReportHistoriaOcupacional()
        {
            var dataListForReport = new ServiceBL().ReportHistoriaOcupacional(_serviceId);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt.TableName = "HistoriaOcupacional";

            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;

        }

        private DataSet GetReportElectrocardiograma()
        {
            var dataListForReport = new ServiceBL().GetReportEstudioElectrocardiografico(_serviceId, Constants.ELECTROCARDIOGRAMA_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtEstudioElectrocardiografico";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;

        }

        private DataSet GetReportPruebaEsfuerzo()
        {
            var aptitudeCertificate = new ServiceBL().GetReportPruebaEsfuerzo(_serviceId, Constants.PRUEBA_ESFUERZO_ID);
            var FuncionesVitales = new ServiceBL().ReportFuncionesVitales(_serviceId, Constants.FUNCIONES_VITALES_ID);
            var Antropometria = new ServiceBL().ReportAntropometria(_serviceId, Constants.ANTROPOMETRIA_ID);

            dsGetRepo = new DataSet();
            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(aptitudeCertificate);
            dt.TableName = "dtPruebaEsfuerzo";
            dsGetRepo.Tables.Add(dt);

            DataTable dt1 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(FuncionesVitales);
            dt1.TableName = "dtFuncionesVitales";
            dsGetRepo.Tables.Add(dt1);

            DataTable dt2 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Antropometria);
            dt2.TableName = "dtAntropometria";
            dsGetRepo.Tables.Add(dt2);

            return dsGetRepo;

        }

        private DataSet GetReportOdontologia()
        {

            //var Path = Server.MapPath("files/");
            //var dataListForReport = new ServiceBL().ReportOdontograma(_serviceId, Constants.ODONTOGRAMA_ID, Path);

            //dsGetRepo = new DataSet();

            //DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            //dt.TableName = "dtOdontograma";
            //dsGetRepo.Tables.Add(dt);

            //return dsGetRepo;

            return null;

        }

        private DataSet GetReportOftalmologia()
        {
            var dataListForReport = new PacientBL().GetOftalmologia(_serviceId, Constants.OFTALMOLOGIA_ID);
            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt.TableName = "dtOftalmologia";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;

        }

        private DataSet GetReportOftalmologiaAntiguo()
        {
            var dataListForReport = new PacientBL().GetReportOftalmologiaAntiguo(_serviceId, Constants.OFTALMOLOGIA_ID);
            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt.TableName = "dtOftalmologiaAntiguo";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;

        }

        private DataSet GetReportPsicologia()
        {
            var dataListForReport = new PacientBL().GetFichaPsicologicaOcupacional(_serviceId);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt.TableName = "InformePsico";

            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;

        }

        private DataSet GetReportRX()
        {
            //var rp = new crInformeRadiologico();

            var dataListForReport = new ServiceBL().ReportRadiologico(_serviceId, Constants.RX_TORAX_ID);
            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt.TableName = "dtRadiologico";

            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;

        }

        private DataSet GetReportInformeRadiograficoOIT()
        {
            var dataListForReport = new ServiceBL().ReportInformeRadiografico(_serviceId, Constants.OIT_ID);

            dsGetRepo = new DataSet();
            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtInformeRadiografico";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;

        }

        private DataSet GetReportTamizajeDermatologico()
        {
            var dataListForReport = new ServiceBL().ReportTamizajeDermatologico(_serviceId, Constants.TAMIZAJE_DERMATOLOGIO_ID);

            dsGetRepo = new DataSet();
            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtTamizajeDermatologico";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetReportGinecologia()
        {
            var dataListForReport = new ServiceBL().GetReportEvaluacionGinecologico(_serviceId, Constants.GINECOLOGIA_ID);

            dsGetRepo = new DataSet();
            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtEvaGinecologico";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetReportEspirometriaCuestionario()
        {
            var dataListForReport = new ServiceBL().GetReportCuestionarioEspirometria(_serviceId, Constants.ESPIROMETRIA_ID);

            dsGetRepo = new DataSet();
            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtCuestionarioEspirometria";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetReportEspirometria()
        {
            var dataListForReport = new ServiceBL().GetReportCuestionarioEspirometria(_serviceId, Constants.ESPIROMETRIA_ID);

            dsGetRepo = new DataSet();
            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtCuestionarioEspirometria";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetReportOsteo()
        {
            var dataListForReport = new ServiceBL().GetReportOsteo(_serviceId, Constants.OSTEO_MUSCULAR_ID_1);

            dsGetRepo = new DataSet();
            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtOsteo";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetReportEvaluacionPsicolaboralPersonal()
        {
            var dataListForReport = new ServiceBL().GetReportEvaluacionPsicolaborlaPersonal(_serviceId, Constants.EVALUACION_PSICOLABORAL);

            dsGetRepo = new DataSet();
            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtEvaluacionPsicolaboralPersonal";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetReportAudiometria()
        {
            try
            {
                var serviceBL = new ServiceBL();
                DataSet dsAudiometria = new DataSet();

                var dxList = serviceBL.GetDiagnosticRepositoryByComponent(_serviceId, Constants.AUDIOMETRIA_ID);
                var dtDx = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dxList);
                dtDx.TableName = "dtDiagnostic";
                dsAudiometria.Tables.Add(dtDx);

                //// Obtener las recomendaciones (removiendo las duplicadas)
                //var recom = dxList.SelectMany(s => s.Recomendations)
                //                  .GroupBy(x => x.v_RecommendationName)
                //                  .Select(group => group.First())
                //                  .ToList();

                var recom = dxList.SelectMany(s => s.Recomendations).ToList();

                //DataTable dtRecom = new DataTable();
                //dtRecom.TableName = "dtRecomendation";
                //dtRecom.Columns.Add("v_RecommendationName", typeof(string));

                //foreach (var item in recom)
                //{
                //    var row = dtRecom.NewRow();
                //    row["v_RecommendationName"] = item.v_RecommendationName;
                //    dtRecom.Rows.Add(row);
                //}

                //dsAudiometria.Tables.Add(dtRecom);

                var dtReco = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(recom);
                dtReco.TableName = "dtRecomendation";
                dsAudiometria.Tables.Add(dtReco);

                //-------******************************************************************************************

                var audioUserControlList = serviceBL.ReportAudiometriaUserControl(_serviceId, Constants.AUDIOMETRIA_ID);
                //aqui hay error corregir despues del cine
                var audioCabeceraList = serviceBL.ReportAudiometria(_serviceId, Constants.AUDIOMETRIA_ID);

                var dtAudiometriaUserControl = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(audioUserControlList);

                var dtCabecera = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(audioCabeceraList);


                dtCabecera.TableName = "dtAudiometria";
                dtAudiometriaUserControl.TableName = "dtAudiometriaUserControl";

                dsAudiometria.Tables.Add(dtCabecera);
                dsAudiometria.Tables.Add(dtAudiometriaUserControl);



                return dsAudiometria;
            }
            catch (Exception)
            {

                throw;
            }


        }

        private DataSet GetReportHistoriaOcupacionalAudiometria()
        {
            var serviceBL = new ServiceBL();
            var dataListForReport = serviceBL.ReportHistoriaOcupacionalAudiometria(_serviceId);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt.TableName = "dtHistoriaOcupacional";

            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;

        }

        #endregion

        //private void ChooseReport(crConsolidatedReports rp, string componentId)
        //{
        //    DataSet ds = null;

        //    switch (componentId)
        //    {
        //        //case Constants.INFORME_CERTIFICADO_APTITUD:
        //        //    ds = GetReportCertificadoAptitud();
        //        //    rp.Subreports["crOccupationalMedicalAptitudeCertificate.rpt"].SetDataSource(ds);
        //        //    rp.SectionCertificadoAptitud.SectionFormat.EnableSuppress = false;
        //        //    break;
        //        case Constants.SOMNOLENCIA_ID:
        //            ds = GetReportSomnolencia();
        //            rp.Subreports["crTestEpwotrh.rpt"].SetDataSource(ds);
        //            rp.DetailSection31.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.TEST_VERTIGO_ID:
        //            ds = GetReportTestVertigo();
        //            rp.Subreports["crTestDeVertigo.rpt"].SetDataSource(ds);
        //            rp.DetailSection30.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.SINTOMATICO_ID:
        //            ds = GetReportSintomatico();
        //            rp.Subreports["crSintomaticoResp.rpt"].SetDataSource(ds);
        //            rp.DetailSection29.SectionFormat.EnableSuppress = false;
        //            break;

        //        case Constants.OTOSCOPIA_ID:
        //            ds = GetReportOtoscopia();
        //            rp.Subreports["crFichaOtoscopia.rpt"].SetDataSource(ds);
        //            rp.DetailSection24.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.ACUMETRIA_ID:
        //            ds = GetReportAcumetria();
        //            rp.Subreports["crFichaAcumetria.rpt"].SetDataSource(ds);
        //            rp.DetailSection23.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.EVA_NEUROLOGICA_ID:
        //            ds = GetReportEvaNeurologica();
        //            rp.Subreports["crEvaluacionNeurologica.rpt"].SetDataSource(ds);
        //            rp.DetailSection23.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.TESTOJOSECO_ID:
        //            ds = GetReportTestOjoSeco();
        //            rp.Subreports["crCuestionarioOjoSeco.rpt"].SetDataSource(ds);
        //            rp.DetailSection22.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.C_N_ID:
        //            ds = GetReportCuestionarioNordico();
        //            rp.Subreports["crCuestionarioNordico.rpt"].SetDataSource(ds);
        //            rp.DetailSection21.SectionFormat.EnableSuppress = false;
        //            break;

        //        case Constants.CONSENTIMIENTO_INFORMADO:
        //            ds = GetReportConsentimientoInformado();
        //            rp.Subreports["crConsentimiento.rpt"].SetDataSource(ds);
        //            rp.DetailSection19.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.OSTEO_MUSCULAR_ID_1:
        //            // Osteomuscular1
        //            ds = GetReportOsteomuscular1();
        //            rp.Subreports["crMuscoloEsqueletico.rpt"].SetDataSource(ds);
        //            rp.SectionOsteomuscular1.SectionFormat.EnableSuppress = false;
                  
        //            rp.Subreports["crMuscoloEsqueletico2.rpt"].SetDataSource(ds);
        //            rp.SectionOsteomuscular2.SectionFormat.EnableSuppress = false;                                     
        //            break;

        //        case Constants.EVA_ERGONOMICA_ID:
        //            // Osteomuscular1
        //            ds = GetReportEvaErgonomica();
        //            rp.Subreports["crEvaluacionErgonomica01.rpt"].SetDataSource(ds);
        //            rp.DetailSection25.SectionFormat.EnableSuppress = false;

        //            rp.Subreports["crEvaluacionErgonomica02.rpt"].SetDataSource(ds);
        //            rp.DetailSection26.SectionFormat.EnableSuppress = false;

        //            rp.Subreports["crEvaluacionErgonomica03.rpt"].SetDataSource(ds);
        //            rp.DetailSection27.SectionFormat.EnableSuppress = false;
        //            break;


        //        case Constants.OSTEO_MUSCULAR_ID_2:
        //            // Osteomuscular1
        //            ds = GetReportOsteomuscular2();
        //            rp.Subreports["crEvaluacionOsteomuscular.rpt"].SetDataSource(ds);
        //            rp.DetailSection20.SectionFormat.EnableSuppress = false;
                   
        //            break;
        //        case Constants.INFORME_HISTORIA_OCUPACIONAL:
        //            ds = GetReportHistoriaOcupacional();
        //            rp.Subreports["crHistoriaOcupacional.rpt"].SetDataSource(ds);
        //            rp.SectionHistoriaOcupacional.SectionFormat.EnableSuppress = false;
        //            rp.SectionHistoriaOcupacional.SectionFormat.PageOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
        //            rp.SectionHistoriaOcupacional.ReportObjects["SubReportHistoriaOcupacional"].Width = 15905;
        //            break;
        //        case Constants.ALTURA_7D_ID:
        //            ds = GetReportAnexo7D();
        //            rp.Subreports["crAnexo7D.rpt"].SetDataSource(ds);
        //            rp.SectionAnexo7D.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.ALTURA_ESTRUCTURAL_ID:
        //            ds = GetReportAlturaFisica();
        //            rp.Subreports["crAlturaMayor.rpt"].SetDataSource(ds);
        //            rp.SectionAlturaEstructural.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.INFORME_LABORATORIO_ID:      // Falta implementar
        //            //rp.SectionLaboratorio.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.ELECTROCARDIOGRAMA_ID:
        //            ds = GetReportElectrocardiograma();
        //            rp.Subreports["crEstudioElectrocardiografico.rpt"].SetDataSource(ds);
        //            rp.SectionElectrocardiograma.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.PRUEBA_ESFUERZO_ID:
        //            ds = GetReportPruebaEsfuerzo();
        //            rp.Subreports["crPruebaEsfuerzo.rpt"].SetDataSource(ds);
        //            rp.SectionPruebaEsfuerzo.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.ODONTOGRAMA_ID:
        //            ds = GetReportOdontologia();
        //            rp.Subreports["crOdontograma.rpt"].SetDataSource(ds);
        //            rp.SectionOdontologia.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.AUDIOMETRIA_ID:      // Falta implementar
        //            ds = GetReportAudiometria();
        //            rp.Subreports["crFichaAudiometria.rpt"].SetDataSource(ds);
        //            rp.SectionAudiometria.SectionFormat.EnableSuppress = false;

        //            // Historia Ocupacional Audiometria
        //            ds = GetReportHistoriaOcupacionalAudiometria();
        //            rp.Subreports["crHistoriaOcupacionalAudiometria.rpt"].SetDataSource(ds);
        //            rp.SectionHistoriaOcupacionalAudiometria.SectionFormat.EnableSuppress = false;
        //            rp.SectionHistoriaOcupacionalAudiometria.SectionFormat.PageOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
        //            rp.SectionHistoriaOcupacionalAudiometria.ReportObjects["SubReportHistoriaOcupacionalAudiometria"].Width = 15905;
        //            break;
        //        case Constants.GINECOLOGIA_ID:      // Falta implementar
        //            ds = GetReportGinecologia();
        //            rp.Subreports["crEvaluacionGenecologica.rpt"].SetDataSource(ds);
        //            rp.SectionGinecologia.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.OFTALMOLOGIA_ID:
        //            var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();

        //            if (MedicalCenter.v_IdentificationNumber == "20550353858")
        //            {
        //                ds = GetReportOftalmologiaAntiguo();
        //                rp.Subreports["crOftalmologia_S_J.rpt"].SetDataSource(ds);
        //                rp.SectionOftalmologia.SectionFormat.EnableSuppress = false;
        //            }
        //            else
        //            {
        //                ds = GetReportOftalmologia();
        //                rp.Subreports["crOftalmologia.rpt"].SetDataSource(ds);
        //                rp.DetailSection28.SectionFormat.EnableSuppress = false;
        //            }
                
        //            break;
        //        case Constants.PSICOLOGIA_ID:
        //            ds = GetReportPsicologia();
        //            rp.Subreports["InformePsicologicoOcupacional.rpt"].SetDataSource(ds);
        //            rp.SectionPsicologia.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.RX_TORAX_ID:
        //            ds = GetReportRX();
        //            rp.Subreports["crInformeRadiologico.rpt"].SetDataSource(ds);
        //            rp.SectionRayosX.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.INFORME_RADIOGRAFICO_OIT:
        //            ds = GetReportInformeRadiograficoOIT();
        //            rp.Subreports["crInformeRadiograficoOIT.rpt"].SetDataSource(ds);
        //            rp.SectionRayosXOIT.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.TAMIZAJE_DERMATOLOGIO_ID:
        //            ds = GetReportTamizajeDermatologico();
        //            rp.Subreports["crTamizajeDermatologico.rpt"].SetDataSource(ds);
        //            rp.SectionTamizajeDermatologico.SectionFormat.EnableSuppress = false;
        //            break;
        //        case Constants.ESPIROMETRIA_ID:
        //            ds = GetReportEspirometriaCuestionario();
        //            rp.Subreports["crCuestionarioEspirometria.rpt"].SetDataSource(ds);
        //            rp.SectionEspirometriaCuestionario.SectionFormat.EnableSuppress = false;
        //            break;
        //        //case Constants.ESPIROMETRIA_ID:
        //        //    ds = GetReportEspirometria();
        //        //    rp.Subreports["crInformeEspirometria.rpt"].SetDataSource(ds);
        //        //    rp.SectionEspirometria.SectionFormat.EnableSuppress = false;
        //        //    break;
        //        case Constants.EVALUACION_PSICOLABORAL:
        //            ds = GetReportEvaluacionPsicolaboralPersonal();
        //            rp.Subreports["crEvaluacionPsicolaboralPersonal.rpt"].SetDataSource(ds);
        //            rp.SectionEvaluacionPsicolaboralPersonal.SectionFormat.EnableSuppress = false;
        //            break;
        //        default:
        //            break;
        //    }
        //}

    }
}