using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bytescout.Spreadsheet;
using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;

namespace Sigesoft.Server.WebClientAdmin.UI.Consultorios
{
    public partial class frmPeligros : System.Web.UI.Page
    {
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        DataHierarchyBL _objDataHierarchyBL = new DataHierarchyBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OperationResult objOperationResult = new OperationResult();
                var Combo235 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 235);
                var Combo236 = _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 236);
                Utils.LoadDropDownList(ddlTiempo, "Value1", "Id", Combo235, DropDownListAction.Select);
                Utils.LoadDropDownList(ddlNivelRuido, "Value1", "Id", Combo236, DropDownListAction.Select);

                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                //Llenar combo ItemParameter Tree
                ddlPeligros.DataTextField = "Description";
                ddlPeligros.DataValueField = "Id";
                ddlPeligros.DataSimulateTreeLevelField = "Level";
                ddlPeligros.DataEnableSelectField = "EnabledSelect";
                List<DataForTreeView> t = _objDataHierarchyBL.GetSystemParameterForComboTree(ref objOperationResult, 145);
                ddlPeligros.DataSource = t;
                ddlPeligros.DataBind();
                this.ddlPeligros.Items.Insert(0, new FineUI.ListItem("-- Seleccione --", "-1"));
                Session["v_WorkstationDangersId"] = null;

                LoadData();
            }

        }

        private void LoadData()
        {
            string v_HistoryId = "";

            if (Request.QueryString["v_HistoryId"] != null)
                v_HistoryId = Request.QueryString["v_HistoryId"].ToString();

            Session["v_HistoryId"] = v_HistoryId;
            ActualizarGrilla();
        }
       

        protected void ddlPeligros_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            if (ddlPeligros.SelectedValue == "5")
            {
                ddlTiempo.SelectedValue = "-1";
                ddlNivelRuido.SelectedValue = "-1";
                txtFuenteRuido.Text = "";

                ddlTiempo.Enabled = true;
                txtFuenteRuido.Enabled = true;
                ddlNivelRuido.Enabled = true;
            }
            else
            {
                ddlTiempo.SelectedValue = "-1";
                ddlNivelRuido.SelectedValue = "-1";
                txtFuenteRuido.Text = "";
                ddlTiempo.Enabled = false;
                txtFuenteRuido.Enabled = false;
                ddlNivelRuido.Enabled = false;
            }
        }

        protected void grd_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            HistoryBL oHistoryBL = new HistoryBL();
            OperationResult objOperationResult = new OperationResult();
            if (e.CommandName == "DeleteRegistro")
            {
                string v_WorkstationDangersId = grd.DataKeys[grd.SelectedRowIndex][0].ToString();
                oHistoryBL.DeleteWorkstationDangers(ref objOperationResult, v_WorkstationDangersId, ((ClientSession)Session["objClientSession"]).GetAsList());

                ActualizarGrilla();
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            workstationdangersDto objworkstationdangersDto = new workstationdangersDto();
            HistoryBL oHistoryBL = new HistoryBL();
            if (Session["v_WorkstationDangersId"] == null)
            {
                //objworkstationdangersDto.v_WorkstationDangersId = NewId;
                objworkstationdangersDto.i_DangerId = int.Parse(ddlPeligros.SelectedValue.ToString());

                objworkstationdangersDto.v_HistoryId = Session["v_HistoryId"].ToString();
                objworkstationdangersDto.v_TimeOfExposureToNoise = txtFuenteRuido.Text;
                objworkstationdangersDto.i_NoiseLevel = int.Parse(ddlNivelRuido.SelectedValue.ToString());
                objworkstationdangersDto.i_NoiseSource = int.Parse(ddlTiempo.SelectedValue.ToString());

                oHistoryBL.AddWorkstationDangers(ref objOperationResult,objworkstationdangersDto,((ClientSession)Session["objClientSession"]).GetAsList());

                Session["v_WorkstationDangersId"] = null;
            }
            else
            {
                objworkstationdangersDto.v_WorkstationDangersId = Session["v_WorkstationDangersId"].ToString();
                objworkstationdangersDto.i_DangerId = int.Parse(ddlPeligros.SelectedValue.ToString());

                objworkstationdangersDto.v_HistoryId = Session["v_HistoryId"].ToString();
                objworkstationdangersDto.v_TimeOfExposureToNoise = txtFuenteRuido.Text;
                objworkstationdangersDto.i_NoiseLevel = int.Parse(ddlNivelRuido.SelectedValue.ToString());
                objworkstationdangersDto.i_NoiseSource = int.Parse(ddlTiempo.SelectedValue.ToString());
                oHistoryBL.UpdateWorkstationDangers(ref objOperationResult, objworkstationdangersDto, ((ClientSession)Session["objClientSession"]).GetAsList());

            }

            ddlPeligros.SelectedValue = "-1";
            ddlTiempo.SelectedValue = "-1";
            ddlNivelRuido.SelectedValue = "-1";
            txtFuenteRuido.Text = "";
            ddlTiempo.Enabled = false;
            txtFuenteRuido.Enabled = false;
            ddlNivelRuido.Enabled = false;
            ActualizarGrilla();
        }

        void ActualizarGrilla()
        {
            HistoryBL oHistoryBL = new HistoryBL();
            OperationResult objOperationResult = new OperationResult();
            grd.DataSource = oHistoryBL.GetWorkstationDangersagedAndFiltered(ref objOperationResult, 0, null, "", "", Session["v_HistoryId"].ToString());
            grd.DataBind();

        }

        
    }
}