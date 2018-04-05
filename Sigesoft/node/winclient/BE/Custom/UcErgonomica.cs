using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class UcErgonomica
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


        public string EVA_ERGONOMICA_HOMBORS { get; set; }
        public string EVA_ERGONOMICA_CUELLO { get; set; }
        public string EVA_ERGONOMICA_ESPALDA { get; set; }
        public string EVA_ERGONOMICA_RODILLAS { get; set; }
        public string EVA_ERGONOMICA_RODILLAS_2 { get; set; }
        public string EVA_ERGONOMICA_BRAZO_MUNE { get; set; }
        public string EVA_ERGONOMICA_BRAZO_MUNE_2 { get; set; }
        public string EVA_ERGONOMICA_MANOS { get; set; }
        public string EVA_ERGONOMICA_RODILLAS_3 { get; set; }
        public string EVA_ERGONOMICA_CUELLOS_HOMB { get; set; }
        public string EVA_ERGONOMICA_CUELLOS_HOMB_2 { get; set; }
        public string EVA_ERGONOMICA_ZONA_LUMBAR { get; set; }
        public string EVA_ERGONOMICA_ZONA_LUMBAR_2 { get; set; }
        public string EVA_ERGONOMICA_MANOS_BRAZOS { get; set; }
        public string EVA_ERGONOMICA_MANOS_BRAZOS_2 { get; set; }
        public string EVA_ERGONOMICA_CONCLUSIONES { get; set; }
        public string Dx { get; set; }
        public string Recomendaciones { get; set; }
        public string Dni { get; set; }

        public string TipoEso { get; set; }
        public string EmpresaCliente { get; set; }
    }
}
