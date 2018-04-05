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


namespace Sigesoft.Server.WebClientAdmin.UI.Servicios
{
    public partial class FRMCAMBIOFECHASERVICIO : System.Web.UI.Page
    {
        ServiceBL _oServiceBL = new ServiceBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            }         
        }

        private void LoadData()
        {
            //setear dato de aptitud
            //if (Request.QueryString["v_ServiceId"] != null)
            //    Session["ServiceId"] = Request.QueryString["v_ServiceId"].ToString();
            //if (Request.QueryString["d_ServiceDate"] != null)
            //    Session["d_ServiceDate"] = Request.QueryString["d_ServiceDate"].ToString();

            //    dpFechaServicio.SelectedDate = DateTime.Parse(Session["d_ServiceDate"].ToString());

            dpFechaServicio.SelectedDate = DateTime.Now;
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();


            var servicios = (List<MyListWeb>)Session["objListaCambioFecha"];

            foreach (var item in servicios)
            {
                //Actualizar Aptitud del Servicio
                _oServiceBL.ActualizarFechaServicio(ref objOperationResult,item.IdServicio , dpFechaServicio.SelectedDate, ((ClientSession)Session["objClientSession"]).GetAsList());

            }
            
         
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