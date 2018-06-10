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
    public partial class FRM031K : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            List<MyListWeb> ListaServicios = (List<MyListWeb>)Session["objLista"];

            LinkButton objLinkButton = new LinkButton();
            objLinkButton.ID = ListaServicios[0].IdServicio;
            objLinkButton.Text = ListaServicios[0].Paciente + "-N009-ME000000015";
            //objLinkButton.Text = Session["IdServicio"].ToString() + ".pdf";
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

            path = rutaReportes + senderCtrl.ID.ToString() + "-312.pdf";

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