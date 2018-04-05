using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Common
{
    // Clase entidad que contiene información de las tablas del sistema. Se utiliza para la sincronización.
    [DataContract()]
    public partial class TableInfoDto
    {
        [DataMember()]
        public Int32 i_TableInfoId { get; set; }

        [DataMember()]
        public String v_Table { get; set; }

        [DataMember()]
        public String v_Group { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Sync { get; set; }

        [DataMember()]
        public String v_SyncType { get; set; }

        [DataMember()]
        public Nullable<Int32> i_SyncOrder { get; set; }

        [DataMember()]
        public String v_ParentTables { get; set; }

        [DataMember()]
        public String v_PKPrefix { get; set; }

        [DataMember()]
        public String v_PK { get; set; }

        [DataMember()]
        public Nullable<Int32> i_TableId { get; set; }

        [DataMember()]
        public String v_Secuentiality { get; set; }

        [DataMember()]
        public String v_CreatedIn { get; set; }

        [DataMember()]
        public String v_Management { get; set; }
    }
}
