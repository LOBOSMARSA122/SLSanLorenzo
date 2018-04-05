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
    public partial class FRM030 : System.Web.UI.Page
    {
        SyncBL _objSyncBL = new SyncBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (!IsPostBack)
            {
                // Establecer el filtro inicial para los datos
                Session["strFilterExpression"] = null;
              
                //Llenado de combos                
                Utils.LoadDropDownList(ddlNodeId, "Value1", "Id", BLL.Utils.GetAllNodeForCombo(ref objOperationResult), DropDownListAction.All);
         

            }    
        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            //grdData.RecordCount = GetTotalCount();
            grdData.DataSource = GetData(grdData.PageIndex, grdData.PageSize, "", strFilterExpression);
            grdData.DataBind();
        }

        private ServerNodeSyncList[] GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objSyncBL.GetServerNodeSyncPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression).ToArray();

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }

            return _objData;
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddlNodeId.SelectedValue != "-1")
            {
                if (!string.IsNullOrEmpty(ddlNodeId.SelectedValue)) Filters.Add("i_NodeId==" + ddlNodeId.SelectedValue.ToString());
            }

            // Create the Filter Expression
            string strFilterExpression = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    strFilterExpression = strFilterExpression + item + " && ";
                }
                strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
            }

            // Save the Filter expression in the Session
            Session["strFilterExpression"] = strFilterExpression;

            // Refresh the grid
            grdData.PageIndex = 0;
            this.BindGrid();
            // 
        }

    }
}