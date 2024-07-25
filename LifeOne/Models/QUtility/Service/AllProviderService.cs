using LifeOne.Models.QUtility.API;
using LifeOne.Models.QUtility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.QUtility.Service
{
    public class AllProviderService
    {
        public ResponseModel GetAllproviderdetails(RequestModel _model)
        {
            try
            {               
                string _url = "GetAllProvider";   //BaseUrl+API URL                       
                ResponseModel _rsponsedetails = GenericAPI.sendRequestToUtilityAPIPost(_model, _url);
                return _rsponsedetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}