using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmAdjuntarDeclaracionJurada : Form
    {
        private string _filePath;
        private string _fileName;
        string ruta;
        string _serviceId;
        public frmAdjuntarDeclaracionJurada(string serviceId)
        {
            InitializeComponent();
            _serviceId = serviceId;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            FileInfoDto fileInfo = null;
            DirectoryInfo rutaOrigen = null;
            string ext = "";
            rutaOrigen = new DirectoryInfo(Common.Utils.GetApplicationConfigValue("DeclaracionJurada"));

            ext = Path.GetExtension(_filePath);
            File.Copy(_filePath, rutaOrigen + _serviceId + "-" + "DJ" + ext);
            MessageBox.Show("Se adjuntó correctamente", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ruta = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString();
                txtFileName.Text = Path.GetFileName(openFileDialog1.FileName);
                // Setear propiedades de usuario
                _fileName = Path.GetFileName(openFileDialog1.FileName);
                _filePath = openFileDialog1.FileName;
            }
        }

        private void frmAdjuntarDeclaracionJurada_Load(object sender, EventArgs e)
        {

        }
    }
}
