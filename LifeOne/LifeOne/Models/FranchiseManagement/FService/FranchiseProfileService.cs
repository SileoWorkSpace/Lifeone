using LifeOne.Models.FranchiseManagement.FDAL;
using LifeOne.Models.FranchiseManagement.FEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FService
{
    public class FranchiseProfileService
    {
        ~FranchiseProfileService() { }

       
        public ResponseFranchiseProfileModel FranchiseChangePasswordService(MFranchiseProfile _obj)
        {
            ResponseFranchiseProfileModel _objResponseModel = new ResponseFranchiseProfileModel();
            try
            {
                _objResponseModel = DALFranchiseProfile.FranchiseChangePassword(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }


        public List<MFranchiseProfile> GetFranchiseProfileService(MFranchiseProfile _obj)
        {
            List<MFranchiseProfile> _objResponseModel = new List<MFranchiseProfile>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALFranchiseProfile.GetData(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}