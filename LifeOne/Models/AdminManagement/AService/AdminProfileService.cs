
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.AdminManagement.ADAL;

namespace LifeOne.Models.AdminManagement.AService
{
    public class AdminProfileService
    {
        ~AdminProfileService() { }


        public ResponseAdminProfileModel AdminChangePasswordService(MAdminProfile _obj)
        {
            ResponseAdminProfileModel _objResponseModel = new ResponseAdminProfileModel();
            try
            {
                _objResponseModel = DALAdminProfile.AdminChangePassword(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseAdminProfileModel AdminChangeProfilePictureService(MAdminProfile _obj)
        {
            ResponseAdminProfileModel _objResponseModel = new ResponseAdminProfileModel();
            try
            {
                _objResponseModel = DALAdminProfile.AdminChangeProfilePicture(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseAdminProfileModel AssociateChangeProfilePictureService(MAdminProfile _obj)
        {
            ResponseAdminProfileModel _objResponseModel = new ResponseAdminProfileModel();
            try
            {
                _objResponseModel = DALAdminProfile.UserChangeProfile(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseAdminProfileModel FranchiseChangePasswordService(MAdminProfile _obj)
        {
            ResponseAdminProfileModel _objResponseModel = new ResponseAdminProfileModel();
            try
            {
                _objResponseModel = DALAdminProfile.FranchiseChangePassword(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }


        public ResponseAdminProfileModel UserChangePasswordService(MAdminProfile _obj)
        {
            ResponseAdminProfileModel _objResponseModel = new ResponseAdminProfileModel();
            try
            {
                _objResponseModel = DALAdminProfile.UserChangePassword(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

    }
}