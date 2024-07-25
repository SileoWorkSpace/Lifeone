using LifeOne.Models.TravelModel.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LifeOne.Models.TravelModel.DAL
{
    public class DABusBooking
    {

        public static DataSet SaveBusBookingRequestAndResponse(RequestModel _objRequestModel, BusModel.GetBookingResponse datalist)
        {
            try
            {
                BusModel.GetBookRequest obj = new BusModel.GetBookRequest();
                Random rnd = new Random();
                string BookingId = Convert.ToString(rnd.Next(1, 100000000));
                SaveBusBookingDetail objsave = new SaveBusBookingDetail();
                objsave.ResponseStatus = datalist.ResponseStatus;
                objsave.UsertrackId = datalist.UserTrackId;
                objsave.FK_MemID = Convert.ToInt16(obj.FK_MemID);
                objsave.Title = obj.BookingInput.BookingCustomerDetails.Title;
                objsave.Name = obj.BookingInput.BookingCustomerDetails.Name;
                objsave.Address = obj.BookingInput.BookingCustomerDetails.Address;
                objsave.CustomerContactNumber = obj.BookingInput.BookingCustomerDetails.ContactNumber;
                objsave.ContactNumber = obj.BookingInput.BookingCustomerDetails.ContactNumber;
                objsave.City = obj.BookingInput.BookingCustomerDetails.City;
                objsave.CountryId = obj.BookingInput.BookingCustomerDetails.CountryId;
                objsave.CustomerEmailId = obj.BookingInput.BookingCustomerDetails.EmailId;
                objsave.EmailId = obj.BookingInput.BookingCustomerDetails.EmailId;
                objsave.IdProofId = obj.BookingInput.BookingCustomerDetails.IdProofId;
                objsave.IdProofNumber = obj.BookingInput.BookingCustomerDetails.IdProofNumber;
                objsave.TotalTickets = obj.BookingInput.BookingDetails.TotalTickets;
                objsave.TotalAmount = obj.BookingInput.BookingDetails.TotalAmount;
                objsave.TransportId = obj.BookingInput.BookingDetails.TransportId;
                objsave.ScheduleId = obj.BookingInput.BookingDetails.ScheduleId;
                objsave.StationId = obj.BookingInput.BookingDetails.StationId;
                objsave.TravelDate = obj.BookingInput.BookingDetails.TravelDate;
                objsave.ServiceTax = datalist.BookingOutput.TicketDetails.ServiceTax;
                objsave.ConvenienceFee = datalist.BookingOutput.TicketDetails.ConvenienceFee;
                objsave.Commission = datalist.BookingOutput.TicketDetails.Commission;
                objsave.CancellationPolicy = datalist.BookingOutput.TicketDetails.CancellationPolicy;
                objsave.TransactionId = datalist.BookingOutput.TicketDetails.PNRDetails.TransactionId;
                objsave.TransportPNR = datalist.BookingOutput.TicketDetails.PNRDetails.TransportPNR;
                objsave.TotalAmount = datalist.BookingOutput.TicketDetails.PNRDetails.TotalAmount;
                objsave.TransportId = obj.BookingInput.BookingDetails.TransportId;
                objsave.TotalTickets = datalist.BookingOutput.TicketDetails.PNRDetails.TotalTickets;
                objsave.BusName = datalist.BookingOutput.TicketDetails.PNRDetails.BusName;
                objsave.Origin = datalist.BookingOutput.TicketDetails.PNRDetails.Origin;
                objsave.Destination = datalist.BookingOutput.TicketDetails.PNRDetails.Destination;
                objsave.TravelDate = datalist.BookingOutput.TicketDetails.PNRDetails.TravelDate;
                objsave.DepartureTime = datalist.BookingOutput.TicketDetails.PNRDetails.DepartureTime;
                objsave.TransportName = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.TransportName;
                objsave.Address1 = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.Address1;
                objsave.Address2 = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.Address2;
                objsave.Address3 = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.Address3;
                objsave.CityNamePinCode = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.CityNamePinCode;
                objsave.ContactNumber = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.ContactNumber;
                objsave.Fax = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.Fax;
                objsave.EmailId = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.EmailId;
                objsave.Website = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.Website;

                DataTable dtPaxdetails = new DataTable();
                dtPaxdetails.Columns.Add("TicketNumber");
                dtPaxdetails.Columns.Add("SeatNo");
                dtPaxdetails.Columns.Add("SeatType");
                dtPaxdetails.Columns.Add("PassengerName");
                dtPaxdetails.Columns.Add("Gender");
                dtPaxdetails.Columns.Add("Age");
                dtPaxdetails.Columns.Add("Fare");
                dtPaxdetails.Columns.Add("BoardingId");
                dtPaxdetails.Columns.Add("Place");
                dtPaxdetails.Columns.Add("Time");
                dtPaxdetails.Columns.Add("Address");
                dtPaxdetails.Columns.Add("LandMark");
                dtPaxdetails.Columns.Add("ContactNumber");
                dtPaxdetails.Columns.Add("DroppingId");
                dtPaxdetails.Columns.Add("DroppingPlace");
                dtPaxdetails.Columns.Add("DroppingTime");
                dtPaxdetails.Columns.Add("DroppingAddress");
                dtPaxdetails.Columns.Add("DroppingLandmark");
                dtPaxdetails.Columns.Add("DroppingContactNumber");
                dtPaxdetails.Columns.Add("ReportingTime");
                dtPaxdetails.Columns.Add("ServiceCharges");
                for (int i = 0; i < datalist.BookingOutput.TicketDetails.PNRDetails.PaxList.Count; i++)
                {


                    dtPaxdetails.Rows.Add
                    (

                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].TicketNumber,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].SeatNo,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].SeatType,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].PassengerName,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].Gender,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].Age,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].Fare,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.BoardingId,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.Place,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.Time,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.Address,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.LandMark,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.ContactNumber,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].DroppingPoints.DroppingId,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].DroppingPoints.Place,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].DroppingPoints.Time,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].DroppingPoints.Address,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].DroppingPoints.LandMark,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].DroppingPoints.ContactNumber,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].ReportingTime,
                    datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].ServiceCharges
                    );


                }
                objsave.PaxList = dtPaxdetails;
                objsave.CompanyId = Models.TravelModel.Common.BusAPIDetails.CompanyId;
                objsave.TransType = Models.TravelModel.Common.BusAPIDetails.TransType;
                objsave.BookingId = BookingId;
                objsave.Status = "SUCCESS";
                objsave.BookedDate = DateTime.Now.ToString();

                DataSet ds = objsave.SaveBusBookDetails();

                string TicketNumber = ""; string SeatNo = ""; string SeatType = ""; string PassengerName = ""; string Gender = ""; string Age = ""; string Fare = ""; string Place = ""; string Time = ""; string Address = ""; string ReportingTime = ""; string Telephone = ""; string StatusFor = "Live"; string Boarding = "";

                string TransportPNR = objsave.TransportPNR;
                string BookedDate = objsave.BookedDate;
                string TransportName = objsave.TransportName;

                string Address1 = objsave.Address1;
                string Address2 = objsave.Address2;
                string Address3 = objsave.Address3;
                string CityNamePinCode = objsave.CityNamePinCode;
                string ContactNumber = objsave.ContactNumber;
                string EmailId = obj.BookingInput.BookingCustomerDetails.EmailId;

                string Website = objsave.Website;
                string BusName = objsave.BusName;
                string TravelDate = objsave.TravelDate;
                string DepartureTime = objsave.DepartureTime;
                string Origin = objsave.Origin;
                string Destination = objsave.Destination;
                string CancellationPolicy = objsave.CancellationPolicy;
                string TotalAmount = objsave.TotalAmount;
                for (int i = 0; i < datalist.BookingOutput.TicketDetails.PNRDetails.PaxList.Count; i++)
                {


                    dtPaxdetails.Rows.Add
                        (

                        TicketNumber += datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].TicketNumber + "<br />",
                        SeatNo += datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].SeatNo + "<br />",
                        PassengerName += datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].PassengerName + "<br />",
                        Gender += datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].Gender + "<br />",
                        Age += datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].Age + "<br />",
                        Fare += datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].Fare + "<br />",
                        Place += datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.Place + "<br />",
                        Time += datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.Time + "<br />",
                        Boarding = datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.Place,
                        Telephone = datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.ContactNumber,
                        ReportingTime = datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].ReportingTime,
                        Address = datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.Address,
                        SeatType = datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].SeatType
                        );


                }
              
                return ds;
            }

              
            catch (Exception)
            {
                throw;
            }
        }
        public static DataSet SaveBusBookingInCaseOfFailure(RequestModel _objRequestModel, FlightAPI.BookTicket datalist)
        {
            try
            {
                BusModel.GetBookRequest obj = new BusModel.GetBookRequest();
                Random rnd = new Random();
                string BookingId = Convert.ToString(rnd.Next(1, 100000000));
                SaveBusBookingDetail objsave = new SaveBusBookingDetail();
                objsave.ResponseStatus = datalist.ResponseStatus;
                objsave.UsertrackId = obj.UserTrackId;
                objsave.FK_MemID = Convert.ToInt16(obj.FK_MemID);
                objsave.Title = obj.BookingInput.BookingCustomerDetails.Title;
                objsave.Name = obj.BookingInput.BookingCustomerDetails.Name;
                objsave.Address = obj.BookingInput.BookingCustomerDetails.Address;
                objsave.ContactNumber = obj.BookingInput.BookingCustomerDetails.ContactNumber;
                objsave.City = obj.BookingInput.BookingCustomerDetails.City;
                objsave.CountryId = obj.BookingInput.BookingCustomerDetails.CountryId;
                objsave.EmailId = obj.BookingInput.BookingCustomerDetails.EmailId;
                objsave.IdProofId = obj.BookingInput.BookingCustomerDetails.IdProofId;
                objsave.IdProofNumber = obj.BookingInput.BookingCustomerDetails.IdProofNumber;
                objsave.TotalTickets = obj.BookingInput.BookingDetails.TotalTickets;
                objsave.TotalAmount = obj.BookingInput.BookingDetails.TotalAmount;
                objsave.TransportId = obj.BookingInput.BookingDetails.TransportId;
                objsave.ScheduleId = obj.BookingInput.BookingDetails.ScheduleId;
                objsave.StationId = obj.BookingInput.BookingDetails.StationId;
                objsave.TravelDate = obj.BookingInput.BookingDetails.TravelDate;
                objsave.CompanyId = Models.TravelModel.Common.BusAPIDetails.CompanyId;
                objsave.TransType = Models.TravelModel.Common.BusAPIDetails.TransType;
                objsave.BookingId = BookingId;
                objsave.Status = "FAILED";
                objsave.BookedDate = DateTime.Now.ToString();

                DataSet ds = objsave.SaveBusBookDetails();
                return ds;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}