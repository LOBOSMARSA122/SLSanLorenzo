using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
  public  class ComponentFieldValuesRecommendationList
    {
        public string v_ComponentFieldValuesRecommendationId { get; set; }
        public string v_ComponentFieldValuesId { get; set; }
        //public int i_RecomendationId { get; set; }
        public string v_MasterRecommendationRestricctionId { get; set; }
        public string v_RecomendationName { get; set; }


        public int? i_RecordStatus { get; set; }
        public int? i_RecordType { get; set; }

        public string i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
