using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.Entity
{
    
    public class MTataSky
    {
        public string Response { get; set; }
        public string Request { get; set; }
        public string keyPath { get; set; }
        public string Message { get; set; }
        public string SessionNo { get; set; }
        public string SDCode { get; set; }
        public string APCode { get; set; }
        public string OPCode { get; set; }
        public string Type { get; set; }
        public string AMOUNT_ALL { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string NUMBER { get; set; }
        public string ProductId { get; set; }
        public string bookingDate { get; set; }
        public string benAddressline1 { get; set; }
        public string benAddressline2 { get; set; }
        public string benCity { get; set; }
        public string Pin { get; set; }
        public string State { get; set; }
        public string Amount { get; set; }
        public string CERTNo { get; set; }
        public string RegionId { get; set; }
        public string FK_MemId { get; set; }
        public string ValidateToken { get; set; }
        public string WalletDetailsId { get; set; }
        public string BBPSApiIntegrationId { get; set; }
        public string alltransactionlogId { get; set; }
        public string TransactionId { get; set; }

    }

    public class MTataSkyResponse
    {
        public string DATE { get; set; }
        public string SESSION { get; set; }
        public string ERROR { get; set; }
        public string RESULT { get; set; }
        public string ERRMSG { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        //public List<ADDINFO> ADDINFO { get; set; }

    }
}