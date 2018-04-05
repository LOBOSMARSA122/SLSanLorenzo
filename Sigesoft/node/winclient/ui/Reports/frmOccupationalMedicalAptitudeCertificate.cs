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
    public partial class frmOccupationalMedicalAptitudeCertificate : Form
    {
        private string _serviceId;

        public frmOccupationalMedicalAptitudeCertificate(string serviceId)
        {
            InitializeComponent();
            _serviceId = serviceId;
        }

        private void ShowReport()
        {
            OperationResult objOperationResult = new OperationResult();
            var aptitudeCertificate = new ServiceBL().GetAptitudeCertificate(ref objOperationResult, _serviceId).ToList();
            var TipoEso = aptitudeCertificate[0].i_EsoTypeId.ToString();
            if (TipoEso == "3")
            {
                var rp = new Reports.crOccupationalMedicalAptitudeCertificateRetiros();
                DataSet ds = new DataSet();
                DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(aptitudeCertificate);
                dt.TableName = "AptitudeCertificate";
                ds.Tables.Add(dt);
                rp.SetDataSource(ds);

                crystalReportViewer1.ReportSource = rp;
                crystalReportViewer1.Show();
            }
            else
            {
                var rp = new Reports.crOccupationalMedicalAptitudeCertificate();
                DataSet ds = new DataSet();
                DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(aptitudeCertificate);
                dt.TableName = "AptitudeCertificate";
                ds.Tables.Add(dt);
                rp.SetDataSource(ds);

                crystalReportViewer1.ReportSource = rp;
                crystalReportViewer1.Show();
            }
          

           

          
        }

        private void frmOccupationalMedicalAptitudeCertificate_Load(object sender, EventArgs e)
        {
            ShowReport();
        }

    }
}
