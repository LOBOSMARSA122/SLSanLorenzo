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
    public partial class FRM005B : System.Web.UI.Page
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
                btnNew.OnClientClick = winEdit.GetSaveStateReference(hfRefresh.ClientID) + winEdit.GetShowReference("FRM005C.aspx?Mode=New&nodeId=" + NodeId.ToString());
                BindGrid();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        private void BindGrid()
        {
            grd.DataSource = GetData();
            grd.DataBind();
        }
    
        private List<NodeOrganizationLoactionWarehouseList> GetData()
        {
            OperationResult objOperationResult = new OperationResult();
            List<NodeOrganizationLoactionWarehouseList> objData = _objNodeBL.GetNodeOrganization(ref objOperationResult, NodeId,txtOrganizationFilter.Text);

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
                //int nodeId = Convert.ToInt32(grd.DataKeys[grd.SelectedRowIndex][0]);
                string organizationId = grd.DataKeys[grd.SelectedRowIndex][1].ToString();
                string locationId = grd.DataKeys[grd.SelectedRowIndex][2].ToString();
                
                // Borrar  
                _objNodeBL.UpdateNodeOrganizationChangeStatusAll(ref objOperationResult, NodeId, organizationId, locationId, 1, ((ClientSession)Session["objClientSession"]).GetAsList());
                
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