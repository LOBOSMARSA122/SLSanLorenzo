using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
 public   class PlanAtencionIntegral
    {
        public string v_PersonId { get; set; }
        public int? i_TipoId { get; set; }
        public string v_Descripcion { get; set; }
        public DateTime? d_Fecha { get; set; }
        public string v_Lugar { get; set; }

        public string i_IsDeleted { get; set; }
        public string i_InsertUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public string i_UpdateUserId { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
