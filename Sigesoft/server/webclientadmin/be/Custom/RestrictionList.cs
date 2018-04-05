using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class RestrictionList
    {
        public string v_RestrictionId { get; set; }
        public string v_RestrictionByDiagnosticId { get; set; }
        public string v_DiagnosticRepositoryId { get; set; }
        public string v_ServiceId { get; set; }
        public string v_ComponentId { get; set; }
        public string v_MasterRestrictionId { get; set; }

        public int? i_RecordStatus { get; set; }
        public int? i_RecordType { get; set; }

        public int? i_ItemId { get; set; }

        public int? i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public DateTime? d_StartDateRestriction { get; set; }
        public DateTime? d_EndDateRestriction { get; set; }

        //
        public string v_ComponentFieldValuesRestrictionId { get; set; }
        public string v_ComponentFieldValuesId { get; set; }
        //

        public string v_RestrictionName { get; set; }
        public int i_Item { get; set; }

        public string v_DiseasesId { get; set; } 
    }
}
