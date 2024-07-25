using LifeOne.Models.QUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeOne.Models.QUtility.InterFace
{
    public interface IWallet
    {
        ResponseModel ChkCompanyWalletBalanceService(RequestModel _obj);
        WalletTransactionResponse WalletTransactionsService(WalletTransaction objWalletTrans);
    }
}
