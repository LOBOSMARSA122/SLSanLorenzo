using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
   public  class PersonMedicalHistoryList
    {
        public string Enfermedad { get; set; }
        public string v_PersonId { get; set; }
        public string v_DiseasesId { get; set; }
        public string v_FechaInicio { get; set; }
        public bool AsociadoTrabajo { get; set; }
        public int? i_AsociadoTrabajo { get; set; }
        public string v_DiagnosticDetail { get; set; }
        public string v_TreatmentSite { get; set; }
        public string v_PersonMedicalHistoryId { get; set; }

        public int? i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public string v_GroupName { get; set; }
        public string v_DiseasesName { get; set; }
        public string v_TypeDiagnosticName { get; set; }
        public DateTime? d_StartDate { get; set; }

        public int? i_TypeDiagnosticId { get; set; }
        public int? i_Answer { get; set; }
    }
}
