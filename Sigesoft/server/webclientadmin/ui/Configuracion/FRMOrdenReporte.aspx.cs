using Sigesoft.Common;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OrganizationBL oOrganizationBL = new OrganizationBL();
                OperationResult objOperationResult = new OperationResult();

                grdData.DataSource = oOrganizationBL.GetAllOrdenReporteNuevo(ref objOperationResult, 0, null, "", "");
                grdData.DataBind();
            }
           
        }

        protected void grdData_RowDataBound(object sender, FineUI.GridRowEventArgs e)
        {

        }

        protected void grdData_Sort(object sender, FineUI.GridSortEventArgs e)
        {

        }
    }
}