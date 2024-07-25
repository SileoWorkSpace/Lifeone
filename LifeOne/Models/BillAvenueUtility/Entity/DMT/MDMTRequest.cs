using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace LifeOne.Models.BillAvenueUtility.Entity.DMT
{
    public class MDMTRequest
    {

        [XmlRoot(ElementName = "dmtServiceRequest")]
        public class MDmtServiceRequest:MCommon
        {
            [XmlElement(ElementName = "requestType")]
            public string RequestType { get; set; }
            [XmlElement(ElementName = "senderMobileNumber")]
            public string SenderMobileNumber { get; set; }
            [XmlElement(ElementName = "txnType")]
            public string TxnType { get; set; }
            [XmlElement(ElementName = "senderName")]
            public string SenderName { get; set; }
            [XmlElement(ElementName = "senderPin")]
            public string SenderPin { get; set; }

            // Verify Sender
            [XmlElement(ElementName = "otp")]
            public string Otp { get; set; }
            [XmlElement(ElementName = "additionalRegData")]
            public string AdditionalRegData { get; set; }

            //Get Recieptance
            [XmlElement(ElementName = "recipientId")]
            public string RecipientId { get; set; }

            //Add Recipient 
            [XmlElement(ElementName = "recipientName")]
            public string RecipientName { get; set; }
            [XmlElement(ElementName = "recipientMobileNumber")]
            public string RecipientMobileNumber { get; set; }
            [XmlElement(ElementName = "bankCode")]
            public string BankCode { get; set; }
            [XmlElement(ElementName = "bankAccountNumber")]
            public string BankAccountNumber { get; set; }
            [XmlElement(ElementName = "ifsc")]
            public string Ifsc { get; set; }
        }

        [XmlRoot(ElementName = "dmtServiceResponse")]
        public class MDmtServiceResponse
        {
            [XmlElement(ElementName = "additionalLimitAvailable")]
            public string AdditionalLimitAvailable { get; set; }
            [XmlElement(ElementName = "availableLimit")]
            public string AvailableLimit { get; set; }
            [XmlElement(ElementName = "mobileVerified")]
            public string MobileVerified { get; set; }
            [XmlElement(ElementName = "respDesc")]
            public string RespDesc { get; set; }
            [XmlElement(ElementName = "responseCode")]
            public string ResponseCode { get; set; }
            [XmlElement(ElementName = "responseReason")]
            public string ResponseReason { get; set; }
            [XmlElement(ElementName = "senderCity")]
            public string SenderCity { get; set; }
            [XmlElement(ElementName = "senderMobileNumber")]
            public string SenderMobileNumber { get; set; }
            [XmlElement(ElementName = "senderName")]
            public string SenderName { get; set; }
            [XmlElement(ElementName = "totalLimit")]
            public string TotalLimit { get; set; }
            [XmlElement(ElementName = "usedLimit")]
            public string UsedLimit { get; set; }

            [XmlElement("errorInfo")]
            public MErrorInfo errorInfo { get; set; }
            [XmlElement(ElementName = "additionalRegData")]
            public string AdditionalRegData { get; set; }

            //Dmt AllRecipient 
            [XmlElement(ElementName = "recipientList")]
            public RecipientList RecipientList { get; set; }

            //Get Bank List
            [XmlElement(ElementName = "bankList")]
            public BankList BankList { get; set; }
            public string TxnType { get; set; }

        }

        [XmlRoot(ElementName = "bankInfoArray")]
        public class BankInfoArray
        {
            [XmlElement(ElementName = "accountVerificationAllowed")]
            public string AccountVerificationAllowed { get; set; }
            [XmlElement(ElementName = "bankCode")]
            public string BankCode { get; set; }
            [XmlElement(ElementName = "bankName")]
            public string BankName { get; set; }
            [XmlElement(ElementName = "impsAllowed")]
            public string ImpsAllowed { get; set; }
            [XmlElement(ElementName = "neftAllowed")]
            public string NeftAllowed { get; set; }
        }

        [XmlRoot(ElementName = "bankList")]
        public class BankList
        {
            [XmlElement(ElementName = "bankInfoArray")]
            public List<BankInfoArray> BankInfoArray { get; set; }
        }


        [XmlRoot(ElementName = "recipientList")]
        public class RecipientList
        {
            [XmlElement(ElementName = "dmtRecipient")]
            public List<DmtRecipient> DmtRecipient { get; set; }
        }


        [XmlRoot(ElementName = "dmtRecipient")]
        public class DmtRecipient
        {
            [XmlElement(ElementName = "bankAccountNumber")]
            public string BankAccountNumber { get; set; }
            [XmlElement(ElementName = "bankCode")]
            public string BankCode { get; set; }
            [XmlElement(ElementName = "bankName")]
            public string BankName { get; set; }
            [XmlElement(ElementName = "ifsc")]
            public string Ifsc { get; set; }
            [XmlElement(ElementName = "isVerified")]
            public string IsVerified { get; set; }
            [XmlElement(ElementName = "recipientId")]
            public string RecipientId { get; set; }
            [XmlElement(ElementName = "recipientName")]
            public string RecipientName { get; set; }
            [XmlElement(ElementName = "recipientStatus")]
            public string RecipientStatus { get; set; }
            [XmlElement(ElementName = "verifiedName")]
            public string VerifiedName { get; set; }
        }




    }
}