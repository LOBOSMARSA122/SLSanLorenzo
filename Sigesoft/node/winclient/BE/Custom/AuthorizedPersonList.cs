using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class AuthorizedPersonList
    {
         
        public string v_PersonId { get; set; }
         
        public String v_FirstName { get; set; }
         
        public String v_FirstLastName { get; set; }
         
        public String v_SecondLastName { get; set; }
         
        public Nullable<Int32> i_DocTypeId { get; set; }
         
        public String v_DocNumber { get; set; }
         
        public Nullable<DateTime> d_Birthdate { get; set; }
         
        public String v_BirthPlace { get; set; }
         
        public Nullable<Int32> i_SexTypeId { get; set; }
         
        public Nullable<Int32> i_MaritalStatusId { get; set; }
         
        public Nullable<Int32> i_LevelOfId { get; set; }
         
        public String v_TelephoneNumber { get; set; }
         
        public String v_AdressLocation { get; set; }
         
        public String v_GeografyLocationId { get; set; }
         
        public String v_ContactName { get; set; }
         
        public String v_EmergencyPhone { get; set; }
         
        public String v_Mail { get; set; }
         
        public string v_DocTypeName { get; set; }
         
        public string v_SexTypeName { get; set; }
         
        public string v_CreationUser { get; set; }
         
        public string v_UpdateUser { get; set; }
         
        public DateTime? d_CreationDate { get; set; }
         
        public DateTime? d_UpdateDate { get; set; }

        public int? i_IsDeleted { get; set; }
         
        public int? i_UpdateNodeId { get; set; }

        public Byte[] b_Photo { get; set; }

        public int i_Correlative { get; set; }

        public int i_BloodGroupId { get; set; }
        public int i_BloodFactorId { get; set; }

         
        public Byte[] b_FingerPrintTemplate { get; set; }

         
        public Nullable<Int32> i_FingerPrintQuality { get; set; }

         
        public Nullable<Int32> i_FingerPrintId { get; set; }

         
        public String v_FingerPrintId { get; set; }

         
        public Byte[] b_RubricImage { get; set; }

         
        public Byte[] b_FingerPrintImage { get; set; }

         
        public String t_RubricImageText { get; set; }

         
        public String b_RubricImageBitVaring { get; set; }

        public string v_CurrentOccupation { get; set; }

         
        public Nullable<Int32> i_DepartmentId { get; set; }

         
        public Nullable<Int32> i_ProvinceId { get; set; }

         
        public Nullable<Int32> i_DistrictId { get; set; }

         
        public Nullable<Int32> i_ResidenceInWorkplaceId { get; set; }

         
        public String v_ResidenceTimeInWorkplace { get; set; }

         
        public Nullable<Int32> i_TypeOfInsuranceId { get; set; }

         
        public Nullable<Int32> i_NumberLivingChildren { get; set; }

         
        public Nullable<Int32> i_NumberDependentChildren { get; set; }

        public String v_OrganitationName { get; set; }

        public string v_AuthorizedPersonId { get; set; }

        public string v_ProtocolId { get; set; }

        public string v_ProtocolName { get; set; }

        public string v_OccupationName { get; set; }

        public DateTime? d_EntryToMedicalCenter { get; set; }

        public string v_Pacient { get; set; }

        public string v_FullPersonName { get; set; }
    }
}
