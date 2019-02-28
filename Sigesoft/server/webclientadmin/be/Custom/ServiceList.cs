using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class ServiceList
    {
        public string v_ServiceId { get; set; }
        public string v_Trabajador { get; set; }
        public string v_IdTrabajador { get; set; }
        public DateTime d_ServiceDate { get; set; }
        public string v_AptitudeStatusName { get; set; }
        public string v_ProtocolName { get; set; }

        public int? i_TypeEsoId { get; set; }
        public int i_AptitudeId { get; set; }
        public string v_ProtocolId { get; set; }
        public string v_HCL { get; set; }
        public string EmpresaCliente { get; set; }

        public string EstadoCola { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }

        public string v_Restricction { get; set; }
        public string v_PersonId { get; set; }
        public int i_AptitudeStatusId { get; set; }
        public string v_Pacient { get; set; }
        public DateTime? d_FechaNacimiento { get; set; }
        public string v_Genero { get; set; }
        public string v_TipoEso { get; set; }
        public string v_GrupoRiesgo { get; set; }
        public string v_Puesto { get; set; }
        public string v_ComponentId { get; set; }
        public string v_ObsStatusService { get; set; }
        public string Dni { get; set; }
        public int i_CategoryId { get; set; }
        public int i_IsRequiredId { get; set; }
        public int i_EsoTypeId { get; set; }

        public string v_ExploitedMineral { get; set; }
        public int? i_AltitudeWorkId { get; set; }
        public int? i_PlaceWorkId { get; set; }
        public string AreaEmpresa { get; set; }
        public string v_SectorName { get; set; }
        public int? i_StatusLiquidation { get; set; }

        public string ComentarioAptitud { get; set; }
        public int? i_ServiceComponentStatusId { get; set; }
        public bool AtSchool { get; set; }
        public int? i_SendToTracking { get; set; }
        public string Apellidos { get; set; }
        public int? i_SystemUserEspecialistaId { get; set; }
    }
}
