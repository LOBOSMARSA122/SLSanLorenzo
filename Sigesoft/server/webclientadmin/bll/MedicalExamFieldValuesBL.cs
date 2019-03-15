using Sigesoft.Common;
using Sigesoft.Server.WebClientAdmin.BE;
using Sigesoft.Server.WebClientAdmin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sigesoft.Server.WebClientAdmin.BLL
{
   public  class MedicalExamFieldValuesBL
    {
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

        public string ObtenerComponentDx(string pComponentFieldId)
        {

            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var query = (from A in dbContext.componentfields
                         where A.v_ComponentFieldId == pComponentFieldId && A.i_IsDeleted == 0

                         select new
                         {
                             ComponentDxId = A.v_ComponentId
                         }).FirstOrDefault();


            return query.ComponentDxId;

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

        public List<Sigesoft.Node.WinClient.BE.RecomendationList> ObtenerListaRecomendaciones(string pComponentFieldValuesId, string pServiceId, string pComponentId)
        {
            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
            var query = (from A in dbContext.componentfieldvaluesrecommendation
                         where A.v_ComponentFieldValuesId == pComponentFieldValuesId
                         select new Sigesoft.Node.WinClient.BE.RecomendationList
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
    }
}
