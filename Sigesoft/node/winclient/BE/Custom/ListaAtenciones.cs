using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ListaAtenciones
    {
        public DateTime? d_ServiceDate { get; set; }
        public string v_ServiceId { get; set; }
        public string v_Pacient { get; set; }
        public string v_AptitudeStatusName { get; set; }
        public object CertGenerado { get; set; }
        public object CertEnviado { get; set; }
        public object HistoriaGenerada { get; set; }
        public object HistoriaEnviada { get; set; }

        public int? i_EnvioCertificado { get; set; }
        public int? i_EnvioHistoria { get; set; }
        
          public string v_CustomerOrganizationId { get; set; }
          public string v_CustomerLocationId { get; set; }

          public int? i_StatusLiquidation { get; set; }

          public object StatusLiquidation { get; set; }

          public string Protocolo { get; set; }
            
    }
}
