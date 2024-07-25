using LifeOne.Models.TravelModel.Entity;
using LifeOne.Models.TravelModel.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.TravelModel.DAL;
using LifeOne.Models.API;
using LifeOne.Models.API.DAL;

namespace LifeOne.Models.TravelModel.Service
{
    public class BusDetailsServices
    {

        public ResponseModel GetOriginService(RequestModel _obj)
        {
            try
            {
                string _url = "GetOrigin";
                ResponseModel _rsponsedetails = TravelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;
                //return DALCommon.CustomErrorhandleForPreaidAndOtherServices(ex.ToString(), "DALSaveTransactionDetails");
            }
        }
        public ResponseModel GetDestinationService(RequestModel _obj)
        {
            try
            {
                string _url = "GetDestination";
                ResponseModel _rsponsedetails = TravelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;
                //return DALCommon.CustomErrorhandleForPreaidAndOtherServices(ex.ToString(), "DALSaveTransactionDetails");
            }
        }

        public ResponseModel GetSearchService(RequestModel _obj)
        {
            try
            {
                string _url = "GetSearch";
                ResponseModel _rsponsedetails = TravelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;
                //return DALCommon.CustomErrorhandleForPreaidAndOtherServices(ex.ToString(), "DALSaveTransactionDetails");
            }
        }
        public ResponseModel GetSeatMapService(RequestModel _obj)
        {
            try
            {
                string _url = "GetSeatMap";
                ResponseModel _rsponsedetails = TravelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;
                //return DALCommon.CustomErrorhandleForPreaidAndOtherServices(ex.ToString(), "DALSaveTransactionDetails");
            }
        }
        public ResponseModel GetBookService(RequestModel _obj)
        {
            try
            {
                string _url = "GetBook";
                ResponseModel _rsponsedetails = TravelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                BusModel.GetBookingResponse _bokingappResponse = DALCommon.CustomDeserializeObjectWithDecryptString<BusModel.GetBookingResponse>(_rsponsedetails.body);
                if (_bokingappResponse.ResponseStatus == "1") //success                
                    DABusBooking.SaveBusBookingRequestAndResponse(_obj, _bokingappResponse);
                else
                    DABusBooking.SaveBusBookingRequestAndResponse(_obj, _bokingappResponse);  //failure                
                return _rsponsedetails;
               
            }
            catch (Exception ex)
            {
                throw ex;
                //return DALCommon.CustomErrorhandleForPreaidAndOtherServices(ex.ToString(), "DALSaveTransactionDetails");
            }
        }
        public ResponseModel GetBusTransactionStatusService(RequestModel _obj)
        {
            try
            {
                string _url = "GetBusTransactionStatus";
                ResponseModel _rsponsedetails = TravelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;
                //return DALCommon.CustomErrorhandleForPreaidAndOtherServices(ex.ToString(), "DALSaveTransactionDetails");
            }
        }
        public ResponseModel GetReprintService(RequestModel _obj)
        {
            try
            {
                string _url = "GetReprint";
                ResponseModel _rsponsedetails = TravelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;
                //return DALCommon.CustomErrorhandleForPreaidAndOtherServices(ex.ToString(), "DALSaveTransactionDetails");
            }
        }
        public ResponseModel GetCancellationPolicyService(RequestModel _obj)
        {
            try
            {
                string _url = "GetCancellationPolicy";
                ResponseModel _rsponsedetails = TravelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;
                //return DALCommon.CustomErrorhandleForPreaidAndOtherServices(ex.ToString(), "DALSaveTransactionDetails");
            }
        }
        public ResponseModel GetCancellationPenaltyService(RequestModel _obj)
        {
            try
            {
                string _url = "GetCancellationPenalty";
                ResponseModel _rsponsedetails = TravelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;
                //return DALCommon.CustomErrorhandleForPreaidAndOtherServices(ex.ToString(), "DALSaveTransactionDetails");
            }
        }
        public ResponseModel GetCancellationService(RequestModel _obj)
        {
            try
            {
                string _url = "GetCancellation";
                ResponseModel _rsponsedetails = TravelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;
                //return DALCommon.CustomErrorhandleForPreaidAndOtherServices(ex.ToString(), "DALSaveTransactionDetails");
            }
        }
        public ResponseModel GetBookedHistoryService(RequestModel _obj)
        {
            try
            {
                string _url = "GetBookedHistory";
                BusModel.GetBookedHistoryRequest obj = new BusModel.GetBookedHistoryRequest();
                ResponseModel _rsponsedetails = TravelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                List<History.BookedTicket> list = new List<History.BookedTicket>();
                History.BusBookingHistory datalist = null;
                History.BookedHistoryOutput objdata = new History.BookedHistoryOutput();
                CustomerDb db = new CustomerDb();
                list = db.GetBusHistory(obj);
                if (list != null && list.Count > 0)
                {
                    objdata.BookedTickets = list;
                    datalist = new History.BusBookingHistory();
                    datalist.ResponseStatus = "1";
                    datalist.BookedHistoryOutput = objdata;
                }
                else
                {
                    datalist = new History.BusBookingHistory();
                    datalist.ResponseStatus = "0";
                    datalist.BookedHistoryOutput = null;
                    datalist.FailureRemarks = "Data not available";
                }
                ResponseModel __rsponsedetails = new ResponseModel();
                __rsponsedetails.body = DALCommon.CustomSerializeObjectWithEncryptString<History.BusBookingHistory>(datalist);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;
                //return DALCommon.CustomErrorhandleForPreaidAndOtherServices(ex.ToString(), "DALSaveTransactionDetails");
            }
        }
        public ResponseModel GetBusAccountStatementService(RequestModel _obj)
        {
            try
            {
                string _url = "GetBusAccountStatement";
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