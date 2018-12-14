
using System.Collections.Generic;

namespace Sigesoft.Node.WinClient.BE
{
    public class ProtocolProcess
    {
        public string ProtocolId { get; set; }
        public string Name { get; set; }
        public string EmployerOrganizationId { get; set; }
        public string EmployerLocationId { get; set; }
        public int? EsoTypeId { get; set; }
        public string CustomerOrganizationId { get; set; }
        public string CustomerLocationId { get; set; }
        public string WorkingOrganizationId { get; set; }
        public string WorkingLocationId { get; set; }
        public int? MasterServiceTypeId { get; set; }
        public int? MasterServiceId { get; set; }
        public List<Component> Components{ get; set; }
    }

    public class Component
    {
        public string ProtocolComponentId { get; set; }
        public string ProtocolId { get; set; }
        public string ComponentId { get; set; }
    }
}
