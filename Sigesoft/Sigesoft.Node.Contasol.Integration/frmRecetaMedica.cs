using Sigesoft.Node.WinClient.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.UI.Reports;
using System.Data.SqlClient;


namespace Sigesoft.Node.Contasol.Integration
{
    public partial class frmRecetaMedica : Form
    {
        private string _serviceId;
        private OperationResult _pobjOperationResult;
        private readonly RecetaBl _objRecetaBl;
        private readonly List<DiagnosticRepositoryList> _listDiagnosticRepositoryLists;
        private string _protocolId;

        public frmRecetaMedica(List<DiagnosticRepositoryList> ListaDX, string serviceId, string protocolId)
        {
            _serviceId = serviceId;
            InitializeComponent();
            _objRecetaBl = new RecetaBl();
            _pobjOperationResult = new OperationResult();
            _listDiagnosticRepositoryLists = ListaDX;
            _protocolId = protocolId;
            
        }

        private void GetData(List<DiagnosticRepositoryList> ListaDX)
        {
           
            try
            {
                ListaDX.ForEach(l => l.RecipeDetail = new List<recetaDto>());
                var data = _objRecetaBl.GetHierarchycalData(ref _pobjOperationResult, ListaDX);
                
                if (data.Any())
                {
                    var previousIndex = grdTotalDiagnosticos.ActiveRow != null ? grdTotalDiagnosticos.ActiveRow.Index : 0;
                    grdTotalDiagnosticos.DataSource = data;
                    grdTotalDiagnosticos.Rows.Refresh(RefreshRow.ReloadData);
                    grdTotalDiagnosticos.Rows[previousIndex].Activate();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, @"GetData()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MedicinaReceta(string serviceId) {
            var data = _objRecetaBl.GetHierarchycalData(ref _pobjOperationResult, _listDiagnosticRepositoryLists);

            if (data.Any())
            {
                var previousIndex = grdTotalDiagnosticos.ActiveRow != null ? grdTotalDiagnosticos.ActiveRow.Index : 0;
                grdTotalDiagnosticos.DataSource = data;
                grdTotalDiagnosticos.Rows.Refresh(RefreshRow.ReloadData);
                grdTotalDiagnosticos.Rows[previousIndex].Activate();
            }
        }

        private void frmRecetaMedica_Load(object sender, EventArgs e)
        {
            GetData(_listDiagnosticRepositoryLists);
        }

        private void grdTotalDiagnosticos_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void grdTotalDiagnosticos_ClickCellButton(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            try
            {
                if (e.Cell == null || e.Cell.Row.Cells["v_DiagnosticRepositoryId"].Value == null) return;
                var diagnosticRepositoryId = e.Cell.Row.Cells["v_DiagnosticRepositoryId"].Value.ToString();
                var categoryName = e.Cell.Row.Cells["v_ComponentName"].Value.ToString();
                #region Conexion SIGESOFT verificar la unidad productiva del componente
                ConexionSigesoft conectasam = new ConexionSigesoft();
                conectasam.opensigesoft();
                var cadena1 = "select CP.v_IdUnidadProductiva " +
                              "from diagnosticrepository DR " +
                              "inner join component CP on DR.v_ComponentId=CP.v_ComponentId "+
                              "where DR.v_DiagnosticRepositoryId='"+diagnosticRepositoryId+"' ";
                SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                SqlDataReader lector = comando.ExecuteReader();
                string LineId = "";
                while (lector.Read())
                {
                    LineId = lector.GetValue(0).ToString();
                }
                lector.Close();
                conectasam.closesigesoft();
                #endregion
                switch (e.Cell.Column.Key)
                {
                    case "_AddRecipe":
                    {
                        var f = new frmAddRecipe(ActionForm.Add, diagnosticRepositoryId, 0, _protocolId, _serviceId, categoryName, LineId) { StartPosition = FormStartPosition.CenterScreen };
                        f.ShowDialog();
                        GetData(_listDiagnosticRepositoryLists);
                    }
                        break;

                    case "_EditRecipe":
                    {
                        var recipeId = int.Parse(e.Cell.Row.Cells["i_IdReceta"].Value.ToString());
                        var f = new frmAddRecipe(ActionForm.Edit, diagnosticRepositoryId, recipeId, _protocolId, _serviceId, categoryName, LineId) { StartPosition = FormStartPosition.CenterScreen };
                        f.ShowDialog();
                        GetData(_listDiagnosticRepositoryLists);
                    }
                        break;

                    case "_DeleteRecipe":
                    {
                        _pobjOperationResult = new OperationResult();
                        var recipeId = int.Parse(e.Cell.Row.Cells["i_IdReceta"].Value.ToString());
                        var msg = MessageBox.Show(@"¿Seguro de eliminar esta receta?", @"Confirmación",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (msg == DialogResult.No) return;
                        _objRecetaBl.DeleteRecipe(ref _pobjOperationResult, recipeId);
                        if (_pobjOperationResult.Success == 0)
                        {
                            MessageBox.Show(_pobjOperationResult.ErrorMessage, @"Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }

                        GetData(_listDiagnosticRepositoryLists);
                    }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"grdTotalDiagnosticos_ClickCellButton()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            var recomendaciones = string.Join("\n-", _listDiagnosticRepositoryLists.Where(o => !string.IsNullOrWhiteSpace(o.v_RecomendationsName)).Select(p => p.v_RecomendationsName).Distinct()).Trim();
            var restricciones = string.Join("\n-", _listDiagnosticRepositoryLists.Where(o => !string.IsNullOrWhiteSpace(o.v_RestrictionsName)).Select(p => p.v_RestrictionsName).Distinct()).Trim();
            var f = new frmReporteReceta(_serviceId, recomendaciones, restricciones);
            f.ShowDialog();
        }

        private void ultraButton2_Click(object sender, EventArgs e)
        {
            var f = new frmConfirmarDespacho(_serviceId);
            f.ShowDialog();

        }
    }
}
