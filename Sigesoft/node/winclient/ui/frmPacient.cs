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
using System.IO;
using System.Drawing.Imaging;
using Infragistics.Win.UltraWinGrid;
using Sigesoft.Node.WinClient.BE.Custom;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmPacient : Form
    {
        #region Delarations


        PacientBL _objBL = new PacientBL();

        //------------------------------------------------------------------------------------
        PacientBL _objPacientBL = new PacientBL();
        personDto objpersonDto;

        private string _fileName;
        private string _filePath;

        string PacientId;
        string Mode;
        string NumberDocument;
        string _personId;

        #endregion

        #region Properties


        public byte[] FingerPrintTemplate { get; set; }

        public byte[] FingerPrintImage { get; set; }

        public byte[] RubricImage { get; set; }

        public string RubricImageText { get; set; }

        #endregion

        //------------------------------------------------------------------------------------

        /// <summary>
        /// Para el comentario de las actualizaciones
        /// </summary>

        #region GetChanges
        string[] nombreCampos =
        {

            "txtName", "ddlDocTypeId", "txtFirstLastName", "txtDocNumber", "txtSecondLastName", "ddlSexTypeId", "txtBirthPlace", "ddlPlaceWorkId",
            "ddlSexTypeId", "txtBirthPlace", "dtpBirthdate", "ddlMaritalStatusId", "txtMail", "txtTelephoneNumber", "ddlLevelOfId", "ddlTypeOfInsuranceId",
            "ddlResidenceInWorkplaceId", "txtResidenceTimeInWorkplace", "txtAdressLocation", "txtNumberDependentChildren", "txtNumberLivingChildren", "ddlBloodGroupId",
            "ddlBloodFactorId", "txtNroPliza", "txtDecucible", "txtNacionalidad", "txtResideAnte", "txtReligion", "ddlDistricId", "txtCurrentOccupation",
            "txtResideAnte", "ddlProvinceId", "ddlDepartamentId", "ddlRelationshipId", "txtNombreTitular", "txtExploitedMineral", "ddlAltitudeWorkId", "ddlPlaceWorkId"
        };

        private List<Campos> ListValuesCampo = new List<Campos>();

        private string GetChanges()
        {
            string cadena = new PacientBL().GetComentaryUpdateByPersonId(PacientId);
            string oldComentary = cadena;
            cadena += "<FechaActualiza:" + DateTime.Now.ToString() + "|UsuarioActualiza:" + Globals.ClientSession.v_UserName + "|";
            bool change = false;
            foreach (var item in nombreCampos)
            {
                var fields = this.Controls.Find(item, true);
                string keyTagControl;
                string value1;

                if (fields.Length > 0)
                {
                    keyTagControl = fields[0].GetType().Name;
                    value1 = GetValueControl(keyTagControl, fields[0]);

                    var ValorCampo = ListValuesCampo.Find(x => x.NombreCampo == item).ValorCampo;
                    if (ValorCampo != value1)
                    {
                        cadena += item + ":" + ValorCampo + "|";
                        change = true;
                    }
                }
            }
            if (change)
            {
                return cadena;
            }

            return oldComentary;
        }

        private void SetOldValues()
        {
            ListValuesCampo = new List<Campos>();
            string keyTagControl = null;
            string value1 = null;
            foreach (var item in nombreCampos)
            {

                var fields = this.Controls.Find(item, true);

                if (fields.Length > 0)
                {
                    keyTagControl = fields[0].GetType().Name;
                    value1 = GetValueControl(keyTagControl, fields[0]);

                    Campos _Campo = new Campos();
                    _Campo.NombreCampo = item;
                    _Campo.ValorCampo = value1;
                    ListValuesCampo.Add(_Campo);
                }
            }
        }

        private string GetValueControl(string ControlId, Control ctrl)
        {
            string value1 = null;

            switch (ControlId)
            {
                case "TextBox":
                    value1 = ((TextBox)ctrl).Text;
                    break;
                case "ComboBox":
                    value1 = ((ComboBox)ctrl).Text;
                    break;
                case "CheckBox":
                    value1 = Convert.ToInt32(((CheckBox)ctrl).Checked).ToString();
                    break;
                case "RadioButton":
                    value1 = Convert.ToInt32(((RadioButton)ctrl).Checked).ToString();
                    break;
                case "DateTimePicker":
                    value1 = ((DateTimePicker)ctrl).Text; ;
                    break;
                case "UltraCombo":
                    value1 = ((UltraCombo)ctrl).Text; ;
                    break;
                default:
                    break;
            }

            return value1;
        }

        #endregion
        


        public frmPacient()
        {
            InitializeComponent();
        }

        public frmPacient(string personId)
        {
            _personId = personId;
            InitializeComponent();
        }

        private void frmAdministracion_Load(object sender, EventArgs e)
        {

            OperationResult objOperationResult = new OperationResult();

            List<KeyValueDTO> _formActions = Sigesoft.Node.WinClient.BLL.Utils.SetFormActionsInSession("frmPacient",
                                                                                        Globals.ClientSession.i_CurrentExecutionNodeId,
                                                                                        Globals.ClientSession.i_RoleId.Value,
                                                                                        Globals.ClientSession.i_SystemUserId);

            //Llenado de combos
            Utils.LoadDropDownList(ddlRelationshipId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 207, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAltitudeWorkId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 208, "i_ParameterId"), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPlaceWorkId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 204, null), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlMaritalStatusId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 101, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDocTypeId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 106, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSexTypeId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 100, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLevelOfId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 108, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlBloodGroupId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 154, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlBloodFactorId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 155, null), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlDepartamentId, "Value1", "Id", BLL.Utils.GetDataHierarchyForComboDepartamento(ref objOperationResult, 113, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlProvinceId, "Value1", "Id", BLL.Utils.GetDataHierarchyForComboProvincia(ref objOperationResult, 113, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDistricId, "Value1", "Id", BLL.Utils.GetDataHierarchyForComboDistrito_(ref objOperationResult, 113), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlResidenceInWorkplaceId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTypeOfInsuranceId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 188, null), DropDownListAction.Select);

            contextMenuStrip1.Items["mnuGridNuevo"].Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmPacient_ADD", _formActions);
            contextMenuStrip1.Items["mnuGridModificar"].Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmPacient_EDIT", _formActions);
            contextMenuStrip1.Items["mnuGridAntecedent"].Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmPacient_VIEW", _formActions);

            List<PersonList_2> ListaPerson = new List<PersonList_2>();
            PacientBL _PacientBL = new PacientBL();
            txtNombreTitular.Select();
            var lista = _PacientBL.LlenarPerson(ref objOperationResult);
            txtNombreTitular.DataSource = lista;
            txtNombreTitular.DisplayMember = "v_name";
            txtNombreTitular.ValueMember = "v_personId";

            txtNombreTitular.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
            txtNombreTitular.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.txtNombreTitular.DropDownWidth = 550;

            txtNombreTitular.DisplayLayout.Bands[0].Columns[0].Width = 20;
            txtNombreTitular.DisplayLayout.Bands[0].Columns[1].Width = 400;

            btnFilter_Click(sender, e);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {

            this.BindGrid();


            // Utilitario para obtener los tamaños de las columnas de la grilla
            //Clipboard.SetText(Utils.GetGridColumnsDetail(grdData));
        }

        private void BindGrid()
        {



            if (_personId != null)
            {
                //var findIndex = grdData.Rows.All.OfType<UltraGridRow>()
                //                .FirstOrDefault(p => p.Cells["v_PersonId"].Value.ToString() == _personId)                                 
                //                .Index;

                //if (findIndex != -1)
                //{
                //    grdData.Rows[findIndex].Selected = true;
                //}

                //_personId = null;
                OperationResult objOperationResult = new OperationResult();

                var Lista = _objBL.GetPacientsPagedAndFilteredByPErsonId(ref objOperationResult, 0, 99999, _personId);
                grdData.DataSource = Lista;
                if (grdData.Rows.Count > 0)
                {
                    txtFirstLastNameDocNumber.Text = Lista[0].v_DocNumber;// +" " + Lista[0].v_SecondLastName + " " + Lista[0].v_FirstName;
                    grdData.Rows[0].Selected = true;
                }
                _personId = null;
            }
            else
            {
                var objData = GetData(0, null, txtFirstLastNameDocNumber.Text);

                grdData.DataSource = objData;
                if (objData != null)
                {
                    lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
                }
                
                if (grdData.Rows.Count > 0)
                {
                    grdData.Rows[0].Selected = true;
                }
            }

        }

        private List<PacientList> GetData(int pintPageIndex, int? pintPageSize, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            string[] Apellidos = pstrFilterExpression.Split(' '); string apMat = ""; string apPat = ""; string nombre = "";
            int x = Apellidos.Count();
            List<PacientList> pacients = null;
            if (x == 1)
            {
                pacients = _objBL.GetPacientsPagedAndFiltered(ref objOperationResult, pintPageIndex, 99999, pstrFilterExpression);
            }
            else if (x == 2)
            {
                apPat = Apellidos[0]; apMat = Apellidos[1];
                pacients = _objBL.GetPacientsPagedAndFiltered_Apellidos(ref objOperationResult, pintPageIndex, 99999, pstrFilterExpression, apPat, apMat);

            }
            else if (x == 3)
            {
                apPat = Apellidos[0]; apMat = Apellidos[1]; nombre = Apellidos[2];
                pacients = _objBL.GetPacientsPagedAndFiltered_Apellidos_Nombre(ref objOperationResult, pintPageIndex, 99999, pstrFilterExpression, apPat, apMat, nombre);

            }
            else if (x >= 4)
            {
                MessageBox.Show("El criterio de búsqueda es de 3 palabras, no admite apellidos compuestos", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return pacients;
            }
           
            

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (pacients == null)
            {
                MessageBox.Show("No se encontraron resultados, busque así: ApPaterno ApMaterno Nombre", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pacients = _objBL.GetPacientsPagedAndFiltered(ref objOperationResult, pintPageIndex, 99999, "");
            }
            
            return pacients;
            
        }

        private void mnuGridNuevo_Click(object sender, EventArgs e)
        {
            frmPacientEdicion frm = new frmPacientEdicion("0", "New");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void mnuGridModificar_Click(object sender, EventArgs e)
        {
            //string strPacientId = grdData.SelectedRows[0].Cells[0].Value.ToString();

            string strPacientId = grdData.Selected.Rows[0].Cells[0].Value.ToString();

            frmPacientEdicion frm = new frmPacientEdicion(strPacientId, "Edit");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }

        }

        private void mnuGridEliminar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                string pstrPacientId = grdData.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
                _objBL.DeletePacient(ref objOperationResult, pstrPacientId, Globals.ClientSession.GetAsList());
                btnFilter_Click(sender, e);
            }
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

                    contextMenuStrip1.Items["verCambiosToolStripMenuItem"].Enabled = true;
                    grdData.Rows[row.Index].Selected = true;
                    contextMenuStrip1.Items["mnuGridNuevo"].Enabled = true;
                    contextMenuStrip1.Items["mnuGridModificar"].Enabled = true;
                    contextMenuStrip1.Items["mnuGridAntecedent"].Enabled = true;

                }
                else
                {
                    contextMenuStrip1.Items["verCambiosToolStripMenuItem"].Enabled = true;
                    contextMenuStrip1.Items["mnuGridNuevo"].Enabled = true;
                    contextMenuStrip1.Items["mnuGridModificar"].Enabled = false;
                    contextMenuStrip1.Items["mnuGridAntecedent"].Enabled = false;
                }

            }
        }

        private void mnuGridAntecedent_Click(object sender, EventArgs e)
        {
            string pstrPacientId = grdData.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            frmHistory frm = new frmHistory(pstrPacientId);
            frm.ShowDialog();
        }

        private void loadData(string strPacientId, string pstrMode)
        {

            Mode = pstrMode;
            PacientId = strPacientId;


            OperationResult objOperationResult = new OperationResult();
            dtpBirthdate.CustomFormat = "dd/MM/yyyy";

            if (Mode == "New")
            {
                // Additional logic here.
                dtpBirthdate.Checked = false;

                ddlDepartamentId.SelectedValue = "609";
                ddlProvinceId.SelectedValue = "610";
                ddlDistricId.SelectedValue = "611";

            }
            else if (Mode == "Edit")
            {
                PacientList objpacientDto = new PacientList();
                objpacientDto = _objPacientBL.GetPacient(ref objOperationResult, PacientId, null);

                ddlRelationshipId.SelectedValue = objpacientDto.i_Relationship == 0 ? "-1" : objpacientDto.i_Relationship.ToString();
                ddlAltitudeWorkId.SelectedValue = objpacientDto.i_AltitudeWorkId == 0 ? "-1" : objpacientDto.i_AltitudeWorkId.ToString();
                ddlPlaceWorkId.SelectedValue = objpacientDto.i_PlaceWorkId == 0 ? "-1" : objpacientDto.i_PlaceWorkId.ToString();
                txtExploitedMineral.Text = objpacientDto.v_ExploitedMineral;


                txtName.Text = objpacientDto.v_FirstName;
                txtFirstLastName.Text = objpacientDto.v_FirstLastName;
                txtSecondLastName.Text = objpacientDto.v_SecondLastName;
                ddlDocTypeId.SelectedValue = objpacientDto.i_DocTypeId.ToString();
                ddlSexTypeId.SelectedValue = objpacientDto.i_SexTypeId.ToString();
                ddlMaritalStatusId.SelectedValue = objpacientDto.i_MaritalStatusId.ToString();
                ddlLevelOfId.SelectedValue = objpacientDto.i_LevelOfId.ToString();
                txtDocNumber.Text = objpacientDto.v_DocNumber;
                NumberDocument = txtDocNumber.Text;
                dtpBirthdate.Value = (DateTime)objpacientDto.d_Birthdate;
                txtBirthPlace.Text = objpacientDto.v_BirthPlace;
                txtTelephoneNumber.Text = objpacientDto.v_TelephoneNumber;
                txtAdressLocation.Text = objpacientDto.v_AdressLocation;
                txtMail.Text = objpacientDto.v_Mail;
                ddlBloodGroupId.SelectedValue = objpacientDto.i_BloodGroupId.ToString();
                ddlBloodFactorId.SelectedValue = objpacientDto.i_BloodFactorId.ToString();

             
                var lista = _objPacientBL.GetAllPuestos();
                txtCurrentOccupation.DataSource = lista;
                txtCurrentOccupation.DisplayMember = "Puesto";
                txtCurrentOccupation.ValueMember = "Puesto";

                txtCurrentOccupation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                txtCurrentOccupation.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                this.txtCurrentOccupation.DropDownWidth = 350;
                txtCurrentOccupation.DisplayLayout.Bands[0].Columns[0].Width = 10;
                txtCurrentOccupation.DisplayLayout.Bands[0].Columns[1].Width = 350;

                if (!string.IsNullOrEmpty(objpacientDto.v_CurrentOccupation))
                {
                    txtCurrentOccupation.Value = objpacientDto.v_CurrentOccupation;
                }

                //txtCurrentOccupation.Text = objpacientDto.v_CurrentOccupation;
                txtNroPliza.Text = objpacientDto.v_NroPoliza;
                txtDecucible.Text = objpacientDto.v_Deducible.ToString();


                FingerPrintTemplate = objpacientDto.b_FingerPrintTemplate;
                FingerPrintImage = objpacientDto.b_FingerPrintImage;
                RubricImage = objpacientDto.b_RubricImage;
                RubricImageText = objpacientDto.t_RubricImageText;

                //ddlDepartamentId.SelectedValue = objpacientDto.i_DepartmentId == null ? "-1" : objpacientDto.i_DepartmentId.ToString();
                //ddlProvinceId.SelectedValue = objpacientDto.i_ProvinceId == null ? "-1" : objpacientDto.i_ProvinceId.ToString();
                //ddlDistricId.SelectedValue = objpacientDto.i_DistrictId == null ? "-1" : objpacientDto.i_DistrictId.ToString();

                ddlDistricId.SelectedValue = objpacientDto.i_DistrictId == null ? "-1" : objpacientDto.i_DistrictId.ToString();
                Utils.LoadDropDownList(ddlProvinceId, "Value1", "Id", BLL.Utils.ObtenerTodasProvincia(ref objOperationResult, 113), DropDownListAction.Select);

                ddlProvinceId.SelectedValue = objpacientDto.i_ProvinceId == null ? "-1" : objpacientDto.i_ProvinceId.ToString();
                Utils.LoadDropDownList(ddlDepartamentId, "Value1", "Id", BLL.Utils.ObtenerTodasDepartamentos(ref objOperationResult, 113), DropDownListAction.Select);
                ddlDepartamentId.SelectedValue = objpacientDto.i_DepartmentId == null ? "-1" : objpacientDto.i_DepartmentId.ToString();


                ddlResidenceInWorkplaceId.SelectedValue = objpacientDto.i_ResidenceInWorkplaceId == null ? "-1" : objpacientDto.i_ResidenceInWorkplaceId.ToString();
                txtResidenceTimeInWorkplace.Text = objpacientDto.v_ResidenceTimeInWorkplace;

                ddlTypeOfInsuranceId.SelectedValue = objpacientDto.i_TypeOfInsuranceId == null ? "-1" : objpacientDto.i_TypeOfInsuranceId.ToString();

                txtNumberLivingChildren.Text = objpacientDto.i_NumberLivingChildren.ToString();
                txtNumberDependentChildren.Text = objpacientDto.i_NumberDependentChildren.ToString();

                pbPersonImage.Image = Common.Utils.BytesArrayToImage(objpacientDto.b_Photo, pbPersonImage);
                txtNombreTitular.Text = objpacientDto.v_OwnerName;

                txtNacionalidad.Text = objpacientDto.v_Nacionalidad;
                txtResideAnte.Text = objpacientDto.v_ResidenciaAnterior;
                txtReligion.Text = objpacientDto.v_Religion ;

            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string Result = "";
            if (uvPacient.Validate(true, false).IsValid)
            {
                if (txtName.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Nombres.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtFirstLastName.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Apellido Paterno.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtSecondLastName.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Apellido Materno.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtDocNumber.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Número Documento.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtMail.Text.Trim() != "")
                {

                    if (!Sigesoft.Common.Utils.email_bien_escrito(txtMail.Text.Trim()))
                    {
                        MessageBox.Show("Por favor ingrese un Email con formato correcto.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (dtpBirthdate.Checked == false)
                {
                    MessageBox.Show("Por favor ingrese una fecha de nacimiento.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int caracteres = txtDocNumber.TextLength;
                if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == (int)Common.Document.DNI)
                {
                    if (caracteres != 8)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == (int)Common.Document.PASAPORTE)
                {
                    if (caracteres != 9)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == (int)Common.Document.LICENCIACONDUCIR)
                {
                    if (caracteres != 9)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == (int)Common.Document.CARNETEXTRANJ)
                {
                    if (caracteres < 9)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (Mode == "New")
                {
                    // Populate the entity
                    objpersonDto = new personDto();
                    objpersonDto.v_FirstName = txtName.Text.Trim();
                    objpersonDto.v_FirstLastName = txtFirstLastName.Text.Trim();
                    objpersonDto.v_SecondLastName = txtSecondLastName.Text.Trim();
                    objpersonDto.i_DocTypeId = Convert.ToInt32(ddlDocTypeId.SelectedValue);
                    objpersonDto.i_SexTypeId = Convert.ToInt32(ddlSexTypeId.SelectedValue);
                    objpersonDto.i_MaritalStatusId = Convert.ToInt32(ddlMaritalStatusId.SelectedValue);
                    objpersonDto.i_LevelOfId = Convert.ToInt32(ddlLevelOfId.SelectedValue);
                    objpersonDto.v_DocNumber = txtDocNumber.Text.Trim();
                    objpersonDto.d_Birthdate = dtpBirthdate.Value;
                    objpersonDto.v_BirthPlace = txtBirthPlace.Text.Trim();
                    objpersonDto.v_TelephoneNumber = txtTelephoneNumber.Text.Trim();
                    objpersonDto.v_AdressLocation = txtAdressLocation.Text.Trim();
                    objpersonDto.v_Mail = txtMail.Text.Trim();
                    objpersonDto.v_CurrentOccupation = txtCurrentOccupation.Text.Trim();
                    objpersonDto.i_BloodGroupId = Convert.ToInt32(ddlBloodGroupId.SelectedValue);
                    objpersonDto.i_BloodFactorId = Convert.ToInt32(ddlBloodFactorId.SelectedValue);
                    objpersonDto.v_NroPoliza = txtNroPliza.Text;
                    objpersonDto.v_Deducible = Convert.ToDecimal(txtDecucible.Text);
                    objpersonDto.i_Relationship = Convert.ToInt32(ddlRelationshipId.SelectedValue);
                    objpersonDto.i_AltitudeWorkId = Convert.ToInt32(ddlAltitudeWorkId.SelectedValue);
                    objpersonDto.i_PlaceWorkId = Convert.ToInt32(ddlPlaceWorkId.SelectedValue);
                    objpersonDto.v_ExploitedMineral = txtExploitedMineral.Text;
                    objpersonDto.v_Religion = txtReligion.Text;
                    objpersonDto.v_Nacionalidad = txtNacionalidad.Text;
                    objpersonDto.v_ResidenciaAnterior = txtResideAnte.Text;
                    objpersonDto.b_FingerPrintTemplate = FingerPrintTemplate;
                    objpersonDto.b_FingerPrintImage = FingerPrintImage;
                    objpersonDto.b_RubricImage = RubricImage;
                    objpersonDto.t_RubricImageText = RubricImageText;

                    objpersonDto.i_DepartmentId = Convert.ToInt32(ddlDepartamentId.SelectedValue);
                    objpersonDto.i_ProvinceId = Convert.ToInt32(ddlProvinceId.SelectedValue);
                    objpersonDto.i_DistrictId = Convert.ToInt32(ddlDistricId.SelectedValue);
                    objpersonDto.i_ResidenceInWorkplaceId = Convert.ToInt32(ddlResidenceInWorkplaceId.SelectedValue);
                    objpersonDto.v_ResidenceTimeInWorkplace = txtResidenceTimeInWorkplace.Text;
                    objpersonDto.i_TypeOfInsuranceId = Convert.ToInt32(ddlTypeOfInsuranceId.SelectedValue);
                    objpersonDto.i_NumberLiveChildren = Convert.ToInt32(txtNumberLivingChildren.Text);
                    objpersonDto.i_NumberDeadChildren = Convert.ToInt32(txtNumberDependentChildren.Text);


                    if (pbPersonImage.Image != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        Bitmap bm = new Bitmap(pbPersonImage.Image);
                        bm.Save(ms, ImageFormat.Bmp);

                        objpersonDto.b_PersonImage = Common.Utils.ResizeUploadedImage(ms);
                        pbPersonImage.Image.Dispose();
                    }
                    else
                    {
                        objpersonDto.b_PersonImage = null;
                    }

                    // Save the data
                    Result = _objPacientBL.AddPacient(ref objOperationResult, objpersonDto, Globals.ClientSession.GetAsList());

                }
                else if (Mode == "Edit")
                {
                    // Populate the entity
                    objpersonDto = new personDto();

                    objpersonDto = _objPacientBL.GetPerson(ref objOperationResult, PacientId);
                    objpersonDto.v_PersonId = PacientId;
                    objpersonDto.v_FirstName = txtName.Text.Trim();
                    objpersonDto.v_FirstLastName = txtFirstLastName.Text.Trim();
                    objpersonDto.v_SecondLastName = txtSecondLastName.Text.Trim();
                    objpersonDto.i_DocTypeId = Convert.ToInt32(ddlDocTypeId.SelectedValue);
                    objpersonDto.i_SexTypeId = Convert.ToInt32(ddlSexTypeId.SelectedValue);
                    objpersonDto.i_MaritalStatusId = Convert.ToInt32(ddlMaritalStatusId.SelectedValue);
                    objpersonDto.i_LevelOfId = Convert.ToInt32(ddlLevelOfId.SelectedValue);
                    objpersonDto.v_DocNumber = txtDocNumber.Text.Trim();
                    objpersonDto.d_Birthdate = dtpBirthdate.Value;
                    objpersonDto.v_BirthPlace = txtBirthPlace.Text.Trim();
                    objpersonDto.v_TelephoneNumber = txtTelephoneNumber.Text.Trim();
                    objpersonDto.v_AdressLocation = txtAdressLocation.Text.Trim();
                    objpersonDto.v_Mail = txtMail.Text.Trim();
                    objpersonDto.v_CurrentOccupation = txtCurrentOccupation.Text.Trim();
                    objpersonDto.b_FingerPrintTemplate = FingerPrintTemplate;
                    objpersonDto.b_FingerPrintImage = FingerPrintImage;
                    objpersonDto.b_RubricImage = RubricImage;
                    objpersonDto.t_RubricImageText = RubricImageText;

                    objpersonDto.v_NroPoliza = txtNroPliza.Text;
                    objpersonDto.v_Deducible = Convert.ToDecimal(txtDecucible.Text);

                    objpersonDto.i_Relationship = Convert.ToInt32(ddlRelationshipId.SelectedValue);
                    objpersonDto.i_AltitudeWorkId = Convert.ToInt32(ddlAltitudeWorkId.SelectedValue);
                    objpersonDto.i_PlaceWorkId = Convert.ToInt32(ddlPlaceWorkId.SelectedValue);
                    objpersonDto.v_ExploitedMineral = txtExploitedMineral.Text;

                    objpersonDto.i_BloodGroupId = Convert.ToInt32(ddlBloodGroupId.SelectedValue);
                    objpersonDto.i_BloodFactorId = Convert.ToInt32(ddlBloodFactorId.SelectedValue);


                    objpersonDto.i_DepartmentId = Convert.ToInt32(ddlDepartamentId.SelectedValue);
                    objpersonDto.i_ProvinceId = Convert.ToInt32(ddlProvinceId.SelectedValue);
                    objpersonDto.i_DistrictId = Convert.ToInt32(ddlDistricId.SelectedValue);
                    objpersonDto.i_ResidenceInWorkplaceId = Convert.ToInt32(ddlResidenceInWorkplaceId.SelectedValue);
                    objpersonDto.v_ResidenceTimeInWorkplace = txtResidenceTimeInWorkplace.Text;
                    objpersonDto.i_TypeOfInsuranceId = Convert.ToInt32(ddlTypeOfInsuranceId.SelectedValue);
                    objpersonDto.i_NumberLivingChildren = Convert.ToInt32(txtNumberLivingChildren.Text);
                    objpersonDto.i_NumberDependentChildren = Convert.ToInt32(txtNumberDependentChildren.Text);


                    if (pbPersonImage.Image != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        Bitmap bm = new Bitmap(pbPersonImage.Image);
                        bm.Save(ms, ImageFormat.Bmp);
                        objpersonDto.b_PersonImage = Common.Utils.ResizeUploadedImage(ms);
                        pbPersonImage.Image.Dispose();
                    }
                    else
                    {
                        objpersonDto.b_PersonImage = null;
                    }

                    // Save the data
                    Result = _objPacientBL.UpdatePacient(ref objOperationResult, objpersonDto, Globals.ClientSession.GetAsList(), NumberDocument, txtDocNumber.Text);

                    ActivarControles(false);
                    btnEditar.Enabled = true;
                    btnGuardar.Enabled = false;
                    btnNuevo.Enabled = true;
                    btnAntecedentes.Enabled = true;

                }
                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    return;
                    //this.Close();
                }
                else  // Operación con error
                {
                    if (Result == "-1")
                    {
                        MessageBox.Show("El Número de documento ya existe.", "! ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(Constants.GenericErrorMessage, "! ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Se queda en el formulario.
                    }

                }

            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            if (pbPersonImage.Image != null)
            {
                pbPersonImage.Image.Dispose();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "Image Files (*.jpg;*.gif;*.jpeg;*.png)|*.jpg;*.gif;*.jpeg;*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!IsValidImageSize(openFileDialog1.FileName))
                    return;

                // Seteaar propiedades del control PictutreBox
                LoadFile(openFileDialog1.FileName);
                //pbPersonImage.SizeMode = PictureBoxSizeMode.Zoom;
                txtFileName.Text = Path.GetFileName(openFileDialog1.FileName);
                // Setear propiedades de usuario
                _fileName = Path.GetFileName(openFileDialog1.FileName);
                _filePath = openFileDialog1.FileName;

                var Ext = Path.GetExtension(txtFileName.Text);

                if (Ext == ".JPG" || Ext == ".GIF" || Ext == ".JPEG" || Ext == ".PNG" || Ext == "")
                {

                    System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(pbPersonImage.Image);

                    Decimal Hv = 280;
                    Decimal Wv = 383;

                    Decimal k = -1;

                    Decimal Hi = bmp1.Height;
                    Decimal Wi = bmp1.Width;

                    Decimal Dh = -1;
                    Decimal Dw = -1;

                    Dh = Hi - Hv;
                    Dw = Wi - Wv;

                    if (Dh > Dw)
                    {
                        k = Hv / Hi;
                    }
                    else
                    {
                        k = Wv / Wi;
                    }

                    pbPersonImage.Height = (int)(k * Hi);
                    pbPersonImage.Width = (int)(k * Wi);
                }
            }
            else
            {
                return;
            }
        }
        private void LoadFile(string pfilePath)
        {
            Image img = pbPersonImage.Image;

            // Destruyo la posible imagen existente en el control
            //
            if (img != null)
            {
                img.Dispose();
            }

            using (FileStream fs = new FileStream(pfilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                Image original = Image.FromStream(fs);
                pbPersonImage.Image = original;

            }
        }

        private bool IsValidImageSize(string pfilePath)
        {
            using (FileStream fs = new FileStream(pfilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                Image original = Image.FromStream(fs);

                if (original.Width > Constants.WIDTH_MAX_SIZE_IMAGE || original.Height > Constants.HEIGHT_MAX_SIZE_IMAGE)
                {
                    MessageBox.Show("La imagen que está tratando de subir es damasiado grande.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        private void ddlDocTypeId_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlDocTypeId.Text == "--Seleccionar--") return;

            OperationResult objOperationResult = new OperationResult();
            DataHierarchyBL objDataHierarchyBL = new DataHierarchyBL();
            datahierarchyDto objDataHierarchyDto = new datahierarchyDto();

            int value = Int32.Parse(ddlDocTypeId.SelectedValue.ToString());
            objDataHierarchyDto = objDataHierarchyBL.GetDataHierarchy(ref objOperationResult, 106, value);
            txtDocNumber.MaxLength = Int32.Parse(objDataHierarchyDto.v_Value2);

        }

        private void txtDocNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == 1)
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
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pbPersonImage.Image = null;
        }

        private void btnWebCam_Click(object sender, EventArgs e)
        {
            try
            {
                frmCamera frm = new frmCamera();
                frm.ShowDialog();

                if (System.Windows.Forms.DialogResult.Cancel != frm.DialogResult)
                {
                    pbPersonImage.Image = frm._Image;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("ddd");
            }
        }

        private void btnCapturedFingerPrintAndRubric_Click(object sender, EventArgs e)
        {
            var frm = new frmCapturedFingerPrint();
            frm.Mode = Mode;

            if (Mode == "Edit")
            {
                frm.FingerPrintTemplate = FingerPrintTemplate;
                frm.FingerPrintImage = FingerPrintImage;
                frm.RubricImage = RubricImage;
                frm.RubricImageText = RubricImageText;
            }

            frm.ShowDialog();

            FingerPrintTemplate = frm.FingerPrintTemplate;
            FingerPrintImage = frm.FingerPrintImage;
            RubricImage = frm.RubricImage;
            RubricImageText = frm.RubricImageText;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            pbPersonImage.Image = null;
            loadData("0", "New");
            ActivarControles(true);
            btnEditar.Enabled = false;
            btnGuardar.Enabled = true;
            btnNuevo.Enabled = false;
            btnAntecedentes.Enabled = false;

            txtFileName.Text = "";
            txtName.Text = "";
            txtFirstLastName.Text = "";
            txtSecondLastName.Text = "";
            ddlMaritalStatusId.SelectedIndex = 0;

            txtDecucible.Text = "0";
            txtNroPliza.Text = "";

            ddlRelationshipId.SelectedIndex = 0;
            ddlAltitudeWorkId.SelectedIndex = 0;
            ddlPlaceWorkId.SelectedIndex = 0;
            txtExploitedMineral.Text = "";

            txtMail.Text = "";
            txtTelephoneNumber.Text = "";
            txtAdressLocation.Text = "";
            txtCurrentOccupation.Text = "";
            txtNombreTitular.Text = "";

            ddlBloodGroupId.SelectedIndex = 0;
            ddlDocTypeId.SelectedIndex = 0;
            txtDocNumber.Text = "";
            ddlSexTypeId.SelectedIndex = 0;
            ddlLevelOfId.SelectedIndex = 0;
            txtBirthPlace.Text = "";
            dtpBirthdate.Value = DateTime.Now;
            ddlBloodFactorId.SelectedIndex = 0;

            //ddlDepartamentId.SelectedValue = "-1";
            //ddlProvinceId.SelectedValue = "-1";
            //ddlDistricId.SelectedValue = "-1";
            ddlDepartamentId.SelectedValue = "609";
            ddlProvinceId.SelectedValue = "610";
            ddlDistricId.SelectedValue = "611";

            ddlResidenceInWorkplaceId.SelectedValue = "-1";
            txtResidenceTimeInWorkplace.Text = "";
            ddlTypeOfInsuranceId.SelectedValue = "-1";
            txtNumberLivingChildren.Text = "";
            txtNumberDependentChildren.Text = "";
            txtNacionalidad.Text = "";
            txtResideAnte.Text = "";
            txtReligion.Text = "";


        }

        private void ActivarControles(Boolean valor)
        {
            txtFileName.ReadOnly = !valor;
            txtName.ReadOnly = !valor;
            txtFirstLastName.ReadOnly = !valor;
            txtSecondLastName.ReadOnly = !valor;
            ddlMaritalStatusId.Enabled = valor;

            ddlRelationshipId.Enabled = valor;
            ddlAltitudeWorkId.Enabled = valor;
            ddlPlaceWorkId.Enabled = valor;
            txtExploitedMineral.ReadOnly = !valor;

            txtNroPliza.ReadOnly = !valor;
            txtDecucible.ReadOnly = !valor;

            txtMail.ReadOnly = !valor;
            txtTelephoneNumber.ReadOnly = !valor;
            txtAdressLocation.ReadOnly = !valor;
            txtCurrentOccupation.ReadOnly = !valor;
            txtNombreTitular.ReadOnly = !valor;
            //ddlBloodGroupId.Enabled = valor;
            ddlDocTypeId.Enabled = valor;
            txtDocNumber.ReadOnly = !valor;
            ddlSexTypeId.Enabled = valor;
            ddlLevelOfId.Enabled = valor;
            txtBirthPlace.ReadOnly = !valor;
            dtpBirthdate.Enabled = valor;
            //ddlBloodFactorId.Enabled = valor;

            btnWebCam.Enabled = valor;
            btnCapturedFingerPrintAndRubric.Enabled = valor;
            btnArchivo1.Enabled = valor;
            btnClear.Enabled = valor;

            ddlDepartamentId.Enabled = valor;
            ddlProvinceId.Enabled = valor;
            ddlDistricId.Enabled = valor;
            ddlResidenceInWorkplaceId.Enabled = valor;
            txtResidenceTimeInWorkplace.ReadOnly = !valor;
            ddlTypeOfInsuranceId.Enabled = valor;
            txtNumberLivingChildren.ReadOnly = !valor;
            txtNumberDependentChildren.ReadOnly = !valor;

            txtNacionalidad.ReadOnly = !valor;
            txtResideAnte.ReadOnly = !valor;
            txtReligion.ReadOnly = !valor;

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ActivarControles(true);
            btnEditar.Enabled = false;
            btnGuardar.Enabled = true;
            btnNuevo.Enabled = false;
            btnAntecedentes.Enabled = true;

            SetOldValues();
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void btnAntecedentes_Click(object sender, EventArgs e)
        {
            //string pstrPacientId = grdData.Selected.Rows[0].Cells[0].Value.ToString();
            string pstrPacientId = PacientId;
            frmHistory frm = new frmHistory(pstrPacientId);
            //frm.FingerPrintImage = FingerPrintImage;
            //frm.RubricImageText = RubricImageText;
            frm.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string Result = "";
            if (uvPacient.Validate(true, false).IsValid)
            {

                #region Validaciones

                if (txtName.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Nombres.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtFirstLastName.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Apellido Paterno.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtSecondLastName.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Apellido Materno.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtDocNumber.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Número Documento.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtMail.Text.Trim() != "")
                {

                    if (!Sigesoft.Common.Utils.email_bien_escrito(txtMail.Text.Trim()))
                    {
                        MessageBox.Show("Por favor ingrese un Email con formato correcto.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (dtpBirthdate.Checked == false)
                {
                    MessageBox.Show("Por favor ingrese una fecha de nacimiento.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int caracteres = txtDocNumber.TextLength;
                if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == (int)Common.Document.DNI)
                {
                    if (caracteres != 8)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == (int)Common.Document.PASAPORTE)
                {
                    if (caracteres != 9)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == (int)Common.Document.LICENCIACONDUCIR)
                {
                    if (caracteres != 9)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (int.Parse(ddlDocTypeId.SelectedValue.ToString()) == (int)Common.Document.CARNETEXTRANJ)
                {
                    if (caracteres < 9)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                #endregion

                if (Mode == "New")
                {
                    // Populate the entity
                    objpersonDto = new personDto();
                    objpersonDto.v_FirstName = txtName.Text.Trim();
                    objpersonDto.v_FirstLastName = txtFirstLastName.Text.Trim();
                    objpersonDto.v_SecondLastName = txtSecondLastName.Text.Trim();
                    objpersonDto.i_DocTypeId = Convert.ToInt32(ddlDocTypeId.SelectedValue);
                    objpersonDto.i_SexTypeId = Convert.ToInt32(ddlSexTypeId.SelectedValue);
                    objpersonDto.i_MaritalStatusId = Convert.ToInt32(ddlMaritalStatusId.SelectedValue);
                    objpersonDto.i_LevelOfId = Convert.ToInt32(ddlLevelOfId.SelectedValue);
                    objpersonDto.v_DocNumber = txtDocNumber.Text.Trim();
                    objpersonDto.d_Birthdate = dtpBirthdate.Value;
                    objpersonDto.v_BirthPlace = txtBirthPlace.Text.Trim();
                    objpersonDto.v_TelephoneNumber = txtTelephoneNumber.Text.Trim();
                    objpersonDto.v_AdressLocation = txtAdressLocation.Text.Trim();
                    objpersonDto.v_Mail = txtMail.Text.Trim();
                    objpersonDto.v_CurrentOccupation = txtCurrentOccupation.Text.Trim();
                    objpersonDto.i_BloodGroupId = Convert.ToInt32(ddlBloodGroupId.SelectedValue);
                    objpersonDto.i_BloodFactorId = Convert.ToInt32(ddlBloodFactorId.SelectedValue);

                    objpersonDto.v_NroPoliza = txtNroPliza.Text;
                    objpersonDto.v_Deducible = txtDecucible.Text == string.Empty ? (decimal?)null : Convert.ToDecimal(txtDecucible.Text);

                    objpersonDto.b_FingerPrintTemplate = FingerPrintTemplate;
                    objpersonDto.b_FingerPrintImage = FingerPrintImage;
                    objpersonDto.b_RubricImage = RubricImage;
                    objpersonDto.t_RubricImageText = RubricImageText;

                    objpersonDto.i_DepartmentId = Convert.ToInt32(ddlDepartamentId.SelectedValue);
                    objpersonDto.i_ProvinceId = Convert.ToInt32(ddlProvinceId.SelectedValue);
                    objpersonDto.i_DistrictId = Convert.ToInt32(ddlDistricId.SelectedValue);
                    objpersonDto.i_ResidenceInWorkplaceId = Convert.ToInt32(ddlResidenceInWorkplaceId.SelectedValue);
                    objpersonDto.v_ResidenceTimeInWorkplace = txtResidenceTimeInWorkplace.Text.Trim();
                    objpersonDto.i_TypeOfInsuranceId = Convert.ToInt32(ddlTypeOfInsuranceId.SelectedValue);
                    objpersonDto.i_NumberLivingChildren = txtNumberLivingChildren.Text == string.Empty ? (int?)null : int.Parse(txtNumberLivingChildren.Text);
                    objpersonDto.i_NumberDependentChildren = txtNumberDependentChildren.Text == string.Empty ? (int?)null : int.Parse(txtNumberDependentChildren.Text);

                    objpersonDto.i_Relationship = Convert.ToInt32(ddlRelationshipId.SelectedValue);
                    objpersonDto.i_AltitudeWorkId = Convert.ToInt32(ddlAltitudeWorkId.SelectedValue);
                    objpersonDto.i_PlaceWorkId = Convert.ToInt32(ddlPlaceWorkId.SelectedValue);
                    objpersonDto.v_ExploitedMineral = txtExploitedMineral.Text;
                    objpersonDto.v_OwnerName = txtNombreTitular.Text;

                    objpersonDto.v_Nacionalidad = txtNacionalidad.Text;
                    objpersonDto.v_ResidenciaAnterior = txtResideAnte.Text;
                    objpersonDto.v_Religion = txtReligion.Text;

                    if (pbPersonImage.Image != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        Bitmap bm = new Bitmap(pbPersonImage.Image);
                        bm.Save(ms, ImageFormat.Jpeg);
                        objpersonDto.b_PersonImage = Common.Utils.ResizeUploadedImage(ms);
                        pbPersonImage.Image.Dispose();
                    }
                    else
                    {
                        objpersonDto.b_PersonImage = null;
                    }

                    // Save the data
                    Result = _objPacientBL.AddPacient(ref objOperationResult, objpersonDto, Globals.ClientSession.GetAsList());

                }
                else if (Mode == "Edit")
                {
                    // Populate the entity
                    objpersonDto = new personDto();

                    objpersonDto = _objPacientBL.GetPerson(ref objOperationResult, PacientId);
                    objpersonDto.v_PersonId = PacientId;
                    objpersonDto.v_FirstName = txtName.Text.Trim();
                    objpersonDto.v_FirstLastName = txtFirstLastName.Text.Trim();
                    objpersonDto.v_SecondLastName = txtSecondLastName.Text.Trim();
                    objpersonDto.i_DocTypeId = Convert.ToInt32(ddlDocTypeId.SelectedValue);
                    objpersonDto.i_SexTypeId = Convert.ToInt32(ddlSexTypeId.SelectedValue);
                    objpersonDto.i_MaritalStatusId = Convert.ToInt32(ddlMaritalStatusId.SelectedValue);
                    objpersonDto.i_LevelOfId = Convert.ToInt32(ddlLevelOfId.SelectedValue);
                    objpersonDto.v_DocNumber = txtDocNumber.Text.Trim();
                    objpersonDto.d_Birthdate = dtpBirthdate.Value;
                    objpersonDto.v_BirthPlace = txtBirthPlace.Text.Trim();
                    objpersonDto.v_TelephoneNumber = txtTelephoneNumber.Text.Trim();
                    objpersonDto.v_AdressLocation = txtAdressLocation.Text.Trim();
                    objpersonDto.v_Mail = txtMail.Text.Trim();
                    objpersonDto.v_CurrentOccupation = txtCurrentOccupation.Text.Trim();
                    objpersonDto.b_FingerPrintTemplate = FingerPrintTemplate;
                    objpersonDto.b_FingerPrintImage = FingerPrintImage;
                    objpersonDto.b_RubricImage = RubricImage;
                    objpersonDto.t_RubricImageText = RubricImageText;
                    objpersonDto.i_BloodGroupId = Convert.ToInt32(ddlBloodGroupId.SelectedValue);
                    objpersonDto.i_BloodFactorId = Convert.ToInt32(ddlBloodFactorId.SelectedValue);
                    objpersonDto.v_NroPoliza = txtNroPliza.Text;
                    objpersonDto.v_Deducible = txtDecucible.Text == string.Empty ? (decimal?)null : Convert.ToDecimal(txtDecucible.Text);
                    objpersonDto.i_DepartmentId = Convert.ToInt32(ddlDepartamentId.SelectedValue);
                    objpersonDto.i_ProvinceId = Convert.ToInt32(ddlProvinceId.SelectedValue);
                    objpersonDto.i_DistrictId = Convert.ToInt32(ddlDistricId.SelectedValue);
                    objpersonDto.i_ResidenceInWorkplaceId = Convert.ToInt32(ddlResidenceInWorkplaceId.SelectedValue);
                    objpersonDto.v_ResidenceTimeInWorkplace = txtResidenceTimeInWorkplace.Text.Trim();
                    objpersonDto.i_TypeOfInsuranceId = Convert.ToInt32(ddlTypeOfInsuranceId.SelectedValue);
                    objpersonDto.i_NumberLivingChildren = txtNumberLivingChildren.Text == string.Empty ? (int?)null : int.Parse(txtNumberLivingChildren.Text);
                    objpersonDto.i_NumberDependentChildren = txtNumberDependentChildren.Text == string.Empty ? (int?)null : int.Parse(txtNumberDependentChildren.Text);
                    objpersonDto.i_Relationship = Convert.ToInt32(ddlRelationshipId.SelectedValue);
                    objpersonDto.i_AltitudeWorkId = Convert.ToInt32(ddlAltitudeWorkId.SelectedValue);
                    objpersonDto.i_PlaceWorkId = Convert.ToInt32(ddlPlaceWorkId.SelectedValue);
                    objpersonDto.v_OwnerName = txtNombreTitular.Text;
                    objpersonDto.v_ExploitedMineral = txtExploitedMineral.Text;

                    objpersonDto.v_Nacionalidad = txtNacionalidad.Text;
                    objpersonDto.v_ResidenciaAnterior = txtResideAnte.Text;
                    objpersonDto.v_Religion = txtReligion.Text;
                    objpersonDto.v_ComentaryUpdate = GetChanges();
                    if (pbPersonImage.Image != null)
                    {
                        MemoryStream ms = new MemoryStream();

                        Bitmap bm = new Bitmap(pbPersonImage.Image);
                        bm.Save(ms, ImageFormat.Jpeg);
                        objpersonDto.b_PersonImage = Common.Utils.ResizeUploadedImage(ms);
                        pbPersonImage.Image.Dispose();

                    }
                    else
                    {
                        objpersonDto.b_PersonImage = null;
                    }

                    // Save the data
                    Result = _objPacientBL.UpdatePacient(ref objOperationResult, objpersonDto, Globals.ClientSession.GetAsList(), NumberDocument, txtDocNumber.Text);

                    ActivarControles(false);
                    btnEditar.Enabled = true;
                    btnGuardar.Enabled = false;
                    btnNuevo.Enabled = true;
                    btnAntecedentes.Enabled = true;

                }
                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {

                    MessageBox.Show("Se grabó correctamente.", "! INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnFilter_Click(sender, e);
                    return;
                    //this.Close();
                }
                else  // Operación con error
                {
                    if (Result == "-1")
                    {
                        MessageBox.Show("El Número de documento ya existe.", "! ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(Constants.GenericErrorMessage, "! ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Se queda en el formulario.
                    }

                }

            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void ddlDepartamentId_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            //OperationResult objOperationResult = new OperationResult();
            //if (ddlDepartamentId.SelectedValue == null) return;
            //if (ddlDepartamentId.SelectedValue.ToString() == "-1")
            //{
            //    Utils.LoadDropDownList(ddlProvinceId, "Value1", "Id", BLL.Utils.GetDataHierarchyForComboProvincia(ref objOperationResult, 113, null), DropDownListAction.Select);
            //}
            //else
            //{
            //    Utils.LoadDropDownList(ddlProvinceId, "Value1", "Id", BLL.Utils.GetDataHierarchyForComboProvincia(ref objOperationResult, 113, int.Parse(ddlDepartamentId.SelectedValue.ToString())), DropDownListAction.Select);
            //}
        }

        private void ddlProvinceId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //OperationResult objOperationResult = new OperationResult();
            //if (ddlProvinceId.SelectedValue == null) return;
            //if (ddlDepartamentId.SelectedValue.ToString() == "-1")
            //{
            //    Utils.LoadDropDownList(ddlDistricId, "Value1", "Id", BLL.Utils.GetDataHierarchyForComboDistrito(ref objOperationResult, 113, null), DropDownListAction.Select);
            //}
            //else
            //{
            //    Utils.LoadDropDownList(ddlDistricId, "Value1", "Id", BLL.Utils.GetDataHierarchyForComboDistrito(ref objOperationResult, 113, int.Parse(ddlProvinceId.SelectedValue.ToString())), DropDownListAction.Select);
            //}


        }

        private void grdData_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grdData.Selected.Rows.Count == 0)
                return;

            string strPacientId = grdData.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            loadData(strPacientId, "Edit");
            ActivarControles(false);
            btnEditar.Enabled = true;
            btnGuardar.Enabled = false;
            btnNuevo.Enabled = true;
            btnAntecedentes.Enabled = true;
        }

        private void txtFirstLastNameDocNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.BindGrid();
            }
        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void dtpBirthdate_Validating(object sender, CancelEventArgs e)
        {
            if (dtpBirthdate.Checked)
            {
                if (dtpBirthdate.Value.Date > DateTime.Now.Date || dtpBirthdate.Value.Date == DateTime.Now.Date)
                {
                    e.Cancel = true;
                    MessageBox.Show("Fecha de nacimiento invalida. No puede ser mayor o igual a la fecha actual.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

        }

        private void ddlDistricId_Leave(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            var distritos = BLL.Utils.BuscarCoincidenciaDistritos(ref objOperationResult, 113, ddlDistricId.Text).OrderByDescending(p => p.Value4).ToList();
            var idDistrito = distritos[0].Value4.ToString();

            var provincia = BLL.Utils.ObtenerProvincia(ref objOperationResult, 113, int.Parse(idDistrito));
            Utils.LoadDropDownList(ddlProvinceId, "Value1", "Id", provincia, DropDownListAction.Select);
            if (provincia.Count > 1)
            {
                ddlProvinceId.SelectedValue = provincia[1].Id;
            }
            var idDepartamento = provincia[1].Value4.ToString();
            var departamento = BLL.Utils.ObtenerProvincia(ref objOperationResult, 113, int.Parse(idDepartamento));

            Utils.LoadDropDownList(ddlDepartamentId, "Value1", "Id", departamento, DropDownListAction.Select);

            if (departamento.Count > 1)
            {
                ddlDepartamentId.SelectedValue = departamento[1].Id;
            }
        }

        private void verCambiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string pacientId = grdData.Selected.Rows[0].Cells["v_PersonId"].Value.ToString();
            string commentary = _objPacientBL.GetComentaryUpdateByPersonId(pacientId);
            if (commentary == "" || commentary == null)
            {
                MessageBox.Show("Aún no se han realizado cambios.", "AVISO", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            var frm = new frmViewChanges(commentary);
            frm.ShowDialog();
        }

    }
}
