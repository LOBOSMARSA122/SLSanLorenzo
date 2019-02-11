using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public class EmpresaMigracion
    {

        public int? i_OrganizationTypeId { get; set; }
        public string v_IdentificationNumber { get; set; }
        public int? i_SectorTypeId { get; set; }
        public string v_Name { get; set; }
        public string v_Address { get; set; }
        public string v_PhoneNumber { get; set; }
        public string v_Mail { get; set; }
        public string v_ContacName { get; set; }
        public string v_Observation { get; set; }
        public List<SedeMigracion> Sedes{ get; set; }
    }

   public class SedeMigracion
   {
       public string Sede { get; set; }
       public List<GesoMigracion> Gesos{ get; set; }
   }

   public class GesoMigracion
   {
       public string Geso{ get; set; }
   }
}
