using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.BillAvenueUtility.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALTransaction
    {
        public List<MTransactionLog> AllTransactionLog()
        {
            try
            {
                List<MTransactionLog> _iresult = new List<MTransactionLog>();
                string _qury = "Proc_AllTransactionsLog";
                _iresult = DBHelperDapper.DAGetDetailsInList<MTransactionLog>(_qury);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}