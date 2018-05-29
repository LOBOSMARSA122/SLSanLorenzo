using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class DiagnosticosRecomendacionesList
    {
        public string ServicioId { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string IdCampo { get; set; }
        public string IdComponente { get; set; }
        public string IdDeseases { get; set; }
        public int? i_FinalQualiticationId { get; set; }
        public string DiseasesName { get; set; }
        public int? i_DiagnosticTypeId { get; set; }
        public string CIE10 { get; set; }
    }
}
