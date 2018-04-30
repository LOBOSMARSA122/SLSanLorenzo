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
using Infragistics.Win;
using ScrapperReniecSunat;

namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmSchedulePerson : Form
    {

        #region Declarations
            
        PacientList objpacientDto;
        personDto objpersonDto;
        string ModePacient;
        string PacientId;
        int ServiceTypeId;
        string ModeAgenda;
        string _CalendarId;
        string _ProtocolId;
        string _ServiceId;
        string NumberDocument;
        string _AuthorizedPersonId = null;
        DateTime? _EntryToMedicalCenter;
        string _occupation;

        #endregion

        public frmSchedulePerson(string strCalendarId, string pstrModeAgenda, string pstrProtocolId)
        {
            InitializeComponent();
            _CalendarId = strCalendarId;
            ModeAgenda = pstrModeAgenda;
            _ProtocolId = pstrProtocolId;
           
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


        private void frmSchedulePerson_Load(object sender, EventArgs e)
        {
            #region Mayusculas - Normal
            var _EsMayuscula = int.Parse(Common.Utils.GetApplicationConfigValue("EsMayuscula"));
            if (_EsMayuscula == 1)
            {
                SearchControlAndSetEvents(this);

            }


            #endregion

            OperationResult objOperationResult = new OperationResult();
            AuthorizedPersonBL objAuthorizedPersonBL = new AuthorizedPersonBL();           
            List<AuthorizedPersonList> objListaAuthorizedPerson = new List<AuthorizedPersonList>();

            
            //Llenado de combos

            Utils.LoadDropDownList(ddlRelationshipId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 207, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlAltitudeWorkId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 208, "i_ParameterId"), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlPlaceWorkId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 204, null), DropDownListAction.Select);
            

            Utils.LoadDropDownList(ddlMaritalStatusId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 101, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDocTypeId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 106, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSexTypeId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 100, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLevelOfId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 108, null), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlServiceTypeId, "Value1", "Id", BLL.Utils.GetServiceType(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetMasterService(ref objOperationResult, -1, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlVipId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlNewContinuationId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 121, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLineStatusId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 120, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlCalendarStatusId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 122, null), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlOrganization, "Value1", "Id", BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlBloodGroupId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 154, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlBloodFactorId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 155, null), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlDepartamentId, "Value1", "Id", BLL.Utils.GetDataHierarchyForComboDepartamento(ref objOperationResult, 113,null), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlProvinceId, "Value1", "Id", BLL.Utils.GetDataHierarchyForComboProvincia(ref objOperationResult, 113, null), DropDownListAction.Select);

            Utils.LoadDropDownList(ddlDistricId, "Value1", "Id", BLL.Utils.GetDataHierarchyForComboDistrito(ref objOperationResult, 113, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlResidenceInWorkplaceId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 111, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTypeOfInsuranceId, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 188, null), DropDownListAction.Select);
                   
            dtpDateTimeCalendar.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            dtpBirthdate.CustomFormat = "dd/MM/yyyy";

            btnSearchProtocol.Enabled = false;
            ddlOrganization.Enabled = false;

            //Cargar Grilla de Persona Autorizadas
            objListaAuthorizedPerson = objAuthorizedPersonBL.GetAuthorizedPersonPagedAndFilteredNOTNULL(ref objOperationResult, 0, null, "", "");
            grdDataPeopleAuthoritation.DataSource = objListaAuthorizedPerson;

       

            if (ModeAgenda == "New")
            {
                ModePacient = "New";
                rbNew.Checked = true;
                dtpBirthdate.Checked = false;
            }
            else if (ModeAgenda == "Edit")
            {
                btnFilter.Enabled = true;
                gbDatosGenerales.Enabled = true;
                gbService.Enabled = true;

                CalendarBL objCalendarBL = new CalendarBL();
                CalendarList objCalendarList = new CalendarList();
                objCalendarList = objCalendarBL.GetCalendarList(ref objOperationResult, _CalendarId);
                _ServiceId = objCalendarList.v_ServiceId;
                txtSearchNroDocument.Text = objCalendarList.v_NumberDocument;
                btnFilter_Click(sender, e);

              
                ddlServiceTypeId.SelectedValue = objCalendarList.i_ServiceTypeId.ToString();
                ddlMasterServiceId.SelectedValue = objCalendarList.i_MasterServiceId.ToString();
                dtpDateTimeCalendar.Value = objCalendarList.d_DateTimeCalendar.Value;
                ddlCalendarStatusId.SelectedValue = 1; // Agendado
                //ddlLineStatusId.SelectedValue =Common.LineStatus.FueraCircuito;
                ddlLineStatusId.SelectedValue = "2";
                ddlNewContinuationId.SelectedValue = "1";
                //ddlServiceTypeId.Enabled = true;
                //ddlMasterServiceId.Enabled = true;
                if (ddlMasterServiceId.SelectedValue.ToString() == "1")
                {
                    ProtocolBL _objProtocoltBL = new ProtocolBL();
                    protocolDto objProtocolDto = new protocolDto();
                    ProtocolList objProtocol = new ProtocolList();
                    objProtocol = _objProtocoltBL.GetProtocolById(ref objOperationResult, objCalendarList.v_ProtocolId);

                    txtProtocolId.Text = objProtocol.v_ProtocolId;
                    txtViewProtocol.Text = objProtocol.v_Protocol;
                    txtViewOrganization.Text = objProtocol.v_Organization;
                    //txtViewLocation.Text = objProtocol.v_Location;
                    txtViewGroupOccupation.Text = objProtocol.v_GroupOccupation;
                    txtViewGes.Text = objProtocol.v_Ges;
                    txtViewComponentType.Text = objProtocol.v_EsoType;
                    txtViewOccupation.Text = objProtocol.v_Occupation;
                    txtViewIntermediaryOrganization.Text = objProtocol.v_IntermediaryOrganization;
                    txtIntermediaryOrganization.Text = objProtocol.v_OrganizationInvoice;
                }
            }
            else if (ModeAgenda == "Reschedule")
            {
                btnFilter.Enabled = false;
                gbDatosGenerales.Enabled = false;
                gbService.Enabled = false;
                btnSearchProtocol.Enabled = false;

                CalendarBL objCalendarBL = new CalendarBL();
                CalendarList objCalendarList = new CalendarList();
                objCalendarList = objCalendarBL.GetCalendarList(ref objOperationResult, _CalendarId);
                _ServiceId = objCalendarList.v_ServiceId;
                txtSearchNroDocument.Text = objCalendarList.v_NumberDocument;
                btnFilter_Click(sender, e);

                ddlServiceTypeId.SelectedValue = objCalendarList.i_ServiceTypeId.ToString();
                ddlMasterServiceId.SelectedValue = objCalendarList.i_MasterServiceId.ToString();
                dtpDateTimeCalendar.Value = objCalendarList.d_DateTimeCalendar.Value;
                ddlCalendarStatusId.SelectedValue = "1";
                //ddlLineStatusId.SelectedValue =Common.LineStatus.FueraCircuito;
                ddlLineStatusId.SelectedValue = "2";
                ddlNewContinuationId.SelectedValue = "1";
                ddlServiceTypeId.Enabled = true;
                ddlMasterServiceId.Enabled = true;
                //if (ddlMasterServiceId.SelectedValue.ToString() == "2")
                //{
                    ProtocolBL _objProtocoltBL = new ProtocolBL();
                    protocolDto objProtocolDto = new protocolDto();
                    ProtocolList objProtocol = new ProtocolList();
                    objProtocol = _objProtocoltBL.GetProtocolById(ref objOperationResult, objCalendarList.v_ProtocolId);

                    txtProtocolId.Text = objProtocol.v_ProtocolId;
                    txtViewProtocol.Text = objProtocol.v_Protocol;
                    txtViewOrganization.Text = objProtocol.v_Organization;
                    //txtViewLocation.Text = objProtocol.v_Location;
                    txtViewGroupOccupation.Text = objProtocol.v_GroupOccupation;
                    txtViewGes.Text = objProtocol.v_Ges;
                    txtViewComponentType.Text = objProtocol.v_EsoType;
                    txtViewOccupation.Text = objProtocol.v_Occupation;
                    txtViewIntermediaryOrganization.Text = objProtocol.v_IntermediaryOrganization;
                    txtIntermediaryOrganization.Text = objProtocol.v_OrganizationInvoice;
                //}
            }

            ddlServiceTypeId.SelectedValue = ((int)ServiceType.Empresarial).ToString();
            ddlMasterServiceId.SelectedValue = ((int)MasterService.Eso).ToString();
            ddlVipId.SelectedValue = "0";
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            PacientBL _objPacientBL = new PacientBL();

            ServiceBL objServiceBL = new ServiceBL();
            objpacientDto = new PacientList();

            BlackListBL objBlackListBL = new BlackListBL();


            List<ServiceList>  objServiceList = new List<ServiceList>(); 
          
                if (txtSearchNroDocument.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese Nro. Documento.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
            
                objpacientDto = _objPacientBL.GetPacient(ref objOperationResult, null, txtSearchNroDocument.Text.Trim());
                if (objpacientDto != null)
                {
                    //Verificar si está en la tabla BlackListPerson
                    var Verificar = objBlackListBL.GetBlackList(ref objOperationResult, objpacientDto.v_PersonId);

                    if (Verificar !=null)
                    {
                        Verificar.d_DateDetection=DateTime.Now;
                        Verificar.i_Status = (int)StatusBlackListPerson.Detectado;
                        objBlackListBL.UpdateBlackList(ref objOperationResult,Verificar, Globals.ClientSession.GetAsList());
                        MensajeAlerta frm = new MensajeAlerta( Verificar.v_Comment,Verificar.d_DateRegister.Value);
                        frm.ShowDialog();
                        return;
                    }

                    PacientId = objpacientDto.v_PersonId;
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
                    ddlBloodFactorId.SelectedValue = objpacientDto.i_BloodFactorId.ToString();
                    ddlBloodGroupId.SelectedValue = objpacientDto.i_BloodGroupId.ToString();
                    txtNroPliza.Text = objpacientDto.v_NroPoliza;
                    txtDecucible.Text = objpacientDto.v_Deducible.ToString();

                    ddlDepartamentId.SelectedValue = objpacientDto.i_DepartmentId == null ? "-1" : objpacientDto.i_DepartmentId.ToString();
                    ddlProvinceId.SelectedValue = objpacientDto.i_ProvinceId == null ? "-1" : objpacientDto.i_ProvinceId.ToString();
                    ddlDistricId.SelectedValue = objpacientDto.i_DistrictId == null ? "-1" : objpacientDto.i_DistrictId.ToString();
                    ddlResidenceInWorkplaceId.SelectedValue = objpacientDto.i_ResidenceInWorkplaceId == null ? "-1" : objpacientDto.i_ResidenceInWorkplaceId.ToString();
                    ddlTypeOfInsuranceId.SelectedValue = objpacientDto.i_TypeOfInsuranceId == null ? "-1" : objpacientDto.i_TypeOfInsuranceId.ToString();

                    txtResidenceTimeInWorkplace.Text = objpacientDto.v_ResidenceTimeInWorkplace;                 
                    txtNumberLivingChildren.Text = objpacientDto.i_NumberLivingChildren.ToString();
                    txtNumberDependentChildren.Text = objpacientDto.i_NumberDependentChildren.ToString();
                    txtNroHermanos.Text = objpacientDto.i_NroHermanos.ToString();
                    
                    var lista = _objPacientBL.GetAllPuestos();
                    txtPuesto.DataSource = lista;
                    txtPuesto.DisplayMember = "Puesto";
                    txtPuesto.ValueMember = "Puesto";

                    txtPuesto.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                    txtPuesto.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
                    this.txtPuesto.DropDownWidth = 250;
                    txtPuesto.DisplayLayout.Bands[0].Columns[0].Width = 10;
                    txtPuesto.DisplayLayout.Bands[0].Columns[1].Width = 250;
                    if (!string.IsNullOrEmpty(objpacientDto.v_CurrentOccupation))
                    {
                        txtPuesto.Value = objpacientDto.v_CurrentOccupation;
                    }
                  //  txtPuesto.Text = objpacientDto.v_CurrentOccupation;
                    ddlRelationshipId.SelectedValue = objpacientDto.i_Relationship == 0 ? "-1" : objpacientDto.i_Relationship.ToString();
                    ddlAltitudeWorkId.SelectedValue = objpacientDto.i_AltitudeWorkId == 0 ? "-1" : objpacientDto.i_AltitudeWorkId.ToString();
                    ddlPlaceWorkId.SelectedValue = objpacientDto.i_PlaceWorkId == 0 ? "-1" : objpacientDto.i_PlaceWorkId.ToString();
                    txtExploitedMineral.Text = objpacientDto.v_ExploitedMineral;


                    ModePacient = "Edit";

                    objServiceList = objServiceBL.GetServicesPagedAndFiltered(ref objOperationResult, null, null, "", "v_PersonId=" + "\"" + PacientId + "\"", null, null, null, DateTime.Parse("01/01/2000"), DateTime.Parse("01/01/2050"));
                    //_ProtocolId = objServiceList[0].v_ProtocolId;
                    grdDataService.DataSource = objServiceList;

                    // para mostrar un formulario cuando tiene datos o existe
                    frmHistorialServicio frmServicios = new frmHistorialServicio(objServiceList);
                    frmServicios.ShowDialog();

                    if (objServiceList.Count != 0)
                    {
                        gbService.Enabled = true;
                    }
                    else
                    {
                        gbService.Enabled = false;
                    }
                    lblRecordCountService.Text = string.Format("Se encontraron {0} registros.", objServiceList.Count());
                    btnSavePacient.Text = "Actualizar Datos Paciente";

                   
                }
                else
                {
                    txtName.Text = "";
                    txtFirstLastName.Text = "";
                    txtSecondLastName.Text = "";
                    ddlDocTypeId.SelectedValue = "-1";
                    ddlSexTypeId.SelectedValue = "-1";
                    ddlMaritalStatusId.SelectedValue = "-1";
                    ddlRelationshipId.SelectedValue = "-1";
                    ddlAltitudeWorkId.SelectedValue = "-1";
                    ddlPlaceWorkId.SelectedValue = "-1";
                    txtExploitedMineral.Text = "";
                    ddlLevelOfId.SelectedValue = "-1";
                    txtDocNumber.Text = txtSearchNroDocument.Text;
                    dtpBirthdate.Value = DateTime.Now;
                    txtBirthPlace.Text = "";
                    txtTelephoneNumber.Text = "";
                    txtAdressLocation.Text = "";
                    txtMail.Text = "";
                    ModePacient = "New";
                    PacientId = null;
                    txtNroPliza.Text = "";
                    txtDecucible.Text = "";

                    ddlDepartamentId.SelectedValue = "-1";
                    ddlProvinceId.SelectedValue = "-1";
                    ddlDistricId.SelectedValue = "-1";
                    ddlResidenceInWorkplaceId.SelectedValue = "-1";
                    txtResidenceTimeInWorkplace.Text = "";
                    ddlTypeOfInsuranceId.SelectedValue ="";
                    txtNumberLivingChildren.Text = "";
                    txtNumberDependentChildren.Text = "";
                    txtNroHermanos.Text = "";
                    txtPuesto.Text = "";

                    //var DialogResult = MessageBox.Show("El paciente no se encuentra registrado en el sistema. Puede registrar al paciente en Datos Generales del Paciente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ObtenerDatosDNI(txtSearchNroDocument.Text.Trim());

                    grdDataService.DataSource = new List<ServiceList>();
                    rbNew.Checked = true;
                    rbNew_CheckedChanged(sender, e);
                    txtViewProtocol.Text = string.Empty;
                    txtViewOrganization.Text = string.Empty;
                    //txtViewLocation.Text = string.Empty;
                    txtViewGroupOccupation.Text = string.Empty;
                    txtViewGes.Text = string.Empty;
                    txtViewComponentType.Text = string.Empty;
                    txtViewOccupation.Text = string.Empty;
                    txtViewIntermediaryOrganization.Text = string.Empty;
                    ddlServiceTypeId.SelectedValue = ((int)ServiceType.Empresarial).ToString();
                    ddlMasterServiceId.SelectedValue = ((int)MasterService.Eso).ToString();
                    ddlVipId.SelectedValue = "0";
                    lblRecordCountService.Text = "No se ha realizado la búsqueda aún.";
                    btnSavePacient.Text = "Guardar Nuevo Paciente";
                    txtName.Focus();
            }
        }

        private void ObtenerDatosDNI(string dni)
        {
            var f = new frmBuscarDatos(dni);
            if (f.ConexionDisponible)
            {
                f.ShowDialog();

                switch (f.Estado)
                {
                    case Estado.NoResul:
                        MessageBox.Show("No se encontró datos de el DNI");
                        break;

                    case Estado.Ok:
                        if (f.Datos != null)
                        {
                            if (!f.EsContribuyente)
                            {
                                var datos = (ReniecResultDto)f.Datos;
                                txtName.Text = datos.Nombre;
                                txtFirstLastName.Text = datos.ApellidoPaterno;
                                txtSecondLastName.Text = datos.ApellidoMaterno;
                            }
                        }
                        break;
                }
            }
            else
                MessageBox.Show("No se pudo conectar la página", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSavePacient_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            PacientBL _objPacientBL = new PacientBL();
            AuthorizedPersonBL oAuthorizedPersonBL = new AuthorizedPersonBL();

            List<AuthorizedPersonList> objListaAuthorizedPerson = new List<AuthorizedPersonList>();
            objpacientDto = new PacientList();

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

                if (ModePacient == "New")
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
                    objpersonDto.b_PersonImage = null;
                    objpersonDto.v_NroPoliza = txtNroPliza.Text;
                    objpersonDto.v_Deducible = txtDecucible.Text ==""?0: Convert.ToDecimal(txtDecucible.Text);

                    objpersonDto.i_DepartmentId = Convert.ToInt32(ddlDepartamentId.SelectedValue);
                    objpersonDto.i_ProvinceId = Convert.ToInt32(ddlProvinceId.SelectedValue);
                    objpersonDto.i_DistrictId = Convert.ToInt32(ddlDistricId.SelectedValue);
                    objpersonDto.i_ResidenceInWorkplaceId = Convert.ToInt32(ddlResidenceInWorkplaceId.SelectedValue);
                    objpersonDto.v_ResidenceTimeInWorkplace = txtResidenceTimeInWorkplace.Text.Trim();
                    objpersonDto.i_TypeOfInsuranceId = Convert.ToInt32(ddlTypeOfInsuranceId.SelectedValue);
                    objpersonDto.i_NumberLivingChildren = txtNumberLivingChildren.Text == String.Empty ? (int?)null : int.Parse(txtNumberLivingChildren.Text);
                    objpersonDto.i_NumberDependentChildren = txtNumberDependentChildren.Text == String.Empty ? (int?)null : int.Parse(txtNumberDependentChildren.Text);
                    objpacientDto.i_NroHermanos = txtNroHermanos.Text == string.Empty ? (int?)null : int.Parse(txtNroHermanos.Text.ToString());
                    objpersonDto.v_CurrentOccupation = txtPuesto.Text;

                    objpersonDto.i_BloodGroupId = Convert.ToInt32(ddlBloodGroupId.SelectedValue);
                    objpersonDto.i_BloodFactorId = Convert.ToInt32(ddlBloodFactorId.SelectedValue);

                    objpersonDto.i_Relationship = Convert.ToInt32(ddlRelationshipId.SelectedValue);
                    objpersonDto.i_AltitudeWorkId = Convert.ToInt32(ddlAltitudeWorkId.SelectedValue);
                    objpersonDto.i_PlaceWorkId = Convert.ToInt32(ddlPlaceWorkId.SelectedValue);
                    objpersonDto.v_ExploitedMineral = txtExploitedMineral.Text;
                    objpersonDto.v_Password = txtDocNumber.Text;
                    
                    if (dtpBirthdate.Value > DateTime.Now.Date)
                    {
                        MessageBox.Show("La fecha de nacimiento no puede ser igual o mayor que la fecha actual","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        return;
                    }
                    // Save the data
                PacientId=  _objPacientBL.AddPacient(ref objOperationResult, objpersonDto, Globals.ClientSession.GetAsList());

                if (objOperationResult.Success != 1)  // Operación sin error
                {
                    MessageBox.Show(objOperationResult.ErrorMessage, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
               
                if (_AuthorizedPersonId != null)
                {
                    oAuthorizedPersonBL.DeleteAuthorizedPerson(ref objOperationResult, _AuthorizedPersonId);
                    _AuthorizedPersonId = null;

                    //Cargar Grilla de Persona Autorizadas
                    objListaAuthorizedPerson = oAuthorizedPersonBL.GetAuthorizedPersonPagedAndFilteredNOTNULL(ref objOperationResult, 0, null, "", "");
                    grdDataPeopleAuthoritation.DataSource = objListaAuthorizedPerson;
                }               

                }
                else if (ModePacient == "Edit")
                {
                    // Populate the entity
                    objpersonDto = new personDto();

                    objpersonDto= _objPacientBL.GetPerson(ref objOperationResult, PacientId);
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
                    objpersonDto.v_NroPoliza = txtNroPliza.Text;
                    objpersonDto.v_Deducible = txtDecucible.Text == ""?0: Convert.ToDecimal(txtDecucible.Text);

                    objpersonDto.i_DepartmentId = Convert.ToInt32(ddlDepartamentId.SelectedValue);
                    objpersonDto.i_ProvinceId = Convert.ToInt32(ddlProvinceId.SelectedValue);
                    objpersonDto.i_DistrictId = Convert.ToInt32(ddlDistricId.SelectedValue);
                    objpersonDto.i_ResidenceInWorkplaceId = Convert.ToInt32(ddlResidenceInWorkplaceId.SelectedValue);
                    objpersonDto.v_ResidenceTimeInWorkplace = txtResidenceTimeInWorkplace.Text.Trim();
                    objpersonDto.i_TypeOfInsuranceId = Convert.ToInt32(ddlTypeOfInsuranceId.SelectedValue);
                    objpersonDto.i_NumberLivingChildren = txtNumberLivingChildren.Text == string.Empty ? (int?)null : int.Parse(txtNumberLivingChildren.Text);
                    objpersonDto.i_NumberDependentChildren = txtNumberDependentChildren.Text == string.Empty ? (int?)null : int.Parse(txtNumberDependentChildren.Text);
                    objpersonDto.i_NroHermanos = txtNroHermanos.Text == string.Empty ? (int?)null : int.Parse(txtNroHermanos.Text.ToString());
                   
                    objpersonDto.v_CurrentOccupation = txtPuesto.Text;

                    objpersonDto.i_BloodGroupId = Convert.ToInt32(ddlBloodGroupId.SelectedValue);
                    objpersonDto.i_BloodFactorId = Convert.ToInt32(ddlBloodFactorId.SelectedValue);

                    objpersonDto.i_Relationship = Convert.ToInt32(ddlRelationshipId.SelectedValue);
                    objpersonDto.i_AltitudeWorkId = Convert.ToInt32(ddlAltitudeWorkId.SelectedValue);
                    objpersonDto.i_PlaceWorkId = Convert.ToInt32(ddlPlaceWorkId.SelectedValue);

                    objpersonDto.v_ExploitedMineral = txtExploitedMineral.Text;


                    if (dtpBirthdate.Value > DateTime.Now.Date)
                    {
                        MessageBox.Show("La fecha de nacimiento no puede ser igual o mayor que la fecha actual", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    // Save the data
                    PacientId = _objPacientBL.UpdatePacient(ref objOperationResult, objpersonDto, Globals.ClientSession.GetAsList(), NumberDocument, txtDocNumber.Text);
                   
                }
                if (objOperationResult.Success == 1)  // Operación sin error
                {
                    MessageBox.Show("Se grabó correctamente los datos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSearchNroDocument.Text = txtDocNumber.Text;
                    btnFilter_Click(sender, e);
                }
                else  // Operación con error
                {
                    if (PacientId == "-1")
                    {
                        MessageBox.Show("El Número de documento ya existe.", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnFilter_Click(sender, e);

                    }
                    else
                    {
                        MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Se queda en el formulario.
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnschedule_Click(object sender, EventArgs e)
        {
            try
            {
                OperationResult objOperationResult = new OperationResult();
                CalendarBL _objCalendarBL = new CalendarBL();
                calendarDto objCalendarDto = new calendarDto();

                componentDto objComponentDto = new componentDto();
                MedicalExamBL objComponentBL = new MedicalExamBL();

                ServiceBL _ObjServiceBL = new ServiceBL();
                serviceDto objServiceDto = new serviceDto();
                servicecomponentDto objServiceComponentDto = new servicecomponentDto();

                ProtocolBL _objProtocolBL = new ProtocolBL();
                List<ProtocolComponentList> objProtocolComponentList = new List<ProtocolComponentList>();

                string NuevoContinuacion;

                string CalendarId;

                if (dtpDateTimeCalendar.Value < DateTime.Now.Date)
                {
                    MessageBox.Show("No se permite agendar con una fecha anterior a la actual.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (uvschedule.Validate(true, false).IsValid)
                {
                    if (PacientId == null)
                    {
                        MessageBox.Show("Por favor seleccione un paciente.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (MessageBox.Show("¿Está seguro de realizar la cita?.", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }

                    if (ModeAgenda == "New")
                    {
                        using (new LoadingClass.PleaseWait(this.Location, "Grabando..."))
                        {
                            if (_EntryToMedicalCenter != null)
                            {
                                objCalendarDto.d_EntryTimeCM = _EntryToMedicalCenter;
                            }

                            objCalendarDto.v_PersonId = PacientId;
                            objCalendarDto.d_DateTimeCalendar = dtpDateTimeCalendar.Value.Date + dtpOnlyTime.Value.TimeOfDay;
                            objCalendarDto.i_ServiceTypeId = Int32.Parse(ddlServiceTypeId.SelectedValue.ToString());
                            objCalendarDto.i_CalendarStatusId = Int32.Parse(ddlCalendarStatusId.SelectedValue.ToString());
                            objCalendarDto.i_ServiceId = Int32.Parse(ddlMasterServiceId.SelectedValue.ToString());

                            //if (ddlMasterServiceId.SelectedValue.ToString() == ((int)MasterService.AtxMedicaParticular).ToString())
                            //{
                            //    objCalendarDto.v_ProtocolId = Constants.CONSULTAMEDICA;
                            //}
                            //else
                            //{
                                objCalendarDto.v_ProtocolId = _ProtocolId;
                            //}

                            objCalendarDto.i_NewContinuationId = Int32.Parse(ddlNewContinuationId.SelectedValue.ToString());
                            objCalendarDto.i_LineStatusId = Int32.Parse(ddlLineStatusId.SelectedValue.ToString());
                            objCalendarDto.i_IsVipId = Int32.Parse(ddlVipId.SelectedValue.ToString());
                            objCalendarDto.v_ServiceId = _ServiceId;

                            if (rbNew.Checked)
                            {
                                NuevoContinuacion = "Nuevo";
                            }
                            else
                            {
                                NuevoContinuacion = "Continuacion";
                            }

                            if (ddlMasterServiceId.SelectedValue.ToString() == ((int)MasterService.AtxMedicaParticular).ToString())
                            {
                                CalendarId = _objCalendarBL.AddShedule_Atx(ref objOperationResult, objCalendarDto, Globals.ClientSession.GetAsList(), Constants.CONSULTAMEDICA, PacientId, Int32.Parse(ddlMasterServiceId.SelectedValue.ToString()), NuevoContinuacion);
                            }
                            else
                            {
                                CalendarId = _objCalendarBL.AddShedule(ref objOperationResult, objCalendarDto, Globals.ClientSession.GetAsList(), _ProtocolId, PacientId, Int32.Parse(ddlMasterServiceId.SelectedValue.ToString()), NuevoContinuacion);
                            }

                        }
                    }
                    else if (ModeAgenda == "Edit")
                    {
                        using (new LoadingClass.PleaseWait(this.Location, "Grabando..."))
                        {
                            CalendarBL oCalendarBL = new CalendarBL();
                            objCalendarDto = oCalendarBL.GetCalendar(ref objOperationResult, _CalendarId);

                            objCalendarDto.v_PersonId = PacientId;
                            objCalendarDto.d_DateTimeCalendar = dtpDateTimeCalendar.Value;
                            objCalendarDto.i_ServiceTypeId = Int32.Parse(ddlServiceTypeId.SelectedValue.ToString());
                            objCalendarDto.i_CalendarStatusId = Int32.Parse(ddlCalendarStatusId.SelectedValue.ToString());
                            objCalendarDto.i_ServiceId = Int32.Parse(ddlMasterServiceId.SelectedValue.ToString());
                            objCalendarDto.v_CalendarId = _CalendarId;
                            objCalendarDto.v_ServiceId = _ServiceId;

                            if (ddlMasterServiceId.SelectedValue.ToString() == ((int)MasterService.AtxMedicaParticular).ToString())
                            {
                                objCalendarDto.v_ProtocolId = Constants.CONSULTAMEDICA;
                            }
                            else
                            {
                                objCalendarDto.v_ProtocolId = _ProtocolId;
                            }

                            objCalendarDto.i_NewContinuationId = Int32.Parse(ddlNewContinuationId.SelectedValue.ToString());
                            objCalendarDto.i_LineStatusId = Int32.Parse(ddlLineStatusId.SelectedValue.ToString());
                            objCalendarDto.i_IsVipId = Int32.Parse(ddlVipId.SelectedValue.ToString());

                            _objCalendarBL.UpdateCalendar(ref objOperationResult, objCalendarDto, Globals.ClientSession.GetAsList());
                        }

                    }
                    else if (ModeAgenda == "Reschedule")
                    {
                        using (new LoadingClass.PleaseWait(this.Location, "Grabando..."))
                        {
                            CalendarBL objCalendarBL = new CalendarBL();

                            ServiceBL objServiceBL = new ServiceBL();

                            if (ddlMasterServiceId.SelectedValue.ToString() == ((int)MasterService.AtxMedicaParticular).ToString())
                            {
                                objCalendarBL.Reschedule(ref objOperationResult, Globals.ClientSession.GetAsList(), _CalendarId, dtpDateTimeCalendar.Value, Int32.Parse(ddlVipId.SelectedValue.ToString()), Constants.CONSULTAMEDICA, PacientId, Int32.Parse(ddlMasterServiceId.SelectedValue.ToString()));
                            }
                            else
                            {
                                objCalendarBL.Reschedule(ref objOperationResult, Globals.ClientSession.GetAsList(), _CalendarId, dtpDateTimeCalendar.Value, Int32.Parse(ddlVipId.SelectedValue.ToString()), _ProtocolId, PacientId, Int32.Parse(ddlMasterServiceId.SelectedValue.ToString()));
                            }

                        }
                    }

                    //// Analizar el resultado de la operación
                    if (objOperationResult.Success == 1)  // Operación sin error
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                      
                    }
                    else  // Operación con error
                    {
                        if (objOperationResult.ErrorMessage != null)
                        {
                            MessageBox.Show(objOperationResult.ErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // Se queda en el formulario.
                        }
                    }
                }

            }
            catch (Exception ex)
            {               
               MessageBox.Show(Common.Utils.ExceptionFormatter(ex), "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void btnSearchProtocol_Click(object sender, EventArgs e)
        {
            Configuration.frmProtocolManagement frm = new Configuration.frmProtocolManagement("View", int.Parse(ddlServiceTypeId.SelectedValue.ToString()),int.Parse(ddlMasterServiceId.SelectedValue.ToString()));
            frm.ShowDialog();

            if (frm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;

            if (string.IsNullOrEmpty(frm._pstrProtocolId)) 
                 return;

             _ProtocolId = frm._pstrProtocolId;

            OperationResult objOperationResult = new OperationResult();
            ProtocolBL _objProtocoltBL = new ProtocolBL();
            protocolDto objProtocolDto = new protocolDto();
            ProtocolList objProtocol = new ProtocolList();

            objProtocol = _objProtocoltBL.GetProtocolById(ref objOperationResult, _ProtocolId);

            txtProtocolId.Text = objProtocol.v_ProtocolId;
            txtViewProtocol.Text = objProtocol.v_Protocol;
            txtViewOrganization.Text = objProtocol.v_Organization;
            //txtViewLocation.Text = objProtocol.v_Location;
            txtViewGroupOccupation.Text = objProtocol.v_GroupOccupation;
            txtViewGes.Text = objProtocol.v_Ges;
            txtViewComponentType.Text = objProtocol.v_EsoType;
            txtViewOccupation.Text = txtPuesto.Text;
            txtViewIntermediaryOrganization.Text = objProtocol.v_IntermediaryOrganization;
            txtIntermediaryOrganization.Text = objProtocol.v_OrganizationInvoice + " / " + objProtocol.v_Location;

        }

        private void rbContinuation_CheckedChanged(object sender, EventArgs e)
        {
            //ddlNewContinuationId.SelectedValue = ((int)modality.ContinuacionServicio).ToString(); //"2";
            //ddlLineStatusId.SelectedValue = ((int)LineStatus.FueraCircuito).ToString(); //"2";
            //ddlCalendarStatusId.SelectedValue =((int)CalendarStatus.Agendado).ToString(); //"1"; //Agendado;
            //gbService.Enabled = true;
            //ddlNewContinuationId.Enabled = false;
            //ddlServiceTypeId.Enabled = true;
            //ddlMasterServiceId.Enabled = true;
            //btnSearchProtocol.Enabled = false;

            ////ModeAgenda = "Edit";

            ddlNewContinuationId.SelectedValue = ((int)modality.ContinuacionServicio).ToString(); //"2";
            ddlLineStatusId.SelectedValue = ((int)LineStatus.FueraCircuito).ToString(); //"2";
            ddlCalendarStatusId.SelectedValue = ((int)CalendarStatus.Agendado).ToString(); //"1"; //Agendado;
            gbService.Enabled = true;
            ddlNewContinuationId.Enabled = false;
            ddlServiceTypeId.Enabled = true;
            ddlMasterServiceId.Enabled = true;
            btnSearchProtocol.Enabled = false;

            //ModeAgenda = "Edit";

        }

        private void rbNew_CheckedChanged(object sender, EventArgs e)
        {
            ddlNewContinuationId.SelectedValue = ((int)modality.NuevoServicio).ToString(); //"1";
            ddlLineStatusId.SelectedValue = ((int)LineStatus.FueraCircuito).ToString(); //"2";
            ddlCalendarStatusId.SelectedValue = ((int)CalendarStatus.Agendado).ToString(); //"1"; //Agendado;
            gbService.Enabled = false;
            ddlServiceTypeId.Enabled = true;
            ddlMasterServiceId.Enabled = true;
            ddlServiceTypeId.SelectedValue = "-1";
            ddlMasterServiceId.SelectedValue = "-1";
            //ModeAgenda = "New";
            _ProtocolId = null;

            txtViewProtocol.Text ="";
            txtViewOrganization.Text = "";
            //txtViewLocation.Text = "";
            txtViewGroupOccupation.Text = "";
            txtViewGes.Text = "";
            txtViewComponentType.Text = "";
            txtViewOccupation.Text = "";
            txtViewIntermediaryOrganization.Text = "";


        }
        
        private void txtSearchNroDocument_KeyPress(object sender, KeyPressEventArgs e)
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
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnFilter_Click(sender, e);
            }
        }

        private void ddlMasterServiceId_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((string)ddlMasterServiceId.SelectedValue == ((int)MasterService.Eso).ToString() || ddlServiceTypeId.SelectedValue.ToString() == ((int)ServiceType.Preventivo).ToString())
            {
                ddlOrganization.Enabled = false;

                if (ModeAgenda == "Reschedule")
                {
                    btnSearchProtocol.Enabled = false;
                }
                else
                {
                    if (rbContinuation.Checked == true)
                    {
                        btnSearchProtocol.Enabled = false;
                    }
                    else
                    {
                        btnSearchProtocol.Enabled = true;
                    }
                    
                }

            }
            else if ((string)ddlMasterServiceId.SelectedValue == null || (string)ddlMasterServiceId.SelectedValue == "-1")
            {
                
            }
            else
            {
                //btnSearchProtocol.Enabled = false;
                btnSearchProtocol.Enabled = true;
                ddlOrganization.Enabled = true;
                ddlOrganization.SelectedValue = "-1";
                txtProtocolId.Text = "";
                txtViewProtocol.Text = "";
                txtViewComponentType.Text = "";
                txtViewOccupation.Text = "";
                txtViewGes.Text = "";
                txtViewGroupOccupation.Text = "";
                txtViewOrganization.Text = "";
                txtIntermediaryOrganization.Text = "";
                txtViewIntermediaryOrganization.Text = "";

            }

            if (ddlMasterServiceId.SelectedValue == null) return;

            if (ddlMasterServiceId.SelectedValue.ToString() == ((int)MasterService.AtxMedicaParticular).ToString())
            {
                uvschedule.GetValidationSettings(txtViewProtocol).Condition = new OperatorCondition(ConditionOperator.NotEquals, "", false, typeof(string));
                uvschedule.GetValidationSettings(txtViewProtocol).IsRequired = false;
            }
            else
            {
                ddlOrganization.SelectedValue = "-1";
                uvschedule.GetValidationSettings(txtViewProtocol).Condition = new OperatorCondition(ConditionOperator.NotEquals, "--Seleccionar--", true, typeof(string));
                uvschedule.GetValidationSettings(txtViewProtocol).IsRequired = true;
            }
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

        private void ddlServiceTypeId_TextChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlMasterServiceId, "Value1", "Id", BLL.Utils.GetMasterService(ref objOperationResult, int.Parse(ddlServiceTypeId.SelectedValue.ToString()),Globals.ClientSession.i_CurrentExecutionNodeId), DropDownListAction.Select);
            
            if (ddlServiceTypeId.SelectedValue.ToString() == ((int)ServiceType.Empresarial).ToString() )
            {
                if (rbContinuation.Checked == true)
                {
                    btnSearchProtocol.Enabled = false;
                }                
            }
            else
            {
                //btnSearchProtocol.Enabled = false;
                btnSearchProtocol.Enabled = true;
                ddlOrganization.Enabled = false;
                txtProtocolId.Text = "";
                txtViewProtocol.Text = "";
                //txtViewLocation.Text = "";
                txtViewComponentType.Text = "";
                txtViewOccupation.Text = "";
                txtViewGes.Text = "";
                txtViewGroupOccupation.Text = "";
                txtViewOrganization.Text = "";
                txtIntermediaryOrganization.Text = "";
                txtViewIntermediaryOrganization.Text = "";
                ddlOrganization.SelectedValue = "-1";
            }           
           
        }

        private void grdDataService_MouseDown(object sender, MouseEventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            ProtocolBL _objProtocoltBL = new ProtocolBL();
            ProtocolList objProtocol = new ProtocolList();

            CalendarBL objCalendarBL = new CalendarBL();
            calendarDto objCalendarObj = new calendarDto();
            if (e.Button == MouseButtons.Left)
            {
                Point point = new System.Drawing.Point(e.X, e.Y);
                Infragistics.Win.UIElement uiElement = ((Infragistics.Win.UltraWinGrid.UltraGridBase)sender).DisplayLayout.UIElement.ElementFromPoint(point);

                if (uiElement == null || uiElement.Parent == null)
                    return;

                Infragistics.Win.UltraWinGrid.UltraGridRow row = (Infragistics.Win.UltraWinGrid.UltraGridRow)uiElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridRow));

                if (row != null)
                {
                    int i_MasterServiceId = int.Parse(grdDataService.Selected.Rows[0].Cells["i_MasterServiceId"].Value.ToString());

                    if (i_MasterServiceId == (int)Common.MasterService.Eso)
                    {
                        _ProtocolId = grdDataService.Selected.Rows[0].Cells["v_ProtocolId"].Value.ToString();
                        _CalendarId = grdDataService.Selected.Rows[0].Cells["v_CalendarId"].Value.ToString();
                        objProtocol = _objProtocoltBL.GetProtocolById(ref objOperationResult, _ProtocolId);
                        _ProtocolId = objProtocol.v_ProtocolId;
                        _ServiceId = grdDataService.Selected.Rows[0].Cells["v_ServiceId"].Value.ToString();  
                        txtProtocolId.Text = objProtocol.v_ProtocolId;
                        txtViewProtocol.Text = objProtocol.v_Protocol;
                        txtViewOrganization.Text = "";
                        txtViewOrganization.Text = objProtocol.v_Organization;
                        txtViewGroupOccupation.Text = objProtocol.v_GroupOccupation;
                        txtViewGes.Text = objProtocol.v_Ges;
                        txtViewComponentType.Text = objProtocol.v_EsoType;
                        txtViewOccupation.Text = objProtocol.v_Occupation;
                        txtViewIntermediaryOrganization.Text = objProtocol.v_IntermediaryOrganization;
                        ServiceTypeId = Convert.ToInt32(objProtocol.i_EsoTypeId);

                        objCalendarObj = objCalendarBL.GetCalendar(ref objOperationResult, _CalendarId);

                        ddlServiceTypeId.SelectedValue = objCalendarObj.i_ServiceTypeId.ToString();
                        ddlMasterServiceId.SelectedValue = objCalendarObj.i_ServiceId.ToString();
                    }
                    else
                    {
                        txtViewProtocol.Text = "Sin Protocolo";
                    }
                }
                else
                {
                }

            } 
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

        private void ddlDepartamentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (ddlDepartamentId.SelectedValue == null) return;
            if (ddlDepartamentId.SelectedValue.ToString() == "-1")
            {
                Utils.LoadDropDownList(ddlProvinceId, "Value1", "Id", BLL.Utils.GetDataHierarchyForComboProvincia(ref objOperationResult, 113, null), DropDownListAction.Select);
            }
            else
            {
                Utils.LoadDropDownList(ddlProvinceId, "Value1", "Id", BLL.Utils.GetDataHierarchyForComboProvincia(ref objOperationResult, 113, int.Parse(ddlDepartamentId.SelectedValue.ToString())), DropDownListAction.Select);
            }
        }

        private void ddlProvinceId_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (ddlProvinceId.SelectedValue == null) return;
            if (ddlDepartamentId.SelectedValue.ToString() == "-1")
            {
                Utils.LoadDropDownList(ddlDistricId, "Value1", "Id", BLL.Utils.GetDataHierarchyForComboDistrito(ref objOperationResult, 113, null), DropDownListAction.Select);
            }
            else
            {
                Utils.LoadDropDownList(ddlDistricId, "Value1", "Id", BLL.Utils.GetDataHierarchyForComboDistrito(ref objOperationResult, 113, int.Parse(ddlProvinceId.SelectedValue.ToString())), DropDownListAction.Select);
            }
        }

        private void ddlDistricId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void grdDataPeopleAuthoritation_DoubleClick(object sender, EventArgs e)
        {
            if (grdDataPeopleAuthoritation.Selected.Rows.Count == 0)
                return;

            AuthorizedPersonBL objAuthorizedPersonBL = new AuthorizedPersonBL();
            authorizedpersonDto objAuthorizedPersonDto = new authorizedpersonDto();
            OperationResult objOperationResult = new OperationResult();

                objAuthorizedPersonDto = objAuthorizedPersonBL.GetAuthorizedPerson(ref objOperationResult, _AuthorizedPersonId);

                string Pacient = objAuthorizedPersonDto.v_FirstName + " " + objAuthorizedPersonDto.v_FirstLastName + " " + objAuthorizedPersonDto.v_SecondLastName;

                DialogResult Result = MessageBox.Show("¿Desea registar al trabajador " + Pacient + " en el sistema?", "LISTA AUTORIZADOS!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (Result == System.Windows.Forms.DialogResult.Yes)
                {
                    ddlDocTypeId.SelectedValue = objAuthorizedPersonDto.i_DocTypeId.ToString();
                    txtName.Text = objAuthorizedPersonDto.v_FirstName;
                    txtFirstLastName.Text = objAuthorizedPersonDto.v_FirstLastName;
                    txtSecondLastName.Text = objAuthorizedPersonDto.v_SecondLastName;
                   
                    if (objAuthorizedPersonDto.d_BirthDate != null)
                    {
                        dtpBirthdate.Checked = true;
                        dtpBirthdate.Value = DateTime.Parse(objAuthorizedPersonDto.d_BirthDate.ToString());
                    }
                    txtDocNumber.Text = objAuthorizedPersonDto.v_DocNumber;
                    ddlSexTypeId.SelectedValue = objAuthorizedPersonDto.i_SexTypeId.ToString();
                    _AuthorizedPersonId = objAuthorizedPersonDto.v_AuthorizedPersonId;
                    _ProtocolId = objAuthorizedPersonDto.v_ProtocolId;
                    if (objAuthorizedPersonDto.d_EntryToMedicalCenter.ToString() != "")
                    {
                        _EntryToMedicalCenter = DateTime.Parse(objAuthorizedPersonDto.d_EntryToMedicalCenter.ToString());

                    }
                    //Llenar datos del Protocolo

                    ProtocolBL _objProtocoltBL = new ProtocolBL();
                    protocolDto objProtocolDto = new protocolDto();
                    ProtocolList objProtocol = new ProtocolList();

                    objProtocol = _objProtocoltBL.GetProtocolById(ref objOperationResult, _ProtocolId);

                    //Verificar si el nodo puede hacer este servicio .... esta pendiente
                    ddlServiceTypeId.SelectedValue = objProtocol.i_ServiceTypeId.ToString();
                    ddlMasterServiceId.SelectedValue = objProtocol.i_MasterServiceId.ToString();

                    txtProtocolId.Text = objProtocol.v_ProtocolId;
                    txtViewProtocol.Text = objProtocol.v_Protocol;
                    txtViewOrganization.Text = objProtocol.v_Organization;
                    txtViewGroupOccupation.Text = objProtocol.v_GroupOccupation;
                    txtViewGes.Text = objProtocol.v_Ges;
                    txtViewComponentType.Text = objProtocol.v_EsoType;
                    txtViewOccupation.Text = objProtocol.v_Occupation;
                    txtViewIntermediaryOrganization.Text = objProtocol.v_IntermediaryOrganization;
                    txtIntermediaryOrganization.Text = objProtocol.v_OrganizationInvoice + " / " + objProtocol.v_Location;
                }
                else
                {
                    _AuthorizedPersonId = null;
                }
        }

        private void grdDataPeopleAuthoritation_MouseDown(object sender, MouseEventArgs e)
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
                    grdDataPeopleAuthoritation.Rows[row.Index].Selected = true;
                    _AuthorizedPersonId = grdDataPeopleAuthoritation.Rows[row.Index].Cells["v_AuthorizedPersonId"].Value.ToString();

                    //if (grdDataPeopleAuthoritation.Rows[row.Index].Cells["d_EntryToMedicalCenter"].Value != null)
                    //{
                    //    //contextMenuStrip1.Items["mnuGridCancelar"].Enabled = true;
                    //    btnCancelPersonAuthoritation.Enabled = true;
                    //}
                    //else
                    //{
                    //    //contextMenuStrip1.Items["mnuGridCancelar"].Enabled = false;
                    //    btnCancelPersonAuthoritation.Enabled = false;
                    //}

                }
                else
                {
                    ////contextMenuStrip1.Items["mnuGridCancelar"].Enabled = false;
                    //btnCancelPersonAuthoritation.Enabled = false;
                }

            }
        }

        private void txtNameOrOrganization_TextChanged(object sender, EventArgs e)
        {
            AuthorizedPersonBL objAuthorizedPersonBL = new AuthorizedPersonBL();
            List<AuthorizedPersonList> _objListaAuthorizedPerson = new List<AuthorizedPersonList>();
            OperationResult objOperationResult = new OperationResult();

            string pstrFilterExpression = "(v_Pacient.Contains(\"" + txtNameOrOrganization.Text.Trim() + "\")" + ")" + "||" + "(v_OrganitationName.Contains(\"" + txtNameOrOrganization.Text.Trim() + "\")" + ")";
            _objListaAuthorizedPerson = objAuthorizedPersonBL.GetAuthorizedPersonPagedAndFilteredNOTNULL(ref objOperationResult, 0, null, "", pstrFilterExpression);
            grdDataPeopleAuthoritation.DataSource = _objListaAuthorizedPerson;

        }

        private void btnAntecedentes_Click(object sender, EventArgs e)
        {
            frmHistory frm = new frmHistory(PacientId);
            frm.ShowDialog();
        }

        private void txtSearchNroDocument_TextChanged(object sender, EventArgs e)
        {
            btnFilter.Enabled = (txtSearchNroDocument.TextLength > 0);
        }

        private void txtNroHermanos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtNumberLivingChildren_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtNumberDependentChildren_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

     
    
    }
}
