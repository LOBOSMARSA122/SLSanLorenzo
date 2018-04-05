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
  public class SyncBL
  {
      #region Software Component Release

      public List<SoftwareComponentReleaseList> GetSoftwareComponentReleasePagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
      {
          //mon.IsActive = true;

          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

              var query = from A in dbContext.softwarecomponentrelease

                          select new SoftwareComponentReleaseList
                          {
                              i_SoftwareComponentId = A.i_SoftwareComponentId,
                              v_SoftwareComponentVersion = A.v_SoftwareComponentVersion,
                              i_DeploymentFileId = A.i_DeploymentFileId.Value,
                              d_ReleaseDate = A.d_ReleaseDate.Value,
                              v_DatabaseVersionRequired = A.v_DatabaseVersionRequired,
                              v_ReleaseNotes = A.v_ReleaseNotes,
                              v_AdditionalInformation1 = A.v_AdditionalInformation1,
                              v_AdditionalInformation2 = A.v_AdditionalInformation2,
                              i_IsPublished = A.i_IsPublished.Value,
                              i_IsLastVersion = A.i_IsLastVersion.Value,
                              d_InsertDate = A.d_InsertDate.Value,
                              d_UpdateDate = A.d_UpdateDate.Value
                          };


              if (!string.IsNullOrEmpty(pstrFilterExpression))
              {
                  query = query.Where(pstrFilterExpression);
              }
              if (!string.IsNullOrEmpty(pstrSortExpression))
              {
                  query = query.OrderBy(pstrSortExpression);
              }

              List<SoftwareComponentReleaseList> objData = query.ToList();
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

      public softwarecomponentreleaseDto GetSoftwareComponentRelease(ref OperationResult pobjOperationResult, int pintSoftwareComponentId, string pintSoftwareComponentVersion)
      {
          //mon.IsActive = true;

          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              softwarecomponentreleaseDto objDtoEntity = null;

              var objEntity = (from a in dbContext.softwarecomponentrelease
                               where a.i_SoftwareComponentId == pintSoftwareComponentId && a.v_SoftwareComponentVersion == pintSoftwareComponentVersion
                               select a).FirstOrDefault();

              if (objEntity != null)
                  objDtoEntity = softwarecomponentreleaseAssembler.ToDTO(objEntity);

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

      public void AddSoftwareComponentRelease(ref OperationResult pobjOperationResult, softwarecomponentreleaseDto pobjDtoEntity, List<string> ClientSession)
      {
          //mon.IsActive = true;
          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              softwarecomponentrelease objEntity = softwarecomponentreleaseAssembler.ToEntity(pobjDtoEntity);


              objEntity.d_InsertDate = DateTime.Now;

              dbContext.AddTosoftwarecomponentrelease(objEntity);
              dbContext.SaveChanges();

              pobjOperationResult.Success = 1;
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "SOFTWARE COMPONENT RELEASE", "SoftwareComponentId=" + objEntity.i_SoftwareComponentId.ToString() + " / SoftwareComponentVersion = " + objEntity.v_SoftwareComponentVersion, Success.Ok, null);

              return;
          }
          catch (Exception ex)
          {
              pobjOperationResult.Success = 0;
              pobjOperationResult.ExceptionMessage = ex.Message;
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "SOFTWARE COMPONENT RELEASE", "SoftwareComponentId=" + pobjDtoEntity.i_SoftwareComponentId.ToString() + " / SoftwareComponentVersion = " + pobjDtoEntity.v_SoftwareComponentVersion, Success.Failed, ex.Message);
              return;
          }
      }

      public void UpdateSoftwareComponentRelease(ref OperationResult pobjOperationResult, softwarecomponentreleaseDto pobjDtoEntity, List<string> ClientSession)
      {
          //mon.IsActive = true;
          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

              // Obtener la entidad fuente
              var objEntitySource = (from a in dbContext.softwarecomponentrelease
                                     where a.i_SoftwareComponentId == pobjDtoEntity.i_SoftwareComponentId && a.v_SoftwareComponentVersion == pobjDtoEntity.v_SoftwareComponentVersion
                                     select a).FirstOrDefault();

              // Crear la entidad con los datos actualizados
              pobjDtoEntity.d_UpdateDate = DateTime.Now;
              softwarecomponentrelease objEntity = softwarecomponentreleaseAssembler.ToEntity(pobjDtoEntity);

              // Copiar los valores desde la entidad actualizada a la Entidad Fuente
              dbContext.softwarecomponentrelease.ApplyCurrentValues(objEntity);

              // Guardar los cambios
              dbContext.SaveChanges();

              pobjOperationResult.Success = 1;

              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "SOFTWARE COMPONENT RELEASE", "SoftwareComponentId=" + objEntity.i_SoftwareComponentId.ToString() + " / SoftwareComponentVersion = " + objEntity.v_SoftwareComponentVersion, Success.Ok, null);
              return;
          }
          catch (Exception ex)
          {
              pobjOperationResult.Success = 0;
              pobjOperationResult.ExceptionMessage = ex.Message;
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "SOFTWARE COMPONENT RELEASE", "SoftwareComponentId=" + pobjDtoEntity.i_SoftwareComponentId.ToString() + " / SoftwareComponentVersion = " + pobjDtoEntity.v_SoftwareComponentVersion, Success.Failed, ex.Message);
              return;
          }
      }

      #endregion

      #region deploymentfile

      public List<DeploymentFileList> GetDeploymentFilePagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
      {
          //mon.IsActive = true;
          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

              var query = from A in dbContext.deploymentfile

                          select new DeploymentFileList
                          {
                              i_DeploymentFileId = A.i_DeploymentFileId,
                              v_FileName = A.v_FileName,
                              v_Description = A.v_Description,
                              i_SoftwareComponentId = A.i_SoftwareComponentId.Value,
                              v_TargetSoftwareComponentVersion = A.v_TargetSoftwareComponentVersion,
                              v_PackageFiles = A.v_PackageFiles,
                              r_PackageSizeKb = A.r_PackageSizeKb.Value
                          };


              if (!string.IsNullOrEmpty(pstrFilterExpression))
              {
                  query = query.Where(pstrFilterExpression);
              }
              if (!string.IsNullOrEmpty(pstrSortExpression))
              {
                  query = query.OrderBy(pstrSortExpression);
              }

              List<DeploymentFileList> objData = query.ToList();
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

      public deploymentfileDto GetDeploymentFile(ref OperationResult pobjOperationResult, int pintDeploymentFileId)
      {
          //mon.IsActive = true;

          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              deploymentfileDto objDtoEntity = null;

              var objEntity = (from a in dbContext.deploymentfile
                               where a.i_DeploymentFileId == pintDeploymentFileId 
                               select a).FirstOrDefault();

              if (objEntity != null)
                  objDtoEntity = deploymentfileAssembler.ToDTO(objEntity);

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

      public void AddDeploymentFile(ref OperationResult pobjOperationResult, deploymentfileDto pobjDtoEntity, List<string> ClientSession)
      {
          //mon.IsActive = true;
          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
              deploymentfile objEntity = deploymentfileAssembler.ToEntity(pobjDtoEntity);


              dbContext.AddTodeploymentfile(objEntity);
              dbContext.SaveChanges();

              pobjOperationResult.Success = 1;
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "DEPLOYMENT FILE", "DeploymentFileId=" + objEntity.i_DeploymentFileId.ToString() , Success.Ok, null);

              return;
          }
          catch (Exception ex)
          {
              pobjOperationResult.Success = 0;
              pobjOperationResult.ExceptionMessage = ex.Message;
              // Llenar entidad Log
              LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "DEPLOYMENT FILE", "DeploymentFileId=" + pobjDtoEntity.i_DeploymentFileId.ToString(), Success.Failed, ex.Message);
              return;
          }
      }
      
      #endregion

      #region ServerNodeSync

      public List<ServerNodeSyncList> GetServerNodeSyncPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
      {
          //mon.IsActive = true;

          try
          {
              SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

              var query = from A in dbContext.servernodesync
                          join B in dbContext.node on A.i_NodeId  equals B.i_NodeId
                          select new ServerNodeSyncList
                          {
                              i_NodeId = A.i_NodeId,
                              v_NodeName = B.v_Description,
                              v_DataSyncVersion = A.v_DataSyncVersion,
                              i_DataSyncFrecuency = A.i_DataSyncFrecuency.Value,
                              i_Enabled = A.i_Enabled.Value,
                              d_LastSuccessfulDataSync = A.d_LastSuccessfulDataSync,
                              i_LastServerProcessStatus = A.i_LastServerProcessStatus,
                              i_LastNodeProcessStatus = A.i_LastNodeProcessStatus,
                              d_NextDataSync = A.d_NextDataSync,
                              v_LastServerPackageFileName = A.v_LastServerPackageFileName,
                              v_LastServerProcessErrorMessage = A.v_LastServerProcessErrorMessage,
                              v_LastNodePackageFileName = A.v_LastNodePackageFileName,
                              v_LastNodeProcessErrorMessage = A.v_LastNodeProcessErrorMessage
                          };


              if (!string.IsNullOrEmpty(pstrFilterExpression))
              {
                  query = query.Where(pstrFilterExpression);
              }
              if (!string.IsNullOrEmpty(pstrSortExpression))
              {
                  query = query.OrderBy(pstrSortExpression);
              }

              List<ServerNodeSyncList> objData = query.ToList();
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
  }
}
