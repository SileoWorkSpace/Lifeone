using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class AchieverClubBonusService
    {
        ~AchieverClubBonusService() { }

        public List<MAchieverClubBonusMaster> GetAchieverClubBonusService(MAchieverClubBonusMaster _obj)
        {
            List<MAchieverClubBonusMaster> _objResponseModel = new List<MAchieverClubBonusMaster>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALAchieverClubBonus.GetData(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
        public AchieverClubBonusMasterResponse SaveAchieverClubBonusService(MAchieverClubBonusMaster _obj)
        {
            AchieverClubBonusMasterResponse _objResponseModel = new AchieverClubBonusMasterResponse();
            try
            {
                _objResponseModel = DALAchieverClubBonus.SaveAchieverClubBonusMaster(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}