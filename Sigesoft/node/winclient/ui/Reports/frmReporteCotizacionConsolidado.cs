using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmReporteCotizacionConsolidado : Form
    {
        private string _ServiceOrderId;
        public frmReporteCotizacionConsolidado(string pstrServiceOrderId)
        {
            InitializeComponent();
            _ServiceOrderId = pstrServiceOrderId;
        }

        private void frmReporteCotizacionConsolidado_Load(object sender, EventArgs e)
        {
            ShowReport();
        }

        private void ShowReport()
        {
            OperationResult objOperationResult = new OperationResult();
            var rp = new Reports.crCotizacionConsolidado();

            var OrderService = new ServiceOrderBL().GetReportCotizacionConsolidado(_ServiceOrderId);

            var x = OrderService.FindAll(P => P.r_Price != 0); // eliminamos los componentes con precio 0

            DataSet ds = new DataSet();
            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(x);
            dt.TableName = "ServiceOrder";
            ds.Tables.Add(dt);
            rp.SetDataSource(ds);

            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Show();
        }
    }
}
