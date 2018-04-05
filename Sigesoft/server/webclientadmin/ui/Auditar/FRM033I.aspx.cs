using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sigesoft.Server.WebClientAdmin.BLL;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Common;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.IO;

namespace Sigesoft.Server.WebClientAdmin.UI.Auditar
{
    public partial class FRM033I : System.Web.UI.Page
    {
        SystemParameterBL _objSystemParameterBL = new SystemParameterBL();
        ProtocolBL oProtocolBL = new ProtocolBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["v_ServiceId"] != null)
                   Session["ServiceId"] = Request.QueryString["v_ServiceId"].ToString();
                if (Request.QueryString["v_IdTrabajador"] != null)
                    Session["IdTrabajador"] = Request.QueryString["v_IdTrabajador"].ToString();

                List<string> servicios = new List<string>();
                servicios.Add(Session["ServiceId"].ToString());
                var obj = oProtocolBL.ObtenerIdsParaImportacion(servicios, int.Parse(Session["ConsultorioId"].ToString()));
               Session["ServiceComponentId"] = obj[0].ServicioComponentId.ToString();
                LoadCombox();


                var Valores = new ServiceBL().ValoresComponente(Session["ServiceId"].ToString(), Constants.ELECTROCARDIOGRAMA_ID);

                if (Valores.Count() != 0)
                {

                    dllRitmo.SelectedValue = Valores.Find(p => p.v_ComponentFieldId == Constants.ELECTROCARDIOGRAMA_ONDA_P_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.ELECTROCARDIOGRAMA_ONDA_P_ID).v_Value1;
                    txtFrecuencia.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.ELECTROCARDIOGRAMA_RITMO_SINUAL_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.ELECTROCARDIOGRAMA_RITMO_SINUAL_ID).v_Value1;

                    txtSegPR.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.ELECTROCARDIOGRAMA_INTERVALO_PR_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.ELECTROCARDIOGRAMA_INTERVALO_PR_ID).v_Value1;
                    txtOndaQRS.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.ELECTROCARDIOGRAMA_COMPLEJO_QRS_ANORMAL_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.ELECTROCARDIOGRAMA_COMPLEJO_QRS_ANORMAL_ID).v_Value1;

                    txtSegQT.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.ELECTROCARDIOGRAMA_INTERVALO_QT_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.ELECTROCARDIOGRAMA_INTERVALO_QT_ID).v_Value1;
                    txtEjeORS.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.ELECTROCARDIOGRAMA_OTROS2_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.ELECTROCARDIOGRAMA_OTROS2_ID).v_Value1;

                    dllConclusiones.SelectedValue = Valores.Find(p => p.v_ComponentFieldId == Constants.ELECTROCARDIOGRAMA_ONDA_T_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.ELECTROCARDIOGRAMA_ONDA_T_ID).v_Value1;

                    txtDescripcion.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.ELECTROCARDIOGRAMA_DESCRIPCION_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.ELECTROCARDIOGRAMA_DESCRIPCION_ID).v_Value1;

                }
            }
        }

        private void LoadCombox()
        {
            OperationResult objOperationResult = new OperationResult();

            Utils.LoadDropDownList(dllRitmo, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 221), DropDownListAction.Select);
            Utils.LoadDropDownList(dllConclusiones, "Value1", "Id", _objSystemParameterBL.GetSystemParameterForCombo(ref objOperationResult, 221), DropDownListAction.Select);
           
        }

        protected void btnSaveRefresh_Click(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();
           ServiceBL _serviceBL = new ServiceBL();
            Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList serviceComponentFieldValues = null;
            Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList serviceComponentFields = null;
            List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList> _serviceComponentFieldValuesList = null;
            List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList> _serviceComponentFieldsList = null;

            if (_serviceComponentFieldsList == null)
                _serviceComponentFieldsList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList>();



            //Rítmo**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ELECTROCARDIOGRAMA_ONDA_P_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = dllRitmo.SelectedValue.ToString();
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //Frecuencia**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ELECTROCARDIOGRAMA_RITMO_SINUAL_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtFrecuencia.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);




            //SEGMENTO PR**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ELECTROCARDIOGRAMA_INTERVALO_PR_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtSegPR.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);



            //ONDA QRS**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ELECTROCARDIOGRAMA_COMPLEJO_QRS_ANORMAL_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtOndaQRS.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);



            //SEGEMENTO QT**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ELECTROCARDIOGRAMA_INTERVALO_QT_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtSegQT.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);



            //EJE ORS**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ELECTROCARDIOGRAMA_OTROS2_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtEjeORS.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);



            //CONCLUSIONES**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ELECTROCARDIOGRAMA_ONDA_T_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = dllConclusiones.SelectedValue.ToString();
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);




            //DESCRIPCIÓN**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.ELECTROCARDIOGRAMA_DESCRIPCION_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtDescripcion.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);



            var result = _serviceBL.AddServiceComponentValues(ref objOperationResult,
                                                                 _serviceComponentFieldsList,
                                                                  ((ClientSession)Session["objClientSession"]).GetAsList(),
                                                                 Session["IdTrabajador"].ToString(),
                                                                 Session["ServiceComponentId"].ToString());


            Alert.ShowInTop("Datos grabados correctamente.");

        }
    }
}