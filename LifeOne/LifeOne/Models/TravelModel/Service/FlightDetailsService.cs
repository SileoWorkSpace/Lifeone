using LifeOne.Models.TravelModel.Entity;
using LifeOne.Models.TravelModel.DAL;
using LifeOne.Models.TravelModel.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.TravelModel.Service
{
    public class FlightDetailsService
    {

        public ResponseModel GetFlightDetailService(RequestModel _obj)
        {
            DALCommon _objDALCommon = new DALCommon();
            try
            {
                string _url = "GetFlightDetailsAPI";
                ResponseModel _rsponsedetails = TravelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;
                //return DALCommon.CustomErrorhandleForPreaidAndOtherServices(ex.ToString(), "DALSaveTransactionDetails");
            }
        }

        public ResponseModel GetTaxService(RequestModel _obj)
        {
            DALCommon _objDALCommon = new DALCommon();
            try
            {
                string _url = "GetTaxAPI";
                ResponseModel _rsponsedetails = TravelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;
                //return DALCommon.CustomErrorhandleForPreaidAndOtherServices(ex.ToString(), "DALSaveTransactionDetails");
            }
        }

        public ResponseModel GetDownloadTicket(RequestModel _obj)
        {
            DALCommon _objDALCommon = new DALCommon();
            try
            {
                string _url = "DownloadTicket";
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