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
    public class WalletTransfer : MPaging
    {
        public string LoginId { get; set; }
        public int FK_MemId { get; set; }
        public string FromLoginId { get; set; }
        public string ToLoginId { get; set; }
        public decimal Amount { get; set; }
        public string TransferDate { get; set; } 
        
        public DataTable getwalletdetails { get; set; }

        public DataSet GetWalletTransfer()
        {
            SqlParameter[] para = { 
                                        new SqlParameter("@FK_MemId",FK_MemId),                                    new SqlParameter("@FromLoginId",FromLoginId),
                                        new SqlParameter("@ToLoginId",ToLoginId),
                                        new SqlParameter("@Page",Page),
                                        new SqlParameter("@Size",Size),

            };
            DataSet ds = DBHelper.ExecuteQuery("WalletTransferDetails", para);
            return ds;
        }
    }
}