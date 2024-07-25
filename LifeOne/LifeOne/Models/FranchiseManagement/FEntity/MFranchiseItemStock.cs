using LifeOne.Models.QUtility.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;


namespace LifeOne.Models.FranchiseManagement.FEntity
{
    public class MFranchiseItemStock : MFranchiseorderRequest
    {
        public int MyProperty { get; set; }
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }
        public decimal CGST { get; set; }
        public decimal IGST { get; set; }
        public decimal SGST { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Type { get; set; }
        public List<MFranchiseItemStock> LstStock { get; set; }
        public int? Page { get; set; }
        public int Size { get; set; }
        public int TotalRecords { get; set; }
        public string SearchKey { get; set; }
        public string SearchValue { get; set; }
        public Pager Pager { get; set; }
    }
}