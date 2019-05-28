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
using ScrapperReniecSunat;
using Sigesoft.Node.WinClient.BE.Custom;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmEmpresa : Form
    {
        #region Declarations

        OrganizationBL _objBL1 = new OrganizationBL();
        string strFilterExpression1;
        ProtocolBL _oProtocolBL = new ProtocolBL();
        //--------------------------------------------------------------------------

        OrganizationBL _objBL = new OrganizationBL();
        organizationDto objOrganizationDto;

        LocationBL _objLocationBL = new LocationBL();
        locationDto objLocationDto;

        WarehouseBL _objWarehouseBL = new WarehouseBL();
        warehouseDto objWarehouseDto;

        GroupOccupationBL _objGroupOccupationBL = new GroupOccupationBL();
        groupoccupationDto objgroupoccupationDto;

        AreaBL _objAreaBL = new AreaBL();
        areaDto objareaDto;

        GesBL _objGesBL = new GesBL();
        gesDto objGesDto = new gesDto();

        OccupationBL _objOccupationBL = new OccupationBL();
        occupationDto objOccupationDto;
        organizationDto OldData = new organizationDto();
        string _empresaPlantillaId = "";
        string _nombreEmpresaPlantillaId = "";
        private CodigoEmpresaList _objCodigoEmpresaList = new CodigoEmpresaList();

        string _organizationId, Mode;
        string _locationId;
        string _OccupationId;
        string Temp_IdentificationNumber = null;
        string strFilterExpression;
        string ModeAlmacen;
        string ModeLocation;
        string ModeGESO;
        string ModeArea;
        string ModoGES;
        string ModeOccupation;
        private string _fileName;
        private string _filePath;
        string _SectorName;
        string _SectorCodigo;

        #endregion

        //--------------------------------------------------------------------------

        #region GetChanges
        string[] nombreCampos =
        {

            "ddlOrganizationypeId1", "txtCiiu", "txtIdentificationNumber", "txtSector", "txtName",
            "txtContacName1", "txtEmail", "txtContacto", "txtEmailContacto", "txtContactoeMedico", "txtEmailMedico",
            "txtAddress", "txtObservation", "txtPhoneNumber"
        };

        private List<Campos> ListValuesCampo = new List<Campos>();

        private string SetChanges()
        {
            string cadena = _objBL.GetComentaryUpdateByOrganizationId(_organizationId);
            if (cadena == null)
                cadena = "";
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
                    value1 = Convert.ToInt32(((CheckBox)ctrl).Checked).ToString() == "0" ? "SI" : "NO";
                    break;
                case "RadioButton":
                    value1 = Convert.ToInt32(((RadioButton)ctrl).Checked).ToString() == "0" ? "SI" : "NO";
                    break;
                default:
                    break;
            }

            return value1;
        }

        #endregion
        
        public frmEmpresa()
        {

            InitializeComponent();
            //grdData.AutoGenerateColumns = false;


            //------------------------------------------------------------------------------------------
            //Desactivar Autogeneracion de Colummnas grillas

            ModeAlmacen = "New";
            ModeLocation = "New";
            ModeGESO = "New";
            ModeArea = "New";
            ModoGES = "New";
            ModeOccupation = "New";

            //if (Mode == "New")
            //{
            //    tabControl1.Enabled = false;
            //}
            //---------------------------------------------------------------------------------------------
            LoadData("0", "New");

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
            this.Show();
            tabControl1.TabPages.Remove(tabGES);
            tabControl1.TabPages.Remove(tabPuesto);
            //tabControl1.TabPages.Remove(tabAlmacen);

            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);

            }


            #endregion

            OperationResult objOperationResult = new OperationResult();

            List<KeyValueDTO> _formActions = Sigesoft.Node.WinClient.BLL.Utils.SetFormActionsInSession("frmEmpresa", Globals.ClientSession.i_CurrentExecutionNodeId, Globals.ClientSession.i_RoleId.Value, Globals.ClientSession.i_SystemUserId);

            contextMenuStrip1.Items["mnuGridNuevo"].Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEmpresa_ADD", _formActions);
            contextMenuStrip1.Items["mnuGridModificar"].Enabled = Sigesoft.Node.WinClient.BLL.Utils.IsActionEnabled("frmEmpresa_EDIT", _formActions);

            // Establecer el filtro inicial para los datos
            strFilterExpression1 = null;

            //Llenado de combos
            Utils.LoadDropDownList(ddlOrganizationTypeId1, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 103, null), DropDownListAction.All);
            Utils.LoadDropDownList(ddlSectorTypeId1, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 104, null), DropDownListAction.All);


            // COMENTAR ESTA LÍNEA DE CÓDIGO... ASI DE SIMPLE
            //btnFilter_Click(sender, e);








            if (grdData.Rows.Count != 0)
                grdData.Rows[0].Selected = true;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddlOrganizationTypeId1.SelectedValue.ToString() != "-1") Filters.Add("i_OrganizationTypeId==" + ddlOrganizationTypeId1.SelectedValue);
            if (ddlSectorTypeId1.SelectedValue.ToString() != "-1") Filters.Add("i_SectorTypeId ==" + ddlSectorTypeId1.SelectedValue);
            if (!string.IsNullOrEmpty(txtIdentificationNumber1.Text)) Filters.Add("v_IdentificationNumber==" + "\"" + txtIdentificationNumber1.Text.Trim() + "\"");
            if (!string.IsNullOrEmpty(txtName1.Text)) Filters.Add("v_Name.Contains(\"" + txtName1.Text.Trim() + "\")");
            if (!string.IsNullOrEmpty(txtCiiuBus.Text)) Filters.Add("v_SectorCodigo.Contains(\"" + txtCiiuBus.Text.Trim() + "\")");
            Filters.Add("i_IsDeleted==0");
            // Create the Filter Expression
            strFilterExpression1 = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    strFilterExpression1 = strFilterExpression1 + item + " && ";
                }
                strFilterExpression1 = strFilterExpression1.Substring(0, strFilterExpression1.Length - 4);
            }

            this.BindGrid();

            // Utilitario para obtener los tamaños de las columnas de la grilla
            //Clipboard.SetText(Utils.GetGridColumnsDetail(grdData));
        }

        private void BindGrid()
        {

            var objData = GetData(0, null, "v_Name ASC", strFilterExpression1);

            grdData.DataSource = objData;
            lblRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());

            if (grdData.Rows.Count > 0)
                grdData.Rows[0].Selected = true;
        }

        private List<OrganizationList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objBL1.GetOrganizationsPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        private void mnuGridNuevo_Click(object sender, EventArgs e)
        {
            frmEmpresaEdicion frm = new frmEmpresaEdicion("0", "New");
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // Refrescar grilla
                btnFilter_Click(sender, e);
            }
        }

        private void mnuGridModificar_Click(object sender, EventArgs e)
        {
            string strOrganizationId = grdData.Selected.Rows[0].Cells[0].Value.ToString();

            frmEmpresaEdicion frm = new frmEmpresaEdicion(strOrganizationId, "Edit");
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
                // Delete the item
                //string pstrLocationId = grdData.SelectedRows[0].Cells[0].Value.ToString();
                string pstrOrganizationId = grdData.Selected.Rows[0].Cells[0].Value.ToString();

                //Verificar si tiene una Sede relacionada

                //Verificar si tiene un GESO relacionada

                //Verificar si tiene un Área relacionada

                //Verificar si tiene un GES relacionada

                //Verificar si tiene un Puesto relacionada

                //Verificar si tiene un Almacén relacionada


                _objBL1.DeleteOrganization(ref objOperationResult, pstrOrganizationId, Globals.ClientSession.GetAsList());

                btnFilter_Click(sender, e);
            }
        }

        private void grdData_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            Point point = new System.Drawing.Point(e.X, e.Y);
            Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

            if (uiElement == null || uiElement.Parent == null)
                return;

            Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

            if (row != null)
            {
                cmEmpresas.Items["verCambiosToolStripMenuItem"].Enabled = true;
                grdData.Rows[row.Index].Selected = true;
                contextMenuStrip1.Items["mnuGridNuevo"].Enabled = true;
                contextMenuStrip1.Items["mnuGridModificar"].Enabled = true;
            }
            else
            {
                cmEmpresas.Items["verCambiosToolStripMenuItem"].Enabled = false;
                contextMenuStrip1.Items["mnuGridNuevo"].Enabled = true;
                contextMenuStrip1.Items["mnuGridModificar"].Enabled = false;
            }

            //} 
        }

        private void grdData_BeforeRowActivate(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e)
        {

            //if (grdData.Rows.Count >0)
            //{                
            //    string strOrganizationId = e.Row.Cells[0].Value.ToString();
            //    LoadData(strOrganizationId, "Edit");
            //    ActivarControles(false);
            //    btnEditar.Enabled = true;
            //    btnOK.Enabled = false;
            //    btnNuevo.Enabled = true;
            //}
        }

        private void ActivarControles(Boolean valor)
        {

            ddlOrganizationypeId1.Enabled = valor;
            ddlSectorTypeId.Enabled = valor;
            txtName.ReadOnly = !valor;
            txtIdentificationNumber.ReadOnly = !valor;
            txtFactor.ReadOnly = !valor;
            txtFactorMed.ReadOnly = !valor;
            txtContacName1.ReadOnly = !valor;
            txtObservation.ReadOnly = !valor;
            rbConDx.Enabled = valor;
            rbSinDx.Enabled = valor;
            txtPhoneNumber.ReadOnly = !valor;
            txtEmail.ReadOnly = !valor;
            txtAddress.ReadOnly = !valor;
            txtContacto.ReadOnly = !valor;
            txtContactoeMedico.ReadOnly = !valor;
            txtEmailMedico.ReadOnly = !valor;

            txtEmailContacto.ReadOnly = !valor;

            groupBox3.Enabled = valor;
            groupBox4.Enabled = valor;
            groupBox5.Enabled = valor;
            groupBox6.Enabled = valor;
            groupBox7.Enabled = valor;
            groupBox8.Enabled = valor;
            groupBox9.Enabled = valor;
            groupBox10.Enabled = valor;
            groupBox11.Enabled = valor;
            groupBox12.Enabled = valor;
            pnlImage.Enabled = valor;

        }

        private void FillCombo()
        {
            OperationResult objOperationResult = new OperationResult();

            //TAB WAREHOUSE - Location
            Utils.LoadDropDownList(dllSearchWarehouseLocation, "Value1", "Id", BLL.Utils.GetLocation(ref objOperationResult, _organizationId), DropDownListAction.All);
            Utils.LoadDropDownList(dllInsertWarehouseLocation, "Value1", "Id", BLL.Utils.GetLocation(ref objOperationResult, _organizationId), DropDownListAction.Select);

            // TAB GESO - Location
            Utils.LoadDropDownList(ddlSearchGESOLocation, "Value1", "Id", BLL.Utils.GetLocation(ref objOperationResult, _organizationId), DropDownListAction.All);
            Utils.LoadDropDownList(dllInsertGESOLocation, "Value1", "Id", BLL.Utils.GetLocation(ref objOperationResult, _organizationId), DropDownListAction.Select);

            //TAB AREA - Location
            Utils.LoadDropDownList(ddlInsertAreaLocation, "Value1", "Id", BLL.Utils.GetLocation(ref objOperationResult, _organizationId), DropDownListAction.Select);

            //TAB GES - AREA
            Utils.LoadDropDownList(ddlSearchGESArea, "Value1", "Id", BLL.Utils.GetArea(ref objOperationResult, _organizationId), DropDownListAction.All);
            Utils.LoadDropDownList(dllInsertGESArea, "Value1", "Id", BLL.Utils.GetArea(ref objOperationResult, _organizationId), DropDownListAction.Select);

            //TAB OCCUPATION - GES
            Utils.LoadDropDownList(dllSearchOccupationArea, "Value1", "Id", BLL.Utils.GetArea(ref objOperationResult, _organizationId), DropDownListAction.Select);
            Utils.LoadDropDownList(dllSearchOccupationGES, "Value1", "Id", BLL.Utils.GetGES(ref objOperationResult, "-1"), DropDownListAction.All);
            Utils.LoadDropDownList(ddlSearchOccupationGESO, "Value1", "Id", BLL.Utils.GetGESO(ref objOperationResult, _organizationId), DropDownListAction.All);

            Utils.LoadDropDownList(ddlInsertOccupationArea, "Value1", "Id", BLL.Utils.GetArea(ref objOperationResult, _organizationId), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlInsertOccupationGES, "Value1", "Id", BLL.Utils.GetGES(ref objOperationResult, "-1"), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlInsertOccupationGESO, "Value1", "Id", BLL.Utils.GetGESO(ref objOperationResult, _organizationId), DropDownListAction.Select);
        }

        private void LoadData(string pstrOrganizationId, string pstrMode)
        {
            Mode = pstrMode;
            this.Text = "Empresa" + " (" + pstrOrganizationId + ")";
            _organizationId = pstrOrganizationId;
            Mode = pstrMode;
            OperationResult objOperationResult = new OperationResult();

            Utils.LoadDropDownList(ddlOrganizationypeId1, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 103, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSectorTypeId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 104, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlInsertCC, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 110, "v_Value2"), DropDownListAction.Select);

            FillCombo();
            if (Mode == "New")
            {
                // Additional logic here.

            }
            else if (Mode == "Edit")
            {
                // Get the Entity Data
                objOrganizationDto = new organizationDto();

                objOrganizationDto = _objBL.GetOrganization(ref objOperationResult, _organizationId);
                //PARA DETECTAR LOS CAMBIOS
                OldData = objOrganizationDto;
                //////
                ddlOrganizationypeId1.SelectedValue = objOrganizationDto.i_OrganizationTypeId.ToString();
                ddlSectorTypeId.SelectedValue = objOrganizationDto.i_SectorTypeId.ToString();
                txtName.Text = objOrganizationDto.v_Name;
                txtIdentificationNumber.Text = objOrganizationDto.v_IdentificationNumber;
                txtAddress.Text = objOrganizationDto.v_Address;
                txtPhoneNumber.Text = objOrganizationDto.v_PhoneNumber;
                txtContacName1.Text = objOrganizationDto.v_ContacName;
                txtObservation.Text = objOrganizationDto.v_Observation;
                if (objOrganizationDto.i_NumberQuotasMen == (int)EmpresaDx.ConDx)
                {
                    rbConDx.Checked = true;
                    rbSinDx.Checked = false;
                }
                else
                {
                    rbConDx.Checked = false;
                    rbSinDx.Checked = true;
                }


                txtEmail.Text = objOrganizationDto.v_Mail;
                txtSector.Text = objOrganizationDto.v_SectorName;
                txtCiiu.Text = objOrganizationDto.v_SectorCodigo;
                txtContacto.Text = objOrganizationDto.v_Contacto;
                txtContactoeMedico.Text = objOrganizationDto.v_ContactoMedico;
                txtEmailMedico.Text = objOrganizationDto.v_EmailMedico;
                txtEmailContacto.Text = objOrganizationDto.v_EmailContacto;
                Temp_IdentificationNumber = objOrganizationDto.v_IdentificationNumber;
                txtFactor.Text = objOrganizationDto.r_Factor.ToString();
                txtFactorMed.Text = objOrganizationDto.r_FactorMed.ToString();


                pbEmpresaImage.Image = Common.Utils.BytesArrayToImage(objOrganizationDto.b_Image, pbEmpresaImage);

                SetOldValues();
            }

            //Cargar Grillas
            string pstrFilterExpression = string.Format("v_OrganizationId ==\"" + _organizationId + "\"");

            var objDataLocation = _objLocationBL.GetLocationPagedAndFiltered(ref objOperationResult, 0, null, "V_Name ASC", pstrFilterExpression);
            grdDataLocation.DataSource = objDataLocation;
            lblLocationRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataLocation.Count());
            if (grdDataLocation.Rows.Count != 0)
                grdDataLocation.Rows[0].Selected = true;

            var objDataWarehouse = _objWarehouseBL.GetWarehousePagedAndFiltered(ref objOperationResult, 0, null, "v_LocationIdName,V_Name ASC", pstrFilterExpression);
            grdDataWarehouse.DataSource = objDataWarehouse;
            lblWarehouseRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataWarehouse.Count());
            if (grdDataWarehouse.Rows.Count != 0)
                grdDataWarehouse.Rows[0].Selected = true;

            var objGroupOccupation = _objGroupOccupationBL.GetGroupOccupationPagedAndFiltered(ref objOperationResult, 0, null, "v_LocationName,V_Name ASC", "", _organizationId);
            grdDataGESO.DataSource = objGroupOccupation;
            lblGroupOccupationRecordCount.Text = string.Format("Se encontraron {0} registros.", objGroupOccupation.Count());
            if (grdDataGESO.Rows.Count != 0)
                grdDataGESO.Rows[0].Selected = true;

            var objDataArea = _objAreaBL.GetAreaPagedAndFiltered(ref objOperationResult, 0, null, "v_LocationName,V_Name ASC", "", _organizationId);
            grdDataArea.DataSource = objDataArea;
            lblAreaRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataArea.Count());
            if (grdDataArea.Rows.Count != 0)
                grdDataArea.Rows[0].Selected = true;

            var objDataGES = _objGesBL.GetGESPagedAndFiltered(ref objOperationResult, 0, null, "v_AreaName,V_Name ASC", "", _organizationId);
            grdDataGes.DataSource = objDataGES;
            lblGesRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataGES.Count());
            if (grdDataGes.Rows.Count != 0)
                grdDataGes.Rows[0].Selected = true;

            var objDataOccupation = _objOccupationBL.GetOccupationPagedAndFiltered(ref objOperationResult, 0, null, "v_GesName,v_GroupOccupationName,V_Name ASC", "", _organizationId);
            grdDataOccupation.DataSource = objDataOccupation;
            lblOccupationRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataOccupation.Count());
            if (grdDataOccupation.Rows.Count != 0)
                grdDataOccupation.Rows[0].Selected = true;
        }

        #region Location
        private void btnInsertLocation_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            if (uvSede.Validate(true, false).IsValid)
            {
                #region Validaciones

                if (txtInsertLocation.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para la Sede.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("¿Está seguro de agregar / modificar la sede?.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }

                #endregion

                if (ModeLocation == "New")
                {
                    objLocationDto = new locationDto();

                    // Populate the entity
                    objLocationDto.v_OrganizationId = _organizationId;
                    objLocationDto.v_Name = txtInsertLocation.Text;
                    // Save the data
                    _locationId = _objLocationBL.AddLocation(ref objOperationResult, objLocationDto, Globals.ClientSession.GetAsList());

                }
                else if (ModeLocation == "Edit")
                {

                    // Populate the entity
                    objLocationDto.v_OrganizationId = _organizationId;
                    objLocationDto.v_Name = txtInsertLocation.Text;

                    // Save the data
                    _locationId = _objLocationBL.UpdateLocation(ref objOperationResult, objLocationDto, Globals.ClientSession.GetAsList());

                }


                //Rutina para Asignar la Empresa creada automaticamente al nodo actual
                NodeOrganizationLoactionWarehouseList objNodeOrganizationLoactionWarehouseList = new NodeOrganizationLoactionWarehouseList();
                List<nodeorganizationlocationwarehouseprofileDto> objnodeorganizationlocationwarehouseprofileDto = new List<nodeorganizationlocationwarehouseprofileDto>();

                //Llenar Entidad Empresa/sede
                objNodeOrganizationLoactionWarehouseList.i_NodeId = Globals.ClientSession.i_CurrentExecutionNodeId;
                objNodeOrganizationLoactionWarehouseList.v_OrganizationId = _organizationId;
                objNodeOrganizationLoactionWarehouseList.v_LocationId = _locationId;

                //Llenar Entidad Almacén
                var objInsertWarehouseList = InsertWarehouse(objLocationDto.v_LocationId);


                _objBL.AddNodeOrganizationLoactionWarehouse(ref objOperationResult, objNodeOrganizationLoactionWarehouseList, objInsertWarehouseList, Globals.ClientSession.GetAsList());



                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    //ACTUALIZAR GRILLA
                    string pstrFilterExpression = string.Format("v_OrganizationId ==\"" + _organizationId + "\"");
                    var objDataLocation = _objLocationBL.GetLocationPagedAndFiltered(ref objOperationResult, 0, null, "V_Name ASC", pstrFilterExpression);
                    grdDataLocation.DataSource = objDataLocation;
                    lblLocationRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataLocation.Count());

                    ModeLocation = "New";

                    FillCombo();
                }
                else  // Operación con error
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Se queda en el formulario.
                }

                //Limpiar controles
                txtInsertLocation.Text = "";
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private List<nodeorganizationlocationwarehouseprofileDto> InsertWarehouse(string pstrLocationId)
        {
            OperationResult objOperationResult = new OperationResult();
            List<nodeorganizationlocationwarehouseprofileDto> objwarehouseListAdd = new List<nodeorganizationlocationwarehouseprofileDto>();

            string pstrFilterExpression = "v_LocationId ==" + "\"" + pstrLocationId + "\"";
            var _objData = _objWarehouseBL.GetWarehousePagedAndFiltered(ref objOperationResult, 0, null, "", pstrFilterExpression);

            // Datos de Almacén
            foreach (var item in _objData)
            {
                // Datos de Almacen
                nodeorganizationlocationwarehouseprofileDto objWarehouse = new nodeorganizationlocationwarehouseprofileDto();
                objWarehouse.i_NodeId = Globals.ClientSession.i_CurrentExecutionNodeId;
                objWarehouse.v_OrganizationId = item.v_OrganizationId;
                objWarehouse.v_LocationId = item.v_LocationId;
                objWarehouse.v_WarehouseId = item.v_WarehouseId;

                objwarehouseListAdd.Add(objWarehouse);

            }

            return objwarehouseListAdd.Count == 0 ? null : objwarehouseListAdd;
        }

        private void mnuGridLocationDelete_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            //// Obtener los IDs de la fila seleccionada

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                //Obtener el Id de LocationId
                string pstrLocationId = grdDataLocation.Selected.Rows[0].Cells[0].Value.ToString();

                string pstrFilterExpression = "v_LocationId ==" + "\"" + pstrLocationId + "\"";

                // Verificar si Sede está relacionada a un GESO                
                var _objDataGESO = _objGroupOccupationBL.GetGroupOccupationPagedAndFiltered(ref objOperationResult, 0, null, "", pstrFilterExpression, _organizationId);
                // Verificar si Sede está relacionada a un Área
                var objDataArea = _objAreaBL.GetAreaPagedAndFiltered(ref objOperationResult, 0, null, "", pstrFilterExpression, _organizationId);
                //Verificar si Almacén está relacionada con un Almacén
                var _objDataWarehouse = _objWarehouseBL.GetWarehousePagedAndFiltered(ref objOperationResult, 0, null, "", pstrFilterExpression);

                if (_objDataGESO.Count() != 0)
                {
                    MessageBox.Show("La sede esta relacionada con un GESO", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (objDataArea.Count() != 0)
                {
                    MessageBox.Show("La sede esta relacionada con un Área", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (_objDataWarehouse.Count() != 0)
                {
                    MessageBox.Show("La sede esta relacionada con un Almacén", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Delete the item

                    _objLocationBL.DeleteLocation(ref objOperationResult, pstrLocationId, Globals.ClientSession.GetAsList());

                    //ACTUALIZAR GRILLA
                    pstrFilterExpression = string.Format("v_OrganizationId ==\"" + _organizationId + "\"");
                    var objDataLocation = _objLocationBL.GetLocationPagedAndFiltered(ref objOperationResult, 0, null, "V_Name ASC", pstrFilterExpression);
                    grdDataLocation.DataSource = objDataLocation;
                    lblLocationRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataLocation.Count());
                }

            }
        }

        #endregion

        #region Organization
        private void btnOK_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string pstrFilterEpression;

            if (uvOrganization.Validate(true, false).IsValid)
            {
                #region Validaciones

                if (txtIdentificationNumber.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para la Nro Identificación.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtName.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para la Razón Social.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtSector.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para Sector", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Está seguro de agregar / modificar la Empresa?.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }

                #endregion

                if (Mode == "New")
                {
                    objOrganizationDto = new organizationDto();
                    // Populate the entity
                    objOrganizationDto.i_OrganizationTypeId = int.Parse(ddlOrganizationypeId1.SelectedValue.ToString());
                    objOrganizationDto.i_SectorTypeId = int.Parse(ddlSectorTypeId.SelectedValue.ToString());
                    objOrganizationDto.v_IdentificationNumber = txtIdentificationNumber.Text.Trim();
                    objOrganizationDto.v_Name = txtName.Text.Trim();
                    objOrganizationDto.v_Address = txtAddress.Text.Trim();
                    objOrganizationDto.v_PhoneNumber = txtPhoneNumber.Text.Trim();
                    objOrganizationDto.v_ContacName = txtContacName1.Text.Trim();
                    objOrganizationDto.v_Observation = txtObservation.Text.Trim();
                    objOrganizationDto.v_Mail = txtEmail.Text.Trim();
                    objOrganizationDto.v_SectorCodigo = _SectorCodigo;
                    objOrganizationDto.v_SectorName = _SectorName;
                    objOrganizationDto.v_Contacto = txtContacto.Text;
                    objOrganizationDto.v_ContactoMedico = txtContactoeMedico.Text;
                    objOrganizationDto.v_EmailMedico = txtEmailMedico.Text;
                    objOrganizationDto.v_EmailContacto = txtEmailContacto.Text;
                    objOrganizationDto.i_NumberQuotasMen = rbConDx.Checked == true ? (int)EmpresaDx.ConDx : (int)EmpresaDx.SinDx;
                    if (txtFactor.Text != "")
                    {
                        objOrganizationDto.r_Factor = decimal.Parse(txtFactor.Text); 
                    }
                    if (txtFactorMed.Text != "")
                    {
                        objOrganizationDto.r_FactorMed = decimal.Parse(txtFactorMed.Text);
                    }
                    
                    

                    var arr = txtAddress.Text.Split('-').Reverse().ToArray();
                    var sede = arr[0].ToString();


                    if (pbEmpresaImage.Image != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        Bitmap bm = new Bitmap(pbEmpresaImage.Image);
                        bm.Save(ms, ImageFormat.Jpeg);
                        objOrganizationDto.b_Image = Common.Utils.ResizeUploadedImage(ms);
                        //pbEmpresaImage.Image.Dispose();
                    }
                    else
                    {
                        objOrganizationDto.b_Image = null;
                    }

                    //pstrFilterEpression = "i_IsDeleted==0 && v_IdentificationNumber==(\"" +  + "\")";
                    if (_objBL.OrganizacionExiste(txtIdentificationNumber.Text.Trim()))
                    {
                        MessageBox.Show("El Nro. Identificación ya existe:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        // Save the data
                        _organizationId = _objBL.AddOrganization(ref objOperationResult, objOrganizationDto, Globals.ClientSession.GetAsList());

                        //Setear Orden de empresa 
                        List<ordenreporteDto> ListaOrdem = new List<ordenreporteDto>();
                        ordenreporteDto oordenreporteDto = null;
                        var Lista = _oProtocolBL.GetOrdenReportes(ref objOperationResult, "N009-OO000000052");
                        foreach (var item in Lista)
                        {
                            oordenreporteDto = new ordenreporteDto();
                            oordenreporteDto.i_Orden = item.i_Orden;
                            oordenreporteDto.v_OrganizationId = _organizationId;
                            oordenreporteDto.v_NombreReporte = item.v_NombreReporte;
                            oordenreporteDto.v_ComponenteId = item.v_ComponenteId;
                            oordenreporteDto.v_NombreCrystal = item.v_NombreCrystal;
                            oordenreporteDto.i_NombreCrystalId = item.i_NombreCrystalId;
                            ListaOrdem.Add(oordenreporteDto);
                        }
                        _oProtocolBL.AddOrdenReportes(ref objOperationResult, ListaOrdem, Globals.ClientSession.GetAsList());

                        //Agregar Sede
                        objLocationDto = new locationDto();
                        // Populate the entity
                        objLocationDto.v_OrganizationId = _organizationId;
                        objLocationDto.v_Name = sede;
                        // Save the data
                        var locationId = _objLocationBL.AddLocation(ref objOperationResult, objLocationDto, Globals.ClientSession.GetAsList());
                        //Rutina para Asignar la Empresa creada automaticamente al nodo actual
                        NodeOrganizationLoactionWarehouseList objNodeOrganizationLoactionWarehouseList = new NodeOrganizationLoactionWarehouseList();

                        //Llenar Entidad Empresa/sede
                        objNodeOrganizationLoactionWarehouseList.i_NodeId = Globals.ClientSession.i_CurrentExecutionNodeId;
                        objNodeOrganizationLoactionWarehouseList.v_OrganizationId = _organizationId;
                        objNodeOrganizationLoactionWarehouseList.v_LocationId = locationId;

                        _objBL.AddNodeOrganizationLoactionWarehouse(ref objOperationResult, objNodeOrganizationLoactionWarehouseList, null, Globals.ClientSession.GetAsList());

                        //Crear GESO
                        objgroupoccupationDto = new groupoccupationDto();
                        // Populate the entity
                        objgroupoccupationDto.v_Name = "ADMINISTRATIVO";
                        objgroupoccupationDto.v_LocationId = locationId;
                        // Save the data
                        _objGroupOccupationBL.AddGroupOccupation(ref objOperationResult, objgroupoccupationDto, Globals.ClientSession.GetAsList());

                        objgroupoccupationDto = new groupoccupationDto();
                        // Populate the entity
                        objgroupoccupationDto.v_Name = "OPERARIO";
                        objgroupoccupationDto.v_LocationId = locationId;
                        // Save the data
                        _objGroupOccupationBL.AddGroupOccupation(ref objOperationResult, objgroupoccupationDto, Globals.ClientSession.GetAsList());
                        txtIdentificationNumber1.Text = objOrganizationDto.v_IdentificationNumber;

                        btnFilter_Click(sender, e);
                    }
                }
                else if (Mode == "Edit")
                {

                    // Populate the entity
                    objOrganizationDto.v_OrganizationId = _organizationId;
                    objOrganizationDto.i_OrganizationTypeId = int.Parse(ddlOrganizationypeId1.SelectedValue.ToString());
                    objOrganizationDto.i_SectorTypeId = int.Parse(ddlSectorTypeId.SelectedValue.ToString());
                    objOrganizationDto.v_IdentificationNumber = txtIdentificationNumber.Text.Trim();
                    objOrganizationDto.v_Name = txtName.Text.Trim();
                    objOrganizationDto.v_Address = txtAddress.Text.Trim();
                    objOrganizationDto.v_PhoneNumber = txtPhoneNumber.Text.Trim();
                    objOrganizationDto.v_ContacName = txtContacName1.Text.Trim();
                    objOrganizationDto.v_Observation = txtObservation.Text.Trim();
                    objOrganizationDto.v_Mail = txtEmail.Text.Trim();
                    objOrganizationDto.v_SectorCodigo = _SectorCodigo;
                    objOrganizationDto.v_SectorName = _SectorName;
                    objOrganizationDto.v_Contacto = txtContacto.Text;
                    objOrganizationDto.v_ContactoMedico = txtContactoeMedico.Text;
                    objOrganizationDto.v_EmailMedico = txtEmailMedico.Text;
                    objOrganizationDto.v_EmailContacto = txtEmailContacto.Text;
                    objOrganizationDto.i_NumberQuotasMen = rbConDx.Checked == true ? (int)EmpresaDx.ConDx : (int)EmpresaDx.SinDx;
                    if (txtFactor.Text != "")
                    {
                        objOrganizationDto.r_Factor = decimal.Parse(txtFactor.Text);
                    }
                    if (txtFactorMed.Text != "")
                    {
                        objOrganizationDto.r_FactorMed = decimal.Parse(txtFactorMed.Text);
                    }
                    objOrganizationDto.v_ComentaryUpdate = SetChanges();
                    if (pbEmpresaImage.Image != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        Bitmap bm = new Bitmap(pbEmpresaImage.Image);
                        bm.Save(ms, ImageFormat.Jpeg);
                        objOrganizationDto.b_Image = Common.Utils.ResizeUploadedImage(ms);
                        //pbEmpresaImage.Image.Dispose();
                    }
                    else
                    {
                        objOrganizationDto.b_Image = null;
                    }

                    if (Temp_IdentificationNumber == objOrganizationDto.v_IdentificationNumber)
                    {
                        // Save the data
                        _objBL.UpdateOrganization(ref objOperationResult, objOrganizationDto, Globals.ClientSession.GetAsList());

                        ////Eliminar Antiguos Registros
                        //_oProtocolBL.DeleteOrdenReportes(ref objOperationResult, _organizationId);

                        //List<ordenreporteDto> ListaOrdem = new List<ordenreporteDto>();
                        //ordenreporteDto oordenreporteDto = null;
                        //var Lista = _oProtocolBL.GetOrdenReportes(ref objOperationResult, "N009-OO000000052");
                        //foreach (var item in Lista)
                        //{
                        //    oordenreporteDto = new ordenreporteDto();
                        //    oordenreporteDto.i_Orden = item.i_Orden;
                        //    oordenreporteDto.v_OrganizationId = _organizationId;
                        //    oordenreporteDto.v_NombreReporte = item.v_NombreReporte;
                        //    oordenreporteDto.v_ComponenteId = item.v_NombreCrystal;
                        //    oordenreporteDto.v_NombreCrystal = item.v_NombreCrystal;
                        //    oordenreporteDto.i_NombreCrystalId = item.i_NombreCrystalId;
                        //    ListaOrdem.Add(oordenreporteDto);
                        //}
                        //_oProtocolBL.AddOrdenReportes(ref objOperationResult, ListaOrdem, Globals.ClientSession.GetAsList());
                    }
                    else
                    {
                        pstrFilterEpression = "i_IsDeleted==0 && v_IdentificationNumber==(\"" + txtIdentificationNumber.Text.Trim() + "\")";
                        if (_objBL.GetOrganizationsPagedAndFiltered(ref objOperationResult, null, null, null, pstrFilterEpression).Count() != 0)
                        {
                            MessageBox.Show("El Nro Identificación ya existe:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            // Save the data
                            _objBL.UpdateOrganization(ref objOperationResult, objOrganizationDto, Globals.ClientSession.GetAsList());

                            //Eliminar Antiguos Registros
                            //_oProtocolBL.DeleteOrdenReportes(ref objOperationResult, _organizationId);

                            //List<ordenreporteDto> ListaOrdem = new List<ordenreporteDto>();
                            //ordenreporteDto oordenreporteDto = null;
                            //var Lista = _oProtocolBL.GetOrdenReportes(ref objOperationResult, "N009-OO000000052");
                            //foreach (var item in Lista)
                            //{
                            //    oordenreporteDto = new ordenreporteDto();
                            //    oordenreporteDto.i_Orden = item.i_Orden;
                            //    oordenreporteDto.v_OrganizationId = _organizationId;
                            //    oordenreporteDto.v_NombreReporte = item.v_NombreReporte;
                            //    oordenreporteDto.v_ComponenteId = item.v_NombreCrystal;
                            //    oordenreporteDto.v_NombreCrystal = item.v_NombreCrystal;
                            //    oordenreporteDto.i_NombreCrystalId = item.i_NombreCrystalId;
                            //    ListaOrdem.Add(oordenreporteDto);
                            //}
                            //_oProtocolBL.AddOrdenReportes(ref objOperationResult, ListaOrdem, Globals.ClientSession.GetAsList());
                        }
                    }

                    //Verificar si el Email existe en la tabla Email
                    EmailBL oEmailBL = new EmailBL();
                    emailDto oemailDto;
                    oemailDto = oEmailBL.VerificarSiExisteEmail(ref objOperationResult, txtEmail.Text.Trim());
                    if (oemailDto == null)
                    {
                        oemailDto = new emailDto();
                        oemailDto.v_Email = txtEmail.Text.Trim();
                        oEmailBL.AddEmail(ref objOperationResult, oemailDto);
                    }

                    ActivarControles(false);
                    btnEditar.Enabled = true;
                    btnOK.Enabled = false;
                    btnNuevo.Enabled = true;
                    btnAgregarSector.Enabled = false;

                    SetOldValues();
                }



                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    tabControl1.Enabled = true;
                    Temp_IdentificationNumber = txtIdentificationNumber.Text;
                    objOrganizationDto = _objBL.GetOrganization(ref objOperationResult, _organizationId);
                    Mode = "Edit";
                    //this.DialogResult = System.Windows.Forms.DialogResult.OK;

                    //this.Close();
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



        #endregion

        #region GESO

        private void btnFilterGroupOccupation_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddlSearchGESOLocation.SelectedValue.ToString() != "-1") Filters.Add("v_LocationId==\"" + ddlSearchGESOLocation.SelectedValue + "\"");
            if (!string.IsNullOrEmpty(txtSearchGeso.Text)) Filters.Add("v_Name.Contains(\"" + txtSearchGeso.Text.Trim() + "\")");

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

            this.BindGridGroupOccupation();

            // Utilitario para obtener los tamaños de las columnas de la grilla
            //Clipboard.SetText(Utils.GetGridColumnsDetail(grdDataWarehouse));
        }

        private void btnInsertGeso_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            if (uvGESO.Validate(true, false).IsValid)
            {
                if (txtInsertGeso.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para la GESO.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("¿Está seguro de agregar / modificar la GESO?.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                if (ModeGESO == "New")
                {
                    objgroupoccupationDto = new groupoccupationDto();
                    // Populate the entity
                    objgroupoccupationDto.v_Name = txtInsertGeso.Text;
                    objgroupoccupationDto.v_LocationId = dllInsertGESOLocation.SelectedValue.ToString();
                    // Save the data
                    _objGroupOccupationBL.AddGroupOccupation(ref objOperationResult, objgroupoccupationDto, Globals.ClientSession.GetAsList());

                }
                else if (ModeGESO == "Edit")
                {

                    // Populate the entity
                    objgroupoccupationDto.v_Name = txtInsertGeso.Text;
                    objgroupoccupationDto.v_LocationId = dllInsertGESOLocation.SelectedValue.ToString();

                    // Save the data
                    _objGroupOccupationBL.UpdateGroupOccupation(ref objOperationResult, objgroupoccupationDto, Globals.ClientSession.GetAsList());

                }

                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    btnFilterGroupOccupation_Click(sender, e);
                    ModeGESO = "New";
                    FillCombo();
                }
                else  // Operación con error
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Se queda en el formulario.
                }

                //Limpiar controles
                txtInsertGeso.Text = "";
                dllInsertGESOLocation.SelectedValue = "-1";
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void mnuGridGroupOccupationEdit_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string strGroupOccupationId = grdDataGESO.Selected.Rows[0].Cells[0].Value.ToString();

            objgroupoccupationDto = _objGroupOccupationBL.GetGroupOccupation(ref objOperationResult, strGroupOccupationId);

            txtInsertGeso.Text = objgroupoccupationDto.v_Name;
            dllInsertGESOLocation.SelectedValue = objgroupoccupationDto.v_LocationId;
            ModeGESO = "Edit";
        }

        private void mnuGridGroupOccupationDelete_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada
            string pstrGroupOccupationId = grdDataGESO.Selected.Rows[0].Cells[0].Value.ToString();

            string pstrFilterExpression = "v_GroupOccupationId ==" + "\"" + pstrGroupOccupationId + "\"";

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            var _objDataGroupOccupation = _objGroupOccupationBL.GetGroupOccupationPagedAndFiltered(ref objOperationResult, 0, null, "", pstrFilterExpression, _organizationId);

            if (_objDataGroupOccupation.Count != 0)
            {
                MessageBox.Show("La sede esta relacionada con un Puesto", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                // Delete the item

                _objGroupOccupationBL.DeleteGroupOccupation(ref objOperationResult, pstrGroupOccupationId, Globals.ClientSession.GetAsList());

                btnFilterGroupOccupation_Click(sender, e);
            }
        }
        private void BindGridGroupOccupation()
        {
            var objData = GetDataGroupOccupation(0, null, "v_LocationName,v_Name ASC", strFilterExpression);

            grdDataGESO.DataSource = objData;
            lblGroupOccupationRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<GroupOccupationList> GetDataGroupOccupation(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objGroupOccupationBL.GetGroupOccupationPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, _organizationId);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        #endregion

        #region ÁREA
        private void btnInsertArea_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (uvArea.Validate(true, false).IsValid)
            {
                if (txtInsertArea.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para la Área.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("¿Está seguro de agregar / modificar la Área?.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                if (ModeArea == "New")
                {
                    objareaDto = new areaDto();
                    // Populate the entity
                    objareaDto.v_Name = txtInsertArea.Text;
                    objareaDto.v_LocationId = ddlInsertAreaLocation.SelectedValue.ToString();
                    // Save the data
                    _objAreaBL.AddArea(ref objOperationResult, objareaDto, Globals.ClientSession.GetAsList());

                }
                else if (ModeArea == "Edit")
                {

                    // Populate the entity
                    objareaDto.v_Name = txtInsertArea.Text;
                    objareaDto.v_LocationId = ddlInsertAreaLocation.SelectedValue.ToString();

                    // Save the data
                    _objAreaBL.UpdateArea(ref objOperationResult, objareaDto, Globals.ClientSession.GetAsList());

                }

                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    string pstrFilterExpression = string.Format("v_OrganizationId ==\"" + _organizationId + "\"");
                    var objDataArea = _objAreaBL.GetAreaPagedAndFiltered(ref objOperationResult, 0, null, "v_LocationName,V_Name ASC", "", _organizationId);
                    grdDataArea.DataSource = objDataArea;
                    lblAreaRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataArea.Count());

                    ModeArea = "New";
                    FillCombo();
                }
                else  // Operación con error
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Se queda en el formulario.
                }

                //Limpiar controles
                txtInsertArea.Text = "";
                ddlInsertAreaLocation.SelectedValue = "-1";
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void mnuGridAreaEdit_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string strAreaId = grdDataArea.Selected.Rows[0].Cells[0].Value.ToString();

            objareaDto = _objAreaBL.GetArea(ref objOperationResult, strAreaId);

            txtInsertArea.Text = objareaDto.v_Name;
            ddlInsertAreaLocation.SelectedValue = objareaDto.v_LocationId;
            ModeArea = "Edit";
        }

        private void mnuGridAreaDelete_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                // Delete the item
                string pstrAreaId = grdDataArea.Selected.Rows[0].Cells[0].Value.ToString();
                _objAreaBL.DeleteArea(ref objOperationResult, pstrAreaId, Globals.ClientSession.GetAsList());

                string pstrFilterExpression = string.Format("v_OrganizationId ==\"" + _organizationId + "\"");
                var objDataArea = _objAreaBL.GetAreaPagedAndFiltered(ref objOperationResult, 0, null, "V_Name ASC", "", _organizationId);
                grdDataArea.DataSource = objDataArea;
                lblAreaRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataArea.Count());
            }
        }

        #endregion

        #region GES
        private void btnFilterGES_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddlSearchGESArea.SelectedValue.ToString() != "-1") Filters.Add("v_AreaId==\"" + ddlSearchGESArea.SelectedValue + "\"");
            if (!string.IsNullOrEmpty(txtSearchGes.Text)) Filters.Add("v_Name.Contains(\"" + txtSearchGes.Text.Trim() + "\")");

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

            this.BindGridGES();

            // Utilitario para obtener los tamaños de las columnas de la grilla
            //Clipboard.SetText(Utils.GetGridColumnsDetail(grdDataWarehouse));
        }

        private void btnInsertGES_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (uvGES.Validate(true, false).IsValid)
            {
                if (txtInsertGES.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para la GES.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Está seguro de agregar / modificar la GES?.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                if (ModoGES == "New")
                {
                    // Populate the entity
                    objGesDto.v_Name = txtInsertGES.Text;
                    objGesDto.v_AreaId = dllInsertGESArea.SelectedValue.ToString();
                    // Save the data
                    _objGesBL.AddGES(ref objOperationResult, objGesDto, Globals.ClientSession.GetAsList());

                }
                else if (ModoGES == "Edit")
                {

                    // Populate the entity
                    objGesDto.v_Name = txtInsertGES.Text;
                    objGesDto.v_AreaId = dllInsertGESArea.SelectedValue.ToString();

                    // Save the data
                    _objGesBL.UpdateGES(ref objOperationResult, objGesDto, Globals.ClientSession.GetAsList());

                }

                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    btnFilterGES_Click(sender, e);
                    ModoGES = "New";
                    FillCombo();
                }
                else  // Operación con error
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Se queda en el formulario.
                }

                //Limpiar controles
                txtInsertGeso.Text = "";
                dllInsertGESOLocation.SelectedValue = "-1";

            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void mnuGridGESEdit_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string strGESId = grdDataGes.Selected.Rows[0].Cells[2].Value.ToString();

            objGesDto = _objGesBL.GetGES(ref objOperationResult, strGESId);

            txtInsertGES.Text = objGesDto.v_Name;
            dllInsertGESArea.SelectedValue = objGesDto.v_AreaId;
            ModoGES = "Edit";
        }

        private void mnuGridGESDelete_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                // Delete the item
                string strGESId = grdDataGes.Selected.Rows[0].Cells[2].Value.ToString();
                _objGesBL.DeleteGES(ref objOperationResult, strGESId, Globals.ClientSession.GetAsList());

                btnFilterGES_Click(sender, e);
            }
        }

        private void BindGridGES()
        {
            var objData = GetDataGES(0, null, "v_AreaName,v_Name ASC", strFilterExpression);

            grdDataGes.DataSource = objData;
            lblGesRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<GesList> GetDataGES(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objGesBL.GetGESPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, _organizationId);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }

        #endregion

        #region Occupation
        private void btnFilterOccupation_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (dllSearchOccupationGES.SelectedValue.ToString() != "-1") Filters.Add("v_GesId==\"" + dllSearchOccupationGES.SelectedValue + "\"");
            if (ddlSearchOccupationGESO.SelectedValue.ToString() != "-1") Filters.Add("v_GroupOccupationId==\"" + ddlSearchOccupationGESO.SelectedValue + "\"");
            if (!string.IsNullOrEmpty(txtSearchOccupation.Text)) Filters.Add("v_Name.Contains(\"" + txtSearchOccupation.Text.Trim() + "\")");

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

            this.BindGridOccupation();

            // Utilitario para obtener los tamaños de las columnas de la grilla
            //Clipboard.SetText(Utils.GetGridColumnsDetail(grdDataWarehouse));
        }

        private void btnInsertOccupation_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (uvOccupation.Validate(true, false).IsValid)
            {
                if (txtInsertOccupation.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para la Puesto.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("¿Está seguro de agregar / modificar la Puesto?.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                if (ModeOccupation == "New")
                {
                    objOccupationDto = new occupationDto();
                    // Populate the entity
                    objOccupationDto.v_Name = txtInsertOccupation.Text;
                    objOccupationDto.v_GesId = ddlInsertOccupationGES.SelectedValue.ToString();
                    objOccupationDto.v_GroupOccupationId = ddlInsertOccupationGESO.SelectedValue.ToString();
                    // Save the data
                    _objOccupationBL.AddOccupation(ref objOperationResult, objOccupationDto, Globals.ClientSession.GetAsList());

                }
                else if (ModeOccupation == "Edit")
                {
                    // Populate the entity
                    //objOccupationDto.v_OccupationId = _OccupationId;
                    objOccupationDto.v_Name = txtInsertOccupation.Text;
                    objOccupationDto.v_GesId = ddlInsertOccupationGES.SelectedValue.ToString();
                    objOccupationDto.v_GroupOccupationId = ddlInsertOccupationGESO.SelectedValue.ToString();

                    // Save the data
                    _objOccupationBL.UpdateOccupation(ref objOperationResult, objOccupationDto, Globals.ClientSession.GetAsList());

                }

                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    btnFilterOccupation_Click(sender, e);
                    ModeOccupation = "New";
                    FillCombo();
                }
                else  // Operación con error
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Se queda en el formulario.
                }

                //Limpiar controles
                txtInsertOccupation.Text = "";
                ddlInsertOccupationGES.SelectedValue = "-1";
                ddlInsertOccupationGESO.SelectedValue = "-1";
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void mnuGridOccupationEdit_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string strOccupationId = grdDataOccupation.Selected.Rows[0].Cells[4].Value.ToString();

            List<OccupationList> OccupationList = new List<OccupationList>();
            objOccupationDto = _objOccupationBL.GetOccupation(ref objOperationResult, strOccupationId);
            OccupationList = _objOccupationBL.GetOccupationList(ref objOperationResult, strOccupationId);

            ddlInsertOccupationArea.SelectedValue = OccupationList[0].v_AreaId;
            txtInsertOccupation.Text = OccupationList[0].v_Name;
            ddlInsertOccupationGESO.SelectedValue = OccupationList[0].v_GroupOccupationId;
            ddlInsertOccupationGES.SelectedValue = OccupationList[0].v_GesId;

            ModeOccupation = "Edit";
        }

        private void mnuGridOccupationDelete_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                // Delete the item
                string strOccupationId = grdDataOccupation.Selected.Rows[0].Cells[4].Value.ToString();
                _objOccupationBL.DeleteOccupation(ref objOperationResult, strOccupationId, Globals.ClientSession.GetAsList());

                btnFilterOccupation_Click(sender, e);
            }
        }

        private void BindGridOccupation()
        {
            var objData = GetDataOccupation(0, null, "v_GesName,v_GroupOccupationName,V_Name ASC", strFilterExpression);

            grdDataOccupation.DataSource = objData;
            lblOccupationRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<OccupationList> GetDataOccupation(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objOccupationBL.GetOccupationPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, _organizationId);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }


        #endregion

        #region Warehouse
        private void btnFilterWarehouse_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (dllSearchWarehouseLocation.SelectedValue.ToString() != "-1") Filters.Add("v_LocationId==\"" + dllSearchWarehouseLocation.SelectedValue + "\"");
            if (!string.IsNullOrEmpty(txtSearchWarehouse.Text)) Filters.Add("v_Name.Contains(\"" + txtSearchWarehouse.Text.Trim() + "\")");

            Filters.Add("i_IsDeleted==0 && v_OrganizationId ==\"" + _organizationId + "\" ");
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

            this.BindGridWarehouse();

            // Utilitario para obtener los tamaños de las columnas de la grilla
            //Clipboard.SetText(Utils.GetGridColumnsDetail(grdDataWarehouse));
        }

        private void btnInsertWarehouse_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (uvWarehouse.Validate(true, false).IsValid)
            {
                if (txtInsertWarehouse.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para la Almacén.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Está seguro de agregar / modificar la Almacén?.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                if (ModeAlmacen == "New")
                {
                    objWarehouseDto = new warehouseDto();
                    // Populate the entity
                    objWarehouseDto.v_OrganizationId = _organizationId;
                    objWarehouseDto.v_Name = txtInsertWarehouse.Text;
                    objWarehouseDto.v_LocationId = dllInsertWarehouseLocation.SelectedValue.ToString();
                    objWarehouseDto.i_CostCenterId = Convert.ToInt32(ddlInsertCC.SelectedValue.ToString());
                    // Save the data
                    _objWarehouseBL.AddWarehouse(ref objOperationResult, objWarehouseDto, Globals.ClientSession.GetAsList());

                }
                else if (ModeAlmacen == "Edit")
                {

                    // Populate the entity
                    objWarehouseDto.v_OrganizationId = _organizationId;
                    objWarehouseDto.v_Name = txtInsertWarehouse.Text;
                    objWarehouseDto.v_LocationId = dllInsertWarehouseLocation.SelectedValue.ToString();
                    objWarehouseDto.i_CostCenterId = Convert.ToInt32(ddlInsertCC.SelectedValue.ToString());
                    // Save the data
                    _objWarehouseBL.UpdateWarehouse(ref objOperationResult, objWarehouseDto, Globals.ClientSession.GetAsList());

                }

                //Rutina para Asignar la Empresa creada automaticamente al nodo actual
                NodeOrganizationLoactionWarehouseList objNodeOrganizationLoactionWarehouseList = new NodeOrganizationLoactionWarehouseList();
                List<nodeorganizationlocationwarehouseprofileDto> objnodeorganizationlocationwarehouseprofileDto = new List<nodeorganizationlocationwarehouseprofileDto>();

                //Llenar Entidad Empresa/sede
                objNodeOrganizationLoactionWarehouseList.i_NodeId = Globals.ClientSession.i_CurrentExecutionNodeId;
                objNodeOrganizationLoactionWarehouseList.v_OrganizationId = _organizationId;
                objNodeOrganizationLoactionWarehouseList.v_LocationId = dllInsertWarehouseLocation.SelectedValue.ToString();

                //Llenar Entidad Almacén
                var objInsertWarehouseList = InsertWarehouse(objNodeOrganizationLoactionWarehouseList.v_LocationId);


                _objBL.AddNodeOrganizationLoactionWarehouse(ref objOperationResult, objNodeOrganizationLoactionWarehouseList, objInsertWarehouseList, Globals.ClientSession.GetAsList());



                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    btnFilterWarehouse_Click(sender, e);
                    ModeAlmacen = "New";
                    FillCombo();
                }
                else  // Operación con error
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Se queda en el formulario.
                }

                //Limpiar controles
                txtInsertWarehouse.Text = "";
                dllInsertWarehouseLocation.SelectedValue = "-1";
                ddlInsertCC.SelectedValue = "-1";
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void mnuGridWarehouseEdit_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string strWarehouseId = grdDataWarehouse.Selected.Rows[0].Cells[2].Value.ToString();

            objWarehouseDto = _objWarehouseBL.GetWarehouse(ref objOperationResult, strWarehouseId);

            txtInsertWarehouse.Text = objWarehouseDto.v_Name;
            dllInsertWarehouseLocation.SelectedValue = objWarehouseDto.v_LocationId;
            ddlInsertCC.SelectedValue = objWarehouseDto.i_CostCenterId.ToString();
            ModeAlmacen = "Edit";
        }
        private void mnuGridWarehouseDelete_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada

            DialogResult Result = MessageBox.Show("¿Está seguro de eliminar este registro?:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Result == System.Windows.Forms.DialogResult.Yes)
            {
                // Delete the item
                string strWarehouseId = grdDataWarehouse.Selected.Rows[0].Cells[2].Value.ToString();
                _objWarehouseBL.DeleteWarehouse(ref objOperationResult, strWarehouseId, Globals.ClientSession.GetAsList());

                btnFilterWarehouse_Click(sender, e);
            }

        }

        private void BindGridWarehouse()
        {

            var objData = GetDataWarehouse(0, null, "v_LocationIdName,V_Name ASC", strFilterExpression);

            grdDataWarehouse.DataSource = objData;
            lblWarehouseRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<WarehouseList> GetDataWarehouse(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objWarehouseBL.GetWarehousePagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return _objData;
        }
        #endregion

        private void grdDataLocation_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right && btnOK.Enabled == true)
            //{
            //    Point point = new System.Drawing.Point(e.X, e.Y);
            //    Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

            //    if (uiElement == null || uiElement.Parent == null)
            //        return;

            //    Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

            //    if (row != null)
            //    {
            //        grdDataLocation.Rows[row.Index].Selected = true;
            //        contextMenuLocation.Items["mnuGridLocationEdit"].Enabled = true;
            //    }
            //    else
            //    {
            //        contextMenuLocation.Items["mnuGridLocationEdit"].Enabled = false;

            //    }
            //}
        }

        private void grdDataGESO_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right && btnOK.Enabled == true)
            //{
            //    Point point = new System.Drawing.Point(e.X, e.Y);
            //    Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

            //    if (uiElement == null || uiElement.Parent == null)
            //        return;

            //    Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

            //    if (row != null)
            //    {
            //        grdDataGESO.Rows[row.Index].Selected = true;
            //        contextMenuGESO.Items["mnuGridGroupOccupationEdit"].Enabled = true;
            //    }
            //    else
            //    {
            //        contextMenuGESO.Items["mnuGridGroupOccupationEdit"].Enabled = false;
            //    }
            //}
        }

        private void grdDataArea_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right && btnOK.Enabled == true)
            //{
            //    Point point = new System.Drawing.Point(e.X, e.Y);
            //    Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

            //    if (uiElement == null || uiElement.Parent == null)
            //        return;

            //    Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

            //    if (row != null)
            //    {
            //        grdDataArea.Rows[row.Index].Selected = true;
            //        contextMenuArea.Items["mnuGridAreaEdit"].Enabled = true;
            //    }
            //    else
            //    {
            //        contextMenuArea.Items["mnuGridAreaEdit"].Enabled = false;

            //    }
            //}
        }

        private void grdDataGes_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right && btnOK.Enabled == true)
            //{
            //    Point point = new System.Drawing.Point(e.X, e.Y);
            //    Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

            //    if (uiElement == null || uiElement.Parent == null)
            //        return;

            //    Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

            //    if (row != null)
            //    {
            //        grdDataGes.Rows[row.Index].Selected = true;
            //        contextMenuGES.Items["mnuGridGESEdit"].Enabled = true;
            //    }
            //    else
            //    {
            //        contextMenuGES.Items["mnuGridGESEdit"].Enabled = false;

            //    }
            //}
        }

        private void grdDataOccupation_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right && btnOK.Enabled == true)
            //{
            //    Point point = new System.Drawing.Point(e.X, e.Y);
            //    Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

            //    if (uiElement == null || uiElement.Parent == null)
            //        return;

            //    Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

            //    if (row != null)
            //    {
            //        grdDataOccupation.Rows[row.Index].Selected = true;
            //        contextMenuOccupation.Items["mnuGridOccupationEdit"].Enabled = true;
            //    }
            //    else
            //    {
            //        contextMenuOccupation.Items["mnuGridOccupationEdit"].Enabled = false;

            //    }
            //}
        }

        private void grdDataWarehouse_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right && btnOK.Enabled == true)
            //{
            //    Point point = new System.Drawing.Point(e.X, e.Y);
            //    Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

            //    if (uiElement == null || uiElement.Parent == null)
            //        return;

            //    Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

            //    if (row != null)
            //    {
            //        grdDataWarehouse.Rows[row.Index].Selected = true;
            //        contextMenuWarehouse.Items["mnuGridWarehouseEdit"].Enabled = true;
            //    }
            //    else
            //    {
            //        contextMenuWarehouse.Items["mnuGridWarehouseEdit"].Enabled = false;

            //    }
            //}
        }

        private void txtIdentificationNumber_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ddlInsertOccupationArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (ddlInsertOccupationArea.SelectedValue == null) return;

            Utils.LoadDropDownList(ddlInsertOccupationGES, "Value1", "Id", BLL.Utils.GetGES(ref objOperationResult, ddlInsertOccupationArea.SelectedValue.ToString()), DropDownListAction.Select);
        }

        private void dllSearchOccupationArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            if (dllSearchOccupationArea.SelectedValue == null) return;
            Utils.LoadDropDownList(dllSearchOccupationGES, "Value1", "Id", BLL.Utils.GetGES(ref objOperationResult, dllSearchOccupationArea.SelectedValue.ToString()), DropDownListAction.Select);

        }

        private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (Mode == "New")
            {

                e.Cancel = true;
            }
            else
            {

                e.Cancel = false;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ActivarControles(true);
            btnEditar.Enabled = false;
            btnAgregarSector.Enabled = true;
            btnOK.Enabled = true;
            btnNuevo.Enabled = false;
            
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            pbEmpresaImage.Image = null;
            txtCiiu.Text = String.Empty;
            tabControl1.SelectedIndex = 0;

            LoadData("0", "New");

            ActivarControles(true);
            btnEditar.Enabled = false;
            btnAgregarSector.Enabled = true;
            btnOK.Enabled = true;
            btnNuevo.Enabled = false;

            ddlOrganizationypeId1.SelectedIndex = 0;
            ddlSectorTypeId.SelectedIndex = 0;
            txtName.Text = "";
            txtIdentificationNumber.Text = "";
            txtContacName1.Text = "";
            txtObservation.Text = "";
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtSector.Text = "";
            ddlOrganizationypeId1.SelectedValue = "1";
            txtFactor.Text = "1";
            txtFactorMed.Text = "1";
           

        }


        private void button7_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnEditarSede_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            //string strLocationId = grdDataLocation.SelectedRows[0].Cells[0].Value.ToString();

            string strLocationId = grdDataLocation.Selected.Rows[0].Cells[0].Value.ToString();

            objLocationDto = _objLocationBL.GetLocation(ref objOperationResult, strLocationId);

            txtInsertLocation.Text = objLocationDto.v_Name;
            ModeLocation = "Edit";
        }

        private void btnEditarArea_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string strAreaId = grdDataArea.Selected.Rows[0].Cells[0].Value.ToString();

            objareaDto = _objAreaBL.GetArea(ref objOperationResult, strAreaId);

            txtInsertArea.Text = objareaDto.v_Name;
            ddlInsertAreaLocation.SelectedValue = objareaDto.v_LocationId;
            ModeArea = "Edit";
        }

        private void btnEditarGES_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string strGESId = grdDataGes.Selected.Rows[0].Cells[2].Value.ToString();

            objGesDto = _objGesBL.GetGES(ref objOperationResult, strGESId);

            txtInsertGES.Text = objGesDto.v_Name;
            dllInsertGESArea.SelectedValue = objGesDto.v_AreaId;
            ModoGES = "Edit";
        }

        private void btnEditarPuesto_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string strOccupationId = grdDataOccupation.Selected.Rows[0].Cells[4].Value.ToString();

            List<OccupationList> OccupationList = new List<OccupationList>();
            objOccupationDto = _objOccupationBL.GetOccupation(ref objOperationResult, strOccupationId);
            OccupationList = _objOccupationBL.GetOccupationList(ref objOperationResult, strOccupationId);

            ddlInsertOccupationArea.SelectedValue = OccupationList[0].v_AreaId;
            txtInsertOccupation.Text = OccupationList[0].v_Name;
            ddlInsertOccupationGESO.SelectedValue = OccupationList[0].v_GroupOccupationId;
            ddlInsertOccupationGES.SelectedValue = OccupationList[0].v_GesId;

            ModeOccupation = "Edit";
        }

        private void btnEditarAlmacen_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string strWarehouseId = grdDataWarehouse.Selected.Rows[0].Cells[2].Value.ToString();

            objWarehouseDto = _objWarehouseBL.GetWarehouse(ref objOperationResult, strWarehouseId);

            txtInsertWarehouse.Text = objWarehouseDto.v_Name;
            dllInsertWarehouseLocation.SelectedValue = objWarehouseDto.v_LocationId;
            ddlInsertCC.SelectedValue = objWarehouseDto.i_CostCenterId.ToString();
            ModeAlmacen = "Edit";
        }

        private void btnEditarGESO_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string strGroupOccupationId = grdDataGESO.Selected.Rows[0].Cells[0].Value.ToString();

            objgroupoccupationDto = _objGroupOccupationBL.GetGroupOccupation(ref objOperationResult, strGroupOccupationId);

            txtInsertGeso.Text = objgroupoccupationDto.v_Name;
            dllInsertGESOLocation.SelectedValue = objgroupoccupationDto.v_LocationId;
            ModeGESO = "Edit";
        }

        private void grdData_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (grdData.Selected.Rows.Count == 0)
                return;

            string strOrganizationId = grdData.Selected.Rows[0].Cells[0].Value.ToString();
            _SectorCodigo = grdData.Selected.Rows[0].Cells["v_SectorCodigo"].Value == null ? "" : grdData.Selected.Rows[0].Cells["v_SectorCodigo"].Value.ToString();
            _SectorName = grdData.Selected.Rows[0].Cells["v_SectorName"].Value == null ? "" : grdData.Selected.Rows[0].Cells["v_SectorName"].Value.ToString();
            LoadData(strOrganizationId, "Edit");
            ActivarControles(false);
            btnEditar.Enabled = true;
            //btnAgregarSector.Enabled = true;
            btnOK.Enabled = false;
            btnNuevo.Enabled = true;
            button2.Enabled = true;
            btnObtenerProtocolos.Enabled = true;
        }

        private void txtName1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void txtIdentificationNumber1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFilter_Click(null, null);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

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

                    System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(pbEmpresaImage.Image);

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

                    pbEmpresaImage.Height = (int)(k * Hi);
                    pbEmpresaImage.Width = (int)(k * Wi);
                }
            }
            else
            {
                return;
            }
        }


        private void LoadFile(string pfilePath)
        {
            Image img = pbEmpresaImage.Image;

            // Destruyo la posible imagen existente en el control
            //
            if (img != null)
            {
                img.Dispose();
            }

            using (FileStream fs = new FileStream(pfilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                Image original = Image.FromStream(fs);
                pbEmpresaImage.Image = original;

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            pbEmpresaImage.Image = null;
        }

        private void btnAgregarSector_Click(object sender, EventArgs e)
        {
            frmCodigoEmpresa frm = new frmCodigoEmpresa();
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.Cancel) return;

            _objCodigoEmpresaList = frm._objCodigoEmpresaList;

            if (_objCodigoEmpresaList.v_CIIUId == null)
            {
                //txtDiagnostic.Text = String.Empty;
            }
            else
            {
                txtSector.Text = _objCodigoEmpresaList.v_Name;
                txtCiiu.Text = _objCodigoEmpresaList.v_CIIUId;
                _SectorName = _objCodigoEmpresaList.v_Name;
                _SectorCodigo = _objCodigoEmpresaList.v_CIIUId;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmCodigoEmpresa frm = new frmCodigoEmpresa();
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.Cancel) return;

            _objCodigoEmpresaList = frm._objCodigoEmpresaList;

            if (_objCodigoEmpresaList.v_CIIUId == null)
            {
                //txtDiagnostic.Text = String.Empty;
            }
            else
            {
                txtCiiuBus.Text = _objCodigoEmpresaList.v_CIIUId;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = string.Empty;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdData, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnObtenerProtocolos_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            ProtocolBL oProtocolBL = new ProtocolBL();
            var Protocolos = oProtocolBL.GetProtocolosByEmpresaId(ref objOperationResult, _organizationId);
            var Cadena = "";

            foreach (var item in Protocolos)
            {
                Cadena += item.v_Name + "\t" + item.v_ProtocolId + "\n";
            }

            Clipboard.SetDataObject(Cadena, true);

            MessageBox.Show("La información se copió correctamente en el porta papeles ", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAsignarOrden_Click(object sender, EventArgs e)
        {
            List<string> ListaEmpresas = new List<string>();

            foreach (var item in grdData.Rows)
            {
                if ((bool)item.Cells["b_Seleccionar"].Value)
                {
                    string x = item.Cells["v_OrganizationId"].Value.ToString();
                    ListaEmpresas.Add(x);
                }
            }

            frmOrdenReportes frm = new frmOrdenReportes(ListaEmpresas, _empresaPlantillaId, _nombreEmpresaPlantillaId);
            frm.ShowDialog();

            BindGrid();


            _empresaPlantillaId = "";
            _nombreEmpresaPlantillaId = "";
        }

        private void grdData_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            int Contador = 0;
            if ((e.Cell.Column.Key == "b_Seleccionar"))
            {
                if ((e.Cell.Value.ToString() == "False"))
                {
                    e.Cell.Value = true;
                }
                else
                {
                    e.Cell.Value = false;
                }

                foreach (var item in grdData.Rows)
                {
                    if ((bool)item.Cells["b_Seleccionar"].Value)
                    {
                        Contador++;
                    }
                }
                if (Contador == 1)
                {
                    _empresaPlantillaId = grdData.Selected.Rows[0].Cells["v_OrganizationId"].Value.ToString();
                    _nombreEmpresaPlantillaId = grdData.Selected.Rows[0].Cells["v_Name"].Value.ToString();
                    btnAsignarOrden.Enabled = true;
                }
                else if (Contador == 0)
                {
                    _empresaPlantillaId = "";
                    _nombreEmpresaPlantillaId = "";
                    btnAsignarOrden.Enabled = false;
                }
            }
        }

        private void txtIdentificationNumber_KeyDown(object sender, KeyEventArgs e)
        {
            //using (new LoadingClass.PleaseWait(this.Location, "Consultando..."))
            //{                
           
            if (e.KeyCode == Keys.Enter && txtIdentificationNumber.TextLength >= 8)
            {
                if (_objBL.OrganizacionExiste(txtIdentificationNumber.Text.Trim()))
                {
                    MessageBox.Show(@"El Nro. Identificación ya existe", @"ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var f = new frmBuscarDatos(txtIdentificationNumber.Text);
                if (f.ConexionDisponible)
                {
                    if (f.EsContribuyente && !ScrapperReniecSunat.Utils.EsRucValido(txtIdentificationNumber.Text))
                    {
                        MessageBox.Show(@"RUC Inválido", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    f.ShowDialog();

                    switch (f.Estado)
                    {
                        case Estado.NoResul:
                            MessageBox.Show(@"No se encontró datos de el RUC");
                            break;

                        case Estado.Ok:
                            if (f.Datos != null)
                            {
                                if (f.EsContribuyente)
                                {
                                    var datos = (SunatResultDto)f.Datos;
                                    var actividad = datos.ActividadEconomica.Split(new[] { '-', '\n' });
                                    txtName.Text = datos.RazonSocial;
                                    txtAddress.Text = datos.DireccionFiscal;
                                    txtPhoneNumber.Text = datos.Telefonos;
                                    if (actividad.Length >= 2)
                                    {
                                        _SectorCodigo = txtCiiu.Text = actividad[0];
                                        _SectorName = txtSector.Text = actividad[1];
                                    }
                                }
                            }
                            break;
                    }
                }
                else
                    MessageBox.Show(@"No se pudo conectar a la página de la SUNAT", @"Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //};

        }

        private void frmEmpresa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
           var empresas =  _objBL.EmpresasSalus();

           foreach (var empresa in empresas)
           {
               objOrganizationDto = new organizationDto();
               // Populate the entity
               objOrganizationDto.i_OrganizationTypeId = empresa.i_OrganizationTypeId;
               objOrganizationDto.i_SectorTypeId = empresa.i_SectorTypeId;
               objOrganizationDto.v_IdentificationNumber = empresa.v_IdentificationNumber;
               objOrganizationDto.v_Name = empresa.v_Name;
               objOrganizationDto.v_Address = empresa.v_Address;
               objOrganizationDto.v_PhoneNumber = empresa.v_PhoneNumber;
               objOrganizationDto.v_ContacName = empresa.v_ContacName;
               objOrganizationDto.v_Observation = empresa.v_Observation;
               objOrganizationDto.v_Mail = empresa.v_Mail;

               _organizationId = _objBL.AddOrganization(ref objOperationResult, objOrganizationDto, Globals.ClientSession.GetAsList());

               foreach (var sede in empresa.Sedes)
               {
                   objLocationDto = new locationDto();
                   objLocationDto.v_OrganizationId = _organizationId;
                   objLocationDto.v_Name = sede.Sede;
                   // Save the data
                   var locationId = _objLocationBL.AddLocation(ref objOperationResult, objLocationDto, Globals.ClientSession.GetAsList());
                   NodeOrganizationLoactionWarehouseList objNodeOrganizationLoactionWarehouseList = new NodeOrganizationLoactionWarehouseList();

                   objNodeOrganizationLoactionWarehouseList.i_NodeId = Globals.ClientSession.i_CurrentExecutionNodeId;
                   objNodeOrganizationLoactionWarehouseList.v_OrganizationId = _organizationId;
                   objNodeOrganizationLoactionWarehouseList.v_LocationId = locationId;

                   _objBL.AddNodeOrganizationLoactionWarehouse(ref objOperationResult, objNodeOrganizationLoactionWarehouseList, null, Globals.ClientSession.GetAsList());

                   foreach (var geso in sede.Gesos)
                   {
                       objgroupoccupationDto = new groupoccupationDto();
                       objgroupoccupationDto.v_Name = geso.Geso;
                       objgroupoccupationDto.v_LocationId = locationId;
                       // Save the data
                       _objGroupOccupationBL.AddGroupOccupation(ref objOperationResult, objgroupoccupationDto, Globals.ClientSession.GetAsList());
                   }
               }
           }
        }

        private void ddlOrganizationypeId1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlOrganizationypeId1.SelectedValue != null)
            {
                if (ddlOrganizationypeId1.SelectedValue.ToString() == "4")
                {
                    lblFactor.Visible = true;
                    txtFactor.Visible = true;
                    lblFactorMed.Visible = true;
                    txtFactorMed.Visible = true;
                    if (Mode == "New" || Mode == "Edit")
                    {
                        txtFactor.Enabled = true;
                        txtFactorMed.Enabled = true;
                    }
                    
                }
                else
                {
                    lblFactor.Visible = false;
                    txtFactor.Visible = false;
                    lblFactorMed.Visible = false;
                    txtFactorMed.Visible = false;
                    txtFactor.Enabled = false;
                    txtFactorMed.Enabled = false;
                }
            }
            
            
        }

        private void verCambiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string commentary = new OrganizationBL().GetComentaryUpdateByOrganizationId(_organizationId);
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
