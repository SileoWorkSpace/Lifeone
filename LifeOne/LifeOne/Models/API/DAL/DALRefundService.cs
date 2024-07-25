using AesEncryption;
using LifeOne.Models.BillAvenueUtility.Entity;
using Newtonsoft.Json;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace LifeOne.Models.API.DAL
{
    public class DALRefundService : DbContext
    {
        public DALRefundService() : base("name=ConnectionString")
        {
            this.Database.CommandTimeout = 180;
        }

        public ResultSet WalletRefundAmount(decimal WalletAmount, long MemberId, string OrderNo, string TransactionNo)
        {
            var sqlparams = new SqlParameter[] {
                    new SqlParameter {ParameterName="@WalletAmount",Value=WalletAmount},
                    new SqlParameter {ParameterName="@MemId",Value=MemberId},
                    new SqlParameter {ParameterName="@OrderNo",Value=OrderNo},
                    new SqlParameter {ParameterName="@TransactionNo",Value=TransactionNo}
            };
            var _proc = @"Proc_WalletRefundAmount @WalletAmount,@MemId,@OrderNo,@TransactionNo";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public ResultSet CashBackRefundAmount(decimal CashBackAmount, long MemberId, string OrderNo, string TransactionNo)
        {
            var sqlparams = new SqlParameter[] {

                    new SqlParameter {ParameterName="@CashBackAmount",Value=CashBackAmount},
                    new SqlParameter {ParameterName="@MemId",Value=MemberId},
                    new SqlParameter {ParameterName="@OrderNo",Value=OrderNo},
                    new SqlParameter {ParameterName="@TransactionNo",Value=TransactionNo}
            };
            var _proc = @"Proc_CashBackRefundAmount @CashBackAmount,@MemId,@OrderNo,@TransactionNo";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public AdminManagement.AEntity.MRazorPayRefundResponse GetRazorPayRefund(decimal Amount, string paymentId)
        {
            AdminManagement.AEntity.MRazorPayRefundResponse model = new AdminManagement.AEntity.MRazorPayRefundResponse();
            try
            {
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                int amt = (int)(Amount * 100.00M);

                string key = ConfigurationManager.AppSettings["RazorKey"].ToString();
                string secret = ConfigurationManager.AppSettings["RazorSecret"].ToString();

                RazorpayClient client = new RazorpayClient(key, secret);

                Payment payment = client.Payment.Fetch(paymentId);

                Dictionary<string, object> input = new Dictionary<string, object>();
                input.Add("amount", amt);
                Refund refund = payment.Refund(input);
                model.entity = Convert.ToString(refund.Attributes["entity"]);
                model.id = Convert.ToString(refund.Attributes["id"]);
                model.payment_id = Convert.ToString(refund.Attributes["payment_id"]);
                model.amount = Convert.ToString(refund.Attributes["amount"]);
                model.code = "1";

            }
            catch (Exception ex)
            {
                model.code = "0";
                model.Msg = ex.Message;
            }

            return model;
        }

        public ResultSet GateWayRefundAmount(string RefundId, string PaymentId, long Fk_MemId, int Amount, string OrderNo)
        {
            var sqlparams = new SqlParameter[] {

                    new SqlParameter {ParameterName="@RefundId",Value=RefundId},
                    new SqlParameter {ParameterName="@PaymentId",Value=PaymentId},
                    new SqlParameter {ParameterName="@MemId",Value=Fk_MemId},
                    new SqlParameter {ParameterName="@Amount",Value=Amount},
                    new SqlParameter {ParameterName="@OrderNo",Value=OrderNo}
            };
            var _proc = @"Proc_GateWayPaymentRefund @RefundId,@PaymentId,@MemId,@Amount,@OrderNo";
            var res = this.Database.SqlQuery<ResultSet>(_proc, sqlparams).FirstOrDefault();
            return res;
        }

        public string sendMShoppingCancelOrder(string url, string Method, string body)
        {
            string responseText = "0";
            try
            {
                string MBaseUrl = "https://shop.LifeOne.com/api/";
                string AeskeyM = ConfigurationManager.AppSettings["Aeskey"].ToString();
                string _modaldata = Crypto.Encryption(AeskeyM, body);
                RequestModel requestModel = new RequestModel();
                requestModel.body = _modaldata;

                var client = new HttpClient();
                client.BaseAddress = new Uri(MBaseUrl);
                client.DefaultRequestHeaders.Add("contentType", "application/json");

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                //HTTP POST
                var responseTask = client.PostAsJsonAsync(url, requestModel);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    string response = result.Content.ReadAsStringAsync().Result;
                    ResponseModel respomodel = JsonConvert.DeserializeObject<ResponseModel>(response);
                    //Decryption
                    string _decryptdeserializeresponse = ApiEncrypt_Decrypt.DecryptString(AeskeyM, respomodel.body);
                    //final reponse
                    MagentoResponseModel finalresponse = JsonConvert.DeserializeObject<MagentoResponseModel>(_decryptdeserializeresponse);
                    if (finalresponse.response == "success")
                    {
                        responseText = finalresponse.message;
                    }
                }
                return responseText;
            }
            catch (Exception e)
            {
                return responseText;
            }

        }
    }
}