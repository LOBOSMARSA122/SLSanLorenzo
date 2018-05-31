using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class frmPopupServiciosAnteriores : Form
    {
        private string _personId;
        private string _serviceId;
        private string _serviceIdByWiewServiceHistory;
        private ServiceBL _serviceBL = new ServiceBL();
        public frmPopupServiciosAnteriores(string pstrPersonId, string pstrServiceId)
        {
            _personId = pstrPersonId;
            _serviceId = pstrServiceId;
            InitializeComponent();
        }

        private void frmPopupServiciosAnteriores_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            var services = _serviceBL.GetServicesConsolidateForService(ref objOperationResult, _personId, _serviceId);

            grdServiciosAnteriores.DataSource = services;

            // Analizar el resultado de la operación
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVerServicioAnterior_Click(object sender, EventArgs e)
        {

            var frm = new Operations.frmEso(_serviceIdByWiewServiceHistory, null, "View", (int)MasterService.Eso);
            frm.ShowDialog();
        }

        private void grdServiciosAnteriores_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    grdServiciosAnteriores.Rows[row.Index].Selected = true;
                    _serviceIdByWiewServiceHistory = grdServiciosAnteriores.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
                    //cmVerServicioAnterior.Items["mnuVerServicio"].Enabled = true;
                }
                else
                {
                    //cmVerServicioAnterior.Items["mnuVerServicio"].Enabled = false;

                }

            }
        }

        private void grdServiciosAnteriores_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            btnVerServicioAnterior.Enabled = (grdServiciosAnteriores.Selected.Rows.Count > 0);

            if (grdServiciosAnteriores.Selected.Rows.Count == 0)
                return;

            _serviceIdByWiewServiceHistory = grdServiciosAnteriores.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();

        }

        private void btnDescargarServicio_Click(object sender, EventArgs e)
        {
            string origen = Common.Utils.GetApplicationConfigValue("rutaReportes").ToString() + _serviceIdByWiewServiceHistory + ".pdf";
            string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string destino = mdoc + "/" + _serviceIdByWiewServiceHistory + ".pdf";

            if (File.Exists(destino))
            {
                System.IO.File.Delete(destino);
                File.Copy(origen, destino);
            }
            else { File.Copy(origen, destino); }

            MessageBox.Show("El archivo se grabó correctamente en la carpeta MIS DOCUMENTOS.", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }
    }
}
