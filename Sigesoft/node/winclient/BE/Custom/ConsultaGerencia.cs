
using System;

namespace Sigesoft.Node.WinClient.BE
{
    public class ConsultaGerencia
    {
        public string ServiceId { get; set; }
        public DateTime? ServiceDate { get; set; }
        public string ProtocolId { get; set; }
        public string ProtocolName { get; set; }
        public string WorkerId { get; set; }
        public string WorkerName { get; set; }
        public int? EsoTypeId { get; set; }
        public string EsoTypeName { get; set; }
        public string EmployerOrganizationId { get; set; }
        public string WorkingOrganizationId { get; set; }
        public string CustomerOrganizationId { get; set; }
        public int? MasterServiceTypeId { get; set; }
        public string MasterServiceTypeName { get; set; }
        public string ComponentId { get; set; }
        public float? PriceComponent { get; set; }
        public decimal? SaldoPaciente { get; set; }
        public decimal? SaldoAseguradora { get; set; }
        public int? ConCargoA { get; set; }
    }

}
