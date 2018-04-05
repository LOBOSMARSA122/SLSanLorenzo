using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public class GesList
    {
       public string v_AreaId { get; set; }
       public string v_AreaName { get; set; }
       public string v_GesId { get; set; }      
        public string v_Name { get; set; }

        public string i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    
   }
}
