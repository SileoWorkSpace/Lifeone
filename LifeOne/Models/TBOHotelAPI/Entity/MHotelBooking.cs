using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.TBOHotelAPI.Entity
{
    public class MHotelBooking
    {
    }

    public class MHotelBookReq
    {
        public string ResultIndex { get; set; }
        public string HotelCode { get; set; }
        public string HotelName { get; set; }
        public string HotelImage { get; set; }
        public string GuestNationality { get; set; }
        public string NoOfRooms { get; set; }
        public string ClientReferenceNo { get; set; }
        public string IsVoucherBooking { get; set; }
        public List<BookHotelRoomsDetail> HotelRoomsDetails { get; set; }
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string TraceId { get; set; }
        public long Fk_MemId { get; set; }
        public long CompanyId { get; set; }
        public string CheckinDate { get; set; }
        public string CheckOutDate { get; set; }
        public int CityId { get; set; }
        public int NoOfAdults { get; set; }
        public int NoOfChild { get; set; }
    }

    public class BookPrice
    {
        public string CurrencyCode { get; set; }
        public string RoomPrice { get; set; }
        public string Tax { get; set; }
        public string ExtraGuestCharge { get; set; }
        public string ChildCharge { get; set; }
        public string OtherCharges { get; set; }
        public string Discount { get; set; }
        public string PublishedPrice { get; set; }
        public string PublishedPriceRoundedOff { get; set; }
        public string OfferedPrice { get; set; }
        public string OfferedPriceRoundedOff { get; set; }
        public string AgentCommission { get; set; }
        public string AgentMarkUp { get; set; }
        public string ServiceTax { get; set; }
        public string TDS { get; set; }
    }

    public class HotelPassenger
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string Middlename { get; set; }
        public string LastName { get; set; }
        public string Phoneno { get; set; }
        public string Email { get; set; }
        public int PaxType { get; set; }
        public bool LeadPassenger { get; set; }
        public int Age { get; set; }
        public string PassportNo { get; set; }
        public DateTime? PassportIssueDate { get; set; }
        public DateTime? PassportExpDate { get; set; }
        public string PAN { get; set; }
    }

    public class BookHotelRoomsDetail
    {
        public string RoomIndex { get; set; }
        public string RoomTypeCode { get; set; }
        public string RoomTypeName { get; set; }
        public string RatePlanCode { get; set; }
        public object BedTypeCode { get; set; }
        public int SmokingPreference { get; set; }
        public string Supplements { get; set; }
        public BookPrice Price { get; set; }
        public List<HotelPassenger> HotelPassenger { get; set; }
    }


    public class BookResult
    {
        public bool VoucherStatus { get; set; }
        public int ResponseStatus { get; set; }
        public Error Error { get; set; }
        public string TraceId { get; set; }
        public int Status { get; set; }
        public string HotelBookingStatus { get; set; }
        public string ConfirmationNo { get; set; }
        public string BookingRefNo { get; set; }
        public int BookingId { get; set; }
        public bool IsPriceChanged { get; set; }
        public bool IsCancellationPolicyChanged { get; set; }
    }

    public class MBookResponse
    {
        public BookResult BookResult { get; set; }
    }

    public class BookingHistoryReq
    {
        public int BookingId { get; set; }
        public string HotelName { get; set; }
        public string HotelCode { get; set; }
        public long Fk_MemId { get; set; }
        public long CompanyId { get; set; }
    }
    public class BookingHistoryList
    {
        public int BookingId { get; set; }
        public string HotelCode { get; set; }
        public string HotelName { get; set; }
        public string HotelImage { get; set; }
        public string DESTINATION { get; set; }
        public string CheckinDate { get; set; }
        public string CheckOutDate { get; set; }
        public string BookingDate { get; set; }
    }
    public class BookingHistoryResponse
    {
        public string Response { get; set; }
        public List<BookingHistoryList> Lst { get; set; }
    }

    public class CityDetailById
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
        public string DESTINATION { get; set; }
    }
}