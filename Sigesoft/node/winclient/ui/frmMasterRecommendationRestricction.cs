using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.BE;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmMasterRecommendationRestricction : Form
    {
        MasterRecommendationRestricctionBL _objBL = new MasterRecommendationRestricctionBL();
        int _typifyingId;
        string strFilterExpression;
        public string _masterRecommendationRestricctionId;
        public string _masterRecommendationRestricctionName;
        ModeOperation _mode;
        public string _nameInvokerForm;
        public List<RestrictionList> _restrictions = null;
        public DateTime? _startDate = null;
        public DateTime? _endDate = null;
        public string _serviceId;

        public frmMasterRecommendationRestricction(string pstrTypifyingName, int pintTypifyingId, ModeOperation pstrMode)
        {
            InitializeComponent();
            this.Text = "Administración de" + this.Text + " " + pstrTypifyingName;
            _typifyingId = pintTypifyingId;
            _mode = pstrMode;

        }
        private void SearchControlAndSetEvents(Control ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).CharacterCasing = CharacterCasing.Upper;
                }

                if (ctrl is Infragistics.Win.UltraWinEditors.UltraTextEditor)
                {
                    ((Infragistics.Win.UltraWinEditors.UltraTextEditor)ctrl).CharacterCasing = CharacterCasing.Upper;
                }
                if (ctrl.HasChildren)
                    SearchControlAndSetEvents(ctrl);

            }

        }
        private void frmMasterRecommendationRestricction_Load(object sender, EventArgs e)
        {
            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);

            }


            #endregion
            OperationResult objOperationResult = new OperationResult();

            switch (_mode)
            {
                case ModeOperation.Total:
                    contextMenuStrip1.Items["mnuGridNuevo"].Enabled = true;
                    contextMenuStrip1.Items["mnuGridModificar"].Enabled = true;
                    contextMenuStrip1.Items["eliminarToolStripMenuItem"].Enabled = true;
                    break;
                case ModeOperation.Parcial:
                    contextMenuStrip1.Items["mnuGridNuevo"].Enabled = true;
                    contextMenuStrip1.Items["mnuGridModificar"].Enabled = false;
                    contextMenuStrip1.Items["eliminarToolStripMenuItem"].Enabled = false;
                    break;
                default:
                    break;
            }



            // Establecer el filtro inicial para los datos
            strFilterExpression = null;

            btnFilter_Click(sender, e);

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtName.Text)) Filters.Add("v_Name.Contains(\"" + txtName.Text.Trim() + "\")");
            Filters.Add("i_TypifyingId==" + _typifyingId);
            Filters.Add("i_IsDeleted==0");
            // Create the Filter Expression
            strFilterExpression = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    strFilterExpression = strFilterExpression + item + " && ";
                }
                strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
            }

            this.BindGrid();
        }

        private void BindGrid()
        {

            var objData = GetData(0, null, "v_Name ASC", strFilterExpression);

            grdData.DataSource = objData;
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<MasterRecommendationRestricctionList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objBL.GetMasterRecommendationRestricctionPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void mnuGridNuevo_Click(object sender, EventArgs e)
        {
            frmMasterRecommendationRestricctionEdicion frm = new frmMasterRecommendationRestricctionEdicion(_typifyingId, "New", "","");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void mnuGridModificar_Click(object sender, EventArgs e)
        {
            string strMasterRecommendationRestricctionId = grdData.Selected.Rows[0].Cells[0].Value.ToString();

            frmMasterRecommendationRestricctionEdicion frm = new frmMasterRecommendationRestricctionEdicion(_typifyingId, "Edit", strMasterRecommendationRestricctionId,"");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                // Delete the item
                string strMasterRecommendationRestricctionId = grdData.Selected.Rows[0].Cells[0].Value.ToString();
                _objBL.DeleteMasterRecommendationRestricction(ref objOperationResult, strMasterRecommendationRestricctionId, Globals.ClientSession.GetAsList());

                btnFilter_Click(sender, e);
            }
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            if (grdData.Selected.Rows.Count == 0)
                return;

            _masterRecommendationRestricctionId = grdData.Selected.Rows[0].Cells[0].Value.ToString();
            _masterRecommendationRestricctionName = grdData.Selected.Rows[0].Cells[1].Value.ToString();

            if (_nameInvokerForm == "frmMedicalConsult")
            {
                if (_restrictions == null)
                    _restrictions = new List<RestrictionList>();

                var existRestric = _restrictions.Find(p => p.v_MasterRestrictionId == _masterRecommendationRestricctionId && p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                if (existRestric != null)
                {
                    MessageBox.Show("Por favor seleccione otra Restricción.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Abrir Popup para setear rango de fechas
                var frm = new Operations.Popups.frmAddSelectRangeDateForRestriction();
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.Cancel)
                    return;

                _startDate = frm._startDate;
                _endDate = frm._endDate;

                var findResult = _restrictions.Find(p => p.v_MasterRestrictionId == _masterRecommendationRestricctionId);

                // La restriccion ya esta agregada
                if (findResult == null)
                {
                    var restriction = new RestrictionList();

                    restriction.v_RestrictionByDiagnosticId = Guid.NewGuid().ToString();
                    restriction.v_DiagnosticRepositoryId = Guid.NewGuid().ToString();
                    restriction.v_MasterRestrictionId = _masterRecommendationRestricctionId;
                    restriction.v_ServiceId = _serviceId;
                    restriction.v_RestrictionName = _masterRecommendationRestricctionName;
                    restriction.d_StartDateRestriction = _startDate;
                    restriction.d_EndDateRestriction = _endDate;
                    restriction.i_RecordStatus = (int)RecordStatus.Agregado;
                    restriction.i_RecordType = (int)RecordType.Temporal;

                    _restrictions.Add(restriction);
                }
                else
                {
                    if (findResult.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                    {
                        if (findResult.i_RecordType == (int)RecordType.NoTemporal)   // El registro Tiene in ID de BD
                        {
                            findResult.d_StartDateRestriction = _startDate;
                            findResult.d_EndDateRestriction = _endDate;
                            findResult.i_RecordStatus = (int)RecordStatus.Grabado;
                        }
                        else if (findResult.i_RecordType == (int)RecordType.Temporal)   // El registro tiene un ID temporal [GUID]
                        {
                            findResult.d_StartDateRestriction = _startDate;
                            findResult.d_EndDateRestriction = _endDate;
                            findResult.i_RecordStatus = (int)RecordStatus.Agregado;
                        }
                    }
                }

            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void grdData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    grdData.Rows[row.Index].Selected = true;
                    contextMenuStrip1.Items["mnuGridNuevo"].Enabled = true;
                    contextMenuStrip1.Items["mnuGridModificar"].Enabled = true;
                    contextMenuStrip1.Items["eliminarToolStripMenuItem"].Enabled = true;
                }
                else
                {
                    contextMenuStrip1.Items["mnuGridNuevo"].Enabled = true;
                    contextMenuStrip1.Items["mnuGridModificar"].Enabled = false;
                    contextMenuStrip1.Items["eliminarToolStripMenuItem"].Enabled = false;


                }

            }
        }

        private void grdData_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            btnDelete.Enabled = 
            btnEditar.Enabled = (grdData.Selected.Rows.Count > 0);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                // Delete the item
                string strMasterRecommendationRestricctionId = grdData.Selected.Rows[0].Cells["v_MasterRecommendationRestricctionId"].Value.ToString();
                _objBL.DeleteMasterRecommendationRestricction(ref objOperationResult, strMasterRecommendationRestricctionId, Globals.ClientSession.GetAsList());

                btnFilter_Click(sender, e);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmMasterRecommendationRestricctionEdicion frm = new frmMasterRecommendationRestricctionEdicion(_typifyingId, "New", "","");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string strMasterRecommendationRestricctionId = grdData.Selected.Rows[0].Cells[0].Value.ToString();

            frmMasterRecommendationRestricctionEdicion frm = new frmMasterRecommendationRestricctionEdicion(_typifyingId, "Edit", strMasterRecommendationRestricctionId,"");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                 // Get the filters from the UI
                List<string> Filters = new List<string>();
                if (!string.IsNullOrEmpty(txtName.Text)) Filters.Add("v_Name.Contains(\"" + txtName.Text.Trim() + "\")");
                Filters.Add("i_TypifyingId==" + _typifyingId);
                Filters.Add("i_IsDeleted==0");
                // Create the Filter Expression
                strFilterExpression = null;
                if (Filters.Count > 0)
                {
                    foreach (string item in Filters)
                    {
                        strFilterExpression = strFilterExpression + item + " && ";
                    }
                    strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
                }

                this.BindGrid();
            }
        }

    }
}
