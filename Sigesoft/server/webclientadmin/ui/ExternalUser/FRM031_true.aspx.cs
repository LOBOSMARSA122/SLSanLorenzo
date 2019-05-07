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
    public partial class FRM031 : System.Web.UI.Page
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
                dpFechaFin.SelectedDate = DateTime.Now;// DateTime.Parse("25/07/2014");
                LoadComboBox();
                btnNewCertificado.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("../ExternalUser/FRM031J.aspx");
                btnExAltura.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("../ExternalUser/FRM031K.aspx");
                btnPsico.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("../ExternalUser/FRM031_PSICO.aspx");
                btnToxi.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("../ExternalUser/FRM031M.aspx");
                btnNewFichaOcupacional.OnClientClick = winEdit2.GetSaveStateReference(hfRefresh.ClientID) + winEdit2.GetShowReference("../ExternalUser/FRM031H.aspx");
                btnFMT1.OnClientClick = Window2.GetSaveStateReference(hfRefresh.ClientID) + Window2.GetShowReference("../ExternalUser/FRMFMT1.aspx");
                btnInterConsulta.OnClientClick = Window2.GetSaveStateReference(hfRefresh.ClientID) + Window2.GetShowReference("../ExternalUser/FRMINTERC.aspx");
                //btnNewExamenes.OnClientClick = winEdit3.GetSaveStateReference(hfRefresh.ClientID) + winEdit3.GetShowReference("../ExternalUser/FRM031E.aspx");

                ////Session["CertificadoAptitud"] = false;
                ////Session["FichaOcupacional"] = false;
            }
        }
        
        private void LoadComboBox()
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlAptitud, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 124), DropDownListAction.All);
            Utils.LoadDropDownList(ddlTipoESO, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 118), DropDownListAction.All);
            Utils.LoadDropDownList(ddlProtocolo, "Value1", "Id", _objProtocolBL.GetProtocolBySystemUser(ref objOperationResult, ((ClientSession)Session["objClientSession"]).i_SystemUserId), DropDownListAction.Select );

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

        protected void grdData_RowCommand(object sender, GridCommandEventArgs e)
        {
                
        }
               
        protected void grdData_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
        }       

        protected void ddlProtocolo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtEmpresa.Text = _objProtocolBL.GetNameOrganizationCustomer(ddlProtocolo.SelectedValue.ToString());
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {

            if (ddlProtocolo.SelectedValue == "-1")
            {
                //Alert.ShowInTop("Por favor seleccionar un protocolo");
                //return;
            }

            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddlTipoESO.SelectedValue.ToString() != "-1") Filters.Add("i_TypeEsoId==" + ddlTipoESO.SelectedValue);
            if (ddlAptitud.SelectedValue.ToString() != "-1") Filters.Add("i_AptitudeId==" + ddlAptitud.SelectedValue);
            if (!string.IsNullOrEmpty(txtTrabajador.Text)) Filters.Add("v_Trabajador.Contains(\"" + txtTrabajador.Text.ToUpper().Trim() + "\")");            
            if (ddlProtocolo.SelectedValue.ToString() != "-1") Filters.Add("v_ProtocolId==" + "\"" + ddlProtocolo.SelectedValue + "\"");
            if (!string.IsNullOrEmpty(txtHCL.Text)) Filters.Add("v_HCL==" + "\"" + txtHCL.Text + "\"");
            if (ddlEmpresa.SelectedValue.ToString() != "-1") Filters.Add("v_CustomerOrganizationId==" + "\"" + ddlEmpresa.SelectedValue + "\"");
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
                btnNewCertificado.Enabled = (bool)Session["CertificadoAptitud"];
                btnNewFichaOcupacional.Enabled = (bool)Session["FichaOcupacional"];

                btnExAltura.Enabled = (bool)Session["ExamenAltura"];
                btnFMT1.Enabled = (bool)Session["FMT1"];
                btnInterConsulta.Enabled = (bool)Session["Interconsultas"];
                btnToxi.Enabled = true;
                //btnFMT1.Enabled = (bool)Session["CertificadoAptitud"];
                if (selectedCount > 1)
                {
                    btnNewFichaOcupacional.Enabled = false;
                    btnNewCertificado.Enabled = false;
                    btnFMT1.Enabled = false;
                    btnInterConsulta.Enabled = false;
                    btnExAltura.Enabled = false;
                }
                else
                {
                    btnNewFichaOcupacional.Enabled = (bool)Session["FichaOcupacional"];
                    btnNewCertificado.Enabled = (bool)Session["CertificadoAptitud"];
                    
                    btnExAltura.Enabled = (bool)Session["ExamenAltura"];
                    btnFMT1.Enabled = (bool)Session["FMT1"];
                    btnInterConsulta.Enabled = (bool)Session["Interconsultas"];

                    int rowIndex = grdData.SelectedRowIndexArray[0];
                    var dataKeys = grdData.DataKeys[rowIndex];
                    var components = _ServiceBL.GetServiceComponentByServiceIdAndComponentId(dataKeys[0].ToString(), "N009-ME000000303");
                    if (components != null)
                        btnToxi.Enabled = true;
                    else
                        btnToxi.Enabled = false;
                }
                
            }
            else
            {
                btnNewCertificado.Enabled = false;
                btnFMT1.Enabled = false;
                btnNewFichaOcupacional.Enabled = false;
                btnInterConsulta.Enabled = false;
                btnExAltura.Enabled = false;
                btnToxi.Enabled = false;
            }

            if (grdData.SelectedRowIndexArray.Length == 0)
            {
                List<MyListWeb> lista1 = new List<MyListWeb>();
                return lista1;
            }
            int rowIndex_ = grdData.SelectedRowIndexArray[0];

            var dataKeys_ = grdData.DataKeys[rowIndex_];

            if (dataKeys_[5] == null || dataKeys_[5].ToString() == "0")
            {
                btnInterConsulta.Enabled = false;
            }
            else
            {
                btnInterConsulta.Enabled = true;
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
                        IdPaciente = dataKeys[1].ToString(),
                        EmpresaCliente = dataKeys[2].ToString(),
                        Paciente = dataKeys[3].ToString(),
                    });
                    
                //}

            }

            Session["objLista"] = lista;

            return lista;
        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            grdData.DataSource = GetData(grdData.PageIndex, grdData.PageSize, "v_ServiceId", strFilterExpression);
            grdData.DataBind();
        }

        private List<ServiceList> GetData(int pintPageIndex, int pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var _objData = _ServiceBL.GetService(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression,dpFechaInicio.SelectedDate.Value,dpFechaFin.SelectedDate.Value.AddDays(1));

            if (ddlEmpresa.SelectedValue.ToString() == "-1")
            {
                 var ObtenerEmpresasCliente = new ProtocolBL().GetOrganizationCustumerByProtocolSystemUser(ref objOperationResult, ((ClientSession)Session["objClientSession"]).i_SystemUserId);
              
                List<string> l = new List<string>();

                foreach (var item in ObtenerEmpresasCliente)
	            {
		            l.Add(item.IdEmpresaCliente);
	            }

               var x = _objData.FindAll(p => l.Contains(p.v_CustomerOrganizationId)).ToList();
                lblContador.Text = "Se encontraron " + x.Count().ToString() + " registros";
                if (objOperationResult.Success != 1)
                {
                    Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                }


                return x;
            }
            else
            {
                lblContador.Text = "Se encontraron " + _objData.Count().ToString() + " registros";
                if (objOperationResult.Success != 1)
                {
                    Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                }


                return _objData;
            }
         
        }

        protected void grdData_RowClick(object sender, GridRowClickEventArgs e)
        {
             LlenarLista();
        }

        protected void winEdit1_Close(object sender, EventArgs e)
        {
           
        }

        protected void winEdit2_Close(object sender, EventArgs e)
        {
            if (Session["EliminarArchivo"] != null )
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

        protected void Window2_Close(object sender, EventArgs e)
        {
            if (Session["EliminarArchivo"] != null)
            {
                File.Delete(Session["EliminarArchivo"].ToString());
            }
            
        }

        
      }
}