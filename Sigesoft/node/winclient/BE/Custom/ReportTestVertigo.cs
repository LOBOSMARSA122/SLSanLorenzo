using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportTestVertigo
    {
        public string Nombres { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public string TEST_VERTIGO_1 { get; set; }
        public string TEST_VERTIGO_2 { get; set; }
        public string TEST_VERTIGO_3 { get; set; }
        public string TEST_VERTIGO_4 { get; set; }
        public string TEST_VERTIGO_5 { get; set; }
        public string TEST_VERTIGO_6 { get; set; }
        public string TEST_VERTIGO_7 { get; set; }
        public string TEST_VERTIGO_8 { get; set; }
        public string Conclusiones { get; set; }

        public byte[] Firma { get; set; }
        public byte[] Sello { get; set; }
        public byte[] Logo { get; set; }
        
        public string NombreUsuarioGraba { get; set; }

        public string Dx { get; set; }
        public string Recomendaciones { get; set; }
        public string EmpresaCliente { get; set; }
    }
}
