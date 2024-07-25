using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class ProductServiceModel
    {
    }

    public class CategoryModel
    {
        public int Id { get; set; }
        public int? ProcID { get; set; }
        public string CropName { get; set; }
        public string CropImage { get; set; }
        public string CreateDate { get; set; }
        public List<CategoryModel> CategoryList { get; set; }
    }

    public class SubCategoryViewModel
    {
        public Nullable<int> Id { get; set; }
        public int? ProcID { get; set; }
        public int CategoryID { get; set; }
        public string Category { get; set; }
        public string Crop_ProductName { get; set; }
        public string ImageUrl { get; set; }
        public List<SubCategoryViewModel> SubCategoryList { get; set; }
    }
    public class KaryonProductModel
    {
        public long Id { get; set; }
        public int FK_OrbitType { get; set; }
        public int? ProcID { get; set; }
        public string ProductName { get; set; }
        public string ProductSKU { get; set; }
        public string Unit { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }
        public decimal MRP { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal GST { get; set; }

    }

    public class KaryonProductOptionModel
    {
        public int BoxSize { get; set; }
        public decimal Size { get; set; }
        public decimal Price { get; set; }
        public decimal DP { get; set; }
        public decimal PV { get; set; }
    }

    public class SaveKaryonProductModel
    {
        public KaryonProductModel ProductDetails { get; set; }
        public List<KaryonProductOptionModel> ProductOptionsList { get; set; }
    }
    public class AddUpdateDeleteModel
    {
        public bool Status { get; set; }
        public string Msg { get; set; }
        public dynamic Data { get; set; }
    }

   

}