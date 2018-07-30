using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BLL
{
    public class HospitalizacionHabitacionBL
    {

        public List<HospitalizacionHabitacionList> GetHospitalizacionHabitacionPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.hospitalizacionhabitacion
                            join B in dbContext.systemparameter on new { a = A.i_HabitacionId.Value, b = 309 } equals new { a = B.i_ParameterId, b = B.i_GroupId } into B_join
                            from B in B_join.DefaultIfEmpty()
                            where A.i_IsDeleted == 0 

                            select new HospitalizacionHabitacionList
                            {
                                v_HospitalizacionHabitacionId = A.v_HospitalizacionHabitacionId,
                                v_HopitalizacionId =A.v_HopitalizacionId,
                                i_HabitacionId = A.i_HabitacionId.Value,
                                d_StartDate = A.d_StartDate.Value,
                                d_EndDate = A.d_EndDate.Value,
                                NroHabitacion = B.v_Value1
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

                List<HospitalizacionHabitacionList> objData = query.ToList();
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

        public hospitalizacionhabitacionDto GetHospitalizacionHabitacion(ref OperationResult pobjOperationResult, string pstrHospitalizacionHabitacionId)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                hospitalizacionhabitacionDto objDtoEntity = null;

                var objEntity = (from a in dbContext.hospitalizacionhabitacion
                                 where a.v_HospitalizacionHabitacionId == pstrHospitalizacionHabitacionId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = hospitalizacionhabitacionAssembler.ToDTO(objEntity);

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

        public string AddHospitalizacionHabitacion(ref OperationResult pobjOperationResult, hospitalizacionhabitacionDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;
            string NewId = "(No generado)";
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                hospitalizacionhabitacion objEntity = hospitalizacionhabitacionAssembler.ToEntity(pobjDtoEntity);

                objEntity.d_InsertDate = DateTime.Now;
                objEntity.i_InsertUserId = Int32.Parse(ClientSession[2]);
                objEntity.i_IsDeleted = 0;
                // Autogeneramos el Pk de la tabla                 
                int intNodeId = int.Parse(ClientSession[0]);
                NewId = Common.Utils.GetNewId(intNodeId, Utils.GetNextSecuentialId(intNodeId, 352), "HH"); ;
                objEntity.v_HospitalizacionHabitacionId = NewId;

                dbContext.AddTohospitalizacionhabitacion(objEntity);
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ÁREA", "v_HospitalizacionHabitacionId=" + NewId.ToString(), Success.Ok, null);
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.CREACION, "ÁREA", "v_HospitalizacionHabitacionId=" + NewId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                
            }
            return NewId;
        }

        public void UpdateHospitalizacionHabitacion(ref OperationResult pobjOperationResult, hospitalizacionhabitacionDto pobjDtoEntity, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.hospitalizacionhabitacion
                                       where a.v_HospitalizacionHabitacionId == pobjDtoEntity.v_HospitalizacionHabitacionId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                pobjDtoEntity.d_UpdateDate = DateTime.Now;
                pobjDtoEntity.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                hospitalizacionhabitacion objEntity = hospitalizacionhabitacionAssembler.ToEntity(pobjDtoEntity);

                // Copiar los valores desde la entidad actualizada a la Entidad Fuente
                dbContext.hospitalizacionhabitacion.ApplyCurrentValues(objEntity);

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ÁREA", "v_HospitalizacionHabitacionId=" + objEntity.v_HospitalizacionHabitacionId.ToString(), Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ACTUALIZACION, "ÁREA", "v_HospitalizacionHabitacionId=" + pobjDtoEntity.v_HospitalizacionHabitacionId.ToString(), Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public void DeleteHospitalizacionHabitacion(ref OperationResult pobjOperationResult, string pstrHospitalizacionHabitacionId, List<string> ClientSession)
        {
            //mon.IsActive = true;

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                // Obtener la entidad fuente
                var objEntitySource = (from a in dbContext.hospitalizacionhabitacion
                                       where a.v_HospitalizacionHabitacionId == pstrHospitalizacionHabitacionId
                                       select a).FirstOrDefault();

                // Crear la entidad con los datos actualizados
                objEntitySource.d_UpdateDate = DateTime.Now;
                objEntitySource.i_UpdateUserId = Int32.Parse(ClientSession[2]);
                objEntitySource.i_IsDeleted = 1;

                // Guardar los cambios
                dbContext.SaveChanges();

                pobjOperationResult.Success = 1;
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "ÁREA", "", Success.Ok, null);
                return;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                // Llenar entidad Log
                LogBL.SaveLog(ClientSession[0], ClientSession[1], ClientSession[2], LogEventType.ELIMINACION, "ÁREA", "", Success.Failed, pobjOperationResult.ExceptionMessage);
                return;
            }
        }

        public HospitalizacionHabitacionList GetHabitaciónHospi(ref OperationResult pobjOperationResult, int habId)
        {
            //mon.IsActive = true;
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
               
                    var query = (from a in dbContext.hospitalizacionhabitacion
                             join b in dbContext.hospitalizacion on a.v_HopitalizacionId equals b.v_HopitalizacionId

                             join J1 in dbContext.systemparameter on new { a = 309, b = a.i_HabitacionId.Value }
                                                                     equals new { a = J1.i_GroupId, b = J1.i_ParameterId } into J1_join
                             from J1 in J1_join.DefaultIfEmpty()

                             where a.i_IsDeleted == 0 && J1.i_ParameterId == habId
                             select new HospitalizacionHabitacionList
                             {
                                 v_HopitalizacionId = b.v_HopitalizacionId,
                                 i_HabitacionId = J1.i_ParameterId,
                                 NroHabitacion = J1.v_Value1,
                                 d_Precio = a.d_Precio.Value
                             }

                            ).FirstOrDefault();
                pobjOperationResult.Success = 1;
                return query;
            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }


        public SystemParameterList GetHabitaciónH(ref OperationResult pobjOperationResult, int habId)
        {

            SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

            var query = (from a in dbContext.systemparameter
                         where a.i_IsDeleted == 0 && a.i_ParameterId == habId && a.i_GroupId == 309
                         select new SystemParameterList
                         {
                             i_ParameterId = a.i_ParameterId,
                             i_GroupId = a.i_GroupId,
                             v_Value1 = a.v_Value1,
                             v_Value2 = a.v_Value2
                         }
                    ).FirstOrDefault();
            pobjOperationResult.Success = 1;
            return query;
        }

        public hospitalizacionhabitacionDto GetHabitacion(ref OperationResult objOperationResult, string _habitacionId)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                hospitalizacionhabitacionDto objDtoEntity = null;

                var objEntity = (from a in dbContext.hospitalizacionhabitacion
                                 where a.v_HospitalizacionHabitacionId == _habitacionId
                                 select a).FirstOrDefault();

                if (objEntity != null)
                    objDtoEntity = hospitalizacionhabitacionAssembler.ToDTO(objEntity);

                objOperationResult.Success = 1;
                return objDtoEntity;
            }
            catch (Exception ex)
            {
                objOperationResult.Success = 0;
                objOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

    }
}
