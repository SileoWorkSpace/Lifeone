using AesEncryption;
using LifeOne.Models.API;
using LifeOne.Models.API.DAL;
using LifeOne.Models.BillAvenueUtility.Entity;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Web.Http;

namespace LifeOne.Controllers
{
    public class ShiprocketController : ApiController
    {
        string Aeskey = ConfigurationManager.AppSettings["Aeskey"].ToString();

               
        //public bool ShiprocketOrderList()
        //{
        //    Root response = new Root();
        //    ShiprocketViewModel model = new ShiprocketViewModel();

        //    bool _result = false;
        //    try
        //    {               
        //        var client = new RestClient("https://apiv2.shiprocket.in/v1/external/orders");
        //        client.Timeout = -1;
        //        var request = new RestRequest(Method.GET);
        //        request.AddHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjEyODU1NzQsImlzcyI6Imh0dHBzOi8vYXBpdjIuc2hpcHJvY2tldC5pbi92MS9leHRlcm5hbC9hdXRoL2xvZ2luIiwiaWF0IjoxNjE3NjI1MDk3LCJleHAiOjE2MTg0ODkwOTcsIm5iZiI6MTYxNzYyNTA5NywianRpIjoibk5OdmVlR0tYUVFOQXFrciJ9.kDEoov9KBaIwO_jjCRHMh80LD_v5sVg54ytzhnCUXu8");
        //        IRestResponse res = client.Execute(request);
        //        response = JsonConvert.DeserializeObject<Root>(res.Content);
        //        model.ShiprocketOrder = "<result>";
        //        model.ShiprocketProductOrder = "<result>";
        //        model.ShiprocketShipments = "<result>";

        //        if (response.data!= null)
        //        {
        //            response.data.ForEach(x =>
        //            {
        //                model.ShiprocketOrder += "<data>" +
        //                                        "<ShiprocketId>" + x.id + "</ShiprocketId>" +
        //                                        "<channel_id>" + x.channel_id + "</channel_id>" +
        //                                        "<channel_name>" + x.channel_name + "</channel_name>" +
        //                                        "<base_channel_code>" + x.base_channel_code + "</base_channel_code>" +
        //                                        "<channel_order_id>" + x.channel_order_id + "</channel_order_id>" +
        //                                        "<customer_name>" + x.customer_name + "</customer_name>" +
        //                                        "<customer_email>" + x.customer_email + "</customer_email>" +
        //                                        "<customer_phone>" + x.customer_phone + "</customer_phone>" +
        //                                        "<customer_address>" + x.customer_address + "</customer_address>" +
        //                                        "<customer_city>" + x.customer_city + "</customer_city>" +
        //                                        "<customer_state>" + x.customer_state + "</customer_state>" +
        //                                        "<customer_pincode>" + x.customer_pincode + "</customer_pincode>" +
        //                                        "<customer_latitude>" + x.customer_latitude + "</customer_latitude>" +
        //                                        "<customer_longitude>" + x.customer_longitude + "</customer_longitude>" +
        //                                        "<customer_alternate_phone>" + x.customer_alternate_phone + "</customer_alternate_phone>" +
        //                                        "<pickup_location>" + x.pickup_location + "</pickup_location>" +


        //                                        "<order_type>" + x.order_type + "</order_type>" +
        //                                        "<payment_status>" + x.payment_status + "</payment_status>" +
        //                                        "<total>" + x.total + "</total>" +
        //                                        "<tax>" + x.tax + "</tax>" +
        //                                        "<sla>" + x.sla + "</sla>" +
        //                                        "<shipping_method>" + x.shipping_method + "</shipping_method>" +
        //                                        "<status>" + x.status + "</status>" +
        //                                        "<status_code>" + x.status_code + "</status_code>" +
        //                                        "<payment_method>" + x.payment_method + "</payment_method>" +
        //                                        "<change_payment_mode>" + x.change_payment_mode + "</change_payment_mode>" +
        //                                        "<is_international>" + x.is_international + "</is_international>" +
        //                                        "<channel_created_at>" + Convert.ToDateTime(x.channel_created_at) + "</channel_created_at>" +
        //                                        "<created_at>" + Convert.ToDateTime(x.created_at) + "</created_at>" +
        //                                        "<updated_at>" + Convert.ToDateTime(x.updated_at) + "</updated_at>";

