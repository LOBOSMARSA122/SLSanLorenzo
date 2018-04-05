using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportOstocopia
    {
        public string IdServicio { get; set; }
        public string IdServiciComponent { get; set; }
        public string TipoEso { get; set; }
        public DateTime FechaEvaluacion { get; set; }
        public string Trabajador { get; set; }
        public string DNI { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Genero { get; set; }
        public string Puesto { get; set; }
        public string NombreUsuarioGraba { get; set; }
        public byte[] FirmaUsuarioGraba { get; set; }

        public  string ANTECEDENTES_OTOLOGICOS {get;set;}// "N009-MF000002003";
        public  string ZUMBIDOS {get;set;}// "N009-MF000002004";
        public  string SECRECION_DEL_OIDO {get;set;}// "N009-MF000002005";
        public  string MAREOS {get;set;}// "N009-MF000002006";
        public  string OTALGIA {get;set;}// "N009-MF000002007";
        public  string DISMIUCION_DE_AUDICION {get;set;}// "N009-MF000002008";
        public  string OTROS {get;set;}// "N009-MF000002009";
        public  string ENF_TRACTO_RASPIRATORIO_ACTUAL {get;set;}// "N009-MF000002010";
        public  string MEMBRAMA_TIMPANICA_OIDO_DERECHO {get;set;}// "N009-MF000002011";
        public  string CONDUCTO_AUDITIVO_OIDO_DERECHO {get;set;}// "N009-MF000002012";
        public  string MEMBRANA_TIMPANICA_OIDO_IZQUIERDO {get;set;}// "N009-MF000002013";
        public  string CONDUCTO_AUDITIVO_OIDO_IZQUIERDO {get;set;}// "N009-MF000002014";
    }
}
