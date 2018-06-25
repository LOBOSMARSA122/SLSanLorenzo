using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class DatosPersonales
    {
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public string Nombres { get; set; }
        public int GeneroId { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string LugarNacimiento { get; set; }
        public string Procedencia { get; set; }
        public int GradoInstruccionId { get; set; }
        public int EstadoCivilId { get; set; }
        public string Ocupacion { get; set; }
        public string Domicilio { get; set; }
        public string CentroEducativo { get; set; }
        public int Hijosvivos { get; set; }


    }
}
