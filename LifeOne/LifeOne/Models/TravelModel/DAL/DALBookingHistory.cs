using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LifeOne.Models.TravelModel.DAL
{
    public class DALBookingHistory
    {

        public static DataSet GetBookingHistory(BookingParameter _param)
        {
            try
            {
                DataSet ds = _param.BookingHis();
                return ds;
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}