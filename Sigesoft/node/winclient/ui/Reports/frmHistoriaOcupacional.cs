using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmHistoriaOcupacional : Form
    {
        private string _serviceId;
        public frmHistoriaOcupacional(string pstrServiceId)
        {
            _serviceId = pstrServiceId;
            InitializeComponent();
        }

        private void frmHistoriaOcupacional_Load(object sender, EventArgs e)
        {
            ShowReport();
        }

        private void ShowReport()
        {
            try
            {
                OperationResult objOperationResult = new OperationResult();

                var rp = new Reports.crHistoriaOcupacional();

                var aptitudeCertificate = new ServiceBL().ReportHistoriaOcupacional(_serviceId);

                DataSet ds1 = new DataSet();

                DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(aptitudeCertificate);

                dt.TableName = "HistoriaOcupacional";

                ds1.Tables.Add(dt);

                rp.SetDataSource(ds1);

                crystalReportViewer1.ReportSource = rp;
                crystalReportViewer1.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
