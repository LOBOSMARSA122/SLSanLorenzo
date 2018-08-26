using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class EnviarSeguimientoSigesoft
    {

        public string v_ServiceId { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_PersonId { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int TipoDocumentoId { get; set; }
        public string NroDocumento { get; set; }
        public int GeneroId { get; set; }

        public DateTime? FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public byte[] Foto { get; set; }
        public string LugarNacimiento { get; set; }
        public int EstadoCivilId { get; set; }
        public string PuestoLaboral { get; set; }
        public int? GradoInstruccionId { get; set; }
        public string Direccion { get; set; }

        public int AptitudId { get; set; }
        public DateTime? FechaExamen { get; set; }
        public int TipoExamenId { get; set; }

        public List<DiagnosticoSeguimientoSigesoft> Dxs { get; set; }

    }
}
