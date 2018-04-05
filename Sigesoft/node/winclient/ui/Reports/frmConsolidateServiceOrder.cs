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
    public partial class frmConsolidateServiceOrder : Form
    {
        crConsolidateServiceOrder rp = null;
        private string _serviceOrderId;
        private List<string> _protocolId;
        DataSet dsGetRepo = null;

        public frmConsolidateServiceOrder(string pstrServiceOrder, List<string> ProtocolId)
        {
            InitializeComponent();
            _serviceOrderId = pstrServiceOrder;
            _protocolId = ProtocolId;
            CargarReporte();
        }
        private void CargarReporte()
        {
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                rp = new Reports.crConsolidateServiceOrder();

                ChooseReport(rp, _protocolId, _protocolId.Count());

                crystalReportViewer1.EnableDrillDown = false;
                var Path = Application.StartupPath;
                rp.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Path + @"\Temp\Reporte.pdf");
                crystalReportViewer1.ReportSource = rp;
                crystalReportViewer1.Show();
            }
        }
        private void frmConsolidateServiceOrder_Load(object sender, EventArgs e)
        {
           
        }

        private void ChooseReport(crConsolidateServiceOrder rp, List<string> protocolId, int Cantidad)
        {
            DataSet ds = null;

            switch (Cantidad)
            {
                case 1:
                       ds = GetReportServiceOrder(protocolId[0]);
                        rp.Subreports["crServiceOrder.rpt"].SetDataSource(ds);
                        rp.report1.SectionFormat.EnableSuppress = false;
                        break;

                case 2:
                        rp.Subreports["crServiceOrder.rpt"].SetDataSource(GetReportServiceOrder(protocolId[0]));
                        rp.report1.SectionFormat.EnableSuppress = false;
                     
                        rp.Subreports["crServiceOrder.rpt - 01"].SetDataSource(GetReportServiceOrder(protocolId[1]));
                        rp.report2.SectionFormat.EnableSuppress = false;
                        break;
                case 3:
                        rp.Subreports["crServiceOrder.rpt"].SetDataSource(GetReportServiceOrder(protocolId[0]));
                        rp.report1.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 01"].SetDataSource(GetReportServiceOrder(protocolId[1]));
                        rp.report2.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 02"].SetDataSource(GetReportServiceOrder(protocolId[2]));
                        rp.report3.SectionFormat.EnableSuppress = false;
                        break;
                case 4:
                        rp.Subreports["crServiceOrder.rpt"].SetDataSource(GetReportServiceOrder(protocolId[0]));
                        rp.report1.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 01"].SetDataSource(GetReportServiceOrder(protocolId[1]));
                        rp.report2.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 02"].SetDataSource(GetReportServiceOrder(protocolId[2]));
                        rp.report3.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 03"].SetDataSource(GetReportServiceOrder(protocolId[3]));
                        rp.report4.SectionFormat.EnableSuppress = false;

                        break;
                case 5:
                        rp.Subreports["crServiceOrder.rpt"].SetDataSource(GetReportServiceOrder(protocolId[0]));
                        rp.report1.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 01"].SetDataSource(GetReportServiceOrder(protocolId[1]));
                        rp.report2.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 02"].SetDataSource(GetReportServiceOrder(protocolId[2]));
                        rp.report3.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 03"].SetDataSource(GetReportServiceOrder(protocolId[3]));
                        rp.report4.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 04"].SetDataSource(GetReportServiceOrder(protocolId[4]));
                        rp.report5.SectionFormat.EnableSuppress = false;
                        break;
                case 6:
                        rp.Subreports["crServiceOrder.rpt"].SetDataSource(GetReportServiceOrder(protocolId[0]));
                        rp.report1.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 01"].SetDataSource(GetReportServiceOrder(protocolId[1]));
                        rp.report2.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 02"].SetDataSource(GetReportServiceOrder(protocolId[2]));
                        rp.report3.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 03"].SetDataSource(GetReportServiceOrder(protocolId[3]));
                        rp.report4.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 04"].SetDataSource(GetReportServiceOrder(protocolId[4]));
                        rp.report5.SectionFormat.EnableSuppress = false;

                         rp.Subreports["crServiceOrder.rpt - 05"].SetDataSource(GetReportServiceOrder(protocolId[5]));
                        rp.report6.SectionFormat.EnableSuppress = false;
                        break;
                case 7:
                        rp.Subreports["crServiceOrder.rpt"].SetDataSource(GetReportServiceOrder(protocolId[0]));
                        rp.report1.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 01"].SetDataSource(GetReportServiceOrder(protocolId[1]));
                        rp.report2.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 02"].SetDataSource(GetReportServiceOrder(protocolId[2]));
                        rp.report3.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 03"].SetDataSource(GetReportServiceOrder(protocolId[3]));
                        rp.report4.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 04"].SetDataSource(GetReportServiceOrder(protocolId[4]));
                        rp.report5.SectionFormat.EnableSuppress = false;

                         rp.Subreports["crServiceOrder.rpt - 05"].SetDataSource(GetReportServiceOrder(protocolId[5]));
                        rp.report6.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 06"].SetDataSource(GetReportServiceOrder(protocolId[6]));
                        rp.report7.SectionFormat.EnableSuppress = false;
                        break;
                case 8:
                         rp.Subreports["crServiceOrder.rpt"].SetDataSource(GetReportServiceOrder(protocolId[0]));
                        rp.report1.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 01"].SetDataSource(GetReportServiceOrder(protocolId[1]));
                        rp.report2.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 02"].SetDataSource(GetReportServiceOrder(protocolId[2]));
                        rp.report3.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 03"].SetDataSource(GetReportServiceOrder(protocolId[3]));
                        rp.report4.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 04"].SetDataSource(GetReportServiceOrder(protocolId[4]));
                        rp.report5.SectionFormat.EnableSuppress = false;

                         rp.Subreports["crServiceOrder.rpt - 05"].SetDataSource(GetReportServiceOrder(protocolId[5]));
                        rp.report6.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 06"].SetDataSource(GetReportServiceOrder(protocolId[6]));
                        rp.report7.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 07"].SetDataSource(GetReportServiceOrder(protocolId[7]));
                        rp.report8.SectionFormat.EnableSuppress = false;
                        break;
                case 9:
                              rp.Subreports["crServiceOrder.rpt"].SetDataSource(GetReportServiceOrder(protocolId[0]));
                        rp.report1.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 01"].SetDataSource(GetReportServiceOrder(protocolId[1]));
                        rp.report2.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 02"].SetDataSource(GetReportServiceOrder(protocolId[2]));
                        rp.report3.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 03"].SetDataSource(GetReportServiceOrder(protocolId[3]));
                        rp.report4.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 04"].SetDataSource(GetReportServiceOrder(protocolId[4]));
                        rp.report5.SectionFormat.EnableSuppress = false;

                         rp.Subreports["crServiceOrder.rpt - 05"].SetDataSource(GetReportServiceOrder(protocolId[5]));
                        rp.report6.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 06"].SetDataSource(GetReportServiceOrder(protocolId[6]));
                        rp.report7.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 07"].SetDataSource(GetReportServiceOrder(protocolId[7]));
                        rp.report8.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 08"].SetDataSource(GetReportServiceOrder(protocolId[8]));
                        rp.report9.SectionFormat.EnableSuppress = false;
                        break;
                case 10:
                        rp.Subreports["crServiceOrder.rpt"].SetDataSource(GetReportServiceOrder(protocolId[0]));
                        rp.report1.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 01"].SetDataSource(GetReportServiceOrder(protocolId[1]));
                        rp.report2.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 02"].SetDataSource(GetReportServiceOrder(protocolId[2]));
                        rp.report3.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 03"].SetDataSource(GetReportServiceOrder(protocolId[3]));
                        rp.report4.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 04"].SetDataSource(GetReportServiceOrder(protocolId[4]));
                        rp.report5.SectionFormat.EnableSuppress = false;

                         rp.Subreports["crServiceOrder.rpt - 05"].SetDataSource(GetReportServiceOrder(protocolId[5]));
                        rp.report6.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 06"].SetDataSource(GetReportServiceOrder(protocolId[6]));
                        rp.report7.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 07"].SetDataSource(GetReportServiceOrder(protocolId[7]));
                        rp.report8.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 08"].SetDataSource(GetReportServiceOrder(protocolId[8]));
                        rp.report9.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 09"].SetDataSource(GetReportServiceOrder(protocolId[9]));
                        rp.report10.SectionFormat.EnableSuppress = false;
                        break;
                case 11:
                        rp.Subreports["crServiceOrder.rpt"].SetDataSource(GetReportServiceOrder(protocolId[0]));
                        rp.report1.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 01"].SetDataSource(GetReportServiceOrder(protocolId[1]));
                        rp.report2.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 02"].SetDataSource(GetReportServiceOrder(protocolId[2]));
                        rp.report3.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 03"].SetDataSource(GetReportServiceOrder(protocolId[3]));
                        rp.report4.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 04"].SetDataSource(GetReportServiceOrder(protocolId[4]));
                        rp.report5.SectionFormat.EnableSuppress = false;

                         rp.Subreports["crServiceOrder.rpt - 05"].SetDataSource(GetReportServiceOrder(protocolId[5]));
                        rp.report6.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 06"].SetDataSource(GetReportServiceOrder(protocolId[6]));
                        rp.report7.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 07"].SetDataSource(GetReportServiceOrder(protocolId[7]));
                        rp.report8.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 08"].SetDataSource(GetReportServiceOrder(protocolId[8]));
                        rp.report9.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 09"].SetDataSource(GetReportServiceOrder(protocolId[9]));
                        rp.report10.SectionFormat.EnableSuppress = false;

                      rp.Subreports["crServiceOrder.rpt - 10"].SetDataSource(GetReportServiceOrder(protocolId[10]));
                        rp.report11.SectionFormat.EnableSuppress = false;
                        break;
                case 12:
                           rp.Subreports["crServiceOrder.rpt"].SetDataSource(GetReportServiceOrder(protocolId[0]));
                        rp.report1.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 01"].SetDataSource(GetReportServiceOrder(protocolId[1]));
                        rp.report2.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 02"].SetDataSource(GetReportServiceOrder(protocolId[2]));
                        rp.report3.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 03"].SetDataSource(GetReportServiceOrder(protocolId[3]));
                        rp.report4.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 04"].SetDataSource(GetReportServiceOrder(protocolId[4]));
                        rp.report5.SectionFormat.EnableSuppress = false;

                         rp.Subreports["crServiceOrder.rpt - 05"].SetDataSource(GetReportServiceOrder(protocolId[5]));
                        rp.report6.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 06"].SetDataSource(GetReportServiceOrder(protocolId[6]));
                        rp.report7.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 07"].SetDataSource(GetReportServiceOrder(protocolId[7]));
                        rp.report8.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 08"].SetDataSource(GetReportServiceOrder(protocolId[8]));
                        rp.report9.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 09"].SetDataSource(GetReportServiceOrder(protocolId[9]));
                        rp.report10.SectionFormat.EnableSuppress = false;

                      rp.Subreports["crServiceOrder.rpt - 10"].SetDataSource(GetReportServiceOrder(protocolId[10]));
                        rp.report11.SectionFormat.EnableSuppress = false;

                             rp.Subreports["crServiceOrder.rpt - 11"].SetDataSource(GetReportServiceOrder(protocolId[11]));
                        rp.report12.SectionFormat.EnableSuppress = false;
                        break;
                case 13:
                                   rp.Subreports["crServiceOrder.rpt"].SetDataSource(GetReportServiceOrder(protocolId[0]));
                        rp.report1.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 01"].SetDataSource(GetReportServiceOrder(protocolId[1]));
                        rp.report2.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 02"].SetDataSource(GetReportServiceOrder(protocolId[2]));
                        rp.report3.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 03"].SetDataSource(GetReportServiceOrder(protocolId[3]));
                        rp.report4.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 04"].SetDataSource(GetReportServiceOrder(protocolId[4]));
                        rp.report5.SectionFormat.EnableSuppress = false;

                         rp.Subreports["crServiceOrder.rpt - 05"].SetDataSource(GetReportServiceOrder(protocolId[5]));
                        rp.report6.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 06"].SetDataSource(GetReportServiceOrder(protocolId[6]));
                        rp.report7.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 07"].SetDataSource(GetReportServiceOrder(protocolId[7]));
                        rp.report8.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 08"].SetDataSource(GetReportServiceOrder(protocolId[8]));
                        rp.report9.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 09"].SetDataSource(GetReportServiceOrder(protocolId[9]));
                        rp.report10.SectionFormat.EnableSuppress = false;

                      rp.Subreports["crServiceOrder.rpt - 10"].SetDataSource(GetReportServiceOrder(protocolId[10]));
                        rp.report11.SectionFormat.EnableSuppress = false;

                             rp.Subreports["crServiceOrder.rpt - 11"].SetDataSource(GetReportServiceOrder(protocolId[11]));
                        rp.report12.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 12"].SetDataSource(GetReportServiceOrder(protocolId[12]));
                        rp.report13.SectionFormat.EnableSuppress = false;
                        break;
                case 14:
                         rp.Subreports["crServiceOrder.rpt"].SetDataSource(GetReportServiceOrder(protocolId[0]));
                        rp.report1.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 01"].SetDataSource(GetReportServiceOrder(protocolId[1]));
                        rp.report2.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 02"].SetDataSource(GetReportServiceOrder(protocolId[2]));
                        rp.report3.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 03"].SetDataSource(GetReportServiceOrder(protocolId[3]));
                        rp.report4.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 04"].SetDataSource(GetReportServiceOrder(protocolId[4]));
                        rp.report5.SectionFormat.EnableSuppress = false;

                         rp.Subreports["crServiceOrder.rpt - 05"].SetDataSource(GetReportServiceOrder(protocolId[5]));
                        rp.report6.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 06"].SetDataSource(GetReportServiceOrder(protocolId[6]));
                        rp.report7.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 07"].SetDataSource(GetReportServiceOrder(protocolId[7]));
                        rp.report8.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 08"].SetDataSource(GetReportServiceOrder(protocolId[8]));
                        rp.report9.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 09"].SetDataSource(GetReportServiceOrder(protocolId[9]));
                        rp.report10.SectionFormat.EnableSuppress = false;

                      rp.Subreports["crServiceOrder.rpt - 10"].SetDataSource(GetReportServiceOrder(protocolId[10]));
                        rp.report11.SectionFormat.EnableSuppress = false;

                             rp.Subreports["crServiceOrder.rpt - 11"].SetDataSource(GetReportServiceOrder(protocolId[11]));
                        rp.report12.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 12"].SetDataSource(GetReportServiceOrder(protocolId[12]));
                        rp.report13.SectionFormat.EnableSuppress = false;

                          rp.Subreports["crServiceOrder.rpt - 13"].SetDataSource(GetReportServiceOrder(protocolId[13]));
                        rp.report14.SectionFormat.EnableSuppress = false;
                        break;

                case 15:
                        rp.Subreports["crServiceOrder.rpt"].SetDataSource(GetReportServiceOrder(protocolId[0]));
                        rp.report1.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 01"].SetDataSource(GetReportServiceOrder(protocolId[1]));
                        rp.report2.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 02"].SetDataSource(GetReportServiceOrder(protocolId[2]));
                        rp.report3.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 03"].SetDataSource(GetReportServiceOrder(protocolId[3]));
                        rp.report4.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 04"].SetDataSource(GetReportServiceOrder(protocolId[4]));
                        rp.report5.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 05"].SetDataSource(GetReportServiceOrder(protocolId[5]));
                        rp.report6.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 06"].SetDataSource(GetReportServiceOrder(protocolId[6]));
                        rp.report7.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 07"].SetDataSource(GetReportServiceOrder(protocolId[7]));
                        rp.report8.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 08"].SetDataSource(GetReportServiceOrder(protocolId[8]));
                        rp.report9.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 09"].SetDataSource(GetReportServiceOrder(protocolId[9]));
                        rp.report10.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 10"].SetDataSource(GetReportServiceOrder(protocolId[10]));
                        rp.report11.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 11"].SetDataSource(GetReportServiceOrder(protocolId[11]));
                        rp.report12.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 12"].SetDataSource(GetReportServiceOrder(protocolId[12]));
                        rp.report13.SectionFormat.EnableSuppress = false;

                        rp.Subreports["crServiceOrder.rpt - 13"].SetDataSource(GetReportServiceOrder(protocolId[13]));
                        rp.report14.SectionFormat.EnableSuppress = false;

                       rp.Subreports["crServiceOrder.rpt - 14"].SetDataSource(GetReportServiceOrder(protocolId[14]));
                        rp.report15.SectionFormat.EnableSuppress = false;
                        break;
                default:
                        break;
            }
         


        }

        private DataSet GetReportServiceOrder(string pstrProtocolId)
        {
            var dataListForReport = new ServiceOrderBL().GetReportServiceOrder(_serviceOrderId, pstrProtocolId).FindAll( p => p.r_Price !=0);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt.TableName = "ServiceOrder";

            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;


        }

    }
}
