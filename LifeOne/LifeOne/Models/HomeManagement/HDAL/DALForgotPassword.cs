using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.HomeManagement.HEntity;
using LifeOne.Models.API;

namespace LifeOne.Models.HomeManagement.HDAL
{
    public class DALForgotPassword
    {
        public static MForgotPassword SendOTP(MForgotPassword _param)
        {
            try
            {
                MForgotPassword _iresult = new MForgotPassword();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@UserId", _param.UserId);
                if (_param.ProdID == 2)
                    _iresult = DBHelperDapper.DAAddAndReturnModel<MForgotPassword>("dbo.Proc_FranchiseGetOTP", queryParameters);
                else
                    _iresult = DBHelperDapper.DAAddAndReturnModel<MForgotPassword>("dbo.GetOTP", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MForgotPassword SendOTPForEmployee(MForgotPassword _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@UserId", _param.UserId);
                MForgotPassword _iresult = DBHelperDapper.DAAddAndReturnModel<MForgotPassword>("dbo.GetOTPForEmployee", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ResultSet MatchOTP(MForgotPassword _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@UserId", _param.UserId);
                queryParameters.Add("@OTP", _param.OTP);
                queryParameters.Add("@AndroidId", "");
                queryParameters.Add("@ProdId", _param.ProdID);
                ResultSet _iresult = DBHelperDapper.DAAddAndReturnModel<ResultSet>("dbo.MatchOTP", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ResultSet MatchOTPForEmployee(MForgotPassword _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@UserId", _param.UserId);
                queryParameters.Add("@OTP", _param.OTP);
                queryParameters.Add("@AndroidId", "");
                ResultSet _iresult = DBHelperDapper.DAAddAndReturnModel<ResultSet>("dbo.MatchOTPForEmployee", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ResultSet ForgotPassword(MForgotPassword _param)
        {
            try
            {
                ResultSet _iresult = new ResultSet();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@MobileNo", _param.UserId);
                queryParameters.Add("@NewPassword", _param.NewPassword);
                queryParameters.Add("@NormalPass", _param.NormalPass);
                queryParameters.Add("@ProdId", _param.ProdID);
                _iresult = DBHelperDapper.DAAddAndReturnModel<ResultSet>("dbo.Proc_ForgetPassword", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResultSet SetNewPassword(string UserId, string Password, string ENCPassword)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@UserId", UserId);
                queryParameters.Add("@Password", Password);
                queryParameters.Add("@ENCPassword", @ENCPassword);
                ResultSet _iresult = DBHelperDapper.DAAddAndReturnModel<ResultSet>("dbo.ProSetNewPassword", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}