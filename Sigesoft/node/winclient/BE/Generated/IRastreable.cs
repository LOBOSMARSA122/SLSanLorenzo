using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public abstract class Rastreable
    {
        public TipoRegistro TipoReg { get; set; }
        public bool Editado { get; set; }
    }

    public enum TipoRegistro
    {
        Nueva = 0,
        Edicion = 1,
        Eliminacion = 2
    }
}
