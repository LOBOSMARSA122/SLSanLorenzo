//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2018/11/23 - 10:30:29
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
    public partial class obtenernetoporcobrarResultDto
    {
        [DataMember()]
        public Nullable<Decimal> d_NetoXCobrar { get; set; }

        public obtenernetoporcobrarResultDto()
        {
        }

        public obtenernetoporcobrarResultDto(Nullable<Decimal> d_NetoXCobrar)
        {
			this.d_NetoXCobrar = d_NetoXCobrar;
        }
    }
}
