//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/12/27 - 12:11:13
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    [DataContract()]
    public partial class medicoDto
    {
        [DataMember()]
        public String v_MedicoId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_MasterServiceId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_SystemUserId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_MasterServiceTypeId { get; set; }

        [DataMember()]
        public Nullable<Decimal> r_Clinica { get; set; }

        [DataMember()]
        public Nullable<Decimal> r_Medico { get; set; }

        [DataMember()]
        public Nullable<Int32> i_CategoryId { get; set; }

        [DataMember()]
        public Nullable<Decimal> r_Price { get; set; }

        [DataMember()]
        public String v_EmployerOrganizationId { get; set; }

        [DataMember()]
        public String v_CustomerOrganizationId { get; set; }

        [DataMember()]
        public String v_WorkingOrganizationId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_IsDeleted { get; set; }

        [DataMember()]
        public Nullable<Int32> i_InsertUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_InsertDate { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UpdateUserId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_UpdateDate { get; set; }

        public medicoDto()
        {
        }

        public medicoDto(String v_MedicoId, Nullable<Int32> i_MasterServiceId, Nullable<Int32> i_SystemUserId, Nullable<Int32> i_MasterServiceTypeId, Nullable<Decimal> r_Clinica, Nullable<Decimal> r_Medico, Nullable<Int32> i_CategoryId, Nullable<Decimal> r_Price, String v_EmployerOrganizationId, String v_CustomerOrganizationId, String v_WorkingOrganizationId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate)
        {
			this.v_MedicoId = v_MedicoId;
			this.i_MasterServiceId = i_MasterServiceId;
			this.i_SystemUserId = i_SystemUserId;
			this.i_MasterServiceTypeId = i_MasterServiceTypeId;
			this.r_Clinica = r_Clinica;
			this.r_Medico = r_Medico;
			this.i_CategoryId = i_CategoryId;
			this.r_Price = r_Price;
			this.v_EmployerOrganizationId = v_EmployerOrganizationId;
			this.v_CustomerOrganizationId = v_CustomerOrganizationId;
			this.v_WorkingOrganizationId = v_WorkingOrganizationId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
        }
    }
}
