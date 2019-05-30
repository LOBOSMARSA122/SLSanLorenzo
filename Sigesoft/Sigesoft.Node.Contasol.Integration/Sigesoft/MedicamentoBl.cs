using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using SAMBHS.Almacen.BL;
using SAMBHS.Common.BE;
using SAMBHS.Common.DataModel;
using Sigesoft.Common;
using Sigesoft.Node.Contasol.Integration.Contasol.Models;
using ConnectionState = System.Data.ConnectionState;
using Sigesoft.Node.WinClient.BE;

namespace Sigesoft.Node.Contasol.Integration
{
    public class MedicamentoBl
    {
        public List<MedicamentoDto> GetListMedicamentos(ref OperationResult pobjOperationResult, string nombre,
            string accionFarmaco)
        {
            try
            {
                nombre = string.IsNullOrWhiteSpace(nombre) ? "null" : "'%" + nombre.Trim().ToLower() + "%'";
                accionFarmaco = string.IsNullOrWhiteSpace(accionFarmaco) ? "null" : "'%" + accionFarmaco.Trim().ToLower() + "%'";
                var periodo = DateTime.Today.Year.ToString();

                using (var cnx = ConnectionHelper.GetConnection)
                {
                    if (cnx.State != ConnectionState.Open) cnx.Open();

                    var query =
                        "select \"v_IdProductoDetalle\" as \"IdProductoDetalle\" , \"v_CodInterno\" as \"CodInterno\", \"v_Descripcion\" as \"Nombre\",  " +
                        "\"v_Presentacion\" as \"Presentacion\", \"v_Concentracion\" as \"Concentracion\",  " +
                        "\"v_Ubicacion\" as \"Ubicacion\", " +
                        "p.\"v_IdLinea\" as \"IdLinea\", " +
                        "p.\"v_AccionFarmaco\" as \"AccionFarmaco\", p.\"v_PrincipioActivo\" as \"PrincipioActivo\", " +
                        "p.\"v_Laboratorio\" as \"Laboratorio\", p.\"d_PrecioVenta\" as \"PrecioVenta\" " +
                        ",pa.\"d_StockActual\" as \"Stock\" " + ", p.\"d_PrecioMayorista\" as \"d_PrecioMayorista\" " +
                        "from producto p " +
                        "join productodetalle pd on p.\"v_IdProducto\" = pd.\"v_IdProducto\" " +
                        "join productoalmacen pa on pd.\"v_IdProductoDetalle\" = pa.\"v_ProductoDetalleId\" " +
                        "where (" + nombre + " is null or lower(p.\"v_Descripcion\") like " + nombre + ") and p.i_EsActivo =1 and pa.d_StockActual > 0 and " +
                        "(" + accionFarmaco + " is null or lower(p.\"v_AccionFarmaco\") like " + accionFarmaco + ") " +
                        "and pa.i_IdAlmacen = 1 and p.\"i_Eliminado\" = 0 and pa.v_Periodo=2019 ;";

                    var listado = cnx.Query<MedicamentoDto>(query).ToList();

                    pobjOperationResult.Success = 1;
                    return listado;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "MedicamentoBl.GetListMedicamentos()";
                return null;
            }
        }

        //public string NombreProducto(string IdProductoDetalle)
        //{
        //    try
        //    {
        //        using (var cnx = ConnectionHelper.GetConnection)
        //        {
        //            if (cnx.State != ConnectionState.Open) cnx.Open();

        //            var query = " select IdProductoDetalle " +
        //                        " from productodetalle pd " +
        //                        " inner join producto p on pd.v_IdProducto = p.v_IdProducto " +
        //                        " where pd.v_IdProductoDetalle = '" + IdProductoDetalle + "'";

        //            var listado = cnx.Query<MedicamentoDto>(query).ToList();

        //            return listado[0].Nombre;
        //        }               
        //    }
        //    catch (Exception ex)
        //    {
                
        //        throw;
        //    }
        //}

        public void AddUpdateMedicamento(ref OperationResult pobjOperationResult, MedicamentoDto pobjDto)
        {
            try
            {
                using (var dbContext = ConnectionHelper.ContasolContext)
                {
                    var entidad = dbContext.productodetalle.FirstOrDefault(p => p.v_IdProductoDetalle == pobjDto.IdProductoDetalle);
                    
                    var nombre = pobjDto.Nombre.Trim();
                    var presentacion = pobjDto.Presentacion.Trim();
                    var concentracion = pobjDto.Concentracion.Trim();
                    if (entidad == null)
                    {
                        var yaExiste =
                            dbContext.producto.Any(p => p.v_Descripcion.Trim().Equals(nombre) &&
                                                            p.v_Presentacion.Trim().Equals(presentacion) && p
                                                                .v_Concentracion.Trim()
                                                                .Equals(concentracion));

                        if (yaExiste) throw new Exception("El medicamento ya fue registrado anteriormente!");
                        var productoToInsert = new productoDto
                        {
                            v_Descripcion = nombre,
                            v_Presentacion = presentacion,
                            v_Concentracion = concentracion,
                            v_AccionFarmaco = pobjDto.AccionFarmaco,
                            v_Ubicacion = pobjDto.Ubicacion,
                            i_IdProveedor = -1,
                            i_IdTipoProducto = 1,
                            i_IdUsuario = -1,
                            i_EsAfectoDetraccion = 0,
                            i_IdUnidadMedida = pobjDto.IdUnidadMedida,
                            d_Empaque = 1,
                            v_CodInterno = pobjDto.CodInterno,
                            i_EsActivo = 1,
                            i_EsActivoFijo = 0,
                            i_EsLote = 0,
                            i_EsServicio = 0,
                            i_SolicitarNroLoteIngreso = 1,
                            i_SolicitarNroLoteSalida = 1,
                            i_NombreEditable = 0,
                            i_ValidarStock = 1,
                            v_IdLinea = pobjDto.IdLinea
                        };
                        var op = new SAMBHS.Common.Resource.OperationResult();
                        var productoBl = new ProductoBL();
                        productoBl.InsertarProducto(ref op, productoToInsert, null, dbContext);
                    }
                    else
                    {
                        var prodOriginal = entidad.producto;
                        prodOriginal.v_Descripcion = nombre;
                        prodOriginal.v_Presentacion = presentacion;
                        prodOriginal.v_Concentracion = concentracion;
                        prodOriginal.v_AccionFarmaco = pobjDto.AccionFarmaco;
                        prodOriginal.v_Ubicacion = pobjDto.Ubicacion;
                        prodOriginal.i_IdUnidadMedida = pobjDto.IdUnidadMedida;
                        prodOriginal.d_Empaque = 1;
                        prodOriginal.v_CodInterno = pobjDto.CodInterno;
                        prodOriginal.i_EsActivo = 1;
                        prodOriginal.i_EsActivoFijo = 0;
                        prodOriginal.i_EsLote = 0;
                        prodOriginal.i_EsServicio = 0;
                        prodOriginal.i_SolicitarNroLoteIngreso = 1;
                        prodOriginal.i_SolicitarNroLoteSalida = 1;
                        dbContext.producto.ApplyCurrentValues(prodOriginal);
                    }
                    dbContext.SaveChanges();
                    pobjOperationResult.Success = 1;
                }
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ErrorMessage = ex.Message;
                pobjOperationResult.ExceptionMessage = ex.InnerException != null
                    ? ex.InnerException.Message
                    : string.Empty;
                pobjOperationResult.AdditionalInformation = "MedicamentoBl.AddUpdateMedicamento()";
            }
        }

        //public medicamentosDto GetMedicamentoById(ref OperationResult pobjOperationResult, int recipeId)
        //{
        //    try
        //    {
        //        using (var dbContext = new SigesoftEntitiesModel())
        //        {
        //            var entidad = dbContext.medicamentos.FirstOrDefault(p => p.i_MedicamentoId == recipeId);
        //            if (entidad == null) throw new Exception("El objeto ya no existe!");
        //            pobjOperationResult.Success = 1;
        //            return entidad.ToDTO();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        pobjOperationResult.Success = 0;
        //        pobjOperationResult.ErrorMessage = ex.Message;
        //        pobjOperationResult.ExceptionMessage = ex.InnerException != null
        //            ? ex.InnerException.Message
        //            : string.Empty;
        //        pobjOperationResult.AdditionalInformation = "medicamentosBl.GetMedicamentoById()";
        //        return null;
        //    }
        //}

       //public class FacturaCobranza
       // {
       //     public string v_DocumentoRef { get; set; }
       // }

        public FacturaCobranza ObtnerNroFacturaCobranza(string nroFactura)
        {
            var obj = nroFactura.Split('-');
            var serie = obj[0].ToString();
            var correlativo = obj[1].ToString();

            using (var cnx = ConnectionHelper.GetConnection)
            {
                if (cnx.State != ConnectionState.Open) cnx.Open();

                var query = "select " +
                            " v.t_InsertaFecha AS FechaCreacion, " +
                            " v.t_FechaVencimiento AS FechaVencimiento, " +
                            " v.v_IdVenta, " +
                            " v.d_Total AS NetoXCobrar, " +
                            "  v.v_SerieDocumento + '-' + v.v_CorrelativoDocumento AS NroComprobante, " +
                            "  Sum(cd.d_ImporteSoles) AS TotalPagado, " +
                            "  STUFF((    SELECT ',' + SUB.v_DocumentoRef AS [text()] " +
                            "                         FROM cobranzadetalle SUB " +
                            "                      WHERE SUB.v_IdVenta = v.v_IdVenta " +
                            "                    FOR XML PATH('') " +
                            "              ), 1, 1, '' ) AS DocuemtosReferencia " +
                            " from venta v " +
                            " inner join cobranzadetalle cd on v.v_IdVenta = cd.v_IdVenta " +
                            " where cd.i_Eliminado = 0 and  v.v_SerieDocumento='" + serie + "' and v.v_CorrelativoDocumento='" + correlativo + "'" +
                            " group by v.v_IdVenta, v.d_Total,v.t_InsertaFecha, v.t_FechaVencimiento,v.v_SerieDocumento,v.v_CorrelativoDocumento";

                var result = cnx.Query<FacturaCobranza>(query).FirstOrDefault();                             

                if (result != null)
                {
                    return result;
                }

                else
                {
                    var query1 = "select v.v_IdVenta,  " +
                           " v.t_InsertaFecha AS FechaCreacion, " +
                           " v.t_FechaVencimiento AS FechaVencimiento,  " +
                           " v.d_Total AS NetoXCobrar,  " +
                           " 0 AS TotalPagado,  " +
                           " '---' AS DocuemtosReferencia, " +
                           "  v.v_SerieDocumento + '-' + v.v_CorrelativoDocumento AS NroComprobante " +
                           " from venta v  " +
                           " where v.v_SerieDocumento='" + serie + "' and v.v_CorrelativoDocumento='" + correlativo + "'";

                    var result2 = cnx.Query<FacturaCobranza>(query1).FirstOrDefault();
                    return result2;
                }
                
            }

            //return null;
           
        }

        public List<FacturaCobranza> EmpresaDeudora(string rucEmpresa)
        {

            using (var cnx = ConnectionHelper.GetConnection)
            {
                if (cnx.State != ConnectionState.Open) cnx.Open();

                var query = "select " +
                 " v.t_InsertaFecha AS FechaCreacion, " +
                 " v.t_FechaVencimiento AS FechaVencimiento, " +
                 " v.v_IdVenta, " +
                "  Sum(d_Total) / CASE WHEN (select count(*) from cobranzadetalle where v_IdVenta = v.v_IdVenta)= 0 THEN 1 ELSE (select count(*) from cobranzadetalle where v_IdVenta = v.v_IdVenta)END AS NetoXCobrar,   " +
                "  v.v_SerieDocumento + '-' + v.v_CorrelativoDocumento AS NroComprobante, " +
                 " Sum(cd.d_ImporteSoles) AS TotalPagado, " +
                "  CASE WHEN (Sum(d_Total)/    CASE WHEN (select count(*)  from cobranzadetalle where v_IdVenta = v.v_IdVenta) = 0 THEN 1 ELSE 	(select count(*) from cobranzadetalle where v_IdVenta = v.v_IdVenta) END ) -  Sum(cd.d_ImporteSoles)  = 0 THEN 'NO DEBE' ELSE 'DEBE' END AS Condicion  " +
                " from venta v " +
                " inner join cliente c on c.v_IdCliente =  v.v_IdCliente " +
                " left join cobranzadetalle cd on v.v_IdVenta = cd.v_IdVenta " +
                " where c.v_NroDocIdentificacion = '" + rucEmpresa + "' and v.i_Eliminado = 0" +
                " group by v.v_IdVenta, v.d_Total,v.t_InsertaFecha, v.t_FechaVencimiento,v.v_SerieDocumento,v.v_CorrelativoDocumento";

                var result = cnx.Query<FacturaCobranza>(query).ToList();

                return result;

                //var query = "select " +
                // " v.t_InsertaFecha AS FechaCreacion, " +
                // " v.t_FechaVencimiento AS FechaVencimiento, " +
                // " v.v_IdVenta, " +
                //"  Sum(d_Total) / (select count(*) from cobranzadetalle where v_IdVenta = v.v_IdVenta) AS NetoXCobrar, " +
                //"  v.v_SerieDocumento + '-' + v.v_CorrelativoDocumento AS NroComprobante, " +
                // " Sum(cd.d_ImporteSoles) AS TotalPagado, " +
                //"  CASE WHEN Sum(d_Total)/ (select count(*) from cobranzadetalle where v_IdVenta = v.v_IdVenta) -  Sum(cd.d_ImporteSoles)  = 0 THEN 'NO DEBE' ELSE 'DEBE' END AS Condicion " +
                //" from venta v " +
                //" inner join cliente c on c.v_IdCliente =  v.v_IdCliente " +
                //" inner join cobranzadetalle cd on v.v_IdVenta = cd.v_IdVenta " +
                //" where c.v_NroDocIdentificacion = '" + rucEmpresa + "' " +
                //" group by v.v_IdVenta, v.d_Total,v.t_InsertaFecha, v.t_FechaVencimiento,v.v_SerieDocumento,v.v_CorrelativoDocumento";

            }
        }

        public List<FacturaCobranza> EmpresaDeudora(string correlativoDocumento, string serieDocumento)
        {

            using (var cnx = ConnectionHelper.GetConnection)
            {
                if (cnx.State != ConnectionState.Open) cnx.Open();

                var query = "select " +
                 " v.t_InsertaFecha AS FechaCreacion, " +
                 " v.t_FechaVencimiento AS FechaVencimiento, " +
                 " v.v_IdVenta, " +
                "  Sum(d_Total) / CASE WHEN (select count(*) from cobranzadetalle where v_IdVenta = v.v_IdVenta)= 0 THEN 1 ELSE (select count(*) from cobranzadetalle where v_IdVenta = v.v_IdVenta)END AS NetoXCobrar,   " +
                "  v.v_SerieDocumento + '-' + v.v_CorrelativoDocumento AS NroComprobante, " +
                 " Sum(cd.d_ImporteSoles) AS TotalPagado, " +
                "  CASE WHEN (Sum(d_Total)/    CASE WHEN (select count(*)  from cobranzadetalle where v_IdVenta = v.v_IdVenta) = 0 THEN 1 ELSE 	(select count(*) from cobranzadetalle where v_IdVenta = v.v_IdVenta) END ) -  Sum(cd.d_ImporteSoles)  = 0 THEN 'NO DEBE' ELSE 'DEBE' END AS Condicion  " +
                " from venta v " +
                " inner join cliente c on c.v_IdCliente =  v.v_IdCliente " +
                " left join cobranzadetalle cd on v.v_IdVenta = cd.v_IdVenta " +
                " where  v.v_CorrelativoDocumento = '" + correlativoDocumento + "' and v.v_SerieDocumento='" + serieDocumento + "' and v.i_Eliminado = 0 " +
                " group by v.v_IdVenta, v.d_Total,v.t_InsertaFecha, v.t_FechaVencimiento,v.v_SerieDocumento,v.v_CorrelativoDocumento";

                var result = cnx.Query<FacturaCobranza>(query).ToList();

                return result;


            }
        }

        public List<FacturaCobranza> EmpresaDeudora_Fechas_Fac(string rucEmpresa, DateTime? inicio, DateTime? fin)
        {

            using (var cnx = ConnectionHelper.GetConnection)
            {
                if (cnx.State != ConnectionState.Open) cnx.Open();


                var query = "select " +
                 " v.t_InsertaFecha AS FechaCreacion, " +
                 " v.t_FechaVencimiento AS FechaVencimiento, " +
                 " v.v_IdVenta, " +
                "  Sum(d_Total) / CASE WHEN (select count(*) from cobranzadetalle where v_IdVenta = v.v_IdVenta)= 0 THEN 1 ELSE (select count(*) from cobranzadetalle where v_IdVenta = v.v_IdVenta)END AS NetoXCobrar,   " +
                "  v.v_SerieDocumento + '-' + v.v_CorrelativoDocumento AS NroComprobante, " +
                 " Sum(cd.d_ImporteSoles) AS TotalPagado, " +
                "  CASE WHEN (Sum(d_Total)/    CASE WHEN (select count(*)  from cobranzadetalle where v_IdVenta = v.v_IdVenta) = 0 THEN 1 ELSE 	(select count(*) from cobranzadetalle where v_IdVenta = v.v_IdVenta) END ) -  Sum(cd.d_ImporteSoles)  = 0 THEN 'NO DEBE' ELSE 'DEBE' END AS Condicion  " +
                " from venta v " +
                " inner join cliente c on c.v_IdCliente =  v.v_IdCliente " +
                " left join cobranzadetalle cd on v.v_IdVenta = cd.v_IdVenta " +
                " where c.v_NroDocIdentificacion = '" + rucEmpresa + "' and v.i_Eliminado = 0  and v.t_InsertaFecha >= '" + inicio.Value.Date.ToString("yyyy-dd-MM") + "' and v.t_InsertaFecha <= '" + fin.Value.Date.ToString("yyyy-dd-MM") + "'" +
                " group by v.v_IdVenta, v.d_Total,v.t_InsertaFecha, v.t_FechaVencimiento,v.v_SerieDocumento,v.v_CorrelativoDocumento";

                var result = cnx.Query<FacturaCobranza>(query).ToList();

                return result;
            }
        }

    }
}
