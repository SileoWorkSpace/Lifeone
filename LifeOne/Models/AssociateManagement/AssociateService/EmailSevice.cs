using LifeOne.Models.AssociateManagement.AssociateDAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateService
{
    public class EmailSevice
    {
        public EmailResponse SaveEmail(string xmldata)
        {
            EmailResponse _objResponseModel = new EmailResponse();
            try
            {
                _objResponseModel = DALEmail.SaveEmail(xmldata);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }
    }
}