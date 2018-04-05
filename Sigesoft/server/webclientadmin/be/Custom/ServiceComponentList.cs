using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.BE
{
     public class ServiceComponentList
    {
        public string v_ServiceComponentId { get; set; }
        public string v_ServiceId { get; set; }
        public string v_ComponentId { get; set; }
        public string v_ComponentName { get; set; }
        public int? i_ServiceComponentStatusId { get; set; }
        public string v_ServiceComponentStatusName { get; set; }
        public int? i_ExternalInternalId { get; set; }
        public int? i_ServiceComponentTypeId { get; set; }
        public int? i_IsVisibleId { get; set; }
        public int? i_IsInheritedId { get; set; }
        public DateTime? d_StartDate { get; set; }
        public DateTime? d_EndDate { get; set; }
        public int i_index { get; set; }
        public Single? r_Price { get; set; }
        public int? i_IsInvoicedId { get; set; }
        public int? i_IsRequiredId { get; set; }
        public int? i_IsManuallyAddedId { get; set; }
        public int? i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public int i_QueueStatusId { get; set; }
        public string v_QueueStatusName { get; set; }

        public string v_Comment { get; set; }

        //public List<ServiceComponentFieldsList> ServiceComponentFields { get; set; }

        public int ServiceStatusId { get; set; }
        public string v_Motive { get; set; }
        public int? i_ControlId { get; set; }

        public int i_CategoryId { get; set; }
        public string v_CategoryName { get; set; }

        public int? i_IsApprovedId { get; set; }

        public List<DiagnosticRepositoryList> DiagnosticRepository { get; set; }

        public string v_InternalGroup { get; set; }
        public string v_InternalCode { get; set; }

        public int? i_StatusLiquidation { get; set; }

        public byte[] FirmaMedico { get; set; }
        public int Orden { get; set; }
        public bool AtSchool { get; set; }
         
    }
}
