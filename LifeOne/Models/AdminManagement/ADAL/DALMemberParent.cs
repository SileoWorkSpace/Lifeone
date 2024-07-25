using LifeOne.Models.AdminManagement.AEntity;
using LifeOne.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeOne.Models.AdminManagement.ADAL
{
    public class DALMemberParent
    {
        public static MemberParent BindMemberParentDetailByLoginID(MemberParent obj)
        {
            try
            {
                string _qury = "Proc_MemberParent @LoginID=" + obj.LoginID + ", @ProcType=" + obj.PrdId + "";
                MemberParent _iresult = DBHelperDapper.DAGetDetailsInList<MemberParent>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MemberParent UpdateMemberParentDetail(MemberParent obj)
        {
            try
            {                
                string _qury = "Proc_MemberParent @LoginID=" + obj.LoginID + ", @ProcType=" + 2 + ",@FK_MemId=" + obj.FK_MemId + ",@New_SponsorId=" + obj.SponsorId + ",@CreatBy=" + SessionManager.Fk_MemId + "";
                MemberParent _iresult = DBHelperDapper.DAGetDetailsInList<MemberParent>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static MemberParent AddMiddleMemberByLoginID(MemberParent obj)
        {
            try
            {
                string _qury = "Proc_AddMiddleMemberParent @LoginID=" + obj.LoginID + ",@FK_MemId=" + obj.FK_MemId + ",@Old_SponsorId=" + obj.CuurSponserID + ",@New_SponsorId=" + obj.SponsorId + ",@CreatBy=" + SessionManager.Fk_MemId + "";
                MemberParent _iresult = DBHelperDapper.DAGetDetailsInList<MemberParent>(_qury).FirstOrDefault();
                return _iresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}