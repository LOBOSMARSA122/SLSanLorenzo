using Infragistics.Win.UltraWinGrid;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigesoft.Node.WinClient.UI.Configuration
{
    public partial class FrmMaintenance : Form
    {
        private List<KeyValueDTO> _docType = new List<KeyValueDTO>();
        private ProtocolBL _protocolBL = new ProtocolBL();
        PacientBL _objPacientBL = new PacientBL();
        systemuserDto _objSystemUserTemp = new systemuserDto();
        personDto objPerson;
        SecurityBL _objSecurityBL = new SecurityBL();
        private List<KeyValueDTO> _notificationUserExternal = null;
        private int _lenght;
        private string _protocolId;
        string _personId;
        private List<KeyValueDTO> _permissesUserExternal = null;
        private int? _systemUserId;
        private string _mode;
        frmWaiting frmWaiting = new frmWaiting("Enviando Notificación");
        public FrmMaintenance()
        {
            InitializeComponent();
        }

        private void FrmMaintenance_Load(object sender, EventArgs e)
        {
            _mode = "New";
            CustomGrid();
            loadCombo();
            LoadAllchkList();
        }

        private void CustomGrid()
        {
            UltraGridColumn c = grdData.DisplayLayout.Bands[0].Columns["select"];
            c.CellActivation = Activation.AllowEdit;
            c.CellClickAction = CellClickAction.Edit;
        }

        private void loadCombo()
        {
            OperationResult objOperationResult = new OperationResult();
            _docType = BLL.Utils.GetDataHierarchyForCombo(ref objOperationResult, 106, null);

            Utils.LoadDropDownList(cboExternalUser, "Value1", "Id", BLL.Utils.GetAllExternalSystemUser(ref objOperationResult, ""), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlDocType, "Value1", "Id", _docType, DropDownListAction.Select);

            var clientOrganization = BLL.Utils.GetAllOrganizations(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);

            chkEmpresas.DataSource = clientOrganization;
            chkEmpresas.DisplayMember = "Value1";
            chkEmpresas.ValueMember = "Id";
            
        }

        private void cboExternalUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            ProfessionalBL oProfessionalBL = new ProfessionalBL();
            OperationResult objOperationResult = new OperationResult();

            if (cboExternalUser.SelectedValue == null)
                return;

            if (cboExternalUser.SelectedValue.ToString() == "-1")
            {
                lblNameExternalUser.Text = "Nombres y Apellidos del Profesional";
                return;
            }



            clearForm();
            LoadAllchkList();
            _mode = "Edit";
            var oSystemUserList = oProfessionalBL.GetSystemUserNameExternal(ref objOperationResult, int.Parse(cboExternalUser.SelectedValue.ToString()));
            lblNameExternalUser.Text = oSystemUserList== null? "": oSystemUserList.v_PersonName;
            _systemUserId = int.Parse(cboExternalUser.SelectedValue.ToString());
            _personId = oSystemUserList== null? null: oSystemUserList.v_PersonId;

            LoadGrid();
            LoadDataExternalUser();

           
        }

        private void clearForm()
        {            
            _personId = null;
            _protocolId = null;
            txtName.Text = "";
            ddlDocType.SelectedValue = "-1";
            txtFirstLastName.Text = "";
            txtDocNumber.Text = "";
            txtSecondLastName.Text = "";
            txtMail.Text = "";
            txtUserName.Text = "";
            txtPassword1.Text = "";
            txtPassword2.Text = "";
            //cboEmpresa.SelectedValue = "-1";
            LoadGrid();
            //chklNotificaciones.DataSource = new List<KeyValueDTO>();
            //chklPermisosOpciones.DataSource = new List<KeyValueDTO>();
        }

        List<ProtocolList> _protocolsOld = new List<ProtocolList>();
        private void LoadGrid()
        {
            var protocols = _protocolBL.GetProtocolsBySystemUserExternal(_systemUserId == null ? -1 : _systemUserId.Value);
            _protocolsOld = protocols;     

            grdData.DataSource = protocols;
            if (grdData.Rows.Count > 0)
            {
                grdData.Rows[0].Selected = true;
                _protocolId = grdData.Rows[0].Cells["v_ProtocolId"].Value.ToString();
            }   
        }

        private void LoadDataExternalUser()
        {
            // Setear lenght dimamicos de numero de documento
            SetLenght(ddlDocType.SelectedValue.ToString());

            OperationResult objCommonOperationResultedit = new OperationResult();
            objPerson = _objPacientBL.GetPerson(ref objCommonOperationResultedit, _personId);

            Text = this.Text + " (" + objPerson.v_FirstName + " " + objPerson.v_FirstLastName + " " + objPerson.v_SecondLastName + ")";

            // Informacion de la persona
            txtName.Text = objPerson.v_FirstName;
            txtFirstLastName.Text = objPerson.v_FirstLastName;
            txtSecondLastName.Text = objPerson.v_SecondLastName;
            txtDocNumber.Text = objPerson.v_DocNumber;
            ddlDocType.SelectedValue = objPerson.i_DocTypeId.ToString();
            txtDocNumber.Text = objPerson.v_DocNumber;
            txtMail.Text = objPerson.v_Mail;


            for (int i = 0; i < chkEmpresas.Items.Count - 1; i++)
            {
                chkEmpresas.SetItemChecked(i, false);
            }

            // Informacion del usuario
            OperationResult objOperationResult = new OperationResult();
            _objSystemUserTemp = _objSecurityBL.GetSystemUser(ref objOperationResult, _systemUserId.Value);

            //cboEmpresa.SelectedItem = item;// _objSystemUserTemp.v_SystemUserByOrganizationId == null ? "-1" : _objSystemUserTemp.v_SystemUserByOrganizationId;
            if (!string.IsNullOrEmpty(_objSystemUserTemp.v_SystemUserByOrganizationId))
            {
                var organizationIds = _objSystemUserTemp.v_SystemUserByOrganizationId.Split(',').ToList();

                foreach (var item in organizationIds)
                {
                    for (int i = 0; i < chkEmpresas.Items.Count; i++)
                    {
                        KeyValueDTO obj = (KeyValueDTO)chkEmpresas.Items[i];

                        if (obj.Id.Trim() == item.Trim())
                        {
                            chkEmpresas.SetItemChecked(i, true);
                            break;
                        }
                    }
                }
   
            }
           


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
                    KeyValueDTO obj = (KeyValueDTO)chklPermisosOpciones.Items[i];

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

        private void LoadAllchkList()
        {
            OperationResult objOperationResult = new OperationResult();

            for (int i = 0; i < chklPermisosOpciones.Items.Count - 1; i++)
            {
                chklPermisosOpciones.SetItemChecked(i, false);
            }
            this.chklPermisosOpciones.SelectedValueChanged -= new System.EventHandler(this.chklPermisosOpciones_SelectedValueChanged);

            chklPermisosOpciones.DataSource = _protocolBL.GetExternalPermisionForChekedListByTypeId(ref objOperationResult, (int)ExternalUserFunctionalityType.PermisosOpcionesUsuarioExternoWeb);
            chklPermisosOpciones.DisplayMember = "Value1";
            chklPermisosOpciones.ValueMember = "Id";

            this.chklPermisosOpciones.SelectedValueChanged += new System.EventHandler(this.chklPermisosOpciones_SelectedValueChanged);

            for (int i = 0; i < chklNotificaciones.Items.Count - 1; i++)
            {
                chklNotificaciones.SetItemChecked(i, false);
            }
            this.chklNotificaciones.SelectedValueChanged -= new System.EventHandler(this.chklNotificaciones_SelectedValueChanged);
            chklNotificaciones.DataSource = _protocolBL.GetExternalPermisionForChekedListByTypeId(ref objOperationResult, (int)ExternalUserFunctionalityType.NotificacionesUsuarioExternoWeb);
            chklNotificaciones.DisplayMember = "Value1";
            chklNotificaciones.ValueMember = "Id";
            this.chklNotificaciones.SelectedValueChanged += new System.EventHandler(this.chklNotificaciones_SelectedValueChanged);
        }

        private void grdData_ClickCell(object sender, ClickCellEventArgs e)
        {
            if ((e.Cell.Column.Key != "select")) return;

            var protocolId = grdData.Rows[e.Cell.Row.Index].Cells["v_ProtocolId"].Value;
            var rowChange = _protocolsOld.Find(p => p.v_ProtocolId == protocolId);
            if ((e.Cell.Value.ToString() == "False"))
            {
                e.Cell.Value = true;
                rowChange.i_RecordStatus = (int)RecordStatus.Agregado;
            }
            else
            {
                e.Cell.Value = false;
                rowChange.i_RecordStatus = (int)RecordStatus.EliminadoLogico;
            } 
        }

        private void btnUpdateProtocolSystemUser_Click(object sender, EventArgs e)
        {
            if (_systemUserId == null)
            {
                MessageBox.Show("Debe Seleccionar un Usuario", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return; 
            }

            var listNew = new List<protocolsystemuserDto>();
            var protocolsNew = _protocolsOld.FindAll(p => p.i_RecordStatus == (int)RecordStatus.Agregado).ToList();
            var permisos = chklPermisosOpciones.CheckedItems;

            foreach (var protNew in protocolsNew)
            {               
                foreach (var item in permisos)
                {
                    var oprotocolsystemuserDto = new protocolsystemuserDto();
                    oprotocolsystemuserDto.i_SystemUserId = _systemUserId.Value;
                    oprotocolsystemuserDto.v_ProtocolId = protNew.v_ProtocolId;
                    KeyValueDTO obj = (KeyValueDTO)item;
                    oprotocolsystemuserDto.i_ApplicationHierarchyId = int.Parse(obj.Id);
                    listNew.Add(oprotocolsystemuserDto);    
                }                
            }

            var listRemove = new List<protocolsystemuserDto>();
            var protocolsRemove = _protocolsOld.FindAll(p => p.i_RecordStatus == (int)RecordStatus.EliminadoLogico).ToList();
            foreach (var protRemove in protocolsRemove)
            {
                foreach (var item in permisos)
                {
                    var oprotocolsystemuserDto = new protocolsystemuserDto();
                    oprotocolsystemuserDto.i_SystemUserId = _systemUserId.Value;
                    oprotocolsystemuserDto.v_ProtocolId = protRemove.v_ProtocolId;
                    KeyValueDTO obj = (KeyValueDTO)item;
                    oprotocolsystemuserDto.i_ApplicationHierarchyId = int.Parse(obj.Id);
                    listRemove.Add(oprotocolsystemuserDto);
                }                
            }
            OperationResult objOperationResult = new OperationResult();

            if (listNew.Count == 0 && listRemove.Count == 0)
            {
                MessageBox.Show("No hubo ningún cambio en los protocolos", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _protocolBL.UpdateProtocolSystemUser(ref objOperationResult, listNew, listRemove, Globals.ClientSession.GetAsList());

            if (objOperationResult.Success == 1)
            {
                MessageBox.Show("Se grabó correctamente la información. ", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {
                if (objOperationResult.ErrorMessage != null)
                {
                    MessageBox.Show(objOperationResult.ErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(Constants.GenericErrorMessage, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSaveExternalUser_Click(object sender, EventArgs e)
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
                    objPerson.v_DocNumber = txtDocNumber.Text.Trim();
                    objPerson.v_Mail = txtMail.Text.Trim();


                    // Datos de usuario
                    systemuserDto pobjSystemUser = new systemuserDto();
                    pobjSystemUser.v_UserName = txtUserName.Text.Trim();
                    pobjSystemUser.v_Password = SecurityBL.Encrypt(txtPassword2.Text.Trim());
                    pobjSystemUser.i_SystemUserTypeId = (int)SystemUserTypeId.External;
                    //if (rbFEchaExpiracion.Checked)                  
                    //    pobjSystemUser.d_ExpireDate = dtpExpiredDate.Value.Date;
                    var ListIds = new List<string>();

                    for (int i = 0; i < chkEmpresas.CheckedItems.Count; i++)
                    {
                        KeyValueDTO obj = (KeyValueDTO)chkEmpresas.CheckedItems[i];
                        ListIds.Add(obj.Id);
                    }

                    var concateOrganizationId = string.Join(",", ListIds.ToList().Select(p => p));

                    pobjSystemUser.v_SystemUserByOrganizationId = concateOrganizationId;


                    // Graba persona                        
                    systemUserId = _protocolBL.AddPersonUsuarioExterno(ref objOperationResult,
                                                              objPerson,
                                                              null,
                                                              pobjSystemUser,
                                                              Globals.ClientSession.GetAsList());

                    Utils.LoadDropDownList(cboExternalUser, "Value1", "Id", BLL.Utils.GetAllExternalSystemUser(ref objOperationResult, ""), DropDownListAction.Select);
          
                    cboExternalUser.SelectedValue = systemUserId.ToString();
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

                    //this.Enabled = false;
                    //frmWaiting.Show(this);
                    //bgwSendEmail.RunWorkerAsync();

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
                    objPerson.v_DocNumber = txtDocNumber.Text.Trim();
                    objPerson.v_Mail = txtMail.Text.Trim();

                    #endregion
   
                    #region Datos de Usuario

                    // Datos de Usuario
                    _objSystemUserTemp.i_SystemUserId = _objSystemUserTemp.i_SystemUserId;
                    _objSystemUserTemp.v_PersonId = _personId;
                    _objSystemUserTemp.v_UserName = txtUserName.Text;
                    _objSystemUserTemp.d_InsertDate = _objSystemUserTemp.d_InsertDate;
                    _objSystemUserTemp.i_InsertUserId = _objSystemUserTemp.i_SystemUserId;
                    _objSystemUserTemp.i_IsDeleted = _objSystemUserTemp.i_IsDeleted;

                    var ListIds = new List<string>();

                    for (int i = 0; i < chkEmpresas.CheckedItems.Count; i++)
                    {
                        KeyValueDTO obj = (KeyValueDTO)chkEmpresas.CheckedItems[i];
                        ListIds.Add(obj.Id);
                    }

                    var concateOrganizationId = string.Join(",", ListIds.ToList().Select(p => p));

                    _objSystemUserTemp.v_SystemUserByOrganizationId = concateOrganizationId;
                    if (rbFEchaExpiracion.Checked)
                        _objSystemUserTemp.d_ExpireDate = dtpExpiredDate.Value.Date;
                    else if (rbNuncaCaduca.Checked)
                        _objSystemUserTemp.d_ExpireDate = null;

                    #endregion
                                      

                    // Actualiza persona                 
                    _protocolBL.UpdateSystemUserExternal(ref objOperationResult,
                                                    isChangeDocNumber,
                                                    objPerson,
                                                    null,
                                                    isChangeUserName,
                                                    _objSystemUserTemp,
                                                    null,
                                                    null,
                                                    null,
                                                    null,
                                                   Globals.ClientSession.GetAsList());

                    //if (sendNotification)
                    //{
                    //    this.Enabled = false;
                    //    frmWaiting.Show(this);
                    //    bgwSendEmail.RunWorkerAsync();
                    //}

                }

                if (objOperationResult.ErrorMessage != null)
                {
                    MessageBox.Show(objOperationResult.ErrorMessage, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (objOperationResult.Success != 1)
                    {
                        MessageBox.Show("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Se grabó correctamente", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


            }
            else
            {
                MessageBox.Show("Por favor corrija la información ingresada. Vea los indicadores de error.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void btnAddExternalUser_Click(object sender, EventArgs e)
        {
            _mode = "New";
            _systemUserId = null;
            _protocolId = "";

            clearForm();
            LoadAllchkList();
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

        private void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetLenght(ddlDocType.SelectedValue.ToString());
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
            this.Enabled = true;
            frmWaiting.Visible = false;

            if ((bool)e.Result == true)
            {
                MessageBox.Show("Su correo ha sido enviado correctamente.", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void chklPermisosOpciones_SelectedValueChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            var data = _protocolBL.GetProtocolSystemUserByExternalUserId(_systemUserId);

            var groupByProtocol = data.GroupBy(g => g.v_ProtocolId).Select(s => s.First()).ToList();

            var value = chklPermisosOpciones.GetItemChecked(chklPermisosOpciones.SelectedIndex);
            var applicationHierarchyId = (KeyValueDTO)chklPermisosOpciones.SelectedItem;

            if (value)
            {
                var list = new List<protocolsystemuserDto>();
                foreach (var protocol in groupByProtocol)
                {
                    var oProtocolsystemuserDto = new protocolsystemuserDto();
                    oProtocolsystemuserDto.i_SystemUserId = _systemUserId.Value;
                    oProtocolsystemuserDto.v_ProtocolId = protocol.v_ProtocolId;
                    oProtocolsystemuserDto.i_ApplicationHierarchyId = int.Parse(applicationHierarchyId.Id);
                    list.Add(oProtocolsystemuserDto);
                }

                _protocolBL.AddProtocolSystemUser(ref objOperationResult, list, _systemUserId, Globals.ClientSession.GetAsList(), false);
            }
            else
            {
                foreach (var protocol in groupByProtocol)
                {
                    _protocolBL.DeletePermissisoByExternalUser(_systemUserId.Value, int.Parse(applicationHierarchyId.Id), protocol.v_ProtocolId, Globals.ClientSession.GetAsList());
                }
            }

        }

        private void chklNotificaciones_SelectedValueChanged(object sender, EventArgs e)
        {

        }   
    }
}
