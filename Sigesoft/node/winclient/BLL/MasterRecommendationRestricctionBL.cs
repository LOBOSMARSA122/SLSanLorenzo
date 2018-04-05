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
   public class MasterRecommendationRestricctionBL
    {
       public List<MasterRecommendationRestricctionList> GetMasterRecommendationRestricctionPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.masterrecommendationrestricction                           
                            join J1 in dbContext.systemuser on new { i_InsertUserId = A.i_InsertUserId.Value }
                                                            equals new { i_InsertUserId = J1.i_SystemUserId } into J1_join
                            from J1 in J1_join.DefaultIfEmpty()

                            join J2 in dbContext.systemuser on new { i_UpdateUserId = A.i_UpdateUserId.Value }
                                                            equals new { i_UpdateUserId = J2.i_SystemUserId } into J2_join
                            from J2 in J2_join.DefaultIfEmpty()
                            select new MasterRecommendationRestricctionList
                            {
                                v_MasterRecommendationRestricctionId  = A.v_MasterRecommendationRestricctionId,
                                v_Name = A.v_Name,
                                i_TypifyingId = A.i_TypifyingId.Value,
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

                List<MasterRecommendationRestricctionList> objData = query.ToList();
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

       public masterrecommendationrestricctionDto GetMasterRecommendationRestricction(ref OperationResult pobjOperationResult, string pstrMasterRecommendationRestricctionId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                masterrecommendationrestricctionDto objDtoEntity = null;

                var objEntity = (from a in dbContext.masterrecommendationrestricction
                                 where a.v_MasterRecommendationRestricctionId == pstrMasterRecommendationRestricctionId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = masterrecommendationrestricctionAssembler.ToDTO(objEntity);

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

       public string AddMasterRecommendationRestricction(ref OperationResult pobjOperationResult, masterrecommendationrestricctionDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                masterrecommendationrestricction objEntity = masterrecommendationrestricctionAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla                 
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 43), "MR"); ;
                objEntity.v_MasterRecommendationRestricctionId = NewId;

                dbContext.AddTomasterrecommendationrestricction(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "RECOMENDACIÓN / RESTRICCIÓN", "v_MasterRecommendationRestricctionId=" + NewId.ToString(), Success.Ok, null);
                return NewId;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "RECOMENDACIÓN / RESTRICCIÓN", "v_MasterRecommendationRestricctionId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return null;
            }
        }

       public void UpdateMasterRecommendationRestricction(ref OperationResult pobjOperationResult, masterrecommendationrestricctionDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.masterrecommendationrestricction
                                       where a.v_MasterRecommendationRestricctionId == pobjDtoEntity.v_MasterRecommendationRestricctionId
                                       select a).FirstOrDefault();



                // Crear la entidad con los datos actualizados
                pobjDtoEntity.v_Name = pobjDtoEntity.v_Name;
                pobjDtoEntity.i_TypifyingId = objEntitySource.i_TypifyingId;
                pobjDtoEntity.i_IsDeleted = 1;
                pobjDtoEntity.i_InsertUserId = objEntitySource.i_InsertUserId;
                pobjDtoEntity.d_InsertDate = objEntitySource.d_InsertDate;
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);

                masterrecommendationrestricction objEntity = masterrecommendationrestricctionAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.masterrecommendationrestricction.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "RECOMENDACIÓN / RESTRICCIÓN", "v_MasterRecommendationRestricctionId=" + objEntity.v_MasterRecommendationRestricctionId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "RECOMENDACIÓN / RESTRICCIÓN", "v_MasterRecommendationRestricctionId=" + pobjDtoEntity.v_MasterRecommendationRestricctionId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

       public void DeleteMasterRecommendationRestricction(ref OperationResult pobjOperationResult, string pstrMasterRecommendationRestricctionId, List<string> ClientSession)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.masterrecommendationrestricction
                                       where a.v_MasterRecommendationRestricctionId == pstrMasterRecommendationRestricctionId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "RECOMENDACIÓN / RESTRICCIÓN", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "RECOMENDACIÓN / RESTRICCIÓN", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

    }
}
