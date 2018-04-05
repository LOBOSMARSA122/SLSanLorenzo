using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
   public class AreaList
    {
        public string v_AreaId { get; set; }
        public string v_Name { get; set; }
        public string v_LocationId{ get; set; }
        public string v_LocationName { get; set; }
        
        public string i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
       //hola
    }
}
