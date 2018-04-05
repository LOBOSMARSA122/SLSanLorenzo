using Sigesoft.Node.Contasol.Integration.Contasol;
using Sigesoft.Node.Contasol.Integration.Contasol.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.Contasol.Integration
{
    public partial class frmLineaSelector : Form
    {
        public LineaDto LineaSeleccionada
        {
            get
            {
                if (ultraGrid1.ActiveRow == null) return null;
                return (LineaDto)ultraGrid1.ActiveRow.ListObject;
            }
        }

        public frmLineaSelector()
        {
            InitializeComponent();
        }

        private void frmLineaSelector_Load(object sender, EventArgs e)
        {
            ultraGrid1.DataSource = MedicamentoDao.ObtenerLineas();
        }

        private void ultraGrid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            Close();
        }
    }
}
