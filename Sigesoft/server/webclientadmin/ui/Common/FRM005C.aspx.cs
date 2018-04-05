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

namespace Sigesoft.Server.WebClientAdmin.UI.Common
{
    public partial class FRM005C : System.Web.UI.Page
    {
        #region Declarations

        SecurityBL _objSecurityBL = new SecurityBL();
        NodeBL _objNodeBL = new NodeBL();
        //List<nodeorganizationlocationwarehouseprofileDto> _warehouseList = null;

        #endregion

        #region Properties

        public int NodeId
        {
            get
            {
                if (Request.QueryString["nodeId"] != null)
                {
                    string nodeId = Request.QueryString["nodeId"].ToString();
                    if (!string.IsNullOrEmpty(nodeId))
                    {
                        return Convert.ToInt32(nodeId);
                    }
                }

                return 0;
            }
        }

        public string OrganizationId
        {
            get
            {
                if (Request.QueryString["organizationId"] != null)
                {
                    string organizationId = Request.QueryString["organizationId"].ToString();
                    if (!string.IsNullOrEmpty(organizationId))
                    {
                        return organizationId;
                    }
                }

                return string.Empty;
            }
        }

        public string LocationId
        {
            get
            {
                if (Request.QueryString["locationId"] != null)
                {
                    string locationId = Request.QueryString["locationId"].ToString();
                    if (!string.IsNullOrEmpty(locationId))
                    {
                        return locationId;
                    }
                }

                return string.Empty;
            }
        }

        public string Mode
        {
            get
            {
                if (Request.QueryString["Mode"] != null)
                {
                    string mode = Request.QueryString["Mode"].ToString();
                    if (!string.IsNullOrEmpty(mode))
                    {
                        return mode;
                    }
                }

                return string.Empty;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
                //_warehouseList = new List<nodeorganizationlocationwarehouseprofileDto>();
                //Session["sobjwarehouseList"] = _warehouseList;
                //int systemUserId = -1;
                //if (Request.QueryString["systemUserId"] != null) systemUserId = int.Parse(Request.QueryString["systemUserId"].ToString());
                //btnNew.OnClientClick = winEdit.GetSaveStateReference(hfRefresh.ClientID) + winEdit.GetShowReference("FRM002B.aspx?Mode=New&systemUserId=" + systemUserId.ToString());

            }
        }

