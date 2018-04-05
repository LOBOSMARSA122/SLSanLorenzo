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

namespace Sigesoft.Server.WebClientAdmin.UI
{
    public partial class FRM007A : System.Web.UI.Page
    {
        ApplicationHierarchyBL _objProxySecurity = new ApplicationHierarchyBL();
        SystemParameterBL _objProxyCommon = new SystemParameterBL();

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
            OperationResult objOperationResultCommon = new OperationResult();
            //Llenado de combos
           
            Utils.LoadDropDownList(ddlApplicationHierarchyTypeId, "Value1", "Id", _objProxyCommon.GetSystemParameterForCombo(ref objOperationResultCommon, 106), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlScopeId, "Value1", "Id", _objProxyCommon.GetSystemParameterForCombo(ref objOperationResultCommon, 104), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlTypeFormId, "Value1", "Id", _objProxyCommon.GetSystemParameterForCombo(ref objOperationResultCommon, 151), DropDownListAction.Select);
            Utils.LoadDropDownList(ddlBusinessRule, "Value1", "Id", _objProxyCommon.GetSystemParameterForCombo(ref objOperationResultCommon, 108), DropDownListAction.Select);

            //Llenar combo Parameter Tree
            ddlParentId.DataTextField = "Description";
            ddlParentId.DataValueField = "Id";
            ddlParentId.DataSimulateTreeLevelField = "Level";
            ddlParentId.DataEnableSelectField = "EnabledSelect";
            List<DtvAppHierarchy> t = _objProxySecurity.GetApplicationHierarchyForCombo(ref objOperationResult).ToList();
            ddlParentId.DataSource = t;
            ddlParentId.DataBind();
            this.ddlParentId.Items.Insert(0, new FineUI.ListItem("-- Seleccione --", "-1"));

            string Mode = Request.QueryString["Mode"].ToString();
            int ApplicationHierarchyId = -1;

            if (Request.QueryString["i_ApplicationHierarchyId"] != null)
                ApplicationHierarchyId = int.Parse(Request.QueryString["i_ApplicationHierarchyId"].ToString());

            if (Mode == "New")
            {
                // Additional logic here.
              
            }
            else if (Mode == "Edit")
            {
                // Get the Entity Data
                applicationhierarchyDto objEntity = _objProxySecurity.GetApplicationHierarchy(ref objOperationResult, ApplicationHierarchyId);

                // Save the entity on the session
                Session["objEntity"] = objEntity;

                // Show the data on the form
                ddlTypeFormId.SelectedValue = objEntity.i_TypeFormId.ToString();
                ddlApplicationHierarchyTypeId.SelectedValue = objEntity.i_ApplicationHierarchyTypeId.ToString();
                txtDescription.Text = objEntity.v_Description;
                txtForm.Text = objEntity.v_Form;
                txtCode.Text = objEntity.v_Code;
                ddlParentId.SelectedValue = objEntity.i_ParentId.ToString();
                ddlScopeId.SelectedValue = objEntity.i_ScopeId.ToString();
            }           
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString["Mode"].ToString();
            OperationResult objOperationResult = new OperationResult();

            if (Mode == "New")
            {
                // Create the entity
                applicationhierarchyDto objEntity = new applicationhierarchyDto();

                // Populate the entity
                objEntity.i_ApplicationHierarchyTypeId =int.Parse(ddlApplicationHierarchyTypeId.SelectedValue);
                objEntity.i_TypeFormId = int.Parse(ddlTypeFormId.SelectedValue);
                objEntity.v_Description = txtDescription.Text ;
                objEntity.v_Form = txtForm.Text;
                objEntity.v_Code =txtCode.Text;
                objEntity.i_ParentId = int.Parse(ddlParentId.SelectedValue) ;
                objEntity.i_ScopeId = int.Parse(ddlScopeId.SelectedValue); 
                // Save the data                  
                _objProxySecurity.AddApplicationHierarchy(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());
              
            }
            else if (Mode == "Edit")
            {                
                // Get the entity from the session
                applicationhierarchyDto objEntity = (applicationhierarchyDto)Session["objEntity"];

                // Populate the entity
                objEntity.i_ApplicationHierarchyTypeId = int.Parse(ddlApplicationHierarchyTypeId.SelectedValue);
                objEntity.i_TypeFormId = int.Parse(ddlTypeFormId.SelectedValue);
                objEntity.v_Description = txtDescription.Text;
                objEntity.v_Form = txtForm.Text;
                objEntity.v_Code = txtCode.Text;
                objEntity.i_ParentId = int.Parse(ddlParentId.SelectedValue.ToString());
                objEntity.i_ScopeId = int.Parse(ddlScopeId.SelectedValue); // Save the data
                _objProxySecurity.UpdateApplicationHierarchy(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());
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