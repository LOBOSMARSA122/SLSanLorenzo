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
    public partial class frmAddMedicalBreak : Form
    {
        #region Declarations
             
        public List<DiagnosticRepositoryList> _medicalBreaks = null;     
        public string _serviceId;
        private ServiceBL _serviceBL = new ServiceBL();
        public string _mode;

        #endregion

        #region Properties

        public string DiagnosticRepositoryId { get; set; }
        public string DiseasesName { get; set; }

        #endregion

        public frmAddMedicalBreak()
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
            if (_medicalBreaks == null)
                return;

            ListViewItem listViewItem = null;

            // Discriminar a los registros eliminados logicamente
            _medicalBreaks = _medicalBreaks.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

            foreach (var item in _medicalBreaks)
            {
                listViewItem = new ListViewItem(new[] { item.v_DiseasesName, item.v_DiagnosticRepositoryId});
                lvDescansoMedico.Items.Add(listViewItem);
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
            if (_medicalBreaks == null)
                _medicalBreaks = new List<DiagnosticRepositoryList>();

            // Save ListView / recorrer la lista
            foreach (ListViewItem item in lvDescansoMedico.Items)
            {
                var fields = item.SubItems;

                var interconsultation = _medicalBreaks.Find(p => p.v_DiagnosticRepositoryId == fields[1].Text);

                if (interconsultation == null)   // agregar con normalidad [insert]  a la bolsa  
                {
                    DiagnosticRepositoryList diagnosticRepository = new DiagnosticRepositoryList();
                    diagnosticRepository.v_DiseasesName = fields[0].Text;                 
                    diagnosticRepository.v_DiagnosticRepositoryId = fields[1].Text;  
                    diagnosticRepository.i_GenerateMedicalBreak = (int)SiNo.SI;             
                    diagnosticRepository.i_RecordType = (int)RecordType.Temporal;
                    diagnosticRepository.i_RecordStatus = (int)RecordStatus.Agregado;
                    _medicalBreaks.Add(diagnosticRepository);
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

        private void AddDiagnosticForMedicalBreak()
        {
            var findResult = lvDescansoMedico.FindItemWithText(DiagnosticRepositoryId);

            // El examen ya esta agregado
            if (findResult != null)
            {
                MessageBox.Show("Por favor seleccione otro Diagnóstico.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }      

            var row = new ListViewItem(new[] { DiseasesName, DiagnosticRepositoryId});

            lvDescansoMedico.Items.Add(row);

            lblRecordCountInterconsultas.Text = string.Format("Diagnósticos Seleccionados {0}", lvDescansoMedico.Items.Count);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AddDiagnosticForMedicalBreak();
        }

        private void grdDiagnosticos_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnAgregar.Enabled = (grdDiagnosticos.Selected.Rows.Count > 0);

            if (grdDiagnosticos.Selected.Rows.Count == 0)
                return;

            DiagnosticRepositoryId = grdDiagnosticos.Selected.Rows[0].Cells["v_DiagnosticRepositoryId"].Value.ToString();
            DiseasesName = grdDiagnosticos.Selected.Rows[0].Cells["v_DiseasesName"].Value.ToString();
           
        }

        private void lvInterconsultas_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            btnRemover.Enabled = (lvDescansoMedico.SelectedItems.Count > 0);
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            var selectedItem = lvDescansoMedico.SelectedItems[0];
            var diagnosticRepositoryId = selectedItem.SubItems[1].Text;

            // Eliminacion fisica
            lvDescansoMedico.Items.Remove(selectedItem);
            lblRecordCountInterconsultas.Text = string.Format("Diagnósticos Seleccionados {0}", lvDescansoMedico.Items.Count);

            // Chekar que la lista de seleccionados este instanciada y contenga registros
            if (_medicalBreaks == null || _medicalBreaks.Count == 0)
                return;

            // Eliminacion logica en mi lista temp
            var auxiliaryExam = _medicalBreaks.Find(p => p.v_DiagnosticRepositoryId == diagnosticRepositoryId);

            if (auxiliaryExam != null)
                auxiliaryExam.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
        }

        private void grdDiagnosticos_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {          
            AddDiagnosticForMedicalBreak();
        }

    }
}
