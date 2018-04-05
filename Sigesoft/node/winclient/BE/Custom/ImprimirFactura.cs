using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public  class ImprimirFactura
    {

       public string NumeroFactura { get; set; }
       public string RazonSocial { get; set; }
       public string Direccion { get; set; }
       public string Ruc { get; set; }
       public DateTime? FechaFacturacion { get; set; }

       public int Cantidad { get; set; }
       public string Examen { get; set; }
       public decimal PrecioUnitario { get; set; }
       public decimal ValorVenta { get; set; }

       public decimal SubTotal { get; set; }
       public decimal Igv { get; set; }
       public decimal Total { get; set; }

       public decimal Detraccion { get; set; }
       public string Paciente { get; set; }
       public string Poliza { get; set; }
       public DateTime? FechaNacimiento { get; set; }
       public decimal? Deducible { get; set; }
       public decimal TotalPagar { get; set; }

    }
}
