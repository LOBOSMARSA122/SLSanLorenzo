//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/05/30 - 10:17:03
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
    public partial class tramasDto
    {
        [DataMember()]
        public String v_TramaId { get; set; }

        [DataMember()]
        public String v_TipoRegistro { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_FechaIngreso { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_FechaAlta { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Genero { get; set; }

        [DataMember()]
        public Nullable<Int32> i_GrupoEtario { get; set; }

        [DataMember()]
        public String v_DiseasesName { get; set; }

        [DataMember()]
        public String v_CIE10Id { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UPS { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Procedimiento { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Especialidad { get; set; }

        [DataMember()]
        public Nullable<Int32> i_TipoParto { get; set; }

        [DataMember()]
        public Nullable<Int32> i_TipoNacimiento { get; set; }

        [DataMember()]
        public Nullable<Int32> i_TipoComplicacion { get; set; }

        [DataMember()]
        public Nullable<Int32> i_Programacion { get; set; }

        [DataMember()]
        public Nullable<Int32> i_TipoCirugia { get; set; }

        [DataMember()]
        public Nullable<Int32> i_HorasProg { get; set; }

        [DataMember()]
        public Nullable<Int32> i_HorasEfect { get; set; }

        [DataMember()]
        public Nullable<Int32> i_HorasActo { get; set; }

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

        public tramasDto()
        {
        }

        public tramasDto(String v_TramaId, String v_TipoRegistro, Nullable<DateTime> d_FechaIngreso, Nullable<DateTime> d_FechaAlta, Nullable<Int32> i_Genero, Nullable<Int32> i_GrupoEtario, String v_DiseasesName, String v_CIE10Id, Nullable<Int32> i_UPS, Nullable<Int32> i_Procedimiento, Nullable<Int32> i_Especialidad, Nullable<Int32> i_TipoParto, Nullable<Int32> i_TipoNacimiento, Nullable<Int32> i_TipoComplicacion, Nullable<Int32> i_Programacion, Nullable<Int32> i_TipoCirugia, Nullable<Int32> i_HorasProg, Nullable<Int32> i_HorasEfect, Nullable<Int32> i_HorasActo, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, String v_ComentaryUpdate)
        {
			this.v_TramaId = v_TramaId;
			this.v_TipoRegistro = v_TipoRegistro;
			this.d_FechaIngreso = d_FechaIngreso;
			this.d_FechaAlta = d_FechaAlta;
			this.i_Genero = i_Genero;
			this.i_GrupoEtario = i_GrupoEtario;
			this.v_DiseasesName = v_DiseasesName;
			this.v_CIE10Id = v_CIE10Id;
			this.i_UPS = i_UPS;
			this.i_Procedimiento = i_Procedimiento;
			this.i_Especialidad = i_Especialidad;
			this.i_TipoParto = i_TipoParto;
			this.i_TipoNacimiento = i_TipoNacimiento;
			this.i_TipoComplicacion = i_TipoComplicacion;
			this.i_Programacion = i_Programacion;
			this.i_TipoCirugia = i_TipoCirugia;
			this.i_HorasProg = i_HorasProg;
			this.i_HorasEfect = i_HorasEfect;
			this.i_HorasActo = i_HorasActo;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.v_ComentaryUpdate = v_ComentaryUpdate;
        }
    }
}
