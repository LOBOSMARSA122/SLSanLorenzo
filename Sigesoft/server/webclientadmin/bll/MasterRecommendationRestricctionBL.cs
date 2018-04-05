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
    public class MasterRecommendationRestricctionBL
    {
        public masterrecommendationrestricctionDto GetMasterRecommendationRestricctionByName(string pstrMasterRecommendationRestricctionName)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                masterrecommendationrestricctionDto objDtoEntity = null;

                var objEntity = (from a in dbContext.masterrecommendationrestricction
                                 where a.v_Name == pstrMasterRecommendationRestricctionName
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = masterrecommendationrestricctionAssembler.ToDTO(objEntity);

                return objDtoEntity;
            }
            catch (Exception ex)
            {
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


    }
}
