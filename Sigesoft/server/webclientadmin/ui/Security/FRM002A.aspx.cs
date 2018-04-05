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

namespace Sigesoft.Server.WebClientAdmin.UI.Security
{
    public partial class FRM002A : System.Web.UI.Page
    {
        #region Declarations
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        DataHierarchyBL _objDataHierarchyBL = new DataHierarchyBL();
        PacientBL _objPacientBL = new PacientBL();
        SecurityBL _objSecurityBL = new SecurityBL();
     
        #endregion
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {              
                LoadData();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
                ViewState["isDeleted"] = false;

                //// SIMULAR SESION

                //ClientSession clientSession = new ClientSession();
                //clientSession.i_SystemUserId = 1;
                //clientSession.v_UserName = "";
                //clientSession.i_CurrentExecutionNodeId = 1;
                //clientSession.i_CurrentOrganizationId = 0;
                //clientSession.v_PersonId = "N001-PP0000000001";

                //Session["objClientSession"] = clientSession;

            }

        }

        private void LoadComboBox()
        {
            OperationResult objOperationResult = new OperationResult();

            var _DocType = _objDataHierarchyBL.GetDataHierarchyForCombo(ref objOperationResult, 106);


            
            Utils.LoadDropDownList(ddlDocType, "Description", "Id", _DocType, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSexType, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 100), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMaritalStatus, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 101), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLevelOfId, "Description", "Id", _objDataHierarchyBL.GetDataHierarchyForCombo(ref objOperationResult, 108), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlRolVenta, "Description", "Id", _objDataHierarchyBL.GetDataHierarchyForCombo(ref objOperationResult, 121), DropDownListAction.Select);
            Session["objDocType"] = _DocType;
           
            OperationResult objOperationResult1 = new OperationResult();

            //Llenar combo ItemParameter Tree
            ddlProfession.DataTextField = "Description";
            ddlProfession.DataValueField = "Id";
            ddlProfession.DataSimulateTreeLevelField = "Level";
            ddlProfession.DataEnableSelectField = "EnabledSelect";
            List<DataForTreeView> t = _objDataHierarchyBL.GetDataHierarchyForCombo(ref objOperationResult1, 101);
            ddlProfession.DataSource = t;
            ddlProfession.DataBind();
            this.ddlProfession.Items.Insert(0, new FineUI.ListItem("-- Seleccione --", "-1"));


           
        }

        private bool IsValidDocumentNumberLenght()
        {
            var lenght = Convert.ToInt32(ViewState["lenght"]);
            if (txtDocNumber.Text.Trim().Length < lenght || txtDocNumber.Text.Trim().Length > lenght)
            {
                txtDocNumber.MarkInvalid(String.Format("El número de Carateres requeridos es {0}", lenght));
                return false;
            }
            return true;
        }

        private byte[] FileUploadToByteArray()
        {
            byte[] byteArrayPhoto = null;

            if (filePhoto.PostedFile.InputStream != null)
            {
                using (BinaryReader reader = new BinaryReader(filePhoto.PostedFile.InputStream))
                {
                    byte[] Photo = reader.ReadBytes(filePhoto.PostedFile.ContentLength);
                    byteArrayPhoto = Photo;
                }
            }
            return byteArrayPhoto.Length == 0 ? null : byteArrayPhoto;
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString["Mode"].ToString();
            string personId = string.Empty;
            int systemUserId = -1;

            int intCurrentExecutionNodeId = ((ClientSession)Session["objClientSession"]).i_CurrentExecutionNodeId;
            int intCurrentOrganizationId = ((ClientSession)Session["objClientSession"]).i_CurrentOrganizationId;
            byte[] byteArrayPhoto = null;


                      
            if (Mode == "New")
            {
                #region Validations
                // Validar la longitud de los numeros de documentos
                if (!IsValidDocumentNumberLenght())
                {
                    return;
                }
                #endregion
             
                // Datos de persona
                personDto objPerson = new personDto();
                objPerson.v_FirstName = txtFirstName.Text.Trim().ToUpper();
                objPerson.v_FirstLastName = txtFirstLastName.Text.Trim().ToUpper();
                objPerson.v_SecondLastName = txtSecondLastName.Text.Trim().ToUpper();
                objPerson.i_DocTypeId = Convert.ToInt32(ddlDocType.SelectedValue);
                objPerson.i_SexTypeId = Convert.ToInt32(ddlSexType.SelectedValue);
                objPerson.i_MaritalStatusId = Convert.ToInt32(ddlMaritalStatus.SelectedValue);
                objPerson.i_LevelOfId = Convert.ToInt32(ddlLevelOfId.SelectedValue);
                objPerson.v_DocNumber = txtDocNumber.Text.Trim();
                objPerson.d_Birthdate = dpBirthdate.SelectedDate;
                objPerson.v_BirthPlace = txtBirthPlace.Text.Trim().ToUpper();
                objPerson.v_TelephoneNumber = txtTelephoneNumber.Text.Trim();
                objPerson.v_AdressLocation = txtAdressLocation.Text.Trim().ToUpper();
                objPerson.v_Mail = txtMail.Text.Trim();

                professionalDto objProfessional = null;

                // Datos de Profesional                      
                objProfessional = new professionalDto();
                objProfessional.i_ProfessionId = Convert.ToInt32(ddlProfession.SelectedValue);
                if (txtProfessionalCode.Text.Trim() != string.Empty)
                    objProfessional.v_ProfessionalCode = txtProfessionalCode.Text.Trim();
                if (txtProfessionalInformation.Text.Trim() != string.Empty)
                    objProfessional.v_ProfessionalInformation = txtProfessionalInformation.Text.Trim();

                if (filePhoto.HasFile) // Si hay una imagen cargada lista para ser serializada sino se graba x defecto null 
                {
                    byteArrayPhoto = FileUploadToByteArray();
                    objProfessional.b_SignatureImage = byteArrayPhoto;
                }

                // Datos de usuario
                systemuserDto pobjSystemUser = new systemuserDto();               
                pobjSystemUser.v_PersonId = personId;
                pobjSystemUser.v_UserName = txtUserName.Text.Trim();
                pobjSystemUser.v_Password = SecurityBL.Encrypt(txtPassword2.Text.Trim());
                pobjSystemUser.i_RolVentaId = int.Parse(ddlRolVenta.SelectedValue.ToString());
                pobjSystemUser.i_SystemUserTypeId = (int)SystemUserTypeId.Internal;
                // Graba persona      
                OperationResult objOperationResult1 = new OperationResult();
                personId = _objPacientBL.AddPerson(ref objOperationResult1,
                                                          objPerson,
                                                          objProfessional,
                                                          pobjSystemUser,
                                                          ((ClientSession)Session["objClientSession"]).GetAsList());

                if (personId == "-1")
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

                #region Validate Document Number Lenght
                // Validar la longitud de los numeros de documentos
                if (!IsValidDocumentNumberLenght())
                {
                    return;
                }
                #endregion

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

                #region Validate Document Number
                // Almacenar temporalmente el número de documento del usuario actual
                var _docNumberTemp = _objPerson.v_DocNumber;
                if (txtDocNumber.Text != _docNumberTemp)
                {
                    isChangeDocNumber = true;
                }
                #endregion

                // Datos de persona
                _objPerson.v_FirstName = txtFirstName.Text.Trim().ToUpper();
                _objPerson.v_FirstLastName = txtFirstLastName.Text.Trim().ToUpper();
                _objPerson.v_SecondLastName = txtSecondLastName.Text.Trim().ToUpper();
                _objPerson.i_DocTypeId = Convert.ToInt32(ddlDocType.SelectedValue);
                _objPerson.i_SexTypeId = Convert.ToInt32(ddlSexType.SelectedValue);
                _objPerson.i_MaritalStatusId = Convert.ToInt32(ddlMaritalStatus.SelectedValue);
                _objPerson.i_LevelOfId = Convert.ToInt32(ddlLevelOfId.SelectedValue);
                _objPerson.v_DocNumber = txtDocNumber.Text;
                _objPerson.d_Birthdate = dpBirthdate.SelectedDate;
                _objPerson.v_BirthPlace = txtBirthPlace.Text.Trim().ToUpper();
                _objPerson.v_TelephoneNumber = txtTelephoneNumber.Text;
                _objPerson.v_AdressLocation = txtAdressLocation.Text.Trim().ToUpper();
                _objPerson.v_Mail = txtMail.Text;
                _objPerson.i_UpdateNodeId = ((ClientSession)Session["objClientSession"]).i_CurrentExecutionNodeId;

                // Datos de Profesional
                professionalDto _objProfessional = Session["objProfessional"] as professionalDto;

                var b = filePhoto.HasFile;              

                if (_objProfessional != null)
                {
                    _objProfessional.v_PersonId = personId;
                    _objProfessional.i_ProfessionId = Convert.ToInt32(ddlProfession.SelectedValue);
                    _objProfessional.v_ProfessionalCode = txtProfessionalCode.Text;
                    _objProfessional.v_ProfessionalInformation = txtProfessionalInformation.Text;
                    _objProfessional.i_UpdateNodeId = ((ClientSession)Session["objClientSession"]).i_CurrentExecutionNodeId;

                    if (filePhoto.HasFile) // chekar si el control Upload tiene una imagen cargada para serializar
                    {
                        var byteArrayPhoto1 = FileUploadToByteArray();
                        _objProfessional.b_SignatureImage = byteArrayPhoto1;
                    }
                    else
                    {
                        bool isDeleted = Convert.ToBoolean(ViewState["isDeleted"]);

                        if (isDeleted)
                        {
                            _objProfessional.b_SignatureImage = null;
                        }
                    }
                }
               
         
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
                systemUser.i_SystemUserTypeId = (int)SystemUserTypeId.Internal;
                systemUser.i_RolVentaId = int.Parse(ddlRolVenta.SelectedValue.ToString());
                // Actualiza persona
                OperationResult objOperationResult1 = new OperationResult();
                _objPacientBL.UpdatePerson(ref objOperationResult1, 
                                                isChangeDocNumber,
                                                _objPerson,
                                                _objProfessional,
                                                isChangeUserName,
                                                systemUser,
                                                ((ClientSession)Session["objClientSession"]).GetAsList());
                ImgPhoto.Dispose();

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
                ddlMaritalStatus.SelectedValue = personDTO.i_MaritalStatusId.ToString();
                ddlLevelOfId.SelectedValue = personDTO.i_LevelOfId.ToString();
                ddlDocType.SelectedValue = personDTO.i_DocTypeId.ToString();
                ddlSexType.SelectedValue = personDTO.i_SexTypeId.ToString();
                txtTelephoneNumber.Text = personDTO.v_TelephoneNumber;
                txtAdressLocation.Text = personDTO.v_AdressLocation;
                txtMail.Text = personDTO.v_Mail;

                // Setear lenght dimamicos de numero de documento
                SetLenght(ddlDocType.SelectedValue);

                // Informacion de Profesional
                OperationResult objCommonOperationResult1 = new OperationResult();
                var objProfessional = _objPacientBL.GetProfessional(ref objCommonOperationResult1, personId);

                if (objProfessional != null)
                {
                    ddlProfession.SelectedValue = objProfessional.i_ProfessionId.ToString();
                    txtProfessionalCode.Text = objProfessional.v_ProfessionalCode;
                    txtProfessionalInformation.Text = objProfessional.v_ProfessionalInformation;
                    ImgPhoto.ImageUrl = null;
                    if (objProfessional.b_SignatureImage != null)
                    {
                        string pathImage = byteArrayToImage(objProfessional.b_SignatureImage);                   
                        string str = @"~\Utils\GetImageText.ashx?" + getParameterRequest("imgDeliverValid", "180", "", "Arial Black", "Black", "9", "30", "20", "");                    
                        ImgPhoto.ImageUrl = str;                
                    }
                }
                else
                {
                    objProfessional = new professionalDto();
                }

                Session["objProfessional"] = objProfessional;

               
                // Informacion del usuario
                OperationResult objOperationResult = new OperationResult();
                systemuserDto objSystemUser = _objSecurityBL.GetSystemUser(ref objOperationResult, systemUserId);
                
                Session["objSystemUser"] = objSystemUser;

                txtUserName.Text = objSystemUser.v_UserName;
                txtPassword1.Text = objSystemUser.v_Password;
                txtPassword2.Text = objSystemUser.v_Password;
                ddlRolVenta.SelectedValue = objSystemUser.i_RolVentaId.ToString();
            }
        }     

        protected void winEdit_Close(object sender, WindowCloseEventArgs e)
        {

        }

        protected void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetLenght(ddlDocType.SelectedValue);
        }

        private void SetLenght(string SelectedValue)
        {
            if (SelectedValue == "-1") return;

            var docType = Session["objDocType"] as List<DataForTreeView>;
            // Buscar la longitud adecuada en funcion al tipo de documento seleccionado
            var searchResult = docType.Single(p => p.Id == Convert.ToInt32(SelectedValue));
            ViewState["lenght"] = Convert.ToInt32(searchResult.Description2);
            //txtDocNumber.Text = string.Empty;
            //txtDocNumber.Focus();
           
        }

        protected void filePhoto_FileSelected(object sender, EventArgs e)
        {
            try
            {
                if (ImgPhoto.ImageUrl != string.Empty)
                {
                    ImgPhoto.ImageUrl = null;
                    filePhoto.Dispose();
                }

                string fileName = filePhoto.ShortFileName;
                if (fileName != "")
                {
                    string Ext = fileName.Substring(fileName.IndexOf('.') + 0).ToUpper();

                    if (Ext == ".JPG" || Ext == ".GIF" || Ext == ".JPEG" || Ext == ".PNG" || Ext == "")
                    {
                        fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                        fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;

                        System.IO.FileInfo info = new System.IO.FileInfo(fileName);

                        if (filePhoto.HasFile)
                        {
                            filePhoto.SaveAs(Server.MapPath("~/upload/" + fileName));

                            System.Drawing.Bitmap bmp1 = new System.Drawing.Bitmap(Server.MapPath("~/upload/" + fileName));
                            
                            Decimal Hv = 150;
                            Decimal Wv = 180;

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

                            ImgPhoto.ImageHeight = (int)(k * Hi);
                            ImgPhoto.ImageWidth = (int)(k * Wi);

                        }

                        ImgPhoto.ImageUrl = ResolveUrl("~/upload/" + fileName);

                        //Eliminar la imagen de la capeta Upload
                        //if (System.IO.File.Exists(Server.MapPath("~/upload/" + fileName)))
                        //{
                        //    System.IO.File.Delete(Server.MapPath("~/upload/" + fileName));
                        //}

                    }
                    else
                    {
                        Alert.ShowInTop("Solo se puede subir imágenes con esta extensión : .JPG , .GIF , .JPEG , .PNG");
                        return;
                    }
                }
                else
                {
                    Alert.ShowInTop("Debe seleccionar una imagen");
                }
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }
        }

        private string byteArrayToImage(byte[] byteArrayIn)
        {
            System.Drawing.Image newImage = null;
            Decimal Hv, Wv, k, Hi, Wi, Dh, Dw;
            string _pathUrl = string.Empty;

            try
            {
                var filePaths = Directory.GetFiles(Server.MapPath("~/Temp"));
                foreach (string filePath in filePaths)
                {
                    File.Delete(filePath);
                }

                string strFileName = Server.MapPath("~/Temp/") + "UploadedImage.jpg";

                if (byteArrayIn != null)
                {
                    using (MemoryStream stream = new MemoryStream(byteArrayIn))
                    {
                        newImage = System.Drawing.Image.FromStream(stream);
                        Hv = 150;
                        Wv = 180;

                        k = -1;

                        Hi = newImage.Height;
                        Wi = newImage.Width;

                        Dh = -1;
                        Dw = -1;

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
                      
                        newImage.Save(strFileName);

                    }

                    ImgPhoto.ImageHeight = (int)(k * Hi);
                    ImgPhoto.ImageWidth = (int)(k * Wi);

                    _pathUrl = ResolveUrl("~/Temp/" + "UploadedImage.jpg");

                }
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }

            return _pathUrl;

        }    

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ImgPhoto.ImageUrl = null;                  
            filePhoto.Reset();
            ViewState["isDeleted"] = true;
        }

        #region Util

        private string getParameterRequest(string pstrType, string pstrWidht, string pstrMessage, string pstrFontFamily, string pstrFontColor, string pstrFontSize, string pstrPosX, string pstrPosY, string pstrPkImagen)
        {
            var query = new StringBuilder();
            if (pstrType.Length > 0)
            {
                query.Append("type=");
                query.Append(HttpUtility.UrlEncode(pstrType));
                query.Append("&");
            }

            if (pstrPkImagen.Length > 0)
            {
                query.Append("pkImagen=");
                query.Append(HttpUtility.UrlEncode(pstrPkImagen));
                query.Append("&");
            }
            //'Ancho
            if (pstrWidht.Length > 0)
            {
                query.Append("ancho=");
                query.Append(pstrWidht);
                query.Append("&");
            }
            //'Texto/Mensaje
            if (pstrMessage.Length > 0)
            {
                query.Append("t=");
                query.Append(HttpUtility.UrlEncode(pstrMessage));
                query.Append("&");
            }
            //'Fuente/Tipografia
            if (pstrFontFamily.Length > 0)
            {
                query.Append("ff=");
                query.Append(HttpUtility.UrlEncode(pstrFontFamily));
                query.Append("&");
            }
            if (pstrFontColor.Length > 0)
            {
                query.Append("fc=");
                query.Append(HttpUtility.UrlEncode(pstrFontColor));
                query.Append("&");
            }
            if (pstrFontSize.Length > 0)
            {
                query.Append("fs=");
                query.Append(HttpUtility.UrlEncode(pstrFontSize));
                query.Append("&");
            }
            if (pstrPosX.Length > 0)
            {
                query.Append("fx=");
                query.Append(HttpUtility.UrlEncode(pstrPosX));
                query.Append("&");
            }
            if (pstrPosY.Length > 0)
            {
                query.Append("fy=");
                query.Append(HttpUtility.UrlEncode(pstrPosY));
                query.Append("&");
            }

            return query.ToString();
        }      



        #endregion

    }
}