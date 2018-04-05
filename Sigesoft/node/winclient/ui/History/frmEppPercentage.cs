using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.History
{
    public partial class frmEppPercentage : Form
    {
        public int? _Porcentage = 0;
        public frmEppPercentage(string EppName, double porcentage)
        {
            InitializeComponent();

            this.Text = this.Text + EppName;
            if (porcentage == null)
            {
                txtPorcentage.Text = "";
            }
            else if (porcentage != 0)
            {
                txtPorcentage.Text = porcentage.ToString();
            }
        }

        //private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (Char.IsDigit(e.KeyChar))
        //    {
        //        e.Handled = false;
        //    }
        //    else
        //        if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
        //        {
        //            e.Handled = false;
        //        }
        //        else
        //        {
        //            //el resto de teclas pulsadas se desactivan
        //            e.Handled = true;
        //        }      
        //}

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void frmEppPercentage_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPorcentage.Text == "")
            {
                _Porcentage = null;
            }
            else
            {
                if (int.Parse(txtPorcentage.Text.ToString()) > 100)
                {
                    MessageBox.Show("Este campo no puede ser mayor a 100", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    _Porcentage = int.Parse(txtPorcentage.Text.ToString());
                }


            }

            DialogResult = System.Windows.Forms.DialogResult.OK;


        }

        private void txtPorcentage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan
                    e.Handled = true;
                }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
