using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
   public class SupplierList
    {
       public string v_SupplierId { get; set; }
       public Int32 i_SectorTypeId { get; set; }
       public string v_SectorTypeIdName { get; set; }
       public string v_IdentificationNumber { get; set; }
       public string v_Name { get; set; }
       public string v_Address { get; set; }
       public string v_PhoneNumber { get; set; }
       public string v_Mail { get; set; }
       public string v_CreationUser { get; set; }
       public string v_UpdateUser { get; set; }
       public DateTime? d_CreationDate { get; set; }
       public DateTime? d_UpdateDate { get; set; }
       public int? i_IsDeleted { get; set; }
    }
}
