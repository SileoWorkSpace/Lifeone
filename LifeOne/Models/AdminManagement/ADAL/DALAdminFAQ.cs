using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
   
    public class DALAdminFAQ
    {
        public static List<MAdminFAQ> GetFAQData(MAdminFAQ obj)
        {
            try
            {
                if(obj.Page==0)
                {
                    obj.Page = 1;
                }
                if(obj.Size==0)
                {
                    obj.Size = 100;
                }
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Pk_FAQId", obj.Pk_FAQId);                             
                queryParameters.Add("@Page", obj.Page);
                queryParameters.Add("@Size", obj.Size);                
                List<MAdminFAQ> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAdminFAQ>("GetFAQDetails", queryParameters);               
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseFAQModel UploadFAQ(MAdminFAQ obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Pk_FAQId", obj.Pk_FAQId);
                queryParameters.Add("@Question", obj.Question);
                queryParameters.Add("@Answer", obj.Answer);
                queryParameters.Add("@CreatedBy", obj.CreatedBy);
                queryParameters.Add("@ProcId", obj.ProcId);
                ResponseFAQModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseFAQModel>("UploadFAQ", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}