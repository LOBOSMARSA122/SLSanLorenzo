using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Sigesoft.Node.WinClient.BE
{
    [DataContract]
    public class PersonList
    {
        [DataMember]
        public Int32 i_PersonId { get; set; }
        [DataMember]
        public String v_FirstName { get; set; }
        [DataMember]
        public String v_FirstLastName { get; set; }
        [DataMember]
        public String v_SecondLastName { get; set; }
        [DataMember]
        public Nullable<Int32> i_DocTypeId { get; set; }
        [DataMember]
        public String v_DocNumber { get; set; }
        [DataMember]
        public Nullable<DateTime> d_Birthdate { get; set; }
        [DataMember]
        public String v_BirthPlace { get; set; }
        [DataMember]
        public Nullable<Int32> i_SexTypeId { get; set; }
        [DataMember]
        public Nullable<Int32> i_MaritalStatusId { get; set; }
        [DataMember]
        public Nullable<Int32> i_LevelOfId { get; set; }
        [DataMember]
        public String v_TelephoneNumber { get; set; }
        [DataMember]
        public String v_AdressLocation { get; set; }
        [DataMember]
        public String v_GeografyLocationId { get; set; }
        [DataMember]
        public String v_ContactName { get; set; }
        [DataMember]
        public String v_EmergencyPhone { get; set; }
        [DataMember]
        public String v_Mail { get; set; }
        [DataMember]
        public byte b_Photo { get; set; }
        [DataMember]
        public string v_CreationUser { get; set; }
        [DataMember]
        public string v_UpdateUser { get; set; }
        [DataMember]
        public DateTime? d_CreationDate { get; set; }

        public String v_CentroEducativo { get; set; }

        [DataMember]
        public int? i_IsDeleted { get; set; }

        public String TipoDocumento { get; set; }
        public String Puesto { get; set; }
        public int? Edad { get; set; }

        public String Empresa { get; set; }
        public String Perfil { get; set; }
        public String TipoExamen { get; set; }

        public String v_PersonId { get; set; }
        public byte[] b_PersonImage { get; set; }

         public int?  i_BloodGroupId { get; set; }
         public int? i_BloodFactorId { get; set; }
         public byte[] b_FingerPrintTemplate { get; set; }
         public byte[] b_RubricImage { get; set; }
         public byte[] b_FingerPrintImage { get; set; }
       public String t_RubricImageText { get; set; }
       public String v_CurrentOccupation { get; set; }
       public int? i_DepartmentId { get; set; }
       public int? i_ProvinceId { get; set; }
       public int? i_DistrictId { get; set; }
       public int? i_ResidenceInWorkplaceId { get; set; }
       public String v_ResidenceTimeInWorkplace { get; set; }
       public int? i_TypeOfInsuranceId { get; set; }
       public int? i_NumberLivingChildren { get; set; }
       public int? i_NumberDependentChildren { get; set; }
       public int? i_OccupationTypeId { get; set; }
       public String v_OwnerName { get; set; }
       public int? i_NumberLiveChildren { get; set; }
       public int? i_NumberDeadChildren { get; set; }


       public int? i_InsertUserId { get; set; }
       public DateTime? d_InsertDate { get; set; }
       public int? i_UpdateUserId { get; set; }
       public DateTime? d_UpdateDate { get; set; }
       public int? i_InsertNodeId { get; set; }
       public int? i_UpdateNodeId { get; set; }
       public int? i_Relationship { get; set; }
       public String v_ExploitedMineral  { get; set; }
       public int? i_AltitudeWorkId { get; set; }
       public int? i_PlaceWorkId { get; set; }
       public String v_NroPoliza  { get; set; }
       public decimal?  v_Deducible  { get; set; }
       public int? i_NroHermanos { get; set; }
       public String v_Password  { get; set; }

       public String v_Religion { get; set; }
       public String v_Nacionalidad { get; set; }
       public String v_ResidenciaAnterior { get; set; }
    }
}
