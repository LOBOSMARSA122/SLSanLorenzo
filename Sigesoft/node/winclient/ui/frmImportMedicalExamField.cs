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
    public partial class frmImportMedicalExamField : Form
    {
        private string _ComponentFieldId = string.Empty;
        private string _GroupName;
        List<ComponentFieldsList> _TempComponentFieldsList = new List<ComponentFieldsList>();
        ComponentFieldsList _objComponentFields = new ComponentFieldsList();
        List<componentfieldsDto> _componentfieldsListDto = null;
        List<componentfieldsDto> _componentfieldsDtoDelete = null;
        List<componentfieldsDto> _componentfieldsListUpdate = null;
        componentfieldsDto _objcomponentfieldsDto;

        MedicalExamFieldsBL _objMedicalExamFieldsBL = new MedicalExamFieldsBL();

        ComponentFieldsList _objComponentFieldsamc = new ComponentFieldsList();

        private int _IndexList;

        string _ComponentId;
        public frmImportMedicalExamField(string pstrComponentId, string pstrComponentName)
        {
            InitializeComponent();
            this.Text = "Componente :" + this.Text + " (" + pstrComponentName + ")";
            _ComponentId = pstrComponentId;
        }

        private void frmImportMedicalExamField_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlComponentId, "Value1", "Id", BLL.Utils.GetComponents(ref objOperationResult), DropDownListAction.Select);

            //try
            //{
                _TempComponentFieldsList = _objMedicalExamFieldsBL.GetComponentFieldsagedAndFiltered(ref objOperationResult, 0, null, "", "v_ComponentId==" + "\"" + _ComponentId + "\"");
                grdDataCommponentField.DataSource = _TempComponentFieldsList;

            //    MessageBox.Show(_TempComponentFieldsList[0].v_ComponentFieldId);
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.ToString());
            //}
           

        }

        private void ddlComponentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();


            for (int i = 0; i < checkedListBox1.Items.Count -1; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            checkedListBox1.DataSource = BLL.Utils.GetFieldsByComponent(ref objOperationResult, ddlComponentId.SelectedValue.ToString(),"Parcial");
            checkedListBox1.DisplayMember = "Value1";
            checkedListBox1.ValueMember = "Id";
          
        }

        private void btnSave_Click(object sender, EventArgs e)
       {
             OperationResult objOperationResult = new OperationResult();
            //if (_Mode == "New")
            //{

            //}
            //else if (_Mode == "Edit")
            //{
             _componentfieldsListDto = new List<componentfieldsDto>();
             _componentfieldsListUpdate = new List<componentfieldsDto>();
             _componentfieldsDtoDelete = new List<componentfieldsDto>();
                //Temporal ComponentFields
                foreach (var item in _TempComponentFieldsList)
                {

                    //Add
                    if (item.i_RecordType == (int)RecordType.Temporal && item.i_RecordStatus == (int)RecordStatus.Agregado)
                    {
                        componentfieldsDto componentfieldsDto = new componentfieldsDto();

                        componentfieldsDto.v_ComponentId = item.v_ComponentId;
                        componentfieldsDto.v_ComponentFieldId = item.v_ComponentFieldId;
                        componentfieldsDto.v_Group = item.v_Group;

                        _componentfieldsListDto.Add(componentfieldsDto);
                    }

                    // Update
                    if (item.i_RecordType == (int)RecordType.NoTemporal && (item.i_RecordStatus == (int)RecordStatus.Modificado || item.i_RecordStatus == (int)RecordStatus.Grabado))
                    {
                        componentfieldsDto componentfieldsDto = new componentfieldsDto();

                        componentfieldsDto.v_ComponentId = item.v_ComponentId;
                        componentfieldsDto.v_ComponentFieldId = item.v_ComponentFieldId;
                        componentfieldsDto.v_Group = item.v_Group;

                        _componentfieldsListUpdate.Add(componentfieldsDto);
                    }

                    //Delete
                    if (item.i_RecordType == (int)RecordType.NoTemporal && item.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                    {
                        componentfieldsDto componentfieldsDto = new componentfieldsDto();
                        componentfieldsDto.v_ComponentId = item.v_ComponentId;
                        componentfieldsDto.v_ComponentFieldId = item.v_ComponentFieldId;

                        _componentfieldsDtoDelete.Add(componentfieldsDto);
                    }
                //}
                }
                _objMedicalExamFieldsBL.AddComponentFields(ref objOperationResult,
                                            _componentfieldsListDto,
                                            _componentfieldsListUpdate,
                                            _componentfieldsDtoDelete,
                                            Globals.ClientSession.GetAsList());
                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else  // Operación con error
                {
                    MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Se queda en el formulario.
                }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnMoveCommponet_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            ComponentFieldsList objComponentFields;


            if (checkedListBox1.Items.Count == 0) return;

            //Si la lista temporal es null se la setea con una lista vacia
            if (_TempComponentFieldsList == null)
            {
                _TempComponentFieldsList = new List<ComponentFieldsList>();
            }

            frmGroupMedicalExamField frm = new frmGroupMedicalExamField("");
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {

                for (int i = 0; i < checkedListBox1.CheckedItems.Count ; i++)
                {
                    objComponentFields = new ComponentFieldsList();

                    KeyValueDTO obj = (KeyValueDTO)checkedListBox1.CheckedItems[i];
                    string ComponentFieldId = obj.Id;
                    string ComponentFieldName = obj.Value1;
                    //Busco en la lista temporal si ya se agrego el item seleccionado
                    var findResult = _TempComponentFieldsList.Find(p => p.v_ComponentFieldId == ComponentFieldId);
                    if (findResult == null)
                    {
                        objComponentFields.v_ComponentId = _ComponentId;
                        objComponentFields.v_ComponentFieldId = ComponentFieldId;
                        objComponentFields.v_Group = frm._GroupName;
                        objComponentFields.v_TextLabel = ComponentFieldName;
                        objComponentFields.i_RecordStatus = (int)RecordStatus.Agregado;
                        objComponentFields.i_RecordType = (int)RecordType.Temporal;
                        _TempComponentFieldsList.Add(objComponentFields);
                    }
                    else
                    {
                        if (findResult.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                        {
                            if (findResult.i_RecordType == (int)RecordType.NoTemporal)// El registro Tiene in ID de BD
                            {
                                findResult.v_ComponentId = _ComponentId;
                                findResult.v_ComponentFieldId = ComponentFieldId;
                                findResult.v_Group = frm._GroupName;
                                objComponentFields.v_TextLabel = ComponentFieldName;
                                findResult.i_RecordStatus = (int)RecordStatus.Grabado;
                            }
                            else if (findResult.i_RecordType == (int)RecordType.Temporal) // El registro tiene un ID temporal [GUID]
                            {
                                findResult.v_ComponentId = _ComponentId;
                                findResult.v_ComponentFieldId = ComponentFieldId;
                                findResult.v_Group = frm._GroupName;
                                objComponentFields.v_TextLabel = ComponentFieldName;
                                findResult.i_RecordStatus = (int)RecordStatus.Agregado;
                            }
                        }
                        else
                        {
                            MessageBox.Show("El campo : " + findResult.v_TextLabel + " ya existe.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //return;
                        }
                    }                   
                }
                var dataList = _TempComponentFieldsList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                // Cargar grilla
                grdDataCommponentField.DataSource = new ComponentFieldsList();
                grdDataCommponentField.DataSource = dataList;
                grdDataCommponentField.Refresh();

            }
        }

        private void btnDeleteCommponent_Click(object sender, EventArgs e)
        {
            if (_ComponentFieldId != "")
            {
                var findResult = _TempComponentFieldsList.Find(p => p.v_ComponentId == _ComponentId && p.v_ComponentFieldId == _ComponentFieldId);
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;

                var dataList = _TempComponentFieldsList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdDataCommponentField.DataSource = new ComponentFieldsList();
                grdDataCommponentField.DataSource = dataList;
                grdDataCommponentField.Refresh();
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string GroupName = grdDataCommponentField.Rows[0].Cells[2].Value.ToString();
            frmGroupMedicalExamField frm = new frmGroupMedicalExamField(GroupName);
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (_objComponentFieldsamc.i_RecordType == (int)RecordType.Temporal)
                {
                    _objComponentFieldsamc.i_RecordStatus = (int)RecordStatus.Agregado;
                }
                else if (_objComponentFieldsamc.i_RecordType == (int)RecordType.NoTemporal)
                {
                    _objComponentFieldsamc.i_RecordStatus = (int)RecordStatus.Modificado;
                }


                _objComponentFieldsamc.v_Group = frm._GroupName;

                _TempComponentFieldsList[_IndexList] = _objComponentFieldsamc;

                // Cargar grilla
                grdDataCommponentField.DataSource = new ComponentFieldsList();
                grdDataCommponentField.DataSource = _TempComponentFieldsList;
                grdDataCommponentField.Refresh();
            }
        }

        private void grdDataCommponentField_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    btnDeleteCommponent.Enabled = true;
                    grdDataCommponentField.Rows[row.Index].Selected = true;
                    _ComponentFieldId = grdDataCommponentField.Selected.Rows[0].Cells[0].Value.ToString();
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    grdDataCommponentField.Rows[row.Index].Selected = true;
                    contextMenuImport.Items["modificarToolStripMenuItem"].Enabled = true;
                    _GroupName = grdDataCommponentField.Rows[0].Cells[2].Value.ToString();
                    _ComponentFieldId = grdDataCommponentField.Selected.Rows[0].Cells[0].Value.ToString();
                    _objComponentFieldsamc = _TempComponentFieldsList.FindAll(p => p.v_ComponentId == _ComponentId && p.v_ComponentFieldId == _ComponentFieldId).FirstOrDefault();
                    _IndexList = _TempComponentFieldsList.FindIndex(p => p.v_ComponentId == _ComponentId && p.v_ComponentFieldId == _ComponentFieldId);
                }
                else
                {
                    contextMenuImport.Items["modificarToolStripMenuItem"].Enabled = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
             string GroupName = grdDataCommponentField.Rows[0].Cells[2].Value.ToString();
            frmGroupMedicalExamField frm = new frmGroupMedicalExamField(GroupName);
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (_objComponentFieldsamc.i_RecordType == (int)RecordType.Temporal)
                {
                    _objComponentFieldsamc.i_RecordStatus = (int)RecordStatus.Agregado;
                }
                else if (_objComponentFieldsamc.i_RecordType == (int)RecordType.NoTemporal)
                {
                    _objComponentFieldsamc.i_RecordStatus = (int)RecordStatus.Modificado;
                }


                _objComponentFieldsamc.v_Group = frm._GroupName;

                _TempComponentFieldsList[_IndexList] = _objComponentFieldsamc;

                // Cargar grilla
                grdDataCommponentField.DataSource = new ComponentFieldsList();
                grdDataCommponentField.DataSource = _TempComponentFieldsList;
                grdDataCommponentField.Refresh();
        }

    }
}
}
