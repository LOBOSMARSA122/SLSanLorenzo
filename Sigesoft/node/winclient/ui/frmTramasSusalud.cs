using Infragistics.Win.UltraWinGrid;
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
    public partial class frmTramasSusalud : Form
    {
        public frmTramasSusalud()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string tabName = utcSusalud.SelectedTab.Text;
            frmRegistroEmAmHos frmRegistroEm = new frmRegistroEmAmHos(tabName);
            frmRegistroEm.Text = "Registrar: "+tabName;
            if (tabName == "Ambulatorio" || tabName == "Emergencia"  || tabName == "Partos" || tabName == "Cirugías")
            {
                frmRegistroEm.Size = new Size(638, 196);
            }
            else if (tabName == "Hospitalización" )
            {
                frmRegistroEm.Size = new Size(638, 236);
            }
            else if (tabName == "Procedimientos / Cirugía")
            {
                frmRegistroEm.Size = new Size(638, 300);
            } 
            frmRegistroEm.Show();

           
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            frmExportTramas frm = new frmExportTramas();
            frm.Show();
        }

       
       
        
    }
}
