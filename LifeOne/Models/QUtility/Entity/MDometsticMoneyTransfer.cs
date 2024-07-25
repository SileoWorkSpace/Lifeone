using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.Entity
{
    public class MDometsticMoneyTransfer : PrepaidAPI
    {
        public string AMOUNT_ALL { get; set; }
        public string Type { get; set; }
    }
    public class MDometsticMoneyTransferResponse
    {
        public string DATE { get; set; }
        public string SESSION { get; set; }
        public string ERROR { get; set; }
        public string RESULT { get; set; }
        //public string TRANSID { get; set; }
        public string ERRMSG { get; set; }
        public string AUTHCODE { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }

        public string TRANSID { get; set; }
        public string TRNXSTATUS { get; set; }
        public string REFERENCENO { get; set; }
        public string BILLERCONFIRMATIONNUMBER { get; set; }

        
    }

}