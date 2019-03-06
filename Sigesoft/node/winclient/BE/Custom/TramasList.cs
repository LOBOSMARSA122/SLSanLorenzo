using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class TramasList
    {
        public string v_TramaId { get; set; }

        public string v_TipoRegistro { get; set; }

        public DateTime? d_FechaIngreso { get; set; }

        public DateTime? d_FechaAlta { get; set; }

        public int? i_Genero { get; set; }

        public string Genero { get; set; }

        public int? i_GrupoEtario { get; set; }

        public string GrupoEtario { get; set; }

        public string v_DiseasesName { get; set; }

        public string v_CIE10Id { get; set; }

        public int? i_UPS { get; set; }

        public int? i_Procedimiento { get; set; }

        public string procedimiento_Detail { get; set; }

        public string dead_prod { get; set; }

        public int? i_Especialidad { get; set; }

        public int? i_TipoParto { get; set; }

        public string tipoParto_Detail { get; set; }

        public int? i_TipoNacimiento { get; set; }

        public string tipoNacimiento_Detail { get; set; }

        public int? i_TipoComplicacion { get; set; }

        public string tipoCompliacacion_Detail { get; set; }

        public int? i_Programacion { get; set; }

        public string programacion_Detail { get; set; }

        public int? i_TipoCirugia { get; set; }

        public string tipoCirugia_Detail { get; set; }

        public int? i_HorasProg { get; set; }

        public int? i_HorasEfect { get; set; }

        public int? i_HorasActo { get; set; }

        public int? i_IsDeleted { get; set; }

        public int? i_InsertUserId { get; set; }

        public DateTime? d_InsertDate { get; set; }

        public int? i_UpdateUserId { get; set; }

        public DateTime? d_UpdateDate { get; set; }

        public string User_Crea { get; set; }
        public string User_Act { get; set; }
        public string ups_Detail { get; set; }

    }
}
