using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class ReportEstadistica
    {
        public string ServicioId { get; set; }
        public string ProtocoloId { get; set; }
        public string EmpresaId { get; set; }
        public string SedeId { get; set; }
        public string ExamenDxId { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public int? GeneroId { get; set; }
        public string Trabajador { get; set; }
        public string GESOId { get; set; }

        public string DxId { get; set; }
        public string DxNombre { get; set; }
        public int NroTrabajadores { get; set; }

        public string DiagnosticrepositoryId { get; set; }

        public string P_Parc { get; set; }
        public string P_Total { get; set; }
        public string CIE10 { get; set; }
    }
}
