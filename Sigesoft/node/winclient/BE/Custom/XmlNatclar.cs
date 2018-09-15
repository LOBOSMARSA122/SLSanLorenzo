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

        public string IDEstructura { get; set; }
        public string IDCentro { get; set; }
        public string IDExamen { get; set; }
        public string IDActuacion { get; set; }
        public int? TipoExamen { get; set; }
        public int? IDEstado { get; set; }
        public string FechaUltimaRegla { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public List<Perfiles> Examenes { get; set; }

    }

    public class Perfiles
    {
       
        public int? CategoriaId { get; set; }
        public string Perfil1 { get; set; }
        public string Prueba { get; set; }
        public string ValorPrueba { get; set; }
        public string Observaciones { get; set; }
        public string ComponentId { get; set; }
        public string ServiceComponentId { get; set; }
        public List<Pruebas> Pruebas { get; set; }
    }

    public class Pruebas
    {
        public string Prueba { get; set; }
    }
}
