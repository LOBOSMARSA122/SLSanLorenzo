using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class DataEso
    {
        public string v_ServiceId { get; set; }
        public string v_ServiceComponentId { get; set; }
        public string v_PersonId { get; set; }
        public string categoria { get; set; }
        public string Estado { get; set; }
        public string NroIntentos { get; set; }
        public string Detalle { get; set; }

        public List<ServiceComponentFieldsList> datos { get; set; }
    }
}
