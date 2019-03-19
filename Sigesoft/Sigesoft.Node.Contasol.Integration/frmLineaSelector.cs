using Infragistics.Win.UltraWinGrid;
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
        //DataGridView ultraGrid1 = new System.Windows.Forms.DataGridView();
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
            lblConteoLineas.Text = string.Format("Se encontraron {0} registros.", this.ultraGrid1.Rows.Count());
        }

        private void ultraGrid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            Close();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (txtUnidadProductiva.Text != "")
            {
                var objDataService = MedicamentoDao.ObtenerLineasWhere(txtUnidadProductiva.Text);
                ultraGrid1.DataSource = objDataService;
                lblConteoLineas.Text = string.Format("Se encontraron {0} registros.", this.ultraGrid1.Rows.Count());
                //this.ultraGrid1.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            }
            else
            {
                var objDataService = MedicamentoDao.ObtenerLineas();
                ultraGrid1.DataSource = objDataService;
                lblConteoLineas.Text = string.Format("Se encontraron {0} registros.", this.ultraGrid1.Rows.Count());
                //this.ultraGrid1.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            }
            
        }

        private void txtUnidadProductiva_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnFilter_Click(sender, e);
            }
        }
    }
}
