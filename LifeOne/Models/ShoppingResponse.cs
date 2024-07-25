using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LifeOne.Models.ShoppingRequest;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System.Data;
using System.Data.SqlClient;
using LifeOne.Models.API.DAL;

namespace LifeOne.Models
{
    public class ShoppingResponse
    {
        #region Shopping API
        public class CategoryResponse
        {
            public List<CategoryRequest> result { get; set; }
            public string response { get; set; }
            public string message { get; set; }
        }
        public class ProductResponse
        {

            public List<ProductRequest> result { get; set; }
            public string response { get; set; }
            public string message { get; set; }
        }
        public class AddressResponse
        {

            public List<AddressRequest> result { get; set; }
            public string response { get; set; }
            public string message { get; set; }
        }
        public class CommonResponse
        {

         
            public string response { get; set; }
            public string message { get; set; }
        }
        public class PlaceOrderResponse
        {


            public string response { get; set; }
            public string message { get; set; }
            public string OrderId { get; set; }
            public long FK_OrderId { get;  set; }
        }
        public class GetOrderListResponse
        {
            public string response { get; set; }
            public string message { get; set; }

            public List<GetOrdersRequest> result { get; set; }
        }
        public class ShoppingOrderDetailsResponse
        {
            public string response { get; set; }
            public string message { get; set; }

            public List<ShoppingOrderDetailsRequest> result { get; set; }
        }
        public class PayoutDetailsResponse
        {
            public string response { get; set; }
            public string message { get; set; }

            public List<PayoutDetailsRequest> result { get; set; }
        }
        #endregion ShoppingAPI
        #region Premium Club API
        public class Premium_ClubMembers
        {

            public List<PremiumClubMembers> result { get; set; }
            public string response { get; set; }
            public string message { get; set; }
        } 
        public class Premium_ClubQualifier
        {

            public List<PremiumClubQualifier> result { get; set; }
            public string response { get; set; }
            public string message { get; set; }
        }
        #endregion  Premium Club API

      
        public class APayoutReport
        {

            public List<AssociatePayoutReport> result { get; set; }
            public string response { get; set; }
            public string message { get; set; }
        }

        public class APayoutTypeReport
        {

            public List<AssociatePayoutTypeReport> result { get; set; }
            public string response { get; set; }
            public string message { get; set; }
        }
        public class QrCodeResponse
        {
            public string response { get; set; }
            public string message { get; set; }
        }
        public class RecoginationResponse
        {
           
            public string response { get; set; }
            public string message { get; set; }
            public List<Recogination> recoginationLst { get; set; }
        }
        public class RewardResponse
        {

            public string response { get; set; }
            public string FkSetRewardId { get; set; }
            public string Action { get; set; }
            public string message { get; set; }
            public List<Reward> rewardLst { get; set; }
        }
        public class Reward
        {
            public string RewardName { get; set; }
            public string RewardAmount { get; set; }
            public string LeftBusiness { get; set; }
            public string BalanceLeft { get; set; }
            public string RightBusiness { get; set; }
            public string BalanceRight { get; set; }
            public string Status { get; set; }
            public string AchievedOn { get; set; }
            public string FkSetRewardId { get; set; }
            public string TargetPoint { get;  set; }
        }
        public class Recogination
        {
            public string RecognitionName { get; set; }
            public string AchievedOn { get; set; }
            public string ImageURL { get; set; }
            public string RecogStatus { get; set; }
            public string TargetPoint { get; set; }
        }
        public class BusinessSummaryResponse
        {

           
            public string TotalSponsoring { get; set; }
            public string ActiveSponsoring { get; set; }
            public string InactiveSponsoring { get; set; }
            public string DirectBv { get; set; }
            public string LeftBV { get; set; }
            public string RightBV { get; set; }
            public string MatchingBV { get; set; }
            public string DirectBusiness { get; set; }
            public string LeftBVRepurchase { get; set; }
            public string RightBVRepurchase { get; set; }
            public string MatchingBVRepurchase { get; set; }
            public List<DirectSponsoring> directSponsorings { get; set; }
            public string response { get; set; }
            public string message { get; set; }
        }
        public class CashFreeOrderResponse
        {
            public int code { get;  set; }
            public string message { get;  set; }
            public string OrderId { get;  set; }
            public string FK_OrderId { get;  set; }
        }
        public class Settlements
        {
            public string url { get; set; }
        }
        public class CustomerDetails
        {
            public string customer_id { get; set; }
            public object customer_name { get; set; }
            public string customer_email { get; set; }
            public string customer_phone { get; set; }
        }

        public class OrderMeta
        {
            public object return_url { get; set; }
            public object notify_url { get; set; }
            public object payment_methods { get; set; }
        }

        public class Payments
        {
            public string url { get; set; }
        }

        public class Refunds
        {
            public string url { get; set; }
        }
    }
}