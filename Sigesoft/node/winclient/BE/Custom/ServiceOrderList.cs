using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
    public class ServiceOrderList
    {        
        public String v_ServiceOrderId { get; set; }        
        public String v_CustomServiceOrderId { get; set; }        
        public String v_Description { get; set; }        
        public String v_Comentary { get; set; }        
        public Nullable<Int32> i_NumberOfWorker { get; set; }        
        public Nullable<Single> r_TotalCost { get; set; }        
        public Nullable<DateTime> d_DeliveryDate { get; set; }        
        public Nullable<Int32> i_ServiceOrderStatusId { get; set; }
        public string v_ServiceOrderStatusName { get; set; }
        public string v_ProtocolId { get; set; }
        public Nullable<Int32> i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }      
        public string v_UpdateUser { get; set; } 
        public DateTime? d_CreationDate { get; set; }   
        public DateTime? d_UpdateDate { get; set; }


       public string v_Protocol {get;set;}
       public string v_Organization {get;set;}
       public string v_ContacName {get;set;}
       public string v_Address {get;set;}
       public string v_EsoType {get;set;}
       public string v_ComponentName {get;set;}
       public Nullable<Single> r_Price { get; set; }
       public DateTime d_InsertDate { get; set; }

       public string v_CustomerOrganizationName { get; set; }
       public string v_CustomerOrganizationId { get; set; }
       public string v_CustomerLocationId { get; set; }

       public string v_EmployerOrganizationName { get; set; }
       public string v_EmployerOrganizationId { get; set; }
       public string v_EmployerLocationId { get; set; }

       public string v_WorkingOrganizationName { get; set; }
       public string v_WorkingOrganizationId { get; set; }
       public string v_WorkingLocationId { get; set; }

       public Byte[] Logo { get; set; }

       public string RucCliente { get; set; }

       public string EmpresaPropietaria { get; set; }
       public string EmpresaPropietariaDireccion { get; set; }
       public string EmpresaPropietariaTelefono { get; set; }
       public string EmpresaPropietariaEmail { get; set; }
       public string GESO { get; set; }

       public byte[] Firma { get; set; }

       public int? CantidadTrabajadores { get; set; }
    
    }
}
