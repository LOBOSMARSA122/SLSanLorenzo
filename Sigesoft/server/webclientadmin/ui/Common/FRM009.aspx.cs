using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;

namespace Sigesoft.Server.WebClientAdmin.UI
{
    public partial class FRM009 : System.Web.UI.Page
    {

        LogBL _objLogBL = new LogBL();
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        NodeBL _objNodeBL = new NodeBL();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                OperationResult objOperationResult = new OperationResult();
                Utils UtilComboBox = new Utils();
         
                //Llenado de combos
                Utils.LoadDropDownList(ddlNodeId, "v_Description", "i_NodeId", _objNodeBL.GetAllNode(ref objOperationResult), DropDownListAction.All);                
                Utils.LoadDropDownList(ddlEventTypeId, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 102), DropDownListAction.All);
                Utils.LoadDropDownList(ddlSuccess, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 111), DropDownListAction.All);
                // Establecer el filtro inicial para los datos
                Session["strFilterExpression"] = null;
                BindGrid();
            }
        }

        protected void grdData_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            grdData.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        // Esto actualiza la grilla al cerrar el popup de edición. Solo se invoca si al cerrar el popup se hace el postback.
        protected void winEdit_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);           
            grdData.RecordCount = GetTotalCount(strFilterExpression);
            grdData.DataSource = GetData(grdData.PageIndex, grdData.PageSize, "d_Date DESC", strFilterExpression);
            grdData.DataBind();
        }

        private int GetTotalCount(string pstrFilterExpression)
        {
           OperationResult objOperationResult = new OperationResult();
           return _objLogBL.GetLogsCount(ref objOperationResult, pstrFilterExpression);
        }

        private List<LogList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
           List<LogList> _objData = _objLogBL.GetLogsPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }

            return _objData;
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
          
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddlNodeId.SelectedValue != "-1") Filters.Add("i_NodeId==" + ddlNodeId.SelectedValue);
      
            if (ddlEventTypeId.SelectedValue != "-1") Filters.Add("i_EventTypeId==" + ddlEventTypeId.SelectedValue);
            if (ddlSuccess.SelectedValue != "-1") Filters.Add("i_Success==" + ddlSuccess.SelectedValue);

            if (!string.IsNullOrEmpty(txtUserName.Text)) Filters.Add("v_SystemUserName.Contains(\"" + txtUserName.Text.Trim().ToUpper() + "\")");
            if (!string.IsNullOrEmpty(txtProcessEntity.Text)) Filters.Add("v_ProcessEntity.Contains(\"" + txtProcessEntity.Text.Trim().ToUpper() + "\")");
            if (!string.IsNullOrEmpty(txtElementItem.Text)) Filters.Add("v_ElementItem.Contains(\"" + txtElementItem.Text.Trim().ToUpper() + "\")");
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
        }

        
    }
}