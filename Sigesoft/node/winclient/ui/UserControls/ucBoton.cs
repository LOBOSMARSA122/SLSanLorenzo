using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Sigesoft.Node.WinClient.UI.UserControls
{
    public partial class ucBoton : UserControl
    {
        public ucBoton()
        {
            InitializeComponent();
        }
        public string Dni { get; set; }
        public string Examen { get; set; }
        public DateTime FechaServicio { get; set; }

        private void btnBoton_Click(object sender, EventArgs e)
        {
            DirectoryInfo rutaOrigen =null;
            string rutaDestino = null;
            string Fecha = FechaServicio.Day.ToString().PadLeft(2, '0') + FechaServicio.Month.ToString().PadLeft(2, '0') + FechaServicio.Year.ToString();

            if (Examen == "RAYOS X")
            {
                rutaOrigen= new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgRxOrigen").ToString());
                rutaDestino = Common.Utils.GetApplicationConfigValue("ImgRxDestino").ToString() + Dni + "-"+ Fecha;


                FileInfo[] files = rutaOrigen.GetFiles();
                int Contador = 1;
                foreach (FileInfo file in files)
                {
                    if (file.ToString().Count() > 16)
                    {
                        if (file.ToString().Substring(0, 17) == Dni + "-" + Fecha)
                        {
                            file.CopyTo(rutaDestino + "-" + Contador + ".jpg");
                            Contador++;
                        };
                    }
                }
            }
            else if (Examen == "ELECTROCARDIOGRAMA")
            {
                rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgEKGOrigen").ToString());
                rutaDestino = Common.Utils.GetApplicationConfigValue("ImgEKGDestino").ToString() + Dni + "-" + Fecha;


                FileInfo[] files = rutaOrigen.GetFiles();
                int Contador = 1;
                foreach (FileInfo file in files)
                {
                    if (file.ToString().Count() > 16)
                    {
                        if (file.ToString().Substring(0, 17) == Dni + "-" + Fecha)
                        {
                            file.CopyTo(rutaDestino + "-" + Contador + ".pdf");
                            Contador++;
                        };
                    }
                }
            }
            else if (Examen == "ESPIROMETRÍA")
            {
                rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgESPIROOrigen").ToString());
                rutaDestino = Common.Utils.GetApplicationConfigValue("ImgESPIDestino").ToString() + Dni + "-" + Fecha;


                FileInfo[] files = rutaOrigen.GetFiles();
                int Contador = 1;
                foreach (FileInfo file in files)
                {
                    if (file.ToString().Count() > 16)
                    {
                        if (file.ToString().Substring(0, 17) == Dni + "-" + Fecha)
                        {
                            file.CopyTo(rutaDestino + "-" + Contador + ".pdf");
                            Contador++;
                        };
                    }
                }
            }

            else if (Examen == "LABORATORIO")
            {
                rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("ImgLABOrigen").ToString());
                rutaDestino = Common.Utils.GetApplicationConfigValue("ImgLABDestino").ToString() + Dni + "-" + Fecha;


                FileInfo[] files = rutaOrigen.GetFiles();
                int Contador = 1;
                foreach (FileInfo file in files)
                {
                    if (file.ToString().Count() > 16)
                    {
                        if (file.ToString().Substring(0, 17) == Dni + "-" + Fecha)
                        {
                            file.CopyTo(rutaDestino + "-" + Contador + ".pdf");
                            Contador++;
                        };
                    }
                }
            }
           


            MessageBox.Show("Los archivos se copiaron correctamente en la siguiente ruta: " + Common.Utils.GetApplicationConfigValue("ImgRxDestino").ToString());


        }


    }
}
