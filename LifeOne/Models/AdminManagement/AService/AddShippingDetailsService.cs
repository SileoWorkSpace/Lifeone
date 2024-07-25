using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.FranchiseManagement.FEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class AddShippingDetailsService
    {
        public static int AddShippingInformationService(MAddShippingInformation _param)
        {

            int _iresult = DALShippingInformation.DALAddShippingInformation(_param);
            return _iresult;

        }
        public static int AddActiveMemberShippingInformationService(MAddShippingInformation _param)
        {

            int _iresult = DALShippingInformation.DALAddActiveMemberShippingInformation(_param);
            return _iresult;

        }

        public static int AddAssignShippingInformationService(MAddShippingInformation _param)
        {

            int _iresult = DALShippingInformation.DALAddShippingInformationForCustomer(_param);
            return _iresult;

        }

        public static int ApproveOrderStockService(MBindAssignOrdertoFranchise _param)
        {
            try
            {
                _param.Fk_memid = Convert.ToInt64(SessionManager.Fk_MemId);
                int _iresult = DALShippingInformation.DALApproveOrderStock(_param);
                return _iresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<MAddShippingInformation> GetNewOrderRequestService(string _ReqId)
        {
            List<MAddShippingInformation> _objResponseModel = null;
            try
            {
                _objResponseModel = DALShippingInformation.DALGetSgippingInformationOnEdit(_ReqId);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public static List<MAddShippingInformation> GetEditOrderRequestService(string Fk_TransId)
        {
            List<MAddShippingInformation> _objResponseModel = null;
            try
            {
                _objResponseModel = DALShippingInformation.DALGetShippingInformationEdit(Fk_TransId);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }


        public static List<MAddShippingInformation> GetAssignEditOrderService(string Fk_FbSID, long FId)
        {
            List<MAddShippingInformation> _objResponseModel = null;
            try
            {
                _objResponseModel = DALShippingInformation.DALAssignGetSgippingInformationOnEdit(Fk_FbSID,FId);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }


        public static List<MShowSHippingInfoToAssociate> GetShippingInforForAssociateServie(string _ReqId)
        {
            List<MShowSHippingInfoToAssociate> _objResponseModel = null;
            try
            {
                _objResponseModel = DALShippingInformation.DalShowShippingInfoToAsssciate(_ReqId);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }


        public static List<MBindAssignOrdertoFranchise> GetAssignFranchOrdrInfoamtion(string _FkBID)
        {
            List<MBindAssignOrdertoFranchise> _objResponseModel = null;
            try
            {
                _objResponseModel = DALShippingInformation.DALGetAssignFranchOrdrInfoamtion(_FkBID);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public static ShippingDetailViewModel GetShippingInvoice(long FranchiseId, int FK_FbSID)
        {
            ShippingDetailViewModel _objResponseModel = null;
            try
            {                
                return _objResponseModel= DBHelperDapper.DALGetShippingInvoice(FranchiseId, FK_FbSID); 
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public static UpdateInvoicedateReponse UpdateInvoicedate(int RequestId, string Invoicedate)
        {
            UpdateInvoicedateReponse _objResponseModel = null;
            try
            {
                return _objResponseModel = DALShippingInformation.DALUpdateInvoiceDate(RequestId, Invoicedate);
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}