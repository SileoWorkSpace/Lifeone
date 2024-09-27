using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class AdminDALWallet
    {
        public static List<MAdminWallet> GetData(MAdminWallet obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@LoginId", obj.LoginId);
                queryParameters.Add("@Name", obj.DisplayName);
                queryParameters.Add("@TransactionNo", obj.TransactionNo);
                queryParameters.Add("@FromDate", obj.FromDate);
                queryParameters.Add("@ToDate", obj.ToDate);
                queryParameters.Add("@Page", obj.Page);
                queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);

                List<MAdminWallet> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminWallet>("Proc_GetShoppingWalletRequest", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseBankModel ApproveRequest(MAdminWallet obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Pk_RequestId", obj.Pk_RequestId);
                ResponseBankModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseBankModel>("Proc_WalletRequestApproveUpdate", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseBankModel DeclineRequest(MAdminWallet obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Pk_RequestId", obj.Pk_RequestId);
                queryParameters.Add("@Remark", obj.ApprovelRemark);

                ResponseBankModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseBankModel>("proc_WalletRequestDeleteUpdate", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static List<MAdminWallet> GetFranchisePaymnetData(MAdminWallet obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@LoginId", obj.LoginId);
                queryParameters.Add("@Name", obj.DisplayName);
                queryParameters.Add("@Isapprove", obj.IsApprove);
                queryParameters.Add("@FromDate", obj.FromDate);
                queryParameters.Add("@ToDate", obj.ToDate); 
                queryParameters.Add("@PinCode", obj.PinCode);
                queryParameters.Add("@Page", obj.Page);
                queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);
                //List<MAdminWallet> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminWallet>("Proc_GetFranchisePaymnetRequest", queryParameters);
                //List<MAdminWallet> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminWallet>("Proc_GetFranchisePaymnetRequest_Bkp2632021", queryParameters);
                List<MAdminWallet> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminWallet>("Proc_GetFranchisePaymnetRequestV1", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static ResponseBankModel ApproveFranchisePaymentRequest(MAdminWallet obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_RequestId", obj.Pk_RequestId);
                queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);
                ResponseBankModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseBankModel>("Proc_FranchisePaymentRequestApproveUpdate", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseBankModel ApproveFranchisePaymentRequestV1(MAdminWallet obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_RequestId", obj.Pk_RequestId);
                queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);
                ResponseBankModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseBankModel>("Proc_FranchisePaymentRequestApproveUpdateV1", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseBankModel DeclineFranchisePaymnetRequest(MAdminWallet obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Pk_RequestId", obj.Pk_RequestId);
                queryParameters.Add("@Remark", obj.ApprovelRemark);

                ResponseBankModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseBankModel>("proc_FranchiseDeclineUpdate", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static List<MAdminWallet> GetFranchiseRequestPaymentHistory(MAdminWallet obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_RequestId", obj.Pk_RequestId);
                List<MAdminWallet> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminWallet>("Proc_GetFranchisePaymnetRequestHistory", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseBankModel Proc_FranchisePaymentRequestApproveUpdateChild(ApproveWalletModel obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@id", obj.Id);
                queryParameters.Add("@PK_RequestId", obj.Pk_RequestId);
                queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);
                queryParameters.Add("@Remarks", obj.Remark);
                queryParameters.Add("@Type", obj.StatusValue);
                queryParameters.Add("@ActualandNewAmount", obj.ActualAmount);                
                ResponseBankModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseBankModel>("Proc_FranchisePaymentRequestApproveUpdateChild", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<VirtualCompanion> GetVirtualFrnachisePaymentRequest(VirtualCompanion obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ProcId", 1);
                queryParameters.Add("@Name", obj.AssociateName);
                queryParameters.Add("@Mobile", obj.MobileNo);
                queryParameters.Add("@Page", obj.Page);
                queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);
                List<VirtualCompanion> _iresult = DBHelperDapper.DAAddAndReturnModelList<VirtualCompanion>("dbo.VirtualCompanionPaymentRequest", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<VirtualCompanion> GetVirtualFrnachiseApproveDecine(VirtualCompanion obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ProcId", 2);
                queryParameters.Add("@LoginId", obj.AssociateId);
                queryParameters.Add("@Name", obj.AssociateName);
                queryParameters.Add("@Mobile", obj.MobileNo);
                queryParameters.Add("@Page", obj.Page);
                queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);
                List<VirtualCompanion> _iresult = DBHelperDapper.DAAddAndReturnModelList<VirtualCompanion>("dbo.VirtualCompanionPaymentRequest", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<VirtualCompanion> GetVirtualFrnachiseApproveDecineForExcel(VirtualCompanion obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ProcId", 3);
                queryParameters.Add("@LoginId", obj.AssociateId);
                queryParameters.Add("@Name", obj.AssociateName);
                queryParameters.Add("@Mobile", obj.MobileNo);
                queryParameters.Add("@Page", obj.Page);
                queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);
                List<VirtualCompanion> _iresult = DBHelperDapper.DAAddAndReturnModelList<VirtualCompanion>("dbo.VirtualCompanionPaymentRequest", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResponseBankModel CompanionApproveDeclineRequest(ViretualCompanionPaymentRequest obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@CompanionId", obj.CompanionId);
                queryParameters.Add("@Type", obj.Type);
                queryParameters.Add("@Remark", obj.Remark??string.Empty);
                queryParameters.Add("@Pincode", obj.Pincode??string.Empty);
                ResponseBankModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseBankModel>("dbo.Proc_VirtualCompanionPaymentRequestApprovedecline", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static FranchiseDetailsByPincode GetFranchiseDetailsByPincode(string Pincode)
        {
            try
            {
                string _qury = "Proc_GetFranchiseByPinCode @Pincode=" + Pincode + "";
                FranchiseDetailsByPincode _iresult = DBHelperDapper.DAGetDetailsInList<FranchiseDetailsByPincode>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MAdminWallet> GetVirtualCompanionPaymnetData(MAdminWallet obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@LoginId", obj.LoginId);
                queryParameters.Add("@Name", obj.DisplayName);
                queryParameters.Add("@Isapprove", obj.IsApprove);
                queryParameters.Add("@FromDate", obj.FromDate);
                queryParameters.Add("@ToDate", obj.ToDate);
                queryParameters.Add("@Page", obj.Page);
                queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);
                List<MAdminWallet> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminWallet>("dbo.Proc_GetVirtualCompanionPaymnetRequest", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MAdminWallet> GetFranchisePaymnetDataForExcel(MAdminWallet obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@LoginId", obj.LoginId);
                queryParameters.Add("@Name", obj.DisplayName);
                queryParameters.Add("@Isapprove", obj.IsApprove);
                queryParameters.Add("@FromDate", obj.FromDate);
                queryParameters.Add("@ToDate", obj.ToDate);
                queryParameters.Add("@Page", obj.Page);
                queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);
                //List<MAdminWallet> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminWallet>("Proc_GetFranchisePaymnetRequest", queryParameters);
                //List<MAdminWallet> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminWallet>("Proc_GetFranchisePaymnetRequest_Bkp2632021", queryParameters);
                List<MAdminWallet> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminWallet>("dbo.Proc_GetFranchisePaymnetRequestV1ForExcel", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MAdminWallet> GetEWalletData(MAdminWallet obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@LoginId", obj.LoginId);
                queryParameters.Add("@FromDate", obj.FromDate);
                queryParameters.Add("@ToDate", obj.ToDate);
                queryParameters.Add("@Page", obj.Page);
                queryParameters.Add("@Size", obj.Size > 0 ? obj.Size : SessionManager.Size);
                queryParameters.Add("@ExportToExcel",obj.IsExport);

                List<MAdminWallet> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminWallet>("GetEwalletLedger", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}