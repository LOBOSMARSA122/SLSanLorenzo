//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/05/30 - 10:15:14
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
    public partial class diseasesDto
    {
        [DataMember()]
        public String v_DiseasesId { get; set; }

        [DataMember()]
        public String v_CIE10Id { get; set; }

        [DataMember()]
        public String v_Name { get; set; }

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
        public String v_ComentaryUpdate { get; set; }

        [DataMember()]
        public List<componentfieldvaluesDto> componentfieldvalues { get; set; }

        [DataMember()]
        public List<diagnosticrepositoryDto> diagnosticrepository { get; set; }

        [DataMember()]
        public cie10Dto cie10 { get; set; }

        [DataMember()]
        public List<dxfrecuenteDto> dxfrecuente { get; set; }

        [DataMember()]
        public List<familymedicalantecedentsDto> familymedicalantecedents { get; set; }

        [DataMember()]
        public List<personmedicalhistoryDto> personmedicalhistory { get; set; }

        public diseasesDto()
        {
        }

        public diseasesDto(String v_DiseasesId, String v_CIE10Id, String v_Name, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, String v_ComentaryUpdate, List<componentfieldvaluesDto> componentfieldvalues, List<diagnosticrepositoryDto> diagnosticrepository, cie10Dto cie10, List<dxfrecuenteDto> dxfrecuente, List<familymedicalantecedentsDto> familymedicalantecedents, List<personmedicalhistoryDto> personmedicalhistory)
        {
			this.v_DiseasesId = v_DiseasesId;
			this.v_CIE10Id = v_CIE10Id;
			this.v_Name = v_Name;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.v_ComentaryUpdate = v_ComentaryUpdate;
			this.componentfieldvalues = componentfieldvalues;
			this.diagnosticrepository = diagnosticrepository;
			this.cie10 = cie10;
			this.dxfrecuente = dxfrecuente;
			this.familymedicalantecedents = familymedicalantecedents;
			this.personmedicalhistory = personmedicalhistory;
        }
    }
}
