using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class CargaOftalmologia
    {
        public string v_ServiceId { get; set; }
        public string v_PersonId { get; set; }
        public string UsaLentesSI { get; set; }
        public string UsaLentesNO { get; set; }
        public string UltimaRefraccion { get; set; }

        public string CataratasSINO { get; set; }
        public string HipertensionSINO { get; set; }
        public string glaucomaSINO { get; set; }        
        public string DiabetesSINO { get; set; }
        public string traumatismoSINO { get; set; }
        public string SoldaduraSINO { get; set; }
        public string SustQuimicasSINO { get; set; }
        public string AmbliopiaSINO { get; set; }
    }
}
