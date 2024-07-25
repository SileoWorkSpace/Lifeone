using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.API.DAL;

namespace LifeOne.Models.FranchiseManagement.FDAL
{
    public class DALFranchiseBusinessReport
    {
        public int Fk_FranchiseId { get; set; }
       
        public string FromDate { get; set; }
        public string LoginId { get; set; }
        public string ToDate { get; set; }
        public string InvoiceId { get; set; }
        public string TeamType { get; set; }

        public List<FranchiseBusinessReportModel> BusinessReportList(DALFranchiseBusinessReport model)
        {
            List<FranchiseBusinessReportModel> BusinessList = new List<FranchiseBusinessReportModel>();
            try
            {
                SqlParameter[] param =
                {
                 
                  new  SqlParameter("@fromDate",model.FromDate==""?null:model.FromDate),
                  new  SqlParameter("@ToDate",model.ToDate==""?null:model.ToDate),
                  new  SqlParameter("@TeamType",model.ToDate==""?null:model.TeamType),
                  new SqlParameter("@LoginId",model.LoginId==""?null:model.LoginId),
                  new SqlParameter("@Fk_FranchiseId",model.Fk_FranchiseId)
                  
                };
                DataSet ds = DBHelper.ExecuteQuery("FranchiseBusinessSummaryReport", param);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BusinessList.Add(new FranchiseBusinessReportModel
                        {
                            LoginId = dr["LoginId"].ToString(),
                            DisplayName = dr["DisplayName"].ToString(),
                            Amount = Convert.ToDecimal(dr["TotalAmt"].ToString()),
                            PaidAmount = Convert.ToDecimal(dr["PaidAmt"].ToString()),
                            Fk_MemId = Convert.ToInt32(dr["FK_MemId"].ToString()),
                            City = dr["City"].ToString(),
                            InvoiceNo = dr["InvoiceNo"].ToString(),
                            MobileNo = dr["Mobile"].ToString(),
                            State = dr["state"].ToString(),
                            Pv = Convert.ToDecimal(dr["TotalPv"].ToString())


                        });
                    }


                }
            }
            catch (Exception ex)
            {

            }
            return BusinessList;


        }
    }
}