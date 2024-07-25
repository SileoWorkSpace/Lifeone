using LifeOne.Models.QUtility.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeOne.Models.QUtility.DAL.DInterface
{
    public interface DIPrepaid
    {
      
        DataSet PrepaidRecharge(RequestModel _objRequestModel, List<PrepaidResponse> _objResponseModel, WalletTransactionResponse _wallettranResponse);
        DataSet MObileProvider(string Provider);

    }
}
