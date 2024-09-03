using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class AdminImageUploadService
    {
        ~AdminImageUploadService() { }
        ResponseUploadImage _objResponseModel = new ResponseUploadImage();        
        public ResponseUploadImage SaveImage(MAdminImageUpload _obj)
        {

            try
            {
                ResponseUploadImage _objResponseModel = new ResponseUploadImage();                                                                              
                _objResponseModel = DALAdminImageUpload.ImageUpload(_obj);
                
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseUploadImage DeleteImage(MAdminImageUpload _obj)
        {
            try
            {
                ResponseUploadImage _objResponseModel = new ResponseUploadImage();                
                _objResponseModel = DALAdminImageUpload.ImageUpload(_obj);

                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public List<MAdminImageUpload> ImageGetService(MAdminImageUpload _obj)
        {
            List<MAdminImageUpload> _objResponseModel = new List<MAdminImageUpload>();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALAdminImageUpload.GetImageList(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseUploadImage SaveVideo(MAdminImageUpload _obj)
        {

            try
            {
                ResponseUploadImage _objResponseModel = new ResponseUploadImage();
               
                _objResponseModel = DALAdminImageUpload.VideoUpload(_obj);

                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseUploadImage DeleteVideo(MAdminImageUpload _obj)
        {

            try
            {
                ResponseUploadImage _objResponseModel = new ResponseUploadImage();

                _objResponseModel = DALAdminImageUpload.VideoUpload(_obj);

                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public List<MAdminImageUpload> GetVideoService(MAdminImageUpload _obj)
        {
            List<MAdminImageUpload> _objResponseModel = new List<MAdminImageUpload>();
            try
            {

                //decrypt app team reponse
                _objResponseModel = DALAdminImageUpload.GetVideoDetails(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}