        //                if (x.others != null)
        //                {
        //                    model.ShiprocketOrder += "<store_id>" + x.others.store_id + "</store_id>" +
        //                                   "<entity_id>" + x.others.entity_id + "</entity_id>" +
        //                                   "<increment_id>" + x.others.increment_id + "</increment_id>" +
        //                                   "<payment_code>" + x.others.payment_code + "</payment_code>" +
        //                                   "<billing_phone>" + x.others.billing_phone + "</billing_phone>" +
        //                                   "<billing_state>" + x.others.billing_state + "</billing_state>" +
        //                                   "<currency_code>" + x.others.currency_code + "</currency_code>" +
        //                                   "<billing_country>" + x.others.billing_country + "</billing_country>" +
        //                                   "<billing_pincode>" + x.others.billing_pincode + "</billing_pincode>" +
        //                                   "<shipping_amount>" + x.others.shipping_amount + "</shipping_amount>" +
        //                                   "<shipping_method>" + x.others.shipping_method + "</shipping_method>" +
        //                                   "<billing_isd_code>" + x.others.billing_isd_code + "</billing_isd_code>" +
        //                                   "<total_qty_ordered>" + x.others.total_qty_ordered + "</total_qty_ordered>" +
        //                                   "<highest_percentage>" + x.others.billing_state + "</highest_percentage>";
        //                }
        //                if (x.pickup_address_detail != null)
        //                {
        //                    model.ShiprocketOrder += "<PickUpID>" + x.pickup_address_detail.id + "</PickUpID>" +
        //                                   "<name>" + x.pickup_address_detail.name + "</name>" +
        //                                   "<address>" + x.pickup_address_detail.address + "</address>" +
        //                                   "<address_2>" + x.pickup_address_detail.address_2 + "</address_2>" +
        //                                   "<city>" + x.pickup_address_detail.city + "</city>" +
        //                                   "<state>" + x.pickup_address_detail.state + "</state>" +
        //                                   "<address_type>" + x.pickup_address_detail.address_type + "</address_type>" +
        //                                   "<pin_code>" + x.pickup_address_detail.pin_code + "</pin_code>" +
        //                                   "<pickup_code>" + x.pickup_address_detail.pickup_code + "</pickup_code>" +
        //                                   "<lat>" + x.pickup_address_detail.lat + "</lat>" +
        //                                   "<long>" + x.pickup_address_detail.state + "</long>";
        //                }

        //                model.ShiprocketOrder += "</data>";

        //                if (x.products.Count > 0)
        //                {
        //                    x.products.ForEach(y =>
        //                    {
        //                        model.ShiprocketProductOrder += "<data>" +
        //                                       "<ShiprocketId>" + x.id + "</ShiprocketId>" +
        //                                       "<PId>" + y.id + "</PId>" +
        //                                       "<product_id>" + y.product_id + "</product_id>" +
        //                                       "<channel_order_product_id>" + y.product_id + "</channel_order_product_id>" +
        //                                       "<name>" + y.product_id + "</name>" +
        //                                       "<channel_sku>" + y.channel_sku + "</channel_sku>" +
        //                                       "<quantity>" + y.quantity + "</quantity>" +
        //                                       "<available>" + y.available + "</available>" +
        //                                       "<status>" + y.status + "</status>" +
        //                                       "<price>" + y.price + "</price>" +
        //                                       "<product_cost>" + y.product_cost + "</product_cost>" +
        //                                       "<hsn>" + y.hsn + "</hsn>" +
        //                                        "</data>";
        //                    });
        //                }

