using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FEntity
{
    public class MAssignShippingInfo
    {
        public string FirstName { get; set; }
        public string Address1 { get; set; }
        public string OtherAdrInfo { get; set; }
        public string TransferInvoiceNo { get; set; }
        public long FranchiseId { get; set; }
        public int FK_FbSID { get; set; }
        public string EmailAddress { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string CurrStatus { get; set; }
        public string Remarks { get; set; }

        public string mobile { get; set; }
        public string Email { get; set; }

        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public string ReqQty { get; set; }


        public string UniId { get; set; }
        public string ShippingStatus { get; set; }

        public string ShippingDate { get; set; }
        public int? Page { get; set; }
        public int Size { get; set; }
        public int TotalRecord { get; set; }       
        public Pager Pager { get; set; }
        public List<MAssignShippingInfo> objlist { get; set; }



    }
}