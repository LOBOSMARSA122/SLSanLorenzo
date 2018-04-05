using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ProtocolSystemUSerExternalList
    {      
        public String v_ProtocolSystemUserId { get; set; }
        public Int32 i_SystemUserId { get; set; }
        public string v_UserName { get; set; }
        public String v_ProtocolId { get; set; }
        public String v_ProtocolName { get; set; }
        public Nullable<Int32> i_ApplicationHierarchyId { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerOrganizationName { get; set; }
        public string v_PersonId { get; set; }
        public int? i_IsDeleted { get; set; }
    }
}
