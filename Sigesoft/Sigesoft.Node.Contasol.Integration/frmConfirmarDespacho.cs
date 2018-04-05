using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sigesoft.Node.Contasol.Integration
{
    public partial class frmConfirmarDespacho : Form
    {
        private readonly string _serviceId;
        private OperationResult _objOperationResult;
        private List<recetadespachoDto> _dataReporte;
        public frmConfirmarDespacho(string serviceId)
        {
            InitializeComponent();
            _serviceId = serviceId;
        }

        private void frmConfirmarDespacho_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            _objOperationResult = new OperationResult();
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
                    ultraGrid1.DataSource = _dataReporte;
                },
                TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            try
            {
                _objOperationResult = new OperationResult();
                var objRecetaBl = new RecetaBl();
                var data = (List<recetadespachoDto>)ultraGrid1.DataSource;
                objRecetaBl.UpdateDespacho(ref _objOperationResult, data);
                if (_objOperationResult.Success == 0)
                {
                    MessageBox.Show(_objOperationResult.ErrorMessage, @"Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
