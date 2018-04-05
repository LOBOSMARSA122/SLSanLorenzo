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
    public partial class frmEpp : System.Web.UI.Page
    {
        DataHierarchyBL _objDataHierarchyBL = new DataHierarchyBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OperationResult objOperationResult = new OperationResult();

                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                //Llenar combo ItemParameter Tree
                ddlEpps.DataTextField = "Description";
                ddlEpps.DataValueField = "Id";
                ddlEpps.DataSimulateTreeLevelField = "Level";
                ddlEpps.DataEnableSelectField = "EnabledSelect";
                List<DataForTreeView> t = _objDataHierarchyBL.GetSystemParameterForComboTree(ref objOperationResult, 146);
                ddlEpps.DataSource = t;
                ddlEpps.DataBind();
                this.ddlEpps.Items.Insert(0, new FineUI.ListItem("-- Seleccione --", "-1"));
                Session["v_TypeofEEPId"] = null;

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

        void ActualizarGrilla()
        {
            HistoryBL oHistoryBL = new HistoryBL();
            OperationResult objOperationResult = new OperationResult();
            grd.DataSource = oHistoryBL.GetTypeOfEEPPagedAndFiltered(ref objOperationResult, 0, null, "", "", Session["v_HistoryId"].ToString());
            grd.DataBind();

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            typeofeepDto objtypeofeepDto = new typeofeepDto();
            HistoryBL oHistoryBL = new HistoryBL();
            if (Session["v_TypeofEEPId"] == null)
            {
               
                objtypeofeepDto.i_TypeofEEPId = int.Parse(ddlEpps.SelectedValue.ToString());

                objtypeofeepDto.v_HistoryId = Session["v_HistoryId"].ToString();
                objtypeofeepDto.r_Percentage = txtPorcentaje11.Text == ""? (float?)null :int.Parse(txtPorcentaje11.Text);

                oHistoryBL.AddTypeOfEEPP(ref objOperationResult, objtypeofeepDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                Session["v_TypeofEEPId"] = null;
            }
            else
            {
                objtypeofeepDto.v_TypeofEEPId = Session["v_TypeofEEPId"].ToString();
                objtypeofeepDto.i_TypeofEEPId = int.Parse(ddlEpps.SelectedValue.ToString());

                objtypeofeepDto.v_HistoryId = Session["v_HistoryId"].ToString();
                objtypeofeepDto.r_Percentage = int.Parse(txtPorcentaje11.Text);
                oHistoryBL.UpdateTypeOfEEPP(ref objOperationResult, objtypeofeepDto, ((ClientSession)Session["objClientSession"]).GetAsList());

            }

            ddlEpps.SelectedValue = "-1";
            txtPorcentaje11.Text = "";            
            ActualizarGrilla();
        }

        protected void grd_RowCommand(object sender, GridCommandEventArgs e)
        {
            HistoryBL oHistoryBL = new HistoryBL();
            OperationResult objOperationResult = new OperationResult();
            if (e.CommandName == "DeleteRegistro")
            {
                string v_TypeofEEPId = grd.DataKeys[grd.SelectedRowIndex][0].ToString();
                oHistoryBL.DeleteTypeOfEEPP(ref objOperationResult, v_TypeofEEPId, ((ClientSession)Session["objClientSession"]).GetAsList());

                ActualizarGrilla();
            }
        }
    }
}