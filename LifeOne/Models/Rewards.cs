using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.API;
using LifeOne.Models.API.DAL;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using DBHelper = LifeOne.Models.Common.DBHelper;

namespace LifeOne.Models
{
    public class Rewards : MPaging
    {
        public string LoginId { get; set; }
        public string FK_MemId { get; set; }
        public string RewardName { get; set; }
        public string ToursName { get; set; }
        public string RewardImage { get; set; }
        public string ToursImage{ get; set; }
        public string RewardStatus { get; set; }
        public decimal LeftBusiness { get; set; }
        public decimal RightBusiness { get; set; }

        public DataTable getRewardDetails { get; set; }

        public DataSet GetRewardDetails()
        {
            SqlParameter[] para = {                                        
                                        new SqlParameter("@LoginId",LoginId),
                                        new SqlParameter("@FK_MemId",FK_MemId),
                                        new SqlParameter("@Status",RewardStatus=="0"?null:RewardStatus),                                          
                                        new SqlParameter("@Page",Page),
                                        new SqlParameter("@Size",Size),

            };
            DataSet ds = DBHelper.ExecuteQuery("GetRewardDetails", para);
            return ds;
        }
    }
}