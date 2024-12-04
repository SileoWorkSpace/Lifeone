using System;
using System.Web.Http;
using System.Configuration;
using LifeOne.Models.QUtility.Entity;
using LifeOne.Models.QUtility.Service;

namespace LifeOne.Controllers
{
	public class UtilitiesController : ApiController
    {
        string Aeskey = ConfigurationManager.AppSettings["Aeskey"].ToString();
        [HttpPost] //Hit the Quaere company wallet api
        public ResponseModel CheckCompanyWalletBalance(RequestModel _model)
        {
            try
            {
                ResponseModel _result = new ResponseModel();
                WalletService _objwalletservice = new WalletService();
                _result = _objwalletservice.ChkCompanyWalletBalanceService(_model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ResponseModel CheckMemberWalletBalance(RequestModel _model)
        {
            try
            {
                WalletService _objwalletservice = new WalletService();
                ResponseModel _result = _objwalletservice.ChkMemberWalletBalanceService(_model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel VerifyMemberOTP(RequestModel _model)
        {
            try
            {
                VerifyTransactionPasswordService _objwalletservice = new VerifyTransactionPasswordService();
                ResponseModel _result = _objwalletservice.VerifyMemberWalletPassword(_model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Begin
        //All Recharge Informaiton
        //payada and LifeOnee intigration
      
        [HttpPost]
        public ResponseModel PrePaidRecharge(RequestModel _model)
        {
            try
            {
                PrepaidService _objprepaidService = new PrepaidService();
                ResponseModel _result = _objprepaidService.CallPrepaidAndOtherRechargeService(_model, "PrepaidRecharge", "Prepaid Recharge", "PrepaidRecharge");
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel PostpaidRecharge(RequestModel _model)
        {
            try
            {
                PostPaidService _objpostpaidService = new PostPaidService();
                ResponseModel _result = _objpostpaidService.CallPostPaidAndOtherRechargeService(_model, "PostpaidRecharge", "Postpaid Recharge", "BBPS");
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ResponseModel DTH(RequestModel _model)
        {
            try
            {   //CallPrepaidAndOtherRechargeService - Enable us to recharge Prepaid, PostPaid(BBPS),DTH,TataskyRecharge,ThemeParkGiftCard            
                DTHService _objprepaidService = new DTHService();
                ResponseModel _result = _objprepaidService.CallDTHRechargeService(_model, "DTH", "DTH Recharge", "DTH");
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

       
        [HttpPost]
        public ResponseModel DataCard(RequestModel _model)
        {
            try
            {
                DataCardService _objprepaidService = new DataCardService();
                ResponseModel _result = _objprepaidService.CallDataCardAndOtherRechargeService(_model, "DataCard", "DataCard Recharge", "DataCard");
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel BroadBandProvider(RequestModel _model)
        {
            try
            {
                BroadBandService _objprepaidService = new BroadBandService();
                ResponseModel _result = _objprepaidService.CallBroadaBandAndOtherRechargeService(_model, "BroadBandProvider", "BroadBand Recharge", "BBPS");
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel BroadBandVerify(RequestModel _model)
        {
            try
            {
                BroadBandService _objprepaidService = new BroadBandService();
                ResponseModel _result = _objprepaidService.VerifyBraodBandService(_model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel WaterBillPayment(RequestModel _model)
        {
            try
            {
                WaterBillPaymentService _objprepaidService = new WaterBillPaymentService();
                ResponseModel _result = _objprepaidService.CallWaterBillPaymentService(_model, "WaterBillPayment", "Water Bill Payment", "BBPS");
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ResponseModel WaterBillVerification(RequestModel _model)
        {
            try
            {
                WaterBillPaymentService _objprepaidService = new WaterBillPaymentService();
                ResponseModel _result = _objprepaidService.VerifyWaterBillService(_model);
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ResponseModel ElectricityBillPayment(RequestModel _model)
        {
            try
            {
                ElectricityBillPaymentService _objprepaidService = new ElectricityBillPaymentService();
                ResponseModel _result = _objprepaidService.CallElectricityBillPaymentService(_model, "ElectricityBillPayment", "Electricity Bill Payment", "BBPS");
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel ElectricityVerification(RequestModel _model)
        {
            try
            {
                ElectricityBillPaymentService _objprepaidService = new ElectricityBillPaymentService();
                ResponseModel _result = _objprepaidService.VerifyElectricityVerificationService(_model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ResponseModel GasBillPayment(RequestModel _model)
        {
            try
            {
                GasBillPaymentService _objprepaidService = new GasBillPaymentService();
                ResponseModel _result = _objprepaidService.CallGasBillPaymentService(_model, "GasBillPayment", "Gas Bill Payment", "BBPS");
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        //On Cyber plate API Name ForRechargeMobilePrepaid
        public ResponseModel JioMobilePrepaidRecharge(RequestModel _model)
        {
            try
            {
                PrepaidService _objprepaidService = new PrepaidService();
                ResponseModel _result = _objprepaidService.CallPrepaidAndOtherRechargeService(_model, "ForRechargeMobilePrepaid", "Prepaid Recharge", "PrepaidRecharge");
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseModel JioMobilePrepaidRecharge_V2(RequestModel _model)
        {
            try
            {
                PrepaidService _objprepaidService = new PrepaidService();
                ResponseModel _result = _objprepaidService.CallPrepaidAndOtherRechargeService_V2(_model, "ForRechargeMobilePrepaid", "Prepaid Recharge", "PrepaidRecharge");
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel TakaSkyBooking(RequestModel _model)
        {
            try
            {
                TataSkyService _objprepaidService = new TataSkyService();
                ResponseModel _result = _objprepaidService.TakaSkyRechargeService(_model, "Booking", "Tata sky Recharge", "TataskyRecharge");
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //End
        //Recharge Process

        //Otherpart of the API Like GetGetAllProviderStateWise, GetRecentRecharge and many other different API here        
        /*Bind State here*/
        [HttpPost]
        public ResponseModel GetAllProviderStateWise(RequestModel _model)
        {
            try
            {
                ResponseModel _result = new ResponseModel();
                StateService _stateService = new StateService();
                _result = _stateService.GetAllProviderStateWiseService(_model);
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        /*Get Recent GetRecentRecharge Service*/
        public ResponseModel GetRecentRecharge(RequestModel _model)
        {
            try
            {
                ResponseModel _result = new ResponseModel();
                RechargeHistoryService _rechargeService = new RechargeHistoryService();
                _result = _rechargeService.GetRecentRecharegService(_model);
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        /*Get Recent ForFetchPlanPrepaid Service*/
        public ResponseModel ForFetchPlanPrepaid(RequestModel _model)
        {
            try
            {
                ResponseModel _result = new ResponseModel();
                ForFetchPlanPrepaidService _rechargeService = new ForFetchPlanPrepaidService();
                _result = _rechargeService.GetAllForFetchPlanPrepaidService(_model);
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        /*Get Recent ForFetchPlanPrepaid Service*/
        public ResponseModel GetAllProvider(RequestModel _model)
        {
            try
            {
                ResponseModel _result = new ResponseModel();
                AllProviderService _rechargeService = new AllProviderService();
                _result = _rechargeService.GetAllproviderdetails(_model);
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ResponseModel Insurance(RequestModel _model)
        {
            try
            {
                InsuranceService _objprepaidService = new InsuranceService();
                ResponseModel _result = _objprepaidService.CallInsuranceAndOtherRechargeService(_model, "Insurance", "Insurance Payment", "BBPS");
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel InsuranceVerify(RequestModel _model)
        {
            try
            {
                InsuranceService _objprepaidService = new InsuranceService();
                ResponseModel _result = _objprepaidService.VerifyInsuranceService(_model);
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ResponseModel GasPaymentVerification(RequestModel _model)
        {
            try
            {
                GasBillPaymentService _objprepaidService = new GasBillPaymentService();
                ResponseModel _result = _objprepaidService.VerifyGasService(_model);
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*started Beneficary  here 23/08/2020*/
        public ResponseModel CustomerVerification(RequestModel _model)
        {
            try
            {
                BeneficiaryService _objprepaidService = new BeneficiaryService();
                ResponseModel _result = _objprepaidService.CustomerVerificationService(_model);
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseModel RemitterRegistration(RequestModel _model)
        {
            try
            {
                BeneficiaryService _objprepaidService = new BeneficiaryService();
                ResponseModel _result = _objprepaidService.RemitterRegistrationService(_model);
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseModel RemitterRegistrationValidationOTP(RequestModel _model)
        {
            try
            {
                BeneficiaryService _objprepaidService = new BeneficiaryService();
                ResponseModel _result = _objprepaidService.RemitterRegistrationValidationOTPService(_model);
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ResponseModel BeneficiaryRegistration(RequestModel _model)
        {
            try
            {
                BeneficiaryService _objprepaidService = new BeneficiaryService();
                ResponseModel _result = _objprepaidService.BeneficiaryRegistrationService(_model);
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //FundTransfer API left API 
        [HttpPost]
        public ResponseModel AddBeneficiaryDetils(RequestModel _model)
        {
            try
            {
                BeneficiaryService _objprepaidService = new BeneficiaryService();
                ResponseModel _result = _objprepaidService.AddBeneficiaryDetilsService(_model);
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ResponseModel FundTransfer(RequestModel _model)
        {
            try
            {
                BeneficiaryService _objprepaidService = new BeneficiaryService();
                ResponseModel _result = _objprepaidService.FundTransfer(_model);
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ResponseModel FundTransferLog(RequestModel _model)
        {
            try
            {
                BeneficiaryService _objprepaidService = new BeneficiaryService();
                ResponseModel _result = _objprepaidService.FundTransferLogService(_model);
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ResponseModel GetBeneficiaryDetils(RequestModel _model)
        {
            try
            {
                BeneficiaryService _objprepaidService = new BeneficiaryService();
                ResponseModel _result = _objprepaidService.GetBeneficiaryDetilService(_model);
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ResponseModel BeneficiaryDelete(RequestModel _model)
        {
            try
            {
                BeneficiaryService _objprepaidService = new BeneficiaryService();
                ResponseModel _result = _objprepaidService.BeneficiaryDeleteService(_model);
                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
