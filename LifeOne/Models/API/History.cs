using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class History
    {
        public class BookedTicket
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

        public class BookedHistoryOutput
        {
            public List<BookedTicket> BookedTickets { get; set; }
        }

        public class BusBookingHistory
        {
            public string ResponseStatus { get; set; }
            public string FailureRemarks { get; set; }
            public BookedHistoryOutput BookedHistoryOutput { get; set; }
        }

        public class CancellationPolicyInput
        {
            public string ResponseStatus { get; set; }
            public string TransportId { get; set; }
            public string TravelDate { get; set; }
            public string ScheduleId { get; set; }
            public string TripId { get; set; }           
        }

    }
}