using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeOne.Models.AdminManagement.ADAL
{
	public class AgricultureMasterDAL
    {
        private static string connectionString = string.Empty;
        public static string connection()
        {
            try
            {
                return connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            }
            catch (Exception)
            {
                //todo error handling  mechanism
                throw;
            }
        }
        public static List<ProductMasterModel> BindProductMster()
        {
            try
            {
                var queryParameters = new DynamicParameters();
                List<ProductMasterModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<ProductMasterModel>("ProBindProduct", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<LanguageMasterModel> BindAllLanguageMster()
        {
            try
            {
                var queryParameters = new DynamicParameters();
                List<LanguageMasterModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<LanguageMasterModel>("ProGetAllLanguage", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static AddUpdateResponseResponseModel AddMaster(string s1, string s2)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@CropDetail", s1);
                queryParameters.Add("@CropName", s2);
                AddUpdateResponseResponseModel _iresult = DBHelperDapper.DAAddAndReturnModel<AddUpdateResponseResponseModel>("Proc_InsertCrop", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static AddUpdateResponseResponseModel UpdateCropMaster(string XML, string CropCode, string CropType)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@CropCode", CropCode);
                queryParameters.Add("@CropType", CropType);
                queryParameters.Add("@CropDetail", XML);
                AddUpdateResponseResponseModel _iresult = DBHelperDapper.DAAddAndReturnModel<AddUpdateResponseResponseModel>("ProcUpdateCrop", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MAgricultureMasterModel GetCropDetailsByCropCode(string CropCode, string CropType)
        {
            try
            {
                MAgricultureMasterModel _result = new MAgricultureMasterModel();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@CropCode", CropCode);
                queryParameters.Add("@CropType", CropType);
                List<CropDetailsFullDetailsModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<CropDetailsFullDetailsModel>("ProGetCropDetailsForEdit", queryParameters);
                _result = _iresult.GroupBy(x => x.CropCode).Select(x => new MAgricultureMasterModel
                {
                    CropType = x.FirstOrDefault().CropType,
                    CropCategory = x.FirstOrDefault().CropCategory,
                    MethodOfApplication = x.FirstOrDefault().MethodOfApplication,
                    CropId = x.FirstOrDefault().Crop_Id,
                    CropCode = x.FirstOrDefault().CropCode,
                    CropName = x.FirstOrDefault().CropName,
                    TotalDaysOfCrop = x.FirstOrDefault().TotalDaysOfCrop,
                    Crop_ProductId=x.FirstOrDefault().Crop_ProductId ,
                    _DayList = x.GroupBy(y => new { y.Days }).Select(y => new DayListModel
                    {
                        Day = y.FirstOrDefault().Days,
                        ToDays = y.FirstOrDefault().ToDays,
                        Area = y.FirstOrDefault().Area,
                        AreaType = y.FirstOrDefault().AreaType,
                        _DayWiseProductList = y.Where(p => p.Days == y.Key.Days).Select(z => new DayWiseProductListModel
                        {
                            ProductId = z.ProductId,
                            ProductName = z.ProductName,
                            Price = z.Price,
                            Quantity = z.ProductQty,
                            Description = z.Description
                        }).ToList()

                    }).ToList()
                }).FirstOrDefault();
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<_AgricultureMasterModel> _GetCropDetailsByCropCodeReport(string CropCode, string CropType)
        {
            try
            {
                List<_AgricultureMasterModel> _Response = new List<_AgricultureMasterModel>();
                _MAgricultureMasterModel _result = new _MAgricultureMasterModel();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@CropCode", CropCode);

                List<_CropDetailsFullDetailsModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<_CropDetailsFullDetailsModel>("ProGetCropDetails_Report", queryParameters);  

                _Response = _iresult.GroupBy(z => z.CropType).Select(z => new _AgricultureMasterModel
                {
                    CropType = z.FirstOrDefault().CropType,
                    _OList = z.GroupBy(p => new { p.CropCode }).Select(x => new _MAgricultureMasterModel
                    {
                        CropType = x.FirstOrDefault().CropType,
                        CropCategory = x.FirstOrDefault().CropCategory,
                        MethodOfApplication = x.FirstOrDefault().MethodOfApplication,
                        CropCode = x.FirstOrDefault().CropCode,
                        CropName = x.FirstOrDefault().CropName,
                        TotalDaysOfCrop = x.FirstOrDefault().TotalDays,
                        _DayList = x.GroupBy(y => new { y.ToDays }).Select(y => new _DayListModel
                        {
                            Day = y.FirstOrDefault().Days,
                            ToDays = y.FirstOrDefault().ToDays,
                            Area = y.FirstOrDefault().Area,
                            AreaType = y.FirstOrDefault().AreaType,
                            _DayWiseProductList = y.Where(p => p.ToDays == y.Key.ToDays).Select(zz => new _DayWiseProductListModel
                            {
                                ProductName = zz.ProductName,
                                Price = zz.SalePrice,
                                Quantity = zz.ProductQty,
                                Description = zz.Description
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList();
                return _Response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<_AgricultureMasterModel> _GetCropDetailsByCropCode(string CropCode, int? FKMemid, int PK_OrderId, string LanguageCode)
        {
            try
            {
                List<_AgricultureMasterModel> _Response = new List<_AgricultureMasterModel>();
                _MAgricultureMasterModel _result = new _MAgricultureMasterModel();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@CropCode", CropCode);
                queryParameters.Add("@FKMemid", FKMemid);
                queryParameters.Add("@PK_OrderId", PK_OrderId);
                queryParameters.Add("@LanguageCode", LanguageCode);
                List<_CropDetailsFullDetailsModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<_CropDetailsFullDetailsModel>("Proc_GetProductPurchaseHistoryDetail_Karyon", queryParameters);

                _Response = _iresult.GroupBy(z => z.CropType).Select(z => new _AgricultureMasterModel
                {
                    CropType = z.FirstOrDefault().CropType,
                    _OList = z.GroupBy(p => new { p.CropCode }).Select(x => new _MAgricultureMasterModel
                    {
                        CropType = x.FirstOrDefault().CropType,
                        CropCategory = x.FirstOrDefault().CropCategory,
                        MethodOfApplication = x.FirstOrDefault().MethodOfApplication,
                        CropCode = x.FirstOrDefault().CropCode,
                        CropName = x.FirstOrDefault().CropName,
                        TotalDaysOfCrop = x.FirstOrDefault().TotalDays,
                        _DayList = x.GroupBy(y => new { y.ToDays }).Select(y => new _DayListModel
                        {
                            Day = y.FirstOrDefault().Days,
                            ToDays = y.FirstOrDefault().ToDays,
                            Area = y.FirstOrDefault().Area,
                            AreaType = y.FirstOrDefault().AreaType,
                            _DayWiseProductList = y.Where(p => p.ToDays == y.Key.ToDays).Select(zz => new _DayWiseProductListModel
                            {
                                ProductName = zz.ProductName,
                                Price = zz.SalePrice,
                                Quantity = zz.ProductQty,
                                Description = zz.Description
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList();
                return _Response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<AgricultureMasterModel> BindAgricultureMasterReport(AgricultureMasterFilter req)
        {
            try
            {
                List<AgricultureMasterModel> _result = new List<AgricultureMasterModel>();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FromDate", req.FromDate);
                queryParameters.Add("@ToDate", req.ToDate);
                queryParameters.Add("@CropCode", req.CropCode);
                queryParameters.Add("@CropType", req.CropType);
                queryParameters.Add("@CropName", req.CropName);
                queryParameters.Add("@PageNo", req.Page);
                queryParameters.Add("@PageSize", SessionManager.Size);
                _result = DBHelperDapper.DAAddAndReturnModelList<AgricultureMasterModel>("ProGetAllCropDetails", queryParameters);
                return _result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static AddUpdateResponseResponseModel DeleteCrop(string CropCode, string CropType)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@CropCode", CropCode);
                queryParameters.Add("@CropType", CropType);
                AddUpdateResponseResponseModel _iresult = DBHelperDapper.DAAddAndReturnModel<AddUpdateResponseResponseModel>("Proc_DeleteCrop", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<CropCategoryModel> BindCropCategoryList()
        {
            try
            {
                var queryParameters = new DynamicParameters();
                List<CropCategoryModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<CropCategoryModel>("GetCropCategoryList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<CropProductModel> BindCropProductByCategoyId(int CategoryId)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@CategoryId", CategoryId);
                List<CropProductModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<CropProductModel>("GetCropProductByCategoryId", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}