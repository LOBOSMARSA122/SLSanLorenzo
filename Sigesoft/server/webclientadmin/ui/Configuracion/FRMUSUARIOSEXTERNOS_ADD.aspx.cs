using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;


namespace Sigesoft.Server.WebClientAdmin.UI.Configuracion
{
    public partial class FRMUSUARIOSEXTERNOS_ADD : System.Web.UI.Page
    {
        private List<protocolsystemuserDto> _tmpListProtocolSystemUser = null;
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        DataHierarchyBL _objDataHierarchyBL = new DataHierarchyBL();
        PacientBL _objPacientBL = new PacientBL();
        SecurityBL _objSecurityBL = new SecurityBL();
        ProtocolBL _protocolBL = new ProtocolBL();
        List<KeyValueDTO> oLista = new List<KeyValueDTO>();
        OrganizationBL oOrganizationBL = new OrganizationBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["ListaEmpresas"] = null;
                LoadComboBox();
                LoadData();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            }
        }

        private void LoadData()
        {
            string Mode = Request.QueryString["Mode"].ToString();
            int systemUserId = -1;
            string personId = string.Empty;

            LoadComboBox();

            if (Mode == "New")
            {
                // Additional logic here.

                txtFirstName.Focus(true);

            }
            else if (Mode == "Edit")
            {
                if (Request.QueryString["systemUserId"] != null)
                    systemUserId = int.Parse(Request.QueryString["systemUserId"].ToString());
                if (Request.QueryString["personId"] != null)
                    personId = Request.QueryString["personId"].ToString();

                OperationResult objCommonOperationResultedit = new OperationResult();
                personDto personDTO = _objPacientBL.GetPerson(ref objCommonOperationResultedit, personId);

                Session["objEntity"] = personDTO;

                // Informacion de la persona
                txtFirstName.Text = personDTO.v_FirstName;
                txtFirstLastName.Text = personDTO.v_FirstLastName;
                txtSecondLastName.Text = personDTO.v_SecondLastName;
                txtDocNumber.Text = personDTO.v_DocNumber;
                dpBirthdate.SelectedDate = personDTO.d_Birthdate;
                txtBirthPlace.Text = personDTO.v_BirthPlace;
             
              
                ddlDocType.SelectedValue = personDTO.i_DocTypeId.ToString();
                ddlSexType.SelectedValue = personDTO.i_SexTypeId.ToString();
                txtTelephoneNumber.Text = personDTO.v_TelephoneNumber;
              
                txtMail.Text = personDTO.v_Mail;
                              
                // Informacion del usuario
                OperationResult objOperationResult = new OperationResult();
                systemuserDto objSystemUser = _objSecurityBL.GetSystemUser(ref objOperationResult, systemUserId);

                Session["objSystemUser"] = objSystemUser;

                txtUserName.Text = objSystemUser.v_UserName;
                txtPassword1.Text = objSystemUser.v_Password;
                txtPassword2.Text = objSystemUser.v_Password;


                //Obtener la empresa por medio del systemuserId
               var EmpresaId_ProtocoloId =  _protocolBL.ObtenerEmpresaPorSystemUserId(systemUserId);
               //ddlEmpresaCliente.SelectedValue = EmpresaId_ProtocoloId.EmpresaId;

             var ListaEmpresas=  oOrganizationBL.GetEmpresasPorUsuarioExterno(objSystemUser.i_SystemUserId);
             Session["ListaEmpresas"] = ListaEmpresas;
             grdData.DataSource = ListaEmpresas;
             grdData.DataBind();

                //Obtener Los permisos por el ProtocolId
               var Permisos = _protocolBL.ObtenerPermisosPorProtocoloId(EmpresaId_ProtocoloId.ProtocoloId, systemUserId);
               foreach (var item in Permisos)
               {
                   if (item.i_ApplicationHierarchyId == 1084)
                   {
                       chkAdminServicios.Checked = true;
                   }
                   if (item.i_ApplicationHierarchyId == 2000)
                   {
                       chkAgenda.Checked = true;
                   }
                   if (item.i_ApplicationHierarchyId == 3000)
                   {
                       chkEstadistica.Checked = true;
                   }
                   else if (item.i_ApplicationHierarchyId == 1087)
                   {
                       chkCertificado.Checked = true;
                   }
                   if (item.i_ApplicationHierarchyId == 165)
                   {
                       chkExamenes.Checked = true;
                   }
                   else if (item.i_ApplicationHierarchyId == 1086)
                   {
                       chkFichaOcupacional.Checked = true;
                   }
                   else if (item.i_ApplicationHierarchyId == 1085)
                   {
                       chkSegTrabajador.Checked = true;
                   }
               }

               
            }

        }

        private void LoadComboBox()
        {
          
            OperationResult objOperationResult = new OperationResult();
            var _DocType = _objDataHierarchyBL.GetDataHierarchyForCombo(ref objOperationResult, 106);
            Utils.LoadDropDownList(ddlDocType, "Description", "Id", _DocType, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSexType, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 100), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlEmpresaCliente, "Value1", "Id", oOrganizationBL.GetAllOrganizations(ref objOperationResult), DropDownListAction.Select);
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult1 = new OperationResult();
            string Mode = Request.QueryString["Mode"].ToString();
            int systemUserId = -1;
            string SihayError = "";
            string personId = "";
            if (Mode == "New")
            {
                // Datos de persona
                personDto objPerson = new personDto();
                objPerson.v_FirstName = txtFirstName.Text.Trim().ToUpper();
                objPerson.v_FirstLastName = txtFirstLastName.Text.Trim().ToUpper();
                objPerson.v_SecondLastName = txtSecondLastName.Text.Trim().ToUpper();
                objPerson.i_DocTypeId = Convert.ToInt32(ddlDocType.SelectedValue);
                objPerson.i_SexTypeId = Convert.ToInt32(ddlSexType.SelectedValue);
                objPerson.v_DocNumber = txtDocNumber.Text.Trim();
                objPerson.d_Birthdate = dpBirthdate.SelectedDate;
                objPerson.v_BirthPlace = txtBirthPlace.Text.Trim().ToUpper();
                objPerson.v_TelephoneNumber = txtTelephoneNumber.Text.Trim();
                objPerson.v_Mail = txtMail.Text.Trim();

                // Datos de usuario
                systemuserDto pobjSystemUser = new systemuserDto();
                pobjSystemUser.v_UserName = txtUserName.Text.Trim();
                pobjSystemUser.v_Password = SecurityBL.Encrypt(txtPassword2.Text.Trim());
                pobjSystemUser.i_SystemUserTypeId = (int)SystemUserTypeId.External;

                systemUserId = _objPacientBL.AddPersonUsuarioExterno(ref objOperationResult1,
                                                          objPerson,
                                                          null,
                                                          pobjSystemUser,
                                                          ((ClientSession)Session["objClientSession"]).GetAsList());


                if (_tmpListProtocolSystemUser == null)
                    _tmpListProtocolSystemUser = new List<protocolsystemuserDto>();


                //Obtener Todos los protocolos de la Empresa         

                var ListaEmpresas = (List<KeyValueDTO>)Session["ListaEmpresas"];
                foreach (var itemEmpresa in ListaEmpresas)
                {
                    string[] x = itemEmpresa.Id.ToString().Split('|');


                    var ListaProtocolos = _protocolBL.DevolverProtocolosPorEmpresa(x[0].ToString(), x[1].ToString());
                    foreach (var item in ListaProtocolos)
                    {
                        _tmpListProtocolSystemUser = new List<protocolsystemuserDto>();
                        protocolsystemuserDto protocolSystemUser;
                        if (chkAdminServicios.Checked)
                        {
                            protocolSystemUser = new protocolsystemuserDto();
                            protocolSystemUser.i_ApplicationHierarchyId = 1084;
                            protocolSystemUser.v_ProtocolId = item.v_ProtocolId;
                            _tmpListProtocolSystemUser.Add(protocolSystemUser);
                        }

                        if (chkAgenda.Checked)
                        {
                            protocolSystemUser = new protocolsystemuserDto();
                            protocolSystemUser.i_ApplicationHierarchyId = 2000;
                            protocolSystemUser.v_ProtocolId = item.v_ProtocolId;
                            _tmpListProtocolSystemUser.Add(protocolSystemUser);

                            protocolSystemUser = new protocolsystemuserDto();
                            protocolSystemUser.i_ApplicationHierarchyId = 2001;
                            protocolSystemUser.v_ProtocolId = item.v_ProtocolId;
                            _tmpListProtocolSystemUser.Add(protocolSystemUser);
                        }

                        if (chkEstadistica.Checked)
                        {
                            protocolSystemUser = new protocolsystemuserDto();
                            protocolSystemUser.i_ApplicationHierarchyId = 3000;
                            protocolSystemUser.v_ProtocolId = item.v_ProtocolId;
                            _tmpListProtocolSystemUser.Add(protocolSystemUser);
                        }

                        if (chkCertificado.Checked)
                        {
                            protocolSystemUser = new protocolsystemuserDto();
                            protocolSystemUser.i_ApplicationHierarchyId = 1087;
                            protocolSystemUser.v_ProtocolId = item.v_ProtocolId;
                            _tmpListProtocolSystemUser.Add(protocolSystemUser);
                        }

                        if (chkExamenes.Checked)
                        {
                            protocolSystemUser = new protocolsystemuserDto();
                            protocolSystemUser.i_ApplicationHierarchyId = 165;
                            protocolSystemUser.v_ProtocolId = item.v_ProtocolId;
                            _tmpListProtocolSystemUser.Add(protocolSystemUser);
                        }

                        if (chkFichaOcupacional.Checked)
                        {
                            protocolSystemUser = new protocolsystemuserDto();
                            protocolSystemUser.i_ApplicationHierarchyId = 1086;
                            protocolSystemUser.v_ProtocolId = item.v_ProtocolId;
                            _tmpListProtocolSystemUser.Add(protocolSystemUser);

                        }

                        if (chkSegTrabajador.Checked)
                        {
                            protocolSystemUser = new protocolsystemuserDto();
                            protocolSystemUser.i_ApplicationHierarchyId = 1085;
                            protocolSystemUser.v_ProtocolId = item.v_ProtocolId;
                            _tmpListProtocolSystemUser.Add(protocolSystemUser);
                        }

                        // Graba UsuarioExterno                        
                        SihayError = _protocolBL.AddSystemUserExternal(ref objOperationResult1, _tmpListProtocolSystemUser, ((ClientSession)Session["objClientSession"]).GetAsList(), systemUserId);
                    }
                }
           


              bgwSendEmail_DoWork();

              if (SihayError == "-1")
                {
                    Alert.ShowInTop(objOperationResult1.ErrorMessage);
                    return;
                }
                else
                {
                    if (objOperationResult1.Success != 1)
                    {
                        Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult1.ExceptionMessage);
                        return;
                    }
                }
           
            }
            else if (Mode == "Edit")
            {
                if (Request.QueryString["personId"] != null)
                    personId = Request.QueryString["personId"].ToString();
                if (Request.QueryString["systemUserId"] != null)
                    systemUserId = int.Parse(Request.QueryString["systemUserId"].ToString());              

                systemuserDto systemUser = new systemuserDto();
                personDto _objPerson = Session["objEntity"] as personDto;
                systemuserDto _objSystemUserTemp = Session["objSystemUser"] as systemuserDto;

                bool isChangeUserName = false;
                bool isChangeDocNumber = false;

                #region Validate SystemUSer
                // Almacenar temporalmente el nombre de usuario actual
                var _userNameTemp = _objSystemUserTemp.v_UserName;
                if (txtUserName.Text != _userNameTemp)
                {
                    isChangeUserName = true;
                }
                #endregion

                // Datos de persona
                _objPerson.v_FirstName = txtFirstName.Text.Trim().ToUpper();
                _objPerson.v_FirstLastName = txtFirstLastName.Text.Trim().ToUpper();
                _objPerson.v_SecondLastName = txtSecondLastName.Text.Trim().ToUpper();
                _objPerson.i_DocTypeId = Convert.ToInt32(ddlDocType.SelectedValue);
                _objPerson.i_SexTypeId = Convert.ToInt32(ddlSexType.SelectedValue);
                _objPerson.v_DocNumber = txtDocNumber.Text;
                _objPerson.d_Birthdate = dpBirthdate.SelectedDate;
                _objPerson.v_BirthPlace = txtBirthPlace.Text.Trim().ToUpper();
                _objPerson.v_TelephoneNumber = txtTelephoneNumber.Text;
                _objPerson.v_Mail = txtMail.Text;
                _objPerson.i_UpdateNodeId = ((ClientSession)Session["objClientSession"]).i_CurrentExecutionNodeId;

                // Almacenar temporalmente el password del usuario actual
                var _passTemp = _objSystemUserTemp.v_Password;

                // Si el password actual es diferente al ingresado en la cajita de texto, quiere decir que se ha cambiado el password por lo tanto
                // se bede encriptar el nuevo password
                if (txtPassword2.Text != _passTemp)
                {
                    systemUser.v_Password = SecurityBL.Encrypt(txtPassword2.Text.Trim());
                }
                else
                {
                    systemUser.v_Password = txtPassword2.Text.Trim();
                }

                // Datos de Usuario
                systemUser.i_SystemUserId = _objSystemUserTemp.i_SystemUserId;
                systemUser.v_PersonId = personId;
                systemUser.v_UserName = txtUserName.Text;
                systemUser.d_InsertDate = _objSystemUserTemp.d_InsertDate;
                systemUser.i_InsertUserId = _objSystemUserTemp.i_SystemUserId;
                systemUser.i_IsDeleted = _objSystemUserTemp.i_IsDeleted;
                systemUser.i_SystemUserTypeId = (int)SystemUserTypeId.External;
                // Actualiza persona

                _objPacientBL.UpdatePerson(ref objOperationResult1,
                                                isChangeDocNumber,
                                                _objPerson,
                                                null,
                                                isChangeUserName,
                                                systemUser,
                                                ((ClientSession)Session["objClientSession"]).GetAsList());


                //Eliminar Los permisos de este usuario
                _protocolBL.EliminarFisicamentePermisosPorUsuario(ref objOperationResult1, systemUserId);

                //Obtener Permisos nuevos
                             //Obtener Todos los protocolos de la Empresa       

                //string[] x = ddlEmpresaCliente.SelectedValue.ToString().Split('|');
                  var ListaEmpresas = (List<KeyValueDTO>)Session["ListaEmpresas"];
                  foreach (var itemEmpresa in ListaEmpresas)
                  {
                      string[] x = itemEmpresa.Id.ToString().Split('|');

                      var ListaProtocolos = _protocolBL.DevolverProtocolosPorEmpresa(x[0].ToString(), x[1].ToString());

                      //var ListaProtocolos=  _protocolBL.DevolverProtocolosPorEmpresa(ddlEmpresaCliente.SelectedValue.ToString());
                      foreach (var item in ListaProtocolos)
                      {
                          _tmpListProtocolSystemUser = new List<protocolsystemuserDto>();
                          protocolsystemuserDto protocolSystemUser;
                          if (chkAdminServicios.Checked)
                          {
                              protocolSystemUser = new protocolsystemuserDto();
                              protocolSystemUser.i_ApplicationHierarchyId = 1084;
                              protocolSystemUser.v_ProtocolId = item.v_ProtocolId;
                              _tmpListProtocolSystemUser.Add(protocolSystemUser);
                          }

                          if (chkAgenda.Checked)
                          {
                              protocolSystemUser = new protocolsystemuserDto();
                              protocolSystemUser.i_ApplicationHierarchyId = 2000;
                              protocolSystemUser.v_ProtocolId = item.v_ProtocolId;
                              _tmpListProtocolSystemUser.Add(protocolSystemUser);

                              protocolSystemUser = new protocolsystemuserDto();
                              protocolSystemUser.i_ApplicationHierarchyId = 2001;
                              protocolSystemUser.v_ProtocolId = item.v_ProtocolId;
                              _tmpListProtocolSystemUser.Add(protocolSystemUser);
                          }

                          if (chkCertificado.Checked)
                          {
                              protocolSystemUser = new protocolsystemuserDto();
                              protocolSystemUser.i_ApplicationHierarchyId = 1087;
                              protocolSystemUser.v_ProtocolId = item.v_ProtocolId;
                              _tmpListProtocolSystemUser.Add(protocolSystemUser);
                          }

                          if (chkEstadistica.Checked)
                          {
                              protocolSystemUser = new protocolsystemuserDto();
                              protocolSystemUser.i_ApplicationHierarchyId = 3000;
                              protocolSystemUser.v_ProtocolId = item.v_ProtocolId;
                              _tmpListProtocolSystemUser.Add(protocolSystemUser);
                          }

                          if (chkExamenes.Checked)
                          {
                              protocolSystemUser = new protocolsystemuserDto();
                              protocolSystemUser.i_ApplicationHierarchyId = 165;
                              protocolSystemUser.v_ProtocolId = item.v_ProtocolId;
                              _tmpListProtocolSystemUser.Add(protocolSystemUser);
                          }

                          if (chkFichaOcupacional.Checked)
                          {
                              protocolSystemUser = new protocolsystemuserDto();
                              protocolSystemUser.i_ApplicationHierarchyId = 1086;
                              protocolSystemUser.v_ProtocolId = item.v_ProtocolId;
                              _tmpListProtocolSystemUser.Add(protocolSystemUser);

                          }

                          if (chkSegTrabajador.Checked)
                          {
                              protocolSystemUser = new protocolsystemuserDto();
                              protocolSystemUser.i_ApplicationHierarchyId = 1085;
                              protocolSystemUser.v_ProtocolId = item.v_ProtocolId;
                              _tmpListProtocolSystemUser.Add(protocolSystemUser);
                          }

                          // Graba UsuarioExterno                        
                          SihayError = _protocolBL.AddSystemUserExternal(ref objOperationResult1, _tmpListProtocolSystemUser, ((ClientSession)Session["objClientSession"]).GetAsList(), systemUserId);
            
                  }

              

              }





                if (objOperationResult1.ErrorMessage != null)
                {
                    Alert.ShowInTop(objOperationResult1.ErrorMessage);
                    return;
                }
                else
                {
                    if (objOperationResult1.Success != 1)
                    {
                        Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult1.ExceptionMessage);
                        return;
                    }
                }             

            }

            // Cerrar página actual y hacer postback en el padre para actualizar
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());   
        }

        private void bgwSendEmail_DoWork()
        {
            OperationResult objOperationResult = new OperationResult();

            try
            {
                // Obtener los Parametros necesarios para el envio de notificación
                var configEmail = _objSystemParameterBL.GetSystemParameterForComboAll(ref objOperationResult, 161, "i_ParameterId");

                string smtp = configEmail[0].Value1.ToLower();
                int port = int.Parse(configEmail[1].Value1);
                string from = configEmail[2].Value1.ToLower();
                string fromPassword = configEmail[4].Value1;
                string subject = configEmail[6].Value1;
                bool enableSsl = Convert.ToBoolean(int.Parse(configEmail[3].Value1));
                string personName = string.Format("{0} {1} {2}", txtFirstName.Text.Trim(), txtFirstLastName.Text.Trim(), txtSecondLastName.Text.Trim());
                string message = string.Format(configEmail[5].Value1, personName, txtUserName.Text, txtPassword2.Text);
           
                // Enviar notificación de usuario y clave via email
                Sigesoft.Common.Utils.SendMessage(smtp, port, enableSsl, true, from, fromPassword, txtMail.Text.Trim(), "", subject, message, null);
            }
            catch (Exception ex)
            {                
              
            }

        }

        protected void btnAgregarEmpresa_Click(object sender, EventArgs e)
        {
            if (ddlEmpresaCliente.SelectedValue != "-1")
            {
                oLista = new List<KeyValueDTO>();

                if (Session["ListaEmpresas"] != null)
                {
                    oLista = (List<KeyValueDTO>)Session["ListaEmpresas"];
                }
                KeyValueDTO oKeyValueDTO = new KeyValueDTO();

                oKeyValueDTO.Id = ddlEmpresaCliente.SelectedValue;
                oKeyValueDTO.Value1 = ddlEmpresaCliente.SelectedText;


                oLista.Add(oKeyValueDTO);                
                grdData.DataSource = oLista;
                grdData.DataBind();
                Session["ListaEmpresas"] = oLista;
                ddlEmpresaCliente.SelectedValue = "-1";
            }
        }

        protected void grdData_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAction")
            {
                int x =grdData.SelectedRowIndex;
                oLista = (List<KeyValueDTO>)Session["ListaEmpresas"];
                oLista.RemoveAt(x);

                Session["ListaEmpresas"] = oLista;
                grdData.DataSource = oLista;
                grdData.DataBind();

            }
        }
       

    }
}