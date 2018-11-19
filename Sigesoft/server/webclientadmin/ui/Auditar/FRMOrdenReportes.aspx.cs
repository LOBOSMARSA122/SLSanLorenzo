using FineUI;
using NetPdf;
using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
//using System.Web.UI.WebControls;

namespace Sigesoft.Server.WebClientAdmin.UI.Auditar
{
    public partial class FRMOrdenReportes : System.Web.UI.Page
    {
        List<ServiceComponentList> ConsolidadoReportes = new List<ServiceComponentList>();
        ServiceBL _serviceBL = new ServiceBL();
        private string _serviceId;
        private string _EmpresaClienteId;
        private int _flagPantalla;
        private MergeExPDF _mergeExPDF = new MergeExPDF();
        PacientBL _pacientBL = new PacientBL();
        HistoryBL _historyBL = new HistoryBL();
        private string _pacientId;
        private string _tempSourcePath;
        private string _customerOrganizationName;
        private string _personFullName;
        private List<string> _filesNameToMerge = new List<string>();
        List<ServiceComponentList> _listaDosaje = new List<ServiceComponentList>();
        DataSet dsGetRepo = null;
        string ruta;
        int _Eso;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            lbltodos.Text = "Seleccionar Todos";
            List<ServiceComponentList> serviceComponents = new List<ServiceComponentList>();
            OrganizationBL oOrganizationBL = new OrganizationBL();
            OperationResult objOperationResult = new OperationResult();
            List<ServiceComponentList> ListaFinalOrdena = new List<ServiceComponentList>();
            List<ServiceComponentList> ListaOrdenada = new List<ServiceComponentList>();


            _serviceId = Request.QueryString["_serviceId"].ToString();
            _pacientId = Request.QueryString["_pacientId"].ToString();
            _customerOrganizationName = Request.QueryString["_customerOrganizationName"].ToString();
            _personFullName = Request.QueryString["_personFullName"].ToString();
            _flagPantalla = int.Parse(Request.QueryString["flagPantalla"].ToString());
            _EmpresaClienteId = Request.QueryString["_EmpresaClienteId"].ToString();
            _Eso = int.Parse(Request.QueryString["Eso"].ToString());

            //Trae todos los examenes o componentes que el trabajador var a pasar
            serviceComponents = _serviceBL.GetServiceComponentsForManagementReport(_serviceId);

