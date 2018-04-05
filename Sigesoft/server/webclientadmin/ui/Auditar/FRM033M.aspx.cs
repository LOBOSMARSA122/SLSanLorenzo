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
using System.Drawing;

namespace Sigesoft.Server.WebClientAdmin.UI.Auditar
{
    public partial class FRM033M : System.Web.UI.Page
    {
        //private Sigesoft.Node.WinClient.BLL.MultimediaFileBL _multimediaFileBL = new Sigesoft.Node.WinClient.BLL.MultimediaFileBL();
        List<FileInfoDto> objMainEntityDetail = null;
        ServiceBL oServiceBL = new ServiceBL();
         OperationResult objOperationResult = new OperationResult();
         private byte[] _file = null;
         //System.Web.UI.WebControls.Image pbFile = new System.Web.UI.WebControls.Image();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["v_ServiceId"] != null)
                    Session["ServiceId"] = Request.QueryString["v_ServiceId"].ToString();
                if (Request.QueryString["v_IdTrabajador"] != null)
                    Session["IdTrabajador"] = Request.QueryString["v_IdTrabajador"].ToString();

                if (Request.QueryString["v_ProtocolId"] != null)
                    Session["ProtocolId"] = Request.QueryString["v_ProtocolId"].ToString();
                
                LoadData();
                LlenarCombo();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();

            }
        }

        private void LlenarCombo()
        {
            //Llenar combo consultorio 
            int Nodo = 9;
            int RolId = int.Parse(((ClientSession)Session["objClientSession"]).i_RoleId.ToString());

            // Obtener permisos de cada examen de un rol especifico
            var componentProfile = oServiceBL.GetRoleNodeComponentProfileByRoleNodeId(Nodo, RolId);

            var _componentListTemp = oServiceBL.GetAllComponents(ref objOperationResult);

            Session["componentListTemp"] = _componentListTemp;
            var xxx = _componentListTemp.FindAll(p => p.Value4 != -1);

            List<KeyValueDTO> groupComponentList = xxx.GroupBy(x => x.Value4).Select(group => group.First()).ToList();

            groupComponentList.AddRange(_componentListTemp.ToList().FindAll(p => p.Value4 == -1));
            // Remover los componentes que no estan asignados al rol del usuario
            var results = groupComponentList.FindAll(f => componentProfile.Any(t => t.v_ComponentId == f.Value2));
            Utils.LoadDropDownList(ddlConsultorio, "Value1", "Value4", results, DropDownListAction.Select);
        }


         protected void ddlConsultorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlConsultorio.SelectedValue == "-1")
            {
                return;
            }
            //Obtener Componentes del consultorio en función de su protoclo
            var CategoriaId = ddlConsultorio.SelectedValue;

            //Obtener los componetes de ese servicio en función de la categoría seleccionada
            
            var Examenes = oServiceBL.DevolverExamenesPorCategoria(Session["ServiceId"].ToString(), int.Parse(CategoriaId));

            Utils.LoadDropDownList(ddlExamen, "Value1", "Id", Examenes, DropDownListAction.Select);
            
        }

        private void LoadData()
        {

            OperationResult objOperationResult = new OperationResult();


            objMainEntityDetail = new List<FileInfoDto>();
            Session["objMainEntityDetail"] = objMainEntityDetail;
            //Llenado de combos
         
            //string Mode = Request.QueryString["Mode"].ToString();

            //int DeploymentFileId = 0;


            //if (Request.QueryString["i_DeploymentFileId"] != null)
            //    DeploymentFileId = int.Parse(Request.QueryString["i_DeploymentFileId"].ToString());

            //if (Mode == "New")
            //{
            //}
            //else
            //{
             
            //    //// Get the Entity Data
            //    //deploymentfileDto objEntity = _objSyncBL.GetDeploymentFile(ref objOperationResult, DeploymentFileId);

            //    //// Save the entity on the session
            //    //Session["objEntity"] = objEntity;

            //    //// Show the data on the form
            //    //txtDeploymentFileId.Text = objEntity.i_DeploymentFileId.ToString();
            //    //txtDeploymentFileId.Enabled = false;
            //    //ddlSoftwareComponentId.SelectedValue = objEntity.i_SoftwareComponentId.ToString();
            //    //ddlSoftwareComponentId.Enabled = false;
            //    //txtFileName.Text = objEntity.v_FileName;
            //    //txtFileName.Enabled = false;
            //    //txtTargetSoftwareComponentVersion.Text = objEntity.v_TargetSoftwareComponentVersion;
            //    //txtTargetSoftwareComponentVersion.Enabled = false;
            //    //txtDescription.Text = objEntity.v_Description;
            //    //txtDescription.Enabled = false;
            //    //txtPackageFiles.Text = objEntity.v_PackageFiles;
            //    //txtPackageFiles.Enabled = false;
            //    //txtPackageSizeKb.Text = objEntity.r_PackageSizeKb.ToString();
            //    //txtPackageSizeKb.Enabled = false;
            //    filePhoto.Enabled = false;
            //    grdData.Enabled = false;
            //    btnSaveRefresh.Visible = false;
            //}
        }

     
        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            List<FileInfoDto> objDetailData = Session["objMainEntityDetail"] as List<FileInfoDto>;
            FileInfoDto fileInfo = null;
            foreach (var item in objDetailData)
            {

                //Obtener CategoriaId
                //var oKeyValues = (KeyValueDTO)ddlComponentId.SelectedItem;
                var CategoriaId = int.Parse(ddlConsultorio.SelectedValue.ToString());

                //Obtener lista de componentes de un protocolo por su categoria
                ProtocolBL oProtocolBL = new ProtocolBL();
                var ListaComponentesCategoria = oProtocolBL.GetProtocolComponents(ref objOperationResult, item.ProtocolId).FindAll(p => p.i_CategoryId == CategoriaId);

                var OrdenDescListaComponentesCategoria = ListaComponentesCategoria.OrderBy(o => o.v_ComponentId).ToList();



                var oserviceComponent = oServiceBL.GetServiceComponentByServiceIdAndComponentId(item.ServiceId, OrdenDescListaComponentesCategoria[0].v_ComponentId);
                if (oserviceComponent != null)
                {

                    string serviceComponentId = oserviceComponent.v_ServiceComponentId;


                    LoadFileNoLock(item.RutaLarga);   

                    fileInfo = new FileInfoDto();

                    fileInfo.Id = null;
                    fileInfo.PersonId = item.PersonId;
                    fileInfo.ServiceComponentId = serviceComponentId;
                    fileInfo.FileName = item.RutaCorta;
                    fileInfo.Description = "";
                    fileInfo.ByteArrayFile = _file;
                    //fileInfo.ThumbnailFile = Common.Utils.imageToByteArray1(pbFile.Image);
                    fileInfo.Action = (int)ActionForm.Add;

                    // Grabar
                    oServiceBL.AddMultimediaFileComponent(ref objOperationResult, fileInfo, ((ClientSession)Session["objClientSession"]).GetAsList());
                }

                //Analizar el resultado de la operación
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

        }

        private void LoadFileNoLock(string pfilePath)
        {


            string fileExt = pfilePath.Substring(pfilePath.LastIndexOf('.') + 1).ToUpper();

            FileExtension fe;
            bool enumParseResult = Enum.TryParse(fileExt, true, out fe);

            if (enumParseResult)
            {
                SetImagenInfo(fe, pfilePath);
            }
            else
            {
                //MessageBox.Show("El archivo que está intentando subir no es soportado por el sistema.\nPor favor seleccione otro tipo de archivo", "ADVERTENCIA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void SetImagenInfo(FileExtension fe, string pfilePath)
        {
            Bitmap imageOriginal = null;

            try
            {
                switch (fe)
                {
                    #region Establecer -> Imagen / extension / color de borde

                    case FileExtension.JPG:
                        imageOriginal = new Bitmap(pfilePath);
                        //pbFile.Image = Common.Utils.ThumbnailImage(imageOriginal, 180);
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.Gray;
                        _file = Utils.imageToByteArray1(imageOriginal);
                        break;
                    case FileExtension.GIF:
                        imageOriginal = new Bitmap(pfilePath);
                        //pbFile.Image = Common.Utils.ThumbnailImage(imageOriginal, 180);
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.Gray;
                        _file = Utils.imageToByteArray1(imageOriginal);
                        break;
                    case FileExtension.JPEG:
                        imageOriginal = new Bitmap(pfilePath);
                        //pbFile.Image = Common.Utils.ThumbnailImage(imageOriginal, 180);
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.Gray;
                        _file = Utils.imageToByteArray1(imageOriginal);
                        break;
                    case FileExtension.PNG:
                        imageOriginal = new Bitmap(pfilePath);
                        //pbFile.Image = Common.Utils.ThumbnailImage(imageOriginal, 180);
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.Gray;
                        _file = Utils.imageToByteArray1(imageOriginal);
                        break;
                    case FileExtension.BMP:
                        imageOriginal = new Bitmap(pfilePath);
                        //pbFile.Image = Common.Utils.ThumbnailImage(imageOriginal, 180);
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.Gray;
                        _file = Utils.imageToByteArray1(imageOriginal);
                        break;
                    case FileExtension.XLS:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "Excel.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.Green;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.XLSX:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "Excel.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.Green;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.DOC:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "word.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.DodgerBlue;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.DOCX:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "word.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.DodgerBlue;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.PDF:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "Pdf.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.DarkRed;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.PPT:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "ppt.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.DarkOrange;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.PPTX:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "ppt.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.DarkOrange;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.TXT:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "txt.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.Gray;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.AVI:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "avi.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.Black;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.MPG:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "mpg.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.Black;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.MPEG:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "mpeg.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.Black;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.MOV:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "mov.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.Black;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.WMV:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "wmv.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.Black;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.FLV:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "flv.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.Black;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.MP3:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "mp3.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.MediumPurple;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.MP4:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "mp4.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.MediumPurple;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.WMA:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "wma.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.MediumPurple;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    case FileExtension.WAV:
                        //pbFile.Image = Image.FromFile(Constants.IMAGE_PREVIEW_DIRECTORY + "wav.png");
                        //lblExt.Text = fe.ToString();
                        //pnlPreviewTitle.BackColor = Color.MediumPurple;
                        _file = File.ReadAllBytes(pfilePath);
                        break;
                    default:
                        break;

                    #endregion
                }

                if (imageOriginal != null) imageOriginal.Dispose();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = filePhoto.ShortFileName;
                if (fileName != "")
                {
                    fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");

                    if (filePhoto.HasFile)
                    {
                        filePhoto.SaveAs(Server.MapPath("~/Uploads/" + fileName));

                        List<FileInfoDto> objDetailData = Session["objMainEntityDetail"] as List<FileInfoDto>;
                        FileInfoDto objDetailItem = new FileInfoDto();

                        objDetailItem.ServiceId = Session["ServiceId"].ToString();
                        objDetailItem.PersonId = Session["IdTrabajador"].ToString();

                        objDetailItem.RutaCorta = Path.GetFileName(fileName);
                        objDetailItem.RutaLarga = Server.MapPath("~/Uploads/" + fileName);
                        objDetailItem.FileName = fileName;
                        objDetailItem.PersonId = Session["IdTrabajador"].ToString();
                        objDetailItem.ProtocolId = Session["ProtocolId"].ToString();

                        objDetailData.Add(objDetailItem);

                        Session["objMainEntityDetail"] = objDetailData;
                        BindGridDetail();
                    }
                }

            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message);
            }

        }

        private void BindGridDetail()
        {
            List<FileInfoDto> objDetailData = Session["objMainEntityDetail"] as List<FileInfoDto>;
            if (objDetailData != null)
            {
                grdData.DataSource = objDetailData;
                grdData.DataBind();
            }
        }

        protected void grdData_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAction")
            {
                DeleteItem();
            }
        }

        private void DeleteItem()
        {
            List<FileInfoDto> objDetailData = Session["objMainEntityDetail"] as List<FileInfoDto>;
            if (objDetailData != null)
            {
                File.Delete(objDetailData[grdData.SelectedRowIndex].FileName);
                objDetailData.RemoveAt(grdData.SelectedRowIndex);

                grdData.DataSource = objDetailData;
                grdData.DataBind();
            }
        }
    }
}