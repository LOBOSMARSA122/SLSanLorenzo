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
    public class SystemParameterBL
    {
        //Devart.Data.PostgreSql.PgSqlMonitor mon = new Devart.Data.PostgreSql.PgSqlMonitor();

        public List<SystemParameterList> GetSystemParametersPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, int? pintGroupId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.systemparameter
                            join B in dbContext.systemparameter on new { a = A.i_ParentParameterId.Value, b = A.i_GroupId }
                                                                equals new { a = B.i_ParameterId, b = 147 } into B_join
                            from B in B_join.DefaultIfEmpty()

                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                            equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            join J3 in dbContext.diseases on new { a = A.v_Value1 }
                                                          equals new { a = J3.v_DiseasesId } into J3_join
                            from J3 in J3_join.DefaultIfEmpty()

                            //where B.i_GroupId == pintGroupId
                            select new SystemParameterList
                            {
                                i_GroupId = A.i_GroupId,
                                i_ParameterId = A.i_ParameterId,
                                i_ParentGroupId = A.i_GroupId,
                                i_ParentParameterId = A.i_ParentParameterId.Value,
                                v_Value1 = A.v_Value1,
                                v_Value2 = A.v_Value2,
                                v_DiseasesName = J3.v_Name,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                i_IsDeleted = A.i_IsDeleted,
                                Enfermedad = B.v_Value1,
                                v_DiseaseName = J3.v_Name
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

        public List<SystemParameterList> GetSystemParametersPagedAndFilteredNew(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, int? pintGroupId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.systemparameter
                            join B in dbContext.systemparameter on new { a = A.i_ParentParameterId.Value, b = A.i_GroupId }
                                                                equals new { a = B.i_ParameterId, b = 147 } into B_join
                            from B in B_join.DefaultIfEmpty()

                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                            equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            join J3 in dbContext.diseases on new { a = A.v_Value1 }
                                                          equals new { a = J3.v_DiseasesId } into J3_join
                            from J3 in J3_join.DefaultIfEmpty()

                            where B.i_GroupId == pintGroupId && B.i_IsDeleted == 0
                            select new SystemParameterList
                            {
                                i_GroupId = A.i_GroupId,
                                i_ParameterId = A.i_ParameterId,
                                i_ParentGroupId = A.i_GroupId,
                                i_ParentParameterId = A.i_ParentParameterId.Value,
                                v_Value1 = A.v_Value1,
                                v_Value2 = A.v_Value2,
                                v_DiseasesName = J3.v_Name,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                i_IsDeleted = A.i_IsDeleted,
                                Enfermedad = B.v_Value1,
                                v_DiseaseName = J3.v_Name,
                                SI =  false,
                                NO = true,
                                ND = false,
                                i_Answer = 0
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

        public SystemParameterList GetParentNameSystemParameter(ref OperationResult pobjOperationResult,int pintParameterId, int pintGroupId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from A in dbContext.systemparameter
                             where A.i_ParameterId == pintParameterId && A.i_GroupId == pintGroupId
                            select new SystemParameterList
                            {
                                i_ParentParameterId = A.i_ParentParameterId.Value,
                            }).FirstOrDefault();

                var query2 = (from A in dbContext.systemparameter
                              where A.i_ParameterId == query.i_ParentParameterId && A.i_GroupId == pintGroupId
                             select new SystemParameterList
                             {
                                 v_Value1 = A.v_Value1,
                             }).FirstOrDefault();

                SystemParameterList objData = query2;
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
          
        public SystemParameterList GetParentNameSystemParameter(ref OperationResult pobjOperationResult, string pstrValue, int pintGroupId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from A in dbContext.systemparameter
                             where A.v_Value1 == pstrValue && A.i_GroupId == pintGroupId
                             select new SystemParameterList
                             {
                                 i_ParentParameterId = A.i_ParentParameterId.Value,
                             }).FirstOrDefault();

                var query2 = (from A in dbContext.systemparameter
                              where A.i_ParameterId == query.i_ParentParameterId && A.i_GroupId == pintGroupId
                              select new SystemParameterList
                              {
                                  v_Value1 = A.v_Value1,
                              }).FirstOrDefault();

                SystemParameterList objData = query2;
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

        public SystemParameterList GetParentNameSystemParameter(ref OperationResult pobjOperationResult, int pintParameterId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from A in dbContext.systemparameter
                             where A.i_ParameterId == pintParameterId && A.i_GroupId == 146
                             select new SystemParameterList
                             {
                                 v_Value1 = A.v_Value1,
                             }).FirstOrDefault();



                SystemParameterList objData = query;
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

        public SystemParameterList GetParentNameSystemParameterHabitsNoxious(ref OperationResult pobjOperationResult, int pintParameterId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from A in dbContext.systemparameter
                             where A.i_ParameterId == pintParameterId && A.i_GroupId == 148
                             select new SystemParameterList
                             {
                                 v_Value1 = A.v_Value1,
                             }).FirstOrDefault();



                SystemParameterList objData = query;
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

        public SystemParameterList GetParentNameSystemParameterFamilyMedicAntecedent(ref OperationResult pobjOperationResult, int pintParameterId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();


                var query = (from A in dbContext.systemparameter
                             where A.i_ParameterId == pintParameterId && A.i_GroupId == 149 && A.i_ParentParameterId != -1
                             select new SystemParameterList
                             {
                                 i_ParentParameterId = A.i_ParentParameterId.Value,
                             }).FirstOrDefault();

                var query2 = (from A in dbContext.systemparameter
                              where A.i_ParameterId == query.i_ParentParameterId && A.i_GroupId == 149
                              select new SystemParameterList
                              {
                                  v_Value1 = A.v_Value1,
                                  i_ParentParameterId = A.i_ParentParameterId.Value,
                                  v_DiseasesName = A.v_Value1
                              }).FirstOrDefault();

                SystemParameterList objData = query2;
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
        
        public SystemParameterList GetSystemParameterFamilyMedicAntecedent(ref OperationResult pobjOperationResult , int pintParameterId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.systemparameter
                             where A.i_ParameterId == pintParameterId && A.i_GroupId == 149 
                             select new SystemParameterList
                             {
                                 i_ParameterId = A.i_ParameterId,
                                 i_ParentParameterId = A.i_ParentParameterId.Value,
                                 v_DiseasesName = A.v_Value1
                             }).FirstOrDefault();


                SystemParameterList objData = query;
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

        public SystemParameterList GetGroupFamilyMedicAntecedent(ref OperationResult pobjOperationResult, int pintParentParameterId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.systemparameter
                             where A.i_ParameterId == pintParentParameterId && A.i_GroupId == 149
                             select new SystemParameterList
                             {
                                 v_Value1 = A.v_Value1
                             }).FirstOrDefault();


                SystemParameterList objData = query;
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


        public DataHierarchyList ObtenerIdPadreDataHierarchy(ref OperationResult pobjOperationResult, string pstrComponenteId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from a in dbContext.datahierarchy
                             where a.i_GroupId == 123 && a.v_Value2 == pstrComponenteId
                             select new DataHierarchyList
                             {
                                 i_ItemId = a.i_ItemId
                             }

                             ).FirstOrDefault();

                //DataHierarchyList objData = query;
                return query;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }


           public void ActualizarValorParametro(int pintGrupoId, int pstrValorAntiguo)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.systemparameter
                                       where a.i_GroupId==  259  && a.i_ParameterId == 1
                                       select a).FirstOrDefault();
                objEntitySource.v_Value2 = (pstrValorAntiguo + 1).ToString();
              
                // Guardar los cambios
                dbContext.SaveChanges();

                return;
            }
            catch (Exception ex)
            {

                // Llenar entidad Log
                return;
            }
        }

    }
}
