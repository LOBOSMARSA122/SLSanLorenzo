using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportCuestionarioNordico
    {
        public string Nombre_Trabajador { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public int Genero { get; set; }


        public string C_N_CABECERA_TIPO_TRABAJO_REALIZA_ID { get; set; }
        public string C_N_CABECERA_TIEMPO_LABOR_ID { get; set; }
        public string C_N_CABECERA_HORAS_TRABAJO_SEMANAL_ID { get; set; }
        public string C_N_CABECERA_DIESTRO_ID { get; set; }
        public string C_N_CABECERA_ZURDO_ID { get; set; }

        public string C_N_LOCOMOCION_TODOS_CUELLOS_ID { get; set; }
        public string C_N_LOCOMOCION_TODOS_HOMBROS_ID { get; set; }
        public string C_N_LOCOMOCION_TODOS_CODOS_ID { get; set; }
        public string C_N_LOCOMOCION_TODOS_MUÑECA_ID { get; set; }
        public string C_N_LOCOMOCION_TODOS_ESPALDA_ALTA_ID { get; set; }
        public string C_N_LOCOMOCION_TODOS_ESPALDA_BAJA_ID { get; set; }
        public string C_N_LOCOMOCION_TODOS_CADERAS_MUSLOS_ID { get; set; }
        public string C_N_LOCOMOCION_TODOS_RODILLAS_ID { get; set; }
        public string C_N_LOCOMOCION_TODOS_TOBILLOS_PIES_ID { get; set; }

        public string C_N_LOCOMOCION_12_MESES_CUELLO_ID { get; set; }
        public string C_N_LOCOMOCION_12_MESES_HOMBROS_ID { get; set; }
        public string C_N_LOCOMOCION_12_MESES_CODOS_ID { get; set; }
        public string C_N_LOCOMOCION_12_MESES_MUÑECA_ID { get; set; }
        public string C_N_LOCOMOCION_12_MESES_ESPALDA_ALTA_ID { get; set; }
        public string C_N_LOCOMOCION_12_MESES_ESPALDA_BAJA_ID { get; set; }
        public string C_N_LOCOMOCION_12_MESES_CADERAS_MUSLOS_ID { get; set; }
        public string C_N_LOCOMOCION_12_MESES_RODILLAS_ID { get; set; }
        public string C_N_LOCOMOCION_12_MESES_TOBILLOS_ID { get; set; }

        public string C_N_LOCOMOCION_7_DIAS_CUELLO_ID { get; set; }
        public string C_N_LOCOMOCION_7_DIAS_HOMBROS_ID { get; set; }
        public string C_N_LOCOMOCION_7_DIAS_CODOS_ID { get; set; }
        public string C_N_LOCOMOCION_7_DIAS_MUÑECA_ID { get; set; }
        public string C_N_LOCOMOCION_7_DIAS_ESPALDA_ALTA_ID { get; set; }
        public string C_N_LOCOMOCION_7_DIAS_ESPALDA_BAJA_ID { get; set; }
        public string C_N_LOCOMOCION_7_DIAS_CADERA_ID { get; set; }
        public string C_N_LOCOMOCION_7_DIAS_RODILLAS_ID { get; set; }
        public string C_N_LOCOMOCION_7_DIAS_TOBILLOS_ID { get; set; }

        public string C_N_ESPALDA_BAJA_PROBLEMAS_ESPALDA_BAJA_ID { get; set; }
        public string C_N_ESPALDA_BAJA_HOSPITALIZADO_PROBLEMA_ESPALDA_BAJA_ID { get; set; }
        public string C_N_ESPALDA_BAJA_CAMBIOS_TRABAJO_ACTIVIDAD_PROBLEMA_ESPALDA_BAJA_ID { get; set; }
        public string C_N_ESPALDA_BAJA_CURACION_TOTAL_ESPALDA_BAJA_ID { get; set; }
        public string C_N_ESPALDA_BAJA_ACTIVIDAD_TRABAJO_1_ID { get; set; }
        public string C_N_ESPALDA_BAJA_ACTIVIDAD_RECREATIVA_1_ID { get; set; }


        public string C_N_DURACION_PRBLEMAS_IMPEDIR_RUTINA_1_ID { get; set; }
        public string C_N_VISTO_PROFESIONAL_ESPALDA_BAJA_ID { get; set; }
        public string C_N_PROBLEMAS_ESPALDA_BAJA_7_DIAS_ID { get; set; }
        
        public string C_N_PROBLEMAS_HOMBROS_PROBLEMAS_HOMBROS_ID { get; set; }
        public string C_N_PROBLEMAS_HOMBROS_LESION_HOMBROS_ACCIDENTES_ID { get; set; }
        public string C_N_PROBLEMAS_HOMBROS_CAMBIO_TRABAJO_ACTIVIDAD_HOMBROS_ID { get; set; }
        public string C_N_PROBLEMAS_HOMBROS_PROBLEMAS_HOMBROS_ULTIMOS_12_MESES_ID { get; set; }
        public string C_N_PROBLEMAS_HOMBROS_TIEMPO_TOTAL_PROBLEMAS_HOMBROS_ID { get; set; }

        public string C_N_PROBLEMAS_HOMBROS_ACTIVIDAD_TRABAJO_2_ID { get; set; }
        public string C_N_PROBLEMAS_HOMBROS_ACTIVIDAD_RECREATIVA_2_ID { get; set; }
        public string C_N_PROBLEMAS_HOMBROS_DURACION_PRBLEMAS_IMPEDIR_RUTINA_2_ID { get; set; }
        public string C_N_PROBLEMAS_HOMBROS_VISTO_PROFESIONAL_HOMBROS_ID { get; set; }
        public string C_N_PROBLEMAS_HOMBROS_PROBLEMAS_HOMBROS_DURANTE_7_DIAS_ID { get; set; }
        
        public string C_N_PROBLEMA_CUELLO_PROBLEMA_CUELLO_ID { get; set; }
        public string C_N_PROBLEMA_CUELLO_LESIONADO_CUELLO_ACCIDENTE_ID { get; set; }
        public string C_N_PROBLEMA_CUELLO_CAMBIO_TRABAJO_PROBLEMA_CUELLO_ID { get; set; }
        public string C_N_PROBLEMA_CUELLO_DURACION_TOTAL_TIEMPO_PROBLEMA_CUELLO_ID { get; set; }
        public string C_N_PROBLEMA_CUELLO_DURACION_PRBLEMAS_IMPEDIR_RUTINA_3_ID { get; set; }
        public string C_N_PROBLEMA_CUELLO_VISTO_PROFESIONAL_CUELLO_ID { get; set; }
        public string C_N_PROBLEMA_CUELLO_PROBLEMAS_CUELLO_DURANTE_7_DIAS_ID { get; set; }
        public string C_N_PROBLEMA_CUELLO_ACTIVIDAD_TRABAJO_3_ID { get; set; }
        public string C_N_PROBLEMA_CUELLO_ACTIVIDAD_RECREATIVA_3_ID { get; set; }

        public string NombreUsuarioGraba { get; set; }





        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }
        public string Conclusiones { get; set; }


        public byte[] FirmaTrabajador { get; set; }
        public byte[] HuellaTrabajador { get; set; }
        public byte[] LOGOCLIENTE { get; set; }

        public byte[] FirmaMedicina { get; set; }




    }
}
