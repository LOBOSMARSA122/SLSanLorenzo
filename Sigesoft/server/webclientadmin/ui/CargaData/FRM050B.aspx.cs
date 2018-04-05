using Sigesoft.Common;
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


namespace Sigesoft.Server.WebClientAdmin.UI.CargaData
{
    public partial class FRM050B : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
                LinkButton objLinkButton = new LinkButton();

                objLinkButton.ID = "btnDescargar";
                objLinkButton.Text = "Descargar.xlsx";

                objLinkButton.Click += new EventHandler(link_Click);

                DivControls.Controls.Add(objLinkButton);
                DivControls.Controls.Add(new LiteralControl("<br>"));
 
        }

        private void link_Click(object sender, System.EventArgs e)
        {

            string rutaReportes = WebConfigurationManager.AppSettings["Plantilla"];
          
            OperationResult objOperationResult = new OperationResult();
            LinkButton senderCtrl = (LinkButton)sender;
            string path;
            path = rutaReportes + "Plantilla.xlsx";


            Download(senderCtrl.Text, path);

        }


        public static void Download(string sFileName, string sFilePath)
        {
            HttpContext.Current.Response.ContentType = "APPLICATION/OCTET-STREAM";
            String Header = "Attachment; Filename=" + sFileName;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
            System.IO.FileInfo Dfile = new System.IO.FileInfo(sFilePath);
            HttpContext.Current.Response.WriteFile(Dfile.FullName);
            HttpContext.Current.Response.End();
        }      

    }
}