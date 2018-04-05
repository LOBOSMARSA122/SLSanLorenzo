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
using Sigesoft.Server.WebClientAdmin.BE;
using FineUI;

namespace Sigesoft.Server.WebClientAdmin.UI.ExternalUser
{
    public partial class FRMCancelarAgenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            }
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            CalendarBL objCalendarBL = new CalendarBL();
            OperationResult objOperationResult = new OperationResult();


            List<MyListWeb> ListaServicios = (List<MyListWeb>)Session["objLista"];
            if (ListaServicios == null)
            {
                Alert.Show("Seleccione un registro");
                return;
            }
            else
            {
                foreach (var item in ListaServicios)
                {
                    calendarDto objCalendarDto = new calendarDto();

                    objCalendarDto = objCalendarBL.GetCalendar(ref objOperationResult, item.CalendarId);
                   objCalendarDto.v_CalendarId = item.CalendarId;
                   objCalendarDto.i_CalendarStatusId = 4;//Cancelado
                   objCalendarDto.i_LineStatusId = 2;//Fuera de Circuito

                   objCalendarBL.UpdateCalendar(ref objOperationResult, objCalendarDto, ((ClientSession)Session["objClientSession"]).GetAsList());
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
}