using LifeOne.Models.QUtility.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API.DAL
{

 
    public class Dashboard : DBHelper
    {
        public DataSet DashboardData(string Id)
        {
            SqlParameter[] para =
            {
                new SqlParameter ("@Fk_MemId",Id)
            };
            DataSet dsresult = ExecuteQuery("Proc_DashboardApi", para);
            return dsresult;
        }

        public DataSet DashboardDataV2(string Id)
        {
            SqlParameter[] para =
            {
                new SqlParameter ("@Fk_MemId",Id)
            };
            DataSet dsresult = ExecuteQuery("Proc_DashboardApi_V2", para);
            return dsresult;
        }
    }




}