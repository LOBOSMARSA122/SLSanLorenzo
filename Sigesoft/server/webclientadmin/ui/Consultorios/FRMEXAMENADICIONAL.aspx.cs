using FineUI;
using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Sigesoft.Server.WebClientAdmin.UI.Servicios
{
    public partial class FRMEXAMENADICIONAL : System.Web.UI.Page
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
            OperationResult objOperationResult = new OperationResult();
       
            if (Request.QueryString["v_ServiceId"] != null)
                Session["SerId"] = Request.QueryString["v_ServiceId"].ToString();

            //Listar Componentes Adicionales por Protocolo
            var lComponentes = _oServiceBL.GetAllComponents_(ref objOperationResult);

            var l = _oServiceBL.GetServiceComponentsByRequired_(ref objOperationResult, Session["SerId"].ToString(), SiNo.NO);

            foreach (var item in lComponentes)
            {
                foreach (var item1 in l)
                {
                    if (item1.v_ComponentId == item.v_ComponentId)
                    {
                        item.v_ComponentName = item1.v_ComponentName;
                        item.Adicional = item1.i_IsRequiredId == 0 ? false : true;
                        item.v_ServiceComponentId = item1.v_ServiceComponentId;
                        item.Flag = 1;
                    }
                   
                }
            }

            lComponentes.Sort((y, x) => x.Adicional.CompareTo(y.Adicional));

        grdData.DataSource = lComponentes;
         grdData.DataBind();

         Session["lComponentes_OLD"] = lComponentes;
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
            List<ServiceComponentList> lServiceComponentList = new List<ServiceComponentList>();
            ServiceBL _ObjServiceBL = new ServiceBL();
            CheckBoxField field1 = (CheckBoxField)grdData.FindColumn("CheckBoxField2");
            for (int i = 0; i < grdData.Rows.Count; i++)
            {
                servicecomponentDto objServiceComponentDto = new servicecomponentDto();

                bool ChecAdicional = field1.GetCheckedState(i);
                  var Flag =   grdData.Rows[i].Values[4].ToString();

                  if (ChecAdicional )
                {
                    objServiceComponentDto.v_ServiceId = Session["SerId"].ToString();
                    objServiceComponentDto.i_ExternalInternalId = 1;//interno
                    objServiceComponentDto.i_ServiceComponentTypeId = 1;
                    objServiceComponentDto.i_IsVisibleId = 1;
                    objServiceComponentDto.i_IsInheritedId = 0;
                    objServiceComponentDto.d_StartDate = null;
                    objServiceComponentDto.d_EndDate = null;
                    objServiceComponentDto.i_index = 1;
                    objServiceComponentDto.r_Price = 10;
                    objServiceComponentDto.v_ComponentId = grdData.Rows[i].Values[3];
                    objServiceComponentDto.i_IsInvoicedId = 0;
                    objServiceComponentDto.i_ServiceComponentStatusId = 1;// (int)Common.ServiceStatus.PorIniciar;
                    objServiceComponentDto.i_QueueStatusId = 1;// (int)Common.QueueStatusId.LIBRE;
                    objServiceComponentDto.i_Iscalling = 0;// (int)Common.Flag_Call.NoseLlamo;
                    objServiceComponentDto.i_IsManuallyAddedId = 0;// (int)Common.SiNo.NO;
                    objServiceComponentDto.i_IsRequiredId = 1;// (int)Common.SiNo.SI;
                    if (Flag == "0")
                    {
                        _ObjServiceBL.AddServiceComponent(ref objOperationResult, objServiceComponentDto, ((ClientSession)Session["objClientSession"]).GetAsList());
                    }
                    else if (Flag == "1")
                    {
                        _ObjServiceBL.UpdateAdditionalExam(ref objOperationResult, grdData.Rows[i].Values[5].ToString(), 1, ((ClientSession)Session["objClientSession"]).GetAsList());
                    }
                    
                }
                  else if (ChecAdicional == false)
                {
                    //var Lista_OLD = (List<Categoria>)Session["lComponentes_OLD"];

                  //var Flag =   grdData.Rows[i].Values[4].ToString();
                  if (Flag == "1")
                  {
                      _ObjServiceBL.UpdateAdditionalExam(ref objOperationResult, grdData.Rows[i].Values[5].ToString(), 0, ((ClientSession)Session["objClientSession"]).GetAsList());

                  }
                    //Lista_OLD.Find(p => p.v_ComponentId == grdData.Rows[i].Values[3].ToString());


                }
                   
               
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