using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class UcOsteo
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

        public string UC_OSTEO_FLEXIBILIDAD { get; set; }
        public string UC_OSTEO_FLEXIBILIDAD_PTJ { get; set; }
        public string UC_OSTEO_FLEXIBILIDAD_OBS { get; set; }
        public string UC_OSTEO_CADERA { get; set; }
        public string UC_OSTEO_CADERA_PTJ { get; set; }
        public string UC_OSTEO_CADERA_OBS { get; set; }
        public string UC_OSTEO_MUSLO { get; set; }
        public string UC_OSTEO_MUSLO_PTJ { get; set; }
        public string UC_OSTEO_MUSLO_OBS { get; set; }
        public string UC_OSTEO_ABDOMEN { get; set; }
        public string UC_OSTEO_ABDOMEN_PTJ { get; set; }
        public string UC_OSTEO_ABDOMEN_OBS { get; set; }
        public string UC_OSTEO_ABD_180 { get; set; }
        public string UC_OSTEO_ABD_180_PTJ { get; set; }
        public string UC_OSTEO_ABD_180_SINO { get; set; }
        public string UC_OSTEO_ABD_60 { get; set; }
        public string UC_OSTEO_ABD_60_PTJ { get; set; }
        public string UC_OSTEO_ABD_60_SINO { get; set; }
        public string UC_OSTEO_ABD_90 { get; set; }
        public string UC_OSTEO_ABD_90_PTJ { get; set; }
        public string UC_OSTEO_ABD_90_SINO { get; set; }

        public string UC_OSTEO_ROTACION { get; set; }
        public string UC_OSTEO_ROTACION_PTJ { get; set; }
        public string UC_OSTEO_ROTACION_SINO { get; set; }
        public string UC_OSTEO_OBS { get; set; }
        public string UC_OSTEO_TOTAL1 { get; set; }
        public string UC_OSTEO_TOTAL2 { get; set; }
        public string TipoEso { get; set; }
        public string Dni { get; set; }
    }
}
