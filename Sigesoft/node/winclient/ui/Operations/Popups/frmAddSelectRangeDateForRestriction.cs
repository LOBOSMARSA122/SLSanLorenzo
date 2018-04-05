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
    public partial class frmAddSelectRangeDateForRestriction : Form
    {
        public DateTime? _startDate = null;
        public DateTime? _endDate = null;

        public frmAddSelectRangeDateForRestriction()
        {
            InitializeComponent();
        }

        private void rbFechaDuracion_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaIniRestricciones.Enabled = dtpFechaFinRestricciones.Enabled= true;
        }

        private void rbSinFecha_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaIniRestricciones.Enabled = dtpFechaFinRestricciones.Enabled = false;
        }

        private void dtpFechaIniRestricciones_Validating(object sender, CancelEventArgs e)
        {
            if (dtpFechaIniRestricciones.Value.Date > dtpFechaFinRestricciones.Value.Date)
            {
                e.Cancel = true;
                MessageBox.Show("El campo fecha Desde no puede ser Mayor a la fecha Hasta.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void dtpFechaFinRestricciones_Validating(object sender, CancelEventArgs e)
        {
            if (dtpFechaFinRestricciones.Value.Date < dtpFechaIniRestricciones.Value.Date)
            {
                e.Cancel = true;
                MessageBox.Show("El campo fecha Hasta no puede ser Menor a la fecha Desde.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (rbFechaDuracion.Checked)
            {
                _startDate = dtpFechaIniRestricciones.Value.Date;
                _endDate = dtpFechaFinRestricciones.Value.Date;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
