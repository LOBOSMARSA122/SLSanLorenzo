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

namespace Sigesoft.Server.WebClientAdmin.UI.Sync
{
    public partial class FRM029B : System.Web.UI.Page
    {
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        SyncBL _objSyncBL = new SyncBL();
        List<TestList> objMainEntityDetail = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                LoadData();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            }
        }

        private void LoadData()
        {

            OperationResult objOperationResult = new OperationResult();


            objMainEntityDetail = new List<TestList>();
            Session["objMainEntityDetail"] = objMainEntityDetail;
            //Llenado de combos
            Utils.LoadDropDownList(ddlSoftwareComponentId, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 144), DropDownListAction.Select);
            string Mode = Request.QueryString["Mode"].ToString();

            int DeploymentFileId = 0;


            if (Request.QueryString["i_DeploymentFileId"] != null)
                DeploymentFileId = int.Parse(Request.QueryString["i_DeploymentFileId"].ToString());

            if (Mode == "New")
            {
            }
            else
            {
                txtPackageSizeKb.Visible = true;
                txtPackageFiles.Visible = true;
                // Get the Entity Data
                deploymentfileDto objEntity = _objSyncBL.GetDeploymentFile(ref objOperationResult, DeploymentFileId);

                // Save the entity on the session
                Session["objEntity"] = objEntity;

                // Show the data on the form
                txtDeploymentFileId.Text = objEntity.i_DeploymentFileId.ToString();
                txtDeploymentFileId.Enabled = false;
                ddlSoftwareComponentId.SelectedValue = objEntity.i_SoftwareComponentId.ToString();
                ddlSoftwareComponentId.Enabled = false;
                txtFileName.Text = objEntity.v_FileName;
                txtFileName.Enabled = false;
                txtTargetSoftwareComponentVersion.Text = objEntity.v_TargetSoftwareComponentVersion;
                txtTargetSoftwareComponentVersion.Enabled = false;
                txtDescription.Text = objEntity.v_Description;
                txtDescription.Enabled = false;
                txtPackageFiles.Text = objEntity.v_PackageFiles;
                txtPackageFiles.Enabled = false;
                txtPackageSizeKb.Text = objEntity.r_PackageSizeKb.ToString();
                txtPackageSizeKb.Enabled = false;
                filePhoto.Enabled = false;
                grdData.Enabled = false;
                btnSaveRefresh.Visible = false;
            }
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString["Mode"].ToString();
            OperationResult objOperationResult = new OperationResult();
            List<TestList> objDetailData = Session["objMainEntityDetail"] as List<TestList>;

            if (Mode == "New")
            {
                // Create the entity
                deploymentfileDto objEntity = new deploymentfileDto();

                objEntity.i_DeploymentFileId = int.Parse(txtDeploymentFileId.Text.ToString());
                objEntity.i_SoftwareComponentId = int.Parse(ddlSoftwareComponentId.SelectedValue.ToString());
                objEntity.v_FileName = txtFileName.Text.Trim().ToUpper();
                objEntity.i_DeploymentFileId = int.Parse(txtDeploymentFileId.Text.ToString());
                objEntity.v_TargetSoftwareComponentVersion = txtTargetSoftwareComponentVersion.Text.Trim().ToUpper();
                objEntity.v_Description = txtDescription.Text.Trim().ToUpper();

                //Grabar Grilla
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string PackageFile ="";
                List<string> Files = new List<string>();
                foreach (var item in objDetailData)
                {
                    PackageFile = Path.Combine(Sigesoft.Common.Utils.GetApplicationExecutingFolder(), objEntity.v_FileName);                   
                    Files.Add(item.v_Path);
                    objEntity.v_PackageFiles = sb.AppendLine(item.v_FileName + " , ").ToString();
                }
                // Crear el archivo comprimido
                Sigesoft.Common.Utils.CompressFile(PackageFile, Files);
                byte[] PackageFileByteArray = Sigesoft.Common.Utils.GetBytesOfFile(PackageFile);
                objEntity.b_FileData = PackageFileByteArray;
                objEntity.r_PackageSizeKb = PackageFileByteArray.Length;
                // Save the data                  
                _objSyncBL.AddDeploymentFile(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());

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
                                             
                        List<TestList> objDetailData = Session["objMainEntityDetail"] as List<TestList>;
                        TestList objDetailItem = new TestList();

                        objDetailItem.v_FileName = fileName;
                        objDetailItem.v_Path = Server.MapPath("~/Uploads/" + fileName);
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
            List<TestList> objDetailData = Session["objMainEntityDetail"] as List<TestList>;
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
            List<TestList> objDetailData = Session["objMainEntityDetail"] as List<TestList>;
            if (objDetailData != null)
            {
                File.Delete(objDetailData[grdData.SelectedRowIndex].v_Path);
                objDetailData.RemoveAt(grdData.SelectedRowIndex);
                
                grdData.DataSource = objDetailData;
                grdData.DataBind();
            }
        }
    }
}