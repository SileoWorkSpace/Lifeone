using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class WalletService
    {
        ~WalletService() { }

        public List<MAdminWallet> GetWalletService(MAdminWallet _obj)
        {
            List<MAdminWallet> _objResponseModel = new List<MAdminWallet>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = AdminDALWallet.GetData(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseBankModel ApproveRequestService(MAdminWallet _obj)
        {
            ResponseBankModel _objResponseModel = new ResponseBankModel();
            try
            {
                _objResponseModel = AdminDALWallet.ApproveRequest(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public ResponseBankModel DeclineRequestService(MAdminWallet _obj)
        {
            ResponseBankModel _objResponseModel = new ResponseBankModel();
            try
            {
                _objResponseModel = AdminDALWallet.DeclineRequest(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public List<MAdminWallet> GetFranchisePaymentService(MAdminWallet _obj)
        {
            List<MAdminWallet> _objResponseModel = new List<MAdminWallet>();
            try
            {
                //decrypt app team reponse
                _objResponseModel = AdminDALWallet.GetFranchisePaymnetData(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseBankModel ApproveFranchisePaymentRequestService(MAdminWallet _obj)
        {
            ResponseBankModel _objResponseModel = new ResponseBankModel();
            try
            {
                _objResponseModel = AdminDALWallet.ApproveFranchisePaymentRequest(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public ResponseBankModel DeclineFranchisePaymentRequestService(MAdminWallet _obj)
        {
            ResponseBankModel _objResponseModel = new ResponseBankModel();
            try
            {
                _objResponseModel = AdminDALWallet.DeclineFranchisePaymnetRequest(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public List<MAdminWallet> GetFranchiseRequestPaymentHistory(MAdminWallet _obj)
        {
            List<MAdminWallet> _objResponseModel = new List<MAdminWallet>();
            try
            {
                //decrypt app team reponse
                _objResponseModel = AdminDALWallet.GetFranchiseRequestPaymentHistory(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseBankModel Proc_FranchisePaymentRequestApproveUpdateChild(ApproveWalletModel model)
        {
            ResponseBankModel _objResponseModel = new ResponseBankModel();
            try
            {
                _objResponseModel = AdminDALWallet.Proc_FranchisePaymentRequestApproveUpdateChild(model);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public List<VirtualCompanion> GetVirtualCompanionPaymentRequest(VirtualCompanion _obj)
        {
            List<VirtualCompanion> _objResponseModel = new List<VirtualCompanion>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = AdminDALWallet.GetVirtualFrnachisePaymentRequest(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public List<VirtualCompanion> GetVirtualCompanionApproveDecline(VirtualCompanion _obj)
        {
            List<VirtualCompanion> _objResponseModel = new List<VirtualCompanion>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = AdminDALWallet.GetVirtualFrnachiseApproveDecine(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public List<VirtualCompanion> GetVirtualCompanionApproveDeclineForExcel(VirtualCompanion _obj)
        {
            List<VirtualCompanion> _objResponseModel = new List<VirtualCompanion>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = AdminDALWallet.GetVirtualFrnachiseApproveDecineForExcel(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public ResponseBankModel CompanionApproveDeclineRequestService(ViretualCompanionPaymentRequest _obj)
        {
            ResponseBankModel _objResponseModel = new ResponseBankModel();
            try
            {
                _objResponseModel = AdminDALWallet.CompanionApproveDeclineRequest(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public static FranchiseDetailsByPincode GetFranchiseDetailByPincode(string Pincode)
        {
            FranchiseDetailsByPincode obj = AdminDALWallet.GetFranchiseDetailsByPincode(Pincode);
            return obj;
        }
        public List<MAdminWallet> GetVirtualCompanionPaymentService(MAdminWallet _obj)
        {
            List<MAdminWallet> _objResponseModel = new List<MAdminWallet>();
            try
            {
                //decrypt app team reponse
                _objResponseModel = AdminDALWallet.GetVirtualCompanionPaymnetData(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public List<MAdminWallet> GetFranchisePaymentForExcelService(MAdminWallet _obj)
        {
            List<MAdminWallet> _objResponseModel = new List<MAdminWallet>();
            try
            {
                //decrypt app team reponse
                _objResponseModel = AdminDALWallet.GetFranchisePaymnetDataForExcel(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

    }
}