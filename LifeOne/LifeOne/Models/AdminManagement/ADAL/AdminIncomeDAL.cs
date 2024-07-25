using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API.DAL;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class AdminIncomeDAL
    {
        public List<AdminIncomeDetailsModel> GetIncomeDetails(AdminIncomeDetailsModel mdl)
        {
            SqlParameter[] para ={
                                  new SqlParameter("@LoginId", mdl.LoginID),
                                  new SqlParameter("@FK_PlanId", mdl.PlanId),
                                  new SqlParameter("@Page", mdl.Page??1),
                                  new SqlParameter("@Size", SessionManager.Size),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("IncomeDetailsAssociates", para);
            List<AdminIncomeDetailsModel> list = new List<AdminIncomeDetailsModel>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    list.Add(new AdminIncomeDetailsModel
                    {
                        LoginID = dr["LoginID"].ToString(),
                        DisplayName = dr["DisplayName"].ToString(),
                        PlanName = dr["PlanName"].ToString(),
                        NetAmount = dr["NetAmount"].ToString(),
                        ClosingDate = dr["ClosingDate"].ToString(),
                        PayoutNo = dr["PayoutNo"].ToString(),
                        CommissionPercentage = dr["CommissionPercentage"].ToString(),
                        BusinessAmount = dr["BusinessAmount"].ToString(),
                        TotalRecords = Convert.ToInt32(dr["TotalRecords"])
                    });
                }
            }
            int totalRecords = 0;
            if (list != null && list.Count() > 0)
            {
                totalRecords = Convert.ToInt32(list[0].TotalRecords);
                var pager = new Common.Pager(totalRecords, mdl.Page, SessionManager.Size);
                mdl.Pager = pager;
            }
            else
            {
                list = new List<AdminIncomeDetailsModel>();
            }
            return list;
        }

        
    }
}