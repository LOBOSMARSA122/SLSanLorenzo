using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportInformeEcograficoProstata
    {
        public string Nombre_Trabajador { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }

        public string INFORME_ECOGRAFICO_PROSTATA_MOTIVO_EXAMEN { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_RECEPCION { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_PAREDES1 { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_PAREDES2 { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_CONT_ANECOICO { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_IAMGENES_EXPANSIVAS { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_CALCULOS_INTERIOR { get; set; }

        public string INFORME_ECOGRAFICO_PROSTATA_TAMAÑO { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_VOL_PREMICCIONAL { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_VOL_POSMICCIONAL { get; set; }
        public  string INFORME_ECOGRAFICO_PROSTATA_RETECION { get; set; }

        public string INFORME_ECOGRAFICO_PROSTATA_BORDES { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_DIAMETRO_TRANSVERSO { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_ANTERO { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_DIAMETRO_LONGITUDINAL { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_VOLUMEN { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_VOLUMEN_VN { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_ECOESTRUCTURA { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_DESCRIPCION_OTROS { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_NINGUNA { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_OBSERVACIONES { get; set; }
        public string INFORME_ECOGRAFICO_PROSTATA_CONCLUSIONES { get; set; }

        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }
        public string Conclusiones { get; set; }

        public string NombreUsuarioGraba { get; set; }
    }
}
