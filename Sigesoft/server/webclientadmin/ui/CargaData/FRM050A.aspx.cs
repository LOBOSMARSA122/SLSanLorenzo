using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BLL;
using System.Data;
using System.IO;
using System.Diagnostics;
using NetPdf;
using FineUI;


namespace Sigesoft.Server.WebClientAdmin.UI.CargaData
{
    public partial class FRM050A : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadCombo();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            }
        }

        private void loadCombo()
        {
            ProtocolBL oProtocolBL = new ProtocolBL();
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlProtocoloId, "v_Name", "Id", oProtocolBL.DevolverProtocolosPorEmpresaOnly(Session["EmpresaClienteId"].ToString()), DropDownListAction.Select);
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {

            Session["ProtocoloId"] = ddlProtocoloId.SelectedValue.ToString();
            Session["ProtocoloNombre"] = ddlProtocoloId.SelectedText;
          
                // Cerrar página actual y hacer postback en el padre para actualizar
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
         
        }
    }
}