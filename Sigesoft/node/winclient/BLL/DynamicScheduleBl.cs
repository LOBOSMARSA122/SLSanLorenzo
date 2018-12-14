using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BLL
{
    public class DynamicScheduleBl
    {
        public List<ComponentsForHeaderGrid> GetAllComponent()
        {
            var dbContext = new SigesoftEntitiesModel();
            var objEntity = (from a in dbContext.component
                            join b in dbContext.systemparameter on new {a = a.i_CategoryId.Value, b = 116} 
                                    equals new {a = b.i_ParameterId, b = b.i_GroupId} into bJoin
                            from b in bJoin.DefaultIfEmpty()
                            where a.i_IsDeleted == 0
                            select new ComponentsForHeaderGrid
                            {
                                ComponentName = a.v_Name,
                                CategoryName = b.v_Value1,
                                ComponentId = a.v_ComponentId
                            }).ToList();

            return objEntity;
        }
    }

    public class ComponentsForHeaderGrid
    {
        public string CategoryName { get; set; }
        public string ComponentName { get; set; }
        public string ComponentId { get; set; }
    }
}
