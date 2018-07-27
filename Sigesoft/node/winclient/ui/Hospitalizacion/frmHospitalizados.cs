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

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmHospitalizados : Form
    {
        string strFilterExpression;
        List<HospitalizacionList> _objData = new List<HospitalizacionList>();
        HospitalizacionBL _objHospBL = new HospitalizacionBL();
        List<string> ListaComponentes = new List<string>();
        private string _ticketId;
        private int _rowIndexPc;
        private List<TicketList> _tempTicket = null;

        private TicketBL _ticketlBL = new TicketBL();

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

         
        }
        //
        private void BindGrid()
        {
            var objData = GetData(0, null, "v_HopitalizacionId ASC", strFilterExpression);
            grdData.DataSource = objData;
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            this.grdData.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;

            if (grdData.Rows.Count>0)
            {
                grdData.Rows[0].Selected = true;
                btnTicket.Enabled = true;
                btnEditarTicket.Enabled = true;
            }
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
            var ticketId = grdData.Selected.Rows[0].Cells["v_TicketId"].Value.ToString();
            //MessageBox.Show("Service: " + TserviceId);
            _ticketId = ticketId;
             frmTicket ticket = new frmTicket(_tempTicket, string.Empty, _ticketId, "Edit");
            ticket.ShowDialog();

            btnFilter_Click(sender, e);
            //grdData.DataSource = new List<TicketDetalleList>();
            //lblRecordCount.Text = "";
        }

        private void grd_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            //btnEditarTicket.Enabled = btnEliminarTicket.Enabled = (grdData.Selected.Rows.Count > 0);
            //if (grdData.Selected.Rows.Count == 0)
            //    return;


            //_rowIndexPc = ((Infragistics.Win.UltraWinGrid.UltraGrid)sender).Selected.Rows[0].Index;
            //_ticketId = grdData.Selected.Rows[0].Cells["v_TicketId"].Value.ToString();
            ////if (grdData.Selected.Rows.Count != 0)
            ////{
               
            //    float Total = 0;
            //    _ticketId = grdData.Selected.Rows[0].Cells["v_TicketId"].Value.ToString();

             
            //    // Cargar componentes de un protocolo seleccionado
            //    OperationResult objOperationResult = new OperationResult();

            //    var dataListPc = _objHospBL.BuscarTickets(ticketId);

            //    grdData.DataSource = dataListPc;

            //    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", dataListPc.Count());

            //    if (objOperationResult.Success != 1)
            //    {
            //        MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

    }
}
