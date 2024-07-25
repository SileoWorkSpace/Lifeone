using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class NewsService
    {
        ~NewsService() { }

        public List<MNews> GetNewsService(MNews _obj)
        {
            List<MNews> _objResponseModel = new List<MNews>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALNews.GetData(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponseNewsModel SaveNewsService(MNews _obj)
        {
            ResponseNewsModel _objResponseModel = new ResponseNewsModel();
            try
            {
                _objResponseModel = DALNews.SaveNews(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}