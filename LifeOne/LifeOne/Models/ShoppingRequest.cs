using LifeOne.Models.API.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models
{
    public class ShoppingRequest
    {
        public class CategoryRequest
        {
            public long Pk_CategoryId { get; set; }
            public string CategoryName { get; set; }
            public string ImageUrl { get; set; }
            public bool IsActive { get; set; }

        }
        public class ProductRequest
        {
            public long Pk_ProductId { get; set; }
            public long Fk_CategoryId { get; set; }
            public string ProductName { get; set; }
            public string ProductImage { get; set; }
            public string ProductDescription { get; set; }
            public bool IsActive { get; set; }
            public string CategoryName { get; set; }
            public decimal MRP { get; set; }
            public decimal OfferedPrice { get; set; }
            public decimal DP { get; set; }
            public decimal BV { get; set; }


        }
        public class AddressRequest
        {
            public long Fk_MemId { get; set; }
            public long Pk_AddressId { get; set; }
            public string Name { get; set; }
            public string MobileNo { get; set; }
            public string Landmark { get; set; }
            public string Address { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string PinCode { get; set; }
            public string AddressType { get; set; }
            public bool IsDefault { get; set; }


        }
        public class UpdatePlaceOrderStatus
        {
            public string paymentId { get; set; }
            public string Status { get; set; }
            public long OrderID { get; set; }
            public int OpCode { get; set; }
            public DataSet UpdateStatus()
            {

                try
                {
                    SqlParameter[] para = {
                                        new SqlParameter("@FK_OrderId", OrderID),
                                        new SqlParameter("@Status", Status),
                                        new SqlParameter("@paymentId", paymentId),
                                        new SqlParameter("@OpCode", OpCode)


                 };
                    DataSet ds = DBHelper.ExecuteQuery("PlaceShoppingOrder", para);
                    return ds;

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

        }
        public class PlaceShoppingOrderRequest
        {
            public long Fk_MemId { get; set; }
            public int OpCode { get; set; }
            public long Fk_AddressId { get; set; }
            public long FK_OrderId { get; set; }

            public string Type { get; set; }
            public string OrderIdRazorpay { get; set; }

            public DataTable dtProductDetails { get; set; }
            public DataTable dtPaymentDetails { get; set; }

            public List<ProductDetails> ProductDetails { get; set; }
            public List<PaymentDetails> PaymentDetails { get; set; }

            public DataSet PlaceOrder()
            {

                try
                {
                    SqlParameter[] para = {
                                        new SqlParameter("@FK_MemId", Fk_MemId),
                                        new SqlParameter("@FK_AddressId", Fk_AddressId),
                                        new SqlParameter("@dtProductDetails", dtProductDetails),
                                        new SqlParameter("@dtPaymentDetails", dtPaymentDetails),
                                        new SqlParameter("@OrderId", OrderIdRazorpay),
                                        new SqlParameter("@OpCode", OpCode),
                                        new SqlParameter("@FK_OrderId", FK_OrderId)


                 };
                    DataSet ds = DBHelper.ExecuteQuery("PlaceShoppingOrder", para);
                    return ds;

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

        }
        public class ProductDetails
        {
            public long Fk_ProductId { get; set; }
            public decimal Amount { get; set; }
            public decimal Quantity { get; set; }
        }
        public class PaymentDetails
        {
            public string PaymentMode { get; set; }
            public decimal Amount { get; set; }
        }
        public class GetOrdersRequest
        {
            public long Fk_OrderId { get; set; }
            public long Fk_MemId { get; set; }
            public long OpCode { get; set; }
            public string OrderNo { get; set; }
            public string Name { get; set; }
            public string MobileNo { get; set; }
            public string Address { get; set; }
            public string Landmark { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string PinCode { get; set; }
            public string OrderDate { get; set; }
            public string OrderStatus { get; set; }
            public decimal GatewayAmount { get; set; }
            public decimal WalletAmount { get; set; }
        }
        public class ShoppingOrderDetailsRequest
        {
            public long FK_OrderId { get; set; }
            public long OpCode { get; set; }
            public string ProductName { get; set; }
            public string ProductImage { get; set; }
            public decimal Amount { get; set; }
            public long Quantity { get; set; }
            public decimal FinalAmount { get; set; }
            public decimal GatewayAmount { get; set; }
            public decimal WalletAmount { get; set; }
            public string Name { get; set; }
            public string MobileNo { get; set; }
            public string Address { get; set; }
            public string Landmark { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string PinCode { get; set; }
            public string OrderNo { get; set; }
            public string OrderDate { get; set; }
            public string OrderStatus { get; set; }
            public string ShippingId { get; set; }
            public string token { get; set; }


        }

        public class PayoutDetailsRequest
        {
            public string PayoutNo { get; set; }
            public string LoginId { get; set; }
            public string FirstName { get; set; }
            public string Amount { get; set; }
            public string Date { get; set; }
            public long Fk_MemId { get; set; }
            public long Fk_PlanId { get; set; }

            public string ClosingDate { get; set; }

            public decimal DirectIncome { get; set; }
            public decimal MatchingBonus { get; set; }
            public decimal LeaderShipBonus { get; set; }
            public decimal HybridMatchingBonus { get; set; }
            public decimal BinaryIncome { get; set; }
            public decimal LDBRepurchase { get; set; }
            public decimal CarFund { get; set; }
            public decimal HouseFund { get; set; }
            public decimal DirectorClub { get; set; }
            public decimal LARBonus { get; set; }
            public decimal SponsorRoyalty { get; set; }
            public decimal GrossAmount { get; set; }
            public decimal TDSAmount { get; set; }
            public decimal ProcessingFee { get; set; }
            public decimal NetAmount { get; set; }
            public string ChequeNo { get; set; }
            public string ChaqueDate { get; set; }
            public string BankName { get; set; }
            public decimal PrevLeft { get; set; }
            public decimal PrevRight { get; set; }
            public decimal CurrLeft { get; set; }
            public decimal CurrRight { get; set; }
            public decimal BalLeft { get; set; }
            public decimal BalRight { get; set; }
            //public string CarFund { get; set; }
            //public string HouseFund { get; set; }
            //public string DirectorClub { get; set; }
            public decimal ChairManClub { get; set; }
            //public string LARBonus { get; set; }
            //public string Date { get; set; }

        }
        public class BusinessSummaryRequest
        {
            public int Fk_MemId { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }

            public DataSet GetBusinessSummary()
            {
                SqlParameter[] para = {
                                        new SqlParameter("@Fk_MemId", Fk_MemId),
                                        new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", ToDate)

                                     };
                DataSet ds = DBHelper.ExecuteQuery("GetBusinessSummaryForAssociate", para);
                return ds;
            }


        }
        public class RecoginationRegest
        {
            public int Fk_MemId { get; set; }
            

            public DataSet GetRecognition()
            {
                SqlParameter[] para = {
                                        new SqlParameter("@Fk_MemId", Fk_MemId)
                                      
                                     };
                DataSet ds = DBHelper.ExecuteQuery("GetRecognition", para);
                return ds;
            }


        }
        public class RewardRegest
        {
            public int Fk_MemId { get; set; }
            public int FkSetRewardId { get; set; }
            public string Action { get; set; }


            public DataSet Rewards()
            {
                SqlParameter[] para = {
                                        new SqlParameter("@FKMemId", Fk_MemId),
                                         new SqlParameter ("@Fk_SetRewardId",FkSetRewardId),
                                         new SqlParameter ("@Action",Action)

                                     };
                DataSet ds = DBHelper.ExecuteQuery("webGetRewardAchiever", para);
                return ds;
            }


        }
        public class CreateOrderCashFreeRequest
        {
            public int? Fk_MemId { get; set; }
            public decimal? Amount { get; set; }
            public int? IsTesting { get; set; }
            public string OrderId { get; set; }
            public string Type { get; set; }


            public DataSet CreateOrder()
            {
                SqlParameter[] para = {
                                        new SqlParameter("@Fk_MemId", Fk_MemId),
                                        new SqlParameter("@Amount", Amount),
                                        new SqlParameter("@IsTesting", IsTesting),
                                        new SqlParameter("@OrderId", OrderId),
                                        new SqlParameter("@Type", Type)


                                     };
                DataSet ds = DBHelper.ExecuteQuery("CreateOrder", para);
                return ds;
            }


        }
        public class order_meta
        {
            public string notify_url { get; set; }
        }
        public class CustomerDetails
        {
            public string customer_id { get; set; }
            public string customer_email { get; set; }
            public string customer_phone { get; set; }
            public string customer_name { get; set; }
        }

        public class DirectSponsoring
        {
            public string LoginId { get; set; }
            public string Name { get; set; }
            public string PackageName { get; set; }
            public string FLoginId { get; set; }
            public string FranchiseName { get; set; }
            public string TopupDate { get; set; }
            public string JoiningDate { get; set; }
            public string PayMentMode { get; set; }
            public string Amount { get; set; }
            public string BV { get; set; }
        }


    }
}