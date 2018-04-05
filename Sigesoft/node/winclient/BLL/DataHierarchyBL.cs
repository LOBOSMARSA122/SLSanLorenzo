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
    public class DataHierarchyBL
    {
        public datahierarchyDto GetDataHierarchy(ref OperationResult pobjOperationResult, int pintGroupId, int pintParameterId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                datahierarchyDto objDtoEntity = null;

                var objEntity = (from a in dbContext.datahierarchy
                                 where a.i_GroupId == pintGroupId && a.i_ItemId == pintParameterId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = datahierarchyAssembler.ToDTO(objEntity);

                pobjOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

        public List<DataHierarchyList> GetDataHierarchies(ref OperationResult pobjOperationResult,int pintGroupId ,int pintItemId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from A in dbContext.datahierarchy

                            //join J4 in dbContext.datahierarchy on new { ItemId = A.i_ParentItemId.Value, groupId = A.i_GroupId }
                            //                              equals new { ItemId = J4.i_ItemId, groupId = J4.i_GroupId } into J4_join
                            //from J4 in J4_join.DefaultIfEmpty()

                            where A.i_ParentItemId == pintItemId && A.i_GroupId == pintGroupId
                                  && (A.i_IsDeleted == 0 || A.i_IsDeleted == null)

                            select new DataHierarchyList
                            {
                                i_GroupId = A.i_GroupId,
                                i_ItemId = A.i_ItemId,
                                v_Value1 = A.v_Value1,
                                i_ParentItemId = A.i_ParentItemId.Value
                            };


                List<DataHierarchyList> objData = query.ToList();
                pobjOperationResult.Success = 1;
                return objData;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                return null;
            }
        }

    }
}
