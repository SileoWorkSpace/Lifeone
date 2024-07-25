using LifeOne.Models.TravelModel.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.TravelModel.DAL
{
    public class DALCommon
    {
        public static List<T> GetGenericAPIResponseWithDecryptList<T>(ResponseModel _obj)
        {
            try
            {
                string _date = ApiEncrypt_Decrypt.DecryptString(DBHelper.Aeskey, _obj.body);
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
                string _dateresponse = ApiEncrypt_Decrypt.DecryptString(DBHelper.Aeskey, _body);
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
                string _encryptedText = ApiEncrypt_Decrypt.EncryptString(DBHelper.Aeskey, _sResult);
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
                string _encryptedText = ApiEncrypt_Decrypt.EncryptString(DBHelper.Aeskey, _sResult);
                return _encryptedText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}