using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class ChangeProfileRepository
    {
        public DataSet GetChangeProfileDetails(ChangeProfile obj)
        {
            SqlParameter[] Para =
             {
                new SqlParameter("@Fk_MemId",obj.Fk_MemId ),
                new SqlParameter("@Pk_Id",obj.Pk_Id )

            };
            DataSet Ds = DBHelper.ExecuteQuery("[ChangeProfileAllSelect]", Para);
            return Ds;
        }
        public DataSet ChangeProfile(ChangeProfile obj)
        {
            SqlParameter[] Para =
            {
                new SqlParameter("@LoginId",obj.LoginId ),
                new SqlParameter("@MobileNo",obj.NewMobileNo ),
                new SqlParameter("@EmailId",obj.NewEmailId ),
                //new SqlParameter("@OldMobileNo",obj.OldMobileNo ),
                //new SqlParameter("@OldEmailId",obj.OldEmailId ),
                //new SqlParameter("@OldName",obj.OldName ),
                new SqlParameter("@NewName",obj.NewName ),
                new SqlParameter("@UpdatedBy",obj.UpdatedBy)
            };
            DataSet Ds = DBHelper.ExecuteQuery("[UpdateProfile]", Para);
            return Ds;
        }
        public DataSet ChangeProfileUpdate(ChangeProfile obj)
        {
            SqlParameter[] Para =
          {
                new SqlParameter("@LoginId",obj.LoginId ),
                new SqlParameter("@MobileNo",obj.NewMobileNo ),
                new SqlParameter("@EmailId",obj.NewEmailId ),
                //new SqlParameter("@OldMobileNo",obj.OldMobileNo ),
                //new SqlParameter("@OldEmailId",obj.OldEmailId ),
                //new SqlParameter("@OldName",obj.OldName ),
                new SqlParameter("@NewName",obj.NewName ),
                new SqlParameter("@UpdatedBy",obj.UpdatedBy)
  };
            DataSet Ds = DBHelper.ExecuteQuery("[ChangeProfileUpdate]", Para);
            return Ds;
        }     
        public DataTable GetProfile(ChangeProfile obj)
        {
            SqlParameter[] para =
            {
             new SqlParameter("@LoginId",obj.LoginId),
            };
            DataTable dt = DBHelper.ExecuteQuery("GetProfileDetails", para).Tables[0];
            return dt;
        }
    }
}