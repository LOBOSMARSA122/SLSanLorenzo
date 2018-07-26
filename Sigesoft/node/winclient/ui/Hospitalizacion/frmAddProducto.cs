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
        private OperationResult _pobjOperationResult;
        private ticketdetalleDto _ticketdetalleDto;

        public List<TicketDetalleList> _TempTicketDetalleList = null;
        TicketDetalleList _objTicketDetalleList = null;
        
        string _ProductoId = null;
        //string TicketIdpublic string _TicketId;

        public frmAddProducto()
        {
            //_TicketId = TicketId;
            InitializeComponent();
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
                txtMedicamento.Tag = medicamento.IdProductoDetalle;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                _pobjOperationResult = new OperationResult();
                if (txtMedicamento.Tag == null)
                {
                    MessageBox.Show(@"Por favor seleccione un medicamento", @"Error de validación", MessageBoxButtons.OK);
                    txtMedicamento.Focus();
                    return;
                }

                if (_TempTicketDetalleList == null)
                {
                    _TempTicketDetalleList = new List<TicketDetalleList>();
                }

                OperationResult objOperationResult = new OperationResult();
                //string[] componentIdFrom = _TempTicketDetalleList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico)
                //                                                              .Select(p => p.v_TicketId).ToArray();

            // Verificar si el componente actual a agregar tiene campos ya existentes en el mismo protocolo.
            //bool IsExists = _protocolBL.IsExistscomponentfieldsInCurrentProtocol(ref objOperationResult, componentIdFromProtocol, _componentId);

            //if (IsExists)
            //{
            //    var msj = string.Format("El examen ({0}) no se puede agregar porqué tiene un campo que se repite en otro componente del mismo protocolo.", lblExamenSeleccionado.Text);
            //    MessageBox.Show(msj, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

                var findResult = _TempTicketDetalleList.Find(p => p.v_IdProductoDetalle == _ProductoId);
                _objTicketDetalleList = new TicketDetalleList();
                decimal d;
                if (findResult == null)
                {

                    //_objTicketDetalleList.v_TicketDetalleId = Guid.NewGuid().ToString();
                    //_objTicketDetalleList.v_TicketId = _ticketId;

                    _objTicketDetalleList.v_IdProductoDetalle = txtMedicamento.Tag.ToString();
                    _objTicketDetalleList.v_NombreProducto = txtMedicamento.Text;
                    _objTicketDetalleList.d_Cantidad = decimal.TryParse(txtCantidad.Text, out d) ? d : 0;
                    //_objTicketDetalleList.i_EsDespachado = int.Parse()
                    _objTicketDetalleList.i_RecordType = (int)RecordType.Temporal;

                    _TempTicketDetalleList.Add(_objTicketDetalleList);
                }
                else    // El examen ya esta agregado en la bolsa hay que actualizar su estado
                {
                    if (findResult.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                    {
                        if (findResult.i_RecordType == (int)RecordType.NoTemporal)   // El registro Tiene in ID de BD
                        {

                            _objTicketDetalleList.v_IdProductoDetalle = txtMedicamento.Tag.ToString();
                            _objTicketDetalleList.v_NombreProducto = txtMedicamento.Text;
                            _objTicketDetalleList.d_Cantidad = decimal.TryParse(txtCantidad.Text, out d) ? d : 0;

                            findResult.i_RecordStatus = (int)RecordStatus.Grabado;
                        }
                        else if (findResult.i_RecordType == (int)RecordType.Temporal)   // El registro tiene un ID temporal [GUID]
                        {
                            _objTicketDetalleList.v_IdProductoDetalle = txtMedicamento.Tag.ToString();
                            _objTicketDetalleList.v_NombreProducto = txtMedicamento.Text;
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

                MessageBox.Show("Se grabo correctamente.", "INFORMACION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                //if (_pobjOperationResult.Success == 0)
                //{
                //    MessageBox.Show(_pobjOperationResult.ErrorMessage, @"Error", MessageBoxButtons.OK,
                //    MessageBoxIcon.Error);
                //    return;
                //}

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"btnGuardar_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
