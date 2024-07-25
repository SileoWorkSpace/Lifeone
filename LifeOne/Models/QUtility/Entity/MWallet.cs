using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.Entity
{
    public class MWallet
    {


    }


    public class MWalletCompany : MCommon
    {

    }

    public class WalletTransaction
    {
        public string Fk_MemId { get; set; }
        public string Amount { get; set; }
        public string Operator { get; set; }
        public string Number { get; set; }
        public string ActionType { get; set; }
        public string Action { get; set; }
        public string TransactionNo { get; set; }
        public string RechargeType { get; set; }
    }

    public class WalletTransactionResponse
    {
        public string Response { get; set; }
        public string WalletDetailsId { get; set; }
        public string BBPSApiIntegrationId { get; set; }
        public string alltransactionlogId { get; set; }
        public string TransactionId { get; set; }
    }

    public class BalanceResponse : MCommon
    {
        public decimal BalanceAmount { get; set; }
        public string Status { get; set; }
        public string ValidateToken { get; set; }
        public string DebitCardCharge { get; set; }
        public string CreditCardCharge { get; set; }
        public string DebitCardMinCharge { get; set; }
        public string DebitCardMaxCharge { get; set; }
        public string ChargeableAmount { get; set; }

    }

    public class GetWalletResponse
    {
        //Fk_MemId FirstName   UpdatedBalance Email
        public string Response { get; set; }
        public string Balance { get; set; }
        public string BalanceAmount { get; set; }
        public string Status { get; set; }
        public String ValidateToken { get; set; }


    }

    public class CheckWalletStatus
    {
        public string Fk_MemId { get; set; }
        public string Amount { get; set; }
        public string Number { get; set; }

    }
  
}