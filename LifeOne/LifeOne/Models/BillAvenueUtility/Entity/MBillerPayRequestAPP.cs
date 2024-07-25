using LifeOne.Models.BillAvenues.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.BillAvenueUtility.Entity
{
    public class MBillerPayRequestAPP : MBillerFetchRequestAPP
    {
        public string BillAmount { get; set; }
        public string Amount { get; set; }
        public string BillDate { get; set; }
        public string BillNumber { get; set; }
        public string BillPeriod { get; set; }
        public string CustomerName { get; set; }
        public string DueDate { get; set; }
        public string CustConvFee { get; set; }
        public string PaymentMode { get; set; } //cash, chq, wallet, credit card, debit card here        
        public string QuickPay { get; set; } //Y,N 
        public string SplitPay { get; set; } //Y,N         
        public string LatePaymentFee { get; set; }
        public string FixedCharges { get; set; }
        public string AdditionalCharges { get; set; }
        public string Remarks { get; set; }
        public string Currency { get; set; }
        public string RequestId { get; set; }
        public string BillerAdhoc { get; set; }


    }

    public class MBillerPayRequestAPPQuickPay : MBillerFetchRequestAPP
    {

        public string Amount { get; set; }


        //public string BillAmount { get; set; }
        //public string BillDate { get; set; }        
        //public string BillNumber { get; set; }        
        //public string BillPeriod { get; set; }        
        //public string CustomerName { get; set; }        
        //public string DueDate { get; set; }
        //public string LatePaymentFee { get; set; }
        //public string FixedCharges { get; set; }
        //public string AdditionalCharges { get; set; }


        public string CustConvFee { get; set; }
        public string PaymentMode { get; set; } //cash, chq, wallet, credit card, debit card here        
        public string QuickPay { get; set; } //Y,N 
        public string SplitPay { get; set; } //Y,N         

        public string Remarks { get; set; }
        public string Currency { get; set; }
        public string RequestId { get; set; }
        public string BillerAdhoc { get; set; }

        public List<info> info { get; set; }

    }



}