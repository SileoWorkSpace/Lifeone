using LifeOne.Models.AssociateManagement.AssociateDAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateService
{
    public class KYCService
    {

        public KYCResponse SaveKYC(KYCDetails _obj)
        {
            KYCResponse _objResponseModel = new KYCResponse();
            try
            {
                _objResponseModel = DALKYC.SaveKYCDetails(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

        public List<KYCDetails> GetKycInfo(KYCDetails _obj)
        {
            List<KYCDetails> _objResponseModel = new List<KYCDetails>();
            try
            {
                _objResponseModel = DALKYC.GetKYCInfo(_obj);
                return _objResponseModel;
            }
            catch (Exception ex)
            {
                return _objResponseModel;
            }
        }

    }
}
