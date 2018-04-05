using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportEvaGinecologico
    {
        public string Ficha { get; set; }
        public string Historia { get; set; }
        public string NombreTrabajador { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Seguro { get; set; }
        public string EmpresaCliente { get; set; }
        public string EmpresaEmpleadora { get; set; }
        public string CentroMedico { get; set; }
        public string Medico { get; set; }
        public DateTime FechaAtencion { get; set; }
        public DateTime Fum { get; set; }
        public string Gestapara { get; set; }
        public string FechaPAP { get; set; }
        public string MAC { get; set; }
        public string Menarquia { get; set; }
        public string FechaMamografia { get; set; }
        public string RegimenCatamenial { get; set; }
        public string CirugiaGinecologica { get; set; }
        public string Leucorrea { get; set; }
        public string LeucorreaDescripcion { get; set; }
        public string Dipareunia { get; set; }
        public string DipareuniaDescripcion { get; set; }
        public string Incontinencia { get; set; }
        public string IncontinenciaDescripcion { get; set; }
        public string Otros { get; set; }
        public string OtrosDescripcion { get; set; }
        public string EvaluacionGinecologica { get; set; }
        public string ExamenMama { get; set; }
        public string ResultadoPAP { get; set; }
        public string ResultadoMamografia { get; set; }
        
        public byte[] FirmaDoctor { get; set; }
        public byte[] FotoPaciente { get; set; }
        public byte[] Logo { get; set; }
        public string ResultadoMama { get; set; }

        public string DiagnosticosMama { get; set; }
        public string DiagnosticosEvaGinecologica { get; set; }
        public string DiagnosticosEcografiaMama { get; set; }
        public string DiagnosticosPapanicolao { get; set; }
        public string DiagnosticosResultadoMamografia { get; set; }

        public string AntecedentesPersonales { get; set; }
        public string AntecendentesFamiliares { get; set; }

        public string Diagnosticos { get; set; }
        public string Recomendaciones { get; set; }
        public string v_DiagnosticRepositoryId { get; set; }
        public string v_ComponentId { get; set; }

        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }
    }
}
