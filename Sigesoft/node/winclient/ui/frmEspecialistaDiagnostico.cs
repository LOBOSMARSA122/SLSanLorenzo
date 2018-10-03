using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using System.Text.RegularExpressions;
using Infragistics.Win.UltraWinMaskedEdit;
using Sigesoft.Node.WinClient.UI.UserControls;


namespace Sigesoft.Node.WinClient.UI
{

    public partial class frmEspecialistaDiagnostico : Form
    {
        private List<DiagnosticRepositoryList> _tmpExamDiagnosticComponentList = null;
        private string _serviceId = null;
        private string _componentId;
        ServiceBL _serviceBL = new ServiceBL();

        public frmEspecialistaDiagnostico(string pstrServiceId, string pstrComponentId)
        {
            _serviceId = pstrServiceId;
            _componentId = pstrComponentId;
            InitializeComponent();
        }

        private void btnAgregarDxExamen_Click(object sender, EventArgs e)
        {
            var frm = new Sigesoft.Node.WinClient.UI.Operations.Popups.frmAddExamDiagnosticComponent("New");
            frm._componentId = _componentId;
            frm._serviceId = _serviceId;

            if (_tmpExamDiagnosticComponentList != null)
            {
                frm._tmpExamDiagnosticComponentList = _tmpExamDiagnosticComponentList;
            }

            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            // Refrescar grilla
            // Actualizar variable
            if (frm._tmpExamDiagnosticComponentList != null)
            {
                _tmpExamDiagnosticComponentList = frm._tmpExamDiagnosticComponentList;

                var dataList = _tmpExamDiagnosticComponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdDiagnosticoPorExamenComponente.DataSource = dataList;
                lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", dataList.Count());
            }
        }

        private void frmEspecialistaDiagnostico_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            var diagnosticList = _serviceBL.GetServiceComponentDisgnosticsForGridView(ref objOperationResult,
                                                                                     _serviceId,
                                                                                     _componentId);

            // Limpiar variable que contiene los Dx sugeridos / manuales
            if (_tmpExamDiagnosticComponentList != null)
                _tmpExamDiagnosticComponentList = null;

            if (diagnosticList != null && diagnosticList.Count != 0)
            {
                // Cargar la grilla de DX sugeridos / manuales
                grdDiagnosticoPorExamenComponente.DataSource = diagnosticList;
                lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", diagnosticList.Count());

                // Cargar mi lista temporal con data k viene de BD
                _tmpExamDiagnosticComponentList = diagnosticList;

                // Analizar el resultado de la operación
                if (objOperationResult.Success != 1)
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Limpiar la grilla de DX con una entidad vacia
                grdDiagnosticoPorExamenComponente.DataSource = new DiagnosticRepositoryList();
                lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", 0);

            }
            PintargrdDiagnosticoPorExamenComponente();
        }

