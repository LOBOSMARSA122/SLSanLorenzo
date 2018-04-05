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
    public partial class FRM012 : System.Web.UI.Page
    {
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        SyncBL _objSyncBL = new SyncBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (!IsPostBack)
            {
                // Establecer el filtro inicial para los datos
                Session["strFilterExpression"] = null;
                btnNew.OnClientClick = winEdit.GetSaveStateReference(hfRefresh.ClientID) + winEdit.GetShowReference("FRM028A.aspx?Mode=New");
                //Llenado de combos
                Utils.LoadDropDownList(ddlSoftwareComponentId, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 144), DropDownListAction.All);
           
            }      
        }

        protected void grdData_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            grdData.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void winEdit_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            //grdData.RecordCount = GetTotalCount();
            grdData.DataSource = GetData(grdData.PageIndex, grdData.PageSize, "", strFilterExpression);
            grdData.DataBind();
        }

        private SoftwareComponentReleaseList[] GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _objSyncBL.GetSoftwareComponentReleasePagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression).ToArray();

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
            if (ddlSoftwareComponentId.SelectedValue !="-1")
            {
                if (!string.IsNullOrEmpty(ddlSoftwareComponentId.SelectedValue)) Filters.Add("i_SoftwareComponentId==" + ddlSoftwareComponentId.SelectedValue.ToString());
            }

            if (!string.IsNullOrEmpty(txtSoftwareComponentVersion.Text)) Filters.Add("v_SoftwareComponentVersion.Contains(\"" + txtSoftwareComponentVersion.Text.Trim().ToUpper() + "\")");
   
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