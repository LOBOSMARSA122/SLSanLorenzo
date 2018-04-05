using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class DiagnosticsByAgeGroup
    {
        public string IdServicio { get; set; }
        public string IdProtocolId { get; set; }
        public string IdTrabajador { get; set; }
        public string Trabajador { get; set; }
      
        public DateTime FechaServicioDate { get; set; }
        public string FechaServicio { get; set; }
       
        public DateTime? FechaNacimiento { get; set; }

        public string GrupoEtario { get; set; }
        public string GeneroTrabajador { get; set; }

        public string v_CustomerOrganizationName { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }

        public string v_DiseasesId { get; set; }
        public string v_DiseasesName { get; set; }

        public int EdadTrabajador { get; set; }
        public int CantTrabajador { get; set; }

        public byte[] b_Logo { get; set; }

        public string EmpresaCliente { get; set; }
        public byte[] LogoCliente { get; set; }
        public string EmpresaPropietaria { get; set; }
        public byte[] LogoPropietaria { get; set; }
    }
}
