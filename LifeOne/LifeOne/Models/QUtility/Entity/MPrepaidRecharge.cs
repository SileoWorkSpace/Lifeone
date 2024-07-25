using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.Entity
{
    public class MPrepaidRecharge: MCommon
    {
        public int FK_MemId { get; set; }
        public decimal Amount { get; set; }        
        public string Provider { get; set; }
    }
    public class PrepaidResponse
    {
        public string DATE { get; set; }
        public string SESSION { get; set; }
        public string ERROR { get; set; }
        public string RESULT { get; set; }
        public string TRANSID { get; set; }
        public string TRNXSTATUS { get; set; }
        public string AUTHCODE { get; set; }
        public string ERRMSG { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public int CompanyId { get; set; }

    }
    public class PrepaidAPI 
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
        public string ACCOUNT { get; set; }
        public string AMOUNT { get; set; }
        public string FK_MemId { get; set; }
        public string Provider { get; set; }
        public string ValidateToken { get; set; }
        public string WalletDetailsId { get; set; }
        public string BBPSApiIntegrationId { get; set; }
        public string alltransactionlogId { get; set; }
        public string TransactionId { get; set; }
        public long CompanyId { get; set; }
        public string WalletAmount { get; set; }
        public string GatewayAmount { get; set; }

    }
}