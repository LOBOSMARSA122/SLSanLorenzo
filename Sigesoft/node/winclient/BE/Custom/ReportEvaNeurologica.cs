using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportEvaNeurologica
    {
        public string Paciente { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public string Genero { get; set; }
        public DateTime? FechaServicio { get;set;}
        public string DNI { get; set; }
        public string v_ServiceId { get; set; }
        public string Empresa { get; set; }

        public string EVA_NEUROLOGICA_TEST_ROMBERG_ID { get; set; }
        public string EVA_NEUROLOGICA_PRUEBA_MARCHA_ID { get; set; }
        public string EVA_NEUROLOGICA_PRUEBA_INDICE_NARIZ_ID { get; set; }
        public string EVA_NEUROLOGICA_MIEMBRO_SUP_ID { get; set; }
        public string EVA_NEUROLOGICA_MIEMBRO_INF_ID { get; set; }
        public string EVA_NEUROLOGICA_POSITIVO_ID { get; set; }
        public string EVA_NEUROLOGICA_FLEXION_ID { get; set; }
        public string EVA_NEUROLOGICA_CONCLUSION_ID { get; set; }

        public byte[] FirmaMedico { get; set; }
        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }

        public string NombreUsuarioGraba { get; set; }



    }
}
