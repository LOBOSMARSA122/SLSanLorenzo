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
    public partial class FRM035B : System.Web.UI.Page
    {
        OrganizationBL _oOrganizationBL = new OrganizationBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnNew.OnClientClick = winEdit.GetSaveStateReference(hfRefresh.ClientID) + winEdit.GetShowReference("FRM035B_1.aspx?Mode=New");
                LoadData();
                BindGrid();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            }
        }

        private void BindGrid()
        {
            grdData.DataSource = GetData(grdData.PageIndex, grdData.PageSize, "", "v_OrganizationId==" + "\"" + Session["v_OrganizationId"].ToString() + "\"");
            grdData.DataBind();
        }

        private List<Sigesoft.Node.WinClient.BE.LocationList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _oOrganizationBL.GetLocationPagedAndFiltered(ref objOperationResult, 0, null, "V_Name ASC", pstrFilterExpression);

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

            BindGrid();

        }

        protected void grdData_PageIndexChange(object sender, GridPageEventArgs e)
        {

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
            string LocationId = grdData.DataKeys[grdData.SelectedRowIndex][0].ToString();
            
            // Delete the item
            OperationResult objOperationResult = new OperationResult();
            _oOrganizationBL.DeleteLocation(ref objOperationResult, LocationId, ((ClientSession)Session["objClientSession"]).GetAsList());
        }

        protected void winEdit_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
    }
}