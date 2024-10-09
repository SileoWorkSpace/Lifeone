using Dapper;
using LifeOne.Models.AdminManagement.AEntity;

using Microsoft.Owin.Security.Notifications;
using Newtonsoft.Json;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifeOne.Models.BillAvenueUtility.DAL;
using LifeOne.Models.QUtility.Entity;
using LifeOne.Models.QUtility.Service;
using System.Data;
using System.Reflection;
using LifeOne.Models.FranchiseManagement.FEntity;
using System.Security.Cryptography;
using System.Text;


namespace LifeOne.Models.Common
{

    public class DALCommon
    {

        public WalletTransactionResponse DALSaveTransactionDetails(RequestModel _obj, string _Action, string _ActionType, string _RechargeType)
        {
            try
            {
                string _dataRequestModel = ApiEncrypt_Decrypt.DecryptString(DBHelper.Aeskey, _obj.body);
                PrepaidAPI para = JsonConvert.DeserializeObject<PrepaidAPI>(_dataRequestModel);
                WalletTransaction _objWalletTransaction = new WalletTransaction();
                _objWalletTransaction.Fk_MemId = para.FK_MemId;
                _objWalletTransaction.Amount = para.AMOUNT;
                _objWalletTransaction.Operator = para.Provider;
                _objWalletTransaction.Number = para.NUMBER;
                _objWalletTransaction.ActionType = _ActionType;//"Prepaid Recharge";
                _objWalletTransaction.Action = _Action;
                _objWalletTransaction.RechargeType = _RechargeType;// "PrepaidRecharge";
                WalletService _objwalletservice = new WalletService();
                WalletTransactionResponse objResTrans = _objwalletservice.WalletTransactionsService(_objWalletTransaction);
                return objResTrans;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public static List<T> GetGenericRechareAPIResponseWithDecryptList<T>(AddUpdateResponseResponseModel _obj)
        {
            try
            {
                string _date = ApiEncrypt_Decrypt.DecryptString(DBHelper.Aeskey, _obj.body);
                List<T> _objPrepaidResponse = JsonConvert.DeserializeObject<List<T>>(_date);
                return _objPrepaidResponse;
            }
            catch (Exception e)
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
        public static AddUpdateResponseResponseModel CustomErrorhandleForPreaidAndOtherServices(string _errormessage)
        {
            try
            {
                List<PrepaidResponse> _objlist = new List<PrepaidResponse>();
                PrepaidResponse _objmodel = new PrepaidResponse();
                AddUpdateResponseResponseModel _objResponseModel = new AddUpdateResponseResponseModel();
                _objmodel.ERROR = "Custom Error: " + _errormessage + " Please Contact To Admin.";
                _objmodel.Status = "0";
                _objlist.Add(_objmodel);
                string _sResult = JsonConvert.SerializeObject(_objlist);
                string _encryptedText = ApiEncrypt_Decrypt.EncryptString(DBHelper.Aeskey, _sResult);
                _objResponseModel.body = _encryptedText;
                return _objResponseModel;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public static AddUpdateResponseResponseModel CustomErrorhandleForPreaidAndOtherServices(string _errormessage, string _funtionName)
        {
            try
            {
                List<PrepaidResponse> _objlist = new List<PrepaidResponse>();
                PrepaidResponse _objmodel = new PrepaidResponse();
                AddUpdateResponseResponseModel _objResponseModel = new AddUpdateResponseResponseModel();
                _objmodel.ERROR = "Custom Error: " + _errormessage + " Please Contact To Admin. " + _funtionName;
                _objmodel.Status = "0";
                _objlist.Add(_objmodel);
                string _sResult = JsonConvert.SerializeObject(_objlist);
                string _encryptedText = ApiEncrypt_Decrypt.EncryptString(DBHelper.Aeskey, _sResult);
                _objResponseModel.body = _encryptedText;
                return _objResponseModel;

            }
            catch (Exception e)
            {
                throw;
            }
        }
        public static AddUpdateResponseResponseModel CustomErrorhandleForBroadBandAndOtherServices(string _errormessage)
        {
            try
            {
                List<MBroadBandProviderResponse> _objlist = new List<MBroadBandProviderResponse>();
                MBroadBandProviderResponse _objmodel = new MBroadBandProviderResponse();
                AddUpdateResponseResponseModel _objResponseModel = new AddUpdateResponseResponseModel();
                _objmodel.ERROR = "Custom Error: " + _errormessage + " Please Contact To Admin.";
                _objmodel.Status = "0";
                _objlist.Add(_objmodel);
                string _sResult = JsonConvert.SerializeObject(_objlist);
                string _encryptedText = ApiEncrypt_Decrypt.EncryptString(DBHelper.Aeskey, _sResult);
                _objResponseModel.body = _encryptedText;
                return _objResponseModel;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string DateString = "";
            // DateTime Dt;

            string[] DatePart = (InputDate).Split(new string[] { "-", @"/" }, StringSplitOptions.None);
            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];
                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Month + "/" + Day + "/" + Year;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }

            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }
        }

        public static string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "ZERO";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKHS ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            //if ((number / 10) > 0)
            //{
            //    words += ConvertNumbertoWords(number / 10) + " RUPEES ";
            //    number %= 10;
            //}
            if (number > 0)
            {
                //if (words != "") words += "AND ";
                var unitsMap = new[]
                {
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };
                var tensMap = new[]
                {
            "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            HttpContext.Current.Session["words"] = words;
            return words;
        }
        public static string GlobalDecrypText(string key)
        {
            return key = key.Replace(" ", "+");
        }

        public static string IncomePlanName(int PlanId)
        {
            string Planname = string.Empty;
            switch (PlanId)
            {
                case 1:
                    Planname = "Self Income";
                    break;
                case 2:
                    Planname = "Team Income";
                    break;
                case 3:
                    Planname = "Air Orbit Income";
                    break;
                case 4:
                    Planname = "Water Orbit Income";
                    break;
                case 5:
                    Planname = "Space Orbit Income";
                    break;
                case 6:
                    Planname = "Royalty Income";
                    break;
                case 7:
                    Planname = "Royalty Orbit Income";
                    break;
                case 8:
                    Planname = "Leadership Income";
                    break;
                case 9:
                    Planname = "Club Bonus";
                    break;
            }

            return Planname;
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        public static string SerializeObjectWithoutEncryption<T>(T _obj)
        {
            try
            {
                string _sResult = JsonConvert.SerializeObject(_obj);
                return _sResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    public class DALBindCommonDropdown
    {
        public static List<SelectListItem> BindDropdown(int _procId, long _dataValue)
        {
            List<SelectListItem> _objlist = new List<SelectListItem>();
            try
            {
                string _qury = "Proc_BindDropDown @ProcId=" + _procId + ",@Value='" + _dataValue + "'";
                _objlist = DBHelperDapper.DAGetDetailsInList<SelectListItem>(_qury);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _objlist;
        }
        public static List<SelectListItem> BindProductDropdown(int _procId, long _dataValue)
        {
            List<SelectListItem> _objlist = new List<SelectListItem>();
            try
            {
                string _qury = "BindAllProduct @ProcId=" + _procId + ",@Value='" + _dataValue + "'";
                _objlist = DBHelperDapper.DAGetDetailsInList<SelectListItem>(_qury);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _objlist;
        }

        public static List<SelectListItem> PackageBindDropdown(int _procId, long _dataValue)
        {
            List<SelectListItem> _objlist = new List<SelectListItem>();
            try
            {
                string _qury = "Proc_BindDropDown @ProcId=" + _procId + ",@Value='" + _dataValue + "'";
                _objlist = DBHelperDapper.DAGetDetailsInList<SelectListItem>(_qury);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _objlist;
        }


        public static List<SelectListItem> PackageList()
        {
            List<SelectListItem> _objlist = new List<SelectListItem>();
            try
            {
                string _qury = "BindPackageList";
                _objlist = DBHelperDapper.DAGetDetailsInList<SelectListItem>(_qury);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _objlist;
        }
        public static List<SelectListItem> BindFranchiseProductDropdown(int _procId, long _dataValue, long franchiseId)
        {
            List<SelectListItem> _objlist = new List<SelectListItem>();
            try
            {
                string _qury = "Proc_BindFranchiseProductDDl @ProcId=" + _procId + ",@Value='" + _dataValue + "',@FranchiseId=" + franchiseId + "";
                _objlist = DBHelperDapper.DAGetDetailsInList<SelectListItem>(_qury);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _objlist;
        }

        public static List<PaymentModeListViewModel> BindPaymentMode()
        {
            List<PaymentModeListViewModel> _objlist = new List<PaymentModeListViewModel> {
               new PaymentModeListViewModel{Value="0",Text="--Select--"},
                new PaymentModeListViewModel{Value="Cash",Text="Cash"},
                new PaymentModeListViewModel{Value="Cheque",Text="Cheque"},
                new PaymentModeListViewModel{Value="NEFT",Text="NEFT"},
                new PaymentModeListViewModel{Value="RTGS",Text="RTGS"},
                new PaymentModeListViewModel{Value="IMPS",Text="IMPS"},
                new PaymentModeListViewModel{Value="UPI",Text="UPI"},
            };
            return _objlist;
        }

        #region PayoutMaster 
        public static List<SelectListItem> BindPayoutMaster()
        {
            DataSet ds = DBHelper.ExecuteQuery("SelectPayoutMaster");
            List<SelectListItem> ListOfPayout = new List<SelectListItem>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ListOfPayout.Add(new SelectListItem { Value = "0", Text = "--Select Payout--" });
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListOfPayout.Add(new SelectListItem { Text = dr["PayoutValue"].ToString(), Value = dr["PayoutNo"].ToString() });
                }
            }
            return ListOfPayout;
        }

        public static List<SelectListItem> BindFinancealYear()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetFinancealYear");
            List<SelectListItem> ListOfFY = new List<SelectListItem>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ListOfFY.Add(new SelectListItem { Value = "", Text = "--Select Year--" });
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListOfFY.Add(new SelectListItem { Text = dr["EndYear"].ToString(), Value = dr["FY_ID"].ToString() });
                }
            }
            return ListOfFY;
        }

        public static List<SelectListItem> BindFinancealMonth()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetFinancealMonth");
            List<SelectListItem> ListOfFY = new List<SelectListItem>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ListOfFY.Add(new SelectListItem { Value = "", Text = "--Select Month--" });
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListOfFY.Add(new SelectListItem { Text = dr["FinancealMonth"].ToString(), Value = dr["FYM_ID"].ToString() });
                }
            }
            return ListOfFY;
        }
        public static List<SelectListItem> BindRepurchasePayoutMaster()
        {
            DataSet ds = DBHelper.ExecuteQuery("SelectRepurchasePayoutMaster");
            List<SelectListItem> ListOfPayout = new List<SelectListItem>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ListOfPayout.Add(new SelectListItem { Value = "0", Text = "--Select Payout--" });
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListOfPayout.Add(new SelectListItem { Text = dr["PayoutValue"].ToString(), Value = dr["PayoutNo"].ToString() });
                }
            }
            return ListOfPayout;
        }

        #endregion

        //public static List<SelectListItem> GetRepurchasePayoutMaster()
        //{
        //    DataSet ds = DBHelper.ExecuteQuery("SelectRepurchasePayoutMaster_ADP");
        //    List<SelectListItem> ListOfPayout = new List<SelectListItem>();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        ListOfPayout.Add(new SelectListItem { Value = "0", Text = "--Select Payout--" });
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            ListOfPayout.Add(new SelectListItem { Text = dr["PayoutValue"].ToString(), Value = dr["PayoutNo"].ToString() });
        //        }
        //    }
        //    return ListOfPayout;
        //}

        #region Bind Franchise       
        public static List<MOrder> BindFranchise()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetFranchise");
            List<MOrder> ListFranchise = new List<MOrder>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ListFranchise.Add(new MOrder { PKFranchiseRegistrationId = "0", LoginID = "--Select Franchisee--" });
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListFranchise.Add(new MOrder { LoginID = dr["LoginID"].ToString(), PKFranchiseRegistrationId = dr["PKFranchiseRegistrationId"].ToString() });
                }
            }


            return ListFranchise;
        }