        private void LoadData()
        {
            OperationResult objOperationResult1 = new OperationResult();

            ddlLocation.Items.Insert(0, new FineUI.ListItem(Constants.Select, Constants.SelectValue));

            var objOrganizations = Sigesoft.Server.WebClientAdmin.BLL.Utils.GetAllOrganizationsForCombo(ref objOperationResult1);

            Sigesoft.Server.WebClientAdmin.UI.Utils.LoadDropDownList(ddlOrganization, "Value1", "Id", objOrganizations, DropDownListAction.Select);


            if (Mode == "New")
            {

            }
            else if (Mode == "Edit")
            {
                ddlOrganization.SelectedValue = OrganizationId;
                LoadLocation();
                ddlLocation.SelectedValue = LocationId;
                ddlOrganization.Enabled = false;
                ddlLocation.Enabled = false;
                LoadTreeView();

                OperationResult objOperationResult2 = new OperationResult();
                var objDataForShowTreeView = Sigesoft.Server.WebClientAdmin.BLL.Utils.GetWarehouseForShowTreeView(ref objOperationResult2, NodeId, OrganizationId, LocationId);

                // Marcar (CkeckBox) los almacenes Activos
                foreach (var item in objDataForShowTreeView)
                {
                    SearchNode(tvWarehouse.Nodes, item.Id, true);
                }
            
            }

        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            if (Mode == "New")
            {
                #region Validate

              

                #endregion

                // Datos de nodo / Empresa / Sede 
                NodeOrganizationLoactionWarehouseList objNodeOrgLocaWarehouse = new NodeOrganizationLoactionWarehouseList();
                objNodeOrgLocaWarehouse.i_NodeId = NodeId;
                objNodeOrgLocaWarehouse.v_OrganizationId = ddlOrganization.SelectedValue;
                objNodeOrgLocaWarehouse.v_LocationId = ddlLocation.SelectedValue;

                // Datos de Almacén

                var objInsertWarehouseList = InsertWarehouse();
                
                OperationResult objOperationResult1 = new OperationResult();
                // Graba Nodo / Empresa / Sede / Almacén
                _objNodeBL.AddNodeOrganizationLoactionWarehouse(ref objOperationResult1, objNodeOrgLocaWarehouse, objInsertWarehouseList, ((ClientSession)Session["objClientSession"]).GetAsList());

                if (objOperationResult1.ErrorMessage != null)
                {
                    Alert.ShowInTop(objOperationResult1.ErrorMessage);
                    return;
                }

                if (objOperationResult1.Success != 1)
                {
                    Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult1.ExceptionMessage);
                }

            }
            else if (Mode == "Edit")
            {
                NodeOrganizationLoactionWarehouseList objNodeOrgLocaWarehouse = new NodeOrganizationLoactionWarehouseList();
                objNodeOrgLocaWarehouse.i_NodeId = NodeId;
                objNodeOrgLocaWarehouse.v_OrganizationId = ddlOrganization.SelectedValue;
                objNodeOrgLocaWarehouse.v_LocationId = ddlLocation.SelectedValue;

                OperationResult objOperationResult2 = new OperationResult();
                var objAddWarehouseList = UpdateWarehouse();
                var objDeleteWarehouseList = DeleteWarehouse();

                _objNodeBL.UpdateNodeOrganizationLoactionWarehouse(ref objOperationResult2, 
                                                                    objNodeOrgLocaWarehouse, 
                                                                    objAddWarehouseList,
                                                                    objDeleteWarehouseList,
                                                                    ((ClientSession)Session["objClientSession"]).GetAsList());

                if (objOperationResult2.ErrorMessage != null)
                {
                    Alert.ShowInTop(objOperationResult2.ErrorMessage);
                    return;
                }

            }

