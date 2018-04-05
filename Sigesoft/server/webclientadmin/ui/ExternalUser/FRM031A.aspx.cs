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

namespace Sigesoft.Server.WebClientAdmin.UI.ExternalUser
{
    public partial class FRM031A : System.Web.UI.Page
    {
        private string _tempSourcePath;
        private MergeExPDF _mergeExPDF = new MergeExPDF();

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }


        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            List<string> _filesNameToMerge = new List<string>();

                _tempSourcePath = Path.Combine(Server.MapPath("/TempMerge"));
                var w = (List<string>)Session["objLista"];


                foreach (var item in w)
                {

                    ReportDocument rp = new ReportDocument();

                    OperationResult objOperationResult = new OperationResult();

                    var aptitudeCertificate = new ServiceBL().GetAptitudeCertificate(ref objOperationResult, item.ToString());

                    DataSet ds = new DataSet();
                    DataTable dt = Sigesoft.Server.WebClientAdmin.UI.Utils.ConvertToDatatable(aptitudeCertificate);
                    dt.TableName = "AptitudeCertificate";
                    ds.Tables.Add(dt);
                    rp.Load(Server.MapPath("crOccupationalMedicalAptitudeCertificate.rpt"));
                    rp.SetDataSource(ds);

                    rp.SetDataSource(ds);
                    var ruta = Server.MapPath("files/CM" + item.ToString() + ".pdf");


                    _filesNameToMerge.Add(ruta);

                    rp.ExportToDisk(ExportFormatType.PortableDocFormat, ruta);
                }
                _mergeExPDF.FilesName = _filesNameToMerge;
                _mergeExPDF.DestinationFile =Server.MapPath("files/yyy.pdf");

                _mergeExPDF.Execute();
                _mergeExPDF.RunFile();
                //rp.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat,
                //Response, false, "PersonDetails");

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}