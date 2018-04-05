using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Sigesoft.Server.WebClientAdmin.UI.Consultorios
{
    [ScriptService]
    public partial class canvas1 : System.Web.UI.Page
    {
        //static string path = @"D:\imgOdonto\";

        static string path = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        [WebMethod]

        public static void UploadImage(string imageData, string xxx)
        {
            //string fileNameWitPath = path + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "- ").Replace(":", "") + ".png";
            path = System.Web.HttpContext.Current.Server.MapPath("~/Consultorios/imgOdonto/");
            string fileNameWitPath = path + xxx + ".jpg";
            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {

                using (BinaryWriter bw = new BinaryWriter(fs))
                {

                    byte[] data = Convert.FromBase64String(imageData);

                    bw.Write(data);

                    bw.Close();
                }

            }

        }
    }
}