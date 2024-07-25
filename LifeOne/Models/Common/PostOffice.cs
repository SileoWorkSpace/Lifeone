using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.Common
{
    [Serializable]
    public class PostOffice
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string BranchType { get; set; }
        public string DeliveryStatus { get; set; }
        public string Taluk { get; set; }
        public string Circle { get; set; }
        public string District { get; set; }
        public string Division { get; set; }
        public string Region { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Responce { get; set; }
        public int CityID { get; set; }
        public int StateID { get; set; }
        public string Districts { get; set; }



        public string P_City { get; set; }
        public string P_State { get; set; }
        public string Cr_City { get; set; }
        public string Cr_State { get; set; }

    }
}