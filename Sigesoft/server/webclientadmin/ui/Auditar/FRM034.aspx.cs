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

namespace Sigesoft.Server.WebClientAdmin.UI.Auditar
{
    public partial class FRM044 : System.Web.UI.Page
    {
        string managementReports="";
        private string _serviceId;
        private string _EmpresaClienteId;
        private string _pacientId;
        private string _protocolId;
        private string _customerOrganizationName;
        private string _personFullName;
        private List<ServiceList> datasource; 

        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        ProtocolBL _objProtocolBL = new ProtocolBL();
        ServiceBL _ServiceBL = new ServiceBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OperationResult objOperationResult = new OperationResult();
                dpFechaInicio.SelectedDate = DateTime.Now.AddDays(-1); //DateTime.Parse("25/07/2014");
                dpFechaFin.SelectedDate = DateTime.Now;// DateTime.Parse("25/07/2014");
                LoadComboBox();
                //btnNewFichaOcupacional.OnClientClick = winEdit2.GetSaveStateReference(hfRefresh.ClientID) + winEdit2.GetShowReference("../ExternalUser/FRM031C.aspx");
                btnNewExamenes.OnClientClick = winEdit3.GetSaveStateReference(hfRefresh.ClientID) + winEdit3.GetShowReference("../ExternalUser/FRM031Z.aspx");
                btnNewCertificados.OnClientClick = Window2.GetSaveStateReference(hfRefresh.ClientID) + Window2.GetShowReference("../ExternalUser/FRM031X.aspx");
                btnOrdenReportes.OnClientClick = Window3.GetSaveStateReference(hfRefresh.ClientID) + Window3.GetShowReference(managementReports);

