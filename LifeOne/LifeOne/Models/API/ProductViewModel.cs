using LifeOne.Models.Common;
using LifeOne.Models.FranchiseManagement.FEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class ProductViewModel
    {
        public long flag { get; set; }
        public long ProductId { get; set; }
        public long MagentoProdId { get; set; }
        public long KeyId { get; set; }
        public decimal Size { get; set; }
        public string ProductName { get; set; }
        public string ProductSKU { get; set; }
        public string ProductImage { get; set; }
        public decimal MRP { get; set; }

        public string Units { get; set; }
        public string Message { get; set; }
        public string ProdID { get; set; }
        public string ProductDescription { get; set; }
        public decimal PV { get; set; }
        public decimal BV { get; set; }
        public decimal DP { get; set; }
        public decimal Price { get; set; }
        public decimal GST { get; set; }
        public decimal SalesPrice { get; set; }
        public int OptionTypeId { get; set; }
        public int ToatlRecord { get; set; }
        public List<ProductUnitSizeViewModel> ProductUnitSizeList { get; set; }
        public List<ProductViewModel> ProductList { get; set; }
        public Pager Pager { get; set; }
        public int page { get; set; }
        public int FK_OrbitType { get; set; }
        public int BoxSize { get; set; }
        public decimal BoxPv { get; set; }
        public int BoxQty { get; set; }
        public int ProdId { get; set; }

        public int CmpID { get; set; }
    }

    public class ProductUnitSizeViewModel
    {
        public long KeyId { get; set; }
        public decimal Size { get; set; }
        public decimal Price { get; set; }
        public int OptionTypeId { get; set; }
    }
    public class ProductPurchaseHistory
    {
        public int PK_OrderId { get; set; }
        public string CropName { get; set; }
        public string CropCode { get; set; }
        public string OrderNo { get; set; }
        public decimal Area { get; set; }
        public string CreateDate { get; set; }
        public string SowingDate { get; set; }
    }
    public class ProductPurchaseHistoryResponse
    {
        public List<ProductPurchaseHistory> result { get; set; }
        public string Message { get; set; }
        public string Response { get; set; }
    }


    public class ProductDetialReq
    {
        public string CropCode { get; set; }
        public double Fk_MemId { get; set; }
        public string PK_OrderId { get; set; }
        public string LanguageCode { get; set; }

    }

    public class ProductPurchaseHistoryDetail
    {
        public string Response { get; set; }
        public string Message { get; set; }
        public List<ProductDetialListModel> result { get; set; }
    }

    public class ProductDetialListModel
    {
        public string CropType { get; set; }
        public string MethodOfApplication { get; set; }
        public List<ProductDetialListModel1> ProductDetialList { get; set; }
    }

    public class ProductDetialListModel1
    {

        public string Days { get; set; }
        public string Date { get; set; }
        public List<ProductDetailbyLangunage> ProductDetailbyLangunageList { get; set; }
    }

    public class ProductDetailbyLangunage3
    {
        public string Response { get; set; }
        public string Message { get; set; }
        public List<ProductDetialListModel> result { get; set; }
    }

    public class ProductDetailbyLangunage
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
        public string Date { get; set; }
        public string DocumentUrl { get; set; }

    }

    public class MProductListAPIResponse
    {
        public int Flag { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public List<ProductViewModel> _ProductViewModellist { get; set; }

    }

    public class MOrderteampForKaryonResponse
    {
        public int Flag { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public List<MFranchiseorderRequest> _tempOrderDetails { get; set; }

    }
    public class MorderDetailsKaryonResponse
    {
        public int Flag { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public List<MOrder> _tempOrderDetails { get; set; }

    }

    public class MshippinginformationResponseAPP
    {
        public int Flag { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public List<LifeOne.Models.AdminManagement.AEntity.MAddShippingInformation> shippingdetails { get; set; }

    }


    public class MBindProductCompanyREponse
    {
        public int Flag { get; set; }
        public string response { get; set; }
        public string message { get; set; }
        public List<MBindCompanyList> _ProductViewModellist { get; set; }

    }

    public class MBindCompanyList
    {
        public int ProdId { get; set; }
        public int CmpID { get; set; }
        public string CmpName { get; set; }

    }
}