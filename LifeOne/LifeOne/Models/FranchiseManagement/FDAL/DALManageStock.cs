using LifeOne.Models.API.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FDAL
{
    public class DALManageStock
    {

        public static DataSet ReturnDatasetForUpateingFranStock(long _reqId, string _tablename)
        {
            try
            {
                SqlParameter[] para ={

                                   new SqlParameter("@requestId",Convert.ToInt64(_reqId)),
                                    new SqlParameter("@tablename",_tablename)
                       };
                DataSet ds = DBHelper.ExecuteQuery("GetFranchiseMemberRequestedStock", para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }


        }

    }
}