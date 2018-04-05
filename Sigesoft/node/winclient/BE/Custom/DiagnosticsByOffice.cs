using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class DiagnosticsByOffice
    {
        public string v_ComponentId { get; set; }
        public string Trabajador { get; set; }
        public string v_ComponentName { get; set; }
        public string v_DiseasesId { get; set; }
        public string v_DiseasesName { get; set; }
        public byte[] b_Logo { get; set; }
        public int NroHallazgos { get; set; }
        public string v_GroupOccupationId { get; set; }
        public string v_CustomerOrganizationName { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
        public string IdProtocolId { get; set; }
    }
}
