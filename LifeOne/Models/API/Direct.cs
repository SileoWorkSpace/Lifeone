using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class Direct
    {
        public long FK_MemId { get; set; }
        public long FK_sponsorId { get; set; }
        public string MemberLoginId { get; set; }
        public string SponsorLoginId { get; set; }
        public string MemberName { get; set; }
        public string SponsorName { get; set; }       
    }

    public class ReturnResponseDirect
    {
        public string response { get; set; }
        public string message { get; set; }
        public List<Direct> lstDirect { get; set; }
    }

}