using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class ReportOdontograma_CI
    {
        public string EmpresaCliente { get; set; }
        public string Ficha { get; set; }
        public string Hc { get; set; }
        public string NombreTrabajador { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        public byte[] FirmaMedico { get; set; }
        public byte[] FirmaTrabajador { get; set; }
        public byte[] HuellaTrabajador { get; set; }
        public string NombreUsuarioGraba { get; set; }

        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }

        public string Antecedentes { get; set; }
        public string SINTOMAS { get; set; }
        public string NRODIENTESCONCARIES { get; set; }
        public string NRODIENTESAUSENTES { get; set; }
        public string NRODIENTESCONCURADOS { get; set; }
        public string SINCARIES { get; set; }

        public string PuestoTrabajo { get; set; }
        public string DxExamen { get; set; }
        public string Recomendaciones { get; set; }
    }
}
