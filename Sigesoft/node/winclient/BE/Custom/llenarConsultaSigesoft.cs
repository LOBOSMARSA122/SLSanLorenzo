using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class llenarConsultaSigesoft
    {
        public string IdComponente { get; set; }
        public string IdService { get; set; }
        public string Nombre_Componente { get; set; }
        public decimal Total { get; set; }
        public int Contador { get; set; }
        public string Geso { get; set; }
        public string EmpresaCliente { get; set; }        
        public string ProtocoloId { get; set; }
        public string TipoESO { get; set; }
    }
}
