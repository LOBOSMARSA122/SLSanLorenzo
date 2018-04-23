using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportConsentimiento
    {   
        public string Fecha { get; set; }
        public string NombreTrabajador { get; set; }
        public string NroDocumento { get; set; }
        public string Ocupacion { get; set; }
        public string Empresa { get; set; }
        public string Contratista { get; set; }
        public byte[] FirmaTrabajador { get; set; }
        public byte[] HuellaTrabajador { get; set; }
        public byte[] Logo { get; set; }

        public string LugarProcedencia { get; set; }

        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }

        public string v_AdressLocation { get; set; }

        public DateTime? d_ServiceDate { get; set; }
        public string v_ServiceDate { get; set; }

        public string Componentes { get; set; }
        public string ComponentesLaboratorio { get; set; }
    }
}
