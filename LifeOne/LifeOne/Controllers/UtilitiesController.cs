using AesEncryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using Newtonsoft.Json;
using LifeOne.Models.QUtility.Entity;
using LifeOne.Models.QUtility.Service;
using LifeOne.Models.API;
using System.Data.SqlClient;
using LifeOne.Models.AdminManagement.ADAL;
using Dapper;
using LifeOne.Models.API.DAL;
using LifeOne.Models.BillAvenueUtility.DAL;

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
        public ResponseModel PrePaidRechargeV2(RequestModel _model)
        {
            try
            {
                RechargeModel req = new RechargeModel();
                var QuaryPara = new DynamicParameters();
                RechargeApiModeModel ApiModeRes = DBHelperDapper.DAAddAndReturnModel<RechargeApiModeModel>("ProGetRechargeAPIMode", QuaryPara);
                ResponseModel _result = new ResponseModel();
                if (ApiModeRes.IsPayAdda)
                {
                    ApiCommonRechargeModel _Req = DALCommon.CustomDeserializeObjectWithDecryptString<ApiCommonRechargeModel>(_model.body);
                    var QuaryParameter = new DynamicParameters();
                    QuaryParameter.Add("@BillerId", string.Empty);
                    QuaryParameter.Add("@Provider", _Req.Provider);
                    QuaryParameter.Add("@TypeId", 1);
                    RechargeModel _obj = DBHelperDapper.DAAddAndReturnModel<RechargeModel>("ProGetOperatorByBillerId", QuaryParameter);
                    if (_obj != null)
                    {
                        //if (_obj.OperatorId == 10)//--10 Used for Jio temprary added this code will remove it ahead
                        //    _result = new UtilitiesController().JioMobilePrepaidRecharge(_model);
                        //else
                        //{
                            req.CustomerNumber = _Req.Number;
                            req.RechargeType = _obj.RechargeType;
                            req.OperatorId = _obj.OperatorId;
                            req.TransactionAmount = decimal.ToInt32(Convert.ToDecimal(_Req.Amount));
                            _result = new RechargerApiBusiness().RechargeV2(req, _model, "Prepaid Recharge", "PrepaidRecharge");
                       //}
                    }
                    else
                    {
                        return new ResponseModel { body = DALCommon.CustomSerializeObjectWithEncryptString(new List<PrepaidResponse> { new PrepaidResponse { Message = "Invalid Provider", Status = "failed", ERROR = "1", RESULT = "1", ERRMSG = "Invalid Provider" } }) };
                    }
                }
                else if (ApiModeRes.IsLifeOnee)
                {
                    ApiCommonRechargeModel _Req = DALCommon.CustomDeserializeObjectWithDecryptString<ApiCommonRechargeModel>(_model.body);
                    if (_Req.Provider.ToLower() != "jio")
                    {
                        _result = new PrepaidService().CallPrepaidAndOtherRechargeService(_model, "PrepaidRecharge", "Prepaid Recharge", "PrepaidRecharge");
                    }
                    else
                    {
                        _result = new UtilitiesController().JioMobilePrepaidRecharge(_model);
                    }
                }
                else
                {
                    return new ResponseModel { body = DALCommon.CustomSerializeObjectWithEncryptString(new List<PrepaidResponse> { new PrepaidResponse { Message = "Recharge mode not enabled", Status = "failed", ERROR = "1", RESULT = "1", ERRMSG = "Recharge mode not enabled" } }) };
                }
                return _result;
            }
            catch (Exception ex)
            {
                return new ResponseModel { body = DALCommon.CustomSerializeObjectWithEncryptString(new List<PrepaidResponse> { new PrepaidResponse { Message = "We are facing some technical issues as of now please try again later", Status = "failed", ERROR = "1", RESULT = "1", ERRMSG = "We are facing some technical issues as of now please try again later" } }) };
            }
        }
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
        public ResponseModel DTHV2(RequestModel _model)
        {
            try
            {
                RechargeModel req = new RechargeModel();
                var QuaryPara = new DynamicParameters();
                RechargeApiModeModel ApiModeRes = DBHelperDapper.DAAddAndReturnModel<RechargeApiModeModel>("ProGetRechargeAPIMode", QuaryPara);
                ResponseModel _result = new ResponseModel();
                if (ApiModeRes.IsPayAdda)
                {
                    ApiCommonRechargeModel _Req = DALCommon.CustomDeserializeObjectWithDecryptString<ApiCommonRechargeModel>(_model.body);
                    var QuaryParameter = new DynamicParameters();
                    QuaryParameter.Add("@BillerId", string.Empty);
                    QuaryParameter.Add("@Provider", _Req.Provider);
                    QuaryParameter.Add("@TypeId", 2);
                    RechargeModel _obj = DBHelperDapper.DAAddAndReturnModel<RechargeModel>("ProGetOperatorByBillerId", QuaryParameter);
                    if (_obj != null)
                    {
                        req.CustomerNumber = _Req.Number;
                        req.RechargeType = _obj.RechargeType;
                        req.OperatorId = _obj.OperatorId;
                        req.TransactionAmount = decimal.ToInt32(Convert.ToDecimal(_Req.Amount));
                        _result = new RechargerApiBusiness().RechargeV2(req, _model, "DTH Recharge", "DTH");
                    }
                    else
                    {
                        return new ResponseModel { body = DALCommon.CustomSerializeObjectWithEncryptString(new List<PrepaidResponse> { new PrepaidResponse { Message = "Invalid Provider", Status = "failed", ERROR = "1", RESULT = "1", ERRMSG = "Invalid Provider" } }) };
                    }
                }
                else if (ApiModeRes.IsLifeOnee)
                {

                    //CallPrepaidAndOtherRechargeService - Enable us to recharge Prepaid, PostPaid(BBPS),DTH,TataskyRecharge,ThemeParkGiftCard            
                    DTHService _objprepaidService = new DTHService();
                    _result = _objprepaidService.CallDTHRechargeService(_model, "DTH", "DTH Recharge", "DTH");
                }
                else
                {
                    return new ResponseModel { body = DALCommon.CustomSerializeObjectWithEncryptString(new List<PrepaidResponse> { new PrepaidResponse { Message = "Recharge mode not enabled", Status = "failed", ERROR = "1", RESULT = "1", ERRMSG = "Recharge mode not enabled" } }) };
                }
                return _result;
            }
            catch (Exception)
            {
                return new ResponseModel { body = DALCommon.CustomSerializeObjectWithEncryptString(new List<PrepaidResponse> { new PrepaidResponse { Message = "We are facing some technical issues as of now please try again later", Status = "failed", ERROR = "-1", RESULT = "-1", ERRMSG = "We are facing some technical issues as of now please try again later" } }) };
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
