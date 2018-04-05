using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Sigesoft.Server.WebClientAdmin.BE
{
   public class ServerNodeSyncList
    {        
        public Int32 i_NodeId { get; set; }
        public string v_NodeName { get; set; }
        public String v_DataSyncVersion { get; set; }        
        public Nullable<Int32> i_DataSyncFrecuency { get; set; }        
        public Nullable<Int32> i_Enabled { get; set; }       
        public Nullable<DateTime> d_LastSuccessfulDataSync { get; set; }      
        public Nullable<Int32> i_LastServerProcessStatus { get; set; }       
        public Nullable<Int32> i_LastNodeProcessStatus { get; set; }      
        public Nullable<DateTime> d_NextDataSync { get; set; }     
        public String v_LastServerPackageFileName { get; set; }      
        public String v_LastServerProcessErrorMessage { get; set; }     
        public String v_LastNodePackageFileName { get; set; } 
        public String v_LastNodeProcessErrorMessage { get; set; }
    }
}
