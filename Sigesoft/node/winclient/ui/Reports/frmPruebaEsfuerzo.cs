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
    public partial class frmPruebaEsfuerzo : Form
    {
        private string _serviceId;
        private string _ComponentId;

        public frmPruebaEsfuerzo(string serviceId, string ComponentId)
        {
            InitializeComponent();
            _serviceId = serviceId;
            _ComponentId = ComponentId;
        }

        private void frmPruebaEsfuerzo_Load(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                ShowReport();
            }
        }

        private void ShowReport()
        {
            OperationResult objOperationResult = new OperationResult();

            var rp = new Reports.crPruebaEsfuerzo();

            var aptitudeCertificate = new ServiceBL().GetReportPruebaEsfuerzo(_serviceId, _ComponentId);
            var FuncionesVitales = new ServiceBL().ReportFuncionesVitales(_serviceId, Constants.FUNCIONES_VITALES_ID);
            var Antropometria = new ServiceBL().ReportAntropometria(_serviceId, Constants.ANTROPOMETRIA_ID);

            
            DataSet ds1 = new DataSet();
            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(aptitudeCertificate);
            dt.TableName = "dtPruebaEsfuerzo";
            ds1.Tables.Add(dt);

            DataTable dt1 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(FuncionesVitales);
            dt1.TableName = "dtFuncionesVitales";
            ds1.Tables.Add(dt1);

            DataTable dt2 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Antropometria);
            dt2.TableName = "dtAntropometria";
            ds1.Tables.Add(dt2);

            rp.SetDataSource(ds1);

            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Show();

        }
    }
}
