using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Common;
using System.Data;
using System.Data.SqlClient;

namespace LifeOne.Models.AssociateManagement.AssociateDAL
{
    public class DALEwalletRequests
    {
        public DataSet GetEWalletRequest(EwalletRequest objewallet)
        {

            SqlParameter[] para = {
                                      new SqlParameter("@Fk_MemId", objewallet.Fk_MemId),
                                      new SqlParameter("@FromDate", objewallet.FromDate),
                                       new SqlParameter("@ToDate", objewallet.ToDate)
                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetEwalletRequest", para);
            return ds;
        }

        public DataSet SaveEWalletRequest(EwalletRequest objewallet)
        {

            SqlParameter[] para = {
                                      new SqlParameter("@Fk_MemId", objewallet.Fk_MemId),
                                      new SqlParameter("@Amount", objewallet.Amount),
                                      new SqlParameter("@PaymentMode", objewallet.PaymentMode),
                                      new SqlParameter("@BankId", objewallet.BankId),
                                      new SqlParameter("@ChequeNo", objewallet.DDChequeNo),
                                      new SqlParameter("@ChequeDate", objewallet.DDChequeDate),
                                      new SqlParameter("@BankBranch", objewallet.BankBranch),
                                      new SqlParameter("@CreatedBy", objewallet.CreatedBy),
                                      new SqlParameter("@Document", objewallet.Document),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("EwalletRequest", para);
            return ds;
        }

        public DataSet GetEWalletLedger(CashbackWallet obj)
        {

            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", obj.LoginId),
                                      new SqlParameter("@FromDate", obj.FromDate),
                                       new SqlParameter("@ToDate", obj.ToDate)
                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetEWalletLedger", para);
            return ds;
        }
    }
}