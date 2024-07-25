using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class EmailSendService
    {

        public List<MEmailSend> GetListForSendEmail(MEmailSend _obj)
        {
            List<MEmailSend> _objResponseModel = new List<MEmailSend>();
            try
            {
                _objResponseModel = DALEmailSend.GetData(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public List<MEmailSend> GetDetailsForSendEmail(MEmailSend _obj)
        {
            List<MEmailSend> _objResponseModel = new List<MEmailSend>();
            try
            {
                _objResponseModel = DALEmailSend.GetDatails(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public EmailSendResponse UpdateEmail(MEmailSend _obj)
        {
            EmailSendResponse _objResponseModel = new EmailSendResponse();
            try
            {
                _objResponseModel = DALEmailSend.UpdateEmailDetails(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}