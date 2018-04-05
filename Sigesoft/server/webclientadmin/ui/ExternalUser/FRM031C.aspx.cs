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

namespace Sigesoft.Server.WebClientAdmin.UI.ExternalUser
{
    public partial class FRM031C : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<ServiceComponentList> serviceComponents = new List<ServiceComponentList>();
            ServiceBL _serviceBL = new ServiceBL();
            string[] InformeAnexo3121 = new string[] 
            { 
                Constants.EXAMEN_FISICO_ID
               
            };

            string[] InformeFisico7C1 = new string[] 
            { 
                Constants.EXAMEN_FISICO_7C_ID
               
            };
            if (!IsPostBack)
            {
                btnSaveRefresh.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("../ExternalUser/FRM031D.aspx");

                serviceComponents = _serviceBL.GetServiceComponentsForManagementReport(Session["ServiceId"].ToString());

                List<Sigesoft.Node.WinClient.BE.ServiceComponentList> fichasMedicas = new List<Sigesoft.Node.WinClient.BE.ServiceComponentList>();

                var ResultadoAnexo3121 = serviceComponents.FindAll(p => InformeAnexo3121.Contains(p.v_ComponentId)).ToList();
                  if (ResultadoAnexo3121.Count() != 0)
                  {
                      fichasMedicas.Add(new Sigesoft.Node.WinClient.BE.ServiceComponentList { v_ComponentName = "Anexo 312", v_ComponentId = Constants.INFORME_ANEXO_312 });
                  }

                  var ResultadoFisico7C1 = serviceComponents.FindAll(p => InformeFisico7C1.Contains(p.v_ComponentId)).ToList();
                  if (ResultadoFisico7C1.Count() != 0)
                  {
                      fichasMedicas.Add(new Sigesoft.Node.WinClient.BE.ServiceComponentList { v_ComponentName = "Anexo 7C", v_ComponentId = Constants.INFORME_ANEXO_7C });
                  }

                fichasMedicas.Add(new Sigesoft.Node.WinClient.BE.ServiceComponentList { v_ComponentName = "Laboratorio Clínico", v_ComponentId = Constants.INFORME_LABORATORIO_CLINICO });  
         

                informesSeleccionados.DataTextField = "v_ComponentName";
                informesSeleccionados.DataValueField = "v_ComponentId";
                informesSeleccionados.DataSource = fichasMedicas;
                informesSeleccionados.DataBind();
            }
        }

        protected void informesSeleccionados_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> lista = new List<string>();

            int selectedCount = informesSeleccionados.SelectedItemArray.Length;

            //var lista = List<ServiceComponentList>()
            foreach (var item in informesSeleccionados.SelectedItemArray)
            {
                lista.Add(item.Value);
            }

            Session["objListaExamenes"] = lista;

            if (selectedCount > 0)
            {
                btnSaveRefresh.Enabled = true;
            }
            else
            {
                btnSaveRefresh.Enabled = false;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());               
         
        }

        
    }
}