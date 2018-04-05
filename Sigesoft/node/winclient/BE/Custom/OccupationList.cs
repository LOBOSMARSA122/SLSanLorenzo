using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
   public class OccupationList
    {

        public string v_GesId { get; set; }
        public string v_GesName { get; set; }
        public string v_GroupOccupationId { get; set; }
        public string v_GroupOccupationName { get; set; }
        public string v_OccupationId { get; set; }
        public string v_Name { get; set; }

        public string v_AreaId { get; set; }

        public string i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
