using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class FamilyMedicalAntecedentsList
    {
        public string v_FamilyMedicalAntecedentsId { get; set; }
        public string v_Familia { get; set; }
        public string v_PersonId { get; set; }
        public string v_CommentFamili { get; set; }
        public int? i_TypeFamilyId { get; set; }

        public int? i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public string v_TypeFamilyName { get; set; }
        public string v_DiseaseName { get; set; }
        public string v_Comment { get; set; }
    }
}
