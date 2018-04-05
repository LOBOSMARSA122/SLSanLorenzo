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
    public partial class frmOdontograma : Form
    {
        private string _serviceId;
        public frmOdontograma(string pstrServiceId)
        {
            _serviceId = pstrServiceId;
            InitializeComponent();
        }

        private void frmOdontograma_Load(object sender, EventArgs e)
        {
            ShowReport();
        }

        private void ShowReport()
        {
            try
            {
                OperationResult objOperationResult = new OperationResult();

                var rp = new Reports.crOdontograma();
                var Path = Application.StartupPath;
                var Odontograma = new ServiceBL().ReportOdontograma(_serviceId, Constants.ODONTOGRAMA_ID, Path);
    
                DataSet ds1 = new DataSet();
                DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Odontograma);
                dt.TableName = "dtOdontograma";
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
