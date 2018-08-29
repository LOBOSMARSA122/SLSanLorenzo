using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class XmlNatclar
    {
        public string Hc { get; set; }
        public int? TipoDocumento { get; set; }
        public string Dni { get; set; }
        public int? Sexo { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Nombre { get; set; }
        public int? EstadoCivil { get; set; }
        public DateTime? FechaNacimientoSigesoft { get; set; }
        public string FechaNacimiento { get; set; }
        public string ProvinciaNacimiento { get; set; }
        public string DistritoNacimiento { get; set; }
        public string DepartamentoNacimiento { get; set; }
        public string Email { get; set; }
        public string ResidenciaActual { get; set; }
        public string Direccion { get; set; }
    }
}
