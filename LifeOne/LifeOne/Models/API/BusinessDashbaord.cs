
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.API
{
    public class BusinessDashboardRequest
    {
        public long Fk_MemId { get; set; }
        public DataSet GetBusinessDashboard()
        {
            SqlParameter[] Para =
                {
                new SqlParameter("@FK_MemID",Fk_MemId)

            };
            DataSet ds = DBHelper.ExecuteQuery("BusinessDashboard", Para);
            return ds;
        }
    }
    public class BusinessDashbaord
    {
        public string SelfTeam { get; set; }
        public string TotalTeam { get; set; }
        public string LoginId { get; set; }
        public string JoiningDate { get; set; }
        public string Name { get; set; }
        public string Response { get; set; }

        public string Message { get; set; }
        public List<LevelStatus> LevelStatusList { get; set; }

    }

    public class LevelStatus
    {
        public string LevelName { get; set; }
        public string LevelCount { get; set; }
        public string TeamCount { get; set; }
    }
}