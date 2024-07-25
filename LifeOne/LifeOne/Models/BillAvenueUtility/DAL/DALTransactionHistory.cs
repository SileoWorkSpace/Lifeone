using LifeOne.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;
namespace LifeOne.Models.BillAvenueUtility.DAL
{
    public class DALTransactionHistory
    {
        public static List<MTransactionHistoryResponse> TransactionHistory(string CompanyId ,string Fk_MemId)
        {
            try
            {
                List<MTransactionHistoryResponse> _iresult = new List<MTransactionHistoryResponse>();
                string _qury = "BATransactionHistory @CompanyId=" + CompanyId + ",@Fk_MemId=" + Fk_MemId + "";
                _iresult = DBHelperDapper.DAGetDetailsInList<MTransactionHistoryResponse>(_qury);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}