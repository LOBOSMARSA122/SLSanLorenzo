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
    public partial class FrmAddPlain : Form
    {
        public FrmAddPlain()
        {
            InitializeComponent();
        }

        private void ultraGrid1_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {

        }

        private void btnAgregarExamenAuxiliar_Click(object sender, EventArgs e)
        {
            var frm = new FrmDeducibleCoaseguro();


            frm.ShowDialog();

 
        }

        private void btnRemoverExamenAuxiliar_Click(object sender, EventArgs e)
        {

        }

        private void lvExamenesSeleccionados_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}
