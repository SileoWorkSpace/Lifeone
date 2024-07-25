using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace LifeOne.Models.BillAvenueUtility.Entity
{

    [XmlRoot(ElementName = "MBillerWalletSatus")]
    public class MBillerWalletSatus
    {
        [XmlElement(ElementName = "CompanyId")]
        public string CompanyId { get; set; }

        [XmlElement(ElementName = "Fk_MemId")]
        public string Fk_MemId { get; set; }

        [XmlElement(ElementName = "Amount")]
        public string Amount { get; set; }
    }
    [XmlRoot(ElementName = "MBillerWalletResponse")]
    public class MBillerWalletResponse
    {
        public string Msg { get; set; }
        public string Balance { get; set; }
        public string Status { get; set; }
        public string ResponseCode { get; set; }
        public MErrorInfo errorInfo { get; set; }
    }
}