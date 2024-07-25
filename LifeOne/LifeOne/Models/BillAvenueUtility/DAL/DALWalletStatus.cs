using Dapper;
using LifeOne.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.Common;


namespace LifeOne.Models.BillAvenueUtility.DAL
{
    public class DALWalletStatus
    {
        public static MBillerWalletResponse CheckWalletStatus(MBillerWalletSatus _objrequest)
        {
            try
            {
                MBillerWalletResponse _iresult = new MBillerWalletResponse();
                string _qury = "CHeckWalletStatus @Amount=" + _objrequest.Amount + ",@CompanyId=" + _objrequest.CompanyId+ "";
                _iresult = DBHelperDapper.DAGetDetails<MBillerWalletResponse>(_qury);
                return _iresult;
            }
            catch (Exception ex)    
            {
                throw ex;
            }
        }
    }
}