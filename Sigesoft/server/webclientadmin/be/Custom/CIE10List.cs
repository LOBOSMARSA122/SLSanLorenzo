using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    [DataContract]
  public   class CIE10List
    {
        [DataMember]
        public string v_CIE10Code { get; set; }
        [DataMember]
        public string v_CIE10Description1 { get; set; }
        [DataMember]
        public string v_CIE10Description2 { get; set; }
        [DataMember]
        public string v_CreationUser { get; set; }
        [DataMember]
        public string v_UpdateUser { get; set; }
        [DataMember]
        public DateTime? d_CreationDate { get; set; }
        [DataMember]
        public DateTime? d_UpdateDate { get; set; }
        [DataMember]
        public int? i_IsDeleted { get; set; }
    }
}
