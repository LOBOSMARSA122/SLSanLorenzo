using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    public class CalendarList
    {
        public string v_CalendarId { get; set; }
        public string v_PersonId { get; set; }
        public string v_IdTrabajador { get; set; }
        public string v_Trabajador { get; set; }

        public string v_Pacient { get; set; }
        public string v_NumberDocument { get; set; }
        public DateTime? d_DateTimeCalendar { get; set; }
        public string v_ServiceId { get; set; }
        public Int32 i_ServiceTypeId { get; set; }
        public string v_ServiceTypeName { get; set; }
        public Int32 i_CalendarStatusId { get; set; }
        public string v_CalendarStatusName { get; set; }

        public Int32 i_MasterServiceId { get; set; }
        public string v_ServiceName { get; set; }
        public string v_ProtocolId { get; set; }
        public Int32 i_NewContinuationId { get; set; }
        public string v_NewContinuationName { get; set; }
        public Int32 i_LineStatusId { get; set; }
        public string v_LineStatusName { get; set; }
        public Int32 i_IsVipId { get; set; }
        public string v_IsVipName { get; set; }
        public DateTime i_Age { get; set; }

        public string v_OrganizationId { set; get; }
        public string v_OrganizationName { set; get; }
        public int i_ServiceId { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
        public int? i_IsDeleted { get; set; }
        public string v_ServiceStatusName { get; set; }
        public string v_AptitudeStatusName { get; set; }

        public string v_EsoTypeName { get; set; }
        public string v_ProtocolName { get; set; }
        public int i_EsoTypeId { get; set; }
        public string v_ServiceComponentId { get; set; }
        public DateTime d_CircuitStartDate { get; set; }
        public DateTime d_ServiceDate { get; set; }
        public int i_QueueStatusId { get; set; }
        public string v_QueueStatusName { get; set; }
        public DateTime d_Birthdate { get; set; }
        public string v_OrganizationLocationProtocol { set; get; }

        public string v_OrganizationLocationService { set; get; }
        public string v_NameOffice { get; set; }
        public string v_OfficeNumber { get; set; }
        public int i_ServiceStatusId { get; set; }
        public string v_ServiceComponentName { get; set; }
        public int i_Iscalling { get; set; }
        public int i_Gender { get; set; }
        public string v_DocNumber { get; set; }
        public Byte[] b_PersonImage { get; set; }
        public string v_OrganizationIntermediaryName { set; get; }
        public int i_DocTypeId { get; set; }
        public string v_DocTypeName { get; set; }
        public DateTime? d_EntryTimeCM { get; set; }

        public int i_CategoryId { get; set; }
        public string v_CategoryName { get; set; }

        public string v_ComponentId { get; set; }

        // //////

        public string v_CustomerOrganizationName { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }

        public string v_EmployerOrganizationName { get; set; }
        public string v_EmployerOrganizationId { get; set; }
        public string v_EmployerLocationId { get; set; }

        public string v_WorkingOrganizationName { get; set; }
        public string v_WorkingOrganizationId { get; set; }
        public string v_WorkingLocationId { get; set; }
        public object b_IsAttended { get; set; }

        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }

        public int i_AptitudeId { get; set; }
        public int i_TypeEsoId { get; set; }

        public string v_HCL { get; set; }
        public string EmpresaCliente { get; set; }
        public DateTime? d_SalidaCM { get; set; }

        public string Puesto { get; set; }
        public string v_TelephoneNumber { get; set; }

        public int i_Edad { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }


        public string ServicioId { get; set; }
        public string Genero { get; set; }

    }
}
