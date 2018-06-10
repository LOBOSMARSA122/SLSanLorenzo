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

namespace Sigesoft.Server.WebClientAdmin.UI.ExternalUser
{
    public partial class FRM031I : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["Dni"] != null)
                    Session["Dni"] = Request.QueryString["Dni"].ToString();
                if (Request.QueryString["Apellidos"] != null)
                    Session["Apellidos"] = Request.QueryString["Apellidos"].ToString();

               
            }

            if (!(bool)Session["ArchivosAdjuntos"])
            {
                //FineUI.PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference()); 
                Label lbl1 = new Label();
                lbl1.Text = "No tiene permiso para descargar Adjuntos";
                lbl1.ID = "lb1";
                DivControls.Controls.Add(lbl1);
            }
            else
            {
                FileInfo[] files = null;
                if (Session["CategoriaId"] == null || Session["CategoriaId"].ToString() == "-1")
                {
                    DirectoryInfo rutaOrigen = new DirectoryInfo(WebConfigurationManager.AppSettings["ImgUSUEXTE"]);
                    files = rutaOrigen.GetFiles();
                }
                else if (Session["CategoriaId"].ToString() == "6")//RX
                {
                    DirectoryInfo rutaOrigen = new DirectoryInfo(WebConfigurationManager.AppSettings["ImgRxOrigen"]);
                    files = rutaOrigen.GetFiles();
                }
                else if (Session["CategoriaId"].ToString() == "16")//ESPIRO
                {
                    DirectoryInfo rutaOrigen = new DirectoryInfo(WebConfigurationManager.AppSettings["ImgESPIROOrigen"]);
                    files = rutaOrigen.GetFiles();
                }
                else if (Session["CategoriaId"].ToString() == "5")//EKG
                {
                    DirectoryInfo rutaOrigen = new DirectoryInfo(WebConfigurationManager.AppSettings["ImgEKGOrigen"]);
                    files = rutaOrigen.GetFiles();
                }
                else if (Session["CategoriaId"].ToString() == "1")//LAB
                {
                    DirectoryInfo rutaOrigen = new DirectoryInfo(WebConfigurationManager.AppSettings["ImgLABOrigen"]);
                    files = rutaOrigen.GetFiles();
                }
                else
                {
                    DirectoryInfo rutaOrigen = new DirectoryInfo(WebConfigurationManager.AppSettings["ImgUSUEXTE"]);
                    files = rutaOrigen.GetFiles();
                }

                foreach (var item in files)
                {
                    if (item.ToString().Substring(0, 8) == Session["Dni"].ToString())
                    {
                        LinkButton objLinkButton = new LinkButton();

                        objLinkButton.ID = item.Name;
                        objLinkButton.Text = item.Name;

                        objLinkButton.Click += new EventHandler(link_Click);

                        DivControls.Controls.Add(objLinkButton);
                        DivControls.Controls.Add(new LiteralControl("<br>"));
                    }
                    if (item.ToString().ToUpper() == Session["Apellidos"].ToString() + "-3.PDF")
                    {
                        LinkButton objLinkButton = new LinkButton();

                        objLinkButton.ID = item.Name;
                        objLinkButton.Text = item.Name;

                        objLinkButton.Click += new EventHandler(link_Click);

                        DivControls.Controls.Add(objLinkButton);
                        DivControls.Controls.Add(new LiteralControl("<br>"));
                    }
                    if (item.ToString().ToUpper() == Session["Apellidos"].ToString() + "-1.PDF")
                    {
                        LinkButton objLinkButton = new LinkButton();

                        objLinkButton.ID = item.Name;
                        objLinkButton.Text = item.Name;

                        objLinkButton.Click += new EventHandler(link_Click);

                        DivControls.Controls.Add(objLinkButton);
                        DivControls.Controls.Add(new LiteralControl("<br>"));
                    }
                    if (item.ToString().ToUpper() == Session["Apellidos"].ToString() + "-2.PDF")
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
            
        }

        private void link_Click(object sender, System.EventArgs e)
        {
            string rutaReportes = "";

            if (Session["CategoriaId"] == null)
            {
                rutaReportes = WebConfigurationManager.AppSettings["ImgUSUEXTE"];
            }
            else if (Session["CategoriaId"].ToString() == "6")//RX
            {
                rutaReportes = WebConfigurationManager.AppSettings["ImgRxOrigen"];
            }
            else if (Session["CategoriaId"].ToString() == "16")//ESPIRO
            {
                rutaReportes = WebConfigurationManager.AppSettings["ImgESPIROOrigen"];
            }
            else if (Session["CategoriaId"].ToString() == "5")//EKG
            {
                rutaReportes = WebConfigurationManager.AppSettings["ImgEKGOrigen"];
            }
            else if (Session["CategoriaId"].ToString() == "1")//LAB
            {
                rutaReportes = WebConfigurationManager.AppSettings["ImgLABOrigen"];
            }
                   

            OperationResult objOperationResult = new OperationResult();
            LinkButton senderCtrl = (LinkButton)sender;
            string path;
            path = rutaReportes + senderCtrl.ID.ToString() ;


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