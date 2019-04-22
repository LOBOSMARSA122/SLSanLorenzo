using NetPdf;
using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Sigesoft.Server.WebClientAdmin.UI.Clientes
{
    public partial class FRM080 : System.Web.UI.Page
    {
        ServiceBL oServiceBL = new ServiceBL();
        List<ServiceList> ListaServiceList = new List<ServiceList>();
        protected void Page_Load(object sender, EventArgs e)
        {

            //Obtener Los servicios 
            ListaServiceList = oServiceBL.ObtenerServiciosPorPersonId(Session["IdPersona"].ToString());

          foreach (var item in ListaServiceList)
          {
              LinkButton objLinkButton = new LinkButton();

              objLinkButton.ID = item.v_ServiceId;
              Session["ServiceId"] = item.v_ServiceId;
              objLinkButton.Text = item.d_ServiceDate.ToString();

              objLinkButton.Click += new EventHandler(link_Click);

              DivControls.Controls.Add(objLinkButton);
              DivControls.Controls.Add(new LiteralControl("<br>"));
          }
        }

        private void link_Click(object sender, System.EventArgs e)
        {
            string rutaReportes = WebConfigurationManager.AppSettings["rutaReportes"];

            OperationResult objOperationResult = new OperationResult();
            LinkButton senderCtrl = (LinkButton)sender;
            string path;
            List<string> _filesNameToMerge = new List<string>();
            MergeExPDF _mergeExPDF = new MergeExPDF();
    
                _filesNameToMerge.Add(rutaReportes + Session["ServiceId"].ToString() + "-FMT.pdf");
                _filesNameToMerge.Add(rutaReportes + Session["ServiceId"].ToString() + "-312.pdf");
                _filesNameToMerge.Add(rutaReportes + Session["ServiceId"].ToString() + "-7C.pdf");
        
            var x = _filesNameToMerge.ToList();
            _mergeExPDF.FilesName = x;
            _mergeExPDF.DestinationFile = rutaReportes + "Descargar Historia" + ".pdf";// Server.MapPath(@"\TempMerge\" + "Descargar" + ".pdf");
            _mergeExPDF.DestinationFile = rutaReportes + "Descargar Historia" + ".pdf";
            _mergeExPDF.Execute();
            path = rutaReportes + "Descargar Historia" + ".pdf";
            Download(senderCtrl.Text, path);
        }

        public static void Download(string sFileName, string sFilePath)
        {
            HttpContext.Current.Response.ContentType = "APPLICATION/OCTET-STREAM";
            String Header = "Attachment; Filename=" + sFileName;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
            System.IO.FileInfo Dfile = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(sFilePath));
            HttpContext.Current.Response.WriteFile(Dfile.FullName);
            HttpContext.Current.Response.End();
        }      
    }
}