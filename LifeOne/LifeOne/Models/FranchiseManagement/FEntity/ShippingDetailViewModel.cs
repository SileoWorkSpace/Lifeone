using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FEntity
{
    public class ShippingDetailModel
    {
        public int FK_FbSID { get; set; }
        public long FranchiseId { get; set; }
    }

    public class ShippingDetailViewModel
    {
        public int FK_FbSID { get; set; }
        public long FranchiseId { get; set; }
        public string MemberName { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string OrderNo { get; set; }
        public string OrderDate { get; set; }
        public string TransferInvoiceNo { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ReqQty { get; set; }
        public decimal MRP { get; set; }
        public ShippingDetailViewModel MemberDetails { get; set; }
        public List<ShippingDetailViewModel> ShippingDetailList { get; set; }
    }
}