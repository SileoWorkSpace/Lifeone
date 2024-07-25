using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public static class ProductService
    {
        public static AddUpdateDeleteModel BindCategoryList(CategoryModel model)
        {
            return DALProductServices.BindCategoryList(model);
        }

        public static List<WebSitePopup> ChangeWebSitePopup(WebSitePopup model)
        {
            return DALProductServices.ChangeWebSitePopup(model);
        }
        public static WebSitePopup UpdateWebSitePopup(WebSitePopup model)
        {
            return DALProductServices.UpdateWebSitePopup(model);
        }
        
        public static ResponseUnitModel SaveCropCategorySave(CategoryModel model)
        {
            ResponseUnitModel _objResponseModel = new ResponseUnitModel();
            try
            {
                _objResponseModel = DALProductServices.SaveCropCategorySave(model);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public static List<MAdminProductMaster> Karyon_ProductMasterGetService(MAdminProductMaster _obj)
        {
            List<MAdminProductMaster> _objResponseModel = new List<MAdminProductMaster>();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALProductServices.GetProductList(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public static ResponseMaster Karyon_ProductMasterSaveService(MAdminProductMaster _obj)
        {
            ResponseMaster _objResponseModel = new ResponseMaster();
            try
            {
                if (_obj.BoxType == "Box")
                {
                    _obj.IsBox = true;
                }
                if (_obj.Pk_ProductId != null)
                {
                    _objResponseModel = DALProductServices.UpdateKaryon_ProductMaster(_obj);
                }
                else
                {
                    _objResponseModel = DALProductServices.Save(_obj);
                }


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public static List<ProductViewModel> Karyon_ProductMasterEditService(MAdminProductMaster _obj)
        {
            List<ProductViewModel> _objResponseModel = new List<ProductViewModel>();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALProductServices.GetProductEdit(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public static ResponseMaster Karyon_ProductMasterDeleteService(string id)
        {
            ResponseMaster _objResponseModel = new ResponseMaster();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALProductServices.ProductDelete(id);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public static CropProductModel BindCropProduct(CropProductModel model)
        {
            return DALProductServices.BindCropProduct(model);
        }
        public static ResponseUnitModel SaveCropProduct(CropProductModel model)
        {
            ResponseUnitModel _objResponseModel = new ResponseUnitModel();
            try
            {
                _objResponseModel = DALProductServices.SaveCropProduct(model);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public static ResponseUnitModel DeleteCropProduct(CropProductModel model)
        {
            ResponseUnitModel _objResponseModel = new ResponseUnitModel();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALProductServices.SaveCropProduct(model);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public static SubCategoryViewModel BindCropSubCategory(SubCategoryViewModel model)
        {
            return DALProductServices.BindCropSubCategory(model);
        }
        public static ResponseUnitModel SaveCropSubCategorySave(SubCategoryViewModel model)
        {
            ResponseUnitModel _objResponseModel = new ResponseUnitModel();
            try
            {
                _objResponseModel = DALProductServices.SaveCropSubCategorySave(model);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}