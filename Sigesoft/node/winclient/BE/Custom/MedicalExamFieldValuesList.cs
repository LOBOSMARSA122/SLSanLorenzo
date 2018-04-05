using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
  public class MedicalExamFieldValuesList
    {
        public string v_MedicalExamFieldValuesId { get; set; }
        public string v_MedicalExamFieldsId { get; set; }
        public string v_AnalyzingValue1 { get; set; }
        public string v_AnalyzingValue2 { get; set; }
        public int i_OperatorId { get; set; }
        public string v_Recommendation { get; set; }
        public int i_Cie10Id { get; set; }
        public string v_Restriction { get; set; }
        public string v_LegalStandard { get; set; }

        public string v_OperatorName { get; set; }
        public int i_IsAnormal { get; set; }
        public string v_IsAnormal { get; set; }
        public string v_Cie10Name { get; set; }

      
        public int i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
