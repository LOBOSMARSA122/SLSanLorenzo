using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReporteAsistencia
    {
        public string FechaHora { get; set; }
        public string HoraIngreso { get; set; }
        public string HoraSalida { get; set; }
        public string Paciente { get; set; }
        public string DNI { get; set; }
        public string Edad { get; set; }
        public string Empresa { get; set; }
        public string TipoEso { get; set; }
        public string GrupoRiesgo { get; set; }
        public string Puesto { get; set; }
        public string EstadoCita { get; set; }
        public string EstadoAptitud { get; set; }

        public byte[] LogoPropietaria { get; set; }
        public string NombreEmpresaPropietaria { get; set; }
        public string NombreEmpresaCliente { get; set; }
    }
}
