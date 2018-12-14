using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class ComponentList
    {
        public string v_ComponentId { get; set; }
        public string v_Name { get; set; }
        public string v_ServiceComponentId { get; set; }
        //public string v_IsGroupName { get; set; }
        public int? i_CategoryId { get; set; }
        public string v_CategoryName { get; set; }

        //public int i_DiagnosableId { get; set; }
        //public string v_DiagnosableName { get; set; }
        public int? i_ComponentTypeId { get; set; }
        //public string v_ComponentTypeName { get; set; }
        public float? r_BasePrice { get; set; }
        public int? i_UIIsVisibleId { get; set; }

        public int i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }
        public string[] componentsId { get; set; }

        // jerarquia

        public List<ComponentFieldsList> Fields { get; set; }

        public int? i_Index { get; set; }

        public int? i_GroupedComponentId { get; set; }
        public string v_GroupedComponentName { get; set; }
        public int i_IsGroupedComponent { get; set; }
        public List<ComponentList> GroupedComponentsName { get; set; }
        public string v_ComponentCopyId { get; set; }
        public int? i_ServiceComponentStatusId { get; set; }

    }
}