            // Cerrar página actual y hacer postback en el padre para actualizar
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        protected void ddlOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!tvWarehouse.Enabled)
                tvWarehouse.Enabled = true;
            LoadLocation();         
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Mode == "New")
            {
                if (!tvWarehouse.Enabled)
                    tvWarehouse.Enabled = true;

                // Validar nodo / organización
                if (!IsValidNodeOrganization())
                {
                    tvWarehouse.Enabled = false;
                    return;
                }
            }
           
            // carga del TreeView de Almacenes
            LoadTreeView();
        }

        private void LoadLocation()
        {
            tvWarehouse.Nodes.Clear();
            OperationResult objOperationResult = new OperationResult();
            var objorgLocationWarehouse = Sigesoft.Server.WebClientAdmin.BLL.Utils.GetLocationByOrganizationIdForCombo(ref objOperationResult, ddlOrganization.SelectedValue);

            Sigesoft.Server.WebClientAdmin.UI.Utils.LoadDropDownList(ddlLocation,
                "Value1",
                "Id",
                objorgLocationWarehouse,
                DropDownListAction.Select);

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                return;
            }
          
        }

        private void LoadTreeView()
        {          
            string organizationId = ddlOrganization.SelectedValue;
            string locationId = ddlLocation.SelectedValue;

            OperationResult objOperationResult = new OperationResult();
            var objWarehouse = Sigesoft.Server.WebClientAdmin.BLL
                               .Utils
                               .GetWarehouseByOrganizationAndLocationForTreeView(ref objOperationResult, organizationId, locationId);

            if (objWarehouse != null)
            {
                tvWarehouse.Nodes.Clear();
                FineUI.TreeNode nodePrimary = null;

                // armado del tree con todos los almacenes por organizaciones
                foreach (var item in objWarehouse)
                {
                    nodePrimary = new FineUI.TreeNode();
                    nodePrimary.EnableCheckBox = true;
                    nodePrimary.Text = item.Value1;
                    nodePrimary.NodeID = item.Id;
                    tvWarehouse.Nodes.Add(nodePrimary);
                }       
            }
            
        }

        private List<nodeorganizationlocationwarehouseprofileDto> InsertWarehouse()
        {
            List<nodeorganizationlocationwarehouseprofileDto> objwarehouseListAdd = new List<nodeorganizationlocationwarehouseprofileDto>();         

            // Datos de Almacén
            foreach (var item in tvWarehouse.Nodes)
            {
                if (item.Checked == true)
                {
                    // Datos de Almacen
                    nodeorganizationlocationwarehouseprofileDto objWarehouse = new nodeorganizationlocationwarehouseprofileDto();
                    objWarehouse.i_NodeId = NodeId;
                    objWarehouse.v_OrganizationId = ddlOrganization.SelectedValue;
                    objWarehouse.v_LocationId = ddlLocation.SelectedValue;
                    objWarehouse.v_WarehouseId = item.NodeID;

                    objwarehouseListAdd.Add(objWarehouse);
                }

            }

            return objwarehouseListAdd.Count == 0 ? null : objwarehouseListAdd;
        }

        private List<nodeorganizationlocationwarehouseprofileDto> UpdateWarehouse()
        {
            List<nodeorganizationlocationwarehouseprofileDto> objwarehouseListUpdate = new List<nodeorganizationlocationwarehouseprofileDto>();

            foreach (var item in tvWarehouse.Nodes)
            {
                if (item.Checked)
                {
                    if (item.CommandName != "1")
                    {
                        // Datos de Almacen
                        nodeorganizationlocationwarehouseprofileDto objWarehouse = new nodeorganizationlocationwarehouseprofileDto();
                        objWarehouse.i_NodeId = NodeId;
                        objWarehouse.v_OrganizationId = ddlOrganization.SelectedValue;
                        objWarehouse.v_LocationId = ddlLocation.SelectedValue;
                        objWarehouse.v_WarehouseId = item.NodeID;

                        objwarehouseListUpdate.Add(objWarehouse);
                    }
                }
            }

            return objwarehouseListUpdate.Count == 0 ? null : objwarehouseListUpdate;

        }

        private List<nodeorganizationlocationwarehouseprofileDto> DeleteWarehouse()
        {
            List<nodeorganizationlocationwarehouseprofileDto> objwarehouseListDelete = new List<nodeorganizationlocationwarehouseprofileDto>();

            foreach (var item in tvWarehouse.Nodes)
            {
                if (!item.Checked)
                {
                    if (item.CommandName == "1")
                    {
                        // Datos de Almacen
                        nodeorganizationlocationwarehouseprofileDto objWarehouse = new nodeorganizationlocationwarehouseprofileDto();
                        objWarehouse.i_NodeId = NodeId;
                        objWarehouse.v_OrganizationId = ddlOrganization.SelectedValue;
                        objWarehouse.v_LocationId = ddlLocation.SelectedValue;
                        objWarehouse.v_WarehouseId = item.NodeID;

                        objwarehouseListDelete.Add(objWarehouse);
                    }
                }
            }

            return objwarehouseListDelete.Count == 0 ? null : objwarehouseListDelete;

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

        private bool IsValidNodeOrganization()
        {
            // Validar existencia de un nodo
            OperationResult objOperationResult6 = new OperationResult();
            string organizationId = ddlOrganization.SelectedValue;
            string locationId = ddlLocation.SelectedValue;
            string filterExpression = string.Format("i_NodeId=={0}&&v_OrganizationId==\"{1}\"&&v_LocationId==\"{2}\"", NodeId, organizationId, locationId);
            var recordCount = _objNodeBL.GetNodeOrganizationCount(ref objOperationResult6, filterExpression);

            if (recordCount != 0)
            {
                Alert.ShowInTop(string.Format("<font color='red'> {0} / {1} </font> ya se encuentra registrado.<br> Por favor ingrese otra Empresa / Sede para este nodo.", ddlOrganization.SelectedText, ddlLocation.SelectedText));
                return false;
            }
            return true;
        }

    }
}