using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.DAL;
using Sigesoft.Common;
using System.Linq.Dynamic;

namespace Sigesoft.Server.WebClientAdmin.BLL
{
    public class ApplicationHierarchyBL
    {
        #region"ApplicationHierarchy"

        public applicationhierarchyDto GetApplicationHierarchy(ref OperationResult pobjOperationResult, int pintApplicationHierarchyId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                applicationhierarchyDto objDtoEntity = null;

                var objEntity = (from a in dbContext.applicationhierarchy
                                 where a.i_ApplicationHierarchyId == pintApplicationHierarchyId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = applicationhierarchyAssembler.ToDTO(objEntity);

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

        public void AddApplicationHierarchy(ref OperationResult pobjOperationResult, applicationhierarchyDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                applicationhierarchy objEntity = applicationhierarchyAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                //// Autogeneramos el Pk de la tabla
                int SecuentialId = Utils.GetNextSecuentialId(int.Parse(ClientSession[0].ToString()), 1);
                objEntity.i_ApplicationHierarchyId = SecuentialId;
                dbContext.AddToapplicationhierarchy(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "APLICACIÓN JERÁRQUICA", "i_ApplicationHierarchyId=" + objEntity.i_ApplicationHierarchyId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "APLICACIÓN JERÁRQUICA", "i_ApplicationHierarchyId=" + pobjDtoEntity.i_ApplicationHierarchyId.ToString(), Success.Failed, ex.Message);
                return;
            }
        }

        public void UpdateApplicationHierarchy(ref OperationResult pobjOperationResult, applicationhierarchyDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.applicationhierarchy
                                       where a.i_ApplicationHierarchyId == pobjDtoEntity.i_ApplicationHierarchyId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                //pobjDtoEntity.i_ApplicationHierarchyId = pintApplicationHierarchyId;
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                applicationhierarchy objEntity = applicationhierarchyAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.applicationhierarchy.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "APLICACIÓN JERÁRQUICA", "i_ApplicationHierarchyId=" + objEntity.i_ApplicationHierarchyId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "APLICACIÓN JERÁRQUICA", "i_ApplicationHierarchyId=" + pobjDtoEntity.i_ApplicationHierarchyId.ToString(), Success.Failed, ex.Message);
                return;
            }
        }

