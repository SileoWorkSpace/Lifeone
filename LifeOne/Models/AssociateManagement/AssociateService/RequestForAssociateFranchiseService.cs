using LifeOne.Models.AssociateManagement.AssociateDAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using LifeOne.Models.Common;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateService
{
    public class RequestForAssociateFranchiseService
    {

        public List<RequestForAssociateFranchise> RequestDetailsForAssociate(long Fk_MemId)
        {

           List<RequestForAssociateFranchise> result = new List<RequestForAssociateFranchise>();
            try
            {
                result = RequestForAssociateFranchiseDAL.RequestForAssociateFranchiseDetails(Fk_MemId);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public ResponseforRequestForFranchise RequestForAssociateFrnachise(string type,long Fk_MemId,string Reamrk)
        {

            ResponseforRequestForFranchise result = new ResponseforRequestForFranchise();
            try
            {
                result = RequestForAssociateFranchiseDAL.RequestForAssociateFranchise(type, Fk_MemId, Reamrk);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public MOrderModel AssociatePersonalDetails()
        {
            MOrderModel response = DALAssociateProfile.AssociatePersonalDetails(SessionManager.AssociateFk_MemId);
            return response;
        }

    }
}