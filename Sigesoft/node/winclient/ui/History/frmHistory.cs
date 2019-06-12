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
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win;


namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmHistory : Form
    {
        #region Declarations
        
      
        private int FMatchType, fpcHandle;
        private bool FAutoIdentify;

        string _PersonMedicalHistoryId;
        string _NoxiousHabitsId;
        string _FamilyMedicAntecedentId;
        int _IndexPersonMedicalHistoryList;
        int _IndexFamilyMedicAntecendentList;
        int _IndexNoxiousHabitsList;
        int _TypeDiagnosticId;
        string _DiagnosticName;
        string _NoxiousHabitsName =null;
        string _Frecuency;
        string _Comment;
        string _CommentFamilyMedic;
        string _TypeFamilyName;
        DateTime _StartDate;
        string _DiagnosticDetail;
        DateTime? _Date =null;
        string _TreatmentSite;
        string _Hospital;
        string _Complicaciones;
        string _PacientId;
        string _GroupPopupFamilyMedical;
        byte[] _personImage;
        string _personName;
        //byte[] _FingerPrintImage;
        bool _Validation;

        bool _ResultMedicoPersonales = false;
        bool _ResultlHabitosNoxivos = false;
        bool _ResultMedicoFamiliares = false;
        private int _ParentParameterId;

        List<personmedicalhistoryDto> _personmedicalhistoryDto = null;
        List<personmedicalhistoryDto> _personmedicalhistoryDelete = null;
        List<personmedicalhistoryDto> _personmedicalhistoryUpdate = null;

        List<noxioushabitsDto> _noxioushabitsDto = null;
        List<noxioushabitsDto> _noxioushabitsDelete = null;
        List<noxioushabitsDto> _noxioushabitsUpdate = null;

        List<familymedicalantecedentsDto> _familymedicalantecedentsDto = null;
        List<familymedicalantecedentsDto> _familymedicalantecedentsDelete = null;
        List<familymedicalantecedentsDto> _familymedicalantecedentsUpdate = null;

        HistoryBL _objHistoryBL = new HistoryBL();
        List<PersonMedicalHistoryList> _TempPersonMedicalHistoryList = new List<PersonMedicalHistoryList>();
        List<PersonMedicalHistoryList> _TempPersonMedicalHistoryListOld = new List<PersonMedicalHistoryList>();  
        PersonMedicalHistoryList _objPersonMedicalHistoryamc = new PersonMedicalHistoryList();

        List<NoxiousHabitsList> _TempNoxiousHabitsList = new List<NoxiousHabitsList>();
        List<NoxiousHabitsList> _TempNoxiousHabitsListOld = new List<NoxiousHabitsList>();
        NoxiousHabitsList _objNoxiousHabitsyamc = new NoxiousHabitsList();

        List<FamilyMedicalAntecedentsList> _TempFamilyMedicalAntecedentsList = new List<FamilyMedicalAntecedentsList>();
        List<FamilyMedicalAntecedentsList> _TempFamilyMedicalAntecedentsListOld = new List<FamilyMedicalAntecedentsList>();
        FamilyMedicalAntecedentsList _objFamilyMedicalAntecedentsListamc = new FamilyMedicalAntecedentsList();

        List<SystemParameterList> _objSystemParameterList;

        #endregion

        public frmHistory(string pstrPacientId)
        {
            try
            {
                OperationResult objOperationResult = new OperationResult();
                PacientBL objPacienteBL = new PacientBL();
                personDto objPersonDto = new personDto();
                HistoryBL objHistoryBL = new HistoryBL();
                List<HistoryList> HistoryList = new List<HistoryList>();

                InitializeComponent();

                //****************************************************************************************************//
                SystemParameterBL objSystemParameterBL = new SystemParameterBL();
                _objSystemParameterList = new List<SystemParameterList>();
                SystemParameterList objSystemParameter = new SystemParameterList();


                HistoryBL oHistoryBL = new HistoryBL();
                List<PersonMedicalHistoryList> PersonMedicalHistoryList = new List<PersonMedicalHistoryList>();
                PersonMedicalHistoryList = oHistoryBL.GetPersonMedicalHistoryPagedAndFilteredByPersonId1(ref objOperationResult, 0, null, "", "", pstrPacientId);

                if (PersonMedicalHistoryList.Count == 0)
                {
                    _objSystemParameterList = objSystemParameterBL.GetSystemParametersPagedAndFilteredNew(ref objOperationResult, 0, null, "", "i_GroupId== 147 && i_IsDeleted == 0", 147);
                }
                else
                {
                    foreach (var item in PersonMedicalHistoryList)
                    {
                        objSystemParameter.v_Value1 = item.v_DiseasesId;
                        objSystemParameter.v_DiseaseName = item.v_DiseasesName;
                        if (item.i_Answer == (int)SiNo.NO)
                        {
                            objSystemParameter.NO = true;
                        }
                        else if (item.i_Answer == (int)SiNo.SI)
                        {
                            objSystemParameter.SI = true;
                        }
                        else if (item.i_Answer == (int)SiNo.NONE)
                        {
                            objSystemParameter.ND = true;
                        }

                        _objSystemParameterList.Add(objSystemParameter);
                    }
                    _objSystemParameterList = objSystemParameterBL.GetSystemParametersPagedAndFilteredNew(ref objOperationResult, 0, null, "", "i_GroupId== 147 && i_IsDeleted == 0", 147);
                    foreach (var item in PersonMedicalHistoryList)
                    {
                        SystemParameterList Result = _objSystemParameterList.Find(p => p.v_Value1 == item.v_DiseasesId);

                        if (Result == null)
                            Result = new SystemParameterList();

                        Result.i_Answer = item.i_Answer;

                        if (item.i_Answer == (int)SiNo.NO)
                        {
                            Result.NO = true;
                            Result.SI = false;
                            Result.ND = false;
                        }
                        else if (item.i_Answer == (int)SiNo.SI)
                        {
                            Result.SI = true;
                            Result.NO = false;
                            Result.ND = false;
                        }
                        else if (item.i_Answer == (int)SiNo.NONE)
                        {
                            Result.ND = true;
                            Result.SI = false;
                            Result.NO = false;
                        }
                    }

                }

                ultraGrid2.DataSource = _objSystemParameterList;


                //*****************************************************************************************************//
                _PacientId = pstrPacientId;
                objPersonDto = objPacienteBL.GetPerson(ref objOperationResult, _PacientId);
                _personName = objPersonDto.v_FirstName + " " + objPersonDto.v_FirstLastName + " " + objPersonDto.v_SecondLastName;
                //_FingerPrintImage = objPersonDto.b_FingerPrintTemplate;
                _Validation = false;
                Byte[] ooo = objPersonDto.b_PersonImage;
                if (ooo == null)
                {
                    pbEmployee.Image = Resources.nofoto;
                }
                else
                {
                    pbEmployee.Image = Common.Utils.BytesArrayToImageOficce(ooo, pbEmployee);
                    _personImage = ooo;
                }

                txtEmployee.Text = _personName;
                Utils.LoadDropDownList(ddlDocTypeId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 106, null), DropDownListAction.Select);
                ddlDocTypeId.SelectedValue = objPersonDto.i_DocTypeId.ToString();
                txtNumDocument.Text = objPersonDto.v_DocNumber;

                Utils.LoadDropDownList(cbEstCivil, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 101, null), DropDownListAction.Select);
                cbEstCivil.SelectedValue = objPersonDto.i_MaritalStatusId.ToString();

                Utils.LoadDropDownList(cbGInstruccion, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 108, null), DropDownListAction.Select);
                cbGInstruccion.SelectedValue = objPersonDto.i_LevelOfId.ToString();

                txtFNac.Text = objPersonDto.d_Birthdate.ToString().Split(' ')[0];
                DateTime FechaNacimiento = (DateTime)objPersonDto.d_Birthdate;
                int PacientAge = DateTime.Today.AddTicks(-FechaNacimiento.Ticks).Year - 1;
                txtAge.Text = PacientAge.ToString();
                textHijos.Text = (objPersonDto.i_NumberLivingChildren + objPersonDto.i_NumberDependentChildren).ToString();
                textNacionalidad.Text = objPersonDto.v_Nacionalidad;
                textReligion.Text = objPersonDto.v_Religion;

                this.Text = this.Text + "Antecedentes del Paciente : " + " (" + _personName + ")";

                HistoryList = objHistoryBL.GetHistoryPagedAndFiltered(ref objOperationResult, 0, null, "", "", _PacientId);
                if (HistoryList.Count == 0) return;
                //FingerPrintImage = HistoryList[0].b_FingerPrintImage;
                //RubricImageText = HistoryList[0].t_RubricImageText;

                //if (FingerPrintImage == null || FingerPrintImage.Count() == 0) return;


                //pbFingerPrint.Image = Common.Utils.byteArrayToImage(FingerPrintImage);

                //if (RubricImageText == null) return;

                //sigPlusNET1.SetSigString(RubricImageText);
            }
            catch (Exception)
            {
                
                return;
            }
            
        }

        private void mnuNewOccupational_Click(object sender, EventArgs e)
        {
            frmOccupationalHistory frm;
            if (_Validation == true)
            {
                frm = new frmOccupationalHistory("New", null, _PacientId, true);
            }
            else
            {
                frm = new frmOccupationalHistory("New", null, _PacientId, false);
            }
           
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                BindGridOccupational();
            }
        }

        private void mnuEditOccupational_Click(object sender, EventArgs e)
        {
            string HistoryId = grdDataOccupational.Selected.Rows[0].Cells[0].Value.ToString();
            frmOccupationalHistory frm;

            if (_Validation == true)
            {
                frm = new frmOccupationalHistory("Edit", HistoryId, _PacientId, true);
            }
            else
            {
                frm = new frmOccupationalHistory("Edit", HistoryId, _PacientId, false);
            }

            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                BindGridOccupational();
            }
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


        private void frmHistory_Load(object sender, EventArgs e)
        {
            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);

            }


            #endregion
            //Muestro la ultraGrid1 abierto
            this.ultraGrid2.Rows.ExpandAll(true);
            // Iniciar el componente huellero
            //InitFingerPrint();

            //tabControl1.Enabled = false;

            // Iniciar el componente Firma
            //sigPlusNET1.SetTabletState(1);

            lblResultFirma.Text = "";
            ShowHintImageFirma(3);

            FAutoIdentify = false;
            btnDelSignature.Enabled = false;

            //EnrollFingerPrint(null, null);

            BindGridOccupational();
            BindGridPersonMedical();
            BindGridNoxiousHabits();
            BindGridFamilyMedicAntecendet();
            //LoadTreePersonMedical(147);
            LoadTreeNoxiuosHabits(148);
            LoadTreeFamilyMedicalAntecedents(149);

            //if (FingerPrintImage == null) 
                //return;

            //pbFingerPrint.Image = Common.Utils.byteArrayToImage(FingerPrintImage);

            //if (RubricImageText == null) 
            //    return;

            //sigPlusNET1.SetSigString(RubricImageText);

            //this.ultraGrid1.Rows.ExpandAll(true);

        }

        private void BindGridOccupational()
        {
            var objData = GetData(0, null, "d_EndDate DESC", null);

            grdDataOccupational.DataSource = objData;
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }
     
        private void grdDataOccupational_MouseDown(object sender, MouseEventArgs e)
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
                    grdDataOccupational.Rows[row.Index].Selected = true;
                    contextMenuStripOcupacional.Items["mnuNewOccupational"].Enabled = true;
                    contextMenuStripOcupacional.Items["mnuEditOccupational"].Enabled = true;
                    if (_Validation)
                    {
                        btnDelete.Enabled = true;
                        contextMenuStripOcupacional.Items["mnuDeleteOccupational"].Enabled = true;
                    }
                    else
                    {
                        btnDelete.Enabled = false;
                        contextMenuStripOcupacional.Items["mnuDeleteOccupational"].Enabled = false;
                    }
                    //contextMenuStripOcupacional.Items["mnuDeleteOccupational"].Enabled = true;
                }
                else
                {
                    contextMenuStripOcupacional.Items["mnuNewOccupational"].Enabled = true;
                    contextMenuStripOcupacional.Items["mnuEditOccupational"].Enabled = false;
                    if (_Validation)
                    {
                        btnDelete.Enabled = true;
                        contextMenuStripOcupacional.Items["mnuDeleteOccupational"].Enabled = true;
                    }
                    else
                    {
                        btnDelete.Enabled = false;
                        contextMenuStripOcupacional.Items["mnuDeleteOccupational"].Enabled = false;
                    }
                    //contextMenuStripOcupacional.Items["mnuDeleteOccupational"].Enabled = false;
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    grdDataOccupational.Rows[row.Index].Selected = true;
                    btnEdit.Enabled = true;
                    if (_Validation)
                    {
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        btnDelete.Enabled = false;
                    }
                    //btnDelete.Enabled = true;
                }
                else
                {
                    //contextMenuStripOcupacional.Items["mnuNewOccupational"].Enabled = true;
                    //contextMenuStripOcupacional.Items["mnuEditOccupational"].Enabled = false;
                    //contextMenuStripOcupacional.Items["mnuDeleteOccupational"].Enabled = false;
                    btnEdit.Enabled = false;
                    if (_Validation)
                    {
                        btnDelete.Enabled = true;
                        contextMenuStripOcupacional.Items["mnuDeleteOccupational"].Enabled = true;
                    }
                    else
                    {
                        btnDelete.Enabled = false;
                        contextMenuStripOcupacional.Items["mnuDeleteOccupational"].Enabled = false;
                    }
                    //btnDelete.Enabled = false;
                }
            }
        }

        private void mnuDeleteOccupational_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            
            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                string HistoryId = grdDataOccupational.Selected.Rows[0].Cells["v_HistoryId"].Value.ToString();

                _objHistoryBL.DeleteHistory(ref objOperationResult, HistoryId, Globals.ClientSession.GetAsList());

                BindGridOccupational();
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

        private void BtnNew_Click(object sender, EventArgs e)
        {
            frmOccupationalHistory frm;
            if (_Validation == true)
            {
                frm = new frmOccupationalHistory("New", null, _PacientId, true);
            }
            else
            {
                frm = new frmOccupationalHistory("New", null, _PacientId,false);
            }
           
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                BindGridOccupational();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            string HistoryId = grdDataOccupational.Selected.Rows[0].Cells["v_HistoryId"].Value.ToString();

            frmOccupationalHistory frm;
            if (_Validation == true)
            {
                frm = new frmOccupationalHistory("Edit", HistoryId, _PacientId, true);
                btnDelete.Enabled = true;
            }
            else
            {
                frm = new frmOccupationalHistory("Edit", HistoryId, _PacientId, false);
                btnDelete.Enabled = false;
            }           

            //frmOccupationalHistory frm = new frmOccupationalHistory("Edit", HistoryId, _PacientId,false);
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                BindGridOccupational();
            }      

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {        
            OperationResult objOperationResult = new OperationResult();

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                string HistoryId = grdDataOccupational.Selected.Rows[0].Cells["v_HistoryId"].Value.ToString();

                _objHistoryBL.DeleteHistory(ref objOperationResult, HistoryId, Globals.ClientSession.GetAsList());

                BindGridOccupational();
            }
         
        }

        private void pbEmployee_Click(object sender, EventArgs e)
        {
            if (_personImage != null)
            {
                var frm = new Sigesoft.Node.WinClient.UI.Operations.Popups.frmPreviewImagePerson(_personImage, _personName);
                frm.ShowDialog();
            }
        }

        #region Tab PersonMedical

        private void BindGridPersonMedical()
        {
            //List<PersonMedicalHistoryList> objData = GetDataPersonMedical(0, null, "d_StartDate DESC", null);
            _TempPersonMedicalHistoryList = GetDataPersonMedical(0, null, "d_StartDate DESC", null);
            _TempPersonMedicalHistoryListOld = GetDataPersonMedical(0, null, "d_StartDate DESC", null);

            var objData = GetDataPersonMedical(0, null, "d_StartDate DESC", null);
            grdDataPersonMedical.DataSource = objData;
            lblRecordCountPersonMedical.Text = string.Format("Se encontraron {0} registros.", objData.Count());

             
        }

        private List<HistoryList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objHistoryBL.GetHistoryPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, _PacientId);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private List<PersonMedicalHistoryList> GetDataPersonMedical(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objHistoryBL.GetPersonMedicalHistoryPagedAndFilteredByPersonId(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, _PacientId);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }
        
        private void ToolStripMenuEditPersonMedical_Click(object sender, EventArgs e)
        {
            History.frmPersonMedicalPopup frm = new History.frmPersonMedicalPopup(_DiagnosticName, _TypeDiagnosticId, _StartDate, _DiagnosticDetail, _Date, _TreatmentSite, _Hospital, _Complicaciones);
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                _ResultMedicoPersonales = true;
                if (_objPersonMedicalHistoryamc.i_RecordType == (int)RecordType.Temporal)
                {
                    _objPersonMedicalHistoryamc.i_RecordStatus = (int)RecordStatus.Agregado;
                }
                else if (_objPersonMedicalHistoryamc.i_RecordType == (int)RecordType.NoTemporal)
                {
                    _objPersonMedicalHistoryamc.i_RecordStatus = (int)RecordStatus.Modificado;
                }

                _objPersonMedicalHistoryamc.i_TypeDiagnosticId = frm._TypeDiagnosticId;
                _objPersonMedicalHistoryamc.d_Date = frm._Date;
                _objPersonMedicalHistoryamc.d_StartDate = frm._StartDate;
                _objPersonMedicalHistoryamc.v_DiagnosticDetail = frm._DiagnosticDetail;
                _objPersonMedicalHistoryamc.v_TreatmentSite = frm._TreatmentSite;
                _objPersonMedicalHistoryamc.NombreHospital = frm._Hospital;
                _objPersonMedicalHistoryamc.v_Complicaciones = frm._Complicaciones;
                _TempPersonMedicalHistoryList[_IndexPersonMedicalHistoryList] = _objPersonMedicalHistoryamc;

                // Cargar grilla
                grdDataPersonMedical.DataSource = new PersonMedicalHistoryList();
                grdDataPersonMedical.DataSource = _TempPersonMedicalHistoryList;
                grdDataPersonMedical.Refresh();
            }
        }

        private void grdDataPersonMedical_MouseDown(object sender, MouseEventArgs e)
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
                    btnDeletePersonMedical.Enabled = true;
                    grdDataPersonMedical.Rows[row.Index].Selected = true;
                    _PersonMedicalHistoryId = grdDataPersonMedical.Selected.Rows[0].Cells[0].Value.ToString();
                    //_Date = grdDataPersonMedical.Selected.Rows[0].Cells[0].Value.ToString();   
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
                    grdDataPersonMedical.Rows[row.Index].Selected = true;
                    contextMenuPersonMedical.Items["ToolStripMenuEditPersonMedical"].Enabled = true;

                    if (grdDataPersonMedical.Selected.Rows[0].Cells["v_GroupName"].Value != null)
                        _DiagnosticName = grdDataPersonMedical.Selected.Rows[0].Cells["v_GroupName"].Value.ToString();
                    else
                        _DiagnosticName = string.Empty;

                    _TypeDiagnosticId = int.Parse(grdDataPersonMedical.Rows[row.Index].Cells["i_TypeDiagnosticId"].Value.ToString());
                    _StartDate = DateTime.Parse(grdDataPersonMedical.Rows[row.Index].Cells["d_StartDate"].Value.ToString());
                    _DiagnosticDetail = grdDataPersonMedical.Rows[row.Index].Cells["v_DiagnosticDetail"].Value.ToString();
                    _TreatmentSite = grdDataPersonMedical.Rows[row.Index].Cells["v_TreatmentSite"].Value.ToString();
                    _Hospital = grdDataPersonMedical.Rows[row.Index].Cells["NombreHospital"].Value.ToString();
                    _Complicaciones = grdDataPersonMedical.Rows[row.Index].Cells["v_Complicaciones"].Value.ToString();
                    _PersonMedicalHistoryId = grdDataPersonMedical.Rows[row.Index].Cells["v_PersonMedicalHistoryId"].Value.ToString();
                    _objPersonMedicalHistoryamc = _TempPersonMedicalHistoryList.FindAll(p => p.v_PersonMedicalHistoryId == _PersonMedicalHistoryId).FirstOrDefault();
                    _IndexPersonMedicalHistoryList = _TempPersonMedicalHistoryList.FindIndex(p => p.v_PersonMedicalHistoryId == _PersonMedicalHistoryId);
                }
                else
                {
                    contextMenuPersonMedical.Items["ToolStripMenuEditPersonMedical"].Enabled = false;
                }
            }
        }

        private void btnDeletePersonMedical_Click(object sender, EventArgs e)
        {
            if (_PersonMedicalHistoryId != "")
            {
                _ResultMedicoPersonales = true;
                var findResult = _TempPersonMedicalHistoryList.Find(p => p.v_PersonMedicalHistoryId == _PersonMedicalHistoryId);
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;

                var dataList = _TempPersonMedicalHistoryList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdDataPersonMedical.DataSource = new PersonMedicalHistoryList();
                grdDataPersonMedical.DataSource = dataList;
                grdDataPersonMedical.Refresh();

               var x = _objSystemParameterList.Find(p => p.v_Value1 == findResult.v_DiseasesId);
               if (x != null)
               {
                   x.NO = true;
                   x.ND = false;
                   x.SI = false;
                   ultraGrid2.DataSource = _objSystemParameterList;
                   ultraGrid2.Refresh();
               }               
            }           
        }

        private void btnSavePersonMedical_Click(object sender, EventArgs e)
        {
            _personmedicalhistoryDto = new List<personmedicalhistoryDto>();
            _personmedicalhistoryUpdate = new List<personmedicalhistoryDto>();
            _personmedicalhistoryDelete = new List<personmedicalhistoryDto>();

            OperationResult objOperationResult = new OperationResult();
            foreach (var item in _TempPersonMedicalHistoryList)
            {
                //Add
                if (item.i_RecordType == (int)RecordType.Temporal && item.i_RecordStatus == (int)RecordStatus.Agregado)
                {
                    personmedicalhistoryDto personmedicalhistoryDtoDto = new personmedicalhistoryDto();

                    personmedicalhistoryDtoDto.i_TypeDiagnosticId = item.i_TypeDiagnosticId;
                    personmedicalhistoryDtoDto.v_PersonId = _PacientId;
                    personmedicalhistoryDtoDto.v_DiseasesId = item.v_DiseasesId;
                    personmedicalhistoryDtoDto.d_StartDate = item.d_StartDate;
                    personmedicalhistoryDtoDto.v_DiagnosticDetail = item.v_DiagnosticDetail;
                    personmedicalhistoryDtoDto.v_TreatmentSite = item.v_TreatmentSite;
                    personmedicalhistoryDtoDto.NombreHospital = item.NombreHospital;
                    personmedicalhistoryDtoDto.v_Complicaciones = item.v_Complicaciones;
                    personmedicalhistoryDtoDto.i_AnswerId = item.i_Answer;

                    _personmedicalhistoryDto.Add(personmedicalhistoryDtoDto);
                }

                // Update
                if (item.i_RecordType == (int)RecordType.NoTemporal && (item.i_RecordStatus == (int)RecordStatus.Modificado || item.i_RecordStatus == (int)RecordStatus.Grabado))
                {
                    personmedicalhistoryDto personmedicalhistoryDtoDto = new personmedicalhistoryDto();

                    personmedicalhistoryDtoDto.v_PersonMedicalHistoryId = item.v_PersonMedicalHistoryId;
                    personmedicalhistoryDtoDto.i_TypeDiagnosticId = item.i_TypeDiagnosticId;
                    personmedicalhistoryDtoDto.v_PersonId = _PacientId;
                    personmedicalhistoryDtoDto.v_DiseasesId = item.v_DiseasesId;
                    personmedicalhistoryDtoDto.d_StartDate = item.d_StartDate;
                    personmedicalhistoryDtoDto.v_DiagnosticDetail = item.v_DiagnosticDetail;
                    personmedicalhistoryDtoDto.v_TreatmentSite = item.v_TreatmentSite;
                    personmedicalhistoryDtoDto.NombreHospital = item.NombreHospital;
                    personmedicalhistoryDtoDto.v_Complicaciones = item.v_Complicaciones;
                    personmedicalhistoryDtoDto.i_AnswerId = item.i_Answer;
                    _personmedicalhistoryUpdate.Add(personmedicalhistoryDtoDto);
                }

                //Delete
                if (item.i_RecordType == (int)RecordType.NoTemporal && item.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                {
                    personmedicalhistoryDto personmedicalhistoryDtoDto = new personmedicalhistoryDto();

                    personmedicalhistoryDtoDto.v_PersonMedicalHistoryId = item.v_PersonMedicalHistoryId;

                    _personmedicalhistoryDelete.Add(personmedicalhistoryDtoDto);
                }
            }

            foreach (var item in _objSystemParameterList)
            {
                if (item.i_Answer == (int)SiNo.NONE)
                {
                    personmedicalhistoryDto personmedicalhistoryDtoDto = new personmedicalhistoryDto();

                    personmedicalhistoryDtoDto.i_TypeDiagnosticId = null;
                    personmedicalhistoryDtoDto.v_PersonId = _PacientId;
                    personmedicalhistoryDtoDto.v_DiseasesId = item.v_Value1;
                    personmedicalhistoryDtoDto.d_StartDate = null;
                    personmedicalhistoryDtoDto.v_DiagnosticDetail = String.Empty;
                    personmedicalhistoryDtoDto.v_TreatmentSite = String.Empty;
                    personmedicalhistoryDtoDto.NombreHospital = string.Empty;
                    personmedicalhistoryDtoDto.v_Complicaciones = String.Empty;
                    personmedicalhistoryDtoDto.i_AnswerId = item.i_Answer;

                    _personmedicalhistoryDto.Add(personmedicalhistoryDtoDto);
                }
            }
            
            _objHistoryBL.AddPersonMedicalHistory(ref objOperationResult,
                                                    _personmedicalhistoryDto,
                                                    _personmedicalhistoryUpdate,
                                                    _personmedicalhistoryDelete,
                                                    Globals.ClientSession.GetAsList());
            BindGridPersonMedical();
            //// Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
                //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                //this.Close();
                _ResultMedicoPersonales = false;
                MessageBox.Show("Se grabo correctamente", "! INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else  // Operación con error
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Se queda en el formulario.
            }
           
        }

        private void btnCancelPersonMedical_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOtros_Click(object sender, EventArgs e)
        {
            PersonMedicalHistoryList objPersonMedicalHistory = new PersonMedicalHistoryList();
            DiseasesList objDiseasesList = new DiseasesList();
            frmDiseases frm = new frmDiseases();
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.Cancel) return;
            objDiseasesList = frm._objDiseasesList;

            History.frmPersonMedicalPopup frmPersonMedicalPopup = new History.frmPersonMedicalPopup(objDiseasesList.v_Name, -1, DateTime.Now, "", DateTime.Now, "","","");
            frmPersonMedicalPopup.ShowDialog();

            if (frmPersonMedicalPopup.DialogResult != DialogResult.OK) return;
            //Busco en la lista temporal si ya se agrego el item seleccionado
            var findResult = _TempPersonMedicalHistoryList.Find(p => p.v_DiseasesId == objDiseasesList.v_DiseasesId &&  p.d_StartDate.Value.Date == frmPersonMedicalPopup._StartDate);
            if (findResult == null)
            {
                objPersonMedicalHistory.v_PersonMedicalHistoryId = Guid.NewGuid().ToString();
                objPersonMedicalHistory.v_PersonId = _PacientId;
                objPersonMedicalHistory.v_DiseasesId = objDiseasesList.v_DiseasesId;
                objPersonMedicalHistory.v_GroupName = "ENFERMEDADES OTROS";
                objPersonMedicalHistory.i_TypeDiagnosticId = frmPersonMedicalPopup._TypeDiagnosticId;
                objPersonMedicalHistory.v_DiseasesName = objDiseasesList.v_Name;
                objPersonMedicalHistory.v_TypeDiagnosticName = frmPersonMedicalPopup._TypeDiagnosticName;
                objPersonMedicalHistory.d_StartDate = frmPersonMedicalPopup._StartDate;
                objPersonMedicalHistory.v_DiagnosticDetail = frmPersonMedicalPopup._DiagnosticDetail;
                objPersonMedicalHistory.d_Date = frmPersonMedicalPopup._Date;
                objPersonMedicalHistory.v_TreatmentSite = frmPersonMedicalPopup._TreatmentSite;
                objPersonMedicalHistory.NombreHospital = frmPersonMedicalPopup._Hospital;
                objPersonMedicalHistory.v_Complicaciones = frmPersonMedicalPopup._Complicaciones;
                objPersonMedicalHistory.i_RecordStatus = (int)RecordStatus.Agregado;
                objPersonMedicalHistory.i_RecordType = (int)RecordType.Temporal;
                objPersonMedicalHistory.i_Answer = 1; // SI
                _TempPersonMedicalHistoryList.Add(objPersonMedicalHistory);
            }
            else
            {
                if (findResult.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                {
                    if (findResult.i_RecordType == (int)RecordType.NoTemporal)// El registro Tiene in ID de BD
                    {
                        findResult.i_TypeDiagnosticId = frmPersonMedicalPopup._TypeDiagnosticId;
                        findResult.d_StartDate = frmPersonMedicalPopup._StartDate;
                        findResult.v_DiagnosticDetail = frmPersonMedicalPopup._DiagnosticDetail;
                        findResult.d_Date = frmPersonMedicalPopup._Date;
                        findResult.v_TreatmentSite = frmPersonMedicalPopup._TreatmentSite;
                        findResult.NombreHospital = frmPersonMedicalPopup._Hospital;
                        findResult.v_Complicaciones = frmPersonMedicalPopup._Complicaciones;
                        findResult.i_RecordStatus = (int)RecordStatus.Grabado;
                    }
                    else if (findResult.i_RecordType == (int)RecordType.Temporal) // El registro tiene un ID temporal [GUID]
                    {
                        findResult.i_TypeDiagnosticId = frmPersonMedicalPopup._TypeDiagnosticId;
                        findResult.d_StartDate = frmPersonMedicalPopup._StartDate;
                        findResult.v_DiagnosticDetail = frmPersonMedicalPopup._DiagnosticDetail;
                        findResult.d_Date = frmPersonMedicalPopup._Date;
                        findResult.v_TreatmentSite = frmPersonMedicalPopup._TreatmentSite;
                        findResult.NombreHospital = frmPersonMedicalPopup._Hospital;
                        findResult.v_Complicaciones = frmPersonMedicalPopup._Complicaciones;
                        findResult.i_RecordStatus = (int)RecordStatus.Agregado;
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione otro item. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            var dataList = _TempPersonMedicalHistoryList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

            // Cargar grilla
            grdDataPersonMedical.DataSource = new PersonMedicalHistoryList();
            grdDataPersonMedical.DataSource = dataList;
            grdDataPersonMedical.Refresh();



        }
        #endregion
          
        #region Tab Noxious Habits

        private void BindGridNoxiousHabits()
        {
            var objData = GetDataNoxiousHabits(0, null, "", null);
            _TempNoxiousHabitsList = objData;           
            grdDataNoxiousHabits.DataSource = objData;
            lblRecordCountPersonMedical.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            _TempNoxiousHabitsListOld = GetDataNoxiousHabits(0, null, "", null);
        }

        private List<NoxiousHabitsList> GetDataNoxiousHabits(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objHistoryBL.GetNoxiousHabitsPagedAndFilteredByPersonId(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, _PacientId);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void LoadTreeNoxiuosHabits(int pintItemId)
        {
            OperationResult objOperationResult = new OperationResult();
            SystemParameterBL objSystemParameterBL = new SystemParameterBL();
            List<SystemParameterList> objSystemParameterList = new List<SystemParameterList>();

            treeViewNoxiousHabits.Nodes.Clear();
            TreeNode nodePrimary = null;

            objSystemParameterList = objSystemParameterBL.GetSystemParametersPagedAndFiltered(ref objOperationResult, 0, null, "", "i_GroupId==" + pintItemId, 148);

            foreach (var item in objSystemParameterList)
            {
                switch (item.i_ParentParameterId.ToString())
                {
                    #region Add Main Nodes
                    case "-1": // 1. Add Main nodes:
                        nodePrimary = new TreeNode();
                        nodePrimary.Text = item.v_Value1;
                        nodePrimary.Name = item.i_ParameterId.ToString();
                        treeViewNoxiousHabits.Nodes.Add(nodePrimary);
                        break;
                    #endregion
                    default: // 2. Add Option nodes:
                        foreach (TreeNode tnitem in treeViewNoxiousHabits.Nodes)
                        {
                            TreeNode tnOption = SelectChildrenRecursive(tnitem, item.i_ParentParameterId.ToString());

                            if (tnOption != null)
                            {
                                TreeNode childNode = new TreeNode();
                                childNode.Text = item.v_DiseasesName;
                                childNode.Name = item.v_Value1.ToString();
                                tnOption.Nodes.Add(childNode);
                                break;
                            }
                        }
                        break;
                }
            }
            treeViewNoxiousHabits.ExpandAll();
        }

        private void btnMoveNoxiousHabits_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            SystemParameterBL objSystemParameterBL = new SystemParameterBL();
            SystemParameterList objSystemParameterList = new SystemParameterList();

            NoxiousHabitsList objPersonMedicalHistory = new NoxiousHabitsList();

            _ResultlHabitosNoxivos = true;
            //Si no se selecciona nada sale
            if (treeViewNoxiousHabits.SelectedNode == null) return;
            //Si la lista temporal es null se la setea con una lista vacia
            if (_TempNoxiousHabitsList == null)
            {
                _TempNoxiousHabitsList = new List<NoxiousHabitsList>();
            }

            int ParameterId =int.Parse(treeViewNoxiousHabits.SelectedNode.Name.ToString());
            objSystemParameterList = objSystemParameterBL.GetParentNameSystemParameterHabitsNoxious(ref objOperationResult, ParameterId);

            if (objSystemParameterList == null) return;

            History.frmNoxiousHabitsPopup frm = new History.frmNoxiousHabitsPopup(treeViewNoxiousHabits.SelectedNode.Text,null,null);
            frm.ShowDialog();
            if (frm.DialogResult != System.Windows.Forms.DialogResult.OK) return;
            string TypeHabitsName = treeViewNoxiousHabits.SelectedNode.Text.ToString();

            //Busco en la lista temporal si ya se agrego el item seleccionado
            var findResult = _TempNoxiousHabitsList.Find(p => p.v_TypeHabitsName == TypeHabitsName);
            if (findResult == null)
            {
                objPersonMedicalHistory.v_NoxiousHabitsId = Guid.NewGuid().ToString();
                objPersonMedicalHistory.v_PersonId = _PacientId;
                objPersonMedicalHistory.v_Frequency = frm._Frequency;
                objPersonMedicalHistory.v_Comment = frm._Comment;
                objPersonMedicalHistory.i_TypeHabitsId = int.Parse(treeViewNoxiousHabits.SelectedNode.Name.ToString());
                objPersonMedicalHistory.v_TypeHabitsName = treeViewNoxiousHabits.SelectedNode.Text.ToString();
                objPersonMedicalHistory.i_RecordStatus = (int)RecordStatus.Agregado;
                objPersonMedicalHistory.i_RecordType = (int)RecordType.Temporal;
                _TempNoxiousHabitsList.Add(objPersonMedicalHistory);
            }
            else
            {
                if (findResult.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                {
                    if (findResult.i_RecordType == (int)RecordType.NoTemporal)// El registro Tiene in ID de BD
                    {
                        findResult.v_Frequency = frm._Frequency;
                        findResult.v_Comment = frm._Comment;
                        findResult.i_RecordStatus = (int)RecordStatus.Grabado;
                    }
                    else if (findResult.i_RecordType == (int)RecordType.Temporal) // El registro tiene un ID temporal [GUID]
                    {
                        findResult.v_Frequency = frm._Frequency;
                        findResult.v_Comment = frm._Comment;
                        findResult.i_RecordStatus = (int)RecordStatus.Agregado;
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione otro item. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            var dataList = _TempNoxiousHabitsList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

            // Cargar grilla
            grdDataNoxiousHabits.DataSource = new NoxiousHabitsList();
            grdDataNoxiousHabits.DataSource = dataList;
            grdDataNoxiousHabits.Refresh();
        }
       
        private void btnDeleteNoxiousHabits_Click(object sender, EventArgs e)
        {
            if (_NoxiousHabitsId != "")
            {
                _ResultlHabitosNoxivos = true;
                var findResult = _TempNoxiousHabitsList.Find(p => p.v_NoxiousHabitsId == _NoxiousHabitsId);
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;

                var dataList = _TempNoxiousHabitsList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdDataNoxiousHabits.DataSource = new NoxiousHabitsList();
                grdDataNoxiousHabits.DataSource = dataList;
                grdDataNoxiousHabits.Refresh();
            }
        }

        private void btnSaveNoxiousHabits_Click(object sender, EventArgs e)
        {
            _noxioushabitsDto = new List<noxioushabitsDto>();
            _noxioushabitsUpdate = new List<noxioushabitsDto>();
            _noxioushabitsDelete = new List<noxioushabitsDto>();

            OperationResult objOperationResult = new OperationResult();
            foreach (var item in _TempNoxiousHabitsList)
            {
                //Add
                if (item.i_RecordType == (int)RecordType.Temporal && item.i_RecordStatus == (int)RecordStatus.Agregado)
                {
                    noxioushabitsDto noxioushabitsDto = new noxioushabitsDto();

                    noxioushabitsDto.v_Frequency = item.v_Frequency;
                    noxioushabitsDto.v_Comment = item.v_Comment;
                    noxioushabitsDto.v_PersonId = _PacientId;
                    noxioushabitsDto.i_TypeHabitsId = item.i_TypeHabitsId;

                    _noxioushabitsDto.Add(noxioushabitsDto);
                }

                // Update
                if (item.i_RecordType == (int)RecordType.NoTemporal && (item.i_RecordStatus == (int)RecordStatus.Modificado || item.i_RecordStatus == (int)RecordStatus.Grabado))
                {
                    noxioushabitsDto noxioushabitsDto = new noxioushabitsDto();

                    noxioushabitsDto.v_NoxiousHabitsId = item.v_NoxiousHabitsId;
                    noxioushabitsDto.v_Frequency = item.v_Frequency;
                    noxioushabitsDto.v_Comment = item.v_Comment;
                    noxioushabitsDto.v_PersonId = _PacientId;

                    _noxioushabitsUpdate.Add(noxioushabitsDto);

                }

                //Delete
                if (item.i_RecordType == (int)RecordType.NoTemporal && item.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                {
                    noxioushabitsDto noxioushabitsDto = new noxioushabitsDto();

                    noxioushabitsDto.v_NoxiousHabitsId = item.v_NoxiousHabitsId;

                    _noxioushabitsDelete.Add(noxioushabitsDto);
                }
            }

            _objHistoryBL.AddNoxiousHabits(ref objOperationResult,
                                                    _noxioushabitsDto,
                                                    _noxioushabitsUpdate,
                                                    _noxioushabitsDelete,
                                                    Globals.ClientSession.GetAsList());

            BindGridNoxiousHabits();

            //// Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
                _ResultlHabitosNoxivos = false;
                //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                MessageBox.Show("Se grabo correctamente", "! INFORMACIÓN ¡", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else  // Operación con error
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Se queda en el formulario.
            }


           

        }

        private void btnCancelNoxiuosHabits_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolStripMenuEditNoxiousHabits_Click(object sender, EventArgs e)
        {
            History.frmNoxiousHabitsPopup frm = new History.frmNoxiousHabitsPopup(_NoxiousHabitsName,_Frecuency,_Comment);
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                _ResultlHabitosNoxivos = true;
                if (_objNoxiousHabitsyamc.i_RecordType == (int)RecordType.Temporal)
                {
                    _objNoxiousHabitsyamc.i_RecordStatus = (int)RecordStatus.Agregado;
                }
                else if (_objNoxiousHabitsyamc.i_RecordType == (int)RecordType.NoTemporal)
                {
                    _objNoxiousHabitsyamc.i_RecordStatus = (int)RecordStatus.Modificado;
                }

                _objNoxiousHabitsyamc.v_Frequency = frm._Frequency;
                _objNoxiousHabitsyamc.v_Comment = frm._Comment;

                _TempNoxiousHabitsList[_IndexNoxiousHabitsList] = _objNoxiousHabitsyamc;

                // Cargar grilla
                grdDataNoxiousHabits.DataSource = new NoxiousHabitsList();
                grdDataNoxiousHabits.DataSource = _TempNoxiousHabitsList;
                grdDataNoxiousHabits.Refresh();
            }
        }
        
        private void grdDataNoxiousHabits_MouseDown(object sender, MouseEventArgs e)
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
                    grdDataNoxiousHabits.Rows[row.Index].Selected = true;
                    btnDeleteNoxiousHabits.Enabled = true;
                    _NoxiousHabitsId = grdDataNoxiousHabits.Selected.Rows[0].Cells["v_NoxiousHabitsId"].Value.ToString();
                    _NoxiousHabitsName = grdDataNoxiousHabits.Selected.Rows[0].Cells["v_TypeHabitsName"].Value.ToString();

                }
            }


            if (e.Button == MouseButtons.Right)
            {
                //grdDataNoxiousHabits

                //string habitoNoc = grdDataNoxiousHabits.Selected.Rows[0].Cells["v_NoxiousHabitsId"].Value.ToString();

                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    grdDataNoxiousHabits.Rows[row.Index].Selected = true;
                    contextMenuNoxiousHabits.Items["ToolStripMenuEditNoxiousHabits"].Enabled = true;

                    _Comment = grdDataNoxiousHabits.Selected.Rows[0].Cells[4].Value.ToString();

                    if (grdDataNoxiousHabits.Selected.Rows[0].Cells[0].Value == null)
                    {
                        _NoxiousHabitsName = grdDataNoxiousHabits.Selected.Rows[0].Cells["v_TypeHabitsName"].Value.ToString();
                    }
                    else
                    {
                        _NoxiousHabitsName = grdDataNoxiousHabits.Selected.Rows[0].Cells["v_TypeHabitsName"].Value.ToString();
                    }


                    //_NoxiousHabitsName = grdDataNoxiousHabits.Selected.Rows[0].Cells[1].Value.ToString();
                    _Frecuency = grdDataNoxiousHabits.Rows[row.Index].Cells[3].Value.ToString();
                    _NoxiousHabitsId = grdDataNoxiousHabits.Selected.Rows[0].Cells["v_NoxiousHabitsId"].Value.ToString();
                    //_Comment = grdDataNoxiousHabits.Rows[row.Index].Cells[4].Value.ToString();
                    _objNoxiousHabitsyamc = _TempNoxiousHabitsList.FindAll(p => p.v_NoxiousHabitsId == _NoxiousHabitsId).FirstOrDefault();
                    _IndexNoxiousHabitsList = _TempNoxiousHabitsList.FindIndex(p => p.v_NoxiousHabitsId == _NoxiousHabitsId);
                }
                else
                {
                    contextMenuNoxiousHabits.Items["ToolStripMenuEditNoxiousHabits"].Enabled = false;
                }
            }
        }

        #endregion

        #region Family Medical Antecedents

        private void treeViewFamilyMedicalAntecedents_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewFamilyMedicalAntecedents.SelectedNode != null)
            {
                SystemParameterBL objSystemParameterBL = new SystemParameterBL();
                systemparameterDto objsystemparameterDto = new systemparameterDto();
                OperationResult objOperationResult = new OperationResult();
                int ParameterId = int.Parse(treeViewFamilyMedicalAntecedents.SelectedNode.Name.ToString());
                _GroupPopupFamilyMedical = treeViewFamilyMedicalAntecedents.SelectedNode.Text.ToString();
                objsystemparameterDto = objSystemParameterBL.GetSystemParameter(ref objOperationResult, 149, ParameterId);
                _ParentParameterId = objsystemparameterDto.i_ParentParameterId.Value;
                if (objsystemparameterDto.i_ParentParameterId == -1)
                {
                    btnOtrosFamilyMedical.Enabled = true;
                    btnMoveFamilyMedical.Enabled = false;
                }
                else
                {
                    btnOtrosFamilyMedical.Enabled = false;
                    btnMoveFamilyMedical.Enabled = true;
                }

            }
        }      

        private void BindGridFamilyMedicAntecendet()
        {
            var objData = GetDataFamilyMedicalAntecendent(0, null, "", null);
            _TempFamilyMedicalAntecedentsList = objData;
            grdDataFamilyMedical.DataSource = objData;
            lblRecordCountFamilyMedic.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            _TempFamilyMedicalAntecedentsListOld = GetDataFamilyMedicalAntecendent(0, null, "", null);
        }

        private List<FamilyMedicalAntecedentsList> GetDataFamilyMedicalAntecendent(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objHistoryBL.GetFamilyMedicalAntecedentsPagedAndFilteredByPersonId(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, _PacientId);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }
        
        private void LoadTreeFamilyMedicalAntecedents(int pintItemId)
        {
            OperationResult objOperationResult = new OperationResult();
            SystemParameterBL objSystemParameterBL = new SystemParameterBL();
            List<SystemParameterList> objSystemParameterList = new List<SystemParameterList>();

            treeViewFamilyMedicalAntecedents.Nodes.Clear();
            TreeNode nodePrimary = null;

            objSystemParameterList = objSystemParameterBL.GetSystemParametersPagedAndFiltered(ref objOperationResult, 0, null, "i_ParameterId,v_Value2", "i_GroupId==" + pintItemId, 0);

            foreach (var item in objSystemParameterList)
            {
                switch (item.i_ParentParameterId.ToString())
                {
                    #region Add Main Nodes
                    case "-1": // 1. Add Main nodes:
                        nodePrimary = new TreeNode();
                        nodePrimary.Text = item.v_Value1;
                        nodePrimary.Name = item.i_ParameterId.ToString();
                        treeViewFamilyMedicalAntecedents.Nodes.Add(nodePrimary);
                        break;
                    #endregion
                    default: // 2. Add Option nodes:
                        foreach (TreeNode tnitem in treeViewFamilyMedicalAntecedents.Nodes)
                        {
                            TreeNode tnOption = SelectChildrenRecursive(tnitem, item.i_ParentParameterId.ToString());

                            if (tnOption != null)
                            {
                                TreeNode childNode = new TreeNode();
                                childNode.Text = item.v_DiseasesName;
                                childNode.Name = item.i_ParameterId.ToString();
                                tnOption.Nodes.Add(childNode);
                                break;
                            }
                        }
                        break;
                }
            }
            treeViewFamilyMedicalAntecedents.ExpandAll();
        }

        private void btnOtrosFamilyMedical_Click(object sender, EventArgs e)
        {
            FamilyMedicalAntecedentsList objFamilyMedicAntecedent = new FamilyMedicalAntecedentsList();
            DiseasesList objDiseasesList = new DiseasesList();
            frmDiseases frm = new frmDiseases();
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.Cancel) return;
            objDiseasesList = frm._objDiseasesList;
            //History.frmFamilyMedicPopup frm = new History.frmFamilyMedicPopup(treeViewFamilyMedicalAntecedents.SelectedNode.Text, "", objSystemParameterList1.v_Value1, _ParentParameterId, null);
            History.frmFamilyMedicPopup frmFamilyMedicAntecedentPopup = new History.frmFamilyMedicPopup(objDiseasesList.v_Name, "", _GroupPopupFamilyMedical, _ParentParameterId,null);
            frmFamilyMedicAntecedentPopup.ShowDialog();

            if (frmFamilyMedicAntecedentPopup.DialogResult != System.Windows.Forms.DialogResult.OK) return;
            //Busco en la lista temporal si ya se agrego el item seleccionado
            var findResult = _TempFamilyMedicalAntecedentsList.Find(p => p.v_DiseasesId == objDiseasesList.v_DiseasesId);

            if (findResult == null)
            {
                objFamilyMedicAntecedent.v_FamilyMedicalAntecedentsId = Guid.NewGuid().ToString();
                objFamilyMedicAntecedent.v_PersonId = _PacientId;
                objFamilyMedicAntecedent.v_DiseasesId = objDiseasesList.v_DiseasesId;
                objFamilyMedicAntecedent.v_DiseaseName = objDiseasesList.v_Name;

                if (treeViewFamilyMedicalAntecedents.SelectedNode == null) // Boton otros (no tiene grupo)
                {
                    objFamilyMedicAntecedent.i_TypeFamilyId = int.Parse(treeViewFamilyMedicalAntecedents.SelectedNode.Name.ToString());
                    objFamilyMedicAntecedent.v_TypeFamilyName = "";
                    objFamilyMedicAntecedent.v_Comment = frmFamilyMedicAntecedentPopup._CommentFamilyMedic;
                    objFamilyMedicAntecedent.i_RecordStatus = (int)RecordStatus.Agregado;
                    objFamilyMedicAntecedent.i_RecordType = (int)RecordType.Temporal;
                    _TempFamilyMedicalAntecedentsList.Add(objFamilyMedicAntecedent);

                }
                else
                {
                    if (int.Parse(treeViewFamilyMedicalAntecedents.SelectedNode.Name.ToString()) == 1)
                    {
                        objFamilyMedicAntecedent.i_TypeFamilyId = 53;
                    }
                    else if (int.Parse(treeViewFamilyMedicalAntecedents.SelectedNode.Name.ToString()) == 2)
                    {
                        objFamilyMedicAntecedent.i_TypeFamilyId = 41;
                    }
                    else if (int.Parse(treeViewFamilyMedicalAntecedents.SelectedNode.Name.ToString()) == 3)
                    {
                        objFamilyMedicAntecedent.i_TypeFamilyId = 32;
                    }
                    else if (int.Parse(treeViewFamilyMedicalAntecedents.SelectedNode.Name.ToString()) == 13)
                    {
                        objFamilyMedicAntecedent.i_TypeFamilyId = 19;
                    }
                    else if (int.Parse(treeViewFamilyMedicalAntecedents.SelectedNode.Name.ToString()) == 66)
                    {
                        objFamilyMedicAntecedent.i_TypeFamilyId = 67;
                    }

                    //objFamilyMedicAntecedent.i_TypeFamilyId = int.Parse(treeViewFamilyMedicalAntecedents.SelectedNode.Name.ToString());
                    objFamilyMedicAntecedent.v_TypeFamilyName = _GroupPopupFamilyMedical;
                    objFamilyMedicAntecedent.v_Comment = frmFamilyMedicAntecedentPopup._CommentFamilyMedic;
                    objFamilyMedicAntecedent.i_RecordStatus = (int)RecordStatus.Agregado;
                    objFamilyMedicAntecedent.i_RecordType = (int)RecordType.Temporal;
                    _TempFamilyMedicalAntecedentsList.Add(objFamilyMedicAntecedent);
                }

               
            }
            else
            {
                if (findResult.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                {
                    if (findResult.i_RecordStatus == (int)RecordType.NoTemporal)// El registro Tiene in ID de BD
                    {
                        findResult.v_Comment = frmFamilyMedicAntecedentPopup._CommentFamilyMedic;
                        findResult.i_RecordStatus = (int)RecordStatus.Grabado;
                    }
                    else if (findResult.i_RecordStatus == (int)RecordType.Temporal) // El registro tiene un ID temporal [GUID]
                    {
                        findResult.v_Comment = frmFamilyMedicAntecedentPopup._CommentFamilyMedic;
                        findResult.i_RecordStatus = (int)RecordStatus.Agregado;
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione otro item. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            var dataList = _TempFamilyMedicalAntecedentsList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

            // Cargar grilla
            grdDataFamilyMedical.DataSource = new FamilyMedicalAntecedentsList();
            grdDataFamilyMedical.DataSource = dataList;
            grdDataFamilyMedical.Refresh();

        }

        private void btnMoveFamilyMedical_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            SystemParameterBL objSystemParameterBL = new SystemParameterBL();
            SystemParameterList objSystemParameterList = new SystemParameterList();
            SystemParameterList objSystemParameterList1 = new SystemParameterList();
            FamilyMedicalAntecedentsList objFamilyMedicalAntecedents = new FamilyMedicalAntecedentsList();


            _ResultMedicoFamiliares = true;
            //Si no se selecciona nada sale
            if (treeViewFamilyMedicalAntecedents.SelectedNode == null) return;

            //Si la lista temporal es null se la setea con una lista vacia
            if (_TempFamilyMedicalAntecedentsList == null)
            {
                _TempFamilyMedicalAntecedentsList = new List<FamilyMedicalAntecedentsList>();
            }

            int value1 = int.Parse(treeViewFamilyMedicalAntecedents.SelectedNode.Name.ToString());
            objSystemParameterList = objSystemParameterBL.GetSystemParameterFamilyMedicAntecedent(ref objOperationResult, value1);

            objSystemParameterList1 = objSystemParameterBL.GetGroupFamilyMedicAntecedent(ref objOperationResult, objSystemParameterList.i_ParentParameterId);
            if (objSystemParameterList == null) return;

            History.frmFamilyMedicPopup frm = new History.frmFamilyMedicPopup(treeViewFamilyMedicalAntecedents.SelectedNode.Text, "", objSystemParameterList1.v_Value1, _ParentParameterId, value1);
            frm.ShowDialog();
            if (frm.DialogResult != System.Windows.Forms.DialogResult.OK) return;
            //string DiseasesId = objSystemParameterList.v_DiseasesName;
            //int ParameterId = objSystemParameterList.i_ParameterId;
            //Busco en la lista temporal si ya se agrego el item seleccionado
            var findResult = _TempFamilyMedicalAntecedentsList.Find(p => p.i_TypeFamilyId == frm._ParameterId);
            if (findResult == null)
            {
                objFamilyMedicalAntecedents.v_FamilyMedicalAntecedentsId = Guid.NewGuid().ToString();
                objFamilyMedicalAntecedents.v_PersonId = _PacientId;
                objFamilyMedicalAntecedents.v_DiseasesId = frm._DiseasesId;
                objFamilyMedicalAntecedents.v_DiseaseName = treeViewFamilyMedicalAntecedents.SelectedNode.Text.ToString();
                objFamilyMedicalAntecedents.v_Comment = frm._CommentFamilyMedic;                
                objFamilyMedicalAntecedents.i_TypeFamilyId = int.Parse(treeViewFamilyMedicalAntecedents.SelectedNode.Name.ToString());
                objFamilyMedicalAntecedents.v_TypeFamilyName = frm._Group;
                objFamilyMedicalAntecedents.i_RecordStatus = (int)RecordStatus.Agregado;
                objFamilyMedicalAntecedents.i_RecordType = (int)RecordType.Temporal;
                _TempFamilyMedicalAntecedentsList.Add(objFamilyMedicalAntecedents);
            }
            else
            {
                if (findResult.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                {
                    if (findResult.i_RecordType == (int)RecordType.NoTemporal)// El registro Tiene in ID de BD
                    {
                        findResult.v_DiseasesId = frm._DiseasesId;
                        findResult.v_Comment = frm._CommentFamilyMedic;
                        findResult.i_RecordStatus = (int)RecordStatus.Grabado;
                    }
                    else if (findResult.i_RecordType == (int)RecordType.Temporal) // El registro tiene un ID temporal [GUID]
                    {
                        findResult.v_DiseasesId = frm._DiseasesId;
                        findResult.v_Comment = frm._CommentFamilyMedic;
                        findResult.i_RecordStatus = (int)RecordStatus.Agregado;
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione otro item. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            var dataList = _TempFamilyMedicalAntecedentsList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

            // Cargar grilla
            grdDataFamilyMedical.DataSource = new FamilyMedicalAntecedentsList();
            grdDataFamilyMedical.DataSource = dataList;
            grdDataFamilyMedical.Refresh();
        }

        private void btnRemoveFamilyMedical_Click(object sender, EventArgs e)
        {      
            if (_FamilyMedicAntecedentId != "")
            {
                _ResultMedicoFamiliares = true;
                var findResult = _TempFamilyMedicalAntecedentsList.Find(p => p.v_FamilyMedicalAntecedentsId == _FamilyMedicAntecedentId);
                findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;

                var dataList = _TempFamilyMedicalAntecedentsList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                grdDataFamilyMedical.DataSource = new FamilyMedicalAntecedentsList();
                grdDataFamilyMedical.DataSource = dataList;
                grdDataFamilyMedical.Refresh();
            }
        }

        private void btnSaveFamilyMedical_Click(object sender, EventArgs e)
        {
            _familymedicalantecedentsDto = new List<familymedicalantecedentsDto>();
            _familymedicalantecedentsUpdate = new List<familymedicalantecedentsDto>();
            _familymedicalantecedentsDelete = new List<familymedicalantecedentsDto>();

            OperationResult objOperationResult = new OperationResult();
            foreach (var item in _TempFamilyMedicalAntecedentsList)
            {

                //Add
                if (item.i_RecordType == (int)RecordType.Temporal && item.i_RecordStatus == (int)RecordStatus.Agregado)
                {
                    familymedicalantecedentsDto familymedicalantecedentsDto = new familymedicalantecedentsDto();

                    familymedicalantecedentsDto.i_TypeFamilyId = item.i_TypeFamilyId;
                    familymedicalantecedentsDto.v_PersonId = _PacientId;
                    familymedicalantecedentsDto.v_DiseasesId = item.v_DiseasesId;
                    familymedicalantecedentsDto.v_Comment = item.v_Comment;

                    _familymedicalantecedentsDto.Add(familymedicalantecedentsDto);
                }

                // Update
                if (item.i_RecordType == (int)RecordType.NoTemporal && (item.i_RecordStatus == (int)RecordStatus.Modificado || item.i_RecordStatus == (int)RecordStatus.Grabado))
                {
                    familymedicalantecedentsDto familymedicalantecedentsDto = new familymedicalantecedentsDto();

                    familymedicalantecedentsDto.v_FamilyMedicalAntecedentsId = item.v_FamilyMedicalAntecedentsId;
                    familymedicalantecedentsDto.i_TypeFamilyId = item.i_TypeFamilyId;
                    familymedicalantecedentsDto.v_PersonId = _PacientId;
                    familymedicalantecedentsDto.v_DiseasesId = item.v_DiseasesId;
                    familymedicalantecedentsDto.v_Comment = item.v_Comment;

                    _familymedicalantecedentsUpdate.Add(familymedicalantecedentsDto);

                }

                //Delete
                if (item.i_RecordType == (int)RecordType.NoTemporal && item.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                {
                    familymedicalantecedentsDto familymedicalantecedentsDto = new familymedicalantecedentsDto();

                    familymedicalantecedentsDto.v_FamilyMedicalAntecedentsId = item.v_FamilyMedicalAntecedentsId;

                    _familymedicalantecedentsDelete.Add(familymedicalantecedentsDto);
                }
            }

            _objHistoryBL.AddFamilyMedicalAntecedents(ref objOperationResult,
                                                    _familymedicalantecedentsDto,
                                                    _familymedicalantecedentsUpdate,
                                                    _familymedicalantecedentsDelete,
                                                    Globals.ClientSession.GetAsList());

            BindGridFamilyMedicAntecendet();

            //// Analizar el resultado de la operación
            if (objOperationResult.Success == 1)  // Operación sin error
            {
                //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                //this.Close();
                _ResultMedicoFamiliares = false;
                MessageBox.Show("Se grabo correctamente", "! INFORMACIÓN ¡", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else  // Operación con error
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Se queda en el formulario.
            }

           
        }

        private void btnCloseFamilyMedical_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuFamilyMedic_Click(object sender, EventArgs e)
        {
            //= grdDataOccupational.Selected.Rows[0].Cells["v_HistoryId"].Value.ToString();
            int ParameterId = int.Parse( grdDataFamilyMedical.Selected.Rows[0].Cells["i_ParameterId"].Value.ToString());
            int _ParentParameterId = int.Parse(grdDataFamilyMedical.Selected.Rows[0].Cells["i_ParentParameterId"].Value.ToString());
            History.frmFamilyMedicPopup frm = new History.frmFamilyMedicPopup(_DiagnosticName, _CommentFamilyMedic, _TypeFamilyName, _ParentParameterId, ParameterId);
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {

                _ResultMedicoFamiliares = true;
                if (_objFamilyMedicalAntecedentsListamc.i_RecordType == (int)RecordType.Temporal)
                {
                    _objFamilyMedicalAntecedentsListamc.i_RecordStatus = (int)RecordStatus.Agregado;
                }
                else if (_objFamilyMedicalAntecedentsListamc.i_RecordType == (int)RecordType.NoTemporal)
                {
                    _objFamilyMedicalAntecedentsListamc.i_RecordStatus = (int)RecordStatus.Modificado;
                }
                _objFamilyMedicalAntecedentsListamc.v_DiseasesId = frm._DiseasesId;
                _objFamilyMedicalAntecedentsListamc.v_Comment = frm._CommentFamilyMedic;
                _objFamilyMedicalAntecedentsListamc.v_DiseaseName = frm._DiseasesName;
                _TempFamilyMedicalAntecedentsList[_IndexFamilyMedicAntecendentList] = _objFamilyMedicalAntecedentsListamc;

                // Cargar grilla
                grdDataFamilyMedical.DataSource = new FamilyMedicalAntecedentsList();
                grdDataFamilyMedical.DataSource = _TempFamilyMedicalAntecedentsList;
                grdDataFamilyMedical.Refresh();
            }
        }

        private void grdDataFamilyMedical_MouseDown(object sender, MouseEventArgs e)
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
                    btnRemoveFamilyMedical.Enabled = true;
                    grdDataFamilyMedical.Rows[row.Index].Selected = true;
                    _FamilyMedicAntecedentId = grdDataFamilyMedical.Selected.Rows[0].Cells[0].Value.ToString();
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
                    grdDataFamilyMedical.Rows[row.Index].Selected = true;
                    contextMenuFamilyMedic.Items["toolStripMenuFamilyMedic"].Enabled = true;

                    _CommentFamilyMedic = grdDataFamilyMedical.Selected.Rows[0].Cells[6].Value.ToString();

                    if (grdDataFamilyMedical.Selected.Rows[0].Cells[3].Value == null)
                    {
                        _DiagnosticName = grdDataFamilyMedical.Selected.Rows[0].Cells[5].Value.ToString();
                    }
                    else
                    {
                        _DiagnosticName = grdDataFamilyMedical.Selected.Rows[0].Cells[3].Value.ToString();
                    }
                    
                    _TypeFamilyName = grdDataFamilyMedical.Selected.Rows[0].Cells[5].Value.ToString();
                    _FamilyMedicAntecedentId = grdDataFamilyMedical.Rows[row.Index].Cells[0].Value.ToString();
                    _objFamilyMedicalAntecedentsListamc = _TempFamilyMedicalAntecedentsList.FindAll(p => p.v_FamilyMedicalAntecedentsId == _FamilyMedicAntecedentId).FirstOrDefault();
                    _IndexFamilyMedicAntecendentList = _TempFamilyMedicalAntecedentsList.FindIndex(p => p.v_FamilyMedicalAntecedentsId == _FamilyMedicAntecedentId);
                }
                else
                {
                    contextMenuFamilyMedic.Items["toolStripMenuFamilyMedic"].Enabled = false;
                }
            }
        }
        #endregion            

    
        ////////////////////////////////************************************////////////////////////////////////////////////
  

        #region Properties

        //public byte[] FingerPrintTemplate { get; set; }

        //public byte[] FingerPrintImage { get; set; }

        public string Mode { get; set; }

        //public byte[] RubricImage { get; set; }

        //public string RubricImageText { get; set; }

        #endregion

        #region Constructor

        //public frmCapturedFingerPrint()
        //{
        //    InitializeComponent();
        //}

        #endregion

        #region Private Methods

        private void frmCapturedFingerPrint_Load(object sender, EventArgs e)
        {
            //// Iniciar el componente huellero
            //InitFingerPrint();

            //// Iniciar el componente Firma
            //sigPlusNET1.SetTabletState(1);

            //lblResultFirma.Text = "Sensor de firma conectado";
            //ShowHintImageFirma(3);

            //FAutoIdentify = false;       

            //if (Mode == "New")
            //{
            //    //EnrollFingerPrint(null, null);
            //}
            //else if (Mode == "Edit")
            //{
            //    if (FingerPrintImage == null) return;

            //    pbFingerPrint.Image = Common.Utils.byteArrayToImage(FingerPrintImage);

            //    if (RubricImageText ==  null) return;
                    
            //    sigPlusNET1.SetSigString(RubricImageText);

            //    //btnEnroll_Click(null, null);
            //}       
        }   
             
        private void ShowHintInfo(String s)
        {
            if (s != "")
            {
                memoHint.AppendText(s + Environment.NewLine);
                //lblresult.Text = s;
            }
        }

        //Show hint image
        private void ShowHintImage(int iType)
        {
            if (iType == 0)
            {
                imgNO.Visible = false;
                imgOK.Visible = false;
                imgInfo.Visible = false;
            }
            else if (iType == 1)
            {
                imgNO.Visible = false;
                imgOK.Visible = true;
                imgInfo.Visible = false;
            }
            else if (iType == 2)
            {
                imgNO.Visible = true;
                imgOK.Visible = false;
                imgInfo.Visible = false;
            }
            else if (iType == 3)
            {
                imgNO.Visible = false;
                imgOK.Visible = false;
                imgInfo.Visible = true;
            }
            this.Refresh();
        }

        private void ShowHintImageFirma(int iType)
        {
            if (iType == 0)
            {
                imgNoFirma.Visible = false;
                imgOkFirma.Visible = false;
                imgInfoFirma.Visible = false;
            }
            else if (iType == 1)
            {
                imgNoFirma.Visible = false;
                imgOkFirma.Visible = true;
                imgInfoFirma.Visible = false;
            }
            else if (iType == 2)
            {
                imgNoFirma.Visible = true;
                imgOkFirma.Visible = false;
                imgInfoFirma.Visible = false;
            }
            else if (iType == 3)
            {
                imgNoFirma.Visible = false;
                imgOkFirma.Visible = false;
                imgInfoFirma.Visible = true;
            }

            this.Refresh();
        }

        // Initilization FingerPrint
        private void InitFingerPrint()
        {
            //if (ZKFPEngX1.InitEngine() == 0)
            //{
            //    //btnInit.Enabled = false;
            //    FMatchType = 2;
            //    ShowHintInfo("Sensor conectado");
            //    lblresult.Text = "Sensor de Huella conectado";
            //    ShowHintImage(3);
            //    ZKFPEngX1.FPEngineVersion = "9";

            //    //Crear un espacio de caché de identificación de huellas dactilares y devuelve su identificador
            //    fpcHandle = ZKFPEngX1.CreateFPCacheDB();
            //    //EDSensorNum.Text = Convert.ToString(ZKFPEngX1.SensorCount);
            //    //EDSensorIndex.Text = Convert.ToString(ZKFPEngX1.SensorIndex);
            //    //EDSensorSN.Text = ZKFPEngX1.SensorSN;
            //    //ZKFPEngX1.EnrollCount = 3;
            //    //button1.Enabled = true;
            //}
            //else
            //{
            //    ShowHintInfo("Error al conectar el sensor de Huella");
            //}
        }

        //desconectar
        private void DisconnectFingerPrint()
        {
            //ZKFPEngX1.EndEngine();
            //btnInit.Enabled = true;
            //button1.Enabled = false;
        }

        //Comienzo de la inscripción de huellas dactilares
        private void EnrollFingerPrint(object sender, EventArgs e)
        {
            //ZKFPEngX1.CancelEnroll();
            //ZKFPEngX1.EnrollCount = 3;
           // ZKFPEngX1.BeginEnroll();
           // ShowHintInfo("Inicio de Registro");
          
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //DisconnectFingerPrint();
        }

        private void btnAutoverify_Click(object sender, EventArgs e)
        {
            //FAutoIdentify = true;
            //ZKFPEngX1.SetAutoIdentifyPara(FAutoIdentify, fpcHandle, 8);
            //FMatchType = 2;
        }      

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void FingerPrintImageDisposing()
        {
            //if (pbFingerPrint.Image != null)
            //{
            //    pbFingerPrint.Image.Dispose();

            //    pbFingerPrint.Image = null;
            //}
        }

        #endregion

        #region Private Events

        //Show fingerprint image
        private void ZKFPEngX1_OnImageReceived(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnImageReceivedEvent e)
        {
            // ShowHintImage(0);
            //Graphics g = pbFingerPrint.CreateGraphics();
            //Bitmap bmp = new Bitmap(pbFingerPrint.Width, pbFingerPrint.Height);
            //g = Graphics.FromImage(bmp);
            //int dc = g.GetHdc().ToInt32();
            //ZKFPEngX1.PrintImageAt(dc, 0, 0, bmp.Width, bmp.Height);
            //g.Dispose();
            //pbFingerPrint.Image = bmp;
        }

        private void ZKFPEngX1_OnEnroll(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnEnrollEvent e)
        {
            //if (e.actionResult)
            //{
            //    MessageBox.Show("Registro de Huella Dactilar Exitoso！ ", "ZK4500 Finger Print ", MessageBoxButtons.OK);
            //    //e.aTemplate = ZKFPEngX1.GetTemplate();
            //    //ZKFPEngX1.AddRegTemplateToFPCacheDB(fpcHandle, 1, e.aTemplate);

            //    ZKFPEngX1.AddRegTemplateStrToFPCacheDBEx(fpcHandle, 1, ZKFPEngX1.GetTemplateAsStringEx("9"), ZKFPEngX1.GetTemplateAsStringEx("10"));
            //    ShowHintInfo("Registro de Huella Dactilar Exitoso！");
            //    lblresult.Text = "Registro de Huella Dactilar Exitoso！";
            //    ShowHintImage(3);

            //}
            //else
            //{
            //    ShowHintInfo("Error en Registro de Huella Dactilar");
            //    MessageBox.Show("Error en Registro de Huella Dactilar ", "ZK4500 Finger Print ", MessageBoxButtons.OK);
            //    lblresult.Text = "Error en Registro de Huella Dactilar！";
            //    ShowHintImage(2);
            //}
        }

        private void ZKFPEngX1_OnFeatureInfo(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnFeatureInfoEvent e)
        {
            // String strTemp = "Fingerprint calidad";
            //if (e.aQuality != 0)
            //{
            //    strTemp = strTemp + " No buena";
            //    lblresult.Text = strTemp;
            //    ShowHintImage(2);
            //}
            //else
            //{
            //    strTemp = strTemp + " Bueno";
            //}
            //if (ZKFPEngX1.EnrollIndex != 1)
            //{
            //    if (ZKFPEngX1.IsRegister)
            //    {
            //        if (ZKFPEngX1.EnrollIndex - 1 > 0)
            //        {
            //            strTemp = strTemp + '\n' + "Estado de Registro: pulse su dedo " + Convert.ToString(ZKFPEngX1.EnrollIndex - 1) + " veces!";
            //            lblresult.Text = strTemp;
            //            ShowHintImage(3);
            //        }
            //    }
            //}
            //ShowHintInfo(strTemp);
        }

        private void ZKFPEngX1_OnCapture(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnCaptureEvent e)
        {
            //string stmp = "";
            //string Template = ZKFPEngX1.GetTemplateAsString();
            //bool ddd = false;
            //if (_FingerPrintImage == null)
            //{
            //   MessageBox.Show("El trabajador no tiene registrado su huella digital", "!INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //   lblValidationStatus.Text = "NO AUTENTICADO";
            //   lblValidationStatus.ForeColor = System.Drawing.Color.Red;
            //    return;
            //}
            //stmp = System.Convert.ToBase64String(_FingerPrintImage);

            //if (ZKFPEngX1.VerFingerFromStr(ref Template, stmp, false, ref ddd))
            //{
            //    MessageBox.Show("Huella dáctilar correcta", "!INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    _Validation = true;
            //    sigPlusNET1.SetTabletState(1);
            //    btnDelSignature.Enabled = true;
            //    lblValidationStatus.Text = "AUTENTICADO CORRECTAMENTE";
            //    lblValidationStatus.ForeColor = System.Drawing.Color.Blue;
            //}
            //else
            //{
            //    MessageBox.Show("Huella dáctilar incorrecta. Vuelva a intentar", "!ERROR DE VALIDACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //tabControl1.Enabled = false;
            //    _Validation = false;
            //    sigPlusNET1.SetTabletState(2);
            //    btnDelSignature.Enabled = false;
            //}
        }

        private void ZKFPEngX1_OnFingerTouching(object sender, EventArgs e)
        {
              //ShowHintInfo("Tocando");
        }

        private void ZKFPEngX1_OnFingerLeaving(object sender, EventArgs e)
        {
              //ShowHintInfo("Soltando");
        }

        private void btnDelSignature_Click(object sender, EventArgs e)
        {
            //sigPlusNET1.ClearTablet();
        }

        //private void btnDelFingerPrint_Click(object sender, EventArgs e)
        //{
        //    if (ZKFPEngX1.IsRegister)
        //    {
        //        FingerPrintImageDisposing();
        //        ZKFPEngX1.CancelEnroll();
        //        ZKFPEngX1.EnrollCount = 3;
        //        ZKFPEngX1.BeginEnroll();
        //        lblresult.Text = "Sensor de Huella conectado y Listo para iniciar registro.";
        //        ShowHintImage(3);
        //    }
        //    else
        //    {
        //        DialogResult rpta = MessageBox.Show("¿Desea volver a validar? ", "ZK4500 Finger Print ", MessageBoxButtons.YesNo);

        //        if (rpta == DialogResult.Yes)
        //        {
        //            FingerPrintImageDisposing();
        //            //ZKFPEngX1.CancelEnroll();
        //            //ZKFPEngX1.EnrollCount = 3;
        //            //ZKFPEngX1.BeginEnroll();
        //            lblresult.Text = "Sensor de Huella conectado y Listo para iniciar registro.";
        //            //ShowHintImage(3);
        //        }
        //    }
        //}

        #endregion

        private void btnSaveFingerAndRubric_Click(object sender, EventArgs e)
        {

        }

        private void frmHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            //HistoryBL objHistoryBL = new HistoryBL();
            //historyDto objhistoryDto = new historyDto();
            //OperationResult objOperationResult = new OperationResult();

            //if (FingerPrintImage ==null)
            //{
            //    // Imagen de la huella
            //    object image = null;
            //    ZKFPEngX1.GetFingerImage(ref image);

            //    FingerPrintImage = (byte[])image;
            //}
          

            //// Firma imagen   
            //sigPlusNET1.SetImageXSize(500);
            //sigPlusNET1.SetImageYSize(150);
            //sigPlusNET1.SetJustifyMode(5);

            //var myimage = sigPlusNET1.GetSigImage();
            //RubricImage = Common.Utils.imageToByteArray1(myimage);
            //myimage.Dispose();

            //// Firma serializada en formato ASCII hex string
            //RubricImageText = sigPlusNET1.GetSigString();

            //if (RubricImageText == "300D0A300D0A" && grdDataOccupational.Rows.Count() != 0)
            //{
            //    //MessageBox.Show("Se necesita registrar la firma digital del Trabajador", "!INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    //e.Cancel = true; 
            //}
            //else
            //{
            //    if (grdDataOccupational.Rows.Count() == 0) return;

            //    for (int i = 0; i < grdDataOccupational.Rows.Count(); i++)
            //    {
            //        objhistoryDto = objHistoryBL.GetHistory(ref objOperationResult, grdDataOccupational.Rows[i].Cells["v_HistoryId"].Value.ToString());
            //        objhistoryDto.b_FingerPrintImage = FingerPrintImage;
            //        objhistoryDto.b_RubricImage = RubricImage;
            //        objhistoryDto.t_RubricImageText = RubricImageText;
            //        objHistoryBL.UpdateHistoryFingerRubric(ref objOperationResult, objhistoryDto, Globals.ClientSession.GetAsList());
            //    }
            //}
        }

        private void ultraGrid2_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridLayout layout = e.Layout;
            UltraGridBand band = layout.Bands[0];
          
            layout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
            band.SortedColumns.Add("Enfermedad", false, true);

            // Bloquear la columna v_DiseaseName para que no se pueda editar
            UltraGridBand oBand = e.Layout.Bands[0];

            foreach (UltraGridColumn oCol in oBand.Columns)
            {
                if (oCol.Key == "v_DiseaseName")
                {
                    oCol.CellActivation = Activation.NoEdit;
                }
            }
        }

        private void ultraGrid2_CellChange(object sender, CellEventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            SystemParameterBL objSystemParameterBL = new SystemParameterBL();
            SystemParameterList objSystemParameterList = new SystemParameterList();
            PersonMedicalHistoryList objPersonMedicalHistory = new PersonMedicalHistoryList();
            int OldAnswer = 0;
            _ResultMedicoPersonales = true;
            if (e.Cell.Column.Key == "SI" && e.Cell.Text == "True")
            {                
                var i_ParameterId = e.Cell.Row.Cells["i_ParameterId"].Value;
                var rr =  _objSystemParameterList.Find(p => p.i_ParameterId == (int)i_ParameterId);
                OldAnswer = rr.i_Answer;
                rr.i_Answer = (int)SiNo.SI;
                
                foreach (UltraGridCell cell in e.Cell.Row.Cells)
                {
                    if (cell.Column.Key == "NO" || cell.Column.Key == "ND")
                    {
                        cell.Value = false;
                    }                  
                }                
            }
            if (e.Cell.Column.Key == "SI" && e.Cell.Text == "False")
            {
                e.Cell.Value = true;              
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////
            if (e.Cell.Column.Key == "NO" && e.Cell.Text == "True")
            {
                var i_ParameterId = e.Cell.Row.Cells["i_ParameterId"].Value;
                var rr = _objSystemParameterList.Find(p => p.i_ParameterId == (int)i_ParameterId);
                rr.i_Answer = (int)SiNo.NO;

                foreach (UltraGridCell cell in e.Cell.Row.Cells)
                {
                    if (cell.Column.Key == "SI" || cell.Column.Key == "ND")
                    {
                        cell.Value = false;
                    }
                }
          
                // si es no, buscar en la bolsa si esta, si se encuentra cambiar de flag en la _TempPersonMedicalHistoryList a un eliminado lógico
                var findResult = _TempPersonMedicalHistoryList.Find(p => p.v_DiseasesId == rr.v_Value1);
                if (findResult != null)
                {
                    findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
                    findResult.i_Answer = (int)SiNo.NO;
                    var dataList = _TempPersonMedicalHistoryList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                    grdDataPersonMedical.DataSource = new PersonMedicalHistoryList();
                    grdDataPersonMedical.DataSource = dataList;
                    grdDataPersonMedical.Refresh();
                }
            }
            if (e.Cell.Column.Key == "NO" && e.Cell.Text == "False")
            {
                e.Cell.Value = true;
               
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////
            if (e.Cell.Column.Key == "ND" && e.Cell.Text == "True")
            {
                var i_ParameterId = e.Cell.Row.Cells["i_ParameterId"].Value;
                var rr = _objSystemParameterList.Find(p => p.i_ParameterId == (int)i_ParameterId);
                rr.i_Answer = (int)SiNo.NONE;

                foreach (UltraGridCell cell in e.Cell.Row.Cells)
                {
                    if (cell.Column.Key == "NO" || cell.Column.Key == "SI")
                    {
                        cell.Value = false;
                    }
                }
                var findResult = _TempPersonMedicalHistoryList.Find(p => p.v_DiseasesId == rr.v_Value1);
                if (findResult != null)
                {
                    findResult.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
                    var dataList = _TempPersonMedicalHistoryList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);
                    grdDataPersonMedical.DataSource = new PersonMedicalHistoryList();
                    grdDataPersonMedical.DataSource = dataList;
                    grdDataPersonMedical.Refresh();
                }
            }
            if (e.Cell.Column.Key == "ND" && e.Cell.Text == "False")
            {
                e.Cell.Value = true;
               
            }

            if (e.Cell.Column.Key == "SI")
            {
                //Si la lista temporal es null se la setea con una lista vacía
                if (_TempPersonMedicalHistoryList == null)
                {
                    _TempPersonMedicalHistoryList = new List<PersonMedicalHistoryList>();
                }

                string value1 = e.Cell.Row.Cells["v_Value1"].Value.ToString();
                string Value2 = e.Cell.Row.Cells["v_Value2"].Value.ToString();
                objSystemParameterList = objSystemParameterBL.GetParentNameSystemParameter(ref objOperationResult, value1, 147);

                if (objSystemParameterList == null) return;

                History.frmPersonMedicalPopup frm = new History.frmPersonMedicalPopup(Value2, -1, DateTime.Now.Date, null, DateTime.Now.Date, null,null,null);
                frm.ShowDialog();
                if (frm.DialogResult != System.Windows.Forms.DialogResult.OK)
                {
                   
                    var x =   _objSystemParameterList.Find(p => p.v_Value1 == value1);                    
                    x.i_Answer = OldAnswer;
                    if (OldAnswer == (int)SiNo.NO)
                    {
                        x.NO = true;
                        x.SI = false;
                        x.ND = false;

                        foreach (UltraGridCell cell in e.Cell.Row.Cells)
                        {
                            if (cell.Column.Key == "SI" || cell.Column.Key == "ND")
                            {
                                cell.Value = false;
                            }
                        }
                    }
                    else if (OldAnswer == (int)SiNo.SI)
                    {
                        x.SI = true;
                        x.NO = false;
                        x.ND = false;

                        foreach (UltraGridCell cell in e.Cell.Row.Cells)
                        {
                            if (cell.Column.Key == "SI" || cell.Column.Key == "ND")
                            {
                                cell.Value = false;
                            }
                        }
                    }
                    else if (OldAnswer == (int)SiNo.NONE)
                    {
                        x.ND = true;
                        x.SI = false;
                        x.NO = false;

                        foreach (UltraGridCell cell in e.Cell.Row.Cells)
                        {
                            if (cell.Column.Key == "NO" || cell.Column.Key == "SI")
                            {
                                cell.Value = false;
                            }
                        }
                    }
                        ultraGrid2.DataSource = new List<SystemParameterList>();
                        ultraGrid2.Refresh();
                        ultraGrid2.DataSource = _objSystemParameterList;
                        this.ultraGrid2.Rows.ExpandAll(true);
                    return;
                } 
                string DiseasesId = value1;
               
                //Busco en la lista temporal si ya se agrego el item seleccionado
                var findResult = _TempPersonMedicalHistoryList.Find(p => p.v_DiseasesId == DiseasesId && p.d_StartDate == frm._StartDate);
                if (findResult == null)
                {
                    objPersonMedicalHistory.v_PersonMedicalHistoryId = Guid.NewGuid().ToString();
                    objPersonMedicalHistory.v_PersonId = _PacientId;
                    objPersonMedicalHistory.v_DiseasesId = DiseasesId;
                    objPersonMedicalHistory.v_GroupName = objSystemParameterList.v_Value1;
                    objPersonMedicalHistory.i_TypeDiagnosticId = frm._TypeDiagnosticId;
                    objPersonMedicalHistory.v_DiseasesName = Value2;
                    objPersonMedicalHistory.v_TypeDiagnosticName = frm._TypeDiagnosticName;
                    objPersonMedicalHistory.d_StartDate = frm._StartDate;
                    objPersonMedicalHistory.v_DiagnosticDetail = frm._DiagnosticDetail;
                    objPersonMedicalHistory.d_Date = frm._Date;
                    objPersonMedicalHistory.v_TreatmentSite = frm._TreatmentSite;
                    objPersonMedicalHistory.NombreHospital = frm._Hospital;
                    objPersonMedicalHistory.v_Complicaciones = frm._Complicaciones;
                    objPersonMedicalHistory.i_RecordStatus = (int)RecordStatus.Agregado;
                    objPersonMedicalHistory.i_RecordType = (int)RecordType.Temporal;
                    objPersonMedicalHistory.i_Answer = 1; // SI
                    _TempPersonMedicalHistoryList.Add(objPersonMedicalHistory);
                }
                else
                {
                    if (findResult.i_RecordStatus == (int)RecordStatus.EliminadoLogico)
                    {
                        if (findResult.i_RecordType == (int)RecordType.NoTemporal)// El registro Tiene in ID de BD
                        {
                            findResult.i_TypeDiagnosticId = frm._TypeDiagnosticId;
                            findResult.d_StartDate = frm._StartDate;
                            findResult.v_DiagnosticDetail = frm._DiagnosticDetail;
                            findResult.d_Date = frm._Date;
                            findResult.v_TreatmentSite = frm._TreatmentSite;
                            findResult.NombreHospital = frm._Hospital;
                            findResult.v_Complicaciones = frm._Complicaciones;
                            findResult.i_RecordStatus = (int)RecordStatus.Grabado;
                        }
                        else if (findResult.i_RecordType == (int)RecordType.Temporal) // El registro tiene un ID temporal [GUID]
                        {
                            findResult.i_TypeDiagnosticId = frm._TypeDiagnosticId;
                            findResult.d_StartDate = frm._StartDate;
                            findResult.v_DiagnosticDetail = frm._DiagnosticDetail;
                            findResult.d_Date = frm._Date;
                            findResult.v_TreatmentSite = frm._TreatmentSite;
                            findResult.NombreHospital = frm._Hospital;
                            findResult.v_Complicaciones = frm._Complicaciones;
                            findResult.i_RecordStatus = (int)RecordStatus.Agregado;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor seleccione otro item. ya existe", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                var dataList = _TempPersonMedicalHistoryList.FindAll(p => p.i_RecordStatus != (int)RecordStatus.EliminadoLogico);

                // Cargar grilla
                grdDataPersonMedical.DataSource = new PersonMedicalHistoryList();
                grdDataPersonMedical.DataSource = dataList;
                grdDataPersonMedical.Refresh();
            }
            
        }

        private void grdDataOccupational_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnDelete.Enabled = 
            btnEdit.Enabled = (grdDataOccupational.Selected.Rows.Count > 0);
        }

        private void grdDataPersonMedical_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            btnDeletePersonMedical.Enabled = (grdDataPersonMedical.Selected.Rows.Count > 0);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void tabControl1_TabStopChanged(object sender, EventArgs e)
        {
           
        }

        //public static bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        //{
        //    var cnt = new Dictionary<T, int>();
        //    foreach (T s in list1)
        //    {
        //        if (cnt.ContainsKey(s))
        //        {
        //            cnt[s]++;
        //        }
        //        else
        //        {
        //            cnt.Add(s, 1);
        //        }
        //    }
        //    foreach (T s in list2)
        //    {
        //        if (cnt.ContainsKey(s))
        //        {
        //            cnt[s]--;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    return cnt.Values.All(c => c == 0);
        //}

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
          //bool x =  ScrambledEquals(_TempPersonMedicalHistoryList, _TempPersonMedicalHistoryListOld);
            
            if (_ResultMedicoPersonales)
            {
                //tabControl1.SelectedTab = tabPage2;
                DialogResult Result = MessageBox.Show("¿Desea grabar los cambios realizados?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    btnSavePersonMedical_Click(sender, e);
                }
                else
                {
                    //grdDataPersonMedical.DataSource = _TempPersonMedicalHistoryListOld;
                    BindGridPersonMedical();
                }
                _ResultMedicoPersonales = false;
                TabControl Tab = sender as TabControl;
                tabControl1.SelectedTab = Tab.SelectedTab;
            }
            

            if (_ResultlHabitosNoxivos)
            {
                //tabControl1.SelectedTab = tabPage2;
                DialogResult Result = MessageBox.Show("¿Desea grabar los cambios realizados?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    btnSaveNoxiousHabits_Click(sender, e);
                }
                else
                {
                    BindGridNoxiousHabits();
                }
                    
                _ResultlHabitosNoxivos = false;
                TabControl Tab = sender as TabControl;
                tabControl1.SelectedTab = Tab.SelectedTab;
            }

            if (_ResultMedicoFamiliares)
            {
                //tabControl1.SelectedTab = tabPage2;
                DialogResult Result = MessageBox.Show("¿Desea grabar los cambios realizados?", "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    btnSaveFamilyMedical_Click(sender, e);
                }
                else
                {
                    BindGridFamilyMedicAntecendet();
                }
                _ResultMedicoFamiliares = false;
                TabControl Tab = sender as TabControl;
                tabControl1.SelectedTab = Tab.SelectedTab;
            }
           
        }

        private void contextMenuFamilyMedic_Opening(object sender, CancelEventArgs e)
        {

        }

    }
}
