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
    public partial class FRM005DA : System.Web.UI.Page
    {
        NodeBL _nodeBL = new NodeBL();

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

        public int RoleId
        {
            get
            {
                if (Request.QueryString["roleId"] != null)
                {
                    string roleId = Request.QueryString["roleId"].ToString();
                    if (!string.IsNullOrEmpty(roleId))
                    {
                        return Convert.ToInt32(roleId);
                    }
                }

                return 0;
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

        public string RoleName
        {
            get
            {
                if (Request.QueryString["roleName"] != null)
                {
                    string roleName = Request.QueryString["roleName"].ToString();
                    if (!string.IsNullOrEmpty(roleName))
                    {
                        return roleName;
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
                btnNew.OnClientClick = winEditRoleNodeComponent.GetSaveStateReference(hfRefresh.ClientID) + winEditRoleNodeComponent.GetShowReference("FRM005DAA.aspx?Mode=New&nodeId=" + NodeId.ToString() + "&roleId=" + RoleId.ToString());
                LoadData();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            }
        }

        private void LoadData()
        {
            OperationResult objOperationResult1 = new OperationResult();

            BindGrid();

            if (Mode == "New")
            {

            }
            else if (Mode == "Edit")
            {

                txtRoleNode.Text = RoleName;
                OperationResult objOperationResult2 = new OperationResult();
                //var objDataForShowTreeView = Sigesoft.Server.WebClientAdmin.BLL.Utils.GetWarehouseForShowTreeView(ref objOperationResult2, NodeId, OrganizationId, LocationId);



            }

        }

        private void BindGrid()
        {
            grd.DataSource = GetData();
            grd.DataBind();
        }

        private List<RoleNodeComponentProfileList> GetData()
        {
            OperationResult objOperationResult = new OperationResult();
            List<RoleNodeComponentProfileList> objData = _nodeBL.GetRoleNodeComponentProfileForGridView(ref objOperationResult, NodeId, RoleId);

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }
           
            return objData;
        }

        protected void grd_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAction")
            {
                OperationResult objOperationResult = new OperationResult();
                // Obtener los IDs de la fila seleccionada
                string roleNodeComponentId = grd.DataKeys[grd.SelectedRowIndex][0].ToString();

                // Borrar  
                _nodeBL.DeleteRoleNodeComponentProfile(ref objOperationResult, 
                                                                roleNodeComponentId,
                                                                ((ClientSession)Session["objClientSession"]).GetAsList());

                //if (objOperationResult.Success != 1)
                //{
                //    Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                //}

                BindGrid();
            }
        }

        protected void grd_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            grd.PageIndex = e.NewPageIndex;
        }

        protected void winEditRoleNodeComponent_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
    }
}