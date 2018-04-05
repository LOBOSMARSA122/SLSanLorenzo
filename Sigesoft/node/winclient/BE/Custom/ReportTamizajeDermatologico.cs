using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportTamizajeDermatologico
    {

        public string Ficha{get;set;}

        public string Hc{get;set;}

        public string SufreEnfermedadPielSiNo { get; set; }

        public string SiQueDxTiene { get; set; }

        public string ActualmenteLesionSiNo { get; set; }

        public string DondeLocalizaLesion { get; set; }

        public string CuantoTieneLesion { get; set; }

        public string PresentaColoracionPielSiNo { get; set; }

        public string LesionRepiteVariasAniosSiNo { get; set; }

        public string EnrrojecimientoParteCuerpoSiNo { get; set; }

        public string EnrrojecimientoLocaliza { get; set; }

        public string TieneComezonSiNo { get; set; }

        public string ComezonLocaliza { get; set; }

        public string HinchazonParteCuerpoSiNo { get; set; }

        public string HinchazonParteCuerpoLocaliza { get; set; }

        public string AlergiaAsmaSiNo { get; set; }



        public string EppSiNo { get; set; }

        public string TipoProteccionUsa { get; set; }

        public string CambioUnasSiNo { get; set; }

        public string TomandoMedicacionSiNo { get; set; }

        public string ComoLlamaMedicacion { get; set; }

        public string DosisFrecuencia { get; set; }

        public string Descripcion { get; set; }

        public string DermatopiaSiNo { get; set; }

        public string NikolskySiNo { get; set; }


        public byte[] FirmaTrabajador{get;set;}

        public byte[] HuellaTrabajador { get; set; }

        public byte[] FirmaMedico { get; set; }

        public DateTime Fecha{get;set;}

        public string NombreTrabajador { get; set; }

        public string v_OwnerOrganizationName { get; set; }


        public byte[] b_Logo { get; set; }
        public string EmpresaPropietaria { get; set; }
        public string EmpresaPropietariaDireccion { get; set; }
        public string EmpresaPropietariaTelefono { get; set; }
        public string EmpresaPropietariaEmail { get; set; }
        public string NombreUsuarioGraba { get; set; }
        public string EmpresaCliente { get; set; }
    }
}
