//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/03/12 - 15:04:56
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
    public partial class problemaDto
    {
        [DataMember()]
        public String v_ProblemaId { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Tipo { get; set; }

        [DataMember()]
        public String v_PersonId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_Fecha { get; set; }

        [DataMember()]
        public String v_Descripcion { get; set; }

        [DataMember()]
        public Nullable<Int32> i_EsControlado { get; set; }

        [DataMember()]
        public String v_Observacion { get; set; }

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

        public problemaDto()
        {
        }

        public problemaDto(String v_ProblemaId, Nullable<Int32> i_Tipo, String v_PersonId, Nullable<DateTime> d_Fecha, String v_Descripcion, Nullable<Int32> i_EsControlado, String v_Observacion, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate)
        {
			this.v_ProblemaId = v_ProblemaId;
			this.i_Tipo = i_Tipo;
			this.v_PersonId = v_PersonId;
			this.d_Fecha = d_Fecha;
			this.v_Descripcion = v_Descripcion;
			this.i_EsControlado = i_EsControlado;
			this.v_Observacion = v_Observacion;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
        }
    }
}
