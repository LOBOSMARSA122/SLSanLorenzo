using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using System.Data.SqlClient;
using Sigesoft.Node.WinClient.UI.Configuration;

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmHabitacion : Form
    {
        private string _hospitalizacion;
        private string _mode;
        private string _hospitalizacionHabitacionId;
        private string _v_ProtocoloId;

        private hospitalizacionhabitacionDto _hospitalizacionHabitaciónDto = null;
        private HospitalizacionHabitacionBL _hospitalizacionBL = new HospitalizacionHabitacionBL();
        
        public string habitacionId = string.Empty;
        private string lineId;

        public frmHabitacion(string hopitalizacionId, string mode, string hospitalizacionHabitacionId, string v_ProtocoloId)
        {
            _hospitalizacion = hopitalizacionId;
            _mode = mode;
            _v_ProtocoloId = v_ProtocoloId;
            _hospitalizacionHabitacionId = hospitalizacionHabitacionId;
            InitializeComponent();
        }

        private void frmHabitacion_Load(object sender, EventArgs e)
        {                
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(cboHabitación, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 309, null), DropDownListAction.Select);

            if (_mode == "NewASEGU" || _mode == "NewHOSPI")
            {
                 dtpFechaFin.Checked = false;
                 if (_mode == "NewASEGU")
                 {
                     groupBox3.Visible = false;

                 }
                
            } 
            else if (_mode == "Edit")
            {
                _hospitalizacionHabitaciónDto = _hospitalizacionBL.GetHabitacion(ref objOperationResult, _hospitalizacionHabitacionId);

                cboHabitación.SelectedValue = _hospitalizacionHabitaciónDto.i_HabitacionId.ToString();
                dtpFechaInicio.Value = _hospitalizacionHabitaciónDto.d_StartDate.Value;
                if (_hospitalizacionHabitaciónDto.i_ConCargoA == (int)CargoHospitalizacion.Paciente)
                    rbPaciente.Checked = true;
                else
                    rbMedicoTratante.Checked = true;
                if (_hospitalizacionHabitaciónDto.d_EndDate != null)
                {
                    dtpFechaFin.Value = _hospitalizacionHabitaciónDto.d_EndDate.Value;
                }
                txtPrecio.Text =(_hospitalizacionHabitaciónDto.d_Precio).ToString();
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
            OperationResult objOperationResult = new OperationResult();

            if (_hospitalizacionHabitaciónDto == null)
            {
                _hospitalizacionHabitaciónDto = new hospitalizacionhabitacionDto();
            }

            if (_mode == "NewASEGU" || _mode == "NewHOSPI")
            {
                _hospitalizacionHabitaciónDto.v_HopitalizacionId = _hospitalizacion;
                _hospitalizacionHabitaciónDto.i_HabitacionId = int.Parse(cboHabitación.SelectedValue.ToString());
                _hospitalizacionHabitaciónDto.d_StartDate = dtpFechaInicio.Value;
                _hospitalizacionHabitaciónDto.d_EndDate = (DateTime?) (dtpFechaFin.Checked == false ?  (ValueType) null : dtpFechaFin.Value);
                decimal d;
                if (_mode == "NewASEGU")
                {
                    #region Conexion SIGESOFT Obtener Deducible de Habitacion
                    ConexionSigesoft conectasam = new ConexionSigesoft();
                    conectasam.opensigesoft();
                    var cadena1 = "select PL.d_Importe from [dbo].[plan] PL where PL.v_IdUnidadProductiva='" + lineId + "' and PL.v_ProtocoloId='" + _v_ProtocoloId + "' "; 
                    SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                    SqlDataReader lector = comando.ExecuteReader();
                    string deducible = "";
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
                    habitacionId = _hospitalizacionBL.AddHospitalizacionHabitacion(ref objOperationResult, _hospitalizacionHabitaciónDto, Globals.ClientSession.GetAsList());
                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
            else if (_mode == "Edit")
            {
                _hospitalizacionHabitaciónDto.v_HopitalizacionId = _hospitalizacion;
                _hospitalizacionHabitaciónDto.i_HabitacionId = int.Parse(cboHabitación.SelectedValue.ToString());
                _hospitalizacionHabitaciónDto.d_StartDate = dtpFechaInicio.Value;
                _hospitalizacionHabitaciónDto.d_EndDate = dtpFechaFin.Value;
                _hospitalizacionHabitaciónDto.i_ConCargoA = rbMedicoTratante.Checked ? (int)CargoHospitalizacion.MedicoTratante : (int)CargoHospitalizacion.Paciente;
          
                decimal d;
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

        private void cboHabitación_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            SystemParameterList habHospit = new SystemParameterList();

            if (cboHabitación.SelectedValue == null)
            {
                return;
            }
            if (cboHabitación.SelectedValue.ToString() == "-1")
            {
                txtPrecio.Text = ".";
                return;
            }
            if (_mode == "NewASEGU")
            {
                #region Conexion SAM
                ConexionSigesoft conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();
                #endregion
                var cadena1 = "select r_HospitalBedPrice from protocol where v_ProtocolId ='"+_v_ProtocoloId+"'";
                SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                string hab = "";
                while (lector.Read())
                {
                    hab = lector.GetValue(0).ToString();
                }
                lector.Close();
                txtPrecio.Text = hab;
                txtPrecio.Enabled = false;
            }
            else
            {
                habHospit = _hospitalizacionBL.GetHabitaciónH(ref objOperationResult, int.Parse(cboHabitación.SelectedValue.ToString()));

                txtPrecio.Text = habHospit.v_Value2;
            }
            

        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnGuardarTicket_Click(null, null);
            }
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

       
    }
}
