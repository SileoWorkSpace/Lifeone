using AesEncryption;
using SGCareWeb.Models.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Http;
using System.Xml;
using SGCareWeb.Models.TravelModel;
using System.Data;
using SGCareWeb.Models.API.DAL;
using SGCareWeb.Models.QUtility.Entity;
using Newtonsoft.Json.Linq;


namespace SGCareWeb.Controllers
{
    public class TravelAPIController : ApiController
    {
        string Aeskey = ConfigurationManager.AppSettings["AeskeyUtility"].ToString();

        #region Bus API

        #region Get Origin API
        [HttpPost]
        public ResponseModel GetOrigin(RequestModel model)
        {
            ResponseModel returnResponse = new ResponseModel();
            BusModel.GetOriginResponse datalist = new BusModel.GetOriginResponse();
            string EncryptedText = "";
            try
            {
                string soapResult = "";
                HttpWebRequest request = GetOriginDetails();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                BusModel.GetOriginRequest obj = new BusModel.GetOriginRequest();
                string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<BusModel.GetOriginRequest>(dcdata);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();

                    }
                    datalist = JsonConvert.DeserializeObject<BusModel.GetOriginResponse>(soapResult);
                }
            }
            catch (Exception ex)
            {

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusModel.GetOriginResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = Crypto.Encryption(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;

        }

        public static HttpWebRequest GetOriginDetails()
        {
            var url = Models.TravelModel.Common.BusAPIDetails.APIURL;
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url + "/GetOrigin");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }
        #endregion

