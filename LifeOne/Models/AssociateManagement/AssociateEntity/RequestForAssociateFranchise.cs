using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class RequestForAssociateFranchise
    {
        public long Fk_MemId { get; set; } 
        public string DisplayName { get; set; }
        public string LoginId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool IsBusiness { get; set; }
        public bool IsAssociate { get; set; }
        public bool IsFranchise { get; set; }
        public string JoiningDate { get; set; }
        public string RequestDate { get; set; }
        public string AssociateRemark { get; set; }
        public string FranchiseRemark { get; set; }
        public int IsAssociateApprove { get; set; }
        public int IsFranchiseApprove { get; set; }
        public List<RequestForAssociateFranchise> lstRequest { get; set; }
    }
    public class ResponseforRequestForFranchise
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public string Type { get; set; }
    }
}