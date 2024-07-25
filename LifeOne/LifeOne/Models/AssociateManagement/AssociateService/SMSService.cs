using LifeOne.Models.AssociateManagement.AssociateDAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateService
{
    public class SMSService
    {

        public SMSResponse SaveSMS(string xmldata)
        {
            SMSResponse _objResponseModel = new SMSResponse();
            try
            {
                _objResponseModel = DALSMS.SaveSMS(xmldata);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}