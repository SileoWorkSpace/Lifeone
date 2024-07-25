using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.Entity
{
    public class MBeneficiary
    {
    }

    #region Beneficiary Registration(Type= 4)
    public class BeneficiaryRegistration
    {
        public string Response { get; set; }
        public string Request { get; set; }
        public string keyPath { get; set; }
        public string Message { get; set; }
        public string SessionNo { get; set; }
        public string SDCode { get; set; }
        public string APCode { get; set; }
        public string OPCode { get; set; }
        public string CERTNo { get; set; }
        public string NUMBER { get; set; }
        public string Type { get; set; }
        public string remId { get; set; }
        public string lName { get; set; }
        public string fName { get; set; }
        public string benAccount { get; set; }
        public string benIFSC { get; set; }
        public string AMOUNT_ALL { get; set; }
        public string AMOUNT { get; set; }
    }
    public class BeneficiaryRegistrationResponse
    {
        public string DATE { get; set; }
        public string SESSION { get; set; }
        public string ERROR { get; set; }
        public string RESULT { get; set; }
        public string ADDINFO { get; set; }
        //public List<ADDINFO> ADDINFO { get; set; }

    }
    #endregion

    #region Beneficiary Registration Validation- OTPvalidation (Type=2)
    public class BeneficiaryOTPvalidation
    {
        public string Response { get; set; }
        public string Request { get; set; }
        public string keyPath { get; set; }
        public string Message { get; set; }
        public string SessionNo { get; set; }
        public string SDCode { get; set; }
        public string APCode { get; set; }
        public string OPCode { get; set; }
        public string CERTNo { get; set; }
        public string NUMBER { get; set; }
        public string Type { get; set; }
        public string remId { get; set; }
        public string benId { get; set; }
        public string otc { get; set; }
        public string AMOUNT_ALL { get; set; }
        public string AMOUNT { get; set; }
    }
    public class BeneficiaryOTPvalidationResponse
    {
        public string DATE { get; set; }
        public string SESSION { get; set; }
        public string ERROR { get; set; }
        public string RESULT { get; set; }
        public string ADDINFO { get; set; }
        public string errmsg { get; set; }
        //public List<ADDINFO> ADDINFO { get; set; }

    }
    #endregion

    #region  Beneficiary Registration Validation- Resend OTP validation (Type=9) 
    public class BeneficiaryResendOTPvalidation
    {
        public string Response { get; set; }
        public string Request { get; set; }
        public string keyPath { get; set; }
        public string Message { get; set; }
        public string SessionNo { get; set; }
        public string SDCode { get; set; }
        public string APCode { get; set; }
        public string OPCode { get; set; }
        public string CERTNo { get; set; }
        public string NUMBER { get; set; }
        public string Type { get; set; }
        public string remId { get; set; }
        public string benId { get; set; }
        public string lName { get; set; }
        public string fName { get; set; }
        public string benAccount { get; set; }
        public string benIFSC { get; set; }
        public string AMOUNT_ALL { get; set; }
        public string AMOUNT { get; set; }
    }
    public class BeneficiaryResendOTPvalidationResponse
    {
        public string DATE { get; set; }
        public string SESSION { get; set; }
        public string ERROR { get; set; }
        public string RESULT { get; set; }
        public string ADDINFO { get; set; }
        public string errmsg { get; set; }
        //public List<ADDINFO> ADDINFO { get; set; }

    }
    #endregion

    #region Beneficiary AccountVerification(Type= 10)
    public class BeneficiaryAccountVerification
    {
        public string Response { get; set; }
        public string Request { get; set; }
        public string keyPath { get; set; }
        public string Message { get; set; }
        public string SessionNo { get; set; }
        public string SDCode { get; set; }
        public string APCode { get; set; }
        public string OPCode { get; set; }
        public string CERTNo { get; set; }
        public string NUMBER { get; set; }
        public string Type { get; set; }
        public string benAccount { get; set; }
        public string benIFSC { get; set; }
        public string AMOUNT_ALL { get; set; }
        public string AMOUNT { get; set; }
    }
    public class BeneficiaryAccountVerificationResponse
    {
        public string DATE { get; set; }
        public string SESSION { get; set; }
        public string ERROR { get; set; }
        public string RESULT { get; set; }
        public string ADDINFO { get; set; }
        public string errmsg { get; set; }
        //public List<ADDINFO> ADDINFO { get; set; }

    }
    #endregion

    #region Fund Transfer(Type= 3)
    public class FundTransfer
    {
        public string Response { get; set; }
        public string Request { get; set; }
        public string keyPath { get; set; }
        public string Message { get; set; }
        public string SessionNo { get; set; }
        public string SDCode { get; set; }
        public string APCode { get; set; }
        public string OPCode { get; set; }
        public string CERTNo { get; set; }
        public string NUMBER { get; set; }
        public string Type { get; set; }
        public string routingType { get; set; }
        public string benId { get; set; }
        public string AMOUNT_ALL { get; set; }
        public string AMOUNT { get; set; }
        public string FK_MemId { get; set; }
        public string BeneficiaryName { get; set; }
        public string ValidateToken { get; set; }
    }
    public class FundTransferResponse
    {
        public string DATE { get; set; }
        public string SESSION { get; set; }
        public string ERROR { get; set; }
        public string RESULT { get; set; }
        public string ADDINFO { get; set; }
        public string errmsg { get; set; }
        public string TRANSID { get; set; }
        public string AUTHCODE { get; set; }
        public string TRNXSTATUS { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        //public List<ADDINFO> ADDINFO { get; set; }

    }
    #endregion

    #region Beneficiary Delete(Type= 6)
    public class BeneficiaryDelete
    {
        public string Response { get; set; }
        public string Request { get; set; }
        public string keyPath { get; set; }
        public string Message { get; set; }
        public string SessionNo { get; set; }
        public string SDCode { get; set; }
        public string APCode { get; set; }
        public string OPCode { get; set; }
        public string CERTNo { get; set; }
        public string Type { get; set; }
        public string remId { get; set; }
        public string benId { get; set; }
        public string AMOUNT_ALL { get; set; }
        public string AMOUNT { get; set; }
    }
    public class BeneficiaryDeleteResponse
    {
        public string DATE { get; set; }
        public string SESSION { get; set; }
        public string ERROR { get; set; }
        public string RESULT { get; set; }
        public string ADDINFO { get; set; }
        public string errmsg { get; set; }
        public String Status { get; set; }
        public String Message { get; set; }
        //public List<ADDINFO> ADDINFO { get; set; }

    }
    #endregion

    #region Beneficiary Delete Validation (Type=23) 
    public class BeneficiaryDeleteValidation
    {
        public string Response { get; set; }
        public string Request { get; set; }
        public string keyPath { get; set; }
        public string Message { get; set; }
        public string SessionNo { get; set; }
        public string SDCode { get; set; }
        public string APCode { get; set; }
        public string OPCode { get; set; }
        public string CERTNo { get; set; }
        public string Type { get; set; }
        public string remId { get; set; }
        public string benId { get; set; }
        public string otc { get; set; }
        public string AMOUNT_ALL { get; set; }
        public string AMOUNT { get; set; }
    }
    public class BeneficiaryDeleteValidationResponse
    {
        public string DATE { get; set; }
        public string SESSION { get; set; }
        public string ERROR { get; set; }
        public string RESULT { get; set; }
        public string ADDINFO { get; set; }
        //public string errmsg { get; set; }
        //public List<ADDINFO> ADDINFO { get; set; }
    }
    #endregion

    public class BeneficiaryModel
    {
        public Int64 RemitterId { get; set; }
    }
    public class BeneficiaryResponse
    {
        public String RemitterId { get; set; }
        public String BeneficiaryId { get; set; }
        public String BeneficiaryFirstName { get; set; }
        public String BeneficiaryLastName { get; set; }
        public String BeneficiaryAccountNo { get; set; }
        public String BeneficiaryIFSC { get; set; }
        public String BeneficiaryBankName { get; set; }
        public String BeneficiaryMobileNo { get; set; }
        public String Status { get; set; }
        public String Message { get; set; }
        public decimal OntimeTransLimit { get; set; }
        public decimal MOnthlyTransLimit { get; set; }
        public decimal Surcharge { get; set; }
        public decimal Expend { get; set; }
    }
    public class AddBeneficiaryDetils
    {
        public Int64 RemitterId { get; set; }
        public Int64 BeneficiaryId { get; set; }
        public String BeneficiaryFirstName { get; set; }
        public String BeneficiaryLastName { get; set; }
        public String BeneficiaryAccountNo { get; set; }
        public String BeneficiaryBankName { get; set; }
        public String BeneficiaryIFSC { get; set; }
        public String BeneficiaryMobileNo { get; set; }
    }
    public class AddBeneficiaryDetilsResponse
    {
        public String Status { get; set; }
        public String Msg { get; set; }
    }

    public class CustomerValidationInstp
    {
        public string Response { get; set; }
        public string Request { get; set; }
        public string keyPath { get; set; }
        public string Message { get; set; }
        public string SessionNo { get; set; }
        public string SDCode { get; set; }
        public string APCode { get; set; }
        public string OPCode { get; set; }
        public string CERTNo { get; set; }
        public string NUMBER { get; set; }
        public string Type { get; set; }
        public string AMOUNT_ALL { get; set; }
        public string AMOUNT { get; set; }
    }
    public class CustomerValidationInstpResponse
    {
        public string DATE { get; set; }
        public string SESSION { get; set; }
        public string ERROR { get; set; }
        public string RESULT { get; set; }
        public string ADDINFO { get; set; }
        public string errmsg { get; set; }
        //public List<ADDINFO> ADDINFO { get; set; }
    }
}