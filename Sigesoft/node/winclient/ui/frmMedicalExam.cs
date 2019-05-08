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
using Sigesoft.Node.WinClient.UI.Configuration;
using System.Data.SqlClient;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmMedicalExam : Form
    {
        //OrganizationBL _objBL = new OrganizationBL();
        string strFilterExpression;
        MedicalExamBL _objMedicalExamBL = new MedicalExamBL();
        MedicalExamFieldsBL _objMedicalExamFieldsBL = new MedicalExamFieldsBL();
        string strMedicalExamId;
        string _ComponentName;
        int _RowIndexgrdDataMedicalExamFields;

        public frmMedicalExam()
        {
            InitializeComponent();
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

        private void frmAdministracion_Load(object sender, EventArgs e)
        {
            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);

            }


            #endregion
            OperationResult objOperationResult = new OperationResult();

            // Establecer el filtro inicial para los datos
            strFilterExpression = null;
            //Llenado de combos
            Utils.LoadComboTreeBoxList_(ddlCategoryId, BLL.Utils.GetSystemParameterForComboTreeBox(ref objOperationResult, 116, null), DropDownListAction.All);
            cbLine.Select();
            object listaLine = LlenarLines();
            cbLine.DataSource = listaLine;
            cbLine.DisplayMember = "v_Nombre";
            cbLine.ValueMember = "v_IdLinea";
            cbLine.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            cbLine.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.cbLine.DropDownWidth = 590;
            cbLine.DisplayLayout.Bands[0].Columns[0].Width = 20;
            cbLine.DisplayLayout.Bands[0].Columns[1].Width = 550;

            btnFilter_Click(sender, e);
           
        }

        private object LlenarLines()
        {
            #region Conexion SAMBHS
            ConexionSambhs conectaConexionSambhs = new ConexionSambhs(); conectaConexionSambhs.openSambhs();
            var cadenasam = "select v_Nombre, v_IdLinea  from linea where i_Eliminado=0";
            var comando = new SqlCommand(cadenasam, connection: conectaConexionSambhs.conectarSambhs);
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
            conectaConexionSambhs.closeSambhs();
            #endregion

            return objListaLineas;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            using (new LoadingClass.PleaseWait(this.Location, "Cargando..."))
            {
                 //Get the filters from the UI
                List<string> Filters = new List<string>();
                if (ddlCategoryId.SelectedNode.Tag.ToString() != "-1") Filters.Add("i_CategoryId==" + ddlCategoryId.SelectedNode.Tag);
                if (!string.IsNullOrEmpty(txtName.Text)) Filters.Add("v_Name.Contains(\"" + txtName.Text.Trim() + "\")");
                if (!string.IsNullOrEmpty(cbLine.Text)) Filters.Add("v_IdUnidadProductiva.Contains(\"" + txtUnidadProdId.Text.Trim() + "\")");
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

                this.BindGridMedicalExam();
            };
        }

        private void BindGridMedicalExam()
        {
            var objData = GetData(0, null, "v_Name ASC", strFilterExpression);

            grdDataMedicalExam.DataSource = objData;
            lblRecordCountMedicalExam.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            if (grdDataMedicalExam.Rows.Count > 0)
                grdDataMedicalExam.Rows[0].Selected = true;
        }

        private void BindGridMedicalExamFields(string strMedicalExamId)
        {
            strFilterExpression = "v_ComponentId ==" + "\"" + strMedicalExamId + "\"";
            var objData = GetDataMedicalExamFields(0, null, "v_Group , v_TextLabel ASC", strFilterExpression);

            grdDataMedicalExamFields.DataSource = objData;
            lblRecordCountMedicalExamFields.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            //if (grdDataMedicalExamFields.Rows.Count != 0)
            //    grdDataMedicalExamFields.Rows[0].Selected = true;
        }

        private List<MedicalExamFieldsList> GetDataMedicalExamFields(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objMedicalExamFieldsBL.GetMedicalExamFieldsPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }       

        private List<MedicalExamList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objMedicalExamBL.GetMedicalExamPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }       

        #region "Medical Exam"

        private void mnuGridNewMedicalExam_Click(object sender, EventArgs e)
        {
            frmMedicalExamEdicion frm = new frmMedicalExamEdicion("","New", "");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void mnuGridEditMedicalExam_Click(object sender, EventArgs e)
        {
            string strMedicalExamId = grdDataMedicalExam.Selected.Rows[0].Cells[0].Value.ToString();

            frmMedicalExamEdicion frm = new frmMedicalExamEdicion(strMedicalExamId, "Edit", "");
            frm.ShowDialog();

            //if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            //{
                // Refrescar grilla
                btnFilter_Click(sender, e);
            //}
        }

        private void mnuGridDeleteMedicalExam_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "¡ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                //Buscar si el componente C está en un protocolo P. Si está en un protocolo P, NO SE PUEDE ELIMINAR EL COMPONENTE C. En cualquier otro caso -> EL COMPONENTE C SE PUEDE ELIMINAR.
                ProtocolBL objProtocolBL = new ProtocolBL();

                if (objProtocolBL.ValidateComponentElimination(ref objOperationResult, strMedicalExamId) == true)
                {
                    MessageBox.Show("Este registro no se puede eliminar porque está referenciado en un protocolo.", "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var _objData = _objMedicalExamFieldsBL.GetMedicalExamFieldsPagedAndFiltered(ref objOperationResult, 0, null, "", "v_ComponentId ==" + "\"" + strMedicalExamId + "\"");

                 if (_objData.Count > 0)
                 {
                     MessageBox.Show("Este registro no se puede eliminar porque tiene campos registrados.", "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 }
                 else
                 {
                     _objMedicalExamBL.DeleteMedicalExam(ref objOperationResult, strMedicalExamId, Globals.ClientSession.GetAsList());
                     btnFilter_Click(sender, e);
                 }               
            }
        }

        //private void grdDataMedicalExam_Click(object sender, EventArgs e)
        //{
        //    if (grdDataMedicalExam.Rows.Count == 0) return;
        //    if (grdDataMedicalExam.Selected.Rows.Count == 0) return;
                       
        //     strMedicalExamId = grdDataMedicalExam.Selected.Rows[0].Cells[0].Value.ToString();

        //     BindGridMedicalExamFields(strMedicalExamId);
        //}

        #endregion

        #region "Medical Exam Field"

        private void mnuGridNewMedicalExamField_Click(object sender, EventArgs e)
        {
            frmMedicalExamFieldEdicion frm = new frmMedicalExamFieldEdicion(strMedicalExamId, "New","");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                BindGridMedicalExamFields(strMedicalExamId);
            }
        }

        private void mnuGridEditMedicalExamField_Click(object sender, EventArgs e)
        {
            string strMedicalExamFieldId = grdDataMedicalExamFields.Selected.Rows[0].Cells[0].Value.ToString();

            frmMedicalExamFieldEdicion frm = new frmMedicalExamFieldEdicion(strMedicalExamId, "Edit", strMedicalExamFieldId);
            frm.ShowDialog();

            //if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            //{
                // Refrescar grilla
                BindGridMedicalExamFields(strMedicalExamId);
            //}

            grdDataMedicalExamFields.Rows[_RowIndexgrdDataMedicalExamFields].Selected = true;
        }
     
        private void mnuGridDeleteMedicalExamField_Click(object sender, EventArgs e)
        {
            MedicalExamFieldValuesBL _objMedicalExamFieldValuesBL = new MedicalExamFieldValuesBL();
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "¡ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                // Delete the item
                //Buscar si el componente C está en un protocolo P.Si está en un protocolo -> NO SE PUEDE ELIMINAR EL CAMPO X.En cualquier otro caso -> EL CAMPO X SE PUEDE ELIMINAR.
                ProtocolBL objProtocolBL = new ProtocolBL();

                if (objProtocolBL.ValidateComponentElimination(ref objOperationResult, strMedicalExamId) == true)
                {
                    MessageBox.Show("Este registro no se puede eliminar porque está referenciado en un protocolo.", "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string strMedicalExamFieldId = grdDataMedicalExamFields.Selected.Rows[0].Cells[0].Value.ToString();

                var _objData = _objMedicalExamFieldValuesBL.GetMedicalExamFieldValuesPagedAndFiltered(ref objOperationResult, 0, null, "", "v_MedicalExamFieldsId ==" + "\"" + strMedicalExamFieldId + "\"");
                if (_objData.Count > 0)
                {
                    MessageBox.Show("Este registro no se puede eliminar porque tiene campos registrados.", "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    _objMedicalExamFieldsBL.DeleteMedicalExamFields(ref objOperationResult, strMedicalExamId, strMedicalExamFieldId, Globals.ClientSession.GetAsList());

                    BindGridMedicalExamFields(strMedicalExamId);
                }
            }
        }

        #endregion

        private void grdDataMedicalExam_MouseDown(object sender, MouseEventArgs e)
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
                     grdDataMedicalExam.Rows[row.Index].Selected = true;
                     contextMenuMedicalExam.Items["mnuGridNewMedicalExam"].Enabled = true;
                     contextMenuMedicalExam.Items["mnuGridEditMedicalExam"].Enabled = true;
                     contextMenuMedicalExam.Items["mnuGridDeleteMedicalExam"].Enabled = true;                    
                }
                else
                {
                    contextMenuMedicalExam.Items["mnuGridNewMedicalExam"].Enabled = true;
                    contextMenuMedicalExam.Items["mnuGridEditMedicalExam"].Enabled = false;
                    contextMenuMedicalExam.Items["mnuGridDeleteMedicalExam"].Enabled = false;
                }

            }
            if (e.Button == MouseButtons.Left)
            {
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    grdDataMedicalExam.Rows[row.Index].Selected = true;
                    strMedicalExamId = grdDataMedicalExam.Selected.Rows[0].Cells[0].Value.ToString();
                    _ComponentName = grdDataMedicalExam.Selected.Rows[0].Cells[1].Value.ToString();
                    BindGridMedicalExamFields(strMedicalExamId);
                }
                
            }          
        }

        private void grdDataMedicalExamFields_MouseDown(object sender, MouseEventArgs e)
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
                    _RowIndexgrdDataMedicalExamFields = row.Index;
                    grdDataMedicalExamFields.Rows[row.Index].Selected = true;
                    contextMenuMedicalExamFields.Items["mnuGridNewMedicalExamField"].Enabled = true;
                    contextMenuMedicalExamFields.Items["mnuGridEditMedicalExamField"].Enabled = true;
                    contextMenuMedicalExamFields.Items["mnuGridDeleteMedicalExamField"].Enabled = true;
                    contextMenuMedicalExamFields.Items["importarToolStripMenuItem"].Enabled = true;
                }
                else
                {
                    if (grdDataMedicalExam.Selected.Rows.Count != 0)
                    {
                        contextMenuMedicalExamFields.Items["mnuGridNewMedicalExamField"].Enabled = true;
                        contextMenuMedicalExamFields.Items["importarToolStripMenuItem"].Enabled = true;
                    }
                    else
                    {
                        contextMenuMedicalExamFields.Items["mnuGridNewMedicalExamField"].Enabled = false;
                        contextMenuMedicalExamFields.Items["importarToolStripMenuItem"].Enabled = false;
                    }
                    
                    contextMenuMedicalExamFields.Items["mnuGridEditMedicalExamField"].Enabled = false;
                    contextMenuMedicalExamFields.Items["mnuGridDeleteMedicalExamField"].Enabled = false;
                }

            } 
        }

        private void importarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImportMedicalExamField frm = new frmImportMedicalExamField(strMedicalExamId, _ComponentName);
            frm.ShowDialog();
            BindGridMedicalExamFields(strMedicalExamId);
        }

        private void grdDataMedicalExam_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grdDataMedicalExam.Selected.Rows.Count == 0)
                return;
            
            strMedicalExamId = grdDataMedicalExam.Selected.Rows[0].Cells[0].Value.ToString();
            _ComponentName = grdDataMedicalExam.Selected.Rows[0].Cells[1].Value.ToString();
            BindGridMedicalExamFields(strMedicalExamId);
           
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void grdDataMedicalExam_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void grdDataMedicalExamFields_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void lblRecordCountMedicalExamFields_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmMedicalExamEdicion frm = new frmMedicalExamEdicion("", "New", "");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }


        
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string strMedicalExamId = grdDataMedicalExam.Selected.Rows[0].Cells["v_MedicalExamId"].Value.ToString();
            string orden = grdDataMedicalExam.Selected.Rows[0].Cells["i_UIIndex"].Value.ToString();
            frmMedicalExamEdicion frm = new frmMedicalExamEdicion(strMedicalExamId, "Edit", orden);
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "¡ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                //Buscar si el componente C está en un protocolo P. Si está en un protocolo P, NO SE PUEDE ELIMINAR EL COMPONENTE C. En cualquier otro caso -> EL COMPONENTE C SE PUEDE ELIMINAR.
                ProtocolBL objProtocolBL = new ProtocolBL();

                if (objProtocolBL.ValidateComponentElimination(ref objOperationResult, strMedicalExamId) == true)
                {
                    MessageBox.Show("Este registro no se puede eliminar porque está referenciado en un protocolo.", "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var _objData = _objMedicalExamFieldsBL.GetMedicalExamFieldsPagedAndFiltered(ref objOperationResult, 0, null, "", "v_ComponentId ==" + "\"" + strMedicalExamId + "\"");

                if (_objData.Count > 0)
                {
                    MessageBox.Show("Este registro no se puede eliminar porque tiene campos registrados.", "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    _objMedicalExamBL.DeleteMedicalExam(ref objOperationResult, strMedicalExamId, Globals.ClientSession.GetAsList());
                    btnFilter_Click(sender, e);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmMedicalExamFieldEdicion frm = new frmMedicalExamFieldEdicion(strMedicalExamId, "New", "");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                BindGridMedicalExamFields(strMedicalExamId);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (grdDataMedicalExamFields.Selected.Rows.Count == 0) return;
            string strMedicalExamFieldId = grdDataMedicalExamFields.Selected.Rows[0].Cells[0].Value.ToString();

            frmMedicalExamFieldEdicion frm = new frmMedicalExamFieldEdicion(strMedicalExamId, "Edit", strMedicalExamFieldId);
            frm.ShowDialog();

            _RowIndexgrdDataMedicalExamFields = grdDataMedicalExamFields.Selected.Rows[0].Index;

            //if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            //{
                // Refrescar grilla
                BindGridMedicalExamFields(strMedicalExamId);
            //}

            grdDataMedicalExamFields.Rows[_RowIndexgrdDataMedicalExamFields].Selected = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MedicalExamFieldValuesBL _objMedicalExamFieldValuesBL = new MedicalExamFieldValuesBL();
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "¡ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                // Delete the item
                //Buscar si el componente C está en un protocolo P.Si está en un protocolo -> NO SE PUEDE ELIMINAR EL CAMPO X.En cualquier otro caso -> EL CAMPO X SE PUEDE ELIMINAR.
                ProtocolBL objProtocolBL = new ProtocolBL();

                if (objProtocolBL.ValidateComponentElimination(ref objOperationResult, strMedicalExamId) == true)
                {
                    MessageBox.Show("Este registro no se puede eliminar porque está referenciado en un protocolo.", "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string strMedicalExamFieldId = grdDataMedicalExamFields.Selected.Rows[0].Cells[0].Value.ToString();

                var _objData = _objMedicalExamFieldValuesBL.GetMedicalExamFieldValuesPagedAndFiltered(ref objOperationResult, 0, null, "", "v_MedicalExamFieldsId ==" + "\"" + strMedicalExamFieldId + "\"");
                if (_objData.Count > 0)
                {
                    MessageBox.Show("Este registro no se puede eliminar porque tiene campos registrados.", "¡ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    _objMedicalExamFieldsBL.DeleteMedicalExamFields(ref objOperationResult, strMedicalExamId, strMedicalExamFieldId, Globals.ClientSession.GetAsList());

                    BindGridMedicalExamFields(strMedicalExamId);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmImportMedicalExamField frm = new frmImportMedicalExamField(strMedicalExamId, _ComponentName);
            frm.ShowDialog();
            BindGridMedicalExamFields(strMedicalExamId);
        
        }

        private void ddlCategoryId_Click(object sender, EventArgs e)
        {

        }

        private void cbLine_RowSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e)
        {
            #region Conexion SAMBHS
            ConexionSambhs conectaConexionSambhs = new ConexionSambhs(); conectaConexionSambhs.openSambhs();
            var cadenasam = "select v_IdLinea  from linea where i_Eliminado=0 and v_Nombre ='" + cbLine.Text + "'";
            var comando = new SqlCommand(cadenasam, connection: conectaConexionSambhs.conectarSambhs);
            var lector = comando.ExecuteReader();
            string LineId = "";
            List<ListaLineas> objListaLineas = new List<ListaLineas>();

            while (lector.Read())
            {
                LineId = lector.GetValue(0).ToString();
            }
            lector.Close();
            conectaConexionSambhs.closeSambhs();
            #endregion

            txtUnidadProdId.Text = LineId;
        }

    }
}
