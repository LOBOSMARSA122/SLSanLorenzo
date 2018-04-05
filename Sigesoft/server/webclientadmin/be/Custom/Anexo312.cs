using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class Anexo312
    {
        public string PersonId { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? TipoDocumentoId { get; set; }
        public string NroDocumento { get; set; }
        public string DireccionFiscal { get; set; }
        public int? DepartamentoId { get; set; }
        public int? ProvinciaId { get; set; }
        public int? DistritoId { get; set; }
        public int? ResideLugarTrabajo { get; set; }
        public string TiempoResidencia { get; set; }
        public string Telefono { get; set; }
        public int? TipoSeguroId { get; set; }
        public string Email { get; set; }
        public int? EstadoCivilId { get; set; }
        public int? GradoInstruccionId { get; set; }
        public int? HijosVivos { get; set; }
        public int? HijosMuertos { get; set; }
        public string Anamnesis { get; set; }

        public string v_Menarquia { get; set; }
        public string v_Gestapara { get; set; }
        public string v_FechaUltimaRegla { get; set; }
        public int? i_MacId { get; set; }
        public string v_CatemenialRegime { get; set; }
        public string v_CiruGine { get; set; }
        public string v_FechaUltimoPAP { get; set; }
        public string v_ResultadosPAP { get; set; }
        public string v_FechaUltimaMamo { get; set; }
        public string v_ResultadoMamo { get; set; }
        public int? i_StatusAptitud { get; set; }
        public string Comentario { get; set; }
        public string ComentarioMedico { get; set; }
        public string Restricciones { get; set; }
        public string EmpresaCliente { get; set; }
        public string RucEmpresaCliente { get; set; }
        public string ActividadEconomicaEmpresaCliente { get; set; }
        public string PuestoTrabajo { get; set; }
        public string Trabajador { get; set; }
        public int? i_GeneroId { get; set; }
        public string LugarTrabajo { get; set; }

        public string v_BirthPlace { get; set; }
        
        public string v_ExploitedMineral { get; set; }
        public int? i_AltitudeWorkId { get; set; }
        public int? i_PlaceWorkId { get; set; }
        public List<HistoryList> ListaHistoriaOcupacional { get; set; }
        public List<PersonMedicalHistoryList> ListaMedicosPersonales { get; set; }
        public List<NoxiousHabitsList> ListaHabitosNosivos { get; set; }
        public List<FamilyMedicalAntecedentsList> ListaMedicosFamiliares { get; set; }
    }
}
