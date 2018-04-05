using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmFecha : Form
    {
        public DateTime Fecha;
        public frmFecha()
        {
            InitializeComponent();
        }

        private void frmFecha_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Fecha = dtpDateTimeStar.Value;
            this.Close();
        }
    }
}
