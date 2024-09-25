using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALProductServices
    {
        public static AddUpdateDeleteModel BindCategoryList(CategoryModel obj)
        {
            AddUpdateDeleteModel _response = new AddUpdateDeleteModel();
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@CategoryID", obj.Id);
                queryParameters.Add("@ProcID", obj.ProcID);
                _response.Data = DBHelperDapper.DAAddAndReturnModelList<CategoryModel>("Proc_CategoryList", queryParameters);
                if (_response.Data != null)
                {
                    _response.Status = true;
                    _response.Msg = "success";
                }
                else
                {
                    _response.Status = false;
                    _response.Msg = "Data not found!!!";
                }
                return _response;
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Msg = ex.Message;
                return _response;
            }
        }

        public static ResponseUnitModel SaveCropCategorySave(CategoryModel obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@CropId", obj.Id);
                queryParameters.Add("@CropName", obj.CropName);
                queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);
                queryParameters.Add("@ProcId", obj.ProcID);
                queryParameters.Add("@imageUrl", obj.CropImage);
                ResponseUnitModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseUnitModel>("PROC_CropCategory", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseMaster Save(MAdminProductMaster obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ProductName", obj.ProductName);
                queryParameters.Add("@ProductDescription", obj.ProductDescription);
                queryParameters.Add("@MRP", obj.MRP);
                queryParameters.Add("@SalesPrice", obj.SalesPrice);
                queryParameters.Add("@PV", obj.PV);
                queryParameters.Add("@CGST", obj.CGST);
                queryParameters.Add("@IGST", obj.IGST);
                queryParameters.Add("@SGST", obj.SGST);
                queryParameters.Add("@Fk_ProductTypeId", obj.Fk_ProductTypeId);
                queryParameters.Add("@Fk_StatusId", obj.Fk_StatusId);
                queryParameters.Add("@Fk_UnitId", obj.Fk_UnitId);
                queryParameters.Add("@IsBox", obj.IsBox);
                queryParameters.Add("@BoxQty", obj.BoxQty);
                queryParameters.Add("@BoxPv", obj.BoxPV);
                queryParameters.Add("@Fk_OrbitType", obj.FK_OrbitType);
                ResponseMaster _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseMaster>("ProductMasterInsert", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResponseMaster UpdateKaryon_ProductMaster(MAdminProductMaster obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_ProductId", obj.Pk_ProductId);
                queryParameters.Add("@ProductName", obj.ProductName);
                queryParameters.Add("@ProductDescription", obj.ProductDescription);
                queryParameters.Add("@MRP", obj.MRP);
                queryParameters.Add("@SalesPrice", obj.SalesPrice);
                queryParameters.Add("@PV", string.IsNullOrEmpty(obj.PV) ? "0" : obj.PV);
                queryParameters.Add("@ProductImage", obj.ProductImage);
                queryParameters.Add("@CGST", obj.CGST);
                queryParameters.Add("@IGST", obj.IGST);
                queryParameters.Add("@SGST", obj.SGST);
                queryParameters.Add("@Fk_ProductTypeId", obj.Fk_ProductTypeId);
                queryParameters.Add("@Fk_StatusId", obj.Fk_StatusId);
                queryParameters.Add("@Fk_UnitId", obj.Fk_UnitId);
                queryParameters.Add("@IsBox", obj.IsBox);
                queryParameters.Add("@BoxQty", obj.BoxQty);
                queryParameters.Add("@BoxPv", obj.BoxPV);
                queryParameters.Add("@Fk_OrbitType", obj.FK_OrbitType);
                queryParameters.Add("@Updatedby", SessionManager.Fk_MemId);
                ResponseMaster _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseMaster>("ProductMasterUpdate", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MAdminProductMaster> GetProductList(MAdminProductMaster obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                List<MAdminProductMaster> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminProductMaster>("ProductMasterList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ProductViewModel> GetProductEdit(MAdminProductMaster obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ProductID", obj.Pk_ProductId);
                List<ProductViewModel> _iresult = DBHelperDapper.DAAddAndReturnModelList<ProductViewModel>("Proc_GetProductList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResponseMaster ProductDelete(string id)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_ProductId", id);

                ResponseMaster _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseMaster>("Proc_Karyon_ProductDelete", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static CropProductModel BindCropProduct(CropProductModel obj)
        {
            CropProductModel _response = new CropProductModel();
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Id", obj.Id);
                queryParameters.Add("@ProcID", obj.Crop_ProdId);
                _response.CropProductList = DBHelperDapper.DAAddAndReturnModelList<CropProductModel>("Proc_CropProductList", queryParameters);
                return _response;
            }
            catch (Exception ex)
            {
                _response.Flag = 0;
                _response.Msg = ex.Message;
                return _response;
            }
        }

        public static ResponseUnitModel SaveCropProduct(CropProductModel obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Id", obj.Id);
                queryParameters.Add("@CropName", obj.Crop_ProductName);
                queryParameters.Add("@CategoryID", obj.CategoryID);
                queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);
                queryParameters.Add("@ProcId", obj.Crop_ProdId);
                ResponseUnitModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseUnitModel>("PROC_CropProduct", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static SubCategoryViewModel BindCropSubCategory(SubCategoryViewModel obj)
        {
            var queryParameters = new DynamicParameters();
            //queryParameters.Add("@ProcID", obj.ProcID);
            queryParameters.Add("@Id", obj.Id);
            if (obj.Id != null && obj.Id > 0)
                obj = DBHelperDapper.DAAddAndReturnModel<SubCategoryViewModel>("Proc_CropProduct", queryParameters);
            else
                obj.SubCategoryList = DBHelperDapper.DAAddAndReturnModelList<SubCategoryViewModel>("Proc_CropProduct", queryParameters);
            return obj;
        }


        public static List<WebSitePopup>  ChangeWebSitePopup(WebSitePopup obj)
        {
            var queryParameters = new DynamicParameters();
           
            queryParameters.Add("@ImageUrl", obj.ImageUrl);
            queryParameters.Add("@IsActive", obj.IsActive);
            queryParameters.Add("@ActionType", obj.ActiveType);
            List<WebSitePopup> ObjList = DBHelperDapper.DAAddAndReturnModelList<WebSitePopup>("GetUpdateWebSitePopup", queryParameters);
            return ObjList;
        }
        public static WebSitePopup UpdateWebSitePopup(WebSitePopup obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", obj.Id);
            queryParameters.Add("@ImageUrl", obj.ImageUrl);
            queryParameters.Add("@IsActive", obj.IsActive);
            queryParameters.Add("@ActionType", obj.ActiveType);
            WebSitePopup ObjList = DBHelperDapper.DAAddAndReturnModel<WebSitePopup>("GetUpdateWebSitePopup", queryParameters);
            return ObjList;
        }
        public static WebSitePopup WebSitePopup()
        {
            var queryParameters = new DynamicParameters();

         
            queryParameters.Add("@ActionType","Select");
            WebSitePopup ObjList = DBHelperDapper.DAAddAndReturnModel<WebSitePopup>("GetUpdateWebSitePopup", queryParameters);
            return ObjList;
        }
        public static ResponseUnitModel SaveCropSubCategorySave(SubCategoryViewModel obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@CropId", obj.Id);
                queryParameters.Add("@CategoryID", obj.CategoryID);
                queryParameters.Add("@CropName", obj.Crop_ProductName);
                queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);
                queryParameters.Add("@ProcId", obj.ProcID);
                queryParameters.Add("@imageUrl", obj.ImageUrl);
                ResponseUnitModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseUnitModel>("Proc_CropSubCategory", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        #region
        //Bonanza

        public static ModelBonanzaMaster SaveBonanza(ModelBonanzaMaster obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@BonanzaName", obj.BonanzaName);
            queryParameters.Add("@FromDate", obj.FromDate);
            queryParameters.Add("@ToDate", obj.ToDate);
            queryParameters.Add("@Images", obj.Image);
            queryParameters.Add("@IsActive", obj.IsActive);
            ModelBonanzaMaster ObjList = DBHelperDapper.DAAddAndReturnModel<ModelBonanzaMaster>("SaveBonanza", queryParameters);
            return ObjList;
        }


        public static ModelBonanzaMaster UpdateBonanzaMaster(ModelBonanzaMaster obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@Pk_BonanzaId", obj.Pk_BonanzaId);
            queryParameters.Add("@BonanzaName", obj.BonanzaName);
            queryParameters.Add("@FromDate", obj.FromDate);
            queryParameters.Add("@ToDate", obj.ToDate);
            queryParameters.Add("@Image", obj.Image);
            queryParameters.Add("@Status", obj.IsActive);
            ModelBonanzaMaster ObjList = DBHelperDapper.DAAddAndReturnModel<ModelBonanzaMaster>("BonanzaMasterUpdate", queryParameters);
            return ObjList;
        }



        public static ModelBonanzaMaster DeleteBonanzaMaster(ModelBonanzaMaster obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@Pk_BonanzaId", obj.Pk_BonanzaId);
            ModelBonanzaMaster ObjList = DBHelperDapper.DAAddAndReturnModel<ModelBonanzaMaster>("BonanzaMasterDelete", queryParameters);
            return ObjList;
        }
        public static List<ModelBonanzaMaster> BonanzaMasterDetailList(ModelBonanzaMaster obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@PK_RewardId", obj.Pk_BonanzaId);
            List<ModelBonanzaMaster> ObjList = DBHelperDapper.DAAddAndReturnModelList<ModelBonanzaMaster>("BonanzaList", queryParameters);
            return ObjList;
        }
        public static ModelBonanzaMaster BonanzaMasterDetailById(ModelBonanzaMaster obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@PK_RewardId", obj.Pk_BonanzaId);
            ModelBonanzaMaster ObjList = DBHelperDapper.DAAddAndReturnModel<ModelBonanzaMaster>("BonanzaList", queryParameters);
            return ObjList;
        }


        public static ModelBonanzaDetail SaveBonanzaDetail(ModelBonanzaDetail obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@BonanzaDetailName", obj.BonanzaDetailName);
            queryParameters.Add("@BusinessAmount", obj.BusinessAmount);
            queryParameters.Add("@ImageUrl", obj.BonanzaDetailImage);
            queryParameters.Add("@CreatedBy", obj.CreatedBy);
            queryParameters.Add("@FK_BonanzaId", obj.Fk_BonanzaMasterId);
            queryParameters.Add("@BonanzaTarget", obj.BonanzaTarget);
            ModelBonanzaDetail ObjList = DBHelperDapper.DAAddAndReturnModel<ModelBonanzaDetail>("SaveBonanzaDetails", queryParameters);
            return ObjList;
        }

        public static List<ModelBonanzaDetail> BonanzaDetailList(ModelBonanzaDetail obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@PK_BonanzaDetailsId", obj.Pk_BonanzaId);
            List<ModelBonanzaDetail> ObjList = DBHelperDapper.DAAddAndReturnModelList<ModelBonanzaDetail>("BonanzaDetails", queryParameters);
            return ObjList;
        }

        public static ModelBonanzaDetail BonanzaDetailListById(ModelBonanzaDetail obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@PK_BonanzaDetailsId", obj.Pk_BonanzaId);
            ModelBonanzaDetail ObjList = DBHelperDapper.DAAddAndReturnModel<ModelBonanzaDetail>("BonanzaDetails", queryParameters);
            return ObjList;
        }



        public static ModelBonanzaMaster DeleteBonanzaDetail(ModelBonanzaMaster obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@PK_BonanzaDetailsId", obj.Pk_BonanzaId);
            queryParameters.Add("@DeletedBy", obj.CreatedBy);
            ModelBonanzaMaster ObjList = DBHelperDapper.DAAddAndReturnModel<ModelBonanzaMaster>("BonanzaDetailsDelete", queryParameters);
            return ObjList;
        }
        public static ModelBonanzaDetail UpdateBonanzaDetails(ModelBonanzaDetail obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@PK_BonanzaDetailsId", obj.Pk_BonanzaDetailId);
            queryParameters.Add("@BonanzaDetailName", obj.BonanzaDetailName);
            queryParameters.Add("@BusinessAmount", obj.BusinessAmount);
            queryParameters.Add("@ImageUrl", obj.BonanzaDetailImage);
            queryParameters.Add("@UpdateBy", obj.CreatedBy);
            queryParameters.Add("@FK_BonanzaId", obj.Fk_BonanzaMasterId);
            queryParameters.Add("@BonanzaTarget", obj.BonanzaTarget);
            ModelBonanzaDetail ObjList = DBHelperDapper.DAAddAndReturnModel<ModelBonanzaDetail>("BonanzaDetailUpdate", queryParameters);
            return ObjList;
        }

        #endregion



        #region
        //Reward

        public static List<ModelRewardMaster> RewardMasterDetailList(ModelRewardMaster obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@PK_RewardId", obj.Pk_RewardId);
            List<ModelRewardMaster> ObjList = DBHelperDapper.DAAddAndReturnModelList<ModelRewardMaster>("RewardList", queryParameters);
            return ObjList;
        }
        public static ModelRewardMaster RewardMasterDetailListByid(ModelRewardMaster obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@PK_RewardId", obj.Pk_RewardId);
            ModelRewardMaster ObjList = DBHelperDapper.DAAddAndReturnModel<ModelRewardMaster>("RewardList", queryParameters);
            return ObjList;
        }
        public static ModelRewardMaster Savereward(ModelRewardMaster obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@RewardName", obj.RewardName);
            queryParameters.Add("@FromDate", obj.FromDate); 
            queryParameters.Add("@ToDate", obj.ToDate);
            queryParameters.Add("@Images", obj.Image);
            queryParameters.Add("@IsActive", obj.IsActive);
            ModelRewardMaster ObjList = DBHelperDapper.DAAddAndReturnModel<ModelRewardMaster>("SaveReward", queryParameters);
            return ObjList;
        }

        public static ModelRewardMaster Updatereward(ModelRewardMaster obj)
        {
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@PkRewardId", obj.Pk_RewardId);
            queryParameters.Add("@RewardName", obj.RewardName);
            queryParameters.Add("@FromDate", obj.FromDate);
            queryParameters.Add("@ToDate", obj.ToDate);
            queryParameters.Add("@Images", obj.Image);
            queryParameters.Add("@IsActive", obj.IsActive);
            ModelRewardMaster ObjList = DBHelperDapper.DAAddAndReturnModel<ModelRewardMaster>("UpdateReward", queryParameters);
            return ObjList;
        }
        public static List<ModelRewardDetail> RewardDetailList(ModelRewardDetail obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@PK_RewardDetailsId", obj.Pk_BonanzaId);
            List<ModelRewardDetail> ObjList = DBHelperDapper.DAAddAndReturnModelList<ModelRewardDetail>("RewardDetailList", queryParameters);
            return ObjList;
        }

        public static ModelRewardDetail SaveRewardDetail(ModelRewardDetail obj)
        {
            ModelRewardDetail ObjList = new ModelRewardDetail();
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@RewardDetailName", obj.RewardDetailName);
                queryParameters.Add("@BusinessAmount", obj.BusinessAmount);
                queryParameters.Add("@ImageUrl", obj.RewardDetailImage);
                queryParameters.Add("@CreatedBy", obj.CreatedBy);
                queryParameters.Add("@FK_RewardId", obj.Fk_RewardMasterId);
                queryParameters.Add("@RewardTarget", obj.RewardTarget);

                queryParameters.Add("@Day", obj.DayDuration);
                queryParameters.Add("@NoOfPairs", obj.NoOfPairs);
                queryParameters.Add("@Rank", obj.Rank);



                ObjList = DBHelperDapper.DAAddAndReturnModel<ModelRewardDetail>("SaveRewardDetails", queryParameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return ObjList;
        }
        public static ModelRewardMaster DeleteRewardDetail(ModelRewardMaster obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@PK_RewardDetailsId", obj.Pk_RewardId);
            queryParameters.Add("@DeletedBy", obj.CreatedBy);
            ModelRewardMaster ObjList = DBHelperDapper.DAAddAndReturnModel<ModelRewardMaster>("RewardDetailsDelete", queryParameters);
            return ObjList;
        }

        public static ModelRewardMaster DeleteRewardMaster(ModelRewardMaster obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@Pk_RewardId", obj.Pk_RewardId);
            ModelRewardMaster ObjList = DBHelperDapper.DAAddAndReturnModel<ModelRewardMaster>("RewardMasterDelete", queryParameters);
            return ObjList;
        }

        public static ModelRewardDetail UpdateRewardDetails(ModelRewardDetail obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@PK_RewardDetailsId", obj.Pk_RewardDetailId);
            queryParameters.Add("@RewardDetailName", obj.RewardDetailName);
            queryParameters.Add("@RewardAmount", obj.BusinessAmount);
            queryParameters.Add("@DayDuration", obj.DayDuration);
            queryParameters.Add("@NoOfPairs", obj.NoOfPairs);
            queryParameters.Add("@ImageUrl", obj.RewardDetailImage);
            queryParameters.Add("@UpdateBy", obj.CreatedBy);
            queryParameters.Add("@Rank", obj.Rank);
            queryParameters.Add("@RewardTarget", obj.RewardTarget);
            ModelRewardDetail ObjList = DBHelperDapper.DAAddAndReturnModel<ModelRewardDetail>("RewardDetailUpdate", queryParameters);
            return ObjList;
        }

        public static ModelRewardDetail RewardDetailListById(ModelRewardDetail obj)
        {
            var queryParameters = new DynamicParameters();

            queryParameters.Add("@PK_RewardDetailsId", obj.Pk_RewardDetailId);
            ModelRewardDetail ObjList = DBHelperDapper.DAAddAndReturnModel<ModelRewardDetail>("RewardDetailList", queryParameters);
            return ObjList;
        }


        #endregion
    }
}