
using FineUI;
using NetPdf;
using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace Sigesoft.Server.WebClientAdmin.UI.Estadisticas
{
    public partial class DXGRUPOETARIO : System.Web.UI.Page
    {
        OrganizationBL oOrganizationBL = new OrganizationBL();
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        ServiceBL _serviceBL = new ServiceBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OperationResult objOperationResult = new OperationResult();
                DataHierarchyBL _objDataHierarchyBL = new DataHierarchyBL();
                dpFechaInicio.SelectedDate = DateTime.Now.AddDays(-1); //DateTime.Parse("25/07/2014");
                dpFechaFin.SelectedDate = DateTime.Now;// DateTime.Parse("25/07/2014");

                var TipoUsuario = ((ClientSession)Session["objClientSession"]).i_SystemUserTypeId;

                if (TipoUsuario == 1)
                {
                    Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", oOrganizationBL.GetAllOrganizations(ref objOperationResult), DropDownListAction.All);              
                }
                else
                {
                    var RolId = ((ClientSession)Session["objClientSession"]).i_RoleId;
                    if (RolId == 1)
                    {
                        var clientOrganization = oOrganizationBL.GetJoinOrganizationAndLocationALL(ref objOperationResult);
                        Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.Select);
                    }
                    else
                    {
                        var clientOrganization = oOrganizationBL.GetJoinOrganizationAndLocation(ref objOperationResult, ((ClientSession)Session["objClientSession"]).i_SystemUserId);
                        Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.Select);
                    }
                }
               

                Utils.LoadDropDownList(ddlProtocolo, "Value1", "Id", oOrganizationBL.GetProtocolsByOrganizationForCombo(ref objOperationResult, "-1", "-1", null), DropDownListAction.Select);
                Utils.LoadDropDownList(ddlConsultorio, "Description", "Description2", _objDataHierarchyBL.GetDataHierarchyForCombo(ref objOperationResult, 124), DropDownListAction.Select);
                Utils.LoadDropDownList(ddlGrupoEtario, "Value1", "Id", BLL.Utils.GetSystemParameterForCombo(ref objOperationResult, 224, null), DropDownListAction.Select);

                Utils.LoadDropDownList(ddlGESO, "Value1", "Id", BLL.Utils.GetGESO(ref objOperationResult, null), DropDownListAction.Select);
                Utils.LoadDropDownList(ddlTipoESO, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 118), DropDownListAction.Select);
                Utils.LoadDropDownList(ddlGenero, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 100), DropDownListAction.Select);
                List<Sigesoft.Node.WinClient.BE.DiagnosticsByAgeGroup> l = new List<Sigesoft.Node.WinClient.BE.DiagnosticsByAgeGroup>();

            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            List<string> Filters = new List<string>();
            DateTime? pdatBeginDate = dpFechaInicio.SelectedDate.Value;
            DateTime? pdatEndDate = dpFechaFin.SelectedDate.Value;

            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
                Filters.Add("EmpresaId==" + "\"" + id3[0] + "\"&&SedeId==" + "\"" + id3[1] + "\"");
                Session["_IdEmpresaClienete"] = id3[0];
            }

            if (ddlProtocolo.SelectedValue.ToString() != "-1")
                Filters.Add("ProtocoloId==" + "\"" + ddlProtocolo.SelectedValue + "\"");

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
            Session["strFilterExpression"] = strFilterExpression;

            ShowReport(pdatBeginDate, pdatEndDate);
        }

        private void ShowReport(DateTime? beginDate, DateTime? endDate)
        {
            List<ReportEstadistica> ListaCordenadas = new List<ReportEstadistica>();
            OperationResult objOperationResult = new OperationResult();

            if (ddlConsultorio.SelectedValue == "APTITUD")//Aptitud
            {
                var l = _serviceBL.GraficaEstadisticaAptitud(ref objOperationResult, Session["strFilterExpression"].ToString(), beginDate, endDate, ddlConsultorio.SelectedValue, ddlGrupoEtario.SelectedValue, ddlGESO.SelectedValue, ddlGenero.SelectedValue, int.Parse(txtTop.Text.ToString()));
              
                Chart1.Series["Default"].Points.DataBindXY(l, "DxNombre", l, "NroTrabajadores");
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

                var l_Detallada = _serviceBL.GraficaEstadisticaAptitudDetallada(ref objOperationResult, Session["strFilterExpression"].ToString(), beginDate, endDate, ddlConsultorio.SelectedValue, ddlGrupoEtario.SelectedValue, ddlGESO.SelectedValue, ddlGenero.SelectedValue, int.Parse(txtTop.Text.ToString()));

                grdData.DataSource = l_Detallada;
                grdData.DataBind();

                ReportEstadistica oReportEstadistica = null;
                int NroDx = l.Sum(p => p.NroTrabajadores);

                var objData = l_Detallada.AsEnumerable()
                           .GroupBy(x => x.Trabajador)
                           .Select(group => group.First());

                int NroPacientes = objData.Count();
                foreach (var item in l)
                {
                    oReportEstadistica = new ReportEstadistica();
                    oReportEstadistica.DxNombre = item.DxNombre;
                    oReportEstadistica.NroTrabajadores = item.NroTrabajadores;
                    Decimal a = ((decimal.Parse(item.NroTrabajadores.ToString()) / decimal.Parse(NroDx.ToString())) * 100);
                    oReportEstadistica.P_Parc = (a).ToString("#.##") + " %";
                    oReportEstadistica.P_Total = ((decimal.Parse(item.NroTrabajadores.ToString()) / decimal.Parse(NroPacientes.ToString())) * 100).ToString("#.##") + "%";
                    ListaCordenadas.Add(oReportEstadistica);
                }


                Grid1.DataSource = ListaCordenadas;
                Grid1.DataBind();

            }
            else if (ddlConsultorio.SelectedValue == "GRUPO ETARIO")
            {
                var l = _serviceBL.GraficaEstadisticaGrupoEtario(ref objOperationResult, Session["strFilterExpression"].ToString(), beginDate, endDate, ddlConsultorio.SelectedValue, ddlGrupoEtario.SelectedValue, ddlGESO.SelectedValue, ddlGenero.SelectedValue, int.Parse(txtTop.Text.ToString()));

                Chart1.Series["Default"].Points.DataBindXY(l, "DxNombre", l, "NroTrabajadores");
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;


                var l_Detallada = _serviceBL.GraficaEstadisticaGrupoEtario_Detallada(ref objOperationResult, Session["strFilterExpression"].ToString(), beginDate, endDate, ddlConsultorio.SelectedValue, ddlGrupoEtario.SelectedValue, ddlGESO.SelectedValue, ddlGenero.SelectedValue, int.Parse(txtTop.Text.ToString()));

                grdData.DataSource = l_Detallada;
                grdData.DataBind();

                ReportEstadistica oReportEstadistica = null;
                int NroDx = l.Sum(p => p.NroTrabajadores);

                var objData = l_Detallada.AsEnumerable()
                           .GroupBy(x => x.Trabajador)
                           .Select(group => group.First());

                int NroPacientes = objData.Count();
                foreach (var item in l)
                {
                    oReportEstadistica = new ReportEstadistica();
                    oReportEstadistica.DxNombre = item.DxNombre;
                    oReportEstadistica.NroTrabajadores = item.NroTrabajadores;
                    Decimal a = ((decimal.Parse(item.NroTrabajadores.ToString()) / decimal.Parse(NroDx.ToString())) * 100);
                    oReportEstadistica.P_Parc = (a).ToString("#.##") + " %";
                    oReportEstadistica.P_Total = ((decimal.Parse(item.NroTrabajadores.ToString()) / decimal.Parse(NroPacientes.ToString())) * 100).ToString("#.##") + "%";
                    ListaCordenadas.Add(oReportEstadistica);
                }

                Grid1.DataSource = ListaCordenadas;
                Grid1.DataBind();
            }
            else if (ddlConsultorio.SelectedValue == "GENERO")
            {
                var l = _serviceBL.GraficaEstadisticaGenero(ref objOperationResult, Session["strFilterExpression"].ToString(), beginDate, endDate, ddlConsultorio.SelectedValue, ddlGrupoEtario.SelectedValue, ddlGESO.SelectedValue, ddlGenero.SelectedValue, int.Parse(txtTop.Text.ToString()));

                Chart1.Series["Default"].Points.DataBindXY(l, "DxNombre", l, "NroTrabajadores");
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;


                var l_Detallada = _serviceBL.GraficaEstadisticaGenero_Detallada(ref objOperationResult, Session["strFilterExpression"].ToString(), beginDate, endDate, ddlConsultorio.SelectedValue, ddlGrupoEtario.SelectedValue, ddlGESO.SelectedValue, ddlGenero.SelectedValue, int.Parse(txtTop.Text.ToString()));

                grdData.DataSource = l_Detallada;
                grdData.DataBind();

                ReportEstadistica oReportEstadistica = null;
                int NroDx = l.Sum(p => p.NroTrabajadores);

                var objData = l_Detallada.AsEnumerable()
                           .GroupBy(x => x.Trabajador)
                           .Select(group => group.First());

                int NroPacientes = objData.Count();
                foreach (var item in l)
                {
                    oReportEstadistica = new ReportEstadistica();
                    oReportEstadistica.DxNombre = item.DxNombre;
                    oReportEstadistica.NroTrabajadores = item.NroTrabajadores;
                    Decimal a = ((decimal.Parse(item.NroTrabajadores.ToString()) / decimal.Parse(NroDx.ToString())) * 100);
                    oReportEstadistica.P_Parc = (a).ToString("#.##") + " %";
                    oReportEstadistica.P_Total = ((decimal.Parse(item.NroTrabajadores.ToString()) / decimal.Parse(NroPacientes.ToString())) * 100).ToString("#.##") + "%";
                    ListaCordenadas.Add(oReportEstadistica);
                }

                Grid1.DataSource = ListaCordenadas;
                Grid1.DataBind();
            }
            else
            {
                var l = _serviceBL.GraficaEstadistica(ref objOperationResult, Session["strFilterExpression"].ToString(), beginDate, endDate, ddlConsultorio.SelectedValue, ddlGrupoEtario.SelectedValue, ddlGESO.SelectedValue, ddlGenero.SelectedValue, int.Parse(txtTop.Text.ToString()));
                var lDr = _serviceBL.GraficaEstadistica_(ref objOperationResult, Session["strFilterExpression"].ToString(), beginDate, endDate, ddlConsultorio.SelectedValue, ddlGrupoEtario.SelectedValue, ddlGESO.SelectedValue, ddlGenero.SelectedValue, int.Parse(txtTop.Text.ToString()));

                List<string> listaDR = new List<string>();
                foreach (var item in lDr)
                {
                    //lDx.Add(item.DxNombre);
                    listaDR.Add(item.DiagnosticrepositoryId);
                }
                var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');

                var lDetallada = _serviceBL.GraficaEstadisticaDetallada_(ref objOperationResult, listaDR);

                Chart1.Series["Default"].Points.DataBindXY(l, "DxNombre", l, "NroTrabajadores");
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

                grdData.DataSource = lDetallada;
                grdData.DataBind();

                ReportEstadistica oReportEstadistica = null;
                int NroDx = l.Sum(p => p.NroTrabajadores);

                var objData = lDr.AsEnumerable()
                           .GroupBy(x => x.Trabajador)
                           .Select(group => group.First());

                int NroPacientes = objData.Count();
                foreach (var item in l)
                {
                    oReportEstadistica = new ReportEstadistica();
                    oReportEstadistica.DxNombre = item.DxNombre;
                    oReportEstadistica.NroTrabajadores = item.NroTrabajadores;
                    Decimal a = ((decimal.Parse(item.NroTrabajadores.ToString()) / decimal.Parse(NroDx.ToString())) * 100);
                    oReportEstadistica.P_Parc = (a).ToString("#.##") + " %";
                    oReportEstadistica.P_Total = ((decimal.Parse(item.NroTrabajadores.ToString()) / decimal.Parse(NroPacientes.ToString())) * 100).ToString("#.##") + "%";
                    ListaCordenadas.Add(oReportEstadistica);
                }


                Grid1.DataSource = ListaCordenadas;
                Grid1.DataBind();
            }

         
        }

        protected void ddlCustomerOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlCustomerOrganization.SelectedValue == null)
                return;

            if (ddlCustomerOrganization.SelectedValue.ToString() == "-1")
            {
                ddlProtocolo.SelectedValue = "-1";
                ddlProtocolo.Enabled = false;
                return;
            }
            ddlProtocolo.Enabled = true;
            OperationResult objOperationResult = new OperationResult();
            var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
            Utils.LoadDropDownList(ddlProtocolo, "Value1", "Id", oOrganizationBL.GetProtocolsByOrganizationForCombo(ref objOperationResult, id3[0], id3[1], null), DropDownListAction.All);

            LoadcbGESO();
        }

        private void LoadcbGESO()
        {
            var index = ddlCustomerOrganization.SelectedIndex;

            if (index == 0 || index == -1)
            {
                OperationResult objOperationResult = new OperationResult();
                Utils.LoadDropDownList(ddlGESO, "Value1", "Id", BLL.Utils.GetGESO(ref objOperationResult, null), DropDownListAction.Select);
                return;
            }

            var dataList = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
            string idOrg = dataList[0];
            string idLoc = dataList[1];

            OperationResult objOperationResult1 = new OperationResult();
            Utils.LoadDropDownList(ddlGESO, "Value1", "Id", BLL.Utils.GetGESOByOrgIdAndLocationId(ref objOperationResult1, idOrg, idLoc), DropDownListAction.Select);
        }

        protected void aspButtonOI_Click(object sender, EventArgs e)
        {
            List<string> Filters = new List<string>();
            DateTime? pdatBeginDate = dpFechaInicio.SelectedDate.Value;
            DateTime? pdatEndDate = dpFechaFin.SelectedDate.Value;

            if (ddlCustomerOrganization.SelectedValue.ToString() == "-1")
            {
                Alert.Show("Seleccionar Empresa");
                return;
            }

            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
                Filters.Add("EmpresaId==" + "\"" + id3[0] + "\"&&SedeId==" + "\"" + id3[1] + "\"");
                Session["_IdEmpresaClienete"] = id3[0];
            }

            if (ddlProtocolo.SelectedValue.ToString() != "-1")
                Filters.Add("ProtocoloId==" + "\"" + ddlProtocolo.SelectedValue + "\"");

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
            Session["strFilterExpression"] = strFilterExpression;

            ShowReport(pdatBeginDate, pdatEndDate);
        }

        protected void ddlTipoESO_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            if (ddlTipoESO.SelectedValue.ToString() == "-1")
            {
                return;
            }
            int TipoESOId = int.Parse(ddlTipoESO.SelectedValue.ToString());

            if (ddlCustomerOrganization.SelectedValue == null)
                return;

            if (ddlCustomerOrganization.SelectedValue.ToString() == "-1")
            {
                ddlProtocolo.SelectedValue = "-1";
                ddlProtocolo.Enabled = false;
                return;
            }
            ddlProtocolo.Enabled = true;
            var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');
            Utils.LoadDropDownList(ddlProtocolo, "Value1", "Id", oOrganizationBL.GetProtocolsByOrganizationForCombo_(ref objOperationResult, id3[0], id3[1], TipoESOId, null), DropDownListAction.All);


            //Utils.LoadDropDownList(ddlProtocolo, "Value1", "Id", oOrganizationBL.GetProtocolsByOrganizationForCombo_(ref objOperationResult, "-1", "-1", TipoESOId, ""), DropDownListAction.All);

        }


    }
}