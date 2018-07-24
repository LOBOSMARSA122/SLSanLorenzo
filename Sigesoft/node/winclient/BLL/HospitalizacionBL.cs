using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Common;
using System.Data.Objects;

namespace Sigesoft.Node.WinClient.BLL
{
    public class HospitalizacionBL
    {
        public List<HospitalizacionList> GetHospitalizacionPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from A in dbContext.hospitalizacion
                            join person B in dbContext.person on A.v_PersonId equals B.v_PersonId
                            //join G in dbContext.product on F.v_IdProductoDetalle equals G.v_ProductId

                            where A.i_IsDeleted == 0

                            select new HospitalizacionList
                            {
                                d_FechaAlta = A.d_FechaAlta,
                                d_FechaIngreso = A.d_FechaIngreso,
                                v_Paciente = B.v_FirstName +" " +  B.v_FirstLastName + " " + B.v_SecondLastName ,
                                v_HopitalizacionId = A.v_HopitalizacionId,
                                v_PersonId = A.v_PersonId,
                                i_IsDeleted = A.i_IsDeleted.Value,

                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                {
                    query = query.Where("d_FechaIngreso >= @0 && d_FechaIngreso <= @1", pdatBeginDate.Value, pdatEndDate.Value);
                }
                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                if (pintPageIndex.HasValue && pintResultsPerPage.HasValue)
                {
                    int intStartRowIndex = pintPageIndex.Value * pintResultsPerPage.Value;
                    query = query.Skip(intStartRowIndex);
                }
                if (pintResultsPerPage.HasValue)
                {
                    query = query.Take(pintResultsPerPage.Value);
                }

                List<HospitalizacionList> objData = query.ToList();

                // en hospitalizaciones tienes los padres
                var hospitalizaciones = (from a in objData
                         select new HospitalizacionList
                         {
                             v_Paciente = a.v_Paciente,
                             d_FechaIngreso = a.d_FechaIngreso,
                             d_FechaAlta = a.d_FechaAlta,
                             v_HopitalizacionId = a.v_HopitalizacionId,
                             v_PersonId = a.v_PersonId
                         }).ToList();

                var objtData = hospitalizaciones.AsEnumerable()
                    .Where(a => a.v_HopitalizacionId != null)
                    .GroupBy(b => b.v_HopitalizacionId)
                    .Select(group => group.First());

                List<HospitalizacionList> obj = objtData.ToList();
                
                HospitalizacionList hospit;
                List<HospitalizacionList> Lista = new List<HospitalizacionList>();

                foreach (var item in obj)
                {
                    hospit = new HospitalizacionList();

                    hospit.v_HopitalizacionId = item.v_HopitalizacionId;
                    hospit.v_PersonId = item.v_PersonId;
                    hospit.v_Paciente = item.v_Paciente;
                    hospit.d_FechaIngreso = item.d_FechaIngreso;
                    hospit.d_FechaAlta = item.d_FechaAlta;

                    // estos son los hijos de 1 hopitalización
                    var servicios = BuscarServiciosHospitalizacion(item.v_HopitalizacionId).ToList();
                    
                    List<HospitalizacionServiceList> HospitalizacionServicios = new List<HospitalizacionServiceList>();
                    if (servicios.Count > 0)
                    {
                        HospitalizacionServiceList oHospitalizacionServiceList;
                        foreach (var servicio in servicios)
                        {
                            oHospitalizacionServiceList = new HospitalizacionServiceList();
                            // acá vas poblando la entidad hijo hospitalización
                            oHospitalizacionServiceList.v_HopitalizacionId = servicio.v_HopitalizacionId;
                            oHospitalizacionServiceList.v_ServiceId = servicio.v_ServiceId;
                            oHospitalizacionServiceList.v_HospitalizacionServiceId = servicio.v_HospitalizacionServiceId;
                            // acá estoy agregando a las lista
                            HospitalizacionServicios.Add(servicio);
                        }
                        hospit.Servicios= HospitalizacionServicios;
                    }
                    Lista.Add(hospit);
                }
                pobjOperationResult.Success = 1;
                return Lista;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        private List<HospitalizacionServiceList> BuscarServiciosHospitalizacion(string hospitalizacionId)
        {
            //acá hace un select a la tabla hospitalizacionService y buscas todos que tengan foranea HospitalizacionId
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var queryservice = from A in dbContext.hospitalizacion
                        join C in dbContext.hospitalizacionservice on A.v_HopitalizacionId equals C.v_HopitalizacionId
                        join D in dbContext.service on C.v_ServiceId equals D.v_ServiceId

                               where A.v_HopitalizacionId == hospitalizacionId

                        select new HospitalizacionServiceList
                        {
                           v_HospitalizacionServiceId = C.v_HospitalizacionServiceId,
                           v_HopitalizacionId = C.v_HopitalizacionId,
                           v_ServiceId = C.v_ServiceId
                        };
            List<HospitalizacionServiceList> objData = queryservice.ToList();
            // en hospitalizaciones tienes los padres
            var hospitalizacionesservicios = (from a in objData
                                              select new HospitalizacionServiceList
                                     {
                                        v_HospitalizacionServiceId = a.v_HospitalizacionServiceId,
                                        v_HopitalizacionId = a.v_HopitalizacionId,
                                        v_ServiceId = a.v_ServiceId
                                     }).ToList();

            //var objtData = hospitalizacionesservicios.AsEnumerable()
            //        .Where(a => a.v_HopitalizacionId != null)
            //        .GroupBy(b => b.v_HopitalizacionId)
            //        .Select(group => group.First());

            List<HospitalizacionServiceList> obj = hospitalizacionesservicios;


            HospitalizacionServiceList tickets;
            List<HospitalizacionServiceList> Lista = new List<HospitalizacionServiceList>();

            foreach (var item in obj)
            {
                tickets = new HospitalizacionServiceList();

                tickets.v_HospitalizacionServiceId = item.v_HospitalizacionServiceId;
                tickets.v_HopitalizacionId = item.v_HopitalizacionId;
                tickets.v_ServiceId = item.v_ServiceId;

                // estos son los hijos de 1 hopitalización
                var ticketss = BuscarTickets(item.v_ServiceId).ToList();

                List<TicketList> Tickets = new List<TicketList>();
                if (ticketss.Count > 0)
                {
                    TicketList ticketslist;
                    foreach (var tick in ticketss)
                    {
                        ticketslist = new TicketList();
                        // acá vas poblando la entidad hijo hospitalización
                        ticketslist.v_TicketId = tick.v_TicketId;
                        ticketslist.v_ServiceId = tick.v_ServiceId;
                        ticketslist.d_Fecha = tick.d_Fecha;
                        // acá estoy agregando a las lista
                        Tickets.Add(tick);
                    }
                    tickets.Tickets = Tickets;
                }
                Lista.Add(tickets);
            }

            return Lista;
        }

        private List<TicketList> BuscarTickets(string v_ServiceId)
        {
            //acá hace un select a la tabla hospitalizacionService y buscas todos que tengan foranea HospitalizacionId
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var queryticket = from A in dbContext.hospitalizacion
                               join C in dbContext.hospitalizacionservice on A.v_HopitalizacionId equals C.v_HopitalizacionId
                               join D in dbContext.service on C.v_ServiceId equals D.v_ServiceId
                               join E in dbContext.ticket on D.v_ServiceId equals E.v_ServiceId

                               where E.v_ServiceId == v_ServiceId

                               select new TicketList
                               {
                                   v_ServiceId = E.v_ServiceId,
                                   v_TicketId = E.v_TicketId,
                                   d_Fecha = E.d_Fecha
                               };
            List<TicketList> objData = queryticket.ToList();
            // en hospitalizaciones tienes los padres
            var ticket = (from a in objData
                                              select new TicketList
                                              {
                                                  v_TicketId=a.v_TicketId,
                                                  v_ServiceId = a.v_ServiceId,
                                                  d_Fecha = a.d_Fecha
                                              }).ToList();

            var objtData = ticket.AsEnumerable()
                    .Where(a => a.v_TicketId != null)
                    .GroupBy(b => b.v_TicketId)
                    .Select(group => group.First());

            List<TicketList> obj = objtData.ToList();

            TicketList tickets;
            List<TicketList> Lista = new List<TicketList>();

            foreach (var item in obj)
            {
                tickets = new TicketList();

                tickets.v_TicketId = item.v_TicketId;
                tickets.v_ServiceId = item.v_ServiceId;
                tickets.d_Fecha = item.d_Fecha;

                // estos son los hijos de 1 hopitalización
                var ticketssdetalle = BuscarTicketsDetalle(item.v_TicketId).ToList();

                List<TicketDetalleList> Ticketsdetalle = new List<TicketDetalleList>();
                if (ticketssdetalle.Count > 0)
                {
                    TicketDetalleList ticketsdetallelist;
                    foreach (var tickdetalle in ticketssdetalle)
                    {
                        ticketsdetallelist = new TicketDetalleList();
                        // acá vas poblando la entidad hijo hospitalización
                        ticketsdetallelist.v_TicketId = tickdetalle.v_TicketId;
                        ticketsdetallelist.v_TicketDetalleId = tickdetalle.v_TicketDetalleId;
                        ticketsdetallelist.v_IdProductoDetalle = tickdetalle.v_IdProductoDetalle;
                        ticketsdetallelist.d_Cantidad = tickdetalle.d_Cantidad;
                        // acá estoy agregando a las lista
                        Ticketsdetalle.Add(tickdetalle);
                    }
                    tickets.Productos = Ticketsdetalle;
                }
                Lista.Add(tickets);
            }

            return Lista;
        }

        private List<TicketDetalleList> BuscarTicketsDetalle(string v_TicketId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var queryticketdetalle = from A in dbContext.hospitalizacion
                                     join C in dbContext.hospitalizacionservice on A.v_HopitalizacionId equals C.v_HopitalizacionId
                                     join D in dbContext.service on C.v_ServiceId equals D.v_ServiceId
                                     join E in dbContext.ticket on D.v_ServiceId equals E.v_ServiceId
                                     join F in dbContext.ticketdetalle on E.v_TicketId equals F.v_TicketId

                                     where E.v_TicketId == v_TicketId

                              select new TicketDetalleList
                              {
                                  v_TicketDetalleId = F.v_TicketDetalleId,
                                  v_TicketId = F.v_TicketId,
                                  d_Cantidad = F.d_Cantidad.Value,
                                  v_IdProductoDetalle = F.v_IdProductoDetalle
                              };
            List<TicketDetalleList> objData = queryticketdetalle.ToList();
            var ticketdetalle = (from a in objData
                                 select new TicketDetalleList
                          {
                              v_TicketId = a.v_TicketId,
                              v_IdProductoDetalle = a.v_IdProductoDetalle,
                              v_TicketDetalleId = a.v_TicketDetalleId,
                              d_Cantidad = a.d_Cantidad
                          }).ToList();

            //var objtData = ticketdetalle.AsEnumerable()
            //        .Where(a => a.v_TicketDetalleId != null)
            //        .GroupBy(b => b.v_TicketId)
            //        .Select(group => group.First());

            //List<TicketDetalleList> obj = objtData.ToList();

            return ticketdetalle;
        }
    }
}