        //                if (x.shipments.Count > 0)
        //                {
        //                    x.shipments.ForEach(y =>
        //                    {
        //                        model.ShiprocketShipments += "<data>" +
        //                                       "<ShiprocketId>" + x.id + "</ShiprocketId>" +
        //                                       "<ShipmentID>" + y.id + "</ShipmentID>" +
        //                                       "<isd_code>" + y.isd_code + "</isd_code>" +
        //                                       "<courier>" + y.courier + "</courier>" +
        //                                       "<courier_id>" + y.courier_id + "</courier_id>" +
        //                                       "<shipping_charges>" + y.shipping_charges + "</shipping_charges>" +
        //                                       "<weight>" + y.weight + "</weight>" +
        //                                       "<dimensions>" + y.dimensions + "</dimensions>" +
        //                                       "<shipped_date>" + y.shipped_date + "</shipped_date>" +
        //                                       "<pickup_scheduled_date>" + y.pickup_scheduled_date + "</pickup_scheduled_date>" +
        //                                       "<pickup_token_number>" + y.pickup_token_number + "</pickup_token_number>" +
        //                                        "</data>";
        //                    });
        //                }

        //            });

        //            model.ShiprocketOrder += "</result>";
        //            model.ShiprocketProductOrder += "</result>";
        //            model.ShiprocketShipments += "</result>";

        //            var dataRes = DALShiprocket.ShiprocketOrderList(model);
        //            _result = true;
        //        }               
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = false;
        //        throw ex;
        //    }
        //    return _result;
        //}
        
        //[HttpPost]
        //public HttpResponseMessage Cancel_ShiprocketOrder(RequestModel req)
        //{
        //    ResponseModel resmodel = new ResponseModel();
        //    CancelOrderModel cancelOrderModel = new CancelOrderModel();
        //    string EncryptedText = "";
        //    try
        //    {
        //        if (ShiprocketOrderList())
        //        {

        //            /*encrpt and Decrypt order id here */
        //            Data _decyptOrderId = JsonConvert.DeserializeObject<Data>(Crypto.Decryption(Aeskey, req.body));
        //            string _getOrderID = Crypto.Decryption(Aeskey, _decyptOrderId.order_id);
        //            _decyptOrderId.order_id = _getOrderID;
        //            string _sernalizeObject = JsonConvert.SerializeObject(_decyptOrderId);
        //            string bodyResponse= Crypto.Encryption(Aeskey, _sernalizeObject);

        //            //string _getToken = RturnToken();
        //            var client = new RestClient("https://shop.LifeOne.com/api/shiprocket_id.php");
        //            client.Timeout = -1;
        //            var request = new RestRequest(Method.POST);
        //            request.AddHeader("Content-Type", "application/json");
        //            //request.AddParameter("application/json", "{\r\n    \"body\":\"" + req.body + "\"\r\n}", ParameterType.RequestBody);
        //            request.AddParameter("application/json", "{\r\n    \"body\":\"" + bodyResponse + "\"\r\n}", ParameterType.RequestBody);
        //            IRestResponse response = client.Execute(request);
        //            resmodel = JsonConvert.DeserializeObject<ResponseModel>(response.Content);
        //            OrderReqModel orderId = JsonConvert.DeserializeObject<OrderReqModel>(Crypto.Decryption(Aeskey, resmodel.body));
        //            var shiprocketID = DALShiprocket.GetShiprocketOrderID(orderId.data.order_id);

