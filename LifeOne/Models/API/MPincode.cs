using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class MPincode
    {
        public string PinCode { get; set; }
    }
    public class MPincodeDetails
    {
        public string PinCode { get; set; }
        public string Taluka { get; set; }
        public string DistrictName { get; set; }
        public string StateName { get; set; }
        public string City { get; set; }
        public string CircleName { get; set; }
        public string DivisionName { get; set; }
    }


    public class MPincodeDetailsResponse : CommonJsonReponse
    {
        public List<MPincodeDetails> _objPiinList { get; set; }
    }
}