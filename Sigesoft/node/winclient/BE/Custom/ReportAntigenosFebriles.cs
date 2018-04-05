using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
     public class ReportAntigenosFebriles
    {

         public DateTime FechaSinformatear {get;set;}
         public string Trabajador {get;set;}
         public byte[]  FirmaMedico {get;set;}
         public string EmpresaTrabajador { get; set; }
         public string Puesto { get; set; }
         public string Fecha { get; set; }

         public string ParatificoA { get; set; }
         public string ParatificoADeseable { get; set; }

         public string ParatificoB { get; set; }
         public string ParatificoBDeseable { get; set; }

         public string TificoH { get; set; }
         public string TificoHDeseable { get; set; }

         public string TificoO { get; set; }
         public string TificoODeseable { get; set; }

         public string Brucella { get; set; }
         public string BrucellaDeseable { get; set; }

         public byte[] LogoEmpresa { get; set; }
    }
}
