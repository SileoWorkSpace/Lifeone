using LifeOne.Models.API.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FEntity
{
    public class FranchiseEWalletRequest
    {
        public decimal Amount { get; set; }
        public string PaymentMode { get; set; }      
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string TransactionNo { get; set; }
        public string TransactionDate { get; set; }
        public long AddedBy { get; set; }
        public string LoginId { get; set; }
        public string Pk_RequestId { get; set; }
        
        public DataTable dtRequestDetails { get; set; }
        public string FileUpload { get;  set; }
        public string Status { get;  set; }

        public DataTable FranchiseEWalletReq(FranchiseEWalletRequest obj)
        {

            SqlParameter[] para =
            {
            new SqlParameter("@Amount",obj.Amount),
            new SqlParameter("@FileUpload",obj.FileUpload),
            new SqlParameter("@PaymentMode",obj.PaymentMode),
            new SqlParameter("@TransactionNo",obj.TransactionNo),
            new SqlParameter("@TransactionDate",obj.TransactionDate),
            new SqlParameter("@BankName",obj.BankName),
            new SqlParameter("@BankBranch",obj.BankBranch),
            new SqlParameter("@AddedBy",obj.AddedBy)
            };

            DataTable dt = API.DAL.DBHelper.ExecuteQuery("FranchiseEwalletRequest", para).Tables[0];
            return dt;
        }
        public DataSet GetEwalletRequest()
        {
            SqlParameter[] para =
            {
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@Status",Status),
                    new SqlParameter("@AddedBy",AddedBy)

            };

            DataSet ds = DBHelper.ExecuteQuery("GetFranchiseEwalletRequest", para);
            return ds;
        }
        public DataSet ApproveDeclineFranchiseEwalletRequest()
        {
            SqlParameter[] para =
            {
                    new SqlParameter("@Pk_RequestId",Pk_RequestId),
                    new SqlParameter("@ApprovedBy",AddedBy),
                    new SqlParameter("@Status",Status)

            };

            DataSet ds = DBHelper.ExecuteQuery("ApproveDeclineFranchiseEwalletRequest", para);
            return ds;
        }
    }
}