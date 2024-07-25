using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class ResultSet
    {
        public string Msg { get; set; }
        public int Flag { get; set; }
        public long TransId { get; set; }

        public string LoginId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Fk_MemId { get; set; }
        public string TourID { get; set; }
        public string OrderStatus { get; set; }
        public string OrderId { get; set; }
        public bool Iskyc { get; set; }
    }
    public class ResultForCashFree
    {
        public string Msg { get; set; }
    }
    public class GetVendorId
    {
        public int vendor_id { get; set; }
    }
}