using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.DAL;
using Sigesoft.Common;

namespace Sigesoft.Server.WebClientAdmin.BLL
{
    public class DataHierarchyBL
    {
        public List<DataHierarchyList> GetDataHierarchiesPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, int pintGroupId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from A in dbContext.datahierarchy

                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                            equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            join J4 in dbContext.datahierarchy on new { ItemId = A.i_ParentItemId.Value, groupId = A.i_GroupId }
                                                          equals new { ItemId = J4.i_ItemId, groupId = J4.i_GroupId } into J4_join
                            from J4 in J4_join.DefaultIfEmpty()

                            where A.i_GroupId == pintGroupId
                                  && (A.i_IsDeleted == 0 || A.i_IsDeleted == null)

                            select new DataHierarchyList
                            {
                                i_GroupId = A.i_GroupId,
                                i_ItemId = A.i_ItemId,
                                v_Value1 = A.v_Value1,
                                i_ParentItemId = A.i_ParentItemId.Value,
                                v_ParentItemName = J4.v_Value1,
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
                    query = query.Take(pintResultsPerPage.Value);
                }

                List<DataHierarchyList> objData = query.ToList();
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

        public int GetDataHierarchiesCount(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.datahierarchy select a;

                if (!string.IsNullOrEmpty(filterExpression))
                    query = query.Where(filterExpression);

                int intResult = query.Count();

                pobjOperationResult.Success = 1;
                return intResult;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return 0;
            }
        }

        public datahierarchyDto GetDataHierarchy(ref OperationResult pobjOperationResult, int pintGroupId, int pintParameterId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                datahierarchyDto objDtoEntity = null;

                var objEntity = (from a in dbContext.datahierarchy
                                 where a.i_GroupId == pintGroupId && a.i_ItemId == pintParameterId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = datahierarchyAssembler.ToDTO(objEntity);

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

        public void AddDataHierarchy(ref OperationResult pobjOperationResult, datahierarchyDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                datahierarchy objEntity = datahierarchyAssembler.ToEntity(pobjDtoEntity);


                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;

                dbContext.AddTodatahierarchy(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "DATA HIERARCHY", "GroupId=" + objEntity.i_GroupId.ToString() + " / Descripción = " + objEntity.v_Value1, Success.Ok, null);

                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "DATA HIERARCHY", "GroupId=" + pobjDtoEntity.i_GroupId.ToString() + " / Descripción = " + pobjDtoEntity.v_Value1, Success.Failed, ex.Message);
                return;
            }
        }

