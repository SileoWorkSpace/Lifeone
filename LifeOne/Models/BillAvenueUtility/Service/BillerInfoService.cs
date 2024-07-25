using LifeOne.Models.BillAvenueUtility.API;
using LifeOne.Models.BillAvenueUtility.DAL;
using LifeOne.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.BillAvenueUtility.Service
{
    public class BillerInfoService
    {
        ~BillerInfoService() { }

        ResponseModel _objResponseModel = new ResponseModel();
        public ResponseModel GetBillerInfoService(RequestModel _obj)
        {
            List<BillInfoDetails> _responsedetails = null;
            try
            {
                //get Reponse From APP Side ..
                MbillInfoRequest _biilDetails = DALCommon.CustomDeserializeObjectWithDecryptString<MbillInfoRequest>(_obj.body);
                //Now convert the Model into XML for BiilAvenue


                //Calling API Here
                _responsedetails = DALBillInfo.GetBillerDetailsByBillerId(_biilDetails.billerId);
                _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptList<BillInfoDetails>(_responsedetails); // on success we will be added records on
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
                //_responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                //return DALErrorManament.ErroMethod<DALErrorManament>( ex.ToString());
            }
        }

        public ResponseModel GetBillirIdFromCategoryService(RequestModel _obj)
        {
            List<BillInfoDetailsByCategory> _responsedetails = null;
            try
            {
                //get Reponse From APP Side ..
                MbillInfoByCategoryRequest _biilDetails = DALCommon.CustomDeserializeObjectWithDecryptString<MbillInfoByCategoryRequest>(_obj.body);
                //Now convert the Model into XML for BiilAvenue

                //Calling API Here
                _responsedetails = DALBillInfo.GetBillirIdFromCategory(_biilDetails.Category);
                _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptList<BillInfoDetailsByCategory>(_responsedetails); // on success we will be added records on
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
                //_responsedetails.errorInfo.error.errorMessage = DALErrorManament.CommonErrorMsg + ex.ToString();
                //return DALErrorManament.ErroMethod<DALErrorManament>( ex.ToString());
            }

        }
   
    
    
    
    
    
    }
}