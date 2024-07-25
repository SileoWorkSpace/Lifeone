using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALSendSMS
    {
        public static List<MSendSMS> GetData(MSendSMS obj)
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

              
                List<MSendSMS> _iresult = DBHelperDapper.DAAddAndReturnModelList<MSendSMS>("dbo.SendSMSUserList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<MSendSMS> GetDatails(MSendSMS obj)
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

                queryParameters.Add("@Fk_MemId", obj.FK_MemId);
                List<MSendSMS> _iresult = DBHelperDapper.DAAddAndReturnModelList<MSendSMS>("dbo.SendSMSUserDetails", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static SMSResponse UpdateSMSDetails(MSendSMS obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Fk_MemId", obj.FK_MemId);
                SMSResponse _iresult = DBHelperDapper.DAAddAndReturnModel<SMSResponse>("dbo.UpdateSmsStatus", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}