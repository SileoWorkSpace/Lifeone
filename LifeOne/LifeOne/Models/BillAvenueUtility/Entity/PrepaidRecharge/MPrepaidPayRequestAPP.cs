using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.BillAvenueUtility.Entity.PrepaidRecharge
{
    public class MPrepaidPayRequestAPP: MBillerFetchRequestAPP
    {
        public string Location { get; set; }
        public string MobileNumber { get; set; }
        public string PaymentMode { get; set; } //cash, chq, wallet, credit card, debit card here        
        public string QuickPay { get; set; } //Y,N 
        public string SplitPay { get; set; } //Y,N   
        public string CustConvFee { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
       
        public string BillerAdhoc { get; set; }  //pass the true false here
        public string Remarks { get; set; }  //pass the true false here
        public List<Info> _Objlist { get; set; }

    }
}