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


        public List<string> GetAdditionalExamByServiceId( string serviceId)
        {
            SigesoftEntitiesModel dbcontext = new SigesoftEntitiesModel();

            var list = (from ade in dbcontext.additionalexam
                where ade.v_ServiceId == serviceId && ade.i_IsDeleted == 0 && ade.i_IsProcessed == 0
                select ade.v_ComponentId).ToList();

            return list;
        }
    }
}
