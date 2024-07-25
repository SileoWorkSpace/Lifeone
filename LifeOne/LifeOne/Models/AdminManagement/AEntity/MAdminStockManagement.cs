using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.Common;
namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MAdminStockManagement:MPaging
    {
        public long PrdId { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public int Fk_MemId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string TotalQty { get; set; }
        public string CurrentQty { get; set; }
        public string SellQty { get; set; }
        public string LoginId { get; set; }
        public int ProcId { get; set; }
        public decimal CGST { get; set; }
        public decimal IGST { get; set; }
        public decimal SGST { get; set; }
        public decimal PointValue { get; set; }

        public decimal Amount { get; set; }


        public string OrbitName { get; set; }
        public List<MAdminStockManagement> MAdminStockManagementList { get; set; }

    }


    public class ProductStockReport
    {
        public string ProductName { get; set; }
        public string ProductQuantity { get; set; }
        public string OrderQuantity { get; set; }
        public string BalanceQuantity { get; set; }
        public string Price { get; set; }
        public string BV { get; set; }
        public string DP { get; set; }
        public string GST { get; set; }
        public string OrderDate { get; set; }
        public string LoginId { get; set; }




    }
}