using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.Entity
{
    public class MSendEmail
    {
        public string Fk_MemId { get; set; }
        public string TRNXSTATUS { get; set; }
        public string _Category { get; set; }
        public string _DebitedFromAccNo { get; set; }
        public string DATE { get; set; }
        public string NUMBER { get; set; }
        public string AMOUNT { get; set; }
        public string TRANSID { get; set; }
    }


    public class GetResponse
    {
        //Fk_MemId FirstName   UpdatedBalance Email
        public string Response { get; set; }
        public string Fk_MemId { get; set; }
        public string FirstName { get; set; }
        public string UpdatedBalance { get; set; }
        public string Email { get; set; }
    }

}