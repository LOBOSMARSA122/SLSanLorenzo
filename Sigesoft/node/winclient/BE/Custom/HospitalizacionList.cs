using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class HospitalizacionList
    {
        public string v_HopitalizacionId { get; set; }
        public string v_PersonId { get; set; }
        public string v_DocNumber { get; set; }
        public int i_Years { get; set; }
        public DateTime d_Birthdate { get; set; }
        public string v_Paciente { get; set; }
        public DateTime? d_FechaIngreso { get; set; }
        public DateTime? d_FechaAlta { get; set; }
        public int i_IsDeleted { get; set; }
        public string v_Comentario { get; set; }
        public string v_NroLiquidacion { get; set; }
        public string v_NroHospitalizacion { get; set; }

        public decimal? d_PagoMedico { get; set; }
        public int? i_MedicoPago { get; set; }
        public string MedicoPago { get; set; }
        public decimal? d_PagoPaciente { get; set; }
        public int? i_PacientePago { get; set; }
        public string PacientePago { get; set; }

        public string v_MedicoTratante { get; set; }

        public string v_Servicio { get; set; }


        public List<HospitalizacionServiceList> Servicios{ get; set; }
        public List<HospitalizacionHabitacionList> Habitaciones { get; set; }
       
    }
}
