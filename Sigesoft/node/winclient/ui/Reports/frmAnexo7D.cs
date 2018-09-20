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
    public partial class frmAnexo7D : Form
    {
        private string _serviceId;
        private string _componentId;
        public frmAnexo7D(string pstrServiceId, string pstrComponetId)
        {
            _serviceId = pstrServiceId;
            _componentId = pstrComponetId;
            InitializeComponent();
        }

        private void frmAnexo7D_Load(object sender, EventArgs e)
        {
            ShowReport();
        }

        private void ShowReport()
        {
            try
            {
                OperationResult objOperationResult = new OperationResult();

                var rp = new Reports.crAnexo7D();

                //var AscensoAlturas = new ServiceBL().ReportAscensoGrandesAlturas(_serviceId, Constants.ALTURA_7D_ID);
                //var FuncionesVitales = new ServiceBL().ReportFuncionesVitales(_serviceId, Constants.FUNCIONES_VITALES_ID);
                //var Antropometria = new ServiceBL().ReportAntropometria(_serviceId, Constants.ANTROPOMETRIA_ID);

                //DataSet ds1 = new DataSet();
                //DataTable dt = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(AscensoAlturas);
                //dt.TableName = "dtAnexo7D";
                //ds1.Tables.Add(dt);

                //DataTable dt1 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(FuncionesVitales);
                //dt1.TableName = "dtFuncionesVitales";
                //ds1.Tables.Add(dt1);

                //DataTable dt2 = Sigesoft.Node.WinClient.BLL.Utils.ConvertToDatatable(Antropometria);
                //dt2.TableName = "dtAntropometria";
                //ds1.Tables.Add(dt2);
                
                //rp.SetDataSource(ds1);

                //crystalReportViewer2.ReportSource = rp;
                //crystalReportViewer2.Show();


           

            }
            catch (Exception)
            {

                throw;
            }


        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
