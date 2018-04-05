using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.IO;

namespace Sigesoft.Server.WebClientAdmin.UI
{
    /// <summary>
    /// Summary description for GetImageText
    /// </summary>
    public class GetImageText : IHttpHandler
    {
        Int32 _imagenAncho = 50;
        Int32 _imagenAlto = 50;
        //bool _conMarcaDeAgua = true;
        string _texto = "Empty";
        string _tipo = "";
        string _pkImagen = "";
        string _fuenteFamily = "Verdana";
        Single _fuenteSize = 2;
        string _fuenteColor = "Black";
        int _fuentePosX = 0;
        int _fuentePosY = 0;
        private byte[] _byteArraySource = null;

        public void ProcessRequest(HttpContext context)
        {
            getParameters();
            GetImage();
        }

        public bool ThumbnailCallback()
        {
            return false;
        }

        void GetImage()
        {
            byte[] byteArray = null;

            if (_tipo == "tipo1")
            {
                byteArray = null;
            }
            if (_tipo == "tipo2")
            {
                byteArray = null;
            }
            else if (_tipo == "tipo3")
            {
                byteArray = null;
            }

            string filename = HttpContext.Current.Server.MapPath("~/Temp/") + "UploadedImage.jpg";               

            //var imgtmp = System.Drawing.Image.FromFile(filename);
            var imgtmp = LoadImageNoLock(filename);
            byteArray = ImageToBytes(imgtmp);         
           
            //byteArray = _byteArraySource;

            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));

            //Creamos una imagen basada en el archivo original               
            Bitmap imagen = (Bitmap)tc.ConvertFrom(byteArray);
            Graphics imagenGrap;        

            //'Para miniatura
            System.Drawing.Image miniatura;

            //'Resolucion
            imagen.SetResolution(75, 75);

            //'Dimensiones
            if (_imagenAncho < 0)
            {
                _imagenAncho = imagen.Width;
            }

            //'Formula de 3 simple ;)
            _imagenAlto = imagen.Height * _imagenAncho / imagen.Width;

            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            miniatura = imagen.GetThumbnailImage(_imagenAncho, _imagenAlto, myCallback, IntPtr.Zero);

            if (!string.IsNullOrEmpty(_texto))
            {
                if (_texto.Trim().Length > 0)
                {
                    Font marcaAguaFuente = GetFont();
                    imagenGrap = Graphics.FromImage(miniatura);
                    imagenGrap.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    //Con Sombra muy basica
                    //imagenGrap.DrawString(_texto, marcaAguaFuente, ObtenerColor("Black"), _fuentePosX + 2, _fuentePosY + 2)
                    imagenGrap.DrawString(_texto, marcaAguaFuente, GetColor(_fuenteColor), _fuentePosX, _fuentePosY);
                }
            }

            HttpContext.Current.Response.ContentType = "image/Jpeg";
            miniatura.Save(HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

        }

        private Font GetFont()
        {
            Font f = new Font(_fuenteFamily, _fuenteSize, FontStyle.Regular);
            return f;
        }
      
        private Brush GetColor(string ColorName)
        {
            Brush b = new SolidBrush(Color.FromName(ColorName));
            return b;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public byte[] Imagen
        {
            set 
            {
                _byteArraySource = value;
            }
        }

        void getParameters()
        {

            byte[] byteArray = _byteArraySource;

            //Ancho
            if (HttpContext.Current.Request.QueryString["type"] != "")
            {
                _tipo = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["type"]);
            }

            //Ancho
            if (HttpContext.Current.Request.QueryString["ancho"] != "")
            {
                _imagenAncho = Convert.ToInt32(HttpContext.Current.Request.QueryString["ancho"]);
            }

            //Nombre de la Imagen
            if (HttpContext.Current.Request.QueryString["pkImagen"] != "")
            {
                _pkImagen = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["pkImagen"]);
            }

            //Texto/Mensaje
            if (HttpContext.Current.Request.QueryString["t"] != "")
            {
                _texto = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["t"]);
            }

            //Fuente: Family
            if (HttpContext.Current.Request.QueryString["ff"] != "")
            {
                _fuenteFamily = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["ff"]);
            }
            //Fuente: Size
            if (HttpContext.Current.Request.QueryString["fs"] != "")
            {
                _fuenteSize = Convert.ToSingle(HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["fs"]));
            }

            //Fuente: Color
            if (HttpContext.Current.Request.QueryString["fc"] != "")
            {
                _fuenteColor = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["fc"]);
            }

            //Fuente: Posicion X
            if (HttpContext.Current.Request.QueryString["fx"] != "")
            {
                _fuentePosX = Convert.ToInt32(HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["fx"]));
            }
            //Fuente: Posicion Y
            if (HttpContext.Current.Request.QueryString["fy"] != "")
            {
                _fuentePosY = Convert.ToInt32(HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["fy"]));
            }
        }

        public byte[] ImageToBytes(System.Drawing.Image img)
        {
            ImageConverter converter = new ImageConverter();

            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
       
        public static Image LoadImageNoLock(string path)
        {
            var ms = new MemoryStream(File.ReadAllBytes(path)); // Don't use using!!
            return Image.FromStream(ms);
        }

    }
}