using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;


namespace Sigesoft.Server.WebClientAdmin.UI.Configuracion
{
    public partial class FRMUSUARIOSEXTERNOS : System.Web.UI.Page
    {
        SecurityBL _objSecurityBL = new SecurityBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["strFilterExpression"] = null;
                btnNew.OnClientClick = winEditUser.GetShowReference("FRMUSUARIOSEXTERNOS_ADD.aspx?Mode=New");
            
                BindGrid(); 
            }
        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            grdData.RecordCount = GetTotalCount();
            var objUserList = GetData(grdData.PageIndex, grdData.PageSize, "i_SystemUserId ASC", strFilterExpression);

            if (objUserList.Count > 0)
            {
                grdData.DataSource = objUserList;
                grdData.DataBind();
            }
            else
            {
                Alert.ShowInTop("Información de operación:" + System.Environment.NewLine + "No existen registros.");
            }

        }

        private int GetTotalCount()
        {
            OperationResult objOperationResult = new OperationResult();
            string strFilterExpression = "i_IsDeleted=0";
            if (Session["strFilterExpression"] != null)
            {
                var ss = Convert.ToString(Session["strFilterExpression"]);
                if (ss.Length > 0)
                {
                    strFilterExpression += "&&" + ss;
                }

            }

            return _objSecurityBL.GetSystemUserCount(ref objOperationResult, strFilterExpression);

        }

        private List<SystemUserList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            List<SystemUserList> objUserList = _objSecurityBL.GetSystemUserPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression,2);

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }
 

            return objUserList;

        }

        protected void grdData_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            grdData.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string filter = string.Empty;
            if (!string.IsNullOrEmpty(txtUserNameFilter.Text))
            {
                filter = "v_UserName.Contains(\"" + txtUserNameFilter.Text.Trim() + "\")";
            }

            Session["strFilterExpression"] = filter;

            // Refresh the grid
            grdData.PageIndex = 0;
            this.BindGrid();
        }

        protected void grdData_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
        }

        protected void winEditUser_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
    }
}