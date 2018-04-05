using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public  class TiempoTrabajador
    {
        public string ServiceId { get; set; }
        public string Trabajador { get; set; }
        public DateTime? FechaAtencion { get; set; }
        public DateTime? HoraIngreso { get; set; }
        public DateTime? HoraSalida { get; set; }
        public DateTime? TiempoAtencion { get; set; }
        public DateTime? TiempoMuerto { get; set; }
    }
}
