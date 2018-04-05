using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportAlturaEstructural
    {
        public string v_ComponentId { get; set; }
        public string v_ServiceId { get; set; }
        public string ServicioId { get; set; }
        public string NombrePaciente { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string EmpresaTrabajadora { get; set; }
        public string PuestoTrabajo { get; set; }

        public string AntecedenteTecSI { get; set; }
        public string AntecedenteTecNO { get; set; }
        public string AntecedenteTecObs { get; set; }

        public string ConvulsionesSI { get; set; }
        public string ConvulsionesNO { get; set; }
        public string ConvulsionesObs { get; set; }

        public string MareosSI { get; set; }
        public string MareosNO { get; set; }
        public string MareosObs { get; set; }

        public string AgorafobiaSI { get; set; }
        public string AgorafobiaNO { get; set; }
        public string AgorafobiaObs { get; set; }

        public string AcrofobiaSI { get; set; }
        public string AcrofobiaNO { get; set; }
        public string AcrofobiaObs { get; set; }

        public string InsuficienciaCardiacaSI { get; set; }
        public string InsuficienciaCardiacaNO { get; set; }
        public string InsuficienciaCardiacaObs { get; set; }
        
        public string EstereopsiaSI { get; set; }
        public string EstereopsiaNO { get; set; }
        public string EstereopsiaObs { get; set; }

        public string NistagmusEspontaneo { get; set; }
        public string NistagmusProvocado { get; set; }

        public string PrimerosAuxilios { get; set; }
        public string TrabajoNivelMar { get; set; }

        public string Timpanos { get; set; }
        public string Equilibrio { get; set; }
        public string SustentacionPie20Seg { get; set; }
        public string CaminarLibre3Mts { get; set; }
        public string CaminarLibreVendado3Mts { get; set; }
        public string CaminarLibreVendadoPuntaTalon3Mts { get; set; }
        public string Rotar { get; set; }
        public string AdiadocoquinesiaDirecta { get; set; }
        public string AdiadocoquinesiaCruzada { get; set; }


        public string Apto { get; set; }
        public string descripcion { get; set; }


        public Byte[] RubricaMedico { get; set; }
        public Byte[] RubricaTrabajador { get; set; }
        public byte[] HuellaTrabajador { get; set; }

        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }

        public string IMC { get; set; }
        public string Peso { get; set; }
        public string FC { get; set; }
        public string PA { get; set; }
        public string FR { get; set; }
        public string Sat { get; set; }
        public string PAD { get; set; }
        public string talla { get; set; }


        public string NombreUsuarioGraba { get; set; }
        public string EmpresaCliente { get; set; }
        public string TipoExamen { get; set; }
        public string DNI { get; set; }

    }
}
