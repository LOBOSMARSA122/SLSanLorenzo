using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BLL;
using Sigesoft.Node.WinClient.BE;
using System.IO;
using NetPdf;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using Sigesoft.Node.WinClient.UI.Configuration;


namespace Sigesoft.Node.WinClient.UI
{
    public partial class frmExamenesRealizadosCompleto : Form
    {
        ServiceBL _serviceBL = new ServiceBL();
        public frmExamenesRealizadosCompleto()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (uvPacient.Validate(true, false).IsValid)
            {
                var id1 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');

                this.BindGrid(id1[0], id1[1], ddlProtocolId.SelectedValue.ToString());
            }
        }

        private void BindGrid(string pstrEmpresaId, string pstrSedeId, string pstrProtocolId)
        {
            List<ExamenesRealizadosCompleto> ListaFinal = new List<ExamenesRealizadosCompleto>();
            ExamenesRealizadosCompleto oExamenesRealizadosCompleto = null;

            DateTime? pdatBeginDate = dtpDateTimeStar.Value.Date;
            DateTime? pdatEndDate = dptDateTimeEnd.Value.Date.AddDays(1);

            var l = _serviceBL.ListaServicios(pstrEmpresaId, pstrSedeId, pdatBeginDate, pdatEndDate);

            var CantidadServicios = l.GroupBy(x => new { x.Paciente, x.ServiceId })
                  .Select(group => group.First())
                  .ToList();


            for (int i = 0; i < CantidadServicios.Count(); i++)
            {
                oExamenesRealizadosCompleto = new ExamenesRealizadosCompleto();
               var lComponentes = l.FindAll(p => p.ServiceId == CantidadServicios[i].ServiceId);

               oExamenesRealizadosCompleto.ServiceId = CantidadServicios[i].ServiceId;
               oExamenesRealizadosCompleto.Paciente = CantidadServicios[i].Paciente;
               oExamenesRealizadosCompleto.FechaServicio = CantidadServicios[i].FechaServicio;

               var ANEXO_16_N009_ME_000000052 = lComponentes.Find(p => p.ComponentId == "N009-ME000000052");
               if (ANEXO_16_N009_ME_000000052 != null)
                {
                    if (ANEXO_16_N009_ME_000000052.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                    {
                        oExamenesRealizadosCompleto.ANEXO_16_N009_ME_000000052 = "1";
                    }
                    else
                    {
                        oExamenesRealizadosCompleto.ANEXO_16_N009_ME_000000052 = ANEXO_16_N009_ME_000000052.ServiceComponentName;
                    }
                }
                else
                {
                    oExamenesRealizadosCompleto.ANEXO_16_N009_ME_000000052 = "NULL";
                }

               var EVALUACION_ERGONOMICA_N009_ME_000000128 = lComponentes.Find(p => p.ComponentId == "N009-ME000000128");
               if (EVALUACION_ERGONOMICA_N009_ME_000000128 != null)
               {
                   if (EVALUACION_ERGONOMICA_N009_ME_000000128.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.EVALUACION_ERGONOMICA_N009_ME_000000128 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.EVALUACION_ERGONOMICA_N009_ME_000000128 = EVALUACION_ERGONOMICA_N009_ME_000000128.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.EVALUACION_ERGONOMICA_N009_ME_000000128 = "NULL";
               }

               var EVALUACION_NEUROLOGICA_N009_ME_000000085 = lComponentes.Find(p => p.ComponentId == "N009-ME000000085");
               if (EVALUACION_NEUROLOGICA_N009_ME_000000085 != null)
               {
                   if (EVALUACION_NEUROLOGICA_N009_ME_000000085.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.EVALUACION_NEUROLOGICA_N009_ME_000000085 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.EVALUACION_NEUROLOGICA_N009_ME_000000085 = EVALUACION_NEUROLOGICA_N009_ME_000000085.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.EVALUACION_NEUROLOGICA_N009_ME_000000085 = "NULL";
               }


               var EXAMEN_ALTURA_ESTRUCTURAL_18_N009_ME_0000000015 = lComponentes.Find(p => p.ComponentId == "N009-ME000000015");
               if (EXAMEN_ALTURA_ESTRUCTURAL_18_N009_ME_0000000015 != null)
               {
                   if (EXAMEN_ALTURA_ESTRUCTURAL_18_N009_ME_0000000015.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.EXAMEN_ALTURA_ESTRUCTURAL_18_N009_ME_0000000015 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.EXAMEN_ALTURA_ESTRUCTURAL_18_N009_ME_0000000015 = EXAMEN_ALTURA_ESTRUCTURAL_18_N009_ME_0000000015.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.EXAMEN_ALTURA_ESTRUCTURAL_18_N009_ME_0000000015 = "NULL";
               }

               var EXAMEN_ALTURA_GEOGRAFICA_7_D_N002_ME_0000000045 = lComponentes.Find(p => p.ComponentId == "N002-ME000000045");
               if (EXAMEN_ALTURA_GEOGRAFICA_7_D_N002_ME_0000000045 != null)
               {
                   if (EXAMEN_ALTURA_GEOGRAFICA_7_D_N002_ME_0000000045.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.EXAMEN_ALTURA_GEOGRAFICA_7_D_N002_ME_0000000045 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.EXAMEN_ALTURA_GEOGRAFICA_7_D_N002_ME_0000000045 = EXAMEN_ALTURA_GEOGRAFICA_7_D_N002_ME_0000000045.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.EXAMEN_ALTURA_GEOGRAFICA_7_D_N002_ME_0000000045 = "NULL";
               }



               var EXAMEN_FISICO_312_N002_ME_0000000022 = lComponentes.Find(p => p.ComponentId == "N002-ME000000022");
               if (EXAMEN_FISICO_312_N002_ME_0000000022 != null)
               {
                   if (EXAMEN_FISICO_312_N002_ME_0000000022.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.EXAMEN_FISICO_312_N002_ME_0000000022 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.EXAMEN_FISICO_312_N002_ME_0000000022 = EXAMEN_FISICO_312_N002_ME_0000000022.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.EXAMEN_FISICO_312_N002_ME_0000000022 = "NULL";
               }


               var EXAMEN_OSTEOMUSCULAR_N002_ME_0000000046 = lComponentes.Find(p => p.ComponentId == "N002-ME000000046");
               if (EXAMEN_OSTEOMUSCULAR_N002_ME_0000000046 != null)
               {
                   if (EXAMEN_OSTEOMUSCULAR_N002_ME_0000000046.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.EXAMEN_OSTEOMUSCULAR_N002_ME_0000000046 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.EXAMEN_OSTEOMUSCULAR_N002_ME_0000000046 = EXAMEN_OSTEOMUSCULAR_N002_ME_0000000046.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.EXAMEN_OSTEOMUSCULAR_N002_ME_0000000046 = "NULL";
               }


               var SINTOMATICO_RESPIRATORIO_N009_ME_0000000131 = lComponentes.Find(p => p.ComponentId == "N009-ME000000131");
               if (SINTOMATICO_RESPIRATORIO_N009_ME_0000000131 != null)
               {
                   if (SINTOMATICO_RESPIRATORIO_N009_ME_0000000131.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.SINTOMATICO_RESPIRATORIO_N009_ME_0000000131 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.SINTOMATICO_RESPIRATORIO_N009_ME_0000000131 = SINTOMATICO_RESPIRATORIO_N009_ME_0000000131.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.SINTOMATICO_RESPIRATORIO_N009_ME_0000000131 = "NULL";
               }


               var TAMIZAJE_DERMATOLOGICO_N009_ME_0000000044 = lComponentes.Find(p => p.ComponentId == "N009-ME000000044");
               if (TAMIZAJE_DERMATOLOGICO_N009_ME_0000000044 != null)
               {
                   if (TAMIZAJE_DERMATOLOGICO_N009_ME_0000000044.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.TAMIZAJE_DERMATOLOGICO_N009_ME_0000000044 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.TAMIZAJE_DERMATOLOGICO_N009_ME_0000000044 = TAMIZAJE_DERMATOLOGICO_N009_ME_0000000044.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.TAMIZAJE_DERMATOLOGICO_N009_ME_0000000044 = "NULL";
               }


               var TEST_DE_VERTIGO_N009_ME_0000000090 = lComponentes.Find(p => p.ComponentId == "N009-ME000000090");
               if (TEST_DE_VERTIGO_N009_ME_0000000090 != null)
               {
                   if (TEST_DE_VERTIGO_N009_ME_0000000090.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.TEST_DE_VERTIGO_N009_ME_0000000090 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.TEST_DE_VERTIGO_N009_ME_0000000090 = TEST_DE_VERTIGO_N009_ME_0000000090.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.TEST_DE_VERTIGO_N009_ME_0000000090 = "NULL";
               }


               var AUDIOMETRlA_N002_ME_0000000005 = lComponentes.Find(p => p.ComponentId == "N002-ME000000005");
               if (AUDIOMETRlA_N002_ME_0000000005 != null)
               {
                   if (AUDIOMETRlA_N002_ME_0000000005.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.AUDIOMETRlA_N002_ME_0000000005 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.AUDIOMETRlA_N002_ME_0000000005 = AUDIOMETRlA_N002_ME_0000000005.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.AUDIOMETRlA_N002_ME_0000000005 = "NULL";
               }

               var ELECTROCARDIOGRAMA_N002_ME_0000000025 = lComponentes.Find(p => p.ComponentId == "N002-ME000000025");
               if (ELECTROCARDIOGRAMA_N002_ME_0000000025 != null)
               {
                   if (ELECTROCARDIOGRAMA_N002_ME_0000000025.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.ELECTROCARDIOGRAMA_N002_ME_0000000025 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.ELECTROCARDIOGRAMA_N002_ME_0000000025 = ELECTROCARDIOGRAMA_N002_ME_0000000025.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.ELECTROCARDIOGRAMA_N002_ME_0000000025 = "NULL";
               }

               var PRUEBA_DE_ESFUERZO_N002_ME_0000000029 = lComponentes.Find(p => p.ComponentId == "N002-ME000000029");
               if (PRUEBA_DE_ESFUERZO_N002_ME_0000000029 != null)
               {
                   if (PRUEBA_DE_ESFUERZO_N002_ME_0000000029.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.PRUEBA_DE_ESFUERZO_N002_ME_0000000029 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.PRUEBA_DE_ESFUERZO_N002_ME_0000000029 = PRUEBA_DE_ESFUERZO_N002_ME_0000000029.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.PRUEBA_DE_ESFUERZO_N002_ME_0000000029 = "NULL";
               }

               var ESPIROMETRIA_N002_ME_0000000031 = lComponentes.Find(p => p.ComponentId == "N002-ME000000031");
               if (ESPIROMETRIA_N002_ME_0000000031 != null)
               {
                   if (ESPIROMETRIA_N002_ME_0000000031.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.ESPIROMETRIA_N002_ME_0000000031 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.ESPIROMETRIA_N002_ME_0000000031 = ESPIROMETRIA_N002_ME_0000000031.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.ESPIROMETRIA_N002_ME_0000000031 = "NULL";
               }


               var ODONTOGRAMA_N002_ME_0000000027 = lComponentes.Find(p => p.ComponentId == "N002-ME000000027");
               if (ODONTOGRAMA_N002_ME_0000000027 != null)
               {
                   if (ODONTOGRAMA_N002_ME_0000000027.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.ODONTOGRAMA_N002_ME_0000000027 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.ODONTOGRAMA_N002_ME_0000000027 = ODONTOGRAMA_N002_ME_0000000027.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.ODONTOGRAMA_N002_ME_0000000027 = "NULL";
               }

                
               var OFTALMOLOGIA__N002_ME_0000000028 = lComponentes.Find(p => p.ComponentId == "N002-ME000000028");
               if (OFTALMOLOGIA__N002_ME_0000000028 != null)
               {
                   if (OFTALMOLOGIA__N002_ME_0000000028.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.OFTALMOLOGIA__N002_ME_0000000028 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.OFTALMOLOGIA__N002_ME_0000000028 = OFTALMOLOGIA__N002_ME_0000000028.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.OFTALMOLOGIA__N002_ME_0000000028 = "NULL";
               }


               var RADIOGRAFIA_DE_COLUMNA_CERVICO_DORSO_LUMBAR_N009_ME_0000000302 = lComponentes.Find(p => p.ComponentId == "N009-ME000000302");
               if (RADIOGRAFIA_DE_COLUMNA_CERVICO_DORSO_LUMBAR_N009_ME_0000000302 != null)
               {
                   if (RADIOGRAFIA_DE_COLUMNA_CERVICO_DORSO_LUMBAR_N009_ME_0000000302.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.RADIOGRAFIA_DE_COLUMNA_CERVICO_DORSO_LUMBAR_N009_ME_0000000302 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.RADIOGRAFIA_DE_COLUMNA_CERVICO_DORSO_LUMBAR_N009_ME_0000000302 = RADIOGRAFIA_DE_COLUMNA_CERVICO_DORSO_LUMBAR_N009_ME_0000000302.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.RADIOGRAFIA_DE_COLUMNA_CERVICO_DORSO_LUMBAR_N009_ME_0000000302 = "NULL";
               }


               var RADIOGRAFIA_DE_TORAX_N002_ME_0000000032 = lComponentes.Find(p => p.ComponentId == "N002-ME000000032");
               if (RADIOGRAFIA_DE_TORAX_N002_ME_0000000032 != null)
               {
                   if (RADIOGRAFIA_DE_TORAX_N002_ME_0000000032.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.RADIOGRAFIA_DE_TORAX_N002_ME_0000000032 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.RADIOGRAFIA_DE_TORAX_N002_ME_0000000032 = RADIOGRAFIA_DE_TORAX_N002_ME_0000000032.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.RADIOGRAFIA_DE_TORAX_N002_ME_0000000032 = "NULL";
               }


               var RADIOGRAFIA_OIT_N009_ME_0000000062 = lComponentes.Find(p => p.ComponentId == "N009-ME000000062");
               if (RADIOGRAFIA_OIT_N009_ME_0000000062 != null)
               {
                   if (RADIOGRAFIA_OIT_N009_ME_0000000062.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.RADIOGRAFIA_OIT_N009_ME_0000000062 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.RADIOGRAFIA_OIT_N009_ME_0000000062 = RADIOGRAFIA_OIT_N009_ME_0000000062.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.RADIOGRAFIA_OIT_N009_ME_0000000062 = "NULL";
               }



               var RADIOGRAFIA_LUMBOSACRA_N009_ME_0000000130 = lComponentes.Find(p => p.ComponentId == "N009-ME000000130");
               if (RADIOGRAFIA_LUMBOSACRA_N009_ME_0000000130 != null)
               {
                   if (RADIOGRAFIA_LUMBOSACRA_N009_ME_0000000130.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.RADIOGRAFIA_LUMBOSACRA_N009_ME_0000000130 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.RADIOGRAFIA_LUMBOSACRA_N009_ME_0000000130 = RADIOGRAFIA_LUMBOSACRA_N009_ME_0000000130.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.RADIOGRAFIA_LUMBOSACRA_N009_ME_0000000130 = "NULL";
               }

               var ANTROPOMETRIA_N002_ME_0000000002 = lComponentes.Find(p => p.ComponentId == "N002-ME000000002");
               if (ANTROPOMETRIA_N002_ME_0000000002 != null)
               {
                   if (ANTROPOMETRIA_N002_ME_0000000002.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.ANTROPOMETRIA_N002_ME_0000000002 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.ANTROPOMETRIA_N002_ME_0000000002 = ANTROPOMETRIA_N002_ME_0000000002.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.ANTROPOMETRIA_N002_ME_0000000002 = "NULL";
               }

               var FUNCIONES_VITALES_N002_ME_0000000001 = lComponentes.Find(p => p.ComponentId == "N002-ME000000001");
               if (FUNCIONES_VITALES_N002_ME_0000000001 != null)
               {
                   if (FUNCIONES_VITALES_N002_ME_0000000001.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.FUNCIONES_VITALES_N002_ME_0000000001 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.FUNCIONES_VITALES_N002_ME_0000000001 = FUNCIONES_VITALES_N002_ME_0000000001.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.FUNCIONES_VITALES_N002_ME_0000000001 = "NULL";
               }

               var PSICOLOGIA_N002_M5_0000000033 = lComponentes.Find(p => p.ComponentId == "N002-ME000000033");
               if (PSICOLOGIA_N002_M5_0000000033 != null)
               {
                   if (PSICOLOGIA_N002_M5_0000000033.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.PSICOLOGIA_N002_M5_0000000033 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.PSICOLOGIA_N002_M5_0000000033 = PSICOLOGIA_N002_M5_0000000033.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.PSICOLOGIA_N002_M5_0000000033 = "NULL";
               }


               var ACIDO_URICO_N009_ME_0000000086 = lComponentes.Find(p => p.ComponentId == "N009-ME000000086");
               if (ACIDO_URICO_N009_ME_0000000086 != null)
               {
                   if (ACIDO_URICO_N009_ME_0000000086.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.ACIDO_URICO_N009_ME_0000000086 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.ACIDO_URICO_N009_ME_0000000086 = ACIDO_URICO_N009_ME_0000000086.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.ACIDO_URICO_N009_ME_0000000086 = "NULL";
               }


               var AGLUTINACIONES_EN_LAMINA_N009_ME_0000000025 = lComponentes.Find(p => p.ComponentId == "N009-ME000000025");
               if (AGLUTINACIONES_EN_LAMINA_N009_ME_0000000025 != null)
               {
                   if (AGLUTINACIONES_EN_LAMINA_N009_ME_0000000025.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.AGLUTINACIONES_EN_LAMINA_N009_ME_0000000025 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.AGLUTINACIONES_EN_LAMINA_N009_ME_0000000025 = AGLUTINACIONES_EN_LAMINA_N009_ME_0000000025.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.AGLUTINACIONES_EN_LAMINA_N009_ME_0000000025 = "NULL";
               }

               var AMILASA_N009_ME_000000165 = lComponentes.Find(p => p.ComponentId == "N009-ME000000165");
               if (AMILASA_N009_ME_000000165 != null)
               {
                   if (AMILASA_N009_ME_000000165.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.AMILASA_N009_ME_000000165 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.AMILASA_N009_ME_000000165 = AMILASA_N009_ME_000000165.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.AMILASA_N009_ME_000000165 = "NULL";
               }

               var ANT1ESTROPTOLINASA_O_N009_ME_0000000208 = lComponentes.Find(p => p.ComponentId == "N009-ME000000208");
               if (ANT1ESTROPTOLINASA_O_N009_ME_0000000208 != null)
               {
                   if (ANT1ESTROPTOLINASA_O_N009_ME_0000000208.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.ANT1ESTROPTOLINASA_O_N009_ME_0000000208 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.ANT1ESTROPTOLINASA_O_N009_ME_0000000208 = ANT1ESTROPTOLINASA_O_N009_ME_0000000208.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.ANT1ESTROPTOLINASA_O_N009_ME_0000000208 = "NULL";
               }

               var ANTIGENO_PROSTATICO_N009_ME_0000000009 = lComponentes.Find(p => p.ComponentId == "N009-ME00000009");
               if (ANTIGENO_PROSTATICO_N009_ME_0000000009 != null)
               {
                   if (ANTIGENO_PROSTATICO_N009_ME_0000000009.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.ANTIGENO_PROSTATICO_N009_ME_0000000009 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.ANTIGENO_PROSTATICO_N009_ME_0000000009 = ANTIGENO_PROSTATICO_N009_ME_0000000009.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.ANTIGENO_PROSTATICO_N009_ME_0000000009 = "NULL";
               }

               var BENZENO_EXPOSICION_A_GAS_NATURAL_N009_ME_0000000087 = lComponentes.Find(p => p.ComponentId == "N009-ME000000087");
               if (BENZENO_EXPOSICION_A_GAS_NATURAL_N009_ME_0000000087 != null)
               {
                   if (BENZENO_EXPOSICION_A_GAS_NATURAL_N009_ME_0000000087.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.BENZENO_EXPOSICION_A_GAS_NATURAL_N009_ME_0000000087 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.BENZENO_EXPOSICION_A_GAS_NATURAL_N009_ME_0000000087 = BENZENO_EXPOSICION_A_GAS_NATURAL_N009_ME_0000000087.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.BENZENO_EXPOSICION_A_GAS_NATURAL_N009_ME_0000000087 = "NULL";
               }


                //////////////////////////////////////////////////////////////////////////////////////////////////
               var BK_D_RECTO_N009_ME_0000000081 = lComponentes.Find(p => p.ComponentId == "N009-ME000000081");
               if (BK_D_RECTO_N009_ME_0000000081 != null)
               {
                   if (BK_D_RECTO_N009_ME_0000000081.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.BK_D_RECTO_N009_ME_0000000081 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.BK_D_RECTO_N009_ME_0000000081 = BK_D_RECTO_N009_ME_0000000081.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.BK_D_RECTO_N009_ME_0000000081 = "NULL";
               }

               var COLESTE_ROL_TOTAL_N009_ME_0000000016 = lComponentes.Find(p => p.ComponentId == "N009-ME000000016");
               if (COLESTE_ROL_TOTAL_N009_ME_0000000016 != null)
               {
                   if (COLESTE_ROL_TOTAL_N009_ME_0000000016.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.COLESTE_ROL_TOTAL_N009_ME_0000000016 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.COLESTE_ROL_TOTAL_N009_ME_0000000016 = COLESTE_ROL_TOTAL_N009_ME_0000000016.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.COLESTE_ROL_TOTAL_N009_ME_0000000016 = "NULL";
               }


               var COPROCULT1VO_N009_ME_0000000264 = lComponentes.Find(p => p.ComponentId == "N009-ME000000264");
               if (COPROCULT1VO_N009_ME_0000000264 != null)
               {
                   if (COPROCULT1VO_N009_ME_0000000264.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.COPROCULT1VO_N009_ME_0000000264 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.COPROCULT1VO_N009_ME_0000000264 = COPROCULT1VO_N009_ME_0000000264.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.COPROCULT1VO_N009_ME_0000000264 = "NULL";
               }


               var CREATININA_N009_ME_0000000028 = lComponentes.Find(p => p.ComponentId == "N009-ME000000028");
               if (CREATININA_N009_ME_0000000028 != null)
               {
                   if (CREATININA_N009_ME_0000000028.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.CREATININA_N009_ME_0000000028 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.CREATININA_N009_ME_0000000028 = CREATININA_N009_ME_0000000028.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.CREATININA_N009_ME_0000000028 = "NULL";
               }


               var DENGUE_DUO_N009_ME_0000000304 = lComponentes.Find(p => p.ComponentId == "N009-ME000000304");
               if (DENGUE_DUO_N009_ME_0000000304 != null)
               {
                   if (DENGUE_DUO_N009_ME_0000000304.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.DENGUE_DUO_N009_ME_0000000304 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.DENGUE_DUO_N009_ME_0000000304 = DENGUE_DUO_N009_ME_0000000304.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.DENGUE_DUO_N009_ME_0000000304 = "NULL";
               }


               var DEPUR_DE_CREATININA_N009_ME_0000000164 = lComponentes.Find(p => p.ComponentId == "N009-ME000000164");
               if (DEPUR_DE_CREATININA_N009_ME_0000000164 != null)
               {
                   if (DEPUR_DE_CREATININA_N009_ME_0000000164.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.DEPUR_DE_CREATININA_N009_ME_0000000164 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.DEPUR_DE_CREATININA_N009_ME_0000000164 = DEPUR_DE_CREATININA_N009_ME_0000000164.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.DEPUR_DE_CREATININA_N009_ME_0000000164 = "NULL";
               }


               var DHL__1_N009_ME_0000000181 = lComponentes.Find(p => p.ComponentId == "N009-ME000000181");
               if (DHL__1_N009_ME_0000000181 != null)
               {
                   if (DHL__1_N009_ME_0000000181.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.DHL__1_N009_ME_0000000181 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.DHL__1_N009_ME_0000000181 = DHL__1_N009_ME_0000000181.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.DHL__1_N009_ME_0000000181 = "NULL";
               }


               var EXAMEN_COMPLETO_DE_ORINA_N009_ME_0000000046 = lComponentes.Find(p => p.ComponentId == "N009-ME000000046");
               if (EXAMEN_COMPLETO_DE_ORINA_N009_ME_0000000046 != null)
               {
                   if (EXAMEN_COMPLETO_DE_ORINA_N009_ME_0000000046.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.EXAMEN_COMPLETO_DE_ORINA_N009_ME_0000000046 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.EXAMEN_COMPLETO_DE_ORINA_N009_ME_0000000046 = EXAMEN_COMPLETO_DE_ORINA_N009_ME_0000000046.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.EXAMEN_COMPLETO_DE_ORINA_N009_ME_0000000046 = "NULL";
               }

               var EXAMEN_DE_ELISA_M_IV_N009_ME_0000000030 = lComponentes.Find(p => p.ComponentId == "N009-ME000000030");
               if (EXAMEN_DE_ELISA_M_IV_N009_ME_0000000030 != null)
               {
                   if (EXAMEN_DE_ELISA_M_IV_N009_ME_0000000030.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.EXAMEN_DE_ELISA_M_IV_N009_ME_0000000030 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.EXAMEN_DE_ELISA_M_IV_N009_ME_0000000030 = EXAMEN_DE_ELISA_M_IV_N009_ME_0000000030.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.EXAMEN_DE_ELISA_M_IV_N009_ME_0000000030 = "NULL";
               }


               var EXAMENKOH_N009_ME_0000000122 = lComponentes.Find(p => p.ComponentId == "N009-ME000000122");
               if (EXAMENKOH_N009_ME_0000000122 != null)
               {
                   if (EXAMENKOH_N009_ME_0000000122.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.EXAMENKOH_N009_ME_0000000122 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.EXAMENKOH_N009_ME_0000000122 = EXAMENKOH_N009_ME_0000000122.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.EXAMENKOH_N009_ME_0000000122 = "NULL";
               }




                //////////////////////////////////////////////////////////////////////////////////////////////////
               var FECATEST_N009_ME_0000000097 = lComponentes.Find(p => p.ComponentId == "N009-ME000000097");
               if (FECATEST_N009_ME_0000000097 != null)
               {
                   if (FECATEST_N009_ME_0000000097.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.FECATEST_N009_ME_0000000097 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.FECATEST_N009_ME_0000000097 = FECATEST_N009_ME_0000000097.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.FECATEST_N009_ME_0000000097 = "NULL";
               }

               var FIBRINOGENO_N009_ME_0000000149 = lComponentes.Find(p => p.ComponentId == "N009-ME000000149");
               if (FIBRINOGENO_N009_ME_0000000149 != null)
               {
                   if (FIBRINOGENO_N009_ME_0000000149.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.FIBRINOGENO_N009_ME_0000000149 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.FIBRINOGENO_N009_ME_0000000149 = FIBRINOGENO_N009_ME_0000000149.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.FIBRINOGENO_N009_ME_0000000149 = "NULL";
               }


               var FTA_ABS_N009_ME_0000000233 = lComponentes.Find(p => p.ComponentId == "N009-ME000000233");
               if (FTA_ABS_N009_ME_0000000233 != null)
               {
                   if (FTA_ABS_N009_ME_0000000233.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.FTA_ABS_N009_ME_0000000233 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.FTA_ABS_N009_ME_0000000233 = FTA_ABS_N009_ME_0000000233.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.FTA_ABS_N009_ME_0000000233 = "NULL";
               }


               var GLUCOSA_N009_ME_0000000008 = lComponentes.Find(p => p.ComponentId == "N009-ME00000008");
               if (GLUCOSA_N009_ME_0000000008 != null)
               {
                   if (GLUCOSA_N009_ME_0000000008.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.GLUCOSA_N009_ME_0000000008 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.GLUCOSA_N009_ME_0000000008 = GLUCOSA_N009_ME_0000000008.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.GLUCOSA_N009_ME_0000000008 = "NULL";
               }


               var GRUPO_Y_FACTOR_SANGUINEO_N009_ME_0000000000 = lComponentes.Find(p => p.ComponentId == "N009-ME000000000");
               if (GRUPO_Y_FACTOR_SANGUINEO_N009_ME_0000000000 != null)
               {
                   if (GRUPO_Y_FACTOR_SANGUINEO_N009_ME_0000000000.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.GRUPO_Y_FACTOR_SANGUINEO_N009_ME_0000000000 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.GRUPO_Y_FACTOR_SANGUINEO_N009_ME_0000000000 = GRUPO_Y_FACTOR_SANGUINEO_N009_ME_0000000000.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.GRUPO_Y_FACTOR_SANGUINEO_N009_ME_0000000000 = "NULL";
               }


               var H3sAS_N009_ME_0000000121 = lComponentes.Find(p => p.ComponentId == "N009-ME000000121");
               if (H3sAS_N009_ME_0000000121 != null)
               {
                   if (H3sAS_N009_ME_0000000121.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.H3sAS_N009_ME_0000000121 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.H3sAS_N009_ME_0000000121 = H3sAS_N009_ME_0000000121.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.H3sAS_N009_ME_0000000121 = "NULL";
               }


               var HCV_N009_ME_0000000120 = lComponentes.Find(p => p.ComponentId == "N009-ME000000120");
               if (HCV_N009_ME_0000000120 != null)
               {
                   if (HCV_N009_ME_0000000120.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.HCV_N009_ME_0000000120 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.HCV_N009_ME_0000000120 = HCV_N009_ME_0000000120.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.HCV_N009_ME_0000000120 = "NULL";
               }

               var HEMATOCRITO_N009_ME_0000000001 = lComponentes.Find(p => p.ComponentId == "N009-ME00000001");
               if (HEMATOCRITO_N009_ME_0000000001 != null)
               {
                   if (HEMATOCRITO_N009_ME_0000000001.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.HEMATOCRITO_N009_ME_0000000001 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.HEMATOCRITO_N009_ME_0000000001 = HEMATOCRITO_N009_ME_0000000001.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.HEMATOCRITO_N009_ME_0000000001 = "NULL";
               }


               var HEMOGLOBINA_N009_ME_0000000006 = lComponentes.Find(p => p.ComponentId == "N009-ME00000006");
               if (HEMOGLOBINA_N009_ME_0000000006 != null)
               {
                   if (HEMOGLOBINA_N009_ME_0000000006.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.HEMOGLOBINA_N009_ME_0000000006 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.HEMOGLOBINA_N009_ME_0000000006 = HEMOGLOBINA_N009_ME_0000000006.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.HEMOGLOBINA_N009_ME_0000000006 = "NULL";
               }


               var HEMOGLOBINA_GLCOSILADA_N009_ME_0000000154 = lComponentes.Find(p => p.ComponentId == "N009-ME000000154");
               if (HEMOGLOBINA_GLCOSILADA_N009_ME_0000000154 != null)
               {
                   if (HEMOGLOBINA_GLCOSILADA_N009_ME_0000000154.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.HEMOGLOBINA_GLCOSILADA_N009_ME_0000000154 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.HEMOGLOBINA_GLCOSILADA_N009_ME_0000000154 = HEMOGLOBINA_GLCOSILADA_N009_ME_0000000154.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.HEMOGLOBINA_GLCOSILADA_N009_ME_0000000154 = "NULL";
               }


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
               var HEMOGRAMA_AUTOMATIZADO_N009_ME_0000000113 = lComponentes.Find(p => p.ComponentId == "N009-ME000000113");
               if (HEMOGRAMA_AUTOMATIZADO_N009_ME_0000000113 != null)
               {
                   if (HEMOGRAMA_AUTOMATIZADO_N009_ME_0000000113.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.HEMOGRAMA_AUTOMATIZADO_N009_ME_0000000113 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.HEMOGRAMA_AUTOMATIZADO_N009_ME_0000000113 = HEMOGRAMA_AUTOMATIZADO_N009_ME_0000000113.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.HEMOGRAMA_AUTOMATIZADO_N009_ME_0000000113 = "NULL";
               }

               var HEMOGRAMA_COMPLETO_N009_ME_0000000045 = lComponentes.Find(p => p.ComponentId == "N009-ME000000045");
               if (HEMOGRAMA_COMPLETO_N009_ME_0000000045 != null)
               {
                   if (HEMOGRAMA_COMPLETO_N009_ME_0000000045.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.HEMOGRAMA_COMPLETO_N009_ME_0000000045 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.HEMOGRAMA_COMPLETO_N009_ME_0000000045 = HEMOGRAMA_COMPLETO_N009_ME_0000000045.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.HEMOGRAMA_COMPLETO_N009_ME_0000000045 = "NULL";
               }


               var HEPATITIS_A_N009_ME_0000000004 = lComponentes.Find(p => p.ComponentId == "N009-ME00000004");
               if (HEPATITIS_A_N009_ME_0000000004 != null)
               {
                   if (HEPATITIS_A_N009_ME_0000000004.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.HEPATITIS_A_N009_ME_0000000004 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.HEPATITIS_A_N009_ME_0000000004 = HEPATITIS_A_N009_ME_0000000004.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.HEPATITIS_A_N009_ME_0000000004 = "NULL";
               }


               var HEPATITIS_B_N009_ME_0000000301 = lComponentes.Find(p => p.ComponentId == "N009-ME000000301");
               if (HEPATITIS_B_N009_ME_0000000301 != null)
               {
                   if (HEPATITIS_B_N009_ME_0000000301.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.HEPATITIS_B_N009_ME_0000000301 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.HEPATITIS_B_N009_ME_0000000301 = HEPATITIS_B_N009_ME_0000000301.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.HEPATITIS_B_N009_ME_0000000301 = "NULL";
               }


               var HEPATITIS_C_N009_ME_0000000005 = lComponentes.Find(p => p.ComponentId == "N009-ME00000005");
               if (HEPATITIS_C_N009_ME_0000000005 != null)
               {
                   if (HEPATITIS_C_N009_ME_0000000005.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.HEPATITIS_C_N009_ME_0000000005 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.HEPATITIS_C_N009_ME_0000000005 = HEPATITIS_C_N009_ME_0000000005.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.HEPATITIS_C_N009_ME_0000000005 = "NULL";
               }


               var HISOPADO_FARINGEO_N009_ME_0000000095 = lComponentes.Find(p => p.ComponentId == "N009-ME000000095");
               if (HISOPADO_FARINGEO_N009_ME_0000000095 != null)
               {
                   if (HISOPADO_FARINGEO_N009_ME_0000000095.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.HISOPADO_FARINGEO_N009_ME_0000000095 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.HISOPADO_FARINGEO_N009_ME_0000000095 = HISOPADO_FARINGEO_N009_ME_0000000095.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.HISOPADO_FARINGEO_N009_ME_0000000095 = "NULL";
               }


               var HISOPADO_NASOFARINGEO_N009_ME_0000000118 = lComponentes.Find(p => p.ComponentId == "N009-ME000000118");
               if (HISOPADO_NASOFARINGEO_N009_ME_0000000118 != null)
               {
                   if (HISOPADO_NASOFARINGEO_N009_ME_0000000118.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.HISOPADO_NASOFARINGEO_N009_ME_0000000118 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.HISOPADO_NASOFARINGEO_N009_ME_0000000118 = HISOPADO_NASOFARINGEO_N009_ME_0000000118.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.HISOPADO_NASOFARINGEO_N009_ME_0000000118 = "NULL";
               }


               var INFORME_DE_LABORATORIO_N009_ME_0000000002 = lComponentes.Find(p => p.ComponentId == "N009-ME000000002");
               if (INFORME_DE_LABORATORIO_N009_ME_0000000002 != null)
               {
                   if (INFORME_DE_LABORATORIO_N009_ME_0000000002.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.INFORME_DE_LABORATORIO_N009_ME_0000000002 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.INFORME_DE_LABORATORIO_N009_ME_0000000002 = INFORME_DE_LABORATORIO_N009_ME_0000000002.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.INFORME_DE_LABORATORIO_N009_ME_0000000002 = "NULL";
               }

               var INSULINA_BASAL_N009_ME_0000000125 = lComponentes.Find(p => p.ComponentId == "N009-ME000000125");
               if (INSULINA_BASAL_N009_ME_0000000125 != null)
               {
                   if (INSULINA_BASAL_N009_ME_0000000125.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.INSULINA_BASAL_N009_ME_0000000125 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.INSULINA_BASAL_N009_ME_0000000125 = INSULINA_BASAL_N009_ME_0000000125.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.INSULINA_BASAL_N009_ME_0000000125 = "NULL";
               }


               var LIPASA_N009_ME_0000000172 = lComponentes.Find(p => p.ComponentId == "N009-ME000000172");
               if (LIPASA_N009_ME_0000000172 != null)
               {
                   if (LIPASA_N009_ME_0000000172.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.LIPASA_N009_ME_0000000172 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.LIPASA_N009_ME_0000000172 = LIPASA_N009_ME_0000000172.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.LIPASA_N009_ME_0000000172 = "NULL";
               }

                ////////////////////////////////////////////////////////////////////////////
               var MCROALBUMINURIA_N009_ME_0000000117 = lComponentes.Find(p => p.ComponentId == "N009-ME000000117");
               if (MCROALBUMINURIA_N009_ME_0000000117 != null)
               {
                   if (MCROALBUMINURIA_N009_ME_0000000117.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.MCROALBUMINURIA_N009_ME_0000000117 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.MCROALBUMINURIA_N009_ME_0000000117 = MCROALBUMINURIA_N009_ME_0000000117.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.MCROALBUMINURIA_N009_ME_0000000117 = "NULL";
               }

               var PARASITOLOGICO_SERIADO_N009_ME_0000000049 = lComponentes.Find(p => p.ComponentId == "N009-ME000000049");
               if (PARASITOLOGICO_SERIADO_N009_ME_0000000049 != null)
               {
                   if (PARASITOLOGICO_SERIADO_N009_ME_0000000049.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.PARASITOLOGICO_SERIADO_N009_ME_0000000049 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.PARASITOLOGICO_SERIADO_N009_ME_0000000049 = PARASITOLOGICO_SERIADO_N009_ME_0000000049.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.PARASITOLOGICO_SERIADO_N009_ME_0000000049 = "NULL";
               }


               var PARASITOLOGICO_SIMPLE_N009_ME_0000000010 = lComponentes.Find(p => p.ComponentId == "N009-ME000000010");
               if (PARASITOLOGICO_SIMPLE_N009_ME_0000000010 != null)
               {
                   if (PARASITOLOGICO_SIMPLE_N009_ME_0000000010.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.PARASITOLOGICO_SIMPLE_N009_ME_0000000010 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.PARASITOLOGICO_SIMPLE_N009_ME_0000000010 = PARASITOLOGICO_SIMPLE_N009_ME_0000000010.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.PARASITOLOGICO_SIMPLE_N009_ME_0000000010 = "NULL";
               }


               var PCR_N009_ME_0000000205 = lComponentes.Find(p => p.ComponentId == "N009-ME000000205");
               if (PCR_N009_ME_0000000205 != null)
               {
                   if (PCR_N009_ME_0000000205.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.PCR_N009_ME_0000000205 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.PCR_N009_ME_0000000205 = PCR_N009_ME_0000000205.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.PCR_N009_ME_0000000205 = "NULL";
               }


               var PERFIL_HEPATICO_N009_ME_0000000095 = lComponentes.Find(p => p.ComponentId == "N009-ME000000095");
               if (PERFIL_HEPATICO_N009_ME_0000000095 != null)
               {
                   if (PERFIL_HEPATICO_N009_ME_0000000095.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.PERFIL_HEPATICO_N009_ME_0000000095 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.PERFIL_HEPATICO_N009_ME_0000000095 = PERFIL_HEPATICO_N009_ME_0000000095.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.PERFIL_HEPATICO_N009_ME_0000000095 = "NULL";
               }


               var PERFIL_LIPIDICO_N009_ME_0000000114 = lComponentes.Find(p => p.ComponentId == "N009-ME000000114");
               if (PERFIL_LIPIDICO_N009_ME_0000000114 != null)
               {
                   if (PERFIL_LIPIDICO_N009_ME_0000000114.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.PERFIL_LIPIDICO_N009_ME_0000000114 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.PERFIL_LIPIDICO_N009_ME_0000000114 = PERFIL_LIPIDICO_N009_ME_0000000114.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.PERFIL_LIPIDICO_N009_ME_0000000114 = "NULL";
               }


               var PERFIL_TIROIDEO_N009_ME_0000000305 = lComponentes.Find(p => p.ComponentId == "N009-ME000000305");
               if (PERFIL_TIROIDEO_N009_ME_0000000305 != null)
               {
                   if (PERFIL_TIROIDEO_N009_ME_0000000305.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.PERFIL_TIROIDEO_N009_ME_0000000305 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.PERFIL_TIROIDEO_N009_ME_0000000305 = PERFIL_TIROIDEO_N009_ME_0000000305.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.PERFIL_TIROIDEO_N009_ME_0000000305 = "NULL";
               }


               var PLOMO_EN_SANGRE_N009_ME_0000000060 = lComponentes.Find(p => p.ComponentId == "N009-ME000000060");
               if (PLOMO_EN_SANGRE_N009_ME_0000000060 != null)
               {
                   if (PLOMO_EN_SANGRE_N009_ME_0000000060.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.PLOMO_EN_SANGRE_N009_ME_0000000060 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.PLOMO_EN_SANGRE_N009_ME_0000000060 = PLOMO_EN_SANGRE_N009_ME_0000000060.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.PLOMO_EN_SANGRE_N009_ME_0000000060 = "NULL";
               }

               var PROTEINAS_T_Y_FRACC_N009_ME_0000000170 = lComponentes.Find(p => p.ComponentId == "N009-ME000000170");
               if (PROTEINAS_T_Y_FRACC_N009_ME_0000000170 != null)
               {
                   if (PROTEINAS_T_Y_FRACC_N009_ME_0000000170.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.PROTEINAS_T_Y_FRACC_N009_ME_0000000170 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.PROTEINAS_T_Y_FRACC_N009_ME_0000000170 = PROTEINAS_T_Y_FRACC_N009_ME_0000000170.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.PROTEINAS_T_Y_FRACC_N009_ME_0000000170 = "NULL";
               }


               var PROTEINURIA_DE_24_H_N009_ME_0000000165 = lComponentes.Find(p => p.ComponentId == "N009-ME000000165");
               if (PROTEINURIA_DE_24_H_N009_ME_0000000165 != null)
               {
                   if (PROTEINURIA_DE_24_H_N009_ME_0000000165.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.PROTEINURIA_DE_24_H_N009_ME_0000000165 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.PROTEINURIA_DE_24_H_N009_ME_0000000165 = PROTEINURIA_DE_24_H_N009_ME_0000000165.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.PROTEINURIA_DE_24_H_N009_ME_0000000165 = "NULL";
               }


                ////////////////////////////////////////////////////////////////////////////////////////

               var RCTO_DE_PLAQUETAS_N009_ME_0000000146 = lComponentes.Find(p => p.ComponentId == "N009-ME000000146");
               if (RCTO_DE_PLAQUETAS_N009_ME_0000000146 != null)
               {
                   if (RCTO_DE_PLAQUETAS_N009_ME_0000000146.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.RCTO_DE_PLAQUETAS_N009_ME_0000000146 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.RCTO_DE_PLAQUETAS_N009_ME_0000000146 = RCTO_DE_PLAQUETAS_N009_ME_0000000146.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.RCTO_DE_PLAQUETAS_N009_ME_0000000146 = "NULL";
               }



               var REACCION_INFLAMATORIA_N009_ME_0000000119 = lComponentes.Find(p => p.ComponentId == "N009-ME000000119");
               if (REACCION_INFLAMATORIA_N009_ME_0000000119 != null)
               {
                   if (REACCION_INFLAMATORIA_N009_ME_0000000119.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.REACCION_INFLAMATORIA_N009_ME_0000000119 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.REACCION_INFLAMATORIA_N009_ME_0000000119 = REACCION_INFLAMATORIA_N009_ME_0000000119.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.REACCION_INFLAMATORIA_N009_ME_0000000119 = "NULL";
               }



               var SUB_UNIDAD_BETA_CUALITATIVO_N009_ME_0000000027 = lComponentes.Find(p => p.ComponentId == "N009-ME000000027");
               if (SUB_UNIDAD_BETA_CUALITATIVO_N009_ME_0000000027 != null)
               {
                   if (SUB_UNIDAD_BETA_CUALITATIVO_N009_ME_0000000027.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.SUB_UNIDAD_BETA_CUALITATIVO_N009_ME_0000000027 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.SUB_UNIDAD_BETA_CUALITATIVO_N009_ME_0000000027 = SUB_UNIDAD_BETA_CUALITATIVO_N009_ME_0000000027.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.SUB_UNIDAD_BETA_CUALITATIVO_N009_ME_0000000027 = "NULL";
               }



               var SUB_UNIDAD_BETA_CUANTITATIVO_N002_ME_0000000015 = lComponentes.Find(p => p.ComponentId == "N002-ME000000015");
               if (SUB_UNIDAD_BETA_CUANTITATIVO_N002_ME_0000000015 != null)
               {
                   if (SUB_UNIDAD_BETA_CUANTITATIVO_N002_ME_0000000015.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.SUB_UNIDAD_BETA_CUANTITATIVO_N002_ME_0000000015 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.SUB_UNIDAD_BETA_CUANTITATIVO_N002_ME_0000000015 = SUB_UNIDAD_BETA_CUANTITATIVO_N002_ME_0000000015.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.SUB_UNIDAD_BETA_CUANTITATIVO_N002_ME_0000000015 = "NULL";
               }



               var TGO_N009_ME_0000000054 = lComponentes.Find(p => p.ComponentId == "N009-ME000000054");
               if (TGO_N009_ME_0000000054 != null)
               {
                   if (TGO_N009_ME_0000000054.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.TGO_N009_ME_0000000054 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.TGO_N009_ME_0000000054 = TGO_N009_ME_0000000054.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.TGO_N009_ME_0000000054 = "NULL";
               }



               var TGP_N009_ME_0000000055 = lComponentes.Find(p => p.ComponentId == "N009-ME000000055");
               if (TGP_N009_ME_0000000055 != null)
               {
                   if (TGP_N009_ME_0000000055.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.TGP_N009_ME_0000000055 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.TGP_N009_ME_0000000055 = TGP_N009_ME_0000000055.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.TGP_N009_ME_0000000055 = "NULL";
               }



               var T_DE_PROTOMBINA_N009_ME_0000000147 = lComponentes.Find(p => p.ComponentId == "N009-ME000000147");
               if (T_DE_PROTOMBINA_N009_ME_0000000147 != null)
               {
                   if (T_DE_PROTOMBINA_N009_ME_0000000147.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.T_DE_PROTOMBINA_N009_ME_0000000147 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.T_DE_PROTOMBINA_N009_ME_0000000147 = T_DE_PROTOMBINA_N009_ME_0000000147.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.T_DE_PROTOMBINA_N009_ME_0000000147 = "NULL";
               }



               var T_PARCIAL_DE_TROMBOPL_N009_ME_0000000148 = lComponentes.Find(p => p.ComponentId == "N009-ME000000148");
               if (T_PARCIAL_DE_TROMBOPL_N009_ME_0000000148 != null)
               {
                   if (T_PARCIAL_DE_TROMBOPL_N009_ME_0000000148.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.T_PARCIAL_DE_TROMBOPL_N009_ME_0000000148 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.T_PARCIAL_DE_TROMBOPL_N009_ME_0000000148 = T_PARCIAL_DE_TROMBOPL_N009_ME_0000000148.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.T_PARCIAL_DE_TROMBOPL_N009_ME_0000000148 = "NULL";
               }



               var T3_UP_TAKE_N009_ME_0000000280 = lComponentes.Find(p => p.ComponentId == "N009-ME000000280");
               if (T3_UP_TAKE_N009_ME_0000000280 != null)
               {
                   if (T3_UP_TAKE_N009_ME_0000000280.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.T3_UP_TAKE_N009_ME_0000000280 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.T3_UP_TAKE_N009_ME_0000000280 = T3_UP_TAKE_N009_ME_0000000280.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.T3_UP_TAKE_N009_ME_0000000280 = "NULL";
               }



               var T4_LIBRE_DOSAJE_N009_ME_0000000279 = lComponentes.Find(p => p.ComponentId == "N009-ME000000279");
               if (T4_LIBRE_DOSAJE_N009_ME_0000000279 != null)
               {
                   if (T4_LIBRE_DOSAJE_N009_ME_0000000279.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.T4_LIBRE_DOSAJE_N009_ME_0000000279 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.T4_LIBRE_DOSAJE_N009_ME_0000000279 = T4_LIBRE_DOSAJE_N009_ME_0000000279.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.T4_LIBRE_DOSAJE_N009_ME_0000000279 = "NULL";
               }



////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

               var TECNICA_DE_GRAHAM_N009_ME_0000000298 = lComponentes.Find(p => p.ComponentId == "N009-ME000000298");
               if (TECNICA_DE_GRAHAM_N009_ME_0000000298 != null)
               {
                   if (TECNICA_DE_GRAHAM_N009_ME_0000000298.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.TECNICA_DE_GRAHAM_N009_ME_0000000298 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.TECNICA_DE_GRAHAM_N009_ME_0000000298 = TECNICA_DE_GRAHAM_N009_ME_0000000298.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.TECNICA_DE_GRAHAM_N009_ME_0000000298 = "NULL";
               }



               var THENEVON_N009_ME_0000000299 = lComponentes.Find(p => p.ComponentId == "N009-ME000000299");
               if (THENEVON_N009_ME_0000000299 != null)
               {
                   if (THENEVON_N009_ME_0000000299.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.THENEVON_N009_ME_0000000299 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.THENEVON_N009_ME_0000000299 = THENEVON_N009_ME_0000000299.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.THENEVON_N009_ME_0000000299 = "NULL";
               }



               var TIEMPO_DE_COAGULACION_N009_ME_0000000124 = lComponentes.Find(p => p.ComponentId == "N009-ME000000124");
               if (TIEMPO_DE_COAGULACION_N009_ME_0000000124 != null)
               {
                   if (TIEMPO_DE_COAGULACION_N009_ME_0000000124.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.TIEMPO_DE_COAGULACION_N009_ME_0000000124 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.TIEMPO_DE_COAGULACION_N009_ME_0000000124 = TIEMPO_DE_COAGULACION_N009_ME_0000000124.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.TIEMPO_DE_COAGULACION_N009_ME_0000000124 = "NULL";
               }



               var TIEMPO_DE_SANGRIA_N009_ME_0000000123 = lComponentes.Find(p => p.ComponentId == "N009-ME000000123");
               if (TIEMPO_DE_SANGRIA_N009_ME_0000000123 != null)
               {
                   if (TIEMPO_DE_SANGRIA_N009_ME_0000000123.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.TIEMPO_DE_SANGRIA_N009_ME_0000000123 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.TIEMPO_DE_SANGRIA_N009_ME_0000000123 = TIEMPO_DE_SANGRIA_N009_ME_0000000123.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.TIEMPO_DE_SANGRIA_N009_ME_0000000123 = "NULL";
               }



               var TOLERANCIA_A_LA_GLUCOSA_N009_ME_0000000151 = lComponentes.Find(p => p.ComponentId == "N009-ME000000151");
               if (TOLERANCIA_A_LA_GLUCOSA_N009_ME_0000000151 != null)
               {
                   if (TOLERANCIA_A_LA_GLUCOSA_N009_ME_0000000151.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.TOLERANCIA_A_LA_GLUCOSA_N009_ME_0000000151 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.TOLERANCIA_A_LA_GLUCOSA_N009_ME_0000000151 = TOLERANCIA_A_LA_GLUCOSA_N009_ME_0000000151.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.TOLERANCIA_A_LA_GLUCOSA_N009_ME_0000000151 = "NULL";
               }


               var TOXICOL0GICO_N009_ME_0000000303 = lComponentes.Find(p => p.ComponentId == "N009-ME000000303");
               if (TOXICOL0GICO_N009_ME_0000000303 != null)
               {
                   if (TOXICOL0GICO_N009_ME_0000000303.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.TOXICOL0GICO_N009_ME_0000000303 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.TOXICOL0GICO_N009_ME_0000000303 = TOXICOL0GICO_N009_ME_0000000303.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.TOXICOL0GICO_N009_ME_0000000303 = "NULL";
               }



               var TOXICOLOGICO_ALCOHOLEME_N009_ME_0000000041 = lComponentes.Find(p => p.ComponentId == "N009-ME000000041");
               if (TOXICOLOGICO_ALCOHOLEME_N009_ME_0000000041 != null)
               {
                   if (TOXICOLOGICO_ALCOHOLEME_N009_ME_0000000041.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.TOXICOLOGICO_ALCOHOLEME_N009_ME_0000000041 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.TOXICOLOGICO_ALCOHOLEME_N009_ME_0000000041 = TOXICOLOGICO_ALCOHOLEME_N009_ME_0000000041.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.TOXICOLOGICO_ALCOHOLEME_N009_ME_0000000041 = "NULL";
               }


               var TOXICOLOGICO_ANFETAMINAS_N009_ME_0000000043 = lComponentes.Find(p => p.ComponentId == "N009-ME000000043");
               if (TOXICOLOGICO_ANFETAMINAS_N009_ME_0000000043 != null)
               {
                   if (TOXICOLOGICO_ANFETAMINAS_N009_ME_0000000043.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.TOXICOLOGICO_ANFETAMINAS_N009_ME_0000000043 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.TOXICOLOGICO_ANFETAMINAS_N009_ME_0000000043 = TOXICOLOGICO_ANFETAMINAS_N009_ME_0000000043.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.TOXICOLOGICO_ANFETAMINAS_N009_ME_0000000043 = "NULL";
               }


               var TOXICOLOGICO_3ENZOD1ACEPIÑAS_N009_ME_0000000040 = lComponentes.Find(p => p.ComponentId == "N009-ME000000040");
               if (TOXICOLOGICO_3ENZOD1ACEPIÑAS_N009_ME_0000000040 != null)
               {
                   if (TOXICOLOGICO_3ENZOD1ACEPIÑAS_N009_ME_0000000040.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.TOXICOLOGICO_3ENZOD1ACEPIÑAS_N009_ME_0000000040 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.TOXICOLOGICO_3ENZOD1ACEPIÑAS_N009_ME_0000000040 = TOXICOLOGICO_3ENZOD1ACEPIÑAS_N009_ME_0000000040.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.TOXICOLOGICO_3ENZOD1ACEPIÑAS_N009_ME_0000000040 = "NULL";
               }


               var TOXICOLOGICO_CARBOXI_HEMOGLOBINA_N002_ME_0000000042 = lComponentes.Find(p => p.ComponentId == "N009-ME000000279");
               if (TOXICOLOGICO_CARBOXI_HEMOGLOBINA_N002_ME_0000000042 != null)
               {
                   if (TOXICOLOGICO_CARBOXI_HEMOGLOBINA_N002_ME_0000000042.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.TOXICOLOGICO_CARBOXI_HEMOGLOBINA_N002_ME_0000000042 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.TOXICOLOGICO_CARBOXI_HEMOGLOBINA_N002_ME_0000000042 = TOXICOLOGICO_CARBOXI_HEMOGLOBINA_N002_ME_0000000042.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.TOXICOLOGICO_CARBOXI_HEMOGLOBINA_N002_ME_0000000042 = "NULL";
               }





                ///////////////////////////////////////////////////////////////////////////////

               var TOXICOLOGICO_COLINESTERASA_N009_ME_0000000042 = lComponentes.Find(p => p.ComponentId == "N009-ME000000042");
               if (TOXICOLOGICO_COLINESTERASA_N009_ME_0000000042 != null)
               {
                   if (TOXICOLOGICO_COLINESTERASA_N009_ME_0000000042.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.TOXICOLOGICO_COLINESTERASA_N009_ME_0000000042 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.TOXICOLOGICO_COLINESTERASA_N009_ME_0000000042 = TOXICOLOGICO_COLINESTERASA_N009_ME_0000000042.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.TOXICOLOGICO_COLINESTERASA_N009_ME_0000000042 = "NULL";
               }



               var TOXICOLOGICO_DE_COCAINA_Y_MARIHUANA_N009_ME_0000000053 = lComponentes.Find(p => p.ComponentId == "N009-ME000000053");
               if (TOXICOLOGICO_DE_COCAINA_Y_MARIHUANA_N009_ME_0000000053 != null)
               {
                   if (TOXICOLOGICO_DE_COCAINA_Y_MARIHUANA_N009_ME_0000000053.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.TOXICOLOGICO_DE_COCAINA_Y_MARIHUANA_N009_ME_0000000053 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.TOXICOLOGICO_DE_COCAINA_Y_MARIHUANA_N009_ME_0000000053 = TOXICOLOGICO_DE_COCAINA_Y_MARIHUANA_N009_ME_0000000053.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.TOXICOLOGICO_DE_COCAINA_Y_MARIHUANA_N009_ME_0000000053 = "NULL";
               }



               var TRIGLICERIDOS_N009_ME_0000000017 = lComponentes.Find(p => p.ComponentId == "N009-ME000000017");
               if (TRIGLICERIDOS_N009_ME_0000000017 != null)
               {
                   if (TRIGLICERIDOS_N009_ME_0000000017.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.TRIGLICERIDOS_N009_ME_0000000017 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.TRIGLICERIDOS_N009_ME_0000000017 = TRIGLICERIDOS_N009_ME_0000000017.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.TRIGLICERIDOS_N009_ME_0000000017 = "NULL";
               }



               var TSH_N009_ME_0000000278 = lComponentes.Find(p => p.ComponentId == "N009-ME000000278");
               if (TSH_N009_ME_0000000278 != null)
               {
                   if (TSH_N009_ME_0000000278.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.TSH_N009_ME_0000000278 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.TSH_N009_ME_0000000278 = TSH_N009_ME_0000000278.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.TSH_N009_ME_0000000278 = "NULL";
               }



               var UREA_N009_ME_0000000073 = lComponentes.Find(p => p.ComponentId == "N009-ME000000073");
               if (UREA_N009_ME_0000000073 != null)
               {
                   if (UREA_N009_ME_0000000073.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.UREA_N009_ME_0000000073 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.UREA_N009_ME_0000000073 = UREA_N009_ME_0000000073.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.UREA_N009_ME_0000000073 = "NULL";
               }



               var UROCULTIVO_N009_ME_0000000263 = lComponentes.Find(p => p.ComponentId == "N009-ME000000263");
               if (UROCULTIVO_N009_ME_0000000263 != null)
               {
                   if (UROCULTIVO_N009_ME_0000000263.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.UROCULTIVO_N009_ME_0000000263 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.UROCULTIVO_N009_ME_0000000263 = UROCULTIVO_N009_ME_0000000263.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.UROCULTIVO_N009_ME_0000000263 = "NULL";
               }



               var VDRL_N009_M5_0000000003 = lComponentes.Find(p => p.ComponentId == "N009-ME00000003");
               if (VDRL_N009_M5_0000000003 != null)
               {
                   if (VDRL_N009_M5_0000000003.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.VDRL_N009_M5_0000000003 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.VDRL_N009_M5_0000000003 = VDRL_N009_M5_0000000003.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.VDRL_N009_M5_0000000003 = "NULL";
               }



               var VELOCIDAD_DE_SEDIMENT_N009_ME_0000000137 = lComponentes.Find(p => p.ComponentId == "N009-ME000000137");
               if (VELOCIDAD_DE_SEDIMENT_N009_ME_0000000137 != null)
               {
                   if (VELOCIDAD_DE_SEDIMENT_N009_ME_0000000137.ServiceComponentStatusId == (int)ServiceComponentStatus.Evaluado)
                   {
                       oExamenesRealizadosCompleto.VELOCIDAD_DE_SEDIMENT_N009_ME_0000000137 ="1";
                   }
                   else
                   {
                       oExamenesRealizadosCompleto.VELOCIDAD_DE_SEDIMENT_N009_ME_0000000137 = VELOCIDAD_DE_SEDIMENT_N009_ME_0000000137.ServiceComponentName;
                   }
               }
               else
               {
                   oExamenesRealizadosCompleto.VELOCIDAD_DE_SEDIMENT_N009_ME_0000000137 = "NULL";
               }



                ListaFinal.Add(oExamenesRealizadosCompleto);
            }
            

            grdDataService.DataSource = ListaFinal;


            if (grdDataService.Rows.Count > 0)
            {
                grdDataService.Rows[0].Selected = true;
                btnExportExcel.Enabled = true;
                btnEditarESO.Enabled = true;
                btnEditar.Enabled = true;
            }

            //if (!grdDataService.Rows.Any()) return;



        }

        private void ddlCustomerOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomerOrganization.SelectedValue == null)
                return;

            if (ddlCustomerOrganization.SelectedValue.ToString() == "-1")
            {
                ddlProtocolId.SelectedValue = "-1";
                ddlProtocolId.Enabled = false;
                return;
            }

            ddlProtocolId.Enabled = true;

            OperationResult objOperationResult = new OperationResult();

            var id3 = ddlCustomerOrganization.SelectedValue.ToString().Split('|');

            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, id3[0], id3[1], null), DropDownListAction.All);          
            
        }

        private void frmExamenesRealizadosCompleto_Load(object sender, EventArgs e)
        {
            OperationResult objOperationResult = new OperationResult();

            var clientOrganization = BLL.Utils.GetJoinOrganizationAndLocation(ref objOperationResult, Globals.ClientSession.i_CurrentExecutionNodeId);
            Utils.LoadDropDownList(ddlCustomerOrganization, "Value1", "Id", clientOrganization, DropDownListAction.Select);

            Utils.LoadDropDownList(ddlProtocolId, "Value1", "Id", BLL.Utils.GetProtocolsByOrganizationForCombo(ref objOperationResult, "-1", "-1", null), DropDownListAction.Select);
          
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {

            //"Matriz de datos <Empresa Cliente> de <fecha inicio> a <fecha fin>"
            string NombreArchivo = "";

            if (ddlCustomerOrganization.SelectedValue.ToString() != "-1")
            {
                NombreArchivo = "Consolidado de " + ddlCustomerOrganization.Text + " de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
                //NombreArchivo = "Matriz de datos " + ddlCustomerOrganization.Text;

            }
            else
            {
                NombreArchivo = "Consolidado de " + dtpDateTimeStar.Text + " a " + dptDateTimeEnd.Text;
                //NombreArchivo = "Matriz de datos";
            }

            NombreArchivo = NombreArchivo.Replace("/", "_");
            NombreArchivo = NombreArchivo.Replace(":", "_");

            saveFileDialog1.FileName = NombreArchivo;
            saveFileDialog1.Filter = "Files (*.xls;*.xlsx;*)|*.xls;*.xlsx;*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ultraGridExcelExporter1.Export(this.grdDataService, saveFileDialog1.FileName);
                MessageBox.Show("Se exportaron correctamente los datos.", " ¡ INFORMACIÓN !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditarESO_Click(object sender, EventArgs e)
        {
            Form frm;
            string TserviceId = grdDataService.Selected.Rows[0].Cells["ServiceId"].Value.ToString();
        
            this.Enabled = false;
            frm = new Operations.frmEso(TserviceId, null, "View", (int)MasterService.Eso);
            frm.ShowDialog();
            this.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var frm = new frmProtocolEdit(ddlProtocolId.SelectedValue.ToString(), "Edit");
            frm.ShowDialog();
        }

        private void grdDataService_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["ANEXO_16_N009_ME_000000052"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["EVALUACION_ERGONOMICA_N009_ME_000000128"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["EVALUACION_NEUROLOGICA_N009_ME_000000085"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["EXAMEN_ALTURA_ESTRUCTURAL_18_N009_ME_0000000015"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["EXAMEN_ALTURA_GEOGRAFICA_7_D_N002_ME_0000000045"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["EXAMEN_FISICO_312_N002_ME_0000000022"]);
            var x =e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["EXAMEN_OSTEOMUSCULAR_N002_ME_0000000046"]);
            
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["SINTOMATICO_RESPIRATORIO_N009_ME_0000000131"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["TAMIZAJE_DERMATOLOGICO_N009_ME_0000000044"]);

            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["TEST_DE_VERTIGO_N009_ME_0000000090"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["AUDIOMETRlA_N002_ME_0000000005"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["ELECTROCARDIOGRAMA_N002_ME_0000000025"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["PRUEBA_DE_ESFUERZO_N002_ME_0000000029"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["ESPIROMETRIA_N002_ME_0000000031"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["ODONTOGRAMA_N002_ME_0000000027"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["OFTALMOLOGIA__N002_ME_0000000028"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["RADIOGRAFIA_DE_COLUMNA_CERVICO_DORSO_LUMBAR_N009_ME_0000000302"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["RADIOGRAFIA_DE_TORAX_N002_ME_0000000032"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["RADIOGRAFIA_OIT_N009_ME_0000000062"]);

            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["RADIOGRAFIA_LUMBOSACRA_N009_ME_0000000130"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["ANTROPOMETRIA_N002_ME_0000000002"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["FUNCIONES_VITALES_N002_ME_0000000001"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["PSICOLOGIA_N002_M5_0000000033"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["ACIDO_URICO_N009_ME_0000000086"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["AGLUTINACIONES_EN_LAMINA_N009_ME_0000000025"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["AMILASA_N009_ME_000000165"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["ANT1ESTROPTOLINASA_O_N009_ME_0000000208"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["ANTIGENO_PROSTATICO_N009_ME_0000000009"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["BENZENO_EXPOSICION_A_GAS_NATURAL_N009_ME_0000000087"]);

            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["BK_D_RECTO_N009_ME_0000000081"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["COLESTE_ROL_TOTAL_N009_ME_0000000016"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["COPROCULT1VO_N009_ME_0000000264"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["CREATININA_N009_ME_0000000028"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["DENGUE_DUO_N009_ME_0000000304"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["DEPUR_DE_CREATININA_N009_ME_0000000164"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["DHL__1_N009_ME_0000000181"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["EXAMEN_COMPLETO_DE_ORINA_N009_ME_0000000046"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["EXAMEN_DE_ELISA_M_IV_N009_ME_0000000030"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["EXAMENKOH_N009_ME_0000000122"]);


            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["FECATEST_N009_ME_0000000097"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["FIBRINOGENO_N009_ME_0000000149"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["FTA_ABS_N009_ME_0000000233"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["GLUCOSA_N009_ME_0000000008"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["GRUPO_Y_FACTOR_SANGUINEO_N009_ME_0000000000"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["H3sAS_N009_ME_0000000121"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["HCV_N009_ME_0000000120"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["HEMATOCRITO_N009_ME_0000000001"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["HEMOGLOBINA_N009_ME_0000000006"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["HEMOGLOBINA_GLCOSILADA_N009_ME_0000000154"]);

            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["HEMOGRAMA_AUTOMATIZADO_N009_ME_0000000113"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["HEMOGRAMA_COMPLETO_N009_ME_0000000045"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["HEPATITIS_A_N009_ME_0000000004"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["HEPATITIS_B_N009_ME_0000000301"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["HEPATITIS_C_N009_ME_0000000005"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["HISOPADO_FARINGEO_N009_ME_0000000095"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["HISOPADO_NASOFARINGEO_N009_ME_0000000118"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["INFORME_DE_LABORATORIO_N009_ME_0000000002"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["INSULINA_BASAL_N009_ME_0000000125"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["LIPASA_N009_ME_0000000172"]);


            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["MCROALBUMINURIA_N009_ME_0000000117"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["PARASITOLOGICO_SERIADO_N009_ME_0000000049"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["PARASITOLOGICO_SIMPLE_N009_ME_0000000010"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["PCR_N009_ME_0000000205"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["PERFIL_HEPATICO_N009_ME_0000000095"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["PERFIL_LIPIDICO_N009_ME_0000000114"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["PERFIL_TIROIDEO_N009_ME_0000000305"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["PLOMO_EN_SANGRE_N009_ME_0000000060"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["PROTEINAS_T_Y_FRACC_N009_ME_0000000170"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["PROTEINURIA_DE_24_H_N009_ME_0000000165"]);

            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["RCTO_DE_PLAQUETAS_N009_ME_0000000146"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["REACCION_INFLAMATORIA_N009_ME_0000000119"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["SUB_UNIDAD_BETA_CUALITATIVO_N009_ME_0000000027"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["SUB_UNIDAD_BETA_CUANTITATIVO_N002_ME_0000000015"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["TGO_N009_ME_0000000054"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["TGP_N009_ME_0000000055"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["T_DE_PROTOMBINA_N009_ME_0000000147"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["T_PARCIAL_DE_TROMBOPL_N009_ME_0000000148"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["T3_UP_TAKE_N009_ME_0000000280"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["T4_LIBRE_DOSAJE_N009_ME_0000000279"]);

            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["TECNICA_DE_GRAHAM_N009_ME_0000000298"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["THENEVON_N009_ME_0000000299"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["TIEMPO_DE_COAGULACION_N009_ME_0000000124"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["TIEMPO_DE_SANGRIA_N009_ME_0000000123"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["TOLERANCIA_A_LA_GLUCOSA_N009_ME_0000000151"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["TOXICOL0GICO_N009_ME_0000000303"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["TOXICOLOGICO_ALCOHOLEME_N009_ME_0000000041"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["TOXICOLOGICO_ANFETAMINAS_N009_ME_0000000043"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["TOXICOLOGICO_3ENZOD1ACEPIÑAS_N009_ME_0000000040"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["TOXICOLOGICO_CARBOXI_HEMOGLOBINA_N002_ME_0000000042"]);

            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["TOXICOLOGICO_COLINESTERASA_N009_ME_0000000042"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["TOXICOLOGICO_DE_COCAINA_Y_MARIHUANA_N009_ME_0000000053"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["TRIGLICERIDOS_N009_ME_0000000017"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["TSH_N009_ME_0000000278"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["UREA_N009_ME_0000000073"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["UROCULTIVO_N009_ME_0000000263"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["VDRL_N009_M5_0000000003"]);
            e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["VELOCIDAD_DE_SEDIMENT_N009_ME_0000000137"]);

        }
    }
}
