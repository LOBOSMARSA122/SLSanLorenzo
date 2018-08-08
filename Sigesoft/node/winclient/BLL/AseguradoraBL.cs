using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Common;

namespace Sigesoft.Node.WinClient.BLL
{
    public class AseguradoraBL
    {
        public List<LiquidacionAseguradora> GetLiquidacionAseguradoraPagedAndFiltered(ref OperationResult pobjOperationResult,string pstrFilterExpression,DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.service
                            join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                            join C in dbContext.protocol on A.v_ProtocolId equals C.v_ProtocolId
                            join D in dbContext.organization on C.v_EmployerOrganizationId equals D.v_OrganizationId
                    where A.i_IsDeleted == 0 && D.i_OrganizationTypeId == 4
                    select new LiquidacionAseguradora
                    {
                        ServicioId = A.v_ServiceId,
                        FechaServicio = A.d_ServiceDate.Value,
                        Paciente = B.v_FirstName + " " + B.v_FirstLastName + " " + B.v_SecondLastName,
                        PacientDocument = B.v_FirstName + " " + B.v_FirstLastName + " " + B.v_SecondLastName + " " + B.v_DocNumber,
                        EmpresaId = C.v_EmployerOrganizationId,
                        Aseguradora = D.v_Name
                    };

                    if (!string.IsNullOrEmpty(pstrFilterExpression))
                    {
                        query = query.Where(pstrFilterExpression);
                    }
                    if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                    {
                        query = query.Where("FechaServicio >= @0 && FechaServicio <= @1", pdatBeginDate.Value, pdatEndDate.Value);
                    }

                List<LiquidacionAseguradora> data = query.ToList();
                var ListaLiquidacion = new List<LiquidacionAseguradora>();
                LiquidacionAseguradora oLiquidacionAseguradora;
                var detalle = new List<LiquiAseguradoraDetalle>();
                LiquiAseguradoraDetalle oLiquiAseguradoraDetalle;
                foreach (var servicio in data)
                {
                    oLiquidacionAseguradora = new LiquidacionAseguradora();

                    oLiquidacionAseguradora.ServicioId = servicio.ServicioId;
                    oLiquidacionAseguradora.FechaServicio = servicio.FechaServicio;
                    oLiquidacionAseguradora.Paciente = servicio.Paciente;
                    oLiquidacionAseguradora.Aseguradora = servicio.Aseguradora;

                    var serviceComponents = obtenerServiceComponentsByServiceId(servicio.ServicioId);
                    foreach (var componente in serviceComponents)
                    {
                        oLiquiAseguradoraDetalle = new LiquiAseguradoraDetalle();
                        oLiquiAseguradoraDetalle.Descripcion = componente.v_ComponentName;
                        oLiquiAseguradoraDetalle.Valor = componente.d_Importe;
                        oLiquiAseguradoraDetalle.Tipo = componente.i_EsDeducible == 1 ? "DEDUCIBLE" : "COASEGURO";
                        oLiquiAseguradoraDetalle.SaldoPaciente = componente.d_SaldoPaciente.Value;
                        oLiquiAseguradoraDetalle.SaldoAseguradora = componente.d_SaldoAseguradora.Value;
                        oLiquiAseguradoraDetalle.SubTotal = componente.d_SaldoPaciente.Value + componente.d_SaldoAseguradora.Value;
                        detalle.Add(oLiquiAseguradoraDetalle);
                    }

                    var tickets = obtenerTicketsByServiceId(servicio.ServicioId);
                    foreach (var ticket in tickets)
                    {
                        oLiquiAseguradoraDetalle = new LiquiAseguradoraDetalle();
                        oLiquiAseguradoraDetalle.Descripcion = ticket.v_NombreProducto;
                        oLiquiAseguradoraDetalle.Valor = ticket.d_Importe;
                        oLiquiAseguradoraDetalle.Tipo = ticket.i_EsDeducible == 1 ? "DEDUCIBLE" : "COASEGURO";
                        oLiquiAseguradoraDetalle.SaldoPaciente = ticket.d_SaldoPaciente;
                        oLiquiAseguradoraDetalle.SaldoAseguradora = ticket.d_SaldoAseguradora;
                        oLiquiAseguradoraDetalle.SubTotal = ticket.d_SaldoPaciente + ticket.d_SaldoAseguradora;
                        detalle.Add(oLiquiAseguradoraDetalle);
                    }

                    oLiquidacionAseguradora.Detalle = detalle;

                    ListaLiquidacion.Add(oLiquidacionAseguradora);
                }
             
                    pobjOperationResult.Success = 1;
            return ListaLiquidacion;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private List<TicketDetalleList> obtenerTicketsByServiceId(string serviceId)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.ticket
                             join B in dbContext.ticketdetalle on A.v_TicketId equals  B.v_TicketId
                             join C in dbContext.service on serviceId equals C.v_ServiceId
                             join I in dbContext.protocol on C.v_ProtocolId equals I.v_ProtocolId
                             join G in dbContext.plan on new { a = I.v_ProtocolId, b = B.v_IdUnidadProductiva }
                                    equals new { a = G.v_ProtocoloId, b = G.v_IdUnidadProductiva } into G_join
                             from G in G_join.DefaultIfEmpty()
                             where A.v_ServiceId == serviceId && A.i_IsDeleted == 0
                             select new TicketDetalleList
                             {
                                 v_NombreProducto = B.v_Descripcion,
                                 d_SaldoPaciente = B.d_SaldoPaciente.Value,
                                 d_SaldoAseguradora = B.d_SaldoAseguradora.Value,
                                 i_EsDeducible = G.i_EsDeducible.Value,
                                 i_EsCoaseguro = G.i_EsCoaseguro.Value,
                                 d_Importe = G.d_Importe

                             }).ToList();

                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private List<ServiceComponentList> obtenerServiceComponentsByServiceId(string serviceId)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.servicecomponent
                             join C in dbContext.systemuser on A.i_MedicoTratanteId equals C.i_SystemUserId
                             join D in dbContext.person on C.v_PersonId equals D.v_PersonId
                             join B in dbContext.component on A.v_ComponentId equals B.v_ComponentId
                             join F in dbContext.systemparameter on new { a = B.i_CategoryId.Value, b = 116 }
                                     equals new { a = F.i_ParameterId, b = F.i_GroupId } into F_join
                             from F in F_join.DefaultIfEmpty()
                             join H in dbContext.service on A.v_ServiceId equals H.v_ServiceId
                             join I in dbContext.protocol on H.v_ProtocolId equals I.v_ProtocolId
                             join G in dbContext.plan on new { a = I.v_ProtocolId, b = A.v_IdUnidadProductiva }
                                     equals new { a = G.v_ProtocoloId, b = G.v_IdUnidadProductiva } into G_join
                             from G in G_join.DefaultIfEmpty()
                             where A.v_ServiceId == serviceId &&
                                   A.i_IsDeleted == 0 &&
                                   A.i_IsRequiredId == 1 &&
                                   (A.d_SaldoPaciente.Value > 0 || A.d_SaldoAseguradora.Value > 0)

                             select new ServiceComponentList
                             {
                                 v_ServiceComponentId = A.v_ServiceComponentId,
                                 v_ComponentId = A.v_ComponentId,
                                 r_Price = A.r_Price,
                                 v_ComponentName = B.v_Name,
                                 v_CategoryName = F.v_Value1,
                                 MedicoTratante = D.v_FirstName + " " + D.v_FirstLastName + " " + D.v_SecondLastName,
                                 d_InsertDate = A.d_InsertDate,
                                 d_SaldoPaciente = A.d_SaldoPaciente.Value,
                                 d_SaldoAseguradora = A.d_SaldoAseguradora.Value,
                                 i_EsDeducible = G.i_EsDeducible.Value,
                                 i_EsCoaseguro = G.i_EsCoaseguro.Value,
                                 d_Importe = G.d_Importe
                             }).ToList();

                return query;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
