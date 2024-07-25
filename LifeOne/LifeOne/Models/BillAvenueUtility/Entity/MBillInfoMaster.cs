using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.BillAvenueUtility.Entity
{
    public class MBillInfoMaster
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
        public string billerAmountOptions { get; set; }
        public string billerDescription { get; set; }
        public string billerPaymentModes { get; set; }
        public string rechargeAmountInValidationRequest { get; set; }
    }
}