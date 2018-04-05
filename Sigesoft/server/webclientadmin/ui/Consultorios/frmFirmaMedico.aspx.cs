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
using Sigesoft.Server.WebClientAdmin.BE;

namespace Sigesoft.Server.WebClientAdmin.UI.Consultorios
{
    public partial class frmFirmaMedico : System.Web.UI.Page
    {
        SystemParameterBL oSystemParameterBL = new SystemParameterBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlUsuarioGrabar, "Value1", "Id", oSystemParameterBL.GetProfessional(ref objOperationResult, ""), DropDownListAction.Select);
     
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {

        }
    }
}