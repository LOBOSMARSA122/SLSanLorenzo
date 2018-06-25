using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;



namespace Sigesoft.Node.WinClient.BE.Generated
{
    [DataContract()]
    public partial class dbconfigDto
    {
        [DataMember()]
        public Int32 id { get; set; }

        [DataMember()]
        public Int32 version { get; set; }

        public dbconfigDto()
        { 
        }

        public dbconfigDto(Int32 id, Int32 version)
        {
            this.id = id;
            this.version = version;
        }

    }
}
