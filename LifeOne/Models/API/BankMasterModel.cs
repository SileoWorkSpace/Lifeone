using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models
{
    public class BankMasterModel
    {
        public string bankname { get; set; }
        public string branchname { get; set; }
        public string accountmembername { get; set; }
        public string accountno { get; set; }
        public string ifsccode { get; set; }
        public string address { get; set; }

    }

    public class BankMasterResponse
    {
        public BankMasterModel result { get; set; }
        public string response { get; set; }
        public string message { get; set; }
    }
}