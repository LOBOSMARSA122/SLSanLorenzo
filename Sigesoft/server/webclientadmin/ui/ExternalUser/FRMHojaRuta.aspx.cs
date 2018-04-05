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
using FineUI;


namespace Sigesoft.Server.WebClientAdmin.UI.ExternalUser
{
    public partial class FRMHojaRuta : System.Web.UI.Page
    {
        CalendarBL _calendarBL = new CalendarBL();
        ServiceBL _serviceBL = new ServiceBL();
        private MergeExPDF _mergeExPDF = new MergeExPDF();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportDocument rp = new ReportDocument();
                List<string> _filesNameToMerge = new List<string>();
                List<MyListWeb> ListaServicios = (List<MyListWeb>)Session["objLista"];

                if (ListaServicios == null)
                {
                    Alert.Show("Seleccione un registro");
                    return;
                }
                else
                {
                   

                    foreach (var item in ListaServicios)
                    {
                        OperationResult objOperationResult = new OperationResult();
                        var headerRoadMap = _calendarBL.GetHeaderRoadMap(item.CalendarId);
                        var detailRoadMap = _serviceBL.GetServiceComponentsByCategoryExceptLab(ref objOperationResult, item.IdServicio);

                        DataSet ds = new DataSet();
                        DataTable dtHeader = Sigesoft.Server.WebClientAdmin.UI.Utils.ConvertToDatatable(headerRoadMap);
                        DataTable dtDetail = Sigesoft.Server.WebClientAdmin.UI.Utils.ConvertToDatatable(detailRoadMap);
                        dtHeader.TableName = "dtHeaderRoadMap";
                        dtDetail.TableName = "dtDetailRoadMap";

                        ds.Tables.Add(dtHeader);
                        ds.Tables.Add(dtDetail);
                        rp.Load(Server.MapPath("crRoadMapCompleta.rpt"));
                        rp.SetDataSource(ds);

                        var ruta = Server.MapPath("files/HojaRuta-" + item.IdServicio.ToString() + ".pdf");


                        _filesNameToMerge.Add(ruta);

                        rp.ExportToDisk(ExportFormatType.PortableDocFormat, ruta);

                    }

                    if (ListaServicios.Count !=0)
                    {
                        _mergeExPDF.FilesName = _filesNameToMerge;
                        string Dif = Guid.NewGuid().ToString();
                        string NewPath = Server.MapPath("files/" + Dif + ".pdf");
                        _mergeExPDF.DestinationFile = NewPath;
                        _mergeExPDF.Execute();
                        ShowPdf1.FilePath = "files/" + Dif + ".pdf";
                    }
                   
                }

               
            }
        }

       
    }
}