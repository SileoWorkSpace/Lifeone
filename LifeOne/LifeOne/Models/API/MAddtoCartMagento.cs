using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class MAddtoCartMagento
    {
        public string sku { get; set; }
        public int qty { get; set; }
        public string token { get; set; }
        public List<CustomOption> custom_options { get; set; }
        public string type { get; set; }
    }
    public class CustomOption
    {
        public string option_id { get; set; }
        public string option_value { get; set; }
    }

    public class MAssignDynamicProduct
    {
        public string token { get; set; }
        public string Area { get; set; }
        public string CropCode { get; set; }
        public string language { get; set; }
        public string Date { get; set; }
    }
}