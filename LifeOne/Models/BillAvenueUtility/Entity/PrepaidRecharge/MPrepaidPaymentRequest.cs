using LifeOne.Models.API;
using LifeOne.Models.BillAvenues.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;



namespace LifeOne.Models.BillAvenueUtility.Entity.PrepaidRecharge
{

    [XmlRoot(ElementName = "billPaymentRequest")]
    public class MPrepaidPaymentRequest
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
        [XmlElement(ElementName = "amountInfo")]
        public MamountInfo AmountInfo { get; set; }
        [XmlElement(ElementName = "paymentMethod")]
        public MpaymentMethod PaymentMethod { get; set; }

        [XmlElement(ElementName = "paymentInfo")]
        public PaymentInfo paymentInfo { get; set; }




    }
    [XmlRoot(ElementName = "paymentInfo")]
    public class PaymentInfo
    {

        [XmlElement(ElementName = "info")]
        public List<Info> Info { get; set; }
    }

    /*response Class*/

    [XmlRoot(ElementName = "ExtBillPayResponse")]
    public class MExtBillPayResponse
    {
        [XmlElement(ElementName = "responseCode")]
        public string responseCode { get; set; }
        [XmlElement(ElementName = "responseReason")]
        public string ResponseReason { get; set; }
        [XmlElement(ElementName = "txnRefId")]
        public string TxnRefId { get; set; }
        [XmlElement(ElementName = "approvalRefNumber")]
        public string ApprovalRefNumber { get; set; }
        [XmlElement(ElementName = "txnRespType")]
        public string TxnRespType { get; set; }
        [XmlElement(ElementName = "inputParams")]
        public inputParams InputParams { get; set; }
        [XmlElement(ElementName = "CustConvFee")]
        public string CustConvFee { get; set; }
        [XmlElement(ElementName = "RespAmount")]
        public string RespAmount { get; set; }
        [XmlElement(ElementName = "RespBillDate")]
        public string RespBillDate { get; set; }
        [XmlElement(ElementName = "RespBillNumber")]
        public string RespBillNumber { get; set; }
        [XmlElement(ElementName = "RespBillPeriod")]
        public string RespBillPeriod { get; set; }
        [XmlElement(ElementName = "RespCustomerName")]
        public string RespCustomerName { get; set; }
        [XmlElement(ElementName = "RespDueDate")]
        public string RespDueDate { get; set; }
        public MErrorInfo errorInfo { get; set; }

    }

}