        #endregion
        public static List<SelectListItem> BindRecognition()
        {
            DataSet ds = DBHelper.ExecuteQuery("BindRecognition");
            List<SelectListItem> ListRecognition = new List<SelectListItem>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ListRecognition.Add(new SelectListItem { Value = "0", Text = "--Select Recognition--" });
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListRecognition.Add(new SelectListItem { Text = dr["RecognitionName"].ToString(), Value = dr["PK_RcogId"].ToString() });
                }
            }
            return ListRecognition;
        }        
    }

    public class MCommonProperties
    {
        public long ReqId { get; set; }
        public long PrdId { get; set; }
    }

    public class MSimpleString
    {
        public int Code { get; set; }
        public int CheckBox { get; set; }
        public int flag { get; set; }
        public string Remark { get; set; }
        public string LoginId { get; set; }
        public int RequestId { get; set; }
        public string status { get; set; }
        public string Message { get; set; }
        public int Fk_MemId { get; set; }
        public int CustomerId { get; set; }
        public int PerformanceLevelID { get; set; }

    }
    public class CancelOrderResponse
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
    }

    public class MFileUpload
    {
        public string Msg { get; set; }
        public string Path { get; set; }
        public string Result { get; set; }
        public string FileName { get; internal set; }
    }

    public class MPaging
    {
        public int? Page { get; set; }
        public int Size { get; set; }
        public int TotalRecords { get; set; }
        public int TotalRecord { get; set; }
        public string SearchKey { get; set; }
        public string SearchValue { get; set; }
        public Pager Pager { get; set; }
        public int EndPage { get; private set; }


       

    }

    [Serializable]
    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize = 10)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }

    public class UrlEncodingDecoding
    {
        public static string Encode(string encodeMe)
        {
            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(encodeMe);
            return Convert.ToBase64String(encoded);
        }

        public static string Decode(string decodeMe)
        {
            byte[] encoded = Convert.FromBase64String(decodeMe);
            return System.Text.Encoding.UTF8.GetString(encoded);
        }
    }

    public class CryptoData
    {
        public static string Encryption(string Key, string Text)
        {
            string empty = string.Empty;
            try
            {
                return EncryptString(Key, Text);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string Decryption(string Key, string Text)
        {
            string empty = string.Empty;
            try
            {
                return DecryptString(Key, Text);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static string DecryptString(string key, string cipherText)
        {
            if (cipherText.Contains(" "))
            {
                cipherText = cipherText.Replace(" ", "+");
            }

            byte[] iV = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);
            Aes aes = Aes.Create();
            aes.KeySize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iV;
            ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
            MemoryStream stream = new MemoryStream(buffer);
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            StreamReader streamReader = new StreamReader(stream2);
            return streamReader.ReadToEnd();
        }

        private static string EncryptString(string key, string plainText)
        {
            byte[] iV = new byte[16];
            byte[] inArray;
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iV;
                ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream stream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new StreamWriter(stream))
                {
                    streamWriter.Write(plainText);
                }

                inArray = memoryStream.ToArray();
            }

            return Convert.ToBase64String(inArray);
        }
    }



}