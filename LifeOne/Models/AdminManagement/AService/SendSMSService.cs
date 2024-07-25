using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AdminManagement.AEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.AService
{
    public class SendSMSService
    {
        public List<MSendSMS> GetListForSendSMS(MSendSMS _obj)
        {
            List<MSendSMS> _objResponseModel = new List<MSendSMS>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALSendSMS.GetData(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public List<MSendSMS> GetDetailsForSendSMS(MSendSMS _obj)
        {
            List<MSendSMS> _objResponseModel = new List<MSendSMS>();
            try
            {
                //decrypt app team reponse

                _objResponseModel = DALSendSMS.GetDatails(_obj);


                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public SMSResponse UpdateSms(MSendSMS _obj)
        {
            SMSResponse _objResponseModel = new SMSResponse();
            try
            {
                _objResponseModel = DALSendSMS.UpdateSMSDetails(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}