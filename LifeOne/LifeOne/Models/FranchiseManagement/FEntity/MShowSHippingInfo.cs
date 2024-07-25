using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FEntity
{
    public class MShowSHippingInfoToAssociate
    {
        public string FranchiseName { get; set; }
        public string EmailAddress { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string ProductName { get; set; }
        public string ReqQty { get; set; }
        public string Remarks { get; set; }
        public string DocDate { get; set; }        
        public string VehicalInfo { get; set; }
        public string DeliveryAddress { get; set; }
        public string StateCity { get; set; }
        public string PinCode { get; set; }
        public string CourierName { get; set; }
        public string RefNo { get; set; }
        public string ContactPerson { get; set; }

        public string DeliveredStatus { get; set; }

        public string FK_FbSID { get; set; }
        public int FranchiseId { get; set; }

        


    }
}