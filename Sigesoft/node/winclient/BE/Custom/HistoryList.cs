using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
   public class HistoryList
    {
       public int Id { get; set; }

        public String v_HistoryId { get; set; }
        
        public String v_WorkstationDangersId { get; set; }
        
        public String v_TypeofEEPId { get; set; }
        
        public Nullable<DateTime> d_StartDate { get; set; }
        
        public Nullable<DateTime> d_EndDate { get; set; }
        
        public String v_Organization { get; set; }
        
        public String v_TypeActivity { get; set; }
        
       
        
        public String v_workstation { get; set; }

        public string i_IsDeleted { get; set; }

        public string v_CreationUser { get; set; }

        public string v_UpdateUser { get; set; }

        public DateTime? d_CreationDate { get; set; }

        public DateTime? d_UpdateDate { get; set; }

        public byte[] b_FingerPrintImage { get; set; }

        public byte[] b_RubricImage { get; set; }

        public string t_RubricImageText { get; set; }

        public string Fecha { get; set; }

        public string Exposicion { get; set; }

        public Nullable<Int32> i_GeografixcaHeight { get; set; }

        public int i_TypeOperationId { get; set; }

        public string v_TypeOperationName { get; set; }

        public string Epps { get; set; }

        public string v_PathologicalFamilyHistory { get; set; }

        public List<string> Fechas { get; set; }

        public string v_StartDate { get; set; }

        public string v_EndDate { get; set; }

        public string TiempoLabor { get; set; }

        public int i_Trabajo_Actual { get; set; }
        public int? i_SoloAnio { get; set; }
       

    }
}
