using FineUI;
using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sigesoft.Server.WebClientAdmin.UI.Consultorios
{
    public partial class CIE10 : System.Web.UI.Page
    {
        ServiceBL oServiceBL = new ServiceBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            }
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            diseasesDto objDiseaseDto = new diseasesDto();
            diseasesDto objDiseaseDto1 = new diseasesDto();

            if (Session["DiseasesId"] != null)
            {
                objDiseaseDto = oServiceBL.GetDiseases(ref  objOperationResult, Session["DiseasesId"].ToString());

                objDiseaseDto.v_CIE10Id = Session["Cie10Id"].ToString();
                objDiseaseDto.v_Name = txtDxModificado.Text;
                Session["OtroDx"] = objDiseaseDto.v_Name;
                Session["OtroDxId"] = objDiseaseDto.v_DiseasesId;
                oServiceBL.UpdateDiseases(ref objOperationResult, objDiseaseDto, ((ClientSession)Session["objClientSession"]).GetAsList());

            }
            else
            {
                objDiseaseDto.v_CIE10Id = Session["Cie10Id"].ToString();
                objDiseaseDto.v_Name = txtDxModificado.Text;
                Session["OtroDx"] = objDiseaseDto.v_Name;
                
                objDiseaseDto1 = oServiceBL.GetIsValidateDiseases(ref objOperationResult, objDiseaseDto.v_Name);

                if (objDiseaseDto1 == null)
                {
                    objDiseaseDto.v_DiseasesId = oServiceBL.AddDiseases(ref objOperationResult, objDiseaseDto, ((ClientSession)Session["objClientSession"]).GetAsList());
                    Session["OtroDxId"] = objDiseaseDto.v_DiseasesId;
                }
                else
                {
                    Alert.Show("Escoja uno que tenga código interno", "Error de validación", MessageBoxIcon.Warning);
                    return;
                }
            }

            if (objOperationResult.Success == 1)  // Operación sin error
            {
                // Cerrar página actual y hacer postback en el padre para actualizar
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else  // Operación con error
            {
                Alert.ShowInTop("Error en operación:" + System.Environment.NewLine + objOperationResult.ExceptionMessage);
                // Se queda en el formulario.
            }
        }

        protected void grdCie10_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            grdCie10.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void grdCie10_RowClick(object sender, FineUI.GridRowClickEventArgs e)
        {
            int index = e.RowIndex;
            var dataKeys = grdCie10.DataKeys[index];
            Session["Cie10Id"] = dataKeys[0];
            Session["DiseasesId"] = dataKeys[2];
            txtDxModificado.Text = dataKeys[1].ToString();
        }

        protected void btnCie10_Click(object sender, EventArgs e)
        {
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtDx.Text)) Filters.Add("v_CIE10Idv_Name.Contains(\"" + txtDx.Text.Trim() + "\")");
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
            grdCie10.PageIndex = 0;
            this.BindGrid();
        }

        private int GetTotalCount()
        {
            OperationResult objOperationResult = new OperationResult();
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            return oServiceBL.GetCIE10Count(ref objOperationResult, strFilterExpression);
        }

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            OperationResult objOperationResult = new OperationResult();
            grdCie10.RecordCount = GetTotalCount();
            grdCie10.DataSource = oServiceBL.GetDiseasesPagedAndFiltered(ref objOperationResult, grdCie10.PageIndex, grdCie10.PageSize, "v_CIE10Id", strFilterExpression);
            grdCie10.DataBind();
        }
    }
}