        public void DeleteApplicationHierarchy(ref OperationResult pobjOperationResult, int pintApplicationHierarchyId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.applicationhierarchy
                                       where a.i_ApplicationHierarchyId == pintApplicationHierarchyId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "APLICACIÓN JERÁRQUICA", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "APLICACIÓN JERÁRQUICA", "", Success.Failed, ex.Message);
                return;
            }
        }

        public List<DtvForGrwAppHierarchy> GetApplicationHierarchyForGridView(ref OperationResult pobjOperationResult)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<DtvForGrwAppHierarchy> ApplicationHierarchyList;
                List<DtvForGrwAppHierarchy> DataHierarchyListForBinding = new List<DtvForGrwAppHierarchy>();
                ApplicationHierarchyList = (from a in dbContext.applicationhierarchy
                                            join J1 in dbContext.systemparameter on new { a = a.i_ApplicationHierarchyTypeId.Value, b = 106 } equals new { a = J1.i_ParameterId, b = J1.i_GroupId }
                                            join J2 in dbContext.systemparameter on new { a = a.i_ScopeId.Value, b = 104 }
                                                                       equals new { a = J2.i_ParameterId, b = J2.i_GroupId } into J2_join
                                            from J2 in J2_join.DefaultIfEmpty()
                                            where a.i_IsDeleted == 0
                                            select new DtvForGrwAppHierarchy
                                            {
                                                i_GroupId = a.i_ApplicationHierarchyId,
                                                i_ApplicationHierarchyId = a.i_ApplicationHierarchyId,
                                                v_Value1 = a.v_Description,
                                                i_ParentItemId = a.i_ParentId.Value,
                                                v_Form = a.v_Form,
                                                v_Code = a.v_Code,
                                                v_ScopeName = J2.v_Value1,
                                                v_ApplicationHierarchyTypeName = J1.v_Value1
                                            }).ToList();

                // Iterar y ordenar la data
                ProcessDataGridView(ApplicationHierarchyList, -1, DataHierarchyListForBinding, 0);

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

        private void ProcessDataGridView(List<DtvForGrwAppHierarchy> pDataToIterate, int pParentId, List<DtvForGrwAppHierarchy> pResults, int pLevel)
        {
            //mon.IsActive = true;
            var query = from i in pDataToIterate
                        where i.i_ParentItemId == pParentId
                        orderby i.v_Value1 ascending
                        select i;

            foreach (var item in query)
            {
                item.Level = pLevel;
                pResults.Add(item);
                ProcessDataGridView(pDataToIterate, item.i_ApplicationHierarchyId, pResults, pLevel + 1);
            }
        }

        public List<applicationhierarchyDto> GetAllApplicationHierarchy(ref OperationResult pobjOperationResult)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<applicationhierarchyDto> oListApplicationHierarchy;
                oListApplicationHierarchy = (from a in dbContext.applicationhierarchy
                                             select new applicationhierarchyDto
                                             {
                                                 i_ApplicationHierarchyId = a.i_ApplicationHierarchyId,
                                                 i_ApplicationHierarchyTypeId = a.i_ApplicationHierarchyTypeId,
                                                 i_Level = a.i_Level,
                                                 v_Description = a.v_Description,
                                                 v_Form = a.v_Form,
                                                 i_ParentId = a.i_ParentId,
                                                 i_ScopeId = a.i_ScopeId,
                                                 v_Code = a.v_Code
                                             }).ToList();
                pobjOperationResult.Success = 1;
                return oListApplicationHierarchy;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }

        }

        public List<applicationhierarchyDto> GetApplicationHierarchyByScopeId(ref OperationResult pobjOperationResult, int pintScopeId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<applicationhierarchyDto> oListApplicationHierarchy;

                oListApplicationHierarchy = (from a in dbContext.applicationhierarchy                                         
                                             orderby a.v_Description
                                             where (a.i_ScopeId == pintScopeId) && 
                                                   (a.i_IsDeleted == 0) && 
                                                   (a.i_ExternalUserFunctionalityTypeId == null) ||   // Ocultar Permisos externos
                                                   (a.i_ScopeId == -1)                                          
                                             select new applicationhierarchyDto
                                             {
                                                 i_ApplicationHierarchyId = a.i_ApplicationHierarchyId,
                                                 i_ApplicationHierarchyTypeId = a.i_ApplicationHierarchyTypeId,
                                                 i_Level = a.i_Level,
                                                 v_Description = a.v_Description,
                                                 v_Form = a.v_Form,
                                                 i_ParentId = a.i_ParentId,
                                                 i_ScopeId = a.i_ScopeId,
                                                 v_Code = a.v_Code
                                             }).ToList();
                pobjOperationResult.Success = 1;
                return oListApplicationHierarchy;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }

        }

        public List<DtvAppHierarchy> GetApplicationHierarchyForCombo(ref OperationResult pobjOperationResult)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<DtvAppHierarchy> DataHierarchyList;
                List<DtvAppHierarchy> DataHierarchyListForBinding = new List<DtvAppHierarchy>();
                DataHierarchyList = (from a in dbContext.applicationhierarchy
                                     where a.i_IsDeleted == 0
                                     select new DtvAppHierarchy
                                     {
                                         Id = a.i_ApplicationHierarchyId,
                                         Description = a.v_Description,
                                         ParentId = a.i_ParentId.Value,
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

        private void ProcessData(List<DtvAppHierarchy> pDataToIterate, int pParentId, List<DtvAppHierarchy> pResults, int pLevel)
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

        public List<DtvAppHierarchy> GetSystemParameterForCombo(ref OperationResult pobjOperationResult)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                List<DtvAppHierarchy> DataHierarchyList;
                List<DtvAppHierarchy> DataHierarchyListForBinding = new List<DtvAppHierarchy>();
              var  query = from A in dbContext.systemparameter
                                     join B in dbContext.systemparameter on new { a = A.i_ParentParameterId.Value, b = A.i_GroupId }
                                                              equals new { a = B.i_ParameterId, b = 147 } into B_join
                                     from B in B_join.DefaultIfEmpty()
                                    join J3 in dbContext.diseases on new { a = A.v_Value1 }
                                                        equals new { a = J3.v_DiseasesId } into J3_join
                                    from J3 in J3_join.DefaultIfEmpty()

                                     where B.i_IsDeleted == 0 && B.i_GroupId==147
                                     select new DtvAppHierarchy
                                     {
                                         Id = A.i_ParameterId,
                                         Description = J3.v_Name,
                                         ParentId =B.i_ParentParameterId.Value,
                                         EnabledSelect = true,
                                         i_GroupId = A.i_GroupId
                                     };


              query = query.Where("i_GroupId== 147");

              DataHierarchyList = query.ToList();
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
        
        #endregion
    }
}
