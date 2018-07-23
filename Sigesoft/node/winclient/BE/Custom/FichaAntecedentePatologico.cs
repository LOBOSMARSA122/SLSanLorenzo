using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class FichaAntecedentePatologico
    {
        //Datos Generales del Servicio
        public string ServiceId { get; set; }
        public string ServiceComponentId { get; set; }
        public DateTime? FechaServicio { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public int TipoDocumentoId { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string EmpresaCliente { get; set; }
        public string EmpresaTrabajo { get; set; }
        public string EmpresaEmpleadora { get; set; }
        public string Puesto { get; set; }
        public int GeneroId { get; set; }
        public string Genero { get; set; }
        public string LugarNacimiento { get; set; }
        public string LugarProcedencia { get; set; }
        public byte[] FirmaTrabajador { get; set; }
        public byte[] HuellaTrabajador { get; set; }
        public byte[] FirmaUsuarioGraba { get; set; }
        public byte[] FirmaMedicina { get; set; }
        

        public string Alergia { get; set; }
        public string AmigdalitisCronica { get; set; }
        public string Asma { get; set; }
        public string Bocio { get; set; }
        public string Bronconeumonia { get; set; }
        public string BronquitisRepeticion { get; set; }
        public string Colecistitis { get; set; }
        public string Dermatisis { get; set; }
        public string Diabetes { get; set; }
        public string Disenteria { get; set; }
        public string ArritmiasCardiacas { get; set; }
        public string EnfCorazon { get; set; }
        public string Caries { get; set; }
        public string EnfOculares { get; set; }
        public string Epilepcia { get; set; }
        public string FaringitisCronica { get; set; }
        public string FiebreMala { get; set; }
        public string FiebreTifoidea { get; set; }

        public string FiebreReumatica { get; set; }
        public string Forunculosis { get; set; }
        public string GastritisCronica { get; set; }
        public string Gonorrea { get; set; }
        public string Hemorroides { get; set; }
        public string Hepatitis { get; set; }
        public string Hernias { get; set; }
        public string InfUrinaria { get; set; }
        public string Intoxicaciones { get; set; }
        public string LitiasisUrinaria { get; set; }
        public string Meningitis { get; set; }
        public string Neuritis { get; set; }
        public string Otitis { get; set; }
        public string PresionAlta { get; set; }
        public string Paludismo { get; set; }
        public string Parasitosis { get; set; }
        public string Parodititis { get; set; }
        public string Pleuresia { get; set; }

        public string Plubismo { get; set; }
        public string Poliomelitis { get; set; }
        public string ResfrioFrecuente { get; set; }
        public string Reumatismo { get; set; }
        public string Sarampion { get; set; }
        public string Sifilis { get; set; }
        public string Silicosis { get; set; }
        public string Sinusitis { get; set; }
        public string Traumatismo { get; set; }
        public string TosConvulsiva { get; set; }
        public string TrastNervioso { get; set; }
        public string Tuberculosis { get; set; }
        public string Tumores { get; set; }
        public string Ulcera { get; set; }
        public string Gota { get; set; }
        public string Varices { get; set; }
        public string Varicocele { get; set; }
        public string Varicela { get; set; }
        
        public string PerdidaMemoria { get; set; }
        public string Preocupacion { get; set; }
        public string DoloresArteriales { get; set; }
        public string AumentoDisPeso { get; set; }
        public string DolorCabeza { get; set; }
        public string Diarrea { get; set; }
        public string AgitacionEjercicios { get; set; }
        public string DolorOcular { get; set; }
        public string DolorOpresivo { get; set; }
        public string HinchazonPiesManos { get; set; }

        public string Estrenimiento { get; set; }
        public string VomitosConSangre { get; set; }
        public string SangreOrina { get; set; }
        public string TosConSangre { get; set; }
        public string ColoracionAmarilla { get; set; }
        public string IndigestionFrecuente { get; set; }
        public string Insomnio { get; set; }
        public string Lumbalgias { get; set; }
        public string MareosDesmayo { get; set; }
        public string HecesNegras { get; set; }

        public string OrinaDolor { get; set; }
        public string OrinaInvoluntaria { get; set; }
        public string DolorOido { get; set; }
        public string SecrecionOido { get; set; }
        public string Palpitcion { get; set; }
        public string Adormecimiento { get; set; }
        public string PesadillaFrecuente { get; set; }
        public string DolorMuscular { get; set; }
        public string TosCronico { get; set; }
        public string SangradoEncias { get; set; }

        public string Fumar { get; set; }
        public string Licor { get; set; }
        public string Drogas { get; set; }

        public string NumeroCigarrillo { get; set; }
        public string TipoMasFrecuente { get; set; }
        public string TipoProbado { get; set; }

        public string FechaAntecedenteQuirurgico { get; set; }
        public string HospitalAntecedenteQuirurgico { get; set; }
        public string OperacionAntecedenteQuirurgico { get; set; }
        public string DiasAntecedenteQuirurgico { get; set; }
        public string ComplicacionesAntecedenteQuirurgico { get; set; }

        public string InicioMestrucion { get; set; }
        public string InicioVidaSexual { get; set; }
        public string NumeroParejas { get; set; }
        public int? DamasNumeroHijosVivos { get; set; }
        public int? DamasNumeroHijosFallecidos { get; set; }
        public string DamasNumeroAborto { get; set; }
        public string DamasCausaAborto { get; set; }
        public int? VaronesNumeroHijosVivos { get; set; }
        public int? VaromesNumeroHijosFallecidos { get; set; }
        public string VaromesNumeroAbortoPareja { get; set; }
        public string VaronesCausaAborto { get; set; }

        public string Dia { get; set; }
        public string Mes { get; set; }
        public string Anio { get; set; }

         public byte[] b_Logo { get; set; }
         public byte[] b_Logo_Cliente { get; set; }

        public List<CirugiaList> Cirugias { get; set; }
    }
}
