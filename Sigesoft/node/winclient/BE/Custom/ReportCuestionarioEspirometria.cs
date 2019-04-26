using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportCuestionarioEspirometria
    {
        public string IdServicio { get; set; }
        public string ServiceComponentId { get; set; }
        public DateTime? Fecha { get; set; }
        public string Fecha_S { get; set; }
        public string NombreTrabajador { get; set; }
        public DateTime? FechaNacimineto { get; set; }
        public string FechaNacimineto_S { get; set; }
        public int Edad { get; set; }
        public int Genero { get; set; }
        public string Pregunta1ASiNo { get; set; }
        public string Pregunta2ASiNo { get; set; }
        public string Pregunta3ASiNo { get; set; }
        public string Pregunta4ASiNo { get; set; }
        public string Pregunta5ASiNo { get; set; }
        public string Pregunta6ASiNo { get; set; }
        public string Pregunta7ASiNo { get; set; }

        public string ESPIROMETRIA_OBSERVACIONES_ASMA { get; set; }
        public string ESPIROMETRIA_OBSERVACIONES_ASMA_TIEMPO { get; set; }
        public string ESPIROMETRIA_OBSERVACIONES_ASMA_CRISIS { get; set; }


        public string HemoptisisSiNo { get; set; }
        public string PseumotoraxSiNo { get; set; }
        public string TraqueostomiaSiNo { get; set; }
        public string SondaPleuralSiNo { get; set; }
        public string AneurismaSiNo { get; set; }
        public string EmboliaSiNo { get; set; }
        public string InfartoSiNo { get; set; }
        public string InestabilidadSiNo { get; set; }
        public string FiebreSiNo { get; set; }
        public string EmbarazoAvanzadoSiNo { get; set; }
        public string EmbarazoComplicadoSiNo { get; set; }

        

        public string Pregunta1BSiNo { get; set; }
        public string Pregunta2BSiNo { get; set; }
        public string Pregunta3BSiNo { get; set; }
        public string Pregunta4BSiNo { get; set; }
        public string Pregunta5BSiNo { get; set; }
        public string Pregunta6BSiNo { get; set; }
        public string Pregunta7BSiNo { get; set; }

        public byte[] FirmaTrabajador { get; set; }
        public byte[] HuellaTrabajador { get; set; }

        public byte[] Logo { get; set; }

        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }

        public byte[] b_File { get; set; }
        public string NombreUsuarioGraba { get; set; }
        public string EmpresaCliente { get; set; }
        public string Dni { get; set; }
        public string TipoExamen { get; set; }
        public int? TipoEso { get; set; }
        public string EmpresaContratista { get; set; }
        public string FrecuenciaCardiaca { get; set; }
        public string ComentarioPrueba { get; set; }
        public string Caucasico { get; set; }
        public string Fumador { get; set; }
        public string Talla { get; set; }
        public string Peso { get; set; }
        public string ActividadEconomica { get; set; }
        public string TiempoTrabajo { get; set; }
        public string PuestoTrabajo { get; set; }
        public string RazonSocial { get; set; }

        public string FVC { get; set; }
        public string VEF1 { get; set; }
        public string PEF { get; set; }
        public string FER { get; set; }
        public string F25 { get; set; }
        public string F50 { get; set; }
        public string MEF { get; set; }
        public string R50 { get; set; }
        public string MVV { get; set; }
        public string FVCDes { get; set; }
        public string VEF1Des { get; set; }
        public string PEFDes { get; set; }
        public string FERDes { get; set; }
        public string F25Des { get; set; }
        public string F50Des { get; set; }
        public string MEFDes { get; set; }
        public string R50Des { get; set; }
        public string MVVDes { get; set; }


        public string Normal { get; set; }
        public string SindromeRestrictivo { get; set; }
        public string SindromeObtructivo  { get; set; }
        public string Observacion { get; set; }
        public byte[] LogoCliente { get; set; }

        public string NroCigarros { get; set; }
    }    
}
