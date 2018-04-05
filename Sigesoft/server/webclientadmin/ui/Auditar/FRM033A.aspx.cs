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

namespace Sigesoft.Server.WebClientAdmin.UI.Auditar
{

    public partial class FRM033A : System.Web.UI.Page
    {
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        ServiceBL _oServiceBL = new ServiceBL();
      
        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (!IsPostBack)
            {
                loadCombo();
                LoadData();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            }          
        }

        private void LoadData()
        {
            //setear dato de aptitud
            if (Request.QueryString["v_ServiceId"] != null)
                Session["ServiceId"] = Request.QueryString["v_ServiceId"].ToString();
            if (Request.QueryString["i_AptitudeStatusId"] != null)
                Session["AptitudeStatusId"] = int.Parse(Request.QueryString["i_AptitudeStatusId"].ToString());
            if (Request.QueryString["v_ObsStatusService"] != null)
                Session["v_ObsStatusService"] = Request.QueryString["v_ObsStatusService"].ToString();

            ddlAptitud.SelectedValue = Session["AptitudeStatusId"].ToString();

            txtComentario.Text = Session["v_ObsStatusService"].ToString();
        }
        private void loadCombo()
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlAptitud, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 124), DropDownListAction.All);
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            //Actualizar Aptitud del Servicio
            _oServiceBL.ActualizarAptitudServicio(ref objOperationResult, Session["ServiceId"].ToString(), int.Parse(ddlAptitud.SelectedValue.ToString()),((ClientSession)Session["objClientSession"]).GetAsList(), txtComentario.Text,"","",-1);

            //Analizar el resultado de la operación
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
    }
}