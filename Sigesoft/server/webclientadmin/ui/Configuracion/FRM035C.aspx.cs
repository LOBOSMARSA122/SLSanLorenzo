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


namespace Sigesoft.Server.WebClientAdmin.UI.Configuracion
{
    public partial class FRM035C : System.Web.UI.Page
    {
        SystemParameterBL oSystemParameterBL = new SystemParameterBL();
        OrganizationBL _oOrganizationBL = new OrganizationBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnNew.OnClientClick = winEdit.GetSaveStateReference(hfRefresh.ClientID) + winEdit.GetShowReference("FRM035C_1.aspx?Mode=New");
               
                LoadData();
                LoadCombos();
                BindGrid(ddlLocation.SelectedValue);
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            }
        }

        private void LoadCombos()
        {
            OperationResult objOperationResult = new OperationResult();
            var x =BLL.Utils.GetLocation(ref objOperationResult, Session["v_OrganizationId"].ToString());
            Utils.LoadDropDownList(ddlLocation, "Value1", "Id",x , DropDownListAction.Select);
            if (x.Count > 0)
            {
                ddlLocation.SelectedIndex = 1;
            }
        }

        private void BindGrid(string pstrLocationId)
        {
            grdData.DataSource = GetData(grdData.PageIndex, grdData.PageSize, "", "v_LocationId==" + "\"" + pstrLocationId + "\"");
            grdData.DataBind();
        }

        private List<Sigesoft.Node.WinClient.BE.GroupOccupationList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _oOrganizationBL.GetGroupOccupationPagedAndFiltered(ref objOperationResult, 0, null, "V_Name ASC", pstrFilterExpression, Session["v_OrganizationId"].ToString());

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }

            return _objData;
        }

        private void LoadData()
        {
            if (Request.QueryString["v_OrganizationId"] != null)
                Session["v_OrganizationId"] = Request.QueryString["v_OrganizationId"].ToString();

            if (Request.QueryString["v_Name"] != null)
                txtEmpresa.Text = Request.QueryString["v_Name"].ToString();

            //BindGrid();

        }

        protected void grdData_PageIndexChange(object sender, GridPageEventArgs e)
        {

        }

        protected void grdData_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAction")
            {
                DeleteItem();
                BindGrid(ddlLocation.SelectedValue.ToString());
            }
        }

        private void DeleteItem()
        {
            // Obtener los IDs de la fila seleccionada
            string GESOId = grdData.DataKeys[grdData.SelectedRowIndex][0].ToString();

            // Delete the item
            OperationResult objOperationResult = new OperationResult();
            _oOrganizationBL.DeleteGroupOccupation(ref objOperationResult, GESOId, ((ClientSession)Session["objClientSession"]).GetAsList());
        }

        protected void winEdit_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid(ddlLocation.SelectedValue.ToString());
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid(ddlLocation.SelectedValue.ToString());
        }
    }
}