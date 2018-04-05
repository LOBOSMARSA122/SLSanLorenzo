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
    public partial class FRM033J : System.Web.UI.Page
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


                var Valores = new ServiceBL().ValoresComponente(Session["ServiceId"].ToString(), Constants.RX_TORAX_ID);

                if (Valores.Count() != 0)
                {

                    txtVertices.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_VERTICES_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_VERTICES_ID).v_Value1;
                    txtCampoPulmo.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CAMPOS_PULMONARES_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CAMPOS_PULMONARES_ID).v_Value1;

                    txtHilios.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_HILOS_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_HILOS_ID).v_Value1;
                    txtSenosCosto.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_COSTO_ODIAFRAGMATICO_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_COSTO_ODIAFRAGMATICO_ID).v_Value1;

                    txtSenosCardio.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_SENOS_CARDIOFRENICOS_DESCRIPCION_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_SENOS_CARDIOFRENICOS_DESCRIPCION_ID).v_Value1;
                    txtMediastinos.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_MEDIASTINOS_DESCRIPCION_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_MEDIASTINOS_DESCRIPCION_ID).v_Value1;

                    txtSiluetaCardiaca.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_SILUETA_CARDIACA_DESCRIPCION_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_SILUETA_CARDIACA_DESCRIPCION_ID).v_Value1;
                    txtIndiceCardio.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_INDICE_CARDIACO_DESCRIPCION_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_INDICE_CARDIACO_DESCRIPCION_ID).v_Value1;

                    txtPartesBlandas.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_PARTES_BLANDAS_OSEAS_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_PARTES_BLANDAS_OSEAS_ID).v_Value1;
                    txtDescripcion.Text = Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CONCLUSIONES_RADIOGRAFICAS_DESCRIPCION_ID) == null ? string.Empty : Valores.Find(p => p.v_ComponentFieldId == Constants.RX_CONCLUSIONES_RADIOGRAFICAS_DESCRIPCION_ID).v_Value1;

                }


            }
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




            //Verticies**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_VERTICES_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtVertices.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //Campos Pulmonares**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_CAMPOS_PULMONARES_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtCampoPulmo.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //Hilios**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_HILOS_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtHilios.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //Senos CostoDiagragmáticos**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_COSTO_ODIAFRAGMATICO_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtSenosCosto.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);



            //Senos Cardiofrenicos**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_SENOS_CARDIOFRENICOS_DESCRIPCION_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtSenosCardio.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);



            //Mediastinos**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_MEDIASTINOS_DESCRIPCION_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtMediastinos.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //Silueta**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_SILUETA_CARDIACA_DESCRIPCION_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtSiluetaCardiaca.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);



            //Índice Cardio**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_INDICE_CARDIACO_DESCRIPCION_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtIndiceCardio.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //Partes blandas**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_PARTES_BLANDAS_OSEAS_ID;
            serviceComponentFields.v_ServiceComponentId = Session["ServiceComponentId"].ToString();

            _serviceComponentFieldValuesList = new List<Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList>();
            serviceComponentFieldValues = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldValuesList();

            serviceComponentFieldValues.v_ComponentFieldValuesId = null;
            serviceComponentFieldValues.v_Value1 = txtPartesBlandas.Text;
            _serviceComponentFieldValuesList.Add(serviceComponentFieldValues);

            serviceComponentFields.ServiceComponentFieldValues = _serviceComponentFieldValuesList;

            // Agregar a mi lista
            _serviceComponentFieldsList.Add(serviceComponentFields);


            //Descripción**-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-
            serviceComponentFields = new Sigesoft.Node.WinClient.BE.ServiceComponentFieldsList();

            serviceComponentFields.v_ComponentFieldsId = Constants.RX_CONCLUSIONES_RADIOGRAFICAS_DESCRIPCION_ID;
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