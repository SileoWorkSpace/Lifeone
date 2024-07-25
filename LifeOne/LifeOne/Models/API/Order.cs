using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class Order
    {

    }
    public class JsonOrderRequest
    {
        public string orderId { get; set; }
        public string productName { get; set; }
        public decimal productPrice { get; set; }
        public string paymentMode { get; set; }
        public string paymentDate { get; set; }
        public long  memberId { get; set; }
        public decimal payout { get; set; }
        public decimal mainamount { get; set; }
        public decimal walletamount { get; set; }
        public decimal paymentgatewayamount { get; set; }
        public string transactionId { get; set; }
    }

    public class PaypalToWallet
    {
        public long Fk_MemId { get; set; }
        public decimal Amount { get; set; }
        public string InvoiceNo { get; set; }        
        public string Type { get; set; }
        public string OrderId { get; set; }
        public string TransId { get; set; }
        public string Status { get;  set; }


    }

    public class AuditionResponse
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
        public string OrderId { get; set; }
        public string Response { get; set; }
        public string Status { get; set; }
    }

    public class Cartitem
    {

        public string productcode { get; set; }
        public string productname { get; set; }
        public decimal price { get; set; }
        public string producttype { get; set; }       
        public int quatity { get; set; }
        public string image { get; set; }    
        public decimal cashbackper { get; set; }
        public decimal BV { get; set; }
        public decimal DP { get; set; }
        public decimal PV { get; set; }
        public decimal BP { get; set; }

       
        public int ? OptionTypeId { get; set; }
        public int ? OptionId { get; set; }

        public int? PrdId { get; set; }
        

    }
    





    public class CardItemRequest
    {
        public List<Cartitem> cartitems { get; set; }
        public string token { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal cashbackAmount { get; set; }
        public decimal WalletAmount { get; set; }
        public decimal gatewayAmount { get; set; }
        public decimal donation { get; set; }
        public string transactionNo { get; set; }
        public int ProcId { get; set; }
        public long Fk_MemID { get; set; }
        public long orderId { get; set; }
        public string xmldata { get; set; }
        public string OrderNo { get; set; }
        public string method { get; set; }
        public string shippingAddress { get; set; }
        public string billingAddress { get; set; }
        public string CourierCharges { get; set; }
        public string ShoppingFrom { get; set; }
        public string CropCode { get; set; }
        public decimal? Area { get; set; }
        public string SowingDate { get; set; }
        public string OrderIdRazorpay { get; set; }

    }

    public class RecentOrder
    {
        public decimal Amount { get; set;}

        public string OrderId { get; set; }

        public string Type { get; set; }

        public string OrderDate { get; set; }

        public string ActionType { get; set; }
    }

    public class DashboardDetails
    {
        public decimal AllBusiness { get; set; }

        public decimal TodayBusiness { get; set; }

        public decimal TeamBusiness { get; set; }

        public long Teamsize { get; set; }

        public decimal DreamyWallet { get; set; }

         public decimal CashbackWallet { get; set; }

        public decimal CommissionWallet { get; set; }

        public decimal AllOrders { get; set; }

        public decimal MagixMoves { get; set; }

        public decimal LifeTimeIncome { get; set; }

        public decimal ReferralIncome { get; set; }

        public decimal RepurchaseIncomeSelf { get; set; }

        public decimal RepurchaseIncomeTeam { get; set; }
    }

    public class DashboardResponse:CommonJsonReponse
    {
        public DashboardDetails dashboardDetails { get; set; }
        public List<RecentOrder> Order { get; set; }
    }

    public class DashboardResponseV2 : CommonJsonReponse
    {
        public DashboardDetailsV2 dashboardDetails { get; set; }
        public List<RecentOrder> Order { get; set; }
    }

    public class DashboardDetailsV2
    {
        public decimal AllBusiness { get; set; }

        public decimal TodayBusiness { get; set; }

        public decimal TeamBusiness { get; set; }

        public long Teamsize { get; set; }

        public decimal DreamyWallet { get; set; }

        public decimal CashbackWallet { get; set; }

        public decimal CommissionWallet { get; set; }

        public long AllOrders { get; set; }
        public long TodayOrders { get; set; }

        public decimal MagixMoves { get; set; }

        public decimal LifeTimeIncome { get; set; }

        public decimal ReferralIncome { get; set; }

        public decimal RepurchaseIncomeSelf { get; set; }

        public decimal RepurchaseIncomeTeam { get; set; }
        public decimal Gross { get; set; }
        public decimal TDS { get; set; }
        public decimal ServiceCharges { get; set; }
        public decimal NetIncome { get; set; }
        public long DirectCount { get; set; }
    }
    public class GetKYCStatus
    {
        public string Response { get; set; }
        public string Status { get; set; }

    }

    public class SupportType
    {
        public int PK_SupportId { get; set; }
        public string SupportName { get; set; }
        public bool IsActive { get; set; }
    }

    public class GetAllOrderId
    {
        public long Fk_MemId { get; set; }
    }

    public class SupportData: CommonJsonReponse
    {
       public List<SupportType> Item { get; set; }
    }

    public class SupportRequest
    {
        public int Fk_SupportId { get; set; }
          public long Fk_MemId { get; set; }
          public string Message { get; set; }
         public string Subject { get; set; }
    }

    public class SupportResponse:CommonJsonReponse
    {
        public int Flag { get; set; }

        public string TicketNo { get; set; }



    }

    public class GetToken
    {
        public string Token { get; set; }
    }

    public class BillDeskPayment
    {
        public long Fk_MemId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentId { get; set; }       
        public string Status { get; set; }
    }

    public class AllPaymentDetailsRequest
    {

        public string response { get; set; }
        public long Fk_Memid { get; set; }
        public int Action { get; set; }
        public string Status { get; set; }        
        public decimal Amount { get; set; }
        public string PaymentId { get; set; }
        public long Pk_PaymentId { get; set; }
        public string OrderId { get; set; }
        public string Type { get; set; }
        public List<AllPaymentDetailsRequest> result { get; set; }
    }
    public class AllPaymentDetailsResponse
    {
        public long Pk_PaymentId { get; set; }
        public int flag { get; set; }
        public string Message { get; set; }
        public string Response { get; set; }
        public string OrderId { get; set; }
        public string Status { get; set; }

    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Card
    {
        public object channel { get; set; }
        public string card_number { get; set; }
        public string card_network { get; set; }
        public string card_type { get; set; }
        public string card_country { get; set; }
        public string card_bank_name { get; set; }
    }

    public class CustomerDetails
    {
        public string customer_name { get; set; }
        public string customer_id { get; set; }
        public string customer_email { get; set; }
        public string customer_phone { get; set; }
    }

    public class DataCashFree
    {
        public OrderCashFree order { get; set; }
        public PaymentCashFree payment { get; set; }
        public CustomerDetails customer_details { get; set; }
    }

    public class OrderCashFree
    {
        public string order_id { get; set; }
        public double order_amount { get; set; }
        public string order_currency { get; set; }
        public object order_tags { get; set; }
    }

    public class PaymentCashFree
    {
        public int cf_payment_id { get; set; }
        public string payment_status { get; set; }
        public double payment_amount { get; set; }
        public string payment_currency { get; set; }
        public string payment_message { get; set; }
        public DateTime payment_time { get; set; }
        public string bank_reference { get; set; }
        public object auth_id { get; set; }
        public PaymentMethod payment_method { get; set; }
        public string payment_group { get; set; }
    }

    public class PaymentMethod
    {
        public Card card { get; set; }
    }

    public class CashFreeWebHook
    {
        public DataCashFree data { get; set; }
        public DateTime event_time { get; set; }
        public string type { get; set; }
    }



    public class CheckTransaction
    {
        public string Msg { get; set; }
        public int Flag { get; set; }
        public long Fk_Memid { get; set; }
        public decimal Amount { get; set; }
        public string PaymentDate { get; set; }
        public string Status { get; set; }
        public string OrderID { get; set; }
        public string PaymentId { get; set; }
        public string Mobile { get; set; }
        public string Type { get; set; }
        public long Fk_OrderId { get; set; }

    }

   


}