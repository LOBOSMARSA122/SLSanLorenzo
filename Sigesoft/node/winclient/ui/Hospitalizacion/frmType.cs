using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmType : Form
    {
        public string _conCargoA { get; set; }
        public frmType()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            _conCargoA = cboPagador.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
