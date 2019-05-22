using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.UI.Operations.Popups;
using Sigesoft.Node.WinClient.BLL;
using System.Data.SqlClient;

namespace Sigesoft.Node.Contasol.Integration
{
    public partial class frmAddRecipe : Form
    {
        private OperationResult _pobjOperationResult;
        private readonly ActionForm _actionForm;
        private readonly RecetaBl _objRecetaBl;
        private recetaDto _recetaDto;
        private readonly string _idDiagnosticRepository;
        private readonly int _recipeId;
        private string idUnidadProductiva;
        private string _protocolId;
        private string _serviceId;
        private string _categoryName;
        private string _LineId;

        public frmAddRecipe(ActionForm actionForm, string idDiagnosticRepository, int recipeId, string protocolId, string serviceId, string categoryName, string LineId)
        {
            InitializeComponent();
            _recipeId = recipeId;
            _idDiagnosticRepository = idDiagnosticRepository;
            _pobjOperationResult = new OperationResult();
            _objRecetaBl = new RecetaBl();
            _actionForm = actionForm;
            _protocolId = protocolId;
            Text = actionForm == ActionForm.Add ? "Agregar Nueva Receta" : "Editar Receta";
            _serviceId = serviceId;
            _categoryName = categoryName;
            _LineId = LineId;
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void Cargar(string idDiagnostic, int recipeId)
        {
            try
            {
                _pobjOperationResult = new OperationResult();
                switch (_actionForm)
                {
                    case ActionForm.Add:
                        _recetaDto = new recetaDto();
                        _recetaDto.v_DiagnosticRepositoryId = idDiagnostic;
                        break;

                    case ActionForm.Edit:
                        _recetaDto = _objRecetaBl.GetRecipeById(ref _pobjOperationResult, recipeId);
                        if (_pobjOperationResult.Success == 0)
                        {
                            MessageBox.Show(_pobjOperationResult.ErrorMessage, @"Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }
                        txtMedicamento.Text = _recetaDto.NombreMedicamento;
                        txtMedicamento.Tag = _recetaDto.v_IdProductoDetalle;
                        txtCantidad.Text = (_recetaDto.d_Cantidad ?? 0m).ToString(CultureInfo.InvariantCulture);
                        txtDuracion.Text = _recetaDto.v_Duracion.Trim();
                        txtPosologia.Text = _recetaDto.v_Posologia.Trim();
                        idUnidadProductiva = _recetaDto.v_IdUnidadProductiva;
                        txtUnidadProductiva.Text = _recetaDto.v_IdUnidadProductiva;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, @"Cargar()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmAddRecipe_Load(object sender, EventArgs e)
        {
            Cargar(_idDiagnosticRepository, _recipeId);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            TicketBL oTicketBL = new TicketBL();
            try
            {
                _pobjOperationResult = new OperationResult();
                if (!uvDatos.Validate(true, false).IsValid) return;

                if (txtMedicamento.Tag == null)
                {
                    MessageBox.Show(@"Por favor seleccione un medicamento", @"Error de validación", MessageBoxButtons.OK);
                    txtMedicamento.Focus();
                    return;
                }

                decimal d;
                _recetaDto.d_Cantidad = decimal.TryParse(txtCantidad.Text, out d) ? d : 0;
                _recetaDto.v_Duracion = txtDuracion.Text.Trim();
                _recetaDto.v_Posologia = txtPosologia.Text.Trim();
                _recetaDto.t_FechaFin = dtpFechaFin.Value;
                _recetaDto.v_IdProductoDetalle = txtMedicamento.Tag.ToString();
                _recetaDto.v_IdUnidadProductiva = idUnidadProductiva;
                _recetaDto.v_ServiceId = _serviceId;

                var tienePlan = false;
                var resultplan = oTicketBL.TienePlan(_protocolId, txtUnidadProductiva.Text);
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
                                      "inner join servicecomponent SC on PL.v_IdUnidadProductiva=SC.v_IdUnidadProductiva " +
                                      "inner join diagnosticrepository DR on DR.v_ComponentId=SC.v_ComponentId " +
                                      "where PR.v_ProtocolId='"+_protocolId+"' and DR.v_DiagnosticRepositoryId='"+_idDiagnosticRepository+"' ";
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
                            MessageBox.Show(@"El consultorio no tiene Plan de Seguros", @"Error de validación", MessageBoxButtons.OK);
                            return;
                        }
                        lector.Close();
                        conectasam.closesigesoft();
                        #endregion
                        _recetaDto.d_SaldoPaciente = (decimal.Parse(ImporteCo) / 100) * (decimal.Parse(txtNuevoPrecio.Text) * _recetaDto.d_Cantidad);
                        _recetaDto.d_SaldoAseguradora = (decimal.Parse(txtNuevoPrecio.Text) * _recetaDto.d_Cantidad) - _recetaDto.d_SaldoPaciente;
                    }
                    
                }
                else
                {
                    _recetaDto.d_SaldoPaciente = decimal.Parse(txtPrecio.Text) * _recetaDto.d_Cantidad;
                }

                _objRecetaBl.AddUpdateRecipe(ref _pobjOperationResult, _recetaDto);

                if (_pobjOperationResult.Success == 0)
                {
                    MessageBox.Show(_pobjOperationResult.ErrorMessage, @"Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"btnGuardar_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCantidad_Validating(object sender, CancelEventArgs e)
        {
            decimal d;
            if (!string.IsNullOrWhiteSpace(txtCantidad.Text))
                txtCantidad.Text = decimal.TryParse(txtCantidad.Text.Trim(), out d) ? d.ToString(CultureInfo.InvariantCulture) : "0";
        }

        private void txtMedicamento_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
        {
            TicketBL oTicketBL = new TicketBL();
            var f = new frmSearchMedicamento();
            var result = f.ShowDialog();
            if (result == DialogResult.OK)
            {
                var medicamento = f.MedicamentoSeleccionado;
                if (medicamento == null) return;
                txtMedicamento.Text = medicamento.NombreCompleto;
                txtMedicamento.Tag = medicamento.IdProductoDetalle;
                idUnidadProductiva = medicamento.IdLinea;
                txtUnidadProductiva.Text = medicamento.IdLinea;
                txtPrecio.Text = medicamento.PrecioVenta.ToString();
                var tienePlan = false;
                var resultplan = oTicketBL.TienePlan(_protocolId, txtUnidadProductiva.Text);
                if (resultplan.Count > 0) tienePlan = true;
                else tienePlan = false;

                if (tienePlan)
                {
                    if (resultplan[0].i_EsCoaseguro == 1)
                    {
                        #region Conexion SAM
                        ConexionSigesoft conectasam = new ConexionSigesoft();
                        conectasam.opensigesoft();
                        #endregion
                        var cadena1 = "select PR.r_MedicineDiscount, OO.v_Name, PR.v_CustomerOrganizationId from Organization OO inner join protocol PR On PR.v_AseguradoraOrganizationId = OO.v_OrganizationId where PR.v_ProtocolId ='" + _protocolId + "'";
                        SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                        SqlDataReader lector = comando.ExecuteReader();
                        string eps = "";
                        while (lector.Read())
                        {
                            eps = lector.GetValue(0).ToString();
                        }
                        lector.Close();
                        conectasam.closesigesoft();
                        //calculo nuevo precio
                        txtPPS.Text = medicamento.d_PrecioMayorista.ToString();
                        txtDctoEPS.Text = eps;
                        decimal nuevoPrecio = decimal.Parse(txtPPS.Text) - ((decimal.Parse(eps) * decimal.Parse(txtPPS.Text)) / 100);
                        txtNuevoPrecio.Text = nuevoPrecio.ToString();

                    }

                }
            }
        }

        private void ultraLabel7_Click(object sender, EventArgs e)
        {

        }

        private void txtPrecio_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
