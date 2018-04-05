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
   public class EmailBL
    {
        public List<EmailList> LlenarEmail(ref OperationResult pobjOperationResult)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from A in dbContext.email
                            select new EmailList
                            {
                                i_EmailId = A.i_EmailId,
                                v_Email =A.v_Email
                            }).ToList();


                var objData = query.AsEnumerable().
                            GroupBy(g => g.v_Email)
                                        .Select(s => s.First());

                List<EmailList> x =objData.ToList();
                pobjOperationResult.Success = 1;
                return x;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public void AddEmail(ref OperationResult pobjOperationResult, emailDto pobjDtoEntity)
        {
            //mon.IsActive = true;
            int SecuentialId = 0;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                email objEntity = emailAssembler.ToEntity(pobjDtoEntity);

                SecuentialId = Utils.GetNextSecuentialId(1, 400);
                objEntity.i_EmailId = SecuentialId;

                dbContext.AddToemail(objEntity);
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

        public emailDto VerificarSiExisteEmail(ref OperationResult pobjOperationResult, string pstrEmail)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                emailDto objDtoEntity = null;

                var objEntity = (from a in dbContext.email
                                 where a.v_Email == pstrEmail
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = emailAssembler.ToDTO(objEntity);

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
