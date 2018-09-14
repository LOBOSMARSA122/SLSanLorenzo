using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportEstudioElectrocardiografico
    {

        public DateTime FechaNacimiento { get; set; }

        public string NroFicha{get;set;}

        public DateTime? Fecha{get;set;}

        public string Empresa{get;set;}

        public string NroHistoria{get;set;}

        public string DatosPaciente{get;set;}

        public string Puesto{get;set;}

        public int Edad{get;set;}

        public string Genero{get;set;}

        public string SoploSiNo{get;set;}

        public string CansancioSiNo{get;set;}

        public string MareosSiNo{get;set;}

        public string PresionAltaSiNo{get;set;}

        public string DolorPrecordialSiNo{get;set;}

        public string PalpitacionesSiNo{get;set;}

        public string AtaquesCorazonSiNo{get;set;}

        public string PerdidaConcienciaSiNo{get;set;}

        public string ObesidadSiNo{get;set;}

        public string TabaquismoSiNo{get;set;}

        public string DisplidemiaSiNo{get;set;}

        public string DiabetesSiNo{get;set;}

        public string SedentarismoSiNo{get;set;}

        public string Otros1{get;set;}

        public string DolorPrecordial2SiNo{get;set;}

        public string DesmayosSiNo{get;set;}

        public string Palpitaciones2SiNo{get;set;}

        public string DisneaSiNo{get;set;}

        public string Otros2{get;set;}

        public string Mareos2SiNo{get;set;}

        public string VaricesSiNo{get;set;}

        public string ClaudicacSiNo{get;set;}

        public string Ritmo{get;set;}

        public string IntervaloPR{get;set;}

        public string IntervaloQRS{get;set;}

        public string IntervaloQT{get;set;}

        public string OndaP{get;set;}

        public string OndaPAnormal { get; set; }

        public string OndaT{get;set;}

        public string OndaTAnormal { get; set; }

        public string ComplejoQRS{get;set;}

        public string ComplejoQRSAnormal { get; set; }

        public string SegmentoST{get;set;}

        public string SegementoSTAnormal { get; set; }

        public string TrasntornoRitmo{get;set;}

        public string TranstornoConduccion{get;set;}

      

        public string Hallazgos{get;set;}

        public string Recomendaciones{get;set;}

        public byte[] FirmaTecnico{get;set;}

        public byte[] FirmaMedico { get; set; }

        public string NombreDoctor { get; set; }

        public string NombreTecnologo { get; set; }

        public string Segmento_ST { get; set; }

        public string OtrasAlteraciones { get; set; }
        public int? TipoESO { get; set; }
        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }


        public byte[] b_Imagen { get; set; }
        public string NombreUsuarioGraba { get; set; }


        public string FrecuenciaCardiaca { get; set; }
        public string RitmoCardiaco { get; set; }
        public string CalculoIntervaloPr { get; set; }
        public string CalculoIntervaloQt { get; set; }
        public string EjeElectrico { get; set; }
        public string AlteracionesSegmentoSt { get; set; }
        public string OtrasAlteracionesElectro { get; set; }  
        public string Conclusiones { get; set; }
        public string ConclusionesApendice05 { get; set; }


        public string PrGold { get; set; }
        public string QrsGold { get; set; }
        public string QtcGold { get; set; }
        public string EjeCardicacoGold { get; set; }
        public string HallazgoGold { get; set; }
        //public string HallazgoscGold { get; set; }
        public string ObservacionesGold { get; set; }

        public byte[] HuellaPaciente { get; set; }
        public byte[] FirmaPaciente { get; set; }
        public string ConclusionesGold { get; set; }


        public string EkGNormalGold { get; set; }
    }
}
