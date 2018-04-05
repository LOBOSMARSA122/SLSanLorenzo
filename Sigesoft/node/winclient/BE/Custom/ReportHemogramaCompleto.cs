using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportHemogramaCompleto
    {
        public byte[] LogoEmpresa { get; set; }
        public DateTime FechaSinFormato { get; set; }

        public string HematocritoReslt{ get; set; }

        public string HematocritoRef{ get; set; }

        public string HemoglobinaReslt{ get; set; }

        public string HemoglobinaRef{ get; set; }

        public string GlobulosRojosReslt{ get; set; }

        public string GlobulosRojosRef{ get; set; }

        public string LeucocitosReslt{ get; set; }

        public string LeucocitosRef{ get; set; }

        public string RecuPlaquetasResult{ get; set; }

        public string RecuPlaquetasRef{ get; set; }

        public string AbastonadosResult{ get; set; }

        public string AbastonadosRef{ get; set; }

        public string SegmentadosResult{ get; set; }

        public string SegmentadoRef{ get; set; }

        public string EosinofilosResult{ get; set; }

        public string EosinofilosRef{ get; set; }

        public string BasofilosResult{ get; set; }

        public string BasofilosRef{ get; set; }

        public string MonocitosResult{ get; set; }

        public string MonocitosRef{ get; set; }

        public string LinfocitosResult{ get; set; }

        public string LinfocitosRef{ get; set; }

        public string Observaciones{ get; set; }

        public string Sanguineo{ get; set; }

        public string Factor{ get; set; }

        public byte[] FirmaMedico{ get; set; }


        public string Paciente { get; set; }

        public string Empresa { get; set; }

        public string Puesto { get; set; }

        public string Fecha { get; set; }

    }
}
