using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.TravelModel.Entity;
using LifeOne.Models.TravelModel.DAL;
using LifeOne.Models.TravelModel.API;




namespace LifeOne.Models.TravelModel.Service
{
    public class CancelTicketService
    {

        public ResponseModel CancelTicketAPIService(RequestModel _obj)
        {
            DALCommon _objDALCommon = new DALCommon();
            try
            {
                string _url = "CancelTicket"; //API Name
                ResponseModel _rsponsedetails = TravelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                FlightAPI.CancelTicketReponse _cancelTicketReponse = DALCommon.CustomDeserializeObjectWithDecryptString<FlightAPI.CancelTicketReponse>(_rsponsedetails.body);
                DALCancelTicket.SaveCancelRequestAndResponse(_obj, _cancelTicketReponse);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}