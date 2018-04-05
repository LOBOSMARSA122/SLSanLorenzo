using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;

//using FineUI;
using System.Web.Security;
namespace Sigesoft.Server.WebClientAdmin.UI
{
    public partial class frmLogin1 : System.Web.UI.Page
    {
        SecurityBL _objSecurityBL = new SecurityBL();
         PacientBL _objPacientBL = new PacientBL();
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        private void bgwSendEmail(string PersonId, string Nombre, string ApePaterno, string ApeMaterno, string Pass, string Email)
        {
            OperationResult objOperationResult = new OperationResult();

            try
            {
                //Obtener el nuevo Pass
                string decimalNumber = Pass;
                int number = int.Parse(decimalNumber);
                string hex = number.ToString("x");
                string  NuevoPAss = hex.ToString();

                //Actualizar nuevo Pass

                PacientBL oPacientBL = new PacientBL();
                oPacientBL.ActualizarContraseniaPaciente(ref objOperationResult, PersonId, NuevoPAss);


                // Obtener los Parametros necesarios para el envio de notificación
                var configEmail = _objSystemParameterBL.GetSystemParameterForComboOrder(ref objOperationResult, 161, "i_ParameterId");

                string smtp = configEmail[0].Value1.ToLower();
                int port = int.Parse(configEmail[1].Value1);
                string from = configEmail[2].Value1.ToLower();
                string fromPassword = configEmail[4].Value1;
                string subject = configEmail[6].Value1;
                bool enableSsl = Convert.ToBoolean(int.Parse(configEmail[3].Value1));
                string personName = string.Format("{0} {1} {2}", Nombre, ApePaterno, ApeMaterno);
                string message = string.Format(configEmail[5].Value1, personName, email.Value.Trim(), NuevoPAss);
 
                // Enviar notificación de usuario y clave via email
                Sigesoft.Common.Utils.SendMessage(smtp, port, enableSsl, true, from, fromPassword, Email, "", subject, message, null);
            }
            catch (Exception ex)
            {}
          

        }


        protected void go_Click(object sender, EventArgs e)
        {
            //// Autenticación de usuario
            //OperationResult objOperationResult1 = new OperationResult();
            //var objSystemUser = _objSecurityBL.ValidateSystemUser(ref objOperationResult1,
            //                                                        1,
            //                                                        email.Value.Trim(),
            //                                                        Sigesoft.Common.Utils.Encrypt(password.Value.Trim()));

            //if (objSystemUser != null)
            //{
            //    ClientSession clientSession = new ClientSession();
            //    clientSession.i_SystemUserId = objSystemUser.i_SystemUserId;
            //    clientSession.v_UserName = objSystemUser.v_UserName;
            //    clientSession.i_CurrentExecutionNodeId = 1;
            //    clientSession.i_CurrentOrganizationId = 0;
            //    clientSession.v_PersonId = objSystemUser.v_PersonId;
            //    clientSession.i_SystemUserTypeId = (int)objSystemUser.i_SystemUserTypeId.Value;

            //    if (clientSession.i_SystemUserTypeId == 2)
            //    {
            //        Session["EmpresaClienteId"] = objSystemUser.v_EmpresaClienteId;
            //    }
            //    clientSession.i_ProfesionId = objSystemUser.i_ProfesionId;
            //    //Obtener RoleID
            //   var obj =  _objSecurityBL.ObtenerRolIdUser(ref objOperationResult1, 9, objSystemUser.i_SystemUserId);
            //   if (obj != null)
            //   {
            //       clientSession.i_RoleId = obj.i_RolId;
            //   }   
             
            //    Session["objClientSession"] = clientSession;

            //    FormsAuthentication.RedirectFromLoginPage(objSystemUser.v_UserName, true);

            //}
            //else
            //{
            //    //Alert.ShowInTop(objOperationResult1.AdditionalInformation);




            OperationResult objOperationResult1 = new OperationResult();
            //Validar si es un paciente
            var objPaciente = _objPacientBL.ValidarPersonaWeb(ref objOperationResult1,  email.Value.Trim(), password.Value.Trim());

            if (objPaciente != null)
            {
                string DocNumber = objPaciente.v_DocNumber;

                if (objPaciente.v_Password ==  password.Value.Trim())
                {

                    Session["IdPersona"] = objPaciente.v_PersonId;
                    

                    if (objPaciente.v_DocNumber == objPaciente.v_Password)
                    {
                        if (!string.IsNullOrEmpty(objPaciente.v_Mail))
                        {
                            //Alert.ShowInTop("Se le envió un correo electrónico con sus nuevas credenciales.");
                            Text1.Value= "Se le envió un correo electrónico con sus nuevas credenciales.";
                            bgwSendEmail(objPaciente.v_PersonId, objPaciente.v_FirstName, objPaciente.v_FirstLastName, objPaciente.v_SecondLastName, DocNumber, objPaciente.v_Mail);
                        }
                        else
                        {
                            Text1.Value= "No está registrado su correo electrónico.";
                            //Alert.ShowInTop("No está registrado su correo electrónico.");
                        }
                    }
                    else
                    {
                        FormsAuthentication.RedirectFromLoginPage("Trabajador", true);
                    }
                  

                }
                else
                {
                    //Alert.ShowInTop("Usuario o Password incorrectos.");
                    Text1.Value= "Usuario o Password incorrectos.";
                }
            }
            else
            {
                // Autenticación de usuario

                var objSystemUser = _objSecurityBL.ValidateSystemUser(ref objOperationResult1,
                                                                        1,
                                                                       email.Value.Trim(),
                                                                        Sigesoft.Common.Utils.Encrypt( password.Value.Trim()));
                if (objSystemUser != null)
                {
                    ClientSession clientSession = new ClientSession();
                    clientSession.i_SystemUserId = objSystemUser.i_SystemUserId;
                    clientSession.v_UserName = objSystemUser.v_UserName;
                    clientSession.i_CurrentExecutionNodeId = 1;
                    clientSession.i_CurrentOrganizationId =objSystemUser.i_CurrentOrganizationId== null?0: objSystemUser.i_CurrentOrganizationId.Value;//Verifica si es con dx o sin dx
                    clientSession.v_PersonId = objSystemUser.v_PersonId;
                    clientSession.i_SystemUserTypeId = (int)objSystemUser.i_SystemUserTypeId.Value;
                    clientSession.i_ProfesionId = objSystemUser.i_ProfesionId;
                    
                    //Obtener RoleID
                    var obj = _objSecurityBL.ObtenerRolIdUser(ref objOperationResult1, 9, objSystemUser.i_SystemUserId);
                    if (obj != null)
                    {
                        clientSession.i_RoleId = obj.i_RolId;
                    }
                    Session["objClientSession"] = clientSession;

                    FormsAuthentication.RedirectFromLoginPage(objSystemUser.v_UserName, true);
                }
              

            }
           

        }
    }
}