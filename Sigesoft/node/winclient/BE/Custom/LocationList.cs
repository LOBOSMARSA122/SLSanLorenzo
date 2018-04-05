using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
   public class LocationList
    {
       public string v_LocationId { get; set; }
       public string v_OrganizationId { get; set; }
       public string v_OrganizationName { get; set; }
       public string v_Name { get; set; }
       //public string i_IsDeleted { get; set; }
       public string v_CreationUser { get; set; }
       public string v_UpdateUser { get; set; }
       public DateTime? d_CreationDate { get; set; }
       public DateTime? d_UpdateDate { get; set; }
       public int? i_InsertUserId { get; set; }
       public int? i_IsDeleted { get; set; }
       public DateTime? d_InsertDate { get; set; }
       public int? i_UpdateUserId { get; set; }
    }
}
