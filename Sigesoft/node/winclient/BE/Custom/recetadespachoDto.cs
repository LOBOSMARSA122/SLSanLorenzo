using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public partial class recetadespachoDto
    {
        public int RecetaId { get; set; }
        public string NombrePaciente { get; set; }
        public string Medicamento { get; set; }
        public string Presentacion { get; set; }
        public string UnidadMedida { get; set; }
        public string Ubicacion { get; set; }
        public string Duracion { get; set; }
        public DateTime FechaFin { get; set; }
        public string Dosis { get; set; }
        public decimal CantidadRecetada { get; set; }
        public string NombreMedico { get; set; }
        public string MedicoNroCmp { get; set; }
        public byte[] RubricaMedico { get; set; }
        public string NombreClinica { get; set; }
        public string DireccionClinica { get; set; }
        public byte[] LogoClinica { get; set; }
        public bool Despacho { get; set; }
        public string MedicinaId { get; set; }
        //public string EstadoDespacho {
        //    get
        //    {
        //        if (d_MontoDespachado == CantidadRecetada) return "SI";
        //        if (d_MontoDespachado < CantidadRecetada) return "PARCIAL";
        //        if (d_MontoDespachado == 0) return "NO";
        //        return "NO VALIDO";
        //    }
        //}
    }
}
