using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class FichaAudiometricaAudiomax
    {
        //
        public string ServiceId { get; set; }
        public string ServiceComponentId { get; set; }
        public string ApellidosNombre { get; set; }
        public string Referencia { get; set; }
        public string Sede { get; set; }
        public DateTime FechaExamen { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Edad { get; set; }
        public int? i_SexTypeId { get; set; }
        public string Genero { get; set; }
        public string Dni { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Distrito { get; set; }
        public string Ocupacion { get; set; }
        public string Empresa { get; set; }
        public string Actividad { get; set; }
        public string ActividadOtros { get; set; }
        public string Examen { get; set; }

        public string TiempoExpoRuidoTrabajoActual { get; set; }
        public string TiempoExpoRuidoTrabajoAnterior { get; set; }
        public string ElRuidoEs { get; set; }
        public string HorasDiariasExpuestoRudio { get; set; }
        public string EscuchaMusicaMientrasTrabaja { get; set; }
        public string UsaProtectoresAuditivos { get; set; }
        public string FrecuenciaDeUso { get; set; }
        public string FrecuenciaDeUsoEspecificar { get; set; }
        public string ApreciacionRudio { get; set; }

        public string CambioAltitud { get; set; }
        public string ViajeAvion { get; set; }
        public string Acufenos { get; set; }
        public string AcufenosDesdeCuando { get; set; }
        public string Mareos { get; set; }
        public string MareosDesdeCuando { get; set; }
        public string ProblemasComprenderPalabras { get; set; }

        public string ServicioMilitar { get; set; }
        public string PracticaTiro { get; set; }
        public string UsoAudifonos { get; set; }
        public string RealizacionActividades { get; set; }
        public string ConcurrenciaDiscotecas { get; set; }
        public string HabitoEscucharMusica { get; set; }
        public string InmersionesSunmarinas { get; set; }
        public string ExpoToxicosIndustriales { get; set; }
        public string Cianuo { get; set; }
        public string Mercurio { get; set; }
        public string Plomo { get; set; }
        public string Arsenico { get; set; }
        public string Tolueno { get; set; }
        public string Otros { get; set; }

        public string InfeccionOido { get; set; }
        public string Otorrea { get; set; }
        public string AnteQuiruORL { get; set; }
        public string HipertensionArterial { get; set; }
        public string EnfMeniere { get; set; }
        public string HipocusiaFamiliar { get; set; }
        public string DiabetesMellitus { get; set; }
        public string TEC { get; set; }
        public string Rinitis { get; set; }
        public string Sinusitis { get; set; }
        public string Parotiditis { get; set; }
        public string Sarampion { get; set; }
        public string UsoOtotoxicos { get; set; }

        public string Otalgia { get; set; }
        public string OtalgiaDesdeCuando { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Codigo { get; set; }
        public string FechaCalibracion { get; set; }

        public string HizoAudimetria { get; set; }
        public byte[] FirmaTrabajador { get; set; }
        public byte[] HuellaTrabajador { get; set; }


        public string Diagnostico { get; set; }
        public string Recomendaciones { get; set; }

        public byte[] FrimaSelloMedico { get; set; }
    }
}
