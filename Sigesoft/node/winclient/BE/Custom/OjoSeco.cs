using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class OjoSeco
    {
        public string Trabajador { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public DateTime? FechaEvaluacion { get; set; }
        public string Puesto { get; set; }
        public string ServicioId { get; set; }
        public byte[] FirmaUsuarioGraba { get; set; }
        public string NombreUsuarioGraba { get; set; }
        public byte[] HuellaTrabajador { get; set; }
        public byte[] FirmaTrabajador { get; set; }
        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }

        public string OJO_SECO_ENROJECIMIENTO { get; set; }
        public string OJO_SECO_ENROJECIMIENTO_PTJ_1 { get; set; }
        public string OJO_SECO_BORDE { get; set; }
        public string OJO_SECO_BORDE_PTJ_2 { get; set; }
        public string OJO_SECO_ESCAMAS { get; set; }
        public string OJO_SECO_ESCAMAS_PTJ_3 { get; set; }
        public string OJO_SECO_OJOS { get; set; }
        public string OJO_SECO_OJOS_PTJ_4 { get; set; }
        public string OJO_SECO_SECRE { get; set; }
        public string OJO_SECO_SECRE_PTJ_5 { get; set; }
        public string OJO_SECO_SEQUEDAD { get; set; }
        public string OJO_SECO_SEQUEDAD_PTJ_6 { get; set; }
        public string OJO_SECO_ARENILLA { get; set; }
        public string OJO_SECO_ARENILLA_PTJ_7 { get; set; }
        public string OJO_SECO_EXTRANO { get; set; }
        public string OJO_SECO_EXTRANO_PTJ_8 { get; set; }
        public string OJO_SECO_ARDOR { get; set; }
        public string OJO_SECO_ARDOR_PTJ_9 { get; set; }
        public string OJO_SECO_PICOR { get; set; }
        public string OJO_SECO_PICOR_PTJ_10 { get; set; }
        public string OJO_SECO_MALESTAR { get; set; }
        public string OJO_SECO_MALESTAR_PTJ_11 { get; set; }
        public string OJO_SECO_DOLOR { get; set; }
        public string OJO_SECO_DOLOR_PTJ_12 { get; set; }
        public string OJO_SECO_LAGRIMEO { get; set; }
        public string OJO_SECO_LAGRIMEO_PTJ_13 { get; set; }
        public string OJO_SECO_LLOROSOS { get; set; }
        public string OJO_SECO_LLOROSOS_PTJ_14 { get; set; }
        public string OJO_SECO_SENSIBILIDAD { get; set; }
        public string OJO_SECO_SENSIBILIDAD_PTJ_15 { get; set; }
        
        public string OJO_SECO_VISION { get; set; }
        public string OJO_SECO_VISION_PTJ_16 { get; set; }
        public string OJO_SECO_CANSANCION { get; set; }
        public string OJO_SECO_CANSANCION_PTJ_17 { get; set; }
        public string OJO_SECO_PESADEZ { get; set; }
        public string OJO_SECO_PESADEZ_PTJ_18 { get; set; }
        public string OJO_SECO_TOTAL { get; set; }
        public string TipoEso { get; set; }
        public string Dni { get; set; }
        public string EmpresaCliente { get; set; }
        public string Dx { get; set; }
    }
}
