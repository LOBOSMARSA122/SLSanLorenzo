using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ServiceGridJerarquizadaList
    {
        public bool b_FechaEntrega { get; set; }
        public DateTime? d_FechaEntrega { get; set; }
        public string v_ServiceId { get; set; }
        public string v_Pacient { get; set; }
        public string v_PersonId { get; set; }
        public string v_ServiceStatusName { get; set; }
        public int? i_ServiceStatusId { get; set; }
        public string v_AptitudeStatusName {get;set;}
        public string v_OrganizationName { get; set; }
        public string v_LocationName { get; set; }
        public string v_ProtocolId { get; set; }
        public string v_ProtocolName { get; set; }
        public string v_ComponentId { get; set; }
        public int i_LineStatusId { get; set; }
        public string v_DiagnosticRepositoryId { get; set; }
        public string v_DiseasesName { get; set; }
        public DateTime? d_ExpirationDateDiagnostic { get; set; }
        public string v_Recommendation { get; set; }
        public int? i_ServiceId { get; set; }
        public DateTime? d_ServiceDate { get; set; }
        public string v_PacientDocument { get; set; }
        public int? i_ServiceTypeId { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
        public int? i_MasterServiceId { get; set; }
        public int? i_AptitudeStatusId { get; set; }
        public int? i_EsoTypeId { get; set; }
        public int? i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
        public int? i_StatusLiquidation { get; set; }
        public object Liq { get; set; }
        public string v_MasterServiceName { get; set; }
        public string v_EsoTypeName { get; set; }
        public string CIE10 { get; set; }
        public List<DiagnosticRepositoryJerarquizada> Diagnosticos { get; set; }
        public DateTime? d_FechaNacimiento { get; set; }
        public string NroPoliza { get; set; }
        public string Moneda { get; set; }
        public string NroFactura { get; set; }
        public  decimal? Valor { get; set; }
        public int? i_FinalQualificationId { get; set; }
        public string v_Restriccion { get; set; }
        public decimal? d_Deducible { get; set; }
        public int? i_IsDeletedDx { get; set; }
        public byte[] LogoEmpresaPropietaria { get; set; }

        public int? i_IsDeletedRecomendaciones { get; set; }
        public int? i_IsDeletedRestricciones { get; set; }

        public int i_age { get; set; }
        public DateTime? d_BirthDate { get; set; }

        public string UsuarioMedicina { get; set; }

        public string CompMinera { get; set; }
        public string Tercero{ get; set; }
        public int item { get; set; }
    }
}
