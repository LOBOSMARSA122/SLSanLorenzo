using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReporteAramark
    {
        public DateTime? FechaNacimiento { get; set; }
        public string IdServicio { get; set; }
        public string IdProtocolId { get; set; }
        public string IdTrabajador { get; set; }

        public string Trabajador { get; set; }      
        public int EdadTrabajador { get; set; }
        public string TipoEmo { get; set; }
        public string PuestoTrabajo { get; set; }
        public string AreaLaboral { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
        public string v_CustomerOrganizationName { get; set; }
        public DateTime? FechaServicioDate { get; set; }
        public DateTime FechaVencimientoEmo { get; set; }
        public string GeneroTrabajador { get; set; }
        public string AntecedentePersonal1 { get; set; }
        public string AntecedentePersonal2 { get; set; }
        public string AntecedentePersonal3 { get; set; }
        public string AntecedentePersonal4 { get; set; }
        public string AntecedenteFamiliar1 { get; set; }
        public string AntecedenteFamiliar2 { get; set; }
        public string AntecedenteFamiliar3 { get; set; }
        public string PresionSistolica { get; set; }
        public string PresionDiastolica { get; set; }
        public string DXPA { get; set; }
        public string FC { get; set; }
        public string FR { get; set; }
        public string Peso { get; set; }
        public string Talla { get; set; }
        public string IMC { get; set; }
        public string ExamenFisico { get; set; }
        public string SOMA1 { get; set; }//Campo en blanco
        public string SOMA2 { get; set; }//Campo en blanco
        public string SOMA3 { get; set; }//Campo en blanco
        public string Hemoglobina { get; set; }
        public string Hematocrito { get; set; }
        public string Plaquetas { get; set; }
        public string Hemograma { get; set; }
        public string GrupoSanguineoFactor { get; set; }
        public string Glucosa { get; set; }
        public string PruebaToleranciaGlucosa { get; set; }//Campo en blanco
        public string HDLColesterol { get; set; }
        public string LDLColesterol { get; set; }
        public string ColesterolTotal { get; set; }
        public string Triglicerido { get; set; }
        public string TGO { get; set; }
        public string TGP { get; set; }
        public string GGTP { get; set; }//Campo en blanco
        public string BilirrubinaTotal { get; set; }
        public string BilirrubinaDirecta { get; set; }
        public string BilirrubinaIndirecta { get; set; }
        public string Creatina { get; set; }
        public string AcidoUrico { get; set; }
        public string Urea { get; set; }
        public string TificoO { get; set; }
        public string tificoH { get; set; }
        public string ParatificoA { get; set; }
        public string ParatificoB { get; set; }
        public string Brucella { get; set; }
        public string HepatitisAIGM { get; set; }
        public string VDRL1 { get; set; }


        
        public string Coprocultivo { get; set; } //Campo en blanco
        public string MicroOrgGramPos { get; set; }//Campo en blanco
        public string MicroOrgGramNeg { get; set; }//Campo en blanco
        public string Hongos { get; set; }//Campo en blanco
        public string Levaduras { get; set; }//Campo en blanco
        public string Hifas { get; set; }//Campo en blanco
        public string PseudoHifas { get; set; }//Campo en blanco



        public string ExParasitologico { get; set; }
        public string BK { get; set; }
        public string ExOrina1 { get; set; }
        public string CocainaCualitativo { get; set; }
        public string CannabinoidesCualitativo  { get; set; }
        public string BHCG { get; set; }
        public string PSA { get; set; }
        public string EKG1  { get; set; }
        public string PruebaEsfuerzo { get; set; }
        public string Oftalmo1 { get; set; }
        public string TestIshihara1 { get; set; }
        public string EnfermedadesOculares { get; set; }
        public string AudiometriaOI1 { get; set; }
        public string AudiometriaOD1 { get; set; }
        public string OtoscopiaOD { get; set; }
        public string OtoscopiaOI { get; set; }
        public string Espirometria1 { get; set; }
        public string RxTorax1 { get; set; }
        public string Odontologia { get; set; }
        public string DxPsicologia { get; set; }

        public string EcografiaMamas { get; set; }//Campo en blanco
        public string PAP { get; set; }//Campo en blanco
        public string Mamografias { get; set; }//Campo en blanco


        public string EcografiaHigadoBiliares { get; set; }
        public string EcografiaRenalViasUrinarias { get; set; }
        public string EcografiaProstatica { get; set; }
        public string ExamenAlturaEstructural { get; set; }
        public string Psicosensometrico { get; set; }

        public string Hallazgo1 { get; set; }
        public string Hallazgo2 { get; set; }
        public string Hallazgo3 { get; set; }
        public string Hallazgo4 { get; set; }
        public string Hallazgo5 { get; set; }
        public string Hallazgo6 { get; set; }
        public string Hallazgo7 { get; set; }
        public string Hallazgo8 { get; set; }

        public string Recomendacion1 { get; set; }
        public string Recomendacion2 { get; set; }
        public string Recomendacion3 { get; set; }
        public string Recomendacion4 { get; set; }
        public string Recomendacion5 { get; set; }
        public string Recomendacion6 { get; set; }
        public string Recomendacion7 { get; set; }
        public string Recomendacion8 { get; set; }
        public string Recomendacion9 { get; set; }
        public string Recomendacion10 { get; set; }
        public string Recomendacion11 { get; set; }
        public string Recomendacion12 { get; set; }
        public string Recomendacion13 { get; set; }
        public string Recomendacion14 { get; set; }


        public string Observaciones { get; set; }//Campo en blanco
        public string Aptitud { get; set; }

        public string Restriccion1 { get; set; }
        public string Restriccion2 { get; set; }
        public string Restriccion3 { get; set; }
        public string Restriccion4 { get; set; }
        public string Restriccion5 { get; set; }
        public string Restriccion6 { get; set; }

        public string RAC1 { get; set; }
        public string RAC2 { get; set; }
        public string RAC3 { get; set; }
        public string RAC4 { get; set; }
        public string RAC5 { get; set; }
        public string RAC6 { get; set; }
        public string RAC7 { get; set; }
        public string RAC8 { get; set; }
        public string RAC9 { get; set; }
        public string RAC10 { get; set; }
        public string RAC11 { get; set; }

        public string RACPQ { get; set; }
    }
}
