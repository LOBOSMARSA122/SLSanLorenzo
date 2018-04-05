using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportCuestionarioOjoSeco
    {
        public string Trabajador{ get; set; }

        public DateTime? FechaSinFormatear { get; set; }

        public string Fecha { get; set; }

        public string Empresa{ get; set; }

        public string EnrojOcular{ get; set; }

        public string ParpaInflama{ get; set; }

        public string CostraParpa{ get; set; }

        public string OjosPegados{ get; set; }

        public string Secreciones{ get; set; }

        public string SequedadOjo{ get; set; }

        public string SensaArenilla{ get; set; }

        public string SensaCuerpExtra{ get; set; }

        public string Ardor{ get; set; }

        public string Picor{ get; set; }

        public string MalestarOjo{ get; set; }

        public string DolorAgudo{ get; set; }

        public string Lagrimeo{ get; set; }

        public string OjosLlorosos{ get; set; }

        public string SensibilidadLuz{ get; set; }

        public string VisionBorrosa{ get; set; }

        public string CansancioOjo{ get; set; }

        public string SensaPesadez{ get; set; }

        public byte[] FirmaMedico { get; set; }

        public byte[] LogoClinica { get; set; }

        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }
        public string NombreUsuarioGraba { get; set; }
    }
}
