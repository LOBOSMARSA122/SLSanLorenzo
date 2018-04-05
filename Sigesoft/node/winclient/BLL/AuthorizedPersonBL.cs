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
    public class AuthorizedPersonBL
    {
        public List<AuthorizedPersonList> GetAuthorizedPersonPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.authorizedperson

                            select new AuthorizedPersonList
                            {
                                v_AuthorizedPersonId = A.v_AuthorizedPersonId,
                                v_FirstName = A.v_FirstName,
                                v_FirstLastName = A.v_FirstLastName,
                                v_SecondLastName = A.v_SecondLastName,
                                i_DocTypeId = A.i_DocTypeId,
                                v_DocTypeName = A.v_DocTypeName,
                                v_DocNumber = A.v_DocNumber,
                                i_SexTypeId = A.i_SexTypeId,
                                v_SexTypeName = A.v_SexTypeName,
                                //d_BirthDate = (DateTime)A.d_BirthDate,
                                v_OccupationName = A.v_OccupationName,
                                v_OrganitationName = A.v_OrganitationName,
                                d_EntryToMedicalCenter =A.d_EntryToMedicalCenter.Value,
                                v_ProtocolId = A.v_ProtocolId,
                                v_Pacient = A.v_FirstName + " " + A.v_FirstLastName + " " + A.v_SecondLastName

                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                if (pintPageIndex.HasValue && pintResultsPerPage.HasValue)
                {
                    int intStartRowIndex = pintPageIndex.Value * pintResultsPerPage.Value;
                    query = query.Skip(intStartRowIndex);
                }
                if (pintResultsPerPage.HasValue)
                {
                    query = query.Take(pintResultsPerPage.Value);
                }

                List<AuthorizedPersonList> objData = query.ToList();
                pobjOperationResult.Success = 1;
                return objData;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public List<AuthorizedPersonList> GetAuthorizedPersonPagedAndFilteredNOTNULL(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.authorizedperson
                            where A.d_EntryToMedicalCenter != null
                            select new AuthorizedPersonList
                            {
                                v_AuthorizedPersonId = A.v_AuthorizedPersonId,
                                v_FirstName = A.v_FirstName,
                                v_FirstLastName = A.v_FirstLastName,
                                v_SecondLastName = A.v_SecondLastName,
                                i_DocTypeId = A.i_DocTypeId,
                                v_DocTypeName = A.v_DocTypeName,
                                v_DocNumber = A.v_DocNumber,
                                i_SexTypeId = A.i_SexTypeId,
                                v_SexTypeName = A.v_SexTypeName,
                                //d_BirthDate = (DateTime)A.d_BirthDate,
                                v_OccupationName = A.v_OccupationName,
                                v_OrganitationName = A.v_OrganitationName,
                                d_EntryToMedicalCenter = A.d_EntryToMedicalCenter.Value,
                                v_ProtocolId = A.v_ProtocolId,
                                v_Pacient = A.v_FirstName + " " + A.v_FirstLastName + " " + A.v_SecondLastName,
                                v_ProtocolName = A.v_ProtocolName
                              

                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (!string.IsNullOrEmpty(pstrSortExpression))
                {
                    query = query.OrderBy(pstrSortExpression);
                }
                if (pintPageIndex.HasValue && pintResultsPerPage.HasValue)
                {
                    int intStartRowIndex = pintPageIndex.Value * pintResultsPerPage.Value;
                    query = query.Skip(intStartRowIndex);
                }
                if (pintResultsPerPage.HasValue)
                {
                    query = query.Take(pintResultsPerPage.Value);
                }

                List<AuthorizedPersonList> objData = query.ToList();
                pobjOperationResult.Success = 1;
                return objData;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }
       
        public void AddAuthorizedPerson(ref OperationResult pobjOperationResult, authorizedpersonDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                authorizedperson objEntity = authorizedpersonAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
           
                // Autogeneramos el Pk de la tabla                 
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 100), "XX"); ;
                objEntity.v_AuthorizedPersonId = NewId;

                dbContext.AddToauthorizedperson(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PERSONAS AUTORIZADAS", "v_AuthorizedPersonId=" + NewId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "PERSONAS AUTORIZADAS", "v_AuthorizedPersonId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        //public List<authorizedpersonDto> GetAuthorizedPersonAccess(ref OperationResult pobjOperationResult, string pstrNameOrOrganitation)
        //{
        //    //mon.IsActive = true;

        //    try
        //    {
        //        SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
        //        authorizedpersonDto objDtoEntity = null;

        //        var objEntity = (from A in dbContext.authorizedperson                               
        //                         where ((A.v_FirstName +  " " + A.v_FirstLastName + " " + A.v_SecondLastName).Contains(pstrNameOrOrganitation)) || A.v_OrganitationName.Contains(pstrNameOrOrganitation)
        //                         select A);

        //        List<AuthorizedPersonList> objData = objEntity.ToList();
        //        pobjOperationResult.Success = 1;
        //        return objData;

        //        pobjOperationResult.Success = 1;
        //        return objDtoEntity;
        //    }
        //    catch (Exception ex)
        //    {
        //        pobjOperationResult.Success = 0;
        //        pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
        //        return null;
        //    }
        //}

        public void UpdateAuthorizedPerson(ref OperationResult pobjOperationResult, authorizedpersonDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.authorizedperson
                                       where a.v_AuthorizedPersonId == pobjDtoEntity.v_AuthorizedPersonId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                authorizedperson objEntity = authorizedpersonAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.authorizedperson.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PERSONAS AUTORIZADAS", "v_AuthorizedPersonId=" + objEntity.v_AuthorizedPersonId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "PERSONAS AUTORIZADAS", "v_AuthorizedPersonId=" + pobjDtoEntity.v_AuthorizedPersonId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public authorizedpersonDto GetAuthorizedPerson(ref OperationResult pobjOperationResult, string pstrAuthorizedPersonId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                authorizedpersonDto objDtoEntity = null;

                var objEntity = (from A in dbContext.authorizedperson
                                 where A.v_AuthorizedPersonId == pstrAuthorizedPersonId
                                 select A).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = authorizedpersonAssembler.ToDTO(objEntity);

                pobjOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        public void DeleteAuthorizedPerson(ref OperationResult pobjOperationResult, string pintAuthorizedPersonId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.authorizedperson
                                       where a.v_AuthorizedPersonId == pintAuthorizedPersonId 
                                       select a).FirstOrDefault();

                //var registro = (from c in linq.Tabla
                //                where c.id == id
                //                select c).Single();

                //linq.Tabla.DeleteOnSubmit(registro);
                //linq.SubmitChanges();

                dbContext.authorizedperson.DeleteObject(objEntitySource);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
               
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                  return;
            }
        }


        public void DeleteAuthorizedPersonAll(ref OperationResult pobjOperationResult)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.authorizedperson
                                       select a).ToList();
                foreach (var item in objEntitySource)
                {
                    dbContext.authorizedperson.DeleteObject(item);
                    dbContext.SaveChanges();
                }

              

                pobjOperationResult.Success = 1;
                // Llenar entidad Log

                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = ex.Message;
                // Llenar entidad Log
                return;
            }
        }

    }
}
