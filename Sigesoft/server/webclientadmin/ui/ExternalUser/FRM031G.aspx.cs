using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sigesoft.Server.WebClientAdmin.UI.ExternalUser
{
    public partial class FRM031G : System.Web.UI.Page
    {
        PacientBL objPacientBL = new PacientBL();
        List<FileInfoDto> ListamultimediaFile = new List<FileInfoDto>();
        protected void Page_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            if (!IsPostBack)
            {                
                if (Request.QueryString["PersonId"] != null)
                    Session["IdTrabajador"] = Request.QueryString["PersonId"].ToString();
            }

            ListamultimediaFile = objPacientBL.GetMultimediaFileByPersonId(ref objOperationResult, Session["IdTrabajador"].ToString()).FindAll(p => p.FileName != "IMAGEN AUDIOGRAMA OD" && p.FileName != "IMAGEN AUDIOGRAMA OI");

            foreach (var item in ListamultimediaFile)
            {
                LinkButton objLinkButton = new LinkButton();

                objLinkButton.ID = item.MultimediaFileId;
                objLinkButton.Text = item.FileName;
                
                objLinkButton.Click += new EventHandler(link_Click);
              
                DivControls.Controls.Add(objLinkButton);
                DivControls.Controls.Add(new LiteralControl("<br>"));
            }
        }

        private void link_Click(object sender, System.EventArgs e)
        {
             OperationResult objOperationResult = new OperationResult();
            LinkButton senderCtrl = (LinkButton)sender;
            string path;
            path = Server.MapPath("files/" + senderCtrl.Text);

            var multimediaFile = objPacientBL.GetMultimediaFileById(ref objOperationResult, senderCtrl.ID);

            File.WriteAllBytes(path, multimediaFile.ByteArrayFile);

            Download(senderCtrl.Text, "files/" + senderCtrl.Text);

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


        //protected void linkBtnEspirometria_Click(object sender, EventArgs e)
        //{
           
            

        //    //string path;
        //    //path = Server.MapPath("files/ADJEspiro" + Session["IdTrabajador"].ToString() + ".pdf");

        //    //File.WriteAllBytes(path, multimediaFile.ByteArrayFile);
        //    //Download(Session["IdTrabajador"].ToString() + ".pdf", path);
        //}

        //protected void linkBtnRayosX_Click(object sender, EventArgs e)
        //{

        //}

        //protected void LinkBtnAudiologia_Click(object sender, EventArgs e)
        //{

        //}
    }
}