            // Agregar Reportes en duro(pdf)
            #region Reportes
            serviceComponents.Add(new ServiceComponentList { Orden = 1, v_ComponentName = "CONSENTIMIENTO INFORMADO ", v_ComponentId = Constants.CONSENTIMIENTO_INFORMADO });
            serviceComponents.Add(new ServiceComponentList { Orden = 2, v_ComponentName = "CERTIFICADO APTITUD SIN Diagnósticos ", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX });
            serviceComponents.Add(new ServiceComponentList { Orden = 2, v_ComponentName = "CERTIFICADO APTITUD EMPRESARIAL ", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL });
            serviceComponents.Add(new ServiceComponentList { Orden = 2, v_ComponentName = "CERTIFICADO APTITUD", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD });
            serviceComponents.Add(new ServiceComponentList { Orden = 3, v_ComponentName = "FICHA MÉDICA DEL TRABAJADOR 1", v_ComponentId = Constants.INFORME_FICHA_MEDICA_TRABAJADOR });
            serviceComponents.Add(new ServiceComponentList { Orden = 4, v_ComponentName = "FICHA MÉDICA DEL TRABAJADOR 2", v_ComponentId = Constants.INFORME_FICHA_MEDICA_TRABAJADOR_2 });
            serviceComponents.Add(new ServiceComponentList { Orden = 4, v_ComponentName = "FICHA MÉDICA DEL TRABAJADOR 3", v_ComponentId = Constants.INFORME_FICHA_MEDICA_TRABAJADOR_3 });
            serviceComponents.Add(new ServiceComponentList { Orden = 27, v_ComponentName = "INFORME DE LABORATORIO", v_ComponentId = Constants.INFORME_LABORATORIO_CLINICO });
            serviceComponents.Add(new ServiceComponentList { Orden = 6, v_ComponentName = "ANEXO 16 Coimolache", v_ComponentId = Constants.INFORME_ANEXO_16_COIMOLACHE });
            serviceComponents.Add(new ServiceComponentList { Orden = 6, v_ComponentName = "ANEXO 16 Yanacocha", v_ComponentId = Constants.INFORME_ANEXO_16_YANACOCHA });
            serviceComponents.Add(new ServiceComponentList { Orden = 6, v_ComponentName = "ANEXO 16 Pacasmayo", v_ComponentId = Constants.INFORME_ANEXO_16_PACASMAYO });
            serviceComponents.Add(new ServiceComponentList { Orden = 6, v_ComponentName = "ANEXO 16 MINSUR SAN RAFAEL", v_ComponentId = Constants.INFORME_ANEXO_16_MINSURSANRAFAEL });
            serviceComponents.Add(new ServiceComponentList { Orden = 6, v_ComponentName = "ANEXO 16 Shahuindo", v_ComponentId = Constants.INFORME_ANEXO_16_SHAHUINDO });
            serviceComponents.Add(new ServiceComponentList { Orden = 6, v_ComponentName = "ANEXO 16 Gold Field", v_ComponentId = Constants.INFORME_ANEXO_16_GOLD_FIELD });
            serviceComponents.Add(new ServiceComponentList { Orden = 6, v_ComponentName = "ANTECEDENTE PATOLOGICO", v_ComponentId = Constants.INFORME_ANTECEDENTE_PATOLOGICO });
            serviceComponents.Add(new ServiceComponentList { Orden = 48, v_ComponentName = "DECLARACION CI", v_ComponentId = Constants.INFORME_DECLARACION_CI });
            serviceComponents.Add(new ServiceComponentList { Orden = 51, v_ComponentName = "INFORME ESPIROMETRIA", v_ComponentId = Constants.INFORME_ESPIROMETRIA });
            serviceComponents.Add(new ServiceComponentList { Orden = 52, v_ComponentName = "APTITUD YANACOCHA", v_ComponentId = Constants.APTITUD_YANACOCHA });
            serviceComponents.Add(new ServiceComponentList { Orden = 53, v_ComponentName = "INFORME MEDICO OCUPACIONAL COSAPI", v_ComponentId = Constants.INFORME_MEDICO_OCUPACIONAL_COSAPI });
            serviceComponents.Add(new ServiceComponentList { Orden = 54, v_ComponentName = "CERTIFICADO DE APTITUD MEDICO OCUPACIONAL COSAPI", v_ComponentId = Constants.CERTIFICADO_APTITUD_MEDICO });
            serviceComponents.Add(new ServiceComponentList { Orden = 72, v_ComponentName = "INFORME MEDICO SALUD OCUPACIONAL - EXAMEN ANUAL", v_ComponentId = Constants.INFORME_MEDICO_SALUD_OCUPACIONAL_EXAMEN_MEDICO_ANUAL });
            serviceComponents.Add(new ServiceComponentList { Orden = 73, v_ComponentName = "ANEXO 8 INFORME MEDICO OCUPASIONAL", v_ComponentId = Constants.ANEXO_8_INFORME_MEDICO_OCUPACIONAL });
            serviceComponents.Add(new ServiceComponentList { Orden = 74, v_ComponentName = "INFORME RESULTADOS EVALUACION MEDICA - AUTORIZACION", v_ComponentId = Constants.INFORME_RESULTADOS_EVALUACION_MEDICA });
            serviceComponents.Add(new ServiceComponentList { Orden = 75, v_ComponentName = "AGLUTINACIONES KOH SECRECION CIELO AZUL", v_ComponentId = Constants.AGLUTINACIONES_KOH_SECRECION });
            serviceComponents.Add(new ServiceComponentList { Orden = 76, v_ComponentName = "PARASITOLOGICO COPROCULTIVO CIELO AZUL", v_ComponentId = Constants.PARASITOLOGICO_COPROCULTIVO_CIELO_AZUL });
            serviceComponents.Add(new ServiceComponentList { Orden = 77, v_ComponentName = "MARCOBRE PASE MÉDICO", v_ComponentId = Constants.MARCOBRE_PASE_MEDICO });
            serviceComponents.Add(new ServiceComponentList { Orden = 77, v_ComponentName = "DECLARACIÓN JURADA", v_ComponentId = Constants.DECLARACION_JURADA });

