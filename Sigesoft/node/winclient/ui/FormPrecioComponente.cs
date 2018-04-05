﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class FormPrecioComponente : Form
    {
        public float Precio { get; set; }
        public FormPrecioComponente(string pstrNombreComponente, string pdecPrecio)
        {          
            InitializeComponent();
            lblNombreComponente.Text = pstrNombreComponente;
            txtPrecio.Text = pdecPrecio;
            calcular();           
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Precio = float.Parse(txtTotal.Text.ToString());
            this.DialogResult = DialogResult.OK;
        }

        private void txtFactor_TextChanged(object sender, EventArgs e)
        {
            calcular();            
     
        }

       void calcular()
        {

           double Precio = double.Parse(txtPrecio.Text.ToString());
           double Factor = double.Parse(txtFactor.Text.ToString());
           txtTotal.Text = (Precio * Factor).ToString();
        }
                

     

    }
}
