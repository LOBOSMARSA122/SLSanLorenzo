using System;
using System.Collections.Generic;
using System.Linq;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Server.WebClientAdmin.BE;

namespace Sigesoft.Node.WinClient.BLL
{
    public class GerenciaTipoPagoBl
    {
        public List<GerenciaTipoPago> Filter(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.gerenciatipopago(startDate, endDate, 1) 
                            select new GerenciaTipoPago
                            {
                                IdCondicionPago = a.IdCondicionPago,
                                CondicionPago = a.CondicionPago,
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
                var totalTipoPago = comprobantesIngreso.FindAll(p => p.IdCondicionPago == tipoIngreso.IdCondicionPago).ToList().Sum(s => s.Importe);
                oTipo.Total = decimal.Parse(totalTipoPago.ToString());

                var empresas = new List<Empresa>();

                var empresasAgrupadas = empresasIngreso.FindAll(f => f.IdCondicionPago == tipoIngreso.IdCondicionPago).GroupBy(g => g.Empresa).Select(s => s.First()).ToList();
                foreach (var empresa in empresasAgrupadas)
                {
                    var oEmpresa = new Empresa();
                    oEmpresa.Cantidad =  empresasIngreso.FindAll(p => p.Empresa == empresa.Empresa && p.IdCondicionPago == empresa.IdCondicionPago).Count;
                    oEmpresa.EmpresaNombre = empresa.Empresa;
                    oEmpresa.Total = decimal.Parse(empresasAgrupadas.FindAll(p => p.Empresa == empresa.Empresa && p.IdCondicionPago == empresa.IdCondicionPago).Sum(s => s.Importe).ToString());
                    oEmpresa.TipoPago = tipoIngreso.CondicionPago;
                    empresas.Add(oEmpresa);

                    var tiposEsos =  new List<TipoEso>();
                    var perfilesPorEmpresa = empresasIngreso.FindAll(p => p.Empresa == empresa.Empresa && p.IdCondicionPago == empresa.IdCondicionPago).GroupBy(g => g.TipoEso).Select(s => s.First());
                    foreach (var perfilEmpresa in perfilesPorEmpresa)
                    {
                        var oTipoEso = new TipoEso();
                        oTipoEso.TipoPago = empresa.CondicionPago;
                        oTipoEso.EmpresaNombre = empresa.Empresa;
                        oTipoEso.Cantidad = data.FindAll(f =>
                            f.CondicionPago == empresa.CondicionPago && f.Empresa == empresa.Empresa &&
                            f.TipoEso == perfilEmpresa.TipoEso).Count;
                        //if (oTipoEso.Cantidad == 0) continue;
                        oTipoEso.Eso = perfilEmpresa.TipoEso;

                        var totalTipoEso = data.FindAll(f =>
                            f.CondicionPago == empresa.CondicionPago && f.Empresa == empresa.Empresa &&
                            f.TipoEso == perfilEmpresa.TipoEso).Sum(s => s.CostoExamen);

                        oTipoEso.Total = decimal.Parse(totalTipoEso.ToString());

                        tiposEsos.Add(oTipoEso);
                    }

                    oEmpresa.TipoEsos = tiposEsos;
                }

                oTipo.Empresas = empresas;

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

        }

        public List<GerenciaTreeTipoPago> Agrupador_(List<GerenciaTipoPago> data)
        {
            var list = new List<GerenciaTreeTipoPago>();

            var oGerenciaTreeTipoPago = new GerenciaTreeTipoPago();
            var countEgresos = data.FindAll(p => p.Agrupador == "EGRESOS").ToList();
            oGerenciaTreeTipoPago.Cantidad = countEgresos.Count;
            oGerenciaTreeTipoPago.Agrupador = "EGRESOS";
            var totalEgresos = countEgresos.Sum(s => s.Importe);
            oGerenciaTreeTipoPago.Total = totalEgresos;
            list.Add(oGerenciaTreeTipoPago);

            oGerenciaTreeTipoPago = new GerenciaTreeTipoPago();
            var countIngresos = data.FindAll(p => p.Agrupador == "INGRESOS").GroupBy(g => g.Comprobante).Select(s => s.First()).ToList();
            oGerenciaTreeTipoPago.Cantidad = countIngresos.Count;
            oGerenciaTreeTipoPago.Agrupador = "INGRESOS";
            oGerenciaTreeTipoPago.Total = countIngresos.Sum(s => s.Importe);
            list.Add(oGerenciaTreeTipoPago);

            return list;
        }

        public List<GerenciaTreeTipoPago> Agrupador(List<GerenciaTipoPago> data)
        {
            var list = new List<GerenciaTreeTipoPago>();

            string[] agrupadores = { "INGRESOS","EGRESOS" };

            foreach (var agrupador in agrupadores)
            {
                var oGerenciaTreeTipoPago = new GerenciaTreeTipoPago();

                var comprobantes = data.FindAll(p => p.Agrupador == agrupador).GroupBy(g => g.Comprobante)
                    .Select(s => s.First()).ToList();
                oGerenciaTreeTipoPago.Agrupador = agrupador;
                oGerenciaTreeTipoPago.Cantidad = comprobantes.Count;
                oGerenciaTreeTipoPago.Total = comprobantes.Sum(s => s.Importe);
                list.Add(oGerenciaTreeTipoPago);

            }

            return list;
        }
    }
}
