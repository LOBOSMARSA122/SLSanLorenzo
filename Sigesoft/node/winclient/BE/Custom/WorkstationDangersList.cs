using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
   public class WorkstationDangersList
    {        
        public String v_WorkstationDangersId { get; set; }
        public string v_ParentName { get; set; }
        public Nullable<Int32> i_DangerId { get; set; }
        public string v_DangerName { get; set; }

        public int? i_RecordStatus { get; set; }
        public int? i_RecordType { get; set; }

        public string i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public Nullable<Int32> i_NoiseSource { get; set; }
        public Nullable<Int32> i_NoiseLevel { get; set; }
        public String v_TimeOfExposureToNoise { get; set; }
    }
}
