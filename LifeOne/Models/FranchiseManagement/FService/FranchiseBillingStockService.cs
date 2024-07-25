using LifeOne.Models.FranchiseManagement.FDAL;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.FranchiseManagement.FService
{
    public class FranchiseBillingStockService
    {
        public static MFranchiseBillingStockHistory FranchiseBillingStock(MFranchiseBillingStockHistory _data)
        {
            try
            {
                _data.FranchiseId = SessionManager.FranchiseFk_MemId;
                _data.LstStock = DALFranchiseBillingStockHistory.FranchiseBillingStockHistory(_data);
                return _data;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}