using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ProtocolsAndWorkers
    {
        public List<protocolDto> Protocols{ get; set; }
        public List<personDto> Workers { get; set; }
    }

    //public class Protocol
    //{
    //    public string ProtocolId { get; set; }
    //    public string ProtocolName { get; set; }
    //}

    //public class Worker
    //{
    //    public int DocumentTypeId { get; set; }
    //    public string NroDocument { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string SecondLastName { get; set; }
    //    public DateTime Birthdate { get; set; }
    //    public int SexTypeId { get; set; }
    //    public string SexType { get; set; }
    //    public string GesoId { get; set; }
    //    public string Geso { get; set; }
    //    public string CurrentOccupation { get; set; }
    //}
}
