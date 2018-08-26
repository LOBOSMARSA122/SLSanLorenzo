using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class UsuarioExternoSeguimiento
    {
        public string v_PersonId { get; set; }
        public int TipoDocumentoId { get; set; }
        public string NroDocumento { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int GeneroId { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public int EstadoCivilId { get; set; }


        public string Usuario { get; set; }
        public string Clave { get; set; }
        public int? RolId { get; set; }
        public string Direccion { get; set; }
        public int? GradoInstruccionId { get; set; }
        public string LugarNacimiento { get; set; }
        
        public string Colegiatura { get; set; }
        public string InfAdicional { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        
    }
}
