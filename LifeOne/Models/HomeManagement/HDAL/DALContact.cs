using Dapper;
using LifeOne.Models.HomeManagement.HEntity;
using LifeOne.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.HomeManagement.HDAL
{
    public class DALContact
    {
        public static ContactResponse Contact(Contact _param)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Name", _param.Name);
                queryParameters.Add("@mobile", _param.Mobile);
                queryParameters.Add("@DistributorID", _param.DistributorID);
                queryParameters.Add("@Email", _param.Email);
                queryParameters.Add("@Subject", _param.Grievance);
                queryParameters.Add("@Message", _param.Message);
                ContactResponse _iresult = DBHelperDapper.DAAddAndReturnModel<ContactResponse>("SaveEnquiry", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}