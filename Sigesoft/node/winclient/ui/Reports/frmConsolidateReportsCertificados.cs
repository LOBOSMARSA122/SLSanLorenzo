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
using CrystalDecisions.Shared;

namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmConsolidateReportsCertificados : Form
    {
        private string _serviceId;
        private List<string> _componentId;
        DataSet dsGetRepo = null;
        private ServiceBL serviceBL = new ServiceBL();
        crConsolidatedCertificados rp = null;

        public frmConsolidateReportsCertificados()
        {
            InitializeComponent();
        }

        public frmConsolidateReportsCertificados(string serviceId, List<string> componentId)
        {
            InitializeComponent();
            _serviceId = serviceId;
            _componentId = componentId;
        }

        private void frmConsolidateReportsCertificados_Load(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                rp = new Reports.crConsolidatedCertificados();

                foreach (var com in _componentId)
                {
                    ChooseReport(rp, com);
                }

                crystalReportViewer1.EnableDrillDown = false;
                crystalReportViewer1.ReportSource = rp;

                ReportDocument repDoc = rp;

                repDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                repDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
                objDiskOpt.DiskFileName = Application.StartupPath + @"\TempMerge\Crystal2.pdf";
                repDoc.ExportOptions.DestinationOptions = objDiskOpt;
                repDoc.Export();



                //crystalReportViewer1.Show();
            }
        }

        private void ChooseReport(crConsolidatedCertificados rp, string componentId)
        {
            DataSet ds = null;
            switch (componentId)
            {
                case Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL:
                   
                    ds = GetReportCAPE();
                    rp.Subreports["crCertificadoDeAptitudEmpresarial.rpt"].SetDataSource(ds);
                    rp.CAPE.SectionFormat.EnableSuppress = false;
                    break;

                case Constants.INFORME_CERTIFICADO_APTITUD_SM:
                
                    ds = GetReportCAPSM();
                    rp.Subreports["crCertficadoDeAptitudSM.rpt"].SetDataSource(ds);
                    rp.CAPSM.SectionFormat.EnableSuppress = false;
                    break;

                case Constants.INFORME_CERTIFICADO_APTITUD_COMPLETO:
                  
                    ds = GetReportCAPC();
                    rp.Subreports["crCertificadoDeAptitudCompleto.rpt"].SetDataSource(ds);
                    rp.CAPC.SectionFormat.EnableSuppress = false;
                    break;

                case Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX:
                  
                    ds = GetReportCAPSD();
                    rp.Subreports["crCertificadoDeAptitudSinDX.rpt"].SetDataSource(ds);
                    rp.CAPSD.SectionFormat.EnableSuppress = false;
                    break;
                default:
                    break;
            }
        }

        private DataSet GetReportCAPE()
        {
            var dataListForReport = new ServiceBL().GetCAPE(_serviceId);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt.TableName = "AptitudeCertificate";

            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetReportCAPSM()
        {
            dsGetRepo = new DataSet();
            var dataListForReport = new ServiceBL().GetCAPSM(_serviceId);
            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);
            dt.TableName = "AptitudeCertificate";
            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }

        private DataSet GetReportCAPC()
        {
            var Path = Application.StartupPath;
            var dataListForReport = new ServiceBL().GetCAPC(_serviceId);
            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt.TableName = "AptitudeCertificate";

            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }
        private DataSet GetReportCAPSD()
        {
            var Path = Application.StartupPath;
            var dataListForReport = new ServiceBL().GetCAPSD(_serviceId, Path);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt.TableName = "AptitudeCertificate";

            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;
        }
    }
}
