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
    public partial class TiemposTrabajadores : Form
    {
        private string _FilterExpression;
        private DateTime? _FechaInicio;
        private DateTime? _Fechafin;
        private List<string> _Componentes;

        public TiemposTrabajadores(string pstrFilterExpression, DateTime? FechaInicio, DateTime? FechaFin, List<String> ListaComponentes)
        {
            InitializeComponent();
            _FilterExpression = pstrFilterExpression;
            _FechaInicio = FechaInicio;
            _Fechafin = FechaFin;
            _Componentes = ListaComponentes;
        }

        private void TiemposTrabajadores_Load(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                ShowReport();
            }
        }

        private void ShowReport()
        {
            OperationResult objOperationResult = new OperationResult();

            var rp = new Reports.crTiemposTrabajadores();

            var aptitudeCertificate = new ServiceBL().ReporteTiempoTrabajadores(_FilterExpression, _FechaInicio, _Fechafin, _Componentes);
            DataSet ds1 = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(aptitudeCertificate);

            dt.TableName = "dtTiempoTrabajador";

            ds1.Tables.Add(dt);

            rp.SetDataSource(ds1);

            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Show();

        }
    }
}
