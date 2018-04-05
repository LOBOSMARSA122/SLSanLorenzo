using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class CamposAuditoria
    {
        public string UserNameAuditoriaInsert { get; set; }
        public string FechaHoraAuditoriaInsert { get; set; }
        public string UserNameEvaluadorInsert { get; set; }
        public string FechaHoraEvaluadorInsert { get; set; }
        public string UserNameAuditoriaEdit { get; set; }
        public string FechaHoraAuditoriaEdit { get; set; }
        public string UserNameEvaluadorEdit { get; set; }
        public string FechaHoraEvaluadorEdit { get; set; }

        public string UserNameInformadorInsert { get; set; }
        public string UserNameInformadorEdit { get; set; }
        public string FechaHoraInformadorInsert { get; set; }
        public string FechaHoraInformadorEdit { get; set; }
    }
}
