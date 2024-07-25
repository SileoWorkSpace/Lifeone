using LifeOne.Models.BillAvenues.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace LifeOne.Models.BillAvenueUtility.Entity
{
    namespace LifeOne.Models.BillAvenueUtility.Entity
    {

        [Serializable]
        [XmlRoot("billPaymentRequest")]
        public class MBillerPayRequest
        {

            [XmlElement(ElementName = "agentId")]
            public string AgentId { get; set; }
            [XmlElement(ElementName = "billerAdhoc")]
            public string BillerAdhoc { get; set; }

            [XmlElement(ElementName = "agentDeviceInfo")]
            public AgentDeviceInfo AgentDeviceInfo { get; set; }

            [XmlElement(ElementName = "customerInfo")]
            public CustomerInfo CustomerInfo { get; set; }
            [XmlElement(ElementName = "billerId")]
            public string BillerId { get; set; }

            [XmlElement(ElementName = "inputParams")]
            public inputParams InputParams { get; set; }

            [XmlElement(ElementName = "billerResponse")]
            public MbillerResponse BillerResponse { get; set; }
            [XmlElement(ElementName = "additionalInfo")]
            public MadditionalInfo AdditionalInfo { get; set; }

            [XmlElement(ElementName = "amountInfo")]
            public MamountInfo AmountInfo { get; set; }

            [XmlElement(ElementName = "paymentInfo")]
            public MpaymentInfo PaymentInfo { get; set; }
            [XmlElement(ElementName = "paymentMethod")]
            public MpaymentMethod PaymentMethod { get; set; }
        }



        [Serializable]
        [XmlRoot("paymentMethod")]
        public class MpaymentMethod
        {
            public string paymentMode { get; set; }
            public string quickPay { get; set; }
            public string splitPay { get; set; }
        }
        [Serializable]
        [XmlRoot("paymentInfo")]
        public class MpaymentInfo
        {
            [XmlElement(ElementName = "info")]
            public Info Info { get; set; }
        }


        [Serializable]
        [XmlRoot("inputParams")]

        public class inputParams
        {
            [XmlElement("input")]
            public List<input> input { get; set; }
        }
        [Serializable]
        [XmlRoot("input")]

        public class input
        {
            public string paramName { get; set; }
            public string paramValue { get; set; }
        }
        //[Serializable]
        //[XmlRoot("billFetchResponse")]
        //public class MBillerFetchResponse
        //{
        //    public string responseCode { get; set; }
        //    [XmlElement("inputParams")]
        //    public List<inputParams> inputParams { get; set; }
        //    [XmlElement("billerResponse")]
        //    public MbillerResponse billerResponse { get; set; }
        //    [XmlElement("additionalInfo")]
        //    public additionalInfo additionalInfo { get; set; }

        //}

        //[Serializable]
        //[XmlRoot("billerResponse")]
        //public class MbillerResponse
        //{
        //    [XmlElement(ElementName = "billAmount")]
        //    public string BillAmount { get; set; }
        //    [XmlElement(ElementName = "billDate")]
        //    public string BillDate { get; set; }
        //    [XmlElement(ElementName = "billNumber")]
        //    public string BillNumber { get; set; }
        //    [XmlElement(ElementName = "billPeriod")]
        //    public string BillPeriod { get; set; }
        //    [XmlElement(ElementName = "customerName")]
        //    public string CustomerName { get; set; }
        //    [XmlElement(ElementName = "dueDate")]
        //    public string DueDate { get; set; }
        //    [XmlElement(ElementName = "amountOptions")]
        //    public MbilleramountOptions AmountOptions { get; set; }

        //}
        [Serializable]
        [XmlRoot("amountOptions")]
        public class MbilleramountOptions
        {
            [XmlElement(ElementName = "option")]
            public List<option> Option { get; set; }

        }
        [Serializable]
        [XmlRoot("option")]
        public class option
        {
            [XmlElement(ElementName = "amountName")]
            public string AmountName { get; set; }
            [XmlElement(ElementName = "amountValue")]
            public string AmountValue { get; set; }
        }

        [Serializable]
        [XmlRoot("additionalInfo")]
        public class MadditionalInfo
        {
            [XmlElement(ElementName = "info")]
            public List<Info> Info { get; set; }
        }
        [XmlRoot(ElementName = "info")]
        public class Info
        {
            [XmlElement(ElementName = "infoName")]
            public string InfoName { get; set; }
            [XmlElement(ElementName = "infoValue")]
            public string InfoValue { get; set; }
        }
        [Serializable]
        [XmlRoot("amountInfo")]
        public class MamountInfo
        {
            [XmlElement(ElementName = "amount")]
            public string Amount { get; set; }
            [XmlElement(ElementName = "currency")]
            public string Currency { get; set; }

            [XmlElement(ElementName = "custConvFee")]
            public string CustConvFee { get; set; }
            [XmlElement(ElementName = "amountTags")]
            public string AmountTags { get; set; }


        }

        [Serializable]
        [XmlRoot("ExtBillPayResponse")]
        public class MBillerPayRequestResponse
        {
            public string responseCode { get; set; }
            [XmlElement("errorInfo")]
            public MErrorInfo errorInfo { get; set; }

        }
        [XmlRoot(ElementName = "agentDeviceInfo")]
        public class AgentDeviceInfo
        {
            [XmlElement(ElementName = "initChannel")]
            public string InitChannel { get; set; }
            [XmlElement(ElementName = "ip")]
            public string Ip { get; set; }
            [XmlElement(ElementName = "mac")]
            public string Mac { get; set; }
        }

        [XmlRoot(ElementName = "customerInfo")]
        public class CustomerInfo
        {

            [XmlElement(ElementName = "customerMobile")]
            public string CustomerMobile { get; set; }
            [XmlElement(ElementName = "customerEmail")]
            public string CustomerEmail { get; set; }
            [XmlElement(ElementName = "customerAdhaar")]
            public string CustomerAdhaar { get; set; }

            [XmlElement(ElementName = "customerPan")]
            public string CustomerPan { get; set; }


        }
    }


}