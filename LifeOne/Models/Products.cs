using LifeOne.Models.API.DAL;
using System;
using LifeOne.Models.Common;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DBHelper = LifeOne.Models.Common.DBHelper;
using DALCommon = LifeOne.Models.Common.DALCommon;
using Dapper;
using LifeOne.Models.Manager;
using Newtonsoft.Json;
using LifeOne.Models.AdminManagement.AEntity;

namespace LifeOne.Models
{
    public class Products:MPaging
    {
        public int? Pk_ProductId { get; set; }        
        public int? Fk_ProductId { get; set; }        
        public string id { get; set; }        
        public string SubTotal { get; set; }       
        public string ProductName { get; set; }       
        public int? Quantity { get; set; }
        public string Update { get; set; }
        public int? OpCode { get; set; }
        public int Pk_ProductTypeId { get; set; }
        public int Fk_CategoryId { get; set; }
        public bool IsAscDesc { get; set; }
        public string Rating { get; set; }
        public string FkMemId { get; set; }
        public int? FK_MemId { get; set; }
        public int? Pk_Id { get; set; }
        public int? Qty { get; set; }
        public string Status { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
        public string ProductType { get; set; }
        public DataTable DtDetails { get; set; }
        public DataTable DtDetails2 { get; set; }
        public DataTable DtDetailsecond { get; set; }
       
        public string TokenNo { get; set; }
        public string TotalAmount { get; set; }
        public string OfferedPrice { get; set; }
        public string DP { get; set; }
        public string PV { get; set; }
        public string MRP { get; set; }
        public string SalesPrice { get; set; }
        public string Url { get; set; }
        public string ProductImage { get; set; }
        public string ProductTypeImage { get; set; }
        public string PrimaryImage { get; set; }
        public string Review { get; set; }
        public string ReviewCount { get; set; }
        public string AvgRating { get; set; }       
        public string baseurl { get; set; }       
        public string ProductDescription { get; set; }
        public DataTable dtCategory { get; set; }
        public List<Products> objList { get; set; }
        public List<Products> LstMenu { get; set; }
        public List<Products> lstDetails { get; set; }
        public List<WebSitePopup> websiteList { get; set; }
        public DataTable dtVedioLink { get; set; }
        public DataSet GetAllProducts()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Pk_ProductId", Pk_ProductId),
                                        new SqlParameter("@ProductName", ProductName),
                                        new SqlParameter("@Fk_CategoryId", Fk_CategoryId),
                                        


            };
            DataSet ds = DBHelper.ExecuteQuery("GetProductDetails", para);
            return ds;
        }
        public List<Products> GetAllProductsDetail()
        {
            try
            {

                string _qury = string.Empty;
               
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_ProductId", Pk_ProductId);
                queryParameters.Add("@ProductName", ProductName);
                queryParameters.Add("@Fk_CategoryId", Fk_CategoryId);
                List<Products> _iresult = DBHelperDapper.DAAddAndReturnModelList<Products>("GetProductDetails", queryParameters);
                //_iresult = DBHelperDapper.DAAddAndReturnModelList<Products>("GetProductDetails", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet GetAllProductAccPageSize()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Pk_ProductId", Pk_ProductId),
                                        new SqlParameter("@ProductName", ProductName),
                                        new SqlParameter("@Fk_CategoryId", Fk_CategoryId),
                                        new SqlParameter("@Page", Page),
                                        new SqlParameter("@Size", Size),



            };
            DataSet ds = DBHelper.ExecuteQuery("GetProductDetailsPagination", para);
            return ds;
        }
        public DataSet ManageShoppingCart()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@FK_MemId", FK_MemId),
                                        new SqlParameter("@Fk_ProductId", Pk_ProductId),
                                         new SqlParameter("@TokenNo", TokenNo),
                                        new SqlParameter("@Quantity", Quantity),
                                        new SqlParameter("@OpCode", OpCode),
                                        new SqlParameter("@Pk_Id", Pk_Id),

            };
            DataSet ds = DBHelper.ExecuteQuery("ManageShoppingCart", para);
            return ds;
        }
        public DataSet GenerateShoppingToken()
        {

            DataSet ds = DBHelper.ExecuteQuery("GenerateToken");

            return ds;
        }

        public DataSet GetAllCategory()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@PK_ProductTypeId", Pk_ProductTypeId),
                                        new SqlParameter("@IsAscDesc", IsAscDesc),
                                        new SqlParameter("@Status", Status),
                                        new SqlParameter("@Page", Page),
                                        new SqlParameter("@Size", Size),

            };
            DataSet ds = DBHelper.ExecuteQuery("GetProductTypeMaster", para);
            return ds;
        }

        public List<Products> GetAllCategoryDetail()
        {
            try
            {

                string _qury = string.Empty;
                List<Products> _iresult = null;
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@PK_ProductTypeId", Pk_ProductTypeId);
                queryParameters.Add("@IsAscDesc", IsAscDesc);
                queryParameters.Add("@Status", Status);
                queryParameters.Add("@Page", Page);
                queryParameters.Add("@Size", Size);

                _iresult = DBHelperDapper.DAAddAndReturnModelList<Products>("GetProductTypeMaster", queryParameters);



                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<Products> GetAllCartList()
        {
            try
            {
                
                string _qury = string.Empty;
                List<Products> _iresult = null;
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Fk_ProductId", Pk_ProductId);
                queryParameters.Add("@TokenNo", TokenNo);
                queryParameters.Add("@Quantity", Quantity);
                queryParameters.Add("@OpCode", OpCode);
                queryParameters.Add("@Pk_Id", Pk_Id);

                _iresult = DBHelperDapper.DAAddAndReturnModelList<Products>("ManageShoppingCart", queryParameters);



                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet GetProductReview()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_MemId",FkMemId),
                                        new SqlParameter("@OpCode",OpCode),
                                        new SqlParameter("@Rating", Rating),
                                        new SqlParameter("@Review", Review),
                                        new SqlParameter("@Pk_ProductId", Pk_ProductId)


            };
            DataSet ds = DBHelper.ExecuteQuery("ProductReviewRating", para);
            return ds;
        }
        //public DataSet ManageShoppingCart()
        //{
        //    SqlParameter[] para = {
        //                                new SqlParameter("@Fk_ProductId", Pk_ProductId),
        //                                 new SqlParameter("@TokenNo", TokenNo),
        //                                new SqlParameter("@Quantity", Quantity),
        //                                new SqlParameter("@OpCode", OpCode),
        //                                new SqlParameter("@Pk_Id", Pk_Id),

        //    };
        //    DataSet ds = DBHelper.ExecuteQuery("ManageShoppingCart", para);
        //    return ds;
        //}

        public DataSet getvedioLink()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] para = {

                };
                ds = DBHelper.ExecuteQuery("WebsiteVideoList", para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

    }
    public class ProductsDetail
    {
        public string ProductName { get; set; }
        public int? Qty { get; set; }
        public string PK_Id { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public decimal BV { get; set; }
        public string PK_OfferMasterId { get; set; }
        public string CategoryName { get; set; }
        public string PurchasePrice { get; set; }
        public string MRP { get; set; }
        public string PV { get; set; }
        public string TotalAmount { get; set; }
        public string CGST { get; set; }
        public string IGST { get; set; }
        public string SGST { get; set; }
        public string OfferedPrice { get; set; }
        public string SupplierName { get; set; }
        public string BillNo { get; set; }
        public string PayMode { get; set; }
        public int? Page { get; set; }
        public int? Quantity { get; set; }
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }
        public string DirectionOfUse { get; set; }
        public string Avalibility { get; set; }
        public string Doses { get; set; }
        public string BalanceQuantity { get; set; }
        public int Pk_ProductId { get; set; }
        public long Fk_MemId { get; set; }
        public string FkMemId { get; set; }
        public string Pk_SupplierId { get; set; }
        public string LoginId { get; set; }
        public DataTable dtImages { get; set; }
        
        public DataTable dtProductQuantity { get; set; }
        public DataTable dtTopProducts { get; set; }
        public List<ProductsDetail> LstOrder { get; set; }
        
        public long CreatedBy { get; set; }
        public string BillDate { get; set; }
        public string GSTNo { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string PaymentRemark { get; set; }
        public string ChequeDDNo { get; set; }
        public string PaymentDate { get; set; }
        public decimal DP { get; set; }
        public string ChequeDDDate { get; set; }
        public string BankName { get; set; }
        public string PaymentMode { get; set; }
        public string HdnPaymentSlip { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        
        public DataTable dtDetails { get;  set; }
        public DataTable dtProductDetails { get;  set; }
        public int OpCode { get;  set; }
        public string PinCode { get;  set; }
        public string Rating { get;  set; }
        public string Name { get;  set; }
        public string Email { get;  set; }
        public string Review { get;  set; }
        public string ReviewCount { get;  set; }
        public string AvgRating { get;  set; }
        public string CreatedDate { get;  set; }
        public string updateDate { get;  set; }
        public string profilepic { get;  set; }
        public string Fk_CategoryId { get; set; }
        //public int Pk_ProductTypeId { get; set; }
        //public int Fk_CategoryId { get; set; }
        //public bool IsAscDesc { get; set; }
        //public int Size { get; set; }

        public DataSet GetAllProducts()
        {
            SqlParameter[] para = {
                                         //new SqlParameter("@Pk_ProductId", Pk_ProductId),
                                         //new SqlParameter("@ProductName", ProductName),
                                        //new SqlParameter("@Fk_CategoryId", Fk_CategoryId),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetProductMaster", para);
            return ds;
        }
    

        public DataSet AddTempStock()
        {
            try
            {
                SqlParameter[] para = {

                                        new SqlParameter("@Pk_ProductId", Pk_ProductId),
                                        new SqlParameter("@Quantity", Quantity),
                                        new SqlParameter("@CreatedBy", CreatedBy),
                                        new SqlParameter("@PurchasePrice",PurchasePrice)

                 };
                DataSet ds = DBHelper.ExecuteQuery("StockDetailsTemp", para);
                return ds;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet AddAdminStock()
        {
            try
            {
                SqlParameter[] para = {
                        new SqlParameter("@CreatedBy", CreatedBy),
                        new SqlParameter("@SupplierId", Pk_SupplierId),
                        new SqlParameter("@BillNo", BillNo),
                        new SqlParameter("@BillDate", BillDate),
                        new SqlParameter("@TransactionNo", ChequeDDNo),
                        new SqlParameter("@PaymentMode", PaymentMode),
                        new SqlParameter("@TransactionDate", ChequeDDDate),
                        new SqlParameter("@BankName", BankName)
                 };
                DataSet ds = DBHelper.ExecuteQuery("GeneratePurchaseOrder", para);
                return ds;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet DeleteStock()
        {
            try
            {
                SqlParameter[] para = {
                                        new SqlParameter("@PK_Id", PK_Id),


                 };
                DataSet ds = DBHelper.ExecuteQuery("DeleteStocktemp", para);
                return ds;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet GetProductGSTDetails(int Pk_ProductId)
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Pk_ProductId", Pk_ProductId)

            };
            DataSet ds = DBHelper.ExecuteQuery("GetProductGSTDetails", para);   
            return ds;
        }
        public DataSet GetSupplierDetails()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Pk_SupplierId", Pk_SupplierId),
                                        new SqlParameter("@OpCode",OpCode),
                                        new SqlParameter("@SupplierName",SupplierName),
                                        new SqlParameter("@Mobile",Mobile),
                                        new SqlParameter("@PinCode",PinCode),
                                        new SqlParameter("@Address",Address),
                                        new SqlParameter("@GSTNo",GSTNo),
                                        new SqlParameter("@AddedBy",Fk_MemId)

            };
            DataSet ds = DBHelper.ExecuteQuery("SupplierMaster", para);
            return ds;
        }
        public List<ProductsDetail> GetSupplierDetailsList()
        {
            try
            {

                string _qury = string.Empty;
                List<ProductsDetail> _iresult = null;
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_SupplierId", Pk_SupplierId);
                queryParameters.Add("@OpCode", OpCode);
                queryParameters.Add("@SupplierName", SupplierName);
                queryParameters.Add("@OpCode", OpCode);
                queryParameters.Add("@Mobile", Mobile);
                queryParameters.Add("@PinCode", PinCode);
                queryParameters.Add("@Address", Address);
                queryParameters.Add("@GSTNo", GSTNo);
                queryParameters.Add("@AddedBy", Fk_MemId);

                _iresult = DBHelperDapper.DAAddAndReturnModelList<ProductsDetail>("SupplierMaster", queryParameters);



                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet InsertOfferDateMaster()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@PK_OfferMasterId",PK_OfferMasterId),
                                        new SqlParameter("@OpCode",OpCode),
                                        new SqlParameter("@FromDate",FromDate),
                                        new SqlParameter("@ToDate ",ToDate),
                                        new SqlParameter("@BV",BV),
                                        new SqlParameter("@CreatedBy",CreatedBy),
            };
            DataSet ds = DBHelper.ExecuteQuery("Inserttbl_OfferMaster", para);
            return ds;
        }

        public DataSet GetProductReview()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_MemId",FkMemId),
                                        new SqlParameter("@OpCode",OpCode),
                                        new SqlParameter("@Rating", Rating),
                                        new SqlParameter("@Review", Review),
                                        new SqlParameter("@Pk_ProductId", Pk_ProductId)


            };
            DataSet ds = DBHelper.ExecuteQuery("ProductReviewRating", para);
            return ds;
        }
        //public DataSet GetAllCategory()
        //{
        //    SqlParameter[] para = {
        //                                new SqlParameter("@PK_ProductTypeId", Pk_ProductTypeId),
        //                                new SqlParameter("@IsAscDesc", IsAscDesc),
        //                                new SqlParameter("@Status", Status),
        //                                new SqlParameter("@Page", Page),
        //                                new SqlParameter("@Size", Size),

        //    };
        //    DataSet ds = DBHelper.ExecuteQuery("GetProductTypeMaster", para);
        //    return ds;
        //}


    }
}


