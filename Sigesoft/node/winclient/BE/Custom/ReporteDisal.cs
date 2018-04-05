using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReporteDisal
    {
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string IdServicio { get; set; }
        public string IdProtocolId { get; set; }
        public string IdTrabajador { get; set; }
        public string v_ObsStatusService { get; set; }

        public string DNI { get; set; }
        public DateTime? FechaServicioDate { get; set; }
        public string LugarExamen { get; set; }
        public string Trabajador { get; set; }
        public int EdadTrabajador { get; set; }
        public string GeneroTrabajador { get; set; }
        public string PuestoTrabajo { get; set; }
        public string AntecedentesImportancia { get; set; }
        public string Peso { get; set; }
        public string Talla { get; set; }       
        public string IMC { get; set; }
        public string EstadoNutricional { get; set; }
        public string PerimetroAbdominal { get; set; }
        public string DxPerimetroAbdominal { get; set; }
        public string PresionSistolica { get; set; }
        public string PresionDiastolica { get; set; }
        public string DxPA { get; set; }
        public string UsaLentes { get; set; }
        public string AgudezaVisual { get; set; }
        public string OtrasAlteracionesVision { get; set; }
        public string OtrasAlteEnfOculares { get; set; }
        public string Audiometria { get; set; }
        public string ComentarioAudiometria { get; set; }
        public string Espirometria { get; set; }
        public string EvaOsteomuscular { get; set; }
        public string Hemoglobina { get; set; }
        public string DxHB { get; set; }
        public string Hemograma { get; set; }
        public string Glucosa { get; set; }
        public string DxGlucosa { get; set; }
        public string Colesterol { get; set; }
        public string DxColesterol { get; set; }
        public string Trigliceridos { get; set; }
        public string DxTrigliceridos { get; set; }
        public string RPR { get; set; }
        public string ExamenOrina { get; set; }
        public string RadiografiaTorax { get; set; }
        public string ComentarioRadiografia { get; set; }
        public string Odontograma { get; set; }
        public string EKG { get; set; }
        public string ComentarioEKG { get; set; }
        public string Psicologico { get; set; }
        public string OtrosDxs { get; set; }
        public string Aptitud { get; set; }
        public string Retricciones { get; set; }
        public string DxRelacionadoRestriccion { get; set; }
        public string InformeEntregado { get; set; }
        public string ComentariosObsRecomendaciones { get; set; }

    }
}
