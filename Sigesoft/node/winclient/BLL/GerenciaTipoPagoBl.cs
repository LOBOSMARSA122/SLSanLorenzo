using System;
using System.Collections.Generic;
using System.Linq;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BLL
{
    public class GerenciaTipoPagoBl
    {
        public List<GerenciaTipoPago> Filter(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.gerenciatipopago(startDate, endDate, -1) 
                            select new GerenciaTipoPago
                            {
                                IdCondicionPago = a.IdCondicionPago,
                                CondicionPago = a.CondicionPago,
                                //IdFormaPago = a.IdFormaPago,
                                //FormaPago = a.FormaPago,
                                FechaFactura = a.FechaFactura,
                                Comprobante = a.Comprobante,
                                Empresa = a.Empresa,
                                Importe = a.Importe,
                                ServiceId = a.ServiceId,
                                Trabajador = a.Trabajador,
                                FechaServicio = a.FechaServicio,
                                Compania = a.Compania,
                                Contratista = a.Contratista,
                                CostoExamen = a.CostoExamen,
                                TipoEso = a.TipoEso,
                                Agrupador = a.Agrupador
                            }).ToList();

                return query;
            }
            catch (Exception )
            {
                return null;
            }
        }

        public List<GerenciaTreeTipoPago> Agrupador(List<GerenciaTipoPago> data)
        {
            var list = new List<GerenciaTreeTipoPago>();

            var oGerenciaTreeTipoPago = new GerenciaTreeTipoPago();
            var countEgresos = data.FindAll(p => p.Agrupador == "EGRESOS").ToList();
            oGerenciaTreeTipoPago.Total = countEgresos.Count;
            oGerenciaTreeTipoPago.Agrupador = "EGRESOS";
            var totalEgresos = countEgresos.Sum(s => s.Importe);
            oGerenciaTreeTipoPago.Total = totalEgresos;
            list.Add(oGerenciaTreeTipoPago);
            
            oGerenciaTreeTipoPago = new GerenciaTreeTipoPago();
            var countIngresos = data.FindAll(p => p.Agrupador == "INGRESOS").GroupBy(g => g.Comprobante).Select(s => s.First()).ToList();
            oGerenciaTreeTipoPago.Total = countIngresos.Count;
            oGerenciaTreeTipoPago.Agrupador = "INGRESOS";
            oGerenciaTreeTipoPago.Total = countIngresos.Sum(s => s.Importe) - totalEgresos;
            list.Add(oGerenciaTreeTipoPago);

            return list;
        }

        public List<GerenciaTreeTipoPago> ProcessDataTreeView(List<GerenciaTipoPago> data)
        {
            var list = Agrupador(data);
            var empresasIngreso = data.FindAll(p => p.Agrupador == "INGRESOS").ToList();
            var comprobantesIngreso = data.FindAll(p => p.Agrupador == "INGRESOS").GroupBy(g => g.Comprobante).Select(s => s.First()).ToList();
            var tiposIngreso = empresasIngreso.GroupBy(p => p.IdCondicionPago).Select(s => s.First()).ToList();

            var tipos = new List<Tipo>();
            foreach (var tipoIngreso in tiposIngreso)
            {
                var oTipo = new Tipo();
                oTipo.Agrupador = "INGRESOS";
                oTipo.Cantidad = comprobantesIngreso.FindAll(p => p.IdCondicionPago == tipoIngreso.IdCondicionPago).Count;
                oTipo.TipoPago = tipoIngreso.CondicionPago;
                //var importeAgrupadoTipo = comprobantesIngreso.GroupBy(p => new { p.IdCondicionPago, p.Comprobante }).Select(s => s.First()).ToList();
                var totalTipoPago = comprobantesIngreso.FindAll(p => p.IdCondicionPago == tipoIngreso.IdCondicionPago).ToList().Sum(s => s.Importe);
                oTipo.Total = decimal.Parse(totalTipoPago.ToString());
                tipos.Add(oTipo);
            }

            list.Find(p => p.Agrupador == "INGRESOS").Tipos = tipos;

                
            var empresasEgreso = data.FindAll(p => p.Agrupador == "EGRESOS").ToList();
            var comprobantesEgreso = data.FindAll(p => p.Agrupador == "EGRESOS").GroupBy(g => g.Comprobante).Select(s => s.First()).ToList();
            var tiposEgreso = empresasEgreso.GroupBy(p => p.IdCondicionPago).Select(s => s.First()).ToList();
            tipos = new List<Tipo>();
            foreach (var tipoEgreso in tiposEgreso)
            {
                var oTipo = new Tipo();
                oTipo.Agrupador = "EGRESOS";
                oTipo.Cantidad = comprobantesEgreso.FindAll(p => p.IdCondicionPago == tipoEgreso.IdCondicionPago).Count;
                oTipo.TipoPago = tipoEgreso.CondicionPago;
                var totalTipoPago = comprobantesEgreso.FindAll(p => p.IdCondicionPago == tipoEgreso.IdCondicionPago).ToList().Sum(s => s.Importe);
                oTipo.Total = decimal.Parse(totalTipoPago.ToString());
                tipos.Add(oTipo);
            }

            list.Find(p => p.Agrupador == "EGRESOS").Tipos = tipos;


            return list;















            //var list = new List<GerenciaTreeTipoPago>();

            //var oGerenciaTreeTipoPago = new GerenciaTreeTipoPago();

            //oGerenciaTreeTipoPago.Agrupador = "INGRESOS";
            //var importeAgrupado = data.GroupBy(p => p.Comprobante).Select(s => s.First()).ToList();
            //oGerenciaTreeTipoPago.Total = decimal.Parse(importeAgrupado.Sum(p => p.Importe).ToString());

            //var objTipos = data.ToList().GroupBy(p => p.IdCondicionPago).Select(s => s.First()).ToList();
            //var tipos = new List<Tipo>();
            //foreach (var objTipo in objTipos)
            //{
            //    var oTipo = new Tipo();
            //    oTipo.Cantidad = importeAgrupado.FindAll(p => p.IdCondicionPago == objTipo.IdCondicionPago).Count;
            //    oTipo.TipoPago = objTipo.CondicionPago;
            //    var importeAgrupadoTipo = data.GroupBy(p => new { p.IdCondicionPago, p.Comprobante }).Select(s => s.First()).ToList();
            //    var totalTipoPago = importeAgrupadoTipo.FindAll(p => p.IdCondicionPago == objTipo.IdCondicionPago).ToList().Sum(s => s.Importe);
            //    oTipo.Total = decimal.Parse(totalTipoPago.ToString());
            //    tipos.Add(oTipo);

            //    var objEmpresas = data.FindAll(p => p.IdCondicionPago == objTipo.IdCondicionPago).ToList().GroupBy(g => g.Empresa).Select(s => s.First());

            //    var empresas = new List<Empresa>();
            //    foreach (var objEmpresa in objEmpresas)
            //    {
            //        var oEmpresa = new Empresa();
            //        oEmpresa.TipoPago = objTipo.CondicionPago;
            //        oEmpresa.Cantidad = data.FindAll(p => p.Empresa == objEmpresa.Empresa && p.IdCondicionPago == objTipo.IdCondicionPago).Count;
            //        oEmpresa.EmpresaNombre = objEmpresa.Empresa;

            //        var xx = data.ToList().FindAll(p => p.Empresa == objEmpresa.Empresa && p.IdCondicionPago == objTipo.IdCondicionPago);
            //        var importeAgrupadoTipoEmpresa = xx.GroupBy(p => p.IdCondicionPago == objTipo.IdCondicionPago && p.Comprobante == objEmpresa.Empresa).Select(s => s.First()).ToList();
            //        var totalEmpresas = importeAgrupadoTipoEmpresa.FindAll(p => p.Empresa == objEmpresa.Empresa && p.IdCondicionPago == objTipo.IdCondicionPago).ToList().Sum(s => s.Importe);
            //        oEmpresa.Total = decimal.Parse(totalEmpresas.ToString());
            //        empresas.Add(oEmpresa);

            //        var objTiposEsos = data.ToList().FindAll(p => p.Empresa == objEmpresa.Empresa && p.IdCondicionPago == objTipo.IdCondicionPago);
            //        objTiposEsos = objTiposEsos.GroupBy(p => p.TipoEso == objTipo.TipoEso).Select(s => s.First()).ToList().FindAll(f => f.ServiceId != null);
            //        var tiposEsos = new List<TipoEso>();

            //        foreach (var objTiposEso in objTiposEsos)
            //        {
            //            var oTipoEso = new TipoEso();
            //            oTipoEso.TipoPago = objEmpresa.CondicionPago;
            //            oTipoEso.EmpresaNombre = objEmpresa.Empresa;
            //            oTipoEso.Cantidad = data.FindAll(p => p.Empresa == objEmpresa.Empresa && p.IdCondicionPago == objTipo.IdCondicionPago && p.TipoEso == objTiposEso.TipoEso).Count;
            //            if (oTipoEso.Cantidad == 0) continue; 
            //            oTipoEso.Eso = objTiposEso.TipoEso;


            //            var x = data.ToList().FindAll(p => p.Empresa == objEmpresa.Empresa && p.IdCondicionPago == objTipo.IdCondicionPago);
            //            var importeAgrupadoTipoEmpresaTipoEso = x.GroupBy(p => p.IdCondicionPago == objTipo.IdCondicionPago && p.Comprobante == objEmpresa.Empresa && p.TipoEso == objTiposEso.TipoEso).Select(s => s.First()).ToList();
            //            var totalTipoEso = importeAgrupadoTipoEmpresaTipoEso.FindAll(p => p.Empresa == objEmpresa.Empresa && p.IdCondicionPago == objTipo.IdCondicionPago).ToList().Sum(s => s.Importe);

            //            oTipoEso.Total = decimal.Parse(totalTipoEso.ToString());
            //            tiposEsos.Add(oTipoEso);
            //        }

            //        oEmpresa.TipoEsos = tiposEsos;
            //    }

            //    oTipo.Empresas = empresas;
            //}
            //oGerenciaTreeTipoPago.Tipos = tipos;

            //list.Add(oGerenciaTreeTipoPago);
            //return list;
        }
    }
}
