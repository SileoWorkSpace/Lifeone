using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class KaryonOrderSmryItemsAPIViewModel
    {
        public int ProductQty { get; set; }
        public string InvoiceNo { get; set; }
        public string ProductCode { get; set; }
        public decimal MRP { get; set; }
        public decimal DP { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal GSTAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalProductPV { get; set; }
    }

    public class MOrderBillingAPI
    {
        public string Token { get; set; }
        public string KaryOnLoginId { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string DiscountAmount { get; set; }
        public string TaxableAmount { get; set; }
        public string GSTAmount { get; set; }
        public string TotalAmount { get; set; }
        public string TotalPv { get; set; }
        public string AirOrbit { get; set; }
        public string WaterOrbit { get; set; }
        public string SpaceOrbit { get; set; }
    }

    public class KaryionOrderResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }

    }

    public class PayAddaResponse
    {
        public int Flag { get; set; }
        public string Msg { get; set; }

    }
    //Used Models here
    public class MCompanyDetails
    {
        //public string CompanyDbDetailId { get; set; }
        //public string CompanyId { get; set; }
        public string ConnectionString { get; set; }
        //public string CompStauts { get; set; }
        public string CompnaySubCode { get; set; }
        //public string CreatedDate { get; set; }
        //public string Isdeleted { get; set; }
    }



}