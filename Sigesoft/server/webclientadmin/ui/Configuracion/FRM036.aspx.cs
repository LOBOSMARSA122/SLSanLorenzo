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
    public partial class FRM036 : System.Web.UI.Page
    {
        SystemParameterBL _oSystemParameterBL = new SystemParameterBL();
        ProtocolBL _oProtocolBL = new ProtocolBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnNuevoProtocolo.OnClientClick = winEdit.GetSaveStateReference(hfRefresh.ClientID) + winEdit.GetShowReference("FRM036A.aspx?Mode=New");
              
                LoadCombos();
                btnFilter_Click(sender, e);
            }
        }

        private void LoadCombos()
        {
            OperationResult objOperationResult = new OperationResult();
            var dataListOrganization2 = _oSystemParameterBL.GetJoinOrganizationAndLocation(ref objOperationResult, 10);

            Utils.LoadDropDownList(ddlEmpresaCliente,
            "Value1",
            "Id",
            dataListOrganization2,
            DropDownListAction.All);
        }

        protected void grdData_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            if (e.CommandName == "ClonAction")
            {
                ClonAction();
                BindGrid();
            }
        }

        private void ClonAction()
        {
            OperationResult objOperationResult = new OperationResult();
            protocolDto _protocolDTO = new protocolDto();
            ProtocolBL _protocolBL = new ProtocolBL();
            protocolcomponentDto oprotocolcomponentDto = null;
            List<protocolcomponentDto> _protocolcomponentListDTO = new List<protocolcomponentDto>();

            // Obtener los IDs de la fila seleccionada
            string ProtocolId = grdData.DataKeys[grdData.SelectedRowIndex][0].ToString();

            _protocolDTO = _protocolBL.GetProtocol(ref objOperationResult, ProtocolId);
            _protocolDTO.v_Name = _protocolDTO.v_Name + "_Copia";
            _protocolDTO.v_ProtocolId = null;
            // Componentes del protocolo
            var dataListPc = _protocolBL.GetProtocolComponents(ref objOperationResult, ProtocolId);

            foreach (var item in dataListPc)
            {
                oprotocolcomponentDto = new protocolcomponentDto();

                oprotocolcomponentDto.v_ProtocolComponentId = item.v_ProtocolComponentId;
                oprotocolcomponentDto.v_ProtocolId = item.v_ProtocolId;
                oprotocolcomponentDto.v_ComponentId = item.v_ComponentId;
                oprotocolcomponentDto.r_Price = item.r_Price;
                oprotocolcomponentDto.i_OperatorId = item.i_OperatorId;
                oprotocolcomponentDto.i_Age = item.i_Age;
                oprotocolcomponentDto.i_GenderId = item.i_GenderId;
                oprotocolcomponentDto.i_IsConditionalId = item.i_IsConditionalId;
                oprotocolcomponentDto.i_IsDeleted = item.i_IsDeleted;
                //oprotocolcomponentDto.i_InsertUserId = item.i_InsertUserId;
                //oprotocolcomponentDto.d_InsertDate = item.d_InsertDate;
                //oprotocolcomponentDto.i_UpdateUserId = item.i_UpdateUserId;
                oprotocolcomponentDto.d_UpdateDate = item.d_UpdateDate;
                oprotocolcomponentDto.i_IsConditionalIMC = item.i_IsConditionalIMC;
                oprotocolcomponentDto.r_Imc = item.r_Imc;
                oprotocolcomponentDto.i_IsAdditional = item.i_isAdditional;
                _protocolcomponentListDTO.Add(oprotocolcomponentDto);
            }


            _protocolBL.AddProtocol(ref objOperationResult, _protocolDTO, _protocolcomponentListDTO, ((ClientSession)Session["objClientSession"]).GetAsList());


        }

       protected void btnFilter_Click(object sender, EventArgs e)
        {
            // Get the filters from the UI
            List<string> Filters = new List<string>();
            if (ddlEmpresaCliente.SelectedValue.ToString() != "-1") Filters.Add("v_OrganizationId==" + "\"" + ddlEmpresaCliente.SelectedValue + "\"");
            if (!string.IsNullOrEmpty(txtNombreProtocolo.Text)) Filters.Add("v_Protocol.Contains(\"" + txtNombreProtocolo.Text.Trim() + "\")");
 
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
            grdData.RecordCount = GetTotalCount();
            grdData.DataSource = GetData(grdData.PageIndex, grdData.PageSize, "v_Protocol", strFilterExpression);
            grdData.DataBind();
        }

        private List<Sigesoft.Node.WinClient.BE.ProtocolList> GetData(int pintPageIndex, int? pintPageSize, string pstrSortExpression, string pstrFilterExpression)
        {
            OperationResult objOperationResult = new OperationResult();
            var dataList = _oProtocolBL.GetProtocolPagedAndFiltered(ref objOperationResult, pintPageIndex, pintPageSize, pstrSortExpression, pstrFilterExpression);

            
            return dataList;
        }

        protected void winEdit_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }

        protected void grdData_PageIndexChange(object sender, GridPageEventArgs e)
        {
            grdData.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        private int GetTotalCount()
        {
            OperationResult objOperationResult = new OperationResult();
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            return _oProtocolBL.GetProtocolCount(ref objOperationResult, strFilterExpression);
        }
    }
}