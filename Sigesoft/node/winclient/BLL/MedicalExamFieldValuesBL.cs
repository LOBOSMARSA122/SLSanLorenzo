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

   public class MedicalExamFieldValuesBL
    {
       //Devart.Data.PostgreSql.PgSqlMonitor mon = new Devart.Data.PostgreSql.PgSqlMonitor();
       
       public List<MedicalExamFieldValuesList> GetMedicalExamFieldValuesPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.componentfieldvalues
                            join B in dbContext.systemparameter on new { a = A.i_OperatorId.Value, b = 129 } equals new { a = B.i_ParameterId, b = B.i_GroupId }
                            join C in dbContext.systemparameter on new { a = A.i_IsAnormal.Value, b = 111 } equals new { a = C.i_ParameterId, b = C.i_GroupId }
                            //join D in dbContext.diseases on new { A.v_Diseases equals D.v_DiseasesId }

                            join D in dbContext.diseases on new { a = A.v_DiseasesId }
                                         equals new { a = D.v_DiseasesId } into D_join
                            from D in D_join.DefaultIfEmpty()

                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                            equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()
                          
                            where A.i_IsDeleted == 0
                            select new MedicalExamFieldValuesList
                            {
                                v_MedicalExamFieldValuesId = A.v_ComponentFieldValuesId,
                                v_MedicalExamFieldsId = A.v_ComponentFieldId,
                                v_AnalyzingValue1 = A.v_AnalyzingValue1,
                                v_AnalyzingValue2 = A.v_AnalyzingValue2,
                                i_OperatorId = A.i_OperatorId.Value,
                                v_OperatorName = B.v_Value1,
                                v_IsAnormal = C.v_Value1,
                                v_Cie10Name = D.v_Name +" / " + D.v_CIE10Id,
                                v_LegalStandard = A.v_LegalStandard,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                i_IsDeleted = A.i_IsDeleted.Value
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

                List<MedicalExamFieldValuesList> objData = query.ToList();
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

       public string AddMedicalExamFieldValues(ref OperationResult pobjOperationResult, List<ComponentFieldValuesRestrictionList> ComponentFieldValuesRestrictionList, List<ComponentFieldValuesRecommendationList> ComponentFieldValuesRecommendationList, componentfieldvaluesDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                componentfieldvalues objEntity = componentfieldvaluesAssembler.ToEntity(pobjDtoEntity);
                componentfieldvaluesrestrictionDto objcomponentfieldvaluesrestrictionDto = new componentfieldvaluesrestrictionDto();
                componentfieldvaluesrecommendationDto objcomponentfieldvaluesrecommendationDto = new componentfieldvaluesrecommendationDto();

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 19), "MV");
                objEntity.v_ComponentFieldValuesId = NewId;

                dbContext.AddTocomponentfieldvalues(objEntity);
                dbContext.SaveChanges();

                if (ComponentFieldValuesRestrictionList !=null)
                {
                    for (int i = 0; i < ComponentFieldValuesRestrictionList.Count; i++)
                    {
                        objcomponentfieldvaluesrestrictionDto.v_ComponentFieldValuesId = NewId;
                        objcomponentfieldvaluesrestrictionDto.v_MasterRecommendationRestricctionId = ComponentFieldValuesRestrictionList[i].v_MasterRecommendationRestricctionId;

                        AddRestriction(ref pobjOperationResult, objcomponentfieldvaluesrestrictionDto, ClientSession);
                    }
                }

                if (ComponentFieldValuesRecommendationList !=null)
                {
                    for (int i = 0; i < ComponentFieldValuesRecommendationList.Count; i++)
                    {
                        objcomponentfieldvaluesrecommendationDto.v_ComponentFieldValuesId = NewId;
                        objcomponentfieldvaluesrecommendationDto.v_ComponentFieldValuesRecommendationId = ComponentFieldValuesRecommendationList[i].v_MasterRecommendationRestricctionId;

                        AddRecommendation(ref pobjOperationResult, objcomponentfieldvaluesrecommendationDto, ClientSession);
                    }
                }

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "EXAMEN MÉDICO CAMPO VALOR", "v_MedicalExamFieldValuesId=" + NewId.ToString(), Success.Ok, null);
                return NewId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "EXAMEN MÉDICO CAMPO VALOR", "v_MedicalExamFieldValuesId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return null;
            }
        }

       public componentfieldvaluesDto GetMedicalExamFieldValues(ref OperationResult pobjOperationResult, string pstrMedicalExamFieldValuesId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                componentfieldvaluesDto objDtoEntity = null;

                var objEntity = (from a in dbContext.componentfieldvalues
                                 where a.v_ComponentFieldValuesId == pstrMedicalExamFieldValuesId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = componentfieldvaluesAssembler.ToDTO(objEntity);

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

       public void UpdateMedicalExamFieldValues(ref OperationResult pobjOperationResult, List<componentfieldvaluesrestrictionDto> pobjComponentFieldValuesRestrictionAdd, List<componentfieldvaluesrestrictionDto> pobjComponentFieldValuesRestrictionDelete, List<componentfieldvaluesrecommendationDto> pobjComponentFieldValuesRecommendationAdd, List<componentfieldvaluesrecommendationDto> pobjComponentFieldValuesRecommendationDelete, componentfieldvaluesDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
           
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                #region Actualizar Valor

                componentfieldvaluesrestrictionDto objcomponentfieldvaluesrestrictionDto = new componentfieldvaluesrestrictionDto();
                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.componentfieldvalues
                                       where a.v_ComponentFieldValuesId == pobjDtoEntity.v_ComponentFieldValuesId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                componentfieldvalues objEntity = componentfieldvaluesAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.componentfieldvalues.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                #region Crear Restriccion
                int intNodeId = int.Parse(ClientSession[0]);
                foreach (var item in pobjComponentFieldValuesRestrictionAdd)
                {
                    componentfieldvaluesrestriction objEntity1 = componentfieldvaluesrestrictionAssembler.ToEntity(item);

                    objEntity1.d_InsertDate = DateTime.Now;
                    objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                    objEntity1.i_IsDeleted = 0;

                    var NewId1 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 28), "VR");
                    objEntity1.v_ComponentFieldValuesRestrictionId = NewId1;
                    objEntity1.v_ComponentFieldValuesId = pobjDtoEntity.v_ComponentFieldValuesId;

                    dbContext.AddTocomponentfieldvaluesrestriction(objEntity1);
                }
                // Guardar los cambios
                dbContext.SaveChanges();

                #endregion

                #region Eliminar Restriccion

                if (pobjComponentFieldValuesRestrictionDelete != null)
                {
                    // Eliminar Componentes del protocolo
                    foreach (var item in pobjComponentFieldValuesRestrictionDelete)
                    {
                        // Obtener la entidad fuente
                        var objEntitySource1 = (from a in dbContext.componentfieldvaluesrestriction
                                                where a.v_ComponentFieldValuesRestrictionId == item.v_ComponentFieldValuesRestrictionId
                                                select a).FirstOrDefault();

                        // Crear la entidad con los datos actualizados
                        objEntitySource1.d_UpdateDate = DateTime.Now;
                        objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        objEntitySource1.i_IsDeleted = 1;

                    }
                }

                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                #region Crear Recommendation
                foreach (var item in pobjComponentFieldValuesRecommendationAdd)
                {
                    componentfieldvaluesrecommendation objEntity1 = componentfieldvaluesrecommendationAssembler.ToEntity(item);

                    objEntity1.d_InsertDate = DateTime.Now;
                    objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                    objEntity1.i_IsDeleted = 0;

                    var NewId1 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 31), "VC");
                    objEntity1.v_ComponentFieldValuesRecommendationId = NewId1;
                    objEntity1.v_ComponentFieldValuesId = pobjDtoEntity.v_ComponentFieldValuesId;

                    dbContext.AddTocomponentfieldvaluesrecommendation(objEntity1);
                }
                // Guardar los cambios
                dbContext.SaveChanges();

                #endregion

                #region Eliminar Recommendation

                if (pobjComponentFieldValuesRecommendationDelete != null)
                {
                    // Eliminar Componentes del protocolo
                    foreach (var item in pobjComponentFieldValuesRecommendationDelete)
                    {
                        // Obtener la entidad fuente
                        var objEntitySource1 = (from a in dbContext.componentfieldvaluesrecommendation
                                                where a.v_ComponentFieldValuesRecommendationId == item.v_ComponentFieldValuesRecommendationId
                                                select a).FirstOrDefault();

                        // Crear la entidad con los datos actualizados
                        objEntitySource1.d_UpdateDate = DateTime.Now;
                        objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        objEntitySource1.i_IsDeleted = 1;

                    }
                }

                // Guardar los cambios
                dbContext.SaveChanges();
                #endregion

                    //// Guardar los cambios
                    //dbContext.SaveChanges();

                    pobjOperationResult.Success = 1;
                    // Llenar entidad Log
                    LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "EXAMEN MÉDICO CAMPO VALOR", "v_MedicalExamId=" + objEntity.v_ComponentFieldValuesId.ToString(), Success.Ok, null);
                    return;
                
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "EXAMEN MÉDICO CAMPO VALOR", "v_MedicalExamId=" + pobjDtoEntity.v_ComponentFieldValuesId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void DeleteMedicalExamFieldValues(ref OperationResult pobjOperationResult, string pstrMedicalExamFieldValuesId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.componentfieldvalues
                                       where a.v_ComponentFieldValuesId == pstrMedicalExamFieldValuesId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "EXAMEN MÉDICO CAMPO VALOR", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "EXAMEN MÉDICO CAMPO VALOR", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }


        public string ObtenerIdComponentFieldValues(string pComponentFieldId, string pDiseases)
        {
             SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
             var query = (from A in dbContext.componentfieldvalues
                         where A.v_DiseasesId == pDiseases && A.v_ComponentFieldId == pComponentFieldId

                         select new
                         {
                             ComponentFielValuesId = A.v_ComponentFieldValuesId
                         }).FirstOrDefault();


             return query.ComponentFielValuesId;

        }
        # region Diseases

        public List<DiseasesList> GetDiseasesPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from B in dbContext.diseases
                             //join B in dbContext.diseases on new { v_CIE10Id = A.v_CIE10Id }
                             //                               equals new { v_CIE10Id = B.v_CIE10Id } into B_join
                             //from B in B_join.DefaultIfEmpty()

                             join J1 in dbContext.systemuser on new { i_InsertUserId = B.i_InsertUserId.Value }
                                                             equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                             from J1 in J1_join.DefaultIfEmpty()

                             join J2 in dbContext.systemuser on new { i_UpdateUserId = B.i_UpdateUserId.Value }
                                                             equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                             from J2 in J2_join.DefaultIfEmpty()
                             select new DiseasesList
                             {
                                 v_DiseasesId = B.v_DiseasesId,
                                 v_CIE10Id = B.v_CIE10Id,
                                 v_Name = B.v_Name,
                                 v_CIE10Description1 = null,
                                 v_CreationUser = J1.v_UserName,
                                 v_UpdateUser = J2.v_UserName,
                                 d_CreationDate = B.d_InsertDate,
                                 d_UpdateDate = B.d_UpdateDate
                             }).Union(from A in dbContext.cie10
                                      join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                                      equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                                      from J1 in J1_join.DefaultIfEmpty()

                                      join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                                      equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                                      from J2 in J2_join.DefaultIfEmpty()
                                      select new DiseasesList
                                      {
                                          v_DiseasesId = null,
                                          v_CIE10Id = A.v_CIE10Id,
                                          v_Name = A.v_CIE10Description1,
                                          v_CIE10Description1 = A.v_CIE10Description1,
                                          v_CreationUser = J1.v_UserName,
                                          v_UpdateUser = J2.v_UserName,
                                          d_CreationDate = A.d_InsertDate,
                                          d_UpdateDate = A.d_UpdateDate
                                      });


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

                List<DiseasesList> objData = query.ToList();
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

        public string AddDiseases(ref OperationResult pobjOperationResult, diseasesDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                diseases objEntity = diseasesAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 27), "DD");
                objEntity.v_DiseasesId = NewId;


                dbContext.AddTodiseases(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ENFERMEDAD", "v_Diseases=" + NewId.ToString(), Success.Ok, null);
                return NewId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ENFERMEDAD", "v_Diseases=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return null;
            }
        }

        public diseasesDto GetDiseases(ref OperationResult pobjOperationResult, string pstrDiseasesId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                diseasesDto objDtoEntity = null;

                var objEntity = (from a in dbContext.diseases
                                 where a.v_DiseasesId == pstrDiseasesId && a.i_IsDeleted == 0
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = diseasesAssembler.ToDTO(objEntity);

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

        public void UpdateDiseases(ref OperationResult pobjOperationResult, diseasesDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.diseases
                                       where a.v_DiseasesId == pobjDtoEntity.v_DiseasesId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                diseases objEntity = diseasesAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.diseases.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ENFERMEDAD", "v_Diseases=" + objEntity.v_DiseasesId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ENFERMEDAD", "v_Diseases=" + pobjDtoEntity.v_DiseasesId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public diseasesDto GetIsValidateDiseases(ref OperationResult pobjOperationResult, string pstrDiseasesName)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                diseasesDto objDtoEntity = null;

                var objEntity = (from a in dbContext.diseases
                                 where a.v_Name == pstrDiseasesName && a.i_IsDeleted == 0
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = diseasesAssembler.ToDTO(objEntity);

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

        #endregion

        #region Restriction

        public List<ComponentFieldValuesRestrictionList> GetRestrictionsPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.componentfieldvaluesrestriction
                            join B in dbContext.masterrecommendationrestricction on A.v_MasterRecommendationRestricctionId equals B.v_MasterRecommendationRestricctionId
                            //join B in dbContext.datahierarchy on new { a = A.i_RestrictionId.Value, b = 107 } equals new { a = B.i_ItemId, b = B.i_GroupId }

                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                            equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0
                            select new ComponentFieldValuesRestrictionList
                            {
                                v_ComponentFieldValuesRestrictionId = A.v_ComponentFieldValuesRestrictionId,
                                v_ComponentFieldValuesId = A.v_ComponentFieldValuesId,
                                v_MasterRecommendationRestricctionId = A.v_MasterRecommendationRestricctionId,
                                i_RecordStatus = (int)RecordStatus.Grabado,
                                i_RecordType = (int)RecordType.NoTemporal,
                                v_RestrictionName = B.v_Name,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate
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

                List<ComponentFieldValuesRestrictionList> objData = query.ToList();
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
       
        public void AddRestriction(ref OperationResult pobjOperationResult, componentfieldvaluesrestrictionDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                componentfieldvaluesrestriction objEntity = componentfieldvaluesrestrictionAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 28), "VR");
                objEntity.v_ComponentFieldValuesRestrictionId = NewId;


                dbContext.AddTocomponentfieldvaluesrestriction(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "RESTRICCIÓN", "v_ComponentFieldValuesRestrictionId=" + NewId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "RESTRICCIÓN", "v_ComponentFieldValuesRestrictionId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void UpdateRestriction(ref OperationResult pobjOperationResult, componentfieldvaluesrestrictionDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.componentfieldvaluesrestriction
                                       where a.v_ComponentFieldValuesId == pobjDtoEntity.v_ComponentFieldValuesRestrictionId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                componentfieldvaluesrestriction objEntity = componentfieldvaluesrestrictionAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.componentfieldvaluesrestriction.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "RESTRICCIÓN", "v_ComponentFieldValuesRestrictionId=" + objEntity.v_ComponentFieldValuesRestrictionId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "RESTRICCIÓN", "v_ComponentFieldValuesRestrictionId=" + pobjDtoEntity.v_ComponentFieldValuesRestrictionId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }


        #endregion

        #region Recommendation

        public List<ComponentFieldValuesRecommendationList> GetRecommendationsPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.componentfieldvaluesrecommendation
                            join B in dbContext.masterrecommendationrestricction on A.v_MasterRecommendationRestricctionId equals B.v_MasterRecommendationRestricctionId
                            //join B in dbContext.datahierarchy on new { a = A.i_RecommendationId.Value, b = 112 } equals new { a = B.i_ItemId, b = B.i_GroupId }

                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                            equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()

                            where A.i_IsDeleted == 0
                            select new ComponentFieldValuesRecommendationList
                            {
                                v_ComponentFieldValuesRecommendationId = A.v_ComponentFieldValuesRecommendationId,
                                v_ComponentFieldValuesId = A.v_ComponentFieldValuesId,
                                v_MasterRecommendationRestricctionId = A.v_MasterRecommendationRestricctionId,
                                v_RecomendationName = B.v_Name,
                                i_RecordStatus = (int)RecordStatus.Grabado,
                                i_RecordType = (int)RecordType.NoTemporal,
                                v_CreationUser = J1.v_UserName,
                                v_UpdateUser = J2.v_UserName,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate
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

                List<ComponentFieldValuesRecommendationList> objData = query.ToList();
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

        public void AddRecommendation(ref OperationResult pobjOperationResult, componentfieldvaluesrecommendationDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                componentfieldvaluesrecommendation objEntity = componentfieldvaluesrecommendationAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 31), "VC");
                objEntity.v_ComponentFieldValuesRecommendationId = NewId;
                objEntity.v_MasterRecommendationRestricctionId = pobjDtoEntity.v_ComponentFieldValuesRecommendationId;


                dbContext.AddTocomponentfieldvaluesrecommendation(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "RECOMENDACIÓN", "v_ComponentFieldValuesRecommendationId=" + NewId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "RECOMENDACIÓN", "v_ComponentFieldValuesRecommendationId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void UpdateRestriction(ref OperationResult pobjOperationResult, componentfieldvaluesrecommendationDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.componentfieldvaluesrecommendation
                                       where a.v_ComponentFieldValuesId == pobjDtoEntity.v_ComponentFieldValuesRecommendationId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                componentfieldvaluesrecommendation objEntity = componentfieldvaluesrecommendationAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.componentfieldvaluesrecommendation.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "RECOMENDACIÓN", "v_ComponentFieldValuesRecommendationId=" + objEntity.v_ComponentFieldValuesRecommendationId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "RECOMENDACIÓN", "v_ComponentFieldValuesRecommendationId=" + pobjDtoEntity.v_ComponentFieldValuesRecommendationId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }


        public List<RecomendationList> ObtenerListaRecomendaciones(string pComponentFieldValuesId, string pServiceId, string pComponentId)
        {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              var query = (from A in dbContext.componentfieldvaluesrecommendation
                           where A.v_ComponentFieldValuesId == pComponentFieldValuesId
                           select new RecomendationList
                           {
                               v_MasterRecommendationRestrictionId = A.v_MasterRecommendationRestricctionId,
                               v_MasterRecommendationId = A.v_MasterRecommendationRestricctionId,
                               v_ComponentId =pComponentId,
                               v_ServiceId = pServiceId,
                               i_RecordType = (int)RecordType.Temporal,
                               i_RecordStatus = (int)RecordStatus.Agregado
                           }).ToList();


              return query;
        }

        public List<RecomendationList> ObtenerListaRecomendacionesFrecuentes(string pDxFrecuenteId,string pServiceId, string pComponentId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var query = (from A in dbContext.dxfrecuentedetalle
                         where A.v_DxFrecuenteId == pDxFrecuenteId && A.i_IsDeleted == 0
                         select new RecomendationList
                         {
                             v_MasterRecommendationRestrictionId = A.v_MasterRecommendationRestricctionId,
                             v_MasterRecommendationId = A.v_MasterRecommendationRestricctionId,
                             v_ComponentId = pComponentId,
                             v_ServiceId = pServiceId,
                             i_RecordType = (int)RecordType.Temporal,
                             i_RecordStatus = (int)RecordStatus.Agregado
                         }).ToList();


            return query;
        }

        #endregion

        # region Códigos de Empresa

        public List<CodigoEmpresaList> GetCodigoEmpresaPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from B in dbContext.codigoempresa
                             //join B in dbContext.diseases on new { v_CIE10Id = A.v_CIE10Id }
                             //                               equals new { v_CIE10Id = B.v_CIE10Id } into B_join
                             //from B in B_join.DefaultIfEmpty()

                             join J1 in dbContext.systemuser on new { i_InsertUserId = B.i_InsertUserId.Value }
                                                             equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                             from J1 in J1_join.DefaultIfEmpty()

                             join J2 in dbContext.systemuser on new { i_UpdateUserId = B.i_UpdateUserId.Value }
                                                             equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                             from J2 in J2_join.DefaultIfEmpty()
                             select new CodigoEmpresaList
                             {
                                 v_CodigoEmpresaId = B.v_CodigoEmpresaId,
                                 v_CIIUId = B.v_CIIUId,
                                 v_Name = B.v_Name,
                                 v_CIIUDescription1 = null,
                                 v_CreationUser = J1.v_UserName,
                                 v_UpdateUser = J2.v_UserName,
                                 d_CreationDate = B.d_InsertDate,
                                 d_UpdateDate = B.d_UpdateDate
                             }).Union(from A in dbContext.ciiui
                                      join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                                      equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                                      from J1 in J1_join.DefaultIfEmpty()

                                      join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                                      equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                                      from J2 in J2_join.DefaultIfEmpty()
                                      select new CodigoEmpresaList
                                      {
                                          v_CodigoEmpresaId = null,
                                          v_CIIUId = A.v_CIIUId,
                                          v_Name = A.v_CIIUDescription1,
                                          v_CIIUDescription1 = A.v_CIIUDescription1,
                                          v_CreationUser = J1.v_UserName,
                                          v_UpdateUser = J2.v_UserName,
                                          d_CreationDate = A.d_InsertDate,
                                          d_UpdateDate = A.d_UpdateDate
                                      });


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

                List<CodigoEmpresaList> objData = query.ToList();
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

        public string AddCodigoEmpresa(ref OperationResult pobjOperationResult, codigoempresaDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                codigoempresa objEntity = codigoempresaAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 203), "VZ");
                objEntity.v_CodigoEmpresaId = NewId;


                dbContext.AddTocodigoempresa(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ENFERMEDAD", "v_Diseases=" + NewId.ToString(), Success.Ok, null);
                return NewId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ENFERMEDAD", "v_Diseases=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return null;
            }
        }

        public codigoempresaDto GetCodigoEmpresa(ref OperationResult pobjOperationResult, string pstrCodigoEmpresaId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                codigoempresaDto objDtoEntity = null;

                var objEntity = (from a in dbContext.codigoempresa
                                 where a.v_CodigoEmpresaId == pstrCodigoEmpresaId && a.i_IsDeleted == 0
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = codigoempresaAssembler.ToDTO(objEntity);

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

        public void UpdateCodigoEmpresa(ref OperationResult pobjOperationResult, codigoempresaDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.codigoempresa
                                       where a.v_CodigoEmpresaId == pobjDtoEntity.v_CodigoEmpresaId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                codigoempresa objEntity = codigoempresaAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.codigoempresa.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ENFERMEDAD", "v_Diseases=" + objEntity.v_DiseasesId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                //LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ENFERMEDAD", "v_Diseases=" + pobjDtoEntity.v_DiseasesId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public codigoempresaDto GetIsValidateCodigoEmpresa(ref OperationResult pobjOperationResult, string pstrCodigoEmpresaNombre)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                codigoempresaDto objDtoEntity = null;

                var objEntity = (from a in dbContext.codigoempresa
                                 where a.v_Name == pstrCodigoEmpresaNombre && a.i_IsDeleted == 0
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = codigoempresaAssembler.ToDTO(objEntity);

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

        #endregion


        public string ObtenerComponentDx(string pComponentFieldId)
        {

            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var query = (from A in dbContext.componentfields
                         where A.v_ComponentFieldId == pComponentFieldId && A.i_IsDeleted ==0

                         select new
                         {
                             ComponentDxId = A.v_ComponentId
                         }).FirstOrDefault();


            return query.ComponentDxId;
        
        }
    }
}