        //            if (shiprocketID != null && shiprocketID.ShiprocketId > 0)
        //            {
        //                client = new RestClient("https://apiv2.shiprocket.in/v1/external/orders/cancel");
        //                client.Timeout = -1;
        //                request = new RestRequest(Method.POST);
        //                request.AddHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOjEyODU1NzQsImlzcyI6Imh0dHBzOi8vYXBpdjIuc2hpcHJvY2tldC5pbi92MS9leHRlcm5hbC9hdXRoL2xvZ2luIiwiaWF0IjoxNjE3NjI1MDk3LCJleHAiOjE2MTg0ODkwOTcsIm5iZiI6MTYxNzYyNTA5NywianRpIjoibk5OdmVlR0tYUVFOQXFrciJ9.kDEoov9KBaIwO_jjCRHMh80LD_v5sVg54ytzhnCUXu8");
        //                request.AddHeader("Content-Type", "application/json");
        //                request.AddParameter("application/json", "{\r\n  \"ids\": [" + shiprocketID.ShiprocketId + "]\r\n}", ParameterType.RequestBody);
        //                IRestResponse response1 = client.Execute(request);
        //                cancelOrderModel = JsonConvert.DeserializeObject<CancelOrderModel>(response1.Content);
        //            }
        //            else
        //            {
        //                cancelOrderModel.message = "Order cancelled successfully.";
        //                cancelOrderModel.status_code = 200;
        //            }
        //        }
        //        else
        //        {
        //            cancelOrderModel.message = "please try again later";
        //            cancelOrderModel.status_code = 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        cancelOrderModel.message = ex.Message;
        //    }
        //    string CustData = "";
        //    DataContractJsonSerializer js;
        //    MemoryStream ms;
        //    js = new DataContractJsonSerializer(typeof(CancelOrderModel));
        //    ms = new MemoryStream();
        //    js.WriteObject(ms, cancelOrderModel);
        //    ms.Position = 0;
        //    StreamReader sr = new StreamReader(ms);
        //    CustData = sr.ReadToEnd();
        //    sr.Close();
        //    ms.Close();
        //    EncryptedText = Crypto.Encryption(Aeskey, CustData);
        //    resmodel.body = EncryptedText;
        //    return Request.CreateResponse(HttpStatusCode.OK, resmodel);
        //}
        
        //[HttpPost]
        //public HttpResponseMessage Return_ShiprocketOrder(RequestModel req)
        //{
        //    ResponseModel resmodel = new ResponseModel();
        //    CancelOrderModel cancelOrderModel = new CancelOrderModel();
        //    //string EncryptedText = "";
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        cancelOrderModel.message = ex.Message;
        //    }
        //    //string CustData = "";
        //    //DataContractJsonSerializer js;
        //    //MemoryStream ms;
        //    //js = new DataContractJsonSerializer(typeof(CancelOrderModel));
        //    //ms = new MemoryStream();
        //    //js.WriteObject(ms, cancelOrderModel);
        //    //ms.Position = 0;
        //    //StreamReader sr = new StreamReader(ms);
        //    //CustData = sr.ReadToEnd();
        //    //sr.Close();
        //    //ms.Close();
        //    //EncryptedText = Crypto.Encryption(Aeskey, CustData);
        //    //resmodel.body = EncryptedText;
        //    return Request.CreateResponse(HttpStatusCode.OK, resmodel);
        //}


        //public string RturnToken()
        //{
        //    try
        //    {
        //        string _token = "";
        //        var client = new RestClient("https://apiv2.shiprocket.in/v1/external/auth/login");
        //        client.Timeout = -1;
        //        var request = new RestRequest(Method.POST);
        //        request.AddHeader("Content-Type", "application/json");
        //        request.AddParameter("application/json", "{\r\n\"email\": \"shivammishra@quaeretech.com\",\r\n\"password\": \"Quaere321@\"\r\n}", ParameterType.RequestBody);
        //        IRestResponse response = client.Execute(request);
        //        RootTken Finalresponse = JsonConvert.DeserializeObject<RootTken>(response.Content);
        //        return Finalresponse.token;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }      
        //}
    }

    public class RootTken
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public int company_id { get; set; }
        public string created_at { get; set; }
        public string token { get; set; }
    }

}
