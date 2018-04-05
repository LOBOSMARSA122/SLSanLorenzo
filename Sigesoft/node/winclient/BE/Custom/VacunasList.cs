using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class VacunasList
    {
        public string v_ServiceId { get; set; }
        public string v_ServiceComponentId { get; set; }
        public string VacunaId {get; set;}
        public string Vacuna { get; set; }
        public List<VacunaDetalleList> DetalleVacuna { get; set; }
    }
}
