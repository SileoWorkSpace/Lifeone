using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AEntity
{
    public class Franchisetranslations
    {
        public string LoginId { get; set; }
        public long Fk_MemId { get; set; }
        public string Name { get; set; }
        public string JsonstringWallet { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionNo { get; set; }
        public string IsDebitCredit { get; set; }
        public string Amount { get; set; }
        public string Narration { get; set; }
        public string BalanceAmount { get; set; }
        public string Type { get; set; }
        public string AddedBy { get; set; }
        public int OpCode { get; set; }
        public DataSet FranchiseTransactionDetails()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@Narration",Narration),
                    new SqlParameter("@IsDebitCredit",IsDebitCredit),
                    new SqlParameter("@Amount",Amount),
                    new SqlParameter("@AddedBy",AddedBy),
                    new SqlParameter("@OpCode",OpCode),
                };
                DataSet ds = Connection.ExecuteQuery("franchisewallettransaction", para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet FranchiseDetails()
        {
            try
            {
                SqlParameter[] para =
                {
                    new SqlParameter("@LoginId",LoginId),
                    new SqlParameter("@OpCode",OpCode)
                    
                };
                DataSet ds = Connection.ExecuteQuery("franchisewallettransaction", para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}