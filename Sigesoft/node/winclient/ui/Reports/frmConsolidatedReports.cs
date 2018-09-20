using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CrystalDecisions.CrystalReports.Engine;

using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using CrystalDecisions.Shared;

namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmConsolidatedReports : Form
    {
        private string _serviceId;
        private List<string> _componentId;
        DataSet dsGetRepo = null;
        private ServiceBL serviceBL = new ServiceBL();
        crConsolidatedReports rp = null;
        List<ServiceComponentList> _ListaDosaje = new List<ServiceComponentList>();
        public frmConsolidatedReports()
        {
            InitializeComponent();
        }

        public frmConsolidatedReports(string serviceId, List<string> componentId, List<ServiceComponentList> ListaDosaje)
        {
            InitializeComponent();
            _serviceId = serviceId;
            _componentId = componentId;
            _ListaDosaje = ListaDosaje;
            //_serviceId = "N009-SR000000213";
            //_ComponentId = "N002-ME000000045";
        }

        private void frmConsolidatedReports_Load(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                rp = new Reports.crConsolidatedReports();

                foreach (var com in _componentId)
                {
                    ChooseReport(rp, com);
                }

                crystalReportViewer1.EnableDrillDown = false;
                crystalReportViewer1.ReportSource = rp;

                ReportDocument repDoc = rp;

                repDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                repDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
                objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\Crystal1.pdf";
                repDoc.ExportOptions.DestinationOptions = objDiskOpt;
                repDoc.Export();
                //
                //crystalReportViewer1.Show();
            }
        }

        #region Bind Report
        
        private DataSet GetReportAnexo7D()
        {
            var servicesId1 = new List<string>();
            servicesId1.Add(_serviceId);
            var componentReportId1 = new ServiceBL().ObtenerIdsParaImportacionExcel(servicesId1, 11);

            var AscensoAlturas = new ServiceBL().ReportAscensoGrandesAlturas(_serviceId, Constants.ALTURA_7D_ID, componentReportId1[0].ComponentId);     
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
            var servicesId8 = new List<string>();
            servicesId8.Add(_serviceId);
            var componentReportId8 = new ServiceBL().ObtenerIdsParaImportacionExcel(servicesId8, 11);
            var dataListForReport = new PacientBL().GetAlturaEstructural(_serviceId, Constants.ALTURA_ESTRUCTURAL_ID, componentReportId8[0].ComponentId);
             
            //var dataListForReport = new PacientBL().GetAlturaEstructural(_serviceId, Constants.ALTURA_ESTRUCTURAL_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt.TableName = "dtAlturaEstructural";

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
            var dataListForReport = new ServiceBL().ReportOsteoMuscular(_serviceId, Constants.OSTEO_MUSCULAR_ID_2);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt.TableName = "dtOstomuscular";

            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
           

        }

        private DataSet GetReportOsteomuscular2()
        {


            var dataListForReport = new PacientBL().GetMusculoEsqueletico(_serviceId, Constants.OSTEO_MUSCULAR_ID_1);
            //var dataListForReport = new PacientBL().GetMusculoEsqueletico(_serviceId, Constants.OSTEO_MUSCULAR_ID_2);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt.TableName = "dtMusculoEsqueletico";

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
                     
            var Path = Application.StartupPath;
            var dataListForReport = new ServiceBL().ReportOdontograma(_serviceId, Constants.ODONTOGRAMA_ID, Path);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtOdontograma";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;

         
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
            var rp = new Reports.crInformeRadiologico();

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
            var dataListForReport = new ServiceBL().GetReportInformeEspirometria(_serviceId, Constants.ESPIROMETRIA_ID);

            dsGetRepo = new DataSet();
            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtInformeEspirometria";
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
            var dataListForReport = serviceBL.ReportHistoriaOcupacionalAudiometria(_serviceId);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt.TableName = "dtHistoriaOcupacional";

            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;

        }

        private DataSet GetTestOjoSeco()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportCuestionarioOjoSeco(_serviceId, Constants.TESTOJOSECO_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtCuestionarioOjoSeco";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetAutorizacionDrogas()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportAutorizacionDosajeDrogas(_serviceId, Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID, _ListaDosaje);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtAutorizacionDosajeDrogas";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetAntigenosFebriles()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportAntigenosFebriles(_serviceId, Constants.AGLUTINACIONES_LAMINA_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtAntigenosFebriles";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetParasitologicoSeriado()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportParasitologicoSeriado(_serviceId, Constants.PARASITOLOGICO_SERIADO_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtarasitologicoSeriado";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }


        private DataSet GetParasitologicoSimple()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportParasitologicoSimple(_serviceId, Constants.PARASITOLOGICO_SIMPLE_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtParasitologicoSimple";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetHemogramaCompleto()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportHemogramaCompleto(_serviceId, Constants.HEMOGRAMA_COMPLETO_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtHemogramaCompleto";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetExamenCompletoOrina()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportExamenCompletoOrina(_serviceId, Constants.EXAMEN_COMPLETO_DE_ORINA_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtExamenOrinaCompleto";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }


        private DataSet GetCuestionarioNordico()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportCuestionarioNordico(_serviceId, Constants.C_N_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtCustionarioNordico";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetCuestionarioActividadFisica()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportCuestionarioActividadFisica(_serviceId, Constants.CUESTIONARIO_ACTIVIDAD_FISICA);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtCustionarioActividadFisica";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetInformeEcograficoProstata()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportInformeEcograficoProstata(_serviceId, Constants.INFORME_ECOGRAFICO_PROSTATA_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtInformeEcograficoProstata";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetInformeEcograficoAbdominal()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportInformeEcograficoAbdominal(_serviceId, Constants.ECOGRAFIA_ABDOMINAL_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtInformeEcograficoAbdominal";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetInformeEcograficoRenal()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportInformeEcograficoRenal(_serviceId, Constants.ECOGRAFIA_RENAL_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtInformeEcograficoRenal";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetTestVertigo()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportTestVertigo(_serviceId, Constants.TEST_VERTIGO_ID,"");

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtTestVertigo";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetEvaCardiologica()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportEvaluacionCardiologia(_serviceId, Constants.EVA_CARDIOLOGICA_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtEvaCardiologia";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetEvaOsteoSanMartin()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportEvaOsteoSanMartin(_serviceId, Constants.EVA_OSTEO_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtEvaOsteo";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetHistoriaClinicaPsicologica()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetHistoriaClinicaPsicologica(_serviceId, Constants.HISTORIA_CLINICA_PSICOLOGICA_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtHistoriaClinicaPsicologica";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetEvaNeurologica()
        {
            OperationResult objOperationResult = new OperationResult();

            var dataListForReport = new ServiceBL().GetReportEvaNeurologica(_serviceId, Constants.EVA_NEUROLOGICA_ID);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "dtEvaNeurologica";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        #endregion

        private void ChooseReport(crConsolidatedReports rp, string componentId)
        {
            DataSet ds = null;

            switch (componentId)
            {
                case Constants.INFORME_CERTIFICADO_APTITUD:
                    ds = GetReportCertificadoAptitud();
                    var s = ds.Tables[0].Rows[0]["i_EsoTypeId"].ToString();
                    if (s == "3")
                    {
                        rp.Subreports["crOccupationalMedicalAptitudeCertificateRetiros.rpt"].SetDataSource(ds);
                        rp.DetailSection9.SectionFormat.EnableSuppress = false;

                    }
                    else
                    {
                        rp.Subreports["crOccupationalMedicalAptitudeCertificate.rpt"].SetDataSource(ds);
                        rp.SectionCertificadoAptitud.SectionFormat.EnableSuppress = false;
                    }

                    break;
                case Constants.OSTEO_MUSCULAR_ID_1:
                    // Osteomuscular2
                    ds = GetReportOsteomuscular2();
                    rp.Subreports["crMuscoloEsqueletico.rpt"].SetDataSource(ds);
                    rp.SectionOsteomuscular1.SectionFormat.EnableSuppress = false;

                    // Osteomuscular3
                    ds = GetReportOsteo();
                    rp.Subreports["crOsteo.rpt"].SetDataSource(ds);
                    rp.SectionOsteomuscular2.SectionFormat.EnableSuppress = false;
                    break;

                case Constants.OSTEO_MUSCULAR_ID_2:
                    // Osteomuscular1
                    ds = GetReportOsteomuscular1();
                    rp.Subreports["crEvaluacionOsteomuscular.rpt"].SetDataSource(ds);
                    rp.SectionOsteomuscular1.SectionFormat.EnableSuppress = false;
                    break;

                case Constants.INFORME_HISTORIA_OCUPACIONAL:
                    ds = GetReportHistoriaOcupacional();
                    rp.Subreports["crHistoriaOcupacional.rpt"].SetDataSource(ds);
                    rp.SectionHistoriaOcupacional.SectionFormat.EnableSuppress = false;
                    rp.SectionHistoriaOcupacional.SectionFormat.PageOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
                    rp.SectionHistoriaOcupacional.ReportObjects["SubReportHistoriaOcupacional"].Width = 15905;                 
                    break;
                case Constants.ALTURA_7D_ID:
                    ds = GetReportAnexo7D();
                    rp.Subreports["crAnexo7D.rpt"].SetDataSource(ds);
                    rp.SectionAnexo7D.SectionFormat.EnableSuppress = false;                 
                    break;
                case Constants.ALTURA_ESTRUCTURAL_ID:
                    ds = GetReportAlturaFisica();
                    rp.Subreports["crAlturaMayor.rpt"].SetDataSource(ds);
                    rp.SectionAlturaEstructural.SectionFormat.EnableSuppress = false;
                    break;
                case Constants.INFORME_LABORATORIO_ID:      // Falta implementar
                    //rp.SectionLaboratorio.SectionFormat.EnableSuppress = false;
                    break;               
                case Constants.ELECTROCARDIOGRAMA_ID:
                    ds = GetReportElectrocardiograma();
                    rp.Subreports["crEstudioElectrocardiografico.rpt"].SetDataSource(ds);
                    rp.SectionElectrocardiograma.SectionFormat.EnableSuppress = false;
                    break;
                case Constants.PRUEBA_ESFUERZO_ID:
                    ds = GetReportPruebaEsfuerzo();
                    rp.Subreports["crPruebaEsfuerzo.rpt"].SetDataSource(ds);
                    rp.SectionPruebaEsfuerzo.SectionFormat.EnableSuppress = false;
                    break;             
                case Constants.ODONTOGRAMA_ID:
                    ds = GetReportOdontologia();
                    rp.Subreports["crOdontograma.rpt"].SetDataSource(ds);
                    rp.SectionOdontologia.SectionFormat.EnableSuppress = false;
                    break;
                case Constants.AUDIOMETRIA_ID:      // Falta implementar
                    ds = GetReportAudiometria();
                    rp.Subreports["crFichaAudiometria.rpt"].SetDataSource(ds);
                    rp.SectionAudiometria.SectionFormat.EnableSuppress = false;

                    // Historia Ocupacional Audiometria
                    ds = GetReportHistoriaOcupacionalAudiometria();
                    rp.Subreports["crHistoriaOcupacionalAudiometria.rpt"].SetDataSource(ds);
                    rp.SectionHistoriaOcupacionalAudiometria.SectionFormat.EnableSuppress = false;
                    rp.SectionHistoriaOcupacionalAudiometria.SectionFormat.PageOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
                    rp.SectionHistoriaOcupacionalAudiometria.ReportObjects["SubReportHistoriaOcupacionalAudiometria"].Width = 15905;
                    break;
                case Constants.GINECOLOGIA_ID:      // Falta implementar
                    ds = GetReportGinecologia();
                    rp.Subreports["crEvaluacionGenecologica.rpt"].SetDataSource(ds);
                    rp.SectionGinecologia.SectionFormat.EnableSuppress = false;
                    break;
                case Constants.OFTALMOLOGIA_ID:
                    ds = GetReportOftalmologia();
                    rp.Subreports["crOftalmologia.rpt"].SetDataSource(ds);
                    rp.SectionOftalmologia.SectionFormat.EnableSuppress = false;
                    break;
                case Constants.PSICOLOGIA_ID:
                    ds = GetReportPsicologia();
                    rp.Subreports["InformePsicologicoOcupacional.rpt"].SetDataSource(ds);
                    rp.SectionPsicologia.SectionFormat.EnableSuppress = false;
                    break;
                case Constants.RX_TORAX_ID:
                    ds = GetReportRX();
                    rp.Subreports["crInformeRadiologico.rpt"].SetDataSource(ds);
                    rp.SectionRayosX.SectionFormat.EnableSuppress = false;
                    break;
                case Constants.OIT_ID:
                    ds = GetReportInformeRadiograficoOIT();
                    rp.Subreports["crInformeRadiograficoOIT.rpt"].SetDataSource(ds);
                    rp.SectionRayosXOIT.SectionFormat.EnableSuppress = false;
                    break;
                case Constants.TAMIZAJE_DERMATOLOGIO_ID:
                    ds = GetReportTamizajeDermatologico();
                    rp.Subreports["crTamizajeDermatologico.rpt"].SetDataSource(ds);
                    rp.SectionTamizajeDermatologico.SectionFormat.EnableSuppress = false;
                    break;
                case Constants.ESPIROMETRIA_ID:
                    ds = GetReportEspirometriaCuestionario();
                    rp.Subreports["crCuestionarioEspirometria.rpt"].SetDataSource(ds);
                    rp.SectionEspirometriaCuestionario.SectionFormat.EnableSuppress = false;
                    break;
                //case Constants.ESPIROMETRIA_ID:
                //    ds = GetReportEspirometria();
                //    rp.Subreports["crInformeEspirometria.rpt"].SetDataSource(ds);
                //    rp.SectionEspirometria.SectionFormat.EnableSuppress = false;
                //    break;
                case Constants.EVALUACION_PSICOLABORAL:
                    ds = GetReportEvaluacionPsicolaboralPersonal();
                    rp.Subreports["crEvaluacionPsicolaboralPersonal.rpt"].SetDataSource(ds);
                    rp.SectionEvaluacionPsicolaboralPersonal.SectionFormat.EnableSuppress = false;
                    break;
                case Constants.TESTOJOSECO_ID:
                    ds = GetTestOjoSeco();
                    rp.Subreports["crCuestionarioOjoSeco.rpt"].SetDataSource(ds);
                    rp.DetailSection1.SectionFormat.EnableSuppress = false;
                    break;

                case Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID:
                    ds = GetAutorizacionDrogas();
                    rp.Subreports["crAutorizacionDosajeDrogras.rpt"].SetDataSource(ds);
                    rp.DetailSection2.SectionFormat.EnableSuppress = false;
                    break;

                case Constants.AGLUTINACIONES_LAMINA_ID:
                    ds = GetAntigenosFebriles();
                    rp.Subreports["crAntigenosFebriles.rpt"].SetDataSource(ds);
                    rp.DetailSection3.SectionFormat.EnableSuppress = false;
                    break;

                    ///////////////////////////////////////
                case Constants.EXAMEN_COMPLETO_DE_ORINA_ID:
                    ds = GetExamenCompletoOrina();
                    rp.Subreports["crExamenCompletoOrina.rpt"].SetDataSource(ds);
                    rp.DetailSection4.SectionFormat.EnableSuppress = false;
                    break;

                case Constants.HEMOGRAMA_COMPLETO_ID:
                    ds = GetHemogramaCompleto();
                    rp.Subreports["crHemogramaCompleto.rpt"].SetDataSource(ds);
                    rp.DetailSection5.SectionFormat.EnableSuppress = false;
                    break;

                case Constants.PARASITOLOGICO_SIMPLE_ID:
                    ds = GetParasitologicoSimple();
                    rp.Subreports["crParasitologicoSimple.rpt"].SetDataSource(ds);
                    rp.DetailSection6.SectionFormat.EnableSuppress = false;
                    break;

                case Constants.PARASITOLOGICO_SERIADO_ID:
                    ds = GetParasitologicoSeriado();
                    rp.Subreports["crParasitologicoSeriado.rpt"].SetDataSource(ds);
                    rp.DetailSection7.SectionFormat.EnableSuppress = false;
                    break;

                case Constants.C_N_ID:
                    ds = GetCuestionarioNordico();
                    rp.Subreports["crCuestionarioNordico.rpt"].SetDataSource(ds);
                    rp.DetailSection8.SectionFormat.EnableSuppress = false;
                    break;
                case Constants.CUESTIONARIO_ACTIVIDAD_FISICA:
                    ds = GetCuestionarioActividadFisica();
                    rp.Subreports["crCuestionarioActividadFisica.rpt"].SetDataSource(ds);
                    rp.DetailSection10.SectionFormat.EnableSuppress = false;
                    break;
                case Constants.INFORME_ECOGRAFICO_PROSTATA_ID:
                    ds = GetInformeEcograficoProstata();
                    rp.Subreports["crInformeEcograficoProstata.rpt"].SetDataSource(ds);
                    rp.DetailSection11.SectionFormat.EnableSuppress = false;
                    break;

                case Constants.ECOGRAFIA_ABDOMINAL_ID:
                    ds = GetInformeEcograficoAbdominal();
                    rp.Subreports["crInformeEcograficoAbdominal.rpt"].SetDataSource(ds);
                    rp.DetailSection12.SectionFormat.EnableSuppress = false;
                    break;

                case Constants.ECOGRAFIA_RENAL_ID:
                    ds = GetInformeEcograficoRenal();
                    rp.Subreports["crInformeEcograficoRenal.rpt"].SetDataSource(ds);
                    rp.DetailSection13.SectionFormat.EnableSuppress = false;
                    break;

                case Constants.TEST_VERTIGO_ID:
                    ds = GetTestVertigo();
                    rp.Subreports["crTestDeVertigo.rpt"].SetDataSource(ds);
                    rp.DetailSection15.SectionFormat.EnableSuppress = false;
                    break;
                    
                case Constants.EVA_CARDIOLOGICA_ID:
                    ds = GetEvaCardiologica();
                    rp.Subreports["crEvaluacionCardiologicaSM.rpt"].SetDataSource(ds);
                    rp.DetailSection14.SectionFormat.EnableSuppress = false;
                    break;
                case Constants.EVA_OSTEO_ID:
                    ds = GetEvaOsteoSanMartin();
                    rp.Subreports["crOsteoSanMartin.rpt"].SetDataSource(ds);
                    rp.DetailSection16.SectionFormat.EnableSuppress = false;
                    break;
                case Constants.HISTORIA_CLINICA_PSICOLOGICA_ID:
                    ds = GetHistoriaClinicaPsicologica();
                    rp.Subreports["crHistoriaClinicaPsicologica.rpt"].SetDataSource(ds);
                    rp.DetailSection17.SectionFormat.EnableSuppress = false;
                    break;
                case Constants.EVA_NEUROLOGICA_ID:
                    ds = GetEvaNeurologica();
                    rp.Subreports["crEvaluacionNeurologica.rpt"].SetDataSource(ds);
                    rp.DetailSection18.SectionFormat.EnableSuppress = false;
                    break;
                default:                   
                    break;
            }
        }

        private void frmConsolidatedReports_FormClosing(object sender, FormClosingEventArgs e)
        {
            rp.Close();
            rp.Dispose();
            crystalReportViewer1.Dispose();
        }

        private void frmConsolidatedReports_Activated(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.TopMost = false;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

    }
}
