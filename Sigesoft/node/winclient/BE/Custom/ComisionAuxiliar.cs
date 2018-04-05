using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ComisionAuxiliar
    {
        public string IdServicio { get; set; }
        public string Paciente { get; set; }

        public string Componente { get; set; }
        public float? PrecioBase { get; set; }
        public float Comision { get; set; }
        public int CategoriaId { get; set; }
        public int i_InsertUserId { get; set; }

    }
}
