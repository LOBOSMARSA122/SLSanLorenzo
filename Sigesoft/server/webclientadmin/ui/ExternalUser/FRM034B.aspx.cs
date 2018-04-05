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
    
    public partial class FRM034B : System.Web.UI.Page
    {
        private List<string> _filesNameToMerge = new List<string>();
        private MergeExPDF _mergeExPDF = new MergeExPDF();
        protected void Page_Load(object sender, EventArgs e)
        {
            string rutaReportes = WebConfigurationManager.AppSettings["rutaReportes"];
            List<MyListWeb> ListaServicios = (List<MyListWeb>)Session["objLista"];
            var ListaExamenes = (List<string>)Session["objListaExamenes"];
            //Agregar Archivos
            foreach (var item in ListaServicios)
            {
                foreach (var itemExaId in ListaExamenes)
                {
                    _filesNameToMerge.Add(rutaReportes + item.IdServicio + "-" + itemExaId + ".pdf");
                }

            }

            var x = _filesNameToMerge.ToList();
            _mergeExPDF.FilesName = x;
            //_mergeExPDF.DestinationFile = Application.StartupPath + @"\TempMerge\" + _serviceId + ".pdf"; ;
            _mergeExPDF.DestinationFile = rutaReportes + "Borrar.pdf"; ;
            _mergeExPDF.Execute();



            LinkButton objLinkButton = new LinkButton();
            objLinkButton.ID = "Borrar";// ListaServicios[0].IdServicio;
            objLinkButton.Text = ListaServicios[0].Paciente;
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
            path = rutaReportes + senderCtrl.ID.ToString() + ".pdf";


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