        #region Get Destination API
        [HttpPost]
        public ResponseModel GetDestination(RequestModel model)
        {
            BusModel.DestinationRequest obj = new BusModel.DestinationRequest();
            ResponseModel returnResponse = new ResponseModel();
            BusModel.GetDestinationResponse datalist = null;
            string EncryptedText = "";
            try
            {
                string soapResult = "";
                HttpWebRequest request = GetDestinationDetails();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<BusModel.DestinationRequest>(dcdata);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);




                AddAuthJsonAndHitTheAPI(json);

                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();

                    }
                    datalist = JsonConvert.DeserializeObject<BusModel.GetDestinationResponse>(soapResult);
                }
            }
            catch (Exception ex)
            {
                datalist.ResponseStatus = ex.Message;
                datalist.FailureResponse = "1";

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusModel.GetDestinationResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        public static HttpWebRequest GetDestinationDetails()
        {
            var url = Models.TravelModel.Common.BusAPIDetails.APIURL;
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url + "/GetDestination");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        public string AddAuthJsonAndHitTheAPI(string _jsonRequest)
        {
            try
            {

                Authentication Authentication = new Authentication();
                Authentication.LoginId = "RIGHT1234";
                Authentication.Password = "apiRIGHT1234";
                Models.TravelModel.Common.AgentCreditBalance root = new Models.TravelModel.Common.AgentCreditBalance();
                root.Authentication = Authentication;
                string _commonJson = Newtonsoft.Json.JsonConvert.SerializeObject(root);

                var arrayOfObjects = JsonConvert.SerializeObject(
                    new[] { JsonConvert.DeserializeObject(_commonJson.ToString()), JsonConvert.DeserializeObject(_jsonRequest.ToString())
                    }
                );

                //alpha = @"[{'a'='b'},{'a'='b'}]";
                JArray arrayObject = JArray.Parse(arrayOfObjects);
                foreach (var item in arrayObject)
                {
                    //obj["object"].Last.AddAfterSelf(item);
                    //string _JosnAuth = item.ToString().Replace("{{","");
                    //string _JosnAuthNew = _JosnAuth.ToString().Replace("}}", "");
                }
                JArray textArray = JArray.Parse(arrayOfObjects);
                //dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(arrayOfObjects);
                //Newtonsoft.Json.JObject jObject = JObject.Parse(json);
                return arrayOfObjects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Get Search API
        [HttpPost]
        public ResponseModel GetSearch(RequestModel model)
        {
            BusModel.GetSearchRequest obj = new BusModel.GetSearchRequest();
            BusModel.GetSearchResponse datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string soapResult = "";
                HttpWebRequest request = GetSearchDetails();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<BusModel.GetSearchRequest>(dcdata);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();

                    }
                    datalist = JsonConvert.DeserializeObject<BusModel.GetSearchResponse>(soapResult);
                }

            }
            catch (Exception ex)
            {
                datalist.ResponseStatus = ex.Message;
                datalist.FailureRemarks = "1";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusModel.GetSearchResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        public static HttpWebRequest GetSearchDetails()
        {
            var url = Models.TravelModel.Common.BusAPIDetails.APIURL;
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url + "/GetSearch");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }
        #endregion

        #region Get Seat Map
        [HttpPost]
        public ResponseModel GetSeatMap(RequestModel model)
        {
            BusModel.GetSeatMapRequest obj = new BusModel.GetSeatMapRequest();
            BusModel.GetSeatMapResponse datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                Models.TravelModel.Common objcomm = new Models.TravelModel.Common();

                string soapResult = "";
                HttpWebRequest request = GetSeatMapDetails();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<BusModel.GetSeatMapRequest>(dcdata);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();

                    }

                    datalist = JsonConvert.DeserializeObject<BusModel.GetSeatMapResponse>(soapResult);
                }
            }
            catch (Exception ex)
            {
                datalist.ResponseStatus = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusModel.GetSeatMapResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        public static HttpWebRequest GetSeatMapDetails()
        {
            var url = Models.TravelModel.Common.BusAPIDetails.APIURL;
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url + "/GetSeatMap");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        #endregion

        #region Get Book
        [HttpPost]
        public ResponseModel GetBook(RequestModel model)
        {
            BusModel.GetBookRequest obj = new BusModel.GetBookRequest();
            BusModel.GetBookingResponse datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                Models.TravelModel.Common objcomm = new Models.TravelModel.Common();
                string bookingsegments = null;
                APIResponse1 objresponse = new APIResponse1();
                Random rnd = new Random();
                string BookingId = Convert.ToString(rnd.Next(1, 100000000));

                string soapResult = "";
                HttpWebRequest request = GetBookDetails();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<BusModel.GetBookRequest>(dcdata);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();

                    }

                    datalist = JsonConvert.DeserializeObject<BusModel.GetBookingResponse>(soapResult);
                }
                if (datalist.ResponseStatus == "1")
                {
                    SaveBusBookingDetail objsave = new SaveBusBookingDetail();
                    objsave.ResponseStatus = datalist.ResponseStatus;
                    objsave.UsertrackId = datalist.UserTrackId;
                    objsave.FK_MemID = Convert.ToInt16(obj.FK_MemID);
                    objsave.Title = obj.BookingInput.BookingCustomerDetails.Title;
                    objsave.Name = obj.BookingInput.BookingCustomerDetails.Name;
                    objsave.Address = obj.BookingInput.BookingCustomerDetails.Address;
                    objsave.CustomerContactNumber = obj.BookingInput.BookingCustomerDetails.ContactNumber;
                    objsave.ContactNumber = obj.BookingInput.BookingCustomerDetails.ContactNumber;
                    objsave.City = obj.BookingInput.BookingCustomerDetails.City;
                    objsave.CountryId = obj.BookingInput.BookingCustomerDetails.CountryId;
                    objsave.CustomerEmailId = obj.BookingInput.BookingCustomerDetails.EmailId;
                    objsave.EmailId = obj.BookingInput.BookingCustomerDetails.EmailId;
                    objsave.IdProofId = obj.BookingInput.BookingCustomerDetails.IdProofId;
                    objsave.IdProofNumber = obj.BookingInput.BookingCustomerDetails.IdProofNumber;
                    objsave.TotalTickets = obj.BookingInput.BookingDetails.TotalTickets;
                    objsave.TotalAmount = obj.BookingInput.BookingDetails.TotalAmount;
                    objsave.TransportId = obj.BookingInput.BookingDetails.TransportId;
                    objsave.ScheduleId = obj.BookingInput.BookingDetails.ScheduleId;
                    objsave.StationId = obj.BookingInput.BookingDetails.StationId;
                    objsave.TravelDate = obj.BookingInput.BookingDetails.TravelDate;

                    objsave.ServiceTax = datalist.BookingOutput.TicketDetails.ServiceTax;
                    objsave.ConvenienceFee = datalist.BookingOutput.TicketDetails.ConvenienceFee;
                    objsave.Commission = datalist.BookingOutput.TicketDetails.Commission;
                    objsave.CancellationPolicy = datalist.BookingOutput.TicketDetails.CancellationPolicy;
                    objsave.TransactionId = datalist.BookingOutput.TicketDetails.PNRDetails.TransactionId;
                    objsave.TransportPNR = datalist.BookingOutput.TicketDetails.PNRDetails.TransportPNR;
                    objsave.TotalAmount = datalist.BookingOutput.TicketDetails.PNRDetails.TotalAmount;
                    objsave.TransportId = obj.BookingInput.BookingDetails.TransportId;
                    objsave.TotalTickets = datalist.BookingOutput.TicketDetails.PNRDetails.TotalTickets;
                    objsave.BusName = datalist.BookingOutput.TicketDetails.PNRDetails.BusName;
                    objsave.Origin = datalist.BookingOutput.TicketDetails.PNRDetails.Origin;
                    objsave.Destination = datalist.BookingOutput.TicketDetails.PNRDetails.Destination;
                    objsave.TravelDate = datalist.BookingOutput.TicketDetails.PNRDetails.TravelDate;
                    objsave.DepartureTime = datalist.BookingOutput.TicketDetails.PNRDetails.DepartureTime;
                    objsave.TransportName = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.TransportName;
                    objsave.Address1 = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.Address1;
                    objsave.Address2 = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.Address2;
                    objsave.Address3 = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.Address3;
                    objsave.CityNamePinCode = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.CityNamePinCode;
                    objsave.ContactNumber = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.ContactNumber;
                    objsave.Fax = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.Fax;
                    objsave.EmailId = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.EmailId;
                    objsave.Website = datalist.BookingOutput.TicketDetails.PNRDetails.TransportDetails.Website;

                    DataTable dtPaxdetails = new DataTable();
                    dtPaxdetails.Columns.Add("TicketNumber");
                    dtPaxdetails.Columns.Add("SeatNo");
                    dtPaxdetails.Columns.Add("SeatType");
                    dtPaxdetails.Columns.Add("PassengerName");
                    dtPaxdetails.Columns.Add("Gender");
                    dtPaxdetails.Columns.Add("Age");
                    dtPaxdetails.Columns.Add("Fare");
                    dtPaxdetails.Columns.Add("BoardingId");
                    dtPaxdetails.Columns.Add("Place");
                    dtPaxdetails.Columns.Add("Time");
                    dtPaxdetails.Columns.Add("Address");
                    dtPaxdetails.Columns.Add("LandMark");
                    dtPaxdetails.Columns.Add("ContactNumber");
                    dtPaxdetails.Columns.Add("DroppingId");
                    dtPaxdetails.Columns.Add("DroppingPlace");
                    dtPaxdetails.Columns.Add("DroppingTime");
                    dtPaxdetails.Columns.Add("DroppingAddress");
                    dtPaxdetails.Columns.Add("DroppingLandmark");
                    dtPaxdetails.Columns.Add("DroppingContactNumber");
                    dtPaxdetails.Columns.Add("ReportingTime");
                    dtPaxdetails.Columns.Add("ServiceCharges");
                    for (int i = 0; i < datalist.BookingOutput.TicketDetails.PNRDetails.PaxList.Count; i++)
                    {


                        dtPaxdetails.Rows.Add
                        (

                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].TicketNumber,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].SeatNo,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].SeatType,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].PassengerName,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].Gender,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].Age,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].Fare,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.BoardingId,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.Place,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.Time,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.Address,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.LandMark,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.ContactNumber,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].DroppingPoints.DroppingId,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].DroppingPoints.Place,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].DroppingPoints.Time,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].DroppingPoints.Address,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].DroppingPoints.LandMark,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].DroppingPoints.ContactNumber,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].ReportingTime,
                        datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].ServiceCharges
                        );


                    }
                    objsave.PaxList = dtPaxdetails;
                    objsave.CompanyId = Models.TravelModel.Common.BusAPIDetails.CompanyId;
                    objsave.TransType = Models.TravelModel.Common.BusAPIDetails.TransType;
                    objsave.BookingId = BookingId;
                    objsave.Status = "SUCCESS";
                    objsave.BookedDate = DateTime.Now.ToString();

                    DataSet ds = objsave.SaveBusBookDetails();

                    string TicketNumber = ""; string SeatNo = ""; string SeatType = ""; string PassengerName = ""; string Gender = ""; string Age = ""; string Fare = ""; string Place = ""; string Time = ""; string Address = ""; string ReportingTime = ""; string Telephone = ""; string StatusFor = "Live"; string Boarding = "";

                    string TransportPNR = objsave.TransportPNR;
                    string BookedDate = objsave.BookedDate;
                    string TransportName = objsave.TransportName;

                    string Address1 = objsave.Address1;
                    string Address2 = objsave.Address2;
                    string Address3 = objsave.Address3;
                    string CityNamePinCode = objsave.CityNamePinCode;
                    string ContactNumber = objsave.ContactNumber;
                    string EmailId = obj.BookingInput.BookingCustomerDetails.EmailId;

                    string Website = objsave.Website;
                    string BusName = objsave.BusName;
                    string TravelDate = objsave.TravelDate;
                    string DepartureTime = objsave.DepartureTime;
                    string Origin = objsave.Origin;
                    string Destination = objsave.Destination;
                    string CancellationPolicy = objsave.CancellationPolicy;
                    string TotalAmount = objsave.TotalAmount;
                    for (int i = 0; i < datalist.BookingOutput.TicketDetails.PNRDetails.PaxList.Count; i++)
                    {


                        dtPaxdetails.Rows.Add
                            (

                            TicketNumber += datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].TicketNumber + "<br />",
                            SeatNo += datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].SeatNo + "<br />",
                            PassengerName += datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].PassengerName + "<br />",
                            Gender += datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].Gender + "<br />",
                            Age += datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].Age + "<br />",
                            Fare += datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].Fare + "<br />",
                            Place += datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.Place + "<br />",
                            Time += datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.Time + "<br />",
                            Boarding = datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.Place,
                            Telephone = datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.ContactNumber,
                            ReportingTime = datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].ReportingTime,
                            Address = datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].BoardingPoints.Address,
                            SeatType = datalist.BookingOutput.TicketDetails.PNRDetails.PaxList[i].SeatType
                            );


                    }

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        //commen it becuase currently we dont have book.html  -  17/11/2020
                        //string result = Models.TravelModel.Common.SendOTPMail(TransportPNR, BookedDate, TransportName, Address1, Address2, Address3, CityNamePinCode, ContactNumber, EmailId, Website, BusName, TravelDate, DepartureTime, Origin, Destination, TicketNumber, SeatNo, SeatType, PassengerName, Gender, Age, Fare, Place, Time, Address, Telephone, ReportingTime, StatusFor, CancellationPolicy, Boarding, TotalAmount);
                    }
                }
                else
                {
                    SaveBusBookingDetail objsave = new SaveBusBookingDetail();
                    objsave.ResponseStatus = datalist.ResponseStatus;
                    objsave.UsertrackId = obj.UserTrackId;
                    objsave.FK_MemID = Convert.ToInt16(obj.FK_MemID);
                    objsave.Title = obj.BookingInput.BookingCustomerDetails.Title;
                    objsave.Name = obj.BookingInput.BookingCustomerDetails.Name;
                    objsave.Address = obj.BookingInput.BookingCustomerDetails.Address;
                    objsave.ContactNumber = obj.BookingInput.BookingCustomerDetails.ContactNumber;
                    objsave.City = obj.BookingInput.BookingCustomerDetails.City;
                    objsave.CountryId = obj.BookingInput.BookingCustomerDetails.CountryId;
                    objsave.EmailId = obj.BookingInput.BookingCustomerDetails.EmailId;
                    objsave.IdProofId = obj.BookingInput.BookingCustomerDetails.IdProofId;
                    objsave.IdProofNumber = obj.BookingInput.BookingCustomerDetails.IdProofNumber;
                    objsave.TotalTickets = obj.BookingInput.BookingDetails.TotalTickets;
                    objsave.TotalAmount = obj.BookingInput.BookingDetails.TotalAmount;
                    objsave.TransportId = obj.BookingInput.BookingDetails.TransportId;
                    objsave.ScheduleId = obj.BookingInput.BookingDetails.ScheduleId;
                    objsave.StationId = obj.BookingInput.BookingDetails.StationId;
                    objsave.TravelDate = obj.BookingInput.BookingDetails.TravelDate;
                    objsave.CompanyId = Models.TravelModel.Common.BusAPIDetails.CompanyId;
                    objsave.TransType = Models.TravelModel.Common.BusAPIDetails.TransType;
                    objsave.BookingId = BookingId;
                    objsave.Status = "FAILED";
                    objsave.BookedDate = DateTime.Now.ToString();

                    DataSet ds = objsave.SaveBusBookDetails();
                }


            }
            catch (Exception ex)
            {
                datalist.ResponseStatus = ex.Message;
                datalist.FailureRemarks = "1";
            }


            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusModel.GetBookingResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        public static HttpWebRequest GetBookDetails()
        {
            var url = Models.TravelModel.Common.BusAPIDetails.APIURL;
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url + "/GetBook");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        #endregion

        #region Get Transaction Status
        [HttpPost]
        public ResponseModel GetBusTransactionStatus(RequestModel model)
        {
            BusModel.GetTransactionStatusRequest obj = new BusModel.GetTransactionStatusRequest();
            BusModel.GetTransactionStatusResponse datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                Models.TravelModel.Common objcomm = new Models.TravelModel.Common();
                string soapResult = "";
                HttpWebRequest request = GetTransactionStatusDetails();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<BusModel.GetTransactionStatusRequest>(dcdata);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();

                    }

                    datalist = JsonConvert.DeserializeObject<BusModel.GetTransactionStatusResponse>(soapResult);
                }
            }
            catch (Exception ex)
            {
                datalist.ResponseStatus = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusModel.GetTransactionStatusResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        public static HttpWebRequest GetTransactionStatusDetails()
        {
            var url = Models.TravelModel.Common.BusAPIDetails.APIURL;
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url + "/GetTransactionStatus");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;

        }

        #endregion

        #region GetReprint
        [HttpPost]
        public ResponseModel GetReprint(RequestModel model)
        {
            BusModel.GetReprintRequest obj = new BusModel.GetReprintRequest();
            BusModel.GetReprintResponse datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                Models.TravelModel.Common objcomm = new Models.TravelModel.Common();

                string soapResult = "";
                HttpWebRequest request = GetReprintDetails();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<BusModel.GetReprintRequest>(dcdata);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();

                    }

                    datalist = JsonConvert.DeserializeObject<BusModel.GetReprintResponse>(soapResult);
                }


            }
            catch (Exception ex)
            {
                datalist.ResponseStatus = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusModel.GetReprintResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        public static HttpWebRequest GetReprintDetails()
        {
            var url = Models.TravelModel.Common.BusAPIDetails.APIURL;
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url + "/GetReprint");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        #endregion

        #region GetCancellationPolicy 
        [HttpPost]
        public ResponseModel GetCancellationPolicy(RequestModel model)
        {
            BusModel.GetCancellationPolicyRequest obj = new BusModel.GetCancellationPolicyRequest();
            BusModel.GetCancellationPolicyResponse datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                Models.TravelModel.Common objcomm = new Models.TravelModel.Common();

                string soapResult = "";
                HttpWebRequest request = GetCancellationPolicyDetails();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<BusModel.GetCancellationPolicyRequest>(dcdata);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();

                    }
                    datalist = JsonConvert.DeserializeObject<BusModel.GetCancellationPolicyResponse>(soapResult);
                }

            }
            catch (Exception ex)
            {
                datalist.ResponseStatus = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusModel.GetCancellationPolicyResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        public static HttpWebRequest GetCancellationPolicyDetails()
        {
            var url = Models.TravelModel.Common.BusAPIDetails.APIURL;
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url + "/GetCancellationPolicy ");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        #endregion

        #region GetCancellationPenalty 
        [HttpPost]
        public ResponseModel GetCancellationPenalty(RequestModel model)
        {
            BusModel.GetCancellationPenaltyRequest obj = new BusModel.GetCancellationPenaltyRequest();
            BusModel.GetCancellationPenaltyResponse datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                Models.TravelModel.Common objcomm = new Models.TravelModel.Common();

                string soapResult = "";
                HttpWebRequest request = GetCancellationPenaltyDetails();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<BusModel.GetCancellationPenaltyRequest>(dcdata);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();

                    }
                    datalist = JsonConvert.DeserializeObject<BusModel.GetCancellationPenaltyResponse>(soapResult);
                }

            }
            catch (Exception ex)
            {
                datalist.ResponseStatus = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusModel.GetCancellationPenaltyResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        public static HttpWebRequest GetCancellationPenaltyDetails()
        {
            try
            {
                var url = Models.TravelModel.Common.BusAPIDetails.APIURL;
                HttpWebRequest webRequest =
                (HttpWebRequest)WebRequest.Create(@"" + url + "/GetCancellationPenalty");
                webRequest.ContentType = "application/json";
                webRequest.Method = "POST";
                return webRequest;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Get Cancellation
        [HttpPost]
        public ResponseModel GetCancellation(RequestModel model)
        {
            BusModel.GetCancellationRequest obj = new BusModel.GetCancellationRequest();
            BusModel.GetCancellationResponse datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {

                Models.TravelModel.Common objcomm = new Models.TravelModel.Common();

                string soapResult = "";
                Random rnd = new Random();
                string BookingId = Convert.ToString(rnd.Next(1, 100000000));
                HttpWebRequest request = GetCancellationDetails();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<BusModel.GetCancellationRequest>(dcdata);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();

                    }
                    // datalist = JsonConvert.DeserializeObject<GetSeatMapResponse>(soapResult);

                    datalist = JsonConvert.DeserializeObject<BusModel.GetCancellationResponse>(soapResult);
                }
                if (datalist.ResponseStatus == "1")
                {
                    CancelBookingSeat objsave = new CancelBookingSeat();
                    objsave.ResponseStatus = datalist.ResponseStatus;
                    objsave.FK_MemID = obj.FK_MemID;
                    objsave.Title = datalist.CancellationOutput.CanceledTicketDetails.BookingCustomerDetails.Title;
                    objsave.Name = datalist.CancellationOutput.CanceledTicketDetails.BookingCustomerDetails.Name;
                    objsave.Address = datalist.CancellationOutput.CanceledTicketDetails.BookingCustomerDetails.Address;
                    objsave.CustomerContactNumber = datalist.CancellationOutput.CanceledTicketDetails.BookingCustomerDetails.ContactNumber;
                    objsave.ContactNumber = datalist.CancellationOutput.CanceledTicketDetails.BookingCustomerDetails.ContactNumber;
                    objsave.City = datalist.CancellationOutput.CanceledTicketDetails.BookingCustomerDetails.City;
                    objsave.CountryId = datalist.CancellationOutput.CanceledTicketDetails.BookingCustomerDetails.CountryId;
                    objsave.CustomerEmailId = datalist.CancellationOutput.CanceledTicketDetails.BookingCustomerDetails.EmailId;
                    objsave.EmailId = datalist.CancellationOutput.CanceledTicketDetails.BookingCustomerDetails.EmailId;
                    objsave.IdProofId = datalist.CancellationOutput.CanceledTicketDetails.BookingCustomerDetails.IdProofId;
                    objsave.IdProofNumber = datalist.CancellationOutput.CanceledTicketDetails.BookingCustomerDetails.IdProofNumber;
                    objsave.TotalTickets = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.TotalTickets;
                    objsave.TotalAmount = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.TotalAmount;
                    objsave.TransportId = obj.CancellationInput.TransportId;
                    objsave.TravelDate = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.TravelDate;
                    objsave.TransactionId = obj.CancellationInput.TransactionId;
                    objsave.PenaltyAmount = obj.CancellationInput.PenaltyAmount;
                    objsave.TicketNumbers = obj.CancellationInput.TicketNumbers;
                    objsave.TransportId = obj.CancellationInput.TransportId;
                    objsave.TotalTicketsToCancel = obj.CancellationInput.TotalTicketsToCancel;

                    objsave.TransportPNR = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.TransportPNR;
                    objsave.TotalAmount = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.TotalAmount;
                    objsave.CancellationPenalty = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancellationPenalty;
                    objsave.CancellationNUmber = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancellationNumber;
                    objsave.CancellationDateTime = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelledDateTime;

                    objsave.BusName = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.BusName;
                    objsave.Origin = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.Origin;
                    objsave.Destination = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.Destination;
                    objsave.TravelDate = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.TravelDate;
                    objsave.DepartureTime = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.DepartureTime;
                    objsave.TransportName = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.TransportDetails.TransportName;
                    objsave.Address1 = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.TransportDetails.Address1;
                    objsave.Address2 = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.TransportDetails.Address2;
                    objsave.Address3 = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.TransportDetails.Address3;
                    objsave.CityNamePinCode = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.TransportDetails.CityNamePinCode;
                    objsave.ContactNumber = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.TransportDetails.ContactNumber;
                    objsave.Fax = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.TransportDetails.Fax;
                    objsave.EmailId = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.TransportDetails.EmailId;
                    objsave.Website = datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.TransportDetails.Website;

                    DataTable dtCancelPaxdetails = new DataTable();
                    dtCancelPaxdetails.Columns.Add("TicketNumber");
                    dtCancelPaxdetails.Columns.Add("SeatNo");
                    dtCancelPaxdetails.Columns.Add("SeatType");
                    dtCancelPaxdetails.Columns.Add("PassengerName");
                    dtCancelPaxdetails.Columns.Add("Gender");
                    dtCancelPaxdetails.Columns.Add("Age");
                    dtCancelPaxdetails.Columns.Add("Fare");
                    dtCancelPaxdetails.Columns.Add("BoardingId");
                    dtCancelPaxdetails.Columns.Add("Place");
                    dtCancelPaxdetails.Columns.Add("Time");
                    dtCancelPaxdetails.Columns.Add("Address");
                    dtCancelPaxdetails.Columns.Add("LandMark");
                    dtCancelPaxdetails.Columns.Add("ContactNumber");
                    dtCancelPaxdetails.Columns.Add("ReportingTime");
                    dtCancelPaxdetails.Columns.Add("ServiceCharges");
                    for (int i = 0; i < datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList.Count; i++)
                    {


                        dtCancelPaxdetails.Rows.Add
                            (

                            datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList[i].TicketNumber,
                            datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList[i].SeatNo,
                            datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList[i].SeatType,
                            datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList[i].PassengerName,
                            datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList[i].Gender,
                            datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList[i].Age,
                            datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList[i].Fare,
                            datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList[i].BoardingPoints.BoardingId,
                            datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList[i].BoardingPoints.Place,
                            datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList[i].BoardingPoints.Time,
                            datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList[i].BoardingPoints.Address,
                            datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList[i].BoardingPoints.LandMark,
                            datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList[i].BoardingPoints.ContactNumber,
                            datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList[i].ReportingTime,
                            datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList[i].ServiceCharges
                            );


                    }
                    DataTable dtUpdateCancelPaxList = new DataTable();
                    UpdateCancelBookingSeat objUpdate = new UpdateCancelBookingSeat();
                    dtUpdateCancelPaxList.Columns.Add("TicketNumber");
                    for (int i = 0; i < datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList.Count; i++)
                    {


                        dtUpdateCancelPaxList.Rows.Add
                            (

                            datalist.CancellationOutput.CanceledTicketDetails.CanceledPNRDetails.CancelPaxList[i].TicketNumber
                            );
                    }

                    objUpdate.UpdateCancelPaxList = dtUpdateCancelPaxList;
                    DataSet ds1 = objUpdate.UpdateCancelBusBookDetails();

                    objsave.CancelPaxList = dtCancelPaxdetails;

                    objsave.CompanyId = Models.TravelModel.Common.BusAPIDetails.CompanyId;
                    objsave.TransType = Models.TravelModel.Common.BusAPIDetails.TransType;
                    objsave.BookingId = BookingId;
                    objsave.Status = "SUCCESS";
                    objsave.Remarks = "Ticket Booking Cancelled";
                    objsave.BookedDate = DateTime.Now.ToString();

                    DataSet ds = objsave.CancelBusBookDetails();
                }
                else
                {
                    CancelBookingSeat objsave = new CancelBookingSeat();
                    objsave.ResponseStatus = datalist.ResponseStatus;
                    objsave.Remarks = datalist.FailureRemarks;
                    objsave.TransactionId = obj.CancellationInput.TransactionId;
                    objsave.PenaltyAmount = obj.CancellationInput.PenaltyAmount;
                    objsave.TicketNumbers = obj.CancellationInput.TicketNumbers;
                    objsave.TransportId = obj.CancellationInput.TransportId;
                    objsave.TotalTicketsToCancel = obj.CancellationInput.TotalTicketsToCancel;
                    objsave.CompanyId = Models.TravelModel.Common.BusAPIDetails.CompanyId;
                    objsave.TransType = Models.TravelModel.Common.BusAPIDetails.TransType;
                    objsave.BookingId = BookingId;
                    objsave.Status = "FAILED";
                    objsave.FK_MemID = obj.FK_MemID;
                    objsave.BookedDate = DateTime.Now.ToString();

                    DataSet ds = objsave.CancelBusBookDetails();
                }

            }
            catch (Exception ex)
            {
                datalist.ResponseStatus = ex.Message;
                datalist.FailureRemarks = "1";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusModel.GetCancellationResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        public static HttpWebRequest GetCancellationDetails()
        {
            var url = Models.TravelModel.Common.BusAPIDetails.APIURL;
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url + "/GetCancellation");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        #endregion

        #region GetBookedHistory 
        [HttpPost]
        public ResponseModel GetBookedHistory(RequestModel model)
        {
            BusModel.GetBookedHistoryRequest obj = new BusModel.GetBookedHistoryRequest();
            History.BusBookingHistory datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {


                HttpWebRequest request = GetBookedHistoryDetails();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<BusModel.GetBookedHistoryRequest>(dcdata);

                List<History.BookedTicket> list = new List<History.BookedTicket>();

                History.BookedHistoryOutput objdata = new History.BookedHistoryOutput();
                CustomerDb db = new CustomerDb();
                list = db.GetBusHistory(obj);
                if (list != null && list.Count > 0)
                {
                    objdata.BookedTickets = list;
                    datalist = new History.BusBookingHistory();
                    datalist.ResponseStatus = "1";
                    datalist.BookedHistoryOutput = objdata;
                }
                else
                {
                    datalist = new History.BusBookingHistory();
                    datalist.ResponseStatus = "0";
                    datalist.BookedHistoryOutput = null;
                    datalist.FailureRemarks = "Data not available";
                }
                //string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                //byte[] postBytes = Encoding.UTF8.GetBytes(json);
                //Stream requestStream = request.GetRequestStream();
                //// now send it
                //requestStream.Write(postBytes, 0, postBytes.Length);
                //requestStream.Close();
                //using (WebResponse response = request.GetResponse())
                //{
                //    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                //    {
                //        soapResult = rd.ReadToEnd();

                //    }
                //    // datalist = JsonConvert.DeserializeObject<GetSeatMapResponse>(soapResult);

                //    datalist = JsonConvert.DeserializeObject<BusModel.GetBookedHistoryResponse>(soapResult);
                //}


            }
            catch (Exception ex)
            {
                datalist.ResponseStatus = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(History.BusBookingHistory));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        public static HttpWebRequest GetBookedHistoryDetails()
        {
            try
            {

                var url = Models.TravelModel.Common.BusAPIDetails.APIURL;
                HttpWebRequest webRequest =
                (HttpWebRequest)WebRequest.Create(@"" + url + "/GetBookedHistory");
                webRequest.ContentType = "application/json";
                webRequest.Method = "POST";
                return webRequest;

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region GetBusAccountStatement 
        [HttpPost]
        public ResponseModel GetBusAccountStatement(RequestModel model)
        {
            BusModel.GetAccountStatementRequest obj = new BusModel.GetAccountStatementRequest();
            BusModel.GetAccountStatementResponse datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                Models.TravelModel.Common objcomm = new Models.TravelModel.Common();

                string soapResult = "";
                HttpWebRequest request = GetAccountStatementDetails();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<BusModel.GetAccountStatementRequest>(dcdata);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();

                    }
                    // datalist = JsonConvert.DeserializeObject<GetSeatMapResponse>(soapResult);

                    datalist = JsonConvert.DeserializeObject<BusModel.GetAccountStatementResponse>(soapResult);
                }


            }
            catch (Exception ex)
            {
                datalist.ResponseStatus = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusModel.GetAccountStatementResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        public static HttpWebRequest GetAccountStatementDetails()
        {
            var url = Models.TravelModel.Common.BusAPIDetails.APIURL;
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url + "/GetAccountStatement ");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        #endregion


        #endregion

        #region Prepaid Recharge API
        public ResponseModel GetOperators()
        {
            RechargeAPI.Response datalist = null;
            Models.TravelModel.Common.MobileAPIDetails model = new Models.TravelModel.Common.MobileAPIDetails();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {

                string soapResult = "";
                HttpWebRequest request = GetOperator();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();

                    }
                    datalist = JsonConvert.DeserializeObject<RechargeAPI.Response>(soapResult);


                }

            }
            catch (Exception ex)
            {
                datalist.ResponseStatus = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RechargeAPI.Response));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        public ResponseModel GetOperatorsMobile()
        {
            try
            {

                RechargeAPI.Response datalist = null;
                Models.TravelModel.Common.MobileAPIDetails model = new Models.TravelModel.Common.MobileAPIDetails();
                ResponseModel returnResponse = new ResponseModel();
                string EncryptedText = "";
                try
                {


                    CustomerDb db = new CustomerDb();
                    string date = DateTime.Now.ToString("dd/MM/yyyy");
                    var result = db.CheckOperatorStatus(date, "Recharge");
                    if (result.Flag == 1)
                    {
                        var dt = db.GetMobileOperator();
                        if (dt != null)
                        {
                            datalist = new RechargeAPI.Response();
                            datalist.ResponseStatus = "1";
                            SGCareWeb.Models.TravelModel.RechargeAPI.RechargeOperatorsOutput data = new SGCareWeb.Models.TravelModel.RechargeAPI.RechargeOperatorsOutput();
                            data.OperatorLists = dt;
                            datalist.RechargeOperatorsOutput = data;
                        }

                    }
                    else
                    {

                        string soapResult = "";
                        HttpWebRequest request = GetOperator();
                        XmlDocument soapEnvelopeXml = new XmlDocument();
                        string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                        byte[] postBytes = Encoding.UTF8.GetBytes(json);
                        Stream requestStream = request.GetRequestStream();
                        // now send it
                        requestStream.Write(postBytes, 0, postBytes.Length);
                        requestStream.Close();
                        using (WebResponse response = request.GetResponse())
                        {
                            using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                            {
                                soapResult = rd.ReadToEnd();

                            }
                            datalist = JsonConvert.DeserializeObject<RechargeAPI.Response>(soapResult);
                            if (datalist != null)
                            {
                                if (datalist.ResponseStatus == "1")
                                {
                                    string MobileOperators = "<Operator>";
                                    if (datalist.RechargeOperatorsOutput.OperatorLists.Count > 0)
                                    {
                                        foreach (var item in datalist.RechargeOperatorsOutput.OperatorLists)
                                        {
                                            MobileOperators += "<MobileOperators><OperatorCode>" + item.OperatorCode + "</OperatorCode><OperatorDescritpion>" + item.OperatorDescritpion + "</OperatorDescritpion><RechargeType>" + item.RechargeType + "</RechargeType><Commission>" + item.Commission + "</Commission></MobileOperators>";
                                        }
                                    }

                                    MobileOperators += "</Operator>";
                                    db.MobileOperator(MobileOperators);

                                }
                            }

                        }
                    }

                }
                catch (Exception ex)
                {
                    datalist.ResponseStatus = ex.Message;
                }
                string CustData = "";
                DataContractJsonSerializer js;
                MemoryStream ms;
                js = new DataContractJsonSerializer(typeof(RechargeAPI.Response));
                ms = new MemoryStream();
                js.WriteObject(ms, datalist);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                CustData = sr.ReadToEnd();
                sr.Close();
                ms.Close();
                EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
                returnResponse.body = EncryptedText;
                return returnResponse;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static HttpWebRequest GetOperator()
        {
            try
            {
                var url = Models.TravelModel.Common.MobileAPIDetails.APIURL;
                HttpWebRequest webRequest =
                (HttpWebRequest)WebRequest.Create(@"" + url + "/GetOperatorsMobile");
                webRequest.ContentType = "application/json";
                webRequest.Method = "POST";
                return webRequest;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Recharge API
        [HttpPost]
        public ResponseModel GetMobileRechargeDone(RequestModel model)
        {
            RechargeAPI.RechargeRequest obj = new RechargeAPI.RechargeRequest();
            RechargeAPI.Rechargereponse datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            Home objhome = new Home();
            Random rnd = new Random();
            string BookingId = Convert.ToString(rnd.Next(1, 100000000));
            string soapResult = "";
            HttpWebRequest request = GetRechargeDoneRequestRe();
            try
            {
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<RechargeAPI.RechargeRequest>(dcdata);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();

                    }
                    datalist = JsonConvert.DeserializeObject<RechargeAPI.Rechargereponse>(soapResult);


                }
                DebitWallet objwallet = new DebitWallet();

                if (datalist.ResponseStatus == "1")
                {
                    objhome.usertrackid = datalist.UserTrackId;
                    objhome.ReferenceNumber = datalist.RechargeOutput.ReferenceNumber;
                    objhome.OperatorDescription = datalist.RechargeOutput.OperatorDescription;
                    objhome.MobileNumber = datalist.RechargeOutput.MobileNumber;
                    objhome.Amount = datalist.RechargeOutput.Amount;
                    objhome.OperatorTransactionId = datalist.RechargeOutput.OperatorTransactionId;
                    objhome.CompanyId = obj.CompanyId;
                    objhome.TransType = obj.TransType;
                    objhome.BookingId = BookingId;
                    objhome.FK_MemId = obj.FK_MemID;
                    objhome.Type = "Prepaid Mobile Recharge";
                    objhome.Status = "SUCCESS";
                    DataSet ds = objhome.SaveMobileResponse();
                }
                else
                {
                    objhome.usertrackid = datalist.UserTrackId;
                    objhome.ReferenceNumber = datalist.RechargeOutput.ReferenceNumber;
                    objhome.OperatorDescription = datalist.RechargeOutput.OperatorDescription;
                    objhome.MobileNumber = datalist.RechargeOutput.MobileNumber;
                    objhome.Amount = datalist.RechargeOutput.Amount;
                    objhome.OperatorTransactionId = datalist.RechargeOutput.OperatorTransactionId;
                    objhome.CompanyId = obj.CompanyId;
                    objhome.TransType = obj.TransType;
                    objhome.BookingId = BookingId;
                    objhome.Type = "Prepaid Mobile Recharge";
                    objhome.Status = "FAILED";
                    objhome.FK_MemId = obj.FK_MemID;
                    DataSet ds = objhome.SaveMobileResponse();

                }

            }
            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                }
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RechargeAPI.Rechargereponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;

        }
        #endregion

        public static HttpWebRequest GetRechargeDoneRequestRe()
        {
            var url = Models.TravelModel.Common.MobileAPIDetails.APIURL;
            HttpWebRequest webRequest =
                   (HttpWebRequest)WebRequest.Create(@"" + url + "/GetRechargeDoneRequest");

            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        public static HttpWebRequest GetTransactionStatusURL()
        {
            var url = Models.TravelModel.Common.MobileAPIDetails.APIURL;
            HttpWebRequest webRequest =
                   (HttpWebRequest)WebRequest.Create(@"" + url + "/GetTransactionStatus");

            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        public static HttpWebRequest GetRechargeTransactionStatusURL()
        {
            var url = Models.TravelModel.Common.MobileAPIDetails.APIURL;
            HttpWebRequest webRequest =
                   (HttpWebRequest)WebRequest.Create(@"" + url + "/GetRechargeTransactionStatus");



            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        [HttpPost]
        public ResponseModel GetRechargeTransactionStatus(RequestModel model)
        {
            RechargeAPI.TransactionResponse datalist = null;
            RechargeAPI.BillPaymentRequest obj = new RechargeAPI.BillPaymentRequest();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            string soapResult = "";
            HttpWebRequest request = GetRechargeTransactionStatusURL();
            XmlDocument soapEnvelopeXml = new XmlDocument();

            string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
            obj = JsonConvert.DeserializeObject<RechargeAPI.BillPaymentRequest>(dcdata);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            Stream requestStream = request.GetRequestStream();
            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();

                }
                datalist = JsonConvert.DeserializeObject<RechargeAPI.TransactionResponse>(soapResult);


            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RechargeAPI.TransactionResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;

        }

        #region Get Prepaid Print  API
        [HttpPost]
        public ResponseModel GetPrepaidRechargePrint(RequestModel model)
        {
            RechargeAPI.GetRechargeReprintRequest obj = new RechargeAPI.GetRechargeReprintRequest();
            RechargeAPI.GetRechargeReprintResponse datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {

                string soapResult = "";
                HttpWebRequest request = GetBusReprint();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<RechargeAPI.GetRechargeReprintRequest>(dcdata);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();

                    }
                    datalist = JsonConvert.DeserializeObject<RechargeAPI.GetRechargeReprintResponse>(soapResult);


                }

            }
            catch (Exception ex)
            {
                datalist.ResponseStatus = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RechargeAPI.GetRechargeReprintResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        public static HttpWebRequest GetBusReprint()
        {
            var url = Models.TravelModel.Common.MobileAPIDetails.APIURL;
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url + "/GetPrepaidRechargePrint");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }
        #endregion

        [HttpPost]
        public ResponseModel GetRechargeHistory(RequestModel model)
        {
            RechargeAPI.GetRechargeHistoryResponse datalist = null;
            RechargeAPI.GetRechargeHistory obj = new RechargeAPI.GetRechargeHistory();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            HttpWebRequest request = GetRechargeHistoryURL();
            XmlDocument soapEnvelopeXml = new XmlDocument();

            string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
            obj = JsonConvert.DeserializeObject<RechargeAPI.GetRechargeHistory>(dcdata);

            CustomerDb db = new CustomerDb();
            List<RechargeAPI.Recharges> list = new List<RechargeAPI.Recharges>();
            list = db.GetRechargeHistory(obj);
            if (list != null && list.Count > 0)
            {
                datalist = new RechargeAPI.GetRechargeHistoryResponse();
                datalist.ResponseStatus = "1";
                datalist.Recharges = list;
            }
            else
            {
                datalist = new RechargeAPI.GetRechargeHistoryResponse();
                datalist.ResponseStatus = "0";
                datalist.Recharges = null;
                datalist.FailureRemarks = "Data not available";
            }
            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            //byte[] postBytes = Encoding.UTF8.GetBytes(json);
            //Stream requestStream = request.GetRequestStream();
            //// now send it
            //requestStream.Write(postBytes, 0, postBytes.Length);
            //requestStream.Close();
            //using (WebResponse response = request.GetResponse())
            //{
            //    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
            //    {
            //        soapResult = rd.ReadToEnd();

            //    }
            //    datalist = JsonConvert.DeserializeObject<RechargeAPI.GetRechargeHistoryResponse>(soapResult);

            //}
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RechargeAPI.GetRechargeHistoryResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;

        }
        public static HttpWebRequest GetRechargeHistoryURL()
        {
            var url = Models.TravelModel.Common.MobileAPIDetails.APIURL;
            HttpWebRequest webRequest =
                   (HttpWebRequest)WebRequest.Create(@"" + url + "/GetRechargeHistory");



            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        [HttpPost]
        public ResponseModel GetAccountStatement(RequestModel model)
        {
            RechargeAPI.AccountStatementResponse datalist = null;
            BusModel.GetAccountStatementRequest obj = new BusModel.GetAccountStatementRequest();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            string soapResult = "";
            HttpWebRequest request = GetAccountStatementURL();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
            obj = JsonConvert.DeserializeObject<BusModel.GetAccountStatementRequest>(dcdata);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            Stream requestStream = request.GetRequestStream();
            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();

                }
                datalist = JsonConvert.DeserializeObject<RechargeAPI.AccountStatementResponse>(soapResult);

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RechargeAPI.AccountStatementResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;

        }
        public static HttpWebRequest GetAccountStatementURL()
        {
            var url = Models.TravelModel.Common.MobileAPIDetails.APIURL;
            HttpWebRequest webRequest =
                   (HttpWebRequest)WebRequest.Create(@"" + url + "/GetAccountStatement");



            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        #endregion

        #region Postpaid API
        public static HttpWebRequest GetBillPaymentOperatorsRequestURL()
        {
            var url = Models.TravelModel.Common.MobileAPIDetails.APIURL;
            HttpWebRequest webRequest =
                   (HttpWebRequest)WebRequest.Create(@"" + url + "/GetBillPaymentOperatorsRequest");



            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        public ResponseModel GetBillPaymentOperatorsRequest()
        {
            Models.TravelModel.Common.MobileAPIDetails model = new Models.TravelModel.Common.MobileAPIDetails();
            RechargeAPI.PaymentOperatorsResponse datalist = null;
            ResponseModel returnResponse = new ResponseModel();

            string EncryptedText = "";
            try
            {
                CustomerDb db = new CustomerDb();
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                var result = db.CheckOperatorStatus(date, "Payment");
                if (result.Flag == 1)
                {
                    var dt = db.GetPaymentOperator();
                    if (dt != null)
                    {
                        datalist = new RechargeAPI.PaymentOperatorsResponse();
                        datalist.ResponseStatus = "1";
                        SGCareWeb.Models.TravelModel.RechargeAPI.BillPaymentOperatorsOutput data = new SGCareWeb.Models.TravelModel.RechargeAPI.BillPaymentOperatorsOutput();
                        data.OperatorLists = dt;
                        datalist.BillPaymentOperatorsOutput = data;
                    }

                }
                else
                {


                    string soapResult = "";
                    HttpWebRequest request = GetBillPaymentOperatorsRequestURL();
                    XmlDocument soapEnvelopeXml = new XmlDocument();
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    byte[] postBytes = Encoding.UTF8.GetBytes(json);
                    Stream requestStream = request.GetRequestStream();
                    // now send it
                    requestStream.Write(postBytes, 0, postBytes.Length);
                    requestStream.Close();
                    using (WebResponse response = request.GetResponse())
                    {
                        using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                        {
                            soapResult = rd.ReadToEnd();

                        }
                        datalist = JsonConvert.DeserializeObject<RechargeAPI.PaymentOperatorsResponse>(soapResult);
                        if (datalist != null)
                        {
                            if (datalist.ResponseStatus == "1")
                            {
                                string MobileOperators = "<Operator>";
                                if (datalist.BillPaymentOperatorsOutput.OperatorLists.Count > 0)
                                {
                                    foreach (var item in datalist.BillPaymentOperatorsOutput.OperatorLists)
                                    {
                                        MobileOperators += "<MobileOperators><OperatorCode>" + item.OperatorCode + "</OperatorCode><OperatorDescritpion>" + item.OperatorDescritpion + "</OperatorDescritpion><RechargeType>" + item.RechargeType + "</RechargeType><Commission>" + item.Commission + "</Commission></MobileOperators>";
                                    }
                                }

                                MobileOperators += "</Operator>";


                                db.GetBillPaymentOperatorsRequest(MobileOperators);

                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                datalist.ResponseStatus = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RechargeAPI.PaymentOperatorsResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        [HttpPost]
        public ResponseModel GetBillPaymentDoneRequest(RequestModel model)
        {
            RechargeAPI.BillPaymentResponse datalist = null;
            RechargeAPI.BillPaymentRequest obj = new RechargeAPI.BillPaymentRequest();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            Home objhome = new Home();
            APIResponse1 objresponse = new APIResponse1();
            Random rnd = new Random();
            string BookingId = Convert.ToString(rnd.Next(1, 100000000));

            Models.TravelModel.Common objcomm = new Models.TravelModel.Common();
            string soapResult = "";
            HttpWebRequest request = GetBillPaymentDoneRequestURL();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
            obj = JsonConvert.DeserializeObject<RechargeAPI.BillPaymentRequest>(dcdata);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            Stream requestStream = request.GetRequestStream();
            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();

                }
                datalist = JsonConvert.DeserializeObject<RechargeAPI.BillPaymentResponse>(soapResult);


            }
            if (datalist.ResponseStatus == "1")
            {
                objhome.usertrackid = datalist.UserTrackId;
                objhome.FK_MemId = obj.BillPaymentInput.FK_MemID;
                objhome.ReferenceNumber = datalist.BillPaymentOutput.ReferenceNumber;
                objhome.OperatorDescription = datalist.BillPaymentOutput.OperatorDescription;
                objhome.MobileNumber = datalist.BillPaymentOutput.MobileNumber;
                objhome.Amount = datalist.BillPaymentOutput.Amount;
                objhome.RechargeDateTime = datalist.BillPaymentOutput.RechargeDateTime;
                objhome.OperatorTransactionId = datalist.BillPaymentOutput.OperatorTransactionId;
                objhome.CompanyId = Models.TravelModel.Common.MobileAPIDetails.CompanyId;
                objhome.TransType = Models.TravelModel.Common.MobileAPIDetails.TransType;
                objhome.BookingId = BookingId;
                objhome.Type = "Postpaid Mobile Recharge";
                objhome.Status = "SUCCESS";
                DataSet ds = objhome.SaveMobileResponse();
            }
            else
            {
                objhome.usertrackid = datalist.UserTrackId;
                objhome.FK_MemId = obj.BillPaymentInput.FK_MemID;
                objhome.ReferenceNumber = datalist.BillPaymentOutput.ReferenceNumber;
                objhome.OperatorDescription = datalist.BillPaymentOutput.OperatorDescription;
                objhome.MobileNumber = datalist.BillPaymentOutput.MobileNumber;
                objhome.Amount = datalist.BillPaymentOutput.Amount;
                objhome.RechargeDateTime = datalist.BillPaymentOutput.RechargeDateTime;
                objhome.OperatorTransactionId = datalist.BillPaymentOutput.OperatorTransactionId;
                objhome.CompanyId = Models.TravelModel.Common.MobileAPIDetails.CompanyId;
                objhome.TransType = Models.TravelModel.Common.MobileAPIDetails.TransType;
                objhome.BookingId = BookingId;
                objhome.Type = "Postpaid Mobile Recharge";
                objhome.Status = "FAILED";
                DataSet ds = objhome.SaveMobileResponse();
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RechargeAPI.BillPaymentResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;

        }

        public static HttpWebRequest GetBillPaymentDoneRequestURL()
        {
            var url = Models.TravelModel.Common.MobileAPIDetails.APIURL;
            HttpWebRequest webRequest =
                   (HttpWebRequest)WebRequest.Create(@"" + url + "/GetBillPaymentDoneRequest");



            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }
        #endregion

        #region DTH Recharge
        [HttpPost]
        public ResponseModel DTHRecharge(RequestModel model)
        {
            Models.TravelModel.Common objcomm = new Models.TravelModel.Common();
            RechargeAPI.Rechargereponse datalist = null;
            RechargeAPI.RechargeRequest obj = new RechargeAPI.RechargeRequest();
            APIResponse1 objresponse = new APIResponse1();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            Home objhome = new Home();
            Random rnd = new Random();
            string BookingId = Convert.ToString(rnd.Next(1, 100000000));

            string soapResult = "";
            HttpWebRequest request = GetRechargeDoneRequestRe();
            try
            {


                XmlDocument soapEnvelopeXml = new XmlDocument();
                string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<RechargeAPI.RechargeRequest>(dcdata);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }
                    datalist = JsonConvert.DeserializeObject<RechargeAPI.Rechargereponse>(soapResult);
                }
                DebitWallet objwallet = new DebitWallet();

                if (datalist.ResponseStatus == "1")
                {
                    objhome.usertrackid = datalist.UserTrackId;
                    objhome.ReferenceNumber = datalist.RechargeOutput.ReferenceNumber;
                    objhome.OperatorDescription = datalist.RechargeOutput.OperatorDescription;
                    objhome.MobileNumber = datalist.RechargeOutput.MobileNumber;
                    objhome.Amount = datalist.RechargeOutput.Amount;
                    objhome.RechargeDateTime = datalist.RechargeOutput.RechargeDateTime;
                    objhome.OperatorTransactionId = datalist.RechargeOutput.OperatorTransactionId;
                    objhome.CompanyId = Models.TravelModel.Common.MobileAPIDetails.CompanyId;
                    objhome.TransType = Models.TravelModel.Common.MobileAPIDetails.TransType;
                    objhome.BookingId = BookingId;
                    objhome.FK_MemId = obj.FK_MemID;
                    objhome.Type = "DTH Recharge";
                    objhome.Status = "SUCCESS";
                    DataSet ds = objhome.SaveMobileResponse();
                }
                else
                {
                    objhome.usertrackid = datalist.UserTrackId;
                    objhome.ReferenceNumber = datalist.RechargeOutput.ReferenceNumber;
                    objhome.OperatorDescription = datalist.RechargeOutput.OperatorDescription;
                    objhome.MobileNumber = datalist.RechargeOutput.MobileNumber;
                    objhome.Amount = datalist.RechargeOutput.Amount;
                    objhome.RechargeDateTime = datalist.RechargeOutput.RechargeDateTime;
                    objhome.OperatorTransactionId = datalist.RechargeOutput.OperatorTransactionId;
                    objhome.CompanyId = Models.TravelModel.Common.MobileAPIDetails.CompanyId;
                    objhome.TransType = Models.TravelModel.Common.MobileAPIDetails.TransType;
                    objhome.BookingId = BookingId;
                    objhome.Type = "DTH Recharge";
                    objhome.Status = "FAILED";
                    objhome.FK_MemId = obj.FK_MemID;
                    DataSet ds = objhome.SaveMobileResponse();
                }

            }
            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                }
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RechargeAPI.Rechargereponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }
        public static HttpWebRequest DTHRechargestatus()
        {
            var url = Models.TravelModel.Common.MobileAPIDetails.APIURL;
            HttpWebRequest webRequest =
                   (HttpWebRequest)WebRequest.Create(@"" + url + "/DTHRecharge");

            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }
        #endregion


        #region Flight API

        #region Get Flight Availability 
        [HttpPost]
        public ResponseModel GetFlightDetailsAPI(RequestModel model)
        {
            FlightAPI.FlightDetails obj = new FlightAPI.FlightDetails();
            FlightAPI.FlightSearchResponse datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {
                string soapResult = "";
                HttpWebRequest request = GetFlights();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<FlightAPI.FlightDetails>(dcdata);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();

                    }
                    datalist = JsonConvert.DeserializeObject<FlightAPI.FlightSearchResponse>(soapResult);

                }
            }
            catch (Exception ex)
            {

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FlightAPI.FlightSearchResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;

        }

        #endregion

        #region Get Tax API
        public static HttpWebRequest GetTaxURL()
        {
            var url = Models.TravelModel.Common.FlightAPIDetails.APIURL;
            HttpWebRequest webRequest =
                   (HttpWebRequest)WebRequest.Create(@"" + url + "/GetTax");

            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        [HttpPost]
        public ResponseModel GetTaxAPI(RequestModel model)
        {
            FlightAPI.GetTaxRequest obj = new FlightAPI.GetTaxRequest();
            FlightAPI.TaxResponse datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            string soapResult = "";
            HttpWebRequest request = GetTaxURL();
            XmlDocument soapEnvelopeXml = new XmlDocument();

            string usertrackid = obj.UserTrackId;
            string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
            obj = JsonConvert.DeserializeObject<FlightAPI.GetTaxRequest>(dcdata);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            Stream requestStream = request.GetRequestStream();
            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                datalist = JsonConvert.DeserializeObject<FlightAPI.TaxResponse>(soapResult);

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FlightAPI.TaxResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }
        #endregion

        #region Get Fare Rule
        public static HttpWebRequest GetFareRules()
        {
            var url = Models.TravelModel.Common.FlightAPIDetails.APIURL;
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url + "/GetFareRule");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        [HttpPost]
        public ResponseModel FareRulesAPI(RequestModel model)
        {
            FlightAPI.FareRelesRequest Obj = new FlightAPI.FareRelesRequest();
            FlightAPI.FareRelesResponse datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            string soapResult = "";
            HttpWebRequest request = GetFareRules();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
            Obj = JsonConvert.DeserializeObject<FlightAPI.FareRelesRequest>(dcdata);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(Obj);
            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            Stream requestStream = request.GetRequestStream();
            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();

                }
                datalist = JsonConvert.DeserializeObject<FlightAPI.FareRelesResponse>(soapResult);
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FlightAPI.FareRelesResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;

        }
        #endregion

        #region Book Ticket API

        public static HttpWebRequest BookTickeUrl()
        {
            var url = Models.TravelModel.Common.FlightAPIDetails.APIURL;
            HttpWebRequest webRequest =
                   (HttpWebRequest)WebRequest.Create(@"" + url + "/GetBook");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        [HttpPost]
        public ResponseModel BookTicketAPI(RequestModel model)
        {
            FlightAPI.BookTicketAPI obj = new FlightAPI.BookTicketAPI();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            Random rnd = new Random();
            string BookingId = Convert.ToString(rnd.Next(1, 100000000));
            FlightAPI.BookTicket datalist = null;

            APIResponse1 objresponse = new APIResponse1();

            Home objhome = new Home();

            string soapResult = "";
            HttpWebRequest request = BookTickeUrl();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
            obj = JsonConvert.DeserializeObject<FlightAPI.BookTicketAPI>(dcdata);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            Stream requestStream = request.GetRequestStream();
            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();

                }
                datalist = JsonConvert.DeserializeObject<FlightAPI.BookTicket>(soapResult);

            }
            FlightAPI.BookTicket objticket = new FlightAPI.BookTicket();

            if (datalist.ResponseStatus == "1")
            {
                Home objHome = new Home();
                objHome.hermespnr = datalist.BookOutput.FlightTicketDetails[0].HermesPNR;
                objHome.TransactionId = datalist.BookOutput.FlightTicketDetails[0].TransactionId;
                objHome.Name = datalist.BookOutput.FlightTicketDetails[0].CustomerDetails.Name;
                objHome.BookingType = datalist.BookOutput.FlightTicketDetails[0].BookingType;
                objHome.Origin = obj.Origin;
                objHome.Destination = obj.Destination;
                objHome.DepartingDate = obj.DepartingDate;
                objHome.ReturningDate = obj.ReturningDate;
                objHome.Class = obj.Class;
                objHome.Adult = Convert.ToInt16(datalist.BookOutput.FlightTicketDetails[0].AdultCount);
                objHome.Child = Convert.ToInt16(datalist.BookOutput.FlightTicketDetails[0].ChildCount);
                objHome.Infant = Convert.ToInt16(datalist.BookOutput.FlightTicketDetails[0].InfantCount);
                objHome.usertrackid = datalist.UserTrackId;
                objHome.hermespnr = datalist.BookOutput.FlightTicketDetails[0].HermesPNR;
                objHome.TransactionId = datalist.BookOutput.FlightTicketDetails[0].TransactionId;
                objHome.TotalAmount = datalist.BookOutput.FlightTicketDetails[0].TotalAmount;
                objHome.OtherCharges = datalist.BookOutput.FlightTicketDetails[0].OtherCharges;
                objHome.AdultCount = Convert.ToInt16(datalist.BookOutput.FlightTicketDetails[0].AdultCount);
                objHome.ChildCount = Convert.ToInt16(datalist.BookOutput.FlightTicketDetails[0].ChildCount);
                objHome.InfantCount = Convert.ToInt16(datalist.BookOutput.FlightTicketDetails[0].InfantCount);
                objHome.BookingType = datalist.BookOutput.FlightTicketDetails[0].BookingType;
                objHome.TravelType = datalist.BookOutput.FlightTicketDetails[0].TravelType;
                objHome.IssueDateTime = datalist.BookOutput.FlightTicketDetails[0].IssueDateTime;
                objHome.bookingremarks = datalist.BookOutput.FlightTicketDetails[0].BookingRemarks;
                objHome.Title = datalist.BookOutput.FlightTicketDetails[0].CustomerDetails.Title;
                objHome.Name = datalist.BookOutput.FlightTicketDetails[0].CustomerDetails.Name;
                objHome.Address = datalist.BookOutput.FlightTicketDetails[0].CustomerDetails.Address;
                objHome.City = datalist.BookOutput.FlightTicketDetails[0].CustomerDetails.City;
                objHome.CountryId = datalist.BookOutput.FlightTicketDetails[0].CustomerDetails.CountryId;
                objHome.ContactNumber = datalist.BookOutput.FlightTicketDetails[0].CustomerDetails.ContactNumber;
                objHome.EmailId = datalist.BookOutput.FlightTicketDetails[0].CustomerDetails.EmailId;
                objHome.PinCode = datalist.BookOutput.FlightTicketDetails[0].CustomerDetails.PinCode;
                objHome.TerminalName = datalist.BookOutput.FlightTicketDetails[0].TerminalContactDetails.TerminalName;
                objHome.Address1 = datalist.BookOutput.FlightTicketDetails[0].TerminalContactDetails.Address1;
                objHome.Address2 = datalist.BookOutput.FlightTicketDetails[0].TerminalContactDetails.Address2;
                objHome.TerminalCity = datalist.BookOutput.FlightTicketDetails[0].TerminalContactDetails.City;
                objHome.TerminalState = datalist.BookOutput.FlightTicketDetails[0].TerminalContactDetails.State;
                objHome.TerminalCountry = datalist.BookOutput.FlightTicketDetails[0].TerminalContactDetails.Country;
                objHome.TerminalContact = datalist.BookOutput.FlightTicketDetails[0].TerminalContactDetails.ContactNumber;
                objHome.TerminalEmailId = datalist.BookOutput.FlightTicketDetails[0].TerminalContactDetails.EmailId;
                objHome.currencycode = datalist.BookOutput.FlightTicketDetails[0].PaymentDetails.CurrencyCode;
                objHome.amount = datalist.BookOutput.FlightTicketDetails[0].PaymentDetails.Amount;
                objHome.airlinetickettingpcc = datalist.BookOutput.FlightTicketDetails[0].PaymentDetails.AirlinePaymentDetails.TicketingPCC;
                objHome.airlinepaymenttype = datalist.BookOutput.FlightTicketDetails[0].PaymentDetails.AirlinePaymentDetails.PaymentType;
                objHome.airlinevendorcode = datalist.BookOutput.FlightTicketDetails[0].PaymentDetails.AirlinePaymentDetails.VendorCode;
                objHome.cardnumber = datalist.BookOutput.FlightTicketDetails[0].PaymentDetails.AirlinePaymentDetails.CreditCardDetails.CardNumber;
                objHome.cardtype = datalist.BookOutput.FlightTicketDetails[0].PaymentDetails.AirlinePaymentDetails.CreditCardDetails.CardType;
                objHome.airlinecode = datalist.BookOutput.FlightTicketDetails[0].AirlineDetails[0].AirlineCode;
                objHome.airlinepnr = datalist.BookOutput.FlightTicketDetails[0].AirlineDetails[0].AirlinePNR;
                objHome.airlinename = datalist.BookOutput.FlightTicketDetails[0].AirlineDetails[0].AirlineName;
                objHome.airlineaddress1 = datalist.BookOutput.FlightTicketDetails[0].AirlineDetails[0].Address1;
                objHome.airlineaddress2 = datalist.BookOutput.FlightTicketDetails[0].AirlineDetails[0].Address2;
                objHome.airlineCity = datalist.BookOutput.FlightTicketDetails[0].AirlineDetails[0].City;
                objHome.airlinecontactnumber = datalist.BookOutput.FlightTicketDetails[0].AirlineDetails[0].ContactNumber;
                objHome.faxnumber = datalist.BookOutput.FlightTicketDetails[0].AirlineDetails[0].FaxNumber;
                objHome.airlineemailid = datalist.BookOutput.FlightTicketDetails[0].AirlineDetails[0].EMailId;
                objHome.CompanyId = Models.TravelModel.Common.MobileAPIDetails.CompanyId;
                objHome.TransType = Models.TravelModel.Common.MobileAPIDetails.TransType;
                objHome.BookingId = BookingId;
                objHome.Status = "SUCCESS";
                objHome.BookingBy = "Mobile";
                objhome.FK_MemId = obj.FK_MemID;
                DataTable dtpassengerdetails = new DataTable();
                DataTable dtbookingsegments = new DataTable();

                int card = rnd.Next(52);

                dtpassengerdetails.Columns.Add("TicketNumber");
                dtpassengerdetails.Columns.Add("TransmissionControlNo");
                dtpassengerdetails.Columns.Add("PassengerType");
                dtpassengerdetails.Columns.Add("Title");
                dtpassengerdetails.Columns.Add("FirstName");
                dtpassengerdetails.Columns.Add("LastName");
                dtpassengerdetails.Columns.Add("Age");
                dtpassengerdetails.Columns.Add("IdentityProofId");
                dtpassengerdetails.Columns.Add("IdentityProofNumber");
                dtpassengerdetails.Columns.Add("PersonOrgId");
                dtpassengerdetails.Columns.Add("PassengerId");
                dtbookingsegments.Columns.Add("PassengerId");
                dtbookingsegments.Columns.Add("TicketNumber");
                dtbookingsegments.Columns.Add("FlightNumber");
                dtbookingsegments.Columns.Add("AirCraftType");
                dtbookingsegments.Columns.Add("Origin");
                dtbookingsegments.Columns.Add("OriginAirport");
                dtbookingsegments.Columns.Add("DepartureDateTime");
                dtbookingsegments.Columns.Add("Destination");
                dtbookingsegments.Columns.Add("DestinationAirport");
                dtbookingsegments.Columns.Add("Arrivaldatetime");
                dtbookingsegments.Columns.Add("AirlineCode");
                dtbookingsegments.Columns.Add("ClassCode");
                dtbookingsegments.Columns.Add("ClassCodeDesc");
                dtbookingsegments.Columns.Add("FareBasis");
                dtbookingsegments.Columns.Add("BaggageAllowed");
                dtbookingsegments.Columns.Add("StopOverAllowed");
                dtbookingsegments.Columns.Add("FrequentFlyerId");
                dtbookingsegments.Columns.Add("FrequentFlyerNumber");
                dtbookingsegments.Columns.Add("SpecialServiceCode");
                dtbookingsegments.Columns.Add("TotalTaxAmount");
                dtbookingsegments.Columns.Add("GrossAmount");
                dtbookingsegments.Columns.Add("FlightId");
                for (int i = 0; i < datalist.BookOutput.FlightTicketDetails[0].PassengerDetails.Count; i++)
                {
                    string passengerid = card + '0' + datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].TicketNumber;

                    dtpassengerdetails.Rows.Add(datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].TicketNumber, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].TransmissionControlNo, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].PassengerType,
                    datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].Title, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].FirstName, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].LastName,
                    datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].Age, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].IdentityProofId, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].IdentityProofNumber,
                    datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].PersonOrgId, passengerid);

                    for (int j = 0; j < datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments.Count; j++)
                    {

                        dtbookingsegments.Rows.Add(passengerid, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].TicketNumber, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].FlightNumber,
                            datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].AirCraftType, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].Origin, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].OriginAirport,
                            datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].DepartureDateTime, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].Destination, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].DestinationAirport,
                            datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].Arrivaldatetime, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].AirlineCode, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].ClassCode,
                            datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].ClassCodeDesc, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].FareBasis == "" ? "0" : datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].FareBasis, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].BaggageAllowed,
                            datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].StopOverAllowed, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].FrequentFlyerId, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].FrequentFlyerNumber,
                             datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].SpecialServiceCode, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].TotalTaxAmount, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].GrossAmount, datalist.BookOutput.FlightTicketDetails[0].PassengerDetails[i].BookedSegments[j].FlightId);
                    }

                }

                objHome.PassengerDetails = dtpassengerdetails;
                objHome.BookingSegments = dtbookingsegments;
                DataSet ds = objHome.SaveFlightResponse();
            }
            else
            {
                Home objHome = new Home();
                objHome.bookingremarks = datalist.FailureRemarks;
                objHome.usertrackid = datalist.UserTrackId;
                objHome.CompanyId = Models.TravelModel.Common.MobileAPIDetails.CompanyId;
                objHome.TransType = Models.TravelModel.Common.MobileAPIDetails.TransType;
                objHome.BookingId = BookingId;
                objHome.FK_MemId = obj.FK_MemID;
                objHome.Status = "FAILED";
                objHome.BookingBy = "Mobile";
                objHome.BookingType = obj.BookInput.BookingType;
                objHome.Origin = obj.Origin;
                objHome.Destination = obj.Destination;
                objHome.DepartingDate = obj.DepartingDate;
                objHome.ReturningDate = obj.ReturningDate;
                objHome.Class = obj.Class;
                objHome.Adult = obj.BookInput.AdultCount;
                objHome.Child = obj.BookInput.ChildCount;
                objHome.Infant = obj.BookInput.InfantCount;
                objHome.AdultCount = obj.BookInput.AdultCount;
                objHome.ChildCount = obj.BookInput.ChildCount;
                objHome.InfantCount = obj.BookInput.InfantCount;
                objHome.usertrackid = datalist.UserTrackId;
                objHome.IssueDateTime = "";
                objhome.FK_MemId = obj.FK_MemID;
                DataSet ds = objHome.SaveFlightResponse();
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FlightAPI.BookTicket));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;


        }

        #endregion


        #region Get Transaction Details
        public static HttpWebRequest GetTransactionStatusFlightURL()
        {
            var url = Models.TravelModel.Common.FlightAPIDetails.APIURL;
            HttpWebRequest webRequest =
                   (HttpWebRequest)WebRequest.Create(@"" + url + "/GetTransactionStatus");



            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        [HttpPost]
        public ResponseModel GetTransactionStatus(RequestModel model)
        {
            FlightAPI.BookTicketAPI obj = new FlightAPI.BookTicketAPI();
            transaction.TransactionRootobject datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            string soapResult = "";
            HttpWebRequest request = GetTransactionStatusFlightURL();
            XmlDocument soapEnvelopeXml = new XmlDocument();

            string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
            obj = JsonConvert.DeserializeObject<FlightAPI.BookTicketAPI>(dcdata);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            Stream requestStream = request.GetRequestStream();
            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();

                }
                datalist = JsonConvert.DeserializeObject<transaction.TransactionRootobject>(soapResult);
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(transaction.TransactionRootobject));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }
        #endregion


        public static HttpWebRequest GetFlights()
        {
            var url = Models.TravelModel.Common.FlightAPIDetails.APIURL;
            HttpWebRequest webRequest =
                   (HttpWebRequest)WebRequest.Create(@"" + url + "/GetAvailability");  //GetFlightDetailsAPI
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        public static HttpWebRequest GetPartialCancellationURL()
        {
            var url = Models.TravelModel.Common.FlightAPIDetails.APIURL;
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url + "/GetPartialCancellation");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }


        public ResponseModel GetReturnTax(RequestModel model)
        {
            FlightAPI.GetTaxRequest obj = new FlightAPI.GetTaxRequest();
            FlightAPI.TaxResponse datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            string soapResult = "";
            HttpWebRequest request = GetTaxURL();
            XmlDocument soapEnvelopeXml = new XmlDocument();

            string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
            obj = JsonConvert.DeserializeObject<FlightAPI.GetTaxRequest>(dcdata);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            Stream requestStream = request.GetRequestStream();
            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                datalist = JsonConvert.DeserializeObject<FlightAPI.TaxResponse>(soapResult);

            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FlightAPI.TaxResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }


        [HttpPost]
        public ResponseModel CancelTicket(RequestModel model)
        {
            FlightAPI.RootObjectTicket obj = new FlightAPI.RootObjectTicket();
            FlightAPI.CancelTicketReponse datalistprintticket = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            string soapResult = "";
            HttpWebRequest request = GetCancellationURL();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
            obj = JsonConvert.DeserializeObject<FlightAPI.RootObjectTicket>(dcdata);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            Stream requestStream = request.GetRequestStream();
            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();

                }
                datalistprintticket = JsonConvert.DeserializeObject<FlightAPI.CancelTicketReponse>(soapResult);


            }
            BookedHistory objcancel = new BookedHistory();
            objcancel.HermesPNR = obj.CancellationInput.HermesPNR;
            objcancel.AirlinePNR = obj.CancellationInput.AirlinePNR;
            objcancel.CancellationType = "FULL";
            if (datalistprintticket.ResponseStatus == "1")
            {
                objcancel.Remarks = datalistprintticket.CancellationOutput.CancellationDetails.FullCancellationRemarks;
                DataSet ds = objcancel.SaveCancelTicket();
            }
            else
            {
                objcancel.Remarks = datalistprintticket.FailureRemarks;
            }

            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FlightAPI.CancelTicketReponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalistprintticket);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;


        }
        public static HttpWebRequest GetCancellationURL()
        {
            var url = Models.TravelModel.Common.FlightAPIDetails.APIURL;
            HttpWebRequest webRequest =
                   (HttpWebRequest)WebRequest.Create(@"" + url + "/GetCancellation");



            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        [HttpPost]
        public ResponseModel GetSeatMapAfterBooking(RequestModel model)
        {
            FlightAPI.RootObject obj = new FlightAPI.RootObject();
            FlightAPI.SeatMapResponse datseatmap = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            string soapResult = "";
            HttpWebRequest request = GetSeapMapUrl();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
            obj = JsonConvert.DeserializeObject<FlightAPI.RootObject>(dcdata);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            Stream requestStream = request.GetRequestStream();
            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();

                }
                datseatmap = JsonConvert.DeserializeObject<FlightAPI.SeatMapResponse>(soapResult);
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FlightAPI.SeatMapResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datseatmap);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }
        public static HttpWebRequest GetSeapMapUrl()
        {
            var url = Models.TravelModel.Common.FlightAPIDetails.APIURL;
            HttpWebRequest webRequest =
                   (HttpWebRequest)WebRequest.Create(@"" + url + "/GetSeatMapAfterBooking");



            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }



        public static HttpWebRequest GetBookSeatURL()
        {
            var url = Models.TravelModel.Common.FlightAPIDetails.APIURL;
            HttpWebRequest webRequest =
                   (HttpWebRequest)WebRequest.Create(@"" + url + "/GetSeatConfirmAfterBooking");



            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        [HttpPost]
        public ResponseModel SeatBook(RequestModel model)
        {
            FlightAPI.BookSeat objseat = new FlightAPI.BookSeat();
            FlightAPI.SeatBookResponse datseatbook = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            string soapResult = "";
            HttpWebRequest request = GetBookSeatURL();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            string Flightsegments = "";
            string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
            objseat = JsonConvert.DeserializeObject<FlightAPI.BookSeat>(dcdata);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(objseat);

            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            Stream requestStream = request.GetRequestStream();
            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();

                }
                datseatbook = JsonConvert.DeserializeObject<FlightAPI.SeatBookResponse>(soapResult);
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FlightAPI.SeatBookResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datseatbook);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;


        }

        public static HttpWebRequest GetAgentCreditBalanceURL()
        {
            var url = Models.TravelModel.Common.FlightAPIDetails.APIURL;
            HttpWebRequest webRequest =
                   (HttpWebRequest)WebRequest.Create(@"" + url + "/GetAgentCreditBalance");



            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }

        [HttpPost]
        public ResponseModel GetAgentCrediitBallance(RequestModel model)
        {
            RechargeAPI.Response datalist = null;
            Models.TravelModel.Common.AgentCreditBalance obj = new Models.TravelModel.Common.AgentCreditBalance();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            try
            {

                string soapResult = "";
                HttpWebRequest request = GetAgentCreditBalanceURL();
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                obj = JsonConvert.DeserializeObject<Models.TravelModel.Common.AgentCreditBalance>(dcdata);
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                byte[] postBytes = Encoding.UTF8.GetBytes(json);
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }
                    datalist = JsonConvert.DeserializeObject<RechargeAPI.Response>(soapResult);
                }

            }
            catch (Exception ex)
            {
                datalist.ResponseStatus = ex.Message;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RechargeAPI.Response));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

        [HttpPost]
        public ResponseModel GetPartialCancellation(RequestModel model)
        {
            FlightAPI.PartialCancel obj = new FlightAPI.PartialCancel();
            FlightAPI.PartialCancelResponse datalist = null;
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            string soapResult = "";
            HttpWebRequest request = GetPartialCancellationURL();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
            obj = JsonConvert.DeserializeObject<FlightAPI.PartialCancel>(dcdata);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            Stream requestStream = request.GetRequestStream();
            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();

                }
                datalist = JsonConvert.DeserializeObject<FlightAPI.PartialCancelResponse>(soapResult);


            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FlightAPI.PartialCancelResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, datalist);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;

        }

        #region Get Wallet Balance
        [HttpPost]
        public APIResponse1 GetWalletBalance(GetWalletAount objGetWalletAount)
        {
            APIResponse1 objresponse = new APIResponse1();
            DataSet ds = objGetWalletAount.GetAmount();
            try
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    objresponse.Balance = ds.Tables[0].Rows[0]["WalletBalance"].ToString();
                }
                else if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    objresponse.Balance = "Insufficient Balance";
                }
                else
                {
                    objresponse.Balance = "Error";
                }
            }
            catch (Exception ex)
            {
                objresponse.Balance = "Error";
            }
            return objresponse;
            //return Json(objresponse.Response,JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        public ResponseModel GetBookingHistory(RequestModel model)
        {
            List<BusModel.GetBookingHistorydetail> objGetDepartment = new List<BusModel.GetBookingHistorydetail>();
            BookingParameter objParameters = new BookingParameter();
            BusModel.BookingStatus objDepartmentStatus = new BusModel.BookingStatus();
            ResponseModel returnResponse = new ResponseModel();
            string EncryptedText = "";
            if (model != null)
            {
                try
                {
                    string dcdata = AesEncryptDecrypt.DecryptString(Aeskey, model.body);
                    objParameters = JsonConvert.DeserializeObject<BookingParameter>(dcdata);
                    DataSet dsResult = objParameters.BookingHis();
                    try
                    {
                        if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                        {
                            #region User Status
                            foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                            {
                                objDepartmentStatus.Response = "Success";
                                objDepartmentStatus.GetBookingHistorydetail = objGetDepartment;
                            }
                            #endregion

                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objGetDepartment.Add(new BusModel.GetBookingHistorydetail

                                {
                                    Remarks = row1["Remarks"].ToString(),
                                    SeatNo = row1["SeatNo"].ToString(),
                                    SeatAmt = row1["SeatAmt"].ToString(),
                                    SeatCode = row1["SeatCode"].ToString(),
                                    PassengerId = row1["PassengerId"].ToString(),
                                    Pk_FlightResponseId = row1["Pk_FlightResponseId"].ToString(),
                                    HermesPNR = row1["HermesPNR"].ToString(),
                                    AirlineName = row1["AirlineName"].ToString(),
                                    AirlinePNR = row1["AirlinePNR"].ToString(),
                                    BookingType = row1["BookingType"].ToString(),
                                    Origin = row1["Origin"].ToString(),
                                    Destination = row1["Destination"].ToString(),
                                    BoookingDate = row1["BoookingDate"].ToString(),
                                    AirlineCode = row1["AirlineCode"].ToString(),
                                    TerminalName = row1["TerminalName"].ToString(),
                                    TotalAMount = row1["TotalAMount"].ToString(),
                                    DepartingDate = row1["DepartingDate"].ToString(),
                                });
                            }
                        }
                        else
                        {
                            objDepartmentStatus.Response = "Data Not Found";
                        }

                    }
                    catch (Exception ex)
                    {
                        objDepartmentStatus.Response = "Error";
                    }
                }
                catch (Exception ex)
                {
                    objDepartmentStatus.Response = "Error";
                }
            }
            else
            {
                objDepartmentStatus.Response = "Data Not Found";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(BusModel.BookingStatus));
            ms = new MemoryStream();
            js.WriteObject(ms, objDepartmentStatus);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;

        }

        #endregion
        #region Get Cancellation Policy Input
        [HttpGet]
        public ResponseModel GetCancellationPolicyInput(string UserTrackId)
        {
            UserTrackId = UserTrackId.Replace(" ", "+");
            UserTrackId = AesEncryptDecrypt.DecryptString(Aeskey, UserTrackId);
            string EncryptedText = "";

            ResponseModel objApiResponse = new ResponseModel();
            History.CancellationPolicyInput obj = new History.CancellationPolicyInput();
            CustomerDb db = new CustomerDb();
            try
            {

                obj = db.GetCancellationPolicyInput(UserTrackId);
                if (obj != null)
                {
                    obj.ResponseStatus = "1";
                }
                else
                {
                    obj.ResponseStatus = "0";
                }
            }

            catch (Exception ex)
            {
                obj.ResponseStatus = "0";
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(History.CancellationPolicyInput));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = AesEncryptDecrypt.EncryptString(Aeskey, CustData);
            objApiResponse.body = EncryptedText;
            return objApiResponse;
        }
        #endregion

    }
}
