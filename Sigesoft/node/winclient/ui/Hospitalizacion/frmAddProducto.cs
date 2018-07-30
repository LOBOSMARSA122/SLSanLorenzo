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
using Sigesoft.Node.WinClient.UI.Operations.Popups;
namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmAddProducto : Form
    {
        private string _mode = null;
        private string _id = string.Empty;

        public List<TicketDetalleList> _TempTicketDetalleList = null;
        TicketDetalleList _objTicketDetalleList = null;

        private TicketBL _ticketlBL = new TicketBL();
        string _ProductoId = null;

        public frmAddProducto(string id, string mode)
        {
            InitializeComponent();
            _id = id;
            _mode = mode;
        }
        
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var f = new frmSearchMedicamento();
            var result = f.ShowDialog();
            if (result == DialogResult.OK)
            {
                var medicamento = f.MedicamentoSeleccionado;
                if (medicamento == null) return;
                txtMedicamento.Text = medicamento.NombreCompleto;
                txtCodigo.Text = medicamento.CodInterno;
                txtMedicamento.Tag = medicamento.IdProductoDetalle;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
                           
                if (_TempTicketDetalleList == null)
                {
                    _TempTicketDetalleList = new List<TicketDetalleList>();
                }
                OperationResult objOperationResult = new OperationResult();

                string[] componentIdFromProtocol = _TempTicketDetalleList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico)
                                                                                       .Select(p => p.v_IdProductoDetalle).ToArray();

                bool IsExists = _ticketlBL.IsExistsproductoInTicket(ref objOperationResult, componentIdFromProtocol, _ProductoId);

                if (IsExists)
                {
                    var msj = string.Format("El examen producto puede agregar, ya existe", labelmensaje.Text);
                    MessageBox.Show(msj, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_mode == "New")
                {
                    if (txtMedicamento.Tag == null)
                    {
                        MessageBox.Show(@"Por favor seleccione un medicamento", @"Error de validación", MessageBoxButtons.OK);
                        txtMedicamento.Focus();
                        return;
                    }

                    var findResult = _TempTicketDetalleList.Find(p => p.v_IdProductoDetalle == _ProductoId);
                    _objTicketDetalleList = new TicketDetalleList();

                    if (findResult == null)
                    {
                        _objTicketDetalleList.v_TicketDetalleId = Guid.NewGuid().ToString();

                        _objTicketDetalleList.v_IdProductoDetalle = txtMedicamento.Tag.ToString();
                        _objTicketDetalleList.v_NombreProducto = txtMedicamento.Text;
                        _objTicketDetalleList.v_CodInterno = txtCodigo.Text;
                        decimal d;
                        _objTicketDetalleList.d_Cantidad = decimal.TryParse(txtCantidad.Text, out d) ? d : 0;
                        //objTicketDetalleList.i_EsDespachado = int.Parse()
                        _objTicketDetalleList.i_RecordStatus = (int)RecordStatus.Agregado;
                        _objTicketDetalleList.i_RecordType = (int)RecordType.Temporal;

                        _TempTicketDetalleList.Add(_objTicketDetalleList);
                    }
                    else
                    {
                        if (findResult.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                        {
                            if (findResult.i_RecordType == (int)RecordType.NoTemporal)
                            {
                                _objTicketDetalleList.v_IdProductoDetalle = txtMedicamento.Tag.ToString();
                                _objTicketDetalleList.v_NombreProducto = txtMedicamento.Text;
                                _objTicketDetalleList.v_CodInterno = txtCodigo.Text;
                                decimal d;
                                _objTicketDetalleList.d_Cantidad = decimal.TryParse(txtCantidad.Text, out d) ? d : 0;

                                findResult.i_RecordStatus = (int)RecordStatus.Grabado;
                            }
                            else if (findResult.i_RecordType == (int)RecordType.Temporal)   // El registro tiene un ID temporal [GUID]
                            {
                                _objTicketDetalleList.v_IdProductoDetalle = txtMedicamento.Tag.ToString();
                                _objTicketDetalleList.v_NombreProducto = txtMedicamento.Text;
                                _objTicketDetalleList.v_CodInterno = txtCodigo.Text;
                                decimal d;
                                _objTicketDetalleList.d_Cantidad = decimal.TryParse(txtCantidad.Text, out d) ? d : 0;
                                _objTicketDetalleList.i_RecordType = (int)RecordType.Temporal;

                                findResult.i_RecordStatus = (int)RecordStatus.Agregado;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Por favor seleccione otro medicamento. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                else if (_mode == "Edit")   
                {
                     var result = _TempTicketDetalleList.Find(p => p.v_TicketDetalleId == _id);
                     decimal d;
                     result.d_Cantidad = decimal.TryParse(txtCantidad.Text, out d) ? d : 0;
                        result.v_CodInterno = txtCodigo.Text;
                     result.i_RecordStatus = (int)RecordStatus.Modificado;
                }
                MessageBox.Show("Se grabo correctamente.", "INFORMACION!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddProducto_Load(object sender, EventArgs e)
        {
            if (_mode == "Edit")
            {
                btnBuscar.Visible = false;
                txtMedicamento.Enabled = false;
                var findResult = _TempTicketDetalleList.Find(p => p.v_TicketDetalleId == _id);
                txtMedicamento.Text = findResult.v_NombreProducto;
                txtCodigo.Text = findResult.v_CodInterno;
                txtCantidad.Value = findResult.d_Cantidad;
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnGuardar_Click(null, null);
            }
        }
    }
}
