using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.Common
{
    public class CommonRequestModel   : MPaging
    {
        public string LoginId { get; set; }
        public string UserId { get; set; }
        public long? FK_MID { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string From_Date { get; set; }
        public string To_Date { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        //------------Paging-------------------
        public int? PageNo { get; set; }
        public int? PageSize { get; set; }
    }
}