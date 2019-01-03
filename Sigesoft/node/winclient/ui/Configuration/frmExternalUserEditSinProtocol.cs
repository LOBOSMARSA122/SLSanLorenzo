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

namespace Sigesoft.Node.WinClient.UI.Configuration
{
    public partial class frmExternalUserEditSinProtocol : Form
    {
        #region Declarations

        PacientBL _objPacientBL = new PacientBL();
        string _personId;
        string _mode;
        string NumberDocument;
        personDto objPerson;
        systemuserDto _objSystemUserTemp = new systemuserDto();
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        DataHierarchyBL _objDataHierarchyBL = new DataHierarchyBL();
        private List<KeyValueDTO> _docType = new List<KeyValueDTO>();
        private int _lenght;
        SecurityBL _objSecurityBL = new SecurityBL();
        private int? _systemUserId;
        private ProtocolBL _protocolBL = new ProtocolBL();
        private List<protocolsystemuserDto> _tmpListProtocolSystemUser = null;
        private List<protocolsystemuserDto> _listProtocolSystemUserPermisoDelete = null;
        private List<protocolsystemuserDto> _listProtocolSystemUserPermisoUpdate = null;
        private List<protocolsystemuserDto> _listProtocolSystemUserNotifcacionDelete = null;
        private List<protocolsystemuserDto> _listProtocolSystemUserNotifcacionUpdate = null;
        private string _protocolId;
        private List<KeyValueDTO> _permissesUserExternal = null;
        private List<KeyValueDTO> _notificationUserExternal = null;
        private professionalDto _professionalDto = null;
        frmWaiting frmWaiting = new frmWaiting("Enviando Notificación");
        private List<ProtocoloCorto> ListaProtocolos = new List<ProtocoloCorto>();
      

        #endregion

        public frmExternalUserEditSinProtocol(string mode, string personId, int? systemUserId, string protocolId)
        {
            InitializeComponent();
            _personId = personId;
            _systemUserId = systemUserId;
            _mode = mode;
            _protocolId = protocolId;
        }

