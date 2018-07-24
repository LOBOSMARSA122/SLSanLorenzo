using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class HospitalizacionList
    {
        public string v_HopitalizacionId { get; set; }
        public string v_PersonId { get; set; }
        public string v_Paciente { get; set; }
        public DateTime? d_FechaIngreso { get; set; }
        public DateTime? d_FechaAlta { get; set; }
        public int i_IsDeleted { get; set; }

        public List<HospitalizacionServiceList> Servicios{ get; set; }
    }
}
