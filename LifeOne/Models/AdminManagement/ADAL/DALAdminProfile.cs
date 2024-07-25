using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALAdminProfile
    {
        public static ResponseAdminProfileModel AdminChangePassword(MAdminProfile obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FK_MemId", obj.Fk_Memid);
                queryParameters.Add("@NewPasswd", obj.NewPassword);
                queryParameters.Add("@NormalPass", obj.NormalPass);

                ResponseAdminProfileModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseAdminProfileModel>("ChangePasswordAdmin", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseAdminProfileModel AdminChangeProfilePicture(MAdminProfile obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FK_MemId", obj.Fk_Memid);
                queryParameters.Add("@OpCode", obj.OpCode);
                queryParameters.Add("@profilepic", obj.NewPicture);

                ResponseAdminProfileModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseAdminProfileModel>("UpdateAdminProfilePicture", queryParameters);
                

                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseAdminProfileModel FranchiseChangePassword(MAdminProfile obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FK_MemId", obj.Fk_Memid);
                queryParameters.Add("@NewPasswd", obj.NewPassword);
                queryParameters.Add("@NormalPass", obj.NormalPass);

                ResponseAdminProfileModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseAdminProfileModel>("Proc_ChangeFranchisePassword", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseAdminProfileModel UserChangePassword(MAdminProfile obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FK_MemId", obj.Fk_Memid);
                queryParameters.Add("@NewPasswd", obj.NewPassword);
                queryParameters.Add("@NormalPass", obj.NormalPass);
                queryParameters.Add("@OldPassword", obj.OldPassword);
                ResponseAdminProfileModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseAdminProfileModel>("ChangePasswordUser", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseAdminProfileModel UserChangeProfile(MAdminProfile obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@FK_MemId", obj.Fk_Memid);
                queryParameters.Add("@OpCode", obj.OpCode);
                queryParameters.Add("@profilepic", obj.NewPicture);

                ResponseAdminProfileModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseAdminProfileModel>("UpdateAssociateProfilePicture", queryParameters);


                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}