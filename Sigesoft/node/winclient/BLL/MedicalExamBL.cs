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
  public  class MedicalExamBL
    {
      public List<MedicalExamList> GetMedicalExamPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
      {
          //mon.IsActive = true;

          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              var query = from A in dbContext.component
                          
                          join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                          equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                          from J1 in J1_join.DefaultIfEmpty()

                          join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                          equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                          from J2 in J2_join.DefaultIfEmpty()

                          join B in dbContext.systemparameter on new { a = A.i_CategoryId.Value, b = 116 }  // CATEGORIA DEL EXAMEN
                                                          equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                          from B in B_join.DefaultIfEmpty()
                                       
                          join E in dbContext.systemparameter on new { a = A.i_DiagnosableId.Value, b = 111 } equals new { a = E.i_ParameterId, b = E.i_GroupId }
                          join F in dbContext.systemparameter on new { a = A.i_ComponentTypeId.Value, b = 126 } equals new { a = F.i_ParameterId, b = F.i_GroupId }

                          where A.i_IsDeleted == 0
                          select new MedicalExamList
                          {
                              v_MedicalExamId = A.v_ComponentId,
                              v_Name = A.v_Name,
                              i_CategoryId = A.i_CategoryId.Value,
                              v_CategoryName = B.v_Value1,
                              v_DiagnosableName = E.v_Value1,
                              v_ComponentTypeName = F.v_Value1,
                              v_CreationUser = J1.v_UserName,
                              v_UpdateUser = J2.v_UserName,
                              d_CreationDate = A.d_InsertDate,
                              d_UpdateDate = A.d_UpdateDate,
                              i_IsDeleted = A.i_IsDeleted.Value,
                              r_BasePrice = A.r_BasePrice,
                              i_ComponentTypeId = A.i_ComponentTypeId,
                              i_UIIndex = A.i_UIIndex.Value
                              
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

              List<MedicalExamList> objData = query.ToList();
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

      public void AddMedicalExam(ref OperationResult pobjOperationResult, componentDto pobjDtoEntity, List<string> ClientSession)
      {
          //mon.IsActive = true;
          string NewId = "(No generado)";
          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              component objEntity = componentAssembler.ToEntity(pobjDtoEntity);

              //var query = from A in dbContext.component

              var Result = (from A in dbContext.component
                            where A.i_IsDeleted == 0 && A.v_Name == pobjDtoEntity.v_Name
                            select A).FirstOrDefault();
              if (Result != null)
              {
                  pobjOperationResult.ErrorMessage = "El nombre ya se encuentra registrado";
                  pobjOperationResult.Success = 0;
                  return;
              }

              objEntity.d_InsertDate = DateTime.Now;
              objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
              objEntity.i_IsDeleted = 0;
              // Autogeneramos el Pk de la tabla
              int intNodeId = int.Parse(ClientSession[0]);
              NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 17), "ME");
              objEntity.v_ComponentId = NewId;


              dbContext.AddTocomponent(objEntity);
              dbContext.SaveChanges();

              pobjOperationResult.Success = 1;
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "EXAMEN MÉDICO", "v_MedicalExamId=" + NewId.ToString(), Success.Ok, null);
              return;
          }
          catch (Exception ex)
          {
              pobjOperationResult.Success = 0;
              pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "EXAMEN MÉDICO", "v_MedicalExamId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
              return;
          }
      }

      public componentDto GetMedicalExam(ref OperationResult pobjOperationResult, string pstrMedicalExamId)
      {
          //mon.IsActive = true;

          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              componentDto objDtoEntity = null;

              var objEntity = (from a in dbContext.component
                               where a.v_ComponentId == pstrMedicalExamId
                               select a).FirstOrDefault();

              if (objEntity != null)
                  objDtoEntity = componentAssembler.ToDTO(objEntity);

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

      public int GetMedicalExamCount(ref OperationResult pobjOperationResult, string filterExpression)
      {
          //mon.IsActive = true;
          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              var query = from a in dbContext.component select a;

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

      public void UpdateMedicalExam(ref OperationResult pobjOperationResult, bool pbIsChangeName ,componentDto pobjDtoEntity, List<string> ClientSession)
      {
          //mon.IsActive = true;

          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

              #region Validar Nombre de componente

              if (pobjDtoEntity !=null && pbIsChangeName == true)
              {
                OperationResult objOperationResult6 = new OperationResult();
                string strfilterExpression1 = string.Format("v_Name==\"{0}\"&&i_Isdeleted==0", pobjDtoEntity.v_Name);

                var _recordCount1 = GetMedicalExamCount(ref objOperationResult6, strfilterExpression1);

                if (_recordCount1 != 0)
                {
                    pobjOperationResult.ErrorMessage = "El nombre del componente ya existe";
                    return ;
                }
              }
              #endregion
              // Obtener la entidad fuente
              var objEntitySource = (from a in dbContext.component
                                     where a.v_ComponentId == pobjDtoEntity.v_ComponentId
                                     select a).FirstOrDefault();

              // Crear la entidad con los datos actualizados
              pobjDtoEntity.d_UpdateDate = DateTime.Now;
              pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
              component objEntity = componentAssembler.ToEntity(pobjDtoEntity);

              // Copiar los valores desde la entidad actualizada a la Entidad Fuente
              dbContext.component.ApplyCurrentValues(objEntity);

              // Guardar los cambios
              dbContext.SaveChanges();

              pobjOperationResult.Success = 1;
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "EXAMEN MÉDICO", "v_MedicalExamId=" + objEntity.v_ComponentId.ToString(), Success.Ok, null);
              return;
          }
          catch (Exception ex)
          {
              pobjOperationResult.Success = 0;
              pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "EXAMEN MÉDICO", "v_MedicalExamId=" + pobjDtoEntity.v_ComponentId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
              return;
          }
      }

      public void DeleteMedicalExam(ref OperationResult pobjOperationResult, string pstrMedicalExamId, List<string> ClientSession)
      {
          //mon.IsActive = true;

          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

              // Obtener la entidad fuente
              var objEntitySource = (from a in dbContext.component
                                     where a.v_ComponentId == pstrMedicalExamId
                                     select a).FirstOrDefault();

              // Crear la entidad con los datos actualizados
              objEntitySource.d_UpdateDate = DateTime.Now;
              objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
              objEntitySource.i_IsDeleted = 1;

              // Guardar los cambios
              dbContext.SaveChanges();

              pobjOperationResult.Success = 1;
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "EXAMEN MÉDICO", "", Success.Ok, null);
              return;
          }
          catch (Exception ex)
          {
              pobjOperationResult.Success = 0;
              pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "EXAMEN MÉDICO", "", Success.Failed, pobjOperationResult.ExceptionMessage);
              return;
          }
      }

      public bool VerificarComponentePorCategoria(int pintCategoria, string pstrComponente)
      {
          SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

          var objEntity = (from a in dbContext.component
                           where a.i_CategoryId == pintCategoria && a.v_ComponentId== pstrComponente
                           select a).FirstOrDefault();

          if (objEntity != null)
          {
              return true;
          }
          else
          {
              return false;
          }
          
      }

    }
}
