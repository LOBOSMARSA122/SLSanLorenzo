//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/07/30 - 11:06:27
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
    public partial class servicecomponentfieldvaluesmultimediaDto
    {
        [DataMember()]
        public String v_ServiceComponentFieldValuesMultimediaId { get; set; }

        [DataMember()]
        public String v_MultimediaFileId { get; set; }

        [DataMember()]
        public String v_ServiceComponentFieldValuesId { get; set; }

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
        public multimediafileDto multimediafile { get; set; }

        [DataMember()]
        public servicecomponentfieldvaluesDto servicecomponentfieldvalues { get; set; }

        public servicecomponentfieldvaluesmultimediaDto()
        {
        }

        public servicecomponentfieldvaluesmultimediaDto(String v_ServiceComponentFieldValuesMultimediaId, String v_MultimediaFileId, String v_ServiceComponentFieldValuesId, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, multimediafileDto multimediafile, servicecomponentfieldvaluesDto servicecomponentfieldvalues)
        {
			this.v_ServiceComponentFieldValuesMultimediaId = v_ServiceComponentFieldValuesMultimediaId;
			this.v_MultimediaFileId = v_MultimediaFileId;
			this.v_ServiceComponentFieldValuesId = v_ServiceComponentFieldValuesId;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.multimediafile = multimediafile;
			this.servicecomponentfieldvalues = servicecomponentfieldvalues;
        }
    }
}
