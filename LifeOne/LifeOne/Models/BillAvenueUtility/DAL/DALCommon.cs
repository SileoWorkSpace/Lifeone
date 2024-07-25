using LifeOne.Models.BillAvenueUtility.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using LifeOne.Models.API.DAL;

namespace LifeOne.Models.BillAvenueUtility.DAL
{
    public class DALCommon
    {
        static string  APPAeskey = "E91EC2355DEF8DBB75B9D7602C5F8442";
        static string Aeskey = ConfigurationManager.AppSettings["AeskeyUtility"].ToString();
        public static List<T> GetGenericRechareAPIResponseWithDecryptList<T>(ResponseModel _obj)
        {
            try
            {
                string _date = ApiEncrypt_Decrypt.DecryptString(APPAeskey, _obj.body);
                List<T> _objPrepaidResponse = JsonConvert.DeserializeObject<List<T>>(_date);
                return _objPrepaidResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static T CustomDeserializeObjectWithDecryptString<T>(string _body)
        {
            try
            {
                string _dateresponse = ApiEncrypt_Decrypt.DecryptString(BIllAvenueCredientials.AeskeyUtility, _body);
                T _finalResponse = JsonConvert.DeserializeObject<T>(_dateresponse);

                return _finalResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string CustomSerializeObjectWithEncryptString<T>(T _obj)
        {
            try
            {
                string _sResult = JsonConvert.SerializeObject(_obj);
                string _encryptedText = ApiEncrypt_Decrypt.EncryptString(BIllAvenueCredientials.AeskeyUtility, _sResult);
                return _encryptedText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string CustomSerializeObjectWithEncryptList<T>(List<T> _obj)
        {
            try
            {
                string _sResult = JsonConvert.SerializeObject(_obj);
                string _encryptedText = ApiEncrypt_Decrypt.EncryptString(BIllAvenueCredientials.AeskeyUtility, _sResult);
                return _encryptedText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RemoveCompanyIdAndOtherParams(string _xmlresult)
        {
            try
            {
                XDocument doc = XDocument.Parse(_xmlresult);
                doc.Descendants("CompanyId").Remove();
                doc.Descendants("Fk_MemID").Remove();
                doc.Descendants("AdnroidId").Remove();
                doc.Descendants("DeviceId").Remove();
                doc.Descendants("IPAdressAPP").Remove();
                doc.Descendants("ProcId").Remove();
                doc.Descendants("EnvId").Remove();
                string _finalxml = doc.ToString();
                return _finalxml;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetCustomBillerDetail(RequestModel requestModel)
        {
            SqlParameter[] para = {
                                      new SqlParameter("@billercategory", requestModel.BillerCategory),
                                      new SqlParameter("@billerId", requestModel.BillerID),

                                 };
            DataSet ds = DBHelper.ExecuteQuery(ProcedureName.Proc_GetCustomBillerDetail, para);

            return ds;
        }
    }
    public class ProcedureName
    {
        public static string BASaveAllTransactionDetails = "BASaveAllTransactionDetails";
        public static string SaveTransRequest = "SaveTransRequest";
        public static string Proc_GetCustomBillerDetail = "Proc_GetCustomBillerDetail";

    }
    public class BIllAvenueCredientials
    {
        public static string BAVRequestId = "27";
        public static string AeskeyUtility = "Utility@#@132XYZ";


        //public static string BAVinstituteId = "SG05";
        //public static string BAVaccessCode = "AVOF40XA72NH09DZNT";
        //public static string BAVworkingKey = "F3D7B63F6ABF0A7A69637220E6669943";
        //public static string BAVver = "1.0";
        //public static string MBaseUrlAPIBill = "https://stgapi.billavenue.com/billpay/";

        public static string BAVinstituteId = "AV66";
        public static string BAVaccessCode = "AVEE28GH06UN61BZDV";
        public static string BAVworkingKey = "430CC2227E8FB2911B3E75C87DF800C0";
        public static string BAVver = "1.0";
        public static string MBaseUrlAPIBill = "https://api.billavenue.com/billpay/";


    }
    public class DALCommonEncryptionDecryptionForBillAvenue
    {

        static string APPAeskey = "E91EC2355DEF8DBB75B9D7602C5F8442";
        public static List<T> GetGenericRechareAPIResponseWithDecryptList<T>(ResponseModel _obj)
        {
            try
            {
                string _date = ApiEncrypt_Decrypt.DecryptString(APPAeskey, _obj.body);
                List<T> _objPrepaidResponse = JsonConvert.DeserializeObject<List<T>>(_date);
                return _objPrepaidResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static T CustomDeserializeObjectWithDecryptString<T>(string _body)
        {
            try
            {
                string _dateresponse = ApiEncrypt_Decrypt.DecryptString(APPAeskey, _body);
                T _finalResponse = JsonConvert.DeserializeObject<T>(_dateresponse);
                return _finalResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string CustomEncryptString(string _bodyData)
        {
            try
            {
                string _encryptedText = ApiEncrypt_Decrypt.EncryptString(APPAeskey, _bodyData);
                return _encryptedText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string CustomSerializeObjectWithEncryptList<T>(List<T> _obj)
        {
            try
            {
                string _sResult = JsonConvert.SerializeObject(_obj);
                string _encryptedText = ApiEncrypt_Decrypt.EncryptString(APPAeskey, _sResult);
                return _encryptedText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class DALErrorManament
    {

        public static string CommonErrorMsg= "please try again later - System generated Error:- ";

        public static ResponseModel ErroMethod<T>(T _responsedetails)
        {
            ResponseModel _objResponseModel = new ResponseModel();          
            _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString<T>(_responsedetails);
            return _objResponseModel;
        }
          public static ResponseModel ErroMethodstring(string msg)
        {
            ResponseModel _objResponseModel = new ResponseModel();          
            _objResponseModel.body = DALCommon.CustomSerializeObjectWithEncryptString(msg);
            return _objResponseModel;
        }

    }




}