using LifeOne.Models.AssociateManagement.AssociateDAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateService
{
    public class AssociateProfileService
    {

        public static AssociateProfile InvokeCustomerDetailsById(int? Fk_MemId)
        {
            AssociateProfile obj = DALAssociateProfile.GetCustomerDetailsById(Fk_MemId);
            return obj;
        }
        public static List<AssChildDetails> InvokeChildDetailsById(int? Fk_MemId)
        {
            List<AssChildDetails> obj = DALAssociateProfile.GetChildDetailsById(Fk_MemId);
            return obj;
        }

        public static WelcomeLetter WelcomeLetter(long Fk_MemId)
        {
            WelcomeLetter obj = DALAssociateProfile.WelcomeLetter(Fk_MemId);
            return obj;
        }
        public LifeOne.Models.Common.MOrderModel AssociatePersonalDetails()
        {
            LifeOne.Models.Common.MOrderModel response = DALAssociateProfile.AssociatePersonalDetails(SessionManager.AssociateFk_MemId);
            return response;
        }        
    }
}