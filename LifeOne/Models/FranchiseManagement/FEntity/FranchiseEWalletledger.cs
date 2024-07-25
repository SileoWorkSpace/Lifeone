using LifeOne.Models.API.DAL;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FEntity
{
    public class FranchiseEWalletledger:MPaging
    {
        public long FranchiseId { get; set; }
        public string FromDate { get; set; }      
        public string ToDate { get; set; }
        
        public string Paymentdate { get; set; }
        public string Narration { get; set; }
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
        public string Remarks { get; set; }
        public decimal Amount { get; set; }
        public string ActionType { get; set; }
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public string InvoiceNo { get; set; }
        public string ActiveDate { get; set; }
        public decimal BV { get; set; }
       
        public DataTable dtRequestDetails { get; set; }
        public string Balance { get;  set; }
        public string Pk_TransId { get; set; }
        public string PaymentMode { get; set; }
        public string DDChequeNo { get; set; }
        public string FranchiseLoginID { get; set; }
        public string MobileNo { get; set; }
        public string PackageName { get; set; }
        public int FK_PackageId { get; set; }
        public string FranchiseCode { get; set; }
        public string FranchiseName { get; set; }
        //public int Page { get; set; }
        public string SearchParam { get; set; }
        public string Searchby { get; set; }
        public int IsExport { get; set; }
        public int Page { get; set; }
       
        public int TotalCount { get; set; }
        public DataSet GetEwalletLedger(FranchiseEWalletledger franchiseEWalletRequest)
        {
            SqlParameter[] para =
            {
                    new SqlParameter("@FK_FranchiseId",FranchiseId),
                    new SqlParameter("@Page" , Page),
                    new SqlParameter( "@Size" ,Size )

            };

            DataSet ds = Common.DBHelper.ExecuteQuery("GetFranchiseWalletLedger", para);
            return ds;
        }
        public DataSet GetActiveMembersFranchise(string LoginID, string FromDate, string ToDate, long FranchiseId, string FranchiseLoginID, string MobileNo,int FK_PackageId,int Page,int Size,string Pyamentmode,int IsExport, string InvoiceNo )
        {
            SqlParameter[] para =
            {
                    new SqlParameter("@Fk_FranchiseId",FranchiseId),
                    new SqlParameter("@LoginID",string.IsNullOrEmpty(LoginID)?null:LoginID),
                    new SqlParameter("@Fromdate",string.IsNullOrEmpty(FromDate)?null:FromDate),
                    new SqlParameter("@Todate",string.IsNullOrEmpty(ToDate)?null:ToDate),
                    new SqlParameter("@FranchiseLoginID",string.IsNullOrEmpty(FranchiseLoginID)?null:FranchiseLoginID),
                    new SqlParameter("@MobileNo",string.IsNullOrEmpty(MobileNo)?null:MobileNo),
                    new SqlParameter("@Fk_PackageId",FK_PackageId),
                    new SqlParameter("@page", Page),
                    new SqlParameter("@size", Size),
                    new SqlParameter("@PaymentMode", string.IsNullOrEmpty(Pyamentmode)?null:Pyamentmode),
                    new SqlParameter("@ISExport", IsExport),
                    new SqlParameter("@InvoiceNo",string.IsNullOrEmpty(InvoiceNo)?null:InvoiceNo)

        };

            DataSet ds = Common.DBHelper.ExecuteQuery("GetActiveMembersFranchise", para);

            return ds;
        }
        public DataSet GetFranchiseEwalletBalance()
        {
            SqlParameter[] para =
            {
                    new SqlParameter("@Fk_FranchiseId",FranchiseId)

            };

            DataSet ds = Common.DBHelper.ExecuteQuery("GetFranchiseEwalletBalance", para);
            return ds;
        }

    }
}