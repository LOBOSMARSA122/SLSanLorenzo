using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportInformeEcograficoAbdominal
    {

        public string Nombre_Trabajador { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaServicio { get; set; }
        public int Edad { get; set; }


        //public string ECOGRAFIA_ABDOMINAL_ID { get; set; }
        public string ECOGRAFIA_ABDOMINAL_MOTIVO_EXAMEN { get; set; }
        public string ECOGRAFIA_ABDOMINAL_MORFOLOGIA_MOVILIDAD { get; set; }
        public string ECOGRAFIA_ABDOMINAL_BORDES { get; set; }
        public string ECOGRAFIA_ABDOMINAL_MORFOLOGIA_MOVILIDAD_DESCRIPCION { get; set; }
        public string ECOGRAFIA_ABDOMINAL_BORDES_DESCRIPCION { get; set; }
        public string ECOGRAFIA_ABDOMINAL_DIMENSIONES { get; set; }
        public string ECOGRAFIA_ABDOMINAL_PAREMQUIMA { get; set; }
        public string ECOGRAFIA_ABDOMINAL_DIMENSIONES_DESCRIPCION { get; set; }
        public string ECOGRAFIA_ABDOMINAL_ECOGENICIDA { get; set; }
        public string ECOGRAFIA_ABDOMINAL_IMAGENES_EXPANSIVAS { get; set; }
        public string ECOGRAFIA_ABDOMINAL_DILATACION_VIAS_BILIARES { get; set; }
        public string ECOGRAFIA_ABDOMINAL_DIAMETRO_COLEDOCO { get; set; }
        public string ECOGRAFIA_ABDOMINAL_FORMA { get; set; }
        public string ECOGRAFIA_ABDOMINAL_FORMA_DESCRIPCION { get; set; }
        public string ECOGRAFIA_ABDOMINAL_PAREDES1  { get; set; }
        public string ECOGRAFIA_ABDOMINAL_PAREDES2 { get; set; }
        public string ECOGRAFIA_ABDOMINAL_CONT_ANECOICO { get; set; }
        public string ECOGRAFIA_ABDOMINAL_BARRO_BILIAR { get; set; }
        public string ECOGRAFIA_ABDOMINAL_CALCULOS_INTERIOR { get; set; }
        public string ECOGRAFIA_ABDOMINAL_CALCULOS_TAMAÑO{ get; set; }
        public string ECOGRAFIA_ABDOMINAL_DIAMETRO_TRANSVERSO { get; set; }
        public string ECOGRAFIA_ABDOMINAL_DIAMETRO_LOGITUDINAL { get; set; }
        public string ECOGRAFIA_ABDOMINAL_PANCREAS_MORFOLOGIA_MOVILIDAD { get; set; }
        public string ECOGRAFIA_ABDOMINAL_PANCREAS_MEDIDAS { get; set; }
        public string ECOGRAFIA_ABDOMINAL_PANCREAS_MEDIDAS_DESCRIPCION { get; set; }
        public string ECOGRAFIA_ABDOMINAL_CABEZA { get; set; }
        public string ECOGRAFIA_ABDOMINAL_CUELLO { get; set; }
        public string ECOGRAFIA_ABDOMINAL_CUERNO { get; set; }
        public string ECOGRAFIA_ABDOMINAL_COLA { get; set; }
        public string ECOGRAFIA_ABDOMINAL_MEDIDAD_NO_EVALUABLES { get; set; }
        public string ECOGRAFIA_ABDOMINAL_ANORMAL { get; set; }
        public string ECOGRAFIA_ABDOMINAL_DIAMETRO_ANTOPOSTERIOR { get; set; }
        public string ECOGRAFIA_ABDOMINAL_LONGUITUD { get; set; }
        public string ECOGRAFIA_ABDOMINAL_CALIBRES_VASOS { get; set; }
        public string ECOGRAFIA_ABDOMINAL_CALIBRES_VASOS_DESCRIPCION { get; set; }
        public string ECOGRAFIA_ABDOMINAL_LIQUIDO_LIBRE_ANDOMINAL { get; set; }
        public string ECOGRAFIA_ABDOMINAL_NINGUNA { get; set; }
        public string ECOGRAFIA_ABDOMINAL_OBSERVACIONES { get; set; }
        public string ECOGRAFIA_ABDOMINAL_CONCLUSIONES { get; set; }
        public string ECOGRAFIA_ABDOMINAL_PAREMQUIMA_DESCRIPCION { get; set; }
        public string ECOGRAFIA_ABDOMINAL_LIQUIDO_LIBRE_ANDOMINAL_DESCRIPCION { get; set; }

        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }
        public string NombreUsuarioGraba { get; set; }
       
    }
}
