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
        private string _mode = null;
        private string _tickId = string.Empty;
        private int _rowIndexPc;

        private readonly HospitalizacionBL _objHospitalizacionBl;
        private readonly RecetaBl _objRecetaBl;
        private readonly TicketBL _objTicketBl =  new TicketBL();

        ticketdetalleDto _oserviceorderDto = new ticketdetalleDto();

        private List<TicketDetalleList> _tmpTicketDetalleList = null;

        private List<ticketdetalleDto> _ticketdetalleDTO = null;

        ticketDto objticketDto = null;

        private string ticketId = string.Empty;
        private string _ticketdetalletId = string.Empty;
        //private readonly List<HospitalizacionList> _listHospitalizacionList;
        //private readonly List<HospitalizacionServiceList> _listHospitalizacionServiceList;
        private readonly List<TicketList> _listTicketList;
        private readonly List<TicketDetalleList> _listTicketDetalleList;

        public frmTicket(List<TicketList> Lista, string ServiceId, string id, string mode)
        {
            _tickId = id;
            _mode = mode;
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
            _ticketdetalleDTO = new List<ticketdetalleDto>();
            if (objticketDto == null)
            {
                 objticketDto = new ticketDto();
            }
           
            
            //objticketDto.v_TicketId 
            txtNServicio.Text = _serviceId;
            objticketDto.v_ServiceId = txtNServicio.Text;
            objticketDto.d_Fecha = DateTime.Parse(txtFecha.Text);
            if (_mode == "New")
            { 
            
            }
            foreach (var item in _tmpTicketDetalleList)
            {
                ticketdetalleDto ticketDetalle = new ticketdetalleDto();

                ticketDetalle.v_IdProductoDetalle = item.v_IdProductoDetalle;
                ticketDetalle.d_Cantidad = item.d_Cantidad;
                ticketDetalle.i_EsDespachado = item.i_EsDespachado;

                _ticketdetalleDTO.Add(ticketDetalle);
            }

            DialogResult Result = MessageBox.Show("¿Desea Guardar Ticket?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                ticketId = _objTicketBl.AddTicket(ref _pobjOperationResult, objticketDto,_ticketdetalleDTO, Globals.ClientSession.GetAsList());
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

            this.grdTicketDetalle.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;

            if (grdTicketDetalle.Rows.Count != 0)
                grdTicketDetalle.Rows[0].Selected = true;
            if (grdTicketDetalle.Rows.Count != 0)
                grdTicketDetalle.Rows[0].Selected = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var nuevo = new frmAddProducto(string.Empty, "New");
            if (_tmpTicketDetalleList != null)
            {
                nuevo._TempTicketDetalleList = _tmpTicketDetalleList;
            }
            nuevo.ShowDialog();
            this.grdTicketDetalle.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            if (nuevo._TempTicketDetalleList != null)
            {
                _tmpTicketDetalleList = nuevo._TempTicketDetalleList;

                var dataList = _tmpTicketDetalleList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdTicketDetalle.DataSource = new TicketDetalleList();
                grdTicketDetalle.DataSource = dataList;
                grdTicketDetalle.Refresh();
                lblRecordCount2.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }   
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var editar = new frmAddProducto(_ticketdetalletId, "Edit");
            if (_tmpTicketDetalleList != null)
            {
                editar._TempTicketDetalleList = _tmpTicketDetalleList;
            }
            editar.ShowDialog();
            if (editar._TempTicketDetalleList != null)
            {
                _tmpTicketDetalleList = editar._TempTicketDetalleList;

                var dataList = _tmpTicketDetalleList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdTicketDetalle.DataSource = new TicketDetalleList();
                grdTicketDetalle.DataSource = dataList;
                grdTicketDetalle.Refresh();
                lblRecordCount2.Text = string.Format("Se encontraron {0} registros.", dataList.Count());

            }
        }

        private void grdproduc_AfterSelectChange(object sender, MouseEventArgs e)
        {
            btnEditar.Enabled = btnRemover.Enabled = (grdTicketDetalle.Selected.Rows.Count > 0);
            if (grdTicketDetalle.Selected.Rows.Count == 0)
                return;

            _rowIndexPc = ((Infragistics.Win.UltraWinGrid.UltraGrid)sender).Selected.Rows[0].Index;
            _ticketdetalletId = grdTicketDetalle.Selected.Rows[0].Cells["v_TicketDetalleId"].Value.ToString();
        }
    }
}
