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
    public partial class FRM005G : System.Web.UI.Page
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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnNew.OnClientClick = winEdit.GetSaveStateReference(hfRefresh.ClientID) + winEdit.GetShowReference("FRM005H.aspx?Mode=New&nodeId=" + NodeId.ToString());
                BindGrid();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            }
        }

        protected void grd_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            grd.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void winEdit_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            grd.RecordCount = GetTotalCount();
            grd.DataSource = GetData(grd.PageIndex, grd.PageSize, "v_Name ASC", strFilterExpression);
            grd.DataBind();
        }

        private int GetTotalCount()
        {
            OperationResult objOperationResult = new OperationResult();
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            return _objNodeBL.GetAttentionInAreaByNodeCount(ref objOperationResult, strFilterExpression);
        }

        private List<AttentionInAreaList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objNodeBL.GetAttentionInAreaByNodePagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }         

            return _objData;
        }

        protected void grd_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAction")
            {
                DeleteItem();
                BindGrid();
            }
        }

        private void DeleteItem()
        {
            OperationResult objOperationResult = new OperationResult();
            // Obtener los IDs de la fila seleccionada
            string pstrAttentionInAreaId = Convert.ToString( grd.DataKeys[grd.SelectedRowIndex][0]);
            // Delete the item
            _objNodeBL.DeleteAttentionInAreaByNode(ref objOperationResult, pstrAttentionInAreaId, ((ClientSession)Session["objClientSession"]).GetAsList());
                Session["strFilterExpression"] = "";
        }

    }
}
