using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ComponentesHospitalizacion
    {

        public string ServiceComponentId { get; set; }
        public string Categoria { get; set; }
        public string Componente { get; set; }
        public float Precio { get; set; }
        public string MedicoTratante { get; set; }
    }
}
