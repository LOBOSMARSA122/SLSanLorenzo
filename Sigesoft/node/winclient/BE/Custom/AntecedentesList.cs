using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public class AntecedentesList
    {
       public string v_DiseasesId { get; set; }
       public string PersonId { get; set; }
       public string v_DiseasesName { get; set; }
       public DateTime? d_StartDate { get; set; }
       public string Detalle_Dx { get; set; }
       public string Parentesco { get; set; }
       public string  TipoAntecedente { get; set; }
    }
}
