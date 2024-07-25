using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.FranchiseManagement.FEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FDAL
{
    public class DALFranchiseProfile
    {
        public static ResponseFranchiseProfileModel FranchiseChangePassword(MFranchiseProfile obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@OldPassword", obj.OldPassword);
                queryParameters.Add("@NewPassword", obj.NewPassword);
                queryParameters.Add("@Normalpassword", obj.Normalpassword);
                queryParameters.Add("@PKFranchiseRegistrationId", obj.PKFranchiseRegistrationId);
                
                ResponseFranchiseProfileModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseFranchiseProfileModel>("FranchisePassword", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public static List<MFranchiseProfile> GetData(MFranchiseProfile obj)
        {
            try
            {               
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@LoginId", obj.LoginId);
                List<MFranchiseProfile> _iresult = DBHelperDapper.DAAddAndReturnModelList<MFranchiseProfile>("GetFranchise", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}