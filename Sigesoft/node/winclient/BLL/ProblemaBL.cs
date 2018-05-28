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
    public class ProblemaBL
    {
        public List<ProblemaList> GetProblemaPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, string pstrPersonId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.problema
                            join B in dbContext.systemparameter on new { a = A.i_EsControlado.Value, b = 111 }  // CATEGORIA DEL EXAMEN
                                                        equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                            from B in B_join.DefaultIfEmpty()
                            where A.i_IsDeleted == 0 && A.v_PersonId == pstrPersonId

                            select new ProblemaList
                            {
                                v_ProblemaId =  A.v_ProblemaId,
                                i_Tipo = A.i_Tipo,
                                v_PersonId = A.v_PersonId,
                                d_Fecha = A.d_Fecha.Value,
                                v_Descripcion = A.v_Descripcion,
                                i_EsControlado = A.i_EsControlado,
                                v_Observacion = A.v_Observacion,
                                v_EsControlado = B.v_Value1
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

                List<ProblemaList> objData = query.ToList();
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

        public problemaDto GetProblema(ref OperationResult pobjOperationResult, string pstrProblemaId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                problemaDto objDtoEntity = null;

                var objEntity = (from a in dbContext.problema
                                 where a.v_ProblemaId == pstrProblemaId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = problemaAssembler.ToDTO(objEntity);

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

        public void AddProblema(ref OperationResult pobjOperationResult, problemaDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                problema objEntity = problemaAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla                 
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 326), "PM"); ;
                objEntity.v_ProblemaId = NewId;

                dbContext.AddToproblema(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return;
            }
        }

        public void UpdateProblema(ref OperationResult pobjOperationResult, problemaDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.problema
                                       where a.v_ProblemaId == pobjDtoEntity.v_ProblemaId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                problema objEntity = problemaAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.problema.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return;
            }
        }

        public void DeleteProblema(ref OperationResult pobjOperationResult, string pstrProblemaId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.problema
                                       where a.v_ProblemaId == pstrProblemaId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
               return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return;
            }
        }

    }
}
