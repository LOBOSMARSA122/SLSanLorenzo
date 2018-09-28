using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class AudiometriaList
    {      
        public string v_PersonId { get; set; }
        public string v_FullPersonName { get; set; }
        public string v_DocNumber { get; set; }
        public int? i_SexTypeId { get; set; }
        public string v_SexType { get; set; }
        public DateTime? d_BirthDate { get; set; }
        public DateTime? d_ServiceDate { get; set; }
        public string Puesto { get; set; }

        public byte[] FirmaTrabajador { get; set; }
        public byte[] HuellaTrabajador { get; set; }
        public byte[] FirmaMedico { get; set; }
        public byte[] FirmaTecnologo { get; set; }
        public int i_AgePacient { get; set; }

        // Requisitos para la audiometria

        public string CambiosAltitud { get; set; }
        public string ExpuestoRuido { get; set; }
        public string ProcesoInfeccioso { get; set; }
        public string DurmioNochePrevia { get; set; }
        public string ConsumioAlcoholDiaPrevio { get; set; }
      

        // Antecedentes Medicos de importancia

        public string RinitisSinusitis { get; set; }
        public string UsoMedicamentos { get; set; }
        public string Sarampion { get; set; }
        public string Tec { get; set; }
        public string OtitisMediaCronica { get; set; }
        public string DiabetesMellitus { get; set; }
        public string SorderaAntecedente { get; set; }
        public string SorderaFamiliar { get; set; }
        public string Meningitis { get; set; }
        public string Dislipidemia { get; set; }
        public string EnfTiroidea { get; set; }
        public string SustQuimicas { get; set; }
      

        // Hobbies

        public string UsoMP3 { get; set; }
        public string PracticaTiro { get; set; }
        public string Otros { get; set; }

        // Sintomas actuales

        public string Sordera { get; set; }
        public string Otalgia { get; set; }
        public string Acufenos { get; set; }
        public string SecrecionOtica { get; set; }
        public string Vertigos { get; set; }

        // Otoscopia

        public string OidoIzquierdo { get; set; }
        public string OidoDerecho { get; set; }

        // Dx automaticos
        public string DX_OIDO_DERECHO { get; set; }
        public string DX_OIDO_IZQUIERDO { get; set; }
        public string v_RecomendationName { get; set; }


        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }

        public string v_ServiceComponentId { get; set; }
        public string v_EsoTypeName { get; set; }
        public string v_CustomerOrganizationName { get; set; }
        public string v_EmployerOrganizationName { get; set; }

        public string MarcaAudiometria { get; set; }
        public string ModeloAudiometria { get; set; }
        public string CalibracionAudiometria { get; set; }

        public string TiempoTrabajo { get; set; }
        public string NombreUsuarioGraba { get; set; }

        public string Dx { get; set; }
        public string Recomendaciones { get; set; }

        public string AUDIO_NIVEL_AMB_RUIDO_ID { get; set; }
        public string AUDIO_RUIDO_EXTRA_LABO_ID { get; set; }
        public string AUDIO_ANIOS_ID { get; set; }
        public string AUDIO_FREC_SERC_MILI_ID { get; set; }
        public string AUDIO_ANIOS_DEP_AERO_ID { get; set; }
        public string AUDIO_FREC_DEP_AEREO_ID { get; set; }
        public string AUDIO_ANIOS_DEP_SUB_MARINOS_ID { get; set; }
        public string AUDIO_FRE_DEP_SUB_MARINOS_ID { get; set; }
        public string AUDIO_ANIOS_MANI_ARMAS_FUE_ID { get; set; }
        public string AUDIO_FRE_MANI_ARMAS_ID { get; set; }

        public string AUDIO_ANIOS_EXPO_MUSICA_ALTA_ID { get; set; }
        public string AUDIO_FRE_MUSICA_ALTA_ID { get; set; }
        public string AUDIO_ANIOS_USO_AUDIF_ID { get; set; }
        public string AUDIO_FRE_USO_AUDIF_ID { get; set; }
        public string AUDIO_ANIOS_MOTO_ID { get; set; }
        public string AUDIO_FRE_MOTO_ID { get; set; }
        public string AUDIO_ANIOS_OTRO_ID { get; set; }
        public string AUDIO_FRE_OTROS_ID { get; set; }
        public string AUDIO_HORAS_ID { get; set; }
        public string AUDIO_AMBOS_ID { get; set; }
        public string AUDIO_ANIOS_SERVICIO_MILITAR_ID { get; set; }

        public string AUDIO_UMBRAL_DETEC_PALABRA_ID { get; set; }
        public string AUDIO_UMBRAL_DETEC_O_I_ID { get; set; }
        public string AUDIO_UMBRAL_RECO_PALABRA_ID { get; set; }
        public string AUDIO_UMBRAL_RECO_O_I_ID { get; set; }
        public string AUDIO_UMBRAL_MAX_DISC_ID { get; set; }
        public string AUDIO_UMBRAL_MAX_O_I_ID { get; set; }
        public string AUDIO_PORCENTAJE_DISC_ID { get; set; }
        public string AUDIO_PORCENTAJE_DISC_OI_ID { get; set; }
        public string AUDIO_DIAPASONES_ID { get; set; }
        public string AUDIO_DIAPASONES_2_ID { get; set; }


        public string AUDIO_DIAPASONES_3_ID { get; set; }
        public string AUDIO_DIAPASONES_4_ID { get; set; }
        public string AUDIO_RINNER_ID { get; set; }
        public string AUDIO_RINNER_2_ID { get; set; }
        public string AUDIO_RINNER_3_ID { get; set; }
        public string AUDIO_RINNER_4_ID { get; set; }
        public string AUDIO_RINNER_OD_ID { get; set; }
        public string AUDIO_RINNER_OI_ID { get; set; }
        public string AUDIO_WEBER_ID { get; set; }
        public string AUDIO_WEBER_2_ID { get; set; }

        public string AUDIO_WEBER_3_ID { get; set; }
        public string AUDIO_WEBER_4_ID { get; set; }
        public string AUDIO_WEBER_OD_ID { get; set; }
        public string AUDIO_WEBER_OI_ID { get; set; }
        public string AUDIO_BASE_ID { get; set; }
        public string AUDIO_BASE_OD_ID { get; set; }
        public string AUDIO_BASE_OI_ID { get; set; }
        public string AUDIO_REFERENCIAL_ID { get; set; }
        public string AUDIO_REFERENCIAL_OD_ID { get; set; }
        public string AUDIO_REFERENCIAL_OI_ID { get; set; }


        public string AUDIO_ACTUAL_ID { get; set; }
        public string AUDIO_ACTUAL_OD_ID { get; set; }
        public string AUDIO_ACTUAL_OI_ID { get; set; }
        public string MENOSCABO_AUDITIVO { get; set; }


        public string CuaelfrecuencidusoEspecificar { get; set; }
        public string DesdcuandoACUFENOS { get; set; }
        public string DesdcuandoMAREOS { get; set; }
        public string Arsenico { get; set; }
        public string Tolueno { get; set; }
        public string OTROS { get; set; }
        public string Infeccioaoidcronic { get; set; }
        public string OtalgiDesdcuando { get; set; }
        public string Otorrea { get; set; }
        public string AntecedentequirurgicoORL { get; set; }

        public string HIPERTENSIoARTERIAL { get; set; }
        public string EnfermedadMeniere { get; set; }
        public string Hipoacusifamiliar { get; set; }
        public string DIABETEMELLITUS { get; set; }
        public string TEFracturadehuestemporal { get; set; }
        public string Rinitis { get; set; }
        public string Sinusitis { get; set; }
        public string Parotiditis { get; set; }
        public string Sarampion1 { get; set; }
        public string UsdOtotoxicos { get; set; }

        public string Otalgia1 { get; set; }

        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public string DireccionPaciente { get; set; }
        public string Telefono { get; set; }

        public int MyProperty { get; set; }
    }
}
