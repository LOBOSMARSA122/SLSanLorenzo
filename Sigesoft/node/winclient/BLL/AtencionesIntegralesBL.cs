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
    public class AtencionesIntegralesBL
    {
        public serviceDto GetService(string serviceId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                serviceDto objDtoEntity = null;

                var objEntity = (from a in dbContext.service
                                 where a.v_ServiceId == serviceId
                    select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = serviceAssembler.ToDTO(objEntity);

                return objDtoEntity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string AddAdolescente(ref OperationResult objOperationResult, adolescenteDto objAdolDto, List<string> ClientSession)
        {
            string NewId0 = null;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                adolescente objEntity = adolescenteAssembler.ToEntity(objAdolDto);
                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;

                int intNodeId = int.Parse(ClientSession[0]);
                NewId0 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 353), "JO"); ;
                objEntity.v_AdolescenteId = NewId0;

                dbContext.AddToadolescente(objEntity);
                dbContext.SaveChanges();

                objOperationResult.Success = 1;
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "JO", "v_AdolescenteId=" + NewId0.ToString(), Success.Ok, null);

            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "JO", "v_AdolescenteId=" + NewId0.ToString(), Success.Failed, objOperationResult.ExceptionMessage);
            }
            return NewId0;
        }

        public void UpdAdolescente(ref OperationResult objOperationResult, adolescenteDto objAdolDto, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               
                    var objEntitySource = (from a in dbContext.adolescente
                                           where a.v_AdolescenteId == objAdolDto.v_AdolescenteId
                                           select a).FirstOrDefault();
                    objAdolDto.d_UpdateDate = DateTime.Now;
                    objAdolDto.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                    var objStrongEntity = adolescenteAssembler.ToEntity(objAdolDto);
                    dbContext.adolescente.ApplyCurrentValues(objStrongEntity);

                    objOperationResult.Success = 1;
                    dbContext.SaveChanges();

                    LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "JO / UPD", "v_AdolescenteId=" + objAdolDto.v_AdolescenteId, Success.Ok, null);
                    return;
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "JO / UPD", "v_AdolescenteId=" + objAdolDto.v_AdolescenteId, Success.Failed, objOperationResult.ExceptionMessage);
                return;
            }
        }

        public adolescenteDto GetAdolescente(ref OperationResult objOperationResult, string _PersonId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                adolescenteDto objDtoEntity = null;

                var objEntity = (from a in dbContext.adolescente
                                 where a.v_PersonId == _PersonId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = adolescenteAssembler.ToDTO(objEntity);

                objOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        

        public string AddAdulto(ref OperationResult objOperationResult, adultoDto objAdultDto, List<string> ClientSession)
        {
            string NewId0 = null;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                adulto objEntity = adultoAssembler.ToEntity(objAdultDto);
                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;

                int intNodeId = int.Parse(ClientSession[0]);
                NewId0 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 354), "AD"); ;
                objEntity.v_AdultoId = NewId0;

                dbContext.AddToadulto(objEntity);
                dbContext.SaveChanges();

                objOperationResult.Success = 1;
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "AD", "v_AdultoId=" + NewId0.ToString(), Success.Ok, null);

            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "AD", "v_AdultoId=" + NewId0.ToString(), Success.Failed, objOperationResult.ExceptionMessage);
            }
            return NewId0;
        }

        public void UpdAdulto(ref OperationResult objOperationResult, adultoDto objAdultDto, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntitySource = (from a in dbContext.adulto
                                       where a.v_AdultoId == objAdultDto.v_AdultoId
                                       select a).FirstOrDefault();
                objAdultDto.d_UpdateDate = DateTime.Now;
                objAdultDto.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                var objStrongEntity = adultoAssembler.ToEntity(objAdultDto);
                dbContext.adulto.ApplyCurrentValues(objStrongEntity);

                objOperationResult.Success = 1;
                dbContext.SaveChanges();

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "AD / UPD", "v_AdultoId=" + objAdultDto.v_AdultoId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "AD / UPD", "v_AdultoId=" + objAdultDto.v_AdultoId, Success.Failed, objOperationResult.ExceptionMessage);
                return;
            }
        }

        public adultoDto GetAdulto(ref OperationResult objOperationResult, string _PersonId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                adultoDto objDtoEntity = null;

                var objEntity = (from a in dbContext.adulto
                                 where a.v_PersonId == _PersonId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = adultoAssembler.ToDTO(objEntity);

                objOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public string AddAdultoMayor(ref OperationResult objOperationResult, adultomayorDto objAdultMayDto, List<string> ClientSession)
        {
            string NewId0 = null;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                adultomayor objEntity = adultomayorAssembler.ToEntity(objAdultMayDto);
                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;

                int intNodeId = int.Parse(ClientSession[0]);
                NewId0 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 355), "AM"); ;
                objEntity.v_AdultoMayorId = NewId0;

                dbContext.AddToadultomayor(objEntity);
                dbContext.SaveChanges();

                objOperationResult.Success = 1;
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "AM", "v_AdultoMayorId=" + NewId0.ToString(), Success.Ok, null);

            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "AM", "v_AdultoMayorId=" + NewId0.ToString(), Success.Failed, objOperationResult.ExceptionMessage);
            }
            return NewId0;
        }

        public void UpdAdultoMayor(ref OperationResult objOperationResult, adultomayorDto objAdultMayDto, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntitySource = (from a in dbContext.adultomayor
                                       where a.v_AdultoMayorId == objAdultMayDto.v_AdultoMayorId
                                       select a).FirstOrDefault();
                objAdultMayDto.d_UpdateDate = DateTime.Now;
                objAdultMayDto.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                var objStrongEntity = adultomayorAssembler.ToEntity(objAdultMayDto);
                dbContext.adultomayor.ApplyCurrentValues(objStrongEntity);

                objOperationResult.Success = 1;
                dbContext.SaveChanges();

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "AM / UPD", "v_AdultoId=" + objAdultMayDto.v_AdultoMayorId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "AM / UPD", "v_AdultoId=" + objAdultMayDto.v_AdultoMayorId, Success.Failed, objOperationResult.ExceptionMessage);
                return;
            }
        }

        public adultomayorDto GetAdultoMayor(ref OperationResult objOperationResult, string _PersonId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                adultomayorDto objDtoEntity = null;

                var objEntity = (from a in dbContext.adultomayor
                                 where a.v_PersonId == _PersonId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = adultomayorAssembler.ToDTO(objEntity);

                objOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public string AddNinio(ref OperationResult objOperationResult, ninioDto objNinioDto, List<string> ClientSession)
        {
            string NewId0 = null;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                ninio objEntity = ninioAssembler.ToEntity(objNinioDto);
                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;

                int intNodeId = int.Parse(ClientSession[0]);
                NewId0 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 356), "NÑ"); ;
                objEntity.v_NinioId = NewId0;

                dbContext.AddToninio(objEntity);
                dbContext.SaveChanges();

                objOperationResult.Success = 1;
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "NÑ", "v_NinioId=" + NewId0.ToString(), Success.Ok, null);

            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "NÑ", "v_NinioId=" + NewId0.ToString(), Success.Failed, objOperationResult.ExceptionMessage);
            }
            return NewId0;
        }

        public void UpdNinio(ref OperationResult objOperationResult, ninioDto objNinioDto, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntitySource = (from a in dbContext.ninio
                                       where a.v_NinioId == objNinioDto.v_NinioId
                                       select a).FirstOrDefault();
                objNinioDto.d_UpdateDate = DateTime.Now;
                objNinioDto.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                var objStrongEntity = ninioAssembler.ToEntity(objNinioDto);
                dbContext.ninio.ApplyCurrentValues(objStrongEntity);

                objOperationResult.Success = 1;
                dbContext.SaveChanges();

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "NÑ / UPD", "v_NinioId=" + objNinioDto.v_NinioId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "NÑ / UPD", "v_NinioId=" + objNinioDto.v_NinioId, Success.Failed, objOperationResult.ExceptionMessage);
                return;
            }
        }

        public ninioDto GetNinio(ref OperationResult objOperationResult, string _PersonId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                ninioDto objDtoEntity = null;

                var objEntity = (from a in dbContext.ninio
                                 where a.v_PersonId == _PersonId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = ninioAssembler.ToDTO(objEntity);

                objOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

    }
}
