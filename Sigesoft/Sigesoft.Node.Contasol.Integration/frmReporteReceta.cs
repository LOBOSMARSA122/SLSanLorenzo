using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.Contasol.Integration;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;

namespace Sigesoft.Node.WinClient.UI.Reports
{
    public partial class frmReporteReceta : Form
    {
        private readonly string _serviceId;
        private OperationResult _objOperationResult;
        private List<recetadespachoDto> _dataReporte;
        private readonly string _recomendaciones;
        private readonly string _restricciones;

        public frmReporteReceta(string serviceId, string recomendaciones, string restricciones)
        {
            _serviceId = serviceId;
            _recomendaciones = recomendaciones;
            _restricciones = restricciones;
            InitializeComponent();
            _objOperationResult = new OperationResult();
        }

        private void frmReporteReceta_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            var objRecetaBl = new RecetaBl();
            try
            {
                Task.Factory.StartNew(() =>
                {
                    _dataReporte = objRecetaBl.GetRecetaToReport(ref _objOperationResult, _serviceId);

                }, TaskCreationOptions.LongRunning).ContinueWith(t =>
                {
                    if (_objOperationResult.Success == 0)
                    {
                        MessageBox.Show(_objOperationResult.ErrorMessage, @"Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                    var rp = new crRecetaPresentacion();
                    var ds = new DataSet();
                    var dsReport = BLL.Utils.ConvertToDatatable(_dataReporte);
                    ds.Tables.Add(dsReport);
                    ds.Tables[0].TableName = "dsReporteReceta";
                    rp.SetDataSource(dsReport);
                    rp.SetParameterValue("_Recomendaciones", _recomendaciones);
                    rp.SetParameterValue("_Restricciones", _restricciones);
                    crystalReportViewer1.ReportSource = rp;
                },
                TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
