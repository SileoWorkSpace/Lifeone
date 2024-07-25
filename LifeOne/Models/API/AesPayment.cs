using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class AesPayment
    {
        public string AadharNo { get; set; }
        public string BankName { get; set; }
        public string DeviceName { get; set; }
        public string Date { get; set; }
        public string Token { get; set; }
        public string TransNo { get; set; }
        public decimal Amount { get; set; }
        public long Fk_MemId { get; set; }
    }
    public class AesPaymentResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public string Response { get; set; }
    }

    public class AesPaymentHistoryResponse
    {
       public List<AesPayment> result { get; set; }
        public string Message { get; set; }
        public string Response { get; set; }
    }

}