using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ComponentFieldValues
    {
        public string v_ComponentFieldValuesId { get; set; }
        public string v_ComponentFieldsId { get; set; }
        public string v_AnalyzingValue1 { get; set; }
        public string v_AnalyzingValue2 { get; set; }
        public int i_OperatorId { get; set; }
        public string v_Recommendation { get; set; }
        public int i_Cie10Id { get; set; }
        public string v_Restriction { get; set; } 
        public string v_LegalStandard { get; set; }

        public int? i_IsAnormal { get; set; }
        public int? i_ValidationMonths { get; set; }
        public string v_ComponentId { get; set; }

        //public int i_IsDeleted { get; set; }
        //public string v_CreationUser { get; set; }
        //public string v_UpdateUser { get; set; }
        //public DateTime? d_CreationDate { get; set; }
        //public DateTime? d_UpdateDate { get; set; }

        public string v_DiseasesId { get; set; }
        public string v_DiseasesName { get; set; }  // diagnostico
        public string v_CIE10 { get; set; }
        public List<RecomendationList> Recomendations { get; set; }
        public List<RestrictionList> Restrictions { get; set; }
        public int? i_GenderId { get; set; }
        
    }
}
