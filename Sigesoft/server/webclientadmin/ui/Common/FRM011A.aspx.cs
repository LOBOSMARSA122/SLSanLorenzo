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
    public partial class FRM011A : System.Web.UI.Page
    {
        DataHierarchyBL _objBL = new DataHierarchyBL();
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

            string Mode = Request.QueryString["Mode"].ToString();
            int GroupId = -1, ParameterId = -1;

            if (Request.QueryString["i_GroupId"] != null)
                GroupId = int.Parse(Request.QueryString["i_GroupId"].ToString());
            if (Request.QueryString["i_ItemId"] != null)
                ParameterId = int.Parse(Request.QueryString["i_ItemId"].ToString());

            //Llenar combo ItemParameter Tree
            ddlParentItemId.DataTextField = "Description";
            ddlParentItemId.DataValueField = "Id";
            ddlParentItemId.DataSimulateTreeLevelField = "Level";
            ddlParentItemId.DataEnableSelectField = "EnabledSelect";
            List<DataForTreeView> t = _objBL.GetDataHierarchyForCombo(ref objOperationResult, GroupId).ToList();
            ddlParentItemId.DataSource = t;
            ddlParentItemId.DataBind();
            this.ddlParentItemId.Items.Insert(0, new FineUI.ListItem("-- Seleccione --", "-1"));
         
            if (Mode == "New")
            {
                // Additional logic here.
                txtGroupId.Enabled = false;
                txtGroupId.Text = "0";
                ddlParentItemId.Enabled = false;
            }
            else if (Mode == "Edit")
            {
                // Bloquear algunos campos
                txtGroupId.Enabled = false;
                txtParameterId.Enabled = false;
                ddlParentItemId.Enabled = true;

                // Get the Entity Data
                datahierarchyDto objEntity = _objBL.GetDataHierarchy(ref objOperationResult, GroupId, ParameterId);

                // Save the entity on the session
                Session["objEntity"] = objEntity;

                // Show the data on the form
                txtGroupId.Text = objEntity.i_GroupId.ToString();
                txtParameterId.Text = objEntity.i_ItemId.ToString();
                txtDescription.Text = objEntity.v_Value1;
                txtDescription2.Text = objEntity.v_Value2;
                if (objEntity.i_Sort.HasValue) txtUserInterfaceOrder.Text = objEntity.i_Sort.Value.ToString();
                txtField.Text = objEntity.v_Field;
                ddlParentItemId.SelectedValue = objEntity.i_ParentItemId.ToString();

                if (GroupId == 0) ddlParentItemId.Enabled = false;


            }
            if (Mode == "NewChildren")
            {
                txtGroupId.Text = GroupId.ToString();
                txtGroupId.Enabled = false;
                txtParameterId.Focus();
            }
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString["Mode"].ToString();
            OperationResult objOperationResult = new OperationResult();

            if (Mode == "New")
            {
                // Create the entity
                datahierarchyDto objEntity = new datahierarchyDto();

                // Populate the entity
                objEntity.i_GroupId = int.Parse(txtGroupId.Text.Trim());
                objEntity.i_ItemId = int.Parse(txtParameterId.Text.Trim());
                objEntity.v_Value1 = txtDescription.Text.Trim().ToUpper();
                objEntity.v_Value2 = txtDescription2.Text.Trim().ToUpper();
                objEntity.v_Field = txtField.Text.Trim().ToUpper();
                objEntity.i_ParentItemId = -1;
            
                // Obtener el usuario autenticado
                int intUserPersonId = ((ClientSession)Session["objClientSession"]).i_SystemUserId;
                //Validar si la Clave primaria ya existe.
                if (_objBL.GetDataHierarchy(ref objOperationResult, objEntity.i_GroupId, objEntity.i_ItemId) != null)
                {
                    Alert.Show("La clave primaria ya existe！", MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    // Save the data
                    _objBL.AddDataHierarchy(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());
                }

            }
            else if (Mode == "Edit")
            {
                // Obtener el usuario autenticado
                int intUserPersonId = ((ClientSession)Session["objClientSession"]).i_SystemUserId;

                // Get the entity from the session
                datahierarchyDto objEntity = (datahierarchyDto)Session["objEntity"];

                // Populate the entity
                objEntity.i_GroupId = int.Parse(txtGroupId.Text.Trim());
                objEntity.i_ItemId = int.Parse(txtParameterId.Text.Trim());
                objEntity.v_Value1 = txtDescription.Text.Trim().ToUpper();
                if (txtUserInterfaceOrder.Text == "") objEntity.i_Sort = null;
                else objEntity.i_Sort = int.Parse(txtUserInterfaceOrder.Text.Trim());
                objEntity.v_Value2 = txtDescription2.Text.Trim().ToUpper();
                objEntity.v_Field = txtField.Text.Trim().ToUpper();
                objEntity.i_ParentItemId = Int32.Parse(ddlParentItemId.SelectedValue);

             // Save the data
                _objBL.UpdateDataHierarchy(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());
            }
            else if (Mode == "NewChildren")
            {
                // Obtener el usuario autenticado
                //int intUserPersonId = ((ClientSession)Session["objClientSession"]).i_SystemUserId;

                // Create the entity
                datahierarchyDto objEntity = new datahierarchyDto();

                // Populate the entity
                objEntity.i_GroupId = int.Parse(txtGroupId.Text.Trim());
                objEntity.i_ItemId = int.Parse(txtParameterId.Text.Trim());
                objEntity.v_Value1 = txtDescription.Text.Trim().ToUpper();
                if (txtUserInterfaceOrder.Text == "") objEntity.i_Sort = null;
                else objEntity.i_Sort = int.Parse(txtUserInterfaceOrder.Text.Trim());
                objEntity.v_Value2 = txtDescription2.Text.Trim().ToUpper();
                objEntity.v_Field = txtField.Text.Trim().ToUpper();
                objEntity.i_ParentItemId = Int32.Parse(ddlParentItemId.SelectedValue);
               
                if (_objBL.GetDataHierarchy(ref objOperationResult, objEntity.i_GroupId, objEntity.i_ItemId) != null)
                {
                    Alert.Show("¡La clave primaria ya existe！", MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    // Save the data
                    _objBL.AddDataHierarchy(ref objOperationResult, objEntity, ((ClientSession)Session["objClientSession"]).GetAsList());
                }
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