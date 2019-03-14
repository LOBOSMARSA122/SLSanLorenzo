using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinEditors.UltraWinCalc;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmConfigSeguros : Form
    {
        public decimal? paciente;
        public decimal? aseguradora;
        public float? precio;
        public bool result;

        public frmConfigSeguros(int deducible, int coaseguro, decimal importe, string precio, string factor)
        {
            InitializeComponent();
            if (deducible == 1)
            {
                rbDeducible.Checked = true;
                rbCoaseguro.Checked = false;
            }
            else if (coaseguro == 1)
            {
                rbDeducible.Checked = false;
                rbCoaseguro.Checked = true;

            }



            txtFactor.Text = factor;
            txtPrecioBase.Text = precio;
            txtImporte.Text = importe.ToString();
            Calculator(rbCoaseguro.Checked, rbDeducible.Checked, txtImporte.Text);
        }
        private void Calculator(bool coaseguro, bool deducible, string importe)
        {
            txtnuevoPrecio.Text = (double.Parse(txtPrecioBase.Text) * double.Parse(txtFactor.Text)).ToString();
            if (deducible == true)
            {
                txtPagoPaciente.Text = importe;
                txtPagoAseguradora.Text = (double.Parse(txtnuevoPrecio.Text) - double.Parse(txtPagoPaciente.Text)).ToString();
            }
            else if (coaseguro == true)
            {

                txtPagoAseguradora.Text = (double.Parse(importe) * double.Parse(txtnuevoPrecio.Text) / 100).ToString();
                txtPagoPaciente.Text = (double.Parse(txtnuevoPrecio.Text) - double.Parse(txtPagoAseguradora.Text)).ToString();

            }
        }

        private void frmConfigSeguros_Load(object sender, EventArgs e)
        {

        }

        private void txtFactor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                Calculator(rbCoaseguro.Checked, rbDeducible.Checked, txtImporte.Text);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            paciente = decimal.Parse(txtPagoPaciente.Text);
            aseguradora = decimal.Parse(txtPagoAseguradora.Text);
            precio = float.Parse(txtnuevoPrecio.Text);
            result = true;
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            result = false;
            this.Close();

        }

        private void frmConfigSeguros_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (result != true)
            {
                result = false;
            }

        }

        private void txtPrecioBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                Calculator(rbCoaseguro.Checked, rbDeducible.Checked, txtImporte.Text);
            }
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                Calculator(rbCoaseguro.Checked, rbDeducible.Checked, txtImporte.Text);
            }
        }
    }
}
