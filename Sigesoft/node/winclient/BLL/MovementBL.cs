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
  public class MovementBL
    {
      //Devart.Data.PostgreSql.PgSqlMonitor mon = new Devart.Data.PostgreSql.PgSqlMonitor();

        #region ProductWarehouse

        public productwarehouseDto GetProductWarehouse(ref OperationResult pobjOperationResult, string pintWarehouseId, string pintProductId, string pintOrganizationId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                productwarehouseDto objDtoEntity = null;

                var objEntity = (from a in dbContext.productwarehouse
                                 where a.v_WarehouseId == pintWarehouseId && a.v_ProductId == pintProductId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = productwarehouseAssembler.ToDTO(objEntity);

                pobjOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex); 
                return null;
            }
        }

        public List<ProductWarehouseList> DevolverProductos(string pstrDescripcion, int pstrWarehouseId, int pintRolVenta)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            ProductWarehouseList oProductWarehouseList;
            List<ProductWarehouseList> ListProductWarehouseList = new List<ProductWarehouseList>();


            //var ListaProductos = null;// dbContext.devolverproductos(pstrDescripcion, pstrWarehouseId, pintRolVenta);

            ////if (ListaProductos.Count() > 0)
            ////{
            //    foreach (var item in ListaProductos)
            //    {
            //        oProductWarehouseList = new ProductWarehouseList();
            //        oProductWarehouseList.v_ProductId = item.IdProducto;
            //        oProductWarehouseList.v_ProductName = item.Producto;
            //        oProductWarehouseList.r_StockActual = (float)item.Stock.Value;
            //        oProductWarehouseList.v_GenericName = item.Descripcion;
            //        oProductWarehouseList.v_Presentation = item.Caracteristicas;
            //        oProductWarehouseList.v_CategoriaId = item.CategoriaId;
            //        oProductWarehouseList.d_Comision = item.Comision;
            //        oProductWarehouseList.i_Cuota = item.Cuota;
            //        oProductWarehouseList.d_ValorVenta = item.ValorVenta;
            //        ListProductWarehouseList.Add(oProductWarehouseList);
            //    }
            //    return ListProductWarehouseList;
            //}
            //else
            //{
            return null;
            //}
            

           
        }

        public List<ProductWarehouseList> GetProductWarehousePagedAndFiltered(ref OperationResult pobjOperationResult, int pintPageIndex, int  ?pintResultsPerPage, string pstrFilterExpression, string pstrWarehouseId)
        {
            //mon.IsActive = true;
            try
            {

                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
             
                var query = (from A in dbContext.productwarehouse
                             join B in dbContext.product on A.v_ProductId equals B.v_ProductId
                             join J3 in dbContext.datahierarchy on new { a = B.i_CategoryId.Value, b = 103 } equals new { a = J3.i_ItemId, b = J3.i_GroupId }
                             join D in dbContext.warehouse on A.v_WarehouseId equals D.v_WarehouseId
                             join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                   equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                             from J1 in J1_join.DefaultIfEmpty()

                             join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                             equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                             from J2 in J2_join.DefaultIfEmpty()
                             where A.v_WarehouseId == pstrWarehouseId
                             orderby B.v_Name ascending, J3.v_Value1 ascending
                             select new ProductWarehouseList
                             {
                                 v_WarehouseId = A.v_WarehouseId,
                                 //v_OrganizationId = A.v_OrganizationId,
                                 v_ProductId = A.v_ProductId,
                                 i_CategoryId = B.i_CategoryId.Value,
                                 v_CategoryName = J3.v_Value1,
                                 v_ProductName = B.v_Name,
                                 r_StockActual = A.r_StockActual.Value,
                                 r_StockMin = A.r_StockMin.Value,
                                 r_StockMax = A.r_StockMax.Value,
                                 v_Brand = B.v_Brand,
                                 v_Model = B.v_Model,
                                 v_SerialNumber = B.v_SerialNumber,
                                 v_CreationUser = J1.v_UserName,
                                 v_UpdateUser = J2.v_UserName,
                                 d_CreationDate = A.d_InsertDate,
                                 d_UpdateDate = A.d_UpdateDate,
                                 v_GenericName = B.v_GenericName,
                                 v_ProductCode = B.v_ProductCode,
                                 v_Presentation = B.v_Presentation
                             });

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                List<ProductWarehouseList> objData = query.ToList();
                pobjOperationResult.Success = 1;
                return objData;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public List<ProductWarehouseList> GetProductWarehousePagedAndFiltered1(ref OperationResult pobjOperationResult, int pintPageIndex, int? pintResultsPerPage, string pstrProductId)
        {
            //mon.IsActive = true;
            try
            {

                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                //int intStartRowIndex = pintPageIndex * pintResultsPerPage;

                var query = (from A in dbContext.productwarehouse
                             join B in dbContext.product on A.v_ProductId equals B.v_ProductId
                             join J3 in dbContext.datahierarchy on new { a = B.i_CategoryId.Value, b = 103 } equals new { a = J3.i_ItemId, b = J3.i_GroupId }
                             join D in dbContext.warehouse on A.v_WarehouseId equals D.v_WarehouseId
                             join E in dbContext.organization on D.v_OrganizationId equals E.v_OrganizationId
                             join F in dbContext.location on E.v_OrganizationId equals F.v_OrganizationId
                             join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                   equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                             from J1 in J1_join.DefaultIfEmpty()

                             join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                             equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                             from J2 in J2_join.DefaultIfEmpty()
                             where A.v_ProductId == pstrProductId
                             orderby B.v_Name ascending, J3.v_Value1 ascending
                             select new ProductWarehouseList
                             {
                                 v_OrganizationName = E.v_Name + " / " + F.v_Name,
                                 v_WarehouseId = A.v_WarehouseId,
                                 v_WarehouseName = D.v_Name,
                                 //v_OrganizationId = A.v_OrganizationId,
                                 v_ProductId = A.v_ProductId,
                                 i_CategoryId = B.i_CategoryId.Value,
                                 v_CategoryName = J3.v_Value1,
                                 v_ProductName = B.v_Name,
                                 r_StockActual = A.r_StockActual.Value,
                                 r_StockMin = A.r_StockMin.Value,
                                 r_StockMax = A.r_StockMax.Value,
                                 v_Brand = B.v_Brand,
                                 v_Model = B.v_Model,
                                 v_SerialNumber = B.v_SerialNumber,
                                 v_CreationUser = J1.v_UserName,
                                 v_UpdateUser = J2.v_UserName,
                                 d_CreationDate = A.d_InsertDate,
                                 d_UpdateDate = A.d_UpdateDate,

                             });
                List<ProductWarehouseList> objData = query.ToList();
                pobjOperationResult.Success = 1;
                return objData;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }


        public int GetProductWarehousesCount(ref OperationResult pobjOperationResult, string filterExpression, string pstrWarehouseId)
        {
            try
            {
                //SigesoftEntitiesModelDataService objDataService = new SigesoftEntitiesModelDataService();
                //int intResult = objDataService.ProductWarehousesCountFiltered(filterExpression);
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.productwarehouse
                            join B in dbContext.product on A.v_ProductId equals B.v_ProductId
                            where A.v_WarehouseId == pstrWarehouseId
                            select new ProductWarehouseList
                            {
                                v_WarehouseId = A.v_WarehouseId,
                                v_ProductId = A.v_ProductId,
                                i_CategoryId = B.i_CategoryId.Value,
                                v_ProductName = B.v_Name,
                                r_StockActual = A.r_StockActual.Value,
                                r_StockMin = A.r_StockMin.Value,
                                r_StockMax = A.r_StockMax.Value,
                                v_Brand = B.v_Brand,
                                v_Model = B.v_Model,
                                v_SerialNumber = B.v_SerialNumber
                            };

                if (!string.IsNullOrEmpty(filterExpression))
                {
                    query = query.Where(filterExpression);
                }
                List<ProductWarehouseList> objData = query.ToList();
                pobjOperationResult.Success = 1;
                return objData.Count;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return 0;
            }
        }

        public void UpdateProductWarehouse(ref OperationResult pobjOperationResult, productwarehouseDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.productwarehouse
                                       where a.v_WarehouseId == pobjDtoEntity.v_WarehouseId && a.v_ProductId == pobjDtoEntity.v_ProductId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                productwarehouse objEntity = productwarehouseAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.productwarehouse.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;

                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PRODUCTO ALMACEN", "i_WarehouseId=" + objEntity.v_ProductId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PRODUCTO ALMACEN", "i_WarehouseId=" + pobjDtoEntity.v_ProductId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

      public productwarehouseDto GetProductWarehouse(ref OperationResult pobjOperationResult, string pstrWarehouseId, string pstrProductId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                productwarehouseDto objDtoEntity = null;

                var objEntity = (from a in dbContext.productwarehouse
                                 where a.v_WarehouseId == pstrWarehouseId && a.v_ProductId == pstrProductId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = productwarehouseAssembler.ToDTO(objEntity);

                pobjOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        #endregion

        #region Movement

        public List<MovementList> GetMovementsListByWarehouseId(ref OperationResult pobjOperationResult, string pstrWarehouseId,
                int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region Query
                var query =
                        from A in dbContext.movement

                        // SystemUsers (LEFT JOIN) - i_InsertUserId
                        join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                        from J1 in J1_join.DefaultIfEmpty()

                        // SystemUsers (LEFT JOIN) - i_UpdateUserId
                        join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                        from J2 in J2_join.DefaultIfEmpty()

                        // Node (LEFT JOIN) - i_UpdateNodeId
                        join J3 in dbContext.node on new { i_UpdateNodeId = A.i_UpdateNodeId.Value }
                            equals new { i_UpdateNodeId = J3.i_NodeId } into J3_join
                        from J3 in J3_join.DefaultIfEmpty()

                        ////// Node (INNER JOIN) - i_NodeId
                        ////join J4 in dbContext.node on A.i_NodeId equals J4.i_NodeId

                        // SystemParameter (INNER JOIN) - i_IsProcessed
                        join J5 in dbContext.systemparameter on new { a = 111, b = A.i_IsLocallyProcessed.Value }
                                                            equals new { a = J5.i_GroupId, b = J5.i_ParameterId }

                        // Supplier (LEFT JOIN) - i_SupplierId
                        join J6 in dbContext.supplier on new { a = A.v_SupplierId } equals new { a = J6.v_SupplierId } into J6_join
                        from J6 in J6_join.DefaultIfEmpty()

                        // SystemParameter (INNER JOIN) - i_MovementTypeId
                        join J7 in dbContext.systemparameter on new { a = 109, b = A.i_MovementTypeId.Value }
                                                            equals new { a = J7.i_GroupId, b = J7.i_ParameterId }

                        // SystemParameter (INNER JOIN) - i_MotiveTypeId
                        join J8 in dbContext.systemparameter on new { a = 110, b = A.i_MotiveTypeId.Value }
                                                            equals new { a = J8.i_GroupId, b = J8.i_ParameterId }

                        // Condiciones
                        where A.v_WarehouseId == pstrWarehouseId

                        // Ordenamiento
                        orderby A.d_Date descending

                        select new MovementList
                        {
                            v_MovementId = A.v_MovementId,
                            v_SupplierId = A.v_SupplierId,
                            v_SupplierName = J6.v_Name,
                            v_IsProcessed = J5.v_Value1,
                            i_ProcessTypeId = A.i_ProcessTypeId,
                            i_MovementTypeId = A.i_MovementTypeId,
                            v_MovementTypeDescription = J7.v_Value1,
                            i_MotiveTypeId = A.i_MotiveTypeId,
                            v_MotiveTypeDescription = J8.v_Value1,
                            d_MovementDate = A.d_Date,
                            r_TotalQuantity = A.r_TotalQuantity,
                            v_WarehouseId = A.v_WarehouseId,
                            v_ReferentDocument = A.v_ReferenceDocument,

                            v_CreationUser = J1.v_UserName,
                            v_UpdateUser = J2.v_UserName,
                            d_UpdateDate = A.d_UpdateDate,
                            v_UpdateNodeName = J3.v_Description
                        };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                {
                    query = query.Where("d_MovementDate >= @0 && d_MovementDate <= @1", pdatBeginDate.Value, pdatEndDate.Value);
                }
                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                if (pintPageIndex.HasValue && pintResultsPerPage.HasValue)
                {
                    int intStartRowIndex = pintPageIndex.Value * pintResultsPerPage.Value;
                    query = query.Skip(intStartRowIndex);
                    query = query.Take(pintResultsPerPage.Value);
                }
                #endregion

                List<MovementList> objData = query.ToList();

                pobjOperationResult.Success = 1;
                return objData;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public movementDto GetMovement(ref OperationResult pobjOperationResult, string pstrMovementId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                movementDto objDtoEntity = null;

                var objEntity = (from a in dbContext.movement
                                 where a.v_MovementId == pstrMovementId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = movementAssembler.ToDTO(objEntity);

                pobjOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public string AddMovement(ref OperationResult pobjOperationResult, movementDto pobjDtoEntity, List<movementdetailDto> pobjMovementDetailDtoList, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                movement objEntity = movementAssembler.ToEntity(pobjDtoEntity);

                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);

                // Autogeneramos el Pk de la tabla                 
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 3), "MM");
                objEntity.v_MovementId = NewId;

                dbContext.AddTomovement(objEntity);
                dbContext.SaveChanges();

                // Guardar el Detalle (Si es que hay detalle)

                if (pobjMovementDetailDtoList != null)
                {
                    foreach (var item in pobjMovementDetailDtoList)
                    {
                        // Crear el detalle del movimiento
                        movementdetail objDetailEntity = movementdetailAssembler.ToEntity(item);
                        objDetailEntity.v_MovementId = objEntity.v_MovementId;
                        objDetailEntity.v_WarehouseId = objEntity.v_WarehouseId;
                        objDetailEntity.v_ProductId = item.v_ProductId;
                        objDetailEntity.r_StockMax = item.r_StockMax;
                        objDetailEntity.r_StockMin = item.r_StockMin;
                        objDetailEntity.i_MovementTypeId = objEntity.i_MovementTypeId;
                        objDetailEntity.r_Quantity = item.r_Quantity;
                        objDetailEntity.r_Price = item.r_Price;
                        // En la entidad DTO debe estar como mínimo: i_ProductId, r_Quantity y r_Price.
                        objDetailEntity.r_SubTotal = objDetailEntity.r_Quantity * objDetailEntity.r_Price;

                        // Agregar el detalle del movimiento a la BD
                        dbContext.AddTomovementdetail(objDetailEntity);
                    }
                    // Guardar todo en la BD
                    dbContext.SaveChanges();
                }

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "MOVIMIENTO", "v_MovementId=" + NewId.ToString(), Success.Ok, null);
                return NewId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "MOVIMIENTO", "v_MovementId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return null;
            }
        }

        public void UpdateMovement(ref OperationResult pobjOperationResult, movementDto pobjDtoEntity, List<movementdetailDto> pobjMovementDetailDtoList, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region Movement
                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.movement
                                       where a.v_MovementId == pobjDtoEntity.v_MovementId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                movement objEntity = movementAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.movement.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion              

                #region MovementDetail

                // Eliminar el detalle anterior
                List<movementdetail> pobjMovementDetailList = new List<movementdetail>();
                pobjMovementDetailList = (from A in dbContext.movementdetail
                                             where A.v_WarehouseId == pobjDtoEntity.v_WarehouseId && A.v_MovementId == pobjDtoEntity.v_MovementId
                                             select A).ToList();

                foreach (var item in pobjMovementDetailList)
                {
                    dbContext.movementdetail.DeleteObject(item);
                }

                // Guardar el nuevo Detalle (Si es que hay detalle)



                if (pobjMovementDetailDtoList != null)
                {
                    foreach (var item in pobjMovementDetailDtoList)
                    {
                        // Crear el detalle del movimiento
                        movementdetail objDetailEntity = movementdetailAssembler.ToEntity(item);
                        objDetailEntity.v_MovementId = objEntity.v_MovementId;
                        objDetailEntity.v_WarehouseId = objEntity.v_WarehouseId;
                        objDetailEntity.v_ProductId = item.v_ProductId;
                        objDetailEntity.r_StockMax = item.r_StockMax;
                        objDetailEntity.r_StockMin = item.r_StockMin;
                        objDetailEntity.i_MovementTypeId = objEntity.i_MovementTypeId;
                        objDetailEntity.r_Quantity = item.r_Quantity;
                        objDetailEntity.r_Price = item.r_Price;
                        // En la entidad DTO debe estar como mínimo: i_ProductId, r_Quantity y r_Price.
                        objDetailEntity.r_SubTotal = objDetailEntity.r_Quantity * objDetailEntity.r_Price;

                        // Agregar el detalle del movimiento a la BD
                        dbContext.AddTomovementdetail(objDetailEntity);
                    }
                    // Guardar todo en la BD
                    dbContext.SaveChanges();
                }





                #endregion
                
                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "MOVIMIENTO", "v_MovementId=" + objEntity.v_MovementId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "MOVIMIENTO", "v_MovementId=" + pobjDtoEntity.v_MovementId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void DeleteMovement(ref OperationResult pobjOperationResult, string pstrMovementId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.movement
                                       where a.v_MovementId == pstrMovementId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "MOVIMIENTO", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "MOVIMIENTO", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void ProcessMovementIngreso(ref OperationResult pobjOperationResult, int pintNodeId, string pstrMovementId, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                movement objEntity = (from A in dbContext.movement
                                      where A.v_MovementId == pstrMovementId
                                      select A).FirstOrDefault();

                List<movementdetail> objEntityDetailList = (from A in dbContext.movementdetail
                                                            where  A.v_MovementId == objEntity.v_MovementId
                                                            select A).ToList();
                DateTime ProcessMovementDate = DateTime.Now;

                // Recorrer el detalle de movimientos (Si es que hay detalle)
                if (objEntityDetailList != null)
                {
                    foreach (var objEntityDetail in objEntityDetailList)
                    {
                        // Actualizar el stock del producto en el almacén correspondiente
                        productwarehouse itemStock = (from A in dbContext.productwarehouse
                                                      where A.v_WarehouseId == objEntityDetail.v_WarehouseId && A.v_ProductId == objEntityDetail.v_ProductId
                                                            
                                                      select A).FirstOrDefault();

                        // Si el producto no existe en el almacén. Crearlo y asignarle su stock actual
                        if (itemStock == null)
                        {
                            itemStock = new productwarehouse();
                            itemStock.v_WarehouseId = objEntityDetail.v_WarehouseId;
                            itemStock.v_ProductId = objEntityDetail.v_ProductId;
                            itemStock.r_StockMin = 0; // Cambiar por objDetailEntity.r_StockMin, si se implementa.
                            itemStock.r_StockMax = objEntityDetail.r_Quantity; // Cambiar por objDetailEntity.r_StockMax, si se implementa.
                            itemStock.r_StockActual = objEntityDetail.r_Quantity;

                            // Auditoría
                            itemStock.i_InsertUserId = int.Parse(ClientSession[2]);
                            itemStock.d_InsertDate = ProcessMovementDate;

                            // Agregar el elemento de stock a la BD
                            dbContext.AddToproductwarehouse(itemStock);
                        }
                        // Si el producto existe en el almacén. Modificarle su stock actual.
                        else
                        {
                            itemStock.r_StockActual = itemStock.r_StockActual + objEntityDetail.r_Quantity;

                            // Auditoría
                            itemStock.i_UpdateUserId = int.Parse(ClientSession[2]);
                            itemStock.d_UpdateDate = ProcessMovementDate;
                        }
                    }

                    // Establecer el movimiento como Procesado
                    objEntity.i_IsLocallyProcessed = (int)Common.SiNo.SI;  //El movimiento inicia como no procesado  - Recordar: Solo deben subir los movimientos procesados al DATACENTER
                    objEntity.d_UpdateDate = DateTime.Now;
                    objEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                    // Guardar todo en la BD
                    dbContext.SaveChanges();
                }

                pobjOperationResult.Success = 1;
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.PROCESO, "PROCESO DE MOVIMIENTO DE INGRESO", string.Format("Node={0} / Id={1}", pintNodeId, pstrMovementId), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.PROCESO, "PROCESO DE MOVIMIENTO DE INGRESO", string.Format("Node={0} / Id={1}", pintNodeId, pstrMovementId), Success.Failed, ex.Message);
                return;
            }
        }

        public bool ProcessMovementEgreso(ref OperationResult pobjOperationResult, int pintNodeId, string pstrMovementId, List<string> ClientSession)
        {
            //mon.IsActive = true;
            bool booProcessMovement = false;
            string strProcessMessage = null;
            try
            {              
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                movement objEntity = (from A in dbContext.movement
                                      where A.v_MovementId == pstrMovementId
                                      select A).FirstOrDefault();

                List<movementdetail> objEntityDetailList = (from A in dbContext.movementdetail
                                                            where A.v_MovementId == objEntity.v_MovementId
                                                            select A).ToList();
                DateTime ProcessMovementDate = DateTime.Now;

                // Recorrer el detalle de movimientos (Si es que hay detalle)
                if (objEntityDetailList != null)
                {
                    foreach (var objEntityDetail in objEntityDetailList)
                    {
                        // Actualizar el stock del producto en el almacén correspondiente
                        productwarehouse itemStock = (from A in dbContext.productwarehouse
                                                      where A.v_WarehouseId == objEntityDetail.v_WarehouseId && A.v_ProductId == objEntityDetail.v_ProductId
                                                      select A).FirstOrDefault();

                        // Se asume que el producto existe en el almacén. Modificarle su stock actual.
                        if (objEntityDetail.r_Quantity.Value > itemStock.r_StockActual.Value)
                        {
                            // No se puede DEDUCIR
                            booProcessMovement = false;

                            // traer la información del producto

                            var objProduct = (from a in dbContext.product
                                              where a.v_ProductId == objEntityDetail.v_ProductId
                                              select a).FirstOrDefault();

                            strProcessMessage = string.Format("El stock del producto {0} (Stock Actual={1}) es insuficiente. No se procesará el movimiento.",
                                objProduct.v_Name, itemStock.r_StockActual.Value);

                            break;
                        }
                        else
                        {
                            // Si se puede DEDUCIR
                            booProcessMovement = true;

                            // Deducir el stock
                            itemStock.r_StockActual = itemStock.r_StockActual - objEntityDetail.r_Quantity;

                            // Auditoría
                            itemStock.i_UpdateUserId = int.Parse(ClientSession[2]);
                            itemStock.d_UpdateDate = ProcessMovementDate;
                        }
                    }

                    if (booProcessMovement)
                    {
                        // Establecer el movimiento como Procesado
                        objEntity.i_IsLocallyProcessed = (int)Common.SiNo.SI; // Recordar: Solo deben subir los movimientos procesados al DATACENTER
                        objEntity.d_UpdateDate = DateTime.Now;
                        objEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                        // Guardar todo en la BD
                        dbContext.SaveChanges();

                        pobjOperationResult.Success = 1;
                       LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.PROCESO, "PROCESO DE MOVIMIENTO DE EGRESO",string.Format("Node={0} / Id={1}", pintNodeId, pstrMovementId), Success.Ok, null);
                       return booProcessMovement;
                    }
                    else
                    {
                        // No se actualiza nada, ni se procesa el movimiento.
                        pobjOperationResult.Success = 0;
                        pobjOperationResult.ErrorMessage = strProcessMessage;
                        LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.PROCESO, "PROCESO DE MOVIMIENTO DE EGRESO",string.Format("Node={0} / Id={1}", pintNodeId, pstrMovementId), Success.Failed, strProcessMessage);
                        return booProcessMovement;
                    }
                }

                return booProcessMovement;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.PROCESO, "PROCESO DE MOVIMIENTO DE EGRESO",string.Format("Node={0} / Id={1}", pintNodeId, pstrMovementId), Success.Failed, ex.Message);
                return booProcessMovement;
            }
        }

        public void ProcessTransfer(ref OperationResult pobjOperationResult, movementDto pobjDtoEntity, List<movementdetailDto> pobjMovementDetailDtoList, int pintNodeSourceId, int pintNodeDestinationId, string pstrMovementId, List<string> ClientSession)
        {
            try
            {
                string MovementId = null;
                // primero se hace un egreso del almacén de origen
                bool Confirmation =  ProcessMovementEgreso(ref pobjOperationResult, pintNodeSourceId, pstrMovementId, ClientSession);

                // Luego se hace el ingreso en el almacén destino
                if (Confirmation == false)
                {  
                    pobjDtoEntity.v_ParentMovementId = pstrMovementId;
                    pobjDtoEntity.i_IsLocallyProcessed = (int)Common.SiNo.NO; // El movimiento no está procesado aún Localmente
                    MovementId = AddMovement(ref pobjOperationResult, pobjDtoEntity, pobjMovementDetailDtoList, ClientSession);
                }          

                if (Confirmation != false)
                {                   
                    pobjDtoEntity.v_ParentMovementId = pstrMovementId;
                    MovementId = AddMovement(ref pobjOperationResult, pobjDtoEntity, pobjMovementDetailDtoList, ClientSession);

                    ProcessMovementIngreso(ref pobjOperationResult, pintNodeDestinationId, MovementId, ClientSession);
                }
               

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.PROCESO, "PROCESO DE MOVIMIENTO DE EGRESO", string.Format("Node={0} / Id={1}", pintNodeSourceId, pstrMovementId), Success.Failed, ex.Message);
                return;
            }
        }

        public void DiscardMovement(ref OperationResult pobjOperationResult, int pintNodeId, string pstrMovementId, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la cabecera del movimiento
                movement objEntity = (from A in dbContext.movement
                                      where A.v_MovementId == pstrMovementId
                                      select A).FirstOrDefault();

                // Eliminar todo el detalle previo existente
                List<movementdetail> objExistingDetail = (from A in dbContext.movementdetail
                                                          where  A.v_MovementId == objEntity.v_MovementId
                                                          select A).ToList();
                if (objExistingDetail != null)
                {
                    foreach (var objExistingDetailItem in objExistingDetail)
                    {
                        dbContext.DeleteObject(objExistingDetailItem);
                    }
                }

                // Eliminar la cabecera
                dbContext.DeleteObject(objEntity);

                // Guardar en la BD
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "MOVIMIENTO", string.Format("Node={0} / Id={1}", pintNodeId, pstrMovementId), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "MOVIMIENTO", string.Format("Node={0} / Id={1}", pintNodeId, pstrMovementId), Success.Failed, ex.Message);
                return;
            }
        }

        #endregion

        #region MovementDetail

        public List<MovementDetailList> GetMovementDetailListByProductId(ref OperationResult pobjOperationResult, int pintPageIndex, int? pintResultsPerPage, string pstrProductId, string pstrWarehouseId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region Query
                var query =
                        (from A in dbContext.movementdetail
                         join B in dbContext.warehouse on A.v_WarehouseId equals B.v_WarehouseId
                         join D in dbContext.movement on A.v_MovementId equals D.v_MovementId
                         // Products (INNER JOIN) - i_ProductId
                         join J1 in dbContext.product on A.v_ProductId equals J1.v_ProductId

                         // Categories (INNER JOIN) - i_ProductId
                         join J2 in dbContext.datahierarchy on new { a = J1.i_CategoryId.Value, b = 103 } equals new { a = J2.i_ItemId, b = J2.i_GroupId }

                         // Categories (INNER JOIN) - i_ProductId
                         join J3 in dbContext.systemparameter on new { a = A.i_MovementTypeId.Value, b = 109 } equals new { a = J3.i_ParameterId, b = J3.i_GroupId }

                         join J4 in dbContext.systemparameter on new { a = D.i_MotiveTypeId.Value, b = 110 } equals new { a = J4.i_ParameterId, b = J4.i_GroupId }

                         where A.v_ProductId == pstrProductId && A.v_WarehouseId == pstrWarehouseId && D.i_ProcessTypeId == 1
                         orderby D.d_Date descending, A.v_MovementId descending

                         select new MovementDetailList
                         {
                             v_MovementId = A.v_MovementId,
                             v_ProductId = A.v_ProductId,
                             v_WarehouseId = A.v_WarehouseId,
                             v_MovementTypeDescription = J3.v_Value1,
                             v_ProductName = J1.v_Name,
                             v_WarehouseName = B.v_Name,
                             r_Quantity = A.r_Quantity,
                             r_Price = A.r_Price,
                             d_Date = D.d_Date,
                             v_MotiveTypeName = J4.v_Value1
                         });
                #endregion
                List<MovementDetailList> objData = query.ToList();

                pobjOperationResult.Success = 1;
                return objData;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public List<MovementDetailList> GetMovementDeatilPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.movementdetail
                            join B in dbContext.product on A.v_ProductId equals B.v_ProductId
                            join C in dbContext.datahierarchy on new { a = B.i_CategoryId.Value, b = 103 } equals new { a = C.i_ItemId, b = C.i_GroupId }
                            orderby C.v_Value1, B.v_Name ascending
                            select new MovementDetailList
                            {
                                v_MovementId = A.v_MovementId,
                                v_WarehouseId = A.v_WarehouseId,
                                v_ProductId = A.v_ProductId,
                                v_CategoryName = C.v_Value1,
                                v_ProductName = B.v_Name,
                                v_GenericName = B.v_GenericName,
                                v_BarCode = B.v_BarCode,
                                v_ProductCode = B.v_ProductCode,
                                v_Brand = B.v_Brand,
                                v_Model = B.v_Model,
                                v_SerialNumber = B.v_SerialNumber,
                                r_Quantity = A.r_Quantity.Value,
                                r_Price = A.r_Price
                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
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

                List<MovementDetailList> objData = query.ToList();
                pobjOperationResult.Success = 1;
                return objData;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public movementdetailDto GetMovementDetail(ref OperationResult pobjOperationResult, string pstrMovementId, string pstrProductId, string pstrWarehouse)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                movementdetailDto objDtoEntity = null;

                var objEntity = (from a in dbContext.movementdetail
                                 where a.v_MovementId == pstrMovementId && a.v_ProductId == pstrProductId && a.v_WarehouseId == pstrWarehouse
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = movementdetailAssembler.ToDTO(objEntity);

                pobjOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public List<MovementDetailList> GetMovementDetailListByProductId1(ref OperationResult pobjOperationResult, int pintPageIndex, int? pintResultsPerPage, string pstrProductId, int pintTop)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region Query
                var query =
                        (from A in dbContext.movementdetail
                         join B in dbContext.warehouse on A.v_WarehouseId equals B.v_WarehouseId
                         join D in dbContext.movement on A.v_MovementId equals D.v_MovementId
                         join E in dbContext.organization on B.v_OrganizationId equals E.v_OrganizationId
                         //join F in dbContext.location on new { a = E.v_OrganizationId, b = 103 } equals new { a = J2.i_ItemId, b = J2.i_GroupId }

                         join F in dbContext.location on B.v_LocationId equals F.v_LocationId
                         //join G in dbContext.supplier on D.v_SupplierId equals G.v_SupplierId
                         join G in dbContext.supplier on new { v_SupplierId = D.v_SupplierId }
                                equals new { v_SupplierId = G.v_SupplierId } into G_join
                         from G in G_join.DefaultIfEmpty()
                         // Products (INNER JOIN) - i_ProductId
                         join J1 in dbContext.product on A.v_ProductId equals J1.v_ProductId

                         // Categories (INNER JOIN) - i_ProductId
                         join J2 in dbContext.datahierarchy on new { a = J1.i_CategoryId.Value, b = 103 } equals new { a = J2.i_ItemId, b = J2.i_GroupId }

                         // Categories (INNER JOIN) - i_ProductId
                         join J3 in dbContext.systemparameter on new { a = A.i_MovementTypeId.Value, b = 109 } equals new { a = J3.i_ParameterId, b = J3.i_GroupId }

                         join J4 in dbContext.systemparameter on new { a = D.i_MotiveTypeId.Value, b = 110 } equals new { a = J4.i_ParameterId, b = J4.i_GroupId }

                         where A.v_ProductId == pstrProductId
                         && D.i_ProcessTypeId == 1 && D.i_MovementTypeId == 1
                         orderby  A.v_MovementId descending ,
                         D.d_Date descending
                         select new MovementDetailList
                         {
                             v_SupplierName = G.v_Name,
                             v_OrganizationName = E.v_Name + " / " + F.v_Name,
                             v_MovementId = A.v_MovementId,
                             v_ProductId = A.v_ProductId,
                             v_WarehouseId = A.v_WarehouseId,
                             v_MovementTypeDescription = J3.v_Value1,
                             v_ProductName = J1.v_Name,
                             v_WarehouseName = B.v_Name,
                             r_Quantity = A.r_Quantity,
                             r_Price = A.r_Price,
                             d_Date = D.d_Date,
                             v_MotiveTypeName = J4.v_Value1
                         }).Take(pintTop);
                #endregion
                List<MovementDetailList> objData = query.ToList();

                pobjOperationResult.Success = 1;
                return objData;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }


        #endregion

        #region carga inicial de movimientos

        public void GenerateMovementAutomatic(ref OperationResult pobjOperationResult, movementDto pobjDtoEntity, List<movementdetailDto> pobjMovementDetailDtoList, List<string> ClientSession)
        {

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();



                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "MOVIMIENTO AUTOMATICO", string.Empty, Success.Ok, null);

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "MOVIMIENTO AUTOMATICO", string.Empty, Success.Failed, pobjOperationResult.ExceptionMessage);

            }
        }


        #endregion

        #region Migración de Movimientos

        //public void Migration(List<string> ClientSession)
        //{
        //    OperationResult objOperationResult = new OperationResult();

        //    movementDto objmovementDto = new movementDto();
        //    movementdetailDto objmovementdetailDto = new movementdetailDto();

        //    List<movementdetailDto> _movementdetailListDto = null;
        //    _movementdetailListDto = new List<movementdetailDto>();

        //    SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

        //    string _MovementId;
        //    //Query para traer los almacenes
        //    var query = (from a in dbContext.productsformigration
        //                 select new MovementList 
        //                 {
        //                  v_WarehouseId = a.v_WarehouseId,
        //                  i_MotiveTypeId = a.i_MotiveTypeId,
        //                  d_MovementDate =a.d_MovementDate,
        //                 }).Distinct().ToList();

        //    //Bucle para insertar Movement
        //    foreach (var item in query)
        //    {
        //        _movementdetailListDto = new List<movementdetailDto>();

        //        objmovementDto.i_MotiveTypeId = item.i_MotiveTypeId;
        //        objmovementDto.v_SupplierId = null;
        //        objmovementDto.d_Date = item.d_MovementDate;
        //        objmovementDto.v_WarehouseId = item.v_WarehouseId;
        //        objmovementDto.v_ReferenceDocument = "";
        //        objmovementDto.i_IsLocallyProcessed = (int)Common.SiNo.NO;  //El movimiento inicia como no procesado
        //        objmovementDto.i_ProcessTypeId = (int)Common.ProcessType.LOCAL;// Procesado Localmente
        //        objmovementDto.i_MovementTypeId = (int)Common.MovementType.INGRESO; //(Ingreso, Egreso)
        //        objmovementDto.r_TotalQuantity = 0;


        //        //query para el detalle del movimiento
        //        var query1 = (from a in dbContext.productsformigration
        //                      where a.v_WarehouseId == item.v_WarehouseId
        //                     select new MovementDetailList
        //                     {
        //                         r_Quantity = a.r_StockActual,
        //                         r_Price = 0,
        //                         v_ProductId = a.v_ProductId
        //                     }).ToList();
        //        //Bucle para insertar Detalle del Movimiento
        //        foreach (var item1 in query1)
        //        {
        //            objmovementdetailDto = new movementdetailDto();
                    
        //            objmovementdetailDto.i_MovementTypeId = item.i_MovementTypeId;
        //            objmovementdetailDto.r_Quantity = item1.r_Quantity;
        //            objmovementdetailDto.r_Price = item1.r_Price;
        //            objmovementdetailDto.v_ProductId = item1.v_ProductId;                    
        //            objmovementDto.r_TotalQuantity = objmovementDto.r_TotalQuantity + item1.r_Quantity;
        //            _movementdetailListDto.Add(objmovementdetailDto);
        //        }
                
        //        _MovementId = AddMovement(ref objOperationResult, objmovementDto, _movementdetailListDto, ClientSession);

        //        //Método para procesar el movimiento (modifica la tabla ProductWarehouse)
        //        ProcessMovementIngreso(ref objOperationResult,1, _MovementId, ClientSession);
        //    }
            
        //}
        #endregion


        #region Movimientos de Farmacia

        public List<RevisionPuntosList> GetRevisionPuntosList(ref OperationResult pobjOperationResult, int? pintUsuarioId, int? pintRolVentaId, DateTime? FechaInicio, DateTime? FechaFin, int pintAlmacenId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.rolcuotadetalle

                             join B in dbContext.rolcuota on A.v_RolCuotaId equals B.v_RolCuotaId into B_join
                             from B in B_join.DefaultIfEmpty()

                             join C in dbContext.medication on A.v_IdProducto equals C.v_ProductId into C_join
                             from C in C_join.DefaultIfEmpty()


                             where B.i_RolId == pintRolVentaId
                             //&& C.i_InsertUserId == pintUsuarioId
                             select new RevisionPuntosList
                             {
                                v_ProductId = A.v_IdProducto,
                                v_ProductName = A.v_ProductoNombre,
                                i_Cuota = A.i_Cuota,
                                r_Quantity = C.r_Quantity,
                                i_CantidadVendida = C.r_QuantityVendida,
                                d_InsertDate = C.d_InsertDate                              
                             });

                if (FechaInicio.HasValue && FechaFin.HasValue)
                {
                    query = query.Where("d_InsertDate >= @0 && d_InsertDate <= @1", FechaInicio.Value, FechaFin.Value);
                }

                var sql = (from a in query.ToList()

                           select new RevisionPuntosList
                           {
                               v_ProductId = a.v_ProductId,
                               v_ProductName = a.v_ProductName,
                               i_Cuota = a.i_Cuota,
                               r_Quantity = a.r_Quantity,
                               i_CantidadVendida = a.i_CantidadVendida,
                               i_Saldo = a.i_Cuota - a.r_Quantity

                           }).Union(from A in dbContext.rolcuotadetalle
                                    join B in dbContext.rolcuota on A.v_RolCuotaId equals B.v_RolCuotaId into B_join
                                    from B in B_join.DefaultIfEmpty()
                                    where B.i_RolId == pintRolVentaId
                                    select new RevisionPuntosList
                                     {
                                         v_ProductId = A.v_IdProducto,
                                         v_ProductName = A.v_ProductoNombre,
                                         i_Cuota = A.i_Cuota,
                                         r_Quantity = 0,
                                         i_CantidadVendida = 0,
                                         i_Saldo = A.i_Cuota,
                                     }).ToList();

              //var ListaFinal=  sql.GroupBy(x => x.v_ProductName).Select(group => group.First()).ToList();

              var results = (from a in sql.AsEnumerable()
                            group a by new { a.v_ProductName,a.i_Cuota, a.v_ProductId} into g
                            select new RevisionPuntosList
                            {
                                v_ProductId = g.Key.v_ProductId,
                                v_ProductName = g.Key.v_ProductName,
                                i_Cuota = g.Key.i_Cuota,
                                r_Quantity = g.Sum(_ => _.r_Quantity),
                                i_CantidadVendida = g.Sum(_ => _.i_CantidadVendida),
                                i_Saldo = g.Key.i_Cuota - g.Sum(_ => _.r_Quantity)
                            }).ToList();




                // Incluir Valor Venta

              //var ListaProductos = null;// dbContext.devolverproductos("", pintAlmacenId, pintRolVentaId).ToList();


              //List<RevisionPuntosList> oListaRevisionPuntosList = new List<RevisionPuntosList>();
              //RevisionPuntosList oRevisionPuntosList;
              //foreach (var item in results)
              //{

              // var x = ListaProductos.Find(p => p.IdProducto == item.v_ProductId);

              // if (x != null)
              // {
              //     oRevisionPuntosList = new RevisionPuntosList();

              //     oRevisionPuntosList.v_ProductName = x.Producto;
              //     oRevisionPuntosList.i_Cuota = item.i_Cuota;
              //     oRevisionPuntosList.r_Quantity = item.r_Quantity;
              //     oRevisionPuntosList.i_CantidadVendida = item.i_CantidadVendida;
              //     oRevisionPuntosList.d_ValorVenta = x.ValorVenta.Value;
              //     oRevisionPuntosList.d_Importe = (decimal)item.i_CantidadVendida.Value * x.ValorVenta.Value;
              //     oRevisionPuntosList.i_Saldo = item.i_Saldo;

              //     oListaRevisionPuntosList.Add(oRevisionPuntosList);                   
              // }

              //}


              return null;

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

    }
}
