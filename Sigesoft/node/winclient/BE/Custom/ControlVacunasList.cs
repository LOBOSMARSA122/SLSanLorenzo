using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ControlVacunasList
    {
        public string v_ServiceId { get; set; }
        public string v_Pacient { get; set; }
        public DateTime? d_ServiceDate { get; set; }
        public string v_OrganizationName { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
        public List<VacunasList> Vacunas { get; set; }
    }
}
