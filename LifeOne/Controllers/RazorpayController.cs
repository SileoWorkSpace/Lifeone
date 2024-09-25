using iTextSharp.text.pdf;
using LifeOne.Models.QUtility.Entity;
using Newtonsoft.Json;
using Razorpay.Api;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System;
using System.Web.Http;
using LifeOne.Models;
using System.Configuration;


namespace LifeOne.Controllers
{
    public class RazorpayController : ApiController
    {
        string AESKEY = CommonRazorPay.AESKEY;
        string Key = CommonRazorPay.Key;
        string SecretKey = CommonRazorPay.SecretKey;

        [HttpPost]

        public ResponseModel CreateRazorPayOrder(ResponseModel requestModel)
        {
            string OrderId = "", Status = "";
            string EncryptedText = "";
            RazCreateOrderResponse objres = new RazCreateOrderResponse();
            Razorpay.Api.Order objorder = null;
            ResponseModel returnResponse = new ResponseModel();
            try
            {

                string dcdata = ApiEncrypt_Decrypt.DecryptString(AESKEY, requestModel.body);
                RazCreateOrder createOrder = JsonConvert.DeserializeObject<RazCreateOrder>(dcdata);



                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                RazorpayClient client = null;

                client = new RazorpayClient(Key, SecretKey);



                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", createOrder.TotalAmount * 100);
                options.Add("receipt", "");
                options.Add("currency", "INR");
                options.Add("payment_capture", 1);
                objorder = client.Order.Create(options);
                OrderId = objorder["id"].ToString();
                createOrder.OrderId = OrderId;
                createOrder.Request = dcdata;
                DataSet dataSet = createOrder.CreateRazOrder();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            RazCreateOrderRes razCreateOrderRes = new RazCreateOrderRes();
                            razCreateOrderRes.OrderId = OrderId;
                            objres.Response = razCreateOrderRes;
                            objres.Message = "Order Created Successfully";
                            objres.Status = 1;
                        }
                        else
                        {
                            objres.Message = dataSet.Tables[0].Rows[0]["Message"].ToString();

                            objres.Status = 0;
                           
                        }
                    }
                    else
                    {
                        objres.Message = "Problem in Connection";
                        objres.Status = 0;
                    }
                }
                else
                {
                    objres.Message = "Problem in Connection";
                    objres.Status = 0;
                }

            }
            catch (Exception ex)
            {
                objres.Message = "Problem in Connection";
                objres.Status = 0;
            }
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RazCreateOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, objres);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(AESKEY, CustData);
            returnResponse.body = EncryptedText;
            return returnResponse;
        }

    }
}
