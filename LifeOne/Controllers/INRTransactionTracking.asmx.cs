using LifeOne.Models.API;
using LifeOne.Models.API.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;


namespace LifeOne.Controllers
{
    /// <summary>
    /// Summary description for INRTransactionTracking
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class INRTransactionTracking : System.Web.Services.WebService
    {
        //calling API for getting the count details here
        [WebMethod]
        public string GetTransactionDetails()
        {
            try
            {
                string _result = "";

                int total = 0;
                int totalPerPage = 0; int loopcount = 0;

                DateTime currentDate = DateTime.ParseExact(DateTime.Now.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                string strDate = currentDate.AddDays(-60).ToString("yyyy-MM-dd");
                string EndDate = currentDate.ToString("yyyy-MM-dd");

                string apiUrl = "https://inrdeals.com/fetch/reports?token=ksg29g53m239vq3n&id=sbv65458051&startdate=" + strDate + "&enddate=" + EndDate + "&page=" + 1;
                WebRequest request = HttpWebRequest.Create(apiUrl);
                using (WebResponse response = request.GetResponse())
                {
                    if (response != null)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            string jsonText = reader.ReadToEnd();
                            XmlDocument doc = JsonConvert.DeserializeXmlNode(jsonText);
                            string xmldata = doc.InnerXml;
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.LoadXml(xmldata);

                            XmlNodeList xnList = xmldoc.SelectNodes("result");
                            foreach (XmlNode xn in xnList)
                            {
                                total = int.Parse(xn["total"].InnerText);
                                totalPerPage = int.Parse(xn["per_page"].InnerText);
                                loopcount = int.Parse(xn["last_page"].InnerText);
                            }
                            for (int _pageNo = 1; _pageNo <= loopcount; _pageNo++)
                            {
                                /*saved details*/
                                SaveTransactionDetails(strDate, EndDate, _pageNo);
                            }
                        }
                    }
                }
                return _result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //getting details from API and create own datatable so that we can separately save information on the databse ..
        private void SaveTransactionDetails(string strDate, string EndDate, int _pageNo)
        {
            string apiUrl = "https://inrdeals.com/fetch/reports?token=ksg29g53m239vq3n&id=sbv65458051&startdate=" + strDate + "&enddate=" + EndDate + "&page=" + _pageNo;
            WebRequest request = HttpWebRequest.Create(apiUrl);

            int _count = 0;

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    if (response != null)
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            string jsonText = reader.ReadToEnd();
                            XmlDocument doc = JsonConvert.DeserializeXmlNode(jsonText);
                            string xmldata = doc.InnerXml;
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.LoadXml(xmldata);
                            XmlNodeList nodeList = xmldoc.SelectNodes("result/data");

                            string Short_Fall = string.Empty;
                            DataTable dtvalues = new DataTable();
                            dtvalues.Columns.AddRange(new DataColumn[10] {
                                new DataColumn("CustomSubId", typeof(string)),
                                new DataColumn("sub_id1", typeof(string)),
                                new DataColumn("transaction_id", typeof(string)),
                                new DataColumn("sale_amount",typeof(string)),
                                new DataColumn("status", typeof(string)),
                                new DataColumn("store_name", typeof(string)),
                                new DataColumn("sale_date",typeof(string)),
                                new DataColumn("click_id", typeof(string)),
                                new DataColumn("user_commission", typeof(string)),
                                new DataColumn("referrer_url",typeof(string))
                            });

                            foreach (XmlNode xmlNode in nodeList)
                            {
                                string customsubid = "";
                                if (xmlNode["sub_id1"].InnerText != "")
                                    customsubid = xmlNode["sub_id1"].InnerText.Substring(0, 2);
                                else
                                    customsubid = "";

                                string sub_id1 = xmlNode["sub_id1"].InnerText;
                                string transaction_id = xmlNode["transaction_id"].InnerText;
                                string sale_amount = xmlNode["sale_amount"].InnerText;
                                string status = xmlNode["status"].InnerText;
                                string store_name = xmlNode["store_name"].InnerText;
                                string sale_date = xmlNode["sale_date"].InnerText;
                                string click_id = xmlNode["click_id"].InnerText;
                                string user_commission = xmlNode["user_commission"].InnerText;
                                string referrer_url = xmlNode["referrer_url"].InnerText;

                                dtvalues.Rows.Add(customsubid, sub_id1, transaction_id, sale_amount, status, store_name, sale_date, click_id, user_commission, referrer_url);
                                _count++;
                            }

                            //save details as per company
                            SaveCompanyWiseTransactionDetails(dtvalues);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                int excount = _count;
                throw ex;
            }
        }
        private static string Serialize<T>(T dataToSerialize)
        {
            try
            {
                var stringwriter = new System.IO.StringWriter();
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringwriter, dataToSerialize);
                return stringwriter.ToString();
            }
            catch
            {
                throw;
            }
        }
        private string CreateXml(DataRow[] _foundRows)
        {
            List<DataINRTransaction> _objlist = new List<DataINRTransaction>();
            Result _objResult = new Result();
            foreach (DataRow item in _foundRows)
            {
                DataINRTransaction _data = new DataINRTransaction();
                _data.Transaction_id = item["transaction_id"].ToString();
                _data.Sale_amount = item["sale_amount"].ToString();
                _data.Status = item["status"].ToString();
                _data.Store_name = item["store_name"].ToString();
                _data.Sale_date = item["sale_date"].ToString();
                _data.Click_id = item["click_id"].ToString();
                _data.User_commission = item["user_commission"].ToString();
                _data.Referrer_url = item["referrer_url"].ToString();
                _data.Sub_id1 = item["sub_id1"].ToString();
                _objlist.Add(_data);
            }
            _objResult.Data = _objlist;
            string _xmlInnerXml = Serialize<Result>(_objResult);
            return _xmlInnerXml;
        }

