using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Common;

namespace Sigesoft.Node.WinClient.BLL
{
    public class MultimediaFileBL
    {
        private string AddMultimediaFile(ref OperationResult pobjOperationResult, multimediafileDto pobjDtoEntity, List<string> ClientSession, SiNo ExecLog)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                multimediafile objEntity = multimediafileAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;

                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 45), "FU");
                objEntity.v_MultimediaFileId = NewId;

                dbContext.AddTomultimediafile(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;

                if (ExecLog == SiNo.SI)
                {
                    // Llenar entidad Log
                    LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "MULTIMEDIA FILE", "v_MultimediaFileId=" + NewId, Success.Ok, null);              
                }
                
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
               
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "MULTIMEDIA FILE", "v_MultimediaFileId=" + NewId, Success.Failed, pobjOperationResult.ExceptionMessage);
                
            }

            return NewId;

        }
      
        private void UpdateMultimediaFile(ref OperationResult pobjOperationResult, multimediafileDto pobjDtoEntity, List<string> ClientSession, SiNo ExecLog)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.multimediafile
                                       where a.v_MultimediaFileId == pobjDtoEntity.v_MultimediaFileId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.v_FileName = pobjDtoEntity.v_FileName;
                if (pobjDtoEntity.b_File != null)
                    objEntitySource.b_File = pobjDtoEntity.b_File;
                objEntitySource.b_ThumbnailFile = pobjDtoEntity.b_ThumbnailFile;
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
              
                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;

                if (ExecLog == SiNo.SI)
                {
                    // Llenar entidad Log
                    LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "MULTIMEDIA FILE", "v_MultimediaFileId=" + pobjDtoEntity.v_MultimediaFileId, Success.Ok, null);
                }
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "MULTIMEDIA FILE", "v_MultimediaFileId=" + pobjDtoEntity.v_MultimediaFileId, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        private void UpdateServiceComponentMultimedia(ref OperationResult pobjOperationResult, servicecomponentmultimediaDto pobjDtoEntity, List<string> ClientSession, SiNo ExecLog)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.servicecomponentmultimedia
                                       where a.v_ServiceComponentMultimediaId == pobjDtoEntity.v_ServiceComponentMultimediaId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.v_Comment = pobjDtoEntity.v_Comment;          
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;

                if (ExecLog == SiNo.SI)
                {
                    // Llenar entidad Log
                    LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "SERVICE COMPONENT MULTIMEDIA FILE", "v_ServiceComponentMultimediaId=" + pobjDtoEntity.v_ServiceComponentMultimediaId, Success.Ok, null);
                }
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "SERVICE COMPONENT MULTIMEDIA FILE", "v_ServiceComponentMultimediaId=" + pobjDtoEntity.v_ServiceComponentMultimediaId, Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public string[] AddMultimediaFileComponent(ref OperationResult pobjOperationResult, FileInfoDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string[] IDs = new string[2];
            var multimediaFileId = string.Empty;
            var serviceComponentMultimediaId = string.Empty;

            try
            {
                multimediafileDto multimediaFile = new multimediafileDto();
                multimediaFile.v_PersonId = pobjDtoEntity.PersonId;
                multimediaFile.v_FileName = pobjDtoEntity.FileName;
                multimediaFile.b_File = pobjDtoEntity.ByteArrayFile;
                multimediaFile.b_ThumbnailFile = pobjDtoEntity.ThumbnailFile;

                // Grabar MultimediaFile
                multimediaFileId = AddMultimediaFile(ref pobjOperationResult, multimediaFile, ClientSession, SiNo.NO);
                if (pobjOperationResult.Success == 0) return null;

                servicecomponentmultimediaDto serviceComponentMultimedia = new servicecomponentmultimediaDto();
                serviceComponentMultimedia.v_ServiceComponentId = pobjDtoEntity.ServiceComponentId;
                serviceComponentMultimedia.v_MultimediaFileId = multimediaFileId;
                serviceComponentMultimedia.v_Comment = pobjDtoEntity.Description;

                // Grabar MultimediaFileComponent
                serviceComponentMultimediaId = AddServiceComponentMultimedia(ref pobjOperationResult, serviceComponentMultimedia, ClientSession, SiNo.NO);
                if (pobjOperationResult.Success == 0) return null;

                IDs[0] = multimediaFileId;
                IDs[1] = serviceComponentMultimediaId;

                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "MULTIMEDIA FILE", null, Success.Ok, null);

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "MULTIMEDIA FILE", null, Success.Failed, pobjOperationResult.ExceptionMessage);
            }

            return IDs;
        }


        public void UpdateMultimediaFileComponent(ref OperationResult pobjOperationResult, FileInfoDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                multimediafileDto multimediaFile = new multimediafileDto();
                multimediaFile.v_MultimediaFileId = pobjDtoEntity.MultimediaFileId;
                multimediaFile.v_FileName = pobjDtoEntity.FileName;
                multimediaFile.b_File = pobjDtoEntity.ByteArrayFile;
                multimediaFile.b_ThumbnailFile = pobjDtoEntity.ThumbnailFile;

                // Actualizar MultimediaFile
                UpdateMultimediaFile(ref pobjOperationResult, multimediaFile, ClientSession, SiNo.NO);
                if (pobjOperationResult.Success == 0) return;

                servicecomponentmultimediaDto serviceComponentMultimedia = new servicecomponentmultimediaDto();
                serviceComponentMultimedia.v_ServiceComponentMultimediaId = pobjDtoEntity.ServiceComponentMultimediaId;
                serviceComponentMultimedia.v_Comment = pobjDtoEntity.Description;

                // Actualizar MultimediaFileComponent
                UpdateServiceComponentMultimedia(ref pobjOperationResult, serviceComponentMultimedia, ClientSession, SiNo.NO);
                if (pobjOperationResult.Success == 0) return;

                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "MULTIMEDIA FILE", null, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "MULTIMEDIA FILE", null, Success.Failed, pobjOperationResult.ExceptionMessage);

                return;
            }
        }

        public List<FileInfoDto> GetMultimediaFiles(ref OperationResult pobjOperationResult, string ServiceComponentId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from a in dbContext.multimediafile
                                 join b in dbContext.servicecomponentmultimedia on a.v_MultimediaFileId equals b.v_MultimediaFileId
                                 where b.v_ServiceComponentId == ServiceComponentId
                                 && a.i_IsDeleted == 0
                                 select new FileInfoDto
                                 {
                                    MultimediaFileId = a.v_MultimediaFileId,                                   
                                    ServiceComponentMultimediaId = b.v_ServiceComponentMultimediaId,
                                    ServiceComponentId = b.v_ServiceComponentId,
                                    FileName = a.v_FileName,
                                    ByteArrayFile = a.b_File,
                                    ThumbnailFile = a.b_ThumbnailFile,
                                    Description = b.v_Comment
                                 }).ToList();
               
                pobjOperationResult.Success = 1;
                return objEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public FileInfoDto GetMultimediaFileById(ref OperationResult pobjOperationResult, string MultimediaFileId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntity = (from a in dbContext.multimediafile
                                 join b in dbContext.servicecomponentmultimedia on a.v_MultimediaFileId equals  b.v_MultimediaFileId
                                 join c in dbContext.servicecomponent on b.v_ServiceComponentId equals  c.v_ServiceComponentId
                                 join d in dbContext.person on a.v_PersonId equals d.v_PersonId
                                 join e in dbContext.service on c.v_ServiceId equals e.v_ServiceId
                                 where a.v_MultimediaFileId == MultimediaFileId
                                 && a.i_IsDeleted == 0
                                 select new FileInfoDto
                                 {
                                     MultimediaFileId = a.v_MultimediaFileId,                                  
                                     FileName = a.v_FileName,
                                     ByteArrayFile = a.b_File,
                                     dni = d.v_DocNumber,
                                     FechaServicio = e.d_ServiceDate
                                 }).FirstOrDefault();

                pobjOperationResult.Success = 1;
                return objEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }


        public List<multimediafileList> DevolverTodosArchivos()
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var objEntity = (from a in dbContext.multimediafile
                             where a.i_IsDeleted == 0
                             select new multimediafileList
                             {
                                 v_MultimediaFileId = a.v_MultimediaFileId
                             }).ToList();

            return objEntity;

        }
        public void DeleteMultimediaFileComponent(ref OperationResult pobjOperationResult, string pstrMultimediaFileId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.multimediafile
                                       where a.v_MultimediaFileId == pstrMultimediaFileId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "MULTIMEDIA FILE", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "MULTIMEDIA FILE", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        private string AddServiceComponentMultimedia(ref OperationResult pobjOperationResult, servicecomponentmultimediaDto pobjDtoEntity, List<string> ClientSession, SiNo ExecLog)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                servicecomponentmultimedia objEntity = servicecomponentmultimediaAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;

                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 46), "FC");
                objEntity.v_ServiceComponentMultimediaId = NewId;

                dbContext.AddToservicecomponentmultimedia(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;

                if (ExecLog == SiNo.SI)
                {
                    // Llenar entidad Log
                    LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "SERVICE COMPONENT MULTIMEDIA", "v_ServiceComponentMultimediaId=" + NewId.ToString(), Success.Ok, null);
                }

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);

                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "SERVICE COMPONENT MULTIMEDIA", "v_ServiceComponentMultimediaId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
            }

            return NewId;
        }


    }
}
