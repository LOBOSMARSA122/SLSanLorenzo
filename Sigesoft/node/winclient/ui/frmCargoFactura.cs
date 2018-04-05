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
using Sigesoft.Node.WinClient.BE;
using System.IO;
using NetPdf;
using Infragistics.Win.UltraWinGrid;
using System.Diagnostics;
using Sigesoft.Node.WinClient.UI.Reports;


namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmCargoFactura : Form
    {
        public frmCargoFactura()
        {
            InitializeComponent();
        }

        private void frmCargoFactura_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.All);
          
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            frmReporteCargoFactura frm = new frmReporteCargoFactura(txtMes.Text.ToUpper(),dtpDateTimeStar.Value.ToString(),txtNroFactura.Text,txtNroTrabajadores.Text,ddlCustomerOrganization.Text.ToUpper());
            frm.ShowDialog();
        }
    }
}
