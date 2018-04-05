using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
   public class MyListWeb
    {
       public string IdServicio { get; set; }
       public string IdPaciente { get; set; }
       public string EmpresaCliente { get; set; }
       public string Paciente { get; set; }
       public string CalendarId { get; set; }
       public string ProtocolId { get; set; }
       public int? i_StatusLiquidation { get; set; }
    }
}
