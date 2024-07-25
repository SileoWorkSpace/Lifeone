using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class MAgricultureMaster
    {
        public string CropType { get; set; }
        public string CropCategory { get; set; }
        public int TotalDaysOfCrop { get; set; }
        public string CropName { get; set; }  //Used for crop product id
        public int Crop_ProductId { get; set; } // used for crop name 
        public string CropCode { get; set; }
        public string MethodOfApplication { get; set; }
        public List<MDayAreaInformation> _objDayList { get; set; }
        public List<MDayWiseProductMapping> _objDayWiseProductList { get; set; }
        public List<LanguageMasterModel> LanguageList { get; set; }

    }
    public class MDayAreaInformation
    {
        public int CommonId { get; set; }
        public int Day { get; set; }
        public int ToDay { get; set; }
        public decimal Area { get; set; }
        public string AreaType { get; set; }
    }
    public class MDayWiseProductMapping
    {
        public int _ChildCommonId { get; set; }
        public int PrdId { get; set; }
        public int Qty { get; set; }

        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }

    public class ProductMasterModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal SalePrice { get; set; }
        public string Units { get; set; }
    }
    public class LanguageMasterModel
    {
        public int Language_Id { get; set; }
        public string Language_Code { get; set; }
        public string Language { get; set; }
        public string LanguageLable { get; set; }

    }


    public class MAgricultureMasterModel
    {
        public int TotalDaysOfCrop { get; set; }
        public string CropCategory { get; set; }
        public string CropType { get; set; }
        public string MethodOfApplication { get; set; }
        public long CropId { get; set; }
        public string CropCode { get; set; }
        public string CropName { get; set; }
        public int Crop_ProductId { get; set; }
        public List<DayListModel> _DayList { get; set; }
    }
    public class _MAgricultureMasterModel
    {
        public int TotalDaysOfCrop { get; set; }
        public string CropType { get; set; }
        public string CropCategory { get; set; }
        public string MethodOfApplication { get; set; }
        public long CropId { get; set; }
        public string CropCode { get; set; }
        public string CropName { get; set; }
        public List<_DayListModel> _DayList { get; set; }
    }
    public class _AgricultureMasterModel
    {
        public string CropType { get; set; }
        public List<_MAgricultureMasterModel> _OList { get; set; }
    }
    public class DayListModel
    {
        public int CommonId { get; set; }
        public int Day { get; set; }
        public int ToDays { get; set; }
        public decimal Area { get; set; }
        public string AreaType { get; set; }
        public List<DayWiseProductListModel> _DayWiseProductList { get; set; }
    }


    public class _DayListModel
    {
        public int CommonId { get; set; }
        public string Day { get; set; }
        public int ToDays { get; set; }
        public decimal Area { get; set; }
        public string AreaType { get; set; }
        public List<_DayWiseProductListModel> _DayWiseProductList { get; set; }
    }

    public class DayWiseProductListModel
    {
        public int _ChildCommonId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Qty { get; set; }
        public string Description { get; set; }
    }


    public class _DayWiseProductListModel
    {
        public int _ChildCommonId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }
    }
    public class CropDetailsFullDetailsModel
    {
        public int TotalDaysOfCrop { get; set; }
        public int Crop_ProductId { get; set; }
        public string CropCategory { get; set; }
        public long Crop_Id { get; set; }
        public string CropCode { get; set; }
        public int FK_LanguageId { get; set; }
        public string CropName { get; set; }
        public int GroupId { get; set; }
        public int KeyId { get; set; }
        public string FK_CropCode { get; set; }
        public int Days { get; set; }
        public string _Days { get; set; }
        public int ToDays { get; set; }
        public int Area { get; set; }
        public string AreaType { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductQty { get; set; }
        public decimal Price { get; set; }
        public string ProductUnit { get; set; }
        public decimal MRP { get; set; }
        public string Units { get; set; }
        public decimal PV { get; set; }
        public decimal BV { get; set; }
        public string Description { get; set; }
        public string CropType { get; set; }
        public string MethodOfApplication { get; set; }
    }

    public class AgricultureMasterFilter : MPaging
    {
        public string CropName { get; set; }
        public string CropCode { get; set; }
        public string CropType { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
    public class AgricultureMasterReportModel : MPaging
    {
        public List<AgricultureMasterModel> AgricultureMasterList { get; set; }
    }
    public class AgricultureMasterModel
    {
        public string CropType { get; set; }
        public string CropCategory { get; set; }
        public string MethodOfApplication { get; set; }
        public long CropId { get; set; }
        public string CropCode { get; set; }
        public string CropName { get; set; }
        public int TotalDaysOfCrop { get; set; }
        public int? TotalRecords { get; set; }
    }



    public class _CropDetailsFullDetailsModel
    {
        public string CropCode { get; set; }
        public string CropCategory { get; set; }
        public string ProductName { get; set; }
        public string CropName { get; set; }
        public string Days { get; set; }
        public int ToDays { get; set; }
        public decimal Area { get; set; }
        public string AreaType { get; set; }
        public decimal ProductQty { get; set; }
        public string ProductUnit { get; set; }
        public decimal MRP { get; set; }
        public decimal SalePrice { get; set; }
        public string ProductImage { get; set; }
        public string LanguageCode { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CropType { get; set; }
        public string MethodOfApplication { get; set; }
        public string Date { get; set; }
        public string DocumentUrl { get; set; }
        public int TotalDays { get; set; }
    }

}