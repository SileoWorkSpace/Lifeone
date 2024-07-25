using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.TravelModel
{
    public class Login
    {
        public string LoginId { get; set; }
        public string password { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ConatactNo { get; set; }


        public DataSet CheckUserLogin()
        {
            SqlParameter[] para ={
                                new SqlParameter("@LoginId",LoginId),
                                new SqlParameter("@Password",password)
                                };

            DataSet ds = DBHelper.ExecuteQuery(Common.ProcedureName.MemberLogin, para);
            return ds;
        }

        public DataSet WebuserRegistration()
        {
            SqlParameter[] para ={
                                new SqlParameter("@LoginId",LoginId),
                                new SqlParameter("@Title",Title),
                                new SqlParameter("@FirstName",FirstName),
                                new SqlParameter("@MiddleName",MiddleName),
                                new SqlParameter("@LastName",LastName),
                                new SqlParameter("@ContactNo",ConatactNo)
                                };

            DataSet ds = DBHelper.ExecuteQuery("MemberRegistration", para);
            return ds;
        }
    }
}