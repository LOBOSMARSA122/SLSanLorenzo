using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmTracking : Form
    {
        public frmTracking()
        {
            InitializeComponent();
        }

        private void frmTracking_Load(object sender, EventArgs e)
        {
            BindingGrid("");
        }

        private void BindingGrid(string name)
        {
            var DataSource = new TrackingBL().GetAllDataTracking(name);
            grdEstados.DataSource = DataSource;

            foreach (var row in grdEstados.Rows)
            {
                foreach (var cell in row.Cells)
                {
                    if (cell.Value.ToString().Length == 1)
                    {
                        if (int.Parse(cell.Value.ToString()) == (int)TrackingStatus.Iniciado)
                        {
                            cell.Appearance.BackColor = Color.YellowGreen;
                            
                        }
                        if (int.Parse(cell.Value.ToString()) == (int)TrackingStatus.Culminado)
                        {
                            cell.Appearance.BackColor = Color.DarkGreen;
                        }

                        cell.Value = null;
                    }                    
                }
            }

            grdEstados.DataBind();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            BindingGrid(txtPacient.Text);
        }

        
    }
    
}
