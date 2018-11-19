using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ProduccionProfesionalDetalle
    {
        public string NroAtencion { get; set; }
        public DateTime Fecha { get; set; }
        public int Edad { get; set; }
        public string Dni { get; set; }
        public string Paciente { get; set; }
        public string Parentesco { get; set; }
        public string Titular { get; set; }
        public string EmpresaCliente { get; set; }
        public string EmpresaTrabajo { get; set; }
        public string CostoProtocolo { get; set; }
        public string EstoType { get; set; }
    }
}
