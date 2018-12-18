using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public class SpecialistConfiguration
    {
        public string v_MedicoId { get; set; }

        public string i_SystemUserId { get; set; }
        public string SystemUser { get; set; }

        public string i_CategoryId { get; set; }
        public string Category { get; set; }

        public string v_EmployerOrganizationId { get; set; }
        public string EmployerOrganization { get; set; }

        public string v_CustomerOrganizationId { get; set; }
        public string CustomerOrganization { get; set; }

        public string v_WorkingOrganizationId { get; set; }
        public string WorkingOrganization { get; set; }

        public float? Price { get; set; }
    }
}
