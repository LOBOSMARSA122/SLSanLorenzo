//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2019/05/18 - 11:42:10
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
    public partial class adultomayorDto
    {
        [DataMember()]
        public String v_AdultoMayorId { get; set; }

        [DataMember()]
        public String v_PersonId { get; set; }

        [DataMember()]
        public String v_NombreCuidador { get; set; }

        [DataMember()]
        public String v_EdadCuidador { get; set; }

        [DataMember()]
        public String v_DniCuidador { get; set; }

        [DataMember()]
        public String v_MedicamentoFrecuente { get; set; }

        [DataMember()]
        public String v_InicioRS { get; set; }

        [DataMember()]
        public String v_NroPs { get; set; }

        [DataMember()]
        public String v_FechaUR { get; set; }

        [DataMember()]
        public String v_RC { get; set; }

        [DataMember()]
        public String v_Parto { get; set; }

        [DataMember()]
        public String v_Prematuro { get; set; }

        [DataMember()]
        public String v_Aborto { get; set; }

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
        public String v_ReaccionAlergica { get; set; }

        [DataMember()]
        public String v_DescripciónAntecedentes { get; set; }

        [DataMember()]
        public String v_FlujoVaginal { get; set; }

        [DataMember()]
        public String v_ObservacionesEmbarazo { get; set; }

        [DataMember()]
        public String v_ComentaryUpdate { get; set; }

        [DataMember()]
        public personDto person { get; set; }

        public adultomayorDto()
        {
        }

        public adultomayorDto(String v_AdultoMayorId, String v_PersonId, String v_NombreCuidador, String v_EdadCuidador, String v_DniCuidador, String v_MedicamentoFrecuente, String v_InicioRS, String v_NroPs, String v_FechaUR, String v_RC, String v_Parto, String v_Prematuro, String v_Aborto, Nullable<Int32> i_IsDeleted, Nullable<Int32> i_InsertUserId, Nullable<DateTime> d_InsertDate, Nullable<Int32> i_UpdateUserId, Nullable<DateTime> d_UpdateDate, String v_ReaccionAlergica, String v_DescripciónAntecedentes, String v_FlujoVaginal, String v_ObservacionesEmbarazo, String v_ComentaryUpdate, personDto person)
        {
			this.v_AdultoMayorId = v_AdultoMayorId;
			this.v_PersonId = v_PersonId;
			this.v_NombreCuidador = v_NombreCuidador;
			this.v_EdadCuidador = v_EdadCuidador;
			this.v_DniCuidador = v_DniCuidador;
			this.v_MedicamentoFrecuente = v_MedicamentoFrecuente;
			this.v_InicioRS = v_InicioRS;
			this.v_NroPs = v_NroPs;
			this.v_FechaUR = v_FechaUR;
			this.v_RC = v_RC;
			this.v_Parto = v_Parto;
			this.v_Prematuro = v_Prematuro;
			this.v_Aborto = v_Aborto;
			this.i_IsDeleted = i_IsDeleted;
			this.i_InsertUserId = i_InsertUserId;
			this.d_InsertDate = d_InsertDate;
			this.i_UpdateUserId = i_UpdateUserId;
			this.d_UpdateDate = d_UpdateDate;
			this.v_ReaccionAlergica = v_ReaccionAlergica;
			this.v_DescripciónAntecedentes = v_DescripciónAntecedentes;
			this.v_FlujoVaginal = v_FlujoVaginal;
			this.v_ObservacionesEmbarazo = v_ObservacionesEmbarazo;
			this.v_ComentaryUpdate = v_ComentaryUpdate;
			this.person = person;
        }
    }
}
