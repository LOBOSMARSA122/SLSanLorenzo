using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class HojaRutaCabecera
    {
        public string Empresa { get; set; }
        public string Trabajador { get; set; }
        public string Puesto { get; set; }
        public string Dni { get; set; }
        public string TipoEmo { get; set; }
        public DateTime? Fecha { get; set; }
        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
    }
}
