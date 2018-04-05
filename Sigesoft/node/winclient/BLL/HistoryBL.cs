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
   public class HistoryBL
   {
       //Devart.Data.PostgreSql.PgSqlMonitor mon = new Devart.Data.PostgreSql.PgSqlMonitor();

       #region "History"
    
       public List<HistoryList> GetHistoryPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, string pstrPersonId)
       {
           //mon.IsActive = true;

           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               var query = from A in dbContext.history
                           join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                              equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                           from J1 in J1_join.DefaultIfEmpty()

                           join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                           equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                           from J2 in J2_join.DefaultIfEmpty()

                           where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId

                           select new HistoryList
                           {
                               v_HistoryId = A.v_HistoryId,
                               d_StartDate = A.d_StartDate,
                               d_EndDate = A.d_EndDate,
                               v_Organization = A.v_Organization,
                               v_TypeActivity = A.v_TypeActivity,
                               i_GeografixcaHeight = A.i_GeografixcaHeight,
                               v_workstation = A.v_workstation,
                               v_CreationUser = J1.v_UserName,
                               v_UpdateUser = J2.v_UserName,
                               d_CreationDate = A.d_InsertDate,
                               d_UpdateDate = A.d_UpdateDate,
                               b_FingerPrintImage = A.b_FingerPrintImage,
                               b_RubricImage = A.b_RubricImage,
                               t_RubricImageText = A.t_RubricImageText
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

               List<HistoryList> objData = query.ToList();

               var q = (from A in objData
                        let date1 = A.d_StartDate == null ? "" : A.d_StartDate.Value.ToString("MMMM / yyyy")
                        let date2 = A.d_EndDate == null ? "" : A.d_EndDate.Value.ToString("MMMM / yyyy")
                        select new HistoryList
                        {
                            v_HistoryId = A.v_HistoryId,
                            d_StartDate = A.d_StartDate,
                            d_EndDate = A.d_EndDate,
                            v_StartDate = date1,
                            v_EndDate = date2,
                            v_Organization = A.v_Organization,
                            v_TypeActivity = A.v_TypeActivity,
                            i_GeografixcaHeight = A.i_GeografixcaHeight,
                            v_workstation = A.v_workstation,
                            v_CreationUser = A.v_CreationUser,
                            v_UpdateUser = A.v_UpdateUser,
                            d_CreationDate = A.d_CreationDate,
                            d_UpdateDate = A.d_UpdateDate,
                            b_FingerPrintImage = A.b_FingerPrintImage,
                            b_RubricImage = A.b_RubricImage,
                            t_RubricImageText = A.t_RubricImageText
                        }).ToList();

               pobjOperationResult.Success = 1;
               return q;

           }
           catch (Exception ex)
           {
               pobjOperationResult.Success = 0;
               pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
               return null;
           }
       }

       public historyDto GetHistory(ref OperationResult pobjOperationResult, string pstrHistoryId)
       {
           //mon.IsActive = true;

           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               historyDto objDtoEntity = null;

               var objEntity = (from a in dbContext.history
                                where a.v_HistoryId == pstrHistoryId
                                select a).FirstOrDefault();

               if (objEntity != null)
                   objDtoEntity = historyAssembler.ToDTO(objEntity);

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

       public string AddHistory(ref OperationResult pobjOperationResult, List<WorkstationDangersList> WorkstationDangersList, List<TypeOfEEPList> TypeOfEEPList, historyDto pobjDtoEntity, List<string> ClientSession)
       {
           //mon.IsActive = true;
           string NewId = "(No generado)";
           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               history objEntity = historyAssembler.ToEntity(pobjDtoEntity);

               workstationdangersDto objworkstationdangersDto = new workstationdangersDto();
               typeofeepDto objtypeofeepDto = new typeofeepDto();

               #region Historial
               objEntity.d_InsertDate = DateTime.Now;
               objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
               objEntity.i_IsDeleted = 0;
               // Autogeneramos el Pk de la tabla                 
               int intNodeId = int.Parse(ClientSession[0]);
               NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 37), "HH"); ;
               objEntity.v_HistoryId = NewId;

               dbContext.AddTohistory(objEntity);

               // Guardar los cambios
               dbContext.SaveChanges();
            
               #endregion
              
               #region Dangers
               if (WorkstationDangersList != null)
               {
                   for (int i = 0; i < WorkstationDangersList.Count; i++)
                   {
                        objworkstationdangersDto.v_HistoryId = NewId;
                        objworkstationdangersDto.i_DangerId = WorkstationDangersList[i].i_DangerId;
                        
                       // Alejandro: Campos para SMP 
                        objworkstationdangersDto.v_TimeOfExposureToNoise = WorkstationDangersList[i].v_TimeOfExposureToNoise;
                        objworkstationdangersDto.i_NoiseLevel = WorkstationDangersList[i].i_NoiseLevel;
                        objworkstationdangersDto.i_NoiseSource = WorkstationDangersList[i].i_NoiseSource;
                        //****************** 
                       
                       AddWorkstationDangers(ref pobjOperationResult, objworkstationdangersDto, ClientSession);
                   }
               }
               #endregion

               #region EPP
               if (TypeOfEEPList != null)
               {
                   for (int i = 0; i < TypeOfEEPList.Count; i++)
                   {
                       objtypeofeepDto.v_HistoryId = NewId;
                       objtypeofeepDto.i_TypeofEEPId = TypeOfEEPList[i].i_TypeofEEPId;
                       objtypeofeepDto.r_Percentage = TypeOfEEPList[i].r_Percentage;

                       AddTypeOfEEPP(ref pobjOperationResult, objtypeofeepDto, ClientSession);
                   }
               }
               #endregion

              

               pobjOperationResult.Success = 1;
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "HISTORIA", "v_HistoryId=" + NewId.ToString(), Success.Ok, null);
               return NewId;
           }
           catch (Exception ex)
           {
               pobjOperationResult.Success = 0;
               pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "HISTORIA", "v_HistoryId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
               return null;
           }
       }

       public void UpdateHistory(ref OperationResult pobjOperationResult, List<workstationdangersDto> pobjWorkstationDangersAdd, List<workstationdangersDto> pobjWorkstationDangersDelete, List<workstationdangersDto> pobjWorkstationDangersUpdate, List<typeofeepDto> pobjTypeOfEEPAdd, List<typeofeepDto> pobjTypeOfEppUpdate ,List<typeofeepDto> pobjTypeOfEEPDelete, historyDto pobjDtoEntity, List<string> ClientSession)
       {
           //mon.IsActive = true;

           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               
               #region Actualizar Cabecera
               // Obtener la entidad fuente
               var objEntitySource = (from a in dbContext.history
                                      where a.v_HistoryId == pobjDtoEntity.v_HistoryId
                                      select a).FirstOrDefault();

               // Crear la entidad con los datos actualizados
               pobjDtoEntity.d_UpdateDate = DateTime.Now;
               pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
               history objEntity = historyAssembler.ToEntity(pobjDtoEntity);

               // Copiar los valores desde la entidad actualizada a la Entidad Fuente
               dbContext.history.ApplyCurrentValues(objEntity);

              
               #endregion

               #region Crear Danger
               int intNodeId = int.Parse(ClientSession[0]);
               foreach (var item in pobjWorkstationDangersAdd)
               {
                   workstationdangers objEntity1 = new workstationdangers();

                   objEntity1.d_InsertDate = DateTime.Now;
                   objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                   objEntity1.i_IsDeleted = 0;

                   var NewId1 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 39), "HW");
                   objEntity1.v_WorkstationDangersId = NewId1;
                   objEntity1.v_HistoryId = pobjDtoEntity.v_HistoryId;
                   objEntity1.i_DangerId = item.i_DangerId;

                   objEntity1.v_TimeOfExposureToNoise = item.v_TimeOfExposureToNoise;
                   objEntity1.i_NoiseLevel = item.i_NoiseLevel;
                   objEntity1.i_NoiseSource = item.i_NoiseSource;

                   dbContext.AddToworkstationdangers(objEntity1);

               }
              
               #endregion

               #region Eliminar Danger
               if (pobjWorkstationDangersDelete !=null)
               {
                   foreach (var item in pobjWorkstationDangersDelete)
                   {
                        // Obtener la entidad fuente
                        var objEntitySource1 = (from a in dbContext.workstationdangers
                                                where a.v_WorkstationDangersId == item.v_WorkstationDangersId
                                                select a).FirstOrDefault();

                        // Crear la entidad con los datos actualizados
                        objEntitySource1.d_UpdateDate = DateTime.Now;
                        objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                        objEntitySource1.i_IsDeleted = 1;
                   }
               }
             
               #endregion

               #region Actualizar Danger
               if (pobjWorkstationDangersUpdate != null)
               {
                   // Actualizar Componentes del protocolo
                   foreach (var item in pobjWorkstationDangersUpdate)
                   {
                       // Obtener la entidad fuente
                       var objEntitySource1 = (from a in dbContext.workstationdangers
                                               where a.v_WorkstationDangersId == item.v_WorkstationDangersId
                                               select a).FirstOrDefault();

                       // Crear la entidad con los datos actualizados

                       //objEntitySource1.v_ComponentId = item.v_ComponentId;
                       objEntitySource1.v_TimeOfExposureToNoise = item.v_TimeOfExposureToNoise;
                       objEntitySource1.i_NoiseLevel = item.i_NoiseLevel;
                       objEntitySource1.i_NoiseSource = item.i_NoiseSource;

                       objEntitySource1.i_IsDeleted = 0;

                       objEntitySource1.d_UpdateDate = DateTime.Now;
                       objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                   }
               }
             
               #endregion

               #region Crear EPP
               foreach (var item in pobjTypeOfEEPAdd)
               {
                   typeofeep objEntity1 = new typeofeep();
                   objEntity1.d_InsertDate = DateTime.Now;
                   objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                   objEntity1.i_IsDeleted = 0;

                   var NewId1 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 38), "HE");
                   objEntity1.v_TypeofEEPId = NewId1;
                   objEntity1.v_HistoryId = pobjDtoEntity.v_HistoryId;
                   objEntity1.i_TypeofEEPId = item.i_TypeofEEPId;
                   objEntity1.r_Percentage = item.r_Percentage;
                   dbContext.AddTotypeofeep(objEntity1);
               }
              
               #endregion

               #region Eliminar EPP

               if (pobjTypeOfEEPDelete !=null)
               {
                   foreach (var item in pobjTypeOfEEPDelete)
                   {
                       // Obtener la entidad fuente
                       var objEntitySource1 = (from a in dbContext.typeofeep
                                               where a.v_TypeofEEPId == item.v_TypeofEEPId
                                               select a).FirstOrDefault();

                       // Crear la entidad con los datos actualizados
                       objEntitySource1.d_UpdateDate = DateTime.Now;
                       objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                       objEntitySource1.i_IsDeleted = 1;

                   }
               }
              
               #endregion

               #region Actualizar EPP
               if (pobjTypeOfEppUpdate != null)
                {
                    // Actualizar Componentes del protocolo
                    foreach (var item in pobjTypeOfEppUpdate)
                    {
                        // Obtener la entidad fuente
                        var objEntitySource1 = (from a in dbContext.typeofeep
                                                where a.v_TypeofEEPId == item.v_TypeofEEPId
                                                select a).FirstOrDefault();

                        // Crear la entidad con los datos actualizados

                        //objEntitySource1.v_ComponentId = item.v_ComponentId;
                        objEntitySource1.r_Percentage = item.r_Percentage;
                        objEntitySource1.i_IsDeleted = 0;

                        objEntitySource1.d_UpdateDate = DateTime.Now;
                        objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                    }
                }
             
               #endregion

               // Guardar los cambios
               dbContext.SaveChanges();

               pobjOperationResult.Success = 1;
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "HISTORIA", "v_HistoryId=" + objEntity.v_HistoryId.ToString(), Success.Ok, null);
               return;
           }
           catch (Exception ex)
           {
               pobjOperationResult.Success = 0;
               pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "HISTORIA", "v_HistoryId=" + pobjDtoEntity.v_HistoryId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
               return;
           }
       }     
       
       public void DeleteHistory(ref OperationResult pobjOperationResult, string pstrHistoryId, List<string> ClientSession)
       {
           //mon.IsActive = true;

           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

               // Obtener la entidad fuente
               var objEntitySource = (from a in dbContext.history
                                      where a.v_HistoryId == pstrHistoryId
                                      select a).FirstOrDefault();

               // Crear la entidad con los datos actualizados
               objEntitySource.d_UpdateDate = DateTime.Now;
               objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
               objEntitySource.i_IsDeleted = 1;

               // Guardar los cambios
               dbContext.SaveChanges();

               pobjOperationResult.Success = 1;
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "HISTORIA", "", Success.Ok, null);
               return;
           }
           catch (Exception ex)
           {
               pobjOperationResult.Success = 0;
               pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "HISTORIA", "", Success.Failed, pobjOperationResult.ExceptionMessage);
               return;
           }
       }

       public void UpdateHistoryFingerRubric(ref OperationResult pobjOperationResult, historyDto pobjDtoEntity, List<string> ClientSession)
       {
           SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

           try
           {
               // Obtener la entidad fuente
               var objEntitySource = (from a in dbContext.history
                                      where a.v_HistoryId == pobjDtoEntity.v_HistoryId
                                      select a).FirstOrDefault();

               // Crear la entidad con los datos actualizados
               pobjDtoEntity.d_UpdateDate = DateTime.Now;
               pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
               history objEntity = historyAssembler.ToEntity(pobjDtoEntity);

               // Copiar los valores desde la entidad actualizada a la Entidad Fuente
               dbContext.history.ApplyCurrentValues(objEntity);

               // Guardar los cambios
               dbContext.SaveChanges();
           }
           catch (Exception ex)
           {
               
               throw;
           }
          
       }

       public List<HistoryList> GetHistoryReport(string pstrPersonId)
       {
           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               var query = (from A in dbContext.history
                            //join B in dbContext.workstationdangers on A.v_HistoryId equals B.v_HistoryId
                            //join B1 in dbContext.systemparameter on new { a = B.i_DangerId.Value, b = 145 } equals new { a = B1.i_ParameterId, b = B1.i_GroupId }
                            //join C in dbContext.typeofeep on A.v_HistoryId equals C.v_HistoryId
                            //join C1 in dbContext.systemparameter on new { a = C.i_TypeofEEPId.Value, b = 146 } equals new { a = C1.i_ParameterId, b = C1.i_GroupId }
                            join D in dbContext.systemparameter on new { a = A.i_TypeOperationId.Value, b = 204 } equals new { a = D.i_ParameterId, b = D.i_GroupId } into D_join
                            from D in D_join.DefaultIfEmpty()


                            where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId 
                            //&& B.i_IsDeleted == 0 && C.i_IsDeleted == 0

                            select new HistoryList
                            {
                                v_HistoryId = A.v_HistoryId,
                                d_StartDate = A.d_StartDate,
                                d_EndDate = A.d_EndDate,
                                v_Organization = A.v_Organization,
                                v_TypeActivity = A.v_TypeActivity,
                                i_GeografixcaHeight = A.i_GeografixcaHeight,
                                v_workstation = A.v_workstation,
                                d_CreationDate = A.d_InsertDate,
                                d_UpdateDate = A.d_UpdateDate,
                                b_FingerPrintImage = A.b_FingerPrintImage,
                                b_RubricImage = A.b_RubricImage,
                                t_RubricImageText = A.t_RubricImageText,
                                v_TypeOperationName = D.v_Value1
                            }).ToList();
               var q = (from a in query
                        let xxx = new ServiceBL().GetYearsAndMonth(a.d_EndDate, a.d_StartDate)
                        select new HistoryList
                        {
                            v_HistoryId = a.v_HistoryId,
                            d_StartDate = a.d_StartDate,
                            d_EndDate = a.d_EndDate,
                            v_Organization = a.v_Organization,
                            v_TypeActivity = a.v_TypeActivity,
                            i_GeografixcaHeight = a.i_GeografixcaHeight,
                            v_workstation = a.v_workstation,
                            d_CreationDate = a.d_CreationDate,
                            d_UpdateDate = a.d_UpdateDate,
                            b_FingerPrintImage = a.b_FingerPrintImage,
                            b_RubricImage = a.b_RubricImage,
                            t_RubricImageText = a.t_RubricImageText,
                            Fecha = "Fecha Inicio: " + a.d_StartDate.ToString().Substring(4, 7) + "  Fecha Fin: " + a.d_EndDate.ToString().Substring(4, 7),
                            Exposicion = ConcatenateExposiciones(a.v_HistoryId),
                            Epps = ConcatenateEpps(a.v_HistoryId),
                            v_TypeOperationName = a.v_TypeOperationName,
                            TiempoLabor = xxx
                        }).ToList();
               List<HistoryList> objData = q.ToList();
               return objData;

           }
           catch (Exception ex)
           {
               return null;
           }
       }

       private string ConcatenateExposiciones(string pstrHistoryId)
       {
           SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

           var qry = (from a in dbContext.workstationdangers
                      join B1 in dbContext.systemparameter on new { a = a.i_DangerId.Value, b = 145 } equals new { a = B1.i_ParameterId, b = B1.i_GroupId }
                      where a.v_HistoryId == pstrHistoryId &&
                      a.i_IsDeleted == 0
                      select new
                      {
                          v_Exposicion = B1.v_Value1
                      }).ToList();

           return string.Join(", ", qry.Select(p => p.v_Exposicion));
       }

       private string ConcatenateEpps(string pstrHistoryId)
       {
           SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

           var qry = (from a in dbContext.typeofeep
                      join C1 in dbContext.systemparameter on new { a = a.i_TypeofEEPId.Value, b = 146 } equals new { a = C1.i_ParameterId, b = C1.i_GroupId }
                      where a.v_HistoryId == pstrHistoryId &&
                      a.i_IsDeleted == 0
                      select new
                      {
                          v_Epps = C1.v_Value1
                      }).ToList();

           return string.Join(", ", qry.Select(p => p.v_Epps));
       }
       #endregion

       #region TypeOfEEP

       public List<TypeOfEEPList> GetTypeOfEEPPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, string pstrHistorytId)
       {
           //mon.IsActive = true;

           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               var query = from A in dbContext.typeofeep
                           join B in dbContext.systemparameter on new {a= A.i_TypeofEEPId.Value, b =146 } equals new { a = B.i_ParameterId, b= B.i_GroupId }
                           join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                        equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                           from J1 in J1_join.DefaultIfEmpty()

                           join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                           equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                           from J2 in J2_join.DefaultIfEmpty()

                           where A.i_IsDeleted == 0 && A.v_HistoryId == pstrHistorytId

                           select new TypeOfEEPList
                           {
                               v_TypeofEEPId = A.v_TypeofEEPId,
                               v_TypeofEEPName =B.v_Value1,
                               i_TypeofEEPId = A.i_TypeofEEPId,
                               r_Percentage = A.r_Percentage,
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

               List<TypeOfEEPList> objData = query.ToList();
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

       public typeofeepDto GetTypeOfEEPP(ref OperationResult pobjOperationResult, string pstrTypeOfEEPId)
       {
           //mon.IsActive = true;

           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               typeofeepDto objDtoEntity = null;

               var objEntity = (from a in dbContext.typeofeep
                                where a.v_TypeofEEPId == pstrTypeOfEEPId
                                select a).FirstOrDefault();

               if (objEntity != null)
                   objDtoEntity = typeofeepAssembler.ToDTO(objEntity);

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

       public void AddTypeOfEEPP(ref OperationResult pobjOperationResult, typeofeepDto pobjDtoEntity, List<string> ClientSession)
       {
           //mon.IsActive = true;
           string NewId = "(No generado)";
           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               typeofeep objEntity = typeofeepAssembler.ToEntity(pobjDtoEntity);

               objEntity.d_InsertDate = DateTime.Now;
               objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
               objEntity.i_IsDeleted = 0;
               // Autogeneramos el Pk de la tabla                 
               int intNodeId = int.Parse(ClientSession[0]);
               NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 38), "HE"); ;
               objEntity.v_TypeofEEPId = NewId;

               dbContext.AddTotypeofeep(objEntity);
               dbContext.SaveChanges();

               pobjOperationResult.Success = 1;
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "TIPO DE EEP", "v_TypeofEEPId=" + NewId.ToString(), Success.Ok, null);
               return;
           }
           catch (Exception ex)
           {
               pobjOperationResult.Success = 0;
               pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "TIPO DE EEP", "v_TypeofEEPId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
               return;
           }
       }

       public void UpdateTypeOfEEPP(ref OperationResult pobjOperationResult, typeofeepDto pobjDtoEntity, List<string> ClientSession)
       {
           //mon.IsActive = true;

           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

               // Obtener la entidad fuente
               var objEntitySource = (from a in dbContext.typeofeep
                                      where a.v_TypeofEEPId == pobjDtoEntity.v_TypeofEEPId
                                      select a).FirstOrDefault();

               // Crear la entidad con los datos actualizados
               pobjDtoEntity.d_UpdateDate = DateTime.Now;
               pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
               typeofeep objEntity = typeofeepAssembler.ToEntity(pobjDtoEntity);

               // Copiar los valores desde la entidad actualizada a la Entidad Fuente
               dbContext.typeofeep.ApplyCurrentValues(objEntity);

               // Guardar los cambios
               dbContext.SaveChanges();

               pobjOperationResult.Success = 1;
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "TIPO DE EEP", "v_TypeofEEPId=" + objEntity.v_TypeofEEPId.ToString(), Success.Ok, null);
               return;
           }
           catch (Exception ex)
           {
               pobjOperationResult.Success = 0;
               pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "TIPO DE EEP", "v_TypeofEEPId=" + pobjDtoEntity.v_TypeofEEPId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
               return;
           }
       }

       public void DeleteTypeOfEEPP(ref OperationResult pobjOperationResult, string pstrTypeOfEEPPId, List<string> ClientSession)
       {
           //mon.IsActive = true;

           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

               // Obtener la entidad fuente
               var objEntitySource = (from a in dbContext.typeofeep
                                      where a.v_TypeofEEPId == pstrTypeOfEEPPId
                                      select a).FirstOrDefault();

               // Crear la entidad con los datos actualizados
               objEntitySource.d_UpdateDate = DateTime.Now;
               objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
               objEntitySource.i_IsDeleted = 1;

               // Guardar los cambios
               dbContext.SaveChanges();

               pobjOperationResult.Success = 1;
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "TIPO DE EEP", "", Success.Ok, null);
               return;
           }
           catch (Exception ex)
           {
               pobjOperationResult.Success = 0;
               pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "TIPO DE EEP", "", Success.Failed, pobjOperationResult.ExceptionMessage);
               return;
           }
       }

       #endregion

       #region WorkstationDangers
        public List<WorkstationDangersList> GetWorkstationDangersagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, string pstrHistorytId)
       {
           //mon.IsActive = true;
           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               var query = from A in dbContext.workstationdangers
                           join B in dbContext.systemparameter on new { a = A.i_DangerId.Value, b = 145 } equals new { a = B.i_ParameterId, b = B.i_GroupId }
                           join C in dbContext.systemparameter on new {a=B.i_ParentParameterId.Value , b=145 } equals new { a=C.i_ParameterId, b=C.i_GroupId}
                           join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                               equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                           from J1 in J1_join.DefaultIfEmpty()

                           join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                           equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                           from J2 in J2_join.DefaultIfEmpty()

                           where A.i_IsDeleted == 0 && A.v_HistoryId == pstrHistorytId

                           select new WorkstationDangersList
                           {
                               v_WorkstationDangersId = A.v_WorkstationDangersId,
                               v_ParentName = C.v_Value1,
                               i_DangerId = A.i_DangerId,
                               v_DangerName = B.v_Value1,
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

               List<WorkstationDangersList> objData = query.ToList();
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

        public workstationdangersDto GetWorkstationDangers(ref OperationResult pobjOperationResult, string pstrWorkstationDangersId)
       {
           //mon.IsActive = true;

           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               workstationdangersDto objDtoEntity = null;

               var objEntity = (from a in dbContext.workstationdangers
                                where a.v_WorkstationDangersId == pstrWorkstationDangersId
                                select a).FirstOrDefault();

               if (objEntity != null)
                   objDtoEntity = workstationdangersAssembler.ToDTO(objEntity);

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

        public void AddWorkstationDangers(ref OperationResult pobjOperationResult, workstationdangersDto pobjDtoEntity, List<string> ClientSession)
       {
           //mon.IsActive = true;
           string NewId = "(No generado)";
           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               workstationdangers objEntity = workstationdangersAssembler.ToEntity(pobjDtoEntity);

               objEntity.d_InsertDate = DateTime.Now;
               objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
               objEntity.i_IsDeleted = 0;
               // Autogeneramos el Pk de la tabla                 
               int intNodeId = int.Parse(ClientSession[0]);
               NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 39), "HW"); ;
               objEntity.v_WorkstationDangersId = NewId;

               dbContext.AddToworkstationdangers(objEntity);
               dbContext.SaveChanges();

               pobjOperationResult.Success = 1;
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PELIGRO EN EL PUESTO", "v_WorkstationDangersId=" + NewId.ToString(), Success.Ok, null);
               return;
           }
           catch (Exception ex)
           {
               pobjOperationResult.Success = 0;
               pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PELIGRO EN EL PUESTO", "v_WorkstationDangersId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
               return;
           }
       }

        public void UpdateWorkstationDangers(ref OperationResult pobjOperationResult, workstationdangersDto pobjDtoEntity, List<string> ClientSession)
       {
           //mon.IsActive = true;

           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

               // Obtener la entidad fuente
               var objEntitySource = (from a in dbContext.workstationdangers
                                      where a.v_WorkstationDangersId == pobjDtoEntity.v_WorkstationDangersId
                                      select a).FirstOrDefault();

               // Crear la entidad con los datos actualizados
               pobjDtoEntity.d_UpdateDate = DateTime.Now;
               pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
               workstationdangers objEntity = workstationdangersAssembler.ToEntity(pobjDtoEntity);

               // Copiar los valores desde la entidad actualizada a la Entidad Fuente
               dbContext.workstationdangers.ApplyCurrentValues(objEntity);

               // Guardar los cambios
               dbContext.SaveChanges();

               pobjOperationResult.Success = 1;
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PELIGRO EN EL PUESTO", "v_WorkstationDangersId=" + objEntity.v_WorkstationDangersId.ToString(), Success.Ok, null);
               return;
           }
           catch (Exception ex)
           {
               pobjOperationResult.Success = 0;
               pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PELIGRO EN EL PUESTO", "v_WorkstationDangersId=" + pobjDtoEntity.v_WorkstationDangersId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
               return;
           }
       }

        public void DeleteWorkstationDangers(ref OperationResult pobjOperationResult, string pstrWorkstationDangersId, List<string> ClientSession)
       {
           //mon.IsActive = true;

           try
           {
               SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

               // Obtener la entidad fuente
               var objEntitySource = (from a in dbContext.workstationdangers
                                      where a.v_WorkstationDangersId == pstrWorkstationDangersId
                                      select a).FirstOrDefault();

               // Crear la entidad con los datos actualizados
               objEntitySource.d_UpdateDate = DateTime.Now;
               objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
               objEntitySource.i_IsDeleted = 1;

               // Guardar los cambios
               dbContext.SaveChanges();

               pobjOperationResult.Success = 1;
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "PELIGRO EN EL PUESTO", "", Success.Ok, null);
               return;
           }
           catch (Exception ex)
           {
               pobjOperationResult.Success = 0;
               pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
               // Llenar entidad Log
               LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "PELIGRO EN EL PUESTO", "", Success.Failed, pobjOperationResult.ExceptionMessage);
               return;
           }
       }
        #endregion

       #region PersonMedicalHistory

      public List<PersonMedicalHistoryList> GetPersonMedicalHistoryPagedAndFilteredByPersonId1(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, string pstrPersonId)
      {
          //mon.IsActive = true;
          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              var query = from A in dbContext.personmedicalhistory
                          join B in dbContext.systemparameter on new { a = A.v_DiseasesId, b = 147 } 
                                                            equals new { a = B.v_Value1, b = B.i_GroupId } into B_join
                          from B in B_join.DefaultIfEmpty()

                          join C in dbContext.systemparameter on new { a = B.i_ParentParameterId.Value, b = 147 } 
                                                            equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                          from C in C_join.DefaultIfEmpty()

                          join D in dbContext.diseases on A.v_DiseasesId equals D.v_DiseasesId

                          join E in dbContext.systemparameter on new { a = A.i_TypeDiagnosticId.Value, b = 139 }
                                                            equals new { a = E.i_ParameterId, b = E.i_GroupId } into E_join
                          from E in E_join.DefaultIfEmpty()

                          join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                           equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                          from J1 in J1_join.DefaultIfEmpty()

                          join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                          equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                          from J2 in J2_join.DefaultIfEmpty()
                          where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId

                          select new PersonMedicalHistoryList
                          {
                              v_PersonMedicalHistoryId = A.v_PersonMedicalHistoryId,
                              v_PersonId = A.v_PersonId,
                              v_DiseasesId = A.v_DiseasesId,
                              v_DiseasesName = D.v_Name,
                              i_TypeDiagnosticId =  A.i_TypeDiagnosticId,
                              d_StartDate = A.d_StartDate.Value,
                              v_TreatmentSite= A.v_TreatmentSite,
                              i_RecordStatus = (int)RecordStatus.Grabado,
                              i_RecordType = (int)RecordType.NoTemporal,
                              v_GroupName = C.v_Value1,
                              v_TypeDiagnosticName = E.v_Value1,
                              v_DiagnosticDetail = A.v_DiagnosticDetail,
                              v_CreationUser = J1.v_UserName,
                              v_UpdateUser = J2.v_UserName,
                              d_CreationDate = A.d_InsertDate,
                              d_UpdateDate = A.d_UpdateDate,
                              i_Answer = A.i_AnswerId.Value
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

              List<PersonMedicalHistoryList> objData = query.ToList();
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

      public List<PersonMedicalHistoryList> GetPersonMedicalHistoryPagedAndFilteredByPersonId(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, string pstrPersonId)
      {
          //mon.IsActive = true;
          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              var query = from A in dbContext.personmedicalhistory
                          join B in dbContext.systemparameter on new { a = A.v_DiseasesId, b = 147 }
                                                            equals new { a = B.v_Value1, b = B.i_GroupId } into B_join
                          from B in B_join.DefaultIfEmpty()

                          join C in dbContext.systemparameter on new { a = B.i_ParentParameterId.Value, b = 147 }
                                                            equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                          from C in C_join.DefaultIfEmpty()

                          join D in dbContext.diseases on A.v_DiseasesId equals D.v_DiseasesId

                          join E in dbContext.systemparameter on new { a = A.i_TypeDiagnosticId.Value, b = 139 }
                                                            equals new { a = E.i_ParameterId, b = E.i_GroupId } into E_join
                          from E in E_join.DefaultIfEmpty()

                          join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                           equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                          from J1 in J1_join.DefaultIfEmpty()

                          join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                          equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                          from J2 in J2_join.DefaultIfEmpty()
                          where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId && A.i_AnswerId == (int)SiNo.SI

                          select new PersonMedicalHistoryList
                          {
                              v_PersonMedicalHistoryId = A.v_PersonMedicalHistoryId,
                              v_PersonId = A.v_PersonId,
                              v_DiseasesId = A.v_DiseasesId,
                              v_DiseasesName = D.v_Name,
                              i_TypeDiagnosticId = A.i_TypeDiagnosticId,
                              d_StartDate = A.d_StartDate.Value,
                              v_TreatmentSite = A.v_TreatmentSite,
                              i_RecordStatus = (int)RecordStatus.Grabado,
                              i_RecordType = (int)RecordType.NoTemporal,
                              v_GroupName = C.v_Value1 == null ? "ENFERMEDADES OTROS" : C.v_Value1,
                              v_TypeDiagnosticName = E.v_Value1,
                              v_DiagnosticDetail = A.v_DiagnosticDetail,
                              v_CreationUser = J1.v_UserName,
                              v_UpdateUser = J2.v_UserName,
                              d_CreationDate = A.d_InsertDate,
                              d_UpdateDate = A.d_UpdateDate,
                              i_Answer = A.i_AnswerId.Value
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

              List<PersonMedicalHistoryList> objData = query.ToList();
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

      public void AddPersonMedicalHistory(ref OperationResult pobjOperationResult, List<personmedicalhistoryDto> pobjpersonmedicalhistoryAdd, List<personmedicalhistoryDto> pobjpersonmedicalhistoryUpdate, List<personmedicalhistoryDto> pobjpersonmedicalhistoryDelete, List<string> ClientSession)
      {
          string NewId = "(No generado)";
          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              int intNodeId = int.Parse(ClientSession[0]);
              #region Crear Person Medical
              foreach (var item in pobjpersonmedicalhistoryAdd)
              {
                  personmedicalhistory objEntity1 = new personmedicalhistory();
                  objEntity1.d_InsertDate = DateTime.Now;
                  objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                  objEntity1.i_IsDeleted = 0;

                  NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 40), "PH");
                  objEntity1.v_PersonMedicalHistoryId = NewId;
                  objEntity1.v_PersonId = item.v_PersonId;
                  objEntity1.v_DiseasesId = item.v_DiseasesId;
                  objEntity1.i_TypeDiagnosticId = item.i_TypeDiagnosticId;
                  objEntity1.d_StartDate = item.d_StartDate;
                  objEntity1.v_DiagnosticDetail = item.v_DiagnosticDetail;
                  objEntity1.v_TreatmentSite = item.v_TreatmentSite;
                  objEntity1.i_AnswerId = item.i_AnswerId;
                  dbContext.AddTopersonmedicalhistory(objEntity1);
              }
              // Guardar los cambios
              dbContext.SaveChanges();
              #endregion

              #region Update Person Medical
              if (pobjpersonmedicalhistoryUpdate != null)
              {
                  // Actualizar Componentes del protocolo
                  foreach (var item in pobjpersonmedicalhistoryUpdate)
                  {
                      // Obtener la entidad fuente
                      var objEntitySource1 = (from a in dbContext.personmedicalhistory
                                              where a.v_PersonMedicalHistoryId == item.v_PersonMedicalHistoryId
                                              select a).FirstOrDefault();

                      // Crear la entidad con los datos actualizados

                      objEntitySource1.i_TypeDiagnosticId = item.i_TypeDiagnosticId;
                      objEntitySource1.d_StartDate = item.d_StartDate;
                      objEntitySource1.v_DiagnosticDetail = item.v_DiagnosticDetail;
                      objEntitySource1.v_TreatmentSite = item.v_TreatmentSite;
                      objEntitySource1.i_IsDeleted = 0;

                      objEntitySource1.d_UpdateDate = DateTime.Now;
                      objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                  }
              }
              // Guardar los cambios
              dbContext.SaveChanges();
              #endregion

              #region Delete Person Medical
              if (pobjpersonmedicalhistoryDelete != null)
              {
                  foreach (var item in pobjpersonmedicalhistoryDelete)
                  {
                      // Obtener la entidad fuente
                      var objEntitySource1 = (from a in dbContext.personmedicalhistory
                                              where a.v_PersonMedicalHistoryId == item.v_PersonMedicalHistoryId
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

              pobjOperationResult.Success = 1;
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "HISTORIA MÉDICA PERSONAL", "v_PersonMedicalHistoryId=" + NewId, Success.Ok, null);
              return;
          }
          catch (Exception ex)
          {
              pobjOperationResult.Success = 0;
              pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "HISTORIA MÉDICA PERSONAL", "v_PersonMedicalHistoryId=" + NewId, Success.Failed, pobjOperationResult.ExceptionMessage);
              return;
          }

      }

      public List<PersonMedicalHistoryList> GetPersonMedicalHistoryReport(string pstrPersonId)
      {
          //mon.IsActive = true;
          try
          {
           

              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              var query = (from A in dbContext.personmedicalhistory
                        
                          join B in dbContext.systemparameter on new { a = A.v_DiseasesId, b = 147 }
                                                            equals new { a = B.v_Value1, b = B.i_GroupId } into B_join
                          from B in B_join.DefaultIfEmpty()

                          join C in dbContext.systemparameter on new { a = B.i_ParentParameterId.Value, b = 147 }
                                                            equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                          from C in C_join.DefaultIfEmpty()

                          join D in dbContext.diseases on A.v_DiseasesId equals D.v_DiseasesId

                          join E in dbContext.systemparameter on new { a = A.i_TypeDiagnosticId.Value, b = 139 }
                                                            equals new { a = E.i_ParameterId, b = E.i_GroupId } into E_join
                          from E in E_join.DefaultIfEmpty()
                        
                          where (A.i_IsDeleted == 0) && (A.v_PersonId == pstrPersonId)

                          select new PersonMedicalHistoryList
                          {
                              v_PersonMedicalHistoryId = A.v_PersonMedicalHistoryId,
                              v_PersonId = A.v_PersonId,
                              v_DiseasesId = A.v_DiseasesId,
                              v_DiseasesName = D.v_Name,
                              i_TypeDiagnosticId = A.i_TypeDiagnosticId,
                              d_StartDate = A.d_StartDate.Value,
                              v_TreatmentSite = A.v_TreatmentSite,                            
                              v_GroupName = C.v_Value1,
                              v_TypeDiagnosticName = E.v_Value1,
                              v_DiagnosticDetail = A.v_DiagnosticDetail,
                              i_Answer = A.i_AnswerId.Value,
                              
                          }).ToList();

              // add the sequence number on the fly
              var query1 = new List<PersonMedicalHistoryList>();

              query1 = query.Select((x, index) => new PersonMedicalHistoryList
                      {
                          i_Item = index + 1,
                          v_PersonMedicalHistoryId = x.v_PersonMedicalHistoryId,
                          v_PersonId = x.v_PersonId,
                          v_DiseasesId = x.v_DiseasesId,
                          v_DiseasesName = x.v_DiseasesName,
                          i_TypeDiagnosticId = x.i_TypeDiagnosticId,
                          d_StartDate = x.d_StartDate,
                          v_TreatmentSite = x.v_TreatmentSite,
                          v_GroupName = x.v_GroupName,
                          v_TypeDiagnosticName = x.v_TypeDiagnosticName,
                          v_DiagnosticDetail = x.v_DiagnosticDetail,
                          i_Answer = x.i_Answer,
                      }).ToList();

              //List<PersonMedicalHistoryList> objData = query.ToList();

              return query1;

          }
          catch (Exception ex)
          {
              return null;
          }
      }

       #endregion

       #region NoxiousHabits
      public List<NoxiousHabitsList> GetNoxiousHabitsPagedAndFilteredByPersonId(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, string pstrPersonId)
      {
          //mon.IsActive = true;
          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              var query = from A in dbContext.noxioushabits
                          join B in dbContext.systemparameter on new { a = A.i_TypeHabitsId.Value, b = 148 }
                                                         equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                          from B in B_join.DefaultIfEmpty()
                          join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                     equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                          from J1 in J1_join.DefaultIfEmpty()

                          join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                          equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                          from J2 in J2_join.DefaultIfEmpty()

                          where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId && A.i_TypeHabitsId !=0

                          select new NoxiousHabitsList
                          {
                                v_NoxiousHabitsId = A.v_NoxiousHabitsId,
                                v_NoxiousHabitsName = B.v_Value1,
                                v_PersonId = A.v_PersonId,
                                v_Frequency = A.v_Frequency,
                                v_Comment = A.v_Comment,
                                i_TypeHabitsId = B.i_ParameterId,
                                v_TypeHabitsName = B.v_Value1,
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

              List<NoxiousHabitsList> objData = query.ToList();
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

      public void AddNoxiousHabits(ref OperationResult pobjOperationResult, List<noxioushabitsDto> pobjnoxioushabitsAdd, List<noxioushabitsDto> pobjnoxioushabitsUpdate, List<noxioushabitsDto> pobjnoxioushabitsDelete, List<string> ClientSession)
      {
          string NewId = "(No generado)";
          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              int intNodeId = int.Parse(ClientSession[0]);
              #region Crear Noxious Habits
              foreach (var item in pobjnoxioushabitsAdd)
              {
                  noxioushabits objEntity1 = new noxioushabits();
                  objEntity1.d_InsertDate = DateTime.Now;
                  objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                  objEntity1.i_IsDeleted = 0;

                  NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 41), "NX");
                  objEntity1.v_NoxiousHabitsId = NewId;
                  objEntity1.v_PersonId = item.v_PersonId;
                  objEntity1.v_Frequency = item.v_Frequency;
                  objEntity1.v_Comment = item.v_Comment;
                  objEntity1.i_TypeHabitsId = item.i_TypeHabitsId;
                  dbContext.AddTonoxioushabits(objEntity1);
              }
              // Guardar los cambios
              dbContext.SaveChanges();
              #endregion

              #region Update Noxious Habits
              if (pobjnoxioushabitsUpdate != null)
              {
                  // Actualizar Componentes del protocolo
                  foreach (var item in pobjnoxioushabitsUpdate)
                  {
                      // Obtener la entidad fuente
                      var objEntitySource1 = (from a in dbContext.noxioushabits
                                              where a.v_NoxiousHabitsId == item.v_NoxiousHabitsId
                                              select a).FirstOrDefault();

                      // Crear la entidad con los datos actualizados

                      objEntitySource1.v_Frequency = item.v_Frequency;
                      objEntitySource1.v_Comment = item.v_Comment;
                      objEntitySource1.i_IsDeleted = 0;
                      objEntitySource1.d_UpdateDate = DateTime.Now;
                      objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                  }
              }
              // Guardar los cambios
              dbContext.SaveChanges();
              #endregion

              #region Delete Noxious Habits
              if (pobjnoxioushabitsDelete != null)
              {
                  foreach (var item in pobjnoxioushabitsDelete)
                  {
                      // Obtener la entidad fuente
                      var objEntitySource1 = (from a in dbContext.noxioushabits
                                              where a.v_NoxiousHabitsId == item.v_NoxiousHabitsId
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

              pobjOperationResult.Success = 1;
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "HÁBITOS NOCIVOS", "v_NoxiousHabitsId=" + NewId, Success.Ok, null);
              return;
          }
          catch (Exception ex)
          {
              pobjOperationResult.Success = 0;
              pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "HÁBITOS NOCIVOS", "v_NoxiousHabitsId=" + NewId, Success.Failed, pobjOperationResult.ExceptionMessage);
              return;
          }

      }

      public List<NoxiousHabitsList> GetNoxiousHabitsReport(string pstrPersonId)
      {
          //mon.IsActive = true;
          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              var query = (from A in dbContext.noxioushabits
                          join B in dbContext.systemparameter on new { a = A.i_TypeHabitsId.Value, b = 148 }
                                                         equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                          from B in B_join.DefaultIfEmpty()

                          where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId

                          select new NoxiousHabitsList
                          {
                              v_NoxiousHabitsId = A.v_NoxiousHabitsId,
                              v_NoxiousHabitsName = B.v_Value1,
                              v_PersonId = A.v_PersonId,
                              v_Frequency = A.v_Frequency + ", " + A.v_Comment,
                              v_Comment = A.v_Comment ,
                              i_TypeHabitsId = B.i_ParameterId,
                              v_TypeHabitsName = B.v_Value1,
                              i_RecordStatus = (int)RecordStatus.Grabado,
                              i_RecordType = (int)RecordType.NoTemporal,
                              v_DescriptionQuantity = A.v_DescriptionQuantity,
                              v_DescriptionHabit = A.v_DescriptionHabit,
                              v_FrecuenciaHabito = A.v_Frequency

                          }).ToList();


              // add the sequence number on the fly
              var query1 = query.Select((x, index) => new NoxiousHabitsList
              {
                  i_Item = index + 1,
                  v_NoxiousHabitsId = x.v_NoxiousHabitsId,
                  v_NoxiousHabitsName = x.v_NoxiousHabitsName,
                  v_PersonId = x.v_PersonId,
                  v_Frequency = x.v_Frequency,
                  v_Comment = x.v_Comment,
                  i_TypeHabitsId = x.i_TypeHabitsId,
                  v_TypeHabitsName = x.v_TypeHabitsName,
                  v_DescriptionQuantity = x.v_DescriptionQuantity,
                  v_DescriptionHabit = x.v_DescriptionHabit,
                  v_FrecuenciaHabito = x.v_FrecuenciaHabito

              }).ToList();

            
              return query1;

          }
          catch (Exception ex)
          {
              return null;
          }
      }
       #endregion

       #region FamilyMedicalAntecedents
      public List<FamilyMedicalAntecedentsList> GetFamilyMedicalAntecedentsPagedAndFilteredByPersonId(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, string pstrPersonId)
      {
          //mon.IsActive = true;
          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              var query = from A in dbContext.familymedicalantecedents
                          join B in dbContext.systemparameter on new { a = A.i_TypeFamilyId.Value, b = 149 }
                                                         equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                          from B in B_join.DefaultIfEmpty()
                          join C in dbContext.systemparameter on new { a = B.i_ParentParameterId.Value, b = 149 }
                                                       equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                          from C in C_join.DefaultIfEmpty()
                          join D in dbContext.diseases on new { a = A.v_DiseasesId}
                                                 equals new { a = D.v_DiseasesId} into D_join
                          from D in D_join.DefaultIfEmpty()

                          join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                  equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                          from J1 in J1_join.DefaultIfEmpty()

                          join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                          equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                          from J2 in J2_join.DefaultIfEmpty()

                          where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId

                          select new FamilyMedicalAntecedentsList
                          {
                              v_FamilyMedicalAntecedentsId = A.v_FamilyMedicalAntecedentsId,
                              v_PersonId = A.v_PersonId,
                              v_DiseasesId = A.v_DiseasesId,
                              v_DiseaseName = D.v_Name,
                              i_TypeFamilyId = A.i_TypeFamilyId.Value,
                              v_TypeFamilyName = C.v_Value1,
                              v_Comment = A.v_Comment,
                              i_RecordStatus = (int)RecordStatus.Grabado,
                              i_RecordType = (int)RecordType.NoTemporal,
                              v_CreationUser = J1.v_UserName,
                              v_UpdateUser = J2.v_UserName,
                              d_CreationDate = A.d_InsertDate,
                              d_UpdateDate = A.d_UpdateDate,
                              i_ParameterId = B.i_ParameterId,
                              i_ParentParameterId = B.i_ParentParameterId
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

              List<FamilyMedicalAntecedentsList> objData = query.ToList();
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

      public void AddFamilyMedicalAntecedents(ref OperationResult pobjOperationResult, List<familymedicalantecedentsDto> pobjfamilymedicalantecedentsAdd, List<familymedicalantecedentsDto> pobjfamilymedicalantecedentsUpdate, List<familymedicalantecedentsDto> pobjfamilymedicalantecedentsDelete, List<string> ClientSession)
      {
          string NewId = "(No generado)";
          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              int intNodeId = int.Parse(ClientSession[0]);
              #region Crear Antecedentes Familiares
              foreach (var item in pobjfamilymedicalantecedentsAdd)
              {
                  familymedicalantecedents objEntity1 = new familymedicalantecedents();
                  objEntity1.d_InsertDate = DateTime.Now;
                  objEntity1.i_InsertUserId = Int32.Parse(ClientSession[2]);
                  objEntity1.i_IsDeleted = 0;

                  NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 42), "FA");
                  objEntity1.v_FamilyMedicalAntecedentsId = NewId;
                  objEntity1.v_PersonId = item.v_PersonId;
                  objEntity1.v_DiseasesId = item.v_DiseasesId;
                  objEntity1.i_TypeFamilyId = item.i_TypeFamilyId;
                  objEntity1.v_Comment = item.v_Comment;
                  dbContext.AddTofamilymedicalantecedents(objEntity1);
              }
              // Guardar los cambios
              dbContext.SaveChanges();
              #endregion

              #region Update Antecedentes Familiares
              if (pobjfamilymedicalantecedentsUpdate != null)
              {
                  // Actualizar Componentes del protocolo
                  foreach (var item in pobjfamilymedicalantecedentsUpdate)
                  {
                      // Obtener la entidad fuente
                      var objEntitySource1 = (from a in dbContext.familymedicalantecedents
                                              where a.v_FamilyMedicalAntecedentsId == item.v_FamilyMedicalAntecedentsId
                                              select a).FirstOrDefault();

                      // Crear la entidad con los datos actualizados
                      objEntitySource1.v_DiseasesId = item.v_DiseasesId;
                      objEntitySource1.v_Comment = item.v_Comment;
                      objEntitySource1.i_IsDeleted = 0;

                      objEntitySource1.d_UpdateDate = DateTime.Now;
                      objEntitySource1.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                  }
              }
              // Guardar los cambios
              dbContext.SaveChanges();
              #endregion

              #region Delete Antecedentes Familiares
              if (pobjfamilymedicalantecedentsDelete != null)
              {
                  foreach (var item in pobjfamilymedicalantecedentsDelete)
                  {
                      // Obtener la entidad fuente
                      var objEntitySource1 = (from a in dbContext.familymedicalantecedents
                                              where a.v_FamilyMedicalAntecedentsId == item.v_FamilyMedicalAntecedentsId
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

              pobjOperationResult.Success = 1;
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ANTEDECENTES FAMILIARES", "v_FamilyMedicalAntecedentsId=" + NewId, Success.Ok, null);
              return;
          }
          catch (Exception ex)
          {
              pobjOperationResult.Success = 0;
              pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ANTECEDENTES FAMILIARES", "v_FamilyMedicalAntecedentsId=" + NewId, Success.Failed, pobjOperationResult.ExceptionMessage);
              return;
          }

      }

      public List<FamilyMedicalAntecedentsList> GetFamilyMedicalAntecedentsReport(string pstrPersonId)
      {
          //mon.IsActive = true;
          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              var query = (from A in dbContext.familymedicalantecedents
                           join B in dbContext.systemparameter on new { a = A.i_TypeFamilyId.Value, b = 149 }
                                                          equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                           from B in B_join.DefaultIfEmpty()
                           join C in dbContext.systemparameter on new { a = B.i_ParentParameterId.Value, b = 149 }
                                                        equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                           from C in C_join.DefaultIfEmpty()
                           join D in dbContext.diseases on new { a = A.v_DiseasesId }
                                                  equals new { a = D.v_DiseasesId } into D_join
                           from D in D_join.DefaultIfEmpty()

                           where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId

                           select new FamilyMedicalAntecedentsList
                           {
                               v_FamilyMedicalAntecedentsId = A.v_FamilyMedicalAntecedentsId,
                               v_PersonId = A.v_PersonId,
                               v_DiseasesId = A.v_DiseasesId,
                               v_DiseaseName = D.v_Name,
                               //i_TypeFamilyId = A.i_TypeFamilyId.Value,
                               i_TypeFamilyId = C.i_ParameterId,
                               v_TypeFamilyName = C.v_Value1,
                               v_Comment = A.v_Comment,
                               v_FullAntecedentName = D.v_Name + " / " + C.v_Value1 + ", " + A.v_Comment,
                               DxAndComment = D.v_Name + "," + A.v_Comment
                           }).ToList();

              // add the sequence number on the fly
              var query1 = query.Select((x, index) => new FamilyMedicalAntecedentsList
              {
                  i_Item = index + 1,
                  v_FamilyMedicalAntecedentsId = x.v_FamilyMedicalAntecedentsId,
                  v_PersonId = x.v_PersonId,
                  v_DiseasesId = x.v_DiseasesId,
                  v_DiseaseName = x.v_DiseaseName,
                  i_TypeFamilyId = x.i_TypeFamilyId,
                  v_TypeFamilyName = x.v_TypeFamilyName,
                  v_Comment = x.v_Comment,
                  v_FullAntecedentName = x.v_FullAntecedentName,
                  DxAndComment = x.DxAndComment == "" ? "NO REFIERE ANTECEDENTES" : x.DxAndComment
              }).ToList();

              return query1;

          }
          catch (Exception ex)
          {
              return null;
          }
      }

      public List<FamilyMedicalAntecedentsList> GetFamilyMedicalAntecedentsReport_(string pstrPersonId)
      {
          //mon.IsActive = true;
          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              var query = (from A in dbContext.familymedicalantecedents
                           join B in dbContext.systemparameter on new { a = A.i_TypeFamilyId.Value, b = 149 }
                                                          equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                           from B in B_join.DefaultIfEmpty()
                           join C in dbContext.systemparameter on new { a = B.i_ParentParameterId.Value, b = 149 }
                                                        equals new { a = C.i_ParameterId, b = C.i_GroupId } into C_join
                           from C in C_join.DefaultIfEmpty()
                           join D in dbContext.diseases on new { a = A.v_DiseasesId }
                                                  equals new { a = D.v_DiseasesId } into D_join
                           from D in D_join.DefaultIfEmpty()

                           where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId

                           select new FamilyMedicalAntecedentsList
                           {
                               v_FamilyMedicalAntecedentsId = A.v_FamilyMedicalAntecedentsId,
                               v_PersonId = A.v_PersonId,
                               v_DiseasesId = A.v_DiseasesId,
                               v_DiseaseName = D.v_Name,
                               //i_TypeFamilyId = A.i_TypeFamilyId.Value,
                               i_TypeFamilyId = C.i_ParameterId,
                               v_TypeFamilyName = C.v_Value1,
                               v_Comment = A.v_Comment,
                               v_FullAntecedentName = D.v_Name + " / " + C.v_Value1,
                               DxAndComment = D.v_Name + "," + A.v_Comment
                           }).ToList();

              // add the sequence number on the fly
              var query1 = query.Select((x, index) => new FamilyMedicalAntecedentsList
              {
                  i_Item = index + 1,
                  v_FamilyMedicalAntecedentsId = x.v_FamilyMedicalAntecedentsId,
                  v_PersonId = x.v_PersonId,
                  v_DiseasesId = x.v_DiseasesId,
                  v_DiseaseName = x.v_DiseaseName,
                  i_TypeFamilyId = x.i_TypeFamilyId,
                  v_TypeFamilyName = x.v_TypeFamilyName,
                  v_Comment = x.v_Comment,
                  v_FullAntecedentName = x.v_FullAntecedentName,
                  DxAndComment = x.DxAndComment == "" ? "NO REFIERE ANTECEDENTES" : x.DxAndComment
              }).ToList();

              return query1;

          }
          catch (Exception ex)
          {
              return null;
          }
      }


       #endregion


       #region Importacion Matrix 
       
      public void EliminarHystoriaPaciente(string pPersonId)
      {
          SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

          // Obtener la entidad fuente

          #region Historia Ocupacional
          
         
          var objEntitySource = (from a in dbContext.history
                                 where a.v_PersonId == pPersonId && a.i_IsDeleted==0
                                 select a).ToList();


          foreach (var itemHistoria in objEntitySource)
          {
              // Crear la entidad con los datos actualizados
              itemHistoria.d_UpdateDate = DateTime.Now;
              itemHistoria.i_UpdateUserId = null;
              itemHistoria.i_IsDeleted = 1;

              //Eliminar Peligros
              var oPeligros = (from a in dbContext.workstationdangers
                               where a.v_HistoryId == itemHistoria.v_HistoryId && a.i_IsDeleted == 0
                                     select a).ToList();

              foreach (var itemPeligros in oPeligros)
              {
            
                  // Crear la entidad con los datos actualizados
                  itemPeligros.d_UpdateDate = DateTime.Now;
                  itemPeligros.i_UpdateUserId = null;
                  itemPeligros.i_IsDeleted = 1;
              }



              //Eliminar Epps

              var oEpps = (from a in dbContext.typeofeep
                               where a.v_HistoryId == itemHistoria.v_HistoryId && a.i_IsDeleted == 0
                               select a).ToList();

              foreach (var itemEpps in oEpps)
              {

                  // Crear la entidad con los datos actualizados
                  itemEpps.d_UpdateDate = DateTime.Now;
                  itemEpps.i_UpdateUserId = null;
                  itemEpps.i_IsDeleted = 1;
              }

          }
        

          #endregion

          #region Médicos Personales


          var objSourceMedicosPersonales = (from a in dbContext.personmedicalhistory
                                 where a.v_PersonId == pPersonId && a.i_IsDeleted == 0
                                 select a).ToList();

          foreach (var itemMedicosPersonales in objSourceMedicosPersonales)
          {

              // Crear la entidad con los datos actualizados
              itemMedicosPersonales.d_UpdateDate = DateTime.Now;
              itemMedicosPersonales.i_UpdateUserId = null;
              itemMedicosPersonales.i_IsDeleted = 1;
          }



          #endregion

          #region Hábitos Noxivos
          var objSourceHabitosNoxivos= (from a in dbContext.noxioushabits
                                            where a.v_PersonId == pPersonId && a.i_IsDeleted == 0
                                            select a).ToList();

          foreach (var itemHabitosNoxivos in objSourceHabitosNoxivos)
          {

              // Crear la entidad con los datos actualizados
              itemHabitosNoxivos.d_UpdateDate = DateTime.Now;
              itemHabitosNoxivos.i_UpdateUserId = null;
              itemHabitosNoxivos.i_IsDeleted = 1;
          }

          #endregion

          #region Médicos Familiares
          var objSourceFamiliares = (from a in dbContext.familymedicalantecedents
                                         where a.v_PersonId == pPersonId && a.i_IsDeleted == 0
                                         select a).ToList();

          foreach (var itemFamiliares in objSourceFamiliares)
          {

              // Crear la entidad con los datos actualizados
              itemFamiliares.d_UpdateDate = DateTime.Now;
              itemFamiliares.i_UpdateUserId = null;
              itemFamiliares.i_IsDeleted = 1;
          }


          #endregion

          // Guardar los cambios
          dbContext.SaveChanges();
      }


       #endregion



   }
}

