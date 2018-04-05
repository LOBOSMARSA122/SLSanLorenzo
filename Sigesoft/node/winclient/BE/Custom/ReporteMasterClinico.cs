using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReporteMasterClinico
    {
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string IdServicio { get; set; }
        public string IdProtocolId { get; set; }
        public string IdTrabajador { get; set; }

        public string Estatus { get; set; }
        public string Trabajador { get; set; }
        public int EdadTrabajador { get; set; }
        public string DNI { get; set; }
        public string v_CustomerOrganizationName { get; set; }
        public string PuestoTrabajo { get; set; }
        public string LugarExamen { get; set; }
        public string TipoEmo { get; set; }
        public string FechaEmision { get; set; }
        public DateTime? FechaServicioDate { get; set; }
        public DateTime FechaVencimientoEmo { get; set; }
        public string GeneroTrabajador { get; set; }
        public string Talla { get; set; }
        public string Peso { get; set; }
        public string IMC { get; set; }
        public string GrupoSanguineo { get; set; }
        public string FactorSanguineo { get; set; }
        public string Aptitud { get; set; }
        public string Hallazgo1 { get; set; }
        public string Hallazgo2 { get; set; }
        public string Hallazgo3 { get; set; }
        public string Hallazgo4 { get; set; }
        public string Hallazgo5 { get; set; }
        public string Hallazgo6 { get; set; }
        public string Hallazgo7 { get; set; }
        public string Hallazgo8 { get; set; }
        public string ObsHiperArterial { get; set; }
        public string ObsObesidad { get; set; }
        public string ObsDislipidemina { get; set; }
        public string ObsTbc { get; set; }
        public string ObsHipoacusia { get; set; }
        public string ObsPolicitemia { get; set; }
        public string ObsDiabetes { get; set; }

        public string Restriccion1 { get; set; }
        public string Restriccion2 { get; set; }
        public string Restriccion3 { get; set; }
        public string Restriccion4 { get; set; }
        public string Restriccion5 { get; set; }
        public string Restriccion6 { get; set; }
        public string Restriccion7 { get; set; }
        public string Restriccion8 { get; set; }
        public string Restriccion9 { get; set; }

        public string Recomendacion1 { get; set; }
        public string Recomendacion2 { get; set; }
        public string Recomendacion3 { get; set; }
        public string Recomendacion4 { get; set; }
        public string Recomendacion5 { get; set; }
        public string Recomendacion6 { get; set; }
        public string Recomendacion7 { get; set; }
        public string Recomendacion8 { get; set; }
        public string Recomendacion9 { get; set; }
        public string Recomendacion10 { get; set; }
        public string Recomendacion11 { get; set; }
        public string Recomendacion12 { get; set; }
        public string Recomendacion13 { get; set; }
        public string Recomendacion14 { get; set; }

        public string ElectroEncefalograma { get; set; }
        public string PruebaEsfuerzo { get; set; }
        public string ExamenNeurologico { get; set; }
        public string RAC1 { get; set; }
        public string RAC2 { get; set; }
        public string RAC3 { get; set; }
        public string RAC4 { get; set; }
        public string RAC5 { get; set; }
        public string RAC6 { get; set; }
        public string RAC7 { get; set; }
        public string RAC8 { get; set; }
        public string RAC9 { get; set; }
        public string RAC10 { get; set; }
        public string RAC11 { get; set; }

        public string RACPQ { get; set; }


        public string Tetano1 { get; set; }
        public string Tetano2 { get; set; }
        public string Tetano3 { get; set; }

        public string HepB1 { get; set; }
        public string HepB2 { get; set; }
        public string HepB3 { get; set; }

        public string HepA1 { get; set; }
        public string HepA2 { get; set; }

        public string InfluenzaEstacional { get; set; }
        public string InfluenzaH1N1 { get; set; }

        public string Antiamarilica { get; set; }
        public string Inteligencia { get; set; }
        public string Personalidad { get; set; }

        public string RiesgoPsico { get; set; }//Campo en blanco
        public string Estres { get; set; }//Campo en blanco
        public string Ansiedad { get; set; }//Campo en blanco

        public string RiesgoFisico { get; set; }
        public string RiesgoQuimico { get; set; }
        public string RiesgoBiologico{ get; set; }
        public string RiesgoErgonomico { get; set; }
        public string RiesgoPsicolaboral { get; set; }
        public string AptitudParaSeguridad { get; set; }
        public string ObsParaSeguridad { get; set; }

        public string HipertensionArterialControlada { get; set; }
        public string Obesidad1 { get; set; }
        public string DislipidemiaAltoRiesgo { get; set; }
        public string TbcTratamiento { get; set; }
        public string HipoacusiaInducida { get; set; }
        public string Policitemia { get; set; }
        public string DiabetesMellitusControlada { get; set; }
        public string ResultadoDosajePlomo { get; set; }
        public string ExamenPsicosensometrico { get; set; }

    }
}
