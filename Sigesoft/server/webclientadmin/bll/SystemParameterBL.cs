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
    public class SystemParameterBL
    {
        //Devart.Data.PostgreSql.PgSqlMonitor mon = new Devart.Data.PostgreSql.PgSqlMonitor();

        public List<SystemParameterList> GetSystemParametersPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, int pintGroupId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.systemparameter
                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                            equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            //join J3 in dbContext.systemparameter on new { ItemId = A.i_ParentGroupId.Value }
                            //                                 equals new { ItemId = J3.i_ParameterId } into J3_join
                            //from J3 in J3_join.DefaultIfEmpty()

                            //join J4 in dbContext.systemparameter on new { ItemId = A.i_ParentParameterId.Value, groupId = A.i_ParentGroupId.Value }
                            //                              equals new { ItemId = J4.i_ParameterId, groupId = J4.i_GroupId } into J4_join
                            //from J4 in J4_join.DefaultIfEmpty()

                            select new SystemParameterList
                            {
                                i_GroupId = A.i_GroupId,
                                i_ParameterId = A.i_ParameterId,
                                i_ParentGroupId = A.i_GroupId,
                                //v_ParentGroupName = J3.v_Value1,
                                i_ParentParameterId = A.i_ParentParameterId.Value,
                                //v_ParentParameterName = J4.v_Value1,
                                v_Value1 = A.v_Value1,
                                v_Value2 = A.v_Value2,
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

                List<SystemParameterList> objData = query.ToList();
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

        public int GetSystemParametersCount(ref OperationResult pobjOperationResult, string filterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from a in dbContext.systemparameter select a;

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

        public systemparameterDto GetSystemParameter(ref OperationResult pobjOperationResult, int pintGroupId, int pintParameterId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                systemparameterDto objDtoEntity = null;

                var objEntity = (from a in dbContext.systemparameter
                                 where a.i_GroupId == pintGroupId && a.i_ParameterId == pintParameterId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = systemparameterAssembler.ToDTO(objEntity);

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

        public void AddSystemParameter(ref OperationResult pobjOperationResult, systemparameterDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                systemparameter objEntity = systemparameterAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;

                dbContext.AddTosystemparameter(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;

                // Llenar entidad Log                        
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PARÁMETRO", "GroupId=" + objEntity.i_GroupId.ToString() + " / Descripción = " + objEntity.v_Value1, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PARÁMETRO", "GroupId=" + pobjDtoEntity.i_GroupId.ToString() + " / Descripción = " + pobjDtoEntity.v_Value1, Success.Failed, ex.Message);
                return;
            }
        }

        public void UpdateSystemParameter(ref OperationResult pobjOperationResult, systemparameterDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.systemparameter
                                       where a.i_GroupId == pobjDtoEntity.i_GroupId && a.i_ParameterId == pobjDtoEntity.i_ParameterId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                systemparameter objEntity = systemparameterAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.systemparameter.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;

                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PARÁMETRO", "GroupId=" + objEntity.i_GroupId.ToString() + " / Descripción = " + objEntity.v_Value1, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PARÁMETRO", "GroupId=" + pobjDtoEntity.i_GroupId.ToString() + " / Descripción = " + pobjDtoEntity.v_Value1, Success.Failed, ex.Message);
                return;
            }
        }

        public void DeleteSystemParameter(ref OperationResult pobjOperationResult, int pintGroupId, int pintParameterId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from a in dbContext.systemparameter
                                 where a.i_GroupId == pintGroupId && a.i_ParameterId == pintParameterId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                {
                    dbContext.DeleteObject(objEntity);
                    dbContext.SaveChanges();
                }

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "PARÁMETRO", "GroupId=" + objEntity.i_GroupId.ToString() + " / Descripción = " + objEntity.v_Value1, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "PARÁMETRO", "", Success.Failed, ex.Message);
                return;
            }
        }

        public List<KeyValueDTO> GetSystemParameterForCombo(ref OperationResult pobjOperationResult, int pintGroupId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataSystemParameterList;
                DataSystemParameterList = (from a in dbContext.systemparameter
                                           where a.i_GroupId == pintGroupId
                                           select a).AsEnumerable()
                                           .Select(x => new KeyValueDTO
                                           {
                                               Id = x.i_ParameterId.ToString(),
                                               Value1 = x.v_Value1,
                                               Value2 = x.v_Value2
                                           }).ToList();

                pobjOperationResult.Success = 1;
                return DataSystemParameterList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public List<KeyValueDTO> GetSystemParameterByParentForCombo(ref OperationResult pobjOperationResult, int i_ParentGroupId, int i_ParentParameterId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataSystemParameterList;
                DataSystemParameterList = (from a in dbContext.systemparameter
                                           where  a.i_ParentParameterId == i_ParentParameterId
                                           select new KeyValueDTO
                                           {
                                               Id = a.i_ParameterId.ToString(),
                                               Value1 = a.v_Value1,
                                               Value2 = a.v_Value2
                                           }).ToList();

                pobjOperationResult.Success = 1;
                return DataSystemParameterList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }
        
        public List<DataForTreeViewSP> GetSystemParameterForComboTreeView(ref OperationResult pobjOperationResult, int pintGroupId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<DataForTreeViewSP> SystemParameterList;
                List<DataForTreeViewSP> SystemParameterListForBinding = new List<DataForTreeViewSP>();
                SystemParameterList = (from a in dbContext.systemparameter
                                     where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0
                                     orderby a.i_ParentParameterId ascending
                                       select new DataForTreeViewSP
                                     {
                                         Id = a.i_ParameterId,
                                         Description = a.v_Value1,
                                         Description2 = a.v_Value2,
                                         ParentId = a.i_ParentParameterId.Value,
                                         EnabledSelect = true
                                     }
                                           ).ToList();

                // Iterar y ordenar la data
                ProcessData(SystemParameterList, -1, SystemParameterListForBinding, 0);

                pobjOperationResult.Success = 1;
                return SystemParameterListForBinding;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        private void ProcessData(List<DataForTreeViewSP> pDataToIterate, int pParentId, List<DataForTreeViewSP> pResults, int pLevel)
        {
            var query = from i in pDataToIterate
                        where i.ParentId == pParentId
                        orderby i.Description ascending
                        select i;

            foreach (var item in query)
            {
                item.Level = pLevel;
                pResults.Add(item);
                ProcessData(pDataToIterate, item.Id, pResults, pLevel + 1);
            }
        }

        public List<DataTreeViewForGridViewSP> GetSystemParameterForGridView(ref OperationResult pobjOperationResult, int pintGroupId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<DataTreeViewForGridViewSP> SystemParameterList;
                List<DataTreeViewForGridViewSP> SystemParameterListForBinding = new List<DataTreeViewForGridViewSP>();
                SystemParameterList = (from a in dbContext.systemparameter
                                     where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0
                                     orderby a.i_ParentParameterId ascending
                                     select new DataTreeViewForGridViewSP
                                     {
                                         i_GroupId = a.i_GroupId,
                                         i_ParameterId = a.i_ParameterId,
                                         v_Value1 = a.v_Value1,
                                         v_Value2 = a.v_Value2,
                                         i_ParentItemId = a.i_ParentParameterId.Value
                                     }
                                           ).ToList();

                // Iterar y ordenar la data
                ProcessDataGridView(SystemParameterList, -1, SystemParameterListForBinding, 0);

                pobjOperationResult.Success = 1;
                return SystemParameterListForBinding;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        private void ProcessDataGridView(List<DataTreeViewForGridViewSP> pDataToIterate, int pParentId, List<DataTreeViewForGridViewSP> pResults, int pLevel)
        {
            var query = from i in pDataToIterate
                        where i.i_ParentItemId == pParentId
                        orderby i.v_Value1 ascending
                        select i;

            foreach (var item in query)
            {
                item.Level = pLevel;
                pResults.Add(item);
                ProcessDataGridView(pDataToIterate, item.i_ParameterId, pResults, pLevel + 1);
            }
        }


        public int GetSystemParameterMaxGroupId()
        {
            //mon.IsActive = true;
            int maxId;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                maxId = (from a in dbContext.systemparameter
                         where a.i_GroupId == 0
                         select a.i_ParameterId).Max<int>();

                maxId++; 
           
            }
            catch (Exception)
            {
                throw;
            }

            return maxId;

        }

        public int GetSystemParameterMaxParameterId(int groupId)
        {
            //mon.IsActive = true;
            int maxId;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                maxId = (from a in dbContext.systemparameter
                         where a.i_GroupId == groupId
                         select a.i_ParameterId).Max<int>();

                maxId++;

            }
            catch (Exception)
            {
                throw;
            }

            return maxId;

        }

        public List<KeyValueDTO> GetJoinOrganizationAndLocation(ref OperationResult pobjOperationResult, int pintNodeId)
        {
            //Devart.Data.PostgreSql.PgSqlMonitor mon = new Devart.Data.PostgreSql.PgSqlMonitor();
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from n in dbContext.node
                             join a in dbContext.nodeorganizationlocationprofile on n.i_NodeId equals a.i_NodeId
                             join J1 in dbContext.nodeorganizationprofile on new { a = a.i_NodeId, b = a.v_OrganizationId }
                                                      equals new { a = J1.i_NodeId, b = J1.v_OrganizationId } into j1_join
                             from J1 in j1_join.DefaultIfEmpty()
                             join b in dbContext.organization on J1.v_OrganizationId equals b.v_OrganizationId
                             join c in dbContext.location on a.v_LocationId equals c.v_LocationId
                             where
                                   n.i_IsDeleted == 0 &&
                                   a.i_IsDeleted == 0

                             select new RestrictedWarehouseProfileList
                             {
                                 v_OrganizationName = b.v_Name,
                                 v_LocationName = c.v_Name,
                                 v_LocationId = c.v_LocationId,
                                 v_OrganizationId = b.v_OrganizationId,
                                 i_NodeId = J1.i_NodeId,
                             }
                          );

                var q = from a in query.ToList()
                        select new KeyValueDTO
                        {
                            Id = string.Format("{0}|{1}", a.v_OrganizationId, a.v_LocationId),
                            Value1 = string.Format("{0} / Sede: {1} ",
                                     a.v_OrganizationName,
                                     a.v_LocationName
                                    )
                        };

                List<KeyValueDTO> KeyValueDTO = q.OrderBy(p => p.Value1).ToList();

                pobjOperationResult.Success = 1;
                return KeyValueDTO;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public List<KeyValueDTO> GetProtocolsByOrganizationForCombo(ref OperationResult pobjOperationResult, string v_CustomerOrganizationId, string v_CustomerLocationId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.protocol
                            where (a.v_CustomerOrganizationId == v_CustomerOrganizationId) &&
                            (a.v_CustomerLocationId == v_CustomerLocationId) &&
                            (a.i_IsDeleted == 0)
                            select a;

                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                else
                {
                    query = query.OrderBy("v_ProtocolId");
                }

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.v_ProtocolId,
                                Value1 = x.v_Name
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

        public List<KeyValueDTO> GetSystemParameterForComboAll(ref OperationResult pobjOperationResult, int pintGroupId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.systemparameter
                            where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0
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
                                Id = x.i_ParameterId.ToString(),
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

        public List<KeyValueDTO> GetSystemParameterForComboOrder(ref OperationResult pobjOperationResult, int pintGroupId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.systemparameter
                            where a.i_GroupId == pintGroupId && a.i_IsDeleted == 0
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
                                Id = x.i_ParameterId.ToString(),
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

        public List<KeyValueDTO> GetGESO(ref OperationResult pobjOperationResult, string pstrOrganizationId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataGESOList;
                DataGESOList = (from a in dbContext.groupoccupation
                                join b in dbContext.location on a.v_LocationId equals b.v_LocationId
                                join c in dbContext.organization on b.v_OrganizationId equals c.v_OrganizationId
                                where c.v_OrganizationId == pstrOrganizationId && a.i_IsDeleted == 0
                                select a).AsEnumerable()
                                           .Select(x => new KeyValueDTO
                                           {
                                               Id = x.v_GroupOccupationId.ToString(),
                                               Value1 = x.v_Name
                                           }).OrderBy(x => x.Value1).ToList();

                pobjOperationResult.Success = 1;
                return DataGESOList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public List<KeyValueDTO> GetGESOByOrgIdAndLocationId(ref OperationResult pobjOperationResult, string pstrOrganizationId, string locationId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<KeyValueDTO> DataGESOList;

                DataGESOList = (from a in dbContext.groupoccupation
                                join b in dbContext.location on a.v_LocationId equals b.v_LocationId
                                join c in dbContext.organization on b.v_OrganizationId equals c.v_OrganizationId
                                where c.v_OrganizationId == pstrOrganizationId
                                      && b.v_LocationId == locationId
                                      && a.i_IsDeleted == 0

                                select a).AsEnumerable()
                                           .Select(x => new KeyValueDTO
                                           {
                                               Id = x.v_GroupOccupationId.ToString(),
                                               Value1 = x.v_Name
                                           }).OrderBy(x => x.Value1).ToList();

                pobjOperationResult.Success = 1;
                return DataGESOList;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public List<KeyValueDTO> GetSystemParameterByParentIdForCombo(ref OperationResult pobjOperationResult, int pintGroupId, int pintParentParameterId, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.systemparameter
                            where a.i_GroupId == pintGroupId &&
                            a.i_ParentParameterId == pintParentParameterId &&
                            a.i_IsDeleted == 0
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
                                Id = x.i_ParameterId.ToString(),
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

        public List<KeyValueDTO> GetProfessional(ref OperationResult pobjOperationResult, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.systemuser
                            where a.i_IsDeleted == 0 && a.i_SystemUserTypeId == 1
                            select a;

                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                else
                {
                    query = query.OrderBy("v_UserName");
                }

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_SystemUserId.ToString(),
                                Value1 = x.v_UserName
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

        public List<KeyValueDTO> GetProfessionalAuditores(ref OperationResult pobjOperationResult, string pstrSortExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from a in dbContext.systemuser
                            join b in dbContext.professional on a.v_PersonId equals b.v_PersonId
                            where a.i_IsDeleted == 0 && a.i_SystemUserTypeId == 1 && b.i_ProfessionId == 31
                            select a;

                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                else
                {
                    query = query.OrderBy("v_UserName");
                }

                var query2 = query.AsEnumerable()
                            .Select(x => new KeyValueDTO
                            {
                                Id = x.i_SystemUserId.ToString(),
                                Value1 = x.v_UserName
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
       
    }
}
