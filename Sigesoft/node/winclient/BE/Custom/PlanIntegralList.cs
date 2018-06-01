using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class PlanIntegralList
    {
        public string v_PlanIntegral { get; set; }
        public string v_PersonId { get; set; }
        public int? i_TipoId { get; set; }
        public string v_Descripcion { get; set; }
        public DateTime? d_Fecha { get; set; }
        public string v_Fecha { get; set; } 
        public string v_Lugar { get; set; }
        public string v_Tipo { get; set; }

        public string i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }

    public class TipoAtencionList
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public List<PlanIntegralList> List { get; set; }
    }
}
