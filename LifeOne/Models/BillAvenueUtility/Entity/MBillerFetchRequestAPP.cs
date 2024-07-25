using LifeOne.Models.BillAvenues.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace LifeOne.Models.BillAvenueUtility.Entity
{
    public class MBillerFetchRequestAPP
    {
        public long ProjectId { get; set; }
        public long Fk_MemID { get; set; }
        public int EnvId { get; set; } //wheather It is Testing or Main Environment        
        public string AndroidId { get; set; }
        public string DeviceId { get; set; }
        public string AgentId { get; set; }   // it will be provided by Biil Avenue Team       
        public string BillerId { get; set; }
        public string MAC { get; set; }
        public string IP { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerAdhaar { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPan { get; set; }
        public string RequestId { get; set; }

        public string imei { get; set; }
        public string app { get; set; }
        public string os { get; set; }
        public string amount { get; set; }

        public List<input> inputParams { get; set; }

        public MbillerResponse billerResponse { get; set; }
        public additionalInfo additionalInfo { get; set; }

        public amountInfo amountInfo { get; set; }

        public string LatePaymentFee { get; set; }
        public string FixedCharges { get; set; }
        public string AdditionalCharges { get; set; }

        public string PaymentMode { get; set; } //cash, chq, wallet, credit card, debit card here        


        [XmlElement(ElementName = "paymentMethod")]
        public MpaymentMethod PaymentMethod { get; set; }

        [XmlElement(ElementName = "paymentInfo")]
        public MpaymentInfo PaymentInfo { get; set; }

        public List<info> info { get; set; }



    }
   
    public class amountInfo
    {
        public string amount { get; set; }
        public string currency { get; set; }
        public string custConvFee { get; set; }
        public string amountTags { get; set; }

    }
   
}