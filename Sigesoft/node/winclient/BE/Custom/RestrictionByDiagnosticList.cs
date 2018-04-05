using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class RestrictionByDiagnosticList
    {
        public string v_RestrictionByDiagnosticId { get; set; }
        public string v_DiagnosticRepositoryId { get; set; }
        public int? i_RestrictionId { get; set; }

        public int? i_RecordStatus { get; set; }
        public int? i_RecordType { get; set; }

        public int? i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public string v_RestrictionName { get; set; }
    
        //
        public string v_ComponentFieldValuesRestrictionId { get; set; }
        public string v_ComponentFieldValuesId { get; set; }
        //
    }
}
