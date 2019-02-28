using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class HospitalizacionServiceList
    {
        public string v_HospitalizacionServiceId { get; set; }
        public string v_HopitalizacionId { get; set; }
        public string v_NroHospitalizacion { get; set; }
        public string v_ServiceId { get; set; }
        public DateTime? d_ServiceDate{ get; set; }
        public string v_ProtocolName { get; set; }
        public string v_ProtocolId { get; set; }
        public string v_DocNumber { get; set; }
        public List<TicketList> Tickets { get; set; }
        public DateTime? d_FechaAlta { get; set; }
        public List<ComponentesHospitalizacion> Componentes { get; set; }
    }
}
