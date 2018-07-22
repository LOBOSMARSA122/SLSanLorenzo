using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class CirugiaList
    {
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
        public byte[] b_Logo_Cliente { get; set; }

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

        public DateTime? FechaAntecedenteQuirurgico { get; set; }
        public string OperacionAntecedenteQuirurgico { get; set; }
        public string DiasAntecedenteQuirurgico { get; set; }
        public string HospitalAntecedenteQuirurgico { get; set; }
        public string ComplicacionesAntecedenteQuirurgico { get; set; }
        public string dia { get; set; }
        public string mes { get; set; }
        public string anio { get; set; }
        public string v_PersonMedicalHistoryId { get; set; }



    }
}
