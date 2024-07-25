using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.TravelModel.Entity;
using LifeOne.Models.TravelModel.DAL;
using LifeOne.Models.TravelModel.API;



namespace LifeOne.Models.TravelModel.Service
{
    public class BookTicketService
    {

        public ResponseModel BookTicketAPIService(RequestModel _obj)
        {
            DALCommon _objDALCommon = new DALCommon();            
            try
            {
                string _url = "BookTicketAPI"; //API Name
                ResponseModel _rsponsedetails = TravelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                FlightAPI.BookTicket _bokingappResponse = DALCommon.CustomDeserializeObjectWithDecryptString<FlightAPI.BookTicket>(_rsponsedetails.body);
                if (_bokingappResponse.ResponseStatus=="1") //success                
                    DABookTicket.SaveBookingRequestAndResponse(_obj, _bokingappResponse);                
                else                
                    DABookTicket.SaveBookingInCaseOfFailure(_obj, _bokingappResponse);  //failure                
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;                
            }
        }
    }
}
