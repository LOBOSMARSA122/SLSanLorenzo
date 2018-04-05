using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class ReportTamizajeDermatologicoCI
    {
        public string EmpresaCliente { get; set; }
        public string Ficha { get; set; }
        public string Hc { get; set; }
        public string NombreTrabajador { get; set; }
        public DateTime? Fecha { get; set; }
        public byte[] FirmaMedico { get; set; }
        public byte[] FirmaTrabajador { get; set; }
        public byte[] HuellaTrabajador { get; set; }
        public string NombreUsuarioGraba { get; set; }

        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }
        
        public string ALERGIAS_DERMICAS {get;set;}
        public string ALERGIAS_MEDICAMENTOSAS { get; set; }
        public string ENFERMEDADES_PROPIA_DE_LA_PIEL_Y_ANEXOS {get;set;}
        public string DESCRIBIR_ {get;set;}
        public string LUPUS_ERITEMATOSO_SISTEMICO {get;set;}
        public string ENFERMEDAD_TIROIDEA {get;set;}
        public string ARTRITIS_REUMATOIDE {get;set;}
        public string HEPATOPATIAS {get;set;}
        public string PSORIASIS {get;set;}
        public string SINDROME_DE_OVARIO_POLIQUISTICO {get;set;}
        public string DIABETES_MELLITUS_TIPO_2 {get;set;}
        public string OTRAS {get;set;}
        public string DESCRIBIR_2 {get;set;}
        public string MACULA {get;set;}
        public string VESICULA {get;set;}
        public string NODULO {get;set;}
        public string PURPURA {get;set;}
        public string PAPULA {get;set;}
        public string AMPOLLA {get;set;}
        public string PLACA {get;set;}
        public string COMEDONES {get;set;}
        public string TUBERCULO {get;set;}
        public string PUSTULA {get;set;}
        public string QUISTE {get;set;}
        public string TELANGIECTASIA {get;set;}

        public string ESCAMA {get;set;}
        public string PETEQUIA {get;set;}
        public string ANGIOEDEMA {get;set;}
        public string TUMOR {get;set;}
        public string HABON {get;set;}
        public string EQUIMOSIS {get;set;}
        public string DISCROMIAS {get;set;}
        public string OTROS {get;set;}
        public string ESCAMAS {get;set;}
        public string ESCARAS {get;set;}
        public string FISURA {get;set;}
        public string EXCORIACIONES {get;set;}
        public string COSTRAS {get;set;}
        public string CICATRICES {get;set;}
        public string ATROFIA {get;set;}
        public string LIQUENIFICACION {get;set;}

        public string ESCLEROSIS {get;set;}
        public string ULCERAS {get;set;}
        public string EROSION {get;set;}
        public string OTRAS2 {get;set;}
        public string SIN_DERMATOPATIAS {get;set;}

        public int Edad { get; set; }
        public int GeneroId { get; set; }
        public string PuestoTrabajo { get; set; }
        public string DxConcatenados { get; set; }
        public DateTime? FechaNacimiento { get; set; }
    }
}
