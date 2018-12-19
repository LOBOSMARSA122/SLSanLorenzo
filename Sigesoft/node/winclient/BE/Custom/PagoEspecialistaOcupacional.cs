using System.Collections.Generic;

namespace Sigesoft.Node.WinClient.BE
{
    public class PagoEspecialistaOcupacional
    {
        public int NroAtenciones { get; set; }
        public string Minera { get; set; }
        public string Contrata { get; set; }
        public string Terceros { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Total { get; set; }
        public string ServiceIds { get; set; }
        public List<ServiciosEspecialistasOcupacional> Servicios{ get; set; }  
    }

    public class ServiciosEspecialistasOcupacional
    {
        public string ServiceId { get; set; }
        public string Trabajador { get; set; }
        public string Protocolo { get; set; }
    }
}
