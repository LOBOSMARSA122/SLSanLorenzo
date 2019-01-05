using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class Liquidacion
    {
        public string v_PersonId { get; set; }
        public string v_ServiceId { get; set; }
        public string v_OrganizationId { get; set; }
        public int? i_EsoTypeId { get; set; }
        public string Esotype { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_EmployerName { get; set; }
        public string v_EmployerRuc { get; set; }
        public string v_EmployerDireccion { get; set; }
        public string v_EmployerRepr { get; set; }
        public string v_EmployerTelefono { get; set; }
        public string v_EmployerOrganizationId { get; set; }
        public string v_WorkingOrganizationId { get; set; }
        public string v_NroLiquidacion { get; set; }

        public string Trabajador { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public DateTime? FechaExamen { get; set; }
        public string NroDocumemto { get; set; }
        public string Cargo { get; set; }
        public string Perfil { get; set; }
        public decimal Precio { get; set; }
        public string CCosto { get; set; }
        public string v_ProtocolId { get; set; }

        public DateTime? Creacion_Liquidacion { get; set; }

        public string v_CustomerLocationId { get; set; }
        public string v_EmployerLocationId { get; set; }
        public string v_WorkingLocationId { get; set; }
       
        //

        public List<LiquidacionDetalle> Detalle { get; set; }

        public string UsuarioCrea { get; set; }
    }

    public class LiquidacionDetalle
    {
        public string v_PersonId { get; set; }
        public string v_ProtocolId { get; set; }
        public string v_NroLiquidacion { get; set; }
        public bool b_Seleccionar { get; set; }
        public int Item { get; set; }
        public string v_ServiceId { get; set; }
        public string Trabajador { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public DateTime? FechaExamen { get; set; }
        public string NroDocumemto { get; set; }
        public string Cargo { get; set; }
        public string Perfil { get; set; }
        public float SubTotal { get; set; }
        public float Igv { get; set; }
        public float Precio { get; set; }
        public string CCosto { get; set; }

        public string Esotype { get; set; }
        public string UsuarioCrea { get; set; }
    }
}
