using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class RecomendationList
    {
        public string v_RecommendationId { get; set; }
        public string v_ServiceId { get; set; }
        public string v_DiagnosticRepositoryId { get; set; }
        public string v_ComponentId { get; set; }
        public string v_MasterRecommendationId { get; set; }

        public string v_MasterRecommendationRestrictionId { get; set; }

        public int? i_RecordStatus { get; set; }
        public int? i_RecordType { get; set; }

        public int? i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        //

        public string v_ComponentFieldValuesRecommendationId { get; set; }
        public string v_ComponentFieldValuesId { get; set; }

        //

        public string v_RecommendationName { get; set; }

        public int i_Item { get; set; }

        public string v_DiseasesId { get; set; } 
    }
}
