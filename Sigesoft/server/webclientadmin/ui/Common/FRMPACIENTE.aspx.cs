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


namespace Sigesoft.Server.WebClientAdmin.UI.Common
{
    public partial class FRMPACIENTE : System.Web.UI.Page
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

            }
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

                // Graba persona      
                OperationResult objOperationResult1 = new OperationResult();
                personId = _objPacientBL.AddPerson(ref objOperationResult1,
                                                          objPerson,
                                                          null,
                                                          null,
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

                if (Request.QueryString["v_personId"] != null)
                    personId = Request.QueryString["v_personId"].ToString();
              
                personDto _objPerson = Session["objEntity"] as personDto;
           
                #region Validate Document Number Lenght
                // Validar la longitud de los numeros de documentos
                if (!IsValidDocumentNumberLenght())
                {
                    return;
                }
                #endregion

                bool isChangeUserName = false;
                bool isChangeDocNumber = false;

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

                // Actualiza persona
                OperationResult objOperationResult1 = new OperationResult();
                _objPacientBL.UpdatePerson(ref objOperationResult1,
                                                isChangeDocNumber,
                                                _objPerson,
                                                null,
                                                isChangeUserName,
                                                null,
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


        protected void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void LoadComboBox()
        {
            OperationResult objOperationResult = new OperationResult();

            var _DocType = _objDataHierarchyBL.GetDataHierarchyForCombo(ref objOperationResult, 106);
            Utils.LoadDropDownList(ddlDocType, "Description", "Id", _DocType, DropDownListAction.Select);
            Utils.LoadDropDownList(ddlSexType, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 100), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlMaritalStatus, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 101), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlLevelOfId, "Description", "Id", _objDataHierarchyBL.GetDataHierarchyForCombo(ref objOperationResult, 108), DropDownListAction.Select);
             Session["objDocType"] = _DocType;


        }

        private void LoadData()
        {
            string Mode = Request.QueryString["Mode"].ToString();
           
            string personId = string.Empty;

            LoadComboBox();

            if (Mode == "New")
            {
                // Additional logic here.

                txtFirstName.Focus(true);

            }
            else if (Mode == "Edit")
            {
                if (Request.QueryString["v_PersonId"] != null)
                    personId = Request.QueryString["v_PersonId"].ToString();

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

              
             
                    ImgPhoto.ImageUrl = null;
                    if (personDTO.b_PersonImage != null)
                    {
                        string pathImage = byteArrayToImage(personDTO.b_PersonImage);
                        string str = @"~\Utils\GetImageText.ashx?" + getParameterRequest("imgDeliverValid", "180", "", "Arial Black", "Black", "9", "30", "20", "");
                        ImgPhoto.ImageUrl = str;
                    }
               
            }
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

    }
}