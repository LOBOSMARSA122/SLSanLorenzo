using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
   public class HistoryList
    {
        public string v_PersonId { get; set; }
        public string v_Organization { get; set; }
        public string v_TypeActivity { get; set; }
        public string v_workstation { get; set; }
        public string v_Fechas { get; set; }
        public string v_Exposicion { get; set; }
        public string v_Epps { get; set; }
        public string v_TiempoTrabajo { get; set; }
        public int? i_TrabajoActual { get; set; }
        public string v_HistoryId { get; set; }
        public DateTime? d_StartDate { get; set; }
        public DateTime? d_EndDate { get; set; }

         public int i_GeografixcaHeight { get; set; }       
           
        public int? i_IsDeleted { get; set; }
        public int? i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public int? i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
