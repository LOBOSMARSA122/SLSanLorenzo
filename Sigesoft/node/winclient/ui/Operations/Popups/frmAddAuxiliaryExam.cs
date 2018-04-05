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
using System.Collections;

namespace Sigesoft.Node.WinClient.UI.Operations.Popups
{
    public partial class frmAddAuxiliaryExam : Form
    {
        #region Declarations
            
        MedicalExamBL _medicalExamBL = new MedicalExamBL();
        public List<AuxiliaryExamList> _auxiliaryExams = null;
        public string _mode = null;
        public string _serviceId;

        #endregion

        #region Properties

        private string MedicalExamId { get; set; }
        private string MedicalExamName { get; set; }
        private string CategoryName { get; set; }

        #endregion

        public frmAddAuxiliaryExam()
        {
            InitializeComponent();
        }

        private void frmAddAuxiliaryExam_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadSelectedAuxiliaryExam();

            if (_mode == "New")
            {
                
            }
            else
            {
               
            }
           
        }

        private void LoadSelectedAuxiliaryExam()
        {
            if (_auxiliaryExams == null)
                return;

            ListViewItem listViewItem = null;

            // Discriminar a los registros eliminados logicamente
            _auxiliaryExams = _auxiliaryExams.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

            foreach (var item in _auxiliaryExams)
            {
                listViewItem = new ListViewItem(new[] { item.v_ComponentName, item.v_CategoryName, item.v_ComponentId });
                lvExamenesSeleccionados.Items.Add(listViewItem);
            }
        }

