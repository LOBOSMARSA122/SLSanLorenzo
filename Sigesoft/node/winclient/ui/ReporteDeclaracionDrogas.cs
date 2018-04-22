using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class ReporteDeclaracionDrogas : Form
    {
        private readonly string _serviceId;
        public ReporteDeclaracionDrogas(string serviceId)
        {
            InitializeComponent();
            _serviceId = serviceId;
        }

        private void ReporteDeclaracionDrogas_Load(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                ShowReport();
            }
        }

        private void ShowReport()
        {
            var rp = new Reports.crApendice09_Drogas();

            var result = new ServiceBL().GetReportCocainaMarihuana(_serviceId, Constants.TOXICOLOGICO_COCAINA_MARIHUANA_ID);
            if (result == null)
            {
                MessageBox.Show("No hay impresión disponible","Información");
                return;
            }
            var ds1 = new DataSet();

            var dt = BLL.Utils.ConvertToDatatable(result);

            dt.TableName = "dtAutorizacionDosajeDrogas";

            ds1.Tables.Add(dt);

            rp.SetDataSource(ds1);

            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Show();

        }
    }
}
