using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportHistoriaOcupacionalList
    {
        public string Trabajador { get; set; }
        public string IdServicio { get; set; }
        public string IdHistory { get; set; }
        public DateTime? FNacimiento { get; set; }
        public string FechaNacimiento { get; set; }
        public int Genero { get; set; }
        public string LugarNacimiento { get; set; }
        public string LugarProcedencia { get; set; }
        public string Puesto { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Fechas { get; set; }
        public string Empresa { get; set; }
        public int? Altitud { get; set; }
        public string AreaTrabajo { get; set; }
        public string PuestoTrabajo { get; set; }
        public int? IdTipoOperacion { get; set; }
        public string TiempoLabor { get; set; }
        public string Peligros { get; set; }
        public string Epp { get; set; }
        public string ActividadEmpresa{ get; set; }
    
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }

        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }

        public string FuenteRuidoName { get; set; }
        public string NivelRuidoName { get; set; }
        public string TiempoExpoRuidoName { get; set; }

        public string v_PersonId { get; set; }
        public string v_FullPersonName { get; set; }
        public string v_WorkingOrganizationName { get; set; }
        public string v_FullWorkingOrganizationName { get; set; }
        public string NroDocumento { get; set; }

        public byte[] FirmaTrabajador { get; set; }
        public byte[] HuellaTrabajador { get; set; }     
        public byte[] FirmaTecnologo { get; set; }
        public byte[] FirmaMedico { get; set; }
        public int? i_SoloAnio { get; set; }

        public string TotalMeses { get; set; }

        public byte[] FirmaAuditor { get; set; }
        public byte[] b_Logo_Cliente { get; set; }

        public string TiempoTotalLaboral { get; set; }
    }
}
