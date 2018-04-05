using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid.ExcelExport;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmBeforeLiquidationProcess : Form
    {
        ServiceBL _serviceBL = new ServiceBL();
        ServiceList _service = null;

        public frmBeforeLiquidationProcess(ServiceList service)
        {
            InitializeComponent();
            _service = service;
        }

        public frmBeforeLiquidationProcess()
        {
            InitializeComponent();
        }

        private void frmBeforeLiquidationProcess_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();         
            Utils.LoadDropDownList(cbEstadoLiquidacion, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 222, null));
            
            // Set estado x defecto
            if (_service.i_StatusLiquidation == null)
                cbEstadoLiquidacion.SelectedValue = ((int)PreLiquidationStatus.Pendiente).ToString();
            else
                cbEstadoLiquidacion.SelectedValue = _service.i_StatusLiquidation.Value.ToString();

            var preLiquidation = _serviceBL.GetServiceComponentsPreLiquidation(ref objOperationResult, _service.v_ServiceId);

            // Mostrar datos en las cajas de texto
            txtNombreTrabajador.Text = _service.v_Pacient;
            txtEmpresaCliente.Text = _service.v_CustomerOrganizationName;
            txtProtocolo.Text = _service.v_ProtocolName;
            txtTipoServicio.Text = _service.v_MasterServiceName;
            txtTipoESO.Text = _service.v_EsoTypeName;

            // Cargar grilla
            grdLiquidacion.DataSource = preLiquidation;

            // Activar boton de exportar
            btnExportExcel.Enabled = (grdLiquidacion.Rows.Count > 0);
       

        }

        private void grdLiquidacion_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            e.Layout.Bands[0].Columns["r_Price"].Format = "#,##0.00";
           
            // Sumarizado

            UltraGridColumn columnToSummarize = e.Layout.Bands[0].Columns["r_Price"];
            SummarySettings summary = e.Layout.Bands[0].Summaries.Add("Total", SummaryType.Sum, columnToSummarize);
            summary.DisplayFormat = "{0:C}";
            summary.Appearance.TextHAlign = HAlign.Right;
        
            summary.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed
                | SummaryDisplayAreas.GroupByRowsFooter;

            e.Layout.Override.GroupBySummaryValueAppearance.BackColor = SystemColors.Window;
            e.Layout.Override.GroupBySummaryValueAppearance.TextHAlign = HAlign.Right;

            //e.Layout.Bands[0].SummaryFooterCaption = "Total:";
            e.Layout.Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;        
            e.Layout.Override.SummaryFooterSpacingAfter = 5;
            e.Layout.Override.SummaryFooterSpacingBefore = 5;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            UltraGridExcelExporter ultraGridExcelExporter1 = new UltraGridExcelExporter();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.FileName = string.Format("{0} Pre-Liquidación de Atención", _service.v_Pacient);
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ultraGridExcelExporter1.Export(this.grdLiquidacion, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }       
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            var statusLiquidation = Convert.ToInt32(cbEstadoLiquidacion.SelectedValue);
            _serviceBL.UpdateStatusPreLiquidation(ref objOperationResult, statusLiquidation, _service.v_ServiceId, Globals.ClientSession.GetAsList());

            //// Analizar el resultado de la operación
            if (objOperationResult.Success == 0)  
            {
                MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.OK;
           
        }
    }
}
