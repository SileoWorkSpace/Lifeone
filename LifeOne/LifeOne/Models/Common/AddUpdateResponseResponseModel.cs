using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.Common
{
    public class AddUpdateResponseResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public string body { get; set; }
    }
   
}