        private void btnEditarDxExamen_Click(object sender, EventArgs e)
        {
            if (grdDiagnosticoPorExamenComponente.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un registro.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var frm = new Sigesoft.Node.WinClient.UI.Operations.Popups.frmAddExamDiagnosticComponent("Edit");

            var diagnosticRepositoryId = grdDiagnosticoPorExamenComponente.Selected.Rows[0].Cells["v_DiagnosticRepositoryId"].Value.ToString();
            frm._diagnosticRepositoryId = diagnosticRepositoryId;

            frm._componentId = _componentId;
            frm._serviceId = _serviceId;

            if (_tmpExamDiagnosticComponentList != null)
            {
                frm._tmpExamDiagnosticComponentList = _tmpExamDiagnosticComponentList;
            }

            frm.ShowDialog();

            // Refrescar grilla
            // Actualizar variable

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            if (frm._tmpExamDiagnosticComponentList != null)
            {
                _tmpExamDiagnosticComponentList = frm._tmpExamDiagnosticComponentList;

                var dataList = _tmpExamDiagnosticComponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdDiagnosticoPorExamenComponente.DataSource = new DiagnosticRepositoryList();
                grdDiagnosticoPorExamenComponente.DataSource = dataList;
                grdDiagnosticoPorExamenComponente.Refresh();
                lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", dataList.Count());

                PintargrdDiagnosticoPorExamenComponente();
            }
        }

        private void PintargrdDiagnosticoPorExamenComponente()
        {
            // Pinta fila seleccionada
            for (int i = 0; i < grdDiagnosticoPorExamenComponente.Rows.Count; i++)
            {
                var caliFinal = (PreQualification)grdDiagnosticoPorExamenComponente.Rows[i].Cells["i_PreQualificationId"].Value;

                switch (caliFinal)
                {
                    case PreQualification.SinPreCalificar:
                        grdDiagnosticoPorExamenComponente.Rows[i].Appearance.BackColor = Color.Pink;
                        grdDiagnosticoPorExamenComponente.Rows[i].Appearance.BackColor2 = Color.Pink;
                        break;
                    case PreQualification.Aceptado:
                        grdDiagnosticoPorExamenComponente.Rows[i].Appearance.BackColor = Color.LawnGreen;
                        grdDiagnosticoPorExamenComponente.Rows[i].Appearance.BackColor2 = Color.LawnGreen;
                        break;
                    case PreQualification.Rechazado:
                        grdDiagnosticoPorExamenComponente.Rows[i].Appearance.BackColor = Color.DarkGray;
                        grdDiagnosticoPorExamenComponente.Rows[i].Appearance.BackColor2 = Color.DarkGray;

                        break;
                    default:
                        break;
                }
                //Y doy el efecto degradado vertical
                grdDiagnosticoPorExamenComponente.Rows[i].Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;

            }
        }

        private void btnRemoverDxExamen_Click(object sender, EventArgs e)
        {
            if (grdDiagnosticoPorExamenComponente.Selected.Rows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un registro.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == DialogResult.Yes)
            {
                // Delete the item

                // Capturar id desde la griila de restricciones
                var diagnosticRepositoryId = grdDiagnosticoPorExamenComponente.Selected.Rows[0].Cells["v_DiagnosticRepositoryId"].Value.ToString();

                int recordType = int.Parse(grdDiagnosticoPorExamenComponente.Selected.Rows[0].Cells["i_RecordType"].Value.ToString());

                // Buscar registro para remover
                var findResult = _tmpExamDiagnosticComponentList.Find(p => p.v_DiagnosticRepositoryId == diagnosticRepositoryId);

                if (recordType == (int)RecordType.Temporal)
                {
                    _tmpExamDiagnosticComponentList.Remove(findResult);
                }
                else if (recordType == (int)RecordType.NoTemporal)
                {
                    findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
                }

                var dataList = _tmpExamDiagnosticComponentList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                //grdDiagnosticoPorExamenComponente.DataSource = new DiagnosticRepositoryList();
                grdDiagnosticoPorExamenComponente.DataSource = dataList;
                lblRecordCountDiagnosticoPorExamenCom.Text = string.Format("Se encontraron {0} registros.", _tmpExamDiagnosticComponentList.Count());
            }
        }

        private void btnGuardarExamen_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            //var serviceComponentDto = new servicecomponentDto();
            //serviceComponentDto.v_ServiceComponentId = _serviceComponentId;
            //serviceComponentDto.v_Comment = txtComentario.Text;
            //serviceComponentDto.i_ServiceComponentStatusId = int.Parse(cbEstadoComponente.SelectedValue.ToString());
            //serviceComponentDto.i_ExternalInternalId = int.Parse(cbTipoProcedenciaExamen.SelectedValue.ToString());
            //serviceComponentDto.i_IsApprovedId = Convert.ToInt32(chkApproved.Checked);

            //serviceComponentDto.v_ComponentId = _componentId;
            //serviceComponentDto.v_ServiceId = _serviceId;


            _serviceBL.AddDiagnosticRepository(ref objOperationResult,
                                                  _tmpExamDiagnosticComponentList,
                                                   null,
                                                   Globals.ClientSession.GetAsList(),
                                                   true,null);

            MessageBox.Show("se guardó correctamente.", "CORRECTO", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
