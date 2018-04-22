using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class PersonMedicalHistoryList
    {
        public String v_PersonMedicalHistoryId { get; set; }

        public String v_PersonId { get; set; }

        public String v_DiseasesId { get; set; }

        public string v_GroupName { get; set; }

        public string v_DiseasesName { get; set; }

        public Nullable<Int32> i_TypeDiagnosticId { get; set; }

        public Nullable<DateTime> d_StartDate { get; set; }

        public Nullable<DateTime> d_EndDate { get; set; }

        public String v_DiagnosticDetail { get; set; }

        public Nullable<DateTime> d_Date { get; set; }

        public String v_TreatmentSite { get; set; }

        public int? i_RecordStatus { get; set; }

        public int? i_RecordType { get; set; }

        public string v_TypeDiagnosticName { get; set; }

        public string v_AntecedentTypeName { get; set; }

        public string v_TypeFamilyName { get; set; }

        public string v_DateOrGroup { get; set; }

        public int i_TypeFamilyId { get; set; }

        public string v_HistoryId { get; set; }

        public int i_Answer { get; set; }

        public string i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public string v_Item { get; set; }
        public int i_Item { get; set; }

        public String v_Occupation { get; set; }

        public String v_CIE10Id { get; set; }

        public String v_Name { get; set; }

        public string NombreHospital { get; set; }
        public string v_Complicaciones { get; set; }

        // Betoo
    }
}
