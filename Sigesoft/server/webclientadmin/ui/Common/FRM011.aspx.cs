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

namespace Sigesoft.Server.WebClientAdmin.UI
{
    public partial class FRM011 : System.Web.UI.Page
    {
        DataHierarchyBL _objBL = new DataHierarchyBL();
        private Utils _Util = new Utils();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //_Util.SetFormActionsInSession("FRM011");
                //btnNew.Enabled = _Util.IsActionEnabled("FRM011_ADD");

                // Establecer el filtro inicial para los datos
                Session["strFilterExpression"] = null;
                btnNew.OnClientClick = winEdit.GetSaveStateReference(hfRefresh.ClientID) + winEdit.GetShowReference("FRM011A.aspx?Mode=New");
                btnFilter_Click(sender, e);
                //BindGrid();
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

        protected void winViewChildren_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            grdData.RecordCount = GetTotalCount();
            grdData.DataSource = GetData(grdData.PageIndex, grdData.PageSize, "i_GroupId ASC, i_ItemId ASC", strFilterExpression);
            grdData.DataBind();
        }

        private int GetTotalCount()
        {
            OperationResult objOperationResult = new OperationResult();
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            return _objBL.GetDataHierarchiesCount(ref objOperationResult, strFilterExpression);
      
        }

        private List<DataHierarchyList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            List <DataHierarchyList> _objData = _objBL.GetDataHierarchiesPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression,0);

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }

            return _objData;
        }

        protected void grdData_RowCommand(object sender, GridCommandEventArgs e)
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
            int intGroupId = Convert.ToInt32(grdData.DataKeys[grdData.SelectedRowIndex][0]);
            int intParameterId = Convert.ToInt32(grdData.DataKeys[grdData.SelectedRowIndex][1]);

            // Obtener el usuario autenticado
            int intUserPersonId = ((ClientSession)Session["objClientSession"]).i_SystemUserId;
            int contador;

            //Vemos si el Grupo tiene hijos
            string strFilterExpression = string.Format("i_GroupId={0} && i_IsDeleted=0", intParameterId);
            contador = _objBL.GetDataHierarchiesCount(ref objOperationResult, strFilterExpression);
            if (contador > 0)
            {
                Alert.Show("¡El grupo que está tratando de eliminar tiene parámetros！", MessageBoxIcon.Warning);
            }
            else
            {
                // Delete the item
                _objBL.DeleteDataHierarchy(ref objOperationResult, intGroupId, intParameterId, ((ClientSession)Session["objClientSession"]).GetAsList());
                Session["strFilterExpression"] = "i_GroupId==0 && i_IsDeleted==0";
            }         
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtParameterIdFilter.Text)) Filters.Add("i_ItemId==" + txtParameterIdFilter.Text.Trim().ToUpper());
            if (!string.IsNullOrEmpty(txtDescriptionFilter.Text)) Filters.Add("v_Value1.Contains(\"" + txtDescriptionFilter.Text.Trim().ToUpper() + "\")");
            Filters.Add("i_GroupId==0 && (i_IsDeleted==0 || i_IsDeleted==null)");
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

        protected void grdData_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
            //WindowField wfEdit1 = grdData.FindColumn("myWindowField") as WindowField;
            //wfEdit1.Enabled = _Util.IsActionEnabled("FRM011_EDIT");

            //WindowField wfEdit2 = grdData.FindColumn("myWindowFieldChildren") as WindowField;
            //wfEdit2.Enabled = _Util.IsActionEnabled("FRM011_VIEW");

            //LinkButtonField lbfDelete = grdData.FindColumn("lbfAction2") as LinkButtonField;
            //lbfDelete.Enabled = _Util.IsActionEnabled("FRM011_DELETE");
        }

    }
}