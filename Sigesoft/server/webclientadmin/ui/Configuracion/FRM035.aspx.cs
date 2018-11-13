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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.IO;

namespace Sigesoft.Server.WebClientAdmin.UI.Configuracion
{
    public partial class FRM035 : System.Web.UI.Page
    {
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        ProtocolBL _oProtocolBL = new ProtocolBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnEmpresa.OnClientClick = winEdit.GetSaveStateReference(hfRefresh.ClientID) + winEdit.GetShowReference("FRM035A.aspx?Mode=New");
                LoadCombos();
                btnFilter_Click(sender, e);
            }
        }

        private void LoadCombos()
        {
            OperationResult objOperationResult = new OperationResult();
            OrganizationBL oOrganizationBL = new OrganizationBL();
             Utils.LoadDropDownList(ddlTipoEmpresa, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 103), DropDownListAction.All);
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddlTipoEmpresa.SelectedValue.ToString() != "-1") Filters.Add("i_OrganizationTypeId==" + ddlTipoEmpresa.SelectedValue);
           if (!string.IsNullOrEmpty(txtIdentificationNumber1.Text)) Filters.Add("v_IdentificationNumber==" + "\"" + txtIdentificationNumber1.Text.Trim() + "\"");
            if (!string.IsNullOrEmpty(txtName1.Text)) Filters.Add("v_Name.Contains(\"" + txtName1.Text.Trim() + "\")");
            Filters.Add("i_IsDeleted==0");

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

        private int GetTotalCount()
        {
            OperationResult objOperationResult = new OperationResult();
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            return _oProtocolBL.GetEmpresasCount(ref objOperationResult, strFilterExpression);
        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            grdData.RecordCount = GetTotalCount();
            grdData.DataSource = GetData(grdData.PageIndex, grdData.PageSize, "v_Name", strFilterExpression);
            grdData.DataBind();
        }

        private List<Sigesoft.Node.WinClient.BE.OrganizationList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _oProtocolBL.GetOrganizationsPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }

            return _objData;
        }

        protected void grdData_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {

        }

        protected void grdData_PreRowDataBound(object sender, FineUI.GridPreRowEventArgs e)
        {

        }

        protected void grdData_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            grdData.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void winEdit_Close_Empresa(object sender, WindowCloseEventArgs e)
        {
            Session["strFilterExpression"] = "";
            BindGrid();

        }

        protected void winEditSede_Close(object sender, WindowCloseEventArgs e)
        {

        }

        protected void winEditGESO_Close(object sender, WindowCloseEventArgs e)
        {

        }

        protected void winEditOrdenReporte_Close(object sender, WindowCloseEventArgs e)
        {

        }
    }
}