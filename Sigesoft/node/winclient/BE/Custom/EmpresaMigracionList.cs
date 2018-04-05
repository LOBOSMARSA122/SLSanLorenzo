using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class EmpresaMigracionList
    {
        public string v_OrganizationId { get; set; }
        public string v_Name { get; set; }
        public string v_IdentificationNumber { get; set; }

        public List<LocationMigracionList> Sedes { get; set; }
        public List<groupoccupationMigracionList> GESOS { get; set; }
    }
}