            serviceComponents.Add(new ServiceComponentList { Orden = 78, v_ComponentName = "ENTREGA DE EXAMEN MEDICO OCUPACIONAL", v_ComponentId = Constants.ENTREGA_DE_XAMEN_MEDICO_OCUPACIONAL });

            serviceComponents.Add(new ServiceComponentList { Orden = 79, v_ComponentName = "EV. MED. SAN MARTIN - INFORME RESULTADOS", v_ComponentId = Constants.EVALUACION_MEDICO_SAN_MARTIN_INFORME });

            serviceComponents.Add(new ServiceComponentList { Orden = 80, v_ComponentName = "DECLARACION JURADA EMPO YANACOCHA", v_ComponentId = Constants.Declaracion_Jurada_EMPO_YANACOCHA });
            serviceComponents.Add(new ServiceComponentList { Orden = 81, v_ComponentName = "DECLARACION JURADA EMO SECURITAS", v_ComponentId = Constants.Declaracion_Jurada_EMO_SECURITAS });
             
            #endregion      


            
            //?????
            ListaOrdenada = ConsolidadoReportes.OrderBy(p => p.Orden).ToList();

            //Obtiene la configuración de orden de reportes de la empresa seleccionada
            var ListaOrdenReportes = oOrganizationBL.GetOrdenReportes(ref objOperationResult, _EmpresaClienteId);

            if (ListaOrdenReportes.Count > 0)
            {
                ListaOrdenada = new List<ServiceComponentList>();
                ServiceComponentList oServiceComponentList = null;

                foreach (var item in ListaOrdenReportes)
                {
                    oServiceComponentList = new ServiceComponentList();
                    oServiceComponentList.v_ComponentName = item.v_NombreReporte;
                    oServiceComponentList.v_ComponentId = item.v_ComponenteId + "|" + item.i_NombreCrystalId;
                    ListaOrdenada.Add(oServiceComponentList);
                }

                foreach (var item in ListaOrdenada)
                {
                    var array = item.v_ComponentId.Split('|');
                    foreach (var item1 in serviceComponents)
                    {
                        if (array[0].ToString() == item1.v_ComponentId)
                        {
                            ListaFinalOrdena.Add(item);
                        }
                    }
                }

                chkregistros.DataTextField = "v_ComponentName";
                chkregistros.DataValueField = "v_ComponentId";
                chkregistros.DataSource = ListaFinalOrdena;
                chkregistros.DataBind();
            }

            else
            {
                chkregistros.DataTextField = "v_ComponentName";
                chkregistros.DataValueField = "v_ComponentId";
                chkregistros.DataSource = serviceComponents;
                chkregistros.DataBind();
            }
           

        }

        protected void btnGenerarPDF_Click(object sender, EventArgs e)
        {

        }

        protected void chkregistros_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void chktodos_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            if (chk.Checked == true)
            {
                lbltodos.Text = "Deseleccionar Todos";
                for(int i = 0; i < chkregistros.Items.Count; i++){
                    chkregistros.Items[i].Selected = true;
                }
            }
            else
            {
                lbltodos.Text = "Seleccionar Todos";
                for (int i = 0; i < chkregistros.Items.Count; i++)
                {
                    chkregistros.Items[i].Selected = false;
                }
            }    
              
        }
    }
}