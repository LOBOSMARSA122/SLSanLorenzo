using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public  class InterfaceSeguimiento
    {
       public string IdTrabajador { get; set; }
       public string IdServicio { get; set; }
       //Trabajador
        public string NombresTrabajador { get; set; }
        public string ApePaternoTrabajador { get; set; }
        public string ApeMaternoTrabajador { get; set; }
        public int? TipoDocumentoTrabajador { get; set; }
        public string NroDocumentoTrabajador { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int? GeneroId { get; set; }
        public string Email { get; set; }
        public string Puesto { get; set; }
        public int AreaId { get; set; }

        //Protocolo
        public string Proveedor { get; set; }
        public string Protocolo { get; set; }
        public DateTime? FechaAtencion { get; set; }

        public string Trabajador { get; set; }
        public string DNI { get; set; }
       //Datos de Examenes
        public string TabTipo { get; set; }
        public string TabCantidad { get; set; }
        public string TabFrecuencia { get; set; }
        public string AlcoTipo { get; set; }
        public string AlcoCantidad { get; set; }
        public string AlcoFrecuencia { get; set; }
        public string DrogaTipo { get; set; }

        public string DrogaCantidad { get; set; }
        public string DrogaFrecuencia { get; set; }
        public string ActFisiFrecuencia { get; set; }
        public string ActFisiDetalle { get; set; }
        public string DxActividadFisica { get; set; }
        public string DxActividadFisicaCie10 { get; set; }
        public string DxTabaquismo { get; set; }
        public string DxTabaquismoCie10 { get; set; }
        public string PresionAsistolica { get; set; }
        public string PresionADiastolica { get; set; }

        public string Talla { get; set; }
        public string Peso { get; set; }
        public string Imc { get; set; }
        public string DxNutricion { get; set; }
        public string DxNutricionCie10 { get; set; }
        public string DxPresionArterial { get; set; }
        public string DxPresionArterialCie10 { get; set; }
        public string TriajeRecomendaciones { get; set; }
        public string TriajeRestricciones { get; set; }
        public string ExaFisicoConclusion { get; set; }

        public string ExaFisicoDx1 { get; set; }
        public string ExaFisicoDx1Cie10 { get; set; }
        public string ExaFisicoDx2 { get; set; }
        public string ExaFisicoDx2Cie10 { get; set; }
        public string ExaFisicoDx3 { get; set; }
        public string ExaFisicoDx3Cie10 { get; set; }
        public string ExaFisicoDx4 { get; set; }
        public string ExaFisicoDx4Cie10 { get; set; }
        public string ExaFisicoDx5 { get; set; }
        public string ExaFisicoDx5Cie10 { get; set; }

        public string ExaFisicoDx6 { get; set; }
        public string ExaFisicoDx6Cie10 { get; set; }
        public string ExaFisicoDx7 { get; set; }
        public string ExaFisicoDx7Cie10 { get; set; }
        public string ExaFisicoRecomendaciones { get; set; }
        public string ExaFisicoRestricciones { get; set; }
        //Osteomuscular
        public string OsteomuscularDescripcion { get; set; }
        public string OsteomuscularConclusion { get; set; }
        public string OsteomuscularDx1 { get; set; }
        public string OsteomuscularDx1Cie10 { get; set; }
        public string OsteomuscularDx2 { get; set; }
        public string OsteomuscularDx2Cie10 { get; set; }
        public string OsteomuscularDx3 { get; set; }
        public string OsteomuscularDx3Cie10 { get; set; }
        public string OsteomuscularDx4 { get; set; }
        public string OsteomuscularDx4Cie10 { get; set; }
        public string OsteomuscularRecomendaciones { get; set; }
        public string OsteomuscularRestricciones { get; set; }
        //Audimetría
        public string AudioOtoscopiaOd { get; set; }
        public string AudioOtoscopiaOi { get; set; }
        public string AudioDx1 { get; set; }
        public string AudioDx1Cie10 { get; set; }
        public string AudioDx2 { get; set; }
        public string AudioDx2Cie10 { get; set; }
        public string AudioDx3 { get; set; }
        public string AudioDx3Cie10 { get; set; }
        public string AudioDx4 { get; set; }
        public string AudioDx4Cie10 { get; set; }
        public string AudioRecomendaciones { get; set; }
        public string AudioRestricciones { get; set; }
        //Oftalmología
        public string OftalmoAnamnesis { get; set; }
        public string OftalmoVlScOd { get; set; }
        public string OftalmoVlScOi { get; set; }
        public string OftalmoVlCrOd { get; set; }
        public string OftalmoVlCrOi { get; set; }
        public string OftalmoVlEsOd { get; set; }
        public string OftalmoVlEsOi { get; set; }
        public string OftalmoVcScOd { get; set; }
        public string OftalmoVcScOi { get; set; }
        public string OftalmoVcCrOd { get; set; }
        public string OftalmoVcCrOi { get; set; }
        public string OftalmoVcEsOd { get; set; }
        public string OftalmoVcEsOi { get; set; }
        public string OftalmoTestColoresOd { get; set; }
        public string OftalmoTestColoresOi { get; set; }
        public string OftalmoEstereopsisOd { get; set; }
        public string OftalmoEstereopsisOi { get; set; }
        public string OftalmoHallazgos { get; set; }
        public string OftalmoDx1 { get; set; }
        public string OftalmoDx1Cie10 { get; set; }
        public string OftalmoDx2 { get; set; }
        public string OftalmoDx2Cie10 { get; set; }
        public string OftalmoDx3 { get; set; }
        public string OftalmoDx3Cie10 { get; set; }
        public string OftalmoDx4 { get; set; }
        public string OftalmoDx4Cie10 { get; set; }
        public string OftalmoDx5 { get; set; }
        public string OftalmoDx5Cie10 { get; set; }
        public string OftalmoDx6 { get; set; }
        public string OftalmoDx6Cie10 { get; set; }
        public string OftalmoRecomendaciones { get; set; }
        public string OftalmoRestricciones { get; set; }
        //Espirometría
        public string EspiroAntecedentes { get; set; }
        public string EspiroObservacion { get; set; }
        public string EspiroDx1 { get; set; }
        public string EspiroDx1Cie10 { get; set; }
        public string EspiroDx2 { get; set; }
        public string EspiroDx2Cie10 { get; set; }
        public string EspiroDx3 { get; set; }
        public string EspiroDx3Cie10 { get; set; }
        public string EspiroDx4 { get; set; }
        public string EspiroDx4Cie10 { get; set; }
        public string EspiroRecomendaciones { get; set; }
        public string EspiroRestricciones { get; set; }
        //Rx
        public string RxNroPlaca { get; set; }
        public string RxConclusiones { get; set; }
        public string RxDx1 { get; set; }
        public string RxDx1Cie10 { get; set; }
        public string RxDx2 { get; set; }
        public string RxDx2Cie10 { get; set; }
        public string RxDx3 { get; set; }
        public string RxDx3Cie10 { get; set; }
        public string RxDx4 { get; set; }
        public string RxDx4Cie10 { get; set; }
        public string RxRecomendaciones { get; set; }
        public string RxRestricciones { get; set; }
        //OIT
        public string OitNroPlaca { get; set; }
        public string OitNeumoconiosis { get; set; }
        public string OitConclusiones { get; set; }
        public string OitConclusionesDescripcion { get; set; }
        public string OitDx1 { get; set; }
        public string OitDx1Cie10 { get; set; }
        public string OitDx2 { get; set; }
        public string OitDx2Cie10 { get; set; }
        public string OitDx3 { get; set; }
        public string OitDx3Cie10 { get; set; }
        public string OitDx4 { get; set; }
        public string OitDx4Cie10 { get; set; }
        public string OitRecomendaciones { get; set; }
        public string OitRestricciones { get; set; }
        //Psicología
        public string PsicoAreaCognitiva { get; set; }
        public string PsicoAreaEmocional { get; set; }
        public string PsicoEvaEspaciosConfinados { get; set; }
        public string PsicoEvaEspaciosAltura { get; set; }
        public string PsicoRecomendaciones { get; set; }
        public string PsicoRestricciones { get; set; }
        //EKG
        public string EkgAntecedentes { get; set; }
        public string EkgHr { get; set; }
        public string EkgRr { get; set; }
        public string EkgPq { get; set; }
        public string EkgQrs { get; set; }
        public string EkgQt { get; set; }
        public string EkgQtc { get; set; }
        public string EkgSt { get; set; }
        public string EkgDx1 { get; set; }
        public string EkgDx1Ce10 { get; set; }
        public string EkgDx2 { get; set; }
        public string EkgDx2Ce10 { get; set; }
        public string EkgDx3 { get; set; }
        public string EkgDx3Ce10 { get; set; }
        public string EkgDx4 { get; set; }
        public string EkgDx4Ce10 { get; set; }
        public string EkgRecomendaciones { get; set; }
        public string EkgRestricciones { get; set; }
        //GrupoSanguíneo
        public string GrupoSanguineo { get; set; }
        public string FactorSanguineo { get; set; }
        //Colesterol
        public string Colesterol { get; set; }
        public string ColesterolDx { get; set; }
        public string ColesterolDxCie10 { get; set; }
        public string ColesterolRecomendaciones { get; set; }
        public string ColesterolRestricciones { get; set; }
        //Glucosa
        public string Glucosa { get; set; }
        public string GlucosaDx { get; set; }
        public string GlucosaDxCie10 { get; set; }
        public string GlucosaRecomendaciones { get; set; }
        public string GlucosaRestricciones { get; set; }
        //Triglicéridos
        public string Trigliceridos { get; set; }
        public string TrigliceridosDx { get; set; }
        public string TrigliceridosDxCie10 { get; set; }
        public string TrigliceridosRecomendaciones { get; set; }
        public string TrigliceridosRestricciones { get; set; }
        public string Aptitud { get; set; }
        public string Comentario { get; set; }
        public int? AptitudId { get; set; }


        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
       
        public string IdProtocolId { get; set; }
        public byte[] Foto { get; set; }
        public string Foto_String { get; set; }
    }
}
