using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALNews
    {
        public static List<MNews> GetData(MNews obj)
        {
            try
            {
                if (obj.Page == 0)
                {
                    obj.Page = 1;
                }
                if (obj.Size == 0)
                {
                    obj.Size = 100;
                }
                var queryParameters = new DynamicParameters();

                queryParameters.Add("@Pk_NewsId", obj.Pk_NewsId);
                List<MNews> _iresult = DBHelperDapper.DAAddAndReturnModelList<MNews>("GetNews", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponseNewsModel SaveNews(MNews obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();

              
                queryParameters.Add("@News", obj.News);
                queryParameters.Add("@NewsHeading", obj.NewsHeading);
                queryParameters.Add("@NewsPreference", obj.NewsPreference);
                queryParameters.Add("@InfoImgUrl", obj.InfoImgUrl);
                queryParameters.Add("@InfoLink", obj.InfoLink); 
                queryParameters.Add("@CreatedBy", obj.CreatedBy);
                queryParameters.Add("@ProcId", obj.ProcId);
                queryParameters.Add("@Pk_NewsId", obj.Pk_NewsId);
                queryParameters.Add("@AssignTo", obj.AssignToValue);
                ResponseNewsModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponseNewsModel>("News", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}