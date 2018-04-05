using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public class ReporteCartaCotizacion
    {
       public string EmpresaId { get; set; }
       public byte[] LogoPropietaria { get; set; }
       public string EmpresaCliente { get; set; }
       public string RucCliente { get; set; }
       public string DireccionCliente { get; set; }
       public byte[] FirmaSa { get; set; }

       public string NombreEmpresaPropietaria { get; set; }
	public string Representante_Legal_Propietaria { get; set; }
	public string Contacto_RRHH_Propietaria { get; set; }
	public string Contacto_Medico_Propietaria { get; set; }
	public string Email_Representante_Legal_Propietaria { get; set; }
	public string Email_Contacto_RRHH_Propietaria { get; set; }
	public string Email_Contacto_Medico_Propietaria { get; set; }
    public string Telefono_Propietaria { get; set; }

    }
}
