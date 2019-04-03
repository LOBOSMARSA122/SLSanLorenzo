using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmGenerarImprimirLiquidacion : Form
    {
        private string _nroliq;
        public bool result = false;
        public frmGenerarImprimirLiquidacion(string nroliq)
        {
            _nroliq = nroliq;
            InitializeComponent();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)

        {
            string ruta = Common.Utils.GetApplicationConfigValue("LiquidacionAseguradora").ToString();
            string pdfPath = Path.Combine(Application.StartupPath, ruta + _nroliq + ".pdf");
            Process.Start(pdfPath);
            result = true;
            this.Close();
        }
    }
}
