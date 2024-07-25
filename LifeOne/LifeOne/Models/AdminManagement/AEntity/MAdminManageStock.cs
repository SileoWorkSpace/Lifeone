using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MAdminManageStock
    {
        public int Page { get; set; }
        public int Size{ get; set; }
        //public long TotalRecords { get; set; }
        public long Pk_ProductId { get; set; }
        public string ProductName { get; set; }
        public long AvailableQuantity { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }        
        public decimal MRP { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal PV { get; set; }
        public long Fk_ProductTypeId { get; set; }
        public string ProductType { get; set; }
        public long Fk_ProductID { get; set; }
        public long ProductQty { get; set; }
        public long Fk_Memid  { get; set; }
        public long Type { get; set; }
        public string Mode { get; set; }
        public List<MAdminManageStock> objList { get; set; }
    }
    public class ResponseManageStock
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
    }
}