using LifeOne.Models.QUtility.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace LifeOne.Models.API
{
    public class Registration
    {

        public string PlaceUnderId { get; set; }
        public string Leg { get; set; }
        public string InvitedBy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string Normalpassword { get; set; }
        public string Email { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Tehsil { get; set; }
        public bool Iskyc { get; set; }
        public string AadharNo { get; set; }
        public string Address { get; set; }
        public string husbandOrFatherNamesuffix { get; set; }
        public string DOB { get; set; }
        public string YearOfBirth { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public long Fk_memId { get; set; }

        public string KaryonLoginId { get; set; }
        public string SponsorBy { get; set; }

        //Pan and voterid
        public string Type { get; set; }
        public string PanNo { get; set; }
        public string PanImage { get; set; }
        public string VoterId { get; set; }
        public string VoterIdImage { get; set; }
        //---------Add more for kyc------------------
        public string GSTNo { get; set; }
        public string GSTImage { get; set; }
        public string UdyogAadharNo { get; set; }
        public string UdyogAadharImage { get; set; }
        public string CINNo { get; set; }
        public string CINImage { get; set; }
        //-----------------------------------------
        public string AccountType { get; set; }
        public string OTP { get;  set; }
        public string Flag { get;  set; }
    }
    public class RegistrationResponse
    {
        public string response { get; set; }
        public string message { get; set; }
        public bool Iskyc { get; set; }
    }


    public class MShoppingRegistration
    {
        public string fistname { get; set; }
        public string lastname { get; set; }
        public string mobile { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string postcode { get; set; }
        public string login_id { get; set; }
        public string fk_id { get; set; }
        public string sendMShoppingRegistration(string url, string Method, string body, string Token)
        {
            string responseText = "0";
            try
            {
                string ICashCardBaseUrl = ConfigurationManager.AppSettings["MShoppingAPI"].ToString();
                string AeskeyM = ConfigurationManager.AppSettings["Aeskey"].ToString();
                string _modaldata = ApiEncrypt_Decrypt.EncryptString(AeskeyM, body);
                RequestModel requestModel = new RequestModel();
                requestModel.body = _modaldata;

                var client = new HttpClient();
                client.BaseAddress = new Uri(ICashCardBaseUrl);
                client.DefaultRequestHeaders.Add("contentType", "application/json");
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
                    if (finalresponse.response == "Success")
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


    public class MagentoResponseModel
    {
        public string response { get; set; }
        public string message { get; set; }

    }
    public class VerifyPanOrVoterId
    {
        public long Fk_MemId { get; set; }
        public string Type { get; set; }
        public string VoterId { get; set; }
        public string VoterImage { get; set; }
        public string PanNo { get; set; }
        public string PanImage { get; set; }
        //---------Add more for kyc------------------
        public string GSTNo { get; set; }
        public string GSTImage { get; set; }
        public string UdyogAadharNo { get; set; }
        public string UdyogAadharImage { get; set; }
        public string CINNo { get; set; }
        public string CINImage { get; set; }
        public string AccountType { get; set; }
    }
    public class VerifyPanOrVoterIdresponse
    {
        public int Flag { get; set; }
        public string Msg { get; set; }
        public string response { get; set; }

    }

    public class MCareRegistration
    {
        public string SponsorCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string ParentCode { get; set; }
        public string EmailId { get; set; }
        public string PinCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Tehsil { get; set; }
        public string Iskyc { get; set; }
        public string AadharNo { get; set; }
        public string Address { get; set; }
        public string FatherName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Leg { get; set; }
        public int CreatedBy { get; set; }
        public string LoginId { get; set; }
        public int IsLifeoneWellnessRegistration { get; set; }

    }
   

}