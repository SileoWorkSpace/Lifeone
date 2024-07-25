using LifeOne.Models.API.DAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateDAL
{
    public class AssociateIncomeDAL
    {
        public List<AssciateIncomeModel> GetIncomeDetails(AssciateIncomeModel mdl)
        {
            SqlParameter[] para ={
                                  new SqlParameter("@LoginId", mdl.LoginID),
                                  new SqlParameter("@FK_PlanId", mdl.PlanId),
                                  new SqlParameter("@size", SessionManager.Size),
                                  new SqlParameter("@page", mdl.Page)
        };
            DataSet ds = DBHelper.ExecuteQuery("IncomeDetailsAssociates", para);

            List<AssciateIncomeModel> list = new List<AssciateIncomeModel>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    list.Add(new AssciateIncomeModel
                    {

                        LoginID = dr["LoginID"].ToString(),
                        DisplayName = dr["DisplayName"].ToString(),
                        PlanName = dr["PlanName"].ToString(),
                        NetAmount = dr["NetAmount"].ToString(),
                        ClosingDate = dr["ClosingDate"].ToString(),
                        PayoutNo = dr["PayoutNo"].ToString(),
                        CommissionPercentage = dr["CommissionPercentage"].ToString(),
                        BusinessAmount = dr["BusinessAmount"].ToString(),
                        TotalRecords = Convert.ToInt32(dr["TotalRecords"].ToString())
                    }) ;
                }
            }
            return list;
        }

        

    }
}