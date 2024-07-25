using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace LifeOne.Models.BillAvenueUtility.Entity
{
  
    [XmlRoot(ElementName = "depositDetailsRequest")]
    public class MBillDepositEnquiryRequest:MCommon
    {
        [XmlElement(ElementName = "fromDate")]
        public string FromDate { get; set; }
        [XmlElement(ElementName = "toDate")]
        public string ToDate { get; set; }
        [XmlElement(ElementName = "transType")]
        public string TransType { get; set; }
        [XmlElement(ElementName = "agents")]
        public Agents Agents { get; set; }
    }


    [XmlRoot(ElementName = "agents")]
    public class Agents
    {
        [XmlElement(ElementName = "agentId")]
        public string AgentId { get; set; }
    }


 

    [XmlRoot(ElementName = "DepositEnquiryResponse")]
    public class MBillDepositEnquiryResponse
    {
        [XmlElement(ElementName = "instituteId")]
        public string InstituteId { get; set; }
        [XmlElement(ElementName = "currentBalance")]
        public string CurrentBalance { get; set; }
        [XmlElement(ElementName = "currency")]
        public string Currency { get; set; }
        [XmlElement(ElementName = "transaction")]
        public Transaction Transaction { get; set; }
        public string responseCode { get; set; }
    }


    [XmlRoot(ElementName = "entry")]
    public class Entry
    {
        [XmlElement(ElementName = "agentId")]
        public string AgentId { get; set; }
        [XmlElement(ElementName = "transactionId")]
        public string TransactionId { get; set; }
        [XmlElement(ElementName = "requestId")]
        public string RequestId { get; set; }
        [XmlElement(ElementName = "amount")]
        public string Amount { get; set; }
        [XmlElement(ElementName = "transType")]
        public string TransType { get; set; }
        [XmlElement(ElementName = "datetime")]
        public string Datetime { get; set; }
    }

    [XmlRoot(ElementName = "transaction")]
    public class Transaction
    {
        [XmlElement(ElementName = "entry")]
        public List<Entry> Entry { get; set; }
    }

}