        List<MCompanyDetails> _objlist = null;
        private void SaveCompanyWiseTransactionDetails(DataTable _dtcompanywiseCommison)
        {
            try
            {
                CustomerDb _objCustomerDb = new CustomerDb();
                if (_objlist == null)
                {    // if null thn it we will get the data from database
                    _objlist = _objCustomerDb.GetCompanyListFromDB();
                }
                foreach (var item in _objlist)
                {
                    string _companySUbCode = item.CompnaySubCode;
                    string _connectionString = item.ConnectionString;
                    DataRow[] _foundRows = _dtcompanywiseCommison.Select("CustomSubId = '" + _companySUbCode + "'");
                    if (_foundRows.Length > 0)
                    {
                        string INRXMLData = CreateXml(_foundRows);
                        SqlParameter[] para ={
                         new SqlParameter("@INRXMLData",INRXMLData)
                       };

                        int i = ExecuteNonQuery("InsertINRDealsTransation", para, _connectionString);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private int ExecuteNonQuery(string commandText, SqlParameter[] commandParameters, string connectionstring)
        {
            int k = 0;
            string _decruptConnection = AesEncryptDecrypt.DecryptString("Utility@#@132XYZ", connectionstring);
            using (var connection = new SqlConnection(_decruptConnection))
            using (var command = new SqlCommand(commandText, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(commandParameters);
                connection.Open();
                k = command.ExecuteNonQuery();
            }
            return k;
        }

    }

  
    [XmlRoot(ElementName = "data")]
    public class DataINRTransaction
    {
        [XmlElement(ElementName = "transaction_id")]
        public string Transaction_id { get; set; }
        [XmlElement(ElementName = "sale_amount")]
        public string Sale_amount { get; set; }
        [XmlElement(ElementName = "status")]
        public string Status { get; set; }
        [XmlElement(ElementName = "store_name")]
        public string Store_name { get; set; }
        [XmlElement(ElementName = "sale_date")]
        public string Sale_date { get; set; }
        [XmlElement(ElementName = "click_id")]
        public string Click_id { get; set; }
        [XmlElement(ElementName = "user_commission")]
        public string User_commission { get; set; }
        [XmlElement(ElementName = "sub_id1")]
        public string Sub_id1 { get; set; }
        [XmlElement(ElementName = "referrer_url")]
        public string Referrer_url { get; set; }



    }

    [XmlRoot(ElementName = "result")]
    public class Result
    {
        [XmlElement(ElementName = "current_page")]
        public string Current_page { get; set; }
        [XmlElement(ElementName = "data")]
        public List<DataINRTransaction> Data { get; set; }
        [XmlElement(ElementName = "first_page_url")]
        public string First_page_url { get; set; }
        [XmlElement(ElementName = "from")]
        public string From { get; set; }
        [XmlElement(ElementName = "last_page")]
        public string Last_page { get; set; }
        [XmlElement(ElementName = "last_page_url")]
        public string Last_page_url { get; set; }
        [XmlElement(ElementName = "next_page_url")]
        public string Next_page_url { get; set; }
        [XmlElement(ElementName = "path")]
        public string Path { get; set; }
        [XmlElement(ElementName = "per_page")]
        public string Per_page { get; set; }
        [XmlElement(ElementName = "prev_page_url")]
        public string Prev_page_url { get; set; }
        [XmlElement(ElementName = "to")]
        public string To { get; set; }
        [XmlElement(ElementName = "total")]
        public string Total { get; set; }
    }
}
