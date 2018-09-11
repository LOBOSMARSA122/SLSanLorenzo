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
    public partial class frmOccupationalHistory : Form
    {
        string _Mode;
        public string _HistoryId;
        private string _WorkstationDangersId = string.Empty;
        private string _TypeofEEPId = string.Empty;
        private string _TypeofEEPName;
        private double _Percentage;
        private int _IndexListEPP;
        string _PacientId;

        HistoryBL _objHistoryBL = new HistoryBL();
        historyDto _objhistoryDto;
                
        List<WorkstationDangersList> _TempWorkstationDangersList = new List<WorkstationDangersList>();
        
        List<TypeOfEEPList> _TempTypeOfEEPList = new List<TypeOfEEPList>();

        List<workstationdangersDto> _workstationdangersListDto = null;
        List<workstationdangersDto> _workstationdangersListDtoDelete = null;
        List<workstationdangersDto> _workstationdangersListDtoUpdate = null;

        List<typeofeepDto> _typeofeepListDto = null;
        List<typeofeepDto> _typeofeepListDtoDelete = null;
        List<typeofeepDto> _typeofeepListUpdate = null;

        TypeOfEEPList _objTypeOfEEPamc = new TypeOfEEPList();

        public frmOccupationalHistory(string pstrMode , string pstrHistoryId, string pstrPacientId, bool Validation)
        {
            OperationResult objOperationResult = new OperationResult();

            PacientBL objPacientBL = new PacientBL();
            PacientList objpersonDto = new PacientList();

            InitializeComponent();

            objpersonDto = objPacientBL.GetPacient(ref objOperationResult, pstrPacientId, null);
            this.Text = this.Text + "  (" + objpersonDto.v_FirstName + " " + objpersonDto.v_FirstLastName + " " + objpersonDto.v_SecondLastName + ")";
            
            _Mode = pstrMode;
            _HistoryId = pstrHistoryId;
            _PacientId = pstrPacientId;

            Utils.LoadDropDownList(ddlTypeOperationId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 204, null), DropDownListAction.Select);
            
            //frmHistory frm = new frmHistory(_PacientId);
            //bool ResultValidation = false;
            //ResultValidation = frm._Validation;

            //if (Validation)
            //{
            //    btnSave.Enabled = true;
            //}
            //else
            //{
            //    btnSave.Enabled = false;
            //}
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

        private void frmOccupationalHistory_Load(object sender, EventArgs e)
        {
            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);

            }


            #endregion
            OperationResult objOperationResult = new OperationResult();
            HistoryBL objHistoryBL = new HistoryBL();
           
            LoadTreeDangers(145);
            LoadTreeEPP(146);

            dtpDateTimeStar.Value = dtpDateTimeStar.Value.AddMonths(-1);

            DateTime now = DateTime.Now;
            dtpDateTimeStar.Value = now.AddDays(1 - now.Day);

            DateTime now2 = DateTime.Now;
            dptDateTimeEnd.Value = now2.AddDays(1 - now2.Day);

            if (_Mode == "New")
            {
                // Additional logic here.

            }
            else if (_Mode == "Edit")
            {
                _objhistoryDto = new historyDto();

                _objhistoryDto = _objHistoryBL.GetHistory(ref objOperationResult, _HistoryId);

                dtpDateTimeStar.Value = _objhistoryDto.d_StartDate.Value;
                dptDateTimeEnd.Value = _objhistoryDto.d_EndDate.Value;
                txtOrganization.Text = _objhistoryDto.v_Organization;
                txtTypeActivity.Text = _objhistoryDto.v_TypeActivity;
                txtOccupation.Text = _objhistoryDto.v_workstation;
                ddlTypeOperationId.SelectedValue = _objhistoryDto.i_TypeOperationId.ToString();
                txtGeographicalHeight.Text = _objhistoryDto.i_GeografixcaHeight == 0 ? "" : _objhistoryDto.i_GeografixcaHeight.ToString();
                txtActividad.Text = _objhistoryDto.v_ActividadEmpresa;
                if (_objhistoryDto.i_TrabajoActual != null)
                {
                    if ( _objhistoryDto.i_TrabajoActual == 1)
                    {
                        chkPuestoActual.Checked = true; 
                    }
                    else
                    {
                        chkPuestoActual.Checked = false; 
                    }                   
                }
                

                _TempWorkstationDangersList = objHistoryBL.GetWorkstationDangersagedAndFiltered(ref objOperationResult, 0, null, "v_ParentName ASC", "", _HistoryId);
                grdDataDangers.DataSource = _TempWorkstationDangersList;

                _TempTypeOfEEPList = objHistoryBL.GetTypeOfEEPPagedAndFiltered(ref objOperationResult, 0, null, "v_TypeofEEPName ASC", "", _HistoryId);
                grdDataEPP.DataSource = _TempTypeOfEEPList;
            }
            
        }

        private TreeNode SelectChildrenRecursive(TreeNode tn, string searchValue)
        {
            if (tn.Name == searchValue)
            {
                return tn;
            }
            else
            {
                //tn.Collapse();
            }
            if (tn.Nodes.Count > 0)
            {
                TreeNode objNode = new TreeNode();

                foreach (TreeNode tnC in tn.Nodes)
                {
                    objNode = SelectChildrenRecursive(tnC, searchValue);
                    if (objNode != null) return objNode;
                }
            }
            return null;
        }
                     
        private void btnSave_Click(object sender, EventArgs e)
        {
             OperationResult objOperationResult = new OperationResult();
             _workstationdangersListDto = new List<workstationdangersDto>();
             _typeofeepListDto = new List<typeofeepDto>();

             _workstationdangersListDtoDelete = new List<workstationdangersDto>();
             _typeofeepListDtoDelete = new List<typeofeepDto>();

             _typeofeepListUpdate = new List<typeofeepDto>();
             _workstationdangersListDtoUpdate = new List<workstationdangersDto>();


            HistoryBL objHistoryBL = new HistoryBL();

            if (ultraValidator1.Validate(true, false).IsValid)
            {
                if (txtOrganization.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Organización.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtTypeActivity.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Tipo de Actividad.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtOccupation.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Puesto de Trabajo.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //if (txtGeographicalHeight.Text.Trim() == string.Empty)
                //{
                //    MessageBox.Show("Por favor ingrese un nombre apropiado para Altura Geográfica", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                if (_Mode == "New")
                {
                    _objhistoryDto = new historyDto();

                    _objhistoryDto.v_PersonId = _PacientId;
                    _objhistoryDto.d_StartDate = dtpDateTimeStar.Value.Date;
                    _objhistoryDto.d_EndDate = dptDateTimeEnd.Value.Date;
                    _objhistoryDto.v_Organization = txtOrganization.Text;
                    _objhistoryDto.v_TypeActivity = txtTypeActivity.Text;
                    _objhistoryDto.v_workstation = txtOccupation.Text;
                    _objhistoryDto.i_TrabajoActual = chkPuestoActual.Checked == true? 1 :0;
                    _objhistoryDto.i_TypeOperationId = int.Parse(ddlTypeOperationId.SelectedValue.ToString());
                    _objhistoryDto.i_GeografixcaHeight = txtGeographicalHeight.Text == "" ? 0 : int.Parse(txtGeographicalHeight.Text.ToString());
                    _objhistoryDto.v_ActividadEmpresa = txtActividad.Text;
                 _HistoryId = objHistoryBL.AddHistory(ref objOperationResult,_TempWorkstationDangersList,_TempTypeOfEEPList, _objhistoryDto, Globals.ClientSession.GetAsList());

                }
                else if (_Mode == "Edit")
                {
                    _objhistoryDto.v_PersonId = _PacientId;
                    _objhistoryDto.d_StartDate = dtpDateTimeStar.Value.Date;
                    _objhistoryDto.d_EndDate = dptDateTimeEnd.Value.Date;
                    _objhistoryDto.v_Organization = txtOrganization.Text;
                    _objhistoryDto.v_TypeActivity = txtTypeActivity.Text;
                    _objhistoryDto.v_workstation = txtOccupation.Text;
                    _objhistoryDto.i_TrabajoActual = chkPuestoActual.Checked == true ? 1 : 0;
                    _objhistoryDto.i_TypeOperationId = int.Parse(ddlTypeOperationId.SelectedValue.ToString());
                    _objhistoryDto.i_GeografixcaHeight = txtGeographicalHeight.Text == "" ? 0 : int.Parse(txtGeographicalHeight.Text.ToString());
                    _objhistoryDto.v_ActividadEmpresa = txtActividad.Text;
                    //Temporal Peligros
                    foreach (var item in _TempWorkstationDangersList)
                    {
                        //Add
                        if (item.i_RecordType == (int)RecordType.Temporal && item.i_RecordStatus == (int)RecordStatus.Agregado)
                        {
                            workstationdangersDto workstationdangersDto = new workstationdangersDto();

                            workstationdangersDto.i_DangerId = item.i_DangerId;

                            workstationdangersDto.v_TimeOfExposureToNoise = item.v_TimeOfExposureToNoise;
                            workstationdangersDto.i_NoiseLevel = item.i_NoiseLevel;
                            workstationdangersDto.i_NoiseSource = item.i_NoiseSource;

                            _workstationdangersListDto.Add(workstationdangersDto);

                        }

                        // Update
                        if (item.i_RecordType == (int)RecordType.NoTemporal && (item.i_RecordStatus == (int)RecordStatus.Modificado || item.i_RecordStatus == (int)RecordStatus.Grabado))
                        {
                            workstationdangersDto workstationdangersDto = new workstationdangersDto();
                            workstationdangersDto.v_WorkstationDangersId = item.v_WorkstationDangersId;
                            workstationdangersDto.i_DangerId = item.i_DangerId;

                            workstationdangersDto.v_TimeOfExposureToNoise = item.v_TimeOfExposureToNoise;
                            workstationdangersDto.i_NoiseLevel = item.i_NoiseLevel;
                            workstationdangersDto.i_NoiseSource = item.i_NoiseSource;

                            _workstationdangersListDtoUpdate.Add(workstationdangersDto);

                        }

                        //Delete
                        if (item.i_RecordType == (int)RecordType.NoTemporal && item.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                        {
                            workstationdangersDto workstationdangersDto = new workstationdangersDto();
                            workstationdangersDto.v_WorkstationDangersId = item.v_WorkstationDangersId;
                            workstationdangersDto.i_DangerId = item.i_DangerId;
                            _workstationdangersListDtoDelete.Add(workstationdangersDto);
                        }
                    }

                    //Temporal EPP

                    foreach (var item in _TempTypeOfEEPList)
                    {
                        
                        //Add
                        if (item.i_RecordType == (int)RecordType.Temporal && item.i_RecordStatus == (int)RecordStatus.Agregado)
                        {
                            typeofeepDto typeofeepDto = new typeofeepDto();

                            typeofeepDto.i_TypeofEEPId = item.i_TypeofEEPId;
                            typeofeepDto.r_Percentage = item.r_Percentage;

                            _typeofeepListDto.Add(typeofeepDto);
                        }

                        // Update
                        if (item.i_RecordType == (int)RecordType.NoTemporal && (item.i_RecordStatus == (int)RecordStatus.Modificado || item.i_RecordStatus == (int)RecordStatus.Grabado))
                        {
                            typeofeepDto typeofeepDto = new typeofeepDto();

                            typeofeepDto.v_TypeofEEPId = item.v_TypeofEEPId;
                            typeofeepDto.r_Percentage = item.r_Percentage;
                            _typeofeepListUpdate.Add(typeofeepDto);

                        }

                        //Delete
                        if (item.i_RecordType == (int)RecordType.NoTemporal && item.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                        {
                            typeofeepDto typeofeepDto = new typeofeepDto();
                            typeofeepDto.v_TypeofEEPId = item.v_TypeofEEPId;
                            //typeofeepDto.i_TypeofEEPId = item.i_TypeofEEPId;

                            _typeofeepListDtoDelete.Add(typeofeepDto);
                        }
                    }

                    _objHistoryBL.UpdateHistory(ref objOperationResult,
                                                _workstationdangersListDto,
                                                _workstationdangersListDtoDelete,
                                                _workstationdangersListDtoUpdate,
                                                _typeofeepListDto,
                                                _typeofeepListUpdate,
                                                _typeofeepListDtoDelete,
                                                _objhistoryDto,
                                                Globals.ClientSession.GetAsList());

                }
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
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
         
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtGeographicalHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
      
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan
                    e.Handled = true;
                }           
        }

        #region Dangers

        private void LoadTreeDangers(int pintItemId)
        {
            OperationResult objOperationResult = new OperationResult();
            SystemParameterBL objSystemParameterBL = new SystemParameterBL();
            List<SystemParameterList> objSystemParameterList = new List<SystemParameterList>();

            treeViewDangers.Nodes.Clear();
            TreeNode nodePrimary = null;

            objSystemParameterList = objSystemParameterBL.GetSystemParametersPagedAndFiltered(ref objOperationResult, 0, null, "", "i_GroupId==" + pintItemId, 0);
            objSystemParameterList = objSystemParameterList.OrderBy(p => p.i_ParentParameterId).ToList();
            foreach (var item in objSystemParameterList)
            {
                switch (item.i_ParentParameterId.ToString())
                {
                    #region Add Main Nodes
                    case "-1": // 1. Add Main nodes:
                        nodePrimary = new TreeNode();
                        nodePrimary.Text = item.v_Value1;
                        nodePrimary.Name = item.i_ParameterId.ToString();
                        treeViewDangers.Nodes.Add(nodePrimary);
                        break;
                    #endregion
                    default: // 2. Add Option nodes:
                        foreach (TreeNode tnitem in treeViewDangers.Nodes)
                        {
                            TreeNode tnOption = SelectChildrenRecursive(tnitem, item.i_ParentParameterId.ToString());

                            if (tnOption != null)
                            {
                                TreeNode childNode = new TreeNode();
                                childNode.Text = item.v_Value1;
                                childNode.Name = item.i_ParameterId.ToString();
                                tnOption.Nodes.Add(childNode);
                                break;
                            }
                        }
                        break;
                }
            }
            treeViewDangers.ExpandAll();
        }
       
        private void btnMoveDanger_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            SystemParameterBL objSystemParameterBL = new SystemParameterBL();
            SystemParameterList objSystemParameterList = new SystemParameterList();

            WorkstationDangersList objWorkstationDangers = new WorkstationDangersList();

            if (treeViewDangers.SelectedNode == null) return;
            //Si la lista temporal es null se la setea con una lista vacia
            if (_TempWorkstationDangersList == null)       
                _TempWorkstationDangersList = new List<WorkstationDangersList>();
          
            int ParameterId = int.Parse(treeViewDangers.SelectedNode.Name.ToString());
            History.frmOtroPeligro frmOtroPeligro = new History.frmOtroPeligro();
            string OtroDangrer = "";
            if (ParameterId == 35)
            {
                
                frmOtroPeligro.ShowDialog();

               
                OtroDangrer = frmOtroPeligro.DangerName;
                objSystemParameterList = objSystemParameterBL.GetParentNameSystemParameter(ref objOperationResult,frmOtroPeligro.ParameterId, 145);
            }
            else
            {
                objSystemParameterList = objSystemParameterBL.GetParentNameSystemParameter(ref objOperationResult, ParameterId, 145);

            }
            

            History.frmRuidoPopup frm = new History.frmRuidoPopup();

            if (objSystemParameterList != null)
            {
                WorkstationDangersList findResult = new WorkstationDangersList();
                //Busco en la lista temporal si ya se agrego el item seleccionado
                if (ParameterId == 35)
                {
                    findResult = _TempWorkstationDangersList.Find(p => p.i_DangerId == frmOtroPeligro.ParameterId);
                }
                else
                {
                     findResult = _TempWorkstationDangersList.Find(p => p.i_DangerId == ParameterId);
                }
                
                
                if (findResult == null)
                {
                    // Levantar popup para registrar datos propios de ruido
                    //if (ParameterId == (int)PeligrosEnElPuesto.Ruido)
                    //{
                    //    frm.ShowDialog();

                    //    if (frm.DialogResult == DialogResult.Cancel)
                    //        return;

                    //    objWorkstationDangers.v_TimeOfExposureToNoise = frm.FuenteRuido;
                    //    objWorkstationDangers.i_NoiseLevel = frm.NivelRuidoId;
                    //    objWorkstationDangers.i_NoiseSource = frm.TiempoExposicionRuidoId;
                    //}

                    objWorkstationDangers.v_WorkstationDangersId = Guid.NewGuid().ToString();
                    
                    objWorkstationDangers.v_ParentName = objSystemParameterList.v_Value1;
                    if (ParameterId == 35)
                    {
                        objWorkstationDangers.v_DangerName = OtroDangrer;
                        objWorkstationDangers.i_DangerId = frmOtroPeligro.ParameterId;
                    }
                    else
                    {
                        objWorkstationDangers.i_DangerId = ParameterId;
                        objWorkstationDangers.v_DangerName = treeViewDangers.SelectedNode.Text.ToString();
                    }
                    
                    objWorkstationDangers.i_RecordStatus = (int)RecordStatus.Agregado;
                    objWorkstationDangers.i_RecordType = (int)RecordType.Temporal;
                    _TempWorkstationDangersList.Add(objWorkstationDangers);
                }
                else
                {
                    // Levantar popup para registrar datos propios de ruido
                    if (ParameterId == (int)PeligrosEnElPuesto.Ruido)
                    {
                        //if (findResult.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                        //{
                        //    frm.ShowDialog();

                        //    if (frm.DialogResult == DialogResult.Cancel)
                        //        return;
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Por favor seleccione otro item. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    return;
                        //}
                       
                    }

                    if (findResult.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                    {
                        if (findResult.i_RecordType == (int)RecordType.NoTemporal)// El registro Tiene in ID de BD
                        {
                            findResult.i_DangerId = ParameterId;
                            findResult.v_DangerName = objSystemParameterList.v_Value1;
                            findResult.v_DangerName = treeViewDangers.SelectedNode.Text.ToString();

                            findResult.v_TimeOfExposureToNoise = frm.FuenteRuido;
                            findResult.i_NoiseLevel = frm.NivelRuidoId;
                            findResult.i_NoiseSource = frm.TiempoExposicionRuidoId;
                            
                            findResult.i_RecordStatus = (int)RecordStatus.Grabado;
                        }
                        else if (findResult.i_RecordType == (int)RecordType.Temporal) // El registro tiene un ID temporal [GUID]
                        {
                            findResult.i_DangerId = ParameterId;
                            findResult.v_DangerName = objSystemParameterList.v_Value1;
                            findResult.v_DangerName = treeViewDangers.SelectedNode.Text.ToString();

                            findResult.v_TimeOfExposureToNoise = frm.FuenteRuido;
                            findResult.i_NoiseLevel = frm.NivelRuidoId;
                            findResult.i_NoiseSource = frm.TiempoExposicionRuidoId;
                            
                            findResult.i_RecordStatus = (int)RecordStatus.Agregado;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor seleccione otro item. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                var dataList = _TempWorkstationDangersList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                //var peopleInOrder = people.OrderBy(person => person.LastName);

                // Cargar grilla
                grdDataDangers.DataSource = new WorkstationDangersList();
                grdDataDangers.DataSource = dataList;
                grdDataDangers.Refresh();
            }

        }

        private void btnDeleteDanger_Click(object sender, EventArgs e)
        {
            if (_WorkstationDangersId != "")
            {
                if (_Mode == "New")
                {
                    _TempWorkstationDangersList.RemoveAt(int.Parse(grdDataDangers.Rows[0].Index.ToString()));

                    grdDataDangers.DataSource = new WorkstationDangersList();
                    grdDataDangers.DataSource = _TempWorkstationDangersList;
                    grdDataDangers.Refresh();
                }
                else if (_Mode == "Edit")
                {
                    var findResult = _TempWorkstationDangersList.Find(p => p.v_WorkstationDangersId == _WorkstationDangersId);
                    findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;

                    var dataList = _TempWorkstationDangersList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                    grdDataDangers.DataSource = new WorkstationDangersList();
                    grdDataDangers.DataSource = dataList;
                    grdDataDangers.Refresh();
                }
            }

        }

        private void grdDataDangers_MouseDown(object sender, MouseEventArgs e)
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
                    grdDataDangers.Rows[row.Index].Selected = true;
                    _WorkstationDangersId = grdDataDangers.Selected.Rows[0].Cells[0].Value.ToString();
                }
            }
        }

        #endregion

        #region EPP

        private void LoadTreeEPP(int pintItemId)
        {
            OperationResult objOperationResult = new OperationResult();
            SystemParameterBL objSystemParameterBL = new SystemParameterBL();
            List<SystemParameterList> objSystemParameterList = new List<SystemParameterList>();

            treeViewEPP.Nodes.Clear();
            TreeNode nodePrimary = null;

            objSystemParameterList = objSystemParameterBL.GetSystemParametersPagedAndFiltered(ref objOperationResult, 0, null, "", "i_GroupId==" + pintItemId, 0);

            foreach (var item in objSystemParameterList)
            {
                switch (item.i_ParentParameterId.ToString())
                {
                    #region Add Main Nodes
                    case "-1": // 1. Add Main nodes:
                        nodePrimary = new TreeNode();
                        nodePrimary.Text = item.v_Value1;
                        nodePrimary.Name = item.i_ParameterId.ToString();
                        treeViewEPP.Nodes.Add(nodePrimary);
                        break;
                    #endregion
                    default: // 2. Add Option nodes:
                        foreach (TreeNode tnitem in treeViewEPP.Nodes)
                        {
                            TreeNode tnOption = SelectChildrenRecursive(tnitem, item.i_ParentParameterId.ToString());

                            if (tnOption != null)
                            {
                                TreeNode childNode = new TreeNode();
                                childNode.Text = item.v_Value1;
                                childNode.Name = item.i_ParameterId.ToString();
                                tnOption.Nodes.Add(childNode);
                                break;
                            }
                        }
                        break;
                }
            }
            treeViewEPP.ExpandAll();
        }

        private void btnMoveEPP_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            SystemParameterBL objSystemParameterBL = new SystemParameterBL();
            SystemParameterList objSystemParameterList = new SystemParameterList();
            TypeOfEEPList objTypeOfEEP = new TypeOfEEPList();

            if (treeViewEPP.SelectedNode == null) return;

            //Si la lista temporal es null se la setea con una lista vacia
            if (_TempTypeOfEEPList == null)
            {
                _TempTypeOfEEPList = new List<TypeOfEEPList>();
            }

            int ParameterId = int.Parse(treeViewEPP.SelectedNode.Name.ToString());
            History.frmOtroEPP frmOtroEpp = new History.frmOtroEPP();
            string OtroEpp = "";
            if (ParameterId == 16)
            {
                frmOtroEpp.ShowDialog();

                OtroEpp = frmOtroEpp.EppName;
                objSystemParameterList = objSystemParameterBL.GetParentNameSystemParameter(ref objOperationResult, frmOtroEpp.ParameterId);
            }
            else
            {
                objSystemParameterList = objSystemParameterBL.GetParentNameSystemParameter(ref objOperationResult, ParameterId);
            }


            //History.frmEppPercentage frm = new History.frmEppPercentage(objSystemParameterList.v_Value1,0);
            //frm.ShowDialog();

            //if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            //{
                if (objSystemParameterList != null)
                {
                    TypeOfEEPList findResult = new TypeOfEEPList();
                    //Busco en la lista temporal si ya se agrego el item seleccionado
                    if (ParameterId == 16)
                    {
                         findResult = _TempTypeOfEEPList.Find(p => p.i_TypeofEEPId == frmOtroEpp.ParameterId);
                    }
                    else
                    {
                         findResult = _TempTypeOfEEPList.Find(p => p.i_TypeofEEPId == ParameterId);
                    }

                   
                  
                    if (findResult == null)
                    {
                        objTypeOfEEP.v_TypeofEEPId = Guid.NewGuid().ToString();

                        if (ParameterId == 16)
                        {
                            objTypeOfEEP.i_TypeofEEPId = frmOtroEpp.ParameterId;
                            objTypeOfEEP.v_TypeofEEPName = OtroEpp;
                        }
                        else
                        {
                            objTypeOfEEP.i_TypeofEEPId = ParameterId;
                            objTypeOfEEP.v_TypeofEEPName = objSystemParameterList.v_Value1;
                        }


                        objTypeOfEEP.r_Percentage = 100;// frm._Porcentage;
                        objTypeOfEEP.i_RecordStatus = (int)RecordStatus.Agregado;
                        objTypeOfEEP.i_RecordType = (int)RecordType.Temporal;
                        _TempTypeOfEEPList.Add(objTypeOfEEP);
                    }
                    else
                    {
                        if (findResult.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                        {
                            if (findResult.i_RecordType == (int)RecordType.NoTemporal)// El registro Tiene in ID de BD
                            {
                                findResult.i_TypeofEEPId = ParameterId;
                                findResult.v_TypeofEEPName = objSystemParameterList.v_Value1;
                                findResult.r_Percentage = 100;// frm._Porcentage;
                                findResult.i_RecordStatus = (int)RecordStatus.Grabado;
                            }
                            else if (findResult.i_RecordType == (int)RecordType.Temporal) // El registro tiene un ID temporal [GUID]
                            {
                                findResult.i_TypeofEEPId = ParameterId;
                                findResult.v_TypeofEEPName = objSystemParameterList.v_Value1;
                                findResult.r_Percentage = 100;// frm._Porcentage;
                                findResult.i_RecordStatus = (int)RecordStatus.Agregado;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Por favor seleccione otro item. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    var dataList = _TempTypeOfEEPList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                    // Cargar grilla
                    grdDataEPP.DataSource = new TypeOfEEPList();
                    grdDataEPP.DataSource = dataList;
                    grdDataEPP.Refresh();
                }
            //}
        }

        private void btnDeleteEPP_Click(object sender, EventArgs e)
        {
            if (_TypeofEEPId != "")
            {
                if (_Mode == "New")
                {
                    _TempTypeOfEEPList.RemoveAt(int.Parse(grdDataEPP.Rows[0].Index.ToString()));

                    grdDataEPP.DataSource = new TypeOfEEPList();
                    grdDataEPP.DataSource = _TempTypeOfEEPList;
                    grdDataEPP.Refresh();
                }
                else if (_Mode == "Edit")
                {
                    var findResult = _TempTypeOfEEPList.Find(p => p.v_TypeofEEPId == _TypeofEEPId);
                    findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;

                    var dataList = _TempTypeOfEEPList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                    grdDataEPP.DataSource = new TypeOfEEPList();
                    grdDataEPP.DataSource = dataList;
                    grdDataEPP.Refresh();
                }
            }

        }

        private void grdDataEPP_MouseDown(object sender, MouseEventArgs e)
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
                    grdDataEPP.Rows[row.Index].Selected = true;
                    _TypeofEEPId = grdDataEPP.Selected.Rows[0].Cells[0].Value.ToString();           
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
                    grdDataEPP.Rows[row.Index].Selected = true;
                    contextMenuTypeEPP.Items["modificarToolStripMenuItem"].Enabled = true;
                    _TypeofEEPName = grdDataEPP.Selected.Rows[0].Cells[3].Value.ToString();
                    _Percentage = double.Parse(grdDataEPP.Selected.Rows[0].Cells[4].Value.ToString());
                    _TypeofEEPId = grdDataEPP.Selected.Rows[0].Cells[0].Value.ToString();
                    _objTypeOfEEPamc = _TempTypeOfEEPList.FindAll(p => p.v_TypeofEEPId == _TypeofEEPId).FirstOrDefault();
                    _IndexListEPP = _TempTypeOfEEPList.FindIndex(p => p.v_TypeofEEPId == _TypeofEEPId);
                }
                else
                {
                    contextMenuTypeEPP.Items["modificarToolStripMenuItem"].Enabled = false;
                }
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            History.frmEppPercentage frm = new History.frmEppPercentage(_TypeofEEPName,_Percentage);
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (_objTypeOfEEPamc.i_RecordType == (int)RecordType.Temporal )
                {
                    _objTypeOfEEPamc.i_RecordStatus = (int)RecordStatus.Agregado;
                }
                else if (_objTypeOfEEPamc.i_RecordType == (int)RecordType.NoTemporal )
                {
                    _objTypeOfEEPamc.i_RecordStatus = (int)RecordStatus.Modificado;
                }
                _objTypeOfEEPamc.r_Percentage = frm._Porcentage;
               
                _TempTypeOfEEPList[_IndexListEPP] = _objTypeOfEEPamc;

                // Cargar grilla
                grdDataEPP.DataSource = new TypeOfEEPList();
                grdDataEPP.DataSource = _TempTypeOfEEPList;
                grdDataEPP.Refresh();
            }

        }
        #endregion

        private void dtpDateTimeStar_Validating(object sender, CancelEventArgs e)
        {
            //if (dtpDateTimeStar.Value.Date > dptDateTimeEnd.Value.Date)
            //{
            //    e.Cancel = true;
            //    MessageBox.Show("El campo fecha Inicio no puede ser Mayor a la fecha Fin.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
        }

        private void dptDateTimeEnd_Validating(object sender, CancelEventArgs e)
        {
            //if (dptDateTimeEnd.Value.Date < dtpDateTimeStar.Value.Date)
            //{
            //    e.Cancel = true;
            //    MessageBox.Show("El campo Fecha Fin no puede ser Menor a la Fecha Inicio.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
        }
    }
}
