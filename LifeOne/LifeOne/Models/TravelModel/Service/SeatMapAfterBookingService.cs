using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.TravelModel.Entity;
using LifeOne.Models.TravelModel.DAL;
using LifeOne.Models.TravelModel.API;



namespace LifeOne.Models.TravelModel.Service
{
    public class SeatMapAfterBookingService
    {
        public ResponseModel SeatMapAfterBookingAPIService(RequestModel _obj)
        {            
            try
            {
                string _url = "GetSeatMapAfterBooking";
                ResponseModel _rsponsedetails = TravelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;
                //return DALCommon.CustomErrorhandleForPreaidAndOtherServices(ex.ToString(), "DALSaveTransactionDetails");
            }
        }
    }
}