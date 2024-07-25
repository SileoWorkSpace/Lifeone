using LifeOne.Models.TravelModel.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LifeOne.Models.TravelModel.DAL
{
    public class DABookTicket
    {
        public static DataSet SaveBookingRequestAndResponse(RequestModel _objRequestModel, FlightAPI.BookTicket _objResponseModel)
        {
            try
            {
                Random rnd = new Random();
                string BookingId = Convert.ToString(rnd.Next(1, 100000000));
                FlightAPI.BookTicketAPI obj = DALCommon.CustomDeserializeObjectWithDecryptString<FlightAPI.BookTicketAPI>(_objRequestModel.body);
                Home objHome = new Home();
                objHome.hermespnr = _objResponseModel.BookOutput.FlightTicketDetails[0].HermesPNR;
                objHome.TransactionId = _objResponseModel.BookOutput.FlightTicketDetails[0].TransactionId;
                objHome.Name = _objResponseModel.BookOutput.FlightTicketDetails[0].CustomerDetails.Name;
                objHome.BookingType = _objResponseModel.BookOutput.FlightTicketDetails[0].BookingType;
                objHome.Origin = obj.Origin;
                objHome.Destination = obj.Destination;
                objHome.DepartingDate = obj.DepartingDate;
                objHome.ReturningDate = obj.ReturningDate;
                objHome.Class = obj.Class;
                objHome.Adult = Convert.ToInt16(_objResponseModel.BookOutput.FlightTicketDetails[0].AdultCount);
                objHome.Child = Convert.ToInt16(_objResponseModel.BookOutput.FlightTicketDetails[0].ChildCount);
                objHome.Infant = Convert.ToInt16(_objResponseModel.BookOutput.FlightTicketDetails[0].InfantCount);
                objHome.usertrackid = _objResponseModel.UserTrackId;
                objHome.hermespnr = _objResponseModel.BookOutput.FlightTicketDetails[0].HermesPNR;
                objHome.TransactionId = _objResponseModel.BookOutput.FlightTicketDetails[0].TransactionId;
                objHome.TotalAmount = _objResponseModel.BookOutput.FlightTicketDetails[0].TotalAmount;
                objHome.OtherCharges = _objResponseModel.BookOutput.FlightTicketDetails[0].OtherCharges;
                objHome.AdultCount = Convert.ToInt16(_objResponseModel.BookOutput.FlightTicketDetails[0].AdultCount);
                objHome.ChildCount = Convert.ToInt16(_objResponseModel.BookOutput.FlightTicketDetails[0].ChildCount);
                objHome.InfantCount = Convert.ToInt16(_objResponseModel.BookOutput.FlightTicketDetails[0].InfantCount);
                objHome.BookingType = _objResponseModel.BookOutput.FlightTicketDetails[0].BookingType;
                objHome.TravelType = _objResponseModel.BookOutput.FlightTicketDetails[0].TravelType;
                objHome.IssueDateTime = _objResponseModel.BookOutput.FlightTicketDetails[0].IssueDateTime;
                objHome.bookingremarks = _objResponseModel.BookOutput.FlightTicketDetails[0].BookingRemarks;
                objHome.Title = _objResponseModel.BookOutput.FlightTicketDetails[0].CustomerDetails.Title;
                objHome.Name = _objResponseModel.BookOutput.FlightTicketDetails[0].CustomerDetails.Name;
                objHome.Address = _objResponseModel.BookOutput.FlightTicketDetails[0].CustomerDetails.Address;
                objHome.City = _objResponseModel.BookOutput.FlightTicketDetails[0].CustomerDetails.City;
                objHome.CountryId = _objResponseModel.BookOutput.FlightTicketDetails[0].CustomerDetails.CountryId;
                objHome.ContactNumber = _objResponseModel.BookOutput.FlightTicketDetails[0].CustomerDetails.ContactNumber;
                objHome.EmailId = _objResponseModel.BookOutput.FlightTicketDetails[0].CustomerDetails.EmailId;
                objHome.PinCode = _objResponseModel.BookOutput.FlightTicketDetails[0].CustomerDetails.PinCode;
                objHome.TerminalName = _objResponseModel.BookOutput.FlightTicketDetails[0].TerminalContactDetails.TerminalName;
                objHome.Address1 = _objResponseModel.BookOutput.FlightTicketDetails[0].TerminalContactDetails.Address1;
                objHome.Address2 = _objResponseModel.BookOutput.FlightTicketDetails[0].TerminalContactDetails.Address2;
                objHome.TerminalCity = _objResponseModel.BookOutput.FlightTicketDetails[0].TerminalContactDetails.City;
                objHome.TerminalState = _objResponseModel.BookOutput.FlightTicketDetails[0].TerminalContactDetails.State;
                objHome.TerminalCountry = _objResponseModel.BookOutput.FlightTicketDetails[0].TerminalContactDetails.Country;
                objHome.TerminalContact = _objResponseModel.BookOutput.FlightTicketDetails[0].TerminalContactDetails.ContactNumber;
                objHome.TerminalEmailId = _objResponseModel.BookOutput.FlightTicketDetails[0].TerminalContactDetails.EmailId;
                objHome.currencycode = _objResponseModel.BookOutput.FlightTicketDetails[0].PaymentDetails.CurrencyCode;
                objHome.amount = _objResponseModel.BookOutput.FlightTicketDetails[0].PaymentDetails.Amount;
                objHome.airlinetickettingpcc = _objResponseModel.BookOutput.FlightTicketDetails[0].PaymentDetails.AirlinePaymentDetails.TicketingPCC;
                objHome.airlinepaymenttype = _objResponseModel.BookOutput.FlightTicketDetails[0].PaymentDetails.AirlinePaymentDetails.PaymentType;
                objHome.airlinevendorcode = _objResponseModel.BookOutput.FlightTicketDetails[0].PaymentDetails.AirlinePaymentDetails.VendorCode;
                objHome.cardnumber = _objResponseModel.BookOutput.FlightTicketDetails[0].PaymentDetails.AirlinePaymentDetails.CreditCardDetails.CardNumber;
                objHome.cardtype = _objResponseModel.BookOutput.FlightTicketDetails[0].PaymentDetails.AirlinePaymentDetails.CreditCardDetails.CardType;
                objHome.airlinecode = _objResponseModel.BookOutput.FlightTicketDetails[0].AirlineDetails[0].AirlineCode;
                objHome.airlinepnr = _objResponseModel.BookOutput.FlightTicketDetails[0].AirlineDetails[0].AirlinePNR;
                objHome.airlinename = _objResponseModel.BookOutput.FlightTicketDetails[0].AirlineDetails[0].AirlineName;
                objHome.airlineaddress1 = _objResponseModel.BookOutput.FlightTicketDetails[0].AirlineDetails[0].Address1;
                objHome.airlineaddress2 = _objResponseModel.BookOutput.FlightTicketDetails[0].AirlineDetails[0].Address2;
                objHome.airlineCity = _objResponseModel.BookOutput.FlightTicketDetails[0].AirlineDetails[0].City;
                objHome.airlinecontactnumber = _objResponseModel.BookOutput.FlightTicketDetails[0].AirlineDetails[0].ContactNumber;
                objHome.faxnumber = _objResponseModel.BookOutput.FlightTicketDetails[0].AirlineDetails[0].FaxNumber;
                objHome.airlineemailid = _objResponseModel.BookOutput.FlightTicketDetails[0].AirlineDetails[0].EMailId;
                //objHome.CompanyId = Models.TravelModel.Common.MobileAPIDetails.CompanyId;
                //objHome.TransType = Models.TravelModel.Common.MobileAPIDetails.TransType;

                objHome.CompanyId = obj.CompanyId;
                objHome.TransType = "Flight Booking";

                objHome.BookingId = BookingId;
                objHome.Status = "SUCCESS";
                objHome.BookingBy = "Mobile";
                objHome.FK_MemId = obj.FK_MemID;
                DataTable dtpassengerdetails = new DataTable();
                DataTable dtbookingsegments = new DataTable();

                int card = rnd.Next(52);

                dtpassengerdetails.Columns.Add("TicketNumber");
                dtpassengerdetails.Columns.Add("TransmissionControlNo");
                dtpassengerdetails.Columns.Add("PassengerType");
                dtpassengerdetails.Columns.Add("Title");
                dtpassengerdetails.Columns.Add("FirstName");
                dtpassengerdetails.Columns.Add("LastName");
                dtpassengerdetails.Columns.Add("Age");
                dtpassengerdetails.Columns.Add("IdentityProofId");
                dtpassengerdetails.Columns.Add("IdentityProofNumber");
                dtpassengerdetails.Columns.Add("PersonOrgId");
                dtpassengerdetails.Columns.Add("PassengerId");
                dtbookingsegments.Columns.Add("PassengerId");
                dtbookingsegments.Columns.Add("TicketNumber");
                dtbookingsegments.Columns.Add("FlightNumber");
                dtbookingsegments.Columns.Add("AirCraftType");
                dtbookingsegments.Columns.Add("Origin");
                dtbookingsegments.Columns.Add("OriginAirport");
                dtbookingsegments.Columns.Add("DepartureDateTime");
                dtbookingsegments.Columns.Add("Destination");
                dtbookingsegments.Columns.Add("DestinationAirport");
                dtbookingsegments.Columns.Add("Arrivaldatetime");
                dtbookingsegments.Columns.Add("AirlineCode");
                dtbookingsegments.Columns.Add("ClassCode");
                dtbookingsegments.Columns.Add("ClassCodeDesc");
                dtbookingsegments.Columns.Add("FareBasis");
                dtbookingsegments.Columns.Add("BaggageAllowed");
                dtbookingsegments.Columns.Add("StopOverAllowed");
                dtbookingsegments.Columns.Add("FrequentFlyerId");
                dtbookingsegments.Columns.Add("FrequentFlyerNumber");
                dtbookingsegments.Columns.Add("SpecialServiceCode");
                dtbookingsegments.Columns.Add("TotalTaxAmount");
                dtbookingsegments.Columns.Add("GrossAmount");
                dtbookingsegments.Columns.Add("FlightId");
                for (int i = 0; i < _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails.Count; i++)
                {
                    string passengerid = card + '0' + _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].TicketNumber;

                    dtpassengerdetails.Rows.Add(_objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].TicketNumber, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].TransmissionControlNo, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].PassengerType,
                    _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].Title, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].FirstName, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].LastName,
                    _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].Age, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].IdentityProofId, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].IdentityProofNumber,
                    _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].PersonOrgId, passengerid);

                    for (int j = 0; j < _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments.Count; j++)
                    {

                        dtbookingsegments.Rows.Add(passengerid, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].TicketNumber, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].FlightNumber,
                            _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].AirCraftType, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].Origin, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].OriginAirport,
                            _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].DepartureDateTime, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].Destination, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].DestinationAirport,
                            _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].Arrivaldatetime, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].AirlineCode, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].ClassCode,
                            _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].ClassCodeDesc, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].FareBasis == "" ? "0" : _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].FareBasis, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].BaggageAllowed,
                            _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].StopOverAllowed, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].FrequentFlyerId, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].FrequentFlyerNumber,
                             _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].SpecialServiceCode, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].TotalTaxAmount, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].GrossAmount, _objResponseModel.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].FlightId);
                    }

                }

                objHome.PassengerDetails = dtpassengerdetails;
                objHome.BookingSegments = dtbookingsegments;
                DataSet ds = objHome.SaveFlightResponse();
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static DataSet SaveBookingInCaseOfFailure(RequestModel _objRequestModel, FlightAPI.BookTicket _objResponseModel)
        {
            FlightAPI.BookTicketAPI obj = DALCommon.CustomDeserializeObjectWithDecryptString<FlightAPI.BookTicketAPI>(_objRequestModel.body);
            try
            {
                Random rnd = new Random();
                string BookingId = Convert.ToString(rnd.Next(1, 100000000));
                Home objHome = new Home();
                objHome.bookingremarks = _objResponseModel.FailureRemarks;
                objHome.usertrackid = _objResponseModel.UserTrackId;
                //objHome.CompanyId = Models.TravelModel.Common.MobileAPIDetails.CompanyId;
                //objHome.TransType = Models.TravelModel.Common.MobileAPIDetails.TransType;

                objHome.CompanyId = obj.CompanyId;
                objHome.TransType = "Flight Booking";


                objHome.BookingId = BookingId;
                objHome.FK_MemId = obj.FK_MemID;
                objHome.Status = "FAILED";
                objHome.BookingBy = "Mobile";
                objHome.BookingType = obj.BookInput.BookingType;
                objHome.Origin = obj.Origin;
                objHome.Destination = obj.Destination;
                objHome.DepartingDate = obj.DepartingDate;
                objHome.ReturningDate = obj.ReturningDate;
                objHome.Class = obj.Class;
                objHome.Adult = obj.BookInput.AdultCount;
                objHome.Child = obj.BookInput.ChildCount;
                objHome.Infant = obj.BookInput.InfantCount;
                objHome.AdultCount = obj.BookInput.AdultCount;
                objHome.ChildCount = obj.BookInput.ChildCount;
                objHome.InfantCount = obj.BookInput.InfantCount;
                objHome.usertrackid = _objResponseModel.UserTrackId;
                objHome.IssueDateTime = "";
                objHome.FK_MemId = obj.FK_MemID;
                DataSet ds = objHome.SaveFlightResponse();
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

      }
}