using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateDAL
{
    public class DALAssociateProfile
    {

        public static AssociateProfile GetCustomerDetailsById(int? Fk_MemId)
        {
            try
            {
                string _qury = "UserDetailsByIdNew @Fk_MemId=" + Fk_MemId + "";
                AssociateProfile _iresult = DBHelperDapper.DAGetDetailsInList<AssociateProfile>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<AssChildDetails> GetChildDetailsById(int? Fk_MemId)
        {
            try
            {
                string _qury = "GetChildDetailsById @Fk_MemId=" + Fk_MemId + "";
                List<AssChildDetails> _iresult = DBHelperDapper.DAGetDetailsInList<AssChildDetails>(_qury).ToList();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static WelcomeLetter WelcomeLetter(long Fk_MemId)
        {
            try
            {
                string _qury = "Proc_WelcomeLetter @Fk_MemId=" + Fk_MemId + "";
                WelcomeLetter _iresult = DBHelperDapper.DAGetDetailsInList<WelcomeLetter>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static LifeOne.Models.Common.MOrderModel AssociatePersonalDetails(long AssociateId)
        {
            try
            {
                string _qury = "ProGetAssociateDetails @AssociateId=" + AssociateId + "";
                LifeOne.Models.Common.MOrderModel _iresult = DBHelperDapper.DAGetDetailsInList<LifeOne.Models.Common.MOrderModel>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}