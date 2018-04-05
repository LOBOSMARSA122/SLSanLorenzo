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

namespace Sigesoft.Server.WebClientAdmin.UI.Common
{
    public partial class FRM005 : System.Web.UI.Page
    {
        NodeBL _objNodeBL = new NodeBL();
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //_Util.SetFormActionsInSession("FRM005");
                //btnNew.Enabled = _Util.IsActionEnabled("FRM005_ADD");

                Session["strFilterExpression"] = null;
                btnNew.OnClientClick = winEdit.GetSaveStateReference(hfRefresh.ClientID) + winEdit.GetShowReference("FRM005A.aspx?Mode=New");
                BindGrid();

              
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string filter = string.Empty;
            if (!string.IsNullOrEmpty(txtNodeFilter.Text))
            {
                filter = "v_Description.Contains(\"" + txtNodeFilter.Text.Trim() + "\")";
            }

            Session["strFilterExpression"] = filter;

            // Refresh the grid
            grdData.PageIndex = 0;
            this.BindGrid();
        }

        private void BindGrid()
        {
            string _filterExpression =  Convert.ToString(Session["strFilterExpression"]);
            grdData.RecordCount = GetTotalCount(_filterExpression);
            grdData.DataSource = GetData(grdData.PageIndex, grdData.PageSize, "v_Description ASC", _filterExpression);
            grdData.DataBind();

        }

        private int GetTotalCount(string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var nodeCount = _objNodeBL.GetNodeCount(ref objOperationResult, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }

            return nodeCount;
        }

        private List<NodeList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            List<NodeList> objData = _objNodeBL.GetNodePagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }

            return objData;
        }

        protected void grdData_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            grdData.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void grdData_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAction")
            {
                OperationResult objOperationResult = new OperationResult();
                // Obtener los IDs de la fila seleccionada
                int nodeId = Convert.ToInt32(grdData.DataKeys[grdData.SelectedRowIndex][0]);

                // Borrar Nodo 
                _objNodeBL.DeleteNode(ref objOperationResult, nodeId, ((ClientSession)Session["objClientSession"]).GetAsList());

                // Borrar Organizaciones asociadas del nodo actual
                //OperationResult objOperationResult1 = new OperationResult();
                //_objNodeBL.DeleteNodeOrganizations(ref objOperationResult1, _nodeId, 1);

                BindGrid();
            }

        }

        protected void winEdit_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void winEdit_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void grdData_PreDataBound(object sender, EventArgs e)
        {
            //WindowField wfEdit1 = grdData.FindColumn("myWindowField") as WindowField;
            //wfEdit1.Enabled = _Util.IsActionEnabled("FRM005_EDIT");

            //WindowField wfEdit2 = grdData.FindColumn("myWindowField1") as WindowField;
            //wfEdit2.Enabled = _Util.IsActionEnabled("FRM005_EDIT");

            //LinkButtonField lbfDelete = grdData.FindColumn("lbfAction2") as LinkButtonField;
            //lbfDelete.Enabled = _Util.IsActionEnabled("FRM005_DELETE");
        }
    }
}