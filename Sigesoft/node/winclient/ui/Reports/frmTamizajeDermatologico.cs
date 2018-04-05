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
    public partial class frmTamizajeDermatologico : Form
    {
        private string _serviceId;
        private string _componentId;

        public frmTamizajeDermatologico(string pstrServiceId, string pstrComponetId)
        {
            _serviceId = pstrServiceId;
            _componentId = pstrComponetId;
            InitializeComponent();
        }

        private void frmTamizajeDermatologico_Load(object sender, EventArgs e)
        {
            ShowReport();
        }

        private void ShowReport()
        {
            try
            {
                OperationResult objOperationResult = new OperationResult();

                var rp = new Reports.crTamizajeDermatologico();

                var AscensoAlturas = new ServiceBL().ReportTamizajeDermatologico(_serviceId, Constants.TAMIZAJE_DERMATOLOGIO_ID);
              
                DataSet ds1 = new DataSet();
                DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(AscensoAlturas);
                dt.TableName = "dtTamizajeDermatologico";
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
