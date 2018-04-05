using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportRespiratorio
    {
        public string IdServicio { get; set; }
        public string IdServiciComponent { get; set; }
        public string TipoEso { get; set; }
        public DateTime? FechaEvaluacion { get; set; }
        public string DireccionTrabajador { get; set; }
        public string Trabajador { get; set; }
        public string DNI { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Genero { get; set; }
        public string Puesto { get; set; }
        public string NombreUsuarioGraba { get; set; }
        public byte[] FirmaUsuarioGraba { get; set; }

        public byte[] FirmaTrabajador { get; set; }
        public byte[] HuellaTrabajador { get; set; }

        public  string TUBERCULOSIS1 {get;set;}// "N009-MF000002015";
        public  string TOS_POR_MAS_15_DIAS {get;set;}// "N009-MF000002016";
        public  string BAJO_PESO {get;set;}// "N009-MF000002017";
        public  string SUDORACION_NOCTURNA {get;set;}// "N009-MF000002018";
        public  string EXPECTORACION_CON_SANGRE {get;set;}// "N009-MF000002019";
        public  string FAMILIARES_AMIGOS {get;set;}// "N009-MF000002020";
        public  string SOSPECHA_TUBERCULOSIS {get;set;}// "N009-MF000002021";
        public  string OBSERVACIONES {get;set;}// "N009-MF000002022";
        public  string ES_SINTOMATICO_RESPIRATORIO {get;set;}// "N009-MF000002023";
        public  string RESULTADOS_BK_1 {get;set;}// "N009-MF000002024";
        public  string RESULTADOS_BK_2 {get;set;}// "N009-MF000002025";
        public  string RADIOGRAFIA_TORAX {get;set;}// "N009-MF000002026";

        

     
    }
}
