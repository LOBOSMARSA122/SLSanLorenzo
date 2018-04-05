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
    public partial class FRM005D : System.Web.UI.Page
    {
        NodeBL _objNodeBL = new NodeBL();

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
                btnNew.OnClientClick = winEdit.GetSaveStateReference(hfRefresh.ClientID) + winEdit.GetShowReference("FRM005E.aspx?Mode=New&nodeId=" + NodeId.ToString());
                BindGrid();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            }
        }

        private void BindGrid()
        {
            grd.DataSource = GetData();
            grd.DataBind();
        }

        private List<RoleNodeList> GetData()
        {
            OperationResult objOperationResult = new OperationResult();
            List<RoleNodeList> objData = _objNodeBL.GetRoleNode(ref objOperationResult, NodeId);

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }
           
            return objData;
        }

        protected void winEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void grd_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAction")
            {
                OperationResult objOperationResult = new OperationResult();
                // Obtener los IDs de la fila seleccionada
                int roleId = int.Parse(grd.DataKeys[grd.SelectedRowIndex][1].ToString());

                // Borrar  
                _objNodeBL.DeleteRoleAll(ref objOperationResult, NodeId, roleId, 1, ((ClientSession)Session["objClientSession"]).GetAsList());

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
    }
}