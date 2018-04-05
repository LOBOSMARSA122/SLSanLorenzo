using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReporteAtencionMedicaDetallado
    {
        public string Trabajador { get; set; }
        public DateTime? FechaServicio { get; set; }
        public string Producto { get; set; }
        public double CantidadRecetada { get; set; }
        public double CantidadVendida  { get; set; }
        public decimal PrecioVenta { get; set; }
        public double Importe { get; set; }
        public string TipoDocVenta { get; set; }
        public string NroDocVenta { get; set; }
        public string CondicionPago { get; set; }
        public string Vendedor { get; set; }
        public string Profesional { get; set; }
    }
}
