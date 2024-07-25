using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.TravelModel
{
    public class BusModel
    {
        #region Bus API Parameters Class

        #region Get Origin

        public class Authentication
        {

            public string LoginId { get; set; }

            public string Password { get; set; }



        }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 


        public class GetOriginRequest
        {
            public Authentication Authentication { get; set; }
        }


        public class GetOriginsOutput
        {
            public List<GetOriginList> OriginCities { get; set; }

        }

        public class GetOriginList
        {
            public string OriginId { get; set; }
            public string OriginName { get; set; }
        }

        public class GetOriginResponse
        {
            public string ResponseStatus { get; set; }
            public string FailureResponse { get; set; }
            public GetOriginsOutput OriginOutput { get; set; }
        }
        #endregion

        #region Get Destination
        public class GetDestinationOutput
        {
            public List<GetDestinationList> DestinationCities { get; set; }

        }

        public class GetDestinationList
        {
            public string DestinationId { get; set; }
            public string DestinationName { get; set; }
        }

        public class GetDestinationResponse
        {
            public string ResponseStatus { get; set; }
            public string FailureResponse { get; set; }
            public GetDestinationOutput DestinationOutput { get; set; }
        }

        public class DestinationInput
        {
            public string OriginId { get; set; }
        }
        public class DestinationRequest
        {
            public Authentication Authentication { get; set; }
            public DestinationInput DestinationInput { get; set; }
        }

        #endregion

        #region GetSearch

        public class SearchInput
        {
            public int OriginId { get; set; }
            public int DestinationId { get; set; }
            public string TravelDate { get; set; }
        }

        public class GetSearchRequest
        {
            public Authentication Authentication { get; set; }
            public SearchInput SearchInput { get; set; }

        }

        public class GetSearchResponse
        {
            public string ResponseStatus { get; set; }
            public string FailureRemarks { get; set; }
            public string UserTrackId { get; set; }
            public SearchOutput SearchOutput { get; set; }
        }
        public class SearchOutput
        {
            public List<AvailableBuses> AvailableBuses { get; set; }
        }
        public class AvailableBuses
        {
            public string ScheduleId { get; set; }
            public string StationId { get; set; }
            public string BusType { get; set; }
            public string BusName { get; set; }
            public string TransportId { get; set; }
            public string TransportName { get; set; }
            public List<Fares> Fares { get; set; }
            public string DepartureTime { get; set; }
            public string ArrivalTime { get; set; }
            public string AvailableSeatCount { get; set; }
            public List<BoardingPoints> BoardingPoints { get; set; }
            public List<DroppingPoints> DroppingPoints { get; set; }
            public string Commission { get; set; }
            public string TripId { get; set; }
            public string Amenities { get; set; }


        }
        public class Fares
        {
            public string SeatTypeId { get; set; }
            public string SeatType { get; set; }
            public string Fare { get; set; }
            public string ServiceTax { get; set; }
        }
        public class BoardingPoints
        {
            public string BoardingId { get; set; }
            public string Place { get; set; }
            public string Time { get; set; }
            public string Address { get; set; }
            public string LandMark { get; set; }
            public string ContactNumber { get; set; }
        }
        public class DroppingPoints
        {
            public string DroppingId { get; set; }
            public string Place { get; set; }
            public string Time { get; set; }
            public string Address { get; set; }
            public string LandMark { get; set; }
            public string ContactNumber { get; set; }
        }

        #endregion

        #region GetSeatMap
        public class GetSeatMapRequest
        {
            public Authentication Authentication { get; set; }
            public string UserTrackId { get; set; }
            public SeatMapInput SeatMapInput { get; set; }
        }
        public class SeatMapInput
        {
            public string ScheduleId { get; set; }
            public string StationId { get; set; }
            public string TransportId { get; set; }
            public string TravelDate { get; set; }
        }

        public class GetSeatMapResponse
        {
            public string ResponseStatus { get; set; }
            // public string FailureRemarks { get; set; }
            public string UserTrackId { get; set; }
            public SeatMapOutput SeatMapOutput { get; set; }
        }

        public class SeatMapOutput
        {
            public List<BoardingPoints> BoardingPoints { get; set; }
            public List<DroppingPoints> DroppingPoints { get; set; }
            public List<SeatFares> Fares { get; set; }
            public SeatLayoutDetails SeatLayoutDetails { get; set; }
            public string BusNumber { get; set; }
        }

        public class SeatFares
        {
            public string SeatTypeId { get; set; }
            public string SeatType { get; set; }
            public string Fare { get; set; }
            public string ServiceTax { get; set; }
            public string ConvenienceFee { get; set; }
        }

        public class SeatLayoutDetails
        {
            public string LayoutFlag { get; set; }
            public SeatLayout SeatLayout { get; set; }
            public List<BookedSeats> BookedSeats { get; set; }
            public List<AvailableSeats> AvailableSeats { get; set; }
        }
        public class SeatLayout
        {
            public List<SeatColumns> SeatColumns { get; set; }
        }
        public class BookedSeats
        {
            public string SeatNo { get; set; }

            public string Gender { get; set; }
        }
        public class AvailableSeats
        {
            public string SeatNo { get; set; }
        }

        public class SeatColumns
        {
            public List<Seats> Seats { get; set; }
        }
        public class Seats
        {
            public string SeatNo { get; set; }
            public string SeatTypeId { get; set; }
            public string BerthType { get; set; }
            public string SeatType { get; set; }
            public string Fare { get; set; }
            public string ServiceTax { get; set; }
            public string ConvenienceFee { get; set; }
        }

        #endregion

        #region Get Book 

        public class GetBookRequest
        {
            public string UserTrackId { get; set; }
            public string FK_MemID { get; set; }
            public string CompanyId { get; set; }
            public string TransType { get; set; }
            public Authentication Authentication { get; set; }
            public string BookingId { get; set; }
            public BookingInput BookingInput { get; set; }

        }
        public class BookingInput
        {
            public BookingBusCustomerDetails BookingCustomerDetails { get; set; }
            public BusBookingDetails BookingDetails { get; set; }
        }
        public class BookingBusCustomerDetails
        {
            public string Title { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string ContactNumber { get; set; }
            public string City { get; set; }
            public string CountryId { get; set; }
            public string EmailId { get; set; }
            public string IdProofId { get; set; }
            public string IdProofNumber { get; set; }

        }
        public class BusBookingDetails
        {
            public string TotalTickets { get; set; }
            public string TotalAmount { get; set; }
            public string TransportId { get; set; }
            public string ScheduleId { get; set; }
            public string StationId { get; set; }
            public string TravelDate { get; set; }
            public List<BusPassengerList> PassengerList { get; set; }
        }
        public class BusPassengerList
        {
            public string Gender { get; set; }
            public string Age { get; set; }
            public string SeatNo { get; set; }
            public string SeatTypeId { get; set; }
            public string Fare { get; set; }
            public string BoardingId { get; set; }
            public string DroppingId { get; set; }
            public string PassengerName { get; set; }
        }

        public class GetBookingResponse
        {
            public string ResponseStatus { get; set; }
            public string UserTrackId { get; set; }
            public string FailureRemarks { get; set; }
            public BookingOutput BookingOutput { get; set; }
        }
        public class BookingOutput
        {
            public TicketDetails TicketDetails { get; set; }
        }
        public class TicketDetails
        {
            public string ServiceTax { get; set; }
            public string Commission { get; set; }
            public string CancellationPolicy { get; set; }
            public string ConvenienceFee { get; set; }
            public BusBookingCustomerDetails BookingCustomerDetails { get; set; }
            public PNRDetails PNRDetails { get; set; }
        }
        public class BusBookingCustomerDetails
        {
            public string Title { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string ContactNumber { get; set; }
            public string City { get; set; }
            public string CountryId { get; set; }
            public string EmailId { get; set; }
            public string IdProofId { get; set; }
            public string IdProofNumber { get; set; }
            public string BookedDate { get; set; }
        }
        public class PNRDetails
        {
            public string TransactionId { get; set; }
            public string TransportPNR { get; set; }
            public string TotalAmount { get; set; }
            public string TotalTickets { get; set; }
            public string BusName { get; set; }
            public string Origin { get; set; }
            public string Destination { get; set; }
            public string TravelDate { get; set; }
            public string DepartureTime { get; set; }
            public List<PaxList> PaxList { get; set; }
            public TransportDetails TransportDetails { get; set; }
        }
        public class PaxList
        {
            public string TicketNumber { get; set; }
            public string SeatNo { get; set; }
            public string SeatType { get; set; }
            public string PassengerName { get; set; }
            public string Gender { get; set; }
            public string Age { get; set; }
            public string Fare { get; set; }
            public BoardingPoints BoardingPoints { get; set; }
            public DroppingPoints DroppingPoints { get; set; }
            public string ReportingTime { get; set; }
            public string ServiceCharges { get; set; }
        }
        public class TransportDetails
        {
            public string TransportName { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Address3 { get; set; }
            public string CityNamePinCode { get; set; }
            public string ContactNumber { get; set; }
            public string Fax { get; set; }
            public string Website { get; set; }
            public string EmailId { get; set; }
        }


        #endregion

        #region GetTransactionStatus
        public class GetTransactionStatusRequest
        {
            public Authentication Authentication { get; set; }
            public string UserTrackId { get; set; }
        }
        public class GetTransactionStatusResponse
        {
            public string ResponseStatus { get; set; }
            public TransactionStatusOutput TransactionStatusOutput { get; set; }
        }
        public class TransactionStatusOutput
        {
            public string TransactionStatus { get; set; }
            public TicketDetails TicketDetails { get; set; }
            public string Remarks { get; set; }
        }


        #endregion

        #region GetReprint
        public class GetReprintRequest
        {
            public Authentication Authentication { get; set; }
            public ReprintInput ReprintInput { get; set; }
        }
        public class ReprintInput
        {
            public string TransactionId { get; set; }
        }
        public class GetReprintResponse
        {
            public string ResponseStatus { get; set; }
            public string UserTrackId { get; set; }
            public ReprintOutput ReprintOutput { get; set; }
            public string FailureRemarks { get; set; }
        }
        public class ReprintOutput
        {
            public ReprintTicketDetails ReprintTicketDetails { get; set; }
        }
        public class ReprintTicketDetails
        {
            public BusBookingCustomerDetails BookingCustomerDetails { get; set; }
            public ReprintPNRDetails ReprintPNRDetails { get; set; }
            public string ServiceTax { get; set; }
            public string Commission { get; set; }
            public string CancellationPolicy { get; set; }
        }
        public class ReprintPNRDetails
        {
            public string TransactionId { get; set; }
            public string TransportPNR { get; set; }
            public string TotalAmount { get; set; }
            public string TotalTickets { get; set; }
            public string BusName { get; set; }
            public string Origin { get; set; }
            public string Destination { get; set; }
            public string TravelDate { get; set; }
            public string DepartureTime { get; set; }
            public List<ReprintPaxList> ReprintPaxList { get; set; }
            public TransportDetails TransportDetails { get; set; }
        }
        public class ReprintPaxList
        {
            public string TicketNumber { get; set; }
            public string TicketStatus { get; set; }
            public string SeatNo { get; set; }
            public string SeatType { get; set; }
            public string PassengerName { get; set; }
            public string Gender { get; set; }
            public string Age { get; set; }
            public string Fare { get; set; }
            public BoardingPoints BoardingPoints { get; set; }
            public DroppingPoints DroppingPoints { get; set; }
            public string ReportingTime { get; set; }
            public string ServiceCharges { get; set; }
        }
        #endregion

        #region  GetCancellationPolicy
        public class GetCancellationPolicyRequest
        {
            public Authentication Authentication { get; set; }
            public string UserTrackId { get; set; }
            public CancellationPolicyInput CancellationPolicyInput { get; set; }
        }
        public class CancellationPolicyInput
        {
            public string TransportId { get; set; }
            public string TravelDate { get; set; }
            public string ScheduleId { get; set; }
            public string TripId { get; set; }
        }

        public class GetCancellationPolicyResponse
        {
            public string ResponseStatus { get; set; }
            public string FailureRemarks { get; set; }
            public CancellationPolicyOutput CancellationPolicyOutput { get; set; }
        }
        public class CancellationPolicyOutput
        {
            public string CancelPolicyDetails { get; set; }
        }
        #endregion

        #region GetCancellationPenalty 
        public class GetCancellationPenaltyRequest
        {
            public Authentication Authentication { get; set; }
            public CancellationPenaltyInput CancellationPenaltyInput { get; set; }
        }
        public class CancellationPenaltyInput
        {
            public string TransactionId { get; set; }
        }

        public class GetCancellationPenaltyResponse
        {
            public string ResponseStatus { get; set; }
            public string FailureRemarks { get; set; }
            public CancellationPenaltyOutput CancellationPenaltyOutput { get; set; }
        }
        public class CancellationPenaltyOutput
        {
            public ToCancelPNRDetails ToCancelPNRDetails { get; set; }
        }
        public class ToCancelPNRDetails
        {
            public string TransportName { get; set; }
            public string Origin { get; set; }
            public string Destination { get; set; }
            public string BookedDate { get; set; }
            public string TravelDate { get; set; }
            public string DepatureTime { get; set; }
            public string PNRStatus { get; set; }
            public List<ToCancelPaxList> ToCancelPaxList { get; set; }
        }
        public class ToCancelPaxList
        {
            public string TicketNumber { get; set; }
            public string PassengerName { get; set; }
            public string Gender { get; set; }
            public string Age { get; set; }
            public string SeatNo { get; set; }
            public string Fare { get; set; }
            public string PenatlyAmount { get; set; }
        }
        #endregion

        #region GetCancellation
        public class GetCancellationRequest
        {
            public string FK_MemID { get; set; }
            public string CompanyId { get; set; }
            public string TransType { get; set; }
            public string BookingId { get; set; }
            public Authentication Authentication { get; set; }
            public CancellationInput CancellationInput { get; set; }
        }
        public class CancellationInput
        {
            public string TransactionId { get; set; }
            public string TotalTicketsToCancel { get; set; }
            public string PenaltyAmount { get; set; }
            public string TicketNumbers { get; set; }
            public string TransportId { get; set; }
        }
        public class GetCancellationResponse
        {
            public string ResponseStatus { get; set; }
            public string FailureRemarks { get; set; }
            public CancellationOutput CancellationOutput { get; set; }
        }
        public class CancellationOutput
        {
            public CanceledTicketDetails CanceledTicketDetails { get; set; }
        }
        public class CanceledTicketDetails
        {
            public BusBookingCustomerDetails BookingCustomerDetails { get; set; }
            public CanceledPNRDetails CanceledPNRDetails { get; set; }
        }
        public class CanceledPNRDetails
        {
            public string TransactionId { get; set; }
            public string TransportPNR { get; set; }
            public string TotalAmount { get; set; }
            public string TotalTickets { get; set; }
            public string BusName { get; set; }
            public string Origin { get; set; }
            public string Destination { get; set; }
            public string TravelDate { get; set; }
            public string DepartureTime { get; set; }
            public string CancellationPenalty { get; set; }
            public string CancellationNumber { get; set; }
            public string CancelledDateTime { get; set; }
            public List<CancelPaxList> CancelPaxList { get; set; }
            public TransportDetails TransportDetails { get; set; }
        }

        public class CancelPaxList
        {
            public string TicketNumber { get; set; }
            public string TicketStatus { get; set; }
            public string SeatNo { get; set; }
            public string SeatType { get; set; }
            public string PassengerName { get; set; }
            public string Gender { get; set; }
            public string Age { get; set; }
            public string Fare { get; set; }
            public BoardingPoints BoardingPoints { get; set; }
            public DroppingPoints DroppingPoints { get; set; }
            public string ReportingTime { get; set; }
            public string ServiceCharges { get; set; }
        }
        #endregion

        #region GetBookedHistory
        public class GetBookedHistoryRequest
        {
            public Authentication Authentication { get; set; }
            public BookedHistoryInput BookedHistoryInput { get; set; }
        }
        public class BookedHistoryInput
        {
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string RecordsBy { get; set; }
            public long FK_memId { get; set; }
        }

        public class GetBookedHistoryResponse
        {
            public string ResponseStatus { get; set; }
            public string FailureRemarks { get; set; }
            public BookedHistoryOutput BookedHistoryOutput { get; set; }
        }
        public class BookedHistoryOutput
        {
            public List<BookedTickets> BookedTickets { get; set; }
        }
        public class BookedTickets
        {
            public string TransportId { get; set; }
            public string TransportName { get; set; }
            public string TransactionId { get; set; }
            public string BookedDateTime { get; set; }
            public string Origin { get; set; }
            public string Destination { get; set; }
            public string TravelDate { get; set; }
            public string TotalTickets { get; set; }
            public string Amount { get; set; }
            public string PenaltyAmount { get; set; }
            public string TicketStatus { get; set; }
        }
        #endregion

        #region GetAccountStatement
        public class GetAccountStatementRequest
        {
            public Authentication Authentication { get; set; }
            public AccountStatementInput AccountStatementInput { get; set; }
        }
        public class AccountStatementInput
        {
            public string FromDate { get; set; }
            public string ToDate { get; set; }
        }

        public class GetAccountStatementResponse
        {
            public string ResponseStatus { get; set; }
            public string FailureRemarks { get; set; }
            public AccountStatementOutput AccountStatementOutput { get; set; }
        }
        public class AccountStatementOutput
        {
            public string TravelAgentId { get; set; }
            public string TravelAgentName { get; set; }
            public string TotalRemainingAmount { get; set; }
            public List<Transactions> Transactions { get; set; }
        }
        public class Transactions
        {
            public string DateTime { get; set; }
            public string Description { get; set; }
            public string TransactionId { get; set; }
            public string CreditAmount { get; set; }
            public string DebitAmount { get; set; }
            public string RemainingAmount { get; set; }
            public string Remarks { get; set; }
            public string TerminalName { get; set; }
            public string ProductDescription { get; set; }
        }

        #endregion

        public class GetBookingHistorydetail
        {
            public string FromDate { get; set; }

            public string ToDate { get; set; }

            public string FK_MemId { get; set; }


            public string Remarks { get; set; }

            public string SeatNo { get; set; }

            public string SeatAmt { get; set; }

            public string SeatCode { get; set; }

            public string PassengerId { get; set; }

            public string Pk_FlightResponseId { get; set; }

            public string HermesPNR { get; set; }

            public string AirlineName { get; set; }

            public string AirlinePNR { get; set; }

            public string BookingType { get; set; }

            public string Origin { get; set; }

            public string Destination { get; set; }

            public string BoookingDate { get; set; }

            public string AirlineCode { get; set; }

            public string TerminalName { get; set; }

            public string TotalAMount { get; set; }

            public string ButtonDisplay { get; set; }

            public string BookSeatButton { get; set; }

            public string DepartingDate { get; set; }

            public string FlightId { get; set; }

            public string UserTrackId { get; set; }


            public List<GetBookingHistorydetail> historydetails { get; set; }

        }

        public class BookingStatus
        {

            public string Response { get; set; }

            public List<GetBookingHistorydetail> GetBookingHistorydetail { get; set; }
        }

        //public static string AddAuthJsonAndHItTheAPI(string _jsonRequest)
        //{
        //    try
        //    {
        //        Authentication Authentication = new Authentication();
        //        Authentication.LoginId = "hermes";
        //        Authentication.Password = "hermes123";
        //        GetOriginRequest root = new GetOriginRequest();
        //        root.Authentication = Authentication;
        //        string _commonJson = Newtonsoft.Json.JsonConvert.SerializeObject(root);


        //        string arrayOfObjects = JsonConvert.SerializeObject(
        //            JsonConvert.DeserializeObject(_commonJson)

        //        );

        //        string arrayOfObjects1 = JsonConvert.SerializeObject(
        //           JsonConvert.DeserializeObject(_commonJson)
        //       );
        //        string final = arrayOfObjects + arrayOfObjects1;

        //        return arrayOfObjects;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


        #endregion
    }
}