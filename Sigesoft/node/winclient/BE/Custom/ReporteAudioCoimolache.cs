using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReporteAudioCoimolache
    {
        public string PREGUNTA_01 { get; set; }
        public string PREGUNTA_02 { get; set; }
        public string PREGUNTA_03 { get; set; }
        public string PREGUNTA_04 { get; set; }
        public string PREGUNTA_05 { get; set; }
        public string PREGUNTA_06 { get; set; }
        public string PREGUNTA_07 { get; set; }
        public string PREGUNTA_08 { get; set; }
        public string PREGUNTA_09 { get; set; }
        public string PREGUNTA_10 { get; set; }

        public string CONDICION { get; set; }
        public DateTime? FECHA { get; set; }
        public string HORA { get; set; }
        public string EMP_CLIENTE { get; set; }
        public string EMP_CONTRATISTA { get; set; }
        public string NOMBRE_PACIENTE { get; set; }
        public int EDAD { get; set; }
        public string GENERO { get; set; }
        public string PUESTO { get; set; }

        public byte[] FIRMA_TECNICO { get; set; }
        public byte[] FIRMA_MEDICO { get; set; }
        public byte[] FIRMA_PACIENTE { get; set; }
        public byte[] HUELLA_PACIENTE { get; set; }

        public DateTime? FECHA_NACIMIENTO { get; set; }
    }
}
