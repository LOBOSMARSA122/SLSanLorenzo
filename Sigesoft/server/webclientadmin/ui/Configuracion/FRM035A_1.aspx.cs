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

namespace Sigesoft.Server.WebClientAdmin.UI.Configuracion
{
    public partial class FRM035A_1 : System.Web.UI.Page
    {
        ServiceBL oServiceBL = new ServiceBL();
        public Sigesoft.Node.WinClient.BE.CodigoEmpresaList _objCodigoEmpresaList = new Sigesoft.Node.WinClient.BE.CodigoEmpresaList();
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
            codigoempresaDto objDiseaseDto = new codigoempresaDto();
            codigoempresaDto objDiseaseDto1 = new codigoempresaDto();


            if (Session["v_CodigoEmpresaId"] != null)
                {
                    objDiseaseDto = oServiceBL.GetCodigoEmpresa(ref  objOperationResult, Session["v_CodigoEmpresaId"].ToString());

                    objDiseaseDto.v_CIIUId = Session["v_CIIUId"].ToString();
                    objDiseaseDto.v_Name = txtDxModificado.Text;
                    oServiceBL.UpdateCodigoEmpresa(ref objOperationResult, objDiseaseDto, ((ClientSession)Session["objClientSession"]).GetAsList());

                    _objCodigoEmpresaList.v_CodigoEmpresaId = objDiseaseDto.v_CodigoEmpresaId;
                    _objCodigoEmpresaList.v_CIIUId = objDiseaseDto.v_CIIUId;
                   Session["SectoName"] = objDiseaseDto.v_Name;
                }
                else
                {
                    objDiseaseDto.v_CIIUId = Session["v_CIIUId"].ToString();
                    objDiseaseDto.v_Name = txtDxModificado.Text;


                    objDiseaseDto1 = oServiceBL.GetIsValidateCodigoEmpresa(ref objOperationResult, objDiseaseDto.v_Name);

                    if (objDiseaseDto1 == null)
                    {
                        objDiseaseDto.v_CodigoEmpresaId = oServiceBL.AddCodigoEmpresa(ref objOperationResult, objDiseaseDto, ((ClientSession)Session["objClientSession"]).GetAsList());
                    }
                    else
                    {
                        Alert.ShowInTop("Error en Escoja uno que tenga código interno:");                       
                        return;
                    }


                    //objDiseaseDto.v_CodigoEmpresaId=  oServiceBL.AddDiseases(ref objOperationResult, objDiseaseDto, Globals.ClientSession.GetAsList());

                    _objCodigoEmpresaList.v_CodigoEmpresaId = objDiseaseDto.v_CodigoEmpresaId;
                    _objCodigoEmpresaList.v_CIIUId = Session["v_CIIUId"].ToString();
                    Session["SectoName"] = txtDxModificado.Text;
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

        protected void btnCie10_Click(object sender, EventArgs e)
        {
            List<string> Filters = new List<string>();
            if (!string.IsNullOrEmpty(txtDx.Text)) Filters.Add("v_CIIUDescription1.Contains(\"" + txtDx.Text.Trim() + "\")");
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

        private void BindGrid()
        {
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            OperationResult objOperationResult = new OperationResult();
            grdCie10.RecordCount = GetTotalCount();
            grdCie10.DataSource = oServiceBL.GetCodigoEmpresaPagedAndFiltered(ref objOperationResult, grdCie10.PageIndex, grdCie10.PageSize, "v_CIIUId", strFilterExpression);
            grdCie10.DataBind();
        }
        private int GetTotalCount()
        {
            OperationResult objOperationResult = new OperationResult();
            string strFilterExpression = Convert.ToString(Session["strFilterExpression"]);
            return oServiceBL.GetCIIU(ref objOperationResult, strFilterExpression);
        }

        protected void grdCie10_PageIndexChange(object sender, GridPageEventArgs e)
        {
            grdCie10.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void grdCie10_RowClick(object sender, GridRowClickEventArgs e)
        {
            int index = e.RowIndex;
            var dataKeys = grdCie10.DataKeys[index];
            Session["v_CIIUId"] = dataKeys[0];
            Session["v_CodigoEmpresaId"] = dataKeys[2];
            txtDxModificado.Text = dataKeys[1].ToString();
        }

    }
}