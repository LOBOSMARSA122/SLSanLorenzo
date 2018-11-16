using FineUI;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.UI;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sigesoft.Server.WebClientAdmin.UI.Configuracion
{
    public partial class FRMOrdenReporte : System.Web.UI.Page
    {
        OrganizationBL oOrganizationBL = new OrganizationBL();
        OperationResult objOperationResult = new OperationResult();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OperationResult objOperationResult = new OperationResult();
                var Lista = oOrganizationBL.GetOrdenReportes(ref objOperationResult, Request.QueryString["v_OrganizationId"].ToString());
                if (Lista.Count > 0)
                {
                    List<OrdenReportes> ListaCompletaReportes = new List<OrdenReportes>();
                    ListaCompletaReportes = oOrganizationBL.GetAllOrdenReporteNuevo(ref objOperationResult, 0, null, "", "");

                    foreach (var ListaReportes in ListaCompletaReportes)
                    {
                        foreach (var item in Lista)
                        {
                            if (item.v_ComponenteId == ListaReportes.v_ComponenteId)
                            {
                                ListaReportes.v_OrdenReporteId = item.v_OrdenReporteId;
                                ListaReportes.b_Seleccionar = true;
                                ListaReportes.v_ComponenteId = item.v_ComponenteId;
                                ListaReportes.v_NombreReporte = item.v_NombreReporte;
                                ListaReportes.i_Orden = item.i_Orden.Value;
                                ListaReportes.v_NombreCrystal = item.v_NombreCrystal;
                                ListaReportes.i_NombreCrystalId = item.i_NombreCrystalId == null ? (int?)null : item.i_NombreCrystalId.Value;
                            }
                        }
                    }
                    ListaCompletaReportes.Sort((x, y) => x.i_Orden.Value.CompareTo(y.i_Orden.Value));
                    grdData.DataSource = ListaCompletaReportes;
                    grdData.DataBind();
                    btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
                    
                }
                else 
                {
                    bindgridNew();
                }
            }
        }

        private void bindgridNew()
        {
            OperationResult objOperationResult = new OperationResult();
            var Lista = oOrganizationBL.GetAllOrdenReporteNuevo(ref objOperationResult, 0, null, "", "");
            grdData.DataSource = Lista;
            grdData.DataBind();
        }

        protected void grdData_RowDataBound(object sender, FineUI.GridRowEventArgs e)
        {

        }

        protected void grdData_Sort(object sender, FineUI.GridSortEventArgs e)
        {

        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            ordenreporteDto oordenreporteDto = null;
            List<ordenreporteDto> ListaOrdem = new List<ordenreporteDto>();
            int n = 0;
                //Eliminar Antiguos Registros
            oOrganizationBL.DeleteOrdenReportes(ref objOperationResult, Request.QueryString["v_OrganizationId"].ToString());  
            foreach (var row in grdData.Rows)
            {
                if (((FineUI.CheckBoxField)grdData.FindColumn("b_Seleccionar")).GetCheckedState(n))
                { 
                    oordenreporteDto = new ordenreporteDto();
                    oordenreporteDto.i_Orden = int.Parse(((System.Web.UI.WebControls.TextBox)row.FindControl("i_Orden")).Text);
                    oordenreporteDto.v_OrganizationId = Request.QueryString["v_OrganizationId"].ToString();
                    oordenreporteDto.v_NombreReporte = row.Values[1];
                    oordenreporteDto.v_ComponenteId = row.Values[2];
                    oordenreporteDto.v_NombreCrystal = row.Values[4] == null ? "" : row.Values[4];
                    oordenreporteDto.i_NombreCrystalId = row.Values[5] == null || row.Values[5] == "" ? (int?)null : int.Parse(row.Values[4]);
                    ListaOrdem.Add(oordenreporteDto);         
                }
                n++;
            }
            n = 0;
            oOrganizationBL.AddOrdenReportes(ref objOperationResult, ListaOrdem, ((ClientSession)Session["objClientSession"]).GetAsList());
            ActiveWindow.GetConfirmHideReference();
            
        }
    }
}