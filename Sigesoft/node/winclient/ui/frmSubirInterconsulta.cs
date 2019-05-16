using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmSubirInterconsulta : Form
    {
        string _ServiceId = "";
        string _Trabajador = "";
        public frmSubirInterconsulta(string pServiceId, string pTrabajador)
        {
            _ServiceId = pServiceId;
            _Trabajador = pTrabajador;
            InitializeComponent();
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var fileSize = Convert.ToInt32(Convert.ToSingle(Common.Utils.GetFileSizeInMegabytes(openFileDialog1.FileName)));

                if (fileSize > 7)
                {
                    MessageBox.Show("La imagen que está tratando de subir es damasiado grande.\nEl tamaño maximo es de 7 MB.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
               
                txtFileName.Text = Path.GetFileName(openFileDialog1.FileName);
    
            }
            else
            {
                return;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string ruta = Common.Utils.GetApplicationConfigValue("Interconsulta").ToString();
            string destino = ruta + _ServiceId + "-" + _Trabajador + ".pdf";
            if (File.Exists(destino))
            {
                System.IO.File.Delete(destino);
                File.Copy(openFileDialog1.FileName, destino);
            }
            else { File.Copy(openFileDialog1.FileName, destino); }
            MessageBox.Show("El archivo se anexó correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
