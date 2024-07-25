using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class AgricultureModel
    {

    }
    public class CropCategoryModel
    {
        public int Id { get; set; }
        public int CropCategoryId { get; set; }
        public string CropCategory { get; set; }
        public string imageUrl { get; set; }
        public int ToTalVideoCount { get; set; }
        public bool Status { get; set; }
    }
    public class CropProductModel
    {
        public int Flag { get; set; }
        public int Id { get; set; }
        public int Crop_ProdId { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Crop_ProductName { get; set; }
        public string imageUrl { get; set; }
        public int ToTalVideoCount { get; set; }
        public bool Status { get; set; }
        public string CreateDate { get; set; }
        public string Msg { get; set; }
        public bool IsDeleted { get; set; }
        public long CreateBy { get; set; }
        public List<CropProductModel> CropProductList { get; set; }
    }

   
    public class CropCategoryResponse
    {
        public string response { get; set; }
       public List<CropCategoryModel> Data { get; set; }
    }
    public class CropProductResponse
    {
        public string response { get; set; }
        public List<CropProductModel> Data { get; set; }
    }
}