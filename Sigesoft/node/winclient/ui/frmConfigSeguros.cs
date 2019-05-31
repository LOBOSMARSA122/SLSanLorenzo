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
        private string _factor;
        public decimal nuevoPrecio;

        public frmConfigSeguros(string factor)
        {
            InitializeComponent();
            
        }
        private void Calculator(bool coaseguro, bool deducible, string importe, string importeCo)
        {
            txtnuevoPrecio.Text = (double.Parse(txtPrecioBase.Text) * double.Parse(txtFactor.Text)).ToString();
            if (deducible == true)
            {
                txtPagoPaciente.Text = importe;
                txtPagoAseguradora.Text = (double.Parse(txtnuevoPrecio.Text) - double.Parse(txtPagoPaciente.Text)).ToString();
            }
            else if (coaseguro == true)
            {


                txtPagoPaciente.Text = (double.Parse(importeCo) * double.Parse(txtnuevoPrecio.Text) / 100).ToString();
                txtPagoAseguradora.Text = (double.Parse(txtnuevoPrecio.Text) - double.Parse(txtPagoPaciente.Text)).ToString();

            }
        }

        private void txtFactor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                Calculator(rbCoaseguro.Checked, rbDeducible.Checked, txtImporte.Text, txtCoaseguro.Text);
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
                CalcularNuevoPrecio(txtPrecioBase.Text, txtFactor.Text);
            }
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                Calculator(rbCoaseguro.Checked, rbDeducible.Checked, txtImporte.Text, txtCoaseguro.Text);
            }
        }

        private void frmConfigSeguros_Load_1(object sender, EventArgs e)
        {
            txtPrecioBase.Text = "0.00";
            txtFactor.Text = _factor;
            CalcularNuevoPrecio(txtPrecioBase.Text, txtFactor.Text);
        }

        private void CalcularNuevoPrecio(string p1, string p2)
        {
            nuevoPrecio = decimal.Parse(p1) * decimal.Parse(p2) * (decimal) 1.18;
            txtnuevoPrecio.Text = nuevoPrecio.ToString();
        }
    }
}
