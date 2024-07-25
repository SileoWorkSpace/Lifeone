using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;
namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class FranchiseList : MPaging
    {
        public string LoginId { get; set; }
        public string AssociateName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string AppliedForPinCode { get; set; }
        public string AreaName { get; set; }
        public string Village { get; set; }
        public string Taluka { get; set; }
        public string District { get; set; }
        public string PinCode { get; set; }
        public string FranchiseName { get; set; }
        public string address { get; set; }
        public string Status { get; set; }

        public string Type { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string IFSC { get; set; }
        public string AccountNo { get; set; }
        public string GSTNo { get; set; }
        public string PaymentStatus { get; set; }

        public bool IsApproved { get; set; }
        
        public List<FranchiseList> FranchiseListlst { get; set; }   
    }

    public class FranchiseListResponse
    {
        public int Flag { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public List<FranchiseList> FranchiseListlst { get; set; }
    }
}