using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class AdminProductMasterService
    {
        ~AdminProductMasterService() { }
        ResponseMaster _objResponseModel = new ResponseMaster();
        public List<MAdminProductMaster> ProductMasterGetService(MAdminProductMaster _obj)
        {
            List<MAdminProductMaster> _objResponseModel = new List<MAdminProductMaster>();
            try
            {
               
                //decrypt app team reponse
               _objResponseModel = DALAdminProductMaster.GetProductList(_obj);
              
               
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseMaster ProductMasterSaveService(MAdminProductMaster _obj)
        {
          
            try
            {
                ResponseMaster _objResponseModel = new ResponseMaster();
                if (_obj.BoxType == "Box") {
                    _obj.IsBox = true;
                }
                if (_obj.Pk_ProductId != null) 
                {
                    _objResponseModel = DALAdminProductMaster.UpdateProductMaster(_obj);
                }
                else
                {
                    _objResponseModel = DALAdminProductMaster.Save(_obj);
                }
              
               
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }


        public  MAdminProductMaster  ProductMasterEditService(MAdminProductMaster _obj)
        {
             MAdminProductMaster  _objResponseModel = new  MAdminProductMaster();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALAdminProductMaster.GetProductEdit(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseMaster ProductMasterDeleteService(string id)
        {
            ResponseMaster _objResponseModel = new ResponseMaster();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALAdminProductMaster.ProductDelete(id);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseMaster SaveImage(MAdminProductMaster _obj)
        {

            try
            {
                ResponseMaster _objResponseModel = new ResponseMaster();
                if (_obj.BoxType == "Box")
                {
                    _obj.IsBox = true;
                }
                                                                 
                _objResponseModel = DALAdminProductMaster.ImageUpload(_obj);
                
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public List<MAdminProductMaster> ImageGetService(MAdminProductMaster _obj)
        {
            List<MAdminProductMaster> _objResponseModel = new List<MAdminProductMaster>();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALAdminProductMaster.GetImageList(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseMaster SaveVideo(MAdminProductMaster _obj)
        {

            try
            {
                ResponseMaster _objResponseModel = new ResponseMaster();
                if (_obj.BoxType == "Box")
                {
                    _obj.IsBox = true;
                }

                _objResponseModel = DALAdminProductMaster.VideoUpload(_obj);

                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public List<MAdminProductMaster> GetVideoService(MAdminProductMaster _obj)
        {
            List<MAdminProductMaster> _objResponseModel = new List<MAdminProductMaster>();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALAdminProductMaster.GetVideoDetails(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}