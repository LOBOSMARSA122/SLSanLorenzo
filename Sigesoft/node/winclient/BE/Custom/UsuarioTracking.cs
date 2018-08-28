using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class UsuarioTracking
    {
        public int UsuarioId { get; set; }
        public int PersonaId { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public string PreguntaSecreta { get; set; }
        public string RespuestaSecreta { get; set; }
        public DateTime? FechaCaduca { get; set; }
        public int RolId { get; set; }
        public int EmpresaId { get; set; }
    }
}
