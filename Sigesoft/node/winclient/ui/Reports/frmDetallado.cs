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
    public partial class frmDetallado : Form
    {
        private string _FilterExpression;
        private DateTime _FechaInicio;
        private DateTime _FechaFin;
        public frmDetallado(string pstrFilterExpression, DateTime FechaIni, DateTime FechaFin)
        {
            InitializeComponent();
            _FilterExpression = pstrFilterExpression;
            _FechaInicio = FechaIni;
            _FechaFin = FechaFin;
        }

        private void frmDetallado_Load(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                ShowReport();
            }
        }

        private void ShowReport()
        {
            OperationResult objOperationResult = new OperationResult();

            var rp = new Reports.crAgendaDetallado();

            var aptitudeCertificate = new CalendarBL().ReporteAgenda(ref objOperationResult, 0, null, null, _FilterExpression, _FechaInicio, _FechaFin);
            DataSet ds1 = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(aptitudeCertificate);

            dt.TableName = "dtAgendaDetallado";

            ds1.Tables.Add(dt);

            rp.SetDataSource(ds1);

            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Show();

        }
    }
}
