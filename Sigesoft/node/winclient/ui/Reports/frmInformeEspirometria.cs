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
    public partial class frmInformeEspirometria : Form
    {
        private string _serviceId;
        private string _ComponentId;

        public frmInformeEspirometria(string serviceId, string ComponentId)
        {
            InitializeComponent();
            _serviceId = serviceId;
            _ComponentId = ComponentId;
        }

        private void frmInformeEspirometria_Load(object sender, EventArgs e)
        {
            ShowReport();
        }

        private void ShowReport()
        {
            OperationResult objOperationResult = new OperationResult();

            var rp = new Reports.crInformeEspirometria();

            var aptitudeCertificate = new ServiceBL().GetReportInformeEspirometria(_serviceId, _ComponentId);
            DataSet ds1 = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(aptitudeCertificate);

            dt.TableName = "dtInformeEspirometria";

            ds1.Tables.Add(dt);

            rp.SetDataSource(ds1);

            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Show();

        }

    }


}
