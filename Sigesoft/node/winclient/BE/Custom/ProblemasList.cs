using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
  public  class ProblemasList
    {
            public int? i_Tipo { get; set; }
            public DateTime? d_Fecha { get; set; }
            public string  v_Descripcion { get; set; }
            public int?  i_EsControlado { get; set; }
            public string  v_Observacion { get; set; }
            public string v_PersonId { get; set; }
            public string v_EsControlao { get; set; }
            public List<ProblemasList> problemasList { get; set; }

            public int? i_IsDeleted { get; set; }
            public string i_InsertUserId { get; set; }
            public string i_UpdateUserId { get; set; }
            public DateTime? d_InsertDate { get; set; }
            public DateTime? d_UpdateDate { get; set; }

    }
}
