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

namespace Sigesoft.Server.WebClientAdmin.UI.Auditar
{
    public partial class FRM033L : System.Web.UI.Page
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
            if (Request.QueryString["i_ServiceComponentStatusId"] != null)
                Session["i_ServiceComponentStatusId"] = Request.QueryString["i_ServiceComponentStatusId"].ToString();

            if (Request.QueryString["v_ServiceComponentId"] != null)
                Session["v_ServiceComponentId"] = Request.QueryString["v_ServiceComponentId"].ToString();


            ddlStatuComponent.SelectedValue = Session["i_ServiceComponentStatusId"].ToString();

        }
        private void loadCombo()
        {
            OperationResult objOperationResult = new OperationResult();
            Utils.LoadDropDownList(ddlStatuComponent, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 127), DropDownListAction.Select);
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            //Actualizar Aptitud del Servicio
            //_oServiceBL.ActualizarEstadoComponente(ref objOperationResult,Session["v_ServiceComponentId"].ToString(),int.Parse(ddlStatuComponent.SelectedValue.ToString()),((ClientSession)Session["objClientSession"]).GetAsList());
            _oServiceBL.ActualizarEstadoComponentesPorCategoria(ref objOperationResult,int.Parse(Session["CategoriaId"].ToString()),Session["ServiceId"].ToString(),int.Parse(ddlStatuComponent.SelectedValue.ToString()),((ClientSession)Session["objClientSession"]).GetAsList());

            
            var alert = _oServiceBL.GetServiceComponentsCulminados(ref objOperationResult, Session["ServiceId"].ToString());

            if (alert != null && alert.Count > 0)
            {

            }
            else
            {

                //if (cbAptitudEso.SelectedValue.ToString() == ((int)AptitudeStatus.SinAptitud).ToString())
                //{
                Alert.ShowInTop("Todos los Examenes se encuentran concluidos.\nEl estado de la Atención es: En espera de Aptitud .", "INFORMACIÓN!");
                    serviceDto objserviceDto = new serviceDto();
                    objserviceDto = _oServiceBL.GetService(ref objOperationResult, Session["ServiceId"].ToString());
                    objserviceDto.i_ServiceStatusId = (int)ServiceStatus.EsperandoAptitud;
                    objserviceDto.v_Motive = "Esperando Aptitud";
                    _oServiceBL.UpdateService(ref objOperationResult, objserviceDto, ((ClientSession)Session["objClientSession"]).GetAsList());
                //}
                //else
                //{
                //    MessageBox.Show("El servicio ha concluido correctamente.", "INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    serviceDto objserviceDto = new serviceDto();
                //    objserviceDto = objServiceBL.GetService(ref objOperationResult, _serviceId);
                //    objserviceDto.i_ServiceStatusId = (int)ServiceStatus.Culminado;
                //    objserviceDto.v_Motive = "Culminado";
                //    objServiceBL.UpdateService(ref objOperationResult, objserviceDto, Globals.ClientSession.GetAsList());
                //}




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