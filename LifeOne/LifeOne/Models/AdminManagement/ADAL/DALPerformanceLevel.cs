using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALPerformanceLevel
    {
        public static List<MPerformaneLevel> GetData(MPerformaneLevel obj)
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

                queryParameters.Add("@PK_LevelId", obj.PK_LevelId);
                List<MPerformaneLevel> _iresult = DBHelperDapper.DAAddAndReturnModelList<MPerformaneLevel>("GetPerformaneLevelData", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static ResponsePerformaneLevelModel SavePerformaneLevel(MPerformaneLevel obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();


                queryParameters.Add("@PerformanceLevel", obj.PerformanceLevel??string.Empty);
                queryParameters.Add("@TargetPVFrom", obj.TargetPVFrom);
                queryParameters.Add("@TargetPVTo", obj.TargetPVTo);
                queryParameters.Add("@LevelPercentage", obj.LevelPercentage);
                queryParameters.Add("@AdditionalCriteria", obj.AdditionalCriteria ?? string.Empty);
                queryParameters.Add("@UnderLevelID", obj.UnderLevelID);
                queryParameters.Add("@UnderLevelCount", obj.UnderLevelCount);
                queryParameters.Add("@UnderLevelBusiness", obj.UnderLevelBusiness);
                queryParameters.Add("@ProcId", obj.ProcId);
                queryParameters.Add("@PK_LevelId", obj.PK_LevelId);
                ResponsePerformaneLevelModel _iresult = DBHelperDapper.DAAddAndReturnModel<ResponsePerformaneLevelModel>("SaveUpdatePerformaneLevelData", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}