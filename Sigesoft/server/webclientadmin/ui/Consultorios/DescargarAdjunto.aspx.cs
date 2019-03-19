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

namespace Sigesoft.Server.WebClientAdmin.UI.Consultorios
{
    public partial class DescargarAdjunto : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["DniTrabajador"] != null)
                    Session["DniTrabajador"] = Request.QueryString["DniTrabajador"].ToString();

                if (Request.QueryString["Consultorio"] != null)
                    Session["Consultorio"] = Request.QueryString["Consultorio"].ToString();
            }

            FileInfo[] files = null;
           
            if (Session["Consultorio"].ToString() == "RX")//RX
            {
                DirectoryInfo rutaOrigen = new DirectoryInfo(WebConfigurationManager.AppSettings["ImgRxOrigen"]);
                files = rutaOrigen.GetFiles();
            }
            if (Session["Consultorio"].ToString() == "OIT")//RX
            {
                DirectoryInfo rutaOrigen = new DirectoryInfo(WebConfigurationManager.AppSettings["ImgOITOrigen"]);
                files = rutaOrigen.GetFiles();
            }
            else if (Session["Consultorio"].ToString() == "ESPIRO")//ESPIRO
            {
                DirectoryInfo rutaOrigen = new DirectoryInfo(WebConfigurationManager.AppSettings["ImgESPIROOrigen"]);
                files = rutaOrigen.GetFiles();
            }
            else if (Session["Consultorio"].ToString() == "EKG")//EKG
            {
                DirectoryInfo rutaOrigen = new DirectoryInfo(WebConfigurationManager.AppSettings["ImgEKGOrigen"]);
                files = rutaOrigen.GetFiles();
            }
            else if (Session["Consultorio"].ToString() == "LAB")//LAB
            {
                DirectoryInfo rutaOrigen = new DirectoryInfo(WebConfigurationManager.AppSettings["ImgLABOrigen"]);
                files = rutaOrigen.GetFiles();
            }
            else if (Session["Consultorio"].ToString() == "312")//Medicina
            {
                DirectoryInfo rutaOrigen = new DirectoryInfo(WebConfigurationManager.AppSettings["Ruta312"]);
                files = rutaOrigen.GetFiles();
            }
           
            var fechaServicio = DateTime.Parse(Session["d_ServiceDate"].ToString());
            var dia = string.Format("{0}", fechaServicio.Day.ToString("00"));
            var mes = string.Format("{0}", fechaServicio.Month.ToString("00"));
            var anio = string.Format("{0}", fechaServicio.Year.ToString("00"));
            var dni = Session["DniTrabajador"].ToString();
            var largeAdditional = 0;           
            if (dni.Length > 8)
            {
                largeAdditional = dni.Length - 8;
            }
            var largeSubstring = 17 + largeAdditional;
            foreach (var item in files)
            {
                if (item.ToString().Substring(0, largeSubstring) == Session["DniTrabajador"].ToString() + "-" + dia + mes + anio)
                {
                    LinkButton objLinkButton = new LinkButton();

                    objLinkButton.ID = item.Name;
                    objLinkButton.Text = item.Name;

                    objLinkButton.Click += new EventHandler(link_Click);

                    DivControls.Controls.Add(objLinkButton);
                    DivControls.Controls.Add(new LiteralControl("<br>"));
                }
            }
        }

        private void link_Click(object sender, System.EventArgs e)
        {
            string rutaReportes = "";

            if (Session["Consultorio"] == null)
            {
                rutaReportes = WebConfigurationManager.AppSettings["ImgUSUEXTE"];
            }
            else if (Session["Consultorio"].ToString() == "RX")//RX
            {
                rutaReportes = WebConfigurationManager.AppSettings["ImgRxOrigen"];
            }
            else if (Session["Consultorio"].ToString() == "OIT")//OIT
            {
                rutaReportes = WebConfigurationManager.AppSettings["ImgOITOrigen"];
            }
            else if (Session["Consultorio"].ToString() == "ESPIRO")//ESPIRO
            {
                rutaReportes = WebConfigurationManager.AppSettings["ImgESPIROOrigen"];
            }
            else if (Session["Consultorio"].ToString() == "EKG")//EKG
            {
                rutaReportes = WebConfigurationManager.AppSettings["ImgEKGOrigen"];
            }
            else if (Session["Consultorio"].ToString() == "LAB")//LAB
            {
                rutaReportes = WebConfigurationManager.AppSettings["ImgLABOrigen"];
            }
            else if (Session["Consultorio"].ToString() == "312")//LAB
            {
                rutaReportes = WebConfigurationManager.AppSettings["Ruta312"];
            }

            OperationResult objOperationResult = new OperationResult();
            LinkButton senderCtrl = (LinkButton)sender;
            string path;
            path = rutaReportes + senderCtrl.ID.ToString();

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