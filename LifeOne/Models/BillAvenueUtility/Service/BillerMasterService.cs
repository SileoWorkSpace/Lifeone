using SGCareWeb.Models.BillAvenueUtility.API;
using SGCareWeb.Models.BillAvenueUtility.DAL;
using SGCareWeb.Models.BillAvenueUtility.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;

namespace SGCareWeb.Models.BillAvenueUtility.Service
{
    public class BillerMasterService
    {

        // As per this API. we can hit only 3 times in a day. 
        //It Enables us to update our BillInfo Master Table.
        //BillInfo table allows us to know all types of Bill, Prepaid, and Postpaid BillerID which are needed to Pass in every API request.

        ResponseModel _objResponseModel = new ResponseModel();
        public ResponseModel InvokeBillInformationMasterService(RequestModel _obj)
        {
            try
            {            
                //get Reponse From APP Side ..Futher, we will be stored request on the database here..
                MbillInfoRequest _biilDetails = DALCommon.CustomDeserializeObjectWithDecryptString<MbillInfoRequest>(_obj.body);
                string _url = "BillerInfoMaster";
                //Calling API Here
                _objResponseModel = GenericAPI.sendRequestToUtilityAPIPost(_obj, _url);
                MbillerInfoResponse _responsedetails = DALCommon.CustomDeserializeObjectWithDecryptString<MbillerInfoResponse>(_objResponseModel.body);
                if (_responsedetails.responseCode == "000" || _responsedetails.responseCode == "111") //000 Recharge successful   / 111 Recharge in process                
                {
                    //DALBillInfo.SaveBillInfoResponse(_responsedetails);  //DB
                }
                else
                    _objResponseModel.body = MCommonErrorAndSuccessMsg.ErrorMsg;

                _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<MbillerInfoResponse>(_responsedetails); // on success we will be added records on 
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static void  GetDataFromBillInfoMaster(string _xmlDetails)
        {
            try
            {                
                string onlyContent = string.Empty;
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(@"E:/amar-project/RightmoveAPI/SGCareWeb/billinfoResponse.txt");
                XmlNodeList nodeList = xdoc.SelectNodes("billerInfoResponse/biller");
                List<MBillInfoMaster> infos = new List<MBillInfoMaster>();

                foreach (XmlNode xmlNode in nodeList)
                {
                    MBillInfoMaster info = new MBillInfoMaster();
                    //info.billerId = xmlNode.InnerText;
                    info.billerId = xmlNode["billerId"].InnerText;
                    info.billerName = xmlNode["billerName"].InnerText;
                    info.billerCategory = xmlNode["billerCategory"].InnerText;
                    info.billerAdhoc = xmlNode["billerAdhoc"].InnerText;
                    info.billerCoverage = xmlNode["billerCoverage"].InnerText;
                    info.billerFetchRequiremet = xmlNode["billerFetchRequiremet"].InnerText;
                    info.billerPaymentExactness = xmlNode["billerPaymentExactness"].InnerText;
                    info.billerSupportBillValidation = xmlNode["billerSupportBillValidation"].InnerText;
                    info.supportPendingStatus = xmlNode["supportPendingStatus"].InnerText;
                    info.supportDeemed = xmlNode["supportDeemed"].InnerText;
                    info.billerTimeout = xmlNode["billerTimeout"].InnerText;
                    info.billerAmountOptions = xmlNode["billerAmountOptions"].InnerText;
                    info.billerDescription = xmlNode["billerDescription"].InnerText;
                    info.billerPaymentModes = xmlNode["billerPaymentModes"].InnerText;
                    info.rechargeAmountInValidationRequest = xmlNode["rechargeAmountInValidationRequest"].InnerText;
                    infos.Add(info);
                }
                DataTable dt = ToDataTable(infos);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

    }
}