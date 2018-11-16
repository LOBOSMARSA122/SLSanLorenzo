using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ServiceData
    {
        public int? HasSymptomId { get; set; }
        public string MainSymptom { get; set; }
        public int? TimeOfDiseaseTypeId { get; set; }
        public int? TimeOfDisease { get; set; }
        public string Story { get; set; }
        public int? DreamId { get; set; }
        public int? AppetiteId { get; set; }
        public int? DepositionId { get; set; }
        public int? UrineId { get; set; }
        public int? ThirstId { get; set; }
        public string Findings { get; set; }
        public string Menarquia { get; set; }
        public string Gestapara { get; set; }
        public DateTime? Pap { get; set; }
        public DateTime? Fur { get; set; }
        public int? MacId { get; set; }
        public DateTime? Mamografia { get; set; }
        public string CatemenialRegime { get; set; }
        public string CiruGine { get; set; }
        public int? SexTypeId { get; set; }
        public string PersonId { get; set; }
        public string ServiceId { get; set; }
        public string Trabajador { get; set; }
        public string ProtocolName { get; set; }
        public int? EsoTypeId { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? GlobalExpirationDate { get; set; }
        public int AptitudeStatusId { get; set; }

        public string v_InicioVidaSexaul { get; set; }
        public string v_NroParejasActuales { get; set; }
        public string v_NroAbortos { get; set; }
        public string v_PrecisarCausas { get; set; }

    }
}
