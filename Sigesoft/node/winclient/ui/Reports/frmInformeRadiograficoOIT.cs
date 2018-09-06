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
    public partial class frmInformeRadiograficoOIT : Form
    {
        private string _serviceId;
        public frmInformeRadiograficoOIT(string pstrServiceId)
        {
            _serviceId = pstrServiceId;

            InitializeComponent();
        }

        private void frmInformeRadiograficoOIT_Load(object sender, EventArgs e)
        {
            ShowReport();
        }

        private void ShowReport()
        {
            try
            {
                OperationResult objOperationResult = new OperationResult();

                var rp = new Reports.crInformeRadiograficoOIT();

                //var AscensoAlturas = new ServiceBL().ReportInformeRadiografico(_serviceId, Constants.RX_TORAX_ID);
                var AscensoAlturas = new ServiceBL().ReportInformeRadiografico(_serviceId, Constants.OIT_ID);

                DataSet ds1 = new DataSet();
                DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(AscensoAlturas);
                dt.TableName = "dtInformeRadiografico";
                ds1.Tables.Add(dt);

                rp.SetDataSource(ds1);

                crystalReportViewer2.ReportSource = rp;
                crystalReportViewer2.Show();




            }
            catch (Exception)
            {

                throw;
            }


        }

    }
}
