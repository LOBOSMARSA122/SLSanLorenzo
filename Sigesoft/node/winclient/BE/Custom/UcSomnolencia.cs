using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class UcSomnolencia
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

        public string SOMNOLENCIA_1_SENTADO_ID { get; set; }
        public string SOMNOLENCIA_2_MIRANDO_TV_ID { get; set; }
        public string SOMNOLENCIA_3_SENTADO_INACTIVO_ID { get; set; }
        public string SOMNOLENCIA_4_PASAJERO_ID { get; set; }
        public string SOMNOLENCIA_5_ACOSTADO_DESC_ID { get; set; }
        public string SOMNOLENCIA_6_ACOSTADO_CONVER_ID { get; set; }
        public string SOMNOLENCIA_7_SENTADO_TRANQUILO_ID { get; set; }
        public string SOMNOLENCIA_8_CARRO_TRACON_ID { get; set; }
        public string SOMNOLENCIA_1_RESULTADO_ID { get; set; }
        public string SOMNOLENCIA_2_RESULTADO_ID { get; set; }
        public string SOMNOLENCIA_3_RESULTADO_ID { get; set; }
        public string SOMNOLENCIA_4_RESULTADO_ID { get; set; }
        public string SOMNOLENCIA_5_RESULTADO_ID { get; set; }
        public string SOMNOLENCIA_6_RESULTADO_ID { get; set; }
        public string SOMNOLENCIA_7_RESULTADO_ID { get; set; }
        public string SOMNOLENCIA_8_RESULTADO_ID { get; set; }
        public string SOMNOLENCIA_TOTAL_ID { get; set; }
        public string Dx { get; set; }
        public string Recomendaciones { get; set; }

        public string TipoEso { get; set; }
        public string Dni { get; set; }
        public string EmpresaCliente { get; set; }

    }
}
