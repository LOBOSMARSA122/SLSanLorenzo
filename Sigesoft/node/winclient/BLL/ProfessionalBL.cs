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
    public class ProfessionalBL
    {
        public SystemUserList GetSystemUserName(ref OperationResult pobjOperationResult, int SystemUserId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from su1 in dbContext.systemuser
                             join A in dbContext.person on su1.v_PersonId equals A.v_PersonId
                             join B in dbContext.professional on A.v_PersonId equals B.v_PersonId
                             join J1 in dbContext.datahierarchy on new { a = 121, b = su1.i_RolVentaId.Value }
                                                                 equals new { a = J1.i_GroupId, b = J1.i_ItemId } into J1_join
                             from J1 in J1_join.DefaultIfEmpty()

                             where su1.i_IsDeleted == 0 && su1.i_SystemUserId == SystemUserId
                             
                             select new SystemUserList
                             {
                                 v_PersonName = A.v_FirstName + " " + A.v_FirstLastName + " " + A.v_SecondLastName,
                                 v_RolVenta = J1.v_Value1,
                                 i_RolVenta = su1.i_RolVentaId,
                                 InfAdicional = B.v_ProfessionalInformation,
                                v_PersonId = B.v_PersonId
                             }
                            ).FirstOrDefault();



              
                //List<SystemUserList> objData = query.ToList();
                pobjOperationResult.Success = 1;
                return query;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }


        }

        public SystemUserList GetSystemUser(int SystemUserId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from su1 in dbContext.systemuser
                        join A in dbContext.person on su1.v_PersonId equals A.v_PersonId
                        join B in dbContext.professional on A.v_PersonId equals B.v_PersonId

                        where su1.i_IsDeleted == 0 && su1.i_SystemUserId == SystemUserId

                        select new SystemUserList
                        {
                            v_PersonName = A.v_FirstName + " " + A.v_FirstLastName + " " + A.v_SecondLastName,
                            i_ProfesionId = B.i_ProfessionId,
                            CMP = B.v_ProfessionalCode,
                            InfAdicional = B.v_ProfessionalInformation,
                            v_PersonId = B.v_PersonId
                        }
                    ).FirstOrDefault();




                //List<SystemUserList> objData = query.ToList();

                return query;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public SystemUserList GetSystemUserNameExternal(ref OperationResult pobjOperationResult, int SystemUserId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = (from su1 in dbContext.systemuser
                             join A in dbContext.person on su1.v_PersonId equals A.v_PersonId   
                             where su1.i_IsDeleted == 0 && su1.i_SystemUserId == SystemUserId

                             select new SystemUserList
                             {
                                 v_PersonName = A.v_FirstName + " " + A.v_FirstLastName + " " + A.v_SecondLastName,                               
                                 v_PersonId = A.v_PersonId,
                                 i_SystemUserId = su1.i_SystemUserId
                             }
                            ).FirstOrDefault();
                pobjOperationResult.Success = 1;
                return query;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }


        }
        public List<ProduccionCategoria> GetFilterProduccionCategoria(DateTime? pdatBeginDate, DateTime? pdatEndDate) 
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = (from A in dbContext.service
                             join B in dbContext.servicecomponent on A.v_ServiceId equals B.v_ServiceId
                             join bbb in dbContext.component on B.v_ComponentId equals bbb.v_ComponentId 
                             join cat in dbContext.systemparameter on new { a = bbb.i_CategoryId.Value, b = 116 }
                                                                  equals new { a = cat.i_ParameterId, b = cat.i_GroupId } into cat_join
                             from cat in cat_join.DefaultIfEmpty()
                            where pdatBeginDate.Value <= A.d_ServiceDate && pdatEndDate.Value >= A.d_ServiceDate
                                && B.i_IsRequiredId == 1
                                && B.i_IsDeleted == 0
                                && A.i_IsDeleted == 0
                            select new ProduccionCategoria
                            {
                                serviceId = A.v_ServiceId,
                                Categoria = cat.v_Value1,
                                Costo = B.r_Price.Value
                            }).ToList();

                query = query
                        .GroupBy(g => g.Categoria)
                        .Select(s => new ProduccionCategoria
                        {
                            Categoria = s.First().Categoria,
                            Cantidad = s.Count(),
                            Costo = s.Sum(sum => sum.Costo)

                        }).ToList();


                return query;

            }
            catch (Exception ex)
            {                
                throw;
            }
           
        }

    }
}
