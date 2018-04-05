using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public partial class planDto : Rastreable
    {
        public string NombreLinea { get; set; }
        public bool EsDeducible { get; set; }
        public bool EsCoaseguro { get; set; }
    }
}
