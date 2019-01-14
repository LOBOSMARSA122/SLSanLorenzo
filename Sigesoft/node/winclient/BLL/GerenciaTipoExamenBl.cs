using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Server.WebClientAdmin.BE;

namespace Sigesoft.Node.WinClient.BLL
{
    public class GerenciaTipoExamenBl
    {
        public List<GerenciaTipoExamen> Filter(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.gerenciatipoexamen(startDate, endDate, 1)
                             select new GerenciaTipoExamen
                            {
                                FechaFactura = a.FechaFactura,
                                Comprobante = a.Comprobante,
                                Empresa = a.Empresa,
                                ServiceId = a.ServiceId,
                                Trabajador = a.Trabajador,
                                FechaServicio = a.FechaServicio,
                                CostoExamen = a.CostoExamen,
                                TipoEso = a.TipoEso,
                                UsuarioAgenda = a.UsuarioAgenda
                            }).ToList();

                return query;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<GerenciaTreeTipoExamen> ProcessDataTreeView(List<GerenciaTipoExamen> data)
        {
            var list = Agrupador(data);
            Perfiles(data, list);
            return list;
        }

        public List<GerenciaTreeTipoExamen> Agrupador(List<GerenciaTipoExamen> data)
        {
            var list = new List<GerenciaTreeTipoExamen>();
            var oGerenciaTreeTipoExamen = new GerenciaTreeTipoExamen();
            oGerenciaTreeTipoExamen.Cantidad = data.Count;
            oGerenciaTreeTipoExamen.Agrupador = "EXAMENES OCUPACIONALES";
            oGerenciaTreeTipoExamen.Total = decimal.Parse(data.Sum(s => s.CostoExamen).ToString());
            list.Add(oGerenciaTreeTipoExamen);

            return list;
        }

        public List<GerenciaTreeTipoExamen> Perfiles(List<GerenciaTipoExamen> data, List<GerenciaTreeTipoExamen> list)
        {
            var perfiles = new List<Perfil>();
            var tiposEso = data.GroupBy(g => g.TipoEso).Select(s => s.First()).ToList();
            foreach (var tipoEso in tiposEso)
            {
                var oGerenciaTipoExamen = new Perfil();
                oGerenciaTipoExamen.TipoEso = tipoEso.TipoEso;
                oGerenciaTipoExamen.Cantidad = data.FindAll(p => p.TipoEso == tipoEso.TipoEso).Count;
                oGerenciaTipoExamen.Total = decimal.Parse(data.FindAll(p => p.TipoEso == tipoEso.TipoEso).Sum(s => s.CostoExamen).ToString());
                perfiles.Add(oGerenciaTipoExamen);

                
                var empresas = data.FindAll(p => p.TipoEso == tipoEso.TipoEso).GroupBy(g => g.Empresa).Select(s => s.First()).ToList();
                var emp = new List<EmpresaTipoEso>();
                foreach (var empresa in empresas)
                {
                    var oemp = new EmpresaTipoEso();
                    oemp.TipoEso = tipoEso.TipoEso;
                    oemp.Nombre = empresa.Empresa;
                    oemp.Cantidad = data.FindAll(p => p.Empresa == empresa.Empresa && p.TipoEso == tipoEso.TipoEso).Count;
                    oemp.Total = decimal.Parse(data.FindAll(p => p.Empresa == empresa.Empresa && p.TipoEso == tipoEso.TipoEso).Sum(s => s.CostoExamen).ToString());
                    emp.Add(oemp);
                }

                oGerenciaTipoExamen.Empresas = emp;
            }
            list[0].Perfiles = perfiles;

            return list;

        }

        //public List<GerenciaTreeTipoExamen> Empresas(List<GerenciaTipoExamen> data, List<GerenciaTreeTipoExamen> list)
        //{
        //    var empresas = new List<EmpresaTipoEso>();

        //    foreach (var perfil in perfiles)
        //    {
        //        //    var empresasPerfil = data.FindAll(p => p.TipoEso == perfil.TipoEso).GroupBy(g => g.Empresa).Select(s => s.First()).ToList();
        //        //    var listEmpresas = new List<EmpresaTipoEso>();
        //        //    foreach (var empresa in empresasPerfil)
        //        //    {
        //        //        var oEmpresa = new EmpresaTipoEso();
        //        //        oEmpresa.Nombre = empresa.Empresa;
        //        //        oEmpresa.Cantidad = data.FindAll(p => p.Empresa == oEmpresa.Nombre && p.TipoEso == perfil.TipoEso).Count;
        //        //        oEmpresa.Total = decimal.Parse(data.FindAll(p => p.Empresa == oEmpresa.Nombre && p.TipoEso == perfil.TipoEso).Sum(s => s.CostoExamen).ToString());
        //        //        listEmpresas.Add(oEmpresa);
        //        //    }

        //        //    perfil.Empresas = listEmpresas;
        //    }
        //}
    }
}
