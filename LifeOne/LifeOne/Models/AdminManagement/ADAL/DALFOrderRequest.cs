using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Common;
using LifeOne.Models.AdminManagement.ADAL;
using Dapper;
using LifeOne.Models.Manager;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALFOrderRequest
    {
        public static List<MFAOrderRequest> DALGetNewFOrderRequest(MFAOrderRequest _param)
        {
            try
            {
                //string _qry = "Proc_GetFranchiseOrderDetails @ProcId=1,@ParentId=" + _param.ParentId+ ",@Page="+_param.Page+ ",@size="+SessionManager.Size+"";
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ProcId", _param.ProcId);
                queryParameters.Add("@ParentId", _param.ParentId);
                queryParameters.Add("@LoginId", _param.LoginID);
                queryParameters.Add("@Name", _param.FName);
                queryParameters.Add("@mobile", _param.Contact);
                queryParameters.Add("@OrderNo", _param.OrderNo);
                queryParameters.Add("@Page", _param.Page);
                queryParameters.Add("@size", _param.Size > 0 ? _param.Size : SessionManager.Size);
                queryParameters.Add("@IsExport", _param.IsExport);
                List<MFAOrderRequest> _iresult = DBHelperDapper.DAAddAndReturnModelList<MFAOrderRequest>("Proc_GetFranchiseOrderDetails", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*Bind BID order request Information*/
        public static List<MFAOrderRequest> DALAssociateGetNewFOrderRequest(MFAOrderRequest _param)
        {
            try
            {
                //string _qry = " @ProcId=6,@ParentId=" + _param.ParentId + ",@OrderNo=" + _param.OrderNo + ",@DateSearch=" + _param.ShippingDate + ",@Page=" + _param.Page + ",@size=" + SessionManager.Size + "";

                var queryParameters = new DynamicParameters();
                queryParameters.Add("@ProcId", "6");
                queryParameters.Add("@ParentId", _param.ParentId);
                queryParameters.Add("@OrderNo", _param.OrderNo);
                queryParameters.Add("@DateSearch", _param.ReqDate);
                queryParameters.Add("@Page", _param.Page);
                queryParameters.Add("@Size", _param.Size > 0 ? _param.Size : SessionManager.Size);
             
                List<MFAOrderRequest> _iresult = DBHelperDapper.DAAddAndReturnModelList<MFAOrderRequest>("Proc_GetOrderRequestAdmin", queryParameters);               
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public static List<MFAOrderRequestDetail> DALGetNewFOrderRequestDetail(long RequestId) 
        {
            try
            {
                string _qry = "Proc_GetOrderRequestAdmin @ProcId=2,@ReqId="+RequestId+",@Fk_MemId="+SessionManager.Fk_MemId+"";
                List<MFAOrderRequestDetail> _iresult = DBHelperDapper.DAGetDetailsInList<MFAOrderRequestDetail>(_qry);
                return _iresult;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public static List<MFAOrderRequestDetail> DALGetNewFOrderRequestDetailFranchise(long RequestId, long Fk_MemId)
        {
            try
            {
                string _qry = "Proc_GetOrderRequestAdmin @ProcId=5,@ReqId=" + RequestId + ",@FranchiseId=" + Fk_MemId + "";
                List<MFAOrderRequestDetail> _iresult = DBHelperDapper.DAGetDetailsInList<MFAOrderRequestDetail>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<MFAPaymentStatus> DALGetFPaymentDetail(long RequestId)
        {
            try
            {
                string _qry = "Proc_GetOrderRequestAdmin @ProcId=3,@ReqId=" + RequestId + "";
                List<MFAPaymentStatus> _iresult = DBHelperDapper.DAGetDetailsInList<MFAPaymentStatus>(_qry);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MSimpleString DALApproveProduct(XmlModel _objmodel)
        {
            try
            {
                //PROC_ApproveOrderRequest
                //In the case of Franchise product approval, once the Franchise Recive Product Qty...Qty will be Credited on the Franchise Stsock
                var queryParameters = new DynamicParameters(); 
                queryParameters.Add("@XmlData", _objmodel.XmlData);
                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("PROC_ApproveOrderRequest", queryParameters);                
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MSimpleString DALApproveProductByFranchise(XmlModel _objmodel)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@XmlData", _objmodel.XmlData);
                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("PROC_ApproveOrderRequestByFranchise", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MSimpleString DALApproveDeclinePayment(long Reqid,int Status)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Reqid", Reqid);
                queryParameters.Add("@Status", Status);
                MSimpleString _iresult = DBHelperDapper.DAAddAndReturnModel<MSimpleString>("Proc_ApproveDeclinepaymentStatus", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CancelOrderResponse DALFranchiseCancelOrder(string OrderNo)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@orderNo", OrderNo);
                queryParameters.Add("@CreatedBy", SessionManager.Fk_MemId);
                CancelOrderResponse _iresult = DBHelperDapper.DAAddAndReturnModel<CancelOrderResponse>("FranchiseCancelOrder", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CancelOrderResponse FranchiseCancelOrderAfterApproved(string OrderNo)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@orderNo", OrderNo);
                CancelOrderResponse _iresult = DBHelperDapper.DAAddAndReturnModel<CancelOrderResponse>("FranchiseCancelOrderAfterApproved", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CancelOrderResponse FranchiseCancelledOrderItem(int Pk_OrderRequestItemsRId, int Pk_RId)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Pk_OrderRequestItemsRId", Pk_OrderRequestItemsRId);
                queryParameters.Add("@Pk_RId", Pk_RId);
                queryParameters.Add("@CreatedByid", SessionManager.Fk_MemId);
                CancelOrderResponse _iresult = DBHelperDapper.DAAddAndReturnModel<CancelOrderResponse>("ModifyFranchiseApprovedOrder", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}