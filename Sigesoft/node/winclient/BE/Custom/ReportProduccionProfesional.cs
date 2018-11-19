using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ReportProduccionProfesional
    {
      public string NroAtencion { get; set; }
      public DateTime Fecha { get; set; }
      public DateTime? FechaNacimiento { get; set; }
      public int Edad { get; set; }
      public string Dni { get; set; }
      public string Paciente { get; set; }
      public string Parentesco { get; set; }
      public string Titular { get; set; }
      public string EmpresaCliente { get; set; }
      public string EmpresaTrabajo { get; set; }
      public string CostoProtocolo { get; set; }


      public string v_OrganizationId { get; set; }
      public string v_LocationId { get; set; }
      public string v_WorkingOrganizationId { get; set; }
      public string v_WorkingLocationId { get; set; }
      public string v_OrganizationInvoiceId { get; set; }
      public int i_EsoTypeId { get; set; }
      public string EstoType { get; set; }


      public int i_ApprovedUpdateUserId { get; set; }
      public int i_CategoryId { get; set; }
      public string v_PersonId { get; set; }
      public string v_ProtocoloId { get; set; }

      public DateTime? FechaInicio { get; set; }
      public DateTime? FechaFin { get; set; }
      public String Usuario { get; set; }
      public string NombreUsuario { get; set; }
      public string ConsultorioId { get; set; }
      public string Consultorio { get; set; }
      public string v_CustomerOrganizationId { get; set; }
      public string v_CustomerLocationId { get; set; }
      public string EmpresaClienteCabecera { get; set; }

      public string NombreComponente { get; set; }
      public double PrecioUnitario { get; set; }
      public double Pagar { get; set; }

      public List<ReportProduccionProfesional> ProduccionProfesionalDetalle { get; set; }


    }
}
