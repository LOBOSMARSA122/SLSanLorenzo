using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    [DataContract]
   public class DataHierarchyList
    {
        [DataMember]
        public int i_GroupId { get; set; }
        [DataMember]
        public int i_ItemId { get; set; }
        [DataMember]
        public string v_Value1 { get; set; }
        [DataMember]
        public string v_Value2 { get; set; }     
        [DataMember]
        public int i_ParentItemId { get; set; }
        [DataMember]
        public string v_ParentItemName { get; set; }
        [DataMember]
        public string v_CreationUser { get; set; }
        [DataMember]
        public string v_UpdateUser { get; set; }
        [DataMember]
        public DateTime? d_CreationDate { get; set; }
        [DataMember]
        public DateTime? d_UpdateDate { get; set; }
        [DataMember]
        public int? i_IsDeleted { get; set; }
    }

    [Serializable]
    public class DataForTreeView
    {
        public int Id { get; set; }     
        public int ParentId { get; set; }
        public string Description { get; set; }   
        public string Description2 { get; set; }
        public int Level { get; set; } 
        public bool EnabledSelect { get; set; }
        public int i_ParameterId { get; set; }
        public int? i_Sort { get; set; } 
        public override string ToString()
        {
            return String.Format("Id={0} / ParentId={1} / Description={2} / Description2={3}/ Level={4}", Id, ParentId, Description,Description2, Level, true);
        }
    }

    [DataContract]
    public class DataTreeViewForGridView
    {
        [DataMember]
        public int i_ItemId { get; set; }
        [DataMember]
        public int i_ParentItemId { get; set; }
        [DataMember]
        public string v_Value1 { get; set; }
        [DataMember]
        public string v_Value2 { get; set; }
        [DataMember]
        public int Level { get; set; }
        [DataMember]
        public int i_GroupId { get; set; }

        public override string ToString()
        {
            return String.Format("i_ItemId={0} / i_ParentItemId={1} / v_Value1={2} / i_GroupId={3} / Level={4} ", i_ItemId, i_ParentItemId, v_Value1, i_GroupId, Level);
        }

    }
}
