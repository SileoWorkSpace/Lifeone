using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.Entity
{
    public class MDataCardPara
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

        public string FK_MemId { get; set; }
        public string ValidateToken { get; set; }
        public string Provider { get; set; }
        public string WalletDetailsId { get; set; }
        public string BBPSApiIntegrationId { get; set; }
        public string alltransactionlogId { get; set; }
        public string TransactionId { get; set; }

        public string Number { set; get; }
        public string Accounttype { set; get; }
        public string Amount { set; get; }
        public string RechargeDate { set; get; }

        public string FK_MemberId { set; get; }
        public string Session { set; get; }
        public string Error { set; get; }
        public string Result { set; get; }
        public string ErrorMessage { set; get; }

        public string TransactionStatus { set; get; }
        public string AuthCode { set; get; }
        public string GatewayNo { set; get; }
    }

    public class DataCardResponse
    {
        public string DATE { get; set; }
        public string SESSION { get; set; }
        public string ERROR { get; set; }
        public string RESULT { get; set; }
        public string TRANSID { get; set; }
        public string TRNXSTATUS { get; set; }
        public string AUTHCODE { get; set; }
        public string ERRMSG { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}