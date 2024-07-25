using LifeOne.Models.AdminManagement.ADAL;
using LifeOne.Models.AssociateManagement.AssociateEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AssociateManagement.AssociateDAL
{
    public class RequestForAssociateFranchiseDAL
    {
        public static List<RequestForAssociateFranchise> RequestForAssociateFranchiseDetails(long Fk_MemId)
        {
            try
            {
                string _qury = "RequestDetailsForAssociateFranchise @Fk_MemId=" + Fk_MemId + "";
                List<RequestForAssociateFranchise> _iresult = DBHelperDapper.DAGetDetailsInList<RequestForAssociateFranchise>(_qury).ToList();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ResponseforRequestForFranchise RequestForAssociateFranchise(string Type,long Fk_MemId,string Reamrk)
        {
            try
            {
                string _qury = "RequestForAssociateFranchise @Fk_MemId=" + Fk_MemId + ",@type= '"+Type+"',@Remark='"+Reamrk+"'";
                ResponseforRequestForFranchise _iresult = DBHelperDapper.DAGetDetailsInList<ResponseforRequestForFranchise>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}