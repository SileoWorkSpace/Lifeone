using Dapper;
using LifeOne.Models.Common;
using LifeOne.Models.TBOHotelAPI.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using LifeOne.Models.API;

namespace LifeOne.Models.TBOHotelAPI.DAL
{
  
    public class DALHotelBooking
    {

        public static int SaveHotelBookingRequest(MHotelBookReq _objDetails, MBookResponse _objrequestdetails, string JsonReq, DataTable PassengerDetails)
        {
           
            try
            {
              
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ResultIndex", _objDetails.ResultIndex);
                queryParameters.Add("@HotelCode", _objDetails.HotelCode);
                queryParameters.Add("@HotelName", _objDetails.HotelName);
                queryParameters.Add("@HotelImage", _objDetails.HotelImage ?? string.Empty);
                queryParameters.Add("@CheckinDate", _objDetails.CheckinDate);
                queryParameters.Add("@CheckOutDate", _objDetails.CheckOutDate);
                queryParameters.Add("@CityId", _objDetails.CityId);
                queryParameters.Add("@NoOfAdults", _objDetails.NoOfAdults);
                queryParameters.Add("@NoOfChild", _objDetails.NoOfChild);
                queryParameters.Add("@GuestNationality", _objDetails.GuestNationality);
                queryParameters.Add("@ClientReferenceNo", _objDetails.ClientReferenceNo);
                queryParameters.Add("@IsVoucherBooking", _objDetails.IsVoucherBooking);
                queryParameters.Add("@EndUserIP", _objDetails.EndUserIp);
                queryParameters.Add("@TokenId", _objDetails.TokenId);
                queryParameters.Add("@TraceId", _objDetails.TraceId);
                queryParameters.Add("@Fk_MemId", _objDetails.Fk_MemId);
                queryParameters.Add("@ComPanyId", _objDetails.CompanyId);
                queryParameters.Add("@JsonHotelRequest", JsonReq);
                queryParameters.Add("@RoomIndex", _objDetails.HotelRoomsDetails[0].RoomIndex);
                queryParameters.Add("@RoomTypeCode", _objDetails.HotelRoomsDetails[0].RoomTypeCode);
                queryParameters.Add("@RoomTypeName", _objDetails.HotelRoomsDetails[0].RoomTypeName);
                queryParameters.Add("@RatePlanCode", _objDetails.HotelRoomsDetails[0].RatePlanCode);
                queryParameters.Add("@BedTypeCode", _objDetails.HotelRoomsDetails[0].BedTypeCode);
                queryParameters.Add("@SmokingPreference", _objDetails.HotelRoomsDetails[0].SmokingPreference);
                queryParameters.Add("@Supplements", _objDetails.HotelRoomsDetails[0].Supplements);
                queryParameters.Add("@CurrencyCode", _objDetails.HotelRoomsDetails[0].Price.CurrencyCode);
                queryParameters.Add("@RoomPrice", _objDetails.HotelRoomsDetails[0].Price.RoomPrice);
                queryParameters.Add("@Tax", _objDetails.HotelRoomsDetails[0].Price.Tax);
                queryParameters.Add("@ExtraGuestCharge", _objDetails.HotelRoomsDetails[0].Price.ExtraGuestCharge);
                queryParameters.Add("@ChildCharge", _objDetails.HotelRoomsDetails[0].Price.ChildCharge);
                queryParameters.Add("@OtherCharges", _objDetails.HotelRoomsDetails[0].Price.OtherCharges);
                queryParameters.Add("@Discount", _objDetails.HotelRoomsDetails[0].Price.Discount);
                queryParameters.Add("@PublishedPrice", _objDetails.HotelRoomsDetails[0].Price.PublishedPrice);
                queryParameters.Add("@PublishedPriceRoundedOff", _objDetails.HotelRoomsDetails[0].Price.PublishedPriceRoundedOff);
                queryParameters.Add("@OfferedPrice", _objDetails.HotelRoomsDetails[0].Price.OfferedPrice);
                queryParameters.Add("@OfferedPriceRoundedOff", _objDetails.HotelRoomsDetails[0].Price.OfferedPriceRoundedOff);
                queryParameters.Add("@AgentCommission", _objDetails.HotelRoomsDetails[0].Price.AgentCommission);
                queryParameters.Add("@AgentMarkUp", _objDetails.HotelRoomsDetails[0].Price.AgentMarkUp);
                queryParameters.Add("@ServiceTax", _objDetails.HotelRoomsDetails[0].Price.ServiceTax);
                queryParameters.Add("@TDS", _objDetails.HotelRoomsDetails[0].Price.TDS);
                queryParameters.Add("@BookingId", _objrequestdetails.BookResult.BookingId);
                queryParameters.Add("@IsPriceChanged", _objrequestdetails.BookResult.IsPriceChanged);
                queryParameters.Add("@IsCancellationPolicyChanged", _objrequestdetails.BookResult.IsCancellationPolicyChanged);
                queryParameters.Add("@ResponseStatus", _objrequestdetails.BookResult.ResponseStatus);
                queryParameters.Add("@ErrorCode", _objrequestdetails.BookResult.Error.ErrorCode);
                queryParameters.Add("@ErrorMessage", _objrequestdetails.BookResult.Error.ErrorMessage);
                queryParameters.Add("@PassengerDetails", PassengerDetails);
                int _iresult = DBHelperDapper.DAAdd("dbo.TBOHotelBookingInsert", queryParameters);
                if (_iresult > 0)
                {
                    string _qury = "GetCityById @CityId=" + _objDetails.CityId;
                    CityDetailById res =  DBHelperDapper.DAGetDetails<CityDetailById>(_qury);
                    LifeOne.Models.API.Common.SendMailHotelBooking(PassengerDetails.Rows[0]["Title"].ToString() + " " + PassengerDetails.Rows[0]["FirstName"].ToString() + " " + PassengerDetails.Rows[0]["LastName"].ToString(), _objrequestdetails.BookResult.BookingId, _objDetails.CheckinDate, _objDetails.CheckOutDate, _objDetails.HotelName, res.DESTINATION, PassengerDetails.Rows[0]["Email"].ToString(), _objDetails.HotelRoomsDetails[0].Price.RoomPrice, _objDetails.HotelCode, _objDetails.NoOfAdults, _objDetails.NoOfChild);
                }
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
    }
}