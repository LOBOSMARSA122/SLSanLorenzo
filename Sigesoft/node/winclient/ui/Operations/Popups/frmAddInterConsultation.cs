using Infragistics.Win.UltraWinGrid;
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

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class frmAddInterConsultation : Form
    {
        #region Declarations
             
        public List<DiagnosticRepositoryList> _interconsultations = null;     
        public string _serviceId;
        private ServiceBL _serviceBL = new ServiceBL();
        public string _mode;

        #endregion

        #region Properties

        public string DiagnosticRepositoryId { get; set; }
        public string DiseasesName { get; set; }

        #endregion
      
        public frmAddInterConsultation()
        {
            InitializeComponent();
        }

        private void InitializeData()
        {            
            GetDiagnosticsForGridView();
            LoadSelectedInterconsultation();
        }

        private void LoadSelectedInterconsultation()
        {
            if (_interconsultations == null)
                return;

            ListViewItem listViewItem = null;

            // Discriminar a los registros eliminados logicamente
            _interconsultations = _interconsultations.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

            foreach (var item in _interconsultations)
            {
                listViewItem = new ListViewItem(new[] { item.v_DiseasesName, item.v_OfficeName, item.v_DiagnosticRepositoryId, item.v_OfficeId });
                lvInterconsultas.Items.Add(listViewItem);
            }
        }

        private void GetDiagnosticsForGridView()
        {
            OperationResult objOperationResult = new OperationResult();
            var diagnostics = _serviceBL.GetDisgnosticsByServiceId(ref objOperationResult, _serviceId);

            grdDiagnosticos.DataSource = diagnostics;
            lblRecordCountDiagnosticos.Text = string.Format("Se encontraron {0} registros.", diagnostics.Count());

            // Analizar el resultado de la operación
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }         

        }

        private void frmAddInterConsultation_Load(object sender, EventArgs e)
        {
            InitializeData();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_interconsultations == null)
                _interconsultations = new List<DiagnosticRepositoryList>();

            // Save ListView / recorrer la lista
            foreach (ListViewItem item in lvInterconsultas.Items)
            {
                var fields = item.SubItems;

                var interconsultation = _interconsultations.Find(p => p.v_DiagnosticRepositoryId == fields[2].Text);

                if (interconsultation == null)   // agregar con normalidad [insert]  a la bolsa  
                {
                    DiagnosticRepositoryList diagnosticRepository = new DiagnosticRepositoryList();
                    diagnosticRepository.v_DiseasesName = fields[0].Text;
                    diagnosticRepository.v_OfficeName = fields[1].Text;
                    diagnosticRepository.v_DiagnosticRepositoryId = fields[2].Text;                   
                    diagnosticRepository.v_OfficeId = fields[3].Text;
                    diagnosticRepository.i_SendToInterconsultationId = (int)SiNo.SI;

                    diagnosticRepository.i_RecordType = (int)RecordType.Temporal;
                    diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                    _interconsultations.Add(diagnosticRepository);
                }
                else    // el examen ya esta agregado en la bolsa hay que actualizar su estado
                {
                    if (interconsultation.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                    {
                        if (interconsultation.i_RecordType == (int)RecordType.NoTemporal)   // El registro Tiene in ID de BD
                        {
                            interconsultation.i_RecordStatus = (int)RecordStatus.Grabado;
                        }
                        else if (interconsultation.i_RecordType == (int)RecordType.Temporal)   // El registro tiene un ID temporal [GUID]
                        {
                            interconsultation.i_RecordStatus = (int)RecordStatus.Agregado;
                        }
                    }
                }       
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
           
        }

        private void AddInterconsultation()
        {
            var findResult = lvInterconsultas.FindItemWithText(DiagnosticRepositoryId);

            // El examen ya esta agregado
            if (findResult != null)
            {
                MessageBox.Show("Por favor seleccione otro Diagnóstico.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var frm = new Operations.Popups.frmAddSelectOfficeForInterConsultation();

            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Cancel)
                return;

            var row = new ListViewItem(new[] { DiseasesName, frm._officeName, DiagnosticRepositoryId, frm._officeId });

            lvInterconsultas.Items.Add(row);

            lblRecordCountInterconsultas.Text = string.Format("Diagnósticos Seleccionados {0}", lvInterconsultas.Items.Count);
        }

        private void btnAgregarInterConsulta_Click(object sender, EventArgs e)
        {
            AddInterconsultation();
        }

        private void grdDiagnosticos_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnAgregarInterConsulta.Enabled = (grdDiagnosticos.Selected.Rows.Count > 0);

            if (grdDiagnosticos.Selected.Rows.Count == 0)
                return;

            DiagnosticRepositoryId = grdDiagnosticos.Selected.Rows[0].Cells["v_DiagnosticRepositoryId"].Value.ToString();
            DiseasesName = grdDiagnosticos.Selected.Rows[0].Cells["v_DiseasesName"].Value.ToString();
           
        }

        private void lvInterconsultas_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            btnRemoverInterConsulta.Enabled = (lvInterconsultas.SelectedItems.Count > 0);
        }

        private void btnRemoverInterConsulta_Click(object sender, EventArgs e)
        {
            var selectedItem = lvInterconsultas.SelectedItems[0];
            var diagnosticRepositoryId = selectedItem.SubItems[2].Text;

            // Eliminacion fisica
            lvInterconsultas.Items.Remove(selectedItem);
            lblRecordCountInterconsultas.Text = string.Format("Diagnósticos Seleccionados {0}", lvInterconsultas.Items.Count);

            // Chekar que la lista de seleccionados este instanciada y contenga registros
            if (_interconsultations == null || _interconsultations.Count == 0)
                return;

            // Eliminacion logica en mi lista temp
            var auxiliaryExam = _interconsultations.Find(p => p.v_DiagnosticRepositoryId == diagnosticRepositoryId);

            if (auxiliaryExam != null)
                auxiliaryExam.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
        }

        private void grdDiagnosticos_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {          
            AddInterconsultation();
        }

    }
}
