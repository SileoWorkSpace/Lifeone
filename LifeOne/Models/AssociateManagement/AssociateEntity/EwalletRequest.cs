using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateEntity
{
    public class EwalletRequest
    {
        //DbHelper DBHelper = new DbHelper();
        public DataSet ds = new DataSet();
        public DataTable dt = new DataTable();

        [Display(Name = "Requested Amount")]
        public string Amount { get; set; }


        [Display(Name = "Payment Mode")]
        public string PaymentMode { get; set; }


        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Display(Name = "Bank Id")]
        public int BankId { get; set; }

        [Display(Name = "DD/Cheque No")]
        public string DDChequeNo { get; set; }

        [Display(Name = "DD/Cheque Date")]
        public string DDChequeDate { get; set; }

        [Display(Name = "Bank Branch")]
        public string BankBranch { get; set; }

        [Display(Name = "Requested Date")]
        public string RequestedDate { get; set; }
        public string CreatedBy { get; set; }
        public string AdminApprovedStatus { get; set; }
        [Display(Name = "Login Id")]
        public string LoginId { get; set; }
        public string Message { get; set; }
        public string Fk_MemId { get; set; }
        public string PK_RequestID { get; set; }
        public string Document { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        // Admin Payment Transfer Ladger
        public string TransactionDate { get; set; }
        public string Narration { get; set; }
        public string CRAmount { get; set; }
        public string DRAmount { get; set; }
        public string Balance { get; set; }
        public string PK_AdminId { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string SearchBy { get; set; }
        public string Parameter { get; set; }
        public string ToLoginId { get; set; }
        public string ToAmount { get; set; }
        public string ToName { get; set; }
        public string Flag { get; set; }
        public List<EwalletRequest> list1 { get; set; }

        [NotMapped]
        public List<EwalletRequest> WalletRequestList { get; set; }

        public DataSet GetAssociateDetail()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@LoginId",LoginId)
                };
            DataSet ds = Connection.ExecuteQuery("getAssociateDetails", para);
            return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }


    public class CashbackWallet
    {
        public string Fk_MemId { get; set; }
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string TransactionDate { get; set; }
        public string Narration { get; set; }
        public string CrAmount { get; set; }
        public string DrAmount { get; set; }
        public string Balance { get; set; }
        public string Response { get; set; }
        public string Remark { get; set; }
        public string Name { get; set; }
        public int TotalRecord { get; set; }
        public int? Page { get; set; }
        public int Size { get; set; }
        public Pager Pager { get; set; }
        public List<CashbackWallet> CashbackWallets { get; set; }
        public List<CashbackWallet> EWalletRequestLedgerList { get; set; }



        //public DataSet GetCashbackWalletLedger()
        //{

        //    SqlParameter[] para = {
        //                              new SqlParameter("@Fk_MemId", LoginId),
        //                              new SqlParameter("@FromDate", FromDate),
        //                               new SqlParameter("@ToDate", ToDate)
        //                             };
        //    DataSet ds = ExecuteQuery("CashbackWalletLedger", para);
        //    return ds;
        //}


        //public CashbackWallet GetEWalletLedgerList(CashbackWallet model)
        //{
        //    CashbackWallet obj = new CashbackWallet();
        //    List<CashbackWallet> EWalletList = new List<CashbackWallet>();

        //    try
        //    {

        //        SqlParameter[] para = {
        //                              new SqlParameter("@Fk_MemId", model.Fk_MemId),
        //                              new SqlParameter("@FromDate", model.FromDate==""?null:model.FromDate),
        //                               new SqlParameter("@ToDate", model.ToDate==""?null:model.ToDate)
        //                             };
        //        DataSet ds = ExecuteQuery("GetEWalletLedger", para);
        //        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in ds.Tables[0].Rows)
        //            {
        //                EWalletList.Add(new CashbackWallet
        //                {

        //                    Balance = dr["Balance"].ToString(),
        //                    CrAmount = dr["CrAmount"].ToString(),
        //                    DrAmount = dr["DrAmount"].ToString(),
        //                    Narration = dr["Narration"].ToString(),
        //                    TransactionDate = dr["TransactionDate"].ToString(),



        //                });

        //            }
        //            obj.Response = "success";
        //            obj.Remark = "Ledger List";
        //            obj.EWalletRequestLedgerList = EWalletList;



        //        }
        //        else
        //        {
        //            obj.Response = "failure";

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        obj.Remark = ex.Message;
        //    }
        //    return obj;
        //}
    }
}