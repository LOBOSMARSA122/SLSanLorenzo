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


namespace Sigesoft.Server.WebClientAdmin.UI.ExternalUser
{
    public partial class FRM031X : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            List<MyListWeb> ListaServicios = (List<MyListWeb>)Session["objLista"];

            LinkButton objLinkButton = new LinkButton();
            objLinkButton.ID = ListaServicios[0].IdServicio;
            objLinkButton.Text = "Click para Descargar";//ListaServicios[0].Paciente;
            objLinkButton.Click += new EventHandler(link_Click);

            DivControls.Controls.Add(objLinkButton);
            DivControls.Controls.Add(new LiteralControl("<br>"));

        }

        private void link_Click(object sender, System.EventArgs e)
        {
            string rutaReportes = WebConfigurationManager.AppSettings["rutaReportes"];

            OperationResult objOperationResult = new OperationResult();
            LinkButton senderCtrl = (LinkButton)sender;
            string path;
            List<MyListWeb> ListaServicios = (List<MyListWeb>)Session["objLista"];
            List<string> _filesNameToMerge = new List<string>();
            MergeExPDF _mergeExPDF = new MergeExPDF();
            foreach (var item in ListaServicios)
            {
                _filesNameToMerge.Add(rutaReportes + item.IdServicio.ToString() + "-CAPE.pdf");
            }
            var x = _filesNameToMerge.ToList();
            _mergeExPDF.FilesName = x;
            _mergeExPDF.DestinationFile = rutaReportes + "Descargar Certificado" + ".pdf";// Server.MapPath(@"\TempMerge\" + "Descargar" + ".pdf");
            _mergeExPDF.DestinationFile = rutaReportes + "Descargar Certificado" + ".pdf";
            _mergeExPDF.Execute();
            path = rutaReportes + "Descargar Certificado" + ".pdf";
            Download(senderCtrl.Text, path);

        }

        public static void Download(string sFileName, string sFilePath)
        {
            HttpContext.Current.Response.ContentType = "APPLICATION/OCTET-STREAM";
            String Header = "Attachment; Filename=" + sFileName + ".pdf";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
            System.IO.FileInfo Dfile = new System.IO.FileInfo(sFilePath);
            HttpContext.Current.Response.WriteFile(Dfile.FullName);
            HttpContext.Current.Response.End();
        }      
    }
}