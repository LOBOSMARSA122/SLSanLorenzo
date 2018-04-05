using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportParasitologicoSimple
    {
        public DateTime FechaSinFormato { get; set; }

        public string Paciente{ get; set; }

        public string Empresa{ get; set; }

        public string Puesto{ get; set; }

        public string Fecha{ get; set; }


        public string Color{ get; set; }

        public string Consistencia{ get; set; }

        public string RestosAlimenticios{ get; set; }

        public string Sangre{ get; set; }

        public string Moco{ get; set; }

        public string Quistes{ get; set; }

        public string Huevos{ get; set; }

        public string Trofozoitos{ get; set; }

        public string Hematies{ get; set; }

        public string Leucocitos{ get; set; }

        public byte[] FirmaMedico{ get; set; }

        public string Resultados{ get; set; }

        public byte[] LogoEmpresa { get; set; }
    }
}
