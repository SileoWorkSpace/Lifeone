using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace LifeOne.Models.BillAvenueUtility.Entity
{
    [Serializable]
    [XmlRoot("complaintRegistrationReq")]
    public class MBillComplaintRegistration:MCommon
    {
        [XmlElement(ElementName = "complaintType")]
        public string ComplaintType { get; set; }
        [XmlElement(ElementName = "participationType")]
        public string ParticipationType { get; set; }
        [XmlElement(ElementName = "agentId")]
        public string AgentId { get; set; }
        [XmlElement(ElementName = "txnRefId")]
        public string TxnRefId { get; set; }
        [XmlElement(ElementName = "billerId")]
        public string BillerId { get; set; }
        [XmlElement(ElementName = "complaintDesc")]
        public string ComplaintDesc { get; set; }
        [XmlElement(ElementName = "servReason")]
        public string ServReason { get; set; }
        [XmlElement(ElementName = "complaintDisposition")]
        public string ComplaintDisposition { get; set; }

    }
    [Serializable]
    [XmlRoot("complaintRegistrationResp")]
    public class MBillComplaintRegistrationResponse
    {
        public string complaintAssigned { get; set; }
        public string complaintId { get; set; }
        public string responseCode { get; set; }
        public string responseReason { get; set; }
        public MErrorInfo errorInfo { get; set; }
    }

    [Serializable]
    [XmlRoot("complaintTrackingReq")]
    public class MBillComplaintTracking:MCommon
    {
        public string complaintType { get; set; }
        public string complaintId { get; set; }

    }
    [Serializable]
    [XmlRoot("complaintTrackingResp")]
    public class MBillComplaintTrackingResponse:MError
    {
        public string complaintAssigned { get; set; }
        public string complaintId { get; set; }
        public string responseCode { get; set; }
        public string responseReason { get; set; }
        public string complaintStatus { get; set; }
        [XmlElement("errorInfo")]
        public MErrorInfo errorInfo { get; set; }
    }


}