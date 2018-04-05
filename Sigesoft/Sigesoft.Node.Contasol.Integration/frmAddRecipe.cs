using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.UI.Operations.Popups;

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

        public frmAddRecipe(ActionForm actionForm, string idDiagnosticRepository, int recipeId)
        {
            InitializeComponent();
            _recipeId = recipeId;
            _idDiagnosticRepository = idDiagnosticRepository;
            _pobjOperationResult = new OperationResult();
            _objRecetaBl = new RecetaBl();
            _actionForm = actionForm;
            Text = actionForm == ActionForm.Add ? "Agregar Nueva Receta" : "Editar Receta";
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
            var f = new frmSearchMedicamento();
            var result = f.ShowDialog();
            if (result == DialogResult.OK)
            {
                var medicamento = f.MedicamentoSeleccionado;
                if (medicamento == null) return;
                txtMedicamento.Text = medicamento.NombreCompleto;
                txtMedicamento.Tag = medicamento.IdProductoDetalle;
                idUnidadProductiva = medicamento.IdLinea;
            }
        }
    }
}
