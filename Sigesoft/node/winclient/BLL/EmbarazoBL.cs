using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Common;
using System.Data.Objects;
using ConnectionState = System.Data.ConnectionState;

namespace Sigesoft.Node.WinClient.BLL
{
    public class EmbarazoBL
    {
        public string AddEmbarazo(ref OperationResult objOperationResult, embarzoDto objticketDto, List<string> ClientSession)
        {
            string NewId0 = null;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                embarzo objEntity = embarzoAssembler.ToEntity(objticketDto);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla
                int intNodeId = int.Parse(ClientSession[0]);
                NewId0 = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 357), "EM"); ;
                objEntity.v_EmbarazoId = NewId0;

                dbContext.AddToembarzo(objEntity);
                dbContext.SaveChanges();

                objOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "EM", "v_EmbarazoId=" + NewId0.ToString(), Success.Ok, null);
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "EM", "v_EmbarazoId=" + NewId0.ToString(), Success.Failed, objOperationResult.ExceptionMessage);
            }
            return NewId0;
        }

        public embarzoDto GetEmbarazo(ref OperationResult objOperationResult, string _embarazoId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                embarzoDto objDtoEntity = null;

                var objEntity = (from a in dbContext.embarzo
                                 where a.v_EmbarazoId == _embarazoId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = embarzoAssembler.ToDTO(objEntity);

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

        public List<Embarazo> GetEmbarazoFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, string pstrPersonId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.embarzo
                            join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                            where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId

                            select new Embarazo
                            {
                                v_EmbarazoId = A.v_EmbarazoId,
                                v_PersonId = A.v_PersonId,
                                v_Anio = A.v_Anio,
                                v_Cpn = A.v_Cpn,
                                v_Complicacion = A.v_Complicacion,
                                v_Parto = A.v_Parto,
                                v_PesoRn = A.v_PesoRn,
                                v_Puerpio = A.v_Puerpio
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

                List<Embarazo> objData = query.ToList();
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

        public void UpdEmbarazo(ref OperationResult objOperationResult, embarzoDto objEmbarazoDto, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var objEntitySource = (from a in dbContext.embarzo
                                       where a.v_EmbarazoId == objEmbarazoDto.v_EmbarazoId
                                       select a).FirstOrDefault();

                objEmbarazoDto.d_UpdateDate = DateTime.Now;
                objEmbarazoDto.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                var objStrongEntity = embarzoAssembler.ToEntity(objEmbarazoDto);
                dbContext.embarzo.ApplyCurrentValues(objStrongEntity);

                objOperationResult.Success = 1;
                dbContext.SaveChanges();

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "EM / UPD", "v_EmbarazoId=" + objEmbarazoDto.v_EmbarazoId, Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);

                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "EM / UPD", "v_EmbarazoId=" + objEmbarazoDto.v_EmbarazoId, Success.Failed, objOperationResult.ExceptionMessage);
                return;
            }
        }

        public void DeleteEmbarazo(ref OperationResult pobjOperationResult, string embarazoId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.embarzo
                                       where a.v_EmbarazoId == embarazoId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return;
            }
        }
    }
}
