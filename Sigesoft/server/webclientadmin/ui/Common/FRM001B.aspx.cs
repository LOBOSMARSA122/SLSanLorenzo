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
    public partial class FRM001B : System.Web.UI.Page
    {
        SystemParameterBL _objBL = new SystemParameterBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                btnNew.OnClientClick = winEdit.GetSaveStateReference(hfRefresh.ClientID) + winEdit.GetShowReference(string.Format("FRM001A.aspx?Mode=NewChildren&i_GroupId={0}", txtParameterId.Text));
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();                
            }
        }

        private void LoadData()
        {
            OperationResult objOperationResult = new OperationResult();
            //Llenado de combos
            Utils UtilComboBox = new Utils();
           
            string Mode = Request.QueryString["Mode"].ToString();
            int GroupId = -1, ParameterId = -1;

            if (Request.QueryString["i_GroupId"] != null)
                GroupId = int.Parse(Request.QueryString["i_GroupId"].ToString());
            if (Request.QueryString["i_ParameterId"] != null)
                ParameterId = int.Parse(Request.QueryString["i_ParameterId"].ToString());
            ViewState["strFilterExpression"] = string.Format("i_GroupId={0} && i_IsDeleted==0", ParameterId);
          
            if (Mode == "Edit")
            {
                // Obtener el usuario autenticado
                int intUserPersonId = ((ClientSession)Session["objClientSession"]).i_SystemUserId;

                // Bloquear algunos campos
                txtParameterId.Enabled = false;
                txtDescription.Enabled = false;
                // Get the Entity Data

                systemparameterDto objEntity = _objBL.GetSystemParameter(ref objOperationResult, GroupId, ParameterId);

                // Save the entity on the session
                Session["objEntity"] = objEntity;

                // Show the data on the form
                txtParameterId.Text = objEntity.i_ParameterId.ToString();
                txtDescription.Text = objEntity.v_Value1;         
                BindGrid();
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
            string strFilterExpression = Convert.ToString(ViewState["strFilterExpression"]);
            grdData.RecordCount = GetTotalCount();
            grdData.DataSource = GetData(grdData.PageIndex, grdData.PageSize, "i_GroupId ASC, i_ParameterId ASC", strFilterExpression );
            grdData.DataBind();
        }

        private int GetTotalCount()
        {
            OperationResult objOperationResult = new OperationResult();
            //string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            string strFilterExpression = Convert.ToString(ViewState["strFilterExpression"]);
            return _objBL.GetSystemParametersCount(ref objOperationResult, strFilterExpression);
        }

        private List<DataTreeViewForGridViewSP> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            int ItemId = int.Parse(Request.QueryString["i_ParameterId"].ToString());
            List<DataTreeViewForGridViewSP> _objData = _objBL.GetSystemParameterForGridView(ref objOperationResult, ItemId).ToList();

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
            // Obtener los IDs de la fila seleccionada
            int intGroupId = Convert.ToInt32(grdData.DataKeys[grdData.SelectedRowIndex][0]);
            int intParameterId = Convert.ToInt32(grdData.DataKeys[grdData.SelectedRowIndex][1]);

            // Obtener el usuario autenticado
            int intUserPersonId = ((ClientSession)Session["objClientSession"]).i_SystemUserId;

            // Delete the item
            OperationResult objOperationResult = new OperationResult();
            _objBL.DeleteSystemParameter(ref objOperationResult, intGroupId, intParameterId, ((ClientSession)Session["objClientSession"]).GetAsList());
        }

    }
}
