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
    public class MedicalExamBL
    {
        public componentDto GetMedicalExam(ref OperationResult pobjOperationResult, string pstrMedicalExamId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                componentDto objDtoEntity = null;

                var objEntity = (from a in dbContext.component
                                 where a.v_ComponentId == pstrMedicalExamId &&
                                 a.i_IsDeleted == 0 &&
                                 a.i_ComponentTypeId == (int?)ComponentType.Examen
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

    }
}
