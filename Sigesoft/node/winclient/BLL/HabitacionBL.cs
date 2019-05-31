using Sigesoft.Node.WinClient.BE.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BLL
{
    public class HabitacionBL
    {
        public List<HabitacionCustom> GetHabitaciones(string value)
        {
            try
            {
                SigesoftEntitiesModel cnx = new SigesoftEntitiesModel();

                var listHabitaciones = (from sys in cnx.systemparameter
                    where sys.i_GroupId == 309
                    select sys).ToList();

                var listHabitacionesHosp = (from hab in cnx.hospitalizacionhabitacion
                                            where hab.i_EstateRoom != null && (hab.i_EstateRoom == 1 || hab.i_EstateRoom == 2)
                                            select hab).ToList();
                List<HabitacionCustom> ListHabit = new List<HabitacionCustom>();
                foreach (var habit in listHabitaciones)
                {
                    HabitacionCustom objHabit = new HabitacionCustom();
                    if (listHabitacionesHosp.Count > 0)
                    {
                        var objHab = listHabitacionesHosp.FindAll(x => x.i_HabitacionId == habit.i_ParameterId).FirstOrDefault();
                        if (objHab != null)
                        {
                            if (objHab.i_EstateRoom == (int)EstadoHabitacion.Ocupado)
                            {
                                objHabit.Habitacion = habit.v_Value1;
                                objHabit.Estado = "OCUPADO";
                                objHabit.i_HabitacionId = habit.i_ParameterId;
                                objHabit.v_HospHabitacionId = objHab.v_HospitalizacionHabitacionId;
                            }
                            else
                            {
                                objHabit.Habitacion = habit.v_Value1;
                                objHabit.Estado = "EN LIMPIEZA";
                                objHabit.i_HabitacionId = habit.i_ParameterId;
                                objHabit.v_HospHabitacionId = objHab.v_HospitalizacionHabitacionId;
                            }
                            
                        }
                        else
                        {
                            objHabit.Habitacion = habit.v_Value1;
                            objHabit.Estado = "LIBRE";
                            objHabit.i_HabitacionId = habit.i_ParameterId;
                        }
                    }
                    else
                    {
                        objHabit.Habitacion = habit.v_Value1;
                        objHabit.Estado = "LIBRE";
                        objHabit.i_HabitacionId = habit.i_ParameterId;
                    }
                    ListHabit.Add(objHabit);
                }

                return ListHabit.OrderBy(x => x.Habitacion).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool GetHabitacionByHabitacionId(string hospitalizacionId)
        {
            SigesoftEntitiesModel cnx = new SigesoftEntitiesModel();

            var query = (from hosp in cnx.hospitalizacionhabitacion
                where hosp.v_HopitalizacionId == hospitalizacionId select hosp).ToList();

            if (query.Count > 0)
            {
                int ultimo = query.Count - 1;
                int habitacionId = query[ultimo].i_HabitacionId.Value;
                var habitacion = (from hosp in cnx.hospitalizacionhabitacion
                                  where hosp.i_HabitacionId == habitacionId && hosp.i_EstateRoom == (int)EstadoHabitacion.Ocupado
                                  select hosp).FirstOrDefault();

                if (habitacion != null)
                {
                    return true;
                }
                else
                {
                    var ponerOcupado = (from hosp in cnx.hospitalizacionhabitacion
                        where hosp.v_HopitalizacionId == hospitalizacionId
                        select hosp).ToList();

                    ponerOcupado[ultimo].i_EstateRoom = (int) EstadoHabitacion.Ocupado;
                    cnx.SaveChanges();
                }
            }
            

            return false;
        }

        public void UpdateEstateHabitacion(int Estate, int HabitacionId, string HospitalizacionHabId)
        {
            try
            {

                SigesoftEntitiesModel cnx = new SigesoftEntitiesModel();
                var objHospHab = (from hosp in cnx.hospitalizacionhabitacion
                    where hosp.v_HospitalizacionHabitacionId == HospitalizacionHabId
                    select hosp).FirstOrDefault();

                if (objHospHab != null)
                {
                    objHospHab.i_EstateRoom = Estate;
                    cnx.SaveChanges();
                }
                
            }
            catch (Exception e)
            {
                return;
            }
            
        }

        public void UpdateEstateHabitacionLimpieza(string HospitalizacionId)
        {
            try
            {

                SigesoftEntitiesModel cnx = new SigesoftEntitiesModel();
                var ListHospHab = (from hosp in cnx.hospitalizacionhabitacion
                                   where hosp.v_HopitalizacionId == HospitalizacionId && hosp.i_EstateRoom == (int)EstadoHabitacion.Libre
                                   select hosp).ToList();

                if (ListHospHab.Count > 0)
                {
                    int ultimo = ListHospHab.Count - 1;
                    ListHospHab[ultimo].i_EstateRoom = (int)EstadoHabitacion.EnLimpieza;
                    cnx.SaveChanges();
                }

            }
            catch (Exception e)
            {
                return;
            }

        }
        public void UpdateEstateHabitacionByHospId(string hospId)
        {
            try
            {
                SigesoftEntitiesModel cnx = new SigesoftEntitiesModel();
                var objHospHab = (from hosp in cnx.hospitalizacionhabitacion
                                  where hosp.v_HopitalizacionId == hospId && hosp.i_EstateRoom == (int)EstadoHabitacion.Ocupado
                    select hosp).FirstOrDefault();

                objHospHab.i_EstateRoom = (int)EstadoHabitacion.EnLimpieza;

                cnx.SaveChanges();
            }
            catch (Exception e)
            {
                
                throw;
            }
        }
    }
}
