using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
   

    public class MAdminProductMaster
    {
        public string Pk_ProductId { get; set; }
        public string Fk_SupplierId { get; set; }
        public string OfferedQty { get; set; }
        public string ProductName { get; set; }
        public string SupplierName { get; set; }
        public string ProductSKU { get; set; }
        public string ProductDescription { get; set; }
        public string Packing { get; set; }
        public string DirectionOfUse { get; set; }
        public string Doses { get; set; }
        public string Avalibility { get; set; }
        public string SalesPrice { get; set; }
        public string MRP { get; set; }
        public string Size { get; set; }
        public string ProductImage { get; set; }
        public HttpPostedFileBase file { get; set; }
        public string PV { get; set; }
        public string BV { get; set; }
        public string CGST { get; set; }
        public string IGST { get; set; }
        public string ProductType { get; set; }
        public string Unit { get; set; }
        public string Status { get; set; }
        public string SGST { get; set; }
        public string PtypeId { get; set; }
        public string Fk_UnitId { get; set; }
        public string Fk_StatusId { get; set; }
        public string CreatedById { get; set; }
        public int Fk_ProductTypeId { get; set; }
        

        public string BoxType { get; set; }
        public bool IsBox { get; set; }
        public int BoxQty { get; set; }
        public double BoxPV { get; set; }
        public int FK_OrbitType { get; set; }
        public string HSNCode { get; set; }
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedDate { get; set; }
        public string Videolink { get; set; }
        public int OpCode { get; set; }
        public List<MAdminProductMaster> ProductMasterList { get; set; }
        public List<MAdminProductMaster> ObjList{ get; set; }
         
    }
    public class Image 
    {
    public string Url { get; set; }
    }

    public class ResponseMaster 
    {
        public string Flag { get; set; }
        public string Msg { get; set; }
        public string Code { get; set; }
        public string TransId { get; set; }
    }
}