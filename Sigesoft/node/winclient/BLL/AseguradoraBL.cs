using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Common;
using System.Data.SqlClient;

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
                            join CA in dbContext.calendar on A.v_ServiceId equals  CA.v_ServiceId
                            join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                            join C in dbContext.protocol on A.v_ProtocolId equals C.v_ProtocolId
                            join E in dbContext.plan on C.v_ProtocolId equals E.v_ProtocoloId
                            join D in dbContext.organization on E.v_OrganizationSeguroId equals D.v_OrganizationId
                            where A.i_IsDeleted == 0 && 
                                  A.d_ServiceDate >= pdatBeginDate.Value && 
                                  A.d_ServiceDate <= pdatEndDate.Value && 
                                  CA.i_CalendarStatusId != 4
                    select new LiquidacionAseguradora
                    {
                        ServicioId = A.v_ServiceId,
                        FechaServicio = A.d_ServiceDate.Value,
                        Paciente = B.v_FirstName + " " + B.v_FirstLastName + " " + B.v_SecondLastName,
                        PacientDocument = B.v_FirstName + " " + B.v_FirstLastName + " " + B.v_SecondLastName + " " + B.v_DocNumber,
                        EmpresaId = E.v_OrganizationSeguroId,
                        Aseguradora = D.v_Name,
                        Protocolo = C.v_ProtocolId,
                        Factor_ = C.r_PriceFactor,
                        PPS_ = C.r_MedicineDiscount
                    };

                    if (!string.IsNullOrEmpty(pstrFilterExpression))
                    {
                        query = query.Where(pstrFilterExpression);
                    }
                    
                 List<LiquidacionAseguradora> data = query.OrderBy(o => o.FechaServicio).ToList();
                 

                 data = data.GroupBy(p => p.ServicioId).Select(s => s.First()).ToList();

                var ListaLiquidacion = new List<LiquidacionAseguradora>();
                LiquidacionAseguradora oLiquidacionAseguradora;
                
                LiquiAseguradoraDetalle oLiquiAseguradoraDetalle;
                decimal? TotalAseguradora = 0;
                foreach (var servicio in data)
                {
                    oLiquidacionAseguradora = new LiquidacionAseguradora();

                    oLiquidacionAseguradora.ServicioId = servicio.ServicioId;
                    oLiquidacionAseguradora.FechaServicio = servicio.FechaServicio;
                    oLiquidacionAseguradora.Paciente = servicio.Paciente;
                    oLiquidacionAseguradora.Aseguradora = servicio.Aseguradora;
                    oLiquidacionAseguradora.Protocolo = servicio.Protocolo;
                    oLiquidacionAseguradora.Factor = decimal.Round((decimal)servicio.Factor_, 2);
                    oLiquidacionAseguradora.PPS = servicio.PPS_ + "%";


                    var serviceComponents = obtenerServiceComponentsByServiceId(servicio.ServicioId);
                    var detalle = new List<LiquiAseguradoraDetalle>();
                    foreach (var componente in serviceComponents)
                    {

                        oLiquiAseguradoraDetalle = new LiquiAseguradoraDetalle();
                        oLiquiAseguradoraDetalle.Descripcion = componente.v_ComponentName;
                        
                        oLiquiAseguradoraDetalle.Tipo = componente.i_EsDeducible == 1 ? "DEDUCIBLE" : "COASEGURO";
                        string simbolo = "";
                        if (oLiquiAseguradoraDetalle.Tipo == "DEDUCIBLE")
                        {
                            simbolo = " S/.";
                        }
                        else
                        {
                            simbolo = " %";
                        }

                        oLiquiAseguradoraDetalle.Valor = componente.d_Importe.ToString() + simbolo;
                        oLiquiAseguradoraDetalle.SaldoPaciente = componente.d_SaldoPaciente.Value;
                        oLiquiAseguradoraDetalle.SaldoAseguradora = componente.d_SaldoAseguradora.Value;
                        TotalAseguradora += oLiquiAseguradoraDetalle.SaldoAseguradora;
                        oLiquiAseguradoraDetalle.SubTotal = componente.d_SaldoPaciente.Value + componente.d_SaldoAseguradora.Value;
                        oLiquiAseguradoraDetalle.Cantidad = 1M;
                        oLiquiAseguradoraDetalle.PrecioUnitario = decimal.Parse(componente.r_Price.ToString());
                        detalle.Add(oLiquiAseguradoraDetalle);
                    }

                    //var tickets = obtenerTicketsByServiceId(servicio.ServicioId);
                    //foreach (var ticket in tickets)
                    //{
                    //    oLiquiAseguradoraDetalle = new LiquiAseguradoraDetalle();
                    //    oLiquiAseguradoraDetalle.Descripcion = ticket.v_NombreProducto;
                       
                    //    oLiquiAseguradoraDetalle.Tipo = ticket.i_EsDeducible == 1 ? "DEDUCIBLE" : "COASEGURO";
                    //    string simbolo = "";
                    //    if (oLiquiAseguradoraDetalle.Tipo == "DEDUCIBLE")
                    //    {
                    //        simbolo = " S/.";
                    //    }
                    //    else
                    //    {
                    //        simbolo = " %";
                    //    }    
                    //    oLiquiAseguradoraDetalle.Valor = ticket.d_Importe.ToString() + simbolo;
                    //    oLiquiAseguradoraDetalle.SaldoPaciente = ticket.d_SaldoPaciente.Value;
                    //    oLiquiAseguradoraDetalle.SaldoAseguradora = ticket.d_SaldoAseguradora.Value;
                    //    TotalAseguradora += oLiquiAseguradoraDetalle.SaldoAseguradora.Value;
                    //    oLiquiAseguradoraDetalle.SubTotal = ticket.d_SaldoPaciente.Value + ticket.d_SaldoAseguradora.Value;
                    //    oLiquiAseguradoraDetalle.Cantidad = ticket.d_Cantidad;
                    //    oLiquiAseguradoraDetalle.PrecioUnitario = ticket.d_PrecioVenta;
                    //    detalle.Add(oLiquiAseguradoraDetalle);
                    //}

                    //var recetas = obtenerRecetasByServiceId(servicio.ServicioId);
                    //foreach (var receta in recetas)
                    //{
                    //    oLiquiAseguradoraDetalle = new LiquiAseguradoraDetalle();
                    //    oLiquiAseguradoraDetalle.Descripcion = dbContext.obtenerproducto(receta.v_IdProductoDetalle).ToList()[0].v_Descripcion;// receta.v_IdProductoDetalle;
                       
                    //    oLiquiAseguradoraDetalle.Tipo = receta.i_EsDeducible == 1 ? "DEDUCIBLE" : "COASEGURO";
                    //    string simbolo = "";
                    //    if (oLiquiAseguradoraDetalle.Tipo == "DEDUCIBLE")
                    //    {
                    //        simbolo = " S/.";
                    //    }
                    //    else
                    //    {
                    //        simbolo = " %";
                    //    }    
                    //    oLiquiAseguradoraDetalle.Valor = receta.d_Importe.ToString() + simbolo;
                    //    oLiquiAseguradoraDetalle.Cantidad = receta.i_Cantidad.Value;
                    //    oLiquiAseguradoraDetalle.SaldoPaciente = receta.d_SaldoPaciente;
                    //    oLiquiAseguradoraDetalle.SaldoAseguradora = receta.d_SaldoAseguradora;
                    //    TotalAseguradora += oLiquiAseguradoraDetalle.SaldoAseguradora;
                    //    oLiquiAseguradoraDetalle.SubTotal = (oLiquiAseguradoraDetalle.SaldoPaciente.Value + oLiquiAseguradoraDetalle.SaldoAseguradora.Value);
                    //    #region Conexion SIGESOFT
                    //    ConexionSigesoft conectasam = new ConexionSigesoft();
                    //    conectasam.opensigesoft();
                    //    #endregion
                    //    var cadena1 = "select PR.r_MedicineDiscount, OO.v_Name, PR.v_CustomerOrganizationId from Organization OO inner join protocol PR On PR.v_AseguradoraOrganizationId = OO.v_OrganizationId where PR.v_ProtocolId ='" + oLiquidacionAseguradora.Protocolo + "'";
                    //    SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                    //    SqlDataReader lector = comando.ExecuteReader();
                    //    string factor = "";
                    //    while (lector.Read())
                    //    {
                    //        factor = lector.GetValue(0).ToString();
                    //    }
                    //    lector.Close();
                    //    conectasam.closesigesoft();
                    //    #region Conexion SAMBHS
                    //    ConexionSambhs conectaConexionSambhs = new ConexionSambhs();
                    //    conectaConexionSambhs.openSambhs();
                    //    #endregion

                    //    var cadenasam = "select PP.d_PrecioMayorista from producto PP inner join productodetalle PD on PD.v_IdProducto = PP.v_IdProducto where PD.v_IdProductoDetalle ='" + receta.v_IdProductoDetalle + "'";
                    //    comando = new SqlCommand(cadenasam, connection: conectaConexionSambhs.conectarSambhs);
                    //    lector = comando.ExecuteReader();
                    //    string preciounitario = "";
                    //    while (lector.Read())
                    //    {
                    //        preciounitario = lector.GetValue(0).ToString();
                    //    }
                    //    lector.Close();
                    //    conectaConexionSambhs.closeSambhs();

                    //    oLiquiAseguradoraDetalle.PrecioUnitario = decimal.Parse(preciounitario) - (decimal.Parse(preciounitario) * decimal.Parse(factor) / 100);
                    //    detalle.Add(oLiquiAseguradoraDetalle);
                    //}

                    oLiquidacionAseguradora.TotalAseguradora = TotalAseguradora;
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

        private List<recetaList> obtenerRecetasByServiceId(string serviceId)
        {
            try
            {
                 var dbContext = new SigesoftEntitiesModel();
                 var query = (from A in dbContext.receta
                              join B in dbContext.diagnosticrepository on A.v_DiagnosticRepositoryId equals B.v_DiagnosticRepositoryId
                              join C in dbContext.service on A.v_ServiceId equals C.v_ServiceId
                              join I in dbContext.protocol on C.v_ProtocolId equals I.v_ProtocolId
                              join G in dbContext.plan on new { a = I.v_ProtocolId, b = A.v_IdUnidadProductiva }
                                  equals new { a = G.v_ProtocoloId, b = G.v_IdUnidadProductiva } into G_join
                              from G in G_join.DefaultIfEmpty()
                              where A.v_ServiceId == serviceId && C.i_IsDeleted == 0 && (A.d_SaldoPaciente.Value > 0 || A.d_SaldoAseguradora.Value > 0)
                              select new recetaList
                              {
                                  v_IdProductoDetalle = A.v_IdProductoDetalle,
                                  d_SaldoPaciente = A.d_SaldoPaciente,
                                  d_SaldoAseguradora = A.d_SaldoAseguradora,
                                  v_IdUnidadProductiva = A.v_IdUnidadProductiva,
                                  i_EsDeducible = G.i_EsDeducible.Value,
                                  i_EsCoaseguro = G.i_EsCoaseguro.Value,
                                  d_Importe = G.d_Importe,
                                  i_Cantidad = A.d_Cantidad.Value
                              }
                             ).ToList();

                 return query;
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
                                 d_Importe = G.d_Importe,
                                 d_Cantidad = B.d_Cantidad.Value,
                                 d_PrecioVenta = (decimal)B.d_PrecioVenta
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
                                     equals new { a = G.v_ProtocoloId, b = G.v_IdUnidadProductiva } 
                             where A.v_ServiceId == serviceId &&
                                   A.i_IsDeleted == 0 &&
                                   A.i_IsRequiredId == 1 &&
                                   (A.d_SaldoPaciente.Value > 0 || A.d_SaldoAseguradora.Value > 0)

                             select new ServiceComponentList
                             {
                                 v_ServiceComponentId = A.v_ServiceComponentId,
                                 v_ComponentId = A.v_ComponentId,
                                 r_Price = A.r_Price.Value,
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
