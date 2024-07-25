using Dapper;
using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class PerformanceLevelService
    {
        ~PerformanceLevelService() { }

        public List<MPerformaneLevel> GetPerformanceLevelService(MPerformaneLevel _obj)
        {
            List<MPerformaneLevel> _objResponseModel = new List<MPerformaneLevel>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALPerformanceLevel.GetData(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public ResponsePerformaneLevelModel SavePerformanceLevelService(MPerformaneLevel _obj)
        {
            ResponsePerformaneLevelModel _objResponseModel = new ResponsePerformaneLevelModel();
            try
            {
                _objResponseModel = DALPerformanceLevel.SavePerformaneLevel(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}