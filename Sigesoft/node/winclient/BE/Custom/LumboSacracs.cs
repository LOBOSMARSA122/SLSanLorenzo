using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class LumboSacracs
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

        public string LUMBOSACRA_1 { get; set; }
        public string LUMBOSACRA_2 { get; set; }
        public string LUMBOSACRA_3 { get; set; }
        public string LUMBOSACRA_4 { get; set; }
        public string LUMBOSACRA_5 { get; set; }
        public string LUMBOSACRA_6 { get; set; }
        public string LUMBOSACRA_7 { get; set; }
        public string LUMBOSACRA_8 { get; set; }
        public string TipoEso { get; set; }
        public string Dni { get; set; }
        public string EmpresaCliente { get; set; }
    }
}