        public frmExternalUserEditSinProtocol()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            string Result = "";
            string personId;
            bool sendNotification = false;
            int systemUserId = -1;
            string SihayError = "";
            if (uvPacient.Validate(true, false).IsValid)
            {

                #region Validation

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

                if (txtMail.Text.Trim() == "")
                {
                    MessageBox.Show("Por favor ingrese un Email .", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (txtMail.Text.Trim() != "")
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
                if (int.Parse(ddlDocType.SelectedValue.ToString()) == (int)Common.Document.DNI)
                {
                    if (caracteres != 8)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (int.Parse(ddlDocType.SelectedValue.ToString()) == (int)Common.Document.PASAPORTE)
                {
                    if (caracteres != 9)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (int.Parse(ddlDocType.SelectedValue.ToString()) == (int)Common.Document.LICENCIACONDUCIR)
                {
                    if (caracteres != 9)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (int.Parse(ddlDocType.SelectedValue.ToString()) == (int)Common.Document.CARNETEXTRANJ)
                {
                    if (caracteres < 9)
                    {
                        MessageBox.Show("La cantida de caracteres de Número Documento es invalido.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (txtPassword2.Text.Trim() != txtPassword1.Text.Trim())
                {
                    MessageBox.Show("Los Password no coinciden Por favor verifique.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                #endregion

                if (_tmpListProtocolSystemUser == null)
                    _tmpListProtocolSystemUser = new List<protocolsystemuserDto>();                     

                if (_mode == "New")
                {

                    // Validar la longitud de los numeros de documentos
                    if (!IsValidDocumentNumberLenght())
                        return;

                    // Populate the entity
                    objPerson = new personDto();
                    objPerson.v_FirstName = txtName.Text.Trim();
                    objPerson.v_FirstLastName = txtFirstLastName.Text.Trim();
                    objPerson.v_SecondLastName = txtSecondLastName.Text.Trim();
                    objPerson.i_DocTypeId = Convert.ToInt32(ddlDocType.SelectedValue);
                    objPerson.i_SexTypeId = Convert.ToInt32(ddlSexType.SelectedValue);
                    objPerson.i_MaritalStatusId = Convert.ToInt32(ddlMaritalStatus.SelectedValue);
                    objPerson.i_LevelOfId = Convert.ToInt32(ddlLevelOfId.SelectedValue);
                    objPerson.v_DocNumber = txtDocNumber.Text.Trim();
                    objPerson.d_Birthdate = dtpBirthdate.Value;
                    objPerson.v_BirthPlace = txtBirthPlace.Text.Trim();
                    objPerson.v_TelephoneNumber = txtTelephoneNumber.Text.Trim();
                    objPerson.v_AdressLocation = txtAdressLocation.Text.Trim();
                    objPerson.v_Mail = txtMail.Text.Trim();

                  
                    // Datos de usuario
                    systemuserDto pobjSystemUser = new systemuserDto();
                    pobjSystemUser.v_UserName = txtUserName.Text.Trim();
                    pobjSystemUser.v_Password = SecurityBL.Encrypt(txtPassword2.Text.Trim());
                    pobjSystemUser.i_SystemUserTypeId = (int)SystemUserTypeId.External;

                    // Graba persona                        
                    systemUserId = _protocolBL.AddPersonUsuarioExterno(ref objOperationResult,
                                                              objPerson,
                                                              null,
                                                              pobjSystemUser,
                                                              Globals.ClientSession.GetAsList());

                    _systemUserId = systemUserId;

                    //Obtener Todos los protocolos de la Empresa             
                    var idEmpresa = cboEmpresa.SelectedValue.ToString().Split('|');
                    var ListaProtocolos = _protocolBL.DevolverProtocolosPorEmpresa(idEmpresa[0].ToString());


                    #region Eval CheckedList for create new ->  chklPermisosOpciones / chklNotificaciones

                    foreach (var item in ListaProtocolos)
                    {
                        _tmpListProtocolSystemUser = new List<protocolsystemuserDto>();
                        for (int i = 0; i < chklPermisosOpciones.CheckedItems.Count; i++)
                        {
                           
                            protocolsystemuserDto protocolSystemUser = new protocolsystemuserDto();
                            KeyValueDTO obj = (KeyValueDTO)chklPermisosOpciones.CheckedItems[i];
                            protocolSystemUser.v_ProtocolId = item.v_ProtocolId;
                            protocolSystemUser.i_ApplicationHierarchyId = int.Parse(obj.Id);
                            _tmpListProtocolSystemUser.Add(protocolSystemUser);
                        }

                        // Graba UsuarioExterno                        
                        SihayError = _protocolBL.AddSystemUserExternal_(ref objOperationResult, _tmpListProtocolSystemUser, Globals.ClientSession.GetAsList(), systemUserId);
                    }

                  

                    #endregion


                    if (SihayError == "-1")
                    {
                        if (objOperationResult.ErrorMessage != null)
                        {
                            //MessageBox.Show("Por favor ingrese un nombre apropiado para Nombres.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //return;
                            MessageBox.Show(objOperationResult.ErrorMessage, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                   
                    this.Enabled = false;
                    frmWaiting.Show(this);
                    bgwSendEmail.RunWorkerAsync();

                }
                else if (_mode == "Edit")
                {
                   
                    bool isChangeUserName = false;
                    bool isChangeDocNumber = false;

                    // Validar la longitud de los numeros de documentos
                    if (!IsValidDocumentNumberLenght()) return;

                    #region Validate SystemUSer
                    // Almacenar temporalmente el nombre de usuario actual                 
                    if (txtUserName.Text != _objSystemUserTemp.v_UserName)
                    {
                        isChangeUserName = true;
                        sendNotification = true;
                    }
                    #endregion

                    #region Validate Document Number
                    // Almacenar temporalmente el número de documento del usuario actual                
                    if (txtDocNumber.Text != objPerson.v_DocNumber)
                    {
                        isChangeDocNumber = true;
                    }
                    #endregion
              
                    // Almacenar temporalmente el password del usuario actual
                    var passTemp = _objSystemUserTemp.v_Password;

                    // Si el password actual es diferente al ingresado en la cajita de texto, quiere decir que se ha cambiado el password por lo tanto
                    // se bede encriptar el nuevo password
                    if (txtPassword2.Text != passTemp)
                    {
                        _objSystemUserTemp.v_Password = SecurityBL.Encrypt(txtPassword2.Text.Trim());
                        sendNotification = true;
                    }
                    else
                    {
                        _objSystemUserTemp.v_Password = txtPassword2.Text.Trim();
                    }

                    #region Datos de persona

                    // Datos de persona                
                    objPerson.v_PersonId = _personId;
                    objPerson.v_FirstName = txtName.Text.Trim();
                    objPerson.v_FirstLastName = txtFirstLastName.Text.Trim();
                    objPerson.v_SecondLastName = txtSecondLastName.Text.Trim();
                    objPerson.i_DocTypeId = Convert.ToInt32(ddlDocType.SelectedValue);
                    objPerson.i_SexTypeId = Convert.ToInt32(ddlSexType.SelectedValue);
                    objPerson.i_MaritalStatusId = Convert.ToInt32(ddlMaritalStatus.SelectedValue);
                    objPerson.i_LevelOfId = Convert.ToInt32(ddlLevelOfId.SelectedValue);
                    objPerson.v_DocNumber = txtDocNumber.Text.Trim();
                    objPerson.d_Birthdate = dtpBirthdate.Value;
                    objPerson.v_BirthPlace = txtBirthPlace.Text.Trim();
                    objPerson.v_TelephoneNumber = txtTelephoneNumber.Text.Trim();
                    objPerson.v_AdressLocation = txtAdressLocation.Text.Trim();
                    objPerson.v_Mail = txtMail.Text.Trim();

                    #endregion

                    #region Datos de Profesional 
                                      
                    // Datos de Profesional                 
                    if (ddlProfession.SelectedNode.Tag.ToString() != "-1" ||
                        !string.IsNullOrEmpty(txtProfessionalCode.Text) ||
                        !string.IsNullOrEmpty(txtProfessionalInformation.Text))
                    {
                        _professionalDto = new professionalDto();
                        _professionalDto.i_ProfessionId = Convert.ToInt32(ddlProfession.SelectedNode.Tag);
                        if (!string.IsNullOrEmpty(txtProfessionalCode.Text))
                            _professionalDto.v_ProfessionalCode = txtProfessionalCode.Text.Trim();
                        if (!string.IsNullOrEmpty(txtProfessionalInformation.Text))
                            _professionalDto.v_ProfessionalInformation = txtProfessionalInformation.Text.Trim();
                    }

                    #endregion

                    #region Datos de Usuario

                    // Datos de Usuario
                    _objSystemUserTemp.i_SystemUserId = _objSystemUserTemp.i_SystemUserId;
                    _objSystemUserTemp.v_PersonId = _personId;
                    _objSystemUserTemp.v_UserName = txtUserName.Text;
                    _objSystemUserTemp.d_InsertDate = _objSystemUserTemp.d_InsertDate;
                    _objSystemUserTemp.i_InsertUserId = _objSystemUserTemp.i_SystemUserId;
                    _objSystemUserTemp.i_IsDeleted = _objSystemUserTemp.i_IsDeleted;
                    if (rbFEchaExpiracion.Checked)
                        _objSystemUserTemp.d_ExpireDate = dtpExpiredDate.Value.Date;
                    else if (rbNuncaCaduca.Checked)
                        _objSystemUserTemp.d_ExpireDate = null;

                    #endregion

                    //
                    LoadCheckedListForUpdate();
                                                                     
                    // Actualiza persona                 
                    _protocolBL.UpdateSystemUserExternal(ref objOperationResult,
                                                    isChangeDocNumber,
                                                    objPerson,
                                                    _professionalDto,
                                                    isChangeUserName,
                                                    _objSystemUserTemp,
                                                    _listProtocolSystemUserPermisoUpdate,
                                                    _listProtocolSystemUserPermisoDelete,
                                                    _listProtocolSystemUserNotifcacionUpdate,
                                                    _listProtocolSystemUserNotifcacionDelete,
                                                   Globals.ClientSession.GetAsList());

                    if (sendNotification)
                    {
                        this.Enabled = false;
                        frmWaiting.Show(this);
                        bgwSendEmail.RunWorkerAsync();
                    }                        

                }

                if (objOperationResult.ErrorMessage != null)
                {
                    //MessageBox.Show("Por favor ingrese un nombre apropiado para Nombres.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //return;
                    MessageBox.Show(objOperationResult.ErrorMessage,"Error de validación",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (objOperationResult.Success != 1)
                    {
                        MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                        return;
                    }                  
                }

                if (!sendNotification && _mode != "New")
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void LoadCheckedListForUpdate()
        {
            for (int i = 0; i < chklPermisosOpciones.CheckedItems.Count; i++)
            {
                #region UPDATE chklPermisosOpciones

                KeyValueDTO obj = (KeyValueDTO) chklPermisosOpciones.CheckedItems[i];
              
                // Buscar Id en mi lista temp
                var findResult = _permissesUserExternal.Find(p => p.Id == obj.Id);
                // Si se encontro una coincidencia quiere decir que se ha removido el permiso
                if (findResult == null)
                {
                    if (_listProtocolSystemUserPermisoUpdate == null)
                        _listProtocolSystemUserPermisoUpdate = new List<protocolsystemuserDto>();

                    protocolsystemuserDto protocolSystemUser = new protocolsystemuserDto();
                    protocolSystemUser.i_SystemUserId = _systemUserId.Value;
                    protocolSystemUser.v_ProtocolId = _protocolId;
                    protocolSystemUser.i_ApplicationHierarchyId = int.Parse(obj.Id);
                    _listProtocolSystemUserPermisoUpdate.Add(protocolSystemUser);
                }
               

                #endregion

            }

            // DELETE chklPermisosOpciones
            for (int i = 0; i < chklPermisosOpciones.Items.Count; i++)
            {
                #region DELETE chklPermisosOpciones

                KeyValueDTO obj = (KeyValueDTO) chklPermisosOpciones.Items[i];

                // verificar si esta deschekado el item actual
                if (chklPermisosOpciones.GetItemCheckState(i) == CheckState.Unchecked)
                {
                    // Buscar Id en mi lista temp
                    var findResult = _permissesUserExternal.Find(p => p.Id == obj.Id);
                    // Si se encontro coincidencia quiere decir que se ha removido el permiso
                    if (findResult != null)
                    {
                        if (_listProtocolSystemUserPermisoDelete == null)
                            _listProtocolSystemUserPermisoDelete = new List<protocolsystemuserDto>();

                        protocolsystemuserDto protocolSystemUser = new protocolsystemuserDto();
                        protocolSystemUser.i_SystemUserId = _systemUserId.Value;
                        protocolSystemUser.v_ProtocolId = _protocolId;
                        protocolSystemUser.i_ApplicationHierarchyId = int.Parse(obj.Id);
                        _listProtocolSystemUserPermisoDelete.Add(protocolSystemUser);
                    }
                }

                #endregion

            }

            for (int i = 0; i < chklNotificaciones.CheckedItems.Count; i++)
            {
                #region UPDATE chklNotificaciones

                KeyValueDTO obj = (KeyValueDTO)chklNotificaciones.CheckedItems[i];
               
                // Buscar Id en mi lista temp
                var findResult = _notificationUserExternal.Find(p => p.Id == obj.Id);
                // Si se encontro una coincidencia quiere decir que se ha removido el permiso
                if (findResult == null)
                {
                    if (_listProtocolSystemUserNotifcacionUpdate == null)
                        _listProtocolSystemUserNotifcacionUpdate = new List<protocolsystemuserDto>();

                    protocolsystemuserDto protocolSystemUser = new protocolsystemuserDto();
                    protocolSystemUser.i_SystemUserId = _systemUserId.Value;
                    protocolSystemUser.v_ProtocolId = _protocolId;
                    protocolSystemUser.i_ApplicationHierarchyId = int.Parse(obj.Id);
                    _listProtocolSystemUserNotifcacionUpdate.Add(protocolSystemUser);
                }
               
                #endregion

            }

            // DELETE chklNotificaciones
            for (int i = 0; i < chklNotificaciones.Items.Count; i++)
            {
                #region DELETE chklNotificaciones

                KeyValueDTO obj = (KeyValueDTO)chklNotificaciones.Items[i];

                // verificar si esta deschekado el item actual
                if (chklNotificaciones.GetItemCheckState(i) == CheckState.Unchecked)
                {
                    // Buscar Id en mi lista temp
                    var findResult = _notificationUserExternal.Find(p => p.Id == obj.Id);
                    // Si se encontro coincidencia quiere decir que se ha removido el permiso
                    if (findResult != null)
                    {
                        if (_listProtocolSystemUserNotifcacionDelete == null)
                            _listProtocolSystemUserNotifcacionDelete = new List<protocolsystemuserDto>();

                        protocolsystemuserDto protocolSystemUser = new protocolsystemuserDto();
                        protocolSystemUser.i_SystemUserId = _systemUserId.Value;
                        protocolSystemUser.v_ProtocolId = _protocolId;
                        protocolSystemUser.i_ApplicationHierarchyId = int.Parse(obj.Id);
                        _listProtocolSystemUserNotifcacionDelete.Add(protocolSystemUser);
                    }
                }

                #endregion

            }

        }

        private void LoadComboBox()
        {
            OperationResult objOperationResult = new OperationResult();

            _docType = BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 106, null);

            //Llenado de combos
            Utils.LoadDropDownList(ddlMaritalStatus, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 101, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDocType, "Value1", "Id", _docType, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSexType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 100, null), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLevelOfId, "Value1", "Id", BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 108, null), DropDownListAction.Select);
            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);
            Utils.LoadDropDownList(cboEmpresa, "Value1", "Id", clientOrganization, DropDownListAction.Select);
            ////Llenado de combos
            Utils.LoadComboTreeBoxList(ddlProfession, BLL.Utils.GetDataHierarchyForComboTreeBox(ref objOperationResult, 101, null), DropDownListAction.Select);
            Utils.LoadDropDownList(cboProtocolo, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, "-1", "-1", null), DropDownListAction.Select);
          
        }

        private void LoadAllchkList()
        {
            OperationResult objOperationResult = new OperationResult();

            //for (int i = 0; i < chklPermisosOpciones.Items.Count - 1; i++)
            //{
            //    chklPermisosOpciones.SetItemChecked(i, false);
            //}

            chklPermisosOpciones.DataSource = _protocolBL.GetExternalPermisionForChekedListByTypeId(ref objOperationResult, (int)ExternalUserFunctionalityType.PermisosOpcionesUsuarioExternoWeb);
            chklPermisosOpciones.DisplayMember = "Value1";
            chklPermisosOpciones.ValueMember = "Id";

            for (int i = 0; i < chklNotificaciones.Items.Count - 1; i++)
            {
                chklNotificaciones.SetItemChecked(i, false);
            }

            chklNotificaciones.DataSource = _protocolBL.GetExternalPermisionForChekedListByTypeId(ref objOperationResult, (int)ExternalUserFunctionalityType.NotificacionesUsuarioExternoWeb);
            chklNotificaciones.DisplayMember = "Value1";
            chklNotificaciones.ValueMember = "Id";

        }

        private void LoadchkListByProtocolIdAndSystemUserId()
        {
            OperationResult objOperationResult = new OperationResult();

            // PERMISOS / OPCIONES USUARIO EXTERNO WEB
            _permissesUserExternal = _protocolBL.GetExternalPermisionByProtocolIdAndSystemUserId(ref objOperationResult, _protocolId, _systemUserId, (int)ExternalUserFunctionalityType.PermisosOpcionesUsuarioExternoWeb);

            if (_permissesUserExternal == null || _permissesUserExternal.Count == 0) return;

            foreach (var item in _permissesUserExternal)
	        {
		        for (int i = 0; i < chklPermisosOpciones.Items.Count; i++)
                {
                    KeyValueDTO obj = (KeyValueDTO) chklPermisosOpciones.Items[i];

                    if (obj.Id == item.Id)
                    {
                        chklPermisosOpciones.SetItemChecked(i, true);                      
                    }
                }
	        }
                    
            // NOTIFICACIONES USUARIO EXTERNO WEB
            _notificationUserExternal = _protocolBL.GetExternalPermisionByProtocolIdAndSystemUserId(ref objOperationResult, _protocolId, _systemUserId, (int)ExternalUserFunctionalityType.NotificacionesUsuarioExternoWeb);

            if (_notificationUserExternal == null || _notificationUserExternal.Count == 0) return;

            foreach (var item in _notificationUserExternal)
            {
                for (int i = 0; i < chklNotificaciones.Items.Count; i++)
                {
                    KeyValueDTO obj = (KeyValueDTO)chklNotificaciones.Items[i];

                    if (obj.Id == item.Id)
                    {
                        chklNotificaciones.SetItemChecked(i, true);
                    }
                }
            }

        }

        private bool IsValidDocumentNumberLenght()
        {
            if (txtDocNumber.Text.Trim().Length < _lenght || txtDocNumber.Text.Trim().Length > _lenght)
            {
                MessageBox.Show(String.Format("El número de Carateres requeridos es {0}", _lenght));
                return false;
            }
            return true;
        }

        private void SetLenght(string SelectedValue)
        {
            txtDocNumber.Text = string.Empty;

            if (SelectedValue == "-1")
            {
                txtDocNumber.Enabled = false;
                return;
            }

            txtDocNumber.Enabled = true;
            // Buscar la longitud adecuada en funcion al tipo de documento seleccionado
            var searchResult = _docType.Single(p => p.Id == SelectedValue);
            _lenght = Convert.ToInt32(searchResult.Value2);
            txtDocNumber.MaxLength = _lenght;
            txtDocNumber.Focus();
        }

        private void LoadData()
        {
            LoadComboBox();
            LoadAllchkList();

            if (_mode == "New")
            {
                // Additional logic here.
                this.Text = "Nuevo Usuario Externo";
                txtName.Select();
            }
            else if (_mode == "Edit")
            {
               
                // Setear lenght dimamicos de numero de documento
                SetLenght(ddlDocType.SelectedValue.ToString());

                OperationResult objCommonOperationResultedit = new OperationResult();
                objPerson = _objPacientBL.GetPerson(ref objCommonOperationResultedit, _personId);

                this.Text = this.Text + " (" + objPerson.v_FirstName + " " + objPerson.v_FirstLastName + " "+ objPerson.v_SecondLastName + ")";

                // Informacion de la persona
                txtName.Text = objPerson.v_FirstName;
                txtFirstLastName.Text = objPerson.v_FirstLastName;
                txtSecondLastName.Text = objPerson.v_SecondLastName;
                txtDocNumber.Text = objPerson.v_DocNumber;
                dtpBirthdate.Value = objPerson.d_Birthdate.Value;
                txtBirthPlace.Text = objPerson.v_BirthPlace;
                ddlMaritalStatus.SelectedValue = objPerson.i_MaritalStatusId.ToString();
                ddlLevelOfId.SelectedValue = objPerson.i_LevelOfId.ToString();
                ddlDocType.SelectedValue = objPerson.i_DocTypeId.ToString();
                txtDocNumber.Text = objPerson.v_DocNumber;
                ddlSexType.SelectedValue = objPerson.i_SexTypeId.ToString();
                txtTelephoneNumber.Text = objPerson.v_TelephoneNumber;
                txtAdressLocation.Text = objPerson.v_AdressLocation;
                txtMail.Text = objPerson.v_Mail;

                // Informacion de Profesional
                OperationResult objCommonOperationResult1 = new OperationResult();
                var objProfessional = _objPacientBL.GetProfessional(ref objCommonOperationResult1, _personId);

                if (objProfessional != null)
                {
                    ComboTreeNode nodoABuscar = ddlProfession.AllNodes.First(x => x.Tag.ToString() == objProfessional.i_ProfessionId.ToString());
                    ddlProfession.SelectedNode = nodoABuscar;                 
                    txtProfessionalCode.Text = objProfessional.v_ProfessionalCode;
                    txtProfessionalInformation.Text = objProfessional.v_ProfessionalInformation;
                  
                }

                // Informacion del usuario
                OperationResult objOperationResult = new OperationResult();
                _objSystemUserTemp = _objSecurityBL.GetSystemUser(ref objOperationResult, _systemUserId.Value);

                txtUserName.Text = _objSystemUserTemp.v_UserName;
                txtPassword1.Text = _objSystemUserTemp.v_Password;
                txtPassword2.Text = _objSystemUserTemp.v_Password;

                if (_objSystemUserTemp.d_ExpireDate != null)
                {
                    rbFEchaExpiracion.Checked = true;
                    dtpExpiredDate.Value = _objSystemUserTemp.d_ExpireDate.Value;
                }
                else
                {
                    rbNuncaCaduca.Checked = true;
                    dtpExpiredDate.Enabled = false;
                }

                LoadchkListByProtocolIdAndSystemUserId();

            }
        }

        private void frmExternalUserEditSinProtocol_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetLenght(ddlDocType.SelectedValue.ToString());
        }

        private void txtDocNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (int.Parse(ddlDocType.SelectedValue.ToString()) == 1)
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

        private void txtPassword2_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword2.Text != txtPassword1.Text)
            {
                txtPassword2.BackColor = Color.Pink;
            }
            else
            {
                txtPassword2.BackColor = Color.White;
            }
        }

        private void bgwSendEmail_DoWork(object sender, DoWorkEventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            try
            {
                // Obtener los Parametros necesarios para el envio de notificación
                var configEmail = BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 161, "i_ParameterId");

                string smtp = configEmail[0].Value1.ToLower();
                int port = int.Parse(configEmail[1].Value1);
                string from = configEmail[2].Value1.ToLower();
                string fromPassword = configEmail[4].Value1;
                string subject = configEmail[6].Value1;
                bool enableSsl = Convert.ToBoolean(int.Parse(configEmail[3].Value1));
                string personName = string.Format("{0} {1} {2}", txtName.Text.Trim(), txtFirstLastName.Text.Trim(), txtSecondLastName.Text.Trim());
                string message = string.Format(configEmail[5].Value1, personName, txtUserName.Text, txtPassword2.Text);
                e.Result = true;
                // Enviar notificación de usuario y clave via email
                Common.Utils.SendMessage(smtp, port, enableSsl, true, from, fromPassword, txtMail.Text.Trim(), "", subject, message, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Verifique su conexion de internet y/o cable de red,\n es posible que este desconectado.", "Error al enviar notificación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Result = false;
                CloseErrorfrmWaiting();
            }
                                                   
        }

        private void CloseErrorfrmWaiting()
        {
            if (frmWaiting.InvokeRequired)
            {
                this.Invoke(new Action(CloseErrorfrmWaiting));
            }
            else
            {
                this.Enabled = true;
                frmWaiting.Visible = false;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void bgwSendEmail_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void bgwSendEmail_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {         
            //frmWaiting.lblMessageText.Text = "Su correo ha sido enviado \n correctamente.";
            //frmWaiting.lblMessageText.Refresh();
            //System.Threading.Thread.Sleep(3000);
            this.Enabled = true;
            frmWaiting.Visible = false;

            if ((bool)e.Result == true)
            {
                MessageBox.Show("Su correo ha sido enviado correctamente.", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void rbNuncaCaduca_CheckedChanged(object sender, EventArgs e)
        {
            dtpExpiredDate.Enabled = false;
        }

        private void rbFEchaExpiracion_CheckedChanged(object sender, EventArgs e)
        {
            dtpExpiredDate.Enabled = true;
        }

        private void dtpExpiredDate_Validating(object sender, CancelEventArgs e)
        {
            if (dtpExpiredDate.Value.Date <= DateTime.Now.Date)
            {
                e.Cancel = true;
                MessageBox.Show("El campo fecha de Expiración no puede ser menor o igual a la fecha actual.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void cboEmpresa_SelectedValueChanged(object sender, EventArgs e)
        {

            //if (cboEmpresa.SelectedValue == null)
            //    return;
            //if (cboEmpresa.SelectedValue.ToString() == "-1")
            //{
            //    cboProtocolo.SelectedValue = "-1";
            //    return;
            //}

            //OperationResult objOperationResult = new OperationResult();

            //var id3 = cboEmpresa.SelectedValue.ToString().Split('|');

            //Utils.LoadDropDownList(cboProtocolo, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, id3[0], id3[1], null), DropDownListAction.Select);          
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //ProtocoloCorto obj = new ProtocoloCorto();

            //obj.v_ProtocolId = cboProtocolo.SelectedValue.ToString();
            //obj.v_ProtocoloNombre = cboProtocolo.Text.ToString();

            //ListaProtocolos.Add(obj);
            //grdData.DataSource = new List<ProtocoloCorto>();
            //grdData.DataSource = ListaProtocolos;
           
        }

    }
}
