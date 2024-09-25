
using System.Data.SqlClient;
using System.Data;
using System;

namespace LifeOne.Models
{
    public class Razorpay
    {

    }
    public class RazCreateOrderResponse
    {
        public RazCreateOrderRes Response { get; set; }
        public int? Status { get; set; }
        public string Message { get; set; }
    }
    public class RazCreateOrderRes
    {
        public string OrderId { get; set; }
    }
    public class RazCreateOrder
    {
        public string Fk_MemId { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMode { get; set; }
        public string Type { get; set; }
        public string OrderId { get; set; }
        public string Request { get; set; }
        public string RechargeNo { get; set; }
        public DataSet CreateRazOrder()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@Fk_MemId",Fk_MemId),
                                      new SqlParameter("@TotalAmount",TotalAmount),
                                      new SqlParameter("@PaymentMode",PaymentMode),
                                      new SqlParameter("@Type",Type),
                                      new SqlParameter("@OrderId",OrderId),
                                      new SqlParameter("@Request",Request),
                                      new SqlParameter("@RechargeNo",RechargeNo)
                                  };

                DataSet ds = Connection.ExecuteQuery("CreateRazOrder", para);
                return ds;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}