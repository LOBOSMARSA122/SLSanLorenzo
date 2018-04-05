using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Sigesoft.Server.WebClientAdmin.UI.ExternalUser
{
    public partial class FRM032A : System.Web.UI.Page
    {
        PacientBL objPacienteBL = new PacientBL();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                try
                {
                    string PersonId = null;

                    if (Request.QueryString["PersonId"] != null)
                        PersonId = Request.QueryString["PersonId"].ToString();

                    ImgPhoto.ImageUrl = null;

                    var ArrayImage = objPacienteBL.getPhoto(PersonId);
                    if (ArrayImage != null)
                    {
                        string pathImage = byteArrayToImage(ArrayImage);
                        string str = @"~\Utils\GetImageText.ashx?" + getParameterRequest("imgDeliverValid", "300", "", "Arial Black", "Black", "9", "30", "20", "");
                        ImgPhoto.ImageUrl = str;
                    }
                    else
                    {
                        string pathImage = byteArrayToImage(Sigesoft.Common.Utils.FileToByteArray(Server.MapPath(@"~\images\icons\nofoto.jpg")));
                        string str = @"~\Utils\GetImageText.ashx?" + getParameterRequest("imgDeliverValid", "220", "", "Arial Black", "Black", "19", "30", "20", "");
                        ImgPhoto.ImageUrl = str;
                    }
                   
                }
                catch (Exception)
                {
                    
                    throw;
                }
               
            }
           
        }

        private string byteArrayToImage(byte[] byteArrayIn)
        {
            System.Drawing.Image newImage = null;
            Decimal Hv, Wv, k, Hi, Wi, Dh, Dw;
            string _pathUrl = string.Empty;

            try
            {
                var filePaths = Directory.GetFiles(Server.MapPath("~/Temp"));
                foreach (string filePath in filePaths)
                {
                    File.Delete(filePath);
                }

                string strFileName = Server.MapPath("~/Temp/") + "UploadedImage.jpg";

                if (byteArrayIn != null)
                {
                    using (MemoryStream stream = new MemoryStream(byteArrayIn))
                    {
                        newImage = System.Drawing.Image.FromStream(stream);
                        Hv = 150;
                        Wv = 180;

                        k = -1;

                        Hi = newImage.Height;
                        Wi = newImage.Width;

                        Dh = -1;
                        Dw = -1;

                        Dh = Hi - Hv;
                        Dw = Wi - Wv;

                        if (Dh > Dw)
                        {
                            k = Hv / Hi;
                        }
                        else
                        {
                            k = Wv / Wi;
                        }

                        newImage.Save(strFileName);

                    }

                    //ImgPhoto.ImageHeight = (int)(k * Hi);
                    //ImgPhoto.ImageWidth = (int)(k * Wi);

                    _pathUrl = ResolveUrl("~/Temp/" + "UploadedImage.jpg");

                }
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }

            return _pathUrl;

        }

        private string getParameterRequest(string pstrType, string pstrWidht, string pstrMessage, string pstrFontFamily, string pstrFontColor, string pstrFontSize, string pstrPosX, string pstrPosY, string pstrPkImagen)
        {
            var query = new StringBuilder();
            if (pstrType.Length > 0)
            {
                query.Append("type=");
                query.Append(HttpUtility.UrlEncode(pstrType));
                query.Append("&");
            }

            if (pstrPkImagen.Length > 0)
            {
                query.Append("pkImagen=");
                query.Append(HttpUtility.UrlEncode(pstrPkImagen));
                query.Append("&");
            }
            //'Ancho
            if (pstrWidht.Length > 0)
            {
                query.Append("ancho=");
                query.Append(pstrWidht);
                query.Append("&");
            }
            //'Texto/Mensaje
            if (pstrMessage.Length > 0)
            {
                query.Append("t=");
                query.Append(HttpUtility.UrlEncode(pstrMessage));
                query.Append("&");
            }
            //'Fuente/Tipografia
            if (pstrFontFamily.Length > 0)
            {
                query.Append("ff=");
                query.Append(HttpUtility.UrlEncode(pstrFontFamily));
                query.Append("&");
            }
            if (pstrFontColor.Length > 0)
            {
                query.Append("fc=");
                query.Append(HttpUtility.UrlEncode(pstrFontColor));
                query.Append("&");
            }
            if (pstrFontSize.Length > 0)
            {
                query.Append("fs=");
                query.Append(HttpUtility.UrlEncode(pstrFontSize));
                query.Append("&");
            }
            if (pstrPosX.Length > 0)
            {
                query.Append("fx=");
                query.Append(HttpUtility.UrlEncode(pstrPosX));
                query.Append("&");
            }
            if (pstrPosY.Length > 0)
            {
                query.Append("fy=");
                query.Append(HttpUtility.UrlEncode(pstrPosY));
                query.Append("&");
            }

            return query.ToString();
        }      


    }
}