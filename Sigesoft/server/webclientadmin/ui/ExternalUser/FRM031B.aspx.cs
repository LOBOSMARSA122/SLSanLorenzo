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
    public partial class FRM031B : System.Web.UI.Page
    {
        private string _tempSourcePath;
        private MergeExPDF _mergeExPDF = new MergeExPDF();

        protected void Page_Load(object sender, EventArgs e)
        {
            ReportDocument rp = new ReportDocument();
            OperationResult objOperationResult = new OperationResult();
            List<string> _filesNameToMerge = new List<string>();
            string  ServiceId=null;
            //setear dato de aptitud
            if (Request.QueryString["v_ServiceId"] != null)
                ServiceId = Request.QueryString["v_ServiceId"].ToString();


            if ( Session["ConsultorioId"] == null)
            {

            _tempSourcePath = Path.Combine(Server.MapPath("/TempMerge"));
            List<MyListWeb> ListaServicios = (List<MyListWeb>)Session["objLista"];

            foreach (var item in ListaServicios)
            {
                var aptitudeCertificate = new ServiceBL().GetAptitudeCertificate(ref objOperationResult, item.IdServicio).ToList();

                DataSet ds = new DataSet();
                DataTable dt = Sigesoft.Server.WebClientAdmin.UI.Utils.ConvertToDatatable(aptitudeCertificate);
                dt.TableName = "AptitudeCertificate";
                ds.Tables.Add(dt);

                if (aptitudeCertificate[0].i_Age == (int)EmpresaDx.ConDx)
                {
                    if (aptitudeCertificate[0].i_EsoTypeId == ((int)TypeESO.Retiro).ToString())
                    {
                        rp.Load(Server.MapPath("crOccupationalMedicalAptitudeCertificateRetiros.rpt"));
                    }
                    else
                    {
                        if (aptitudeCertificate[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs)
                        {
                            rp.Load(Server.MapPath("cr.CertficadoObservado.rpt"));
                        }
                        else
                        {
                            rp.Load(Server.MapPath("crOccupationalMedicalAptitudeCertificate.rpt"));

                        }
                    }
                }
                else
                {
                    if (aptitudeCertificate[0].i_EsoTypeId == ((int)TypeESO.Retiro).ToString())
                    {
                        rp.Load(Server.MapPath("crOccupationalRetirosSinDx.rpt"));
                    }
                    else
                    {
                        if (aptitudeCertificate[0].i_AptitudeStatusId == (int)AptitudeStatus.AptoObs)
                        {
                            rp.Load(Server.MapPath("cr.CertficadoObservadoSinDx.rpt"));
                        }
                        else
                        {
                            rp.Load(Server.MapPath("crOccupationalMedicalAptitudeCertificateSinDx.rpt"));

                        }
                    }
                }

                rp.SetDataSource(ds);

                var ruta = Server.MapPath("files/CM" + item.IdServicio.ToString() + ".pdf");


                _filesNameToMerge.Add(ruta);

                rp.ExportToDisk(ExportFormatType.PortableDocFormat, ruta);
            }
            _mergeExPDF.FilesName = _filesNameToMerge;
            string Dif = Guid.NewGuid().ToString();
            string NewPath = Server.MapPath("files/" + Dif + ".pdf");
            _mergeExPDF.DestinationFile = NewPath;
            _mergeExPDF.Execute();
            ShowPdf1.FilePath = "files/" + Dif + ".pdf";

            }




           else  if ( Session["ConsultorioId"].ToString() == "6") // RX
            {

                var RX_TORAX_ID = new ServiceBL().ReportRadiologico(ServiceId, Constants.RX_TORAX_ID);

                DataSet ds = new DataSet();
                DataTable dt = Sigesoft.Server.WebClientAdmin.UI.Utils.ConvertToDatatable(RX_TORAX_ID);
                dt.TableName = "dtRadiologico";
                ds.Tables.Add(dt);

                rp.Load(Server.MapPath("crInformeRadiologico.rpt"));

                rp.SetDataSource(ds);

                rp.SetDataSource(ds);
                var ruta = Server.MapPath("files/CM" + ServiceId + ".pdf");


                _filesNameToMerge.Add(ruta);


                var OIT_ID = new ServiceBL().ReportInformeRadiografico(ServiceId, Constants.OIT_ID);
                if (OIT_ID.Count != 0)
                {
                    DataSet dsOIT_ID = new DataSet();
                    DataTable dtOIT_ID = Sigesoft.Server.WebClientAdmin.UI.Utils.ConvertToDatatable(OIT_ID);
                    dtOIT_ID.TableName = "dtInformeRadiografico";
                    dsOIT_ID.Tables.Add(dt);

                    rp.Load(Server.MapPath("crInformeRadiograficoOIT.rpt"));

                    rp.SetDataSource(ds);

                    rp.SetDataSource(ds);
                    var ruta1 = Server.MapPath("files/CM" + ServiceId + ".pdf");


                    _filesNameToMerge.Add(ruta1);
                }
               
                rp.ExportToDisk(ExportFormatType.PortableDocFormat, ruta);


                _mergeExPDF.FilesName = _filesNameToMerge;
                string Dif = Guid.NewGuid().ToString();
                string NewPath = Server.MapPath("files/" + Dif + ".pdf");
                _mergeExPDF.DestinationFile = NewPath;
                _mergeExPDF.Execute();
                ShowPdf1.FilePath = "files/" + Dif + ".pdf";
                
            }
            else if (Session["ConsultorioId"].ToString() == "5") // CARDIO
            {
                var ELECTROCARDIOGRAMA_ID = new ServiceBL().GetReportEstudioElectrocardiografico(ServiceId, Constants.ELECTROCARDIOGRAMA_ID);

                DataSet ds = new DataSet();
                DataTable dt = Sigesoft.Server.WebClientAdmin.UI.Utils.ConvertToDatatable(ELECTROCARDIOGRAMA_ID);
                dt.TableName = "dtEstudioElectrocardiografico";
                ds.Tables.Add(dt);

                rp.Load(Server.MapPath("crEstudioElectrocardiografico.rpt"));

                rp.SetDataSource(ds);

                rp.SetDataSource(ds);
                var ruta = Server.MapPath("files/CM" + ServiceId + ".pdf");


                _filesNameToMerge.Add(ruta);

                rp.ExportToDisk(ExportFormatType.PortableDocFormat, ruta);


                _mergeExPDF.FilesName = _filesNameToMerge;
                string Dif = Guid.NewGuid().ToString();
                string NewPath = Server.MapPath("files/" + Dif + ".pdf");
                _mergeExPDF.DestinationFile = NewPath;
                _mergeExPDF.Execute();
                ShowPdf1.FilePath = "files/" + Dif + ".pdf";

            }
            else if (Session["ConsultorioId"].ToString() == "15") // AUDIO
            {
                DataSet dsAudiometria = new DataSet();
                var dxList = new ServiceBL().GetDiagnosticRepositoryByComponent(ServiceId, Constants.AUDIOMETRIA_ID);
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
                    var dtDx = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dxList);
                    dtDx.TableName = "dtDiagnostic";
                    dsAudiometria.Tables.Add(dtDx);
                }


                var recom = dxList.SelectMany(s1 => s1.Recomendations).ToList();
                if (recom.Count == 0)
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
                    var dtReco = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(recom);
                    dtReco.TableName = "dtRecomendation";
                    dsAudiometria.Tables.Add(dtReco);
                }

                var audioUserControlList = new ServiceBL().ReportAudiometriaUserControl(ServiceId, Constants.AUDIOMETRIA_ID);
                var audioCabeceraList = new ServiceBL().ReportAudiometria(ServiceId, Constants.AUDIOMETRIA_ID);
                var dtAudiometriaUserControl = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(audioUserControlList);
                var dtCabecera = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(audioCabeceraList);


                dtCabecera.TableName = "dtAudiometria";
                dtAudiometriaUserControl.TableName = "dtAudiometriaUserControl";

                dsAudiometria.Tables.Add(dtCabecera);
                dsAudiometria.Tables.Add(dtAudiometriaUserControl);

                rp.Load(Server.MapPath("crFichaAudiometria.rpt"));

                rp.SetDataSource(dsAudiometria);


                var ruta = Server.MapPath("files/AU" + ServiceId + ".pdf");

                _filesNameToMerge.Add(ruta);

                rp.ExportToDisk(ExportFormatType.PortableDocFormat, ruta);




                //HISTORIA OCUPACIONAL AUDIOMETRÍA

                var dataListForReport_1 = new ServiceBL().ReportHistoriaOcupacionalAudiometria(ServiceId);

                DataSet ds = new DataSet();
                DataTable dt = Sigesoft.Server.WebClientAdmin.UI.Utils.ConvertToDatatable(dataListForReport_1);
                dt.TableName = "dtHistoriaOcupacional";
                ds.Tables.Add(dt);

                rp.Load(Server.MapPath("crHistoriaOcupacionalAudiometria.rpt"));

                rp.SetDataSource(ds);

                rp.SetDataSource(ds);
                var ruta1 = Server.MapPath("files/CM" + ServiceId + ".pdf");


                _filesNameToMerge.Add(ruta1);

                rp.ExportToDisk(ExportFormatType.PortableDocFormat, ruta1);


                _mergeExPDF.FilesName = _filesNameToMerge;
                string Dif = Guid.NewGuid().ToString();
                string NewPath = Server.MapPath("files/" + Dif + ".pdf");
                _mergeExPDF.DestinationFile = NewPath;
                _mergeExPDF.Execute();
                ShowPdf1.FilePath = "files/" + Dif + ".pdf";
            }

            else if (Session["ConsultorioId"].ToString() == "1") // LAB
            {
                var ruta = Server.MapPath("files/INFLAB" + ServiceId + ".pdf");

                GenerateLaboratorioReport(ruta, ServiceId);
                _filesNameToMerge.Add(ruta);

                _mergeExPDF.FilesName = _filesNameToMerge;
                string Dif = Guid.NewGuid().ToString();
                string NewPath = Server.MapPath("files/" + Dif + ".pdf");
                _mergeExPDF.DestinationFile = NewPath;
                _mergeExPDF.Execute();
                ShowPdf1.FilePath = "files/" + Dif + ".pdf";
            }
            else if (Session["ConsultorioId"].ToString() == "14") // OFTALMO
            {
                var OFTALMOLOGIA_ID = new PacientBL().GetOftalmologia(ServiceId, Constants.OFTALMOLOGIA_ID);

                DataSet ds = new DataSet();
                DataTable dt = Sigesoft.Server.WebClientAdmin.UI.Utils.ConvertToDatatable(OFTALMOLOGIA_ID);
                dt.TableName = "dtOftalmologia";
                ds.Tables.Add(dt);

                rp.Load(Server.MapPath("crOftalmologia.rpt"));

                rp.SetDataSource(ds);

                rp.SetDataSource(ds);
                var ruta = Server.MapPath("files/CM" + ServiceId + ".pdf");

                _filesNameToMerge.Add(ruta);

                rp.ExportToDisk(ExportFormatType.PortableDocFormat, ruta);

                _mergeExPDF.FilesName = _filesNameToMerge;
                string Dif = Guid.NewGuid().ToString();
                string NewPath = Server.MapPath("files/" + Dif + ".pdf");
                _mergeExPDF.DestinationFile = NewPath;
                _mergeExPDF.Execute();
                ShowPdf1.FilePath = "files/" + Dif + ".pdf";
            }


        }

        private void GenerateLaboratorioReport(string pathFile, string pstrServiceId)
        {
            var MedicalCenter = new ServiceBL().GetInfoMedicalCenter();
            var filiationData = new PacientBL().GetPacientReportEPS(pstrServiceId);
            var serviceComponents = new ServiceBL().GetServiceComponentsReport(pstrServiceId);

            LaboratorioReport.CreateLaboratorioReport(filiationData, serviceComponents, MedicalCenter, pathFile);
        }
    }
}