using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class UcOtoscopiacs
    {
        public string Trabajador { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public DateTime? FechaEvaluacion { get; set; }
        public string Puesto { get; set; }
        public string ServicioId { get; set; }
        public byte[] FirmaUsuarioGraba { get; set; }
        public string NombreUsuarioGraba { get; set; }
        public byte[] HuellaTrabajador { get; set; }
        public byte[] FirmaTrabajador { get; set; }
        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }


        public string OTOSCOPIA_RUIDO { get; set; }
        public string OTOSCOPIA_QUIMICO { get; set; }
        public string OTOSCOPIA_DEPORTE { get; set; }
        public string OTOSCOPIA_RUIDO_EXCE { get; set; }
        public string OTOSCOPIA_MUSICA { get; set; }
        public string OTOSCOPIA_OTOXICOS { get; set; }
        public string OTOSCOPIA_MANIPULACION { get; set; }
        public string OTOSCOPIA_OTOLOGICOS { get; set; }
        public string OTOSCOPIA_ZUMBIDOS { get; set; }
        public string OTOSCOPIA_SECRECION { get; set; }
        public string OTOSCOPIA_MAREOS { get; set; }
        public string OTOSCOPIA_OTALGIA { get; set; }
        public string OTOSCOPIA_DISMINUCION { get; set; }
        public string OTOSCOPIA_TRACTO { get; set; }
        public string OTOSCOPIA_OTROS { get; set; }
        public string OTOSCOPIA_OD_1 { get; set; }

        public string OTOSCOPIA_OD_2 { get; set; }
        public string OTOSCOPIA_OD_3 { get; set; }
        public string OTOSCOPIA_OD_4 { get; set; }
        public string OTOSCOPIA_OI_1 { get; set; }
        public string OTOSCOPIA_OI_2 { get; set; }
        public string OTOSCOPIA_OI_3 { get; set; }
        public string OTOSCOPIA_OI_4 { get; set; }
        public string OTOSCOPIA_OD_DESC { get; set; }
        public string OTOSCOPIA_OI_DESC { get; set; }

        public string TipoEso { get; set; }
        public string Dni { get; set; }
        public string EmpresaCliente { get; set; }
    }
}
