using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.Common;


namespace LifeOne.Models.AdminManagement.AService
{
    public class FOrderRequestService
    {
        public static List<MFAOrderRequest>GetNewOrderRequestService(MFAOrderRequest _obj)
        {
            List<MFAOrderRequest> _objResponseModel = null;
            try
            {
                _objResponseModel = DALFOrderRequest.DALGetNewFOrderRequest(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }


        public static List<MFAOrderRequest> GetAsociateNewOrderRequestService(MFAOrderRequest _obj)
        {
            List<MFAOrderRequest> _objResponseModel = null;
            try
            {
                _objResponseModel = DALFOrderRequest.DALAssociateGetNewFOrderRequest(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }


        public static List<MFAOrderRequestDetail> GetNewOrderRequestItemService(long RequestId)
        {
            List<MFAOrderRequestDetail> _objResponseDetailModel = null;
            try
            {
                _objResponseDetailModel = DALFOrderRequest.DALGetNewFOrderRequestDetail(RequestId);
                return _objResponseDetailModel;
            }
            catch (Exception ex)
            {
                return _objResponseDetailModel;
            }
        }
        public static List<MFAOrderRequestDetail> GetNewOrderRequestItemFranchiseService(long RequestId, long Fk_MemId)
        {
            List<MFAOrderRequestDetail> _objResponseDetailModel = null;
            try
            {
                _objResponseDetailModel = DALFOrderRequest.DALGetNewFOrderRequestDetailFranchise(RequestId, Fk_MemId);
                return _objResponseDetailModel;
            }
            catch (Exception ex)
            {
                return _objResponseDetailModel;
            }
        }

        public static List<MFAPaymentStatus> GetFPaymentDetailService(long RequestId)
        {
            List<MFAPaymentStatus> _objResponseDetailModel = null;
            try
            {
                _objResponseDetailModel = DALFOrderRequest.DALGetFPaymentDetail(RequestId);
                return _objResponseDetailModel;
            }
            catch (Exception ex)
            {
                return _objResponseDetailModel;
            }
        }
        public static MSimpleString ApprveProductService(XmlModel _objxml)
        {
            MSimpleString _objResponseDetailModel = null;
            try
            {
                _objResponseDetailModel = DALFOrderRequest.DALApproveProduct(_objxml);
                return _objResponseDetailModel;
            }
            catch (Exception ex)
            {
                return _objResponseDetailModel;
            }
        }
        public static MSimpleString ApprveProductServiceByFranchise(XmlModel _objxml)
        {
            MSimpleString _objResponseDetailModel = null;
            try
            {
                _objResponseDetailModel = DALFOrderRequest.DALApproveProductByFranchise(_objxml);
                return _objResponseDetailModel;
            }
            catch (Exception ex)
            {
                return _objResponseDetailModel;
            }
        }
        public static MSimpleString ApprveDeclinepaymentService(long Reqid,int Status)
        {
            MSimpleString _objResponseDetailModel = null;
            try
            {
                _objResponseDetailModel = DALFOrderRequest.DALApproveDeclinePayment(Reqid,Status);
                return _objResponseDetailModel;
            }
            catch (Exception ex)
            {
                return _objResponseDetailModel;
            }
        }

        public static CancelOrderResponse FranchiseCancelOrder(string OrderNo)
        {
            CancelOrderResponse _objResponseDetailModel = null;
            try
            {
                _objResponseDetailModel = DALFOrderRequest.DALFranchiseCancelOrder(OrderNo);
                return _objResponseDetailModel;
            }
            catch (Exception ex)
            {
                return _objResponseDetailModel;
            }
        }
    }
}