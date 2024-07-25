using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace LifeOne.Models.BillAvenueUtility.Entity
{
    [XmlRoot(ElementName = "mnpRequest")]
    public class MnpRequest : MCommon
    {
        [XmlElement(ElementName = "agentId")]
        public string AgentId { get; set; }
        [XmlElement(ElementName = "mobileNo")]
        public string MobileNo { get; set; }
    }
    [XmlRoot(ElementName = "mnpResponse")]
    public class MnpResponse
    {
        [XmlElement(ElementName = "responseCode")]
        public string ResponseCode { get; set; }
        [XmlElement(ElementName = "responseReason")]
        public string ResponseReason { get; set; }
        [XmlElement(ElementName = "refId")]
        public string RefId { get; set; }
        [XmlElement(ElementName = "mobileNo")]
        public string MobileNo { get; set; }
        [XmlElement(ElementName = "currentOperator")]
        public string CurrentOperator { get; set; }
        [XmlElement(ElementName = "currentLocation")]
        public string CurrentLocation { get; set; }
        [XmlElement(ElementName = "currentOptBillerId")]
        public string CurrentOptBillerId { get; set; }
        [XmlElement(ElementName = "previousOperator")]
        public string PreviousOperator { get; set; }
        [XmlElement(ElementName = "previousLocation")]
        public string PreviousLocation { get; set; }
        [XmlElement(ElementName = "ported")]
        public string Ported { get; set; }
        [XmlElement("errorInfo")]
        public MErrorInfo errorInfo { get; set; }
    }


}