using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public  class DiagnosticosRecomendaciones
    {

       public string ServicioId { get; set; }
       public List<DiagnosticosRecomendacionesList> DetalleDxRecomendaciones { get; set; }

    }
}