        public void UpdateDataHierarchy(ref OperationResult pobjOperationResult, datahierarchyDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.datahierarchy
                                       where a.i_GroupId == pobjDtoEntity.i_GroupId && a.i_ItemId == pobjDtoEntity.i_ItemId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                datahierarchy objEntity = datahierarchyAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.datahierarchy.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;

                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "DATA HIERARCHY", "GroupId=" + objEntity.i_GroupId.ToString() + " / Descripción = " + objEntity.v_Value1, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "DATA HIERARCHY", "GroupId=" + pobjDtoEntity.i_GroupId.ToString() + " / Descripción = " + pobjDtoEntity.v_Value1, Success.Failed, ex.Message);
                return;
            }
        }

        public void DeleteDataHierarchy(ref OperationResult pobjOperationResult, int pintGroupId, int pintParameterId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.datahierarchy
                                       where a.i_GroupId == pintGroupId && a.i_ItemId == pintParameterId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "DATA HIERARCHY", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "DATA HIERARCHY", "", Success.Failed, ex.Message);
                return;
            }
        }

        public List<DataForTreeView> GetDataHierarchyForCombo(ref OperationResult pobjOperationResult, int pintGroupId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<DataForTreeView> DataHierarchyList;
                List<DataForTreeView> DataHierarchyListForBinding = new List<DataForTreeView>();
                DataHierarchyList = (from a in dbContext.datahierarchy
                                     where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0
                                     orderby a.i_Sort ascending
                                     select new DataForTreeView
                                     {
                                         Id = a.i_ItemId,
                                         Description = a.v_Value1,
                                         Description2 = a.v_Value2,
                                         ParentId = a.i_ParentItemId.Value,
                                         EnabledSelect = true,
                                         i_Sort = a.i_Sort.Value
                                     }
                                           ).ToList();

                // Iterar y ordenar la data
                ProcessData(DataHierarchyList, -1, DataHierarchyListForBinding, 0);

                pobjOperationResult.Success = 1;
                return DataHierarchyListForBinding;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        private void ProcessData(List<DataForTreeView> pDataToIterate, int pParentId, List<DataForTreeView> pResults, int pLevel)
        {
            var query = from i in pDataToIterate
                        where i.ParentId == pParentId
                        orderby i.i_Sort ascending
                        select i;

            foreach (var item in query)
            {
                item.Level = pLevel;
                pResults.Add(item);
                ProcessData(pDataToIterate, item.Id, pResults, pLevel + 1);
            }
        }

        public List<DataTreeViewForGridView> GetDataHierarchyForGridView(ref OperationResult pobjOperationResult, int pintGroupId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<DataTreeViewForGridView> DataHierarchyList;
                List<DataTreeViewForGridView> DataHierarchyListForBinding = new List<DataTreeViewForGridView>();
                DataHierarchyList = (from a in dbContext.datahierarchy
                                     where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0
                                     orderby a.i_ParentItemId ascending
                                     select new DataTreeViewForGridView
                                     {
                                         i_GroupId = a.i_GroupId,
                                         i_ItemId = a.i_ItemId,
                                         v_Value1 = a.v_Value1,
                                        v_Value2 = a.v_Value2,
                                         i_ParentItemId = a.i_ParentItemId.Value
                                     }
                                           ).ToList();

                // Iterar y ordenar la data
                ProcessDataGridView(DataHierarchyList, -1, DataHierarchyListForBinding, 0);

                pobjOperationResult.Success = 1;
                return DataHierarchyListForBinding;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        private void ProcessDataGridView(List<DataTreeViewForGridView> pDataToIterate, int pParentId, List<DataTreeViewForGridView> pResults, int pLevel)
        {
            var query = from i in pDataToIterate
                        where i.i_ParentItemId == pParentId
                        orderby i.v_Value1 ascending
                        select i;

            foreach (var item in query)
            {
                item.Level = pLevel;
                pResults.Add(item);
                ProcessDataGridView(pDataToIterate, item.i_ItemId, pResults, pLevel + 1);
            }
        }

        //public List<DataHierarchyList> GetDataHierarchyParentForCombo(ref OperationResult pobjOperationResult, int pintParentGroupId, int pintParentParameterId)
        //{
        //    //mon.IsActive = true;

        //    try
        //    {
        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
        //        List<DataHierarchyList> DataHierarchyList;
        //        DataHierarchyList = (from a in dbContext.datahierarchy
        //                             where a.i_ParentItemId == pintParentParameterId
        //                             select new DataHierarchyList
        //                             {
        //                                 i_ItemId = a.i_ItemId,
        //                                 v_Value1 = a.v_Value1,
        //                                 v_Value2 = a.v_Value2

        //                             }).ToList();

        //        pobjOperationResult.Success = 1;
        //        return DataHierarchyList;
        //    }
        //    catch (Exception ex)
        //    {
        //        pobjOperationResult.Success = 0;
        //        pobjOperationResult.ExceptionMessage = ex.Message;
        //        return null;
        //    }
        //}

        public List<KeyValueDTO> GetDataHierarchyForComboDepartamento(ref OperationResult pobjOperationResult, int pintGroupId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.datahierarchy
                            where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0 && a.i_ParentItemId == -1
                            select a;

                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                else
                {
                    query = query.OrderBy("v_Value1");
                }

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_ItemId.ToString(),
                                Value1 = x.v_Value1,
                                Value2 = x.v_Value2
                            }).ToList();

                pobjOperationResult.Success = 1;
                return query2;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public List<KeyValueDTO> GetDataHierarchyForComboProvincia(ref OperationResult pobjOperationResult, int pintGroupId, int? pintParentItemId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.datahierarchy
                            where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0 && a.i_ParentItemId == pintParentItemId
                            select a;


                query = query.OrderBy("v_Value1");

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_ItemId.ToString(),
                                Value1 = x.v_Value1,
                                Value2 = x.v_Value2
                            }).ToList();

                pobjOperationResult.Success = 1;
                return query2;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public List<KeyValueDTO> GetDataHierarchyForComboDistrito(ref OperationResult pobjOperationResult, int pintGroupId, int? pintParentItemId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.datahierarchy
                            where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0 && a.i_ParentItemId == pintParentItemId
                            select a;


                query = query.OrderBy("v_Value1");

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_ItemId.ToString(),
                                Value1 = x.v_Value1,
                                Value2 = x.v_Value2
                            }).ToList();

                pobjOperationResult.Success = 1;
                return query2;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }
        
        public List<DataForTreeView> GetSystemParameterForComboTree(ref OperationResult pobjOperationResult, int pintGroupId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<DataForTreeView> DataHierarchyList;
                List<DataForTreeView> DataHierarchyListForBinding = new List<DataForTreeView>();
                DataHierarchyList = (from a in dbContext.systemparameter
                                     where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0
                                     orderby a.i_ParentParameterId ascending
                                     select new DataForTreeView
                                     {
                                         Id = a.i_ParameterId,
                                         Description = a.i_ParentParameterId.Value == -1 ? a.v_Value1 : a.v_Value2,
                                         Description2 = a.v_Value1,
                                         ParentId = a.i_ParentParameterId.Value,
                                         EnabledSelect = true
                                     }
                                           ).ToList();

                // Iterar y ordenar la data
                ProcessData(DataHierarchyList, -1, DataHierarchyListForBinding, 0);

                pobjOperationResult.Success = 1;
                return DataHierarchyListForBinding;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public List<DataForTreeView> GetSystemParameterForComboTree_(ref OperationResult pobjOperationResult, int pintGroupId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<DataForTreeView> DataHierarchyList;
                List<DataForTreeView> DataHierarchyListForBinding = new List<DataForTreeView>();
                DataHierarchyList = (from a in dbContext.systemparameter
                                     where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0
                                     orderby a.i_ParentParameterId ascending
                                     select new DataForTreeView
                                     {
                                         Id = a.i_ParameterId,
                                         Description = a.i_ParentParameterId.Value == -1 ? a.v_Value1 : a.v_Value2,
                                         Description2 = a.v_Value1,
                                         ParentId = a.i_ParentParameterId.Value,
                                         EnabledSelect = true,
                                         i_ParameterId = a.i_ParameterId
                                     }
                                           ).ToList();

                  var q = (from a in DataHierarchyList

                           select new DataForTreeView
                           {
                               Id = a.Id,
                               Description = a.Description + "|" + a.i_ParameterId,
                               Description2 = a.Description2,
                               ParentId = a.ParentId,
                               EnabledSelect = true,
                               i_ParameterId = a.i_ParameterId
                           }
                                           ).ToList();

                // Iterar y ordenar la data
                ProcessData(q, -1, DataHierarchyListForBinding, 0);

                pobjOperationResult.Success = 1;
                return DataHierarchyListForBinding;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }


    }
}
