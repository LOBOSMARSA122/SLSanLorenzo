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
    public partial class frmConsolidateCotizacion : Form
    {
        crConsolidateCotizacion rp = null;
        private string _serviceOrderId;
        private List<string> _protocolId;
        DataSet dsGetRepo = null;

        public frmConsolidateCotizacion(string pstrServiceOrder, List<string> ProtocolId)
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
                rp = new Reports.crConsolidateCotizacion();

                ChooseReport(rp, _protocolId, _protocolId.Count());

                crystalReportViewer1.EnableDrillDown = false;
                var Path = Application.StartupPath;
                rp.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Path + @"\Temp\Reporte.pdf");
                crystalReportViewer1.ReportSource = rp;
                crystalReportViewer1.Show();


            }
        }
        private void frmConsolidateCotizacion_Load(object sender, EventArgs e)
        {
           
        }

        private void ChooseReport(crConsolidateCotizacion rp, List<string> protocolId, int Cantidad)
        {
            DataSet ds = null;
            switch (Cantidad)
            {
                case 1:
                    ds = GetReportCotizacion(protocolId[0]);
                    rp.Subreports["crCotizacion.rpt"].SetDataSource(ds);
                    rp.report1.SectionFormat.EnableSuppress = false;
                    break;

                case 2:
                    rp.Subreports["crCotizacion.rpt"].SetDataSource(GetReportCotizacion(protocolId[0]));
                    rp.report1.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 01"].SetDataSource(GetReportCotizacion(protocolId[1]));
                    rp.report2.SectionFormat.EnableSuppress = false;
                    break;
                case 3:
                    rp.Subreports["crCotizacion.rpt"].SetDataSource(GetReportCotizacion(protocolId[0]));
                    rp.report1.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 01"].SetDataSource(GetReportCotizacion(protocolId[1]));
                    rp.report2.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 02"].SetDataSource(GetReportCotizacion(protocolId[2]));
                    rp.report3.SectionFormat.EnableSuppress = false;
                    break;
                case 4:
                    rp.Subreports["crCotizacion.rpt"].SetDataSource(GetReportCotizacion(protocolId[0]));
                    rp.report1.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 01"].SetDataSource(GetReportCotizacion(protocolId[1]));
                    rp.report2.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 02"].SetDataSource(GetReportCotizacion(protocolId[2]));
                    rp.report3.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 03"].SetDataSource(GetReportCotizacion(protocolId[3]));
                    rp.report4.SectionFormat.EnableSuppress = false;

                    break;
                case 5:
                    rp.Subreports["crCotizacion.rpt"].SetDataSource(GetReportCotizacion(protocolId[0]));
                    rp.report1.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 01"].SetDataSource(GetReportCotizacion(protocolId[1]));
                    rp.report2.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 02"].SetDataSource(GetReportCotizacion(protocolId[2]));
                    rp.report3.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 03"].SetDataSource(GetReportCotizacion(protocolId[3]));
                    rp.report4.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 04"].SetDataSource(GetReportCotizacion(protocolId[4]));
                    rp.report5.SectionFormat.EnableSuppress = false;
                    break;
                case 6:
                    rp.Subreports["crCotizacion.rpt"].SetDataSource(GetReportCotizacion(protocolId[0]));
                    rp.report1.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 01"].SetDataSource(GetReportCotizacion(protocolId[1]));
                    rp.report2.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 02"].SetDataSource(GetReportCotizacion(protocolId[2]));
                    rp.report3.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 03"].SetDataSource(GetReportCotizacion(protocolId[3]));
                    rp.report4.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 04"].SetDataSource(GetReportCotizacion(protocolId[4]));
                    rp.report5.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 05"].SetDataSource(GetReportCotizacion(protocolId[5]));
                    rp.report6.SectionFormat.EnableSuppress = false;
                    break;
                case 7:
                    rp.Subreports["crCotizacion.rpt"].SetDataSource(GetReportCotizacion(protocolId[0]));
                    rp.report1.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 01"].SetDataSource(GetReportCotizacion(protocolId[1]));
                    rp.report2.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 02"].SetDataSource(GetReportCotizacion(protocolId[2]));
                    rp.report3.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 03"].SetDataSource(GetReportCotizacion(protocolId[3]));
                    rp.report4.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 04"].SetDataSource(GetReportCotizacion(protocolId[4]));
                    rp.report5.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 05"].SetDataSource(GetReportCotizacion(protocolId[5]));
                    rp.report6.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 06"].SetDataSource(GetReportCotizacion(protocolId[6]));
                    rp.report7.SectionFormat.EnableSuppress = false;
                    break;
                case 8:
                    rp.Subreports["crCotizacion.rpt"].SetDataSource(GetReportCotizacion(protocolId[0]));
                    rp.report1.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 01"].SetDataSource(GetReportCotizacion(protocolId[1]));
                    rp.report2.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 02"].SetDataSource(GetReportCotizacion(protocolId[2]));
                    rp.report3.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 03"].SetDataSource(GetReportCotizacion(protocolId[3]));
                    rp.report4.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 04"].SetDataSource(GetReportCotizacion(protocolId[4]));
                    rp.report5.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 05"].SetDataSource(GetReportCotizacion(protocolId[5]));
                    rp.report6.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 06"].SetDataSource(GetReportCotizacion(protocolId[6]));
                    rp.report7.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 07"].SetDataSource(GetReportCotizacion(protocolId[7]));
                    rp.report8.SectionFormat.EnableSuppress = false;
                    break;
                case 9:
                    rp.Subreports["crCotizacion.rpt"].SetDataSource(GetReportCotizacion(protocolId[0]));
                    rp.report1.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 01"].SetDataSource(GetReportCotizacion(protocolId[1]));
                    rp.report2.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 02"].SetDataSource(GetReportCotizacion(protocolId[2]));
                    rp.report3.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 03"].SetDataSource(GetReportCotizacion(protocolId[3]));
                    rp.report4.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 04"].SetDataSource(GetReportCotizacion(protocolId[4]));
                    rp.report5.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 05"].SetDataSource(GetReportCotizacion(protocolId[5]));
                    rp.report6.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 06"].SetDataSource(GetReportCotizacion(protocolId[6]));
                    rp.report7.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 07"].SetDataSource(GetReportCotizacion(protocolId[7]));
                    rp.report8.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 08"].SetDataSource(GetReportCotizacion(protocolId[8]));
                    rp.report9.SectionFormat.EnableSuppress = false;
                    break;
                case 10:
                    rp.Subreports["crCotizacion.rpt"].SetDataSource(GetReportCotizacion(protocolId[0]));
                    rp.report1.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 01"].SetDataSource(GetReportCotizacion(protocolId[1]));
                    rp.report2.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 02"].SetDataSource(GetReportCotizacion(protocolId[2]));
                    rp.report3.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 03"].SetDataSource(GetReportCotizacion(protocolId[3]));
                    rp.report4.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 04"].SetDataSource(GetReportCotizacion(protocolId[4]));
                    rp.report5.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 05"].SetDataSource(GetReportCotizacion(protocolId[5]));
                    rp.report6.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 06"].SetDataSource(GetReportCotizacion(protocolId[6]));
                    rp.report7.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 07"].SetDataSource(GetReportCotizacion(protocolId[7]));
                    rp.report8.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 08"].SetDataSource(GetReportCotizacion(protocolId[8]));
                    rp.report9.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 09"].SetDataSource(GetReportCotizacion(protocolId[9]));
                    rp.report10.SectionFormat.EnableSuppress = false;
                    break;
                case 11:
                    rp.Subreports["crCotizacion.rpt"].SetDataSource(GetReportCotizacion(protocolId[0]));
                    rp.report1.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 01"].SetDataSource(GetReportCotizacion(protocolId[1]));
                    rp.report2.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 02"].SetDataSource(GetReportCotizacion(protocolId[2]));
                    rp.report3.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 03"].SetDataSource(GetReportCotizacion(protocolId[3]));
                    rp.report4.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 04"].SetDataSource(GetReportCotizacion(protocolId[4]));
                    rp.report5.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 05"].SetDataSource(GetReportCotizacion(protocolId[5]));
                    rp.report6.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 06"].SetDataSource(GetReportCotizacion(protocolId[6]));
                    rp.report7.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 07"].SetDataSource(GetReportCotizacion(protocolId[7]));
                    rp.report8.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 08"].SetDataSource(GetReportCotizacion(protocolId[8]));
                    rp.report9.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 09"].SetDataSource(GetReportCotizacion(protocolId[9]));
                    rp.report10.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 10"].SetDataSource(GetReportCotizacion(protocolId[10]));
                    rp.report11.SectionFormat.EnableSuppress = false;
                    break;
                case 12:
                    rp.Subreports["crCotizacion.rpt"].SetDataSource(GetReportCotizacion(protocolId[0]));
                    rp.report1.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 01"].SetDataSource(GetReportCotizacion(protocolId[1]));
                    rp.report2.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 02"].SetDataSource(GetReportCotizacion(protocolId[2]));
                    rp.report3.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 03"].SetDataSource(GetReportCotizacion(protocolId[3]));
                    rp.report4.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 04"].SetDataSource(GetReportCotizacion(protocolId[4]));
                    rp.report5.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 05"].SetDataSource(GetReportCotizacion(protocolId[5]));
                    rp.report6.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 06"].SetDataSource(GetReportCotizacion(protocolId[6]));
                    rp.report7.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 07"].SetDataSource(GetReportCotizacion(protocolId[7]));
                    rp.report8.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 08"].SetDataSource(GetReportCotizacion(protocolId[8]));
                    rp.report9.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 09"].SetDataSource(GetReportCotizacion(protocolId[9]));
                    rp.report10.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 10"].SetDataSource(GetReportCotizacion(protocolId[10]));
                    rp.report11.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 11"].SetDataSource(GetReportCotizacion(protocolId[11]));
                    rp.report12.SectionFormat.EnableSuppress = false;
                    break;
                case 13:
                    rp.Subreports["crCotizacion.rpt"].SetDataSource(GetReportCotizacion(protocolId[0]));
                    rp.report1.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 01"].SetDataSource(GetReportCotizacion(protocolId[1]));
                    rp.report2.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 02"].SetDataSource(GetReportCotizacion(protocolId[2]));
                    rp.report3.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 03"].SetDataSource(GetReportCotizacion(protocolId[3]));
                    rp.report4.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 04"].SetDataSource(GetReportCotizacion(protocolId[4]));
                    rp.report5.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 05"].SetDataSource(GetReportCotizacion(protocolId[5]));
                    rp.report6.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 06"].SetDataSource(GetReportCotizacion(protocolId[6]));
                    rp.report7.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 07"].SetDataSource(GetReportCotizacion(protocolId[7]));
                    rp.report8.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 08"].SetDataSource(GetReportCotizacion(protocolId[8]));
                    rp.report9.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 09"].SetDataSource(GetReportCotizacion(protocolId[9]));
                    rp.report10.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 10"].SetDataSource(GetReportCotizacion(protocolId[10]));
                    rp.report11.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 11"].SetDataSource(GetReportCotizacion(protocolId[11]));
                    rp.report12.SectionFormat.EnableSuppress = false;

                    rp.Subreports["crCotizacion.rpt - 12"].SetDataSource(GetReportCotizacion(protocolId[12]));
                    rp.report13.SectionFormat.EnableSuppress = false;
                    break;
                default:
                    break;
            }
        }

        private DataSet GetReportCotizacion(string pstrProtocolId)
        {
            var dataListForReport = new ServiceOrderBL().GetReportServiceOrder(_serviceOrderId, pstrProtocolId).FindAll(p => p.r_Price !=0);

            dsGetRepo = new DataSet();

            DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(dataListForReport);

            dt.TableName = "ServiceOrder";

            dsGetRepo.Tables.Add(dt);

            return dsGetRepo;


        }
    }
}
