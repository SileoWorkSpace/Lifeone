using System;

using LifeOne.Models.TravelModel.Entity;
using LifeOne.Models.TravelModel.API;
using LifeOne.Models.TravelModel.DAL;
using System.Data;
using LifeOne.Models.API.DAL;
using LifeOne.Models.TravelModel;
using LifeOne.Models.API;


namespace LifeOne.Models.TravelModel.Service
{
    public class CancellationPolicyInputService
    {
        public ResponseModel CancellationPolicyInputAPIService(string _userId)
        {
            DALCommon _objDALCommon = new DALCommon();
            try
            {
                History.CancellationPolicyInput obj = new History.CancellationPolicyInput();
                CustomerDb db = new CustomerDb();
                string _dateresponse = ApiEncrypt_Decrypt.DecryptString("Utility@#@132XYZ", _userId);
                History.CancellationPolicyInput objDepartmentStatus = new History.CancellationPolicyInput();
                                
                try
                {
                    obj = db.GetCancellationPolicyInput(_dateresponse);
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
                    obj.ResponseStatus = "Error";
                }

                /*encrypt and return API response*/
                ResponseModel __rsponsedetails = new ResponseModel();
                __rsponsedetails.body = DALCommon.CustomSerializeObjectWithEncryptString<History.CancellationPolicyInput>(obj);
                return __rsponsedetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}