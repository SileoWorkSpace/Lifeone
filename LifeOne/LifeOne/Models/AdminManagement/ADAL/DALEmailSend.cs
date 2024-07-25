using Dapper;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALEmailSend
    {

        public static List<MEmailSend> GetData(MEmailSend obj)
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

                List<MEmailSend> _iresult = DBHelperDapper.DAAddAndReturnModelList<MEmailSend>("dbo.SendEmailUserList", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static List<MEmailSend> GetDatails(MEmailSend obj)
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
                List<MEmailSend> _iresult = DBHelperDapper.DAAddAndReturnModelList<MEmailSend>("dbo.SendEmailUserDetails", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static EmailSendResponse UpdateEmailDetails(MEmailSend obj)
        {
            try
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Fk_MemId", obj.FK_MemId);
                EmailSendResponse _iresult = DBHelperDapper.DAAddAndReturnModel<EmailSendResponse>("dbo.UpdateEmailStatus", queryParameters);
                return _iresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}