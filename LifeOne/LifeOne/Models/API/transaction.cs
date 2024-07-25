using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class transaction
    {
        public class CustomerDetails
        {
            public object Title { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string CountryId { get; set; }
            public string ContactNumber { get; set; }
            public string EmailId { get; set; }
            public object PinCode { get; set; }
        }

        public class AirlineDetail
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

        public class IATADetails
        {
            public string CRSPNR { get; set; }
            public object IATAAgentNumber { get; set; }
            public object IATAAgentName1 { get; set; }
            public string IATAAgentName2 { get; set; }
            public object TicketNumber { get; set; }
        }

        public class CreditCardDetails
        {
            public object CardNumber { get; set; }
            public object CardType { get; set; }
        }

        public class AirlinePaymentDetails
        {
            public string TicketingPCC { get; set; }
            public string PaymentType { get; set; }
            public string VendorCode { get; set; }
            public CreditCardDetails CreditCardDetails { get; set; }
        }

        public class PaymentDetails
        {
            public string CurrencyCode { get; set; }
            public int Amount { get; set; }
            public AirlinePaymentDetails AirlinePaymentDetails { get; set; }
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

        public class LadderDetails
        {
            public string EndorsementRestriction { get; set; }
            public string IssueInExchangeFor { get; set; }
            public string FareCalculation { get; set; }
        }

        public class TaxDetail
        {
            public string Description { get; set; }
            public int Amount { get; set; }
        }

        public class BookedSegment
        {
            public string TicketNumber { get; set; }
            public string FlightNumber { get; set; }
            public string AirCraftType { get; set; }
            public string Origin { get; set; }
            public string OriginAirport { get; set; }
            public string DepartureDateTime { get; set; }
            public string Destination { get; set; }
            public string DestinationAirport { get; set; }
            public string Arrivaldatetime { get; set; }
            public string AirlineCode { get; set; }
            public string ClassCode { get; set; }
            public string ClassCodeDesc { get; set; }
            public string FareBasis { get; set; }
            public string BaggageAllowed { get; set; }
            public string StopOverAllowed { get; set; }
            public string FrequentFlyerId { get; set; }
            public string FrequentFlyerNumber { get; set; }
            public string SpecialServiceCode { get; set; }
            public string MealCode { get; set; }
            public string SeatPreferId { get; set; }
            public string BasicCurrencyCode { get; set; }
            public string CurrencyCode { get; set; }
            public int BasicAmount { get; set; }
            public int EquivalentFare { get; set; }
            public List<TaxDetail> TaxDetails { get; set; }
            public int TotalTaxAmount { get; set; }
            public int TransactionFee { get; set; }
            public int ServiceCharge { get; set; }
            public int GrossAmount { get; set; }
            public object AdditionalSSRDetails { get; set; }
        }

        public class PassengerDetail
        {
            public string TicketNumber { get; set; }
            public string TransmissionControlNo { get; set; }
            public string PassengerType { get; set; }
            public string Title { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Age { get; set; }
            public string IdentityProofId { get; set; }
            public string IdentityProofNumber { get; set; }
            public string PersonOrgId { get; set; }
            public LadderDetails LadderDetails { get; set; }
            public List<BookedSegment> BookedSegments { get; set; }
        }

        public class FlightTicketDetail
        {
            public string HermesPNR { get; set; }
            public string TransactionId { get; set; }
            public CustomerDetails CustomerDetails { get; set; }
            public List<AirlineDetail> AirlineDetails { get; set; }
            public IATADetails IATADetails { get; set; }
            public int TotalSegments { get; set; }
            public int TotalAmount { get; set; }
            public string OtherCharges { get; set; }
            public int AdultCount { get; set; }
            public int ChildCount { get; set; }
            public int InfantCount { get; set; }
            public string BookingType { get; set; }
            public string TravelType { get; set; }
            public string IssueDateTime { get; set; }
            public string BaseOrigin { get; set; }
            public string BaseDestination { get; set; }
            public string TourCode { get; set; }
            public PaymentDetails PaymentDetails { get; set; }
            public TerminalContactDetails TerminalContactDetails { get; set; }
            public List<PassengerDetail> PassengerDetails { get; set; }
            public object BookingRemarks { get; set; }
        }

        public class GSTDetails
        {
            public string GSTNumber { get; set; }
            public string EMailId { get; set; }
            public string CompanyName { get; set; }
            public object ContactNumber { get; set; }
            public object Address { get; set; }
            public object FirstName { get; set; }
            public object LastName { get; set; }
            public object PinCode { get; set; }
            public object Address1 { get; set; }
        }

        public class TransactionStatus
        {
            public int Status { get; set; }
            public List<FlightTicketDetail> FlightTicketDetails { get; set; }
            public string Remarks { get; set; }
            public GSTDetails GSTDetails { get; set; }
        }

        public class TransactionStatusOutput
        {
            public TransactionStatus TransactionStatus { get; set; }
        }

        public class TransactionRootobject
        {
            public int ResponseStatus { get; set; }
            public string UserTrackId { get; set; }
            public TransactionStatusOutput TransactionStatusOutput { get; set; }
        }
    }
}