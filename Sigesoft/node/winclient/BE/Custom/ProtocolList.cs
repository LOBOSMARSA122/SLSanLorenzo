using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ProtocolList
    {
        public bool select { get; set; }
        public string v_ProtocolId { get; set; }
        public string v_Protocol { get; set; }
        public string v_Name { get; set; }
        public string v_Organization { get; set; }     
        public string v_Location { get; set; }     
        public string v_EsoType { get; set; }
        public string v_GroupOccupation { get; set; }
        public string v_OrganizationInvoice { get; set; }     
        public string v_CostCenter { get; set; }
        public string v_IntermediaryOrganization { get; set; }
        public string v_MasterServiceName { get; set; }
        public string v_Ges { get; set; }
        public string v_Occupation { get; set; }

        public string v_OrganizationId { get; set; }
        public int? i_EsoTypeId { get; set; }
        public string v_GroupOccupationId { get; set; }
        public string v_WorkingOrganizationId { get; set; }
        public string v_OrganizationInvoiceId { get; set; }

        public string v_LocationId { get; set; }
        public string v_WorkingLocationId { get; set; }
        public string v_CustomerLocationId { get; set; }

        public int i_MasterServiceId { get; set; }
        public int? i_ServiceTypeId { get; set; }

        public int? i_IsActive { get; set; }
        
        public int i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public string v_ContacName {get;set;}
        public string v_Address { get; set; }

        public int? Comision { get; set; }

        public string v_SectorTypeName { get; set; }
        public string v_OrganizationAddress { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_NombreVendedor { get; set; }
        public string v_ComponenteNombre { get; set; }
        public string AseguradoraId { get; set; }
        public int i_RecordType { get; set; }
        public int i_RecordStatus  { get; set; }
    }
}
