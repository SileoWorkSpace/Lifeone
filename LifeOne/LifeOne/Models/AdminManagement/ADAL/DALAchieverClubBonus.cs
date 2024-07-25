using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALAchieverClubBonus
    {

        public static List<MAchieverClubBonusMaster> GetData(MAchieverClubBonusMaster obj)
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
                List<MAchieverClubBonusMaster> _iresult = DBHelperDapper.DAAddAndReturnModelList<MAchieverClubBonusMaster>("dbo.GetAchieverClubBonusData", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static AchieverClubBonusMasterResponse SaveAchieverClubBonusMaster(MAchieverClubBonusMaster obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();


                queryParameters.Add("@PerformanceLevel", obj.PerformanceLevel ?? string.Empty);
                queryParameters.Add("@SeminarClub", obj.SeminarClub);
                queryParameters.Add("@CarClub", obj.CarClub);
                queryParameters.Add("@TravelClub", obj.TravelClub);
                queryParameters.Add("@ProvidentClub", obj.ProvidentClub);
                queryParameters.Add("@HouseClub", obj.HouseClub);
                queryParameters.Add("@UnderLevelCount", obj.UnderLevelCount);
                queryParameters.Add("@BlueDiamondClub", obj.BlueDiamondClub);
                queryParameters.Add("@CrownAmbassadorClub", obj.CrownAmbassadorClub);
                queryParameters.Add("@PresidentClub", obj.PresidentClub);
                queryParameters.Add("@LevelPercentage", obj.LevelPercentage);
                queryParameters.Add("@AdditionalCriteria", obj.AdditionalCriteria);
                queryParameters.Add("@UnderLevelID", obj.UnderLevelID);
                queryParameters.Add("@ProcId", obj.ProcId);
                queryParameters.Add("@PK_LevelId", obj.PK_LevelId);
                AchieverClubBonusMasterResponse _iresult = DBHelperDapper.DAAddAndReturnModel<AchieverClubBonusMasterResponse>("SaveUpdateAchieverClubBonusMasterData", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}