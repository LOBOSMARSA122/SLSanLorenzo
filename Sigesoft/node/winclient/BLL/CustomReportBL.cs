using Sigesoft.Node.WinClient.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BLL
{
   public class CustomReportBL
    {
       public List<ReportResumeCaja> ResumenCaja(DateTime fInicio, DateTime fFin, int tipoCaja)
       {
           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               var query = dbContext.reportegerencial(fInicio, fFin, tipoCaja).OrderBy(p => p.Fecha).ToList();

               var meses = query.GroupBy(g => g.Mes).Select(s => s.First());
               decimal? totalIngreso = 0;
               decimal? totalEgreso = 0;
               decimal? totalSaldo = 0;

               var list = new List<ReportResumeCaja>();            
               foreach (var mes in meses)
               {
                   var oReportResumeCaja = new ReportResumeCaja();

                   oReportResumeCaja.mes = mes.Mes;

                   var records = query.FindAll(p => p.Mes == mes.Mes);

                   var detalle = new List<DetalleResumenCaja>();
                   foreach (var record in records)
                   {
                       var oDetalleResumenCaja = new DetalleResumenCaja();
                       oDetalleResumenCaja.Fecha = record.Fecha;
                       //oDetalleResumenCaja.TipoDocumento = record.TipoDocumento.Value;
                       oDetalleResumenCaja.Ingreso = record.Ingreso;
                       oDetalleResumenCaja.Egreso = record.Egreso;
                       oDetalleResumenCaja.Saldo = record.Saldo;

                       totalIngreso += record.Ingreso;
                       totalEgreso += record.Egreso;
                       totalSaldo += record.Ingreso - record.Egreso;

                       detalle.Add(oDetalleResumenCaja);
                   }

                   var x = detalle
                                .GroupBy(p => p.Fecha)
                                .Select(s => new DetalleResumenCaja
                                {
                                    Fecha = s.First().Fecha,
                                    Ingreso = s.Sum( c => c.Ingreso).Value,
                                    Egreso = s.Sum(c => c.Egreso).Value,
                                    Saldo = s.Sum( c => c.Ingreso).Value - s.Sum(c => c.Egreso).Value
                                }).ToList();


                   oReportResumeCaja.totalIngreso = totalIngreso;
                   oReportResumeCaja.totalEgreso = totalEgreso;
                   oReportResumeCaja.totalSaldo = totalSaldo;

                   oReportResumeCaja.detalle = x;

                   list.Add(oReportResumeCaja);
               }
               
               return list;
           }
           catch (Exception ex)
           {               
               throw;
           }
       }

       public List<ResumenTipoPago> ReportResumenTipoPago(DateTime fInicio, DateTime fFin, int tipoCaja)
       {
           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

               var query = dbContext.resumentipopago(fInicio, fFin, tipoCaja).OrderBy(p => p.Fecha).ToList();

               var groups = query.GroupBy(g => new { g.IdCondicionPago, g.IdFormaPago }).Select(s => s.First());

               var ListResumenTipoPago = new List<ResumenTipoPago>();

               foreach (var group in groups)
               {
                   var oResumenTipoPago = new ResumenTipoPago();

                   oResumenTipoPago.grupo = group.CondicionPago + "-" + group.FormaPago;
                   oResumenTipoPago.IdCondicionPago = group.IdCondicionPago.Value;
                   oResumenTipoPago.IdFormaPago = group.IdFormaPago.Value;

                   var cobranzas = query.FindAll(p => p.IdCondicionPago == group.IdCondicionPago && p.IdFormaPago == group.IdFormaPago).ToList();

                   var ListDetalleResumenTipoPago = new List<DetalleResumenTipoPago>();
                   decimal? importeTotal = 0;
                   foreach (var cobranza in cobranzas)
                   {
                       var oDetalleResumenTipoPago = new DetalleResumenTipoPago();
                       oDetalleResumenTipoPago.Fecha = cobranza.Fecha;
                       oDetalleResumenTipoPago.Comprobante = cobranza.Comprobante;

                       oDetalleResumenTipoPago.Empresa = cobranza.Empresa;
                       oDetalleResumenTipoPago.Importe = cobranza.Importe.Value;
                       importeTotal += cobranza.Importe.Value;
                       #region Buscar si tiene NroLiquidacion
                       oDetalleResumenTipoPago.Servicios = DetalleFactura(cobranza.Comprobante);
                       oDetalleResumenTipoPago.NroLiquidacion = oDetalleResumenTipoPago.Servicios.Count > 0 ? oDetalleResumenTipoPago.Servicios[0].NroLiquidacion : "";
                       #endregion  

                       ListDetalleResumenTipoPago.Add(oDetalleResumenTipoPago);
                   }

                   oResumenTipoPago.Cobranzas = ListDetalleResumenTipoPago;
                   oResumenTipoPago.ImporteTotal = importeTotal;
                   ListResumenTipoPago.Add(oResumenTipoPago);                   
               }

               return ListResumenTipoPago;
           }
           catch (Exception ex)
           {               
               throw;
           }
       }

       public List<ResumenTipoEmpresa> ReportResumenTipoEmpresa(DateTime fInicio, DateTime fFin)
       {
           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

               var query = dbContext.resumentipoempresa(fInicio, fFin).OrderBy(p => p.v_ServiceId).ToList();

               var list = new List<ResumenTipoEmpresa>();
               foreach (var item in query)
               {
                   var oResumenTipoEmpresa = new ResumenTipoEmpresa();

                   oResumenTipoEmpresa.v_ServiceId = item.v_ServiceId;
                   oResumenTipoEmpresa.EmpresaCliente = item.EmpresaCliente;
                   oResumenTipoEmpresa.EmpresaEmpleadora = item.EmpresaEmpleadora;
                   oResumenTipoEmpresa.EmpresaTrabajo = item.EmpresaTrabajo;
                   oResumenTipoEmpresa.Precio = item.Precio.Value;
                   oResumenTipoEmpresa.Trabajador = item.Trabajador;
                   oResumenTipoEmpresa.FechaExamen = item.FechaExamen;
                   list.Add(oResumenTipoEmpresa);
               }

               return list;
           }
           catch (Exception ex)
           {
               throw;
           }
       }
      
       public List<Servicios> DetalleFactura(string nroFactura)
       {
           SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
           try
           {
               var query = (from A in dbContext.liquidacion
                            join B in dbContext.service on A.v_NroLiquidacion equals B.v_NroLiquidacion
                            join C in dbContext.person on B.v_PersonId equals C.v_PersonId
                            join E in dbContext.protocol on B.v_ProtocolId equals E.v_ProtocolId
                            join J in dbContext.organization on E.v_EmployerOrganizationId equals J.v_OrganizationId into J_join
                            from J in J_join.DefaultIfEmpty()
                            join J11 in dbContext.organization on E.v_CustomerOrganizationId equals J11.v_OrganizationId into J11_join
                            from J11 in J11_join.DefaultIfEmpty()
                            join J22 in dbContext.organization on E.v_WorkingOrganizationId equals J22.v_OrganizationId into J22_join
                            from J22 in J22_join.DefaultIfEmpty()

                            where A.v_NroFactura == nroFactura
                            select new Servicios
                            {
                                NroLiquidacion = A.v_NroLiquidacion,
                                Trabajador = C.v_FirstName + " " + C.v_FirstLastName +  " " + C.v_SecondLastName,
                                ServicioId = B.v_ServiceId,
                                FechaExamen = B.d_ServiceDate.Value,
                                CostoExamen = 0,
                                Compania = J.v_Name,
                                Contratista = J11.v_Name,
                                UsuarioRecepcion = A.v_NroFactura,
                            }).ToList();


               var result = (from A in query
                             select new Servicios
                             {
                                 NroLiquidacion = A.NroLiquidacion,
                                 Trabajador = A.Trabajador,
                                 ServicioId = A.ServicioId,
                                 FechaExamen = A.FechaExamen,
                                 CostoExamen = decimal.Parse(new ServiceBL().GetServiceCost(A.ServicioId)),
                                 Compania = A.Compania,
                                 Contratista = A.Contratista,
                                 UsuarioRecepcion = A.UsuarioRecepcion
                             }).ToList();


               return result;
           }
           catch (Exception ex)
           {               
               throw;
           }
       }

       public class ResumenTipoEmpresa
       {
           public string v_ServiceId { get; set; }
           public string EmpresaCliente { get; set; }
           public string EmpresaEmpleadora { get; set; }
           public string EmpresaTrabajo { get; set; }
           public string Trabajador { get; set; }
           public DateTime? FechaExamen { get; set; }
           public double? Precio { get; set; }
       }

       public class ReportResumeCaja
       {
           public string mes { get; set; }
           public decimal? totalIngreso { get; set; }
           public decimal? totalEgreso { get; set; }
           public decimal? totalSaldo { get; set; }
           public List<DetalleResumenCaja> detalle{ get; set; }
       }

       public class DetalleResumenCaja
       {
           public DateTime? Fecha { get; set; }
           public int? TipoDocumento { get; set; }
           public decimal? Ingreso { get; set; }
           public decimal? Egreso { get; set; }
           public decimal? Saldo { get; set; }
       }

       public class ResumenTipoPago
       {
           public string grupo { get; set; }
           public int IdCondicionPago { get; set; }
           public int IdFormaPago { get; set; }
           public decimal? ImporteTotal { get; set; }
           public List<DetalleResumenTipoPago> Cobranzas { get; set; }
       }

       public class DetalleResumenTipoPago
       {
           public DateTime? Fecha { get; set; }
           public string Comprobante { get; set; }
           public string Empresa { get; set; }
           public decimal Importe { get; set; }
           public string NroLiquidacion { get; set; }
           public List<Servicios> Servicios { get; set; }
       }

       public class Servicios
       {
           public string NroLiquidacion { get; set; }
           public string Trabajador { get; set; }
           public string ServicioId { get; set; }
           public DateTime? FechaExamen { get; set; }
           public decimal? CostoExamen{ get; set; }
           public string Compania { get; set; }
           public string Contratista { get; set; }
           public string UsuarioRecepcion { get; set; }
       }

    }
}
