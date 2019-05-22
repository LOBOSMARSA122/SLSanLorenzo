using Sigesoft.Node.WinClient.BE.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BLL
{
    public class AdditionalExamBL
    {
        public bool AddAdditionalExam(List<AdditionalExamCustom> listAdditionalExam, List<string> ClientSession)
        {
            try
            {
                SigesoftEntitiesModel dbcontext = new SigesoftEntitiesModel();
                int NodeId = int.Parse(ClientSession[0]);
                int UserId = Int32.Parse(ClientSession[2]);
                var NewId = "";
                foreach (var exam in listAdditionalExam)
                {
                    NewId = Common.Utils.GetNewId(NodeId, Utils.GetNextSecuentialId(NodeId, 49), "AE");
                    additionalexamDto objAdditionalExam = new additionalexamDto();
                    objAdditionalExam.v_AdditionalExamId = NewId;
                    objAdditionalExam.v_ServiceId = exam.ServiceId;
                    objAdditionalExam.v_PersonId = exam.PersonId;
                    objAdditionalExam.v_ProtocolId = exam.ProtocolId;
                    objAdditionalExam.v_Commentary = exam.Commentary;
                    objAdditionalExam.v_ComponentId = exam.ComponentId;
                    objAdditionalExam.i_IsNewService = exam.IsNewService;
                    objAdditionalExam.i_IsProcessed = exam.IsProcessed;
                    objAdditionalExam.i_IsDeleted = (int)SiNo.NO;
                    objAdditionalExam.d_InsertDate = DateTime.Now;
                    objAdditionalExam.i_InsertUserId = UserId;

                    additionalexam objEntity = additionalexamAssembler.ToEntity(objAdditionalExam);
                    dbcontext.AddToadditionalexam(objEntity);
                }

                return dbcontext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }

        public void UpdateAdditionalExamByComponentIdAndServiceId(string componentId, string serviceId, int userId)
        {
            try
            {
                SigesoftEntitiesModel dbcontext = new SigesoftEntitiesModel();
                var obj = (from ade in dbcontext.additionalexam
                    where ade.v_ComponentId == componentId && ade.v_ServiceId == serviceId && ade.i_IsDeleted == 0 && ade.i_IsProcessed == 0
                    select ade).FirstOrDefault();

                obj.i_IsNewService = (int)SiNo.NO;
                obj.i_IsProcessed = (int)SiNo.SI;
                obj.i_IsDeleted = (int)SiNo.NO;
                obj.d_UpdateDate = DateTime.Now;
                obj.i_UpdateUserId = userId;

                dbcontext.SaveChanges();
            }
            catch (Exception e)
            {

                throw;
            }   
        }

        public List<AdditionalExamCustom> GetAdditionalExamByServiceId_all(string serviceId, int userId)
        {
            SigesoftEntitiesModel dbcontext = new SigesoftEntitiesModel();

            var list = (from ade in dbcontext.additionalexam
                        where ade.v_ServiceId == serviceId && ade.i_IsDeleted == 0 && ade.i_InsertUserId == userId
                select new AdditionalExamCustom
                {
                    ComponentId = ade.v_ComponentId,
                    ServiceId = ade.v_ServiceId,
                    IsProcessed = ade.i_IsProcessed.Value,
                    IsNewService = ade.i_IsNewService.Value
                }).ToList();

            return list;
        }

        public List<AdditionalExamCustom> GetAdditionalExamByServiceId( string serviceId)
        {
            SigesoftEntitiesModel dbcontext = new SigesoftEntitiesModel();

            var list = (from ade in dbcontext.additionalexam
                where ade.v_ServiceId == serviceId && ade.i_IsDeleted == 0 && ade.i_IsProcessed == 0
                select new AdditionalExamCustom
                {
                    ComponentId = ade.v_ComponentId,
                    ServiceId = ade.v_ServiceId,
                    IsNewService = ade.i_IsNewService.Value
                }).ToList();

            return list;
        }

        public List<AdditionalExamUpdate> GetAdditionalExamForUpdateByServiceId(string serviceId, int userId)
        {
            SigesoftEntitiesModel dbcontext = new SigesoftEntitiesModel();

            var list = (from ade in dbcontext.additionalexam
                join com in dbcontext.component on ade.v_ComponentId equals com.v_ComponentId
                join per in dbcontext.person on ade.v_PersonId equals per.v_PersonId
                where ade.v_ServiceId == serviceId && ade.i_IsDeleted == 0 && ade.i_IsProcessed == 0 && ade.i_InsertUserId == userId
                select new AdditionalExamUpdate
                {
                    v_AdditionalExamId = ade.v_AdditionalExamId,
                    v_ComponentId = ade.v_ComponentId,
                    v_ServiceId = ade.v_ServiceId,
                    v_ComponentName = com.v_Name,
                    i_IsProcessed = ade.i_IsProcessed.Value,
                    v_PacientName = per.v_FirstLastName + " " + per.v_SecondLastName + ", " + per.v_FirstName,
                }).ToList();

            return list;
        }

        public void DeleteAdditionalExam(string _AdditionalExamId, int userId)
        {
            try
            {
                SigesoftEntitiesModel dbcontext = new SigesoftEntitiesModel();
                var obj = (from ade in dbcontext.additionalexam
                           where ade.v_AdditionalExamId == _AdditionalExamId
                    select ade).FirstOrDefault();

                obj.i_IsDeleted = (int)SiNo.SI;
                obj.d_UpdateDate = DateTime.Now;
                obj.i_UpdateUserId = userId;

                dbcontext.SaveChanges();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public bool UpdateComponentAdditionalExam(string NewComponentId, string _AdditionalExamId, int userId)
        {
            try
            {
                SigesoftEntitiesModel dbcontext = new SigesoftEntitiesModel();
                var obj = (from ade in dbcontext.additionalexam
                           where ade.v_AdditionalExamId == _AdditionalExamId 
                    select ade).FirstOrDefault();

                obj.v_ComponentId = NewComponentId;
                obj.d_UpdateDate = DateTime.Now;
                obj.i_UpdateUserId = userId;

                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                return false; 
            }  
        }

        public void ReverseProcessed(string serviceId, string serviceComponentId, int userId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var obj = (from addex in dbContext.additionalexam
                    join src in dbContext.servicecomponent on addex.v_ComponentId equals src.v_ComponentId
                    where addex.i_IsProcessed == (int) SiNo.SI && addex.i_IsDeleted == (int) SiNo.NO &&  addex.v_ServiceId == serviceId &&
                          src.v_ServiceComponentId == serviceComponentId
                    select addex).FirstOrDefault();

                obj.i_IsProcessed = (int) SiNo.NO;
                obj.i_UpdateUserId = userId;
                obj.d_UpdateDate = DateTime.Now;

                dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