        private string BuildFilterExpression()
        {
            // Get the filters from the UI
            string filterExpression = string.Empty;

            List<string> Filters = new List<string>();

            if (cbCategoria.SelectedValue.ToString() != "-1")
            {
                Filters.Add("i_CategoryId==" + int.Parse(cbCategoria.SelectedValue.ToString()));
            }
            else
            {
                Filters.Add("i_CategoryId==1 ||i_CategoryId==6 || i_CategoryId==4");
            }
               
            if (!string.IsNullOrEmpty(txtExamen.Text)) 
                Filters.Add("v_Name.Contains(\"" + txtExamen.Text.Trim() + "\")");
            Filters.Add("i_IsDeleted==0");
         
            // Create the Filter Expression
            filterExpression = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    filterExpression = filterExpression + item + " && ";
                }
                filterExpression = filterExpression.Substring(0, filterExpression.Length - 4);
            }

            return filterExpression;
        }

        private void LoadComboBox()
        {
            OperationResult objOperationResult = new OperationResult();

            var Filter = BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 116, null);

            Utils.LoadDropDownList(cbCategoria, "Value1", "Id", Filter.FindAll(p => p.Id == "1" || p.Id == "6" || p.Id == "4"), DropDownListAction.Select);       
       
            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetDataMedicalExam()
        {
            OperationResult objOperationResult = new OperationResult();
            var filterExpression = BuildFilterExpression();
            var auxiliaryExams = _medicalExamBL.GetMedicalExamPagedAndFiltered(ref objOperationResult, 0, null, "v_Name ASC", filterExpression);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (auxiliaryExams != null && auxiliaryExams.Count > 0)
            {
                grdExamenAuxilar.DataSource = auxiliaryExams;
                lblRecordCountExamenAuxiliar.Text = string.Format("Se encontraron {0} registros.", auxiliaryExams.Count());
                grdExamenAuxilar.Rows[0].Selected = true;
            }
            else
            {
                lblRecordCountExamenAuxiliar.Text = string.Format("Se encontraron {0} registros.", 0);
            }
          
        }

        private void txtExamen_TextChanged(object sender, EventArgs e)
        {
            GetDataMedicalExam();
        }

        private void cbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDataMedicalExam();
        }

        private void AddAuxiliaryExam()
        {
            var findResult = lvExamenesSeleccionados.FindItemWithText(MedicalExamId);

            // El examen ya esta agregado
            if (findResult != null)
            {
                MessageBox.Show("Por favor seleccione otro examen.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = new ListViewItem(new[] { MedicalExamName, CategoryName, MedicalExamId });

            lvExamenesSeleccionados.Items.Add(row);

            gbExamenesSeleccionados.Text = string.Format("Examenes Seleccionados {0}", lvExamenesSeleccionados.Items.Count); 
        }

        private void btnAgregarExamenAuxiliar_Click(object sender, EventArgs e)
        {
            AddAuxiliaryExam();           
        }

        private void btnRemoverExamenAuxiliar_Click(object sender, EventArgs e)
        {
            var selectedItem = lvExamenesSeleccionados.SelectedItems[0];
            var medicalExamId = selectedItem.SubItems[2].Text;

            // Eliminacion fisica
            lvExamenesSeleccionados.Items.Remove(selectedItem);
            gbExamenesSeleccionados.Text = string.Format("Examenes Seleccionados {0}", lvExamenesSeleccionados.Items.Count);

            // Chekar que la lista de examnes seleccionados este instanciada y contenga registros
            if (_auxiliaryExams == null || _auxiliaryExams.Count == 0)
                return;

            // Eliminacion logica en mi lista temp
            var auxiliaryExam = _auxiliaryExams.Find(p => p.v_ComponentId == medicalExamId);

            if (auxiliaryExam != null)
                auxiliaryExam.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
           
        }      

        private void grdExamenAuxilar_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grdExamenAuxilar.Selected.Rows.Count == 0)
                return;

            MedicalExamId = grdExamenAuxilar.Selected.Rows[0].Cells["v_MedicalExamId"].Value.ToString();
            MedicalExamName = grdExamenAuxilar.Selected.Rows[0].Cells["v_Name"].Value.ToString();

            if (grdExamenAuxilar.Selected.Rows[0].Cells["v_CategoryName"].Value != null)
            {
                CategoryName = grdExamenAuxilar.Selected.Rows[0].Cells["v_CategoryName"].Value.ToString();
            }
            else
            {
                CategoryName = string.Empty;
            }
           
                  
        }

        private void lvExamenesSeleccionados_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            btnRemoverExamenAuxiliar.Enabled = (lvExamenesSeleccionados.SelectedItems.Count > 0);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_auxiliaryExams == null)
                _auxiliaryExams = new List<AuxiliaryExamList>();
           
            // Save ListView / recorrer la lista de examenes seleccionados
            foreach (ListViewItem item in lvExamenesSeleccionados.Items)
            {
                var fields = item.SubItems;

                var findAuxiliaryExam = _auxiliaryExams.Find(p => p.v_ComponentId == fields[2].Text);

                if (findAuxiliaryExam == null)   // agregar con normalidad [insert]  a la bolsa  
                {
                    AuxiliaryExamList auxiliaryExam = new AuxiliaryExamList();
                    auxiliaryExam.v_ComponentName = fields[0].Text;
                    auxiliaryExam.v_CategoryName = fields[1].Text;
                    auxiliaryExam.v_ComponentId = fields[2].Text;
                    auxiliaryExam.v_ServiceId = _serviceId;
                    auxiliaryExam.i_RecordType = (int)RecordType.Temporal;
                    auxiliaryExam.i_RecordStatus = (int)RecordStatus.Agregado;
                    _auxiliaryExams.Add(auxiliaryExam);
                }
                else    // el examen ya esta agregado en la bolsa hay que actualizar su estado
                {
                    if (findAuxiliaryExam.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                    {
                        if (findAuxiliaryExam.i_RecordType == (int)RecordType.NoTemporal)   // El registro Tiene in ID de BD
                        {                            
                            findAuxiliaryExam.i_RecordStatus = (int)RecordStatus.Grabado;
                        }
                        else if (findAuxiliaryExam.i_RecordType == (int)RecordType.Temporal)   // El registro tiene un ID temporal [GUID]
                        {
                            findAuxiliaryExam.i_RecordStatus = (int)RecordStatus.Agregado;
                        }
                    }
                }              
            }

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void grdExamenAuxilar_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            AddAuxiliaryExam();
        }

    }
}
