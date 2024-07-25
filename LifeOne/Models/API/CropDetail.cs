using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class CropDetail
    {
        public string LanguageCode { get; set; }
        public string CropCode { get; set; }
        public string CropName { get; set; }

    }

    public class CropListResponse
    {
        public List<CropDetail> result { get; set; }
        public string response { get; set; }
        public string message { get; set; }
    }

    public class CropDetialReq
    {
        public string CropCode { get; set; }
        public double Area { get; set; }
        public string LanguageCode { get; set; }
        public string Date { get; set; }
    }

    public class CropDetailbyLangunage
    {
        public string LanguageCode { get; set; }
        public string CropCode { get; set; }
        public string CropName { get; set; }
        public string ProductName { get; set; }
        public string Days { get; set; }
        public decimal Area { get; set; }
        public decimal ProductQty { get; set; }
        public string ProductUnit { get; set; }
        public decimal MRP { get; set; }
        public decimal SalePrice { get; set; }
        public string ProductImage { get; set; }
        public string Description { get; set; }
        public string CropType { get; set; }
        public string Status { get; set; }
        public string MethodOfApplication { get; set; }

    }

    public class CropDetialListModel1
    {

        public string Days { get; set; }
        public List<CropDetailbyLangunage> CropDetailbyLangunageList { get; set; }
    }

    public class CropDetailbyLangunage3
    {
        public string Response { get; set; }
        public string Message { get; set; }
        public List<CropDetialListModel> result { get; set; }
    }

    public class CropDetialListModel
    {
        public string CropType { get; set; }
        public string MethodOfApplication { get; set; }
        public List<CropDetialListModel1> CropDetialList { get; set; }
    }
}