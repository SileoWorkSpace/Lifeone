using LifeOne.Models.AssociateManagement.AssociateEntity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using LifeOne.Models.API.DAL;

namespace LifeOne.Models.AssociateManagement.AssociateDAL
{
    public class DALAssosiateEwalletLedger
    {
        public DataSet GetEWalletLedger(EwalletLedger obj)
        {

            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", obj.LoginId),
                                      new SqlParameter("@FromDate", obj.FromDate),
                                       new SqlParameter("@ToDate", obj.ToDate),
                                       new SqlParameter("@Page", obj.Page),
                                       new SqlParameter("@Size", obj.Size),
                                       new SqlParameter("@ExportToExcel", obj.IsExport),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetEwalletLedger", para);
            return ds;
        }
    }
}