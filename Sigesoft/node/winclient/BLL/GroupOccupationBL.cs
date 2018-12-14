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
  public class GroupOccupationBL
    {

      public List<GroupOccupationList> GetGroupOccupationPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, string pstrOrganizationId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.groupoccupation
                            join B in dbContext.location on A.v_LocationId equals B.v_LocationId
                            join C in dbContext.organization on B.v_OrganizationId equals C.v_OrganizationId

                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                   equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()
                            where A.i_IsDeleted == 0 && C.v_OrganizationId == pstrOrganizationId

                            select new GroupOccupationList
                            {
                                v_GroupOccupationId = A.v_GroupOccupationId,
                                v_LocationId = A.v_LocationId,
                                v_LocationName = B.v_Name,
                                v_Name = A.v_Name,
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

                List<GroupOccupationList> objData = query.ToList();
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

      public int GetGroupOccupationCount(ref OperationResult pobjOperationResult, string filterExpression)
      {
          //mon.IsActive = true;

          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              var query = from a in dbContext.groupoccupation select a;

              if (!string.IsNullOrEmpty(filterExpression))
                  query = query.Where(filterExpression);

              int intResult = query.Count();

              pobjOperationResult.Success = 1;
              return intResult;
          }
          catch (Exception ex)
          {
              pobjOperationResult.Success = 0;
              pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
              return 0;
          }
      }

      public groupoccupationDto GetGroupOccupation(ref OperationResult pobjOperationResult, string pstrGroupOccupationId)
      {
          //mon.IsActive = true;

          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              groupoccupationDto objDtoEntity = null;

              var objEntity = (from a in dbContext.groupoccupation
                               where a.v_GroupOccupationId == pstrGroupOccupationId
                               select a).FirstOrDefault();

              if (objEntity != null)
                  objDtoEntity = groupoccupationAssembler.ToDTO(objEntity);

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

      public void AddGroupOccupation(ref OperationResult pobjOperationResult, groupoccupationDto pobjDtoEntity, List<string> ClientSession)
      {
          //mon.IsActive = true;
          string NewId = "(No generado)";
          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              groupoccupation objEntity = groupoccupationAssembler.ToEntity(pobjDtoEntity);

              objEntity.d_InsertDate = DateTime.Now;
              objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
              objEntity.i_IsDeleted = 0;
              // Autogeneramos el Pk de la tabla
              int intNodeId = int.Parse(ClientSession[0]);
              NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 13), "OG"); ;
              objEntity.v_GroupOccupationId = NewId;
           
              dbContext.AddTogroupoccupation(objEntity);
              dbContext.SaveChanges();

              pobjOperationResult.Success = 1;
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "GESO", "v_GroupOccupationId=" + NewId.ToString(), Success.Ok, null);
              return;
          }
          catch (Exception ex)
          {
              pobjOperationResult.Success = 0;
              pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "GESO", "v_GroupOccupationId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
              return;
          }
      }

      public void UpdateGroupOccupation(ref OperationResult pobjOperationResult, groupoccupationDto pobjDtoEntity, List<string> ClientSession)
      {
          //mon.IsActive = true;

          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

              // Obtener la entidad fuente
              var objEntitySource = (from a in dbContext.groupoccupation
                                     where a.v_GroupOccupationId == pobjDtoEntity.v_GroupOccupationId
                                     select a).FirstOrDefault();

              // Crear la entidad con los datos actualizados
              pobjDtoEntity.d_UpdateDate = DateTime.Now;
              pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
              groupoccupation objEntity = groupoccupationAssembler.ToEntity(pobjDtoEntity);

              // Copiar los valores desde la entidad actualizada a la Entidad Fuente
              dbContext.groupoccupation.ApplyCurrentValues(objEntity);

              // Guardar los cambios
              dbContext.SaveChanges();

              pobjOperationResult.Success = 1;
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "GESO", "v_GroupOccupationId=" + objEntity.v_GroupOccupationId.ToString(), Success.Ok, null);
              return;
          }
          catch (Exception ex)
          {
              pobjOperationResult.Success = 0;
              pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "GESO", "v_GroupOccupationId=" + pobjDtoEntity.v_GroupOccupationId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
              return;
          }
      }

      public void DeleteGroupOccupation(ref OperationResult pobjOperationResult, string pstrGroupOccupationId, List<string> ClientSession)
      {
          //mon.IsActive = true;

          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

              // Obtener la entidad fuente
              var objEntitySource = (from a in dbContext.groupoccupation
                                     where a.v_GroupOccupationId == pstrGroupOccupationId
                                     select a).FirstOrDefault();

              // Crear la entidad con los datos actualizados
              objEntitySource.d_UpdateDate = DateTime.Now;
              objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
              objEntitySource.i_IsDeleted = 1;

              // Guardar los cambios
              dbContext.SaveChanges();

              pobjOperationResult.Success = 1;
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "GESO", "", Success.Ok, null);
              return;
          }
          catch (Exception ex)
          {
              pobjOperationResult.Success = 0;
              pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "GESO", "", Success.Failed, pobjOperationResult.ExceptionMessage);
              return;
          }
      }

      public groupoccupationDto GetFirstGroupOccupationByLocationId(ref OperationResult pobjOperationResult, string locationId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                groupoccupationDto objDtoEntity = null;

                var objEntity = (from a in dbContext.groupoccupation
                                 where a.v_LocationId == locationId
                    select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = groupoccupationAssembler.ToDTO(objEntity);

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
      }
}
