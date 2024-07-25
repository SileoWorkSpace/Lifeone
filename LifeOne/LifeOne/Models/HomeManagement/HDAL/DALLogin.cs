using LifeOne.Models.HomeManagement.HEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;
using System.Data;
using System.Data.SqlClient;
using LifeOne.Models.AdminManagement.AEntity;
using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Org.BouncyCastle.Asn1.Ocsp;

namespace LifeOne.Models.HomeManagement.HDAL
{
    public class DALLogin
    {
        public LoginResponse CheckLogin(MLogin _objrequest)
        {
            try
            {
                LoginResponse _iresult = new LoginResponse();
                string _qury = "CheckAdminLogin @LoginId='" + _objrequest.Login + "',@Password='" + _objrequest.Password + "'";
                _iresult = DBHelperDapper.DAGetDetails<LoginResponse>(_qury);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public LoginResponse CheckFranchiseLogin(MLogin _objrequest)
        {
            try
            {
                LoginResponse _iresult = new LoginResponse();
                string _qury = "CheckFranchiseLogin @LoginId='" + _objrequest.Login + "',@Password='" + _objrequest.Password + "'";
                _iresult = DBHelperDapper.DAGetDetails<LoginResponse>(_qury);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public LoginResponse CheckLoginUsers(MLogin _objrequest)
        {
            try
            {
              
                LoginResponse _iresult = new LoginResponse();
                string _qury = "CheckAssociateLogin @LoginId='" + _objrequest.Login + "',@Password='" + _objrequest.Password + "'";
                _iresult = DBHelperDapper.DAGetDetails<LoginResponse>(_qury);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetFormPermissionDetails(int userId)
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserId", userId) };
            DataSet ds = DBHelper.ExecuteQuery("FormPermissionDetailsByUserId", para);
            return ds.Tables[0];
        }
        //public List<UserPermissionModel> GetForm_Permission(long userid)
        //{
        //    var queryParameters = new DynamicParameters();
        //    queryParameters.Add("@FK_UserId", userid);
        //    List<UserPermissionModel> t1 = DBHelperDapper.DAAddAndReturnModelList<UserPermissionModel>("FormPermissionDetailsByUserId", queryParameters);
        //    return t1;
        //}


        public MultiUserIdList GetUserListByMobileNo(string Mobile)
        {
            try
            {
                MultiUserIdList _iresult = new MultiUserIdList();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@mobileNo", Mobile);
                _iresult._UserList = DBHelperDapper.DAAddAndReturnModelList<MultiUserIdList>("Proc_GetLoginId", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public MemberExistOrNotModel GetSameMobileUsers(string Mobile)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@mobileNo", Mobile);
                return DBHelperDapper.DAAddAndReturnModel<MemberExistOrNotModel>("GetSameMobileUsers", queryParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static CheckCampingModel CheckCamping()
        //{
        //    try
        //    {
        //        CheckCampingModel _iresult = new CheckCampingModel();
        //        string _qury = "Proc_EnableUserFunctions";
        //        _iresult = DBHelperDapper.DAGetDetails<CheckCampingModel>(_qury);
        //        return _iresult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}