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

namespace Sigesoft.Node.WinClient.UI.Hospitalizacion
{
    public partial class frmHabitacion : Form
    {
        private string _hospitalizacion;
        private string _mode;
        private string _hospitalizacionHabitacionId;

        private hospitalizacionhabitacionDto _hospitalizacionHabitaciónDto = null;
        private HospitalizacionHabitacionBL _hospitalizacionBL = new HospitalizacionHabitacionBL();
        
        public string habitacionId = string.Empty;

        public frmHabitacion(string hopitalizacionId, string mode, string hospitalizacionHabitacionId)
        {
            _hospitalizacion = hopitalizacionId;
            _mode = mode;
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
                    var cadena1 = "select PL.d_Importe " +
                                  "from hospitalizacionservice HP " +
                                  "inner join service SR on HP.v_ServiceId=SR.v_ServiceId " +
                                  "inner join protocol PR on SR.v_ProtocolId=PR.v_ProtocolId " +
                                  "inner join [dbo].[plan] PL on PR.v_ProtocolId=PL.v_ProtocoloId " +
                                  "where v_HopitalizacionId='"+_hospitalizacion+"' and PL.v_IdUnidadProductiva='N009-LN000000042'";
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

            habHospit = _hospitalizacionBL.GetHabitaciónH(ref objOperationResult, int.Parse(cboHabitación.SelectedValue.ToString()));

            txtPrecio.Text = habHospit.v_Value2;

        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnGuardarTicket_Click(null, null);
            }
        }

       
    }
}
