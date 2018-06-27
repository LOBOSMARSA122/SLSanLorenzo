using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class Embarazo
    {
        public string v_EmbarazoId { get; set; }
        public string v_PersonId { get; set; }
        public string v_Anio { get; set; }
        public string v_Cpn { get; set; }
        public string v_Complicacion { get; set; }
        public string v_Parto { get; set; }
        public string v_PesoRn { get; set; }
        public string v_Puerpio { get; set; }
        public string v_ObservacionesGestacion { get; set; }

        public List<Embarazo> embasazosList { get; set; }

        public int? i_IsDeleted { get; set; }
        public string i_InsertUserId { get; set; }
        public string i_UpdateUserId { get; set; }
        public DateTime? d_InsertDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
    }
}