                Session["Examenes"] = false;
                Session["Certificados"] = false;
                //var ObtenerEmpresaCliente = new ProtocolBL().GetOrganizationCustumerByProtocolSystemUser(ref objOperationResult, ((ClientSession)Session["objClientSession"]).i_SystemUserId);
                //if (ObtenerEmpresaCliente != null )
                //{
                //    txtEmpresa.Text = ObtenerEmpresaCliente.CustomerOrganizationName;
                //    Session["EmpresaClienteId"] = ObtenerEmpresaCliente.IdEmpresaCliente;
                //}
              
             
            }
        }

        private void LoadComboBox()
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlAptitud, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 124), DropDownListAction.All);
            Utils.LoadDropDownList(ddlTipoESO, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 118), DropDownListAction.All);
            Utils.LoadDropDownList(ddlProtocolo, "Value1", "Id", _objProtocolBL.GetProtocolBySystemUser(ref objOperationResult, ((ClientSession)Session["objClientSession"]).i_SystemUserId), DropDownListAction.All);
            Utils.LoadDropDownList(ddlEmpresa, "Value1", "Id", _objSystemParameterBL.GetJoinOrganizationAndLocation(ref objOperationResult, ((ClientSession)Session["objClientSession"]).i_SystemUserId), DropDownListAction.All);
            
          
        }

        protected void grdData_RowCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void grdData_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
        }

        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmpresa.SelectedValue == null)
                return;

            if (ddlEmpresa.SelectedValue.ToString() == "-1")
            {
                ddlProtocolo.SelectedValue = "-1";
                ddlProtocolo.Enabled = false;
                return;
            }

            ddlProtocolo.Enabled = true;

            OperationResult objOperationResult = new OperationResult();

            var id3 = ddlEmpresa.SelectedValue.ToString().Split('|');

            //Utils.LoadDropDownList(ddlEsoType, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 118, null), DropDownListAction.All);
            //Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, id3[0], id3[1], null), DropDownListAction.All);          
            Utils.LoadDropDownList(ddlProtocolo, "Value1", "Id", _objSystemParameterBL.GetProtocolsByOrganizationForCombo(ref objOperationResult, id3[0], id3[1], null), DropDownListAction.All);
            
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {

            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddlTipoESO.SelectedValue.ToString() != "-1") Filters.Add("i_TypeEsoId==" + ddlTipoESO.SelectedValue);
            if (ddlAptitud.SelectedValue.ToString() != "-1") Filters.Add("i_AptitudeId==" + ddlAptitud.SelectedValue);
            if (!string.IsNullOrEmpty(txtTrabajador.Text)) Filters.Add("v_Pacient.Contains(\"" + txtTrabajador.Text.ToUpper().Trim() + "\")");
            if (ddlProtocolo.SelectedValue.ToString() != "-1") Filters.Add("v_ProtocolId==" + "\"" + ddlProtocolo.SelectedValue + "\"");
            if (!string.IsNullOrEmpty(txtHCL.Text)) Filters.Add("v_HCL==" + "\"" + txtHCL.Text + "\"");


            if (ddlEmpresa.SelectedValue.ToString() != "-1")
            {
                var id3 = ddlEmpresa.SelectedValue.ToString().Split('|');
                Filters.Add("v_CustomerOrganizationId==" + "\"" + id3[0] + "\"&&v_CustomerLocationId==" + "\"" + id3[1] + "\"");
            }


            //Filters.Add("v_CustomerOrganizationId==" + "\"" + Session["EmpresaClienteId"].ToString() + "\"");





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

        private List<MyListWeb> LlenarLista()
        {
            List<MyListWeb> lista = new List<MyListWeb>();
            int selectedCount = grdData.SelectedRowIndexArray.Length;
            if (selectedCount > 0)
            {

                //btnNewFichaOcupacional.Enabled = true;
                //btnNewExamenes.Enabled = true;     
                btnNewExamenes.Enabled = (bool)Session["Examenes"];
                btnNewCertificados.Enabled = (bool)Session["Certificados"];
                btnOrdenReportes.Enabled = (bool)Session["Certificados"];
                if (selectedCount > 1)
                {
                    //btnNewExamenes.Enabled = false;
                }
                else
                {
                    btnNewExamenes.Enabled = true;
                    btnNewCertificados.Enabled = true;
                    btnOrdenReportes.Enabled = true;
                    Session["Examenes"] = true;
                    Session["Certificados"] = true;
                }

            }
            else
            {
              
                //btnNewFichaOcupacional.Enabled = false;
                btnNewExamenes.Enabled = false;
                btnNewCertificados.Enabled = false;
                btnOrdenReportes.Enabled = false;
            }

            for (int i = 0; i < selectedCount; i++)
            {
                int rowIndex = grdData.SelectedRowIndexArray[i];

                var dataKeys = grdData.DataKeys[rowIndex];
                //for (int j = 0; j < dataKeys.Length; j++)
                //{
                //lista.Add( new MyListWeb< [0].ToString());
                lista.Add(new MyListWeb
                {
                    IdServicio = dataKeys[0].ToString(),
                    Paciente = dataKeys[1].ToString(),
                    EmpresaCliente = dataKeys[2].ToString(),
                });

                //}

            }

            Session["objLista"] = lista;

            return lista;
        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            var x = GetData(grdData.PageIndex, grdData.PageSize, "v_ServiceId", strFilterExpression);
            datasource = x;
            grdData.DataSource = x;            
            grdData.DataBind();          
        }

        private List<ServiceList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _ServiceBL.GetServicesForPrint(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression, dpFechaInicio.SelectedDate.Value, dpFechaFin.SelectedDate.Value.AddDays(1));
            lblContador.Text = "Se encontraron " + _objData.Count().ToString() + " registros";
            if (objOperationResult.Success != 1)
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
            }
            return _objData;
        }

        protected void grdData_RowClick(object sender, GridRowClickEventArgs e)
        {
            LlenarLista();
            int index = grdData.SelectedRowIndex;
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            List<ServiceList> data = GetData(grdData.PageIndex, grdData.PageSize, "v_ServiceId", strFilterExpression);
            foreach (var row in grdData.Rows)
            {
                if (row.RowIndex == index)
                {
                    _serviceId = row.Values[0];
                    _pacientId = row.Values[5];
                    _customerOrganizationName = row.Values[6];
                    _personFullName = row.Values[1];
                    _EmpresaClienteId = row.Values[4];
                }
            }
            int eso = 1;
            int flagPantalla = 2;
            managementReports = "../Auditar/FRMOrdenReportes.aspx?_serviceId=" + _serviceId + "&_pacientId=" + _pacientId + "&_customerOrganizationName=" + _customerOrganizationName + "&_personFullName=" + _personFullName + "&flagPantalla=" + flagPantalla + "&_EmpresaClienteId=" + _EmpresaClienteId + "&eso=" + eso + "";
            btnOrdenReportes.OnClientClick = Window3.GetSaveStateReference(hfRefresh.ClientID) + Window3.GetShowReference(managementReports);
            
        }

        protected void winEdit1_Close(object sender, EventArgs e)
        {

        }

        protected void winEdit2_Close(object sender, EventArgs e)
        {
            if (Session["EliminarArchivo"] != null)
            {
                File.Delete(Session["EliminarArchivo"].ToString());
            }
        }

        protected void winEdit3_Close(object sender, EventArgs e)
        {
            if (Session["EliminarArchivo"] != null)
            {
                File.Delete(Session["EliminarArchivo"].ToString());
            }

        }

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {

        }
    }
}