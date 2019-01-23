//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/01/23 - 17:47:27
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
    public partial class cie10Dto
    {
        [DataMember()]
        public String v_CIE10Id { get; set; }

        [DataMember()]
        public String v_CIE10Description1 { get; set; }

        [DataMember()]
        public String v_CIE10Description2 { get; set; }

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

        [DataMember()]
        public List<diseasesDto> diseases { get; set; }

        [DataMember()]
        public List<dxfrecuenteDto> dxfrecuente { get; set; }

        public cie10Dto()
        {
        }

        public cie10Dto(String v_CIE10Id, String v_CIE10Description1, String v_CIE10Description2, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, List<diseasesDto> diseases, List<dxfrecuenteDto> dxfrecuente)
        {
			this.v_CIE10Id = v_CIE10Id;
			this.v_CIE10Description1 = v_CIE10Description1;
			this.v_CIE10Description2 = v_CIE10Description2;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.diseases = diseases;
			this.dxfrecuente = dxfrecuente;
        }
    }
}
