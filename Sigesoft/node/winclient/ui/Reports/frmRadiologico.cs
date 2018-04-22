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
    public partial class frmRadiologico : Form
    {
        private readonly string _serviceId;

        public frmRadiologico(string serviceId)
        {
            InitializeComponent();
            _serviceId = serviceId;
        }

        private void frmRadiologico_Load(object sender, EventArgs e)
        {
            ShowReport();
        }

        private void ShowReport()
        {
            OperationResult objOperationResult = new OperationResult();

            var rp = new Reports.crInformeRadiologico();

            var aptitudeCertificate = new ServiceBL().ReportRadiologico(_serviceId, Constants.RX_TORAX_ID);
            DataSet ds1 = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(aptitudeCertificate);

            dt.TableName = "dtRadiologico";

            ds1.Tables.Add(dt);

            rp.SetDataSource(ds1);

            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Show();

        }
    }
}
