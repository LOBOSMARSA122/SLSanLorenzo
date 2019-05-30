using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class LiquidacionAseguradora
    {
        public string ServicioId { get; set; }
        public DateTime FechaServicio { get; set; }
        public string PersonId { get; set; }
        public string Paciente { get; set; }
        public string EmpresaId { get; set; }
        public string PacientDocument { get; set; }
        public string Aseguradora { get; set; }
        public string Protocolo { get; set; }
        public decimal? TotalAseguradora { get; set; }
        public double? Factor_ { get; set; }
        public decimal Factor { get; set; }
        public double? PPS_ { get; set; }
        public string PPS { get; set; }

        public List<LiquiAseguradoraDetalle> Detalle { get; set; }
    }
}
