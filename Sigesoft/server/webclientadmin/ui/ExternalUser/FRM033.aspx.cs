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
using System.Web.Services;
using System.Web.Script.Services;
using CrystalDecisions.Shared.Json;
using Newtonsoft.Json;
using Sigesoft.Server.WebClientAdmin.BE.Custom;

namespace Sigesoft.Server.WebClientAdmin.UI.ExternalUser
{
    public partial class FRM031_ : System.Web.UI.Page
    {
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        ProtocolBL _objProtocolBL = new ProtocolBL();
        ServiceBL _ServiceBL = new ServiceBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                OperationResult objOperationResult = new OperationResult();
                dpFechaInicio.SelectedDate = DateTime.Now.AddDays(-1); //DateTime.Parse("25/07/2014");
                dpFechaFin.SelectedDate = DateTime.Now;
                LoadComboBox();
            }

            
        }


        //protected void grdData_Updated(object sender, EventArgs e)
        //{
        //    grdData.DataBind();
        //}

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            List<string> Filters = new List<string>();

            if (ddlEmpresa.SelectedValue.ToString() != "-1")
            {
                Filters.Add("v_CustomerOrganizationId==" + "\"" + ddlEmpresa.SelectedValue + "\"");

            }

            

            string strFilterExpression = null;
            if (Filters.Count > 0)
            {
                foreach (string item in Filters)
                {
                    strFilterExpression = strFilterExpression + item + " && ";
                }
                strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);
                Session["strFilterExpression"] = strFilterExpression;

                //grdData.PageIndex = 0;
                this.BindGrid();

            }
            
        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            //grdData.DataSource = GetData(strFilterExpression);
            //grdData.DataBind();            
        }

        private string organizationId = "";

        protected void SetValue(object sender, EventArgs e)
        {
            organizationId = ddlEmpresa.SelectedValue.ToString();
        }

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static List<Sigesoft.Node.WinClient.BE.MatrizExcel> ImprimirTabla(string FechaInicio, string FechaFin, string OrganizationId)
        {

            if (OrganizationId != "-1")
            {
                List<string> Filters = new List<string>();
                Filters.Add("v_CustomerOrganizationId==" + "\"" + OrganizationId + "\"");
                string strFilterExpression = null;
                if (Filters.Count > 0)
                {
                    foreach (string item in Filters)
                    {
                        strFilterExpression = strFilterExpression + item + " && ";
                    }
                    strFilterExpression = strFilterExpression.Substring(0, strFilterExpression.Length - 4);


                }
                var _objData = new ProtocolBL().ReporteMatrizExcel(DateTime.Parse(FechaInicio), DateTime.Parse(FechaFin), OrganizationId, strFilterExpression);

                return _objData;
            }

            return null;
            //if (ddlEmpresa.SelectedValue.ToString() != "-1")
            //{
            //    var fechInicio = dpFechaInicio.SelectedDate.Value;
            //    Filters.Add("v_CustomerOrganizationId==" + "\"" + ddlEmpresa.SelectedValue + "\"");
            //    
            //    return fechInicio;  

            //}

            //return FechaInicio + "|" + FechaFin + "|" + OrganizationId;
        }

        private List<Sigesoft.Node.WinClient.BE.MatrizExcel> GetData(string pstrFilterExpression)
        {
            try
            {
                OperationResult objOperationResult = new OperationResult();
                var _objData = _objProtocolBL.ReporteMatrizExcel(dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value, ddlEmpresa.SelectedValue.ToString(), pstrFilterExpression);
                return _objData;   
            }
            catch (Exception ex)
            {               
                throw;
            }

        }

        private void LoadComboBox()
        {
            OperationResult objOperationResult = new OperationResult();

            var UsuarioMaster = ((ClientSession)Session["objClientSession"]).v_UserName;
            if (UsuarioMaster == "ricardo.rueda.sj")
            {
                var ObtenerEmpresasCliente = new ProtocolBL().DevolverTodasEmpresas(ref objOperationResult);
                //txtEmpresa.Text = ObtenerEmpresasCliente.CustomerOrganizationName;
                Session["EmpresaClienteId"] = ObtenerEmpresasCliente[0].IdEmpresaCliente;
                Utils.LoadDropDownList(ddlEmpresa, "CustomerOrganizationName", "IdEmpresaCliente", ObtenerEmpresasCliente, DropDownListAction.All);
            }
            else
            {

                var ObtenerEmpresasCliente = new ProtocolBL().GetOrganizationCustumerByProtocolSystemUser(ref objOperationResult, ((ClientSession)Session["objClientSession"]).i_SystemUserId);
                //txtEmpresa.Text = ObtenerEmpresasCliente.CustomerOrganizationName;
                Session["EmpresaClienteId"] = ObtenerEmpresasCliente[0].IdEmpresaCliente;
                Utils.LoadDropDownList(ddlEmpresa, "CustomerOrganizationName", "IdEmpresaCliente", ObtenerEmpresasCliente, DropDownListAction.All);
                //ddlEmpresa.Enabled = false;
                //ddlEmpresa.se
            }


        }
    }
}