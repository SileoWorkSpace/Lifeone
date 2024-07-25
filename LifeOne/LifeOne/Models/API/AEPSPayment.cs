using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using LifeOne.Models.API.DAL;

namespace LifeOne.Models.API
{
    public class AEPSPayment
    {
        public string Fk_MemId { get; set; }
        public string ReferenceNo { get; set; }
        public string OrderId { get; set; }
        public string Amount { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string ProcessingCode { get; set; }
        public string AccountBalance { get; set; }
        public string BankResponseMsg { get; set; }
        public string BankResponseMessage { get; set; }
        public string Date { get; set; }
        public string StatusCode { get; set; }
        public string CommissionAmt { get; set; }
        public string GSTAmount { get; set; }
        public string TDSAmount { get; set; }
        public string IsWalletFailed { get; set; }
        public string WalletFailed { get; set; }
        public string Time { get; set; }

        public DataSet AEPSPaymentInsert()
        {
            SqlParameter[] Para = 
                {
                new SqlParameter("@Fk_MemId",Fk_MemId),
                new SqlParameter("@ReferenceNo",ReferenceNo),
                new SqlParameter("@OrderId",OrderId),
                new SqlParameter("@Amount",Amount),
                new SqlParameter("@OrderStatus",OrderStatus),
                new SqlParameter("@PaymentStatus",PaymentStatus),
                new SqlParameter("@ProcessingCode",ProcessingCode),
                new SqlParameter("@AccountBalance",AccountBalance),
                new SqlParameter("@BankResponseMsg",BankResponseMsg),
                new SqlParameter("@BankResponseMessage",BankResponseMessage),
                new SqlParameter("@Date",Date),
                new SqlParameter("@StatusCode",StatusCode),
                new SqlParameter("@CommissionAmt",CommissionAmt),
                new SqlParameter("@GSTAmount",GSTAmount),
                new SqlParameter("@TDSAmount",TDSAmount),
                new SqlParameter("@IsWalletFailed",IsWalletFailed),
                new SqlParameter("@WalletFailed",WalletFailed),
                new SqlParameter("@Time",Time)

            };
            DataSet ds = DBHelper.ExecuteQuery("AEPSPaymentResponseInsert", Para);
            return ds;
        }

    }
    public class AEPSPaymentResponse
    {
        public string response { get; set; }
        public string message { get; set; }
    }


    public class OrderDetailsByUtility
    {
        public string Fk_MemId { get; set; }
        public string LevelId { get; set; }
        public string UtilityName { get; set; }

        public DataSet GetOrderDetails()
        {
            SqlParameter[] Para =
                {
                new SqlParameter("@FK_MemId",Fk_MemId),
                new SqlParameter("@LevelId",LevelId),
                new SqlParameter("@UtilityName",UtilityName)
            };

            DataSet ds = DBHelper.ExecuteQuery("ProcGetOrdersDetailByUtility", Para);
            return ds;
        }
    }



    public class OrderDetails
    {
        public string loginId { get; set; }
        public string transactionId { get; set; }
        public string storeName { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string utilityName { get; set; }
        public string businessAmount { get; set; }
        public string amount { get; set; }
    }

    public class OrderDetailsResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public List<OrderDetails> orderDetailsList { get; set; }
    }


}