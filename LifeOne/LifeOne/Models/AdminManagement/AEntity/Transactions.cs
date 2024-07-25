using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class RequestDetails
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
        public string Name { get; set; }
        public int IsAssociateApprove { get; set; }
        public int IsFranchiseApprove { get; set; }
        public int Page { get; set; }
        public int size { get; set; }
        public int TotalRecord { get; set; }
        public Pager Pager { get; set; }
        public List<RequestDetails> lstRequest { get; set; }
    }

    public class TransactionsResponse
    {
        public int Code { get; set; }
        public string Msg { get; set; }
    }
}