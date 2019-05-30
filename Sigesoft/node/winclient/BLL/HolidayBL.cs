using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sigesoft.Common;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;

namespace Sigesoft.Node.WinClient.BLL
{
    public class HolidayBL
    {
        public List<holidaysDto> GetHolidays(string reason)
        {
            try
            {
                SigesoftEntitiesModel cnx = new SigesoftEntitiesModel();

                var list = cnx.holidays.Where(x => x.i_IsDeleted == (int)SiNo.NO && x.v_Reason.Contains(reason)).ToDTOs();

                return list;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AddHoliday(holidaysDto dataHoliday, int nodeId)
        {
            try
            {
                SigesoftEntitiesModel cnx = new SigesoftEntitiesModel();
                string newId = Common.Utils.GetNewId(nodeId, Utils.GetNextSecuentialId(nodeId, 51), "FR");
                dataHoliday.v_HolidayId = newId;
                holidays entityHolidays = holidaysAssembler.ToEntity(dataHoliday);

                cnx.AddToholidays(entityHolidays);
                
                return cnx.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteHoliday(string holidayId)
        {
            try
            {
                SigesoftEntitiesModel cnx = new SigesoftEntitiesModel();

                var objHoliday = cnx.holidays.Where(x => x.v_HolidayId == holidayId).FirstOrDefault();

                objHoliday.i_IsDeleted = (int) SiNo.SI;

                return cnx.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateHoliday(holidaysDto dataHoliday)
        {
            try
            {
                SigesoftEntitiesModel cnx = new SigesoftEntitiesModel();

                var objHoliday = cnx.holidays.Where(x => x.v_HolidayId == dataHoliday.v_HolidayId).FirstOrDefault();

                objHoliday.i_Year = dataHoliday.i_Year;
                objHoliday.d_Date = dataHoliday.d_Date;
                objHoliday.v_Reason = dataHoliday.v_Reason;

                return cnx.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
