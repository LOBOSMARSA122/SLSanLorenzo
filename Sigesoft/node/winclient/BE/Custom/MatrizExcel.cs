using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class MatrizExcel
    {

        public string IdServicio { get; set; }
        public string IdTrabajador { get; set; }
        public string IdProtocolId { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }

        public string NombreCompleto { get; set; }
        public string Dni { get; set; }
        public string LugarNacimiento { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public string RangoEdad { get; set; }
        public string Sexo { get; set; }
        public string Domicilio { get; set; }
        public string Ubigueo { get; set; }
        public string EstadoCivil { get; set; }
        public int NroHijos { get; set; }
        public string NivelEstudio { get; set; }
        public string Telefono { get; set; }
        public string EmpresaSede { get; set; }
        public string TipoExamen { get; set; }
        public string Grupo { get; set; }
        public string PuestoPostula { get; set; }
        public string Area { get; set; }
        public string Proveedor { get; set; }
        public DateTime? FechaExamen { get; set; }

        public string ActividadFisica { get; set; }
        public string ActividadFisicaDetalle { get; set; }
        public string ConsumoDrogas { get; set; }
        public string ConsumoDrogasDetalle { get; set; }
        public string ConsumoAlcohol { get; set; }
        public string ConsumoAlcoholDetalle { get; set; }
        public string ConsumoTabaco { get; set; }
        public string ConsumoTabacoDetalle { get; set; }
        public string AnteGinecologicos { get; set; }


        public string v_Menarquia { get; set; }
        public DateTime? d_Fur { get; set; }
        public string v_CatemenialRegime { get; set; }
        public DateTime? d_PAP { get; set; }
        public string v_FechaUltimaMamo { get; set; }

        public string v_Gestapara { get; set; }
        public int i_MacId { get; set; }
        public string v_Mac { get; set; }
        public string v_CiruGine { get; set; }
        public string v_ResultadosPAP { get; set; }
        public string v_ResultadoMamo { get; set; }
        
        
        


        public string AntePatologicos { get; set; }
        public string AnteFamiliares { get; set; }
        public string Alergias { get; set; }
        public string HipertensionArterial { get; set; }
        public string AnteQuirurgicos { get; set; }
        public string Gastritis { get; set; }
        public string DiabetesMellitus { get; set; }
        public string Tuberculosis { get; set; }
        public string Cancer { get; set; }
        public string Convulsiones { get; set; }
        public string AsmaBronquial { get; set; }
        public string Otros { get; set; }


        public string Pa { get; set; }
        public string Fr { get; set; }
        public string Fc { get; set; }
        public string PerAbdominal { get; set; }
        public string PerCadera { get; set; }
        public string Icc { get; set; }
        public string Peso { get; set; }
        public string Talla { get; set; }
        public string Imc { get; set; }
        public string DxNutricional { get; set; }

        public string Sintomatologia { get; set; }
        public string PielAnexos { get; set; }
        public string Cabello { get; set; }
        public string Ojos { get; set; }
        public string Oidos { get; set; }
        public string Nariz { get; set; }
        public string Boca { get; set; }
        public string Cuello { get; set; }
        public string Torax { get; set; }
        public string Cardiovascular { get; set; }
        public string Abdomen { get; set; }
        public string ApGenitourinario { get; set; }
        public string Locomotor { get; set; }
        public string Marcha { get; set; }
        public string Columna { get; set; }
        public string ExtremidadesSuperiores { get; set; }
        public string ExtremidadesInferiores { get; set; }
        public string SistemaLinfatico { get; set; }
        public string Neurologico { get; set; }


        public string Cabeza7C { get; set; }
        public string Cuello7C { get; set; }
        public string Nariz7C { get; set; }
        public string Boca7C { get; set; }
        public string ReflejosPupilares7C { get; set; }
        public string MiembrosSuperiores7C { get; set; }
        public string MiembrosInferiores7C { get; set; }
        public string ReflejosOsteotendiosos7C { get; set; }
        public string Marcha7C { get; set; }
        public string Columna7C { get; set; }
        public string Abdomen7C { get; set; }
        public string AnillosIInguinales7C { get; set; }
        public string Hernias7C { get; set; }
        public string Varices7C { get; set; }
        public string Genitales7C { get; set; }
        public string Ganclios7C { get; set; }
        public string Pulmones7C { get; set; }
        public string TactoRectal7C { get; set; }
                
        public string DxExaMedicoGeneral { get; set; }
        public string DxMusculoEsqueletico { get; set; }
        public string EvAltura180 { get; set; }
        public string Exa7D { get; set; }
        public string EvaNeurologica { get; set; }
        public string TamizajeDermatologico { get; set; }

        public string DxRadiografiaTorax { get; set; }
        public string DxRadiografiaOIT { get; set; }
        public string InidceNeumoconiosis { get; set; }
        
        public string OD_VA_125 { get; set; }
        public string OD_VA_250 { get; set; }
        public string OD_VA_500 { get; set; }
        public string OD_VA_1000 { get; set; }
        public string OD_VA_2000 { get; set; }
        public string OD_VA_3000 { get; set; }
        public string OD_VA_4000 { get; set; }
        public string OD_VA_6000 { get; set; }
        public string OD_VA_8000 { get; set; }

        public string OI_VA_125 { get; set; }
        public string OI_VA_250 { get; set; }
        public string OI_VA_500 { get; set; }
        public string OI_VA_1000 { get; set; }
        public string OI_VA_2000 { get; set; }
        public string OI_VA_3000 { get; set; }
        public string OI_VA_4000 { get; set; }
        public string OI_VA_6000 { get; set; }
        public string OI_VA_8000 { get; set; }

        public string OD_VO_125 { get; set; }
        public string OD_VO_250 { get; set; }
        public string OD_VO_500 { get; set; }
        public string OD_VO_1000 { get; set; }
        public string OD_VO_2000 { get; set; }
        public string OD_VO_3000 { get; set; }
        public string OD_VO_4000 { get; set; }
        public string OD_VO_6000 { get; set; }
        public string OD_VO_8000 { get; set; }

        public string OI_VO_125 { get; set; }
        public string OI_VO_250 { get; set; }
        public string OI_VO_500 { get; set; }
        public string OI_VO_1000 { get; set; }
        public string OI_VO_2000 { get; set; }
        public string OI_VO_3000 { get; set; }
        public string OI_VO_4000 { get; set; }
        public string OI_VO_6000 { get; set; }
        public string OI_VO_8000 { get; set; }
        public string Dxaudiometria { get; set; }

        public string Fvc { get; set; }
        public string Fev1 { get; set; }
        public string Fev1_Fvc { get; set; }
        public string Fev25_75 { get; set; }
        public string DxEspirometria { get; set; }

        public string UsaLentes { get; set; }
        public string VisionCercaOD { get; set; }
        public string VisionCercaOI { get; set; }
        public string AgudezaVisualLejosOD { get; set; }
        public string AgudezaVisualLejosOI { get; set; }
        public string VisionCercaCorregidaOD { get; set; }
        public string VisionCercaCorregidaOI { get; set; }
        public string AgudezaVisualLejosCorregidaOD { get; set; }
        public string AgudezaVisualLejosCorregidaOI { get; set; }
        public string Refraccion { get; set; }
        public string TestIshihara { get; set; }
        public string Estereopsis { get; set; }
        public string FondoOjo { get; set; }
        public string DxOftalmología { get; set; }


        public string NroPiezasAusentes { get; set; }
        public string NroPiezasCaries { get; set; }
        public string DxOdontologia { get; set; }

        public string DxElectrocardiograma { get; set; }
        public string PruebaEsfuerzo { get; set; }

        public string AreaCognitiva { get; set; }
        public string AreaEmocional { get; set; }
        public string AreaPersonal { get; set; }
        public string AptitudPsicologica { get; set; }
        public string DxPsicologia { get; set; }




        public string GrupoFactor { get; set; }
        public string Leucocitos { get; set; }
        public string DxLeucocitos { get; set; }
        public string Hemoglobina { get; set; }
        public string DxHemoglobina { get; set; }
        public string Eosi { get; set; }
        public string RecuentoPlaquetas { get; set; }
        public string DxHemograma { get; set; }
        public string Glucosa { get; set; }
        public string DxGlucosa { get; set; }
        public string Colesterol { get; set; }
        public string DxColesterol { get; set; }
        public string Hdl { get; set; }
        public string DxHdl { get; set; }
        public string Ldl { get; set; }
        public string DxLdl { get; set; }
        public string Vldl { get; set; }
        public string DxVldl { get; set; }
        public string Trigliceridos { get; set; }
        public string DxTgc { get; set; }
        public string Urea { get; set; }
        public string Creatina { get; set; }
        public string Tgo { get; set; }
        public string Tgp { get; set; }
        public string Leuc { get; set; }
        public string Hemat { get; set; }
        public string DxOrina { get; set; }
        public string Marihuana { get; set; }
        public string Cocaina { get; set; }
        public string Vdrl { get; set; }


        public string DxOcu1 { get; set; }
        public string DxOcu2 { get; set; }
        public string DxOcu3 { get; set; }
        public string DxOcu4 { get; set; }
        public string DxOcu5 { get; set; }
        public string DxOcu6 { get; set; }
        public string DxOcu7 { get; set; }
        public string DxOcu8 { get; set; }

        public string DxMed1 { get; set; }
        public string DxMed2 { get; set; }
        public string DxMed3 { get; set; }
        public string DxMed4 { get; set; }
        public string DxMed5 { get; set; }
        public string DxMed6 { get; set; }
        public string DxMed7 { get; set; }
        public string DxMed8 { get; set; }
        public string DxMed9 { get; set; }
        public string DxMed10 { get; set; }

        public string Reco1 { get; set; }
        public string Reco2 { get; set; }
        public string Reco3 { get; set; }
        public string Reco4 { get; set; }
        public string Reco5 { get; set; }
        public string Reco6 { get; set; }
        public string Reco7 { get; set; }
        public string Reco8 { get; set; }
        public string Reco9 { get; set; }
        public string Reco10 { get; set; }
        public string Reco11 { get; set; }
        public string Reco12 { get; set; }
        public string Reco13 { get; set; }
        public string Reco14 { get; set; }

        public string Res1 { get; set; }
        public string Res2 { get; set; }
        public string Res3 { get; set; }
        public string Res4 { get; set; }
        public string Res5 { get; set; }
        public string Res6 { get; set; }

        public int? AptitudId { get; set; }
        public string AptitudMedica { get; set; }
        public string MotivoAptitud { get; set; }
        public string ComentarioAptitud { get; set; }
        public string Evaluador { get; set; }
        public string CMP { get; set; }
        public string Restricciones { get; set; }
        public string Colinesterasa { get; set; }

        public string Colesterolv2 { get; set; }
        public string DxColesterolLipidico { get; set; }

        public string Trigliceridos2 { get; set; }
        public string DxTgc2 { get; set; }



        public string ritmo { get; set; }
        public string pr { get; set; }
        public string qrs { get; set; }
        public string qt { get; set; }
        public string st { get; set; }
        public string ejeqrs { get; set; }
        public string frecuenciacardiaca { get; set; }
       
        
        
    }
}
