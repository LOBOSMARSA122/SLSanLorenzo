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
    public partial class FRM031E : System.Web.UI.Page
    {
        ServiceBL _serviceBL = new ServiceBL();

        protected void Page_Load(object sender, EventArgs e)        
        {
            //if (!IsPostBack)
            //{           
            //List<MyListWeb> ListaServicios = (List<MyListWeb>)Session["objLista"];

            //btnSaveRefresh.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("../ExternalUser/FRM031F.aspx");
            ////btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
            //var serviceComponents = _serviceBL.GetServiceComponentsForManagementReport(ListaServicios[0].IdServicio);

            //#region Examen For Print

            //string[] examenForPrint = new string[] 
            //{ 
            //    Constants.ALTURA_ESTRUCTURAL_ID,
            //    Constants.ALTURA_7D_ID,
            //    Constants.ODONTOGRAMA_ID,
            //    Constants.OSTEO_MUSCULAR_ID_1,
            //    Constants.OFTALMOLOGIA_ID,
            //    Constants.RX_TORAX_ID,
            //    Constants.PRUEBA_ESFUERZO_ID,
            //    Constants.ELECTROCARDIOGRAMA_ID,
            //    Constants.TAMIZAJE_DERMATOLOGIO_ID,
            //    Constants.PSICOLOGIA_ID,
            //    Constants.AUDIOMETRIA_ID,
            //    Constants.ESPIROMETRIA_ID,
            //    //Constants.LABORATORIO_ID,
            //    Constants.GINECOLOGIA_ID,
            //    Constants.EVALUACION_PSICOLABORAL

            //};

            //#endregion
            //// Cargar ListBox de examenes
            //serviceComponents = serviceComponents.FindAll(p => examenForPrint.Contains(p.v_ComponentId));

            ////serviceComponents.Insert(0, new ServiceComponentList { v_ComponentName = "Certificado de Aptitud", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD });
            //serviceComponents.Insert(1, new ServiceComponentList { v_ComponentName = "Historia Ocupacional", v_ComponentId = Constants.INFORME_HISTORIA_OCUPACIONAL });

            //// Si la prueba de RX esta entonces tambien insertar <Informe Radiografico OIT>
            //var findRX = serviceComponents.Find(p => p.v_ComponentId == Constants.RX_TORAX_ID);
            //var findEspiro = serviceComponents.Find(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID);

            //if (findRX != null)
            //{
            //    var newPosition = serviceComponents.IndexOf(findRX) + 1;
            //    serviceComponents.Insert(newPosition, new ServiceComponentList { v_ComponentName = "Informe Radiografico OIT", v_ComponentId = Constants.INFORME_RADIOGRAFICO_OIT });
            //}

            //if (findEspiro != null)
            //{
            //    var newPosition = serviceComponents.IndexOf(findEspiro) + 1;
            //    serviceComponents.Insert(newPosition, new ServiceComponentList { v_ComponentName = "Cuestionario de Espirometria", v_ComponentId = Constants.ESPIROMETRIA_CUESTIONARIO_ID });
            //}

            //chklExamenes.DataTextField = "v_ComponentName";
            //chklExamenes.DataValueField = "v_ComponentId";
            //chklExamenes.DataSource = serviceComponents;
            //chklExamenes.DataBind();
            //}


            if (!IsPostBack)
            {
                List<MyListWeb> ListaServicios = (List<MyListWeb>)Session["objLista"];

                btnSaveRefresh.OnClientClick = winEdit1.GetSaveStateReference(hfRefresh.ClientID) + winEdit1.GetShowReference("../ExternalUser/FRM031F.aspx");
                //btnClose.OnClientClick = ActiveWindow.GetConfirmHideReference();
                var serviceComponents = _serviceBL.GetServiceComponentsForManagementReport(ListaServicios[0].IdServicio);

                foreach (var item in serviceComponents)
                {

                    if (item.v_ComponentId == Constants.OSTEO_MUSCULAR_ID_1)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 12;
                    }
                    else if (item.v_ComponentId == Constants.C_N_ID)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 13;
                    }
                    else if (item.v_ComponentId == Constants.ALTURA_7D_ID)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 14;
                    }
                    else if (item.v_ComponentId == Constants.ALTURA_ESTRUCTURAL_ID)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 15;
                    }
                    else if (item.v_ComponentId == Constants.CUESTIONARIO_ACTIVIDAD_FISICA)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 16;
                    }
                    else if (item.v_ComponentId == Constants.EVAL_NEUROLOGICA_ID)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 17;
                    }
                    else if (item.v_ComponentId == Constants.TAMIZAJE_DERMATOLOGIO_ID)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 18;
                    }
                    else if (item.v_ComponentId == Constants.TEST_VERTIGO_ID)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 19;
                    }
                    else if (item.v_ComponentId == Constants.AUDIOMETRIA_ID)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 20;
                    }
                    else if (item.v_ComponentId == Constants.ESPIROMETRIA_CUESTIONARIO_ID)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 21;
                    }
                    else if (item.v_ComponentId == Constants.ELECTROCARDIOGRAMA_ID)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 22;
                    }
                    else if (item.v_ComponentId == Constants.ODONTOGRAMA_ID)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 23;
                    }
                    else if (item.v_ComponentId == Constants.OFTALMOLOGIA_ID)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 24;
                    }
                    else if (item.v_ComponentId == Constants.TESTOJOSECO_ID)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 25;
                    }
                    else if (item.v_ComponentId == Constants.RX_TORAX_ID)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 26;
                    }
                    else if (item.v_ComponentId == Constants.OIT_ID)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 27;
                    }
                    else if (item.v_ComponentId == Constants.PSICOLOGIA_ID)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 29;
                    }
                    else if (item.v_ComponentId == Constants.HISTORIA_CLINICA_PSICOLOGICA_ID)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 30;
                    }
                    else if (item.v_ComponentId == Constants.EVA_NEUROLOGICA_ID)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 31;
                    }
                    else if (item.v_ComponentId == Constants.FICHA_OTOSCOPIA)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 32;
                    }
                    else if (item.v_ComponentId == Constants.SINTOMATICO_RESPIRATORIO)
                    {
                        var ent = serviceComponents.FirstOrDefault(o => o.v_ComponentId == item.v_ComponentId);
                        ent.Orden = 33;
                    }

                }


                #region Examen For Print

                string[] examenForPrint = new string[] 
            { 
                  Constants.ALTURA_ESTRUCTURAL_ID,
                Constants.ALTURA_7D_ID,
                Constants.CUESTIONARIO_ACTIVIDAD_FISICA,
                Constants.OSTEO_MUSCULAR_ID_1,                
                Constants.ESPIROMETRIA_ID,     
                Constants.AUDIOMETRIA_ID,  
                Constants.OFTALMOLOGIA_ID,
                Constants.TESTOJOSECO_ID, 
                Constants.ELECTROCARDIOGRAMA_ID,
                Constants.EVA_CARDIOLOGICA_ID,
                Constants.EVA_NEUROLOGICA_ID,
                Constants.OSTEO_MUSCULAR_ID_2,
                Constants.EVA_OSTEO_ID,
                Constants.HISTORIA_CLINICA_PSICOLOGICA_ID,
                Constants.EVALUACION_PSICOLABORAL,
                Constants.ECOGRAFIA_ABDOMINAL_ID,
                Constants.INFORME_ECOGRAFICO_PROSTATA_ID,
                Constants.ECOGRAFIA_RENAL_ID,
                Constants.OIT_ID,
                Constants.RX_TORAX_ID,
                Constants.ODONTOGRAMA_ID,
                Constants.TAMIZAJE_DERMATOLOGIO_ID,
                //Constants.PRUEBA_ESFUERZO_ID,
                Constants.PSICOLOGIA_ID,
                Constants.GINECOLOGIA_ID,
                Constants.C_N_ID,
                Constants.TEST_VERTIGO_ID,
                Constants.EVA_ERGONOMICA_ID,
                Constants.SOMNOLENCIA_ID,
                Constants.ACUMETRIA_ID,
                Constants.OTOSCOPIA_ID,
                Constants.SINTOMATICO_ID,
                Constants.LUMBOSACRA_ID

            };
                string[] ExamenBioquimica1 = new string[] 
            { 
                 Constants.GLUCOSA_ID,
                Constants.COLESTEROL_ID,
                Constants.TRIGLICERIDOS_ID,
                Constants.COLESTEROL_HDL_ID,
                Constants.COLESTEROL_LDL_ID,
                Constants.COLESTEROL_VLDL_ID,
                Constants.UREA_ID,
                Constants.CREATININA_ID,
                Constants.TGO_ID,
                Constants.TGP_ID,
                Constants.TEST_ESTEREOPSIS_ID,
                Constants.ANTIGENO_PROSTATICO_ID,
                Constants.PLOMO_SANGRE_ID,
                Constants.BIOQUIMICA01_ID,
                Constants.BIOQUIMICA02_ID,
                Constants.BIOQUIMICA03_ID
            };

                string[] ExamenEspeciales1 = new string[] 
            { 
                  Constants.BK_DIRECTO_ID,
                Constants.EXAMEN_ELISA_ID,
                Constants.HEPATITIS_A_ID,
                Constants.HEPATITIS_C_ID,
                Constants.SUB_UNIDAD_BETA_CUALITATIVO_ID,
                Constants.VDRL_ID,
            };

                string[] InformeAnexo3121 = new string[] 
            { 
                Constants.EXAMEN_FISICO_ID
               
            };

                string[] InformeFisico7C1 = new string[] 
            { 
                Constants.EXAMEN_FISICO_7C_ID
               
            };


                List<ServiceComponentList> ConsolidadoReportes = new List<ServiceComponentList>();
                var serviceComponents11 = _serviceBL.GetServiceComponentsForManagementReport(ListaServicios[0].IdServicio);


                //ConsolidadoReportes.Add(new ServiceComponentList { Orden = 1, v_ComponentName = "CERTIFICADO APTITUD", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD });
                //ConsolidadoReportes.Add(new ServiceComponentList { Orden = 2, v_ComponentName = "FICHA MÉDICA DEL TRABAJADOR", v_ComponentId = Constants.INFORME_FICHA_MEDICA_TRABAJADOR });

                //ConsolidadoReportes.Add(new ServiceComponentList { Orden = 23, v_ComponentName = "FICHA MÉDICA DEL TRABAJADOR INTERNACIONAL", v_ComponentId = Constants.INFORME_MEDICO_TRABAJADOR_INTERNACIONAL });

                //var ResultadoAnexo3121 = serviceComponents11.FindAll(p => InformeAnexo3121.Contains(p.v_ComponentId)).ToList();
                //if (ResultadoAnexo3121.Count() != 0)
                //{
                //    ConsolidadoReportes.Add(new ServiceComponentList { Orden = 3, v_ComponentName = "ANEXO 312", v_ComponentId = Constants.INFORME_ANEXO_312 });
                //}



                //var ResultadoFisico7C1 = serviceComponents11.FindAll(p => InformeFisico7C1.Contains(p.v_ComponentId)).ToList();
                //if (ResultadoFisico7C1.Count() != 0)
                //{
                //    ConsolidadoReportes.Add(new ServiceComponentList { Orden = 4, v_ComponentName = "ANEXO 7C", v_ComponentId = Constants.INFORME_ANEXO_7C });
                //}
                //ConsolidadoReportes.Add(new ServiceComponentList { Orden = 5, v_ComponentName = "HISTORIA OCUPACIONAL", v_ComponentId = Constants.INFORME_HISTORIA_OCUPACIONAL });

                //var ResultadoBioquimico1 = serviceComponents11.FindAll(p => ExamenBioquimica1.Contains(p.v_ComponentId)).ToList();
                //if (ResultadoBioquimico1.Count() != 0)
                //{
                //    ConsolidadoReportes.Add(new ServiceComponentList { Orden = 7, v_ComponentName = "INFORME DE LABORATORIO", v_ComponentId = Constants.INFORME_LABORATORIO_CLINICO });
                //}

                //var ResultadoEspirometria = serviceComponents11.Find(p => p.v_ComponentId == Constants.ESPIROMETRIA_ID);
                //if (ResultadoEspirometria != null)
                //{
                //    ConsolidadoReportes.Add(new ServiceComponentList { Orden = 14, v_ComponentName = "INFORME DE ESPIROMETRÍA", v_ComponentId = Constants.ESPIROMETRIA_ID });
                //}

                ConsolidadoReportes.Add(new ServiceComponentList { Orden = 1, v_ComponentName = "CONSENTIMIENTO INFORMADO ", v_ComponentId = Constants.CONSENTIMIENTO_INFORMADO });
                //ConsolidadoReportes.Add(new ServiceComponentList { Orden = 2, v_ComponentName = "CERTIFICADO APTITUD SIN Diagnósticos ", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD_SIN_DX });
                //ConsolidadoReportes.Add(new ServiceComponentList { Orden = 3, v_ComponentName = "CERTIFICADO APTITUD EMPRESARIAL ", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD_EMPRESARIAL });
                //ConsolidadoReportes.Add(new ServiceComponentList { Orden = 4, v_ComponentName = "CERTIFICADO APTITUD Completo", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD_COMPLETO });
                //ConsolidadoReportes.Add(new ServiceComponentList { Orden = 5, v_ComponentName = "CERTIFICADO APTITUD SM ", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD_SM });
                ConsolidadoReportes.Add(new ServiceComponentList { Orden = 6, v_ComponentName = "CERTIFICADO APTITUD", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD });
                //ConsolidadoReportes.Add(new ServiceComponentList { Orden = 7, v_ComponentName = "INFORME MÉDICO RESUMEN", v_ComponentId = Constants.INFORME_MEDICO_RESUMEN });
                //ConsolidadoReportes.Add(new ServiceComponentList { Orden = 8, v_ComponentName = "FICHA MÉDICA DEL TRABAJADOR", v_ComponentId = Constants.INFORME_FICHA_MEDICA_TRABAJADOR });
                ConsolidadoReportes.Add(new ServiceComponentList { Orden = 8, v_ComponentName = "FICHA MÉDICA DEL TRABAJADOR", v_ComponentId = Constants.INFORME_FICHA_MEDICA_TRABAJADOR_2 });

                //var serviceComponents11 = _serviceBL.GetServiceComponentsForManagementReport(_serviceId);
                var ResultadoAnexo3121 = serviceComponents11.FindAll(p => InformeAnexo3121.Contains(p.v_ComponentId)).ToList();
                if (ResultadoAnexo3121.Count() != 0)
                {
                    ConsolidadoReportes.Add(new ServiceComponentList { Orden = 9, v_ComponentName = "ANEXO 312", v_ComponentId = Constants.INFORME_ANEXO_312 });
                }
                var ResultadoFisico7C1 = serviceComponents11.FindAll(p => InformeFisico7C1.Contains(p.v_ComponentId)).ToList();
                if (ResultadoFisico7C1.Count() != 0)
                {
                    ConsolidadoReportes.Add(new ServiceComponentList { Orden = 10, v_ComponentName = "ANEXO 7C", v_ComponentId = Constants.INFORME_ANEXO_7C });
                }
                ConsolidadoReportes.Add(new ServiceComponentList { Orden = 11, v_ComponentName = "HISTORIA OCUPACIONAL", v_ComponentId = Constants.INFORME_HISTORIA_OCUPACIONAL });
                var ResultadoBioquimico1 = serviceComponents11.FindAll(p => ExamenBioquimica1.Contains(p.v_ComponentId)).ToList();
                if (ResultadoBioquimico1.Count() != 0)
                {
                    ConsolidadoReportes.Add(new ServiceComponentList { Orden = 28, v_ComponentName = "INFORME DE LABORATORIO", v_ComponentId = Constants.INFORME_LABORATORIO_CLINICO });
                }



                ConsolidadoReportes.AddRange(serviceComponents.FindAll(p => examenForPrint.Contains(p.v_ComponentId)));


                ConsolidadoReportes.Add(new ServiceComponentList { v_ComponentName = "CERTIFICADO DE APTITUD COMPLETO", v_ComponentId = Constants.INFORME_CERTIFICADO_APTITUD_COMPLETO });




                var ResultadoExaEspeciales1 = serviceComponents11.FindAll(p => ExamenEspeciales1.Contains(p.v_ComponentId)).ToList();
                if (ResultadoExaEspeciales1.Count() != 0)
                {
                    ConsolidadoReportes.Add(new ServiceComponentList { v_ComponentName = "EXAMENES ESPECIALES", v_ComponentId = Constants.INFORME_EXAMENES_ESPECIALES });
                }

                #endregion



                chklExamenes.DataTextField = "v_ComponentName";
                chklExamenes.DataValueField = "v_ComponentId";
                chklExamenes.DataSource = ConsolidadoReportes;
                chklExamenes.DataBind();
            }
        }

        protected void chklExamenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> lista = new List<string>();

            int selectedCount = chklExamenes.SelectedItemArray.Length;

            //var lista = List<ServiceComponentList>()
            foreach (var item in chklExamenes.SelectedItemArray)
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