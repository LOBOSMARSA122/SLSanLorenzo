using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.BE;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid.DocumentExport;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Sigesoft.Node.Contasol.Integration;
using NetPdf;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmHospitalizados : Form
    {
        string strFilterExpression;
        List<HospitalizacionList> _objData = new List<HospitalizacionList>();
        HospitalizacionBL _objHospBL = new HospitalizacionBL();
        List<string> ListaComponentes = new List<string>();
        private string _ticketId;
        private List<TicketList> _tempTicket = null;
        private TicketBL _ticketlBL = new TicketBL();

        private ServiceBL _serviceBL = new ServiceBL();
        private PacientBL _pacientBL = new PacientBL();
        private OperationResult _objOperationResult = new OperationResult();
        private List<PersonList> personalList;
        private List<HospitalizacionList> hospitalizacionlList;
        private List<HospitalizacionServiceList> hospitalizacionServicelList;
        private List<TicketList> ticketlList;
        private List<TicketDetalleList> ticketdetallelList;
        string _serviceId;
        string _EmpresaClienteId;
        string _pacientId;
        string _customerOrganizationName;
        string _personFullName;
        string ruta;
        int _edad;

        public frmHospitalizados()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtPacient.Text)) Filters.Add("v_Paciente.Contains(\"" + txtPacient.Text.Trim() + "\")");

            Filters.Add("i_IsDeleted==0");
            strFilterExpression = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    strFilterExpression = strFilterExpression + item + " && ";
                }
                strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
            }

           this.BindGrid();
           btnTicket.Enabled = false;
           btnAgregarExamenes.Enabled = false;
           btnAsignarHabitacion.Enabled = false;

         
        }
        //
        private void BindGrid()
        {
            var objData = GetData(0, null, "v_HopitalizacionId ASC", strFilterExpression);
            grdData.DataSource = objData;
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            this.grdData.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
        }

        private void grdData_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            this.BindGrid();
        }

        private List<HospitalizacionList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            _objData = _objHospBL.GetHospitalizacionPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, pdatBeginDate, pdatEndDate);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void btnTicket_Click(object sender, EventArgs e)
        {
            var ServiceId = grdData.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
            //MessageBox.Show("Service: " + TserviceId);
            frmTicket ticket = new frmTicket(_tempTicket, ServiceId, string.Empty, "New");
            ticket.ShowDialog();
            btnFilter_Click(sender, e);
            btnTicket.Enabled = false;
        }

        private void txtHospitalizados_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void calendar1Hospitalizados_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void calendar2Hospitalizados_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void btnEditarTicket_Click(object sender, EventArgs e)
        {
            var ServiceId = grdData.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
            var ticketId = grdData.Selected.Rows[0].Cells["v_TicketId"].Value.ToString();
            _ticketId = ticketId;
            frmTicket ticket = new frmTicket(_tempTicket, ServiceId, _ticketId, "Edit");
            ticket.ShowDialog();

            btnFilter_Click(sender, e);
        }

        private void grd_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            foreach (UltraGridRow rowSelected in this.grdData.Selected.Rows)
            {
                if (rowSelected.Band.Index.ToString() == "1" || rowSelected.Band.Index.ToString() == "2" || rowSelected.Band.Index.ToString() == "3" || rowSelected.Band.Index.ToString() == "4" || rowSelected.Band.Index.ToString() == "5")
                {
                    btnAsignarHabitacion.Enabled = false;
                    btnEditarHabitacion.Enabled = false;
                    //btnReportePDF.Enabled = false;
                }
                else
                {
                    btnAsignarHabitacion.Enabled = true;
                    //btnReportePDF.Enabled = true;
                }

                if (rowSelected.Band.Index.ToString() == "0" || rowSelected.Band.Index.ToString() == "2" || rowSelected.Band.Index.ToString() == "3" || rowSelected.Band.Index.ToString() == "4" || rowSelected.Band.Index.ToString() == "5")
                {
                    btnTicket.Enabled = false;
                    btnAgregarExamenes.Enabled = false;
                    btnEditarHabitacion.Enabled = false;
                    btnReportePDF.Enabled = false;
                }
                else
                {
                    btnTicket.Enabled = true;
                    btnAgregarExamenes.Enabled = true;
                    btnReportePDF.Enabled = true;
                    btnEditarHabitacion.Enabled = false;
                    var serviceId = grdData.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
                    OperationResult pobjOperationResult = new OperationResult();
                    ServiceBL oServiceBL = new ServiceBL();
                    var componentes = oServiceBL.GetServiceComponents_(ref pobjOperationResult, serviceId);
                    
                    ListaComponentes = new List<string>();
                    foreach (var item in componentes)
                    {
                        ListaComponentes.Add(item.v_ComponentId);
                    }

                }

                if (rowSelected.Band.Index.ToString() == "0" || rowSelected.Band.Index.ToString() == "1" || rowSelected.Band.Index.ToString() == "3" || rowSelected.Band.Index.ToString() == "4" || rowSelected.Band.Index.ToString() == "5")
                {
                    btnEditarTicket.Enabled = false;
                    btnEliminarTicket.Enabled = false;
                }
                else
                {
                    btnEditarTicket.Enabled = true;
                    btnEliminarTicket.Enabled = true;
                }

                if (rowSelected.Band.Index.ToString() == "0" || rowSelected.Band.Index.ToString() == "1" || rowSelected.Band.Index.ToString() == "2" || rowSelected.Band.Index.ToString() == "3" || rowSelected.Band.Index.ToString() == "4")
                {
                    btnEditarHabitacion.Enabled = false;
                }
                else
                {
                    btnEditarHabitacion.Enabled = true;
                }
            }
            
            if (grdData.Selected.Rows.Count == 0)
                 return;

            btnExport.Enabled = grdData.Rows.Count > 0;
        }

        private void btnAsignarHabitacion_Click(object sender, EventArgs e)
        {
            var hospitalizacionId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();
            frmHabitacion frm = new frmHabitacion(hospitalizacionId, "New", "");
            frm.ShowDialog();
            btnFilter_Click(sender, e);
            btnAsignarHabitacion.Enabled = false;

        }

        private void btnEditarHabitacion_Click(object sender, EventArgs e)
        {
            var hospitalizacionId = grdData.Selected.Rows[0].Cells["v_HopitalizacionId"].Value.ToString();
            var hospitalizacionHabitacionId = grdData.Selected.Rows[0].Cells["v_HospitalizacionHabitacionId"].Value.ToString();
            frmHabitacion frm = new frmHabitacion(hospitalizacionId, "Edit", hospitalizacionHabitacionId);
            frm.ShowDialog();
            btnFilter_Click(sender, e);
            btnEditarHabitacion.Enabled = false;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string NombreArchivo = "";
            NombreArchivo = "Reporte Hospitalización del " + dtpDateTimeStar.Text + " al " + dptDateTimeEnd.Text;
            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdData, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAgregarExamenes_Click(object sender, EventArgs e)
        {
            var serviceId = grdData.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
            var frm = new frmAddExam(ListaComponentes, "HOSPI") {_serviceId = serviceId};
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;
        }

        private void btnReportePDF_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("¿Desea publicar a la WEB?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            saveFileDialog1.FileName = "Liquidacion";
            saveFileDialog1.Filter = "Files (*.pdf;)|*.pdf;";
            var serviceId = grdData.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();
            using (new LoadingClass.PleaseWait(this.Location, "Generando..."))
            {
                this.Enabled = false;
                var _DataService = _serviceBL.GetServiceReport(serviceId);
                var datosP = _pacientBL.DevolverDatosPaciente(serviceId);
                var MedicalCenter = _serviceBL.GetInfoMedicalCenter();

                string ruta = Common.Utils.GetApplicationConfigValue("rutaLiquidacion").ToString();

                Liquidacion_Hospitalizacion.CreateLiquidacion(ruta + "Hola"+ ".pdf",_DataService, datosP, MedicalCenter);
                this.Enabled = true;
            }
        }

    }
}
