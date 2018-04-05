using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class frmAtxResultadoExamenAuxiliar : Form
    {

        public string ComponentName { get; set; }
        public string FindingText { get; set; }

        public frmAtxResultadoExamenAuxiliar(string examen)
        {
            InitializeComponent();
            ComponentName = examen;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            FindingText = string.Format("{0}: {1}", ComponentName, txtHallazgos.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void frmAtxResultadoExamenAuxiliar_Load(object sender, EventArgs e)
        {
            txtHallazgos.Select();
        }
    }
}
