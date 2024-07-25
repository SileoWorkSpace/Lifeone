using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace LifeOne.Models.BillAvenueUtility.Entity
{

    // string key = "";


    

    public class MCommon
    {
        [XmlElement("CompanyId")]
        public long CompanyId { get; set; }
        [XmlElement("Fk_MemID")]
        public long Fk_MemID { get; set; }
        [XmlElement("IPAdressAPP")]
        public string IPAdressAPP { get; set; }
        [XmlElement("AndroidId")]
        public string AndroidId { get; set; }

        [XmlElement("DeviceId")]
        public string DeviceId { get; set; }
        public string EnvId { get; set; }        
        //public string APIName { get; set; }
        // public string RequestId { get; set; }
        // public int ProcId { get; set; }
        // public string BillerIdRespons { get; set; }

    }

    public class MCommonWithRequesId : MCommon
    {
        public string RequestId { get; set; }
    }



    [Serializable]
     [XmlRoot("inputParams")]
    public class inputParams
    {
        [XmlElement("input")]
        public List<input> input { get; set; }
    }
    [Serializable]
    [XmlRoot("input")]
    public class input
    {
        public string paramName { get; set; }
        public string paramValue { get; set; }
    }




    public class MCommonErrorAndSuccessMsg
    {
        public static string ErrorMsg
        {
            get; set;
            //get { return ErrorMsg; }
            //set { ErrorMsg = "We are facing some technical issues. Please try again later."; }
        }

        public static string SuccessMsg
        {
            get; set;
            //get { return SuccessMsg; }
            //set { SuccessMsg = "Recharge Successful."; }
        }
    }

    [Serializable]
    [XmlRoot("error")]
    public class MError
    {
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
    }
    [Serializable]
    [XmlRoot("errorInfo")]
    public class MErrorInfo
    {
        public MError error { get; set; }
    }
    public class ResponseModel
    {
        public string body { get; set; }
    }
    public class ApiResponse
    {
        public string body { get; set; }
    }

    public class RequestModel
    {
        public string body { get; set; }

        public string BillerID { get; set; }
        public string BillerCategory { get; set; }
    }

    public class ApiEncrypt_Decrypt
    {
        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }
        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {

                aes.KeySize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

    }

    public class Data
    {
        public string order_id { get; set; }
    }

    public class OrderReqModel
    {
        public Data data { get; set; }
    }

    public class CancelOrderModel
    {
        public int status_code { get; set; }
        public string message { get; set; }
    }
}