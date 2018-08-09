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
                        "from producto p " +
                        "join productodetalle pd on p.\"v_IdProducto\" = pd.\"v_IdProducto\" " +
                        "where (" + nombre + " is null or lower(p.\"v_Descripcion\") like " + nombre + ") and " +
                        "(" + accionFarmaco + " is null or lower(p.\"v_AccionFarmaco\") like " + accionFarmaco + ") " +
                        "and p.\"i_Eliminado\" = 0;";

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
    }
}
