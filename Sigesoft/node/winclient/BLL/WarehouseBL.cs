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
   public class WarehouseBL
    {
       //Devart.Data.PostgreSql.PgSqlMonitor mon = new Devart.Data.PostgreSql.PgSqlMonitor();

       public List<WarehouseList> GetWarehousePagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
       {
           //mon.IsActive = true;

           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               var query = from A in dbContext.warehouse
                           join B in dbContext.location on A.v_LocationId equals B.v_LocationId
                           join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                            equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                           from J1 in J1_join.DefaultIfEmpty()

                           join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                           equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                           from J2 in J2_join.DefaultIfEmpty()

                           join J4 in dbContext.datahierarchy on new { ItemId = A.i_CostCenterId.Value, groupId = 110 }
                                                          equals new { ItemId = J4.i_ItemId, groupId = J4.i_GroupId } into J4_join
                           from J4 in J4_join.DefaultIfEmpty()

                           where A.i_IsDeleted == 0
                           select new WarehouseList
                           {
                               v_OrganizationId = A.v_OrganizationId,
                               v_WarehouseId = A.v_WarehouseId,
                               v_LocationId = A.v_LocationId,
                               v_LocationIdName = B.v_Name,
                               i_CostCenterId = A.i_CostCenterId.Value,
                               v_CenterCostoName = J4.v_Value1,
                               v_Name = A.v_Name,
                               v_CreationUser = J1.v_UserName,
                               v_UpdateUser = J2.v_UserName,
                               d_CreationDate = A.d_InsertDate,
                               d_UpdateDate = A.d_UpdateDate,
                               i_IsDeleted = A.i_IsDeleted
                           };

               if (!string.IsNullOrEmpty(pstrFilterExpression))
               {
                   query = query.Where(pstrFilterExpression);
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

               List<WarehouseList> objData = query.ToList();
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

       public void AddWarehouse(ref OperationResult pobjOperationResult, warehouseDto pobjDtoEntity, List<string> ClientSession)
       {
           //mon.IsActive = true;
           string NewId = "(No generado)";
           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               warehouse objEntity = warehouseAssembler.ToEntity(pobjDtoEntity);

               objEntity.d_InsertDate = DateTime.Now;
               objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
               objEntity.i_IsDeleted = 0;
               // Autogeneramos el Pk de la tabla
               int intNodeId = int.Parse(ClientSession[0]);
               NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 2), "WW");
               objEntity.v_WarehouseId = NewId;
           
               //int SecuentialId = Utils.GetNextSecuentialId(1, 2);
               //objEntity.v_WarehouseId = SecuentialId.ToString();

               dbContext.AddTowarehouse(objEntity);
               dbContext.SaveChanges();

               pobjOperationResult.Success = 1;
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ALMACÉN", "v_WarehouseId=" + NewId.ToString(), Success.Ok, null);
               return;
           }
           catch (Exception ex)
           {
               pobjOperationResult.Success = 0;
               pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ALMACÉN", "v_WarehouseId=" + NewId.ToString(), Success.Failed, ex.Message);
               return;
           }
       }

       public warehouseDto GetWarehouse(ref OperationResult pobjOperationResult, string pstrWarehouseId)
       {
           //mon.IsActive = true;

           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               warehouseDto objDtoEntity = null;

               var objEntity = (from a in dbContext.warehouse
                                where a.v_WarehouseId == pstrWarehouseId
                                select a).FirstOrDefault();

               if (objEntity != null)
                   objDtoEntity = warehouseAssembler.ToDTO(objEntity);

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

       public void UpdateWarehouse(ref OperationResult pobjOperationResult, warehouseDto pobjDtoEntity, List<string> ClientSession)
       {
           //mon.IsActive = true;

           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

               // Obtener la entidad fuente
               var objEntitySource = (from a in dbContext.warehouse
                                      where a.v_WarehouseId == pobjDtoEntity.v_WarehouseId
                                      select a).FirstOrDefault();

               // Crear la entidad con los datos actualizados
               pobjDtoEntity.d_UpdateDate = DateTime.Now;
               pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
               warehouse objEntity = warehouseAssembler.ToEntity(pobjDtoEntity);

               // Copiar los valores desde la entidad actualizada a la Entidad Fuente
               dbContext.warehouse.ApplyCurrentValues(objEntity);

               // Guardar los cambios
               dbContext.SaveChanges();

               pobjOperationResult.Success = 1;
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ALMACÉN", "v_WarehouseId=" + objEntity.v_WarehouseId.ToString(), Success.Ok, null);
               return;
           }
           catch (Exception ex)
           {
               pobjOperationResult.Success = 0;
               pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ALMACÉN", "v_WarehouseId=" + pobjDtoEntity.v_WarehouseId.ToString(), Success.Failed, ex.Message);
               return;
           }
       }

       public void DeleteWarehouse(ref OperationResult pobjOperationResult, string pstrWarehouseId, List<string> ClientSession)
       {
           //mon.IsActive = true;

           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

               // Obtener la entidad fuente
               var objEntitySource = (from a in dbContext.warehouse
                                      where a.v_WarehouseId == pstrWarehouseId
                                      select a).FirstOrDefault();

               // Crear la entidad con los datos actualizados
               objEntitySource.d_UpdateDate = DateTime.Now;
               objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
               objEntitySource.i_IsDeleted = 1;

               // Guardar los cambios
               dbContext.SaveChanges();

               pobjOperationResult.Success = 1;
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "ALMACÉN", "", Success.Ok, null);
               return;
           }
           catch (Exception ex)
           {
               pobjOperationResult.Success = 0;
               pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "ALMACÉN", "", Success.Failed, ex.Message);
               return;
           }
       }

   }
}
