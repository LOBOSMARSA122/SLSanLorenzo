using Sigesoft.Node.WinClient.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.UI.Reports;
using Sigesoft.Node.Contasol.Integration;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmTicket : Form
    {
        public  string _serviceId;
        private OperationResult _pobjOperationResult;
        private readonly HospitalizacionBL _objHospitalizacionBl;
        private readonly RecetaBl _objRecetaBl;
        private readonly TicketBL _objTicketBl =  new TicketBL();

        ticketdetalleDto _oserviceorderDto = new ticketdetalleDto();

        private List<TicketDetalleList> _tmpTicketDetalleList = null;

        ticketDto objticketDto;

        string ticketId;
        //private readonly List<HospitalizacionList> _listHospitalizacionList;
        //private readonly List<HospitalizacionServiceList> _listHospitalizacionServiceList;
        private readonly List<TicketList> _listTicketList;
        private readonly List<TicketDetalleList> _listTicketDetalleList;

        public frmTicket(List<TicketList> Lista, string ServiceId)
        {
            _serviceId = ServiceId;
            InitializeComponent();
            _pobjOperationResult = new OperationResult();
            _objHospitalizacionBl = new HospitalizacionBL();
            _objRecetaBl = new RecetaBl();
            _objTicketBl = new TicketBL();
            _listTicketList = Lista;
            _listTicketDetalleList = new List<TicketDetalleList>();
        }

        private void btnCancelarTicket_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardarTicket_Click(object sender, EventArgs e)
        {
            objticketDto = new ticketDto();
            //objticketDto.v_TicketId 
            txtNServicio.Text = _serviceId;
            objticketDto.v_ServiceId = txtNServicio.Text;
            objticketDto.d_Fecha = DateTime.Parse(txtFecha.Text);

            DialogResult Result = MessageBox.Show("¿Desea Guardar Ticket?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                ticketId = _objTicketBl.AddTicket(ref _pobjOperationResult, objticketDto, Globals.ClientSession.GetAsList());
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void frmTicket_Load(object sender, EventArgs e)
        {
            int Year = DateTime.Now.Year;
            int Month = DateTime.Now.Month;
            int intNodeId = int.Parse(Globals.ClientSession.GetAsList()[2]);

            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtNServicio.Text = _serviceId;
        }

        private void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            //var TicketId = ticketId;
            var frm = new frmAddProducto();
            if (_tmpTicketDetalleList != null)
            {
                frm._TempTicketDetalleList = _tmpTicketDetalleList;
            }
            frm.ShowDialog();

            if (frm._TempTicketDetalleList != null)
            {
                _tmpTicketDetalleList = frm._TempTicketDetalleList;

                var dataList = _tmpTicketDetalleList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdTicketDetalle.DataSource = new TicketDetalleList();
                grdTicketDetalle.DataSource = dataList;
                grdTicketDetalle.Refresh();
                //lblRecordCount2.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }   
        }
    }
}
