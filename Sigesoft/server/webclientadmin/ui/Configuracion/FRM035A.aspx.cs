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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.IO;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.UI.Configuracion
{
    public partial class FRM035A : System.Web.UI.Page
    {
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        ProtocolBL _oProtocolBL = new ProtocolBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnCIIU.OnClientClick = winEdit.GetSaveStateReference(hfRefresh.ClientID) + winEdit.GetShowReference("FRM035A_1.aspx?Mode=New");
                if (Request.QueryString["v_OrganizationId"] != null)
                    Session["OrganizationId"] = Request.QueryString["v_OrganizationId"].ToString();
                LoadCombos();
                LoadData();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
                ViewState["isDeleted"] = false;
            }
        }

        public string Mode
        {
            get
            {
                if (Request.QueryString["Mode"] != null)
                {
                    string _mode = Request.QueryString["Mode"].ToString();
                    if (!string.IsNullOrEmpty(_mode))
                    {
                        return _mode;
                    }
                }

                return string.Empty;
            }
        }

        private void LoadData()
        {
            if (Mode == "New")
            {
                // Additional logic here.
                ddlTipoEmpresa.SelectedIndex = 1;
            }
            else if (Mode == "Edit")
            {
                OperationResult objCommonOperationResultedit = new OperationResult();
                organizationDto objorganizationDto = _oProtocolBL.GetOrganization(ref objCommonOperationResultedit, Session["OrganizationId"].ToString());
                Session["objEntity"] = objorganizationDto;
                ddlTipoEmpresa.SelectedValue = objorganizationDto.i_OrganizationTypeId.ToString();

                txtCIIU.Text = objorganizationDto.v_SectorCodigo;
                txtSector.Text = objorganizationDto.v_SectorName;
                txtRUC.Text = objorganizationDto.v_IdentificationNumber;
                txtRazonSocial.Text = objorganizationDto.v_Name;
                txtContacto.Text = objorganizationDto.v_Contacto;
                txtEmail.Text = objorganizationDto.v_EmailContacto;
                txtDireccion.Text = objorganizationDto.v_Address;
                txtTelefono.Text = objorganizationDto.v_PhoneNumber;
                ImgPhoto.ImageUrl = null;
                if (objorganizationDto.b_Image != null)
                {
                    string pathImage = byteArrayToImage(objorganizationDto.b_Image);
                    string str = @"~\Utils\GetImageText.ashx?" + getParameterRequest("imgDeliverValid", "180", "", "Arial Black", "Black", "9", "30", "20", "");
                    ImgPhoto.ImageUrl = str;
                }
            }
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
        
        private void LoadCombos()
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlTipoEmpresa, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 103), DropDownListAction.All);
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
            byte[] byteArrayPhoto = null;
            string Mode = Request.QueryString["Mode"].ToString();
            OperationResult objOperationResult = new OperationResult();

            if (Mode == "New")
            {
                // Create the entity
                organizationDto objEntity = new organizationDto();

                // Populate the entity

                objEntity.i_OrganizationTypeId = string.IsNullOrEmpty(ddlTipoEmpresa.SelectedValue) ? (Int32?)null : Int32.Parse(ddlTipoEmpresa.SelectedValue);
                objEntity.i_SectorTypeId = -1;
                objEntity.v_SectorCodigo =txtCIIU.Text.Trim();
                objEntity.v_SectorName = txtSector.Text;
                objEntity.v_IdentificationNumber = txtRUC.Text.Trim().ToUpper();
                objEntity.v_Name = txtRazonSocial.Text.Trim().ToUpper();
                objEntity.v_Contacto = txtContacto.Text.Trim().ToUpper();
                objEntity.v_EmailContacto = txtEmail.Text.Trim().ToUpper();
                objEntity.v_Address = txtDireccion.Text.Trim().ToUpper();
                objEntity.v_PhoneNumber = txtTelefono.Text.Trim().ToUpper();
                if (filePhoto.HasFile) // Si hay una imagen cargada lista para ser serializada sino se graba x defecto null 
                {
                    byteArrayPhoto = FileUploadToByteArray();
                    objEntity.b_Image = byteArrayPhoto;
                }

                    // Save the data                  
               var empresaId= _oProtocolBL.AddOrganization(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());

                List<ordenreporteDto> ListaOrdem = new List<ordenreporteDto>();
                ordenreporteDto oordenreporteDto = null;
                var Lista = _oProtocolBL.GetOrdenReportes(ref objOperationResult, "N009-OO000000052");
                foreach (var item in Lista)
                {
                    oordenreporteDto = new ordenreporteDto();
                    oordenreporteDto.i_Orden = item.i_Orden;
                    oordenreporteDto.v_OrganizationId = empresaId;
                    oordenreporteDto.v_NombreReporte = item.v_NombreReporte;
                    oordenreporteDto.v_ComponenteId = item.v_NombreCrystal;
                    oordenreporteDto.v_NombreCrystal = item.v_NombreCrystal;
                    oordenreporteDto.i_NombreCrystalId = item.i_NombreCrystalId;
                    ListaOrdem.Add(oordenreporteDto);
                }
                _oProtocolBL.AddOrdenReportes(ref objOperationResult, ListaOrdem, ((ClientSession)Session["objClientSession"]).GetAsList());


            }
            else if (Mode == "Edit")
            {
                // Create the entity
                organizationDto objEntity = new organizationDto();

                // Populate the entity
                objEntity = Session["objEntity"] as organizationDto;

                objEntity.v_OrganizationId = Session["OrganizationId"].ToString();
                objEntity.i_OrganizationTypeId = string.IsNullOrEmpty(ddlTipoEmpresa.SelectedValue) ? (Int32?)null : Int32.Parse(ddlTipoEmpresa.SelectedValue);
                objEntity.i_SectorTypeId = -1;
                objEntity.v_SectorCodigo = txtCIIU.Text.Trim();
                objEntity.v_SectorName = txtSector.Text;
                objEntity.v_IdentificationNumber = txtRUC.Text.Trim().ToUpper();
                objEntity.v_Name = txtRazonSocial.Text.Trim().ToUpper();
                objEntity.v_Contacto = txtContacto.Text.Trim().ToUpper();
                objEntity.v_EmailContacto = txtEmail.Text.Trim().ToUpper();
                objEntity.v_Address = txtDireccion.Text.Trim().ToUpper();
                objEntity.v_PhoneNumber = txtTelefono.Text.Trim().ToUpper();


                if (filePhoto.HasFile) // chekar si el control Upload tiene una imagen cargada para serializar
                {
                    var byteArrayPhoto1 = FileUploadToByteArray();
                    objEntity.b_Image = byteArrayPhoto1;
                }
                else
                {
                    bool isDeleted = Convert.ToBoolean(ViewState["isDeleted"]);

                    if (isDeleted)
                    {
                        objEntity.b_Image = null;
                    }
                }

                // Save the data                  
                _oProtocolBL.UpdateOrganization(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());

                //Eliminar Antiguos Registros
                _oProtocolBL.DeleteOrdenReportes(ref objOperationResult, Session["OrganizationId"].ToString());

                List<ordenreporteDto> ListaOrdem = new List<ordenreporteDto>();
                ordenreporteDto oordenreporteDto = null;
                var Lista = _oProtocolBL.GetOrdenReportes(ref objOperationResult, "N009-OO000000052");
                foreach (var item in Lista)
                {
                    oordenreporteDto = new ordenreporteDto();
                    oordenreporteDto.i_Orden = item.i_Orden;
                    oordenreporteDto.v_OrganizationId = Session["OrganizationId"].ToString();
                    oordenreporteDto.v_NombreReporte = item.v_NombreReporte;
                    oordenreporteDto.v_ComponenteId = item.v_NombreCrystal;
                    oordenreporteDto.v_NombreCrystal = item.v_NombreCrystal;
                    oordenreporteDto.i_NombreCrystalId = item.i_NombreCrystalId;
                    ListaOrdem.Add(oordenreporteDto);
                }
                _oProtocolBL.AddOrdenReportes(ref objOperationResult, ListaOrdem, ((ClientSession)Session["objClientSession"]).GetAsList());



            }

            if (objOperationResult.Success == 1)  // Operación sin error
            {
                // Cerrar página actual y hacer postback en el padre para actualizar
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else  // Operación con error
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                // Se queda en el formulario.
            }
                      
        }

        protected void winEdit_Close(object sender, WindowCloseEventArgs e)
        {
            txtCIIU.Text = Session["v_CIIUId"].ToString();
            txtSector.Text = Session["SectoName"].ToString();
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

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ImgPhoto.ImageUrl = null;
            filePhoto.Reset();
            ViewState["isDeleted"] = true;
        }
    }
}