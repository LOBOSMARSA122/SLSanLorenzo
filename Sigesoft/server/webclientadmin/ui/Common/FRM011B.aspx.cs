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
    public partial class FRM011B : System.Web.UI.Page
    {
        DataHierarchyBL _objBL = new DataHierarchyBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                btnNew.OnClientClick = winEdit.GetSaveStateReference(hfRefresh.ClientID) + winEdit.GetShowReference(string.Format("FRM011A.aspx?Mode=NewChildren&i_GroupId={0}", txtParameterId.Text));
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            }
        }

        private void LoadData()
        {
            OperationResult objOperationResult = new OperationResult();
            //Llenado de combos
            Utils UtilComboBox = new Utils();
           
            string Mode = Request.QueryString["Mode"].ToString();
            int GroupId = -1, ItemId = -1;

            if (Request.QueryString["i_GroupId"] != null)
                GroupId = int.Parse(Request.QueryString["i_GroupId"].ToString());
           
            if (Request.QueryString["i_ItemId"] != null)
                ItemId = int.Parse(Request.QueryString["i_ItemId"].ToString());
            ViewState["ItemId"] = ItemId;
            ViewState["strFilterExpression"] = null;

            if (Mode == "Edit")
            {
                // Bloquear algunos campos
              
                txtParameterId.Enabled = false;
                txtDescription.Enabled = false;
                // Get the Entity Data
                datahierarchyDto objEntity = _objBL.GetDataHierarchy(ref objOperationResult, GroupId, ItemId);
              
                // Save the entity on the session
                Session["objEntity"] = objEntity;

                // Show the data on the form
                txtParameterId.Text = objEntity.i_ItemId.ToString();
                txtDescription.Text = objEntity.v_Value1;
                         
                BindGrid();
            }
        }

        protected void grdData_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            grdData.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        private void BindGrid()
        {
            //string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            string strFilterExpression = Convert.ToString(ViewState["strFilterExpression"]);
            grdData.RecordCount = GetTotalCount();
            grdData.DataSource = GetData(grdData.PageIndex, grdData.PageSize, "i_GroupId ASC, i_ItemId ASC", strFilterExpression);
            grdData.DataBind();
        }

        private int GetTotalCount()
        {
            OperationResult objOperationResult = new OperationResult();
            //string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            //string strFilterExpression = Convert.ToString(ViewState["strFilterExpression"]);
          int   ItemId = int.Parse(Request.QueryString["i_ItemId"].ToString());
          string strFilterExpression = string.Format("i_GroupId=={0} && i_IsDeleted==0", ItemId);
            return _objBL.GetDataHierarchiesCount(ref objOperationResult, strFilterExpression);
        }

        private List<DataTreeViewForGridView> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
           int ItemId = int.Parse(Request.QueryString["i_ItemId"].ToString());

            //Llenar _objData para Grilla Treeview

           //DataHierarchyList[] _objData = _objProxy.GetDataHierarchiesPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, ItemId);
           List<DataTreeViewForGridView> _objData = _objBL.GetDataHierarchyForGridView(ref objOperationResult, ItemId).ToList();
     
            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }

            return _objData;
        }

        protected void winEdit_Close(object sender, EventArgs e)
        {
            BindGrid();
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
            // Obtener los IDs de la fila seleccionada
            int intGroupId = Convert.ToInt32(grdData.DataKeys[grdData.SelectedRowIndex][0]);
            int intParameterId = Convert.ToInt32(grdData.DataKeys[grdData.SelectedRowIndex][1]);

            // Obtener el usuario autenticado
            int intUserPersonId = ((ClientSession)Session["objClientSession"]).i_SystemUserId;

            // Delete the item
            OperationResult objOperationResult = new OperationResult();
            _objBL.DeleteDataHierarchy(ref objOperationResult, intGroupId, intParameterId, ((ClientSession)Session["objClientSession"]).GetAsList());
        }
    }
}