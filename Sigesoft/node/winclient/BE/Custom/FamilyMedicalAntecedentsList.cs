using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
  public class FamilyMedicalAntecedentsList
    {
        public string v_FamilyMedicalAntecedentsId { get; set; }

        public String v_PersonId { get; set; }

        public String v_DiseasesId { get; set; }

        public string v_TypeFamilyName { get; set; }

        public string v_DiseaseName { get; set; }

        public int i_TypeFamilyId { get; set; }       

        public string v_Comment { get; set; }       

        public int? i_RecordStatus { get; set; }

        public int? i_RecordType { get; set; }

        public string i_IsDeleted { get; set; }

        public string v_CreationUser { get; set; }

        public string v_UpdateUser { get; set; }

        public DateTime? d_CreationDate { get; set; }

        public DateTime? d_UpdateDate { get; set; }

        public int i_Item { get; set; }

        public string v_FullAntecedentName { get; set; }

        public string DxAndComment { get; set; }

        public int? i_ParameterId { get; set; }

        public int? i_ParentParameterId { get; set; }

        public string v_CIE10Id { get; set; }
        public string v_Name { get; set; }     
    }
}
