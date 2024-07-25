using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MRazorPayRefundResponse
    {
        public string id { get; set; }
        public string entity { get; set; }
        public string payment_id { get; set; }
        public string status { get; set; }
        public string Msg { get; set; }
        public string amount { get; set; }
        public string code { get; set; }
    }

    public class MagentoResponseModel
    {
        public string response { get; set; }
        public string message { get; set; }
    }

    public class MagentoRequestModel
    {
        public string order_id { get; set; }
    }
}