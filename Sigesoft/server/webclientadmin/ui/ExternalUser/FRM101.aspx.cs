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
namespace Sigesoft.Server.WebClientAdmin.UI.ExternalUser
{
    public partial class FRM101 : System.Web.UI.Page
    {

        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        ProtocolBL _objProtocolBL = new ProtocolBL();
        ServiceBL _ServiceBL = new ServiceBL();
        CalendarBL _CalendarBL = new CalendarBL();
        OrganizationBL oOrganizationBL = new OrganizationBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OperationResult objOperationResult = new OperationResult();
                dpFechaInicio.SelectedDate = DateTime.Now.AddDays(-1); //DateTime.Parse("25/07/2014");
                dpFechaFin.SelectedDate = DateTime.Now;// DateTime.Parse("25/07/2014");
                LoadComboBox();
                btnHojaRuta.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("../ExternalUser/FRMHojaRuta.aspx");
                btnReagendar.OnClientClick = Window1.GetSaveStateReference(hfRefresh.ClientID) + Window1.GetShowReference("../ExternalUser/FRMReagenda.aspx");
                btnEliminarAgenda.OnClientClick = Window2.GetSaveStateReference(hfRefresh.ClientID) + Window2.GetShowReference("../ExternalUser/FRMCancelarAgenda.aspx");


            }
        }

        private void LoadComboBox()
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlTipoESO, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 118), DropDownListAction.All);

            Utils.LoadDropDownList(ddlEmpresa, "Value1", "Id", oOrganizationBL.GetAllOrganizations(ref objOperationResult), DropDownListAction.All);
            var o = _objProtocolBL.DevolverProtocolosPorEmpresaOnly("-1");
            Utils.LoadDropDownList(ddlProtocolo, "Value1", "Id", o, DropDownListAction.Select);
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddlTipoESO.SelectedValue.ToString() != "-1") Filters.Add("i_TypeEsoId==" + ddlTipoESO.SelectedValue);
            if (!string.IsNullOrEmpty(txtTrabajador.Text)) Filters.Add("v_Trabajador.Contains(\"" + txtTrabajador.Text.ToUpper().Trim() + "\")");
            if (ddlProtocolo.SelectedValue.ToString() != "-1") Filters.Add("v_ProtocolId==" + "\"" + ddlProtocolo.SelectedValue + "\"");
            if (!string.IsNullOrEmpty(txtHCL.Text)) Filters.Add("v_HCL==" + "\"" + txtHCL.Text + "\"");

            if (ddlEmpresa.SelectedValue.ToString() != "-1")
            {
                var id3 = ddlEmpresa.SelectedValue.ToString().Split('|');
                Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"");
            }

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

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            var l = GetData(grdData.PageIndex, grdData.PageSize, "v_ServiceId", strFilterExpression);
            grdData.DataSource = l;
            grdData.DataBind();

            Session["GrillaAgenda"] = l;
        }

        private List<CalendarList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _CalendarBL.GetCalendarsPagedAndFiltered_(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value.AddDays(1));
            lblContador.Text = "Se encontraron " + _objData.Count().ToString() + " registros";
            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }

            return _objData;
        }

        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProtocolBL oProtocolBL = new ProtocolBL();
            if (ddlEmpresa.SelectedValue.ToString() != "-1")
            {
                var x = ddlEmpresa.SelectedValue.ToString().Split('|');
                var o = oProtocolBL.DevolverProtocolosPorEmpresa(x[0].ToString(), x[1].ToString());

                Utils.LoadDropDownList(ddlProtocolo, "v_Name", "v_ProtocolId", o, DropDownListAction.Select);
            }
        }

        protected void grdData_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {

        }

        protected void grdData_RowClick(object sender, FineUI.GridRowClickEventArgs e)
        {

            LlenarLista();
        }

        private List<MyListWeb> LlenarLista()
        {
            List<MyListWeb> lista = new List<MyListWeb>();
            int selectedCount = grdData.SelectedRowIndexArray.Length;

            for (int i = 0; i < selectedCount; i++)
            {
                int rowIndex = grdData.SelectedRowIndexArray[i];
                var dataKeys = grdData.DataKeys[rowIndex];
                lista.Add(new MyListWeb
                {
                    IdServicio = dataKeys[0].ToString(),
                    CalendarId = dataKeys[1].ToString(),
                    ProtocolId = dataKeys[2].ToString(),
                    IdPaciente = dataKeys[3].ToString(),
                });

            }

            Session["objLista"] = lista;

            return lista;
        }

        protected void winEdit1_Close(object sender, WindowCloseEventArgs e)
        {

        }

        protected void grdData_RowDataBound(object sender, GridRowEventArgs e)
        {
            CalendarList row = (CalendarList)e.DataItem;




            int val = row.i_CalendarStatusId;

            if (val == 4)
            {
                highlightRows.Text += e.RowIndex.ToString() + ",";
            }

        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            btnFilter_Click(sender, e);
        }
    }
}