using LifeOne.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace LifeOne.Models.BillAvenues.Entity
{
    [Serializable]
    [XmlRoot("billFetchRequest")]
    public class MBillerFetchRequest : MCommon
    {
        public string agentId { get; set; }
       
        [XmlElement("agentDeviceInfo")]        
        public agentDeviceInfo agentDeviceInfo { get; set; }
        [XmlElement("customerInfo")]
        public customerInfo customerInfo { get; set; }
        public string billerId { get; set; }
        [XmlElement("inputParams")]
        public inputParams inputParams { get; set; }
        //[XmlElement("billerResponse")]
        //public MbillerResponse _objbillerResponse { get; set; }
        //[XmlElement("additionalInfo")]
        //public additionalInfo _objadditionalInfo { get; set; }



       

    
     

       

        //[XmlElement("billerResponse")]
        //public MbillerResponse _objbillerResponse { get; set; }


        [XmlElement("additionalInfo")]
        public additionalInfo additionalInfo { get; set; }

    }
    [XmlRoot("agentDeviceInfo")]
    public class agentDeviceInfo 
    {
        public string ip { get; set; }
        public string initChannel { get; set; }
        public string mac { get; set; }

        public string imei { get; set; }
        public string app { get; set; }
        public string os { get; set; }
    }
    [XmlRoot("customerInfo")]
    public class customerInfo
    {
        public string customerMobile { get; set; }
        public string customerEmail { get; set; }
        public string customerAdhaar { get; set; }
        public string customerPan { get; set; }
    }
 
    [Serializable]
    [XmlRoot("billFetchResponse")]
    public class MBillerFetchResponse 
    {
        public string RequestId { get; set; }

        public string responseCode { get; set; }
        [XmlElement("inputParams")]
        public List<inputParams> inputParams { get; set; }
        [XmlElement("billerResponse")]
        public MbillerResponse billerResponse { get; set; }
        [XmlElement("additionalInfo")]
        public additionalInfo additionalInfo { get; set; }
        [XmlElement("errorInfo")]
        public MErrorInfo errorInfo { get; set; }


      

        [XmlElement("agentDeviceInfo")]
        public agentDeviceInfo agentDeviceInfo { get; set; }

        [XmlElement("customerInfo")]
        public customerInfo customerInfo { get; set; }
        public string billerId { get; set; }

       

        //[XmlElement("billerResponse")]
        //public MbillerResponse _objbillerResponse { get; set; }


        //[XmlElement("additionalInfo")]
        //public AdditionalInfo additionalInfo { get; set; }

    }

    [Serializable]
    [XmlRoot("errorInfo")]
    public class errorInfo
    {
       
        [XmlElement("error")]
        public List<error> _objlistMbillerError { get; set; }       

    }
    [Serializable]
    [XmlRoot("error")]
    public class error
    {
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
    }

    [Serializable]
    [XmlRoot("billerResponse")]
    public class MbillerResponse
    {
     
        [XmlElement("amountOptions")]
        public List<MbilleramountOptions> amountOptions { get; set; }
        public string billAmount { get; set; }
        public string billDate { get; set; }
        public string billNumber { get; set; }
        public string billPeriod { get; set; }
        public string customerName { get; set; }
        public string dueDate { get; set; }


   
    }
    [Serializable]
    [XmlRoot("amountOptions")]
    public class MbilleramountOptions
    {
        [XmlElement("option")]
        public List<option> option { get; set; }
        //public string amountName { get; set; }
        //public string amountValue { get; set; }

    }
    [Serializable]
    [XmlRoot("option")]
    public class option
    {
        public string amountName { get; set; }
       public string amountValue { get; set; }
    }

    [Serializable]
    [XmlRoot("additionalInfo")]
    public class additionalInfo
    {
        public List<info> _objinfolist { get; set; }
    }
    [Serializable]
    [XmlRoot("info")]
    public class info 
    {
        public string infoName { get; set; }
        public string infoValue { get; set; }
    }


}