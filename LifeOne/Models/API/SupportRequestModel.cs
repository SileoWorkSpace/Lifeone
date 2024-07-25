using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class SupportRequestModel
    {
        public string MessageId { get; set; }

        public string Fk_MemId { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
        public string LoginId { get; set; }

        public string Mobile { get; set; }
        public string Path { get; set; }

        //public DataSet GetSupportMessageById()
        //{
        //    SqlParameter[] param =
        //        {
        //                new SqlParameter ("@MessageId",MessageId)

        //            };
        //    DataSet dsResult = ExecuteQuery("GetSupportMessageById", param);
        //    return dsResult;
        //}
    }

    public class SupportJsonJResponse
    {
        public List<Support> result { get; set; }
        public string response { get; set; }
        public string message { get; set; }
    }

    public class Support
    {
        public string Message { get; set; }
        public string Subject { get; set; }
        public string TicketNumber { get; set; }
        public string Status { get; set; }
        public string Path { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public int SupportId { get; set; }

    }


    public class MSupportHistoryModel
    {
        public int SupportId { get; set; }
        public int HistoryID { get; set; }
        public int MsgBy { get; set; }
        public int Flag { get; set; }
        public long Fk_MemId { get; set; }
        public string Msg { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string RevertBy { get; set; }
        public string Remark { get; set; }
        public string LoginId { get; set; }
        public string Fk_MemLoginId { get; set; }
        public string MemberProfile { get; set; }
        public string SupportProfile { get; set; }
        public string SupportName { get; set; }
        public string CreateDate { get; set; }
        public bool IsClosed { get; set; }
      
    }
    public class ResponseSupportHistory
    {
        public string response { get; set; }
        public string message { get; set; }
        public List<MSupportHistoryModel> result { get; set; }
    }
    public class OrdersJsonJResponse
    {
        public List<Orders> result { get; set; }
        public string response { get; set; }
        public string message { get; set; }
    }

    public class Orders
    {
        public string OrderNo { get; set; }
        public string TransactionNo { get; set; }
        public string Status { get; set; }
        public string OrderStatus { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public long PK_OrderId { get; set; }
        public long FK_MemId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal cashbackAmount { get; set; }
        public decimal dreamyAmount { get; set; }
        public decimal gatewayAmount { get; set; }
        public string OrderDate { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }

    }
    public class OrdersDetailsJsonJResponse
    {
        public List<OrdersDetails> result { get; set; }
        public string response { get; set; }
        public string message { get; set; }
    }

    public class OrdersDetails
    {
        public string OrderNo { get; set; }
        public string ProductName { get; set; }
        public string Status { get; set; }
        public string OrderStatus { get; set; }
        public string ImageUrl { get; set; }
        public decimal Amount { get; set; }
        public long FK_OrderId { get; set; }
        public long FK_MemId { get; set; }
        public int Quatity { get; set; }
        public string Producttype { get; set; }
        public decimal CashbackPer { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal cashbackAmount { get; set; }
        public decimal WalletAmount { get; set; }
        public decimal gatewayAmount { get; set; }
        public string OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public string TransactionNo { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public decimal CourierCharges { get; set; }
        public string InvoiceUrl { get; set; }
    }
}