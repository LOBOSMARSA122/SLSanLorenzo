using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;
using FineUI;
namespace Sigesoft.Server.WebClientAdmin.UI
{
    public partial class FRM005H : System.Web.UI.Page
    {
        NodeBL _objNodeBL = new NodeBL();
        string AttentionInAreaId;

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
            Utils UtilComboBox = new Utils();
            //Utils.LoadDropDownList(ddlComponentId, "Value1", "Id", BLL.Utils.GetComponents(ref objOperationResult), DropDownListAction.Select);
           
            string Mode = Request.QueryString["Mode"].ToString();
            string AttentionInAreaId = "";

            if (Request.QueryString["v_AttentionInAreaId"] != null)
                AttentionInAreaId = Request.QueryString["v_AttentionInAreaId"].ToString();

            LoadTreeView();
            if (Mode == "New")
            {
                txtName.Text = "";
                txtOfficeNumber.Text = "";
                //ddlComponentId.SelectedValue = "-1";
            }
            else if (Mode == "Edit")
            {
                // Get the Entity Data
                attentioninareaDto objEntity = _objNodeBL.GetAttentionInAreaByNode(ref objOperationResult, AttentionInAreaId);

                // Save the entity on the session
                Session["objEntity"] = objEntity;

                // Show the data on the form
                txtName.Text = objEntity.v_Name;
                txtOfficeNumber.Text = objEntity.v_OfficeNumber;

                var x = Sigesoft.Server.WebClientAdmin.BLL.Utils.GetComponentsByAttentionInArea(ref objOperationResult, AttentionInAreaId);

                foreach (var item in x)
                {
                    SearchNode(tvComponent.Nodes, item.Id, true);
                }
            }           
        }

        private void SearchNode(FineUI.TreeNodeCollection pNodes, string pNodeId, bool pStatus)
        {
            //Busca un nodo en el treeview y chekarlo
            foreach (FineUI.TreeNode sNode in pNodes)
            {
                if (sNode.NodeID.Trim() == pNodeId)
                {
                    sNode.Checked = pStatus;
                    sNode.CommandName = "1";
                    break;
                }
                SearchNode(sNode.Nodes, pNodeId, pStatus);
            }
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString["Mode"].ToString();
            OperationResult objOperationResult = new OperationResult();

            if (Mode == "New")
            {
                // Create the entity
                attentioninareaDto objEntity = new attentioninareaDto();

                // Populate the entity
                objEntity.v_Name = txtName.Text.Trim().ToUpper();
                objEntity.v_OfficeNumber = txtOfficeNumber.Text.Trim().ToUpper();

                // Save the data                  
                AttentionInAreaId = _objNodeBL.AddAttentionInAreaByNode(ref objOperationResult, objEntity, InsertAttentionAreaComponent(),((ClientSession)Session["objClientSession"]).GetAsList());
            }
            else if (Mode == "Edit")
            {
                // Obtener el usuario autenticado
                int intUserPersonId = ((ClientSession)Session["objClientSession"]).i_SystemUserId;

                // Get the entity from the session
                attentioninareaDto objEntity = (attentioninareaDto)Session["objEntity"];

                // Populate the entity
                objEntity.v_Name = txtName.Text.Trim().ToUpper();
                objEntity.v_OfficeNumber = txtOfficeNumber.Text.Trim().ToUpper();

                var objAddAttentionAreaComponentList = UpdateAttentionAreaComponent();
                var objDeleteAttentionAreaComponentList = DeleteAttentionAreaComponent();

                // Save the data
                _objNodeBL.UpdateAttentionInAreaByNode(ref objOperationResult, objEntity, objAddAttentionAreaComponentList, objDeleteAttentionAreaComponentList, ((attentioninareaDto)Session["objEntity"]).v_AttentionInAreaId, ((ClientSession)Session["objClientSession"]).GetAsList());
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

        private List<attentioninareacomponentDto> InsertAttentionAreaComponent()
        {
            List<attentioninareacomponentDto> objwarehouseListAdd = new List<attentioninareacomponentDto>();

            // Datos de Almacén
            foreach (var item in tvComponent.Nodes)
            {
                if (item.Checked == true)
                {
                    attentioninareacomponentDto objattentioninareacomponent = new attentioninareacomponentDto();
                    objattentioninareacomponent.v_AttentionInAreaId = AttentionInAreaId;
                    objattentioninareacomponent.v_ComponentId = item.NodeID;
                    objwarehouseListAdd.Add(objattentioninareacomponent);
                }

            }

            return objwarehouseListAdd.Count == 0 ? null : objwarehouseListAdd;
        }

        private List<attentioninareacomponentDto> UpdateAttentionAreaComponent()
        {
            List<attentioninareacomponentDto> objwarehouseListUpdate = new List<attentioninareacomponentDto>();

            foreach (var item in tvComponent.Nodes)
            {
                if (item.Checked)
                {
                    if (item.CommandName != "1")
                    {
                        attentioninareacomponentDto objattentioninareacomponent = new attentioninareacomponentDto();
                        objattentioninareacomponent.v_AttentionInAreaId = AttentionInAreaId;
                        objattentioninareacomponent.v_ComponentId = item.NodeID;

                        objwarehouseListUpdate.Add(objattentioninareacomponent);
                    }
                }
            }

            return objwarehouseListUpdate.Count == 0 ? null : objwarehouseListUpdate;

        }

        private List<attentioninareacomponentDto> DeleteAttentionAreaComponent()
        {
            List<attentioninareacomponentDto> objwarehouseListDelete = new List<attentioninareacomponentDto>();

            foreach (var item in tvComponent.Nodes)
            {
                if (!item.Checked)
                {
                    if (item.CommandName == "1")
                    {
                        // Datos de Almacen
                        attentioninareacomponentDto objattentioninareacomponent = new attentioninareacomponentDto();
                        objattentioninareacomponent.v_AttentionInAreaId = AttentionInAreaId;
                        objattentioninareacomponent.v_AttentioninAreaComponentId = AttentionInAreaId;
                        objattentioninareacomponent.v_ComponentId = item.NodeID;

                        objwarehouseListDelete.Add(objattentioninareacomponent);
                    }
                }
            }

            return objwarehouseListDelete.Count == 0 ? null : objwarehouseListDelete;

        }

        private void LoadTreeView()
        {            
            //string organizationId = ddlOrganization.SelectedValue;
            //string locationId = ddlLocation.SelectedValue;

            OperationResult objOperationResult = new OperationResult();
            var objComponent = BLL.Utils.GetComponents(ref objOperationResult);

            if (objComponent != null)
            {
                tvComponent.Nodes.Clear();
                FineUI.TreeNode nodePrimary = null;

                // armado del tree con todos los almacenes por organizaciones
                foreach (var item in objComponent)
                {
                    nodePrimary = new FineUI.TreeNode();
                    nodePrimary.EnableCheckBox = true;
                    nodePrimary.Text = item.Value1;
                    nodePrimary.NodeID = item.Id;
                    tvComponent.Nodes.Add(nodePrimary);
                }
            }

        }
    }
}
