using LifeOne.Models.Common;
using LifeOne.Models.TBOHotelAPI.API;
using LifeOne.Models.TBOHotelAPI.DAL;
using LifeOne.Models.TBOHotelAPI.Entity;
using LifeOne.Models.TravelModel.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LifeOne.Models.TBOHotelAPI.Service
{
    public class TBOHotelBookingService
    {

        public ResponseModel GetAuthenticateUserService(RequestModel _obj)
        {
        
            try
            {
                string _url = "AuthenticateUser";
                ResponseModel _rsponsedetails = TBOHotelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;
              
            }
        }

        public ResponseModel GetCountryListService(RequestModel _obj)
        {

            try
            {
                string _url = "CountryList";
                ResponseModel _rsponsedetails = TBOHotelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ResponseModel GetHotelListService(RequestModel _obj)
        {

            try
            {
                string _url = "GetDestiantionList";
                ResponseModel _rsponsedetails = TBOHotelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ResponseModel GetSerachHotelService(RequestModel _obj)
        {

            try
            {
                string _url = "SerachHotel";
                ResponseModel _rsponsedetails = TBOHotelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ResponseModel GetHotelInfoService(RequestModel _obj)
        {

            try
            {
                string _url = "HotelInfo";
                ResponseModel _rsponsedetails = TBOHotelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ResponseModel GetHotelRoomService(RequestModel _obj)
        {

            try
            {
                string _url = "GetHotelRoom";
                ResponseModel _rsponsedetails = TBOHotelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ResponseModel GetBlockRoomService(RequestModel _obj)
        {

            try
            {
                string _url = "GetBlockRoom";
                ResponseModel _rsponsedetails = TBOHotelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ResponseModel HotelBookService(RequestModel _obj)
        {
            
            try
            {
                string _url = "HotelBook";
                MHotelBookReq _passRequest = DALCommon.CustomDeserializeObjectWithDecryptString<MHotelBookReq>(_obj.body);
                string _finalJsonRequest = DALCommon.SerializeObjectWithoutEncryption(_passRequest);
                ResponseModel _rsponsedetails = TBOHotelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                MBookResponse _bokingappResponse = DALCommon.CustomDeserializeObjectWithDecryptString<MBookResponse>(_rsponsedetails.body);
                DataTable PassengerDetails = DALCommon.ToDataTable(_passRequest.HotelRoomsDetails[0].HotelPassenger);
                if (_bokingappResponse.BookResult.Error.ErrorCode == 0) //success                
                    DALHotelBooking.SaveHotelBookingRequest(_passRequest, _bokingappResponse, _finalJsonRequest, PassengerDetails);
                else
                    DALHotelBooking.SaveHotelBookingRequest(_passRequest, _bokingappResponse, _finalJsonRequest, PassengerDetails);  //failure                
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ResponseModel GetBookingDetailService(RequestModel _obj)
        {
            try
            {
                string _url = "GetBookingDetail";
                ResponseModel _rsponsedetails = TBOHotelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ResponseModel SendChangeRequestService(RequestModel _obj)
        {
            try
            {
                string _url = "SendChangeRequest";
                ResponseModel _rsponsedetails = TBOHotelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ResponseModel SendChangeRequestStatusService(RequestModel _obj)
        {
            try
            {
                string _url = "SendChangeRequestStatus";
                ResponseModel _rsponsedetails = TBOHotelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public ResponseModel GetBookingHotelListService(RequestModel _obj)
        {
            try
            {
                string _url = "GetBookingHotelList";
                ResponseModel _rsponsedetails = TBOHotelGenericAPI.sendRequestToUtilityAPIPostFlight(_obj, _url);
                return _rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}