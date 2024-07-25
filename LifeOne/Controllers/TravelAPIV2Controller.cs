using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LifeOne.Models.TravelModel.Entity;
using LifeOne.Models.API.DAL;
using LifeOne.Models.TravelModel;
using LifeOne.Models.TravelModel.Service;
using System.Data;

namespace LifeOne.Controllers
{
    public class TravelAPIV2Controller : ApiController
    {

        #region Flight API
        #region Get Flight Availability 
        [HttpPost]
        public ResponseModel GetFlightDetailsAPI(RequestModel model)
        {
            try
            {
                FlightDetailsService _objprepaidService = new FlightDetailsService();
                ResponseModel _result = _objprepaidService.GetFlightDetailService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel GetTaxAPI(RequestModel model)
        {
            try
            {
                FlightDetailsService _objprepaidService = new FlightDetailsService();
                ResponseModel _result = _objprepaidService.GetTaxService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel FareRulesAPI(RequestModel model)
        {
            try
            {
                FareRulesService _objprepaidService = new FareRulesService();
                ResponseModel _result = _objprepaidService.FareRulesAPIService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel BookTicketAPI(RequestModel model)
        {
            try
            {
                BookTicketService _objBookTicketAPI = new BookTicketService();
                ResponseModel _result = _objBookTicketAPI.BookTicketAPIService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel GetTransactionStatus(RequestModel model)
        {
            try
            {
                TransactionStatusService _objTransactionStatus = new TransactionStatusService();
                ResponseModel _result = _objTransactionStatus.TransactionStatusAPIService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel GetReturnTax(RequestModel model)
        {
            try
            {
                ReturnTaxService _objTransactionStatus = new ReturnTaxService();
                ResponseModel _result = _objTransactionStatus.ReturnTaxAPIService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel CancelTicket(RequestModel model)
        {
            try
            {
                CancelTicketService _objTransactionStatus = new CancelTicketService();
                ResponseModel _result = _objTransactionStatus.CancelTicketAPIService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public ResponseModel GetSeatMapAfterBooking(RequestModel model)
        {
            try
            {
                SeatMapAfterBookingService _objTransactionStatus = new SeatMapAfterBookingService();
                ResponseModel _result = _objTransactionStatus.SeatMapAfterBookingAPIService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public ResponseModel SeatBook(RequestModel model)
        {
            try
            {
                SeatBookService _objTransactionStatus = new SeatBookService();
                ResponseModel _result = _objTransactionStatus.SeatBookAPIService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel GetAgentCrediitBallance(RequestModel model)
        {
            try
            {
                AgentCrediitBallanceService _objTransactionStatus = new AgentCrediitBallanceService();
                ResponseModel _result = _objTransactionStatus.AgentCrediitBallanceAPIService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel GetPartialCancellation(RequestModel model)
        {
            try
            {
                PartialCancellationService _objTransactionStatus = new PartialCancellationService();
                ResponseModel _result = _objTransactionStatus.PartialCancellationAPIService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public ResponseModel GetFlightReprint(RequestModel model)
        {
            try
            {
                PartialCancellationService _objTransactionStatus = new PartialCancellationService();
                ResponseModel _result = _objTransactionStatus.GetFlightReprint(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public APIResponse1 GetWalletBalance(GetWalletAount objGetWalletAount)
        {
            APIResponse1 objresponse = new APIResponse1();
            DataSet ds = objGetWalletAount.GetAmount();
            try
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    objresponse.Balance = ds.Tables[0].Rows[0]["WalletBalance"].ToString();
                }
                else if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    objresponse.Balance = "Insufficient Balance";
                }
                else
                {
                    objresponse.Balance = "Error";
                }
            }
            catch (Exception ex)
            {
                objresponse.Balance = "Error";
            }
            return objresponse;
            //return Json(objresponse.Response,JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ResponseModel GetBookingHistory(RequestModel model)
        {
            try
            {
                BookingHistoryService _objTransactionStatus = new BookingHistoryService();
                ResponseModel _result = _objTransactionStatus.BookingHistoryAPIService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel GetCancellationPolicyInput(string UserTrackId)
        {
            try
            {
                CancellationPolicyInputService _objTransactionStatus = new CancellationPolicyInputService();
                ResponseModel _result = _objTransactionStatus.CancellationPolicyInputAPIService(UserTrackId);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
        #endregion

        #region  BusApI
        [HttpPost]
        public ResponseModel GetOrigin(RequestModel model)
        {
            try
            {
                BusDetailsServices _objGetOrigin = new BusDetailsServices();
                ResponseModel _result = _objGetOrigin.GetOriginService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel GetDestination(RequestModel model)
        {
            try
            {
                BusDetailsServices _objGetOrigin = new BusDetailsServices();
                ResponseModel _result = _objGetOrigin.GetDestinationService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel GetSearch(RequestModel model)
        {
            try
            {
                BusDetailsServices _objGetOrigin = new BusDetailsServices();
                ResponseModel _result = _objGetOrigin.GetSearchService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ResponseModel GetSeatMap(RequestModel model)
        {
            try
            {
                BusDetailsServices _objGetOrigin = new BusDetailsServices();
                ResponseModel _result = _objGetOrigin.GetSeatMapService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ResponseModel GetBook(RequestModel model)
        {
            try
            {
                BusDetailsServices _objGetOrigin = new BusDetailsServices();
                ResponseModel _result = _objGetOrigin.GetBookService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ResponseModel GetBusTransactionStatus(RequestModel model)
        {
            try
            {
                BusDetailsServices _objGetOrigin = new BusDetailsServices();
                ResponseModel _result = _objGetOrigin.GetBusTransactionStatusService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ResponseModel GetReprint(RequestModel model)
        {
            try
            {
                BusDetailsServices _objGetOrigin = new BusDetailsServices();
                ResponseModel _result = _objGetOrigin.GetReprintService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ResponseModel GetCancellation(RequestModel model)
        {
            try
            {
                BusDetailsServices _objGetOrigin = new BusDetailsServices();
                ResponseModel _result = _objGetOrigin.GetCancellationService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel GetBookedHistory(RequestModel model)
        {
            try
            {
                BusDetailsServices _objGetOrigin = new BusDetailsServices();
                ResponseModel _result = _objGetOrigin.GetBookedHistoryService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel GetBusAccountStatement(RequestModel model)
        {
            try
            {
                BusDetailsServices _objGetOrigin = new BusDetailsServices();
                ResponseModel _result = _objGetOrigin.GetBusAccountStatementService(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel DownloadTicket(RequestModel model)
        {
            try
            {
                FlightDetailsService _objprepaidService = new FlightDetailsService();
                ResponseModel _result = _objprepaidService.GetDownloadTicket(model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
