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
using System.Data.SqlClient;
using Sigesoft.Node.WinClient.UI.Configuration;
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
        string _serviceId;
        private string _protocolId;
        private string _modoMasterService;

        public frmAddProducto(string id, string mode, string serviceId, string protocolId, string modoMasterService)
        {
            InitializeComponent();
            _id = id;
            _mode = mode;
            _serviceId = serviceId;
            _protocolId = protocolId;
            _modoMasterService = modoMasterService;
        }
        
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var f = new frmSearchMedicamento();
            var result = f.ShowDialog();
            #region Conexion SIGESOFT Obtener Porcentaje de descuento EPS
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena = "select OO.r_FactorMed from protocol PR inner join organization OO on PR.v_CustomerOrganizationId = OO.v_OrganizationId where PR.v_ProtocolId='" + _protocolId + "'";
            SqlCommand comando = new SqlCommand(cadena, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            string EPS = "";
            while (lector.Read())
            {
                EPS = lector.GetValue(0).ToString();
            }
            lector.Close();
            conectasam.closesigesoft();
            #endregion
            if (result == DialogResult.OK)
            {
                var medicamento = f.MedicamentoSeleccionado;
                if (medicamento == null) return;
                txtMedicamento.Text = medicamento.NombreCompleto;
                txtCodigo.Text = medicamento.CodInterno;
                txtMedicamento.Tag = medicamento.IdProductoDetalle;
                //Nuevo Precio con descuento EPS
                txtPrecioVenta.Text = (medicamento.d_PrecioMayorista - (medicamento.d_PrecioMayorista * decimal.Parse(EPS) / 100)).ToString();
                txtUnidadProductiva.Text = medicamento.IdLinea;
                txtPrecio.Text = medicamento.PrecioVenta.ToString();
                txtPPS.Text = medicamento.d_PrecioMayorista.ToString();
                txtDesctoEPS.Text = EPS;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            TicketBL oTicketBL = new TicketBL();
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
                    // || cbExamen.Text == ""
                    if (txtMedicamento.Tag == null)
                    {
                        string mensage = "";
                        if (txtMedicamento.Tag == null){mensage = @"Por favor seleccione un medicamento";}
                        //else if (cbExamen.Text == ""){mensage = @"Por favor seleccione un examen";}
                        MessageBox.Show(mensage, @"Error de validación", MessageBoxButtons.OK);
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
                        _objTicketDetalleList.v_IdUnidadProductiva = txtUnidadProductiva.Text;
                        var precioTarifa = oTicketBL.ObtenerPrecioTarifario(_serviceId, _objTicketDetalleList.v_IdProductoDetalle);
                        

                        decimal d;
                        _objTicketDetalleList.d_Cantidad = decimal.TryParse(txtCantidad.Text, out d) ? d : 0;

                        var tienePlan = false;
                        var resultplan = oTicketBL.TienePlan(_protocolId, txtUnPdId.Text);
                        if (resultplan.Count > 0) tienePlan = true;
                        else tienePlan = false;

                        if (tienePlan)
                        {
                            if (resultplan[0].i_EsCoaseguro == 1)
                            {
                                #region Conexion SIGESOFT verificar la unidad productiva del componente
                                ConexionSigesoft conectasam = new ConexionSigesoft();
                                conectasam.opensigesoft();
                                var cadena1 = "select PL.d_ImporteCo " +
                                              "from [dbo].[plan] PL " +
                                              "inner join protocol PR on PL.v_ProtocoloId=PR.v_ProtocolId " +
                                              "where PR.v_ProtocolId='"+_protocolId+"' and PL.v_IdUnidadProductiva='"+txtUnPdId.Text+"'";
                                SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                                SqlDataReader lector = comando.ExecuteReader();
                                string ImporteCo = "";
                                bool lectorleido = false;
                                while (lector.Read())
                                {
                                    ImporteCo = lector.GetValue(0).ToString();
                                    lectorleido = true;
                                }

                                if (lectorleido == false)
                                {
                                    MessageBox.Show(@"Elija un Examen que tenga Plan de Seguros", @"Error de validación", MessageBoxButtons.OK);
                                    return;
                                }
                                lector.Close();
                                conectasam.closesigesoft();
                                #endregion
                                _objTicketDetalleList.d_SaldoPaciente = decimal.Parse(txtPrecioVenta.Text) * decimal.Parse(txtCantidad.Text) * decimal.Parse(ImporteCo) / 100;
                                _objTicketDetalleList.d_SaldoAseguradora = (decimal.Parse(txtPrecioVenta.Text) * decimal.Parse(txtCantidad.Text)) - _objTicketDetalleList.d_SaldoPaciente;
                                _objTicketDetalleList.d_PrecioVenta = decimal.Parse(txtPrecioVenta.Text);
                            }
                            else if (resultplan[0].i_EsDeducible == 1)
                            {

                            }
                        }
                        else
                        {
                            _objTicketDetalleList.d_PrecioVenta = decimal.Parse(txtPrecio.Text);// decimal.Parse(txtPrecioVenta.Text);
                        }

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
                                _objTicketDetalleList.v_IdUnidadProductiva = txtUnidadProductiva.Text;
                                var precioTarifa = oTicketBL.ObtenerPrecioTarifario(_serviceId, _objTicketDetalleList.v_IdProductoDetalle);
                                _objTicketDetalleList.d_PrecioVenta = precioTarifa;// decimal.Parse(txtPrecioVenta.Text); 
                                decimal d;
                                _objTicketDetalleList.d_Cantidad = decimal.TryParse(txtCantidad.Text, out d) ? d : 0;

                                var tienePlan = false;
                                var resultplan = oTicketBL.TienePlan(_protocolId, txtUnidadProductiva.Text);
                                if (resultplan.Count > 0) tienePlan = true;
                                else tienePlan = false;

                                if (tienePlan)
                                {
                                    if (resultplan[0].i_EsCoaseguro == 1)
                                    {
                                        _objTicketDetalleList.d_SaldoPaciente = resultplan[0].d_Importe;
                                        _objTicketDetalleList.d_SaldoAseguradora = (decimal.Parse(_objTicketDetalleList.d_PrecioVenta.ToString()) * _objTicketDetalleList.d_Cantidad) - resultplan[0].d_Importe;
                                    }
                                    if (resultplan[0].i_EsDeducible == 1)
                                    {
                                        _objTicketDetalleList.d_SaldoPaciente = resultplan[0].d_Importe * decimal.Parse(_objTicketDetalleList.d_PrecioVenta.ToString()) * _objTicketDetalleList.d_Cantidad / 100;
                                        _objTicketDetalleList.d_SaldoAseguradora = (decimal.Parse(_objTicketDetalleList.d_PrecioVenta.ToString()) * _objTicketDetalleList.d_Cantidad) - _objTicketDetalleList.d_SaldoPaciente;
                                    }
                                }

                                findResult.i_RecordStatus = (int)RecordStatus.Grabado;
                            }
                            else if (findResult.i_RecordType == (int)RecordType.Temporal)   // El registro tiene un ID temporal [GUID]
                            {
                                _objTicketDetalleList.v_IdProductoDetalle = txtMedicamento.Tag.ToString();
                                _objTicketDetalleList.v_NombreProducto = txtMedicamento.Text;
                                _objTicketDetalleList.v_CodInterno = txtCodigo.Text;
                                _objTicketDetalleList.d_PrecioVenta = decimal.Parse(txtPrecioVenta.Text);
                                _objTicketDetalleList.v_IdUnidadProductiva = txtUnidadProductiva.Text;
                                decimal d;
                                _objTicketDetalleList.d_Cantidad = decimal.TryParse(txtCantidad.Text, out d) ? d : 0;

                                var tienePlan = false;
                                var resultplan = oTicketBL.TienePlan(_protocolId, txtUnidadProductiva.Text);
                                if (resultplan.Count > 0) tienePlan = true;
                                else tienePlan = false;

                                if (tienePlan)
                                {
                                    if (resultplan[0].i_EsCoaseguro == 1)
                                    {
                                        _objTicketDetalleList.d_SaldoPaciente = resultplan[0].d_Importe;
                                        _objTicketDetalleList.d_SaldoAseguradora = (decimal.Parse(_objTicketDetalleList.d_PrecioVenta.ToString()) * _objTicketDetalleList.d_Cantidad) - resultplan[0].d_Importe;
                                    }
                                    if (resultplan[0].i_EsDeducible == 1)
                                    {
                                        _objTicketDetalleList.d_SaldoPaciente = resultplan[0].d_Importe * decimal.Parse(_objTicketDetalleList.d_PrecioVenta.ToString()) * _objTicketDetalleList.d_Cantidad / 100;
                                        _objTicketDetalleList.d_SaldoAseguradora = (decimal.Parse(_objTicketDetalleList.d_PrecioVenta.ToString()) * _objTicketDetalleList.d_Cantidad) - _objTicketDetalleList.d_SaldoPaciente;
                                    }
                                }

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
            cbExamen.Visible = false;
            txtComponentId.Visible = false;
            ultraLabel9.Visible = false;

            if (_mode == "Edit")
            {
                btnBuscar.Visible = false;
                txtMedicamento.Enabled = false;
                var findResult = _TempTicketDetalleList.Find(p => p.v_TicketDetalleId == _id);
                txtMedicamento.Text = findResult.v_NombreProducto;
                txtCodigo.Text = findResult.v_CodInterno;
                txtCantidad.Value = findResult.d_Cantidad;
                txtPrecioVenta.Value = findResult.d_PrecioVenta;
            }
            cbExamen.Select();
            object listaExamen = LlenarExamen();
            cbExamen.DataSource = listaExamen;
            cbExamen.DisplayMember = "v_Nombre";
            cbExamen.ValueMember = "v_IdComp";
            cbExamen.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            cbExamen.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.cbExamen.DropDownWidth = 590;
            cbExamen.DisplayLayout.Bands[0].Columns[0].Width = 20;
            cbExamen.DisplayLayout.Bands[0].Columns[1].Width = 550;

            cbLine.Select();
            object listaLine = LlenarLines();
            cbLine.DataSource = listaLine;
            cbLine.DisplayMember = "v_Nombre";
            cbLine.ValueMember = "v_IdLinea";
            cbLine.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            cbLine.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.cbLine.DropDownWidth = 590;
            cbLine.DisplayLayout.Bands[0].Columns[0].Width = 20;
            cbLine.DisplayLayout.Bands[0].Columns[1].Width = 550;
            txtMedicamento.Focus();
        }

        private object LlenarLines()
        {
            #region Conexion SAMBHS
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadenasam = "select LN.v_Nombre as v_Nombre ,PL.v_IdUnidadProductiva as  v_IdLinea " +
                            "from [dbo].[plan] PL " +
                            "inner join protocol PR on PL.v_ProtocoloId=PR.v_ProtocolId " +
                            "inner join [20505310072].[dbo].[linea] LN on PL.v_IdUnidadProductiva=LN.v_IdLinea " +
                            "where PR.v_ProtocolId='" + _protocolId + "'";
            var comando = new SqlCommand(cadenasam, connection: conectasam.conectarsigesoft);
            var lector = comando.ExecuteReader();
            string preciounitario = "";
            List<ListaLineas> objListaLineas = new List<ListaLineas>();

            while (lector.Read())
            {
                ListaLineas Lista = new ListaLineas();
                Lista.v_Nombre = lector.GetValue(0).ToString();
                Lista.v_IdLinea = lector.GetValue(1).ToString();
                objListaLineas.Add(Lista);
            }
            lector.Close();
            conectasam.closesigesoft();
            #endregion

            return objListaLineas;
        }

        private object LlenarExamen()
        {
            #region Conexion SIGESOFT Llenar componentes
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena = "select CP.v_Name, SC.v_ComponentId " +
                         "from service SR " +
                         "inner join servicecomponent SC on SR.v_ServiceId=SC.v_ServiceId " +
                         "inner join component CP on SC.v_ComponentId=CP.v_ComponentId " +
                         "where SR.v_ServiceId='"+_serviceId+"' and SC.r_Price>0";
            SqlCommand comando = new SqlCommand(cadena, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            List<ListaExamen> objListaExamen = new List<ListaExamen>();
            while (lector.Read())
            {
                ListaExamen Lista = new ListaExamen();
                Lista.v_Nombre = lector.GetValue(0).ToString();
                Lista.v_IdComp = lector.GetValue(1).ToString();
                objListaExamen.Add(Lista);
            }
            lector.Close();
            conectasam.closesigesoft();
            #endregion

            return objListaExamen;
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnGuardar_Click(null, null);
            }
        }

        private void cbExamen_RowSelected(object sender, RowSelectedEventArgs e)
        {
            #region Conexion SIGESOFT obtener idcomponent
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
            var cadena = "select CP.v_componentId from component CP where CP.v_Name='"+cbExamen.Text+"'";
            SqlCommand comando = new SqlCommand(cadena, connection: conectasam.conectarsigesoft);
            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                txtComponentId.Text = lector.GetValue(0).ToString();
            }
            lector.Close();
            conectasam.closesigesoft();
            #endregion
        }

        private void cbLine_RowSelected(object sender, RowSelectedEventArgs e)
        {
            #region Conexion SAM obtener id de linea
            ConexionSambhs conectasam = new ConexionSambhs();
            conectasam.openSambhs();
            var cadena = "select v_IdLinea from [dbo].[linea] where v_Nombre='" + cbLine.Text + "' and i_Eliminado=0";
            SqlCommand comandou = new SqlCommand(cadena, connection: conectasam.conectarSambhs);
            SqlDataReader lectoru = comandou.ExecuteReader();
            string lineId = "";
            while (lectoru.Read())
            {
                lineId = lectoru.GetValue(0).ToString();
            }
            lectoru.Close();
            conectasam.closeSambhs();
            #endregion
            txtUnPdId.Text = lineId;
        }
    }
}
