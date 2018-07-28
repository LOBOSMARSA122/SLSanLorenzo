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
        public string _ticketId;
        private OperationResult _pobjOperationResult;
        private string _mode = null;
        private string _tickId = string.Empty;
        private int _rowIndexPc;
        private readonly HospitalizacionBL _objHospitalizacionBl;
        private readonly RecetaBl _objRecetaBl;
        
        private TicketBL _objTicketBl =  new TicketBL();
        private ticketDto objticketDto = null;
        private ticketDto objticketDtoo = null;

        ticketdetalleDto _oserviceorderDto = new ticketdetalleDto();
        
        private List<ticketdetalleDto> _ticketdetalleDTO = null;
        private List<ticketdetalleDto> _ticketdetalleDTODelete = null;
        private List<ticketdetalleDto> _ticketdetalleDTOUpdate = null;
        private List<TicketDetalleList> _tmpTicketDetalleList = null;

        

        private string ticketId = string.Empty;
        private string _ticketdetalletId = string.Empty;
        private readonly List<TicketList> _listTicketList;
        private readonly List<TicketDetalleList> _listTicketDetalleList;

        public frmTicket(List<TicketList> Lista, string IdControl, string id, string mode)
        {
            _tickId = id;
            _mode = mode;
            _serviceId = IdControl;
            //_ticketId = IdControl;
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
                    ticketId = _objTicketBl.AddTicket(ref _pobjOperationResult, objticketDto, _ticketdetalleDTO, Globals.ClientSession.GetAsList());
                    this.Close();
                }
                else
                {
                    this.Close();
                }

                if (!string.IsNullOrEmpty(_tickId))
                {
                    _mode = "Edit";
                    _tickId = txtNTicket.Text;
                }
            }
            else if(_mode == "Edit")
            {
                objticketDto.v_TicketId = _tickId;
                _ticketdetalleDTOUpdate = new List<ticketdetalleDto>();
                _ticketdetalleDTODelete = new List<ticketdetalleDto>();

                foreach (var item in _tmpTicketDetalleList)
                {
                    #region Add
                    if (item.i_RecordType == (int)RecordType.Temporal && item.i_RecordStatus == (int)RecordStatus.Agregado)
                    {
                        ticketdetalleDto ticketdetalleDtoAdd = new ticketdetalleDto();
                        ticketdetalleDtoAdd.v_TicketDetalleId = item.v_TicketDetalleId;
                        ticketdetalleDtoAdd.v_IdProductoDetalle = item.v_IdProductoDetalle;
                        ticketdetalleDtoAdd.d_Cantidad = item.d_Cantidad;
                        ticketdetalleDtoAdd.i_EsDespachado = item.i_EsDespachado;

                        _ticketdetalleDTO.Add(ticketdetalleDtoAdd);
                    }
                    #endregion

                    #region Upd
                    if (item.i_RecordType == (int)RecordType.NoTemporal && item.i_RecordStatus == (int)RecordStatus.Modificado)
                    {
                        ticketdetalleDto ticketdetalleDtoUp = new ticketdetalleDto();
                        ticketdetalleDtoUp.v_TicketDetalleId = item.v_TicketDetalleId;
                        ticketdetalleDtoUp.v_IdProductoDetalle = item.v_IdProductoDetalle;
                        ticketdetalleDtoUp.d_Cantidad = item.d_Cantidad;
                        ticketdetalleDtoUp.i_EsDespachado = item.i_EsDespachado;

                        _ticketdetalleDTOUpdate.Add(ticketdetalleDtoUp);
                    }
                    #endregion

                    #region Del
                    if (item.i_RecordType == (int)RecordType.NoTemporal && item.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                    {
                        ticketdetalleDto ticketdetalleDtoDel = new ticketdetalleDto();

                        ticketdetalleDtoDel.v_TicketDetalleId = item.v_TicketDetalleId;

                        _ticketdetalleDTODelete.Add(ticketdetalleDtoDel);
                    }
                    #endregion
                }
                
                DialogResult Result = MessageBox.Show("¿Desea Guardar Ticket Editado?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    _objTicketBl.UpdateTicket(ref _pobjOperationResult, 
                        objticketDto, 
                        _ticketdetalleDTO,
                        _ticketdetalleDTOUpdate.Count == 0 ? null : _ticketdetalleDTOUpdate,
                        _ticketdetalleDTODelete.Count == 0 ? null : _ticketdetalleDTODelete,
                        Globals.ClientSession.GetAsList());
                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
          
        }

        private void frmTicket_Load(object sender, EventArgs e)
        {
            LoadData();
            if (grdTicketDetalle.Rows.Count != 0)
                grdTicketDetalle.Rows[0].Selected = true;
            if (grdTicketDetalle.Rows.Count != 0)
                grdTicketDetalle.Rows[0].Selected = true;
        }

        private void LoadData()
        {
            this.grdTicketDetalle.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            if (_mode == "New")
            {
                int Year = DateTime.Now.Year;
                int Month = DateTime.Now.Month;
                int intNodeId = int.Parse(Globals.ClientSession.GetAsList()[2]);

                txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtNServicio.Text = _serviceId;

            }
            else if (_mode == "Edit")
            {
                objticketDtoo = _objTicketBl.GetTicket(ref _pobjOperationResult, _tickId);

                txtNTicket.Text = objticketDtoo.v_TicketId;
                txtFecha.Text = objticketDtoo.d_Fecha.ToString();
                txtNServicio.Text = objticketDtoo.v_ServiceId;

                var cargarGrup = _objTicketBl.GetTicketDetails(ref _pobjOperationResult, _tickId);

                grdTicketDetalle.DataSource = cargarGrup;

                _tmpTicketDetalleList = cargarGrup;

                if (_pobjOperationResult.Success != 1)
                {
                    MessageBox.Show("Error en operación:" + System.Environment.NewLine + _pobjOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            

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

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (_mode == "New")
            {
                var resultado = _tmpTicketDetalleList.Find(p => p.v_TicketDetalleId == _ticketdetalletId);
                _tmpTicketDetalleList.Remove(resultado);
            }
            else if (_mode == "Edit")
            {
                var findResult = _tmpTicketDetalleList.Find(p => p.v_TicketDetalleId == _ticketdetalletId);
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
            }

            var listanueva = _tmpTicketDetalleList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
            grdTicketDetalle.DataSource = new TicketDetalleList();
            grdTicketDetalle.DataSource = listanueva;
            grdTicketDetalle.Refresh();
            lblRecordCount2.Text = string.Format("Se encontraron {0} registros.", listanueva.Count());
        }
    }
}
