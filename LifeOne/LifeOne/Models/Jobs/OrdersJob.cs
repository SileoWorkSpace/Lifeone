
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using LifeOne.Models.API;

namespace LifeOne.Models.Jobs
{
    public class OrdersJob 
    {

        public class OrdersResponse
        {
            public string response { get; set; }

            public List<OrdersList> data { get; set; }
        }

        public class OrdersList
        {
            public string order_id { get; set; }
            public string price { get; set; }
            public string created_at { get; set; }
        }


   
    }
}