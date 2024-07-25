using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.ADAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using LifeOne.Models.HomeManagement.HEntity;

namespace LifeOne.Models.AdminManagement.AService
{
    public class AllTransactionService
    {
        public static List<MTransactionLog> AllTransactionLog(MTransactionLog _model)
        {
            try
            {
                List<MTransactionLog> _iresult = new List<MTransactionLog>();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@procid", _model.ProcId);
                queryParameters.Add("@LoginId", _model.LoginId ?? string.Empty);
                queryParameters.Add("@Name", _model.DisplayName ?? string.Empty);
                queryParameters.Add("@Mobile", _model.Mobile ?? string.Empty);
                queryParameters.Add("@email", _model.Email ?? string.Empty);
                queryParameters.Add("@Transactionid", _model.TransactionId ?? string.Empty);
                queryParameters.Add("@Amount", _model.Amount);
                queryParameters.Add("@FromDate", string.IsNullOrEmpty(_model.FromDate) ? null : Common.DALCommon.ConvertToSystemDate(_model.FromDate, "MM-dd-yyyy"));
                queryParameters.Add("@ToDate", string.IsNullOrEmpty(_model.ToDate) ? null : Common.DALCommon.ConvertToSystemDate(_model.ToDate, "MM-dd-yyyy"));
                queryParameters.Add("@Number", _model.Number);
                queryParameters.Add("@Status", _model.Status);
                queryParameters.Add("@ActionType", _model.Utility ?? null);
                queryParameters.Add("@page", _model.Page);
                queryParameters.Add("@size", _model.Size);
                queryParameters.Add("@SLoginId", _model.AssociateLoginId ?? string.Empty); 
                queryParameters.Add("@IsExport", _model.IsExport); 
                _iresult = DBHelperDapper.DAAddAndReturnModelList<MTransactionLog>("Proc_AllTransactionsLog", queryParameters);
                return _iresult;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<WalletDetailModel> WalletDetail(MTransactionLog _model)
        {
            try
            {
                List<WalletDetailModel> _iresult = new List<WalletDetailModel>();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@page", _model.Page);
                queryParameters.Add("@size", _model.Size);
                queryParameters.Add("@Fk_MemId", _model.Fk_MemId);
                _iresult = DBHelperDapper.DAAddAndReturnModelList<WalletDetailModel>("Proc_MemberWalletDetail", queryParameters);
                return _iresult;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<WalletDetailModel> CashBackWalletDetail(MTransactionLog _model)
        {
            try
            {
                List<WalletDetailModel> _iresult = new List<WalletDetailModel>();
                var queryParameters = new DynamicParameters();
                if (_model.ProcId == 1)
                    queryParameters.Add("@ProcId", _model.ProcId);


                queryParameters.Add("@page", _model.Page);
                queryParameters.Add("@size", _model.Size);
                queryParameters.Add("@Fk_MemId", _model.Fk_MemId);
                queryParameters.Add("@FromDate", _model.FromDate);
                queryParameters.Add("@ToDate", _model.ToDate);
                _iresult = DBHelperDapper.DAAddAndReturnModelList<WalletDetailModel>("Proc_MemberCashBackWalletDetail", queryParameters);
                return _iresult;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<WalletDetailModel> CommissionDetailList(MTransactionLog _model)
        {
            try
            {
                List<WalletDetailModel> _iresult = new List<WalletDetailModel>();
                var queryParameters = new DynamicParameters();
                if (_model.ProcId == 1)
                    queryParameters.Add("@ProcId", _model.ProcId);


                queryParameters.Add("@page", _model.Page);
                queryParameters.Add("@size", _model.Size);
                queryParameters.Add("@Fk_MemId", _model.Fk_MemId);
                queryParameters.Add("@FromDate", _model.FromDate);
                queryParameters.Add("@ToDate", _model.ToDate);
                _iresult = DBHelperDapper.DAAddAndReturnModelList<WalletDetailModel>("Proc_MemberCommisionDetail", queryParameters);
                return _iresult;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MTransactionLog> AllRecentTransactionLog(MTransactionLog _model)
        {
            try
            {
                List<MTransactionLog> _iresult = new List<MTransactionLog>();
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@procid", _model.ProcId);
                queryParameters.Add("@LoginId", _model.LoginId ?? string.Empty);
                queryParameters.Add("@Name", _model.DisplayName ?? string.Empty);
                queryParameters.Add("@Mobile", _model.Mobile ?? string.Empty);
                queryParameters.Add("@email", _model.Email ?? string.Empty);
                queryParameters.Add("@Transactionid", _model.TransactionId ?? string.Empty);
                queryParameters.Add("@Amount", _model.Amount);
                queryParameters.Add("@FromDate", string.IsNullOrEmpty(_model.FromDate) ? null : Common.DALCommon.ConvertToSystemDate(_model.FromDate, "MM-dd-yyyy"));
                queryParameters.Add("@ToDate", string.IsNullOrEmpty(_model.ToDate) ? null : Common.DALCommon.ConvertToSystemDate(_model.ToDate, "MM-dd-yyyy"));
                queryParameters.Add("@Number", _model.Number);
                queryParameters.Add("@Status", _model.Status);
                queryParameters.Add("@ActionType", _model.Utility ?? null);
                queryParameters.Add("@page", _model.Page);
                queryParameters.Add("@size", _model.Size);
                queryParameters.Add("@SLoginId", _model.AssociateLoginId ?? string.Empty);
                _iresult = DBHelperDapper.DAAddAndReturnModelList<MTransactionLog>("Proc_RecentAllTransactionsLog", queryParameters);
                return _iresult;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}