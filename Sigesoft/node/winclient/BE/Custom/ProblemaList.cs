using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ProblemaList
    {
        public string v_ProblemaId { get; set; }
        public int? i_Tipo { get; set; }
        public string v_PersonId { get; set; }
        public DateTime d_Fecha { get; set; }
        public string v_Descripcion { get; set; }
        public int? i_EsControlado { get; set; }
        public string v_EsControlado { get; set; }
        public string v_Observacion{ get; set; }

        public string i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
