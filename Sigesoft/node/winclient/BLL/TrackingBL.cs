using Sigesoft.Node.WinClient.BE.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BLL
{
    public class TrackingBL
    {
        public List<TrackingCustom> GetAllDataTracking(string name)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var List = (from per in dbContext.person
                    join pac in dbContext.pacient on per.v_PersonId equals pac.v_PersonId
                    join tra in dbContext.tracking on pac.v_PersonId equals tra.v_PersonId
                    where per.i_IsDeleted == 0 && (per.v_FirstName.Contains(name) || per.v_FirstLastName.Contains(name)
                                                                                  || per.v_SecondLastName
                                                                                      .Contains(name) ||
                                                                                  per.v_DocNumber.Contains(name))
                    select new TrackingCustom
                    {
                        Pacientes = per.v_FirstLastName + " " + per.v_SecondLastName + ", " + per.v_FirstName,
                        Agenda = tra.i_Agenda.Value,
                        Carta_Garantia = tra.i_CartaGarantia.Value,
                        AtencionMedica = tra.i_AtencionMedica.Value,
                        Pre_Liquidacion = tra.i_PreLiquidacion.Value,
                        Control_Calidad = tra.i_ControlCalidad.Value,
                        Facturacion = tra.i_Facturacion.Value,
                        Culminado = tra.i_Culminado.Value,

                    }).ToList();

                return List;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
