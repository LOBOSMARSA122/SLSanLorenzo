using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Server.WebClientAdmin.BE
{
    [DataContract]
    public class ApplicationHierarchyList
    {
        [DataMember]
        public int i_ApplicationHierarchyId { get; set; }
        [DataMember]
        public int i_ApplicationHierarchyTypeId { get; set; }
        [DataMember]
        public int i_Level { get; set; }
        [DataMember]
        public string v_Description { get; set; }
        [DataMember]
        public string v_Form { get; set; }
        [DataMember]
        public string v_Code { get; set; }
        [DataMember]
        public int i_ParentId { get; set; }
        [DataMember]
        public int i_ScopeId { get; set; }
        [DataMember]
        public int i_IsDeleted { get; set; }
        [DataMember]
        public int i_InsertUserId { get; set; }
        [DataMember]
        public DateTime d_InsertDate { get; set; }
        [DataMember]
        public int i_UpdateUserId { get; set; }
        [DataMember]
        public DateTime d_UpdateDate { get; set; }
    }


    [DataContract]
    public class DtvAppHierarchy
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ParentId { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Description2 { get; set; }
        [DataMember]
        public int Level { get; set; }
        [DataMember]
        public bool EnabledSelect { get; set; }
        public int i_GroupId { get; set; }

        public override string ToString()
        {
            return String.Format("Id={0} / ParentId={1} / Description={2} / Description2={3}/ Level={4}", Id, ParentId, Description, Description2, Level, true);
        }
    }

    [DataContract]
    public class DtvForGrwAppHierarchy
    {
        [DataMember]
        public int i_ApplicationHierarchyId { get; set; }
        [DataMember]
        public int i_ParentItemId { get; set; }
        [DataMember]
        public string v_Value1 { get; set; }
        [DataMember]
        public int Level { get; set; }
        [DataMember]
        public int i_GroupId { get; set; }
        [DataMember]
        public string v_ApplicationHierarchyTypeName { get; set; }
        [DataMember]
        public string v_Form { get; set; }
        [DataMember]
        public string v_Code { get; set; }
        [DataMember]
        public int i_ParentId { get; set; }
        [DataMember]
        public int i_ScopeId { get; set; }
        [DataMember]
        public string v_ScopeName { get; set; }
        [DataMember]
        public int i_BusinessRuleId { get; set; }
        [DataMember]
        public string v_BusinessRuleName { get; set; }
        [DataMember]
        public int i_IsDeleted { get; set; }
        [DataMember]
        public int i_InsertUserId { get; set; }
        [DataMember]
        public DateTime d_InsertDate { get; set; }
        [DataMember]
        public int i_UpdateUserId { get; set; }
        [DataMember]
        public DateTime d_UpdateDate { get; set; }

        public override string ToString()
        {
            return String.Format("i_ApplicationHierarchyId={0} / i_ParentItemId={1} / v_Value1={2} / i_GroupId={3} / Level={4}  / v_Form={5} / v_Code={6} / v_ScopeName={7} / v_ApplicationHierarchyTypeName={8}  / v_BusinessRuleName={9}",
    i_ApplicationHierarchyId, i_ParentItemId, v_Value1, i_GroupId, Level, v_Form, v_Code, v_ScopeName, v_ApplicationHierarchyTypeName, v_BusinessRuleName);
        }

    }
}
