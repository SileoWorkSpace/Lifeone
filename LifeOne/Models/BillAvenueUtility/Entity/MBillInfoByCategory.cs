using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace LifeOne.Models.BillAvenueUtility.Entity
{
    [Serializable]
    [XmlRoot("billerInfoRequestByCategory")]
    public class MbillInfoByCategoryRequest : MCommon
    {
        [XmlElement("Category")]
        public string Category { get; set; }
    }

    [Serializable]
    [XmlRoot("billerInfoResponse")]
    public class MbillerInfoResponseByCategory
    {
        public string responseCode { get; set; }

        [XmlElement("biller")]
        public List<MbillerListByCategory> _objbillerlist { get; set; }
        public string billerAmountOptions { get; set; }
        public string billerPaymentModes { get; set; }
        [XmlElement("errorInfo")]
        public MErrorInfo errorInfo { get; set; }
    }
    [Serializable]
    [XmlRoot("biller")]
    public class MbillerListByCategory
    {
        public string billerId { get; set; }
        public string billerName { get; set; }
        public string billerCategory { get; set; }
        public string billerAdhoc { get; set; }
        public string billerCoverage { get; set; }
        public string billerFetchRequiremet { get; set; }
        public string billerPaymentExactness { get; set; }
        public string billerSupportBillValidation { get; set; }
        public string supportPendingStatus { get; set; }
        public string supportDeemed { get; set; }
        public string billerTimeout { get; set; }
        [XmlElement("billerInputParams")]
        public List<MbillerInputParamsbyCategory> _objlistMbillerInputParams { get; set; }
        [XmlElement("billerPaymentChannels")]
        public List<MbillerPaymentChannelsBYCategory> _objlistMbillerPaymentChannels { get; set; }
        public string billerAmountOptions { get; set; }
        public string billerPaymentModes { get; set; }
        public string billerDescription { get; set; }
        public string rechargeAmountInValidationRequest { get; set; }

    }

    [Serializable]
    [XmlRoot("billerInputParams")]

    public class MbillerInputParamsbyCategory
    {
        [XmlElement("paramInfo")]
        public List<MparamInfoByCategory> _objlistMparamInfo { get; set; }
    }

    [Serializable]
    [XmlRoot("billerPaymentChannels")]

    public class MbillerPaymentChannelsBYCategory
    {
        [XmlElement("paymentChannelInfo")]
        public List<MpaymentChannelInfo> _objlistpaymentChannelInfo { get; set; }
    }

    [Serializable]
    [XmlRoot("paramInfo")]
    public class MparamInfoByCategory
    {
        public string paramName { get; set; }
        public string dataType { get; set; }
        public bool isOptional { get; set; }
        public int minLength { get; set; }
        public int maxLength { get; set; }
    }
    [Serializable]
    [XmlRoot("paymentChannelInfo")]
    public class MpaymentChannelInfoBycategory
    {
        public string paymentChannelName { get; set; }
        public int minAmount { get; set; }
        public int maxAmount { get; set; }
    }


    public class BillInfoDetailsByCategory
    {
        public string billerId { get; set; }
        public string billerName { get; set; }
        public string billerCategory { get; set; }
        public string billerAdhoc { get; set; }
        public string billerCoverage { get; set; }
        public string billerFetchRequiremet { get; set; }
        public string billerPaymentExactness { get; set; }
        public string billerSupportBillValidation { get; set; }
        public string supportPendingStatus { get; set; }
        public string supportDeemed { get; set; }
        public string billerTimeout { get; set; }
        public string BillerPaymentMode { get; set; }
        public string jsonValidation { get; set; }
        public string billerPaymentChannels { get; set; }
        public string imageUrl { get; set; }

        
        
    }

}