using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Xml;
namespace LifeOne.Models.API
{
    /// <summary>
    /// Summary description for INRDealsTransaction
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
   
    public class INRDealsTransaction : System.Web.Services.WebService
    {

        static int Page = 1;

        public void SetPage(int _Page)
        {
            INRDealsTransaction.Page = _Page;
        }
        [WebMethod]
        public string GetINRDetails()
        {
            DateTime currentDate = DateTime.ParseExact(DateTime.Now.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            string strDate = currentDate.AddMonths(-2).ToString("yyyy-MM-dd");
            string EndDate = currentDate.ToString("yyyy-MM-dd");
            int TotalpageNo;

            string apiUrl = "https://inrdeals.com/fetch/reports?token=ksg29g53m239vq3n&id=sbv65458051&startdate=" + strDate + "&enddate=" + EndDate + "&page=" + Page;
            WebRequest request = HttpWebRequest.Create(apiUrl);
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
                            if (xmldoc.SelectSingleNode("//data") != null)
                            {
                                int total = 0;
                                int totalPerPage = 0;
                                XmlNodeList xnList = xmldoc.SelectNodes("result");
                                foreach (XmlNode xn in xnList)
                                {

                                    total = int.Parse(xn["total"].InnerText);
                                    totalPerPage = int.Parse(xn["per_page"].InnerText);
                                }
                                int p1 = total % totalPerPage;
                                int p2 = total / totalPerPage;
                                if (p2 == 0)
                                {
                                    TotalpageNo = 1;

                                }
                                else
                                {
                                    if (p2 != 0 && p1 != 0)
                                    {
                                        p2++;
                                        TotalpageNo = p2;
                                    }
                                    else
                                    {
                                        TotalpageNo = p2;
                                    }
                                }
                                string INRXMLData = doc.InnerXml;
                                try
                                {
                                    SqlParameter[] para ={
                                    new SqlParameter("@INRXMLData",INRXMLData)
                                };

                                LifeOne.Models.TravelModel.DBHelper objDb = new LifeOne.Models.TravelModel.DBHelper();
                                int i = objDb.ExecuteNonQuery("InsertINRDealsTransation", para);

                                    if (i > 0)
                                    {
                                        if (TotalpageNo > Page)
                                        {
                                            Page++;
                                            SetPage(Page);
                                            GetINRDetails();
                                        }
                                        else
                                        {
                                            SetPage(1);
                                        }
                                        return "success_1";
                                    }
                                    else
                                    {
                                        if (TotalpageNo > Page)
                                        {
                                            Page++;
                                            SetPage(Page);
                                            GetINRDetails();
                                        }
                                        else
                                        {
                                            SetPage(1);
                                        }
                                        return "record already inserted_1";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ex.Message + "_0";
                                }
                            }
                            else
                            {
                                return "No Record Found_1";
                            }
                        }
                    }
                    else
                    {
                        return "Invalid Request_1";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message + "_0";
            }
        }
    }
}
