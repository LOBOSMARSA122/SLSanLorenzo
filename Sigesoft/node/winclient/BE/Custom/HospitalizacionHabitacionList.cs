using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class HospitalizacionHabitacionList
    {
        public string v_HospitalizacionHabitacionId  { get; set; }
        public string v_HopitalizacionId { get; set; }
        public int i_HabitacionId { get; set; }
        public string NroHabitacion { get; set; }
        public DateTime? d_StartDate { get; set; }
        public DateTime? d_EndDate { get; set; }
        public decimal? d_Precio { get; set; }
        public decimal Total { get; set; }
        public DateTime? d_FechaAlta { get; set; }
        public int? i_conCargoA { get; set; }
    }
}
