using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class OrdenReportes
    {
        public string v_OrdenReporteId { get; set; }
        public bool b_Seleccionar { get; set; }
        public string v_ComponenteId { get; set; }
        public string v_NombreReporte { get; set; }
        public int? i_Orden { get; set; }
        public string v_NombreCrystal { get; set; }
        public int? i_NombreCrystalId { get; set; }
        public string v_ComponenteId_ { get; set; }

        public string v_ColumnaId { get; set; }
        public string v_CampoId { get; set; }
    }
}
