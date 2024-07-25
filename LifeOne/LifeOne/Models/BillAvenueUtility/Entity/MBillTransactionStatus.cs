using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace LifeOne.Models.BillAvenueUtility.Entity
{
    [Serializable]
    [XmlRoot("transactionStatusReq")]
    public class MBillTransactionStatus: MCommon
    {
        public string trackType { get; set; }
        public string trackValue { get; set; }
        public string BillerId { get; set; }
        public string RequestId { get; set; }
    }
    [Serializable]
    [XmlRoot("transactionStatusResp")]
    public class MBillTransactionStatusResponse
    {
        public string responseCode { get; set; }
        public string responseReason { get; set; }
        [XmlElement("txnList")]
        public TxnList txnList { get; set; }

        [XmlElement("errorInfo")]
        public MErrorInfo errorInfo { get; set; }
        public string RequestId { get; set; }
    }


    [Serializable]
    [XmlRoot("TxnList")]
    public class TxnList
    {
        public string agentId { get; set; }
        public string amount { get; set; }
        public string billerId { get; set; }
        public DateTime txnDate { get; set; }
        public string txnReferenceId { get; set; }
        public string txnStatus { get; set; }
    }

    
}