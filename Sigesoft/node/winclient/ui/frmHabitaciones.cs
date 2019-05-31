using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Common;
using System.Data.SqlClient;
using Sigesoft.Node.WinClient.UI.Configuration;
using Sigesoft.Node.WinClient.BE;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmHabitaciones : Form
    {
        private string _hospitalizacion;
        private string _mode;
        private string _hospitalizacionHabitacionId;
        private string _v_ProtocoloId;
        private string lineId;

        private hospitalizacionhabitacionDto _hospitalizacionHabitaciónDto = null;
        private HospitalizacionHabitacionBL _hospitalizacionBL = new HospitalizacionHabitacionBL();
        public string habitacionId = string.Empty;

        public frmHabitaciones(string hopitalizacionId, string mode, string hospitalizacionHabitacionId, string v_ProtocoloId)
        {
            _hospitalizacion = hopitalizacionId;
            _mode = mode;
            _v_ProtocoloId = v_ProtocoloId;
            _hospitalizacionHabitacionId = hospitalizacionHabitacionId;
            InitializeComponent();
            
        }

        private void frmHabitaciones_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            BindingGrid();

            if (_mode == "NewASEGU" || _mode == "NewHOSPI")
            {
                cmEstadosHabitacion.Items["itemLiberar"].Enabled = false;
                //cmEstadosHabitacion.Items["itemLimpieza"].Enabled = false;
                //prueba
                //dtpFechaFin.Checked = false;
                if (_mode == "NewASEGU")
                {
                    groupBox3.Visible = false;

                }

            }
            else if (_mode == "EditASEGU" || _mode == "EditHOSPI")
            {
                cmEstadosHabitacion.Items["itemLiberar"].Enabled = false;
                //cmEstadosHabitacion.Items["itemLimpieza"].Enabled = false;

                _hospitalizacionHabitaciónDto = _hospitalizacionBL.GetHabitacion(ref objOperationResult, _hospitalizacionHabitacionId);

                foreach (var row in grdDataHabitaciones.Rows)
                {
                    if (row.Cells["i_HabitacionId"].Value.ToString() == _hospitalizacionHabitaciónDto.i_HabitacionId.ToString())
                    {
                        row.Selected = true;
                        row.Activated = true;
                        row.Appearance.BackColor = Color.BlueViolet;
                        break;
                    }
                }

                dtpFechaInicio.Value = _hospitalizacionHabitaciónDto.d_StartDate.Value;
                if (_hospitalizacionHabitaciónDto.i_ConCargoA == (int)CargoHospitalizacion.Paciente)
                    rbPaciente.Checked = true;
                else
                    rbMedicoTratante.Checked = true;
                if (_hospitalizacionHabitaciónDto.d_EndDate != null)
                {
                    //dtpFechaFin.Value = _hospitalizacionHabitaciónDto.d_EndDate.Value;
                }
                txtPrecio.Text = (_hospitalizacionHabitaciónDto.d_Precio).ToString();
            }
            else if (_mode == "View")
            {
                gbForm.Visible = false;
                this.Width = 458;
            }
            

            cbLine.Select();
            object listaLine = LlenarLines();
            cbLine.DataSource = listaLine;
            cbLine.DisplayMember = "v_Nombre";
            cbLine.ValueMember = "v_IdLinea";
            cbLine.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            cbLine.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.cbLine.DropDownWidth = 590;
            cbLine.DisplayLayout.Bands[0].Columns[0].Width = 20;
            cbLine.DisplayLayout.Bands[0].Columns[1].Width = 335;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BindingGrid();
        }

        private void BindingGrid()
        {

            var listHab = new HabitacionBL().GetHabitaciones("");
            grdDataHabitaciones.DataSource = listHab;

        }

        private void cbLine_RowSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e)
        {
            #region Conexion SAM obtener id de linea
            ConexionSambhs conectasam = new ConexionSambhs();
            conectasam.openSambhs();
            var cadena = "select v_IdLinea from [dbo].[linea] where v_Nombre='" + cbLine.Text + "' and i_Eliminado=0";
            SqlCommand comandou = new SqlCommand(cadena, connection: conectasam.conectarSambhs);
            SqlDataReader lectoru = comandou.ExecuteReader();
            lineId = "";
            while (lectoru.Read())
            {
                lineId = lectoru.GetValue(0).ToString();
            }
            lectoru.Close();
            conectasam.closeSambhs();
            #endregion

            txtUnidProdId.Text = lineId;
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
                            "where PR.v_ProtocolId='" + _v_ProtocoloId + "'";
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

        private void btnGuardarTicket_Click(object sender, EventArgs e)
        {
            
            if (grdDataHabitaciones.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una habitación", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
            
            if (grdDataHabitaciones.Selected.Rows[0].Cells["Estado"].Value.ToString() == "OCUPADO")
            {
                MessageBox.Show("No puede asignar una habitación que ya esta siendo usada", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
            if (txtPrecio.Text == "" || txtPrecio.Text == null)
            {
                MessageBox.Show("Debe ingresar un precio.", "VALIDACIÓN", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
            OperationResult objOperationResult = new OperationResult();
            int HabitacionId = int.Parse(grdDataHabitaciones.Selected.Rows[0].Cells["i_HabitacionId"].Value.ToString());
            if (_hospitalizacionHabitaciónDto == null)
            {
                _hospitalizacionHabitaciónDto = new hospitalizacionhabitacionDto();
            }

            if (_mode == "NewASEGU" || _mode == "NewHOSPI")
            {
                
                _hospitalizacionHabitaciónDto.v_HopitalizacionId = _hospitalizacion;
                _hospitalizacionHabitaciónDto.i_HabitacionId = HabitacionId;
                _hospitalizacionHabitaciónDto.d_StartDate = dtpFechaInicio.Value;
                
                //_hospitalizacionHabitaciónDto.d_EndDate = (DateTime?)(dtpFechaFin.Checked == false ? (ValueType)null : dtpFechaFin.Value);
                decimal d;
                if (_mode == "NewASEGU")
                {
                    #region Conexion SIGESOFT Obtener Deducible de Habitacion
                    ConexionSigesoft conectasam = new ConexionSigesoft();
                    conectasam.opensigesoft();
                    var cadena1 = "select PL.d_Importe from [dbo].[plan] PL where PL.v_IdUnidadProductiva='" + lineId + "' and PL.v_ProtocoloId='" + _v_ProtocoloId + "' ";
                    SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                    SqlDataReader lector = comando.ExecuteReader();
                    string deducible = "0.00";
                    while (lector.Read()) { deducible = lector.GetValue(0).ToString(); }
                    lector.Close();
                    conectasam.closesigesoft();
                    #endregion
                    _hospitalizacionHabitaciónDto.d_Precio = txtPrecio.Text != string.Empty ? decimal.TryParse(txtPrecio.Text, out d) ? d : 0 : (decimal?)null;
                    _hospitalizacionHabitaciónDto.d_SaldoPaciente = decimal.Parse(deducible);
                    _hospitalizacionHabitaciónDto.d_SaldoAseguradora = decimal.Parse(txtPrecio.Text) - decimal.Parse(deducible);
                    
                }
                else if (_mode == "NewHOSPI")
                {
                    _hospitalizacionHabitaciónDto.i_ConCargoA = rbMedicoTratante.Checked ? (int)CargoHospitalizacion.MedicoTratante : (int)CargoHospitalizacion.Paciente;
                    _hospitalizacionHabitaciónDto.d_Precio = txtPrecio.Text != string.Empty ? decimal.TryParse(txtPrecio.Text, out d) ? d : 0 : (decimal?)null;
                }

                DialogResult Result = MessageBox.Show("¿Desea Guardar Habitación?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    bool IsUpdateHabitacion = new HabitacionBL().UpdateEstateHabitacionByHospId(_hospitalizacion);
                    _hospitalizacionHabitaciónDto.i_EstateRoom = (int)EstadoHabitacion.Ocupado;
                    habitacionId = _hospitalizacionBL.AddHospitalizacionHabitacion(ref objOperationResult, _hospitalizacionHabitaciónDto, Globals.ClientSession.GetAsList());

                    if (IsUpdateHabitacion)
                    {
                        MessageBox.Show(
                            "El estado de la habitación anterior será de 'En Limpieza', por favor dar aviso al personal correspondiente",
                            "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    
                    
                }
                else
                {
                    this.Close();
                }

            }
            else if (_mode == "EditASEGU" || _mode == "EditHOSPI")
            {

                _hospitalizacionHabitaciónDto.v_HopitalizacionId = _hospitalizacion;
                _hospitalizacionHabitaciónDto.i_HabitacionId = HabitacionId;
                _hospitalizacionHabitaciónDto.d_StartDate = dtpFechaInicio.Value;
                //_hospitalizacionHabitaciónDto.d_EndDate = dtpFechaFin.Value;
                _hospitalizacionHabitaciónDto.i_ConCargoA = rbMedicoTratante.Checked ? (int)CargoHospitalizacion.MedicoTratante : (int)CargoHospitalizacion.Paciente;

                decimal d;
                if (_mode == "EditASEGU")
                {
                    _hospitalizacionHabitaciónDto.d_Precio = txtPrecio.Text != string.Empty ? decimal.TryParse(txtPrecio.Text, out d) ? d : 0 : (decimal?)null;
                    _hospitalizacionHabitaciónDto.d_SaldoAseguradora = txtPrecio.Text != string.Empty ? decimal.TryParse(txtPrecio.Text, out d) ? d : 0 : (decimal?)null;
                }
                else if (_mode == "EditHOSPI")
                {
                    _hospitalizacionHabitaciónDto.d_Precio = txtPrecio.Text != string.Empty ? decimal.TryParse(txtPrecio.Text, out d) ? d : 0 : (decimal?)null;
                }
                _hospitalizacionHabitaciónDto.d_Precio = txtPrecio.Text != string.Empty ? decimal.TryParse(txtPrecio.Text, out d) ? d : 0 : (decimal?)null;

                DialogResult Result = MessageBox.Show("¿Desea Guardar Habitación?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    _hospitalizacionBL.UpdateHospitalizacionHabitacion(ref objOperationResult, _hospitalizacionHabitaciónDto, Globals.ClientSession.GetAsList());
                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void itemLiberar_Click(object sender, EventArgs e)
        {
            if (grdDataHabitaciones.Selected.Rows.Count == 0)
            {
                return;
            }
            if (grdDataHabitaciones.Selected.Rows[0].Cells["Estado"].Value.ToString() == "OCUPADO")
            {
                MessageBox.Show("Primero debe dar de alta al paciente.", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            
            var DialogResult = MessageBox.Show("Se procederá a liberar la habitación, ¿desea continuar?",
                "CONFIRMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            

            if (DialogResult == DialogResult.Yes)
            {
                int HabitacionId = int.Parse(grdDataHabitaciones.Selected.Rows[0].Cells["i_HabitacionId"].Value.ToString());
                string HabitacionHospId = "";
                if (grdDataHabitaciones.Selected.Rows[0].Cells["v_HospHabitacionId"].Value != null)
                {
                    HabitacionHospId = grdDataHabitaciones.Selected.Rows[0].Cells["v_HospHabitacionId"].Value.ToString();
                }
                
                new HabitacionBL().UpdateEstateHabitacion((int)EstadoHabitacion.Libre, HabitacionId, HabitacionHospId);

                BindingGrid();
            }
            
        }

        private void grdDataHabitaciones_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grdDataHabitaciones.Selected.Rows.Count == 0) return;
            OperationResult objOperationResult = new OperationResult();
            SystemParameterList habHospit = new SystemParameterList();
            if (_mode == "NewASEGU")
            {
                #region Conexion SAM
                ConexionSigesoft conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();
                #endregion
                var cadena1 = "select r_HospitalBedPrice from protocol where v_ProtocolId ='" + _v_ProtocoloId + "'";
                SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                string hab = "0.00";
                while (lector.Read())
                {
                    hab = lector.GetValue(0).ToString();
                }
                lector.Close();
                txtPrecio.Text = double.Parse(hab).ToString("N2");
                txtPrecio.Enabled = false;
            }
            else
            {
                
                int HabitacionId = int.Parse(grdDataHabitaciones.Selected.Rows[0].Cells["i_HabitacionId"].Value.ToString());
                habHospit = _hospitalizacionBL.GetHabitaciónH(ref objOperationResult, HabitacionId);

                txtPrecio.Text = double.Parse(habHospit.v_Value2).ToString("N2");
            }
        }
    }
}
