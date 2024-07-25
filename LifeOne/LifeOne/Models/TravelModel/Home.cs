
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.TravelModel
{
    public class Home
    {

        public List<Home> FlightDetails { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ContactNumber { get; set; }
        public string EmailId { get; set; }
        public string PinCode { get; set; }
        //public string HermesPNR { get; set; }
        //public string HermesPNR { get; set; }
        //public string HermesPNR { get; set; }
        //public string HermesPNR { get; set; }
        public string RadioButtonVal { get; set; }
        public string FailureRemark { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string DepartingDate { get; set; }
        public string ReturningDate { get; set; }
        public string Class { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public int Infant { get; set; }
        public string usertrackid { get; set; }
        public string hermespnr { get; set; }
        public string TransactionId { get; set; }
        public string TotalAmount { get; set; }
        public string OtherCharges { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int InfantCount { get; set; }
        public string BookingType { get; set; }
        public string IssueDateTime { get; set; }
        public string TravelType { get; set; }
        public string TourCode { get; set; }
        public string bookingremarks { get; set; }
        public string Title { get; set; }

        public string CountryId { get; set; }
        public string TerminalName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string TerminalCity { get; set; }
        public string TerminalState { get; set; }
        public string TerminalCountry { get; set; }
        public string TerminalContact { get; set; }
        public string TerminalEmailId { get; set; }
        public decimal amount { get; set; }
        public string currencycode { get; set; }
        public string cardnumber { get; set; }
        public string cardtype { get; set; }
        public string airlinetickettingpcc { get; set; }
        public string airlinepaymenttype { get; set; }
        public string airlinevendorcode { get; set; }
        public string airlineCity { get; set; }
        public string airlinecode { get; set; }
        public string airlinepnr { get; set; }
        public string airlinename { get; set; }
        public string airlineaddress1 { get; set; }
        public string airlineaddress2 { get; set; }
        public string airlinecontactnumber { get; set; }
        public string faxnumber { get; set; }
        public string airlineemailid { get; set; }
        public DataTable PassengerDetails { get; set; }
        public DataTable BookingSegments { get; set; }

        public string ReferenceNumber { get; set; }
        public string OperatorDescription { get; set; }
        public string MobileNumber { get; set; }
        public string Amount { get; set; }
        public string RechargeDateTime { get; set; }
        public string OperatorTransactionId { get; set; }
        public string FK_MemId { get; set; }
        public string CompanyId { get; set; }
        public string Status { get; set; }
        public string TransType { get; set; }
        public string BookingId { get; set; }
        public string BookingBy { get; set; }
        public string Type { get; set; }
        public string PNR { get; set; }
        public string CustEmail { get; set; }

        public DataSet SaveFlightResponse()
        {
                    SqlParameter[] para = {
                new SqlParameter("@BookingType", BookingType),
                                       new SqlParameter("@Origin", Origin),
                                        new SqlParameter("@Destination", Destination?? string.Empty),
                                       new SqlParameter("@DepartingDate", DepartingDate??string.Empty),
                                       new SqlParameter("@ReturningDate", ReturningDate??string.Empty),
                                       new SqlParameter("@Class", Class ??string.Empty),
                                       new SqlParameter("@Adult", Adult),
                                       new SqlParameter("@Child", Child),
                                       new SqlParameter("@Infant", Infant),
                                       new SqlParameter("@usertrackid", usertrackid),
                                       new SqlParameter("@hermespnr", hermespnr?? string.Empty),
                                       new SqlParameter("@TransactionId", TransactionId?? string.Empty),
                                       new SqlParameter("@TotalAmount", TotalAmount ),
                                       new SqlParameter("@OtherCharges", OtherCharges ),
                                       new SqlParameter("@AdultCount", AdultCount),
                                       new SqlParameter("@ChildCount", ChildCount),
                                       new SqlParameter("@InfantCount", InfantCount),
                                       new SqlParameter("@TravelType", TravelType ?? string.Empty),
                                       new SqlParameter("@IssueDateTime", IssueDateTime ?? string.Empty),
                                       new SqlParameter("@TourCode", TourCode ?? string.Empty),
                                       new SqlParameter("@bookingremarks", bookingremarks ?? string.Empty),
                                       new SqlParameter("@Title", Title ?? string.Empty),
                                       new SqlParameter("@Name", Name ?? string.Empty),
                                       new SqlParameter("@address", Address ?? string.Empty),
                                       new SqlParameter("@city", City ?? string.Empty),
                                       new SqlParameter("@countryid", CountryId),
                                       new SqlParameter("@contactnumber", ContactNumber ?? string.Empty),
                                       new SqlParameter("@emailid", EmailId ?? string.Empty),
                                       new SqlParameter("@pincode", PinCode ?? string.Empty),
                                       new SqlParameter("@terminalname", TerminalName ?? string.Empty),
                                       new SqlParameter("@address1", Address1 ?? string.Empty),
                                       new SqlParameter("@address2", Address2 ?? string.Empty),
                                       new SqlParameter("@terminalcity", TerminalCity ?? string.Empty),
                                       new SqlParameter("@terminalstate", TerminalState ?? string.Empty),
                                       new SqlParameter("@terminalcountry", TerminalCountry ?? string.Empty),
                                       new SqlParameter("@terminalcontact", TerminalContact ?? string.Empty),
                                       new SqlParameter("@terminalemailid", TerminalEmailId ?? string.Empty),
                                       new SqlParameter("@currencycode", currencycode ?? string.Empty),
                                       new SqlParameter("@amount", amount),
                                       new SqlParameter("@airlinetickettingpcc", airlinetickettingpcc ?? string.Empty),
                                       new SqlParameter("@airlinepaymenttype", airlinepaymenttype ?? string.Empty),
                                       new SqlParameter("@airlinevendorcode", airlinevendorcode ?? string.Empty),
                                       new SqlParameter("@cardnumber", cardnumber ?? string.Empty),
                                       new SqlParameter("@cardtype", cardtype ?? string.Empty),
                                       new SqlParameter("@airlinecode", airlinecode ?? string.Empty),
                                       new SqlParameter("@airlinepnr", airlinepnr ?? string.Empty),
                                       new SqlParameter("@airlinename", airlinename ?? string.Empty),
                                       new SqlParameter("@airlineaddress1", airlineaddress1 ?? string.Empty),
                                       new SqlParameter("@airlineaddress2", airlineaddress2 ?? string.Empty),
                                       new SqlParameter("@airlinecity", airlineCity ?? string.Empty),
                                       new SqlParameter("@airlinecontactnumber", airlinecontactnumber ?? string.Empty),
                                       new SqlParameter("@faxnumber", faxnumber ?? string.Empty),
                                       new SqlParameter("@airlineemailid", airlineemailid ?? string.Empty),
                                       new SqlParameter("@CompanyId", CompanyId),
                                       new SqlParameter("@Status", Status ?? string.Empty),
                                       new SqlParameter("@TransType", TransType),
                                       new SqlParameter("@BookingId", BookingId ?? string.Empty),
                                       new SqlParameter("@PassengerDetails", PassengerDetails ),
                                       new SqlParameter("@BookingSegments", BookingSegments),
                                       new SqlParameter("@BookingBy", BookingBy ?? string.Empty),
                                       new SqlParameter("@Fk_MemId", FK_MemId)


                                     };
            DataSet ds = DBHelper.ExecuteQuery(Common.ProcedureName.FlightRequestResponseInsert, para);
            return ds;
        }

        public DataSet SaveMobileResponse()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@UserTrackId", usertrackid),
                                       new SqlParameter("@ReferenceNumber", ReferenceNumber),
                                       new SqlParameter("@OperatorDescription", OperatorDescription),
                                       new SqlParameter("@MobileNumber", MobileNumber),
                                       new SqlParameter("@Amount", Amount),
                                       new SqlParameter("@OperatorTransactionId", OperatorTransactionId),
                                       new SqlParameter("@CompanyId", CompanyId),
                                       new SqlParameter("@Status", Status),
                                       new SqlParameter("@TransType", TransType),
                                       new SqlParameter("@BookingId", BookingId),
                                       new SqlParameter("@Type", Type),
                                       new SqlParameter("@FK_MemId",FK_MemId),
                                  };
            DataSet ds = DBHelper.ExecuteQuery(Common.ProcedureName.MobileResponse, para);
            return ds;
        }


        public DataSet SaveFailureResponse()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@UserTrackId", usertrackid),
                                       new SqlParameter("@FailureRemark", FailureRemark),
                                        new SqlParameter("@hermespnr", hermespnr),
                                         new SqlParameter("@airlinepnr", airlinepnr),

                                  };
            DataSet ds = DBHelper.ExecuteQuery(Common.ProcedureName.FailureResponse, para);
            return ds;
        }

    }



    public class GetWalletAount : DBHelper
    {
        public string FK_MemID { get; set; }

        public DataSet GetAmount()
        {
            SqlParameter[] para =
            {
                new SqlParameter ("@Fk_MemId",FK_MemID)
            };
            DataSet dsresult = ExecuteQuery("GetWalletAmount", para);
            return dsresult;
        }
    }

    public class APIResponse1
    {
        public string Balance { get; set; }
    }
    public class GetCompanyBalanceInput
    {
        public string CompanyId { get; set; }
    }

    public class DebitWallet : DBHelper
    {
        public string CompanyId { get; set; }

        public string DrAmount { get; set; }

        public DataSet DebitAmount()
        {
            SqlParameter[] para =
            {
                new SqlParameter ("@CompanyId",CompanyId),
                new SqlParameter ("@DrAmount",DrAmount)
            };
            DataSet dsresult = ExecuteQuery("DebitWallet", para);
            return dsresult;
        }
    }

    public class WalletResponse
    {
        public string Response { get; set; }
    }

    #region GetSeatBlock
    public class SaveBusPassengerDetail : DBHelper
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string UserTrackId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string City { get; set; }
        public string CountryId { get; set; }
        public string EmailId { get; set; }
        public string IdProofId { get; set; }
        public string IdProofNumber { get; set; }
        public string TotalTickets { get; set; }
        public string TotalAmount { get; set; }
        public string TransportId { get; set; }
        public string ScheduleId { get; set; }
        public string StationId { get; set; }
        public string TravelDate { get; set; }
        public string CompanyId { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string BookedDate { get; set; }
        public string TransactionIdPNR { get; set; }
        public string TransportPNR { get; set; }
        public string BusName { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string DepartureTime { get; set; }
        public string ServiceTax { get; set; }
        public string Commission { get; set; }
        public string CancellationPolicy { get; set; }
        public string BoardingId { get; set; }
        public string BoardingPlace { get; set; }
        public string BoardingTime { get; set; }
        public string LandMark { get; set; }
        public string DroppingId { get; set; }
        public string DroppingPlace { get; set; }
        public string DroppingTime { get; set; }
        public string DroppingAddress { get; set; }
        public string DroppingLandMark { get; set; }
        public string ReportingTime { get; set; }
        public string ServiceCharges { get; set; }
        public string TransportName { get; set; }
        public string TransportAddress1 { get; set; }
        public string TransportAddress2 { get; set; }
        public string TransportAddress3 { get; set; }
        public string TransportPinCode { get; set; }
        public string TransportContactNo { get; set; }
        public string TransportFax { get; set; }
        public string TransportWebSite { get; set; }
        public string TransportEmailId { get; set; }
        public string TransType { get; set; }
        public string BookingId { get; set; }
        public string FK_MemID { get; set; }
        public DataTable PassengerDetails { get; set; }
        public DataSet SaveBusDetails()
        {
            SqlParameter[] para =
            {
                    new SqlParameter ("@LoginId",LoginId),
                    new SqlParameter ("@Password",Password),
                    new SqlParameter ("@UserTrackId",UserTrackId),
                    new SqlParameter ("@Title",Title),
                    new SqlParameter ("@Name",Name),
                    new SqlParameter ("@Address",Address),
                    new SqlParameter ("@ContactNumber",ContactNumber),
                    new SqlParameter ("@City",City),
                    new SqlParameter ("@CountryId",CountryId),
                    new SqlParameter ("@EmailId",EmailId),
                    new SqlParameter ("@IdProofId",IdProofId),
                    new SqlParameter ("@IdProofNumber",IdProofNumber),
                    new SqlParameter ("@TotalTickets",TotalTickets),
                    new SqlParameter ("@TotalAmount",TotalAmount),
                    new SqlParameter ("@TransportId",TransportId),
                    new SqlParameter ("@ScheduleId",ScheduleId),
                    new SqlParameter ("@StationId",StationId),
                    new SqlParameter ("@TravelDate",TravelDate),
                    new SqlParameter ("@CompanyId",CompanyId),
                    new SqlParameter ("@Status",Status),
                    new SqlParameter ("@Remark",Remark),
                    new SqlParameter ("@BookedDate",BookedDate),
                    new SqlParameter ("@TransactionIdPNR",TransactionIdPNR),
                    new SqlParameter ("@TransportPNR",TransportPNR),
                    new SqlParameter ("@BusName",BusName),
                    new SqlParameter ("@Origin",Origin),
                    new SqlParameter ("@Destination",Destination),
                    new SqlParameter ("@DepartureTime",DepartureTime),
                    new SqlParameter ("@ServiceTax",ServiceTax),
                    new SqlParameter ("@Commission",Commission),
                    new SqlParameter ("@CancellationPolicy",CancellationPolicy),
                    new SqlParameter ("@BoardingId",BoardingId),
                    new SqlParameter ("@BoardingPlace",BoardingPlace),
                    new SqlParameter ("@BoardingTime",BoardingTime),
                    new SqlParameter ("@LandMark",LandMark),
                    new SqlParameter ("@DroppingId",DroppingId),
                    new SqlParameter ("@DroppingPlace",DroppingPlace),
                    new SqlParameter ("@DroppingTime",DroppingTime),
                    new SqlParameter ("@DroppingAddress",DroppingAddress),
                    new SqlParameter ("@DroppingLandMark",DroppingLandMark),
                    new SqlParameter ("@ReportingTime",ReportingTime),
                    new SqlParameter ("@ServiceCharges",ServiceCharges),
                    new SqlParameter ("@TransportName",TransportName),
                    new SqlParameter ("@TransportAddress1",TransportAddress1),
                    new SqlParameter ("@TransportAddress2",TransportAddress2),
                    new SqlParameter ("@TransportAddress3",TransportAddress3),
                    new SqlParameter ("@TransportPinCode",TransportPinCode),
                    new SqlParameter ("@TransportContactNo",TransportContactNo),
                    new SqlParameter ("@TransportFax",TransportFax),
                    new SqlParameter ("@TransportWebSite",TransportWebSite),
                    new SqlParameter ("@TransportEmailId",TransportEmailId),
                    new SqlParameter("@BusPassengerDetail", PassengerDetails),
                    new SqlParameter("@TransType", TransType),
                    new SqlParameter("@BookingId", BookingId),
                    new SqlParameter("@FK_MemID", FK_MemID),
            };
            DataSet dsresult = ExecuteQuery("BusSeatBlockInsert", para);
            return dsresult;
        }
    }
    #endregion

    #region Get Book
    public class SaveBusBookingDetail : DBHelper
    {
        public string UsertrackId { get; set; }
        public string ResponseStatus { get; set; }
        public string ServiceTax { get; set; }
        public string ConvenienceFee { get; set; }
        public string Commission { get; set; }
        public string CancellationPolicy { get; set; }
        public string TransportName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string CityNamePinCode { get; set; }
        public string ContactNumber { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string EmailId { get; set; }
        public string TransactionId { get; set; }
        public string TransportPNR { get; set; }
        public string TotalAmount { get; set; }
        public string TotalTickets { get; set; }
        public string BusName { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string TravelDate { get; set; }
        public string DepartureTime { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CustomerContactNumber { get; set; }
        public string City { get; set; }
        public string CountryId { get; set; }
        public string CustomerEmailId { get; set; }
        public string IdProofId { get; set; }
        public string IdProofNumber { get; set; }
        public string BookedDate { get; set; }
        public string CompanyId { get; set; }
        public string Status { get; set; }
        public string TransType { get; set; }
        public string BookingId { get; set; }
        public DataTable PaxList { get; set; }
        public string ScheduleId { get; set; }
        public string StationId { get; set; }
        public string TransportId { get; set; }
        public int FK_MemID { get; set; }
        public DataSet SaveBusBookDetails()
        {
            SqlParameter[] para =
            {
                    new SqlParameter ("@UsertrackId",UsertrackId),
                    new SqlParameter ("@ResponseStatus",ResponseStatus),
                    new SqlParameter ("@ServiceTax ",ServiceTax),
                    new SqlParameter ("@ConvenienceFee ",ConvenienceFee),
                    new SqlParameter ("@Commission",Commission),
                    new SqlParameter ("@CancellationPolicy",CancellationPolicy),
                    new SqlParameter ("@TransportName ",TransportName),
                    new SqlParameter ("@Address1 ",Address1),
                    new SqlParameter ("@Address2",Address2),
                    new SqlParameter ("@Address3 ",Address3),
                    new SqlParameter ("@CityNamePinCode",CityNamePinCode),
                    new SqlParameter ("@ContactNumber ",ContactNumber),
                    new SqlParameter ("@Fax",Fax),
                    new SqlParameter ("@Website",Website),
                    new SqlParameter ("@EmailId",EmailId),
                    new SqlParameter ("@TransactionId",TransactionId),
                    new SqlParameter ("@TransportPNR",TransportPNR),
                    new SqlParameter ("@TotalAmount",TotalAmount),
                    new SqlParameter ("@TotalTickets",TotalTickets),
                    new SqlParameter ("@BusName",BusName),
                    new SqlParameter ("@Origin",Origin),
                    new SqlParameter ("@Destination",Destination),
                    new SqlParameter ("@TravelDate",TravelDate),
                    new SqlParameter ("@DepartureTime",DepartureTime),
                    new SqlParameter ("@Title",Title),
                    new SqlParameter ("@Name",Name),
                    new SqlParameter ("@Address",Address),
                    new SqlParameter ("@CustomerContactNumber",CustomerContactNumber),
                    new SqlParameter ("@City",City),
                    new SqlParameter ("@CountryId ",CountryId),
                    new SqlParameter ("@CustomerEmailId",CustomerEmailId),
                    new SqlParameter ("@IdProofId",IdProofId),
                    new SqlParameter ("@IdProofNumber",IdProofNumber),
                    new SqlParameter ("@BookedDate",BookedDate),
                    new SqlParameter ("@CompanyId",CompanyId),
                    new SqlParameter ("@Status",Status),
                    new SqlParameter ("@TransType",TransType),
                    new SqlParameter ("@BookingId",BookingId),
                    new SqlParameter ("@PaxList",PaxList),
                    new SqlParameter ("@ScheduleId",ScheduleId),
                    new SqlParameter ("@StationId",StationId),
                    new SqlParameter ("@TransportId",TransportId),
                    new SqlParameter("@FK_MemID", FK_MemID),

            };
            DataSet dsresult = ExecuteQuery("GetBusBookInsert", para);
            return dsresult;
        }
    }
    #endregion

    #region Get Cancel Booked Seat
    public class CancelBookingSeat : DBHelper
    {
        public string ResponseStatus { get; set; }
        public string TransportName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string CityNamePinCode { get; set; }
        public string ContactNumber { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string EmailId { get; set; }
        public string TransactionId { get; set; }
        public string TransportPNR { get; set; }
        public string TotalAmount { get; set; }
        public string TotalTickets { get; set; }
        public string BusName { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string TravelDate { get; set; }
        public string DepartureTime { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CustomerContactNumber { get; set; }
        public string City { get; set; }
        public string CountryId { get; set; }
        public string CustomerEmailId { get; set; }
        public string IdProofId { get; set; }
        public string IdProofNumber { get; set; }
        public string BookedDate { get; set; }
        public string CompanyId { get; set; }
        public string Status { get; set; }
        public string TransType { get; set; }
        public string BookingId { get; set; }
        public DataTable CancelPaxList { get; set; }
        public string TotalTicketsToCancel { get; set; }
        public string PenaltyAmount { get; set; }
        public string TicketNumbers { get; set; }
        //public string ScheduleId { get; set; }      
        public string TransportId { get; set; }
        public string CancellationPenalty { get; set; }
        public string CancellationNUmber { get; set; }
        public string CancellationDateTime { get; set; }
        public string Remarks { get; set; }
        public string FK_MemID { get; set; }
        public DataSet CancelBusBookDetails()
        {
            SqlParameter[] para =
            {
                   
                    new SqlParameter ("@ResponseStatus",ResponseStatus),
                    new SqlParameter ("@TransportName ",TransportName),
                    new SqlParameter ("@Address1 ",Address1),
                    new SqlParameter ("@Address2",Address2),
                    new SqlParameter ("@Address3 ",Address3),
                    new SqlParameter ("@CityNamePinCode",CityNamePinCode),
                    new SqlParameter ("@ContactNumber ",ContactNumber),
                    new SqlParameter ("@Fax",Fax),
                    new SqlParameter ("@Website",Website),
                    new SqlParameter ("@EmailId",EmailId),
                    new SqlParameter ("@TransactionId",TransactionId),
                    new SqlParameter ("@TransportPNR",TransportPNR),
                    new SqlParameter ("@TotalAmount",TotalAmount),
                    new SqlParameter ("@TotalTickets",TotalTickets),
                    new SqlParameter ("@BusName",BusName),
                    new SqlParameter ("@Origin",Origin),
                    new SqlParameter ("@Destination",Destination),
                    new SqlParameter ("@TravelDate",TravelDate),
                    new SqlParameter ("@DepartureTime",DepartureTime),
                    new SqlParameter ("@Title",Title),
                    new SqlParameter ("@Name",Name),
                    new SqlParameter ("@Address",Address),
                    new SqlParameter ("@CustomerContactNumber",CustomerContactNumber),
                    new SqlParameter ("@City",City),
                    new SqlParameter ("@CountryId ",CountryId),
                    new SqlParameter ("@CustomerEmailId",CustomerEmailId),
                    new SqlParameter ("@IdProofId",IdProofId),
                    new SqlParameter ("@IdProofNumber",IdProofNumber),
                    new SqlParameter ("@BookedDate",BookedDate),
                    new SqlParameter ("@CompanyId",CompanyId),
                    new SqlParameter ("@Status",Status),
                    new SqlParameter ("@TransType",TransType),
                    new SqlParameter ("@BookingId",BookingId),
                    new SqlParameter ("@CancelPaxList",CancelPaxList),
                    new SqlParameter ("@TotalTicketsToCancel",TotalTicketsToCancel),
                    new SqlParameter ("@PenaltyAmount",PenaltyAmount),
                    new SqlParameter ("@TicketNumbers",TicketNumbers),
                    new SqlParameter ("@TransportId",TransportId),
                    new SqlParameter("@CancellationPenalty",CancellationPenalty),
                    new SqlParameter("@CancellationNUmber", CancellationNUmber),
                    new SqlParameter("@CancellationDateTime",CancellationDateTime),
                    new SqlParameter("@Remarks",Remarks),
                      new SqlParameter("@FK_MemID", FK_MemID)
    };
            DataSet dsresult = ExecuteQuery("GetCancelBusBookInsert", para);
            return dsresult;
        }
    }
    #endregion

    #region Update Cancel PaxList
    public class UpdateCancelBookingSeat : DBHelper
    {
       
        public DataTable UpdateCancelPaxList { get; set; }

        public DataSet UpdateCancelBusBookDetails()
        {
            SqlParameter[] para =
            {

                  
                   
                    new SqlParameter ("@UpdateCancelTicket",UpdateCancelPaxList)

    };
            DataSet dsresult = ExecuteQuery("UpdateCancelBookingDetails", para);
            return dsresult;
        }
    }
    #endregion

    public class BookedHistory
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string TravelType { get; set; }
        public List<BookedHistory> BookingList { get; set; }
        public List<BookedHistory> TicketDetails { get; set; }
        public string HermesPNR { get; set; }
        public string AirlineName { get; set; }
        public string AirlinePNR { get; set; }
        public string BookingType { get; set; }
        public string BookSeatButton { get; set; }
        public string BoookingDate { get; set; }
        public string TerminalName { get; set; }
        public string TotalAMount { get; set; }
        public string PassengerId { get; set; }
        public string Pk_FlightResponseId { get; set; }
        public string TicketNumber { get; set; }
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string DepartureDateTime { get; set; }
        public string Destination { get; set; }
        public string Arrivaldatetime { get; set; }
        public string AirlineCode { get; set; }
        public string ClassCode { get; set; }
        public string ClassCodeDesc { get; set; }
        public string BaggageAllowed { get; set; }
        public string TotalTaxAmount { get; set; }
        public string GrossAmount { get; set; }
        public string Remarks { get; set; }
        public string ButtonDisplay { get; set; }
        public string CancellationType { get; set; }
        public string DepartingDate { get; set; }
        public string UserTrackId { get; set; }
        public string FlightId { get; set; }
        public DataSet GetBookingDetails()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@FromDate", FromDate),
                                       new SqlParameter("@ToDate", ToDate),


                                     };
            DataSet ds = DBHelper.ExecuteQuery(Common.ProcedureName.BookingDetails, para);
            return ds;
        }
        public DataSet GetTicketDetails()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@PassengerId", PassengerId),



                                     };
            DataSet ds = DBHelper.ExecuteQuery(Common.ProcedureName.TicketDetails, para);
            return ds;
        }
        public DataSet SaveCancelTicket()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@HermesPNR", HermesPNR),
                                        new SqlParameter("@Remarks", Remarks),
                                      new SqlParameter("@AirlinePNR", AirlinePNR),
                                      new SqlParameter("@Type", CancellationType),

                                     };
            DataSet ds = DBHelper.ExecuteQuery(Common.ProcedureName.CancelTicketInsert, para);
            return ds;
        }
    }

    public class BookingParameter
    {
        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string Fk_MemId { get; set; }

        public DataSet BookingHis()
        {
            SqlParameter[] para = {

               new SqlParameter("@FromDate", string.IsNullOrEmpty(FromDate) ? null : Common.ConvertToSystemDate(FromDate, "dd/MM/yyyy")),
               new SqlParameter("@ToDate", string.IsNullOrEmpty(ToDate) ? null : Common.ConvertToSystemDate(ToDate, "dd/MM/yyyy")),
               new SqlParameter("@Fk_MemId",Fk_MemId)

                                     };
            DataSet ds = DBHelper.ExecuteQuery("BookingDetails", para);
            return ds;
        }
    }
}