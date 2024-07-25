using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.TravelModel
{
    public class FlightAPI
    {
        #region Flight Details

        public class FlightDetails
        {
            public Authentication Authentication { get; set; }
            public AvailabilityInput AvailabilityInput { get; set; }
        }
        public class AvailabilityInput
        {
            public string BookingType { get; set; }
            public List<JourneyDetail> JourneyDetails { get; set; }
            public string ClassType { get; set; }
            public string AirlineCode { get; set; }
            public int AdultCount { get; set; }
            public int ChildCount { get; set; }
            public int InfantCount { get; set; }
            public int ResidentofIndia { get; set; }
            public string Optional1 { get; set; }
            public string Optional2 { get; set; }
            public string Optional3 { get; set; }
        }
        public class JourneyDetail
        {
            public string Origin { get; set; }
            public string Destination { get; set; }
            public string TravelDate { get; set; }
        }

        public class FlightSearchResponse
        {
            public string ResponseStatus { get; set; }
            public string UserTrackId { get; set; }
            public string FailureRemarks { get; set; }

            public AvailabilityOutput AvailabilityOutput { get; set; }


        }
        public class AvailabilityOutput
        {
            public AvailableFlights AvailableFlights { get; set; }
        }
        public class AvailableFlights
        {
            public List<OngoingFlights> OngoingFlights { get; set; }

            public List<ReturnFlights> ReturnFlights { get; set; }
        }
        public class OngoingFlights
        {
            public List<AvailSegments> AvailSegments { get; set; }
        }
        public class ReturnFlights
        {
            public List<AvailSegments> AvailSegments { get; set; }
        }
        public class AvailSegments
        {
            public string FlightId { get; set; }
            public string AirlineCode { get; set; }
            public string FlightNumber { get; set; }
            public string AirCraftType { get; set; }
            public string Origin { get; set; }
            public string OriginAirportTerminal { get; set; }
            public string Destination { get; set; }
            public string DestinationAirportTerminal { get; set; }
            public string DepartureDateTime { get; set; }
            public string ArrivalDateTime { get; set; }
            public string Duration { get; set; }
            public string NumberofStops { get; set; }
            public string Via { get; set; }
            public string CurrencyCode { get; set; }
            public string Currency_Conversion_Rate { get; set; }
            public List<AvailPaxFareDetails> AvailPaxFareDetails { get; set; }
            public string SupplierId { get; set; }

        }
        public class AvailPaxFareDetails
        {
            public string ClassCode { get; set; }
            public string ClassCodeDesc { get; set; }
            public BaggageAllowed BaggageAllowed { get; set; }
            public Adult Adult { get; set; }
            public Child Child { get; set; }
            public Infant Infant { get; set; }
            public string CancellationCharges { get; set; }
            public string ChangePenalty { get; set; }

        }
        public class BaggageAllowed
        {
            public string CheckInBaggage { get; set; }
            public string HandBaggage { get; set; }
        }
        public class Adult
        {
            public string FareBasis { get; set; }
            public string FareType { get; set; }
            public string BasicAmount { get; set; }
            public string YQ { get; set; }
            public List<TaxDetails> TaxDetails { get; set; }

            public string TotalTaxAmount { get; set; }
            public string GrossAmount { get; set; }
            public string Commission { get; set; }
        }
        public class TaxDetails
        {
            public string Description { get; set; }
            public string Amount { get; set; }

        }
        public class Child
        {
            public string FareBasis { get; set; }
            public string FareType { get; set; }
            public string BasicAmount { get; set; }
            public string YQ { get; set; }
            public List<TaxDetails> TaxDetails { get; set; }

            public string TotalTaxAmount { get; set; }
            public string GrossAmount { get; set; }
            public string Commission { get; set; }
        }

        public class Infant
        {
            public string FareBasis { get; set; }
            public string FareType { get; set; }
            public string BasicAmount { get; set; }
            public string YQ { get; set; }
            public List<TaxDetails> TaxDetails { get; set; }

            public string TotalTaxAmount { get; set; }
            public string GrossAmount { get; set; }
            public string Commission { get; set; }
        }
        #endregion

        #region Get Tax API
        public class GetTaxRequest
        {
            public Authentication Authentication { get; set; }
            public string UserTrackId { get; set; }
            public TaxInput TaxInput { get; set; }
        }
        public class TaxInput
        {
            public List<TaxReqFlightSegment> TaxReqFlightSegments { get; set; }
            public GSTDetails GSTDetails { get; set; }
        }
        public class GSTDetails
        {
            public string GSTNumber { get; set; }
            public string EMailId { get; set; }
            public string CompanyName { get; set; }
            public string ContactNumber { get; set; }
            public string Address { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
        public class TaxReqFlightSegment
        {
            public string FlightId { get; set; }
            public string ClassCode { get; set; }
            public string AirlineCode { get; set; }
            public int ETicketFlag { get; set; }
            public int BasicAmount { get; set; }
            public string SupplierId { get; set; }
        }

        public class TaxResponse
        {
            public string ResponseStatus { get; set; }
            public string UserTrackId { get; set; }
            public TaxOutput TaxOutput { get; set; }
            public string FailureRemarks { get; set; }

        }
        public class TaxOutput
        {
            public string BaggageAndMealsFlag { get; set; }
            public List<TaxResFlightSegments> TaxResFlightSegments { get; set; }
        }
        public class TaxResFlightSegments
        {
            public string FlightId { get; set; }
            public AdultTax AdultTax { get; set; }
            public ChildTax ChildTax { get; set; }
            public InfantTax InfantTax { get; set; }
            public string Baggages { get; set; }
            public string Meals { get; set; }
        }
        public class AdultTax
        {
            public FareBreakUpDetails FareBreakUpDetails { get; set; }
        }
        public class FareBreakUpDetails
        {
            public string BasicCurrencyCode { get; set; }
            public string CurrencyCode { get; set; }
            public string BasicAmount { get; set; }
            public string EquivalentFare { get; set; }
            public List<TaxDetails> TaxDetails { get; set; }
            public string TotalTaxAmount { get; set; }
            public string TransactionFee { get; set; }
            public string ServiceCharge { get; set; }
            public string GrossAmount { get; set; }
            public string Commission { get; set; }
        }
        public class ChildTax
        {
            public FareBreakUpDetails FareBreakUpDetails { get; set; }
        }
        public class InfantTax
        {
            public FareBreakUpDetails FareBreakUpDetails { get; set; }
        }

        #endregion

        #region Fare Rules API
        public class FareRelesRequest
        {
            public Authentication Authentication { get; set; }
            public string UserTrackId { get; set; }
            public FareRuleInput FareRuleInput { get; set; }
        }
        public class FareRuleInput
        {
            public string AirlineCode { get; set; }
            public string FlightId { get; set; }
            public string ClassCode { get; set; }
            public string SupplierId { get; set; }
        }
        public class FareRelesResponse
        {
            public string ResponseStatus { get; set; }
            public string UserTrackId { get; set; }
            public FareRuleOutput FareRuleOutput { get; set; }
        }
        public class FareRuleOutput
        {
            public string FareRules { get; set; }
        }
        #endregion

        #region Book Ticket API
        public class BookTicketAPI
        {
            public Authentication Authentication { get; set; }
            public string UserTrackId { get; set; }
            public string FK_MemID { get; set; }
            public string CompanyId { get; set; }
            public string TransType { get; set; }
            public string BookingId { get; set; }
            public string Origin { get; set; }
            public string Destination { get; set; }
            public string DepartingDate { get; set; }
            public string ReturningDate { get; set; }
            public string Class { get; set; }
            public BookInput BookInput { get; set; }
        }
        public class BookInput
        {
            public CustomerDetails CustomerDetails { get; set; }
            public string SpecialRemarks { get; set; }
            public int NotifyByMail { get; set; }
            public int NotifyBySMS { get; set; }
            public int AdultCount { get; set; }
            public int ChildCount { get; set; }
            public int InfantCount { get; set; }
            public string BookingType { get; set; }
            public decimal TotalAmount { get; set; }
            public object FrequentFlyerRequest { get; set; }
            public object SpecialServiceRequest { get; set; }
            public object FSCMealsRequest { get; set; }
            public List<FlightBookingDetail> FlightBookingDetails { get; set; }
        }
        public class FlightBookingDetail
        {
            public string AirlineCode { get; set; }
            public PaymentDetails PaymentDetails { get; set; }
            public string TourCode { get; set; }
            public List<PassengerDetail> PassengerDetails { get; set; }
        }
        public class FlightBookingDetails
        {
            public string AirlineCode { get; set; }

            public List<PassengerDetail> PassengerDetails { get; set; }

            public PaymentDetails PaymentDetails { get; set; }
            public string TourCode { get; set; }
        }
        public class PassengerDetail
        {
            public string PassengerType { get; set; }
            public string Title { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Gender { get; set; }
            public int Age { get; set; }
            public string DateofBirth { get; set; }
            public string IdentityProofId { get; set; }
            public string IdentityProofNumber { get; set; }
            public List<BookingSegment> BookingSegments { get; set; }
            public object LCCBaggageRequest { get; set; }
            public object LCCMealsRequest { get; set; }
            public object OtherSSRRequest { get; set; }
            public object SeatRequest { get; set; }
        }
        public class BookingSegment
        {
            public string FlightId { get; set; }
            public string ClassCode { get; set; }
            public string SpecialServiceCode { get; set; }
            public string FrequentFlyerId { get; set; }
            public string FrequentFlyerNumber { get; set; }
            public string MealCode { get; set; }
            public string SeatPreferId { get; set; }
            public string SupplierId { get; set; }
        }
        public class BookedSegmentsAPI
        {
            public string TicketNumber { get; set; }
            public string FlightNumber { get; set; }
            public string AirCraftType { get; set; }
            public string Origin { get; set; }
            public string OriginAirport { get; set; }

            public string ClassCode { get; set; }
            public string ClassCodeDesc { get; set; }
            public string FareBasis { get; set; }
            public string BaggageAllowed { get; set; }
            public string DepartureDateTime { get; set; }
            public string Destination { get; set; }
            public string DestinationAirport { get; set; }
            public string Arrivaldatetime { get; set; }
            public string AirlineCode { get; set; }
            public string StopOverAllowed { get; set; }
            public string FrequentFlyerId { get; set; }
            public string FrequentFlyerNumber { get; set; }
            public string SpecialServiceCode { get; set; }
            public string TotalTaxAmount { get; set; }
            public string GrossAmount { get; set; }

            public string BasicAmount { get; set; }
            public string FlightId { get; set; }



            public string MealCode { get; set; }
            public string SeatPreferId { get; set; }
            public string SupplierId { get; set; }

        }
        public class CustomerDetails
        {
            public string Title { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string CountryId { get; set; }
            public string ContactNumber { get; set; }
            public string EmailId { get; set; }
            public string PinCode { get; set; }
        }
        public class AirlineDetails
        {
            public string AirlineCode { get; set; }
            public string AirlinePNR { get; set; }
            public string AirlineName { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string ContactNumber { get; set; }
            public string FaxNumber { get; set; }
            public string EMailId { get; set; }
        }
        public class PaymentDetails
        {
            public string CurrencyCode { get; set; }
            public decimal Amount { get; set; }
            public AirlinePaymentDetails AirlinePaymentDetails { get; set; }

        }
        public class AirlinePaymentDetails
        {
            public string TicketingPCC { get; set; }
            public string PaymentType { get; set; }
            public string VendorCode { get; set; }
            public CreditCardDetails CreditCardDetails { get; set; }
        }
        public class CreditCardDetails
        {
            public string CardNumber { get; set; }
            public string CardType { get; set; }

        }
        public class BookTicket
        {
            public string ResponseStatus { get; set; }
            public string UserTrackId { get; set; }
            public BookOutput BookOutput { get; set; }
            public string FailureRemarks { get; set; }
        }
        public class BookOutput
        {
            public string Blockingdetails { get; set; }
            public List<FlightTicketDetails> FlightTicketDetails { get; set; }
        }
        public class FlightTicketDetails
        {
            public string HermesPNR { get; set; }
            public string TransactionId { get; set; }
            public CustomerDetails CustomerDetails { get; set; }
            public string AdultCount { get; set; }
            public string ChildCount { get; set; }
            public string InfantCount { get; set; }
            public string BookingType { get; set; }
            public string TravelType { get; set; }
            public string IssueDateTime { get; set; }
            public string TotalAmount { get; set; }
            public string OtherCharges { get; set; }
            public string BookingRemarks { get; set; }
            public TerminalContactDetails TerminalContactDetails { get; set; }
            public PaymentDetails PaymentDetails { get; set; }
            public List<AirlineDetails> AirlineDetails { get; set; }
            public List<PassengerDetails> PassengerDetails { get; set; }

            public IATADetails IATADetails { get; set; }

            public string TotalSegments { get; set; }

        }
        public class IATADetails
        {

        }

        public class CityResponse
        {
            public List<airline_code> airline_code { get; set; }
        }
        public class airline_code
        {
            public string code { get; set; }
            public string city { get; set; }
        }


        public class TerminalContactDetails
        {
            public string TerminalName { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
            public string ContactNumber { get; set; }
            public string EmailId { get; set; }
        }

        public class CustomerRequest
        {
            public PassengerDetails PassengerDetails { get; set; }
        }
        public class PassengerDetails
        {
            public string TicketNumber { get; set; }
            public string TransmissionControlNo { get; set; }
            public string PassengerType { get; set; }
            public string Title { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Gender { get; set; }
            public string Age { get; set; }
            public string DateofBirth { get; set; }
            public string IdentityProofId { get; set; }
            public string IdentityProofNumber { get; set; }
            public string PersonOrgId { get; set; }
            public string PaxType { get; set; }
            public string PaxNumber { get; set; }
            public string CompanyId { get; set; }
            public string TransType { get; set; }
            public string BookingId { get; set; }
            public List<BookedSegments> BookedSegments { get; set; }
        }
        public class BookingSegments
        {
            public string TicketNumber { get; set; }
            public string FlightNumber { get; set; }
            public string AirCraftType { get; set; }
            public string Origin { get; set; }
            public string OriginAirport { get; set; }

            public string ClassCode { get; set; }
            public string ClassCodeDesc { get; set; }
            public string FareBasis { get; set; }
            public string BaggageAllowed { get; set; }
            public string DepartureDateTime { get; set; }
            public string Destination { get; set; }
            public string DestinationAirport { get; set; }
            public string Arrivaldatetime { get; set; }
            public string AirlineCode { get; set; }
            public string StopOverAllowed { get; set; }
            public string FrequentFlyerId { get; set; }
            public string FrequentFlyerNumber { get; set; }
            public string SpecialServiceCode { get; set; }
            public string TotalTaxAmount { get; set; }
            public string GrossAmount { get; set; }


            public string FlightId { get; set; }



            public string MealCode { get; set; }
            public string SeatPreferId { get; set; }
            public string SupplierId { get; set; }

        }
        public class BookedSegments
        {
            public string TicketNumber { get; set; }
            public string FlightNumber { get; set; }
            public string AirCraftType { get; set; }
            public string Origin { get; set; }
            public string OriginAirport { get; set; }

            public string ClassCode { get; set; }
            public string ClassCodeDesc { get; set; }
            public string FareBasis { get; set; }
            public string BaggageAllowed { get; set; }
            public string DepartureDateTime { get; set; }
            public string Destination { get; set; }
            public string DestinationAirport { get; set; }
            public string Arrivaldatetime { get; set; }
            public string AirlineCode { get; set; }
            public string StopOverAllowed { get; set; }
            public string FrequentFlyerId { get; set; }
            public string FrequentFlyerNumber { get; set; }
            public string SpecialServiceCode { get; set; }
            public string TotalTaxAmount { get; set; }
            public string GrossAmount { get; set; }

            public string BasicAmount { get; set; }
            public string FlightId { get; set; }
            public string CompanyId { get; set; }
            public string TransType { get; set; }
            public string BookingId { get; set; }


            public string MealCode { get; set; }
            public string SeatPreferId { get; set; }
            public string SupplierId { get; set; }

        }
        public class PrintTicketReponse
        {
            public string FailureRemarks { get; set; }
            public string ResponseStatus { get; set; }
            public string UserTrackId { get; set; }
            public ReprintOutput ReprintOutput { get; set; }
            public FareRuleOutput FareRuleOutput { get; set; }
        }
        public class ReprintOutput
        {
            public TicketDetails TicketDetails { get; set; }
        }
        public class TicketDetails
        {
            public string HermesPNR { get; set; }
            public string TransactionId { get; set; }
            public CustomerDetails CustomerDetails { get; set; }
            public string IssueDateTime { get; set; }
            public List<PassengerDetails> PassengerDetails { get; set; }
            public List<AirlineDetails> AirlineDetails { get; set; }
        }
        public class BookingHistoryResponse
        {
            public string ResponseStatus { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public BookedHistoryOutput BookedHistoryOutput { get; set; }

        }
        public class BookedHistoryOutput
        {

            public List<BookedPNRs> BookedPNRs { get; set; }

        }
        public class BookedPNRs
        {
            public string AirlineName { get; set; }
            public string AirlinePNR { get; set; }
            public string BookingType { get; set; }
            public string Origin { get; set; }
            public string Destination { get; set; }
            public string BookedDateTime { get; set; }
            public string GrossAmount { get; set; }
            public string PNRStatus { get; set; }
            public string TravelType { get; set; }
        }
        public class CancelTicketReponse
        {
            public string FailureRemarks { get; set; }
            public string ResponseStatus { get; set; }
            public PartialCancellationOutput PartialCancellationOutput { get; set; }
            public CancellationOutput CancellationOutput { get; set; }
        }
        public class PartialCancellationOutput
        {
            public PartialCancellationDetails PartialCancellationDetails { get; set; }
            public string PartialCancellationRemarks { get; set; }
        }
        public class PartialCancellationDetails
        {
            public string PartialCancellationRemarks { get; set; }
        }
        public class CancellationOutput
        {
            public CancellationDetails CancellationDetails { get; set; }
        }
        public class CancellationDetails
        {
            public string FullCancellationRemarks { get; set; }
            public PartialCancelPNRDetails PartialCancelPNRDetails { get; set; }
        }
        public class PartialCancelPNRDetails
        {
            public string AirlineCode { get; set; }
            public string HermesPNR { get; set; }
            public string AilinePNR { get; set; }
            public string CRSPNR { get; set; }
            public List<CancelPassengerDetails> CancelPassengerDetails { get; set; }
        }
        public class CancelPassengerDetails
        {
            public string PaxNumber { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PassengerType { get; set; }
            public List<CancelTicketDetails> CancelTicketDetails { get; set; }
        }
        public class CancelTicketDetails
        {
            public string TicketNumber { get; set; }
            public string SegmentId { get; set; }
            public string FlightNumber { get; set; }
            public string Origin { get; set; }
            public string DepartureDateTime { get; set; }
            public string Destination { get; set; }
            public string ArrivalDateTime { get; set; }
            public string ClassCodeDesc { get; set; }
            public string BasicAmount { get; set; }
            public string TotalTaxAmount { get; set; }
            public string GrossAmount { get; set; }
            public string TicketStatus { get; set; }
        }
        #endregion

        #region Flight API Parameters
        public class APIModel
        {
            public string OperatorCode { get; set; }
            public string MobileNumber { get; set; }
            public string Amount { get; set; }
        }



        public class BookSeat
        {
            public Authentication Authentication { get; set; }
            public SeatConfirmAfterBookingInput SeatConfirmAfterBookingInput { get; set; }
        }
        public class SeatConfirmAfterBookingInput
        {
            public string HermesPNR { get; set; }
            public string AirlineCode { get; set; }
            public List<FlightSegment> FlightSegments { get; set; }
        }

        public class FlightSegment
        {
            public string AirlineCode { get; set; }
            public string FlightNumber { get; set; }
            public string DepartureDate { get; set; }


            public string Origin { get; set; }
            public string Destination { get; set; }


            public string ClassCode { get; set; }

            public List<PassengerDetail> PassengerDetails { get; set; }
        }




        public class TransactionStatusResponse
        {
            public string ResponseStatus { get; set; }

            public string UserTrackId { get; set; }
            public TransactionStatusOutput TransactionStatusOutput { get; set; }

        }
        public class TransactionStatusOutput
        {
            public TransactionStatus TransactionStatus { get; set; }
            public string Remarks { get; set; }

            public string GSTDetails { get; set; }
        }
        public class TransactionStatus
        {
            public string Status { get; set; }
            public List<FlightTicketDetails> FlightTicketDetails { get; set; }

        }
        public class PartialCancel
        {
            public Authentication Authentication { get; set; }
            public PartialCancellationInput PartialCancellationInput { get; set; }
            public List<PartialCancelPassengerDetails> PartialCancelPassengerDetails { get; set; }
        }
        public class PartialCancellationInput
        {
            public string HermesPNR { get; set; }
            public string AirlinePNR { get; set; }
            public string CRSPNR { get; set; }
            public List<PartialCancelPassengerDetail> PartialCancelPassengerDetails { get; set; }
        }
        public class PartialCancelPassengerDetails
        {
            public string PaxNumber { get; set; }
            public List<PartialCancelTicketDetail> PartialCancelTicketDetails { get; set; }
        }
        public class PartialCancelTicketDetail
        {
            public string TicketNumber { get; set; }
            public int SegmentId { get; set; }
            public string FlightNumber { get; set; }
            public string Origin { get; set; }
            public string Destination { get; set; }
        }
        public class PartialCancelPassengerDetail
        {
            public int PaxNumber { get; set; }
            public List<PartialCancelTicketDetail> PartialCancelTicketDetails { get; set; }
        }
        public class PartialCancelResponse
        {
            public string FailureRemarks { get; set; }
            public string ResponseStatus { get; set; }
            public PartialCancellationOutput PartialCancellationOutput { get; set; }
        }
        public class SeatMap
        {
            public DataTable BookSeatResponse { get; set; }
            public DataSet SaveSeatBook()
            {
                SqlParameter[] para = {
                                     new SqlParameter("@BookSeatResponse", BookSeatResponse),
                                   new SqlParameter("@bookedby", "Web"),

                                     };
                DataSet ds = DBHelper.ExecuteQuery(Common.ProcedureName.SeatConfirmationInsert, para);
                return ds;
            }
        }

        public class SeatMapResponse
        {
            public string ResponseStatus { get; set; }
            public string FailureRemarks { get; set; }
            public SeatMapAfterBookingOutput SeatMapAfterBookingOutput { get; set; }
        }
        public class SeatMapAfterBookingOutput
        {
            public string AirlineCode { get; set; }
            public string HermesPNR { get; set; }
            public string AirlinePNR { get; set; }
            public List<PassengerDetails> PassengerDetails { get; set; }
            public List<FlightSegments> FlightSegments { get; set; }
            public string FailureRemarks { get; set; }
        }
        public class FlightSegments
        {
            public string FlightNumber { get; set; }
            public string AirlineCode { get; set; }
            public string Origin { get; set; }
            public string Destination { get; set; }
            public string DepartureDate { get; set; }
            public string ArrivalDate { get; set; }
            public string ClassCode { get; set; }
            public string AirCraftType { get; set; }
            public string ClassType { get; set; }
            public SeatLayoutDetails SeatLayoutDetails { get; set; }
        }
        public class SeatLayoutDetails
        {
            public SeatLayout SeatLayout { get; set; }
            public string BookedSeats { get; set; }
            public string AvailableSeats { get; set; }
            public string BlockedSeats { get; set; }
            public List<SeatTypes> SeatTypes { get; set; }
        }
        public class SeatLayout
        {
            public List<SeatRows> SeatRows { get; set; }
        }
        public class SeatRows
        {
            public List<Seats> Seats { get; set; }
        }
        public class Seats
        {
            public string SeatNo { get; set; }
            public string SeatCode { get; set; }
            public string SeatAmount { get; set; }
        }
        public class SeatTypes
        {
            public string SeatTypeName { get; set; }
            public string SeatNos { get; set; }
        }

        public class SeatBookResponse
        {
            public string ResponseStatus { get; set; }
            public SeatConfirmAfterBookingOutput SeatConfirmAfterBookingOutput { get; set; }
            public string FailureRemarks { get; set; }
        }
        public class SeatConfirmAfterBookingOutput
        {
            public string Remarks { get; set; }
        }
        public class Authentication
        {
            public string LoginId { get; set; }
            public string Password { get; set; }
        }

        public class SeatMapAfterBookingInput
        {
            public string HermesPNR { get; set; }
            public string AirlineCode { get; set; }
        }

        public class RootObject
        {
            public Authentication Authentication { get; set; }
            public SeatMapAfterBookingInput SeatMapAfterBookingInput { get; set; }
        }

        public class AuthenticationTicket
        {
            public string LoginId { get; set; }
            public string Password { get; set; }
        }

        public class CancellationInput
        {
            public string HermesPNR { get; set; }
            public string AirlinePNR { get; set; }
            public string CancelType { get; set; }
        }

        public class RootObjectTicket
        {
            public Authentication Authentication { get; set; }
            public CancellationInput CancellationInput { get; set; }
        }
        #endregion










    }
}