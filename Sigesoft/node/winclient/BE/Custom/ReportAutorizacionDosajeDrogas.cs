using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
   public class ReportAutorizacionDosajeDrogas
    {
       public byte[] LogoEmpresa { get; set; }
       public string Fecha{ get; set; }

        public DateTime? FechaSinformatear{ get; set; }

        public string Trabajador{ get; set; }
        public string Dni { get; set; }
        public string Clinica { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public int Edad{ get; set; }

        public string EmpresaTrabajador{ get; set; }

        public string SufreEnfermedadSiNo{ get; set; }

        public string EnfermedadCual{ get; set; }

        public string ConsumeMedicamentoSiNo{ get; set; }

        public string ConsumeMedicamentoCual{ get; set; }

        public string TomaCocaSiNo{ get; set; }

        public string TomaCocaCuantas{ get; set; }

        public string ConsumioUltimo{ get; set; }

        public string ConsumeProductoCocaSiNo { get; set; }

        public string ConsumeProductoCocaCuantas{ get; set; }

        public string ConsumeProductoCocaUltima{ get; set; }

        public string TratamientoAnestesiaSiNo{ get; set; }

        public byte[] FirmaMedico { get; set; }

        public byte[] FirmaTrabajador { get; set; }

        public byte[] HuellaTrabajador { get; set; }

        public string Puesto { get; set; }

        public string Empresa { get; set; }

        public string Metodo { get; set; }
        public string Muestra { get; set; }
        public string Cocaina { get; set; }
        public string Marihuana { get; set; }

        public string Resultado { get; set; }

        public string NombreUsuarioGraba { get; set; }
    }
}
