using LifeOne.Models.TBOHotelAPI.Service;
using LifeOne.Models.TravelModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LifeOne.Controllers
{
    public class TBOHotelBookingController : ApiController
    {
        [HttpPost]
        public ResponseModel AuthenticateUser(RequestModel model)
        {
            try
            {
                TBOHotelBookingService _objHotelBookingService = new TBOHotelBookingService();
                ResponseModel _result = _objHotelBookingService.GetAuthenticateUserService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel CountryList(RequestModel model)
        {
            try
            {
                TBOHotelBookingService _objHotelBookingService = new TBOHotelBookingService();
                ResponseModel _result = _objHotelBookingService.GetCountryListService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ResponseModel GetHotelList(RequestModel model)
        {
            try
            {
                TBOHotelBookingService _objHotelBookingService = new TBOHotelBookingService();
                ResponseModel _result = _objHotelBookingService.GetHotelListService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public ResponseModel SerachHotel(RequestModel model)
        {
            try
            {
                TBOHotelBookingService _objHotelBookingService = new TBOHotelBookingService();
                ResponseModel _result = _objHotelBookingService.GetSerachHotelService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel HotelInfo(RequestModel model)
        {
            try
            {
                TBOHotelBookingService _objHotelBookingService = new TBOHotelBookingService();
                ResponseModel _result = _objHotelBookingService.GetHotelInfoService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel GetHotelRoom(RequestModel model)
        {
            try
            {
                TBOHotelBookingService _objHotelBookingService = new TBOHotelBookingService();
                ResponseModel _result = _objHotelBookingService.GetHotelRoomService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel GetBlockRoom(RequestModel model)
        {
            try
            {
                TBOHotelBookingService _objHotelBookingService = new TBOHotelBookingService();
                ResponseModel _result = _objHotelBookingService.GetBlockRoomService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel HotelBook(RequestModel model)
        {
            try
            {
                TBOHotelBookingService _objHotelBookingService = new TBOHotelBookingService();
                ResponseModel _result = _objHotelBookingService.HotelBookService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel GetBookingDetail(RequestModel model)
        {
            try
            {
                TBOHotelBookingService _objHotelBookingService = new TBOHotelBookingService();
                ResponseModel _result = _objHotelBookingService.GetBookingDetailService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ResponseModel SendChangeRequest(RequestModel model)
        {
            try
            {
                TBOHotelBookingService _objHotelBookingService = new TBOHotelBookingService();
                ResponseModel _result = _objHotelBookingService.SendChangeRequestService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel SendChangeRequestStatus(RequestModel model)
        {
            try
            {
                TBOHotelBookingService _objHotelBookingService = new TBOHotelBookingService();
                ResponseModel _result = _objHotelBookingService.SendChangeRequestStatusService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ResponseModel GetBookingHotelList(RequestModel model)
        {
            try
            {
                TBOHotelBookingService _objHotelBookingService = new TBOHotelBookingService();
                ResponseModel _result = _objHotelBookingService.GetBookingHotelListService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
