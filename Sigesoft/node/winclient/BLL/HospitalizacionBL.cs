using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Common;
using System.Data.Objects;

namespace Sigesoft.Node.WinClient.BLL
{
    public class HospitalizacionBL
    {
        public List<HospitalizacionList> GetHospitalizacionPagedAndFiltered(ref OperationResult pobjOperationResult, int? pintPageIndex, int? pintResultsPerPage, string pstrSortExpression, string pstrFilterExpression, DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {

            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();

                var query = from A in dbContext.hospitalizacion
                            join person B in dbContext.person on A.v_PersonId equals B.v_PersonId
                            join C in dbContext.hospitalizacionservice on A.v_HopitalizacionId equals C.v_HopitalizacionId
                            join D in dbContext.service on C.v_ServiceId equals D.v_ServiceId
                            join E in dbContext.ticket on D.v_ServiceId equals E.v_ServiceId
                            join F in dbContext.ticketdetalle on E.v_TicketId equals F.v_TicketId
                            //join G in dbContext.product on F.v_IdProductoDetalle equals G.v_ProductId

                            where A.i_IsDeleted == 0

                            select new HospitalizacionList
                            {
                                d_FechaAlta = A.d_FechaAlta,
                                d_FechaIngreso = A.d_FechaIngreso,
                                v_Paciente = B.v_FirstName +" " +  B.v_FirstLastName + " " + B.v_SecondLastName ,
                                v_HopitalizacionId = A.v_HopitalizacionId,
                                v_PersonId = A.v_PersonId,
                                i_IsDeleted = A.i_IsDeleted.Value,

                            };

                if (!string.IsNullOrEmpty(pstrFilterExpression))
                {
                    query = query.Where(pstrFilterExpression);
                }
                if (pdatBeginDate.HasValue && pdatEndDate.HasValue)
                {
                    query = query.Where("d_FechaIngreso >= @0 && d_FechaIngreso <= @1", pdatBeginDate.Value, pdatEndDate.Value);
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

                List<HospitalizacionList> objData = query.ToList();

                hospitalizacionserviceDto ohospi = null;

                // en hospitalizaciones tienes los padres
                var hospitalizaciones = (from a in objData
                         select new HospitalizacionList
                         {
                             v_Paciente = a.v_Paciente,
                             d_FechaIngreso = a.d_FechaIngreso,
                             d_FechaAlta = a.d_FechaAlta,
                             v_HopitalizacionId = a.v_HopitalizacionId,
                             v_PersonId = a.v_PersonId
                         }).ToList();

                // aca vas a recorrer cada padre y buscar si tienen hijos (servicios)
                //foreach (var item in hospitalizaciones)
                //{
                //    // estos son los hijos de 1 hopitalización
                //    var servicios = BuscarServiciosHospitalizacion(item.v_HopitalizacionId).ToList();

                //    List<HospitalizacionServiceList> HospitalizacionServicios = new List<HospitalizacionServiceList>();
                //    if (servicios.Count > 0)
                //    {
                //        HospitalizacionServiceList oHospitalizacionServiceList;
                //        foreach (var servicio in servicios)
                //        {
                //            oHospitalizacionServiceList = new HospitalizacionServiceList();
                //            // acá vas poblando la entidad hijo hospitalización
                //            oHospitalizacionServiceList.v_HopitalizacionId = servicio.v_HopitalizacionId;

                //            // acá estoy agregando a las lista
                //            HospitalizacionServicios.Add(servicio);

                //        }
                        
                //    }
                    
                //}

                pobjOperationResult.Success = 1;
                return hospitalizaciones;

            }
            catch (Exception ex)
            {
                pobjOperationResult.Success = 0;
                pobjOperationResult.ExceptionMessage = Common.Utils.ExceptionFormatter(ex);
                return null;
            }
        }

        private List<HospitalizacionServiceList> BuscarServiciosHospitalizacion(string hospitalizacionId)
        {
            //acá hace un select a la tabla hospitalizacionService y buscas todos que tengan foranea HospitalizacionId
            return null;
        }
    }
}
