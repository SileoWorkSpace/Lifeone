using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace LifeOne.Models.BillAvenueUtility.Entity.DMTServices
{

    [XmlRoot(ElementName = "dmtTransactionRequest")]
    public class MDmtTransactionRequest : MCommon
    {
        [XmlElement(ElementName = "agentId")]
        public string AgentId { get; set; }
        [XmlElement(ElementName = "initChannel")]
        public string InitChannel { get; set; }
        [XmlElement(ElementName = "requestType")]
        public string RequestType { get; set; }
        [XmlElement(ElementName = "txnId")]
        public string TxnId { get; set; }
        //Transaction Refund OTP
        [XmlElement(ElementName = "uniqueRefId")]
        public string UniqueRefId { get; set; }
        [XmlElement(ElementName = "otp")]
        public string Otp { get; set; }
        //Fund Transfer
        [XmlElement(ElementName = "recipientId")]
        public string RecipientId { get; set; }
        [XmlElement(ElementName = "txnAmount")]
        public string TxnAmount { get; set; }
        [XmlElement(ElementName = "convFee")]
        public string ConvFee { get; set; }
        [XmlElement(ElementName = "txnType")]
        public string TxnType { get; set; }
        [XmlElement(ElementName = "senderMobileNo")]
        public string SenderMobileNo { get; set; }


    }



    [XmlRoot(ElementName = "dmtTransactionResponse")]
    public class DmtTransactionResponse
    {
        [XmlElement(ElementName = "respDesc")]
        public string RespDesc { get; set; }
        [XmlElement(ElementName = "responseCode")]
        public string ResponseCode { get; set; }
        [XmlElement(ElementName = "responseReason")]
        public string ResponseReason { get; set; }
        [XmlElement(ElementName = "txnId")]
        public string TxnId { get; set; }
        [XmlElement("errorInfo")]
        public MErrorInfo errorInfo { get; set; }
        //Transaction Refund OTP
        [XmlElement(ElementName = "refundTxnId")]
        public string RefundTxnId { get; set; }

        //Fund Transfer
        [XmlElement(ElementName = "fundTransferDetails")]
        public FundTransferDetails FundTransferDetails { get; set; }

        [XmlElement(ElementName = "senderMobileNo")]
        public string SenderMobileNo { get; set; }
        [XmlElement(ElementName = "uniqueRefId")]
        public string UniqueRefId { get; set; }


    }
    [XmlRoot(ElementName = "fundTransferDetails")]
    public class FundTransferDetails
    {
        [XmlElement(ElementName = "fundDetail")]
        public FundDetail FundDetail { get; set; }
    }
    [XmlRoot(ElementName = "fundDetail")]
    public class FundDetail
    {
        [XmlElement(ElementName = "uniqueRefId")]
        public string UniqueRefId { get; set; }
        [XmlElement(ElementName = "bankTxnId")]
        public string BankTxnId { get; set; }
        [XmlElement(ElementName = "custConvFee")]
        public string CustConvFee { get; set; }
        [XmlElement(ElementName = "DmtTxnId")]
        public string DmtTxnId { get; set; }
        [XmlElement(ElementName = "impsName")]
        public string ImpsName { get; set; }
        [XmlElement(ElementName = "refId")]
        public string RefId { get; set; }
        [XmlElement(ElementName = "txnAmount")]
        public string TxnAmount { get; set; }
        [XmlElement(ElementName = "txnStatus")]
        public string TxnStatus { get; set; }

    }

}