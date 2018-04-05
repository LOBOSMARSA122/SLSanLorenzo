using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
   public class ComponentFieldValuesRestrictionList
    {
        public string v_ComponentFieldValuesRestrictionId { get; set; }
        public string v_ComponentFieldValuesId { get; set; }
        //public int i_RestrictionId { get; set; }
        public string v_MasterRecommendationRestricctionId { get; set; }
        public string v_RestrictionName { get; set; }

        public int? i_RecordStatus { get; set; }
        public int? i_RecordType { get; set; }

        public string i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

    }
}
