using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.AdminManagement.ADAL;
namespace LifeOne.Models.AdminManagement.AService
{
    public class UplineService
    {
        public List< MUpline> uplinesDataService(MUpline _obj)
        {
            List<MUpline> _objResponseModel = new List<MUpline>();
            try
            {
                _objResponseModel =  DALUpline.DalUpline(_obj).ToList();
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}