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
    public partial class frmEmpresaEdicion : Form
    {
        OrganizationBL _objBL = new OrganizationBL();
        organizationDto objOrganizationDto ;

        LocationBL _objLocationBL = new LocationBL();
        locationDto objLocationDto;

        WarehouseBL _objWarehouseBL = new WarehouseBL();
        warehouseDto objWarehouseDto ;

        GroupOccupationBL _objGroupOccupationBL = new GroupOccupationBL();
        groupoccupationDto objgroupoccupationDto;

        AreaBL _objAreaBL = new AreaBL();
        areaDto objareaDto;

        GesBL _objGesBL = new GesBL();
        gesDto objGesDto = new gesDto();

        OccupationBL _objOccupationBL = new OccupationBL();
        occupationDto objOccupationDto;

        string _OrganizationId, Mode;
        string _OccupationId;
        string Temp_IdentificationNumber = null;
        string strFilterExpression;
        string ModeAlmacen;
        string ModeLocation ;
        string ModeGESO;
        string ModeArea;
        string ModoGES;
        string ModeOccupation;

        public frmEmpresaEdicion(string pstrOrganizationId, string pstrMode)
        {
            InitializeComponent();
            //Desactivar Autogeneracion de Colummnas grillas

            this.Text = this.Text + " (" + pstrOrganizationId + ")";
            _OrganizationId = pstrOrganizationId;
            Mode = pstrMode;
            ModeAlmacen = "New";
            ModeLocation = "New";
            ModeGESO = "New";
            ModeArea = "New";
            ModoGES = "New";
            ModeOccupation = "New";

            if (Mode == "New") tabControl1.Enabled = false;

        }

        private void frmLogEdicion_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void FillCombo()
        {
            OperationResult objOperationResult = new OperationResult();

            //TAB WAREHOUSE - Location
            Utils.LoadDropDownList(dllSearchWarehouseLocation, "Value1", "Id", BLL.Utils.GetLocation(ref objOperationResult, _OrganizationId), DropDownListAction.All);
            Utils.LoadDropDownList(dllInsertWarehouseLocation, "Value1", "Id", BLL.Utils.GetLocation(ref objOperationResult, _OrganizationId), DropDownListAction.Select);

            // TAB GESO - Location
            Utils.LoadDropDownList(ddlSearchGESOLocation, "Value1", "Id", BLL.Utils.GetLocation(ref objOperationResult, _OrganizationId), DropDownListAction.All);
            Utils.LoadDropDownList(dllInsertGESOLocation, "Value1", "Id", BLL.Utils.GetLocation(ref objOperationResult, _OrganizationId), DropDownListAction.Select);
            
            //TAB AREA - Location
            Utils.LoadDropDownList(ddlInsertAreaLocation, "Value1", "Id", BLL.Utils.GetLocation(ref objOperationResult, _OrganizationId), DropDownListAction.Select);

            //TAB GES - AREA
            Utils.LoadDropDownList(ddlSearchGESArea, "Value1", "Id", BLL.Utils.GetArea(ref objOperationResult, _OrganizationId), DropDownListAction.All);
            Utils.LoadDropDownList(dllInsertGESArea, "Value1", "Id", BLL.Utils.GetArea(ref objOperationResult, _OrganizationId), DropDownListAction.Select);

            //TAB OCCUPATION - GES
            Utils.LoadDropDownList(dllSearchOccupationArea, "Value1", "Id", BLL.Utils.GetArea(ref objOperationResult, _OrganizationId), DropDownListAction.Select);
            Utils.LoadDropDownList(dllSearchOccupationGES, "Value1", "Id", BLL.Utils.GetGES(ref objOperationResult, "-1"), DropDownListAction.All);
            Utils.LoadDropDownList(ddlSearchOccupationGESO, "Value1", "Id", BLL.Utils.GetGESO(ref objOperationResult, _OrganizationId), DropDownListAction.All);

            Utils.LoadDropDownList(ddlInsertOccupationArea, "Value1", "Id", BLL.Utils.GetArea(ref objOperationResult, _OrganizationId), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlInsertOccupationGES, "Value1", "Id", BLL.Utils.GetGES(ref objOperationResult, "-1"), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlInsertOccupationGESO, "Value1", "Id", BLL.Utils.GetGESO(ref objOperationResult, _OrganizationId), DropDownListAction.Select);       
        }

        private void LoadData()
        {
            OperationResult objOperationResult = new OperationResult();
      
            //Organization
            Utils.LoadDropDownList(ddlOrganizationypeId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 103, null), DropDownListAction.Select);
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

                    objOrganizationDto = _objBL.GetOrganization(ref objOperationResult, _OrganizationId);

                    ddlOrganizationypeId.SelectedValue = objOrganizationDto.i_OrganizationTypeId.ToString();
                    ddlSectorTypeId.SelectedValue = objOrganizationDto.i_SectorTypeId.ToString();
                    txtName.Text = objOrganizationDto.v_Name;
                    txtIdentificationNumber.Text = objOrganizationDto.v_IdentificationNumber;
                    txtAddress.Text = objOrganizationDto.v_Address;
                    txtPhoneNumber.Text = objOrganizationDto.v_PhoneNumber;
                    txtContacName1.Text= objOrganizationDto.v_ContacName;
                    txtObservation.Text = objOrganizationDto.v_Observation;
                    txtEmail.Text = objOrganizationDto.v_Mail;

                    Temp_IdentificationNumber = objOrganizationDto.v_IdentificationNumber;
                }

            //Cargar Grillas
                string pstrFilterExpression = string.Format("v_OrganizationId ==\"" + _OrganizationId + "\"");

                var objDataLocation = _objLocationBL.GetLocationPagedAndFiltered(ref objOperationResult, 0, null, "V_Name ASC", pstrFilterExpression);
                grdDataLocation.DataSource = objDataLocation;
                lblLocationRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataLocation.Count());

                var objDataWarehouse = _objWarehouseBL.GetWarehousePagedAndFiltered(ref objOperationResult, 0, null, "v_LocationIdName,V_Name ASC", pstrFilterExpression);
                grdDataWarehouse.DataSource = objDataWarehouse;
                lblWarehouseRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataWarehouse.Count());

                var objGroupOccupation = _objGroupOccupationBL.GetGroupOccupationPagedAndFiltered(ref objOperationResult, 0, null, "v_LocationName,V_Name ASC", "", _OrganizationId);
                grdDataGESO.DataSource = objGroupOccupation;
                lblGroupOccupationRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataWarehouse.Count());

                var objDataArea = _objAreaBL.GetAreaPagedAndFiltered(ref objOperationResult, 0, null, "v_LocationName,V_Name ASC", "", _OrganizationId);
                grdDataArea.DataSource = objDataArea;
                lblAreaRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataArea.Count());

                var objDataGES = _objGesBL.GetGESPagedAndFiltered(ref objOperationResult, 0, null, "v_AreaName,V_Name ASC", "", _OrganizationId);
                grdDataGes.DataSource = objDataGES;
                lblGesRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataGES.Count());

                var objDataOccupation = _objOccupationBL.GetOccupationPagedAndFiltered(ref objOperationResult, 0, null, "v_GesName,v_GroupOccupationName,V_Name ASC", "", _OrganizationId);
                grdDataOccupation.DataSource = objDataOccupation;
                lblOccupationRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataGES.Count());
            
        }

        #region Organization
        private void btnOK_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string pstrFilterEpression;
            if (uvOrganization.Validate(true, false).IsValid)
            {
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
                if (MessageBox.Show("Está seguro de agregar / modificar la Empresa?.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                if (Mode == "New")
                {
                    objOrganizationDto = new organizationDto();
                    // Populate the entity
                    objOrganizationDto.i_OrganizationTypeId = int.Parse(ddlOrganizationypeId.SelectedValue.ToString());
                    objOrganizationDto.i_SectorTypeId = int.Parse(ddlSectorTypeId.SelectedValue.ToString());
                    objOrganizationDto.v_IdentificationNumber = txtIdentificationNumber.Text.Trim();
                    objOrganizationDto.v_Name = txtName.Text.Trim();
                    objOrganizationDto.v_Address = txtAddress.Text.Trim();
                    objOrganizationDto.v_PhoneNumber = txtPhoneNumber.Text.Trim();
                    objOrganizationDto.v_ContacName = txtContacName1.Text.Trim();
                    objOrganizationDto.v_Observation = txtObservation.Text.Trim();
                    objOrganizationDto.v_Mail = txtEmail.Text.Trim();

                    pstrFilterEpression = "i_IsDeleted==0 && v_IdentificationNumber==(\"" + txtIdentificationNumber.Text.Trim() + "\")";
                    if (_objBL.GetOrganizationsPagedAndFiltered(ref objOperationResult, null, null, null, pstrFilterEpression).Count() != 0)
                    {
                        MessageBox.Show("El Nro. Identificación ya existe:" + System.Environment.NewLine + objOperationResult.ExceptionMessage, "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        // Save the data
                     _OrganizationId=   _objBL.AddOrganization(ref objOperationResult, objOrganizationDto, Globals.ClientSession.GetAsList());
                    }
                }
                else if (Mode == "Edit")
                {
                    // Populate the entity
                    objOrganizationDto.v_OrganizationId = _OrganizationId;
                    objOrganizationDto.i_OrganizationTypeId = int.Parse(ddlOrganizationypeId.SelectedValue.ToString());
                    objOrganizationDto.i_SectorTypeId = int.Parse(ddlSectorTypeId.SelectedValue.ToString());
                    objOrganizationDto.v_IdentificationNumber = txtIdentificationNumber.Text.Trim();
                    objOrganizationDto.v_Name = txtName.Text.Trim();
                    objOrganizationDto.v_Address = txtAddress.Text.Trim();
                    objOrganizationDto.v_PhoneNumber = txtPhoneNumber.Text.Trim();
                    objOrganizationDto.v_ContacName = txtContacName1.Text.Trim();
                    objOrganizationDto.v_Observation = txtObservation.Text.Trim();
                    objOrganizationDto.v_Mail = txtEmail.Text.Trim();

                    if (Temp_IdentificationNumber == objOrganizationDto.v_IdentificationNumber)
                    {
                        // Save the data
                        _objBL.UpdateOrganization(ref objOperationResult, objOrganizationDto, Globals.ClientSession.GetAsList());
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
                        }
                    }
                }


                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    tabControl1.Enabled = true;
                    Temp_IdentificationNumber = txtIdentificationNumber.Text ;
                    objOrganizationDto = _objBL.GetOrganization(ref objOperationResult, _OrganizationId);
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

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion     

        #region Location
        private void btnInsertLocation_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            if (uvSede.Validate(true, false).IsValid)
            {
                if (txtInsertLocation.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un nombre apropiado para la Sede.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("¿Está seguro de agregar / modificar la sede?.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.No)
                {                    
                    return;
                }

                if (ModeLocation == "New")
                {
                    objLocationDto = new locationDto();

                    // Populate the entity
                    objLocationDto.v_OrganizationId = _OrganizationId;
                    objLocationDto.v_Name = txtInsertLocation.Text;
                    // Save the data
                    _objLocationBL.AddLocation(ref objOperationResult, objLocationDto, Globals.ClientSession.GetAsList());

                }
                else if (ModeLocation == "Edit")
                {

                    // Populate the entity
                    objLocationDto.v_OrganizationId = _OrganizationId;
                    objLocationDto.v_Name = txtInsertLocation.Text;

                    // Save the data
                    _objLocationBL.UpdateLocation(ref objOperationResult, objLocationDto, Globals.ClientSession.GetAsList());

                }

                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    //ACTUALIZAR GRILLA
                    string pstrFilterExpression = string.Format("v_OrganizationId ==\"" + _OrganizationId + "\"");
                    var objDataLocation = _objLocationBL.GetLocationPagedAndFiltered(ref objOperationResult, 0, null, "V_Name ASC", pstrFilterExpression);
                    grdDataLocation.DataSource = objDataLocation;
                    lblLocationRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataLocation.Count());

                    ModeLocation = "New";

                    FillCombo();
                }
                else  // Operación con error
                {
                    MessageBox.Show(Constants.GenericErrorMessage , "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void mnuGridLocationEdit_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            //string strLocationId = grdDataLocation.SelectedRows[0].Cells[0].Value.ToString();

            string strLocationId = grdDataLocation.Selected.Rows[0].Cells[0].Value.ToString();

            objLocationDto = _objLocationBL.GetLocation(ref objOperationResult, strLocationId);

            txtInsertLocation.Text = objLocationDto.v_Name;
            ModeLocation = "Edit";
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
                var _objDataGESO = _objGroupOccupationBL.GetGroupOccupationPagedAndFiltered(ref objOperationResult, 0, null, "", pstrFilterExpression, _OrganizationId);
                // Verificar si Sede está relacionada a un Área
                var objDataArea = _objAreaBL.GetAreaPagedAndFiltered(ref objOperationResult, 0, null, "", pstrFilterExpression, _OrganizationId);
                //Verificar si Almacén está relacionada con un Almacén
                var _objDataWarehouse = _objWarehouseBL.GetWarehousePagedAndFiltered(ref objOperationResult, 0, null, "", pstrFilterExpression);

                if (_objDataGESO.Count() != 0)
                {
                    MessageBox.Show("La sede esta relacionada con un GESO" , "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (objDataArea.Count() !=0)
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
                    pstrFilterExpression = string.Format("v_OrganizationId ==\"" + _OrganizationId + "\"");
                    var objDataLocation = _objLocationBL.GetLocationPagedAndFiltered(ref objOperationResult, 0, null, "V_Name ASC", pstrFilterExpression);
                    grdDataLocation.DataSource = objDataLocation;
                    lblLocationRecordCount.Text = string.Format("Se encontraron {0} registros.", objDataLocation.Count());
                }
                
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
                    objgroupoccupationDto.v_Name = txtInsertGeso.Text ;
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
           
            var _objDataGroupOccupation = _objGroupOccupationBL.GetGroupOccupationPagedAndFiltered(ref objOperationResult, 0, null, "", pstrFilterExpression, _OrganizationId);
            
            if (_objDataGroupOccupation.Count != 0)
            {
                MessageBox.Show("La sede esta relacionada con un Puesto", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(Result == System.Windows.Forms.DialogResult.Yes)
            {
                // Delete the item
                
                _objGroupOccupationBL.DeleteGroupOccupation(ref objOperationResult, pstrGroupOccupationId, Globals.ClientSession.GetAsList());

                btnFilterGroupOccupation_Click(sender, e);
            }
        }

        private void BindGridGroupOccupation()
        {
            var objData = GetDataGroupOccupation(0,null,"v_LocationName,v_Name ASC", strFilterExpression);

            grdDataGESO.DataSource = objData;
            lblGroupOccupationRecordCount.Text = string.Format("Se encontraron {0} registros.", objData.Count());
        }

        private List<GroupOccupationList> GetDataGroupOccupation(int pintPageIndex, int ?pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objGroupOccupationBL.GetGroupOccupationPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, _OrganizationId);

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
                    objareaDto.v_Name = txtInsertArea.Text ;
                    objareaDto.v_LocationId = ddlInsertAreaLocation.SelectedValue.ToString();

                    // Save the data
                    _objAreaBL.UpdateArea(ref objOperationResult, objareaDto, Globals.ClientSession.GetAsList());

                }

                //// Analizar el resultado de la operación
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    string pstrFilterExpression = string.Format("v_OrganizationId ==\"" + _OrganizationId + "\"");
                    var objDataArea = _objAreaBL.GetAreaPagedAndFiltered(ref objOperationResult, 0, null, "v_LocationName,V_Name ASC", "", _OrganizationId);
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

                string pstrFilterExpression = string.Format("v_OrganizationId ==\"" + _OrganizationId + "\"");
                var objDataArea = _objAreaBL.GetAreaPagedAndFiltered(ref objOperationResult, 0, null, "V_Name ASC", "", _OrganizationId);
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
                    objGesDto.v_Name = txtInsertGES.Text ;
                    objGesDto.v_AreaId = dllInsertGESArea.SelectedValue.ToString();
                    // Save the data
                    _objGesBL.AddGES(ref objOperationResult, objGesDto, Globals.ClientSession.GetAsList());

                }
                else if (ModoGES == "Edit")
                {

                    // Populate the entity
                    objGesDto.v_Name = txtInsertGES.Text ;
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

        private List<GesList> GetDataGES(int pintPageIndex, int ?pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objGesBL.GetGESPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, _OrganizationId);

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

        private List<OccupationList> GetDataOccupation(int pintPageIndex, int ?pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objOccupationBL.GetOccupationPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, _OrganizationId);

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
           
            Filters.Add("i_IsDeleted==0 && v_OrganizationId ==\"" + _OrganizationId + "\" ");
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
                    objWarehouseDto.v_OrganizationId = _OrganizationId;
                    objWarehouseDto.v_Name = txtInsertWarehouse.Text ;
                    objWarehouseDto.v_LocationId = dllInsertWarehouseLocation.SelectedValue.ToString();
                    objWarehouseDto.i_CostCenterId = Convert.ToInt32(ddlInsertCC.SelectedValue.ToString());
                    // Save the data
                    _objWarehouseBL.AddWarehouse(ref objOperationResult, objWarehouseDto, Globals.ClientSession.GetAsList());

                }
                else if (ModeAlmacen == "Edit")
                {

                    // Populate the entity
                    objWarehouseDto.v_OrganizationId = _OrganizationId;
                    objWarehouseDto.v_Name = txtInsertWarehouse.Text ;
                    objWarehouseDto.v_LocationId = dllInsertWarehouseLocation.SelectedValue.ToString();
                    objWarehouseDto.i_CostCenterId = Convert.ToInt32(ddlInsertCC.SelectedValue.ToString());
                    // Save the data
                    _objWarehouseBL.UpdateWarehouse(ref objOperationResult, objWarehouseDto, Globals.ClientSession.GetAsList());

                }

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

        private List<WarehouseList> GetDataWarehouse(int pintPageIndex, int ?pintPageSize, string pstrSortExpression, string pstrFilterExpression)
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
            if (e.Button == MouseButtons.Right)
            {
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    grdDataLocation.Rows[row.Index].Selected = true;
                    contextMenuLocation.Items["mnuGridLocationEdit"].Enabled = true;
                }
                else
                {
                    contextMenuLocation.Items["mnuGridLocationEdit"].Enabled = false;
                   
                }
            }
        }

        private void grdDataGESO_MouseDown(object sender, MouseEventArgs e)
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
                    grdDataGESO.Rows[row.Index].Selected = true;
                    contextMenuGESO.Items["mnuGridGroupOccupationEdit"].Enabled = true;
                }
                else
                {
                    contextMenuGESO.Items["mnuGridGroupOccupationEdit"].Enabled = false;
                }
            }
        }

        private void grdDataArea_MouseDown(object sender, MouseEventArgs e)
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
                    grdDataArea.Rows[row.Index].Selected = true;
                    contextMenuArea.Items["mnuGridAreaEdit"].Enabled = true;
                }
                else
                {
                    contextMenuArea.Items["mnuGridAreaEdit"].Enabled = false;

                }
            }
        }

        private void grdDataGes_MouseDown(object sender, MouseEventArgs e)
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
                    grdDataGes.Rows[row.Index].Selected = true;
                    contextMenuGES.Items["mnuGridGESEdit"].Enabled = true;
                }
                else
                {
                    contextMenuGES.Items["mnuGridGESEdit"].Enabled = false;

                }
            }
        }

        private void grdDataOccupation_MouseDown(object sender, MouseEventArgs e)
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
                    grdDataOccupation.Rows[row.Index].Selected = true;
                    contextMenuOccupation.Items["mnuGridOccupationEdit"].Enabled = true;
                }
                else
                {
                    contextMenuOccupation.Items["mnuGridOccupationEdit"].Enabled = false;

                }
            }
        }

        private void grdDataWarehouse_MouseDown(object sender, MouseEventArgs e)
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
                    grdDataWarehouse.Rows[row.Index].Selected = true;
                    contextMenuWarehouse.Items["mnuGridWarehouseEdit"].Enabled = true;
                }
                else
                {
                    contextMenuWarehouse.Items["mnuGridWarehouseEdit"].Enabled = false;

                }
            }
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

  

     
      
    }
}
