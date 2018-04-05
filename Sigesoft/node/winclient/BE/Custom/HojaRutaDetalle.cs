using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class HojaRutaDetalle
    {
        public string Item { get; set; }
        public string Examen { get; set; }
        public string Consultorio { get; set; }
        public string Estado { get; set; }

        public int? i_CategoryId { get; set; }
    }
}
