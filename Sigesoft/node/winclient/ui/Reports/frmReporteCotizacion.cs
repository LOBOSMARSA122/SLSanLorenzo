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
    public partial class frmReporteCotizacion : Form
    {
        private string _ServiceOrderId;
        public frmReporteCotizacion(string pstrServiceOrderId)
        {
            InitializeComponent();
            _ServiceOrderId = pstrServiceOrderId;
        }

        private void frmReporteCotizacion_Load(object sender, EventArgs e)
        {
            ShowReport();
        }

        private void ShowReport()
        {
            OperationResult objOperationResult = new OperationResult();
            var rp = new Reports.crCotizacion();

            var OrderService = new ServiceOrderBL().GetReportServiceOrder(_ServiceOrderId,"");

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
