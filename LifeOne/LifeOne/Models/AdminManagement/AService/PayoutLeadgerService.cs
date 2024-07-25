using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AssociateManagement.AssociateDAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class PayoutLeadgerService
    {


        public static List<MPayoutLeadger> AllPayoutLeadger(MPayoutLeadger _model)
        {
            try
            {
                List<MPayoutLeadger> _iresult = new List<MPayoutLeadger>();
                _iresult = DAlPayoutLeadger.DAlAllPayoutLeadger(_model);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<CashbackWallet> GetWalletDetail(CashbackWallet obj)
        {
            List<CashbackWallet> lst = new List<CashbackWallet>();
            DALEwalletRequests objDALEwalletRequests = new DALEwalletRequests();
            try
            {
                DataSet ds = objDALEwalletRequests.GetEWalletLedger(obj);
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        CashbackWallet Objload = new CashbackWallet();
                        Objload.TransactionDate = dr["TransactionDate"].ToString();
                        Objload.Narration = dr["Narration"].ToString();
                        Objload.CrAmount = dr["CrAmount"].ToString();
                        Objload.DrAmount = dr["DrAmount"].ToString();
                        Objload.Balance = dr["Balance"].ToString();
                        Objload.LoginId = dr["LoginId"].ToString();
                        Objload.Name = dr["Name"].ToString();
                        lst.Add(Objload);
                    }
                    //objewallet.CashbackWallets = lst;
                }


                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}