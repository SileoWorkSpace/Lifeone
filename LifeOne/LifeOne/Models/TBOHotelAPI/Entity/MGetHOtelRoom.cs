using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.TBOHotelAPI.Entity
{
    public class MGetHOtelRoom
    {
        public string ResultIndex { get; set; }
        public string HotelCode { get; set; }
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string TraceId { get; set; }
    }
    public class Error
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class DayRate
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }

    public class Price
    {
        public string CurrencyCode { get; set; }
        public double RoomPrice { get; set; }
        public int Tax { get; set; }
        public int ExtraGuestCharge { get; set; }
        public int ChildCharge { get; set; }
        public int OtherCharges { get; set; }
        public int Discount { get; set; }
        public double PublishedPrice { get; set; }
        public int PublishedPriceRoundedOff { get; set; }
        public double OfferedPrice { get; set; }
        public int OfferedPriceRoundedOff { get; set; }
        public int AgentCommission { get; set; }
        public int AgentMarkUp { get; set; }
        public double ServiceTax { get; set; }
        public int TDS { get; set; }
    }

    public class CancellationPolicy
    {
        public string BaseCurrency { get; set; }
        public int Charge { get; set; }
        public int ChargeType { get; set; }
        public string Currency { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class HotelRoomsDetail
    {
        public bool RequireAllPaxDetails { get; set; }
        public int RoomIndex { get; set; }
        public string RoomTypeCode { get; set; }
        public string RoomTypeName { get; set; }
        public string RatePlanCode { get; set; }
        public string InfoSource { get; set; }
        public string SequenceNo { get; set; }
        public List<DayRate> DayRates { get; set; }
        public Price Price { get; set; }
        public string RoomPromotion { get; set; }
        public List<string> Amenities { get; set; }
        public string SmokingPreference { get; set; }
        public List<object> BedTypes { get; set; }
        public List<object> Supplements { get; set; }
        public DateTime LastCancellationDate { get; set; }
        public List<CancellationPolicy> CancellationPolicies { get; set; }
        public string CancellationPolicy { get; set; }
        public bool IsPassportMandatory { get; set; }
        public bool IsPANMandatory { get; set; }
        public int? ChildCount { get; set; }
        public object SupplierPrice { get; set; }
    }

    public class RoomCombination
    {
        public List<int> RoomIndex { get; set; }
    }

    public class RoomCombinations
    {
        public string InfoSource { get; set; }
        public List<RoomCombination> RoomCombination { get; set; }
    }

    public class GetHotelRoomResult
    {
        public int ResponseStatus { get; set; }
        public Error Error { get; set; }
        public string TraceId { get; set; }
        public bool IsUnderCancellationAllowed { get; set; }
        public bool IsPolicyPerStay { get; set; }
        public List<HotelRoomsDetail> HotelRoomsDetails { get; set; }
        public RoomCombinations RoomCombinations { get; set; }
    }

    public class MHotelRoomInfoDetails
    {
        public GetHotelRoomResult GetHotelRoomResult { get; set; }
    }

}