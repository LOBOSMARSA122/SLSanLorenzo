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


namespace Sigesoft.Server.WebClientAdmin.UI.Sync
{
    public partial class FRM012A : System.Web.UI.Page
    {
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        SyncBL _objSyncBL = new SyncBL();

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

            //Llenado de combos
            Utils.LoadDropDownList(ddlSoftwareComponentId, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 144), DropDownListAction.Select);
           
         
            string Mode = Request.QueryString["Mode"].ToString();

            int SoftwareComponentId = 0;
            string SoftwareComponentVersion = "";

            if (Request.QueryString["i_SoftwareComponentId"] != null)
                SoftwareComponentId = int.Parse(Request.QueryString["i_SoftwareComponentId"].ToString());
            if (Request.QueryString["v_SoftwareComponentVersion"] != null)
                SoftwareComponentVersion = Request.QueryString["v_SoftwareComponentVersion"].ToString();

            if (Mode == "New")
            {
            }
            else
            {
                ddlSoftwareComponentId.Enabled = false;
                txtSoftwareComponentVersion.Enabled = false;
                // Get the Entity Data
                softwarecomponentreleaseDto objEntity = _objSyncBL.GetSoftwareComponentRelease(ref objOperationResult, SoftwareComponentId, SoftwareComponentVersion);

                // Save the entity on the session
                Session["objEntity"] = objEntity;

                // Show the data on the form
                ddlSoftwareComponentId.SelectedValue = objEntity.i_SoftwareComponentId.ToString();
                txtSoftwareComponentVersion.Text = objEntity.v_SoftwareComponentVersion;
                txtDeploymentFileId.Text = objEntity.i_DeploymentFileId.ToString();
                dpReleaseDate.SelectedDate = objEntity.d_ReleaseDate.Value;
                txtDatabaseVersionRequired.Text = objEntity.v_DatabaseVersionRequired;
                txtReleaseNotes.Text = objEntity.v_ReleaseNotes;
                txtAdditionalInformation1.Text = objEntity.v_AdditionalInformation1;
                txtAdditionalInformation2.Text = objEntity.v_AdditionalInformation2;
                txtIsPublished.Text = objEntity.i_IsPublished.ToString();
                txtIsLastVersion.Text = objEntity.i_IsLastVersion.ToString();

                

            }
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString["Mode"].ToString();
            OperationResult objOperationResult = new OperationResult();

            if (Mode == "New")
            {
                // Create the entity
                softwarecomponentreleaseDto objEntity = new softwarecomponentreleaseDto();


                objEntity.i_SoftwareComponentId = int.Parse(ddlSoftwareComponentId.SelectedValue.ToString());
                objEntity.v_SoftwareComponentVersion = txtSoftwareComponentVersion.Text.ToString();
                objEntity.i_DeploymentFileId = int.Parse(txtDeploymentFileId.Text.ToString());
                objEntity.d_ReleaseDate = dpReleaseDate.SelectedDate;
                objEntity.v_DatabaseVersionRequired = txtDatabaseVersionRequired.Text;
                objEntity.v_ReleaseNotes = txtReleaseNotes.Text;
                objEntity.v_AdditionalInformation1 = txtAdditionalInformation1.Text;
                objEntity.v_AdditionalInformation2 = txtAdditionalInformation2.Text;
                objEntity.i_IsPublished = int.Parse(txtIsPublished.Text.ToString());
                objEntity.i_IsLastVersion = int.Parse(txtIsLastVersion.Text.ToString());
               

                    // Save the data                  
                _objSyncBL.AddSoftwareComponentRelease(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());

            }
            else if (Mode == "Edit")
            {

                // Get the entity from the session
                softwarecomponentreleaseDto objEntity = (softwarecomponentreleaseDto)Session["objEntity"];

                // Populate the entity
                objEntity.i_SoftwareComponentId = int.Parse(ddlSoftwareComponentId.SelectedValue.ToString());
                objEntity.v_SoftwareComponentVersion = txtSoftwareComponentVersion.Text.ToString();
                objEntity.i_DeploymentFileId = int.Parse(txtDeploymentFileId.Text.ToString());
                objEntity.d_ReleaseDate = dpReleaseDate.SelectedDate;
                objEntity.v_DatabaseVersionRequired = txtDatabaseVersionRequired.Text;
                objEntity.v_ReleaseNotes = txtReleaseNotes.Text;
                objEntity.v_AdditionalInformation1 = txtAdditionalInformation1.Text;
                objEntity.v_AdditionalInformation2 = txtAdditionalInformation2.Text;
                objEntity.i_IsPublished = int.Parse(txtIsPublished.Text.ToString());
                objEntity.i_IsLastVersion = int.Parse(txtIsLastVersion.Text.ToString());
                // Save the data
                _objSyncBL.UpdateSoftwareComponentRelease(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());
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
}