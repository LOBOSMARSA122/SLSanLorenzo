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
    public partial class frmInformePsicologicoOcupacional : Form
    {
        private string _serviceId;

        public frmInformePsicologicoOcupacional(string serviceId)
        {
            InitializeComponent();
            _serviceId = serviceId;
        }

        private void ShowReport()
        {
            OperationResult objOperationResult = new OperationResult();

            var rp = new Reports.InformePsicologicoOcupacional();

            var aptitudeCertificate = new PacientBL().GetFichaPsicologicaOcupacional(_serviceId);           

            DataSet ds1 = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(aptitudeCertificate);        

            dt.TableName = "InformePsico";

            ds1.Tables.Add(dt);

            rp.SetDataSource(ds1);
            
            crystalReportViewer1.ReportSource = rp;
            crystalReportViewer1.Show();
         
        }

        private void frmInformePsicologicoOcupacional_Load(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                ShowReport();
            }
        }

        private void frmInformePsicologicoOcupacional_Activated(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.TopMost = false;
        }

    }
}
